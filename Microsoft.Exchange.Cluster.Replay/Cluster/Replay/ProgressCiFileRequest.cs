using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000295 RID: 661
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ProgressCiFileRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x060019E0 RID: 6624 RVA: 0x0006C512 File Offset: 0x0006A712
		internal ProgressCiFileRequest(NetworkChannel channel, Guid dbGuid, string handle) : base(channel, NetworkChannelMessage.MessageType.ProgressCiFileRequest, dbGuid)
		{
			this.handle = handle;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x0006C528 File Offset: 0x0006A728
		internal ProgressCiFileRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.ProgressCiFileRequest, packetContent)
		{
			this.handle = base.Packet.ExtractString();
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0006C574 File Offset: 0x0006A774
		public override void Execute()
		{
			Exception ex = SeederServerContext.RunSeedSourceAction(delegate
			{
				SeederServerContext seederServerContext = base.Channel.GetSeederServerContext(base.DatabaseGuid);
				seederServerContext.GetSeedingProgress(this.handle);
			});
			if (ex != null)
			{
				ReplayCrimsonEvents.CISeedingSourceError.Log<Guid, string, string, string>(base.DatabaseGuid, string.Empty, string.Empty, ex.ToString());
				SeederServerContext.ProcessSourceSideException(ex, base.Channel);
			}
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x0006C5C2 File Offset: 0x0006A7C2
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.handle);
		}

		// Token: 0x04000A60 RID: 2656
		private readonly string handle;
	}
}
