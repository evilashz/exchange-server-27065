using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003B8 RID: 952
	public sealed class MessageViewArrangeByMenu : ContextMenu
	{
		// Token: 0x060023B2 RID: 9138 RVA: 0x000CD5F8 File Offset: 0x000CB7F8
		internal MessageViewArrangeByMenu(Folder folder, UserContext userContext, ColumnId sortedColumn) : base("divAbm", userContext, true)
		{
			this.folder = folder;
			this.sortedColumn = sortedColumn;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000CD618 File Offset: 0x000CB818
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			DefaultFolderType defaultFolderType = Utilities.GetDefaultFolderType(this.folder);
			if (defaultFolderType == DefaultFolderType.SentItems || defaultFolderType == DefaultFolderType.Outbox)
			{
				this.RenderMenuItemWithImageAndText(output, -1795472081, null, ColumnId.SentTime);
			}
			else
			{
				this.RenderMenuItemWithImageAndText(output, -1795472081, null, ColumnId.DeliveryTime);
			}
			this.RenderMenuItemWithImageAndText(output, 1309845117, null, ColumnId.From);
			this.RenderMenuItemWithImageAndText(output, 262509582, null, ColumnId.To);
			this.RenderMenuItemWithImageAndText(output, 1128018090, null, ColumnId.Size);
			this.RenderMenuItemWithImageAndText(output, 2014646167, "divSbj", ColumnId.Subject);
			this.RenderMenuItemWithImageAndText(output, 785343019, null, ColumnId.MailIcon);
			this.RenderMenuItemWithImageAndText(output, 1072079569, null, ColumnId.HasAttachment);
			this.RenderMenuItemWithImageAndText(output, 1569168155, null, ColumnId.Importance);
			this.RenderMenuItemWithImageAndText(output, -568934371, null, ColumnId.ConversationFlagStatus);
			this.RenderMenuItemWithImageAndText(output, 1587370059, null, ColumnId.FlagDueDate);
			this.RenderMenuItemWithImageAndText(output, 1580556595, null, ColumnId.FlagStartDate);
			if (ConversationUtilities.ShouldAllowConversationView(this.userContext, this.folder))
			{
				ContextMenu.RenderMenuDivider(output, null);
				string attributes = ConversationUtilities.IsConversationSortColumn(this.sortedColumn) ? "isCnv=1" : "isCnv=0";
				this.RenderMenuItemWithImageAndText(output, 207081000, ThemeFileId.CheckUnchecked, "gbyCnv", attributes);
			}
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000CD748 File Offset: 0x000CB948
		private void RenderMenuItemWithImageAndText(TextWriter output, Strings.IDs displayString, string idString, ColumnId columnId)
		{
			base.RenderMenuItem(output, LocalizedStrings.GetNonEncoded(displayString), ThemeFileId.Clear, string.IsNullOrEmpty(idString) ? null : idString, null, false, this.FetchAdditionalAttributesForColumn(columnId), null, null, null, null, false);
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000CD780 File Offset: 0x000CB980
		private void RenderMenuItemWithImageAndText(TextWriter output, Strings.IDs displayString, ThemeFileId imageField, string idString, string attributes)
		{
			string nonEncoded = LocalizedStrings.GetNonEncoded(displayString);
			string id;
			if (!string.IsNullOrEmpty(idString))
			{
				id = idString;
			}
			else
			{
				string id2 = base.Id;
				int num = displayString;
				id = id2 + num.ToString(CultureInfo.InvariantCulture);
			}
			int num2 = displayString;
			base.RenderMenuItem(output, nonEncoded, imageField, id, num2.ToString(CultureInfo.InvariantCulture), false, attributes, null, null, null, null, false);
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000CD7D8 File Offset: 0x000CB9D8
		private string FetchAdditionalAttributesForColumn(ColumnId columnId)
		{
			Column column = ListViewColumns.GetColumn(columnId);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" _cid=");
			stringBuilder.Append((int)columnId);
			stringBuilder.Append(" _so=");
			stringBuilder.Append(((int)column.DefaultSortOrder).ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(" _lnk=1");
			stringBuilder.Append(" _tD=");
			stringBuilder.Append(column.IsTypeDownCapable ? "1" : "0");
			return stringBuilder.ToString();
		}

		// Token: 0x040018D1 RID: 6353
		private const string ArrangeByMenuId = "divAbm";

		// Token: 0x040018D2 RID: 6354
		private Folder folder;

		// Token: 0x040018D3 RID: 6355
		private ColumnId sortedColumn;
	}
}
