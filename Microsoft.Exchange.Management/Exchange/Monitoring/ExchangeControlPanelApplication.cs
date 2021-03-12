using System;
using System.Net;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000527 RID: 1319
	internal class ExchangeControlPanelApplication : ExchangeWebApplication
	{
		// Token: 0x06002F72 RID: 12146 RVA: 0x000BF12C File Offset: 0x000BD32C
		public ExchangeControlPanelApplication(string virtualDirectory, WebSession webSession) : base(virtualDirectory, webSession)
		{
			webSession.SendingRequest += this.webSession_SendingRequest;
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000BF148 File Offset: 0x000BD348
		protected override bool IsLanguageSelectionResponse(RedirectResponse response)
		{
			return response.IsRedirect && response.RedirectUrl.IndexOf("/owa/languageselection.aspx", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000BF16B File Offset: 0x000BD36B
		public override bool ValidateLogin()
		{
			return base.ValidateLogin() && null != base.GetCookie("ASP.NET_SessionId");
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000BF188 File Offset: 0x000BD388
		private void webSession_SendingRequest(object sender, HttpWebRequestEventArgs e)
		{
			e.Request.Headers.Set("msExchEcpOutboundProxyVersion", "2");
			Cookie cookie = base.GetCookie("msExchEcpCanary");
			if (cookie != null)
			{
				e.Request.Headers.Set("msExchEcpCanary", cookie.Value);
			}
			cookie = base.GetCookie("msExchEcpCanary.UID");
			if (cookie != null)
			{
				e.Request.Headers.Set("msExchEcpCanary.UID", cookie.Value);
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x000BF203 File Offset: 0x000BD403
		protected Cookie AspNetSessionCookie
		{
			get
			{
				return base.GetCookie("ASP.NET_SessionId");
			}
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000BF234 File Offset: 0x000BD434
		public void Ping(Action<ExchangeControlPanelApplication.PingResponse> onStatusAvailable, Action<Exception> onError)
		{
			base.Get<ExchangeControlPanelApplication.PingResponse>("exhealth.check", delegate(ExchangeControlPanelApplication.PingResponse ping)
			{
				onStatusAvailable(ping);
			}, delegate(Exception ex)
			{
				onError(ex);
			});
		}

		// Token: 0x02000528 RID: 1320
		public static class EcpPaths
		{
			// Token: 0x040021E5 RID: 8677
			public const string Main = "";

			// Token: 0x040021E6 RID: 8678
			public const string Ping = "exhealth.check";
		}

		// Token: 0x02000529 RID: 1321
		public static class Headers
		{
			// Token: 0x040021E7 RID: 8679
			public const string Session = "ASP.NET_SessionId";

			// Token: 0x040021E8 RID: 8680
			public const string Canary = "msExchEcpCanary";

			// Token: 0x040021E9 RID: 8681
			public const string CanaryFromUserId = "msExchEcpCanary.UID";

			// Token: 0x040021EA RID: 8682
			public const string OutboundProxyVersion = "msExchEcpOutboundProxyVersion";

			// Token: 0x040021EB RID: 8683
			public const string CurrentOutboundServerVersion = "2";

			// Token: 0x040021EC RID: 8684
			public const string ApplicationVersion = "msExchEcpVersion";
		}

		// Token: 0x0200052A RID: 1322
		public class PingResponse : WebApplicationResponse
		{
			// Token: 0x17000E21 RID: 3617
			// (get) Token: 0x06002F78 RID: 12152 RVA: 0x000BF278 File Offset: 0x000BD478
			public bool IsAlive
			{
				get
				{
					return base.StatusCode == HttpStatusCode.OK;
				}
			}

			// Token: 0x17000E22 RID: 3618
			// (get) Token: 0x06002F79 RID: 12153 RVA: 0x000BF287 File Offset: 0x000BD487
			// (set) Token: 0x06002F7A RID: 12154 RVA: 0x000BF28F File Offset: 0x000BD48F
			public Version ApplicationVersion { get; private set; }

			// Token: 0x06002F7B RID: 12155 RVA: 0x000BF298 File Offset: 0x000BD498
			public override void SetResponse(HttpWebResponse response)
			{
				base.SetResponse(response);
				string text = response.Headers["msExchEcpVersion"];
				this.ApplicationVersion = (string.IsNullOrEmpty(text) ? new Version() : new Version(text));
			}
		}
	}
}
