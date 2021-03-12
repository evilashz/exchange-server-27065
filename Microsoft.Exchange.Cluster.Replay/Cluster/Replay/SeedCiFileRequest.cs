using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000292 RID: 658
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SeedCiFileRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x060019D1 RID: 6609 RVA: 0x0006C2C8 File Offset: 0x0006A4C8
		internal SeedCiFileRequest(NetworkChannel channel, Guid dbGuid, string endpoint) : this(channel, NetworkChannelMessage.MessageType.SeedCiFileRequest, dbGuid, endpoint, null)
		{
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0006C2D9 File Offset: 0x0006A4D9
		internal SeedCiFileRequest(NetworkChannel channel, byte[] packetContent) : this(channel, NetworkChannelMessage.MessageType.SeedCiFileRequest, packetContent)
		{
			this.endpoint = base.Packet.ExtractString();
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0006C2F9 File Offset: 0x0006A4F9
		protected SeedCiFileRequest(NetworkChannel channel, NetworkChannelMessage.MessageType messageType, Guid dbGuid, string endpoint, string reason) : base(channel, messageType, dbGuid)
		{
			this.endpoint = endpoint;
			this.reason = reason;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0006C314 File Offset: 0x0006A514
		protected SeedCiFileRequest(NetworkChannel channel, NetworkChannelMessage.MessageType messageType, byte[] packetContent) : base(channel, messageType, packetContent)
		{
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0006C37C File Offset: 0x0006A57C
		public override void Execute()
		{
			Exception ex = SeederServerContext.RunSeedSourceAction(delegate
			{
				SeederServerContext seederServerContext = base.Channel.CreateSeederServerContext(base.DatabaseGuid, null, SeedType.Catalog);
				seederServerContext.SeedToEndpoint(this.endpoint, this.reason);
				ReplayCrimsonEvents.CISeedingSourceBeginSucceeded.Log<Guid, string, string, string>(base.DatabaseGuid, seederServerContext.DatabaseName, this.endpoint, string.Empty);
			});
			if (ex != null)
			{
				ReplayCrimsonEvents.CISeedingSourceBeginFailed.Log<Guid, string, string, string>(base.DatabaseGuid, string.Empty, this.endpoint, ex.ToString());
				SeederServerContext.ProcessSourceSideException(ex, base.Channel);
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0006C3CB File Offset: 0x0006A5CB
		protected override void Serialize()
		{
			base.Serialize();
			this.SerializeAdditionalProperties();
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0006C3D9 File Offset: 0x0006A5D9
		protected virtual void SerializeAdditionalProperties()
		{
			base.Packet.Append(this.endpoint);
		}

		// Token: 0x04000A5D RID: 2653
		protected string reason;

		// Token: 0x04000A5E RID: 2654
		protected string endpoint;
	}
}
