namespace FallenNova.DomainModel
{
    /// <summary>
    /// User log type enumeration.
    /// </summary>
    /// <remarks>
    /// Because this solution employs EF Database First, and I didn't wanted to manually edit my Entity Data Model (.edmx) so as to 
    /// avoid issues when updating the model from the database, so I have manually added the following enumeration.
    /// </remarks>
    public partial class UserLogType
    {
        public enum Types
        {
            LoggedInSuccessfullyManualLogin = 1,
            LoggedOut = 2,
            AddedUser = 3,
            EditedUserContactDetails = 4,
            EditedUserStatus = 5,
            SubmittedContactUsMessage = 6,
            LoggedInUnsuccessfullyManualLogin = 7,
            AuthenticatedSuccessfullyAutomaticLogin = 8,
            UpdatedEveOnlineSkillTree = 9,
            EditedUserPassword = 10
        }
    }
}
