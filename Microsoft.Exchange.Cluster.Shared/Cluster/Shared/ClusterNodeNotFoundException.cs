using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000C3 RID: 195
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterNodeNotFoundException : ClusterException
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x0001B5F4 File Offset: 0x000197F4
		public ClusterNodeNotFoundException(string nodeName) : base(Strings.ClusterNodeNotFoundException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001B60E File Offset: 0x0001980E
		public ClusterNodeNotFoundException(string nodeName, Exception innerException) : base(Strings.ClusterNodeNotFoundException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001B629 File Offset: 0x00019829
		protected ClusterNodeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001B653 File Offset: 0x00019853
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001B66E File Offset: 0x0001986E
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x04000715 RID: 1813
		private readonly string nodeName;
	}
}
