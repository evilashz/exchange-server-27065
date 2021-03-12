using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000F70 RID: 3952
	internal abstract class IOutlookPushNotificationSerializer
	{
		// Token: 0x0600641B RID: 25627
		internal abstract byte[] ConvertToPayloadForHxCalendar(PendingOutlookPushNotification notification);

		// Token: 0x0600641C RID: 25628
		internal abstract byte[] ConvertToPayloadForHxMail(PendingOutlookPushNotification notification);
	}
}
