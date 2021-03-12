using System;
using System.Web;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000201 RID: 513
	public class OwaUrl
	{
		// Token: 0x060013E5 RID: 5093 RVA: 0x00047EBD File Offset: 0x000460BD
		private OwaUrl(string path)
		{
			this.path = path;
			this.url = this.ApplicationVRoot + path;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00047EF3 File Offset: 0x000460F3
		public static OwaUrl ApplicationRoot
		{
			get
			{
				return OwaUrl.applicationRoot;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00047EFA File Offset: 0x000460FA
		public static OwaUrl Default14Page
		{
			get
			{
				return OwaUrl.default14Page;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00047F01 File Offset: 0x00046101
		public static OwaUrl Default15Page
		{
			get
			{
				return OwaUrl.default15Page;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00047F08 File Offset: 0x00046108
		public static OwaUrl PLT1Page
		{
			get
			{
				return OwaUrl.plt1Page;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00047F0F File Offset: 0x0004610F
		public static OwaUrl SessionDataPage
		{
			get
			{
				return OwaUrl.sessionDataPage;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x00047F16 File Offset: 0x00046116
		public static OwaUrl PreloadSessionDataPage
		{
			get
			{
				return OwaUrl.preloadSessionDataPage;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00047F1D File Offset: 0x0004611D
		public static OwaUrl RemoteNotification
		{
			get
			{
				return OwaUrl.remoteNotification;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x00047F24 File Offset: 0x00046124
		public static OwaUrl GroupSubscription
		{
			get
			{
				return OwaUrl.groupSubscription;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00047F2B File Offset: 0x0004612B
		public static OwaUrl OehUrl
		{
			get
			{
				return OwaUrl.oehUrl;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x00047F32 File Offset: 0x00046132
		public static OwaUrl AttachmentHandler
		{
			get
			{
				return OwaUrl.attachmentHandler;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00047F39 File Offset: 0x00046139
		public static OwaUrl AuthFolderUrl
		{
			get
			{
				return OwaUrl.authFolderUrl;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00047F40 File Offset: 0x00046140
		public static OwaUrl ErrorPage
		{
			get
			{
				return OwaUrl.errorPage;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00047F47 File Offset: 0x00046147
		public static OwaUrl Error2Page
		{
			get
			{
				return OwaUrl.error2Page;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00047F4E File Offset: 0x0004614E
		public static OwaUrl RedirectionPage
		{
			get
			{
				return OwaUrl.redirectionPage;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00047F55 File Offset: 0x00046155
		public static OwaUrl ProxyLogon
		{
			get
			{
				return OwaUrl.proxyLogon;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00047F5C File Offset: 0x0004615C
		public static OwaUrl LanguagePage
		{
			get
			{
				return OwaUrl.languagePage;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00047F63 File Offset: 0x00046163
		public static OwaUrl LanguagePost
		{
			get
			{
				return OwaUrl.languagePost;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00047F6A File Offset: 0x0004616A
		public static OwaUrl SignOutPage
		{
			get
			{
				return OwaUrl.signOutPage;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x00047F71 File Offset: 0x00046171
		public static OwaUrl LogonFBA
		{
			get
			{
				return OwaUrl.logonFBA;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00047F78 File Offset: 0x00046178
		public static OwaUrl LogonFBAOWABlockedByClientAccessRules
		{
			get
			{
				return OwaUrl.logonFBAOWABlockedByClientAccessRules;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x00047F7F File Offset: 0x0004617F
		public static OwaUrl LogonFBAEACBlockedByClientAccessRules
		{
			get
			{
				return OwaUrl.logonFBAEACBlockedByClientAccessRules;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x00047F86 File Offset: 0x00046186
		public static OwaUrl LogoffOwa
		{
			get
			{
				return OwaUrl.logoffOwa;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x00047F8D File Offset: 0x0004618D
		public static OwaUrl LogoffBlockedByClientAccessRules
		{
			get
			{
				return OwaUrl.logoffBlockedByClientAccessRules;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x00047F94 File Offset: 0x00046194
		public static OwaUrl LogoffChangePassword
		{
			get
			{
				return OwaUrl.logoffChangePassword;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x00047F9B File Offset: 0x0004619B
		public static OwaUrl LogoffAspxPage
		{
			get
			{
				return OwaUrl.logoffAspxPage;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00047FA2 File Offset: 0x000461A2
		public static OwaUrl LogoffPageBlockedByClientAccessRules
		{
			get
			{
				return OwaUrl.logoffPageBlockedByClientAccessRules;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00047FA9 File Offset: 0x000461A9
		public static OwaUrl LogoffChangePasswordPage
		{
			get
			{
				return OwaUrl.logoffChangePasswordPage;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x00047FB0 File Offset: 0x000461B0
		public static OwaUrl InfoFailedToSaveCulture
		{
			get
			{
				return OwaUrl.infoFailedToSaveCulture;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00047FB7 File Offset: 0x000461B7
		public static OwaUrl ProxyPing
		{
			get
			{
				return OwaUrl.proxyPing;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00047FBE File Offset: 0x000461BE
		public static OwaUrl KeepAlive
		{
			get
			{
				return OwaUrl.keepAlive;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00047FC5 File Offset: 0x000461C5
		public static OwaUrl AuthPost
		{
			get
			{
				return OwaUrl.authPost;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00047FCC File Offset: 0x000461CC
		public static OwaUrl AuthDll
		{
			get
			{
				return OwaUrl.authDll;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00047FD3 File Offset: 0x000461D3
		public static OwaUrl PublishedCalendar
		{
			get
			{
				return OwaUrl.publishedCalendar;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x00047FDA File Offset: 0x000461DA
		public static OwaUrl PublishedICal
		{
			get
			{
				return OwaUrl.publishedICal;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00047FE1 File Offset: 0x000461E1
		public string ImplicitUrl
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00047FE9 File Offset: 0x000461E9
		public static OwaUrl Create(string path)
		{
			return new OwaUrl(path);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00047FF4 File Offset: 0x000461F4
		public string GetExplicitUrl(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			string text = this.ApplicationVRoot;
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

		// Token: 0x04000AB1 RID: 2737
		public const string AuthFolder = "auth";

		// Token: 0x04000AB2 RID: 2738
		public const string Oeh = "ev.owa2";

		// Token: 0x04000AB3 RID: 2739
		internal const string ReasonParameterName = "reason";

		// Token: 0x04000AB4 RID: 2740
		internal const string BlockedByClientAccessRulesReason = "5";

		// Token: 0x04000AB5 RID: 2741
		internal const string EACBlockedByClientAccessRulesReason = "6";

		// Token: 0x04000AB6 RID: 2742
		internal const string LogOffCARParamName = "CARBlock";

		// Token: 0x04000AB7 RID: 2743
		internal const string LogOffCARParamValue = "1";

		// Token: 0x04000AB8 RID: 2744
		public readonly string ApplicationVRoot = HttpRuntime.AppDomainAppVirtualPath + "/";

		// Token: 0x04000AB9 RID: 2745
		private readonly string path;

		// Token: 0x04000ABA RID: 2746
		private readonly string url;

		// Token: 0x04000ABB RID: 2747
		private static OwaUrl applicationRoot = OwaUrl.Create(string.Empty);

		// Token: 0x04000ABC RID: 2748
		private static OwaUrl default14Page = OwaUrl.Create("owa14.aspx");

		// Token: 0x04000ABD RID: 2749
		private static OwaUrl default15Page = OwaUrl.Create("default.aspx");

		// Token: 0x04000ABE RID: 2750
		private static OwaUrl sessionDataPage = OwaUrl.Create("sessiondata.ashx");

		// Token: 0x04000ABF RID: 2751
		private static OwaUrl preloadSessionDataPage = OwaUrl.Create("preloadsessiondata.ashx");

		// Token: 0x04000AC0 RID: 2752
		private static OwaUrl plt1Page = OwaUrl.Create("plt1.ashx");

		// Token: 0x04000AC1 RID: 2753
		private static OwaUrl remoteNotification = OwaUrl.Create("remotenotification.ashx");

		// Token: 0x04000AC2 RID: 2754
		private static OwaUrl groupSubscription = OwaUrl.Create("groupsubscription.ashx");

		// Token: 0x04000AC3 RID: 2755
		private static OwaUrl oehUrl = OwaUrl.Create("ev.owa2");

		// Token: 0x04000AC4 RID: 2756
		private static OwaUrl attachmentHandler = OwaUrl.Create("attachment.ashx");

		// Token: 0x04000AC5 RID: 2757
		private static OwaUrl authFolderUrl = OwaUrl.Create("auth/");

		// Token: 0x04000AC6 RID: 2758
		private static OwaUrl errorPage = OwaUrl.Create("auth/error.aspx");

		// Token: 0x04000AC7 RID: 2759
		private static OwaUrl error2Page = OwaUrl.Create("auth/error2.aspx");

		// Token: 0x04000AC8 RID: 2760
		private static OwaUrl redirectionPage = OwaUrl.Create("redir.aspx");

		// Token: 0x04000AC9 RID: 2761
		private static OwaUrl proxyLogon = OwaUrl.Create("proxyLogon.owa");

		// Token: 0x04000ACA RID: 2762
		private static OwaUrl languagePage = OwaUrl.Create("languageselection.aspx");

		// Token: 0x04000ACB RID: 2763
		private static OwaUrl languagePost = OwaUrl.Create("lang.owa");

		// Token: 0x04000ACC RID: 2764
		private static OwaUrl logonFBA = OwaUrl.Create("auth/logon.aspx");

		// Token: 0x04000ACD RID: 2765
		private static OwaUrl signOutPage = OwaUrl.Create(LogOnSettings.SignOutPageUrl);

		// Token: 0x04000ACE RID: 2766
		private static OwaUrl logonFBAOWABlockedByClientAccessRules = OwaUrl.Create(string.Format("auth/logon.aspx?{0}={1}", "reason", "5"));

		// Token: 0x04000ACF RID: 2767
		private static OwaUrl logonFBAEACBlockedByClientAccessRules = OwaUrl.Create(string.Format("auth/logon.aspx?{0}={1}&url={{0}}", "reason", "6"));

		// Token: 0x04000AD0 RID: 2768
		private static OwaUrl logoffOwa = OwaUrl.Create("logoff.owa");

		// Token: 0x04000AD1 RID: 2769
		private static OwaUrl logoffBlockedByClientAccessRules = OwaUrl.Create(string.Format("logoff.owa?{0}={1}", "reason", "5"));

		// Token: 0x04000AD2 RID: 2770
		private static OwaUrl logoffChangePassword = OwaUrl.Create("logoff.owa?ChgPwd=1");

		// Token: 0x04000AD3 RID: 2771
		private static OwaUrl logoffAspxPage = OwaUrl.Create("auth/logoff.aspx?Cmd=logoff&src=exch");

		// Token: 0x04000AD4 RID: 2772
		private static OwaUrl logoffPageBlockedByClientAccessRules = OwaUrl.Create("auth" + string.Format("/logoff.aspx?Cmd=logoff&{0}={1}", "CARBlock", "1"));

		// Token: 0x04000AD5 RID: 2773
		private static OwaUrl logoffChangePasswordPage = OwaUrl.Create("auth/logoff.aspx?Cmd=logoff&ChgPwd=1");

		// Token: 0x04000AD6 RID: 2774
		private static OwaUrl infoFailedToSaveCulture = OwaUrl.Create("info.aspx?Msg=1");

		// Token: 0x04000AD7 RID: 2775
		private static OwaUrl proxyPing = OwaUrl.Create("ping.owa");

		// Token: 0x04000AD8 RID: 2776
		private static OwaUrl keepAlive = OwaUrl.Create("keepalive.owa");

		// Token: 0x04000AD9 RID: 2777
		private static OwaUrl authPost = OwaUrl.Create("auth.owa");

		// Token: 0x04000ADA RID: 2778
		private static OwaUrl authDll = OwaUrl.Create("auth/owaauth.dll");

		// Token: 0x04000ADB RID: 2779
		private static OwaUrl publishedCalendar = OwaUrl.Create("calendar.html");

		// Token: 0x04000ADC RID: 2780
		private static OwaUrl publishedICal = OwaUrl.Create("calendar.ics");

		// Token: 0x04000ADD RID: 2781
		public static OwaUrl SuiteServiceProxyPage = OwaUrl.Create("SuiteServiceProxy.aspx");
	}
}
