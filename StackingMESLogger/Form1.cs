using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StackingMESLogger
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private bool prevDI1State = false;
        private bool isConnected = false;
        private bool isMonitoring = false;

        // 선택된 모델 데이터
        private string selectedModelName = "";
        private int selectedBarcodeCount = 1;
        private string selectedBarcodeFormat = "";
        private int selectedBarcodeLength = 1;

        // 동적 바코드 텍스트박스 리스트
        private List<TextBox> barcodeTextBoxes = new List<TextBox>();

        public Form1()
        {
            InitializeComponent();
            LoadSettings();
            RefreshCOMPorts();
            InitializeModelList();
            UpdateBarcodeTextBoxes();
        }

        // --------------------------------------------------------------------
        // SETTINGS
        // --------------------------------------------------------------------
        private void LoadSettings()
        {
            cmbCOM.Text = Properties.Settings.Default.LastCOM;
            txtLogPath.Text = Properties.Settings.Default.LogPath;

            txtStackName.Text = Properties.Settings.Default.LastMachine ?? "";
            txtManager.Text = Properties.Settings.Default.LastUser ?? "";

            selectedModelName = Properties.Settings.Default.LastModelName ?? "No Model";
            selectedBarcodeCount = Properties.Settings.Default.LastBarcodeQty > 0 ? Properties.Settings.Default.LastBarcodeQty : 1;
            selectedBarcodeFormat = Properties.Settings.Default.LastBarcodeFormat ?? "";
            selectedBarcodeLength = Properties.Settings.Default.LastBarcodeLength > 0 ? Properties.Settings.Default.LastBarcodeLength : 1;
            txtLogCopyPath.Text = Properties.Settings.Default.LogCopyPath;

        }

        private void SaveSettings()
        {
            Properties.Settings.Default.LastCOM = cmbCOM.Text;
            Properties.Settings.Default.LogPath = txtLogPath.Text;

            Properties.Settings.Default.LastMachine = txtStackName.Text;
            Properties.Settings.Default.LastUser = txtManager.Text;

            Properties.Settings.Default.LastModelName = selectedModelName;
            Properties.Settings.Default.LastBarcodeQty = selectedBarcodeCount;
            Properties.Settings.Default.LastBarcodeFormat = selectedBarcodeFormat;
            Properties.Settings.Default.LastBarcodeLength = selectedBarcodeLength;
            Properties.Settings.Default.LogCopyPath = txtLogCopyPath.Text;


            Properties.Settings.Default.Save();

        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
            MessageBox.Show("Settings saved.");
        }

        // --------------------------------------------------------------------
        // COM PORT
        // --------------------------------------------------------------------
        private void RefreshCOMPorts()
        {
            cmbCOM.Items.Clear();
            cmbCOM.Items.AddRange(SerialPort.GetPortNames());

            string lastCom = Properties.Settings.Default.LastCOM;
            if (!string.IsNullOrEmpty(lastCom) && cmbCOM.Items.Contains(lastCom))
                cmbCOM.Text = lastCom;
            else if (cmbCOM.Items.Count > 0)
                cmbCOM.SelectedIndex = 0;
        }

        private void btnRefreshCOM_Click(object sender, EventArgs e)
        {
            RefreshCOMPorts();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbCOM.Text))
            {
                MessageBox.Show("CHECK COM PORT.");
                return;
            }

            try
            {
                serialPort = new SerialPort(cmbCOM.Text, 9600, Parity.None, 8, StopBits.One);
                serialPort.Open();

                isConnected = true;
                lblStatusCOM.Text = "COM Port Connected";
                Console.WriteLine($"[INFO] COM Port Connected: {cmbCOM.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"COM connection failed: {ex.Message}");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                isMonitoring = false;
                isConnected = false;

                if (serialPort != null && serialPort.IsOpen)
                    serialPort.Close();

                lblStatusCOM.Text = "COM Port Disconnected";
                Console.WriteLine("[INFO] COM Port Disconnected");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"COM disconnect failed: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // START / STOP MONITORING
        // --------------------------------------------------------------------
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("COM port not connected.");
                return;
            }

            if (selectedBarcodeLength <= 0)
            {
                MessageBox.Show("Selected model's barcode settings are invalid.");
                return;
            }
            barcodeTextBoxes[0].Focus();
            isMonitoring = true;
            prevDI1State = false;
            Task.Run(() => MonitorDI1());

            // 상태 표시
            lblProgramStatus.Text = "RUN";
            lblProgramStatus.ForeColor = Color.Green;

            Console.WriteLine("[INFO] DI1 Monitoring Started");
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            isMonitoring = false;

            // 상태 표시
            lblProgramStatus.Text = "STOP";
            lblProgramStatus.ForeColor = Color.Red;

            Console.WriteLine("[INFO] DI1 Monitoring Stopped");
        }
        // --------------------------------------------------------------------
        // DI1 Monitor Loop
        // --------------------------------------------------------------------
        private void MonitorDI1()
        {
            while (isMonitoring && isConnected)
            {
                bool di1 = false;

                try
                {
                    di1 = ReadDI1();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ERROR] DI1 Monitor: " + ex.Message);
                }

                this.Invoke((MethodInvoker)delegate
                {
                    // DI 상태 라벨 갱신
                    UpdateDIStatusLabel(di1);

                    // DI1 OFF → ON 엣지
                    // DI1 OFF → ON 엣지
                    if (!prevDI1State && di1)
                    {
                        Console.WriteLine("[EDGE] DI1 OFF→ON detected");

                        // 모든 바코드가 유효한 경우에만 로그 생성
                        if (barcodeTextBoxes.All(t => ValidateSingleBarcode(t.Text)))
                        {
                            foreach (var t in barcodeTextBoxes)
                            {
                                CreateLogFile(t.Text);
                            }

                            // DO1 OFF
                            TurnOffDO1();

                            // 바코드 초기화 및 색상 흰색으로 리셋
                            foreach (var t in barcodeTextBoxes)
                            {
                                t.Text = "";
                                ResetTextboxColor(t); // ForeColor/BackColor 초기화
                            }

                            // 첫 번째 바코드 박스로 포커스 이동
                            if (barcodeTextBoxes.Count > 0)
                                barcodeTextBoxes[0].Focus();
                        }
                        else
                        {
                            Console.WriteLine("[WARN] DI1 ON detected but barcode invalid. Log not created.");
                            MessageBox.Show("Barcode invalid! Log not created.");
                        }
                    }


                    prevDI1State = di1;
                });

                Task.Delay(100).Wait();
            }
        }







        // --------------------------------------------------------------------
        // 바코드 텍스트박스 동적 관리
        // --------------------------------------------------------------------
        private void UpdateBarcodeTextBoxes()
        {
            // 기존 TextBox 제거
            foreach (var tb in barcodeTextBoxes)
            {
                this.Controls.Remove(tb);
                tb.Dispose();
            }
            barcodeTextBoxes.Clear();

            for (int i = 0; i < selectedBarcodeCount; i++)
            {
                TextBox tb = new TextBox
                {
                    Name = $"txtBarcode{i}",
                    Width = 200,
                    Height = 25,
                    Location = new Point(88, 400 + i * 30),
                    ForeColor = Color.Black,
                    Text = ""
                };

                tb.GotFocus += (s, e) =>
                {
                    if (tb.ForeColor == Color.Gray)
                    {
                        tb.Text = "";
                        tb.ForeColor = Color.Black;
                    }
                };

                // ★ KeyDown에서 엔터 감지만
                tb.KeyDown += Barcode_KeyDown;

                barcodeTextBoxes.Add(tb);
                this.Controls.Add(tb);
            }
        }


        // --------------------------------------------------------------------
        // 바코드 입력 검증
        // --------------------------------------------------------------------
        private bool waitingDI1Trigger = false;   // 바코드 완료 후 DI1 트리거 대기 상태
        private bool isInternalChange = false;

        // --------------------------------------------------------------------
        // 바코드 입력 정제 (TextChanged)
        private void BarcodeTextChanged(object sender, EventArgs e)
        {
            if (isInternalChange) return;

            TextBox tb = sender as TextBox;
            if (tb == null) return;

            string original = tb.Text;

            // 1) 제어문자/엔터/공백 제거
            string raw = new string(original
                .Where(ch => !char.IsControl(ch) && !char.IsWhiteSpace(ch))
                .ToArray());

            // 2) 숫자/알파벳만 허용
            raw = new string(raw.Where(ch => char.IsLetterOrDigit(ch)).ToArray());

            // 3) 텍스트 정제 후 반영
            if (original != raw)
            {
                isInternalChange = true;
                tb.Text = raw;
                tb.SelectionStart = tb.Text.Length;
                tb.SelectionLength = 0;
                isInternalChange = false;
            }

            // 4) 빈 문자열이면 중단
            if (raw.Length == 0)
                return;
        }






        private bool ValidateSingleBarcode(string barcode)
        {
            if (barcode.Length != selectedBarcodeLength) return false;

            for (int i = 0; i < selectedBarcodeLength; i++)
            {
                if (i >= selectedBarcodeFormat.Length) return true; // format 짧으면 무시
                if (selectedBarcodeFormat[i] == '*') continue;
                if (selectedBarcodeFormat[i] != barcode[i]) return false;
            }
            return true;
        }


        private bool AreAllBarcodesValid()
        {
            foreach (var tb in barcodeTextBoxes)
            {
                if (tb.Text.Length != selectedBarcodeLength) return false;
                if (!ValidateSingleBarcode(tb.Text)) return false;
            }
            return true;
        }
        // --------------------------------------------------------------------
        // Log File 생성
        // --------------------------------------------------------------------
        private void CreateLogFile(string barcode)
        {
            // 원본 로그 폴더 체크 및 생성
            if (string.IsNullOrEmpty(txtLogPath.Text))
            {
                MessageBox.Show("Log path is empty.");
                return;
            }

            if (!Directory.Exists(txtLogPath.Text))
            {
                try
                {
                    Directory.CreateDirectory(txtLogPath.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot create log directory: " + ex.Message);
                    return;
                }
            }

            string timestamp = DateTime.Now.ToString("yyMMddHHmmss");
            string fileName = $"#1_{timestamp}_{barcode}.txt";
            string originFile = Path.Combine(txtLogPath.Text, fileName);

            string stackName = txtStackName.Text.Trim();
            string manager = txtManager.Text.Trim();
            if (string.IsNullOrWhiteSpace(stackName)) stackName = "STACK-1";
            if (string.IsNullOrWhiteSpace(manager)) manager = "manager";

            string content = $"ATE,L,{stackName},STACK,{barcode},1,1,{timestamp},{manager}";

            try
            {
                File.WriteAllText(originFile, content);
                Console.WriteLine($"[Log Created] {originFile}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save log: " + ex.Message);
                return;
            }

            // LogCopy 기능
            if (!string.IsNullOrEmpty(txtLogCopyPath.Text))
            {
                string modelFolder = Path.Combine(txtLogCopyPath.Text, selectedModelName);
                string dateFolder = Path.Combine(modelFolder, DateTime.Now.ToString("yyyy-MM-dd"));
                try
                {
                    Directory.CreateDirectory(dateFolder); // 없으면 생성
                    string copyFile = Path.Combine(dateFolder, fileName);
                    File.WriteAllText(copyFile, content);
                    Console.WriteLine($"[Log Copied] {copyFile}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to copy log: " + ex.Message);
                }
            }
        }



        // --------------------------------------------------------------------
        // DO1 출력 제어
        // --------------------------------------------------------------------
        private bool currentDO1State = false; // DO1 ON/OFF 상태 저장

        private void PulseDO1()
        {
            if (serialPort == null || !serialPort.IsOpen) return;

            try
            {
                // 이미 켜져 있으면 재시도 없이 바로 상태 기록
                if (ReadDO2Status())
                {
                    currentDO1State = true;
                    UpdateDOStatusLabel();
                    Console.WriteLine("[INFO] DO2 already ON, no command sent.");
                    return;
                }

                byte[] frameOn = { 0x01, 0x05, 0x00, 0x01, 0xFF, 0x00, 0xDD, 0xFA };
                bool isOn = false;
                int retryCount = 0;

                // 최대 5회 재시도
                while (!isOn && retryCount < 5)
                {
                    serialPort.Write(frameOn, 0, frameOn.Length);
                    Console.WriteLine("[DO2] ON command sent");

                    Task.Delay(50).Wait(); // 잠시 대기 후 상태 확인

                    isOn = ReadDO2Status(); // 실제 DO2 상태 확인

                    if (!isOn)
                    {
                        retryCount++;
                        Console.WriteLine($"[WARN] DO2 not ON, retrying... ({retryCount})");
                    }
                }

                currentDO1State = isOn;
                UpdateDOStatusLabel();

                if (isOn)
                    Console.WriteLine("[INFO] DO2 is ON");
                else
                    Console.WriteLine("[ERROR] DO2 failed to turn ON after retries");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] PulseDO1: " + ex.Message);
            }
        }


        // DO2 실제 상태 확인
        private bool ReadDO2Status()
        {
            if (serialPort == null || !serialPort.IsOpen) return false;

            byte[] readFrame = { 0x01, 0x01, 0x00, 0x00, 0x00, 0x08, 0x3D, 0xCC };
            serialPort.DiscardInBuffer();
            serialPort.Write(readFrame, 0, readFrame.Length);

            Task.Delay(50).Wait(); // 장치 응답 대기

            int size = serialPort.BytesToRead;
            if (size < 6) return false;

            byte[] recv = new byte[size];
            serialPort.Read(recv, 0, size);

            // 정상 응답 체크
            if (recv[0] != 0x01 || recv[1] != 0x01 || recv[2] != 1) return false;

            byte status = recv[3]; // 출력 상태
            return (status & 0x02) != 0; // Bit1 = DO2
        }


        private void TurnOffDO1()
        {
            if (serialPort == null || !serialPort.IsOpen) return;

            try
            {
                //DO2
                byte[] frameOff = { 0x01, 0x05, 0x00, 0x01, 0x00, 0x00, 0x9C, 0x0A };
                serialPort.Write(frameOff, 0, frameOff.Length);
                currentDO1State = false; // 상태 기록
                UpdateDOStatusLabel();

                Console.WriteLine("[DO2] OFF");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] WriteDO2 OFF: " + ex.Message);
            }
        }

        private ushort CRC16(byte[] data, int length)
        {
            ushort crc = 0xFFFF;
            for (int pos = 0; pos < length; pos++)
            {
                crc ^= data[pos];
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 1) != 0)
                        crc = (ushort)((crc >> 1) ^ 0xA001);
                    else
                        crc >>= 1;
                }
            }
            return crc;
        }

        private bool ReadDI1()
        {
            if (serialPort == null || !serialPort.IsOpen) throw new Exception("COM Not open");

            byte[] frame = { 0x01, 0x02, 0x00, 0x00, 0x00, 0x08, 0x79, 0xCC };
            serialPort.DiscardInBuffer();
            serialPort.Write(frame, 0, frame.Length);

            Task.Delay(50).Wait();

            int size = serialPort.BytesToRead;
            if (size < 6) return prevDI1State;

            byte[] recv = new byte[size];
            serialPort.Read(recv, 0, size);

            if (recv[0] != 0x01 || recv[1] != 0x02) return prevDI1State;

            byte data = recv[3];
            return (data & 0x01) != 0;
        }

        // --------------------------------------------------------------------
        // 모델 JSON 파일 경로
        // --------------------------------------------------------------------
        private string GetModelJsonPath()
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(exePath, "models.json");
        }

        // --------------------------------------------------------------------
        // 모델 저장 / 로드
        // --------------------------------------------------------------------
        private void SaveModelToJson(string modelName, int qty, string format, int length)
        {
            string path = GetModelJsonPath();
            List<ModelData> models;

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                models = JsonConvert.DeserializeObject<List<ModelData>>(json) ?? new List<ModelData>();
            }
            else
            {
                models = new List<ModelData>();
            }

            // 기존에 같은 이름 있으면 업데이트
            var existing = models.Find(m => m.Name.Equals(modelName, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                existing.BarcodeCount = qty;
                existing.BarcodeFormat = format;
                existing.BarcodeLength = length;
            }
            else
            {
                models.Add(new ModelData
                {
                    Name = modelName,
                    BarcodeCount = qty,
                    BarcodeFormat = format,
                    BarcodeLength = length
                });
            }

            string outJson = JsonConvert.SerializeObject(models, Formatting.Indented);
            File.WriteAllText(path, outJson);

            Console.WriteLine($"[INFO] Model '{modelName}' saved to JSON.");
        }


        private ModelData LoadModelFromJson(string modelName)
        {
            string path = GetModelJsonPath();
            if (!File.Exists(path)) return null;

            string json = File.ReadAllText(path);
            var models = JsonConvert.DeserializeObject<List<ModelData>>(json);
            if (models == null) return null;

            return models.Find(m => m.Name.Equals(modelName, StringComparison.OrdinalIgnoreCase));
        }


        public class ModelData
        {
            public string Name { get; set; } = "No Model";
            public int BarcodeCount { get; set; } = 1;
            public string BarcodeFormat { get; set; } = "";
            public int BarcodeLength { get; set; } = 1;
        }

        // --------------------------------------------------------------------
        // 모델 리스트 초기화 및 JSON 반영
        // --------------------------------------------------------------------
        private void InitializeModelList()
        {
            cmbModelList.Items.Clear();

            string path = GetModelJsonPath();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var models = JsonConvert.DeserializeObject<List<ModelData>>(json);
                if (models != null)
                {
                    foreach (var m in models)
                        cmbModelList.Items.Add(m.Name);
                }
            }

            if (cmbModelList.Items.Count == 0)
                cmbModelList.Items.Add("No Model");

            cmbModelList.SelectedIndex = 0;
            selectedModelName = cmbModelList.Text;
        }


        // --------------------------------------------------------------------
        // 모델 불러오기 버튼
        // --------------------------------------------------------------------
        private void btnLoadModel_Click(object sender, EventArgs e)
        {
            string modelName = cmbModelList.SelectedItem?.ToString() ?? "No Model";
            selectedModelName = modelName;

            if (modelName == "No Model")
            {
                selectedBarcodeCount = 1;
                selectedBarcodeFormat = "";
                selectedBarcodeLength = 1;
            }
            else
            {
                ModelData data = LoadModelFromJson(modelName);
                if (data != null)
                {
                    selectedBarcodeCount = data.BarcodeCount;
                    selectedBarcodeFormat = data.BarcodeFormat;
                    selectedBarcodeLength = data.BarcodeLength;
                }
            }

            UpdateBarcodeTextBoxes();

            // 메시지 박스 띄운 뒤 첫 바코드 텍스트박스에 포커스
            MessageBox.Show($"Model '{selectedModelName}' loaded.");

            if (barcodeTextBoxes.Count > 0)
                barcodeTextBoxes[0].Focus();
        }


        // --------------------------------------------------------------------
        // 모델 세팅폼 열기 버튼
        // --------------------------------------------------------------------
        private void btnOpenModelSetting_Click(object sender, EventArgs e)
        {
            using (ModelSettingForm dlg = new ModelSettingForm())
            {
                dlg.ModelName = selectedModelName;
                dlg.ModelBarcodeCount = selectedBarcodeCount;
                dlg.ModelBarcodeFormat = selectedBarcodeFormat;
                dlg.ModelBarcodeLength = selectedBarcodeLength;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string modelName = dlg.ModelName?.Trim();

                    if (string.IsNullOrEmpty(modelName))
                    {
                        MessageBox.Show("Please Input Model Name");
                        return;
                    }

                    selectedModelName = modelName;
                    selectedBarcodeCount = Math.Max(dlg.ModelBarcodeCount, 1);
                    selectedBarcodeFormat = dlg.ModelBarcodeFormat ?? "";
                    selectedBarcodeLength = Math.Max(dlg.ModelBarcodeLength, 1);

                    // "No Model"은 저장하지 않음
                    if (selectedModelName != "No Model")
                        SaveModelToJson(selectedModelName, selectedBarcodeCount, selectedBarcodeFormat, selectedBarcodeLength);

                    // ComboBox 갱신
                    RefreshModelList();

                    // 바코드 텍스트박스 갱신
                    UpdateBarcodeTextBoxes();

                    // ★ 첫 번째 바코드 텍스트박스로 포커스
                    if (barcodeTextBoxes.Count > 0)
                        barcodeTextBoxes[0].Focus();

                    MessageBox.Show($"Model '{selectedModelName}' saved and loaded.");
                }
            }
        }


        private void btnBrowseLogCopy_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtLogCopyPath.Text = dialog.SelectedPath;

                    // 자동 저장
                    Properties.Settings.Default.LogCopyPath = txtLogCopyPath.Text;
                    Properties.Settings.Default.Save();

                    MessageBox.Show("Log Copy directory saved.");
                }
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAllBarcodes();
        }


        private void ResetAllBarcodes()
        {
            if (barcodeTextBoxes == null || barcodeTextBoxes.Count == 0)
                return;

            foreach (var tb in barcodeTextBoxes)
            {
                tb.Text = "";
                ResetTextboxColor(tb);
            }

            // ★ 첫 박스 포커스 이동 (Invoke로 안전하게)
            if (barcodeTextBoxes.Count > 0)
            {
                this.Invoke((MethodInvoker)(() => barcodeTextBoxes[0].Focus()));
            }

            // DO1 OFF
            TurnOffDO1();

            Console.WriteLine("[INFO] Barcode fields reset and DO1 turned OFF.");
        }




        // 모델 리스트 ComboBox 갱신
        private void RefreshModelList()
        {
            string prevSelected = cmbModelList.Text;
            cmbModelList.Items.Clear();

            string path = GetModelJsonPath();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var models = JsonConvert.DeserializeObject<List<ModelData>>(json);
                if (models != null)
                {
                    foreach (var m in models)
                        cmbModelList.Items.Add(m.Name);
                }
            }

            if (cmbModelList.Items.Count == 0)
                cmbModelList.Items.Add("No Model");

            if (cmbModelList.Items.Contains(prevSelected))
                cmbModelList.Text = prevSelected;
            else
                cmbModelList.SelectedIndex = 0;

            selectedModelName = cmbModelList.Text;
        }



        private void btnBrowsePath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select Log Folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtLogPath.Text = dlg.SelectedPath;
                MessageBox.Show("Log directory saved.");
            }
        }
        private void btnEditModel_Click(object sender, EventArgs e)
        {
            using (var editForm = new ModelEditForm())
            {
                // 선택 모델 이름 전달
                editForm.ModelName = selectedModelName;

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // 모델 추가/수정/삭제 후 ComboBox 즉시 갱신
                    RefreshModelList();

                    // 삭제된 모델일 경우 선택을 No Model로 초기화
                    if (!cmbModelList.Items.Contains(selectedModelName))
                    {
                        cmbModelList.SelectedIndex = 0;
                        selectedModelName = cmbModelList.Text;

                        selectedBarcodeCount = 1;
                        selectedBarcodeFormat = "";
                        selectedBarcodeLength = 1;

                        UpdateBarcodeTextBoxes();
                    }
                    else
                    {
                        // 남아있는 모델이면 선택 유지
                        cmbModelList.SelectedItem = selectedModelName;
                        var data = LoadModelFromJson(selectedModelName);
                        if (data != null)
                        {
                            selectedBarcodeCount = data.BarcodeCount;
                            selectedBarcodeFormat = data.BarcodeFormat;
                            selectedBarcodeLength = data.BarcodeLength;

                            UpdateBarcodeTextBoxes();
                        }
                    }

                    // ★ 여기서 첫 번째 바코드 텍스트박스로 포커스
                    if (barcodeTextBoxes.Count > 0)
                        barcodeTextBoxes[0].Focus();
                }
            }
        }

        private void UpdateDIStatusLabel(bool diState)
        {
            if (lblStatusDI.InvokeRequired)
            {
                lblStatusDI.Invoke((MethodInvoker)(() => UpdateDIStatusLabel(diState)));
                return;
            }

            lblStatusDI.Text = diState ? "DI: ON" : "DI: OFF";
            lblStatusDI.ForeColor = diState ? Color.Green : Color.Red;
        }

        private void UpdateDOStatusLabel()
        {
            if (lblStatusDO.InvokeRequired)
            {
                lblStatusDO.Invoke((MethodInvoker)(() => UpdateDOStatusLabel()));
                return;
            }

           lblStatusDO.Text = currentDO1State ? "DO : ON" : "DO : OFF";
           lblStatusDO.ForeColor = currentDO1State ? Color.Green : Color.Red;
        }





        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeModelList();
            UpdateBarcodeTextBoxes();

            foreach (var tb in barcodeTextBoxes)
            {
                tb.KeyPress += Barcode_KeyPress_Filter;
                tb.KeyDown += Barcode_KeyDown;
            }

            // ★ 폼 로드시 첫 번째 바코드 텍스트박스로 포커스 이동
            if (barcodeTextBoxes.Count > 0)
                barcodeTextBoxes[0].Focus();
        }


        // ------------------------------
        // KeyPress: 문자 입력만 허용
        // ------------------------------
        private void Barcode_KeyPress_Filter(object sender, KeyPressEventArgs e)
        {
            // 엔터(\r, \n), 탭, 스페이스는 KeyDown에서 처리하므로 여기서는 무시
            if (!char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // ------------------------------
        // KeyDown: 엔터 처리 + 바코드 완료
        // ------------------------------
        private void Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                TextBox tb = sender as TextBox;
                HandleBarcodeCompleted(tb);
            }
        }


  

        // 클래스 멤버에 추가
        private bool isHandlingBarcode = false;

        // HandleBarcodeCompleted 수정
        // --------------------------------------------------------------------
        // 바코드 완료 처리 및 검증 (Enter 또는 KeyDown에서 호출)
        private void HandleBarcodeCompleted(TextBox tb)
        {
            if (isHandlingBarcode) return;
            isHandlingBarcode = true;

            try
            {
                if (!isMonitoring)
                {
                    ShowErrorAndReset(tb, "Start Program First");
                    return;
                }

                string data = tb.Text.Trim();
                int index = barcodeTextBoxes.IndexOf(tb);

                // 길이 오류
                if (data.Length != selectedBarcodeLength)
                {
                    ShowErrorAndReset(tb, "Wrong Barcode Length!");
                    return;
                }

                // 형식 오류
                if (!ValidateSingleBarcode(data))
                {
                    ShowErrorAndReset(tb, "Wrong Barcode Format!");
                    return;
                }

                // 중복 바코드 처리
                if (barcodeTextBoxes.Where(t => t != tb).Any(t => t.Text.Trim() == data))
                {
                    isInternalChange = true;

                    // 모든 TextBox 초기화
                    foreach (var t in barcodeTextBoxes)
                    {
                        t.Text = "";
                        ResetTextboxColor(t);
                    }

                    TurnOffDO1();

                    // 메시지박스 후 첫 TextBox 포커스 이동
                    this.BeginInvoke((MethodInvoker)(() =>
                    {
                        MessageBox.Show("Duplicate Barcode Detected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (barcodeTextBoxes.Count > 0)
                            barcodeTextBoxes[0].Focus();
                    }));

                    isInternalChange = false;
                    return;
                }

                // 정상 바코드 표시
                tb.BackColor = Color.LightGreen;
                tb.ForeColor = Color.Black;

                // 다음 칸으로 이동
                if (index + 1 < barcodeTextBoxes.Count)
                {
                    barcodeTextBoxes[index + 1].Focus();
                }

                // 모든 바코드가 정상일 때 DO1 펄스
                if (barcodeTextBoxes.All(t => ValidateSingleBarcode(t.Text)))
                {
                    PulseDO1();
                }
            }
            finally
            {
                isHandlingBarcode = false;
            }
        }

        // 공통 메시지 + 초기화 함수
        private void ShowErrorAndReset(TextBox tb, string message)
        {
            // 텍스트 초기화 및 색상
            isInternalChange = true;
            tb.Text = "";
            ResetTextboxColor(tb);
            TurnOffDO1();
            isInternalChange = false;

            // 메시지박스 및 포커스
            this.BeginInvoke((MethodInvoker)(() =>
            {
                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
            }));
        }

        // TextBox 색상 초기화
        private void ResetTextboxColor(TextBox tb)
        {
            tb.BackColor = Color.White;
            tb.ForeColor = Color.Black;
        }




    }
}
