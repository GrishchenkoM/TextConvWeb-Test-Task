using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Run text transformation
    /// </summary>
    public class ConversionService
    {
        private ConversionService(
            List<Strategy> list, 
            TextTransformation textTransformationStrategy)
        {
            _strategyList = list;
            _textTransformationStrategy = textTransformationStrategy;
        }
        public static ConversionService GetInstance(
            List<Strategy> list, 
            TextTransformation textTransformationStrategy)
        {
            if (_instance == null)
                lock (_object)
                    if (_instance == null)
                        _instance = new ConversionService(
                            list, textTransformationStrategy);

            return _instance;
        }
        public static ConversionService GetConversionService { get { return _instance; } }
        
        public object Convert(string text, string targetStrategyName, string sortName = null)
        {
            foreach (var strategy in _strategyList)
                if (strategy.Name.ToLower().Equals(
                    targetStrategyName.ToLower()))
                {
                    var data = strategy.Convert(text, _textTransformationStrategy, sortName);

                    if (data == null)
                        return "Conversion was crashed";

                    return data;
                }

            return "Strategy not found";
        }

        private readonly List<Strategy> _strategyList;
        private readonly TextTransformation _textTransformationStrategy;

        private static ConversionService _instance;
        private static readonly object _object = new object();
    }
}
