using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000528 RID: 1320
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmClusterNodeNotFoundException : AmClusterException
	{
		// Token: 0x06002FE4 RID: 12260 RVA: 0x000C6673 File Offset: 0x000C4873
		public AmClusterNodeNotFoundException(string nodeName) : base(ReplayStrings.AmClusterNodeNotFoundException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000C668D File Offset: 0x000C488D
		public AmClusterNodeNotFoundException(string nodeName, Exception innerException) : base(ReplayStrings.AmClusterNodeNotFoundException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000C66A8 File Offset: 0x000C48A8
		protected AmClusterNodeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000C66D2 File Offset: 0x000C48D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x000C66ED File Offset: 0x000C48ED
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x040015EB RID: 5611
		private readonly string nodeName;
	}
}
