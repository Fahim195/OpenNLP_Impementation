using opennlp.tools.chunker;
using opennlp.tools.postag;
using opennlp.tools.tokenize;
using System;
using opennlp.tools.cmdline.parser;
using opennlp.tools.parser;
using opennlp.tools.parser.chunking;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parser = opennlp.tools.parser.Parser;

namespace TT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sentence = "The quick brown fox jumps over the lazy dog .";
            string Modelpath = @"C:\Users\Fahim\source\repos\TT\packages\Models\";
            java.io.FileInputStream modelInpStream = new java.io.FileInputStream(Modelpath + "en-sent.bin.zip");
            opennlp.tools.sentdetect.SentenceModel sentenceModel = new opennlp.tools.sentdetect.SentenceModel(modelInpStream);
            opennlp.tools.sentdetect.SentenceDetectorME SentenceDetectorME = new opennlp.tools.sentdetect.SentenceDetectorME(sentenceModel);
            String[] sentences = SentenceDetectorME.sentDetect(sentence);

            java.io.FileInputStream mdl = new java.io.FileInputStream(Modelpath + "en-pos-maxent.zip");
            POSModel model = new POSModel(mdl);
            POSTaggerME tagger = new POSTaggerME(model);
            WhitespaceTokenizer whitespaceTokenizer = WhitespaceTokenizer.INSTANCE;
            String[] tokens = whitespaceTokenizer.tokenize(sentence);
            String[] tags = tagger.tag(tokens);


            java.io.FileInputStream chnkMdl = new java.io.FileInputStream(Modelpath + "en-chunker_2.zip");
            ChunkerModel chunkerModel = new ChunkerModel(chnkMdl);
            // initializing chunker(maximum entropy) with chunker model
            ChunkerME chunker = new ChunkerME(chunkerModel);
            // chunking the given sentence : chunking requires sentence to be tokenized and pos tagged
            String[] chunks = chunker.chunk(tokens, tags);



            java.io.FileInputStream cnkprs = new java.io.FileInputStream(Modelpath + "en-parser-chunking.zip");
            ParserModel mdlo = new ParserModel(cnkprs);
            Parser parser = ParserFactory.create(mdlo);
            Parse[] topParses = ParserTool.parseLine(sentence, parser,1);
            string x="";
            ////for (Parse p : topParses)
            ////{
            ////    x += p.toString();
            ////   is.close();
            ////}
            foreach (Parse p in topParses)
            {

                p.show();

            }
            MessageBox.Show("done");
            //string[] hh=new string[100];
            //for (int i = 0; i < topParses.Length; i++) 
            //{
            //    hh[i] = topParses[i].toString();

            //}
            //ChunkerModel chunkerModel = new ChunkerModel(cnk);
            //ChunkerME chunkerME = new ChunkerME(chunkerModel);
            //String[] result = chunkerME.chunk(tokens, tags);

        }
    }
}
