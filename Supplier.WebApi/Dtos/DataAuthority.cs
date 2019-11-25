using System;
using System.Collections.Generic;
using System.Text;

namespace Dtos
{
    public class DataAuthority
    {
        /// <summary>
        /// 节点Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>
        public string PId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }
    }
}
