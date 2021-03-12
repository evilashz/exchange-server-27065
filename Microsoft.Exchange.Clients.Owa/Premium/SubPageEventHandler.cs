using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200047C RID: 1148
	public class SubPageEventHandler : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x06002C10 RID: 11280 RVA: 0x000F5D9C File Offset: 0x000F3F9C
		protected override void OnPreRender(EventArgs e)
		{
			if (!this.hasError)
			{
				base.Response.AppendHeader("X-OWA-EventResult", "0");
			}
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000F5DBC File Offset: 0x000F3FBC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "subPage", true);
			if (string.IsNullOrEmpty(queryStringParameter))
			{
				throw new OwaInvalidRequestException("subPagePath parameter cannot be null or empty");
			}
			UserControl userControl = (UserControl)this.Page.LoadControl(queryStringParameter);
			if (userControl is OwaSubPage)
			{
				this.subPage = (OwaSubPage)userControl;
				this.subPage.IsInOEHResponse = true;
				this.subPagePlaceHolder.Controls.Add(userControl);
				return;
			}
			throw new OwaInvalidRequestException("The user control is not of OwaSubPage type");
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000F5E43 File Offset: 0x000F4043
		protected void RenderSubPageScripts()
		{
			this.subPage.RenderExternalScriptFiles();
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000F5E50 File Offset: 0x000F4050
		protected void RenderTitle()
		{
			base.SanitizingResponse.Write(this.subPage.Title);
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000F5E68 File Offset: 0x000F4068
		protected override void OnError(EventArgs e)
		{
			this.hasError = true;
			Exception lastError = base.Server.GetLastError();
			base.Server.ClearError();
			Utilities.HandleException(base.OwaContext, lastError, false);
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000F5EA0 File Offset: 0x000F40A0
		protected void RenderPageType()
		{
			base.SanitizingResponse.Write("_PageType=\"");
			base.SanitizingResponse.Write(this.subPage.PageType);
			base.SanitizingResponse.Write("\"");
		}

		// Token: 0x04001D0D RID: 7437
		protected PlaceHolder subPagePlaceHolder;

		// Token: 0x04001D0E RID: 7438
		private OwaSubPage subPage;

		// Token: 0x04001D0F RID: 7439
		private bool hasError;
	}
}
