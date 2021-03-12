using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000347 RID: 839
	internal class NotifyEndOfLogRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x0600224E RID: 8782 RVA: 0x000A0244 File Offset: 0x0009E444
		internal NotifyEndOfLogRequest(NetworkChannel channel, Guid dbGuid, long nextGenOfInterest) : base(channel, NetworkChannelMessage.MessageType.NotifyEndOfLogRequest, dbGuid)
		{
			this.m_nextGenOfInterest = nextGenOfInterest;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000A025A File Offset: 0x0009E45A
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_nextGenOfInterest);
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000A0273 File Offset: 0x0009E473
		internal NotifyEndOfLogRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.NotifyEndOfLogRequest, packetContent)
		{
			this.m_nextGenOfInterest = base.Packet.ExtractInt64();
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000A0294 File Offset: 0x0009E494
		public override void Execute()
		{
			MonitoredDatabase monitoredDatabase = base.Channel.MonitoredDatabase;
			EndOfLog currentEndOfLog = monitoredDatabase.CurrentEndOfLog;
			NotifyEndOfLogReply notifyEndOfLogReply = new NotifyEndOfLogReply(base.Channel, NetworkChannelMessage.MessageType.NotifyEndOfLogReply, currentEndOfLog.Generation, currentEndOfLog.Utc);
			notifyEndOfLogReply.Send();
		}

		// Token: 0x04000E33 RID: 3635
		private long m_nextGenOfInterest;
	}
}
