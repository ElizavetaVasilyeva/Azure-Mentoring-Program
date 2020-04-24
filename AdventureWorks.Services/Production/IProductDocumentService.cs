namespace AdventureWorks.Services.Production
{
    public interface IProductDocumentService
    {
        void AddProductDocument(ProductDocument product);
        ProductDocument GetFile(string fileName, string guid);
    }
}
