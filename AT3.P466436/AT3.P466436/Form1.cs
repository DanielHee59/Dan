using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace AT3.P466436
{
    public partial class Form1 : Form
    {
        private string[] gameNames;
        private string[] gamePlatforms;
        private string[] gameGenre;
        int numItems;
        public Form1()
        {
            InitializeComponent();
            gameNames = new string[20];
           

            gamePlatforms = new string[20];
          

            gameGenre = new string[20];
        

           
            numItems = 0;

            
        }

        private void DisplayNames()
        {
           BubbleSort(gameNames);

            lstOutput.Items.Clear();
      
            for (int i = 0; i < numItems; i++)
            {
                lstOutput.Items.Add(gameNames[i] + " " + gamePlatforms[i] + " " + gameGenre[i]);
            }

          
        }

        private void lstOutput_SelectedIndexChanged(object sender, EventArgs e)
        {

       
            if (lstOutput.SelectedIndex == -1)
            {
               return;
            }
            else 
            {
                tbName.Text = gameNames[lstOutput.SelectedIndex];
                tbPlatform.Text = gamePlatforms[lstOutput.SelectedIndex];
                tbGenre.Text = gameGenre[lstOutput.SelectedIndex];
             
            }
        }

        //Add Button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                MessageBox.Show("Include a valid name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (tbGenre.Text == "")
            {
                MessageBox.Show("Include a valid genre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (tbPlatform.Text == "")
            {
                MessageBox.Show("Include a valid platform", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

            gameNames[numItems] = tbName.Text;
            gamePlatforms[numItems] = tbPlatform.Text;
            gameGenre[numItems] = tbGenre.Text;
            numItems++;

            DisplayNames();

            tbName.Clear();
            tbGenre.Clear();
            tbPlatform.Clear();
        }

        //Clear Button
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbName.Clear();
            tbGenre.Clear();
            tbPlatform.Clear();
        }

        private void DeleteGames(string[]strings, int Games)
        {
            for (int i = Games; i < numItems - 1; i++)
            {
                strings[i] = strings[i + 1];
            }
            
            numItems--;
        }

        //Delete Button
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(lstOutput.SelectedIndex == -1)
            {
                MessageBox.Show("Select an item from the list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DeleteGames(gameNames, lstOutput.SelectedIndex);
            }
            DisplayNames();
        }

        //Reset Button
        private void btnReset_Click(object sender, EventArgs e)
        {
            lstOutput.Items.Clear();
        }

        //Update Button
        private void btnUpdate_Click(object sender, EventArgs e)
        {

            int index = lstOutput.SelectedIndex;

            if (index == -1)
            {
                MessageBox.Show("Please select an item");
            }
            else
            {
                gameNames[index] = tbName.Text;
                gamePlatforms[index] = tbPlatform.Text;
                gameGenre[index] = tbGenre.Text;

                DisplayNames();
            }


        }


        private int BinarySearch(string j)
        {

            int lowerBound = 0;
            int upperBound = numItems - 1;
            int mid; 

            while (true)
            {
                mid = (lowerBound + upperBound) / 2;
                int i = gameNames[mid].CompareTo(j);
                if (i == 0)
                {
                    return mid;
                }
                else if (lowerBound > upperBound)
                {
                    return -1;
                }
                else
                {
                   if(i < 0)
                    {
                        lowerBound = mid + 1;
                    }
                    else
                    {
                        upperBound = mid - 1;
                    }
                }
            }
        }

        //Search Button
        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            int result = BinarySearch(tbName.Text);
           

            if (result == -1)
            {
                 MessageBox.Show("Not Found");
            }
            else           
            {
                lstOutput.SelectedIndex = result;
            }
            
        }
        
        //Save Button
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open("mygames.dat", FileMode.Create)))
                {
                   
                    writer.Write(numItems);


                    for (int i = 0; i < numItems; i++)
                    {
                        writer.Write(gameNames[i]);
                        writer.Write(gamePlatforms[i]);
                        writer.Write(gameGenre[i]);
                        
                    }
                }
            }
            catch (IOException x)
            {
                MessageBox.Show("Exception: " + x.Message);
            }
        }

        //Open Button
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open("mygames.dat", FileMode.Open, FileAccess.Read)))
                {
                    numItems = reader.ReadInt32();


                    for(int i = 0; i < numItems; i++)
                    {
                        gameNames[i] = reader.ReadString();
                        gamePlatforms[i] = reader.ReadString();
                        gameGenre[i] = reader.ReadString();
                    }                                       
                }
            }
            catch(IOException x)
            {
                MessageBox.Show("Exception: " + x.Message);
            }

            DisplayNames();
        }

        public void BubbleSort(string [] gameNames)
        {
            int i;
            string temp;
            string temp2 ;
            string temp3;

            for (int j = numItems-1; j > 0 ; j--)
            {
                for ( i = 0; i < j ; i++)
                {
                    if (gameNames[i].CompareTo(gameNames[i + 1]) > 0)
                    {
                        temp = gameNames[i+1];
                        gameNames[i+1] = gameNames[i];
                        gameNames[i] = temp;

                        temp2 = gamePlatforms[i + 1];
                        gamePlatforms[i + 1] = gamePlatforms[i];
                        gamePlatforms[i] = temp2;

                        temp3 = gameGenre[i + 1];
                        gameGenre[i + 1] = gameGenre[i];
                        gameGenre[i] = temp3;
                    }                   
                }
            }           
        }
    }
}
