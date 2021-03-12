using System;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x02000091 RID: 145
	public class AutoSaveInfo : OwaPage
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0002428C File Offset: 0x0002248C
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0002428F File Offset: 0x0002248F
		protected string MessageIdString
		{
			get
			{
				return this.messageIdString;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00024297 File Offset: 0x00022497
		protected override void OnLoad(EventArgs e)
		{
			this.messageIdString = Utilities.GetQueryStringParameter(base.Request, "id", true);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000242B0 File Offset: 0x000224B0
		protected void RenderNavigation(NavigationModule navigationModule)
		{
			Navigation navigation = new Navigation(navigationModule, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000242DC File Offset: 0x000224DC
		protected void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.None, OptionsBar.RenderingFlags.None, null);
			optionsBar.Render(helpFile);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0002430C File Offset: 0x0002250C
		protected void RenderMailSecondaryNavigation()
		{
			MailSecondaryNavigation mailSecondaryNavigation = new MailSecondaryNavigation(base.OwaContext, base.UserContext.InboxFolderId, null, null, null);
			mailSecondaryNavigation.RenderWithoutMruAndAllFolder(base.Response.Output);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0002434C File Offset: 0x0002254C
		protected void RenderInfoBarIcon()
		{
			base.UserContext.RenderThemeFileUrl(base.Response.Output, ThemeFileId.Informational);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00024369 File Offset: 0x00022569
		protected void RenderInfoBarMessage()
		{
			base.Response.Output.Write(LocalizedStrings.GetHtmlEncoded(1537571484));
		}

		// Token: 0x04000388 RID: 904
		private string messageIdString = string.Empty;
	}
}
