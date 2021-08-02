using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace VoiceBot_v0._1
{
    public partial class Form1 : Form
    {
        // bot speech control
        SpeechSynthesizer spSynthesizer = new SpeechSynthesizer();
        

        //speech choices for the grammarbuilder to pick from
        Choices choiceList = new Choices();
        public Form1()
        {
            SpeechRecognitionEngine spRec = new SpeechRecognitionEngine();

            // speech list that the grammarbuilder will take as a parameter
            choiceList.Add(new String[] { "Hello", "How are you", "What time is it", "What is the biggest galaxy" });

            Grammar gr = new Grammar(new GrammarBuilder(choiceList));

            try
            {
                spRec.RequestRecognizerUpdate();
                spRec.LoadGrammar(gr);

                //WIP
                //*
                //spRec.LoadGrammar(new DictationGrammar());
                spRec.SpeechRecognized += spRec_SpeechRegonized;
                spRec.SetInputToDefaultAudioDevice();
                spRec.RecognizeAsync(RecognizeMode.Multiple);

                spRec.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }

            // bot voice type selection
            spSynthesizer.SelectVoiceByHints(VoiceGender.Neutral);


            // actual (bot)voice test
            spSynthesizer.Speak("Hello, I am voice bot version 0.1");

            InitializeComponent();
        }

        public void Answer(string s)
        {
            spSynthesizer.Speak(s);
        }

        private void spRec_SpeechRegonized(object sender, SpeechRecognizedEventArgs e)
        {
            String r = e.Result.Text;

            //if what was said was "Hello"
            //bot will respond with "Hi"
            if(r == "Hello")
            {
                Answer("Hi");
            }
            if (r == "How are you")
            {
                Answer("Even though the speech system you programmed was garbage, it seems to be working as intented.");
            }
            if (r == "What time is it")
            {
                string currentTime = DateTime.Now.ToString();
                Answer(currentTime);
            }
            if(r == "What is the biggest galaxy")
            {
                Answer("The title for the biggest galaxy known to man is IC 1101. It has a radius of 1.9569 million light years which absolutely dwarfs our Milky Way’s radius of only 52,850 light years. And is around 12.31 billion years old." +
                    "This supergiant elliptical galaxy is located 320 megaparsecs (1.05 billion light-years) from Earth. It’s also pretty damn heavy (no surprise) as it has a mass of around 100 trillion stars. " +
                    "");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
