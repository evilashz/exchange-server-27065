using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000037 RID: 55
	public sealed class Rfc822Date
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x00010F51 File Offset: 0x0000F151
		public static string Format(ExDateTime dateTime)
		{
			return Rfc822Date.Format("dd-MMM-yyyy HH:mm:ss ", dateTime);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00010F5E File Offset: 0x0000F15E
		public static string FormatLong(ExDateTime dateTime)
		{
			return Rfc822Date.Format("ddd, d MMM yyyy HH:mm:ss ", dateTime);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00010F6C File Offset: 0x0000F16C
		public static bool TryParse(string dateTimeString, out ExDateTime date)
		{
			if (string.IsNullOrEmpty(dateTimeString) || (dateTimeString.Length != 25 && dateTimeString.Length != 26))
			{
				date = ResponseFactory.CurrentExTimeZone.ConvertDateTime(ExDateTime.UtcNow);
				return false;
			}
			string s = dateTimeString.Insert(dateTimeString.Length - 2, ":");
			if (!ExDateTime.TryParseExact(s, "d-MMM-yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AllowLeadingWhite, out date))
			{
				return false;
			}
			date = ResponseFactory.CurrentExTimeZone.ConvertDateTime(date);
			return true;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00010FF4 File Offset: 0x0000F1F4
		private static string Format(string format, ExDateTime dateTime)
		{
			StringBuilder stringBuilder = new StringBuilder(40);
			TimeSpan bias = dateTime.Bias;
			stringBuilder.Append(dateTime.ToString(format, CultureInfo.InvariantCulture));
			stringBuilder.Append((bias.Hours * 100 + bias.Minutes).ToString("+0000;-0000"));
			return stringBuilder.ToString();
		}

		// Token: 0x040001F7 RID: 503
		private const string DateTimeFormat = "d-MMM-yyyy HH:mm:ss zzz";
	}
}
