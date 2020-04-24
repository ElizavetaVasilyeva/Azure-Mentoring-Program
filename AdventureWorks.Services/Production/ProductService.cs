using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace AdventureWorks.Services.Production
{
    public class ProductService : IProductService
    {
        private readonly DbModel.Entities _entities = new DbModel.Entities();
        private readonly ILogger _log;

        public ProductService(ILogger log)
        {
            _log = log;
        }

        public IEnumerable<Product> GetProducts()
        {
            try
            {
                var entities = _entities.Products;
                return entities.ToArray().Select(MapProductFromDb);
            }
            catch (Exception e)
            {
                _log.Error(e, "Exception while getting products");
                throw;
            }

        }

        public Product GetProduct(int id)
        {
            try
            {
                return MapProductFromDb(_entities.Products.Find(id));
            }
            catch (Exception e)
            {
                _log.Error(e, "Exception while getting product");
                throw;
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                var productToUpdate = _entities.Products.Find(product.Id);
                if (productToUpdate == null) return;
                _entities.Entry(productToUpdate).CurrentValues.SetValues(MapProductForDb(product));
                _entities.SaveChanges();
            }
            catch (Exception e)
            {
                _log.Error(e, "Exception during updating product");
                throw;
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                _entities.Products.Remove(_entities.Products.SingleOrDefault(x => x.ProductID == id) ??
                                      throw new InvalidOperationException());
                _entities.SaveChanges();
            }
            catch (Exception e)
            {
                _log.Error(e, "Exception during deleting product");
                throw;
            }
        }

        public void AddProduct(Product product)
        {
            try
            {
                _entities.Products.Add(MapProductForDb(product));
                _entities.SaveChanges();
            }
            catch (Exception e)
            {
                _log.Error(e, "Exception during creating product");
                throw;
            }
        }

        private Product MapProductFromDb(DbModel.Product product)
        {
            return new Product
            {
                Id = product.ProductID,
                Name = product.Name,
                ProductNumber = product.ProductNumber,
                MakeFlag = product.MakeFlag,
                FinishedGoodsFlag = product.FinishedGoodsFlag,
                Color = product.Color,
                SafetyStockLevel = product.SafetyStockLevel,
                ReorderPoint = product.ReorderPoint,
                StandardCost = product.StandardCost,
                ListPrice = product.ListPrice,
                Size = product.Size,
                SizeUnitMeasureCode = product.SizeUnitMeasureCode,
                WeightUnitMeasureCode = product.WeightUnitMeasureCode,
                Weight = product.Weight,
                DaysToManufacture = product.DaysToManufacture,
                ProductLine = product.ProductLine,
                Class = product.Class,
                Style = product.Style,
                ProductSubcategoryId = product.ProductSubcategoryID,
                ProductModelId = product.ProductModelID,
                SellStartDate = product.SellStartDate,
                SellEndDate = product.SellEndDate,
                DiscontinuedDate = product.DiscontinuedDate,
                ModifiedDate = product.ModifiedDate
            };
        }

        private DbModel.Product MapProductForDb(Product product)
        {
            return new DbModel.Product
            {
                ProductID = product.Id,
                Name = product.Name,
                ProductNumber = product.ProductNumber,
                MakeFlag = product.MakeFlag,
                FinishedGoodsFlag = product.FinishedGoodsFlag,
                Color = product.Color,
                SafetyStockLevel = product.SafetyStockLevel,
                ReorderPoint = product.ReorderPoint,
                StandardCost = product.StandardCost,
                ListPrice = product.ListPrice,
                Size = product.Size,
                SizeUnitMeasureCode = product.SizeUnitMeasureCode,
                WeightUnitMeasureCode = product.WeightUnitMeasureCode,
                Weight = product.Weight,
                DaysToManufacture = product.DaysToManufacture,
                ProductLine = product.ProductLine,
                Class = product.Class,
                Style = product.Style,
                ProductSubcategoryID = product.ProductSubcategoryId,
                ProductModelID = product.ProductModelId,
                SellStartDate = product.SellStartDate,
                SellEndDate = product.SellEndDate,
                DiscontinuedDate = product.DiscontinuedDate,
                ModifiedDate = product.ModifiedDate
            };
        }
    }
}
