namespace FallenNova.Web.Areas.Secure.Models.CorpModel
{
    public class MembersModel
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string CorporationName { get; set; }
    }

    public class AddMemberModel
    {
        public string Name { get; set; }
    }

    public class MemberDetailsModel
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string CorporationName { get; set; }
    }
}