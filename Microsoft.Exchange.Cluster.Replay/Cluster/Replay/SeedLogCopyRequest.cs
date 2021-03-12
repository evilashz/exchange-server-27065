using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002A4 RID: 676
	internal class SeedLogCopyRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x06001A5D RID: 6749 RVA: 0x0006F3A1 File Offset: 0x0006D5A1
		internal SeedLogCopyRequest(NetworkChannel channel, Guid dbGuid) : base(channel, NetworkChannelMessage.MessageType.SeedLogCopyRequest, dbGuid)
		{
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0006F3B0 File Offset: 0x0006D5B0
		internal SeedLogCopyRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedLogCopyRequest, packetContent)
		{
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0006F3E8 File Offset: 0x0006D5E8
		public override void Execute()
		{
			Exception ex = SeederServerContext.RunSeedSourceAction(delegate
			{
				SeederServerContext seederServerContext = base.Channel.GetSeederServerContext(base.DatabaseGuid);
				seederServerContext.SendLogFiles();
			});
			if (ex != null)
			{
				ReplayCrimsonEvents.LogSeedingSourceError.Log<Guid, string, string, string>(base.DatabaseGuid, string.Empty, base.Channel.PartnerNodeName, ex.ToString());
				SeederServerContext.ProcessSourceSideException(ex, base.Channel);
			}
		}
	}
}
