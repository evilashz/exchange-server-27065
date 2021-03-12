using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000212 RID: 530
	internal class UMHuntGroupNotificationHandler : GenericADNotificationHandler<UMHuntGroup>
	{
		// Token: 0x06000F7E RID: 3966 RVA: 0x00045D30 File Offset: 0x00043F30
		protected override void LogRegistrationError(TimeSpan retryInterval, LocalizedException exception)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnabletoRegisterForHuntGroupADNotifications, null, new object[]
			{
				retryInterval.Minutes,
				exception.Message
			});
		}
	}
}
