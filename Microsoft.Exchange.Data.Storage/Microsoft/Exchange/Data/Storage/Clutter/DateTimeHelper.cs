using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x0200043A RID: 1082
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class DateTimeHelper
	{
		// Token: 0x06003068 RID: 12392 RVA: 0x000C6FD8 File Offset: 0x000C51D8
		public static ExDateTime GetFutureTimestamp(ExDateTime startTime, int afterMinimumDays, DayOfWeek onDayOfWeek, TimeSpan atTimeOfDay, ExTimeZone localTimeZone)
		{
			ExDateTime exDateTime = localTimeZone.ConvertDateTime(startTime);
			int num = (exDateTime.TimeOfDay > atTimeOfDay) ? 1 : 0;
			ExDateTime exDateTime2 = exDateTime.AddDays((double)(afterMinimumDays + num));
			int num2 = onDayOfWeek - exDateTime2.DayOfWeek;
			if (num2 < 0)
			{
				num2 += 7;
			}
			return exDateTime2.AddDays((double)num2).Date.Add(atTimeOfDay).ToUtc();
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000C7045 File Offset: 0x000C5245
		public static ExTimeZone GetUserTimeZoneOrUtc(MailboxSession session)
		{
			return DateTimeHelper.GetUserTimeZoneOrDefault(session, ExTimeZone.UtcTimeZone);
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000C7054 File Offset: 0x000C5254
		public static ExTimeZone GetUserTimeZoneOrDefault(MailboxSession session, ExTimeZone defaultTimeZone)
		{
			ExTimeZone exTimeZone = session.ExTimeZone;
			try
			{
				exTimeZone = TimeZoneHelper.GetUserTimeZone(session);
			}
			catch (TransientException arg)
			{
				InferenceDiagnosticsLog.Log("NotificationManager", string.Format("Ignoring transient exception caught while loading user time zone and defaulting to UTC time zone. Exception: {0}", arg));
			}
			catch (LocalizedException arg2)
			{
				InferenceDiagnosticsLog.Log("NotificationManager", string.Format("Ignoring localized exception caught while loading user time zone and defaulting to UTC time zone. Exception: {0}", arg2));
			}
			exTimeZone = (exTimeZone ?? defaultTimeZone);
			return exTimeZone;
		}
	}
}
