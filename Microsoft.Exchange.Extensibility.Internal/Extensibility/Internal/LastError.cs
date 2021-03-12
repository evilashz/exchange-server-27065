using System;
using System.Globalization;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000054 RID: 84
	public class LastError
	{
		// Token: 0x060002FF RID: 767 RVA: 0x00010CFC File Offset: 0x0000EEFC
		public static bool TryParseSmtpResponseString(string lastErrorDetailsString, out string smtpResponse)
		{
			SmtpResponse smtpResponse2;
			if (SmtpResponse.TryParse(lastErrorDetailsString, out smtpResponse2))
			{
				smtpResponse = lastErrorDetailsString;
				return true;
			}
			return LastError.TryParseSubstring(lastErrorDetailsString, "LED", out smtpResponse);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00010D24 File Offset: 0x0000EF24
		public static bool TryConvertLastRetryTimeToLocalTime(string lastErrorDetailsString, out string convertedString)
		{
			return LastError.TryConvertLastRetryTime(lastErrorDetailsString, DateTimeStyles.AssumeUniversal, out convertedString);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00010D2F File Offset: 0x0000EF2F
		public static bool TryConvertLastRetryTimeToUniversalTime(string lastErrorDetailsString, out string convertedString)
		{
			return LastError.TryConvertLastRetryTime(lastErrorDetailsString, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal, out convertedString);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00010D3A File Offset: 0x0000EF3A
		public LastError(string lastAttemptFqdn, IPEndPoint lastAttemptEndpoint, DateTime? retryTime, SmtpResponse errorDetails) : this(lastAttemptFqdn, lastAttemptEndpoint, retryTime, errorDetails, null)
		{
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00010D48 File Offset: 0x0000EF48
		public LastError(string lastAttemptFqdn, IPEndPoint lastAttemptEndpoint, DateTime? retryTime, SmtpResponse errorDetails, LastError innerError)
		{
			this.lastAttemptFqdn = lastAttemptFqdn;
			this.lastAttemptEndpoint = lastAttemptEndpoint;
			this.retryTime = retryTime;
			this.errorDetails = errorDetails;
			this.innerError = innerError;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00010D75 File Offset: 0x0000EF75
		public string LastAttemptFqdn
		{
			get
			{
				return this.lastAttemptFqdn;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00010D7D File Offset: 0x0000EF7D
		public IPEndPoint LastAttemptEndpoint
		{
			get
			{
				return this.lastAttemptEndpoint;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00010D85 File Offset: 0x0000EF85
		public string LastAttemptIp
		{
			get
			{
				if (this.lastAttemptEndpoint == null)
				{
					return string.Empty;
				}
				return this.lastAttemptEndpoint.Address.ToString();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00010DA5 File Offset: 0x0000EFA5
		public DateTime? LastRetryTime
		{
			get
			{
				return this.retryTime;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00010DAD File Offset: 0x0000EFAD
		public SmtpResponse LastErrorDetails
		{
			get
			{
				return this.errorDetails;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00010DB5 File Offset: 0x0000EFB5
		public LastError InnerError
		{
			get
			{
				return this.innerError;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "[{{{0}={1}}};{{{2}={3}}};{{{4}={5}}};{{{6}={7}}}]", new object[]
			{
				"LRT",
				this.LastRetryTime.ToString() ?? string.Empty,
				"LED",
				this.LastErrorDetails.ToString(),
				"FQDN",
				this.LastAttemptFqdn,
				"IP",
				this.LastAttemptIp
			});
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00010E50 File Offset: 0x0000F050
		public string GetFormattedErrorDetails()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.LastRetryTime != null)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} - ", new object[]
				{
					this.LastRetryTime.ToString()
				});
			}
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "Remote Server ", new object[0]);
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, this.GetReceivingServerErrorDetails("at "), new object[0]);
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " returned '{0}'", new object[]
			{
				this.LastErrorDetails.ToString()
			});
			return stringBuilder.ToString();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00010F14 File Offset: 0x0000F114
		public string GetReceivingServerErrorDetails(string prefix = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (prefix != null && (!string.IsNullOrEmpty(this.LastAttemptFqdn) || !string.IsNullOrEmpty(this.LastAttemptIp)))
			{
				stringBuilder.Append(prefix);
			}
			if (!string.IsNullOrEmpty(this.LastAttemptFqdn))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} ", new object[]
				{
					this.LastAttemptFqdn
				});
			}
			if (!string.IsNullOrEmpty(this.LastAttemptIp))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "({0})", new object[]
				{
					this.LastAttemptIp
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00010FB0 File Offset: 0x0000F1B0
		private static bool TryParseSubstring(string lastErrorDetailsString, string startDelimiter, out string substring)
		{
			substring = string.Empty;
			if (lastErrorDetailsString == null)
			{
				return false;
			}
			int num = LastError.IndexOf(lastErrorDetailsString, startDelimiter);
			if (num == -1)
			{
				return false;
			}
			int num2 = lastErrorDetailsString.IndexOf("}", num);
			if (num2 < num)
			{
				return false;
			}
			substring = lastErrorDetailsString.Substring(num, num2 - num);
			return true;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		private static bool TryConvertLastRetryTime(string lastErrorDetailsString, DateTimeStyles styles, out string convertedString)
		{
			convertedString = string.Empty;
			string text;
			if (!LastError.TryParseSubstring(lastErrorDetailsString, "LRT", out text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			DateTime dateTime;
			if (!DateTime.TryParse(text, null, styles, out dateTime))
			{
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder(lastErrorDetailsString);
			stringBuilder.Replace(text, dateTime.ToString());
			convertedString = stringBuilder.ToString();
			return true;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001105C File Offset: 0x0000F25C
		private static int IndexOf(string lastErrorDetailsString, string startDelimiter)
		{
			string text = string.Format("{{{0}=", startDelimiter);
			int num = lastErrorDetailsString.IndexOf(text);
			if (num == -1)
			{
				return num;
			}
			return num + text.Length;
		}

		// Token: 0x0400034F RID: 847
		private const string LRTMarker = "LRT";

		// Token: 0x04000350 RID: 848
		private const string LEDMarker = "LED";

		// Token: 0x04000351 RID: 849
		private const string FQDNMarker = "FQDN";

		// Token: 0x04000352 RID: 850
		private const string IPMarker = "IP";

		// Token: 0x04000353 RID: 851
		private const string EndDelimiter = "}";

		// Token: 0x04000354 RID: 852
		private readonly string lastAttemptFqdn;

		// Token: 0x04000355 RID: 853
		private readonly IPEndPoint lastAttemptEndpoint;

		// Token: 0x04000356 RID: 854
		private readonly DateTime? retryTime;

		// Token: 0x04000357 RID: 855
		private readonly SmtpResponse errorDetails;

		// Token: 0x04000358 RID: 856
		private readonly LastError innerError;
	}
}
