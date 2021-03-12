using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000014 RID: 20
	internal class MessageEventArgs
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002303 File Offset: 0x00000503
		public MessageEventArgs(IMessageDepotItemWrapper itemWrapper)
		{
			if (itemWrapper == null)
			{
				throw new ArgumentNullException("itemWrapper");
			}
			this.itemWrapper = itemWrapper;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002320 File Offset: 0x00000520
		public IMessageDepotItemWrapper ItemWrapper
		{
			get
			{
				return this.itemWrapper;
			}
		}

		// Token: 0x0400001F RID: 31
		private readonly IMessageDepotItemWrapper itemWrapper;
	}
}
