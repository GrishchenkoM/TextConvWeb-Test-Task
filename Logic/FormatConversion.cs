using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Logic
{	
    /// <summary>
    /// Encapsulate conversion formats
    /// </summary>
    public class FormatConversion
    {
        public FormatConversion()
        {
            FormatConversionList = new List<Strategy>
            {
                new XmlConversion(), 
                new CsvConversion()
            };
        }
        
        public List<Strategy> FormatConversionList { get; private set; }
    }

    public abstract class Strategy
    {
        public string Name { get; protected set; }

        /// <summary>
        /// Create the marking
        /// </summary>
        public abstract object Convert(string text, TextTransformation transformation, string sortName = null);
    }

    public abstract class TextConversion : Strategy
    {
        protected List<List<string>> CreateSentencesList(string text, TextTransformation transformation, string sortName)
        {
            var sentencesList = transformation.Transform(text, sortName);
            return sentencesList;
        }

        protected abstract string[] CreateArray(object obj);
    }

    public class XmlConversion : TextConversion
    {
        public XmlConversion()
        {
            Name = "XML";
        }
        
        public override object Convert(string text, TextTransformation transformation, string sortName = null)
        {
            var sentencesList = CreateSentencesList(text, transformation, sortName);
            
            var data = CreateXDocument(sentencesList);

            return CreateArray(data);
        }

        private XDocument CreateXDocument(List<List<string>> sentencesList)
        {
            XDocument data = null;
            try
            {
                data = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("text",
                    sentencesList.Select(x => new XElement("sentence",
                        x.Select(y => new XElement("word", y)))))
                );
            }
            catch
            {
                // ignored
            }

            return data;
        }

        protected override string[] CreateArray(object obj)
        {
            string[] array = null;
            if (obj is XDocument)
            {
                var data = obj as XDocument;
                array = new[]
                {
                    data.Declaration.ToString(),
                    data.ToString()
                };
            }
            return array;
        }
    }

    public class CsvConversion : TextConversion
    {
        public CsvConversion()
        {
            Name = "CSV";
        }
        public override object Convert(string text, TextTransformation transformation, string sortName = null)
        {
            var sentencesList = CreateSentencesList(text, transformation, sortName);

            return CreateArray(sentencesList);
        }

        protected override string[] CreateArray(object obj)
        {
            try
            {
                if (obj is List<List<string>>)
                {
                    var sentencesList = obj as List<List<string>>;

                    var arrayList = new string[sentencesList.Count];

                    for (var sentence = 0; sentence < sentencesList.Count; ++sentence)
                    {
                        var strBuilder = CreateCsvMark(sentence, sentencesList);

                        arrayList[sentence] = strBuilder.ToString();
                    }

                    return arrayList;
                }
            }
            catch
            {
                // ignored
            }

            return null;
        }

        private StringBuilder CreateCsvMark(int sentence, List<List<string>> sentencesList)
        {
            var strBuilder = new StringBuilder();

            strBuilder.Append("Sentence " + (sentence + 1));

            foreach (var word in sentencesList[sentence])
            {
                strBuilder.Append(", ");
                strBuilder.Append(word);
            }
            return strBuilder;
        }
    }
}
