using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class DagNetwork : IEquatable<DagNetwork>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002C52 File Offset: 0x00000E52
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002C5A File Offset: 0x00000E5A
		public string Name { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002C63 File Offset: 0x00000E63
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002C6B File Offset: 0x00000E6B
		public string Description { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002C74 File Offset: 0x00000E74
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002C7C File Offset: 0x00000E7C
		public bool ReplicationEnabled { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002C85 File Offset: 0x00000E85
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002C8D File Offset: 0x00000E8D
		public bool IsDnsMapped { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002C96 File Offset: 0x00000E96
		public List<string> Subnets
		{
			get
			{
				return this.subnets;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public bool Equals(DagNetwork other)
		{
			return this.subnets.SequenceEqual(other.subnets) && StringUtil.IsEqualIgnoreCase(this.Name, other.Name) && StringUtil.IsEqualIgnoreCase(this.Description, other.Description) && this.ReplicationEnabled == other.ReplicationEnabled && this.IsDnsMapped == other.IsDnsMapped;
		}

		// Token: 0x04000027 RID: 39
		private List<string> subnets = new List<string>();
	}
}
