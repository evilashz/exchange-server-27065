using System;
using System.IO;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000362 RID: 866
	internal class QueryLogRangeRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x060022B6 RID: 8886 RVA: 0x000A17FC File Offset: 0x0009F9FC
		internal QueryLogRangeRequest(NetworkChannel channel, Guid dbGuid) : base(channel, NetworkChannelMessage.MessageType.QueryLogRangeRequest, dbGuid)
		{
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000A180B File Offset: 0x0009FA0B
		internal QueryLogRangeRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.QueryLogRangeRequest, packetContent)
		{
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000A181C File Offset: 0x0009FA1C
		public override void Execute()
		{
			try
			{
				long firstAvailableGeneration = base.Channel.MonitoredDatabase.DetermineStartOfLog();
				EndOfLog currentEndOfLog = base.Channel.MonitoredDatabase.CurrentEndOfLog;
				new QueryLogRangeReply(base.Channel)
				{
					FirstAvailableGeneration = firstAvailableGeneration,
					EndOfLogGeneration = currentEndOfLog.Generation,
					EndOfLogUtc = currentEndOfLog.Utc
				}.Send();
			}
			catch (IOException ex)
			{
				base.Channel.SendException(ex);
				base.Channel.Close();
			}
		}
	}
}
