namespace FallenNova.DomainModel
{
    /// <summary>
    /// Role enumeration.
    /// </summary>
    /// <remarks>
    /// Because this solution employs EF Database First, and I didn't wanted to manually edit my Entity Data Model (.edmx) so as to 
    /// avoid issues when updating the model from the database, so I have manually added the following enumeration.
    /// </remarks>
    public partial class Role
    {
        public enum Roles
        {
            Member = 1,
            Administrator = 2
        }
    }
}
