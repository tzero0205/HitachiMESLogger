using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace HitachiMESLogger

{
    public partial class ModelSettingForm : Form
    {
        private const string JsonFilePath = "models.json";

        public string ModelName { get; set; } = "No Model";
        public int ModelBarcodeCount { get; set; } = 1;
        public string ModelBarcodeFormat { get; set; } = "";
        public int ModelBarcodeLength { get; set; } = 1;

        public List<ModelData> ModelList { get; private set; } = new List<ModelData>();

        public ModelSettingForm()
        {
            InitializeComponent();
            LoadModelsFromJson();
            LoadSettingsToControls();
        }

        private void LoadModelsFromJson()
        {
            if (File.Exists(JsonFilePath))
            {
                try
                {
                    string json = File.ReadAllText(JsonFilePath);
                    ModelList = JsonConvert.DeserializeObject<List<ModelData>>(json) ?? new List<ModelData>();
                }
                catch
                {
                    ModelList = new List<ModelData>();
                }
            }
        }

        private void LoadSettingsToControls()
        {
            if (ModelList.Count > 0)
            {
                var model = ModelList.Find(m => m.Name.Equals(ModelName, StringComparison.OrdinalIgnoreCase)) ?? ModelList[0];

                txtModelName.Text = model.Name;
                nudBarcodeQty.Value = model.BarcodeCount;
                txtBarcodeFormat.Text = model.BarcodeFormat;
                nudBarcodeLength.Value = model.BarcodeLength;
            }
            else
            {
                txtModelName.Text = "No Model";
                nudBarcodeQty.Value = 1;
                txtBarcodeFormat.Text = "";
                nudBarcodeLength.Value = 1;
            }
        }

        // --------------------------------------------------------------------
        // 저장 / 수정
        // --------------------------------------------------------------------
        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            SaveOrUpdateModel();
        }

        private void SaveOrUpdateModel()
        {
            string newModelName = txtModelName.Text.Trim();
            int newCount = (int)nudBarcodeQty.Value;
            string newFormat = txtBarcodeFormat.Text.Trim();
            int newLength = (int)nudBarcodeLength.Value;

            if (string.IsNullOrEmpty(newModelName))
            {
                MessageBox.Show("Input Model Name!");
                return;
            }

            ModelName = newModelName;
            ModelBarcodeCount = newCount;
            ModelBarcodeFormat = newFormat;
            ModelBarcodeLength = newLength;

            var existing = ModelList.Find(m => m.Name.Equals(newModelName, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                // 기존 모델 수정
                existing.BarcodeCount = newCount;
                existing.BarcodeFormat = newFormat;
                existing.BarcodeLength = newLength;
            }
            else
            {
                // 새 모델 추가
                ModelList.Add(new ModelData
                {
                    Name = newModelName,
                    BarcodeCount = newCount,
                    BarcodeFormat = newFormat,
                    BarcodeLength = newLength
                });
            }

            SaveModelsToJson();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        

        // --------------------------------------------------------------------
        // JSON 저장
        // --------------------------------------------------------------------
        private void SaveModelsToJson()
        {
            try
            {
                string json = JsonConvert.SerializeObject(ModelList, Formatting.Indented);
                File.WriteAllText(JsonFilePath, json);
                Console.WriteLine("[INFO] Models JSON updated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Model Save FAIL!: " + ex.Message);
            }
        }

        // 취소 버튼
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    public class ModelData
    {
        public string Name { get; set; } = "No Model";
        public int BarcodeCount { get; set; } = 1;
        public string BarcodeFormat { get; set; } = "";
        public int BarcodeLength { get; set; } = 1;
    }
}
