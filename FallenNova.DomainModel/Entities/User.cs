namespace FallenNova.DomainModel
{
    /// <summary>
    /// User entity calculated properties.
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// The user's full name.
        /// </summary>
        public string FullName
        {
            get { return string.Concat(FirstName, " ", Surname); }
        }
    }
}
