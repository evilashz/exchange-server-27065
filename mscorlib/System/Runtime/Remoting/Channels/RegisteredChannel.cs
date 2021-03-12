using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020007FC RID: 2044
	internal class RegisteredChannel
	{
		// Token: 0x0600586F RID: 22639 RVA: 0x001373A0 File Offset: 0x001355A0
		internal RegisteredChannel(IChannel chnl)
		{
			this.channel = chnl;
			this.flags = 0;
			if (chnl is IChannelSender)
			{
				this.flags |= 1;
			}
			if (chnl is IChannelReceiver)
			{
				this.flags |= 2;
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06005870 RID: 22640 RVA: 0x001373EF File Offset: 0x001355EF
		internal virtual IChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x06005871 RID: 22641 RVA: 0x001373F7 File Offset: 0x001355F7
		internal virtual bool IsSender()
		{
			return (this.flags & 1) > 0;
		}

		// Token: 0x06005872 RID: 22642 RVA: 0x00137404 File Offset: 0x00135604
		internal virtual bool IsReceiver()
		{
			return (this.flags & 2) > 0;
		}

		// Token: 0x0400280E RID: 10254
		private IChannel channel;

		// Token: 0x0400280F RID: 10255
		private byte flags;

		// Token: 0x04002810 RID: 10256
		private const byte SENDER = 1;

		// Token: 0x04002811 RID: 10257
		private const byte RECEIVER = 2;
	}
}
