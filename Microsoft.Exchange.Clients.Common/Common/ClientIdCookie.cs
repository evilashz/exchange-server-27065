using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000012 RID: 18
	public class ClientIdCookie
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00006845 File Offset: 0x00004A45
		private ClientIdCookie()
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006850 File Offset: 0x00004A50
		public static string GetCookieValueAndSetIfNull(HttpContext httpContext)
		{
			ClientIdCookie clientIdCookie = new ClientIdCookie();
			return clientIdCookie.GetCookieValueAndSetIfNullInternal(httpContext);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000686C File Offset: 0x00004A6C
		public static string ParseToPrintableString(string cookieValue)
		{
			if (cookieValue.Length <= 12)
			{
				cookieValue = cookieValue.PadRight(16, ' ');
			}
			return string.Format("{0} - {1} - {2} - {3}", new object[]
			{
				cookieValue.Substring(0, 4),
				cookieValue.Substring(4, 4),
				cookieValue.Substring(8, 4),
				cookieValue.Substring(12)
			});
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000068D0 File Offset: 0x00004AD0
		private string GetCookieValueAndSetIfNullInternal(HttpContext httpContext)
		{
			if (httpContext == null)
			{
				return string.Empty;
			}
			HttpCookie httpCookie = httpContext.Request.Cookies["ClientId"];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie("ClientId");
				httpCookie.Value = this.GetUniqueClientIdCookieValue();
				httpCookie.HttpOnly = true;
				httpCookie.Expires = DateTime.UtcNow.AddDays(365.0);
				httpContext.Response.Cookies.Add(httpCookie);
			}
			return httpCookie.Value;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006950 File Offset: 0x00004B50
		private string GetUniqueClientIdCookieValue()
		{
			Regex regex = new Regex("[^a-z,0,9,A-Z]");
			return regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), string.Empty).ToUpper();
		}

		// Token: 0x04000206 RID: 518
		public const string ClientIdCookieName = "ClientId";

		// Token: 0x04000207 RID: 519
		public const string ClientIdCookieDisplayName = "X-ClientId: ";
	}
}
