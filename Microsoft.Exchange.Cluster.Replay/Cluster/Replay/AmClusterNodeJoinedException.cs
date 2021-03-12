using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000525 RID: 1317
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmClusterNodeJoinedException : AmClusterException
	{
		// Token: 0x06002FD5 RID: 12245 RVA: 0x000C64ED File Offset: 0x000C46ED
		public AmClusterNodeJoinedException(string nodeName) : base(ReplayStrings.AmClusterNodeJoinedException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000C6507 File Offset: 0x000C4707
		public AmClusterNodeJoinedException(string nodeName, Exception innerException) : base(ReplayStrings.AmClusterNodeJoinedException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000C6522 File Offset: 0x000C4722
		protected AmClusterNodeJoinedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000C654C File Offset: 0x000C474C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x000C6567 File Offset: 0x000C4767
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x040015E8 RID: 5608
		private readonly string nodeName;
	}
}
