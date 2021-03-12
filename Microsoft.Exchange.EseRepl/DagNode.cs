using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class DagNode : IEquatable<DagNode>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002D17 File Offset: 0x00000F17
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002D1F File Offset: 0x00000F1F
		public string Name { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D28 File Offset: 0x00000F28
		public List<DagNode.Nic> Nics
		{
			get
			{
				return this.nics;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002D30 File Offset: 0x00000F30
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002D38 File Offset: 0x00000F38
		public int ReplicationPort { get; set; }

		// Token: 0x0600004F RID: 79 RVA: 0x00002D5C File Offset: 0x00000F5C
		public static DagNode FindNode(string nodeName, IEnumerable<DagNode> list)
		{
			return list.FirstOrDefault((DagNode node) => StringUtil.IsEqualIgnoreCase(nodeName, node.Name));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D88 File Offset: 0x00000F88
		public bool Equals(DagNode other)
		{
			return this.nics.SequenceEqual(other.nics) && StringUtil.IsEqualIgnoreCase(this.Name, other.Name);
		}

		// Token: 0x0400002C RID: 44
		private List<DagNode.Nic> nics = new List<DagNode.Nic>();

		// Token: 0x0200000B RID: 11
		public class Nic : IEquatable<DagNode.Nic>
		{
			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000052 RID: 82 RVA: 0x00002DC3 File Offset: 0x00000FC3
			// (set) Token: 0x06000053 RID: 83 RVA: 0x00002DCB File Offset: 0x00000FCB
			public string IpAddress { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000054 RID: 84 RVA: 0x00002DD4 File Offset: 0x00000FD4
			// (set) Token: 0x06000055 RID: 85 RVA: 0x00002DDC File Offset: 0x00000FDC
			public string NetworkName { get; set; }

			// Token: 0x06000056 RID: 86 RVA: 0x00002DE5 File Offset: 0x00000FE5
			public bool Equals(DagNode.Nic other)
			{
				return StringUtil.IsEqualIgnoreCase(this.IpAddress, other.IpAddress) && StringUtil.IsEqualIgnoreCase(this.NetworkName, other.NetworkName);
			}
		}
	}
}
