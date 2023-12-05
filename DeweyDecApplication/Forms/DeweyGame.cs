using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

//-------------------------------------------------------o0oSTARTo0o----------------------------------------------------//
namespace DeweyDecApplication.Forms
{
    public partial class DeweyGame : Form
    {
        //Lists to store information
        private List<PictureBox> pictureBoxList = new List<PictureBox>();
        private List<double> deweyDecNum = new List<double>();
        private List<GameAttempt> lastFiveAttempts = new List<GameAttempt>();
        private List<string> authorInitials = new List<string>();

        private PictureBox currentPictureBox = null;
        private Point originalMouseLocation;
        private Point originalPictureBoxLocation;
        private FlowLayoutPanel originalParent;
        private int gameDuration = 60;
        private Random random = new Random();
        private Stopwatch gameTimerLbl = new Stopwatch();
        private Timer timer;
        private SoundPlayer soundPlayer = new SoundPlayer();

        #region Constructor
        public DeweyGame()
        {
            InitializeComponent();
            soundPlayer.Stream = Properties.Resources.ClickSound;

            pictureBoxList.Add(picBoxBookOne);
            pictureBoxList.Add(picBoxBookTwo);
            pictureBoxList.Add(picBoxBookThree);
            pictureBoxList.Add(picBoxBookFour);
            pictureBoxList.Add(picBoxBookFive);
            pictureBoxList.Add(picBoxBookSix);
            pictureBoxList.Add(picBoxBookSeven);
            pictureBoxList.Add(picBoxBookEight);
            pictureBoxList.Add(picBoxBookNine);
            pictureBoxList.Add(picBoxBookTen);

            foreach (PictureBox pictureBox in pictureBoxList)
            {
                pictureBox.MouseDown += PictureBox_MouseDown;
                pictureBox.MouseMove += PictureBox_MouseMove;
            }

            flowLayoutPanel2.AllowDrop = true;
            flowLayoutPanel2.DragEnter += flowLayoutPanel2_DragEnter;
            flowLayoutPanel2.DragDrop += flowLayoutPanel2_DragDrop;

            //Event handlers for radio buttons
            radioButtonBeginner.CheckedChanged += RadioButtonDifficulty_CheckedChanged;
            radioButtonNormal.CheckedChanged += RadioButtonDifficulty_CheckedChanged;
            radioButtonExperienced.CheckedChanged += RadioButtonDifficulty_CheckedChanged;

            radioButtonBeginner.CheckedChanged += RadioButton_CheckedChanged;
            radioButtonNormal.CheckedChanged += RadioButton_CheckedChanged;
            radioButtonExperienced.CheckedChanged += RadioButton_CheckedChanged;

            //Create and configure the Timer
            timer = new Timer();
            timer.Interval = 1000; //1 second
            timer.Tick += Timer_Tick; //Subscribe to the Tick event

            timer.Start();
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Mouse Down
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentPictureBox = (PictureBox)sender;
                originalMouseLocation = e.Location;
                originalPictureBoxLocation = currentPictureBox.Location;
                originalParent = (FlowLayoutPanel)currentPictureBox.Parent;
                currentPictureBox.DoDragDrop(currentPictureBox, DragDropEffects.Move);
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Mouse Move
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentPictureBox != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    int deltaX = e.X - originalMouseLocation.X;
                    int deltaY = e.Y - originalMouseLocation.Y;
                    currentPictureBox.Left = originalPictureBoxLocation.X + deltaX;
                    currentPictureBox.Top = originalPictureBoxLocation.Y + deltaY;
                }
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Flow Layout Panel2 Drag Enter
        private void flowLayoutPanel2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Flow Layout Panel2 Drag Drop
        private void flowLayoutPanel2_DragDrop(object sender, DragEventArgs e)
        {
            if (currentPictureBox != null)
            {
                FlowLayoutPanel destinationPanel = (FlowLayoutPanel)sender;
                if (destinationPanel != originalParent)
                {
                    originalParent.Controls.Remove(currentPictureBox);
                    destinationPanel.Controls.Add(currentPictureBox);
                    destinationPanel.Controls.Add(currentPictureBox);
                    currentPictureBox.Location = destinationPanel.PointToClient(new Point(e.X, e.Y));

                    UpdateTheProgressBar();
                }
            }
            else
            {
                currentPictureBox.Location = originalPictureBoxLocation;
            }
            currentPictureBox = null;
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Check Order Of Books
        private void CheckBooksOrder()
        {
            double previousDeweyDec = -1.0; //Initialize with a value lower than any possible deweyDec
            bool isCorrectOrder = true;

            foreach (PictureBox pictureBox in flowLayoutPanel2.Controls.OfType<PictureBox>())
            {
                if (pictureBox.Controls.Count > 0 && pictureBox.Controls[0] is Label label)
                {
                    if (double.TryParse(label.Text, out double deweyDec))
                    {
                        if (deweyDec < previousDeweyDec)
                        {
                            isCorrectOrder = false;
                            break;
                        }
                        previousDeweyDec = deweyDec;
                    }
                }
            }

            int timeTakenInSeconds = gameDuration - (int)gameTimerLbl.Elapsed.TotalSeconds;

            if (isCorrectOrder)
            {
                MessageBox.Show($"Congratulations! You've placed the books in the correct order. \r\nYou have {timeTakenInSeconds} seconds to spare.");
                //Record the attempt when the user completes the task
                GameAttempt attempt = new GameAttempt
                {
                    Timestamp = DateTime.Now,
                    TimeTakenInSeconds = timeTakenInSeconds,
                    Difficulty = GetSelectedDifficulty()
                };

                lastFiveAttempts.Add(attempt);

                //Keep only the last 5 attempts
                if (lastFiveAttempts.Count > 5)
                {
                    lastFiveAttempts.RemoveAt(0);
                }

                //Update the display of the last 5 attempts
                UpdateLastFiveAttemptsDisplay();
            }
            else
            {
                MessageBox.Show("The books are not in the correct order.\r\nRestart the game.");
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Updating Progress Bar
        private void UpdateTheProgressBar()
        {
            int pictureBoxCount = flowLayoutPanel2.Controls.OfType<PictureBox>().Count();
            progressBarBookShelf.Maximum = pictureBoxList.Count;

            //Ensure that the progress bar value doesn't exceed the total number of PictureBoxes.
            int progressBarValue = Math.Min(pictureBoxCount, pictureBoxList.Count);
            progressBarBookShelf.Value = progressBarValue;

            //Check if the user has dragged all 10 books to the other panel.
            if (progressBarValue == pictureBoxList.Count)
            {
                //Stop the timer
                timer.Stop();

                //Check the order of the books
                CheckBooksOrder();

                //Reset the game timer
                gameTimerLbl.Reset();

                //Clear the time label
                lblTimeLeft.Text = "00:00";
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Timer Tick
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTimeLabel();
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Get Selected Difficulty
        private string GetSelectedDifficulty()
        {
            if (radioButtonBeginner.Checked)
            {
                return "Beginner";
            }
            else if (radioButtonNormal.Checked)
            {
                return "Normal";
            }
            else if (radioButtonExperienced.Checked)
            {
                return "Experienced";
            }

            //Default value 
            return "Unknown";
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Update the List Box With Last 5 Attempts
        private void UpdateLastFiveAttemptsDisplay()
        {
            listBoxLastAttempts.Items.Clear();

            foreach (var attempt in lastFiveAttempts)
            {
                listBoxLastAttempts.Items.Add($"Difficulty: {attempt.Difficulty}, Time Left: {attempt.TimeTakenInSeconds} seconds");
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Generating Author Initial
        private string GetRandomAuthorInitial()
        {
            
            char firstNameInitial = (char)random.Next('A', 'Z' + 1);
            char lastNameInitial = (char)random.Next('A', 'Z' + 1);

            return $"{firstNameInitial}. {lastNameInitial}";
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Button To Start
        private void btnGameStart_Click(object sender, EventArgs e)
        {
            foreach (PictureBox pictureBox in pictureBoxList)
            {
                pictureBox.Controls.Clear();
            }

            deweyDecNum.Clear();

            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                double deweyDecNums = random.NextDouble() * 1000;
                deweyDecNum.Add(deweyDecNums);

                //Generate random author initial
                string authorInitial = GetRandomAuthorInitial();

                authorInitials.Add(authorInitial);

                //Dewey Decimal Label
                Label deweyLabel = new Label();
                deweyLabel.Text = deweyDecNums.ToString("F2");
                deweyLabel.BackColor = Color.Beige;

                deweyLabel.Location = new System.Drawing.Point(10, 10);

                Size textSizeDewey = TextRenderer.MeasureText(deweyLabel.Text, deweyLabel.Font);
                deweyLabel.Size = textSizeDewey;

                //Author Initial Label
                Label authorLabel = new Label();
                authorLabel.Text = authorInitial;
                authorLabel.BackColor = Color.Beige;

                //Position Author Initial label below Dewey Decimal label
                authorLabel.Location = new System.Drawing.Point(10, 10 + textSizeDewey.Height + 5);

                Size textSizeAuthor = TextRenderer.MeasureText(authorLabel.Text, authorLabel.Font);
                authorLabel.Size = textSizeAuthor;

                PictureBox pictureBox = pictureBoxList[i];

                int centerX = (pictureBox.Width - textSizeDewey.Width) / 2;
                int centerY = (pictureBox.Height - (textSizeDewey.Height + 5 + textSizeAuthor.Height)) / 2;

                deweyLabel.Location = new Point(centerX, centerY);
                authorLabel.Location = new Point(centerX, centerY + textSizeDewey.Height + 5);

                pictureBox.Controls.Add(deweyLabel);
                pictureBox.Controls.Add(authorLabel);
            }
            gameTimerLbl.Restart();
            UpdateTheProgressBar();
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Update Time Label
        private void UpdateTimeLabel()
        {
            int timeRemaining = gameDuration - (int)gameTimerLbl.Elapsed.TotalSeconds;
            int progressBarValue = Math.Max(0, gameDuration - timeRemaining);

            //Display the remaining time in the label
            TimeSpan remainingTimeSpan = TimeSpan.FromSeconds(timeRemaining);
            lblTimeLeft.Text = remainingTimeSpan.ToString(@"mm\:ss");

            if (timeRemaining <= 0)
            {
                gameTimerLbl.Stop();
                timer.Stop();
                MessageBox.Show("Time's up! Game over.");
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

        #region Game Difficulty Settings
        private void RadioButtonDifficulty_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonBeginner.Checked)
            {
                //Set game duration for Beginner 2 minutes
                gameDuration = 120;
            }
            else if (radioButtonNormal.Checked)
            {
                //Set game duration for Normal 1 minute
                gameDuration = 60;
            }
            else if (radioButtonExperienced.Checked)
            {
                //Set game duration for Experienced 30 seconds
                gameDuration = 30;
            }
        }
        #endregion

        #region Radio Buttons
        private void radioButtonBeginner_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonNormal_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonExperienced_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Restart Game
        private void RestartGame()
        {
            //Reset the progress bar
            progressBarBookShelf.Value = 0;

            //Move all PictureBoxes back to their original panel
            foreach (PictureBox pictureBox in pictureBoxList)
            {
                flowLayoutPanel1.Controls.Add(pictureBox); //Assuming flowLayoutPanel1 is the original panel
                pictureBox.Location = new Point(0, 0); //Reset the PictureBox location
            }

            //Clear any labels or other controls inside the PictureBoxes
            foreach (PictureBox pictureBox in pictureBoxList)
            {
                pictureBox.Controls.Clear();
            }

            //Reset game-related variables
            gameTimerLbl.Reset();
            lblTimeLeft.Text = "00:00"; //Reset the time label to "00:00"

            //Restart the timer if it was previously stopped
            if (!timer.Enabled)
            {
                timer.Start();
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//

        #region Restart Button
        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------------//
    }
}
//-----------------------------------------------------------o0oENDo0o----------------------------------------------------//