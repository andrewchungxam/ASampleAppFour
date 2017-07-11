using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Client;

using ASampleApp;
using ASampleApp.Models;


namespace ASampleApp.CosmosDB
{
    public class CosmosDBService
    {

		//DBS - Collections - Documents
		static readonly string DatabaseId = "Xamarin";
		static readonly string CollectionId = "CosmosDog";

		//CLIENT
		static readonly DocumentClient myDocumentClient = new DocumentClient(new Uri(CosmosDBStrings.myEndPoint), CosmosDBStrings.myKey);

		public static List<CosmosDog> MyListOfCosmosDogs;

		//GETALL
		public static async Task<List<CosmosDog>> GetAllCosmosDogs()
		{
			MyListOfCosmosDogs = new List<CosmosDog>();
			try
			{

				var query = myDocumentClient.CreateDocumentQuery<CosmosDog>(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
											.AsDocumentQuery();
				while (query.HasMoreResults)
				{
					MyListOfCosmosDogs.AddRange(await query.ExecuteNextAsync<CosmosDog>());
				}
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
			}
			return MyListOfCosmosDogs;
		}

		//GET
		public static async Task<List<CosmosDog>> GetCosmosDogByIdAsync(string id)
		{
			var result = await myDocumentClient.ReadDocumentAsync<CosmosDog>(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));

			if (result.StatusCode != System.Net.HttpStatusCode.OK)
			{
				return null;
			}

			List<CosmosDog> returnedListCosmosDog = new List<CosmosDog>();
			returnedListCosmosDog.Add(result);

			return returnedListCosmosDog;

		}

		//POST
        public static async Task PostCosmosDogAsync(CosmosDog cosmosDog)
		{
			await myDocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), cosmosDog);

		}

		//PUT
        public static async Task PutCosmosDogAsync(CosmosDog cosmosDog2)
		{
			await myDocumentClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, cosmosDog2.Id), cosmosDog2);
		}

		//DELETE
        public static async Task DeleteCosmosDogAsync(CosmosDog deleteCosmosDog)
		{
			await myDocumentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, deleteCosmosDog.Id));
		}
	}
}
