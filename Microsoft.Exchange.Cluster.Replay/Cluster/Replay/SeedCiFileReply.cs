using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000294 RID: 660
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SeedCiFileReply : NetworkChannelMessage
	{
		// Token: 0x060019DC RID: 6620 RVA: 0x0006C4BC File Offset: 0x0006A6BC
		internal SeedCiFileReply(NetworkChannel channel, string handle) : base(channel, NetworkChannelMessage.MessageType.SeedCiFileReply)
		{
			this.handle = handle;
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0006C4D1 File Offset: 0x0006A6D1
		internal SeedCiFileReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedCiFileReply, packetContent)
		{
			this.handle = base.Packet.ExtractString();
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x0006C4F1 File Offset: 0x0006A6F1
		internal string Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0006C4F9 File Offset: 0x0006A6F9
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.handle);
		}

		// Token: 0x04000A5F RID: 2655
		private readonly string handle;
	}
}
