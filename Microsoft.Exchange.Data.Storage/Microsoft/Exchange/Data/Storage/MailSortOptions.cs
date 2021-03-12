using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007AE RID: 1966
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailSortOptions
	{
		// Token: 0x060049FA RID: 18938 RVA: 0x00135550 File Offset: 0x00133750
		public MailSortOptions(FolderViewColumnId columnId, PropertyDefinition itemSortProperty, PropertyDefinition conversationSortProperty)
		{
			this.ColumnId = columnId;
			this.ItemSortProperty = itemSortProperty;
			this.CoversationSortProperty = conversationSortProperty;
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x060049FB RID: 18939 RVA: 0x0013556D File Offset: 0x0013376D
		// (set) Token: 0x060049FC RID: 18940 RVA: 0x00135575 File Offset: 0x00133775
		public FolderViewColumnId ColumnId { get; private set; }

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x060049FD RID: 18941 RVA: 0x0013557E File Offset: 0x0013377E
		// (set) Token: 0x060049FE RID: 18942 RVA: 0x00135586 File Offset: 0x00133786
		public PropertyDefinition ItemSortProperty { get; private set; }

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x060049FF RID: 18943 RVA: 0x0013558F File Offset: 0x0013378F
		// (set) Token: 0x06004A00 RID: 18944 RVA: 0x00135597 File Offset: 0x00133797
		public PropertyDefinition CoversationSortProperty { get; private set; }

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x06004A01 RID: 18945 RVA: 0x001355A0 File Offset: 0x001337A0
		public bool NeedsDeliveryTimeSecondarySortKey
		{
			get
			{
				return this.ColumnId != FolderViewColumnId.DateTime;
			}
		}

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x06004A02 RID: 18946 RVA: 0x001355AE File Offset: 0x001337AE
		public static ICollection<FolderViewColumnId> SupportedFolderViewColumnIds
		{
			get
			{
				return MailSortOptions.columnMap.Keys;
			}
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x001355BC File Offset: 0x001337BC
		public static SortBy[] GetSortByForFolderViewState(FolderViewState folderViewState)
		{
			MailSortOptions mailSortOptions;
			if (MailSortOptions.columnMap.TryGetValue(folderViewState.SortColumn, out mailSortOptions))
			{
				return mailSortOptions.AsSortBy(folderViewState.View, folderViewState.SortOrder);
			}
			return null;
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x001355F4 File Offset: 0x001337F4
		public SortBy[] AsSortBy(FolderViewType viewType, SortOrder sortOrder)
		{
			PropertyDefinition columnDefinition = (viewType == FolderViewType.ConversationView) ? this.CoversationSortProperty : this.ItemSortProperty;
			SortBy[] array = new SortBy[this.NeedsDeliveryTimeSecondarySortKey ? 2 : 1];
			array[0] = new SortBy(columnDefinition, sortOrder);
			if (this.NeedsDeliveryTimeSecondarySortKey)
			{
				PropertyDefinition columnDefinition2 = (viewType == FolderViewType.ConversationView) ? ConversationItemSchema.ConversationLastDeliveryTime : ItemSchema.ReceivedTime;
				array[1] = new SortBy(columnDefinition2, SortOrder.Descending);
			}
			return array;
		}

		// Token: 0x040027F0 RID: 10224
		private static Dictionary<FolderViewColumnId, MailSortOptions> columnMap = new Dictionary<FolderViewColumnId, MailSortOptions>
		{
			{
				FolderViewColumnId.DateTime,
				new MailSortOptions(FolderViewColumnId.DateTime, ItemSchema.ReceivedTime, ConversationItemSchema.ConversationLastDeliveryTime)
			},
			{
				FolderViewColumnId.From,
				new MailSortOptions(FolderViewColumnId.From, ItemSchema.SentRepresentingDisplayName, ConversationItemSchema.ConversationMVFrom)
			},
			{
				FolderViewColumnId.Size,
				new MailSortOptions(FolderViewColumnId.Size, ItemSchema.Size, ConversationItemSchema.ConversationMessageSize)
			},
			{
				FolderViewColumnId.Subject,
				new MailSortOptions(FolderViewColumnId.Subject, ItemSchema.Subject, ConversationItemSchema.ConversationTopic)
			},
			{
				FolderViewColumnId.HasAttachment,
				new MailSortOptions(FolderViewColumnId.HasAttachment, ItemSchema.HasAttachment, ConversationItemSchema.ConversationHasAttach)
			},
			{
				FolderViewColumnId.Importance,
				new MailSortOptions(FolderViewColumnId.Importance, ItemSchema.Importance, ConversationItemSchema.ConversationImportance)
			},
			{
				FolderViewColumnId.Flagged,
				new MailSortOptions(FolderViewColumnId.Flagged, ItemSchema.FlagStatus, ConversationItemSchema.ConversationFlagStatus)
			},
			{
				FolderViewColumnId.To,
				new MailSortOptions(FolderViewColumnId.To, ItemSchema.DisplayTo, ConversationItemSchema.ConversationMVTo)
			},
			{
				FolderViewColumnId.ItemClass,
				new MailSortOptions(FolderViewColumnId.ItemClass, StoreObjectSchema.ItemClass, ConversationItemSchema.ConversationMessageClasses)
			}
		};
	}
}
