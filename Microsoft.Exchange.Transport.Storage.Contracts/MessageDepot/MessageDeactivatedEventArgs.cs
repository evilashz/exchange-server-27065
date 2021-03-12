using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000016 RID: 22
	internal sealed class MessageDeactivatedEventArgs : MessageEventArgs
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002340 File Offset: 0x00000540
		public MessageDeactivatedEventArgs(IMessageDepotItemWrapper itemWrapper, MessageDeactivationReason reason) : base(itemWrapper)
		{
			this.reason = reason;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002350 File Offset: 0x00000550
		public MessageDeactivationReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000021 RID: 33
		private readonly MessageDeactivationReason reason;
	}
}
