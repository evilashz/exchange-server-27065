using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000025 RID: 37
	public class ConversationMessageViewTable : ViewTable
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x00032C60 File Offset: 0x00030E60
		public ConversationMessageViewTable(Context context, Mailbox mailbox, ExchangeId folderId, IList<ConversationMembersBlob> conversationMembersList) : base(mailbox, DatabaseSchema.MessageTable(mailbox.Database).Table)
		{
			this.folderId = folderId;
			this.messageTable = DatabaseSchema.MessageTable(mailbox.Database);
			this.conversationMembersTableFunction = DatabaseSchema.ConversationMembersBlobTableFunction(base.Mailbox.Database);
			this.conversationMembersList = conversationMembersList;
			base.SetImplicitCriteria(Factory.CreateSearchCriteriaTrue());
			this.SortTable(SortOrder.Empty);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00032CD0 File Offset: 0x00030ED0
		public ConversationMessageViewTable(Context context, Mailbox mailbox, ExchangeId folderId) : this(context, mailbox, folderId, null)
		{
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00032CDC File Offset: 0x00030EDC
		public ConversationMessageViewTable(Context context, Mailbox mailbox, ExchangeId folderId, IList<Column> columns, SortOrder sortOrder, SearchCriteria criteria) : this(context, mailbox, folderId)
		{
			base.SetColumns(context, columns);
			this.SortTable(sortOrder);
			this.Restrict(context, criteria);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00032D01 File Offset: 0x00030F01
		public byte[] ConversationId
		{
			get
			{
				return this.conversationId;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00032D09 File Offset: 0x00030F09
		internal ExchangeId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00032D11 File Offset: 0x00030F11
		protected override Index LogicalKeyIndex
		{
			get
			{
				return this.conversationMembersTableFunction.TableFunction.PrimaryKeyIndex;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00032D23 File Offset: 0x00030F23
		protected override bool MustUseLazyIndex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00032D26 File Offset: 0x00030F26
		public override IList<Column> LongValueColumnsToPreread
		{
			get
			{
				return this.longValueColumnsToPreread;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00032D2E File Offset: 0x00030F2E
		private bool ExpandedConversationMembersView
		{
			get
			{
				return this.conversationMembersList != null;
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00032D3C File Offset: 0x00030F3C
		public override void Restrict(Context context, SearchCriteria restrictCriteria)
		{
			this.conversationId = ConversationMessageViewTable.GetConversationIdFromRestriction(restrictCriteria);
			this.conversationDocumentId = ConversationItem.GetConversationDocumentId(context, base.Mailbox, this.conversationId);
			base.InvalidateBookmarkAndRowCount();
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00032D68 File Offset: 0x00030F68
		public override int GetRowCount(Context context)
		{
			if (this.ExpandedConversationMembersView)
			{
				if (this.conversationMembersList != null)
				{
					return this.conversationMembersList.Count;
				}
				return 0;
			}
			else
			{
				if (this.conversationId == null)
				{
					return 0;
				}
				return base.GetRowCount(context);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00032D9C File Offset: 0x00030F9C
		public override void SetColumns(Context context, IList<Column> columns, ViewSetColumnsFlag flags)
		{
			if (!this.SkipPropertiesPromotionValidation(context, flags))
			{
				Folder folder = Folder.OpenFolder(context, base.Mailbox, this.folderId);
				if (folder == null)
				{
					throw new StoreException((LID)47616U, ErrorCodeValue.InvalidObject);
				}
				PropertyPromotionHelper.ValidatePropertiesPromotion(context, base.Mailbox, folder.GetName(context), columns);
			}
			if (columns != null)
			{
				foreach (Column column in columns)
				{
					ExtendedPropertyColumn extendedPropertyColumn = column as ExtendedPropertyColumn;
					if (!(extendedPropertyColumn == null) && (extendedPropertyColumn.StorePropTag.PropTag == 1071185951U || extendedPropertyColumn.StorePropTag.PropTag == 1071120415U))
					{
						this.longValueColumnsToPreread = new List<Column>
						{
							this.messageTable.OffPagePropertyBlob
						};
						break;
					}
				}
			}
			base.SetColumns(context, columns, flags);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00032E98 File Offset: 0x00031098
		protected override IReadOnlyDictionary<Column, Column> GetColumnRenames(Context context)
		{
			Dictionary<Column, Column> dictionary = new Dictionary<Column, Column>(3);
			dictionary[this.messageTable.VirtualIsRead] = this.messageTable.IsRead;
			dictionary[this.messageTable.VirtualParentDisplay] = TopMessage.CreateVirtualParentDisplayFunctionColumn(this.messageTable, new Func<object[], object>(this.GetParentDisplayColumnFunction));
			dictionary[this.messageTable.VirtualUnreadMessageCount] = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.UnreadCountInt64);
			return dictionary;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00032F18 File Offset: 0x00031118
		protected internal override IList<IIndex> GetInScopePseudoIndexes(Context context, SearchCriteria findRowCriteria, out IList<IIndex> masterIndexes)
		{
			masterIndexes = null;
			IList<ConversationMembersBlob> conversationMembers = this.GetConversationMembers(context);
			if (conversationMembers != null && conversationMembers.Count > 2)
			{
				List<KeyRange> list = new List<KeyRange>(conversationMembers.Count);
				for (int i = 0; i < conversationMembers.Count; i++)
				{
					ConversationMembersBlob conversationMembersBlob = conversationMembers[i];
					StartStopKey startStopKey = new StartStopKey(true, new object[]
					{
						base.Mailbox.MailboxPartitionNumber,
						conversationMembersBlob.FolderId,
						false,
						conversationMembersBlob.MessageId
					});
					list.Add(new KeyRange(startStopKey, startStopKey));
				}
				using (PreReadOperator preReadOperator = Factory.CreatePreReadOperator(context.Culture, context, this.messageTable.Table, this.messageTable.MessageUnique, list, null, true))
				{
					preReadOperator.ExecuteScalar();
				}
			}
			object obj = ConversationMembersBlob.Serialize(conversationMembers);
			return new List<IIndex>(1)
			{
				new SimplePseudoIndex(this.messageTable.Table, this.conversationMembersTableFunction.TableFunction, new object[]
				{
					obj
				}, this.conversationMembersTableFunction.TableFunction.PrimaryKeyIndex.SortOrder, new Dictionary<Column, Column>(4)
				{
					{
						this.messageTable.MailboxPartitionNumber,
						Factory.CreateConstantColumn(base.Mailbox.MailboxPartitionNumber, this.messageTable.MailboxPartitionNumber)
					},
					{
						this.messageTable.FolderId,
						this.conversationMembersTableFunction.FolderId
					},
					{
						this.messageTable.MessageId,
						this.conversationMembersTableFunction.MessageId
					},
					{
						this.messageTable.IsHidden,
						Factory.CreateConstantColumn(false, this.messageTable.IsHidden)
					}
				}, null, true)
			};
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00033104 File Offset: 0x00031304
		public int? GetConversationDocumentId(Context context)
		{
			if (this.conversationDocumentId == null)
			{
				this.conversationDocumentId = ConversationItem.GetConversationDocumentId(context, base.Mailbox, this.conversationId);
			}
			return this.conversationDocumentId;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00033134 File Offset: 0x00031334
		private static byte[] GetConversationIdFromRestriction(SearchCriteria restriction)
		{
			SearchCriteriaCompare searchCriteriaCompare = restriction as SearchCriteriaCompare;
			if (searchCriteriaCompare != null && searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal)
			{
				ConstantColumn constantColumn = (searchCriteriaCompare.Lhs as ConstantColumn) ?? (searchCriteriaCompare.Rhs as ConstantColumn);
				ExtendedPropertyColumn extendedPropertyColumn = (searchCriteriaCompare.Lhs as ExtendedPropertyColumn) ?? (searchCriteriaCompare.Rhs as ExtendedPropertyColumn);
				if (null != constantColumn && null != extendedPropertyColumn && PropTag.Message.ConversationId == extendedPropertyColumn.StorePropTag)
				{
					return constantColumn.Value as byte[];
				}
			}
			throw new StoreException((LID)63096U, ErrorCodeValue.TooComplex);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000331D0 File Offset: 0x000313D0
		private IList<ConversationMembersBlob> GetConversationMembers(Context context)
		{
			if (this.ExpandedConversationMembersView)
			{
				return this.conversationMembersList;
			}
			if (this.conversationId == null)
			{
				throw new StoreException((LID)36935U, ErrorCodeValue.NotInitialized);
			}
			List<ConversationMembersBlob> list = null;
			using (ConversationItem conversationItem = ConversationItem.OpenConversationItem(context, base.Mailbox, this.conversationId))
			{
				if (conversationItem != null)
				{
					byte[] fidMidBlob = conversationItem.GetFidMidBlob();
					int num = 0;
					List<FidMid> list2 = FidMidListSerializer.FromBytes(fidMidBlob, ref num, base.Mailbox.ReplidGuidMap);
					if (list2 != null)
					{
						list = new List<ConversationMembersBlob>(list2.Count);
						for (int i = 0; i < list2.Count; i++)
						{
							list.Add(new ConversationMembersBlob(list2[i].FolderId.To26ByteArray(), list2[i].MessageId.To26ByteArray(), i));
						}
					}
				}
			}
			if (list == null)
			{
				list = new List<ConversationMembersBlob>(0);
			}
			return list;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000332D4 File Offset: 0x000314D4
		private object GetParentDisplayColumnFunction(object[] columnValues)
		{
			Context currentOperationContext = base.Mailbox.CurrentOperationContext;
			Folder folder = null;
			ExchangeId id = ExchangeId.CreateFrom26ByteArray(currentOperationContext, base.Mailbox.ReplidGuidMap, (byte[])columnValues[0]);
			if (id.IsValid)
			{
				folder = Folder.OpenFolder(currentOperationContext, base.Mailbox, id);
			}
			if (folder != null)
			{
				return folder.GetName(currentOperationContext);
			}
			return null;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0003332C File Offset: 0x0003152C
		private bool SkipPropertiesPromotionValidation(Context context, ViewSetColumnsFlag flags)
		{
			return !this.folderId.IsValid || (flags & ViewSetColumnsFlag.NoColumnValidation) == ViewSetColumnsFlag.NoColumnValidation || ((!context.TestCaseId.IsNotNull || !ConversationMessageViewTable.conversationMembersViewValidationTestCases.Contains(context.TestCaseId)) && ViewTable.ClientTypeExcludedFromDefaultPromotedValidation(context.ClientType));
		}

		// Token: 0x0400025F RID: 607
		private static readonly TestCaseId[] conversationMembersViewValidationTestCases = new TestCaseId[0];

		// Token: 0x04000260 RID: 608
		private readonly MessageTable messageTable;

		// Token: 0x04000261 RID: 609
		private readonly ConversationMembersBlobTableFunction conversationMembersTableFunction;

		// Token: 0x04000262 RID: 610
		private readonly IList<ConversationMembersBlob> conversationMembersList;

		// Token: 0x04000263 RID: 611
		private readonly ExchangeId folderId;

		// Token: 0x04000264 RID: 612
		private byte[] conversationId;

		// Token: 0x04000265 RID: 613
		private int? conversationDocumentId;

		// Token: 0x04000266 RID: 614
		private List<Column> longValueColumnsToPreread;
	}
}
