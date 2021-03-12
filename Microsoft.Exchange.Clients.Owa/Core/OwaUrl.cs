using System;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000206 RID: 518
	public class OwaUrl
	{
		// Token: 0x0600117E RID: 4478 RVA: 0x00069BC0 File Offset: 0x00067DC0
		private OwaUrl(string path)
		{
			this.path = path;
			this.url = OwaUrl.applicationVRoot + path;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00069BE0 File Offset: 0x00067DE0
		private static OwaUrl Create(string path)
		{
			return new OwaUrl(path);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00069BE8 File Offset: 0x00067DE8
		public string GetExplicitUrl(OwaContext owaContext)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			return this.GetExplicitUrl(owaContext.HttpContext.Request);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00069C0C File Offset: 0x00067E0C
		public string GetExplicitUrl(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			string text = OwaUrl.applicationVRoot;
			string text2 = request.Headers["X-OWA-ExplicitLogonUser"];
			if (!string.IsNullOrEmpty(text2))
			{
				text = text + text2 + "/";
			}
			if (this.path != null)
			{
				text += this.path;
			}
			return text;
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00069C69 File Offset: 0x00067E69
		public string ImplicitUrl
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x04000BB2 RID: 2994
		private static readonly string applicationVRoot = HttpRuntime.AppDomainAppVirtualPath + "/";

		// Token: 0x04000BB3 RID: 2995
		private static readonly string authFolder = "auth";

		// Token: 0x04000BB4 RID: 2996
		private static readonly string oeh = "ev.owa";

		// Token: 0x04000BB5 RID: 2997
		public static OwaUrl ApplicationRoot = OwaUrl.Create(string.Empty);

		// Token: 0x04000BB6 RID: 2998
		public static OwaUrl Default14Page = OwaUrl.Create("owa14.aspx");

		// Token: 0x04000BB7 RID: 2999
		public static OwaUrl Default15Page = OwaUrl.Create("default.aspx");

		// Token: 0x04000BB8 RID: 3000
		public static OwaUrl Oeh = OwaUrl.Create(OwaUrl.oeh);

		// Token: 0x04000BB9 RID: 3001
		public static OwaUrl AttachmentHandler = OwaUrl.Create("attachment.ashx");

		// Token: 0x04000BBA RID: 3002
		public static OwaUrl AuthFolder = OwaUrl.Create(OwaUrl.authFolder + "/");

		// Token: 0x04000BBB RID: 3003
		public static OwaUrl ErrorPage = OwaUrl.Create(OwaUrl.authFolder + "/error.aspx");

		// Token: 0x04000BBC RID: 3004
		public static OwaUrl Error2Page = OwaUrl.Create(OwaUrl.authFolder + "/error2.aspx");

		// Token: 0x04000BBD RID: 3005
		public static OwaUrl RedirectionPage = OwaUrl.Create("redir.aspx");

		// Token: 0x04000BBE RID: 3006
		public static OwaUrl ProxyLogon = OwaUrl.Create("proxyLogon.owa");

		// Token: 0x04000BBF RID: 3007
		public static OwaUrl CasRedirectPage = OwaUrl.Create("casredirect.aspx");

		// Token: 0x04000BC0 RID: 3008
		public static OwaUrl LanguagePage = OwaUrl.Create("languageselection.aspx");

		// Token: 0x04000BC1 RID: 3009
		public static OwaUrl LanguagePost = OwaUrl.Create("lang.owa");

		// Token: 0x04000BC2 RID: 3010
		public static OwaUrl LogonFBA = OwaUrl.Create("auth/logon.aspx");

		// Token: 0x04000BC3 RID: 3011
		public static OwaUrl Logoff = OwaUrl.Create("logoff.owa");

		// Token: 0x04000BC4 RID: 3012
		public static OwaUrl LogoffChangePassword = OwaUrl.Create("logoff.owa?ChgPwd=1");

		// Token: 0x04000BC5 RID: 3013
		public static OwaUrl LogoffPage = OwaUrl.Create(OwaUrl.authFolder + "/logoff.aspx?Cmd=logoff&src=exch");

		// Token: 0x04000BC6 RID: 3014
		public static OwaUrl LogoffChangePasswordPage = OwaUrl.Create(OwaUrl.authFolder + "/logoff.aspx?Cmd=logoff&ChgPwd=1");

		// Token: 0x04000BC7 RID: 3015
		public static OwaUrl InfoFailedToSaveCulture = OwaUrl.Create("info.aspx?Msg=1");

		// Token: 0x04000BC8 RID: 3016
		public static OwaUrl ProxyHandler = OwaUrl.Create(OwaUrl.oeh + "?oeh=1&ns=HttpProxy&ev=ProxyRequest");

		// Token: 0x04000BC9 RID: 3017
		public static OwaUrl ProxyLanguagePost = OwaUrl.Create(OwaUrl.oeh + "?oeh=1&ns=HttpProxy&ev=LanguagePost");

		// Token: 0x04000BCA RID: 3018
		public static OwaUrl ProxyPing = OwaUrl.Create("ping.owa");

		// Token: 0x04000BCB RID: 3019
		public static OwaUrl ProxyEws = OwaUrl.Create(OwaUrl.oeh + "?oeh=1&ns=EwsProxy&ev=Proxy");

		// Token: 0x04000BCC RID: 3020
		public static OwaUrl HealthPing = OwaUrl.Create(OwaUrl.oeh + "?oeh=1&ns=Monitoring&ev=Ping");

		// Token: 0x04000BCD RID: 3021
		public static OwaUrl KeepAlive = OwaUrl.Create("keepalive.owa");

		// Token: 0x04000BCE RID: 3022
		public static OwaUrl AuthPost = OwaUrl.Create("auth.owa");

		// Token: 0x04000BCF RID: 3023
		public static OwaUrl AuthDll = OwaUrl.Create("auth/owaauth.dll");

		// Token: 0x04000BD0 RID: 3024
		public static OwaUrl PublishedCalendar = OwaUrl.Create("calendar.html");

		// Token: 0x04000BD1 RID: 3025
		public static OwaUrl ReachPublishedCalendar = OwaUrl.Create("reachcalendar.html");

		// Token: 0x04000BD2 RID: 3026
		public static OwaUrl PublishedICal = OwaUrl.Create("calendar.ics");

		// Token: 0x04000BD3 RID: 3027
		public static OwaUrl ReachPublishedICal = OwaUrl.Create("reachcalendar.ics");

		// Token: 0x04000BD4 RID: 3028
		public static OwaUrl WebReadyUrl = OwaUrl.Create("WebReadyView");

		// Token: 0x04000BD5 RID: 3029
		private string path;

		// Token: 0x04000BD6 RID: 3030
		private string url;
	}
}
