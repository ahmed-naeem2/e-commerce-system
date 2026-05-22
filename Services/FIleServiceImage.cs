using e_commerce_system.IServices;


namespace e_commerce_system.Services
{
	public class FIleServiceImage:IFileImageService
	{
		public static readonly string Root = Path.Combine(
		Directory.GetCurrentDirectory(),
		"wwwroot");

		public async Task<string> SaveImageAsync(IFormFile imageFile)
		{
			var extion=Path.GetExtension( imageFile.FileName ).ToLower();

			var FileName = "Uploads/Product"+"/" + Guid.NewGuid().ToString() +extion;
			var FilePath= Path.Combine( Root, FileName );

			if (!Directory.Exists(Root))
			{

				Directory.CreateDirectory(Root);
			}

			using (var stream = new FileStream(FilePath, FileMode.Create)) { 
			
			
			await imageFile.CopyToAsync(stream);
			
			}

			return FileName;
		}

		public static void DeleteImagePath(string Path)
		{
			var FullPath = Root+"/" + Path;
			
			File.Delete(FullPath);
		}
	}
}
