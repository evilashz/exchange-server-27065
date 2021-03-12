using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000525 RID: 1317
	internal abstract class ExchangeWebApplication : WebApplication
	{
		// Token: 0x06002F6E RID: 12142 RVA: 0x000BF072 File Offset: 0x000BD272
		protected ExchangeWebApplication(string virtualDirectory, WebSession webSession) : base(virtualDirectory, webSession)
		{
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x000BF07C File Offset: 0x000BD27C
		public override bool ValidateLogin()
		{
			RedirectResponse response = base.Get<RedirectResponse>("");
			if (this.IsLanguageSelectionResponse(response))
			{
				base.Post<TextResponse>("/owa/lang.owa", new HtmlFormBody
				{
					{
						"lcid",
						1033
					},
					{
						"tzid",
						"Pacific Standard Time"
					}
				});
			}
			return true;
		}

		// Token: 0x06002F70 RID: 12144
		protected abstract bool IsLanguageSelectionResponse(RedirectResponse response);

		// Token: 0x06002F71 RID: 12145 RVA: 0x000BF0D8 File Offset: 0x000BD2D8
		public static WebSession GetWebSession(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			ExchangeWebAppVirtualDirectory exchangeWebAppVirtualDirectory = (ExchangeWebAppVirtualDirectory)instance.VirtualDirectory;
			Uri baseUri = instance.baseUri;
			NetworkCredential credentials = instance.credentials;
			if (exchangeWebAppVirtualDirectory.LiveIdAuthentication)
			{
				return new WindowsLiveIdWebSession(baseUri, credentials, instance.LiveIdAuthenticationConfiguration);
			}
			if (exchangeWebAppVirtualDirectory.FormsAuthentication)
			{
				return new FbaWebSession(baseUri, credentials);
			}
			return new AuthenticateWebSession(baseUri, credentials);
		}

		// Token: 0x02000526 RID: 1318
		internal static class ExchangePaths
		{
			// Token: 0x040021E3 RID: 8675
			public const string LanguageSelection = "/owa/languageselection.aspx";

			// Token: 0x040021E4 RID: 8676
			public const string LanguageSelectionPostUrl = "/owa/lang.owa";
		}
	}
}
