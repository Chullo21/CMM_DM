using System.Reflection;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO.Packaging;
using System.Numerics;
using System.Runtime.Serialization;
using System.Diagnostics.Metrics;

namespace CMM_DM
{
    public partial class Form1 : Form
    {
        static List<CMMData> cmmDataList = new List<CMMData>();
        //static string? tempFilePath;
        static ExcelPackage package = new();

        Positions positions = new();

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
            InitializeComponentAsync();
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

        private void ColumnSetter(ExcelWorksheet ws, int row)
        {
            int counter = 1;
            bool stop = false;

            do
            {
                string? columnName = ws.Cells[row, counter].Value?.ToString();

                if (columnName != null)
                {
                    if (columnName.ToLower() == "actual")
                    {
                        positions.Actual = counter;
                    }
                    else if (columnName.ToLower() == "lower")
                    {
                        positions.Lower = counter;
                    }
                    else if (columnName.ToLower() == "upper")
                    {
                        positions.Upper = counter;
                    }
                    else if (columnName.ToLower() == "nominal")
                    {
                        positions.Nominal = counter;
                    }
                    else if (columnName.ToLower() == "deviation")
                    {
                        positions.Deviation = counter;
                        stop = true;
                    }
                }
                else if (counter == 100)
                {
                    ++row;
                    counter = 1;
                }

                ++counter;

                if (stop) break;

            } while (!stop);
        }

        private int EndChecker(ExcelWorksheet ws, int row)
        {
            int counter = 1;

            do
            {
                if (ws.Cells[row, 2].Value != null)
                {
                    return row;
                }

                ++row;
                ++counter;

            } while (counter <= 50);

            return 0;
        }

        private int IndexValueChecker(string[] values)
        {
            int counter = 0;

            foreach (string val in values)
            {
                if (val != "" && !string.IsNullOrEmpty(val))
                {
                    ++counter;
                }
            }

            return counter;
        }

        private void automateBtn_Click(object sender, EventArgs e)
        {

            int currentRow = 1;
            int nullCounter = 0;

            try
            {
                cmmDataList.Clear();
                dataDgv.Rows.Clear();


                string itemNo = "";

                using (ExcelPackage package = new(directoryTxt.Text))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    ColumnSetter(worksheet, currentRow);

                    do
                    {
                        if (worksheet.Cells[currentRow, 2].Value != null)
                        {
                            if (worksheet.Cells[currentRow, 2, currentRow, positions.Deviation].Merge)
                            {
                                itemNo = worksheet.Cells[currentRow, 2].Value.ToString();
                            }
                            else if (worksheet.Cells[currentRow, 2].Value.ToString() == "Item")
                            {
                                ColumnSetter(worksheet, currentRow);
                            }
                        }

                        ++currentRow;

                        do
                        {
                            string lower = string.IsNullOrWhiteSpace(worksheet.Cells[currentRow, positions.Lower].Value?.ToString()) ? "" : worksheet.Cells[currentRow, positions.Lower].Value.ToString();
                            string upper = string.IsNullOrWhiteSpace(worksheet.Cells[currentRow, positions.Upper].Value?.ToString()) ? "" : worksheet.Cells[currentRow, positions.Upper].Value.ToString();
                            string actual = string.IsNullOrWhiteSpace(worksheet.Cells[currentRow, positions.Actual].Value?.ToString()) ? "" : worksheet.Cells[currentRow, positions.Actual].Value.ToString();
                            string nominal = string.IsNullOrWhiteSpace(worksheet.Cells[currentRow, positions.Nominal].Value?.ToString()) ? "" : worksheet.Cells[currentRow, positions.Nominal].Value.ToString();

                            if (itemNo != "" && lower != "" && upper != "" && decimal.TryParse(lower, out _) && decimal.TryParse(upper, out _))
                            {

                                //dataDgv.Rows.Add(itemNo, nominal, upper, lower, actual);
                                string[] nominalSplit = nominal.Split('\n');
                                string[] actualSplit = actual.Split('\n');

                                int splitCounter = IndexValueChecker(nominalSplit);

                                if (splitCounter > 1)
                                {
                                    for (int i = 0; i < IndexValueChecker(nominalSplit); i++)
                                    {
                                        string[] characteristics;
                                        int charCounter = positions.Nominal - 1;

                                        do
                                        {
                                            if (worksheet.Cells[currentRow, charCounter].Value != null)
                                            {
                                                if (worksheet.Cells[currentRow, charCounter].Value.ToString() != "" && !string.IsNullOrEmpty(worksheet.Cells[currentRow, charCounter].Value.ToString()))
                                                {
                                                    characteristics = worksheet.Cells[currentRow, charCounter].Value.ToString().Split('\n').Where(j => j != "").ToArray();
                                                    break;
                                                }

                                            }

                                            --charCounter;

                                        } while (true);

                                        if (!string.IsNullOrEmpty(nominalSplit[i]))
                                        {
                                            dataDgv.Rows.Add(itemNo, $"{characteristics[i]}{nominalSplit[i]}", upper, lower, actualSplit[i]);
                                        }
                                    }
                                }
                                else
                                {
                                    dataDgv.Rows.Add(itemNo, nominal, upper, lower, actual);
                                }
                            }

                            int tempCounter = currentRow + 1;

                            if (worksheet.SelectedRange[tempCounter, 2, tempCounter, positions.Deviation].Merge == false && worksheet.Cells[tempCounter, 2].Value == null)
                            {
                                ++currentRow;
                                ++nullCounter;

                                if (nullCounter == 50)
                                {
                                    nullCounter = 0;
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }

                        } while (true);

                        int endRow = EndChecker(worksheet, currentRow);

                        if (endRow == 0)
                        {
                            break;
                        }
                        else
                        {
                            currentRow = endRow;
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
                MessageBox.Show($"Something went wrong.\r\n{ex.Message}", "Error");
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

        private int[] GetStarRow()
        {
            int cmmCount = CmmCountFunct();
            int[] res = new int[2];
            int startRow = 38;
            int wPlusser = cmmCount == 1 ? 11 : cmmCount + 10;
            int pageCounter = 0;
            var pack = package.Workbook.Worksheets[pageCounter];

            do
            {
                if (pack.Cells[startRow, wPlusser].Value == null)
                {
                    break;
                }

                if (startRow >= 65)
                {
                    startRow = 14;
                    pack = package.Workbook.Worksheets[++pageCounter];
                }

                startRow++;

            } while (true);

            res[0] = startRow >= 65 ? 65 : startRow;
            res[1] = pageCounter;

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

        private void AddPage()
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

        private void SaveDataBtn_Click(object sender, EventArgs e)
        {
            cmmDataList.Clear();

            if (CmmCountFunct() == 1)
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
            int[] startRow = GetStarRow();
            int thisWIndex = startRow[1];
            int cRow = startRow[0];

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
                    ++thisWIndex;
                    cRow = 14;

                    if (thisWIndex + 1 > package.Workbook.Worksheets.Count)
                    {
                        AddPage();
                    }
                }

                var ws = package.Workbook.Worksheets[thisWIndex];
                int actualColorSetter = 11;

                if (!string.IsNullOrEmpty(data.Nominal) && !char.IsDigit(data.Nominal[0]) && data.Nominal[0] != '-')
                {
                    string dat = data.Nominal;
                    ws.Cells[cRow, 6].Value = data.Nominal[0].ToString();
                    data.Nominal = data.Nominal.Substring(1);
                }

                if (CmmCountFunct() == 1)
                {
                    string itemNo = AndReplacer(data.ItemNo);
                    string? prevChecker = ws.Cells[cRow - 1, 1].Value?.ToString().Trim();
                    ws.Cells[cRow, 1].Value = itemNo.Trim();
                    ws.Cells[cRow, 1].Style.ShrinkToFit = true;

                    if (!string.IsNullOrEmpty(prevChecker) && prevChecker.Trim() == itemNo.Trim())
                    {
                        ws.Cells[cRow, 1].Style.Font.Color.SetColor(Color.White);
                    }

                    if (decimal.Parse(data.MaxTol) == 0.0m)
                    {
                        if (!ws.Cells[cRow, 2].Merge) ws.Cells[cRow, 2, cRow, 5].Merge = true;
                        ws.Cells[cRow, 2].Value = $"{data.Nominal}/-{data.MinTol}/+{data.MaxTol}";
                        ws.Cells[cRow, 9].Value = decimal.Parse(data.Nominal).ToString("0.00");
                        ws.Cells[cRow, 7].Value = OperateTols(data.Nominal, data.MinTol, '-');
                    }
                    else if (decimal.Parse(data.MinTol) == 0.0m)
                    {
                        if (!ws.Cells[cRow, 2].Merge) ws.Cells[cRow, 2, cRow, 5].Merge = true;
                        ws.Cells[cRow, 2].Value = $"{data.Nominal}/-{data.MinTol}/+{data.MaxTol}";
                        ws.Cells[cRow, 7].Value = data.Nominal;
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
                    ws.Cells[cRow, 11].Value = decimal.Parse(data.Actual).ToString("0.00"); // actual

                }
                else
                {
                    ws.Cells[cRow, nextCol].Value = decimal.Parse(data.Actual).ToString("0.00");
                    actualColorSetter = nextCol;
                }

                string actual = data.Actual == "" ? "0.0" : data.Actual;
                string lower = ws.Cells[cRow, 7].Value == null ? "0.0" : ws.Cells[cRow, 7].Value.ToString();
                string upper = ws.Cells[cRow, 9].Value == null ? "0.0" : ws.Cells[cRow, 9].Value.ToString();

                if (decimal.Parse(actual) > decimal.Parse(upper) || decimal.Parse(actual) < decimal.Parse(lower))
                {
                    ws.Cells[cRow, actualColorSetter].Style.Font.Color.SetColor(Color.Red);
                }

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

                    if (CellValueGetter(rowIdentifier, columnIdentifier, 0).ToString().ToLower() == "x")
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
            } while (columnIdentifier <= totalNumberOfCols);
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
                        decimal actual = CellValueGetter(rowIdentifier, columnIdentifier, i);
                        decimal min = CellValueGetter(rowIdentifier, 7, i);
                        decimal max = CellValueGetter(rowIdentifier, 9, i);

                        if (actual == 0.0m && min == 0.0m && max == 0.0m) break;

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

                for (int i = 0; i < package.Workbook.Worksheets.Count; i++)
                {
                    if (judgement) package.Workbook.Worksheets[i].Cells[65, columnIdentifier].Value = "FAIL"; else package.Workbook.Worksheets[i].Cells[65, columnIdentifier].Value = "PASS";
                }

                columnIdentifier++;
            } while (columnIdentifier <= numberOfColumns);
        }

        private decimal CellValueGetter(int row, int col, int worksheet)
        {
            var ws = package.Workbook.Worksheets[worksheet];

            string res = ws.Cells[row, col].Value == null ? "" : ws.Cells[row, col].Value.ToString();

            if (decimal.TryParse(res, out _))
            {
                return decimal.Parse(res);
            }
            else
            {
                return decimal.Parse("0.0");
            }
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
            decimal res = 0;

            try
            {
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
                    res = decimal.Parse(nominal) + decimal.Parse(tolerance);
                }
                else
                {
                    res = decimal.Parse(nominal) - decimal.Parse(tolerance);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
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
            SaveDataBtn.Enabled = true;
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Form1
            // 
            ClientSize = new Size(819, 635);
            Name = "Form1";
            ResumeLayout(false);
        }
    }

    public class Positions
    {
        public int  Nominal { get; set; }
        public int Upper { get; set; }
        public int Lower { get; set; }
        public int Actual { get; set; }
        public int Deviation { get; set; }
    }
}