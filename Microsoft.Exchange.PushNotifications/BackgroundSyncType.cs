using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000022 RID: 34
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal enum BackgroundSyncType
	{
		// Token: 0x0400004B RID: 75
		[EnumMember]
		None,
		// Token: 0x0400004C RID: 76
		[EnumMember]
		Email
	}
}
