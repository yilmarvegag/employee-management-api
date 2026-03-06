using System.Reflection;

namespace EmployeeManagement.Application 
{
    /// <summary>
    /// Provides a reference to the Application layer assembly.
    /// Useful for assembly scanning in configuration of MediatR,
    /// AutoMapper, FluentValidation validators, etc.
    /// </summary>
    public static class AssemblyReference
    {
        /// <summary>
        /// Reference to the current Application layer assembly.
        /// </summary>
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
