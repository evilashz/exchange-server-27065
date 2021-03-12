using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000E7 RID: 231
	public class CasRedirect : OwaPage
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x0003ABD2 File Offset: 0x00038DD2
		protected override void OnLoad(EventArgs e)
		{
			this.isIE = (BrowserType.IE == Utilities.GetBrowserType(base.OwaContext.HttpContext.Request.UserAgent));
			Utilities.DeleteFBASessionCookies(base.Response);
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0003AC02 File Offset: 0x00038E02
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0003AC05 File Offset: 0x00038E05
		protected string RedirectionUrl
		{
			get
			{
				return Utilities.RedirectionUrl(base.OwaContext);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0003AC12 File Offset: 0x00038E12
		protected bool IsTemporaryRedirection
		{
			get
			{
				return base.OwaContext.IsTemporaryRedirection;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0003AC1F File Offset: 0x00038E1F
		protected bool CanAccessUsualAddressInAnHour
		{
			get
			{
				return base.OwaContext.CanAccessUsualAddressInAnHour;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0003AC2C File Offset: 0x00038E2C
		protected bool RenderAddToFavoritesButton
		{
			get
			{
				return this.isIE;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0003AC34 File Offset: 0x00038E34
		protected string UrlTitle
		{
			get
			{
				string arg = Utilities.HtmlEncode(base.OwaContext.MailboxIdentity.GetOWAMiniRecipient().DisplayName);
				return string.Format(LocalizedStrings.GetHtmlEncoded(-456269480), arg);
			}
		}

		// Token: 0x04000581 RID: 1409
		private bool isIE = true;
	}
}
