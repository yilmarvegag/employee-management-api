namespace EmployeeManagement.API.Extensions
{
    public class LowercaseControllerTransformer : IOutboundParameterTransformer
    {
        /// <summary>
        /// Converts the specified value to its lowercase string representation for outbound processing.
        /// </summary>
        /// <param name="value">The value to convert. If null, the method returns null.</param>
        /// <returns>A lowercase string representation of the specified value, or null if <paramref name="value"/> is null.</returns>
        public string? TransformOutbound(object? value)
        {
            return value?.ToString()?.ToLowerInvariant();
        }
    }
}
