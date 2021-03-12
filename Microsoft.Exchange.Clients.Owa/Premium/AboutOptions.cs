using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000435 RID: 1077
	public class AboutOptions : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x000DE566 File Offset: 0x000DC766
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000DE56C File Offset: 0x000DC76C
		protected void RenderAboutDetails()
		{
			string s = null;
			string text = null;
			bool flag = false;
			AboutDetails aboutDetails = new AboutDetails(base.OwaContext);
			for (int i = 0; i < aboutDetails.Count; i++)
			{
				Strings.IDs localizedId;
				aboutDetails.GetDetails(i, out localizedId, out s, out text, out flag);
				base.SanitizingResponse.Write("<div id=\"optAb\"><span class=\"lbl\">");
				if (flag)
				{
					base.SanitizingResponse.Write("&nbsp; &nbsp; &nbsp;");
				}
				base.SanitizingResponse.Write(LocalizedStrings.GetNonEncoded(localizedId));
				if (base.UserContext.IsRtl)
				{
					base.SanitizingResponse.Write("&#x200F;");
				}
				base.SanitizingResponse.Write(":</span> <span id=\"id\">");
				if (text != null)
				{
					base.SanitizingResponse.Write("<span id=\"");
					base.SanitizingResponse.Write(text);
					base.SanitizingResponse.Write("\"></span>");
				}
				else
				{
					Utilities.RenderDirectionEnhancedValue(base.SanitizingResponse, Utilities.SanitizeHtmlEncode(s), base.UserContext.IsRtl);
				}
				base.SanitizingResponse.Write("</span></div>");
			}
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000DE678 File Offset: 0x000DC878
		protected void RenderBreadcrumbs()
		{
			if (!Globals.RenderBreadcrumbsInAboutPage)
			{
				return;
			}
			base.SanitizingResponse.Write("<div id=\"optAb\"><span class=\"lbl\">Breadcrumbs:</span></div>");
			BreadcrumbBuffer breadcrumbs = base.OwaContext.UserContext.Breadcrumbs;
			for (int i = 0; i < breadcrumbs.Count; i++)
			{
				base.SanitizingResponse.Write("<div id=\"optAb\"><span class=\"id\">");
				base.SanitizingResponse.Write(breadcrumbs[i].ToString());
				base.SanitizingResponse.Write("</span></div>");
			}
		}
	}
}
