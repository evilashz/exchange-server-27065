using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000254 RID: 596
	internal enum TextMessageDeliveryStatus
	{
		// Token: 0x040011E3 RID: 4579
		None,
		// Token: 0x040011E4 RID: 4580
		Submitted,
		// Token: 0x040011E5 RID: 4581
		RoutedToDeliveryPoint = 25,
		// Token: 0x040011E6 RID: 4582
		RoutedToExternalMessagingSystem = 50,
		// Token: 0x040011E7 RID: 4583
		Delivered = 100
	}
}
