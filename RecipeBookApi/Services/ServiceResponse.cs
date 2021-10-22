namespace RecipeBookApi.Services
{
    /// <summary>
    /// Service Response.
    /// Use to unify all services with a single service response that contains the result of the methode, the message and the data returned.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        public bool Success { get; internal set; }
        public string Message { get; internal set; }
        public object Data { get; internal set; }
    }
}