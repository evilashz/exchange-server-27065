using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000022 RID: 34
	internal class SecondaryNavigation
	{
		// Token: 0x060000FD RID: 253 RVA: 0x000089A8 File Offset: 0x00006BA8
		public SecondaryNavigation(OwaContext owaContext, StoreObjectId folderId)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			this.owaContext = owaContext;
			this.selectedFolderId = folderId;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000089DC File Offset: 0x00006BDC
		public void RenderManageFolderButton(Strings.IDs buttonLabel, TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table cellspacing=0 cellpadding=0 class=\"snfmb\"><tr><td nowrap>");
			writer.Write("<a href=\"#\" id=\"lnkMngFldr\" onClick=\"return onClkFM()\"><img src=\"");
			this.owaContext.UserContext.RenderThemeFileUrl(writer, ThemeFileId.Root);
			writer.Write("\" alt=\"\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(buttonLabel));
			writer.Write("</a></td></tr></table>");
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00008A45 File Offset: 0x00006C45
		public void RenderHorizontalDivider(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table class=\"sntls\"><tr><td><img src=\"");
			this.owaContext.UserContext.RenderThemeFileUrl(writer, ThemeFileId.Clear1x1);
			writer.Write("\" alt=\"\"></td></tr></table>");
		}

		// Token: 0x040000AA RID: 170
		protected OwaContext owaContext;

		// Token: 0x040000AB RID: 171
		protected StoreObjectId selectedFolderId;
	}
}
