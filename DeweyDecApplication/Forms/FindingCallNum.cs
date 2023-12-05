using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DeweyDecApplication.MyRedBlackTree;
using static System.Windows.Forms.LinkLabel;

//-------------------------------------------------------o0START0o------------------------------------------------//

namespace DeweyDecApplication.Forms
{
    public partial class FindingCallNum : Form
    {
        #region VARIABLES
        private MyRedBlackTree redBlackTree;
        private string[] lines;
        private string filePath;
        private string descriptionLevel1;
        private List<int> answerOptions;
        private int motherKey;
        private Random random = new Random();
        private bool gameStarted = false;
        private Timer timer;
        private DateTime startTime;
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region INITIALIZE COMPONENT
        public FindingCallNum()
        {
            InitializeComponent();
            // Initialize the Timer
            timer = new Timer();
            timer.Interval = 1000; // 1 second interval
            timer.Tick += Timer_Tick;
            // Initialize the Red-Black Tree
            redBlackTree = new MyRedBlackTree();
            // File path to the txt
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TreeData.txt");
            lines = File.ReadAllLines(filePath);
            // Initialize the list to hold answer options
            answerOptions = new List<int>();
            // Disable options buttons initially
            DisableOptionsButtons();
            // Set up event handler for Start button
            buttonStart.Click += ButtonStart_Click;
            buttonRestart.Click += buttonRestart_Click;
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region START GAME
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            // Start the game
            StartGame();
        }

        //----------------------------------------------------------------------------------------------------------------//

        private void StartGame()
        {
            // Enable options buttons
            EnableOptionsButtons();

            // Read file and add to the tree
            readFileAndAddToTree();

            // Set the timer interval based on the selected difficulty
            SetTimerInterval();

            // Start the timer
            startTime = DateTime.Now;
            timer.Start();

            // Generate and display the first set of questions
            generateAnswerOptions();
            displayTreeData();

            // Set game started flag
            gameStarted = true;
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region READ FILE AND ADD TO TREE (LEVEL 3)
        private void readFileAndAddToTree()
        {
            foreach (string line in lines)
            {
                string[] parts = line.Split('.');

                if (parts.Length == 2 && int.TryParse(parts[0], out int data))
                {
                    string desc = parts[1];
                    redBlackTree.Insert(data, desc);
                }
                else
                {
                    Console.WriteLine("Error " + line);
                }
            }

            Random random = new Random();
            Node treeNode = null;
            int labelKey = -1;

            while (treeNode == null || labelKey % 10 == 0)
            {
                labelKey = random.Next(100, 933);
                treeNode = redBlackTree.Find(labelKey);
            }

            descriptionLevel1 = treeNode.desc;
            motherKey = treeNode.data;
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region DISPLAY TREE DATA
        private void displayTreeData()
        {
            textBoxDescription.Text = descriptionLevel1;
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region TIMER INTERVAL
        private void SetTimerInterval()
        {
            // Set the timer interval based on the selected difficulty
            string selectedDifficulty = comboBoxDifficultyLevel.SelectedItem.ToString();

            switch (selectedDifficulty)
            {
                case "Normal":
                    timer.Interval = 1000; // 1 second interval (same as before)
                    break;
                case "Hard":
                    timer.Interval = 5000; // 5 seconds interval
                    break;
                case "Impossible":
                    timer.Interval = 3000; // 3 seconds interval
                    break;
                    // Add more cases if needed
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region GENERATE THE FIRST OPTIONS (LEVEL 1 OPTIONS)
        private void generateAnswerOptions()
        {
            // Clear previous answer options
            answerOptions.Clear();

            Node useNode = redBlackTree.Find(int.Parse(motherKey.ToString()[0] + "00"));
            int correctKey = useNode.data;

            // Generate three unique labelKeys divisible by 100 (excluding the correct answer)
            List<int> candidateKeys = new List<int>();

            // Ensure the correct answer is included
            candidateKeys.Add(correctKey);

            // List to hold the buttons
            List<Button> answerButtons = new List<Button>
            {
                buttonAnswer1,
                buttonAnswer2,
                buttonAnswer3,
                buttonAnswer4
            };

            for (int i = 2; i <= 4; i++)
            {
                int labelKey;
                Node treeNode;

                do
                {
                    labelKey = random.Next(100, 1000);
                } while (labelKey % 100 != 0 || candidateKeys.Contains(labelKey) || (treeNode = redBlackTree.Find(labelKey)) == null);

                candidateKeys.Add(labelKey);

                // Assign values to buttons randomly
                Button randomButton = answerButtons[random.Next(answerButtons.Count)];
                randomButton.Text = $"{labelKey} - {treeNode.desc}";

                // Remove the used button from the list
                answerButtons.Remove(randomButton);

                // Save the correct answer index
                if (labelKey == correctKey)
                {
                    answerOptions.Add(i);
                }
            }

            // Shuffle the answer options
            Shuffle(answerOptions);

            // Assign the correct answer to a random button
            Button correctButton = answerButtons[random.Next(answerButtons.Count)];
            correctButton.Text = $"{correctKey} - {useNode.desc}";
            answerOptions.Insert(0, answerButtons.IndexOf(correctButton) + 1); // Correct answer index
        }

        private void Shuffle(List<int> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region GENERATE NEXT OPTIONS (LEVEL 2 OPTIONS)
        private void generateNextOptions()
        {
            // Generate three unique labelKeys divisible by 10 (including the correct answer)
            List<int> nextOptions = new List<int>();

            Node treeNode = null;

            // List to hold the buttons
            List<Button> answerButtons = new List<Button>
            {
                buttonNextOption1,
                buttonNextOption2,
                buttonNextOption3,
                buttonNextOption4
            };

            // Shuffle the buttons
            ShuffleButtons(answerButtons);

            // Generate an option with the same first two digits as motherKey and divisible by 10
            int firstTwoDigits = int.Parse(motherKey.ToString().Substring(0, 2));
            int labelKey = 0;

            int correctK = int.Parse(firstTwoDigits.ToString() + "0");
            Node tempNode = null;
            tempNode = redBlackTree.Find(correctK);
            answerButtons[0].Text = $"{tempNode.data}, {tempNode.desc}";
            answerButtons.Remove(answerButtons[0]);

            foreach (Button button in answerButtons)
            {
                button.Text = "Incorrect answer";

                int key = -69;

                while (treeNode == null)
                {
                    
                    key = int.Parse($"{random.Next(100, 900)}"); // Adjusted range to be between 10 and 50 (inclusive)

                    // Check if the key is divisible by 10 and not in the usedKeys list
                    if (key % 10 == 0 && key != correctK && key % 100 != 0)
                    {
                        treeNode = redBlackTree.Find(key);
                    }
                }

                button.Text = $"{treeNode.data}, {treeNode.desc}";
                treeNode = null;

            }

            // Check if all answers are correct
            if (AllAnswersCorrect())
            {
                // Stop the timer
                timer.Stop();

            }
        }

        private void ShuffleButtons(List<Button> buttons)
        {
            int n = buttons.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Button value = buttons[k];
                buttons[k] = buttons[n];
                buttons[n] = value;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region BUTTONS
        private void buttonAnswer1_Click(object sender, EventArgs e)
        {
            if (gameStarted && motherKey.ToString()[0] == buttonAnswer1.Text.Split('.')[0][0])
            {
                MessageBox.Show("Well Done!! You've selected the correct answer. \nYou can now select the last option that matches.");
                // Change the color of the correct button to green
                buttonAnswer1.BackColor = Color.Green;

                generateNextOptions();
            }
            else if (gameStarted)
            {
                // Change the color of the incorrect button to red
                buttonAnswer1.BackColor = Color.Red;
                MessageBox.Show("Try again");
            }
        }

        private void buttonAnswer2_Click(object sender, EventArgs e)
        {
            if (gameStarted && motherKey.ToString()[0] == buttonAnswer2.Text.Split('.')[0][0])
            {
                MessageBox.Show("Well Done!! You've selected the correct answer. \nYou can now select the last option that matches.");
                // Change the color of the correct button to green
                buttonAnswer2.BackColor = Color.Green;

                generateNextOptions();
            }
            else if (gameStarted)
            {
                // Change the color of the incorrect button to red
                buttonAnswer2.BackColor = Color.Red;
                MessageBox.Show("Try again");
            }
        }

        private void buttonAnswer3_Click(object sender, EventArgs e)
        {
            if (gameStarted && motherKey.ToString()[0] == buttonAnswer3.Text.Split('.')[0][0])
            {
                MessageBox.Show("Well Done!! You've selected the correct answer. \nYou can now select the last option that matches.");
                // Change the color of the correct button to green
                buttonAnswer3.BackColor = Color.Green;

                generateNextOptions();
            }
            else if (gameStarted)
            {
                // Change the color of the incorrect button to red
                buttonAnswer3.BackColor = Color.Red;
                MessageBox.Show("Try again");
            }
        }

        private void buttonAnswer4_Click(object sender, EventArgs e)
        {
            if (gameStarted && motherKey.ToString()[0] == buttonAnswer4.Text.Split('.')[0][0])
            {
                MessageBox.Show("Well Done!! You've selected the correct answer. \nYou can now select the last option that matches.");
                // Change the color of the correct button to green
                buttonAnswer4.BackColor = Color.Green;

                generateNextOptions();
            }
            else if (gameStarted)
            {
                // Change the color of the incorrect button to red
                buttonAnswer4.BackColor = Color.Red;
                MessageBox.Show("Try again");
            }
        }

        //----------------------------------------------------------------------------------------------------------------//

        private void DisableOptionsButtons()
        {
            buttonAnswer1.Enabled = false;
            buttonAnswer2.Enabled = false;
            buttonAnswer3.Enabled = false;
            buttonAnswer4.Enabled = false;
        }

        private void EnableOptionsButtons()
        {
            buttonAnswer1.Enabled = true;
            buttonAnswer2.Enabled = true;
            buttonAnswer3.Enabled = true;
            buttonAnswer4.Enabled = true;
        }

        //----------------------------------------------------------------------------------------------------------------//

        private void buttonNextOption1_Click(object sender, EventArgs e)
        {
            if (buttonNextOption1.Text.ToString().Substring(0, 2) == (motherKey.ToString().Substring(0, 2)))
            {
                // Stop the timer
                timer.Stop();

                // Display the elapsed time
                TimeSpan elapsedTime = DateTime.Now - startTime;
                MessageBox.Show($"Congratulations! You've completed the game in {elapsedTime.Minutes} minutes and {elapsedTime.Seconds} seconds.");
                buttonNextOption1.BackColor = Color.Green;
            }
            else
            {
                MessageBox.Show("GAME FAILED :(");
                buttonNextOption1.BackColor = Color.Red;
            }
        }

        private void buttonNextOption2_Click(object sender, EventArgs e)
        {
            if (buttonNextOption2.Text.ToString().Substring(0, 2) == (motherKey.ToString().Substring(0, 2)))
            {
                // Stop the timer
                timer.Stop();

                // Display the elapsed time
                TimeSpan elapsedTime = DateTime.Now - startTime;
                MessageBox.Show($"Congratulations! You've completed the game in {elapsedTime.Minutes} minutes and {elapsedTime.Seconds} seconds.");
                buttonNextOption2.BackColor = Color.Green;
            }
            else
            {
                MessageBox.Show("GAME FAILED :(");
                buttonNextOption2.BackColor = Color.Red;
            }
        }

        private void buttonNextOption3_Click(object sender, EventArgs e)
        {
            if (buttonNextOption3.Text.ToString().Substring(0, 2) == (motherKey.ToString().Substring(0, 2)))
            {
                // Stop the timer
                timer.Stop();

                // Display the elapsed time
                TimeSpan elapsedTime = DateTime.Now - startTime;
                MessageBox.Show($"Congratulations! You've completed the game in {elapsedTime.Minutes} minutes and {elapsedTime.Seconds} seconds.");
                buttonNextOption3.BackColor = Color.Green;
            }
            else
            {
                MessageBox.Show("GAME FAILED :(");
                buttonNextOption3.BackColor = Color.Red;
            }
        }

        private void buttonNextOption4_Click(object sender, EventArgs e)
        {
            if (buttonNextOption4.Text.ToString().Substring(0, 2) == (motherKey.ToString().Substring(0, 2)))
            {
                // Stop the timer
                timer.Stop();

                // Display the elapsed time
                TimeSpan elapsedTime = DateTime.Now - startTime;
                MessageBox.Show($"Congratulations! You've completed the game in {elapsedTime.Minutes} minutes and {elapsedTime.Seconds} seconds.");
                buttonNextOption3.BackColor = Color.Green;
            }
            else
            {
                MessageBox.Show("GAME FAILED :(");
                buttonNextOption3.BackColor = Color.Red;
            }
        }

        //----------------------------------------------------------------------------------------------------------------//

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
            // Stop the timer
            timer.Stop();
        }

        //----------------------------------------------------------------------------------------------------------------//

        private void RestartGame()
        {
            // Reset game state
            gameStarted = false;
            // Clear previous answer options
            answerOptions.Clear();
            // Disable options buttons
            DisableOptionsButtons();
            // Reset button colors
            ResetButtonColors();
            // Clear button texts
            buttonAnswer1.Text = "";
            buttonAnswer2.Text = "";
            buttonAnswer3.Text = "";
            buttonAnswer4.Text = "";
            buttonNextOption1.Text = "";
            buttonNextOption2.Text = "";
            buttonNextOption3.Text = "";
            buttonNextOption4.Text = "";
            // Clear description text
            textBoxDescription.Text = "";
            // Clear motherKey
            motherKey = 0;
            // Reset descriptionLevel1
            descriptionLevel1 = "";
            // Clear the timer
            timer.Stop();
            timer.Dispose();
            timer = new Timer();
            // Clear any displayed messages
            MessageBox.Show("Game has reset.");
        }

        private void ResetButtonColors()
        {
            buttonAnswer1.BackColor = SystemColors.Control;
            buttonAnswer2.BackColor = SystemColors.Control;
            buttonAnswer3.BackColor = SystemColors.Control;
            buttonAnswer4.BackColor = SystemColors.Control;
            buttonNextOption1.BackColor = SystemColors.Control;
            buttonNextOption2.BackColor = SystemColors.Control;
            buttonNextOption3.BackColor = SystemColors.Control;
            buttonNextOption4.BackColor = SystemColors.Control;
        }

        //----------------------------------------------------------------------------------------------------------------//

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Calculate elapsed time
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // Display elapsed time in the labelTimer
            labelTimer.Text = $"Time: {elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}";

            // Check if all answers are correct
            if (AllAnswersCorrect())
            {
                // Stop the timer
                timer.Stop();

                // Congratulate the user
                MessageBox.Show($"Congratulations! You've completed the game in {elapsedTime.Minutes} minutes and {elapsedTime.Seconds} seconds.");
            }

            // Check if the elapsed time has reached the limit for the selected difficulty
            string selectedDifficulty = comboBoxDifficultyLevel.SelectedItem.ToString();
            switch (selectedDifficulty)
            {
                case "Normal":
                    // No additional logic for Normal difficulty
                    break;
                case "Hard":
                    if (elapsedTime.TotalSeconds >= 5)
                    {
                        // Disable buttons if 5 seconds have passed
                        DisableOptionsButtons();
                    }
                    break;
                case "Impossible":
                    if (elapsedTime.TotalSeconds >= 3)
                    {
                        // Disable buttons if 3 seconds have passed
                        DisableOptionsButtons();
                    }
                    break;
                    // Add more cases if needed
            }
        }

        //----------------------------------------------------------------------------------------------------------------//

        private void buttonStart_Click_1(object sender, EventArgs e)
        {
            // Start the timer
            startTime = DateTime.Now;
            timer.Start();
        }
        private bool AllAnswersCorrect()
        {
            // Check if all buttons are green
            return
                buttonAnswer1.BackColor == Color.Green &&
                buttonAnswer2.BackColor == Color.Green &&
                buttonAnswer3.BackColor == Color.Green &&
                buttonAnswer4.BackColor == Color.Green;
        }
        #endregion
    }
}

//------------------------------------------------------o0END0o-----------------------------------------------------//