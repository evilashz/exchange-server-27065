using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000215 RID: 533
	internal class UMIPGatewayNotificationHandler : GenericADNotificationHandler<UMIPGateway>
	{
		// Token: 0x06000F8E RID: 3982 RVA: 0x00046204 File Offset: 0x00044404
		protected override void LogRegistrationError(TimeSpan retryInterval, LocalizedException exception)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnabletoRegisterForIPGatewayADNotifications, null, new object[]
			{
				retryInterval.Minutes,
				exception.Message
			});
		}
	}
}
