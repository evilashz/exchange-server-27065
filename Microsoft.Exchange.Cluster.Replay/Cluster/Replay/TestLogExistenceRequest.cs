using System;
using System.IO;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000360 RID: 864
	internal class TestLogExistenceRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x060022AD RID: 8877 RVA: 0x000A16E1 File Offset: 0x0009F8E1
		internal TestLogExistenceRequest(NetworkChannel channel, Guid dbGuid, long logNum) : base(channel, NetworkChannelMessage.MessageType.TestLogExistenceRequest, dbGuid)
		{
			this.m_logGeneration = logNum;
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000A16F7 File Offset: 0x0009F8F7
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_logGeneration);
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000A1710 File Offset: 0x0009F910
		internal TestLogExistenceRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.TestLogExistenceRequest, packetContent)
		{
			this.m_logGeneration = base.Packet.ExtractInt64();
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000A1730 File Offset: 0x0009F930
		public override void Execute()
		{
			try
			{
				string path = base.Channel.MonitoredDatabase.BuildLogFileName(this.m_logGeneration);
				bool logExists = File.Exists(path);
				new TestLogExistenceReply(base.Channel)
				{
					LogExists = logExists
				}.Send();
			}
			catch (IOException ex)
			{
				base.Channel.SendException(ex);
				base.Channel.Close();
			}
		}

		// Token: 0x04000EA0 RID: 3744
		private long m_logGeneration;
	}
}
