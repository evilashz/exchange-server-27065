using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000527 RID: 1319
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmClusterEvictWithoutCleanupException : AmClusterException
	{
		// Token: 0x06002FDF RID: 12255 RVA: 0x000C65F1 File Offset: 0x000C47F1
		public AmClusterEvictWithoutCleanupException(string nodeName) : base(ReplayStrings.AmClusterEvictWithoutCleanupException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000C660B File Offset: 0x000C480B
		public AmClusterEvictWithoutCleanupException(string nodeName, Exception innerException) : base(ReplayStrings.AmClusterEvictWithoutCleanupException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000C6626 File Offset: 0x000C4826
		protected AmClusterEvictWithoutCleanupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000C6650 File Offset: 0x000C4850
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x000C666B File Offset: 0x000C486B
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x040015EA RID: 5610
		private readonly string nodeName;
	}
}
