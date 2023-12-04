using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserType : BaseEntityClass
    {
        
        public string TypeName {  get; set; }

        [JsonIgnore]
        public ICollection<User> Users { get; set; }
    }
}
