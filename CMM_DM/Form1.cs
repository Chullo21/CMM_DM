using System.Reflection;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO.Packaging;

namespace CMM_DM
{
    public partial class Form1 : Form
    {
        static List<CMMData> cmmDataList = new List<CMMData>();
        //static string? tempFilePath;
        static ExcelPackage package = new();

        static bool browseIQA = false;

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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong.\r\n{ex}", "Error");
                clearBtn.PerformClick();
            }
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
            IQATemplateBtn.Enabled = true;
            package = new();
        }

        private void downloadbtn_Click(object sender, EventArgs e)
        {
            JudgementDimensionAnalyser();
            JudgementAppearanceAnalyser();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save File";
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = "CMMNAMEHERE.xlsx";

            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    package.SaveAs(filePath);

                    MessageBox.Show("File saved successfully!");

                    clearBtn.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:\r\n{ex}", "Something went wrong");
            }

        }

        private void SearchIQA_Click(object sender, EventArgs e)
        {
            iqaDir.Text = OpenDiag();
            browseIQA = true;

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
                return openFileDialog.FileName;
            }

            return "";
        }

        private void EnableDownloadBtn(bool enable)
        {
            downloadbtn.Enabled = enable;
        }

        private int GetStarRow()
        {
            int cmmCount = CmmCountFunct();
            int res = 0;
            int startRow = 38;
            int wPlusser = cmmCount == 1 ? 11 : cmmCount + 10;
            var pack = package.Workbook.Worksheets[0];

            do
            {
                object val = pack.Cells[startRow, wPlusser].Value;
                if (val == null)
                {
                    break;
                }

                if (startRow >= 65)
                {
                    startRow = 14;
                }

                startRow++;

            } while (true);

            res = startRow >= 65 ? 65 : startRow;

            return res;

        }

        private int CmmCountFunct()
        {
            return int.Parse(cmmCountTxt.Text);

        }

        private bool VerifyCS()
        {
            using (ExcelPackage ver = new(iqaDir.Text))
            {
                if (ver.Workbook.Worksheets[0].Cells[1, 5].Value?.ToString() != "INSPECTION CHECKLIST")
                {
                    MessageBox.Show("Error IQA Checksheet, you have selected an invalid sheet.\r\nPlease select a valid sheet.", "Something went wrong");
                    iqaDir.Clear();
                    SaveDataBtn.Enabled = false;

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void SaveDataBtn_Click(object sender, EventArgs e)
        {
            cmmDataList.Clear();

            int cmmCount = CmmCountFunct();

            if (cmmCount == 1)
            {
                if (browseIQA)
                {
                    package = new ExcelPackage(new FileInfo(iqaDir.Text));
                    if (VerifyCS()) return;
                }
                else
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    package = new ExcelPackage(assembly.GetManifestResourceStream("CMM_DM.Resources.Templates.IQA_CS.xlsx"));
                }
            }

            int nextCol = 10 + CmmCountFunct();

            int thisWIndex = 0;
            int cRow = GetStarRow();

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

            foreach (var data in cmmDataList)
            {
                if (cRow >= 65)
                {
                    thisWIndex++;
                    cRow = 14;

                    if (cmmCount == 1)
                    {
                        Assembly assembly = Assembly.GetExecutingAssembly();

                        using (Stream? stream = assembly.GetManifestResourceStream("CMM_DM.Resources.Templates.p2.xlsx"))
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

                var ws = package.Workbook.Worksheets[thisWIndex];
                int actualColorSetter = 11;

                if (cmmCount == 1)
                {
                    string itemNo = AndReplacer(data.ItemNo);
                    string? prevChecker = ws.Cells[cRow - 1, 1].Value?.ToString().Trim();
                    ws.Cells[cRow, 1].Value = itemNo.Trim();
                    ws.Cells[cRow, 1].Style.ShrinkToFit = true;

                    if (!string.IsNullOrEmpty(prevChecker) && prevChecker.Trim() == itemNo.Trim())
                    {
                        ws.Cells[cRow, 1].Style.Font.Color.SetColor(Color.White);
                    }

                    if (data.MaxTol == "0.000")
                    {
                        ws.Cells[cRow, 2, cRow, 5].Merge = true;
                        ws.Cells[cRow, 2].Value = $"{data.Nominal}/-{data.MinTol}/+{data.MaxTol}";
                        ws.Cells[cRow, 9].Value = double.Parse(data.Nominal).ToString("0.00");
                        ws.Cells[cRow, 7].Value = OperateTols(data.Nominal, data.MinTol, '-');
                    }
                    else if (data.MinTol == "0.000")
                    {
                        ws.Cells[cRow, 2, cRow, 5].Merge = true;
                        ws.Cells[cRow, 2].Value = $"{data.Nominal}/-{data.MinTol}/+{data.MaxTol}";
                        ws.Cells[cRow, 7].Value = double.Parse(data.Nominal).ToString("0.00");
                        ws.Cells[cRow, 9].Value = OperateTols(data.Nominal, data.MaxTol, '+');
                    }
                    else
                    {
                        ws.Cells[cRow, 3].Value = data.Nominal; //nominal
                        ws.Cells[cRow, 5].Value = data.MaxTol; // tolerance
                        ws.Cells[cRow, 9].Value = OperateTols(data.Nominal, data.MaxTol, '+'); // min tol
                        ws.Cells[cRow, 7].Value = OperateTols(data.Nominal, data.MaxTol, '-'); // max tol
                    }
                    ws.Cells[cRow, 7, cRow, 9].Style.WrapText = false;
                    ws.Cells[cRow, 10].Value = "CMM"; // type
                    ws.Cells[cRow, 11].Value = double.Parse(data.Actual).ToString("0.00"); // actual
                }
                else
                {
                    ws.Cells[cRow, nextCol].Value = double.Parse(data.Actual).ToString("0.00");
                    actualColorSetter = nextCol;
                }

                if (double.Parse(data.Actual) > double.Parse(ws.Cells[cRow, 9].Value?.ToString()) || double.Parse(data.Actual) < double.Parse(ws.Cells[cRow, 7].Value?.ToString())) ws.Cells[cRow, actualColorSetter].Style.Font.Color.SetColor(Color.Red);
                cRow++;
            }

            SaveDataBtn.Enabled = false;
            IQATemplateBtn.Enabled = false;
            getDirBtn.Enabled = true;
            dataDgv.Rows.Clear();
            automateBtn.Enabled = false;
            SearchIQA.Enabled = false;
            EnableDownloadBtn(true);
        }

        private void JudgementAppearanceAnalyser()
        {
            int columnIdentifier = 11;

            int totalNumberOfCols = 10 + int.Parse(cmmCountTxt.Text);

            do
            {
                int rowIdentifier = 21;
                bool judgement = false;

                do
                {

                    if (CellValueGetter(rowIdentifier, columnIdentifier, 0).ToLower() == "x")
                    {
                        judgement = true;
                        break;
                    }

                    rowIdentifier++;
                } while (rowIdentifier <= 36);

                for (int i = 0; i < package.Workbook.Worksheets.Count; i++)
                {
                    if (judgement) package.Workbook.Worksheets[i].Cells[66, columnIdentifier].Value = "FAIL"; else package.Workbook.Worksheets[i].Cells[66, columnIdentifier].Value = "PASS";
                }

                columnIdentifier++;
            }while(columnIdentifier <= totalNumberOfCols);
        }

        private void JudgementDimensionAnalyser()
        {
            int columnIdentifier = 11;
            int numberOfColumns = 10 + int.Parse(cmmCountTxt.Text);

            do
            {               
                bool judgement = false;

                for (int i = 0; i < package.Workbook.Worksheets.Count; i++)
                {
                    int rowIdentifier = 38;

                    do
                    {
                        var actual = double.Parse(CellValueGetter(rowIdentifier, columnIdentifier, i));
                        var min = double.Parse(CellValueGetter(rowIdentifier, 7, i));
                        var max = double.Parse(CellValueGetter(rowIdentifier, 9, i));

                        if (actual == 0.00 && min == 0.00 && max == 0.00) break;

                        if (actual > max || actual < min)
                        {
                            judgement = true;
                            break;
                        }

                        rowIdentifier++;

                    } while (rowIdentifier < 65);

                    if (judgement)
                    {
                        break;
                    }
                    else
                    {
                        rowIdentifier = 14;
                    }
                }

                for(int i = 0; i < package.Workbook.Worksheets.Count; i++)
                {
                    if (judgement) package.Workbook.Worksheets[i].Cells[65, columnIdentifier].Value = "FAIL"; else package.Workbook.Worksheets[i].Cells[65, columnIdentifier].Value = "PASS";
                }

                columnIdentifier++;
            } while (columnIdentifier <= numberOfColumns);
        }

        private string CellValueGetter(int row, int col, int worksheet)
        {
            var ws = package.Workbook.Worksheets[worksheet];

            string res = ws.Cells[row, col].Value == null ? "" : ws.Cells[row, col].Value.ToString();

            return res;
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
            double res = 0.00;

            foreach (char n in nom)
            {
                if (char.IsDigit(n) || n == '.' || n == '-')
                {
                    nominal += n;
                }
            }

            foreach (char t in tol)
            {
                if (char.IsDigit(t) || t == '.' || t == '-')
                {
                    tolerance += t;
                }
            }

            if (op == '+')
            {
                res = ((double.Parse(nominal)) + (double.Parse(tolerance)));
            }
            else
            {
                res = ((double.Parse(nominal)) - (double.Parse(tolerance)));
            }

            return res.ToString("0.00");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CMM DM was developed to assist in data migration from C.M.M to I.Q.A checklist. \r\n\r\nDevelopers:\r\nToledo, John Gabriel D.\r\nBolante, Kylah Mae B.", "About CMM DM");
        }

        private void IQATemplateBtn_Click(object sender, EventArgs e)
        {
            iqaDir.Text = "Template";
            //SearchIQA.Enabled = false;
            SaveDataBtn.Enabled = true;
            //IQATemplateBtn.Enabled = false;
            browseIQA = false;
        }
    }
}