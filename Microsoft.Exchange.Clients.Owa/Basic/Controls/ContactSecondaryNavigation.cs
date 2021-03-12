using System;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200002A RID: 42
	internal sealed class ContactSecondaryNavigation : SecondaryNavigation
	{
		// Token: 0x06000119 RID: 281 RVA: 0x0000955C File Offset: 0x0000775C
		public ContactSecondaryNavigation(OwaContext owaContext, StoreObjectId folderId, ContactFolderList contactFolderList) : base(owaContext, folderId)
		{
			HttpRequest request = owaContext.HttpContext.Request;
			UserContext userContext = owaContext.UserContext;
			this.contactFolderList = contactFolderList;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00009580 File Offset: 0x00007780
		public void RenderContacts(TextWriter writer)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ContactsSecondaryNavigation.Render()");
			writer.Write("<table cellpadding=0 cellspacing=0 class=\"wh100\"><caption>");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-1286941817));
			writer.Write("</caption><tr><td>");
			UserContext userContext = this.owaContext.UserContext;
			if (this.contactFolderList == null)
			{
				this.contactFolderList = new ContactFolderList(userContext, this.selectedFolderId);
			}
			this.contactFolderList.Render(writer);
			base.RenderHorizontalDivider(writer);
			base.RenderManageFolderButton(1219371978, writer);
			writer.Write("</td></tr></table>");
		}

		// Token: 0x040000B8 RID: 184
		private ContactFolderList contactFolderList;
	}
}
