using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000AA RID: 170
	internal enum GcmResponseStatusCode
	{
		// Token: 0x040002EA RID: 746
		Undefined,
		// Token: 0x040002EB RID: 747
		Success,
		// Token: 0x040002EC RID: 748
		MissingRegistration,
		// Token: 0x040002ED RID: 749
		InvalidRegistration,
		// Token: 0x040002EE RID: 750
		MismatchSenderId,
		// Token: 0x040002EF RID: 751
		NotRegistered,
		// Token: 0x040002F0 RID: 752
		MessageTooBig,
		// Token: 0x040002F1 RID: 753
		InvalidDataKey,
		// Token: 0x040002F2 RID: 754
		InvalidTtl,
		// Token: 0x040002F3 RID: 755
		InvalidPackageName,
		// Token: 0x040002F4 RID: 756
		InvalidResponse
	}
}
