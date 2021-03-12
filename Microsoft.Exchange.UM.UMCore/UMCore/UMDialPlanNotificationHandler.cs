using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200020E RID: 526
	internal class UMDialPlanNotificationHandler : GenericADNotificationHandler<UMDialPlan>
	{
		// Token: 0x06000F66 RID: 3942 RVA: 0x00045A08 File Offset: 0x00043C08
		protected override void LogRegistrationError(TimeSpan retryInterval, LocalizedException exception)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnabletoRegisterForDialPlanADNotifications, null, new object[]
			{
				retryInterval.Minutes,
				exception.Message
			});
		}
	}
}
