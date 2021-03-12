using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003B7 RID: 951
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkCorruptDataException : NetworkTransportException
	{
		// Token: 0x060027E9 RID: 10217 RVA: 0x000B6D2A File Offset: 0x000B4F2A
		public NetworkCorruptDataException(string srcNode) : base(ReplayStrings.NetworkCorruptData(srcNode))
		{
			this.srcNode = srcNode;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000B6D44 File Offset: 0x000B4F44
		public NetworkCorruptDataException(string srcNode, Exception innerException) : base(ReplayStrings.NetworkCorruptData(srcNode), innerException)
		{
			this.srcNode = srcNode;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000B6D5F File Offset: 0x000B4F5F
		protected NetworkCorruptDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.srcNode = (string)info.GetValue("srcNode", typeof(string));
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000B6D89 File Offset: 0x000B4F89
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("srcNode", this.srcNode);
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060027ED RID: 10221 RVA: 0x000B6DA4 File Offset: 0x000B4FA4
		public string SrcNode
		{
			get
			{
				return this.srcNode;
			}
		}

		// Token: 0x040013B4 RID: 5044
		private readonly string srcNode;
	}
}
