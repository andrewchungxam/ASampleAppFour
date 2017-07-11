using System;
namespace ASampleApp.Data
{
    public static class FileAccessHelper
    {
        //public FileAccessHelper()
        //{
        //}



#if __ANDROID__
        public static string GetLocalFilePath(string filename)
        {
            string _myDocumentFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			return System.IO.Path.Combine (_myDocumentFolderPath, filename);
 
        }
#endif

#if __IOS__
        public static string GetLocalFilePath(string filename)
        {
            string _myDocumentFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string _myLibraryFolderPath = System.IO.Path.Combine(_myDocumentFolderPath, "..", "Library");

            if (!System.IO.Directory.Exists(_myLibraryFolderPath))
            {
                System.IO.Directory.CreateDirectory(_myLibraryFolderPath);
            }

            return System.IO.Path.Combine(_myDocumentFolderPath, filename);
        }

#endif


#if __Windows__

        public static string GetLocalFilePath(string filename)
        {
            string _myDocumentFolderPath = global:Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            return System.IO.Path.Combine(_myDocumentFolderPath, filename);
        }

#endif

    }
}
