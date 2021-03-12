using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000297 RID: 663
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CancelCiFileRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x060019E9 RID: 6633 RVA: 0x0006C631 File Offset: 0x0006A831
		internal CancelCiFileRequest(NetworkChannel channel, Guid dbGuid) : base(channel, NetworkChannelMessage.MessageType.CancelCiFileRequest, dbGuid)
		{
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0006C640 File Offset: 0x0006A840
		internal CancelCiFileRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.CancelCiFileRequest, packetContent)
		{
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0006C678 File Offset: 0x0006A878
		public override void Execute()
		{
			Exception ex = SeederServerContext.RunSeedSourceAction(delegate
			{
				SeederServerContext seederServerContext = base.Channel.GetSeederServerContext(base.DatabaseGuid);
				seederServerContext.HandleCancelCiFileRequest(null);
			});
			if (ex != null)
			{
				ReplayCrimsonEvents.CISeedingSourceError.Log<Guid, string, string, string>(base.DatabaseGuid, string.Empty, string.Empty, ex.ToString());
				SeederServerContext.ProcessSourceSideException(ex, base.Channel);
			}
		}
	}
}
