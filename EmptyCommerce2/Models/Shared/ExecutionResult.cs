namespace EmptyCommerce2.Models.Shared
{
    public class ExecutionResult
    {
        public bool IsSuccess { get; set; } = true;

        public string ErrorMessage { get; set; }

        public string ErrorCode { get; set; }
    }
}
