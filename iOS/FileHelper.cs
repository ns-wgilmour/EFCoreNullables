using System;
using System.IO;

using Xamarin.Forms;

[assembly: Dependency(typeof(EFCoreNullable.iOS.FileHelper))]

namespace EFCoreNullable.iOS
{
	public class FileHelper : IFileHelper
	{
		public string GetPath(string filename, bool deleteIfExists = false)
		{
			string documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string library = Path.Combine(documents, "..", "Library");

			if (!Directory.Exists(library))
				Directory.CreateDirectory(library);

            string final = Path.Combine(library, filename);
            if (File.Exists(final) && deleteIfExists)
                File.Delete(final);

			return final;
		}
	}
}
