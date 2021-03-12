using System;
using System.Globalization;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006A2 RID: 1698
	public static class EcpDateTimeHelper
	{
		// Token: 0x06004898 RID: 18584 RVA: 0x000DE0BC File Offset: 0x000DC2BC
		public static string UtcToUserDateTimeString(this DateTime? dateTimeWithoutTimeZone)
		{
			if (dateTimeWithoutTimeZone == null)
			{
				return string.Empty;
			}
			return new ExDateTime(ExTimeZone.UtcTimeZone, dateTimeWithoutTimeZone.Value).ToUserDateTimeString();
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x000DE0E3 File Offset: 0x000DC2E3
		public static string UtcToUserDateTimeString(this DateTime dateTimeWithoutTimeZone)
		{
			return new ExDateTime(ExTimeZone.UtcTimeZone, dateTimeWithoutTimeZone).ToUserDateTimeString();
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x000DE0F5 File Offset: 0x000DC2F5
		public static string UtcToUserDateString(this DateTime dateTimeWithoutTimeZone)
		{
			return new ExDateTime(ExTimeZone.UtcTimeZone, dateTimeWithoutTimeZone).ToUserDateString();
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x000DE108 File Offset: 0x000DC308
		public static string ToUserWeekdayDateString(this ExDateTime? exDateTimeValue)
		{
			if (exDateTimeValue == null)
			{
				return string.Empty;
			}
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return exDateTimeValue.Value.ToUserExDateTime().ToString(EcpDateTimeHelper.GetWeekdayDateFormat(true), currentCulture);
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x000DE145 File Offset: 0x000DC345
		public static string ToUserDateTimeString(this ExDateTime? exDateTimeValue)
		{
			if (exDateTimeValue == null)
			{
				return string.Empty;
			}
			return exDateTimeValue.Value.ToUserDateTimeString();
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x000DE162 File Offset: 0x000DC362
		public static string ToUserDateString(this ExDateTime? exDateTimeValue)
		{
			if (exDateTimeValue == null)
			{
				return string.Empty;
			}
			return exDateTimeValue.Value.ToUserDateString();
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x000DE180 File Offset: 0x000DC380
		public static string ToUserDateTimeString(this ExDateTime dateTimeValue)
		{
			string format = null;
			if (EacRbacPrincipal.Instance.DateFormat != null && EacRbacPrincipal.Instance.TimeFormat != null)
			{
				format = EacRbacPrincipal.Instance.DateFormat + " " + EacRbacPrincipal.Instance.TimeFormat;
			}
			return dateTimeValue.ToUserExDateTime().ToString(format);
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x000DE1D8 File Offset: 0x000DC3D8
		public static string ToUserDateString(this ExDateTime dateTimeValue)
		{
			string format = null;
			if (EacRbacPrincipal.Instance.DateFormat != null)
			{
				format = EacRbacPrincipal.Instance.DateFormat;
			}
			return dateTimeValue.ToUserExDateTime().ToString(format);
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x000DE210 File Offset: 0x000DC410
		public static string ToLastMonth()
		{
			return ExDateTime.GetNow(EcpDateTimeHelper.GetCurrentUserTimeZone()).AddMonths(-1).ToString(Strings.LastMonthFormat);
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x000DE242 File Offset: 0x000DC442
		public static string LocalToUserDateTimeString(this DateTime dateTimeInLocalTime)
		{
			return new ExDateTime(ExTimeZone.CurrentTimeZone, dateTimeInLocalTime).ToUserDateTimeString();
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x000DE254 File Offset: 0x000DC454
		public static string LocalToUserDateTimeGeneralFormatString(this DateTime dateTimeInLocalTime)
		{
			return new ExDateTime(ExTimeZone.CurrentTimeZone, dateTimeInLocalTime).ToUserDateTimeGeneralFormatString();
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x000DE266 File Offset: 0x000DC466
		public static string ToUserDateTimeGeneralFormatString(this ExDateTime? exDateTimeValue)
		{
			if (exDateTimeValue == null)
			{
				return string.Empty;
			}
			return exDateTimeValue.Value.ToUserDateTimeGeneralFormatString();
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x000DE284 File Offset: 0x000DC484
		public static string ToUserDateTimeGeneralFormatString(this ExDateTime dateTimeValue)
		{
			return dateTimeValue.ToUserExDateTime().ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x000DE2A9 File Offset: 0x000DC4A9
		public static ExTimeZone GetCurrentUserTimeZone()
		{
			if (EacRbacPrincipal.Instance.UserTimeZone != null)
			{
				return EacRbacPrincipal.Instance.UserTimeZone;
			}
			return ExTimeZone.CurrentTimeZone;
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x000DE2C7 File Offset: 0x000DC4C7
		public static ExDateTime? ToEcpExDateTime(this string dateString)
		{
			return dateString.ToEcpExDateTime("yyyy/MM/dd");
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x000DE2D4 File Offset: 0x000DC4D4
		public static ExDateTime? ToEcpExDateTime(this string dateTimeString, string parseFormat)
		{
			if (dateTimeString.IsNullOrBlank())
			{
				return null;
			}
			ExDateTime? result;
			try
			{
				if (EacRbacPrincipal.Instance.UserTimeZone != null)
				{
					result = new ExDateTime?(ExDateTime.ParseExact(EacRbacPrincipal.Instance.UserTimeZone, dateTimeString, parseFormat, CultureInfo.InvariantCulture));
				}
				else
				{
					int browserTimeZoneOffsetMinutes = EcpDateTimeHelper.GetBrowserTimeZoneOffsetMinutes();
					result = new ExDateTime?(ExDateTime.ParseExact(ExTimeZone.UtcTimeZone, dateTimeString, parseFormat, CultureInfo.InvariantCulture).AddMinutes((double)browserTimeZoneOffsetMinutes));
				}
			}
			catch (FormatException ex)
			{
				ExTraceGlobals.EventLogTracer.TraceError<string, string>(0, 0L, "Fail to parse the date time string: {0}. Got the exception of '{1}'.", dateTimeString, ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x000DE380 File Offset: 0x000DC580
		public static ExDateTime ToUserExDateTime(this ExDateTime dateTimeValue)
		{
			if (EacRbacPrincipal.Instance.UserTimeZone != null)
			{
				return EacRbacPrincipal.Instance.UserTimeZone.ConvertDateTime(dateTimeValue);
			}
			int browserTimeZoneOffsetMinutes = EcpDateTimeHelper.GetBrowserTimeZoneOffsetMinutes();
			return dateTimeValue.ToUtc().AddMinutes((double)(-(double)browserTimeZoneOffsetMinutes));
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x000DE3C4 File Offset: 0x000DC5C4
		private static int GetBrowserTimeZoneOffsetMinutes()
		{
			int result = 0;
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies["TimeOffset"];
			if (httpCookie != null)
			{
				int.TryParse(httpCookie.Value, out result);
			}
			return result;
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x000DE400 File Offset: 0x000DC600
		public static string GetWeekdayDateFormat(bool serverSideFormat)
		{
			string text = EacRbacPrincipal.Instance.DateFormat ?? string.Empty;
			string text2 = "ddd " + text;
			int lcid = CultureInfo.CurrentCulture.LCID;
			switch (lcid)
			{
			case 1041:
			case 1042:
				break;
			default:
				if (lcid != 1055 && lcid != 1063)
				{
					goto IL_6C;
				}
				break;
			}
			if (text.Contains("ddd"))
			{
				text2 = text;
			}
			else
			{
				text2 = text + " (ddd)";
			}
			IL_6C:
			if (!serverSideFormat)
			{
				if (text2.Contains("ddd"))
				{
					text2 = text2.Replace("ddd", "'ddd'");
				}
				if (text.Contains("MMMM"))
				{
					text = text.Replace("MMMM", "'MMMM'");
				}
				else if (text.Contains("MMM"))
				{
					text = text.Replace("MMM", "'MMM'");
				}
			}
			return text2;
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x000DE4D9 File Offset: 0x000DC6D9
		public static string GetUserDateFormat()
		{
			if (EacRbacPrincipal.Instance.DateFormat != null)
			{
				return EacRbacPrincipal.Instance.DateFormat;
			}
			return "yyyy/MM/dd";
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x000DE4F8 File Offset: 0x000DC6F8
		public static DateTime GetReportStartDate()
		{
			int months = -1;
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow.Day <= 14)
			{
				months = -2;
			}
			return new DateTime(utcNow.Year, utcNow.Month, 1).AddMonths(months);
		}

		// Token: 0x0400311B RID: 12571
		public const string BrowserTimeZoneCookieName = "TimeOffset";

		// Token: 0x0400311C RID: 12572
		public const string GeneralizedDateTimeFormat = "yyyy/MM/dd HH:mm:ss";

		// Token: 0x0400311D RID: 12573
		public const string GeneralizedDateFormat = "yyyy/MM/dd";

		// Token: 0x0400311E RID: 12574
		public const string AuditSearchDateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffffzzz";
	}
}
