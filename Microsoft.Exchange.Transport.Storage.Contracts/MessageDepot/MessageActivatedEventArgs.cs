using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000015 RID: 21
	internal sealed class MessageActivatedEventArgs : MessageEventArgs
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002328 File Offset: 0x00000528
		public MessageActivatedEventArgs(IMessageDepotItemWrapper itemWrapper, MessageActivationReason reason) : base(itemWrapper)
		{
			this.reason = reason;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002338 File Offset: 0x00000538
		public MessageActivationReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000020 RID: 32
		private readonly MessageActivationReason reason;
	}
}
