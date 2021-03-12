using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000011 RID: 17
	internal interface IMessageDepotItemWrapper
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000044 RID: 68
		MessageDepotItemState State { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000045 RID: 69
		IMessageDepotItem Item { get; }
	}
}
