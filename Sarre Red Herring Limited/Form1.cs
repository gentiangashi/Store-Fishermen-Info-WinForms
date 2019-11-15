using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sarre_Red_Herring_Limited
{
    public partial class Form1 : Form
    {
        //Initializing Lists
        List<Fleet> boatList = new List<Fleet>();
        List<Fish> fishList = new List<Fish>();
        //Initializing DataTables
        DataTable FishData = new DataTable();
        DataTable BoatData = new DataTable();
        DataTable FishData2 = new DataTable();
        DataTable BoatData2 = new DataTable();
        DataTable QuotaReport = new DataTable();
        //Initializing Boats
        private Fleet currentBoat;
        private int boats = 0;
        //Initializing Fishes
        private Fish currentFish;
        private int fishes = 0;
        //Initializing Boat Parameters
        string licenseVariable;
        decimal loadVariable;
        decimal quotaFilled = 0;
        //Initializing Miscellaneous
        int index = 0;
        int Temp1;
        int Temp2;
        int Temp3;
        int Temp4;

        public Form1()
        {
            InitializeComponent();
        }

        //Initializing Arrays
        string[] Species = new string[10] { "Angler", "Cod", "Haddock", "Hake", "Horse Mackerel", "Witches", "Plaice", "Skate and Rays", "Whiting","Not Selected" };
        decimal[] Tonnes = new decimal[10] { 5, 3, 4, 1, 0.5m, 3, 8, 1.8m, 7,0m };
        decimal[] Factor = new decimal[10] { 1.22m, 1.17m, 1.17m, 1.11m, 1.08m, 1.06m, 1.05m, 1.13m, 1.18m,0m };
        int[] Kilograms = new int[10] { 5000, 3000, 4000, 1000, 500, 3000, 8000, 1800, 7000,0 };
        string[] weightVar = new string[2] { "KG", "T" };
        string[] License = new string[2] { "L", "LL" };
        int[] FishAmount = new int[4];

        string selectedLicense;

        private void Form1_Load(object sender, EventArgs e)
        {
            LLComboBox.Items.AddRange(License);
            LLComboBox.SelectedIndex = 0;
            
            //Changes Image Size To Fit Into PictureBox
            LogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            //Creating Fish List
            var species = new List<object>
            {
            new Fish(Species[0], Factor[0],Kilograms[0]).GetSpecies(),
            new Fish(Species[1], Factor[1],Kilograms[1]).GetSpecies(),
            new Fish(Species[2], Factor[2],Kilograms[2]).GetSpecies(),
            new Fish(Species[3], Factor[3],Kilograms[3]).GetSpecies(),
            new Fish(Species[4], Factor[4],Kilograms[4]).GetSpecies(),
            new Fish(Species[5], Factor[5],Kilograms[5]).GetSpecies(),
            new Fish(Species[6], Factor[6],Kilograms[6]).GetSpecies(),
            new Fish(Species[7], Factor[7],Kilograms[7]).GetSpecies(),
            new Fish(Species[8], Factor[8],Kilograms[8]).GetSpecies(),
            new Fish(Species[9], Factor[9],Kilograms[9]).GetSpecies(),
            };
            //Populating ComboBox With FishList
            Catch1ComboBox.DataSource = new BindingList<object>(species);
            Catch2ComboBox.DataSource = new BindingList<object>(species);
            Catch3ComboBox.DataSource = new BindingList<object>(species);
            Catch4ComboBox.DataSource = new BindingList<object>(species);
            //Setting Default Index For ComboBox
            Catch1ComboBox.SelectedIndex = 9;
            Catch2ComboBox.SelectedIndex = 9;
            Catch3ComboBox.SelectedIndex = 9;
            Catch4ComboBox.SelectedIndex = 9;            
            //Creating Fish DataGrid Columns
            FishDataGridView.DataSource = FishData;
            FishData.Columns.Add("Fish Species");
            FishData.Columns.Add("Factor");
            FishData.Columns.Add("Quota");
            FishDataGridView.RowTemplate.Height = 15;
            //Populating Fish DataGrid
            for (int i = 0; i < 9; i++)
            {
                FishData.Rows.Add(new object[] { Species[i], Factor[i], Kilograms[i] + weightVar[index] });              
            }
            //Creating Boat DataGrid Columns
            BoatDataGridView.DataSource = BoatData;
            BoatData.Columns.Add("Boat Name");
            BoatData.Columns.Add("License Number");
            BoatData.Columns.Add("Maximum Load");
            BoatDataGridView.RowTemplate.Height = 15;
            //Creating Second Fish DataGrid Columns
            FishDataGridView2.DataSource = FishData2;
            FishData2.Columns.Add("Fish Species");
            FishData2.Columns.Add("Amount");
            FishData2.Columns.Add("Fished");
            FishData2.Columns.Add("Quota");
            FishDataGridView2.RowTemplate.Height = 15;
            //Creating Quota Report DataGrid Columns
            FishQuotaReport.DataSource = QuotaReport;
            QuotaReport.Columns.Add("Quota");
            QuotaReport.Columns.Add("Total Weight Caught");
            FishQuotaReport.RowTemplate.Height = 15;
        }       
        //Method To Change Weight To Tonnes
        private void TonnesButton_Click(object sender, EventArgs e)
        {
            index = 1;
            
            DataTable FishData = new DataTable();
            FishData.Columns.Add("Fish Species");
            FishData.Columns.Add("Factor");
            FishData.Columns.Add("Quota");
            //Populates Fish DataGrid
            for (int i = 0; i < 9; i++)
            {
                FishData.Rows.Add(new object[] { Species[i], Factor[i], Tonnes[i] + weightVar[index] });    
            }
            
            FishDataGridView.DataSource = FishData;
            FishDataGridView2.DataSource = FishData2;

            //Reset DataGrid Rows
            BoatData.Rows.Clear();
            FishData2.Rows.Clear();
            QuotaReport.Rows.Clear();
            BoatData2.Rows.Clear();
            //Populating DataGrids In Tonnes Format
            for (int i = 0; i < boatList.Count; i++)
            {
                if (BoatSelector.SelectedIndex == i)
                {
                    //Add Rows For Boat DataGrid
                    BoatData.Rows.Add(new object[] { boatList[i].GetBoatName(), boatList[i].GetBoatLicense(), boatList[i].GetMaximumLoad() / 1000 + weightVar[index] });
                    QuotaReport.Rows.Add(new object[] { boatList[i].GetMaximumLoad() / 1000 + weightVar[index], boatList[i].GetQuotaFilled() / 1000 + weightVar[index] });
                }
            }
            //Add Rows For Fish DataGrid
            FishData2.Rows.Add(new object[] { fishList[Temp1].GetSpecies(), FishAmount[0], fishList[Temp1].GetFactor() * FishAmount[0] / 1000 + weightVar[index], fishList[Temp1].GetQuota() / 1000 + weightVar[index] });
            FishData2.Rows.Add(new object[] { fishList[Temp2].GetSpecies(), FishAmount[1], fishList[Temp2].GetFactor() * FishAmount[1] / 1000 + weightVar[index], fishList[Temp2].GetQuota() / 1000 + weightVar[index] });
            FishData2.Rows.Add(new object[] { fishList[Temp3].GetSpecies(), FishAmount[2], fishList[Temp3].GetFactor() * FishAmount[2] / 1000 + weightVar[index], fishList[Temp3].GetQuota() / 1000 + weightVar[index] });
            FishData2.Rows.Add(new object[] { fishList[Temp4].GetSpecies(), FishAmount[3], fishList[Temp4].GetFactor() * FishAmount[3] / 1000 + weightVar[index], fishList[Temp4].GetQuota() / 1000 + weightVar[index] });
        }
        //Method To Change Weight To Kilograms
        private void KilogramButton_Click(object sender, EventArgs e)
        {
            index = 0;
            //Populates FishDataGrid Columns
            DataTable FishData = new DataTable();
            FishData.Columns.Add("Fish Species");
            FishData.Columns.Add("Factor");
            FishData.Columns.Add("Quota");

            for (int i = 0; i < 9; i++)
            {
                FishData.Rows.Add(new object[] { Species[i], Factor[i], Kilograms[i] + weightVar[index] });
            }            
            FishDataGridView.DataSource = FishData;
            FishDataGridView2.DataSource = FishData2;

            //Reset DataGrid Rows
            BoatData.Rows.Clear();
            FishData2.Rows.Clear();
            QuotaReport.Rows.Clear();
            BoatData2.Rows.Clear();
            //Populating DataGrids In Kilograms Format
            for (int i = 0; i < boatList.Count; i++)
            {
                if (BoatSelector.SelectedIndex == i)
                {
                    //Add Rows For Boat DataGrid
                    BoatData.Rows.Add(new object[] { boatList[i].GetBoatName(), boatList[i].GetBoatLicense(), boatList[i].GetMaximumLoad() + weightVar[index] });
                    QuotaReport.Rows.Add(new object[] { boatList[i].GetMaximumLoad() + weightVar[index], boatList[i].GetQuotaFilled() + weightVar[index] });
                }
            }
            //Add Rows For Fish DataGrid
            FishData2.Rows.Add(new object[] { fishList[Temp1].GetSpecies(), FishAmount[0], fishList[Temp1].GetFactor() * FishAmount[0] + weightVar[index], fishList[Temp1].GetQuota() + weightVar[index] });
            FishData2.Rows.Add(new object[] { fishList[Temp2].GetSpecies(), FishAmount[1], fishList[Temp2].GetFactor() * FishAmount[1] + weightVar[index], fishList[Temp2].GetQuota() + weightVar[index] });
            FishData2.Rows.Add(new object[] { fishList[Temp3].GetSpecies(), FishAmount[2], fishList[Temp3].GetFactor() * FishAmount[2] + weightVar[index], fishList[Temp3].GetQuota() + weightVar[index] });
            FishData2.Rows.Add(new object[] { fishList[Temp4].GetSpecies(), FishAmount[3], fishList[Temp4].GetFactor() * FishAmount[3] + weightVar[index], fishList[Temp4].GetQuota() + weightVar[index] });
        }

        public static bool Check(ComboBox combo, NumericUpDown amount)
        {
            //Error Checking, If Fish Selected & Value = 0, Return Error
            if (combo.SelectedIndex != 9 && amount.Value == 0)
                return false;
            return true;
        }   

        private void BoatSubmitButton_Click(object sender, EventArgs e)
        {
            //Resets Error Labels
            NameErrorLabel.Visible = false;
            LicenseErrorLabel.Visible = false;
            LoadErrorLabel.Visible = false;
            FishErrorLabel.Visible = false;
            FishAmountErrorLabel.Visible = false;
            //Creates Fish Objects
            for (int i = 0; i < 10; i++)
            {
                fishList.Add(new Fish(Species[i], Factor[i], Kilograms[i]));
            }

            //Boat Variables
            licenseVariable = BoatLicenseTextBox.Text;
            loadVariable = MaximumLoadNumeric.Value;

            //Fish Variables          
             FishAmount[0] = int.Parse(NumericAmount1.Text);
             FishAmount[1] = int.Parse(NumericAmount2.Text);
             FishAmount[2] = int.Parse(NumericAmount3.Text);
             FishAmount[3] = int.Parse(NumericAmount4.Text);

            Temp1 = int.Parse(Catch1ComboBox.SelectedIndex.ToString());
            Temp2 = int.Parse(Catch2ComboBox.SelectedIndex.ToString());
            Temp3 = int.Parse(Catch3ComboBox.SelectedIndex.ToString());
            Temp4 = int.Parse(Catch4ComboBox.SelectedIndex.ToString());

            quotaFilled = (fishList[Temp1].GetFactor() * FishAmount[0]) + (fishList[Temp2].GetFactor() * FishAmount[1]) +
                   (fishList[Temp3].GetFactor() * FishAmount[2]) + (fishList[Temp4].GetFactor() * FishAmount[3]);
            
            //Error Checking
            if (BoatNameTextBox.Text == "")
            {
                MessageBox.Show("Please Input Name", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Error Label Made Visible
                NameErrorLabel.Visible = true;                
            }        
            else if(BoatLicenseTextBox.Text == "")
            {                
                MessageBox.Show("Please Input License", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Error Label Made Visible
                LicenseErrorLabel.Visible = true;
            }
            else if (MaximumLoadNumeric.Value == 0 || MaximumLoadNumeric.Value > 1000)
            {
                MessageBox.Show("Maximum Load Must Be Above 0 And Under 1000, Please Try Again", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Error Label Made Visible
                LoadErrorLabel.Visible = true;
            }
            else if (!Check(Catch1ComboBox, NumericAmount1) || !Check(Catch2ComboBox, NumericAmount2) ||
                !Check(Catch3ComboBox, NumericAmount3) || !Check(Catch4ComboBox, NumericAmount4))
                 {
                MessageBox.Show("Please Input Amount Of Fish Caught", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Error Label Made Visible
                FishAmountErrorLabel.Visible = true;
            }                
            else if (Catch1ComboBox.SelectedIndex == 9) 
                 {
                MessageBox.Show("Please Select A Fish Specie", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Error Label Made Visible
                FishErrorLabel.Visible = true;
            }
            else if (loadVariable < quotaFilled)
            {
                MessageBox.Show("You Are Over Your Limit, Please Verify.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Resets Error Labels
                NameErrorLabel.Visible = false;
                LicenseErrorLabel.Visible = false;
                LoadErrorLabel.Visible = false;
                FishErrorLabel.Visible = false;
                FishAmountErrorLabel.Visible = false;
                //Creates Boat Object
                boatList.Add(new Fleet(BoatNameTextBox.Text, selectedLicense +" "+ licenseVariable, loadVariable, quotaFilled));     

                currentBoat = boatList[boats];
                currentFish = fishList[fishes];                
                
                //Reset Boat Boxes
                BoatNameTextBox.Text = "";
                BoatLicenseTextBox.Text = "";
                MaximumLoadNumeric.Value = 0;

                //Reset Combo Boxes
                Catch1ComboBox.SelectedIndex = 9;
                Catch2ComboBox.SelectedIndex = 9;
                Catch3ComboBox.SelectedIndex = 9;
                Catch4ComboBox.SelectedIndex = 9;              

                //Reset Numeric Value Boxes
                NumericAmount1.Value = 0;
                NumericAmount2.Value = 0;
                NumericAmount3.Value = 0;
                NumericAmount4.Value = 0;

                //Increments Boat List
                boats++;

                //Increments Combo Box & Displays It
                var last = boatList.Last(); // using System.Linq;
                BoatSelector.Items.Add(last.GetBoatName() + " - " + last.GetBoatLicense());
                BoatSelector.SelectedIndex +=1;  
            }            
        }

        private void BoatSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            TonnesButton.Enabled = true;
            KilogramButton.Enabled = true;
            //Reset DataGrid Rows
            BoatData.Rows.Clear();
            FishData2.Rows.Clear();
            QuotaReport.Rows.Clear();
            BoatData2.Rows.Clear();
            for (int i = 0; i < boatList.Count; i++)
            {
                if(BoatSelector.SelectedIndex == i && index == 0)
                {
                    //Add Rows For Boat DataGrid
                    BoatData.Rows.Add(new object[] { boatList[i].GetBoatName(), boatList[i].GetBoatLicense(), boatList[i].GetMaximumLoad() + weightVar[index] });
                    QuotaReport.Rows.Add(new object[] { boatList[i].GetMaximumLoad() + weightVar[index], boatList[i].GetQuotaFilled() + weightVar[index] });

                    //Add Rows For Fish DataGrid
                    FishData2.Rows.Add(new object[] { fishList[Temp1].GetSpecies(), FishAmount[0], fishList[Temp1].GetFactor() * FishAmount[0] + weightVar[index], fishList[Temp1].GetQuota() + weightVar[index] });
                    FishData2.Rows.Add(new object[] { fishList[Temp2].GetSpecies(), FishAmount[1], fishList[Temp2].GetFactor() * FishAmount[1] + weightVar[index], fishList[Temp2].GetQuota() + weightVar[index] });
                    FishData2.Rows.Add(new object[] { fishList[Temp3].GetSpecies(), FishAmount[2], fishList[Temp3].GetFactor() * FishAmount[2] + weightVar[index], fishList[Temp3].GetQuota() + weightVar[index] });
                    FishData2.Rows.Add(new object[] { fishList[Temp4].GetSpecies(), FishAmount[3], fishList[Temp4].GetFactor() * FishAmount[3] + weightVar[index], fishList[Temp4].GetQuota() + weightVar[index] });
                }
                else if(BoatSelector.SelectedIndex == i && index == 1)
                {
                    //Add Rows For Boat DataGrid
                    BoatData.Rows.Add(new object[] { boatList[i].GetBoatName(), boatList[i].GetBoatLicense(), boatList[i].GetMaximumLoad() / 1000 + weightVar[index] });
                    QuotaReport.Rows.Add(new object[] { boatList[i].GetMaximumLoad() / 1000 + weightVar[index], boatList[i].GetQuotaFilled() / 1000 + weightVar[index] });

                    //Add Rows For Fish DataGrid
                    FishData2.Rows.Add(new object[] { fishList[Temp1].GetSpecies(), FishAmount[0], fishList[Temp1].GetFactor() * FishAmount[0] / 1000 + weightVar[index], fishList[Temp1].GetQuota() / 1000 + weightVar[index] });
                    FishData2.Rows.Add(new object[] { fishList[Temp2].GetSpecies(), FishAmount[1], fishList[Temp2].GetFactor() * FishAmount[1] / 1000 + weightVar[index], fishList[Temp2].GetQuota() / 1000 + weightVar[index] });
                    FishData2.Rows.Add(new object[] { fishList[Temp3].GetSpecies(), FishAmount[2], fishList[Temp3].GetFactor() * FishAmount[2] / 1000 + weightVar[index], fishList[Temp3].GetQuota() / 1000 + weightVar[index] });
                    FishData2.Rows.Add(new object[] { fishList[Temp4].GetSpecies(), FishAmount[3], fishList[Temp4].GetFactor() * FishAmount[3] / 1000 + weightVar[index], fishList[Temp4].GetQuota() / 1000 + weightVar[index] });
                }
            }           
        }
        //Sets License Variable
        private void LLComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LLComboBox.SelectedIndex == 0)
            {
                selectedLicense = License[0];
            }
            else if (LLComboBox.SelectedIndex == 1)
            {
                selectedLicense = License[1];
            }
        }
    }
}   
            


