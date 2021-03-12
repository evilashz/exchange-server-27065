using System;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200021E RID: 542
	public static class MessageTraceExService
	{
		// Token: 0x0600275E RID: 10078 RVA: 0x0007BB4C File Offset: 0x00079D4C
		public static DateTime CalculateDate(string date, string timeZoneName)
		{
			ExTimeZone utcTimeZone;
			if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(timeZoneName, out utcTimeZone))
			{
				utcTimeZone = ExTimeZone.UtcTimeZone;
			}
			DateTime dateTime = DateTime.ParseExact(date, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
			ExDateTime exDateTime = new ExDateTime(utcTimeZone, dateTime);
			return exDateTime.UniversalTime;
		}
	}
}
