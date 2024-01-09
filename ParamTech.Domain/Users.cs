using ParamTech.Core.Persistence.Repositoires.BaseEntity;

namespace ParamTech.Domain
{
    public class Users : Entity
    {
        public Guid Userid { get; set; }

        public string Userfirstname { get; set; }

        public string Userlastname { get; set; }

        public string Useremailsalt { get; set; }

        public string Useremailhash { get; set; }

        public bool Isok { get; set; }
    }
}
