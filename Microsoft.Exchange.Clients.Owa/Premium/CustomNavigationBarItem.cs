using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020003CA RID: 970
	internal class CustomNavigationBarItem : NavigationBarItemBase
	{
		// Token: 0x06002411 RID: 9233 RVA: 0x000D0198 File Offset: 0x000CE398
		public CustomNavigationBarItem(UserContext userContext, string text, string targetUrl, string largeIcon, string smallIcon) : base(userContext, text, null)
		{
			this.largeIcon = largeIcon;
			this.smallIcon = smallIcon;
			this.targetUrl = targetUrl;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000D01BC File Offset: 0x000CE3BC
		protected override void RenderImageTag(TextWriter writer, bool useSmallIcons, bool isWunderBar)
		{
			writer.Write("<img class=\"");
			writer.Write(isWunderBar ? (useSmallIcons ? "nbMnuImgWS" : "nbMnuImgWB") : "nbMnuImgN");
			writer.Write("\" src=\"");
			writer.Write(useSmallIcons ? this.smallIcon : this.largeIcon);
			writer.Write("\">");
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000D0220 File Offset: 0x000CE420
		protected override void RenderOnClickHandler(TextWriter writer, NavigationModule currentModule)
		{
			Utilities.RenderScriptHandler(writer, "onclick", "sfWinOpn(\"" + this.targetUrl + "\");", false);
		}

		// Token: 0x04001903 RID: 6403
		private readonly string largeIcon;

		// Token: 0x04001904 RID: 6404
		private readonly string smallIcon;

		// Token: 0x04001905 RID: 6405
		private readonly string targetUrl;
	}
}
