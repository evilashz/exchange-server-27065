using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000C0 RID: 192
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterNodeJoinedException : ClusterException
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x0001B46E File Offset: 0x0001966E
		public ClusterNodeJoinedException(string nodeName) : base(Strings.ClusterNodeJoinedException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001B488 File Offset: 0x00019688
		public ClusterNodeJoinedException(string nodeName, Exception innerException) : base(Strings.ClusterNodeJoinedException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001B4A3 File Offset: 0x000196A3
		protected ClusterNodeJoinedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001B4CD File Offset: 0x000196CD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001B4E8 File Offset: 0x000196E8
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x04000712 RID: 1810
		private readonly string nodeName;
	}
}
