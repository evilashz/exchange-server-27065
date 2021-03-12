using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000296 RID: 662
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ProgressCiFileReply : NetworkChannelMessage
	{
		// Token: 0x060019E5 RID: 6629 RVA: 0x0006C5DB File Offset: 0x0006A7DB
		internal ProgressCiFileReply(NetworkChannel channel, int progress) : base(channel, NetworkChannelMessage.MessageType.ProgressCiFileReply)
		{
			this.progress = progress;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0006C5F0 File Offset: 0x0006A7F0
		internal ProgressCiFileReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.ProgressCiFileReply, packetContent)
		{
			this.progress = base.Packet.ExtractInt32();
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x0006C610 File Offset: 0x0006A810
		internal int Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0006C618 File Offset: 0x0006A818
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.progress);
		}

		// Token: 0x04000A61 RID: 2657
		private readonly int progress;
	}
}
