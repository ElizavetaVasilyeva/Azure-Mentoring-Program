using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace AdventureWorks.Services.Production
{
    public class ProductDocumentService : IProductDocumentService
    {
        private string _connectionString;

        public ProductDocumentService()
        {
            _connectionString = ConfigurationManager.AppSettings["AdventureWorks"];
        }

        public void AddProductDocument(ProductDocument productDocument)
        {
            string sql = @"declare @id hierarchyid

                select @id = MAX(DocumentNode)
                from Production.Document
                where DocumentNode.GetAncestor(1) = hierarchyid::GetRoot()

                insert into Production.Document(
	                DocumentNode, 
	                Title, 
	                Owner, 
	                FolderFlag, 
	                FileName, 
	                FileExtension, 
	                Revision, 
	                ChangeNumber, 
	                Status, 
	                ModifiedDate,
                    Document) 
                values(
	                hierarchyid::GetRoot().GetDescendant(@id, null), 
	                @filename, 
	                219, 
	                0,
	                @filename,
	                @extention,
	                0, 
	                0,
	                1,
	                GetDate(),
                    @document)";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@document", productDocument.FileBytes);
                cmd.Parameters.AddWithValue("@filename", productDocument.FileName);
                cmd.Parameters.AddWithValue("@extention", new FileInfo(productDocument.FileName).Extension);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public ProductDocument GetFile(string fileName, string guid)
        {
            var sql = @"select Document, FileName from Production.Document
                            where FileName = @fileName and rowguid = @guid";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@fileName", fileName);
                cmd.Parameters.Add("@guid", SqlDbType.UniqueIdentifier, 16).Value = new Guid(guid);

                conn.Open();
                SqlDataReader mySqlDataReader = cmd.ExecuteReader();

                ProductDocument document = null;
                if (mySqlDataReader.HasRows)
                {
                    document = new ProductDocument();

                    while (mySqlDataReader.Read())
                    {
                        document.FileBytes = (byte[]) mySqlDataReader["Document"];
                        document.FileName = (string) mySqlDataReader["FileName"];
                    }
                }

                mySqlDataReader.Close();
                conn.Close();
                return document;
            }
        }
    }
}
