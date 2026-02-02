using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace StackingMESLogger
{
    public partial class ModelEditForm : Form
    {
        private const string JsonFilePath = "models.json";

        // Form1에서 선택된 모델 전달받는 속성
        public string ModelName { get; set; } = "No Model";

        public List<ModelData> ModelList { get; private set; } = new List<ModelData>();

        public ModelEditForm()
        {
            InitializeComponent();
            LoadModelsFromJson();
            LoadModelsToComboBox();
        }

        // -----------------------------------------------------------
        // JSON 로드
        // -----------------------------------------------------------
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

        // -----------------------------------------------------------
        // 콤보박스에 모델 목록 표시
        // -----------------------------------------------------------
        private void LoadModelsToComboBox()
        {
            cmbModelList.Items.Clear();
            foreach (var m in ModelList)
                cmbModelList.Items.Add(m.Name);

            // 전달받은 ModelName과 일치하는 모델 자동 선택
            int index = cmbModelList.Items.IndexOf(ModelName);
            cmbModelList.SelectedIndex = index >= 0 ? index : 0;
        }

        // -----------------------------------------------------------
        // 모델 선택 시 해당 정보 표시
        // -----------------------------------------------------------
        private void cmbModelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cmbModelList.SelectedItem?.ToString();
            var model = ModelList.Find(m => m.Name == selected);
            if (model != null)
            {
                txtModelName.Text = model.Name;
                nudBarcodeQty.Value = model.BarcodeCount;
                txtBarcodeFormat.Text = model.BarcodeFormat;
                nudBarcodeLength.Value = model.BarcodeLength;
            }
        }

        // -----------------------------------------------------------
        // 모델 수정 저장
        // -----------------------------------------------------------
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string name = txtModelName.Text.Trim();
            int qty = (int)nudBarcodeQty.Value;
            string format = txtBarcodeFormat.Text.Trim();
            int length = (int)nudBarcodeLength.Value;

            var existing = ModelList.Find(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                existing.BarcodeCount = qty;
                existing.BarcodeFormat = format;
                existing.BarcodeLength = length;
            }

            SaveModels();
            MessageBox.Show("Model Saved.");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // -----------------------------------------------------------
        // 모델 삭제
        // -----------------------------------------------------------
        private void btnDeleteModel_Click(object sender, EventArgs e)
        {
            string selected = cmbModelList.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selected)) return;

            var model = ModelList.Find(m => m.Name == selected);
            if (model == null) return;

            if (MessageBox.Show($"Delete '{selected}'Model?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            ModelList.Remove(model);
            SaveModels();
            MessageBox.Show("Model Deleted.");

            this.DialogResult = DialogResult.OK;
            this.Close();   // 삭제 후 폼 종료 → Form1에서 콤보박스 새로고침
        }

        // -----------------------------------------------------------
        // JSON 저장 메서드
        // -----------------------------------------------------------
        private void SaveModels()
        {
            File.WriteAllText(JsonFilePath,
                JsonConvert.SerializeObject(ModelList, Formatting.Indented));
        }

        // -----------------------------------------------------------
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
