using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    [SugarTable("User")]
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int IsDelete { get; set; }

        public string Email { get; set; }

        public string TelPhone { get; set; }

        public string Remark { get; set; }

        public DateTime? AddTime { get; set; }
    }
}
