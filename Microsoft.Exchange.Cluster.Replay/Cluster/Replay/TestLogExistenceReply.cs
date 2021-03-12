using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000361 RID: 865
	internal class TestLogExistenceReply : NetworkChannelFileTransferReply
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x000A17A4 File Offset: 0x0009F9A4
		// (set) Token: 0x060022B2 RID: 8882 RVA: 0x000A17AC File Offset: 0x0009F9AC
		internal bool LogExists
		{
			get
			{
				return this.m_exists;
			}
			set
			{
				this.m_exists = value;
			}
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000A17B5 File Offset: 0x0009F9B5
		internal TestLogExistenceReply(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.TestLogExistenceReply)
		{
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000A17C3 File Offset: 0x0009F9C3
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_exists);
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000A17DC File Offset: 0x0009F9DC
		internal TestLogExistenceReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.TestLogExistenceReply, packetContent)
		{
			this.m_exists = base.Packet.ExtractBool();
		}

		// Token: 0x04000EA1 RID: 3745
		private bool m_exists;
	}
}
