using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001D1 RID: 465
	internal class ServerNotificationHandler : GenericADNotificationHandler<Server>
	{
		// Token: 0x06000D8E RID: 3470 RVA: 0x0003C344 File Offset: 0x0003A544
		protected override void LogRegistrationError(TimeSpan retryInterval, LocalizedException exception)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnabletoRegisterForServerADNotifications, null, new object[]
			{
				retryInterval.Minutes,
				exception.Message
			});
		}
	}
}
