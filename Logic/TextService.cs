using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Logic.Interfaces;

namespace Logic
{
    /// <summary>
    /// Responsible for text parsing and others actions with it
    /// </summary>
    public class TextTransformation
    {
        public TextTransformation() { }

        public TextTransformation(ISort sort)
        {
            _sort = sort;
        }

        /// <summary>
        /// Make list of list of words in strings
        /// </summary>
        public List<List<string>> Transform(string text, string sortName = null)
        {
            // разбить текст на предложения
            var sentences = CreateSentences(text);

            // разбить предложения на список слов
            return CreateSentencesList(sortName, sentences);
        }
        
        private List<string> CreateSentences(string text)
        {
            List<string> sentences = null;
            try
            {
                if (string.IsNullOrEmpty(text))
                    throw new Exception();

                var newText = Regex.Replace(
                Regex.Replace(
                    Regex.Replace(text,
                        "[ ]+", " "),
                    "[\n\t\r'\"]", ""),
                "([ ]?),([ ]?)", " ");

                sentences = Regex.Replace(newText
                    .TrimStart(' ').TrimEnd(' '), "(.$)", "")
                    .Split('.').ToList();

                for (var i = 0; i < sentences.Count; ++i)
                {
                    sentences[i] = Regex.Replace(sentences[i]
                        .TrimStart(' ').TrimEnd(' '), "[ ](\\.)", ".");
                }
            }
            catch
            {
                // ignored
            }

            return sentences;
        }

        private List<List<string>> CreateSentencesList(string sortName, List<string> sentences)
        {
            List<List<string>> sentencesList = null;

            try
            {
                if(sentences == null || sentences.Count == 0)
                    throw new Exception();

                sentencesList = new List<List<string>>();
                foreach (var sentence in sentences)
                {
                    IEnumerable<string> wordsList = sentence.Split(' ').ToList();

                    if (_sort != null)
                        Sort(ref wordsList, sortName);

                    sentencesList.Add(wordsList.ToList());
                }
            }
            catch
            {
                // ignored
            }

            return sentencesList;
        }

        private void Sort(ref IEnumerable<string> array, string sortName = null)
        {
            _sort.Sort(ref array, sortName);
        }
        
        private readonly ISort _sort;
    }
}
