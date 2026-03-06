namespace EmployeeManagement.Domain.Common
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Globally Unique Identifier.
        /// According to a standard Universally Unique Identifier (UUID).
        /// Specified by the Open Software Foundation (OSF).
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Date when the record is created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when the record was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Handles logical deletion of the registry.
        /// True/1 => has been deleted.
        /// False/0 => has not been deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
