using System.Collections.Generic;
using _2013.March.Domain;

namespace _2013.March.Data
{
    public class BusinessLayer
    {
        /// <summary>
        /// An example of the evil that beseiges us...
        /// </summary>
        public void SomethingImportant()
        {
            // Can't be injected
            // Can't be easily mocked
            // Often leads to Cross-cutting concerns
            var prods = LogicLayer.GetProducts();

            // can be easily tested by adding a setter
            var prods2 = LogicLayerSington.Instance.GetProducts();
        }
    }
    public interface ILogicLayerRepository
    {
        List<Product> GetProducts();
    }
    public class LogicLayerRepository : ILogicLayerRepository
    {
        public List<Product> GetProducts()
        {
            return LogicLayer.GetProducts();
        }
    }

    public static class LogicLayer
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>();
        }
    }

    public class LogicLayerSington
    {
        public static LogicLayerSington Instance
        {
            get { return _instance ?? (_instance = new LogicLayerSington()); }
        }
        private static LogicLayerSington _instance;

        public List<Product> GetProducts()
        {
            return new List<Product>();
        }
    }
}
