using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000350 RID: 848
	internal abstract class LegacyItemList : LegacyListViewContents
	{
		// Token: 0x06002003 RID: 8195 RVA: 0x000B993A File Offset: 0x000B7B3A
		protected LegacyItemList(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext) : this(viewDescriptor, sortedColumn, sortOrder, userContext, SearchScope.SelectedFolder)
		{
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x000B9948 File Offset: 0x000B7B48
		protected LegacyItemList(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext, SearchScope folderScope) : base(userContext)
		{
			this.viewDescriptor = viewDescriptor;
			this.sortedColumn = ListViewColumns.GetColumn(sortedColumn);
			this.sortOrder = sortOrder;
			this.folderScope = folderScope;
			for (int i = 0; i < viewDescriptor.PropertyCount; i++)
			{
				base.AddProperty(viewDescriptor.GetProperty(i));
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x000B999D File Offset: 0x000B7B9D
		public override ViewDescriptor ViewDescriptor
		{
			get
			{
				return this.viewDescriptor;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002006 RID: 8198 RVA: 0x000B99A5 File Offset: 0x000B7BA5
		protected SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x000B99AD File Offset: 0x000B7BAD
		protected Column SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000B99B5 File Offset: 0x000B7BB5
		protected static void RenderRowId(TextWriter writer, string id)
		{
			writer.Write(" ");
			writer.Write("id");
			writer.Write("=\"");
			writer.Write("b");
			Utilities.HtmlEncode(id, writer);
			writer.Write("\"");
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x000B99F5 File Offset: 0x000B7BF5
		protected virtual void RenderItemMetaDataExpandos(TextWriter writer)
		{
			this.InternalRenderItemMetaDataExpandos(writer);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000B9A00 File Offset: 0x000B7C00
		protected void InternalRenderItemMetaDataExpandos(TextWriter writer)
		{
			LegacyItemList.RenderRowId(writer, this.DataSource.GetItemId());
			writer.Write(" ");
			writer.Write("t");
			writer.Write("=\"");
			Utilities.HtmlEncode(Utilities.UrlEncode(this.DataSource.GetItemClass()), writer);
			writer.Write("\"");
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000B9A60 File Offset: 0x000B7C60
		protected void RenderItemTooltip(TextWriter writer)
		{
			if (this.folderScope == SearchScope.SelectedFolder)
			{
				return;
			}
			if (!(this.DataSource is FolderListViewDataSource))
			{
				throw new InvalidOperationException(string.Format("DataSource must be FolderListViewDataSource to show item tooltip. Actual Type {0}", this.DataSource.GetType()));
			}
			writer.Write(" title=\"");
			string itemProperty = this.DataSource.GetItemProperty<string>(ItemSchema.ParentDisplayName, string.Empty);
			string htmlEncoded = LocalizedStrings.GetHtmlEncoded(699235260);
			writer.Write(string.Format(htmlEncoded, Utilities.HtmlEncode(itemProperty)));
			writer.Write("\"");
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000B9AE8 File Offset: 0x000B7CE8
		protected void RenderMessageViewItemMetaDataExpandos(TextWriter writer)
		{
			this.InternalRenderItemMetaDataExpandos(writer);
			if (!this.DataSource.GetItemProperty<bool>(MessageItemSchema.IsRead, true))
			{
				writer.Write(" ");
				writer.Write("read");
				writer.Write("=\"0\"");
			}
			bool itemProperty = this.DataSource.GetItemProperty<bool>(MessageItemSchema.IsDraft, false);
			bool itemProperty2 = this.DataSource.GetItemProperty<bool>(MessageItemSchema.HasBeenSubmitted, false);
			if (itemProperty && !itemProperty2)
			{
				writer.Write(" ");
				writer.Write("s");
				writer.Write("=\"Draft\"");
			}
			if (this.viewDescriptor.ContainsColumn(ColumnId.FlagDueDate) || this.viewDescriptor.ContainsColumn(ColumnId.ContactFlagDueDate))
			{
				this.RenderFlagState(writer);
			}
			writer.Write(" ");
			writer.Write("fPhsh");
			int itemProperty3 = this.DataSource.GetItemProperty<int>(ItemSchema.EdgePcl, 1);
			bool itemProperty4 = this.DataSource.GetItemProperty<bool>(ItemSchema.LinkEnabled, false);
			if (JunkEmailUtilities.IsSuspectedPhishingItem(itemProperty3) && !itemProperty4)
			{
				writer.Write("=1");
			}
			else
			{
				writer.Write("=0");
			}
			this.RenderMeetingRequestExpandos(writer);
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000B9C08 File Offset: 0x000B7E08
		protected void RenderMeetingRequestExpandos(TextWriter writer)
		{
			if (base.UserContext.IsFeatureEnabled(Feature.Calendar) && ObjectClass.IsMeetingRequest(this.DataSource.GetItemClass()))
			{
				writer.Write(" fMR=1");
				bool itemProperty = this.DataSource.GetItemProperty<bool>(ItemSchema.IsResponseRequested, false);
				writer.Write(" ");
				writer.Write("fRR");
				writer.Write("=");
				writer.Write(itemProperty ? "1" : "0");
				string itemProperty2 = this.DataSource.GetItemProperty<string>(MessageItemSchema.ReceivedRepresentingEmailAddress, string.Empty);
				string itemProperty3 = this.DataSource.GetItemProperty<string>(CalendarItemBaseSchema.OrganizerEmailAddress, string.Empty);
				string mailboxOwnerLegacyDN = base.UserContext.MailboxSession.MailboxOwnerLegacyDN;
				bool flag = MeetingUtilities.CheckOrganizer(itemProperty2, itemProperty3, mailboxOwnerLegacyDN);
				if (flag)
				{
					writer.Write(" ");
					writer.Write("fDoR");
					writer.Write("=");
					writer.Write(flag ? "1" : "0");
				}
			}
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000B9D14 File Offset: 0x000B7F14
		protected void RenderFlagState(TextWriter writer)
		{
			FlagStatus flagStatus = this.DataSource.GetItemProperty<FlagStatus>(ItemSchema.FlagStatus, FlagStatus.NotFlagged);
			int itemProperty = this.DataSource.GetItemProperty<int>(ItemSchema.ItemColor, int.MinValue);
			bool flag = ObjectClass.IsTask(this.DataSource.GetItemClass());
			if (flag)
			{
				if (this.DataSource.GetItemProperty<bool>(ItemSchema.IsComplete, false))
				{
					flagStatus = FlagStatus.Complete;
				}
				else
				{
					flagStatus = FlagStatus.Flagged;
				}
			}
			if (flagStatus != FlagStatus.NotFlagged)
			{
				ExDateTime itemProperty2 = this.DataSource.GetItemProperty<ExDateTime>(ItemSchema.UtcDueDate, ExDateTime.MinValue);
				if (itemProperty2 != ExDateTime.MinValue)
				{
					writer.Write(" sFlgDt=\"");
					writer.Write(DateTimeUtilities.GetJavascriptDate(itemProperty2));
					writer.Write("\"");
				}
				FlagAction flagActionForItem = FlagContextMenu.GetFlagActionForItem(base.UserContext, itemProperty2, flagStatus);
				writer.Write(" sFA=");
				writer.Write((int)flagActionForItem);
				if (itemProperty == -2147483648 && flagStatus == FlagStatus.Flagged && !flag)
				{
					writer.Write(" sFS=1");
				}
			}
		}

		// Token: 0x04001738 RID: 5944
		protected const string SecondaryRow = "sr";

		// Token: 0x04001739 RID: 5945
		protected const string ItemState = "s";

		// Token: 0x0400173A RID: 5946
		protected const string ItemRead = "read";

		// Token: 0x0400173B RID: 5947
		private const string ResponseRequested = "fRR";

		// Token: 0x0400173C RID: 5948
		private const string IsOrganizer = "fDoR";

		// Token: 0x0400173D RID: 5949
		protected const string FlagId = "tdFlg";

		// Token: 0x0400173E RID: 5950
		protected const string CategoryId = "tdCat";

		// Token: 0x0400173F RID: 5951
		private const string StoreObjectType = "t";

		// Token: 0x04001740 RID: 5952
		private const string ItemId = "id";

		// Token: 0x04001741 RID: 5953
		private const string ItemIdPrefix = "b";

		// Token: 0x04001742 RID: 5954
		private const string IsSuspectedPhishingItemWithoutLinkEnabled = "fPhsh";

		// Token: 0x04001743 RID: 5955
		private ViewDescriptor viewDescriptor;

		// Token: 0x04001744 RID: 5956
		private Column sortedColumn;

		// Token: 0x04001745 RID: 5957
		private SortOrder sortOrder;

		// Token: 0x04001746 RID: 5958
		private SearchScope folderScope;
	}
}
