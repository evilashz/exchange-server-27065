using System;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200052B RID: 1323
	internal class OutlookWebAccessApplication : ExchangeWebApplication
	{
		// Token: 0x06002F7D RID: 12157 RVA: 0x000BF2EE File Offset: 0x000BD4EE
		public OutlookWebAccessApplication(string virtualDirectory, WebSession webSession) : base(virtualDirectory, webSession)
		{
			webSession.SendingRequest += delegate(object sender, HttpWebRequestEventArgs e)
			{
				e.Request.Expect = null;
			};
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000BF31B File Offset: 0x000BD51B
		protected override bool IsLanguageSelectionResponse(RedirectResponse response)
		{
			return response.Text.IndexOf("DA9221EC-C996-4b5a-B238-1B7E5E590944", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000BF334 File Offset: 0x000BD534
		public override bool ValidateLogin()
		{
			return base.ValidateLogin() && null != base.GetCookie("UserContext");
		}

		// Token: 0x0200052C RID: 1324
		private static class PageSignature
		{
			// Token: 0x040021EF RID: 8687
			public const string LanguageSelection = "DA9221EC-C996-4b5a-B238-1B7E5E590944";
		}
	}
}
