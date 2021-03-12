using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200047B RID: 1147
	public class SubPageContainer : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x06002C08 RID: 11272 RVA: 0x000F5C18 File Offset: 0x000F3E18
		protected override void OnLoad(EventArgs e)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "subPage", true);
			UserControl userControl = (UserControl)this.Page.LoadControl(queryStringParameter);
			this.subPage = (OwaSubPage)userControl;
			this.subPage.IsStandalonePage = true;
			this.subPagePlaceHolder.Controls.Add(userControl);
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x000F5C72 File Offset: 0x000F3E72
		protected void RenderSubPageScripts()
		{
			base.RenderExternalScripts(ScriptFlags.IncludeUglobal, this.subPage.ExternalScriptFilesIncludeChildSubPages);
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x000F5C86 File Offset: 0x000F3E86
		protected void RenderTitle()
		{
			base.SanitizingResponse.Write(this.subPage.Title);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000F5C9E File Offset: 0x000F3E9E
		protected void RenderPageType()
		{
			base.SanitizingResponse.Write("_PageType=\"");
			base.SanitizingResponse.Write(this.subPage.PageType);
			base.SanitizingResponse.Write("\"");
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000F5CD8 File Offset: 0x000F3ED8
		protected void RenderHtmlAdditionalAttributes()
		{
			string htmlAdditionalAttributes = this.subPage.HtmlAdditionalAttributes;
			if (!string.IsNullOrEmpty(htmlAdditionalAttributes))
			{
				base.SanitizingResponse.Write(" ");
				base.SanitizingResponse.Write(htmlAdditionalAttributes);
			}
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000F5D15 File Offset: 0x000F3F15
		protected void RenderIfSupportIM()
		{
			if (this.subPage.SupportIM)
			{
				base.SanitizingResponse.Write("_fIM=\"1\"");
			}
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000F5D34 File Offset: 0x000F3F34
		protected void RenderBodyCssClass()
		{
			base.SanitizingResponse.Write("class=\"");
			base.SanitizingResponse.Write(this.subPage.BodyCssClass);
			if (base.SessionContext.IsRtl)
			{
				base.SanitizingResponse.Write(" rtl");
			}
			base.SanitizingResponse.Write("\"");
		}

		// Token: 0x04001D0B RID: 7435
		protected PlaceHolder subPagePlaceHolder;

		// Token: 0x04001D0C RID: 7436
		private OwaSubPage subPage;
	}
}
