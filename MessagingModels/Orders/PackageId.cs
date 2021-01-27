using System;
using System.ComponentModel.DataAnnotations;

namespace MessagingContracts.Orders
{
    public class PackageId
    {
        public PackageId(Guid value)
        {
            this.value = value;
        }

        [Key] public Guid id { get; set; }
        public Guid value { get; set; }
    }
}