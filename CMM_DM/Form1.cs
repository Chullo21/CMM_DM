using System.Reflection;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CMM_DM
{
    public partial class Form1 : Form
    {
        static List<CMMData> cmmDataList = new List<CMMData>();
        static string? tempFilePath;

        private class CMMData
        {
            public string? ItemNo { get; set; }
            public string? MinTol { get; set; }
            public string? MaxTol { get; set; }
            public string? Actual { get; set; }
            public string? Nominal { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
            dataDgv.ReadOnly = false;
            cmmCountTxt.Text = "0";
        }

        private void getDirBtn_Click(object sender, EventArgs e)
        {
            directoryTxt.Text = OpenDiag();

            if (!string.IsNullOrEmpty(directoryTxt.Text))
            {
                automateBtn.Enabled = true;
            }
        }

        private void automateBtn_Click(object sender, EventArgs e)
        {
            cmmDataList.Clear();
            dataDgv.Rows.Clear();

            int startRow = 26;
            int lowerRow = 24;
            int nomRow = 19;

            using (ExcelPackage package = new(directoryTxt.Text))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                do
                {
                    //CMMData data = new();
                    string? itemNo = "";
                    string? primaryNum = worksheet.Cells[startRow, 2].Value?.ToString();

                    if (!string.IsNullOrEmpty(primaryNum) && primaryNum.StartsWith('#'))
                    {
                        itemNo = primaryNum;
                    }

                    int currentRow = startRow + 1;

                    do
                    {
                        string? ValChecker = worksheet.Cells[currentRow, 26].Value?.ToString();
                        if (string.IsNullOrEmpty(ValChecker) || ValChecker == "Actual")
                        {
                            currentRow++;
                            lowerRow = 25;
                            nomRow = 18;
                        }
                        else
                        {
                            break;
                        }
                    } while (true);

                    do
                    {
                        string? lower = worksheet.Cells[currentRow, lowerRow].Value?.ToString();
                        string? upper = worksheet.Cells[currentRow, 20].Value.ToString();
                        string? actual = worksheet.Cells[currentRow, 26].Value?.ToString();
                        string? nominal = worksheet.Cells[currentRow, nomRow].Value?.ToString();

                        //cmmDataList.Add(data);
                        dataDgv.Rows.Add(itemNo, nominal, upper, lower, actual);

                        if (worksheet.Cells[currentRow + 1, 2].Value == null && worksheet.Cells[currentRow + 1, 26].Value != null && !worksheet.Cells[currentRow + 1, 2].Merge)
                        {
                            currentRow++;
                        }
                        else
                        {
                            startRow = currentRow;
                            break;
                        }

                    } while (true);

                    string? endChecker = worksheet.Cells[startRow + 1, 2].Value?.ToString();
                    if (string.IsNullOrEmpty(endChecker))
                    {
                        break;
                    }
                    else
                    {
                        startRow++;
                    }

                } while (true);
            }

            if (!string.IsNullOrEmpty(iqaDir.Text))
            {
                SaveDataBtn.Enabled = true;
            }

            cmmCountTxt.Text = (CmmCountFunct() + 1).ToString();
            getDirBtn.Enabled = false;
            directoryTxt.Clear();
            automateBtn.Enabled = false;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            dataDgv.Rows.Clear();
            directoryTxt.Clear();
            cmmDataList.Clear();
            iqaDir.Clear();
            SaveDataBtn.Enabled = false;
            automateBtn.Enabled = false;
            getDirBtn.Enabled = true;
            SearchIQA.Enabled = true;
            downloadbtn.Enabled = false;
            cmmCountTxt.Text = "0";
        }

        private void downloadbtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save File";
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = "CMMNAMEHERE.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                using (ExcelPackage excel = new ExcelPackage(iqaDir.Text))
                {
                    excel.SaveAs(filePath);
                }

                MessageBox.Show("File saved successfully!");
            }

        }

        private void SearchIQA_Click(object sender, EventArgs e)
        {
            iqaDir.Text = OpenDiag();

            if (!string.IsNullOrEmpty(iqaDir.Text))
            {
                SaveDataBtn.Enabled = true;
            }
        }

        private string OpenDiag()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an Excel file";
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                return selectedFilePath;
            }

            return "";
        }

        private void EnableDownloadBtn(bool enable)
        {
            downloadbtn.Enabled = enable;
        }

        private int[] GetStarRow()
        {
            int cmmCount = CmmCountFunct();

            using (ExcelPackage excel = new ExcelPackage(new FileInfo(iqaDir.Text)))
            {
                int[] ints = new int[2];

                int wIndex = 0;
                int startRow = wIndex > 1 ? 14 : 38;
                int wPlusser = cmmCount == 1 ? 11 : cmmCount + 10;
                do
                {
                    if (excel.Workbook.Worksheets[wIndex].Cells[startRow, wPlusser].Value == null)
                    {
                        break;
                    }

                    if (startRow >= 65)
                    {
                        startRow = 14;
                        wIndex++;
                    }

                    startRow++;

                } while (true);

                ints[0] = wIndex;
                ints[1] = startRow >= 65 ? 65 : startRow;

                return ints;
            }

        }

        private int CmmCountFunct()
        {
            int cmmCount;

            if (int.TryParse(cmmCountTxt.Text, out cmmCount))
            {
                return cmmCount;
            }
            else
            {
                return 0;
            }

        }

        private void SaveDataBtn_Click(object sender, EventArgs e)
        {
            cmmDataList.Clear();

            int[] startRow = GetStarRow();
            int cmmCount = CmmCountFunct();
            int nextCol = 10 + cmmCount;

            int thisWIndex = 0;
            int wIndex = startRow[0];
            int cRow = startRow[1];

            foreach (DataGridViewRow row in dataDgv.Rows)
            {
                CMMData data = new CMMData();
                {
                    data.ItemNo = row.Cells[0].Value?.ToString();
                    data.Nominal = row.Cells[1].Value?.ToString();
                    data.MaxTol = row.Cells[2].Value?.ToString();
                    data.MinTol = row.Cells[3].Value?.ToString();
                    data.Actual = row.Cells[4].Value?.ToString();
                }

                if (!string.IsNullOrEmpty(data.Actual)) cmmDataList.Add(data);
            }

            using (ExcelPackage package = new ExcelPackage(new FileInfo(iqaDir.Text)))
            {
                foreach (var data in cmmDataList)
                {
                    if (cRow >= 65)
                    {
                        thisWIndex++;
                        cRow = 14;

                        if (cmmCount == 1)
                        {
                            Assembly assembly = Assembly.GetExecutingAssembly();

                            using (Stream? stream = assembly.GetManifestResourceStream("CMM_DM.p2.xlsx"))
                            {
                                if (stream != null)
                                {
                                    using (ExcelPackage takep2 = new ExcelPackage(stream))
                                    {
                                        package.Workbook.Worksheets.Add("P" + (package.Workbook.Worksheets.Count + 1).ToString(), takep2.Workbook.Worksheets[0]);
                                    }
                                }
                            }
                        }

                    }

                    if (cmmCount == 1)
                    {
                        var ws = package.Workbook.Worksheets[thisWIndex];
                        string itemNo = AndReplacer(data.ItemNo);

                        ws.Cells[cRow, 1].Value = itemNo;
                        ws.Cells[cRow, 1].Style.ShrinkToFit = true;

                        if (ws.Cells[cRow - 1, 1].Value?.ToString() == itemNo)
                        {
                            ws.Cells[cRow, 1].Style.Font.Color.SetColor(Color.White);
                        }

                        ws.Cells[cRow, 3].Value = data.Nominal;
                        ws.Cells[cRow, 5].Value = data.MaxTol;
                        ws.Cells[cRow, 9].Value = OperateTols(data.Nominal, data.MinTol, '+');                     
                        ws.Cells[cRow, 10].Value = "CMM";
                        ws.Cells[cRow, 11].Value = data.Actual;

                        ws.Cells[cRow, 7].Value = OperateTols(data.Nominal, data.MaxTol, '-');
                    }
                    else
                    {
                        package.Workbook.Worksheets[thisWIndex].Cells[cRow, nextCol].Value = data.Actual;
                    }

                    cRow++;
                }

                if (!tempFile.Checked)
                {
                    tempFilePath = Path.ChangeExtension(Path.GetTempFileName(), ".xlsx");
                    iqaDir.Text = tempFilePath.ToString();
                    tempFile.Checked = true;
                }

                package.SaveAs(new FileInfo(iqaDir.Text));
            }

            SaveDataBtn.Enabled = false;
            getDirBtn.Enabled = true;
            dataDgv.Rows.Clear();
            automateBtn.Enabled = false;
            SearchIQA.Enabled = false;
            EnableDownloadBtn(true);
        }

        private string AndReplacer(string val)
        {
            if (val.Contains("and")) val = Regex.Replace(val, " and ", "-");

            return val;
        }

        private string OperateTols(string nom, string tol, char op)
        {
            string nominal = "";
            string tolerance = "";
            string res = "";

            foreach (char n in nom)
            {
                if (char.IsDigit(n) || n == '.')
                {
                    nominal += n;
                }
            }

            foreach (char t in tol)
            {
                if (char.IsDigit(t) || t == '.')
                {
                    tolerance += t;
                }
            }
          
            if (op == '+')
            {
                res = (double.Parse(nominal) + double.Parse(tolerance)).ToString();
            }
            else
            {
                res = (double.Parse(nominal) - double.Parse(tolerance)).ToString();
            }

            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CMM DM was developed to assist in data migration from C.M.M to I.Q.A checklist. \r\n\r\nDevelopers:\r\nToledo, John Gabriel D.\r\nBolante, Kylah Mae B.", "About CMM DM");
        }

    }
}