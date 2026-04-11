namespace e_commerce_system.Models.Response
{
	public class ApiResponse <T>
	{
		public bool IsSuccess {  get; set; }	
		public T Result { get; set; }	
		public IList<Error> errors { get; set; }


	}



	public class Error
	{
		public string Message { get; set; }

		public String StatusCode { get; set; }
	}
}
