using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnigmaMachine
{
    public partial class Form1 : Form
    {

       // const string testString = "PenguinsKnowYourThoughts";
        const string testString = "b";
        const string testStringDecrypt = "FEFVGBRJQNGLHIXMVGMBOPFF";

     //   const string rotorZ =   "01234567890123456789012345";
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string rotorI =   "EKMFLGDQVZNTOWYHXUSPAIBRCJ"; //I
        const string rotorII =  "AJDKSIRUXBLHWTMCQGZNPYFVOE"; //II
        const string rotorIII = "BDFHJLCPRTXVZNYEIWGAKMUSQO"; //III
        const int rotorINotch = 17;
        const int rotorIINotch = 5;
        const int rotorIIINotch = 22;
       //const string rotor3in = "JCRBIAPSUXHYWOTNZVQDGLFMKE"; //I
       //const string rotor2in = "EOVFYPNZGQCMTWHLBXURISKDJA"; //II
       //const string rotor1in = "OQSUMKAGWIEYNZVXTRPCLJHFDB"; //III


       string rotorLeft = "";
        string rotorMid = "";
        string rotorRight = "";


        //      Rotor Notch   Effect
        //      I   Q If rotor steps from Q to R, the next rotor is advanced idx 17
        //      II  E If rotor steps from E to F, the next rotor is advanced idx 5
        //      III V If rotor steps from V to W, the next rotor is advanced idx 22
        //      IV  J If rotor steps from J to K, the next rotor is advanced
        //      V   Z If rotor steps from Z to A, the next rotor is advanced
        const string reflectorA = "EJMZALYXVBWFCRQUONTSPIKHGD";
        const string reflectorB = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
        const string plugboard1 = "";
        const string plugboard2= "";

        const int initialPositionRotor1 = -1;
        const int initialPositionRotor2 = 0;
        const int initialPositionRotor3 = 0;

      //  int currentRotorRight;
      //  int currentRotorMid;
      //  int currentRotorLeft;

        EnigmaRotor erRight, erMid, erLeft;

        public Form1()
        {
            InitializeComponent();
            this.textBox2.Text = testString;
            rotorLeft = rotorI;
            rotorMid = rotorII;
            rotorRight = rotorIII;
            erRight = new EnigmaRotor(rotorIII, rotorIIINotch, initialPositionRotor1);
            erMid = new EnigmaRotor(rotorII, rotorIINotch, initialPositionRotor2);
            erLeft = new EnigmaRotor(rotorI, rotorINotch, initialPositionRotor3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
             startConversion(this.textBox2.Text);

           // startConversion(testStringDecrypt);
        }

        private void startConversion(string message)
        {
         //   currentRotorRight = initialPositionRotor1;
          //  currentRotorMid = initialPositionRotor2;
          //  currentRotorLeft = initialPositionRotor3;

            string res = "";

            for (int i = 0; i < 1; i++)
            {

           foreach(char letter in message.ToUpper())
            {
             //       System.Diagnostics.Debug.WriteLine("Encoding " + letter);
                res += encodeLetter(letter);
            }
                res += Environment.NewLine;
           

            }
            textBox1.Text = (res);
        }

        private char encodeLetter(char s)
        {
            //increment the rotors
            erRight.increment();

            //  currentRotorRight = (currentRotorRight + 1) % 26;
           // currentRotorRight = erRight.currentPos;
            if (erRight.currentPos == erRight.RotorNotch)
            {
                erMid.increment();
            }
            if (erMid.currentPos == erMid.RotorNotch)
            {
                erLeft.increment();
            }

            //report the letters L to Right
           System.Diagnostics.Debug.WriteLine("Letters: " + alphabet[erLeft.currentPos] + alphabet[erMid.currentPos] + alphabet[erRight.currentPos]);

            char afterRightIn = encodeLetterRotorRightIn(s);
            System.Diagnostics.Debug.WriteLine("Right Rotor (" + alphabet[erRight.currentPos] + ") " + s + " -> "+ afterRightIn);
            char afterMidIn = encodeLetterRotorMidIn(afterRightIn);
            System.Diagnostics.Debug.WriteLine("Middle Rotor (" + alphabet[erMid.currentPos] + ") " + afterRightIn + " -> " + afterMidIn);

          //  System.Diagnostics.Debug.WriteLine("afterMidIn in" + afterMidIn);


            char afterLeftIn = encodeLetterRotorLeftIn(afterMidIn);
            System.Diagnostics.Debug.WriteLine("Middle Rotor (" + alphabet[erLeft.currentPos] + ") " + afterMidIn + " -> " + afterLeftIn);

           // System.Diagnostics.Debug.WriteLine("afterLeftIn in" + afterLeftIn);

            char afterReflector = encodeLetterReflector(afterLeftIn);

            System.Diagnostics.Debug.WriteLine("Middle Rotor (" + alphabet[erLeft.currentPos] + ") " + afterLeftIn + " -> " + afterReflector);

           // System.Diagnostics.Debug.WriteLine("afterReflector" + afterReflector);

            char afterLeftOut = encodeLetterRotorLeftOut(afterReflector);
            System.Diagnostics.Debug.WriteLine("Left Rotor out (" + alphabet[erLeft.currentPos] + ") " + afterReflector + " -> " + afterLeftOut);

            //System.Diagnostics.Debug.WriteLine("afterLeftOut" + afterLeftOut);

            char afterMidOut = encodeLetterRotorMidOut(afterLeftOut);
            System.Diagnostics.Debug.WriteLine("Mid Rotor out (" + alphabet[erMid.currentPos] + ") " + afterLeftOut + " -> " + afterMidOut);

          //  System.Diagnostics.Debug.WriteLine("afterMidOut" + afterMidOut);


            char afterRightOut = encodeLetterRotorRightOut(afterMidOut);
            System.Diagnostics.Debug.WriteLine("Right Rotor out (" + alphabet[erRight.currentPos] + ") " + afterMidOut + " -> " + afterRightOut);

         //   System.Diagnostics.Debug.WriteLine("afterRightOut" + afterRightOut);


            char final = afterRightOut;
            return final;

        }

        private char encodeLetterRotorRightIn(char s)
        {
            int charVal = ((int)(s) - (int)('A')) % 26;
            int adjustedVal = (charVal + erRight.currentPos) % 26;
        //    System.Diagnostics.Debug.WriteLine(currentRotorRight);
            //  System.Diagnostics.Debug.WriteLine(charVal);
         //   System.Diagnostics.Debug.WriteLine("idx res rotor 1" + adjustedVal);
            char sNew = (rotorRight[adjustedVal]);
          //  System.Diagnostics.Debug.WriteLine( (char)(sNew - currentRotorRight));
         //   System.Diagnostics.Debug.WriteLine("res rotor 1 " + sNew);
            return (char)(sNew - erRight.currentPos);
        }

        private char encodeLetterRotorMidIn(char s)
        {
            int charVal = ((int)(s) - (int)('A')) % 26;
            int adjustedVal = (charVal + erMid.currentPos) % 26;
          //  System.Diagnostics.Debug.WriteLine(currentRotorMid);
            //  System.Diagnostics.Debug.WriteLine(charVal);
         //   System.Diagnostics.Debug.WriteLine("idx res rotor 3" + adjustedVal);
            char sNew = (rotorMid[adjustedVal]);
          //  System.Diagnostics.Debug.WriteLine((sNew - currentRotorMid) % 26);
            //     System.Diagnostics.Debug.WriteLine("res roto 2 " + sNew);
            return (char)(sNew - erMid.currentPos);
        }


        private char encodeLetterRotorLeftIn(char s)
        {
            int charVal = ((int)(s) - (int)('A')) % 26;
            int adjustedVal = (charVal + erLeft.currentPos) % 26;
         //   System.Diagnostics.Debug.WriteLine(currentRotorLeft);
            //  System.Diagnostics.Debug.WriteLine(charVal);
          //  System.Diagnostics.Debug.WriteLine("idx res rotor 3" + adjustedVal);
           // char sNew = (rotor3in[adjustedVal]);
            char sNew = (rotorLeft[adjustedVal]);
          //  System.Diagnostics.Debug.WriteLine((sNew - currentRotorLeft) % 26);
            //  System.Diagnostics.Debug.WriteLine("res roto 3 " + sNew);
            return sNew;
        }


        private char encodeLetterRotorOut(char s, EnigmaRotor er )
        {
            char adjChar = (char)(s + er.currentPos);
            System.Diagnostics.Debug.WriteLine("adjChar is " + adjChar);


            //find the character in the string
            int charVal = er.rotorLayout.IndexOf(adjChar);


            char adjustedVal = (char)(charVal + 'A');
            //   int charVal = ((int)(s) - (int)('A')) % 26;
            //   int adjustedVal = (charVal + currentRotorLeft) % 26;
            //   System.Diagnostics.Debug.WriteLine(currentRotorLeft);
            //  System.Diagnostics.Debug.WriteLine(charVal);
            //   System.Diagnostics.Debug.WriteLine("idx res rotor 3" + adjustedVal);
            char sNew = adjustedVal;
            //  System.Diagnostics.Debug.WriteLine("res roto 3 " + sNew);

            //need to move the letter found, back by the rotor position
            char sAfterAdj = (char)(sNew - er.currentPos);
            return sAfterAdj;
        }


        private char encodeLetterRotorRightOut(char s)
        {
            return encodeLetterRotorOut(s, erRight);
        }

        private char encodeLetterRotorMidOut(char s)
        {
            return encodeLetterRotorOut(s, erMid);
        }

        private char encodeLetterRotorLeftOut(char s)
        {

            return encodeLetterRotorOut(s, erLeft);
            //find the character in the string
         //   int charVal = rotorLeft.IndexOf(s);

         //   char adjustedVal = (char)(charVal + 'A');

         ////   int charVal = ((int)(s) - (int)('A')) % 26;
         ////   int adjustedVal = (charVal + currentRotorLeft) % 26;
         ////   System.Diagnostics.Debug.WriteLine(currentRotorLeft);
         //   //  System.Diagnostics.Debug.WriteLine(charVal);
         ////   System.Diagnostics.Debug.WriteLine("idx res rotor 3" + adjustedVal);
         //   char sNew = adjustedVal;
         // //  System.Diagnostics.Debug.WriteLine("res roto 3 " + sNew);
         //   return sNew;
        }

        private char encodeLetterReflector(char s)
        {
            int charVal = ((int)(s) - (int)('A')) % 26;
            int adjustedVal = (charVal + 0) % 26; //reflector does not rotate
           // System.Diagnostics.Debug.WriteLine(currentRotor3);
            //  System.Diagnostics.Debug.WriteLine(charVal);
          //  System.Diagnostics.Debug.WriteLine("idx res refelector" + adjustedVal);

            //using reflector B
            char sNew = (reflectorB[adjustedVal]);
        //    System.Diagnostics.Debug.WriteLine("res reflector 3 " + sNew);
            return sNew;
        }

    }
}
