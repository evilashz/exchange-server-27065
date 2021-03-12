using System;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000023 RID: 35
	internal class CalendarSecondaryNavigation : SecondaryNavigation
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00008A80 File Offset: 0x00006C80
		public CalendarSecondaryNavigation(OwaContext owaContext, StoreObjectId selectedFolderId, ExDateTime? day, CalendarFolderList calendarFolderList) : base(owaContext, selectedFolderId)
		{
			HttpRequest request = owaContext.HttpContext.Request;
			UserContext userContext = owaContext.UserContext;
			if (selectedFolderId == null)
			{
				this.selectedFolderId = (RequestParser.GetFolderIdFromQueryString(request, false) ?? userContext.CalendarFolderId);
			}
			if (day != null)
			{
				this.day = day.Value;
			}
			else
			{
				CalendarModuleViewState calendarModuleViewState = userContext.LastClientViewState as CalendarModuleViewState;
				if (calendarModuleViewState != null && selectedFolderId.Equals(calendarModuleViewState.FolderId))
				{
					this.day = calendarModuleViewState.DateTime;
				}
				else
				{
					this.day = DateTimeUtilities.GetLocalTime().Date;
				}
			}
			this.calendarFolderList = calendarFolderList;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008B20 File Offset: 0x00006D20
		public string Render(TextWriter writer)
		{
			writer.Write("<table cellpadding=0 cellspacing=0 class=\"wh100\"><caption>");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-1286941817));
			writer.Write("</caption><tr><td>");
			DatePicker datePicker = new DatePicker(this.owaContext, this.selectedFolderId, this.day);
			string result = datePicker.Render(writer);
			if (this.calendarFolderList == null)
			{
				this.calendarFolderList = new CalendarFolderList(this.owaContext.UserContext, this.selectedFolderId);
			}
			this.calendarFolderList.Render(writer);
			base.RenderHorizontalDivider(writer);
			base.RenderManageFolderButton(2083254177, writer);
			writer.Write("</td></tr></table>");
			return result;
		}

		// Token: 0x040000AC RID: 172
		private ExDateTime day;

		// Token: 0x040000AD RID: 173
		private CalendarFolderList calendarFolderList;
	}
}
