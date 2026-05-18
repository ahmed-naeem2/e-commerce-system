namespace e_commerce_system.IServices
{
	public interface IFileImageService
	{
		Task <string> SaveImageAsync (IFormFile filePath);

	}
}
