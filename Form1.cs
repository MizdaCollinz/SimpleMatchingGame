using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleMatchingGame
{
    public partial class Form1 : Form
    {
        //Clicked Labels
        Label firstClick = null;
        Label secondClick = null;


        public Form1()
        {
            InitializeComponent();
            //Run method to randomly assign the icons
            AssignIconsToSquares();
        }

        // Use this Random object to choose random icons for the squares
        Random random = new Random();

        // Each of these letters is an interesting icon
        // in the Webdings font,
        // and each icon appears twice in this list
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "~", "~", "z", "z"
        };

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                //For each of the labels
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }

            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            //The label that is clicked is input as sender
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                //Already black, ignore
                //Hidden, reveal
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                //This is the first click, assign and make visible
                if (firstClick == null)
                {
                    firstClick = clickedLabel;
                    clickedLabel.ForeColor = Color.Black;
                    return;
                }
                //Third clicks are ignored
                if (secondClick == null) {
                    //This is the second click, assign and make visible
                    secondClick = clickedLabel;
                    secondClick.ForeColor = Color.Black;
                }
                else
                {
                    return;
                }

                CheckForWinner();

                //If they match keep them visible and reset
                if ( firstClick.Text == secondClick.Text)
                {
                    firstClick = null;
                    secondClick = null;
                    return;
                }

                //Don't match, clear them after a short time
                timer1.Start();

                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Stop
            timer1.Stop();
            //Hide both icons
            firstClick.ForeColor = firstClick.BackColor;
            secondClick.ForeColor = secondClick.BackColor;
            //Nothing has been clicked now
            firstClick = null;
            secondClick = null;
        }

        private void CheckForWinner()
        {
            //Loop through labels, check if all matched 
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if(iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            //Loop didn't call return, success all icons are matched
            MessageBox.Show("You matched all the icons!", "You are Victorious!");
            Close();
        }
    }
}
