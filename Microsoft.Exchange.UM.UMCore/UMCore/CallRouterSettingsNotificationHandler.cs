using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200024F RID: 591
	internal class CallRouterSettingsNotificationHandler : GenericADNotificationHandler<SIPFEServerConfiguration>
	{
		// Token: 0x06001184 RID: 4484 RVA: 0x0004D440 File Offset: 0x0004B640
		protected override void LogRegistrationError(TimeSpan retryInterval, LocalizedException exception)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnabletoRegisterForCallRouterSettingsADNotifications, null, new object[]
			{
				retryInterval.Minutes,
				exception.Message
			});
		}
	}
}
