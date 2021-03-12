using System;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020003F7 RID: 1015
	public static class ExtendedReportService
	{
		// Token: 0x06003387 RID: 13191 RVA: 0x000A1008 File Offset: 0x0009F208
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
