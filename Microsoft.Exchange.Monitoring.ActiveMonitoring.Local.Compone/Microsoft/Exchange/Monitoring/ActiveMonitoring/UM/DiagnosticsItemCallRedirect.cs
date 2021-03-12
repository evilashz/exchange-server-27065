using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004C4 RID: 1220
	internal class DiagnosticsItemCallRedirect : DiagnosticsItemBase
	{
		// Token: 0x06001E84 RID: 7812 RVA: 0x000B8244 File Offset: 0x000B6444
		internal static bool IsExpectedErrorId(int errorid)
		{
			return errorid == 15637 || errorid == 15643;
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x000B8258 File Offset: 0x000B6458
		public string FullRedirectTarget
		{
			get
			{
				Match match = DiagnosticsItemCallRedirect.RedirectPattern.Match(base.Reason);
				if (match.Success)
				{
					return match.Groups["uri"].Value;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x000B829C File Offset: 0x000B649C
		public DateTime RedirectTime
		{
			get
			{
				Match match = DiagnosticsItemCallRedirect.RedirectPattern.Match(base.Reason);
				DateTime dateTime;
				if (match.Success && DateTime.TryParse(match.Groups["time"].Value, out dateTime))
				{
					return dateTime.ToUniversalTime();
				}
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x000B82F0 File Offset: 0x000B64F0
		public string RedirectTarget
		{
			get
			{
				string text = this.FullRedirectTarget;
				Match match = DiagnosticsItemCallRedirect.ToUriRegex.Match(text);
				Match match2 = DiagnosticsItemCallRedirect.MsfeRegex.Match(text);
				if (match.Success)
				{
					if (match2.Success)
					{
						text = string.Format("sip:{0}:{1}", match2.Groups["msfe"].Value, match.Groups["port"].Value);
					}
					else
					{
						text = string.Format("sip:{0}:{1}", match.Groups["host"].Value, match.Groups["port"].Value);
					}
				}
				return text;
			}
		}

		// Token: 0x040015CF RID: 5583
		private static Regex RedirectPattern = new Regex("Redirecting to:(?<uri>.+?)(;time=(?<time>.+))*$");

		// Token: 0x040015D0 RID: 5584
		private static Regex ToUriRegex = new Regex("sip:(?<user>[^@]+)@(?<host>[^:]+):(?<port>\\d+);");

		// Token: 0x040015D1 RID: 5585
		private static Regex MsfeRegex = new Regex("ms-fe=(?<msfe>[^;]+)(;|$)");
	}
}
