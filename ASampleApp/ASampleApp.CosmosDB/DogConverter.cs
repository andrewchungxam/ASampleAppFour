using System;
using ASampleApp.Models;

namespace ASampleApp.CosmosDB
{
    public static class DogConverter
    {
        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number

        public static CosmosDog ConvertToCosmosDog (Dog dog)
        {
            var myIdString = (dog.Id).ToString();
            var myCosmosDog = new CosmosDog()
            {
                Id = myIdString,
                Name = dog.Name,
                FurColor = dog.FurColor,
                DogPictureURL = dog.DogPictureURL,
                DogPictureFile = dog.DogPictureFile,
                DogPictureSource = dog.DogPictureSource
            };
            return myCosmosDog;
        }


		public static Dog ConvertToDog(CosmosDog cosmosDog)
		{
            var myIdInt = Int32.Parse(cosmosDog.Id);

			var myDog = new Dog()
			{
                Id = myIdInt,
				Name = cosmosDog.Name,
				FurColor = cosmosDog.FurColor,
				DogPictureURL = cosmosDog.DogPictureURL,
				DogPictureFile = cosmosDog.DogPictureFile,
				DogPictureSource = cosmosDog.DogPictureSource
			};
			return myDog;
		}
	}
}
