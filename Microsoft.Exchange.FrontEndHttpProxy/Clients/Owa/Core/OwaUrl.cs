using System;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000061 RID: 97
	public class OwaUrl
	{
		// Token: 0x0600030A RID: 778 RVA: 0x00012B6A File Offset: 0x00010D6A
		private OwaUrl(string path)
		{
			this.path = path;
			this.url = "/owa/" + path;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00012B8A File Offset: 0x00010D8A
		public string ImplicitUrl
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00012B94 File Offset: 0x00010D94
		public string GetExplicitUrl(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			string text = OwaUrl.applicationVRoot;
			string text2 = request.Headers["X-OWA-ExplicitLogonUser"];
			if (string.IsNullOrEmpty(text2))
			{
				text2 = request.Headers["msExchEcpESOUser"];
			}
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

		// Token: 0x0600030D RID: 781 RVA: 0x00012C0A File Offset: 0x00010E0A
		private static OwaUrl Create(string path)
		{
			return new OwaUrl(path);
		}

		// Token: 0x040001FA RID: 506
		private static readonly string applicationVRoot = HttpRuntime.AppDomainAppVirtualPath + "/";

		// Token: 0x040001FB RID: 507
		private static readonly string authFolder = "auth";

		// Token: 0x040001FC RID: 508
		private static readonly string oeh = "ev.owa";

		// Token: 0x040001FD RID: 509
		public static OwaUrl ApplicationRoot = OwaUrl.Create(string.Empty);

		// Token: 0x040001FE RID: 510
		public static OwaUrl Default14Page = OwaUrl.Create("owa14.aspx");

		// Token: 0x040001FF RID: 511
		public static OwaUrl Default15Page = OwaUrl.Create("default.aspx");

		// Token: 0x04000200 RID: 512
		public static OwaUrl Oeh = OwaUrl.Create(OwaUrl.oeh);

		// Token: 0x04000201 RID: 513
		public static OwaUrl AttachmentHandler = OwaUrl.Create("attachment.ashx");

		// Token: 0x04000202 RID: 514
		public static OwaUrl AuthFolder = OwaUrl.Create(OwaUrl.authFolder + "/");

		// Token: 0x04000203 RID: 515
		public static OwaUrl ErrorPage = OwaUrl.Create(OwaUrl.authFolder + "/error.aspx");

		// Token: 0x04000204 RID: 516
		public static OwaUrl Error2Page = OwaUrl.Create(OwaUrl.authFolder + "/error2.aspx");

		// Token: 0x04000205 RID: 517
		public static OwaUrl CafeErrorPage = OwaUrl.Create(OwaUrl.authFolder + "/errorFE.aspx");

		// Token: 0x04000206 RID: 518
		public static OwaUrl RedirectionPage = OwaUrl.Create("redir.aspx");

		// Token: 0x04000207 RID: 519
		public static OwaUrl ProxyLogon = OwaUrl.Create("proxyLogon.owa");

		// Token: 0x04000208 RID: 520
		public static OwaUrl CasRedirectPage = OwaUrl.Create("casredirect.aspx");

		// Token: 0x04000209 RID: 521
		public static OwaUrl LanguagePage = OwaUrl.Create("languageselection.aspx");

		// Token: 0x0400020A RID: 522
		public static OwaUrl LanguagePost = OwaUrl.Create("lang.owa");

		// Token: 0x0400020B RID: 523
		public static OwaUrl LogonFBA = OwaUrl.Create("auth/logon.aspx");

		// Token: 0x0400020C RID: 524
		public static OwaUrl Logoff = OwaUrl.Create("logoff.owa");

		// Token: 0x0400020D RID: 525
		public static OwaUrl LogoffChangePassword = OwaUrl.Create("logoff.owa?ChgPwd=1");

		// Token: 0x0400020E RID: 526
		public static OwaUrl LogoffPage = OwaUrl.Create(OwaUrl.authFolder + "/logoff.aspx?Cmd=logoff&src=exch");

		// Token: 0x0400020F RID: 527
		public static OwaUrl LogoffChangePasswordPage = OwaUrl.Create(OwaUrl.authFolder + "/logoff.aspx?Cmd=logoff&ChgPwd=1");

		// Token: 0x04000210 RID: 528
		public static OwaUrl InfoFailedToSaveCulture = OwaUrl.Create("info.aspx?Msg=1");

		// Token: 0x04000211 RID: 529
		public static OwaUrl ProxyHandler = OwaUrl.Create(OwaUrl.oeh + "?oeh=1&ns=HttpProxy&ev=ProxyRequest");

		// Token: 0x04000212 RID: 530
		public static OwaUrl ProxyLanguagePost = OwaUrl.Create(OwaUrl.oeh + "?oeh=1&ns=HttpProxy&ev=LanguagePost");

		// Token: 0x04000213 RID: 531
		public static OwaUrl ProxyPing = OwaUrl.Create("ping.owa");

		// Token: 0x04000214 RID: 532
		public static OwaUrl HealthPing = OwaUrl.Create(OwaUrl.oeh + "?oeh=1&ns=Monitoring&ev=Ping");

		// Token: 0x04000215 RID: 533
		public static OwaUrl KeepAlive = OwaUrl.Create("keepalive.owa");

		// Token: 0x04000216 RID: 534
		public static OwaUrl AuthPost = OwaUrl.Create("auth.owa");

		// Token: 0x04000217 RID: 535
		public static OwaUrl AuthDll = OwaUrl.Create("auth/owaauth.dll");

		// Token: 0x04000218 RID: 536
		public static OwaUrl PublishedCalendar = OwaUrl.Create("calendar.html");

		// Token: 0x04000219 RID: 537
		public static OwaUrl PublishedICal = OwaUrl.Create("calendar.ics");

		// Token: 0x0400021A RID: 538
		public static OwaUrl SuiteServiceProxyPage = OwaUrl.Create("SuiteServiceProxy.aspx");

		// Token: 0x0400021B RID: 539
		private string path;

		// Token: 0x0400021C RID: 540
		private string url;
	}
}
