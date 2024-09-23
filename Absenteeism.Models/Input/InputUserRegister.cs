using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Absenteeism.Models.Input
{
    public class InputUserRegister
    {
        public string UserNumber { get; set; }
        public string Password { get; set; }
        public string UserName{ get; set; }

        [EmailAddress]
        public string Email{ get; set; }
        public int RoleId{ get; set; }
        public int PlantId{ get; set; }
    }
}
