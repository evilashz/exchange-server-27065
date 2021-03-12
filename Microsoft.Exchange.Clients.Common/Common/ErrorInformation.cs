using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200001A RID: 26
	public class ErrorInformation
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00007204 File Offset: 0x00005404
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000720C File Offset: 0x0000540C
		public SupportLevel? SupportLevel { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00007228 File Offset: 0x00005428
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00007230 File Offset: 0x00005430
		public Exception Exception { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00007239 File Offset: 0x00005439
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00007241 File Offset: 0x00005441
		public string Message { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000724A File Offset: 0x0000544A
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00007252 File Offset: 0x00005452
		public ErrorMode? Mode { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000725B File Offset: 0x0000545B
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00007263 File Offset: 0x00005463
		public Strings.IDs? MessageId { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000726C File Offset: 0x0000546C
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00007289 File Offset: 0x00005489
		public string MessageParameter
		{
			get
			{
				if (this.messageParameters.Count == 0)
				{
					return null;
				}
				return this.messageParameters[0];
			}
			set
			{
				if (this.messageParameters.Count == 0)
				{
					this.messageParameters.Add(value);
					return;
				}
				this.messageParameters[0] = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000072B2 File Offset: 0x000054B2
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000072BA File Offset: 0x000054BA
		public bool SendWatsonReport { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000072C3 File Offset: 0x000054C3
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000072CB File Offset: 0x000054CB
		public bool SharePointApp { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000072D4 File Offset: 0x000054D4
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000072DC File Offset: 0x000054DC
		public bool SiteMailbox { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000072E5 File Offset: 0x000054E5
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000072ED File Offset: 0x000054ED
		public string GroupMailboxDestination { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000072F6 File Offset: 0x000054F6
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000072FE File Offset: 0x000054FE
		public string Lids { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00007307 File Offset: 0x00005507
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000730F File Offset: 0x0000550F
		public string CustomParameterName { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00007318 File Offset: 0x00005518
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00007320 File Offset: 0x00005520
		public string CustomParameterValue { get; set; }

		// Token: 0x060000D0 RID: 208 RVA: 0x0000732C File Offset: 0x0000552C
		public static List<string> ParseMessageParameters(string errorMessage, HttpRequest request)
		{
			int parameterCount = ErrorInformation.GetParameterCount(errorMessage);
			List<string> list = new List<string>();
			string item;
			if ((item = request.QueryString["msgParam"]) != null)
			{
				list.Add(item);
				int num = 1;
				while (num < parameterCount && (item = request.QueryString["msgParam" + num]) != null)
				{
					list.Add(item);
					num++;
				}
			}
			if (list.Count < parameterCount)
			{
				throw new ArgumentException("Error message had less format parameters than were passed in the query string");
			}
			return list;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000073A8 File Offset: 0x000055A8
		private static int GetParameterCount(string message)
		{
			Regex regex = new Regex("{(?<index>[0-9]+)(:[^}])?}", RegexOptions.Compiled);
			MatchCollection matchCollection = regex.Matches(message);
			int num = -1;
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				int num2 = int.Parse(match.Result("${index}"));
				num = ((num2 > num) ? num2 : num);
			}
			return num;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007430 File Offset: 0x00005630
		public void AddMessageParameter(string param)
		{
			this.messageParameters.Add(param);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007440 File Offset: 0x00005640
		public void AppendMessageParametersToUrl(StringBuilder urlBuilder)
		{
			int num = 0;
			foreach (string str in this.messageParameters)
			{
				if (num == 0)
				{
					urlBuilder.AppendFormat("&{0}={1}", "msgParam", HttpUtility.UrlEncode(str));
				}
				else
				{
					urlBuilder.AppendFormat("&{0}={1}", "msgParam" + num, HttpUtility.UrlEncode(str));
				}
				num++;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000074D0 File Offset: 0x000056D0
		public void SetCustomParameter(string name, string value)
		{
			this.CustomParameterName = name;
			this.CustomParameterValue = value;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000074E0 File Offset: 0x000056E0
		public void AppendCustomParameterToURL(StringBuilder urlBuilder)
		{
			if (string.IsNullOrEmpty(this.CustomParameterName) || string.IsNullOrEmpty(this.CustomParameterValue))
			{
				return;
			}
			urlBuilder.AppendFormat("{0}={1}", HttpUtility.UrlEncode(this.CustomParameterName), HttpUtility.UrlEncode(this.CustomParameterValue));
		}

		// Token: 0x04000232 RID: 562
		public const string ErrorPageName = "errorfe.aspx";

		// Token: 0x04000233 RID: 563
		public const string ErrorPageUrl = "/owa/auth/errorfe.aspx";

		// Token: 0x04000234 RID: 564
		public const string FirstErrorInfoFormat = "?{0}={1}";

		// Token: 0x04000235 RID: 565
		public const string AddionalErrorInfoFormat = "&{0}={1}";

		// Token: 0x04000236 RID: 566
		public const string ErrorHttpCodeQueryStringName = "httpCode";

		// Token: 0x04000237 RID: 567
		public const string ErrorMessageQueryStringName = "msg";

		// Token: 0x04000238 RID: 568
		public const string ErrorMessageParameterQueryStringName = "msgParam";

		// Token: 0x04000239 RID: 569
		public const string AuthErrorSourceQueryStringName = "authError";

		// Token: 0x0400023A RID: 570
		public const string AuthModeQueryStringName = "m";

		// Token: 0x0400023B RID: 571
		public const string SupportLevelHeaderName = "X-OWASuppLevel";

		// Token: 0x0400023C RID: 572
		public const string SupportLevelLogKey = "suplvl";

		// Token: 0x0400023D RID: 573
		private List<string> messageParameters = new List<string>();
	}
}
