using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roulette_Payout_Calculator
{
    public partial class payoutCalculator : Form
    {
        //defining our initial variables, we want to store betTypes in an array to group them, eg. six-lines, splits
        //initialise the payout variable as 0 and the helpOpen boolean as false
        int attempts = 0; //new branch on git, adding iterative count for how many tries the user has had at the current payout
        int payout = 0;
        int[] betTypeTotals = new int[5];
        payoutInfo helpScreen = new payoutInfo();
        bool helpOpen = false;

        public payoutCalculator()
        {
            InitializeComponent();
        }

        private void payoutCalculator_Load(object sender, EventArgs e)
        {
            //initial spin
            payout = newSpin();

            //disabling the ability to resize the form
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public int newSpin()
        {
            Random rnd = new Random();
            int[] betValue = new int[12];
            bool[] chipsOn = new bool[12];
            int[] chipBetTotal = new int[12];
            int payoutTotal = 0;
            attempts = 0; //zero out the attempts count each new spin

            //disable the button each time there's a new spin because we want at least one guess to be made first
            btnGiveup.Enabled = false;

            //storing the picturebox and label elements in arrays to make referencing easier as we need to loop through them
            PictureBox[] chips = {
                pbChip1, pbChip2, pbChip3, pbChip4, pbChip5, pbChip6, pbChip7, pbChip8, pbChip9, pbChip10, pbChip11, pbChip12
            };

            Label[] betDisplay = {
                lbChip1, lbChip2, lbChip3, lbChip4, lbChip5, lbChip6, lbChip7, lbChip8, lbChip9, lbChip10, lbChip11, lbChip12
            };

            //going from top left to bottom right, these are the X:1 payout odds for each possible bet
            int[] betCalculate = {
                5, 11, 5, 8, 17, 8, 17, 35, 17, 8, 17, 8
            };

            //this loop determines the value of each chip on the layout
            for(int i = 0; i < betValue.Length; i++){
                betValue[i] = rnd.Next(1, 9);
                betDisplay[i].Text = betValue[i].ToString();
            }

            for(int i = 0; i < chips.Length; i++){
                int spawn = rnd.Next(2);

                //50/50 chance to have a chip spawn and add its value to the payout total
                if(spawn == 0){
                    chips[i].Visible = false;
                    chipsOn[i] = false;
                    betDisplay[i].Visible = false;
                } else {
                    chips[i].Visible = true;
                    chipsOn[i] = true;
                    betDisplay[i].Visible = true;
                    chipBetTotal[i] = betValue[i] * betCalculate[i];
                    payoutTotal += chipBetTotal[i];
                }
            }

            //setting all of the bet groups to the respective bet in the array, six-line, street, corner, split and straight up
            betTypeTotals[0] = betValue[0] + betValue[2];
            betTypeTotals[1] = betValue[1];
            betTypeTotals[2] = betValue[3] + betValue[5] + betValue[9] + betValue[11];
            betTypeTotals[3] = betValue[4] + betValue[6] + betValue[8] + betValue[10];
            betTypeTotals[4] = betValue[7];

            Console.WriteLine($"Payout Total: {payoutTotal}");
            return payoutTotal;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            attempts++; //increase the amount of attempts the user has had as soon as they press submit
            //compare the value in the textbox to the value we have stored in 'payout'
            //we always clear the textbox after any guess
            if(tbAnswer.Text == payout.ToString()){
                MessageBox.Show($"Correct! The total payout was {payout}. You attempted this payout {attempts} time(s).");
                payout = newSpin();
            } else {
                wrongAnswer();
            }

            tbAnswer.Text = "";
        }

        public void wrongAnswer(){
            //let the user know their answer was wrong and enable the give up button
            MessageBox.Show("Incorrect. Please check your answer and try again.");
            btnGiveup.Enabled = true;
        }

        private void btnGiveup_Click(object sender, EventArgs e)
        {
            //only clickable after at least one wrong answer, will end the current spin and generate a new one
            MessageBox.Show($"You gave up. The total of the payout was {payout}.");
            tbAnswer.Text = "";
            payout = newSpin();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //flip the current state of helpOpen and either display or hide the payout help screen
            if (!helpOpen){
                helpScreen.Show();
                helpOpen = true;
            } else {
                helpScreen.Hide();
                helpOpen = false;
            }
        }
    }
}
