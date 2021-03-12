using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000BF RID: 191
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterNotJoinedException : ClusterException
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x0001B3EC File Offset: 0x000195EC
		public ClusterNotJoinedException(string nodeName) : base(Strings.ClusterNotJoinedException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001B406 File Offset: 0x00019606
		public ClusterNotJoinedException(string nodeName, Exception innerException) : base(Strings.ClusterNotJoinedException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001B421 File Offset: 0x00019621
		protected ClusterNotJoinedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001B44B File Offset: 0x0001964B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001B466 File Offset: 0x00019666
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x04000711 RID: 1809
		private readonly string nodeName;
	}
}
