using DeweyDecApplication.Recorded_Attempts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

//---------------------------------------------------------o0oSTARTo0o----------------------------------------------------//
namespace DeweyDecApplication.Forms
{
    public partial class IdentifyArea : Form
    {
        #region Variables
        //Lists and Dictionary
        private Dictionary<int, string> randomDict = new Dictionary<int, string>();
        private List<int> availableRanges = new List<int>();
        private List<GameAttempt2> gameAttempts = new List<GameAttempt2>();
        //Random number generator
        private Random random = new Random();
        //Used to get rid of magic numbers
        private const int MinKey = 1;
        private const int MaxKey = 10;
        //SoundPlayer for playing sounds
        private SoundPlayer soundPlayer = new SoundPlayer();
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Constructor
        public IdentifyArea()
        {
            InitializeComponent(); //Initialize the form and its components
            soundPlayer.Stream = Properties.Resources.ClickSound; //Load the click sound
            InitializeAvailableRanges(); //Initialize available ranges for game
            AssignValuesToKeys(); //Assign values to keys in Dictionary for the game

            //Event handlers for radio buttons (Handle radio button changes)
            radioButtonNormal.CheckedChanged += RadioButton_CheckedChanged;
            radioButtonExpert.CheckedChanged += RadioButton_CheckedChanged;         
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Initialize Available Ranges
        private void InitializeAvailableRanges()
        {
            availableRanges.Clear(); //Clear the list of available ranges
            for (int i = 0; i < 10; i++)
            {
                availableRanges.Add(i); //Add numbers from 0 to 9 to the availableRanges list
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Assigning Values To Keys
        private void AssignValuesToKeys()
        {
            //Define the mapping of key ranges to values
            Dictionary<int, string> rangeToValueMapping = new Dictionary<int, string>
            {
                { 1, "Generalities" },
                { 2, "Philosophy" },
                { 3, "Religion" },
                { 4, "Social Science" },
                { 5, "Languages" },
                { 6, "Natural Sciences" },
                { 7, "Applied Sciences" },
                { 8, "Arts and Recreation" },
                { 9, "Literature" },
                { 10, "Geography and History" }
            };
            //Check if the mapping contains exactly 10 entries
            if (rangeToValueMapping.Count != 10)
            {
                throw new InvalidOperationException("The rangeToValueMapping dictionary should have exactly 10 entries.");
            }
            //Populate the 'randomDict' dictionary with the key-value pairs from 'rangeToValueMapping'
            foreach (var kvp in rangeToValueMapping)
            {
                randomDict[kvp.Key] = kvp.Value;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Displaying Keys And Values From Dictionary
        private void DisplayRandomKeysInTextBoxes()
        {
            List<int> selectedKeys = new List<int>();
            List<string> selectedValues = new List<string>();

            //Loop to populate TextBoxes with random keys
            for (int i = 1; i <= 4; i++) //Adjust the loop to work with TextBox1 to TextBox4
            {
                if (availableRanges.Count == 0)
                {
                    //Show a warning message when all ranges have been used
                    MessageBox.Show("All ranges have been used. Start the game again", "No More Ranges", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int randomKey;
                do
                {
                    randomKey = random.Next(MinKey, MaxKey + 1); //Generates keys between 1 and 10
                } while (selectedKeys.Contains(randomKey));

                selectedKeys.Add(randomKey);

                //Find the TextBox control dynamically by name
                TextBox textBox = this.Controls.Find("textBox" + i, true).FirstOrDefault() as TextBox;

                if (textBox != null)
                {
                    //Create a random number within the range associated with the selected key
                    int startRange = (randomKey - 1) * 100;
                    int endRange = startRange + 99;
                    int randomValue = random.Next(startRange, endRange + 1);
                    textBox.Text = $"{randomKey} ({randomValue})";
                }
            }
        }
        #endregion


        //----------------------------------------------------------------------------------------------------------------//

        #region Populate The ComboBoxes
        private void PopulateComboBoxesWithAllValues(ComboBox[] comboBoxes)
        {
            //Create a list of all values from the 'randomDict'
            List<string> allValues = new List<string>(randomDict.Values);

            //Shuffle the values to randomize their order
            RandomizeList(allValues);

            //Populate each combo box with all 10 values
            foreach (var comboBox in comboBoxes)
            {
                comboBox.Items.Clear(); //Clear any existing items
                comboBox.Items.AddRange(allValues.ToArray()); //Add all values to the combo box
                comboBox.SelectedIndex = -1; //Deselect any pre-selected value
            }
        }
        #endregion


        //----------------------------------------------------------------------------------------------------------------//

        #region Shuffle The List And Shuffle TextBoxes
        // Helper method to shuffle a list
        private void RandomizeList<T>(List<T> list)
        {
            var shuffledList = list.OrderBy(item => random.Next()).ToList();
            list.Clear();
            list.AddRange(shuffledList);
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Buttons
        private void btnStart_Click(object sender, EventArgs e)
        {
            DisplayRandomKeysInTextBoxes();

            ComboBox[] comboBoxes = { comboBox1, comboBox2, comboBox3, comboBox4 };
            PopulateComboBoxesWithAllValues(comboBoxes);
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            //Create an array of the combo boxes
            ComboBox[] comboBoxes = { comboBox1, comboBox2, comboBox3, comboBox4 };
            //Create an array of text boxes
            TextBox[] textBoxes = { textBox1, textBox2, textBox3, textBox4 };

            //Initialize a flag to check if all values match
            bool match = true;

            //Check if the values in combo boxes match the displayed key ranges in text boxes
            for (int i = 0; i < comboBoxes.Length; i++)
            {
                if (comboBoxes[i].SelectedIndex >= 0)
                {
                    //Get the selected value from the combo box
                    string selectedValue = comboBoxes[i].SelectedItem.ToString();
                    //Get the displayed key range from the text box
                    string textBoxDisplay = textBoxes[i].Text;
                    //Extract the key (range) from the displayed text
                    int textBoxKey = int.Parse(textBoxDisplay.Split('(')[0]);

                    //Check if the selected value matches the key
                    if (!randomDict.ContainsKey(textBoxKey) || randomDict[textBoxKey] != selectedValue)
                    {
                        match = false;
                        break;
                    }
                }
            }

            //Display a message based on whether there's a match
            if (match)
            {
                MessageBox.Show("GREAT JOB! The Descriptions Selected Match Call Numbers.", "Match", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Oops. The Descriptions Selected Don't Match The Call Numbers. Try again.", "No Match", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //Create a new GameAttempt2 object to store the attempt information
            GameAttempt2 currentAttempt = new GameAttempt2
            {
                Difficulty = radioButtonNormal.Checked ? "Normal" : "Expert",
                Correct = match,  //Based on whether there's a match
                KeyValues = new Dictionary<int, string>()
            };

            //Add the current attempt to the gameAttempts list
            gameAttempts.Add(currentAttempt);

            //Limit the list to the last 5 attempts if needed
            if (gameAttempts.Count > 5)
            {
                gameAttempts.RemoveAt(0);
            }

            //Display the last 5 game attempts in the RichTextBox
            DisplayLast5GameAttempts();
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Dislpaying Of The Lat 5 Attempts
        private void DisplayLast5GameAttempts()
        {
            //Clear any previous display
            richTextBoxLastGameAttempts.Clear();

            //Iterate through the last 5 game attempts and display them in the RichTextBox
            for (int i = Math.Max(0, gameAttempts.Count - 5); i < gameAttempts.Count; i++)
            {
                var attempt = gameAttempts[i];
                richTextBoxLastGameAttempts.AppendText($"Attempt {i + 1} - Difficulty: {attempt.Difficulty}, Correct: {attempt.Correct}\n");

                //Display the key-value pairs
                foreach (var keyValue in attempt.KeyValues)
                {
                    richTextBoxLastGameAttempts.AppendText($"Key: {keyValue.Key}, Value: {keyValue.Value}\n");
                }

                richTextBoxLastGameAttempts.AppendText("\n");
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Radio Button Sound
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton.Checked)
            {
                //Play the sound
                soundPlayer.Play();
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Radio Buttons
        private void radioButtonNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNormal.Checked)
            {
                //Change the image in pictureBoxDeweyChart to your Normal image
                pictureBoxDeweyChart.Image = Properties.Resources.DeweyChartNormal;
            }
        }

        private void radioButtonExpert_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonExpert.Checked)
            {
                //Change the image in pictureBoxDeweyChart to your expert image
                pictureBoxDeweyChart.Image = Properties.Resources.DeweyChartExpert;
            }
        }
        #endregion
    }
}
//-----------------------------------------------------------o0oENDo0o----------------------------------------------------//