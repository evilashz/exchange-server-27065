using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000BE RID: 190
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterNotInstalledException : ClusterException
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x0001B36A File Offset: 0x0001956A
		public ClusterNotInstalledException(string nodeName) : base(Strings.ClusterNotInstalledException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001B384 File Offset: 0x00019584
		public ClusterNotInstalledException(string nodeName, Exception innerException) : base(Strings.ClusterNotInstalledException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001B39F File Offset: 0x0001959F
		protected ClusterNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001B3C9 File Offset: 0x000195C9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001B3E4 File Offset: 0x000195E4
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x04000710 RID: 1808
		private readonly string nodeName;
	}
}
