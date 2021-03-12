using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000205 RID: 517
	internal class UMAutoAttendantNotificationHandler : GenericADNotificationHandler<UMAutoAttendant>
	{
		// Token: 0x06000F11 RID: 3857 RVA: 0x00044384 File Offset: 0x00042584
		protected override void LogRegistrationError(TimeSpan retryInterval, LocalizedException exception)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnabletoRegisterForAutoAttendantADNotifications, null, new object[]
			{
				retryInterval.Minutes,
				exception.Message
			});
		}
	}
}
