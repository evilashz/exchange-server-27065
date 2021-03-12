using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000018 RID: 24
	internal sealed class MessageRemovedEventArgs : MessageEventArgs
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000025B4 File Offset: 0x000007B4
		public MessageRemovedEventArgs(IMessageDepotItemWrapper itemWrapper, MessageRemovalReason reason, bool generateNdr) : base(itemWrapper)
		{
			this.reason = reason;
			this.generateNdr = generateNdr;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000025CB File Offset: 0x000007CB
		public MessageRemovalReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000025D3 File Offset: 0x000007D3
		public bool GenerateNdr
		{
			get
			{
				return this.generateNdr;
			}
		}

		// Token: 0x0400002F RID: 47
		private readonly MessageRemovalReason reason;

		// Token: 0x04000030 RID: 48
		private readonly bool generateNdr;
	}
}
