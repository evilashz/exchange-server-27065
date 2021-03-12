using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Services.OnlineMeetings.Autodiscover.DataContract;
using Microsoft.Exchange.Services.OnlineMeetings.ResourceContract;

namespace Microsoft.Exchange.Services.OnlineMeetings.Autodiscover
{
	// Token: 0x0200002D RID: 45
	internal static class AutodiscoverExtensions
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x0000634D File Offset: 0x0000454D
		internal static string RootRedirectUrl(this AutodiscoverResponse thisObject)
		{
			return AutodiscoverExtensions.GetRootLinkToken(thisObject, "redirect");
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000635A File Offset: 0x0000455A
		internal static string RootOAuthToken(this AutodiscoverResponse thisObject)
		{
			return AutodiscoverExtensions.GetRootLinkToken(thisObject, "oauth");
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006368 File Offset: 0x00004568
		internal static string UserRedirectUrl(this AutodiscoverResponse thisObject)
		{
			string userLinkToken = AutodiscoverExtensions.GetUserLinkToken(thisObject, "redirect");
			if (string.IsNullOrEmpty(userLinkToken))
			{
				return thisObject.RootOAuthToken();
			}
			return userLinkToken;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006391 File Offset: 0x00004591
		internal static string UserExternalUcwaToken(this AutodiscoverResponse thisObject)
		{
			return AutodiscoverExtensions.GetUserLinkToken(thisObject, "external/ucwa");
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000639E File Offset: 0x0000459E
		internal static string UserInternalUcwaToken(this AutodiscoverResponse thisObject)
		{
			return AutodiscoverExtensions.GetUserLinkToken(thisObject, "internal/ucwa");
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000063AB File Offset: 0x000045AB
		internal static string GetRequestHeadersAsString(this WebRequest thisObject)
		{
			if (thisObject != null)
			{
				return thisObject.Headers.GetWebHeaderCollectionAsString();
			}
			return "Count:0";
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000063C1 File Offset: 0x000045C1
		internal static string GetResponseHeadersAsString(this WebResponse thisObject)
		{
			if (thisObject != null)
			{
				return thisObject.Headers.GetWebHeaderCollectionAsString();
			}
			return "Count:0";
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000063D8 File Offset: 0x000045D8
		internal static string GetWebHeaderCollectionAsString(this WebHeaderCollection thisObject)
		{
			if (thisObject != null)
			{
				StringBuilder stringBuilder = new StringBuilder(string.Format("Count:{0}", thisObject.Count));
				try
				{
					foreach (object obj in thisObject.Keys)
					{
						string text = (string)obj;
						string arg = (string.Compare(text, WellKnownHeader.Authorization, StringComparison.OrdinalIgnoreCase) != 0) ? thisObject[text] : "[redacted]";
						stringBuilder.AppendFormat("\n{0}:{1}", text, arg);
					}
				}
				catch (InvalidOperationException)
				{
				}
				return stringBuilder.ToString();
			}
			return "Count:0";
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006498 File Offset: 0x00004698
		internal static string GetResponseBodyAsString(this WebResponse thisObject)
		{
			if (thisObject != null)
			{
				using (Stream responseStream = thisObject.GetResponseStream())
				{
					if (responseStream != null)
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							responseStream.Seek(0L, SeekOrigin.Begin);
							return streamReader.ReadToEnd();
						}
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000653C File Offset: 0x0000473C
		private static string GetUserLinkToken(AutodiscoverResponse response, string tokenName)
		{
			if (string.IsNullOrEmpty(tokenName))
			{
				return string.Empty;
			}
			IEnumerable<string> source = from links in response.UserLinks
			where string.Compare(links.Token.ToLowerInvariant(), tokenName.ToLowerInvariant(), StringComparison.Ordinal) == 0
			select links.Href;
			return source.FirstOrDefault<string>();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000065DC File Offset: 0x000047DC
		private static string GetRootLinkToken(AutodiscoverResponse response, string tokenName)
		{
			if (string.IsNullOrEmpty(tokenName))
			{
				return string.Empty;
			}
			IEnumerable<string> source = from links in response.RootLinks
			where string.Compare(links.Token.ToLowerInvariant(), tokenName.ToLowerInvariant(), StringComparison.Ordinal) == 0
			select links.Href;
			return source.FirstOrDefault<string>();
		}

		// Token: 0x0400012D RID: 301
		private const string ZeroCountString = "Count:0";

		// Token: 0x0400012E RID: 302
		private const string RedirectToken = "redirect";

		// Token: 0x0400012F RID: 303
		private const string OAuthToken = "oauth";

		// Token: 0x04000130 RID: 304
		private const string ExternalUcwaToken = "external/ucwa";

		// Token: 0x04000131 RID: 305
		private const string InternalUcwaToken = "internal/ucwa";
	}
}
