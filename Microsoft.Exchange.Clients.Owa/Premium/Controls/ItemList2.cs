using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000305 RID: 773
	internal abstract class ItemList2 : ListViewContents2
	{
		// Token: 0x06001D52 RID: 7506 RVA: 0x000A915D File Offset: 0x000A735D
		protected ItemList2(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext) : this(viewDescriptor, sortedColumn, sortOrder, userContext, SearchScope.SelectedFolder, false)
		{
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x000A916C File Offset: 0x000A736C
		protected ItemList2(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext, SearchScope folderScope) : this(viewDescriptor, sortedColumn, sortOrder, userContext, folderScope, false)
		{
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x000A917C File Offset: 0x000A737C
		protected ItemList2(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext, SearchScope folderScope, bool renderLastModifiedTime) : base(userContext)
		{
			this.viewDescriptor = viewDescriptor;
			this.sortedColumn = ListViewColumns.GetColumn(sortedColumn);
			this.sortOrder = sortOrder;
			this.folderScope = folderScope;
			this.RenderLastModifiedTime = renderLastModifiedTime;
			if (folderScope != SearchScope.SelectedFolder && !(this is ConversationItemList2) && folderScope != SearchScope.SelectedFolder)
			{
				base.AddProperty(ItemSchema.ParentDisplayName);
			}
			for (int i = 0; i < viewDescriptor.PropertyCount; i++)
			{
				base.AddProperty(viewDescriptor.GetProperty(i));
			}
			if (this.RenderLastModifiedTime)
			{
				base.AddProperty(StoreObjectSchema.LastModifiedTime);
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x000A9207 File Offset: 0x000A7407
		public override ViewDescriptor ViewDescriptor
		{
			get
			{
				return this.viewDescriptor;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x000A920F File Offset: 0x000A740F
		public SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x000A9217 File Offset: 0x000A7417
		public Column SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x000A921F File Offset: 0x000A741F
		protected static void RenderRowId(TextWriter writer, string id)
		{
			writer.Write(" ");
			writer.Write("id");
			writer.Write("=\"");
			writer.Write("b");
			Utilities.HtmlEncode(id, writer);
			writer.Write("\"");
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x000A925F File Offset: 0x000A745F
		protected static void RenderInstanceKey(TextWriter writer, string instanceKey)
		{
			writer.Write(" ");
			writer.Write("ik");
			writer.Write("=\"");
			Utilities.HtmlEncode(instanceKey, writer);
			writer.Write("\"");
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x000A9294 File Offset: 0x000A7494
		protected virtual void RenderItemMetaDataExpandos(TextWriter writer)
		{
			this.InternalRenderItemMetaDataExpandos(writer);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x000A92A0 File Offset: 0x000A74A0
		protected void InternalRenderItemMetaDataExpandos(TextWriter writer)
		{
			writer.Write(" ");
			writer.Write("t");
			writer.Write("=\"");
			Utilities.HtmlEncode(Utilities.UrlEncode(this.DataSource.GetItemClass()), writer);
			writer.Write("\"");
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x000A92F0 File Offset: 0x000A74F0
		protected void RenderLastModifiedTimeExpando(TextWriter writer)
		{
			ExDateTime itemProperty = this.DataSource.GetItemProperty<ExDateTime>(StoreObjectSchema.LastModifiedTime, ExDateTime.MinValue);
			if (itemProperty != ExDateTime.MinValue)
			{
				writer.Write(" sLstMdfyTm=\"");
				writer.Write(itemProperty.UtcTicks);
				writer.Write("\"");
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x000A9344 File Offset: 0x000A7544
		protected void RenderItemTooltip(TextWriter writer)
		{
			if (this.folderScope == SearchScope.SelectedFolder)
			{
				return;
			}
			if (!(this.DataSource is FolderListViewDataSource) && !(this.DataSource is ListViewNotificationDataSource))
			{
				throw new InvalidOperationException(string.Format("DataSource must be FolderListViewDataSource to show item tooltip. Actual Type {0}", this.DataSource.GetType()));
			}
			writer.Write(" title=\"");
			string itemProperty = this.DataSource.GetItemProperty<string>(ItemSchema.ParentDisplayName, string.Empty);
			string htmlEncodedLocalizeString = this.GetHtmlEncodedLocalizeString(699235260);
			writer.Write(string.Format(htmlEncodedLocalizeString, Utilities.HtmlEncode(itemProperty)));
			writer.Write("\"");
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x000A93DC File Offset: 0x000A75DC
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
			if (itemProperty)
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
			int itemProperty2 = this.DataSource.GetItemProperty<int>(ItemSchema.EdgePcl, 1);
			bool itemProperty3 = this.DataSource.GetItemProperty<bool>(ItemSchema.LinkEnabled, false);
			if (JunkEmailUtilities.IsSuspectedPhishingItem(itemProperty2) && !itemProperty3)
			{
				writer.Write("=1");
			}
			else
			{
				writer.Write("=0");
			}
			bool itemProperty4 = this.DataSource.GetItemProperty<bool>(StoreObjectSchema.IsRestricted, false);
			if (itemProperty4 && base.UserContext.IsIrmEnabled)
			{
				ContentRight itemProperty5 = (ContentRight)this.DataSource.GetItemProperty<int>(MessageItemSchema.DRMRights, 0);
				RenderingUtilities.RenderExpando(writer, "fRplR", itemProperty5.IsUsageRightGranted(ContentRight.Reply) ? 0 : 1);
				RenderingUtilities.RenderExpando(writer, "fRAR", itemProperty5.IsUsageRightGranted(ContentRight.ReplyAll) ? 0 : 1);
				RenderingUtilities.RenderExpando(writer, "fFR", itemProperty5.IsUsageRightGranted(ContentRight.Forward) ? 0 : 1);
			}
			if (ConversationUtilities.IsConversationExcludedType(this.DataSource.GetItemClass()))
			{
				RenderingUtilities.RenderExpando(writer, "fExclCnv", 1);
			}
			this.RenderMeetingRequestExpandos(writer);
			if (this.RenderLastModifiedTime)
			{
				this.RenderLastModifiedTimeExpando(writer);
			}
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x000A95A0 File Offset: 0x000A77A0
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
				bool flag = MeetingUtilities.CheckOrganizer(itemProperty2, itemProperty3, base.UserContext.MailboxOwnerLegacyDN);
				if (flag)
				{
					writer.Write(" ");
					writer.Write("fDoR");
					writer.Write("=");
					writer.Write(flag ? "1" : "0");
				}
			}
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x000A96A0 File Offset: 0x000A78A0
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

		// Token: 0x04001577 RID: 5495
		internal const string IsExcludedFromConversations = "fExclCnv";

		// Token: 0x04001578 RID: 5496
		protected const string SecondaryRow = "sr";

		// Token: 0x04001579 RID: 5497
		protected const string ItemState = "s";

		// Token: 0x0400157A RID: 5498
		protected const string ItemRead = "read";

		// Token: 0x0400157B RID: 5499
		private const string ResponseRequested = "fRR";

		// Token: 0x0400157C RID: 5500
		private const string IsOrganizer = "fDoR";

		// Token: 0x0400157D RID: 5501
		protected const string FlagId = "divFlg";

		// Token: 0x0400157E RID: 5502
		protected const string CategoryId = "divCat";

		// Token: 0x0400157F RID: 5503
		private const string StoreObjectType = "t";

		// Token: 0x04001580 RID: 5504
		private const string ItemId = "id";

		// Token: 0x04001581 RID: 5505
		private const string InstanceKey = "ik";

		// Token: 0x04001582 RID: 5506
		private const string ItemIdPrefix = "b";

		// Token: 0x04001583 RID: 5507
		private const string IsSuspectedPhishingItemWithoutLinkEnabled = "fPhsh";

		// Token: 0x04001584 RID: 5508
		private const string IsReplyRestricted = "fRplR";

		// Token: 0x04001585 RID: 5509
		private const string IsReplyAllRestricted = "fRAR";

		// Token: 0x04001586 RID: 5510
		private const string IsForwardRestricted = "fFR";

		// Token: 0x04001587 RID: 5511
		protected readonly bool RenderLastModifiedTime;

		// Token: 0x04001588 RID: 5512
		private ViewDescriptor viewDescriptor;

		// Token: 0x04001589 RID: 5513
		private Column sortedColumn;

		// Token: 0x0400158A RID: 5514
		private SortOrder sortOrder;

		// Token: 0x0400158B RID: 5515
		private SearchScope folderScope;
	}
}
