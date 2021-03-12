using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000340 RID: 832
	internal class ConversationPartsList2 : ItemList2
	{
		// Token: 0x06001F86 RID: 8070 RVA: 0x000B56C4 File Offset: 0x000B38C4
		public ConversationPartsList2(SortOrder sortOrder, UserContext userContext, SearchScope folderScope) : base(ConversationPartsList2.conversationPartsViewDescriptor, ColumnId.DeliveryTime, sortOrder, userContext, folderScope)
		{
			base.AddProperty(ItemSchema.Id);
			base.AddProperty(MessageItemSchema.IsRead);
			base.AddProperty(StoreObjectSchema.ParentItemId);
			base.AddProperty(StoreObjectSchema.EntryId);
			base.AddProperty(StoreObjectSchema.ChangeKey);
			base.AddProperty(ItemSchema.ConversationId);
			base.AddProperty(ItemSchema.EdgePcl);
			base.AddProperty(ItemSchema.LinkEnabled);
			base.AddProperty(ItemSchema.IsResponseRequested);
			base.AddProperty(MessageItemSchema.ReceivedRepresentingEmailAddress);
			base.AddProperty(CalendarItemBaseSchema.OrganizerEmailAddress);
			base.AddProperty(StoreObjectSchema.ItemClass);
			base.AddProperty(ItemSchema.IconIndex);
			base.AddProperty(MessageItemSchema.MessageInConflict);
			base.AddProperty(StoreObjectSchema.IsRestricted);
			base.AddProperty(MessageItemSchema.DRMRights);
			base.AddProperty(MessageItemSchema.RequireProtectedPlayOnPhone);
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x000B579B File Offset: 0x000B399B
		public override ViewDescriptor ViewDescriptor
		{
			get
			{
				return ConversationPartsList2.conversationPartsViewDescriptor;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x000B57A2 File Offset: 0x000B39A2
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x000B57AF File Offset: 0x000B39AF
		public ConversationPartsDataSource ConversationPartsDataSource
		{
			get
			{
				return (ConversationPartsDataSource)this.DataSource;
			}
			set
			{
				this.DataSource = value;
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000B57B8 File Offset: 0x000B39B8
		internal void RenderConversationParts(TextWriter writer, OwaStoreObjectId conversationId, Folder contextFolder, Folder dataFolder)
		{
			ExDateTime? lastDeliveryTime = ConversationUtilities.GetLastDeliveryTime(this.ConversationPartsDataSource.Conversation);
			writer.Write("<div id=\"");
			writer.Write("tblExp");
			writer.Write("\" ");
			writer.Write("expId");
			writer.Write("=\"");
			Utilities.HtmlEncode(conversationId.ToString(), writer);
			writer.Write("\" ");
			writer.Write("iGC");
			writer.Write("=");
			writer.Write(ConversationUtilities.GetGlobalCount(this.ConversationPartsDataSource.Conversation));
			writer.Write(">");
			FolderViewStates folderViewStates = base.UserContext.GetFolderViewStates(contextFolder);
			ReadingPanePosition readingPanePosition = folderViewStates.ReadingPanePosition;
			int num;
			if (readingPanePosition == ReadingPanePosition.Right)
			{
				num = folderViewStates.ViewWidth;
			}
			else
			{
				num = 800;
			}
			int num2 = (num - ListViewColumns.GetColumn(ColumnId.DeliveryTime).Width - ListViewColumns.GetColumn(ColumnId.MailIcon).Width - ListViewColumns.GetColumn(ColumnId.From).Width - ListViewColumns.GetColumn(ColumnId.Importance).Width - ListViewColumns.GetColumn(ColumnId.HasAttachment).Width - ListViewColumns.GetColumn(ColumnId.Categories).Width - ListViewColumns.GetColumn(ColumnId.FlagDueDate).Width - 18) / 4;
			num2 = Math.Max(num2, 0);
			List<StoreObjectId> localItemIds = ConversationUtilities.GetLocalItemIds(this.ConversationPartsDataSource.Conversation, dataFolder);
			ConversationUtilities.MarkLocalNodes(this.ConversationPartsDataSource.Conversation, localItemIds);
			while (this.DataSource.MoveNext())
			{
				this.RenderConversationPartRow(writer, contextFolder, dataFolder, lastDeliveryTime, num2);
			}
			writer.Write("</div>");
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000B5938 File Offset: 0x000B3B38
		private void RenderConversationPartRow(TextWriter writer, Folder contextFolder, Folder dataFolder, ExDateTime? lastDeliveryTime, int maximumIndentLevel)
		{
			IConversationTreeNode currentNode = this.ConversationPartsDataSource.GetCurrentNode();
			if (!ConversationUtilities.ShouldRenderItem(currentNode, (MailboxSession)dataFolder.Session))
			{
				return;
			}
			StoreObjectId itemProperty = this.DataSource.GetItemProperty<StoreObjectId>(StoreObjectSchema.ParentItemId);
			bool flag = (bool)this.DataSource.GetItemProperty<object>(MessageItemSchema.IsRead);
			bool flag2 = !ConversationUtilities.IsLocalNode(currentNode);
			bool flag3 = Utilities.IsDefaultFolderId((MailboxSession)dataFolder.Session, itemProperty, DefaultFolderType.DeletedItems);
			writer.Write("<div id=\"");
			writer.Write("ver");
			writer.Write("\" class=\"");
			writer.Write(flag2 ? "df " : string.Empty);
			writer.Write(flag ? string.Empty : "ur");
			writer.Write((this.DataSource.CurrentItem == this.DataSource.EndRange) ? " eclr" : string.Empty);
			writer.Write("\">");
			writer.Write("<div class=cData");
			ItemList2.RenderRowId(writer, this.DataSource.GetItemId());
			this.RenderConversationPartMetaDataExpandos(writer, currentNode, dataFolder, itemProperty, lastDeliveryTime);
			writer.Write(">");
			writer.Write("</div>");
			int num2;
			if (this.ConversationPartsDataSource.SortOrder == ConversationTreeSortOrder.DeepTraversalAscending || this.ConversationPartsDataSource.SortOrder == ConversationTreeSortOrder.DeepTraversalDescending)
			{
				IConversationTreeNode conversationTreeNode = currentNode;
				int num = 0;
				while (conversationTreeNode != null && num < maximumIndentLevel)
				{
					num++;
					conversationTreeNode = conversationTreeNode.ParentNode;
				}
				num2 = num * 4 + 18;
			}
			else
			{
				num2 = 22;
			}
			writer.Write("<div class=expc2 style=\"");
			writer.Write(base.UserContext.IsRtl ? "right:" : "left:");
			writer.Write(num2);
			writer.Write("px;width:");
			writer.Write(20);
			writer.Write("px;\">");
			base.RenderMessageIcon(writer);
			writer.Write("</div>");
			writer.Write("<div class=expc3 style=\"");
			writer.Write(base.UserContext.IsRtl ? "right:" : "left:");
			writer.Write(num2 + 20);
			writer.Write("px;\">");
			if (flag3)
			{
				writer.Write("<strike>");
			}
			base.RenderColumn(writer, ColumnId.From);
			if (flag3)
			{
				writer.Write("</strike>");
			}
			writer.Write("</div>");
			if (flag2)
			{
				base.UserContext.ClearFolderNameCache();
				writer.Write("<div class=\"expc4 df\">");
				base.RenderVLVAnchorOpen(writer);
				writer.Write(Utilities.HtmlEncode(base.UserContext.GetCachedFolderName(itemProperty, (MailboxSession)dataFolder.Session)));
				base.RenderVLVAnchorClose(writer);
				writer.Write("</div>");
			}
			else
			{
				writer.Write("<div class=\"expc4 r\">");
				base.RenderColumn(writer, ColumnId.DeliveryTime);
				writer.Write("</div>");
			}
			FlagStatus itemProperty2 = this.DataSource.GetItemProperty<FlagStatus>(ItemSchema.FlagStatus, FlagStatus.NotFlagged);
			writer.Write("<div class=\"c7");
			if (itemProperty2 != FlagStatus.NotFlagged)
			{
				writer.Write(" stky");
			}
			writer.Write("\" id=");
			writer.Write("divFlg");
			writer.Write(">");
			base.RenderColumn(writer, ColumnId.FlagDueDate);
			writer.Write("</div>");
			string[] itemProperty3 = this.DataSource.GetItemProperty<string[]>(ItemSchema.Categories, null);
			writer.Write("<div class=\"r c6");
			if (itemProperty3 != null && itemProperty3.Length > 0)
			{
				writer.Write(" stky");
			}
			writer.Write("\" id=");
			writer.Write("divCat");
			writer.Write(">");
			base.RenderColumn(writer, ColumnId.Categories);
			writer.Write("</div>");
			writer.Write("<div class=c5 style=\"text-decoration:none;\">");
			bool flag4 = base.RenderColumn(writer, ColumnId.Importance, false);
			base.RenderColumn(writer, ColumnId.HasAttachment, !flag4);
			writer.Write("</div>");
			base.RenderSelectionImage(writer);
			writer.Write("</div>");
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000B5D0B File Offset: 0x000B3F0B
		protected override void ValidatedRender(TextWriter writer, int startRange, int endRange)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x000B5D14 File Offset: 0x000B3F14
		private void RenderConversationPartMetaDataExpandos(TextWriter writer, IConversationTreeNode current, Folder dataFolder, StoreObjectId currentFolderId, ExDateTime? lastDeliveryTime)
		{
			base.RenderMessageViewItemMetaDataExpandos(writer);
			if (lastDeliveryTime != null && this.DataSource.GetItemProperty<ExDateTime>(ItemSchema.ReceivedTime, ExDateTime.MinValue) == lastDeliveryTime)
			{
				writer.Write(" ");
				writer.Write("fLR");
				writer.Write("=");
				writer.Write(1);
			}
			if (current.ParentNode != null && current.ParentNode.ParentNode != null)
			{
				VersionedId versionedId = (VersionedId)current.ParentNode.StorePropertyBags[0].TryGetProperty(ItemSchema.Id);
				if (versionedId != null)
				{
					writer.Write(" ");
					writer.Write("pId");
					writer.Write("=");
					Utilities.HtmlEncode(Utilities.GetItemIdString(versionedId.ObjectId, dataFolder), writer);
				}
			}
			writer.Write(" ");
			writer.Write("fId");
			writer.Write("=");
			Utilities.HtmlEncode(currentFolderId.ToBase64String(), writer);
			StoreObjectId storeObjectId = base.UserContext.GetDeletedItemsFolderId((MailboxSession)dataFolder.Session).StoreObjectId;
			writer.Write(" ");
			writer.Write("fInDelItms");
			writer.Write("=");
			Utilities.HtmlEncode(currentFolderId.Equals(storeObjectId) ? "1" : "0", writer);
			bool flag = Utilities.IsDefaultFolderId(base.UserContext.MailboxSession, currentFolderId, DefaultFolderType.JunkEmail);
			writer.Write(" ");
			writer.Write("fJnk");
			writer.Write("=");
			writer.Write(flag ? "1" : "0");
		}

		// Token: 0x040016E8 RID: 5864
		private const ColumnId SortByColumn = ColumnId.DeliveryTime;

		// Token: 0x040016E9 RID: 5865
		private const int IndentationConstant = 18;

		// Token: 0x040016EA RID: 5866
		private const int IndentationInterval = 4;

		// Token: 0x040016EB RID: 5867
		private const int MaximumViewWidth = 800;

		// Token: 0x040016EC RID: 5868
		private const int ExpandedRowColumn2Width = 20;

		// Token: 0x040016ED RID: 5869
		private const string Depth = "depth";

		// Token: 0x040016EE RID: 5870
		private const string ExpandedConversationTable = "tblExp";

		// Token: 0x040016EF RID: 5871
		private const string ExpandedConversationId = "expId";

		// Token: 0x040016F0 RID: 5872
		private const string ConversationGlobalCount = "iGC";

		// Token: 0x040016F1 RID: 5873
		private const string ExpansionRow = "ver";

		// Token: 0x040016F2 RID: 5874
		private const string IsLastReceivedItem = "fLR";

		// Token: 0x040016F3 RID: 5875
		private const string ParentId = "pId";

		// Token: 0x040016F4 RID: 5876
		private const string FolderId = "fId";

		// Token: 0x040016F5 RID: 5877
		private const string IsInDeletedItems = "fInDelItms";

		// Token: 0x040016F6 RID: 5878
		private const string IsInJunkFolder = "fJnk";

		// Token: 0x040016F7 RID: 5879
		private static ViewDescriptor conversationPartsViewDescriptor = new ViewDescriptor(ColumnId.DeliveryTime, false, new ColumnId[]
		{
			ColumnId.MailIcon,
			ColumnId.From,
			ColumnId.DeliveryTime,
			ColumnId.Importance,
			ColumnId.HasAttachment,
			ColumnId.Categories,
			ColumnId.FlagDueDate
		});
	}
}
