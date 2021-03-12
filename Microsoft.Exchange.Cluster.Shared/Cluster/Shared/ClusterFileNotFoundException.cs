using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000C1 RID: 193
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterFileNotFoundException : ClusterException
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x0001B4F0 File Offset: 0x000196F0
		public ClusterFileNotFoundException(string nodeName) : base(Strings.ClusterFileNotFoundException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001B50A File Offset: 0x0001970A
		public ClusterFileNotFoundException(string nodeName, Exception innerException) : base(Strings.ClusterFileNotFoundException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001B525 File Offset: 0x00019725
		protected ClusterFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001B54F File Offset: 0x0001974F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001B56A File Offset: 0x0001976A
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x04000713 RID: 1811
		private readonly string nodeName;
	}
}
