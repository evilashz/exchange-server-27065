using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.LazyIndexing;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000070 RID: 112
	public class MessageViewTable : ViewTable
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x000471CC File Offset: 0x000453CC
		public MessageViewTable(Context context, Mailbox mailbox, ExchangeId fid, bool? hiddenItemView, bool useLazyIndex, bool useCoveringIndex, bool useIndexForEmptyFolder) : base(mailbox, Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database).Table)
		{
			this.fid = fid;
			this.hiddenItemView = hiddenItemView;
			this.useLazyIndex = useLazyIndex;
			this.useCoveringIndex = useCoveringIndex;
			this.useIndexForEmptyFolder = useIndexForEmptyFolder;
			if (!this.IsMailboxScopeView)
			{
				this.folder = Folder.OpenFolder(context, base.Mailbox, this.fid);
				if (this.folder == null)
				{
					throw new StoreException((LID)58880U, ErrorCodeValue.InvalidObject);
				}
				this.searchFolder = (this.folder as SearchFolder);
			}
			this.messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database);
			SearchCriteria searchCriteria = Factory.CreateSearchCriteriaCompare(this.messageTable.MailboxPartitionNumber, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(mailbox.MailboxPartitionNumber, this.messageTable.MailboxPartitionNumber));
			if (hiddenItemView != null)
			{
				searchCriteria = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
				{
					searchCriteria,
					Factory.CreateSearchCriteriaCompare(this.messageTable.IsHidden, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(hiddenItemView.Value ? SerializedValue.BoxedTrue : SerializedValue.BoxedFalse, this.messageTable.IsHidden))
				});
			}
			if (this.searchFolder == null && !this.IsMailboxScopeView)
			{
				searchCriteria = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
				{
					searchCriteria,
					Factory.CreateSearchCriteriaCompare(this.messageTable.FolderId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(fid.To26ByteArray(), this.messageTable.FolderId))
				});
			}
			if (this.IsMailboxScopeView)
			{
				ExchangeId conversationFolderId = ConversationItem.GetConversationFolderId(context, mailbox);
				searchCriteria = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
				{
					searchCriteria,
					Factory.CreateSearchCriteriaCompare(this.messageTable.FolderId, SearchCriteriaCompare.SearchRelOp.NotEqual, Factory.CreateConstantColumn(conversationFolderId.To26ByteArray(), this.messageTable.FolderId)),
					Factory.CreateSearchCriteriaCompare(this.messageTable.FolderId, SearchCriteriaCompare.SearchRelOp.NotEqual, Factory.CreateConstantColumn(null, this.messageTable.FolderId))
				});
			}
			base.SetImplicitCriteria(searchCriteria);
			if (this.useLazyIndex)
			{
				this.SetDefaultSortOrder();
				return;
			}
			if (this.IsMailboxScopeView)
			{
				base.SortTable(this.messageTable.MessagePK.SortOrder);
				return;
			}
			if (this.searchFolder == null)
			{
				base.SortTable(this.messageTable.MessageUnique.SortOrder);
				return;
			}
			int? logicalIndexNumber = this.searchFolder.GetLogicalIndexNumber(context);
			if (logicalIndexNumber != null)
			{
				LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, base.Mailbox, this.Fid, logicalIndexNumber.Value);
				base.SortTable(logicalIndex.LogicalSortOrder);
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00047453 File Offset: 0x00045653
		public MessageViewTable(Context context, Mailbox mailbox, ExchangeId fid, bool hiddenItemView) : this(context, mailbox, fid, new bool?(hiddenItemView), true, false, false)
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00047468 File Offset: 0x00045668
		public MessageViewTable(Context context, Mailbox mailbox, ExchangeId fid, bool hiddenItemView, IList<Column> columns, SortOrder sortOrder, SearchCriteria criteria) : this(context, mailbox, fid, new bool?(hiddenItemView), true, false, false)
		{
			base.SetColumns(context, columns);
			this.SortTable(sortOrder);
			this.Restrict(context, criteria);
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00047497 File Offset: 0x00045697
		public static int SmallFolderThreshold
		{
			get
			{
				return MessageViewTable.smallFolderThreshold.Value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x000474A3 File Offset: 0x000456A3
		public ExchangeId Fid
		{
			get
			{
				return this.fid;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x000474AB File Offset: 0x000456AB
		public Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x000474B3 File Offset: 0x000456B3
		public SearchFolder SearchFolder
		{
			get
			{
				return this.searchFolder;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x000474BB File Offset: 0x000456BB
		public override IList<Column> LongValueColumnsToPreread
		{
			get
			{
				return this.longValueColumnsToPreread;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x000474C3 File Offset: 0x000456C3
		internal bool IsSearchFolder
		{
			get
			{
				return this.searchFolder != null;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x000474D1 File Offset: 0x000456D1
		protected override Index LogicalKeyIndex
		{
			get
			{
				if (!this.useLazyIndex)
				{
					return null;
				}
				return this.messageTable.MessagePK;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x000474E8 File Offset: 0x000456E8
		protected override bool MustUseLazyIndex
		{
			get
			{
				return !this.IsMailboxScopeView && (this.searchFolder != null || base.MvExplosion || base.IsCategorizedView);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0004751C File Offset: 0x0004571C
		protected override Dictionary<Column, FilterFactorHint> FilterFactorHints
		{
			get
			{
				return this.filterFactorHints;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00047524 File Offset: 0x00045724
		protected bool IsMailboxScopeView
		{
			get
			{
				return this.fid == ExchangeId.Zero;
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00047538 File Offset: 0x00045738
		public static bool PlanUsesIndex(Context context, List<DataAccessOperator> planOperators, LogicalIndex index)
		{
			for (int i = 0; i < planOperators.Count; i++)
			{
				SimpleQueryOperator simpleQueryOperator = planOperators[i] as SimpleQueryOperator;
				if (simpleQueryOperator != null && simpleQueryOperator.OperatorUsesTablePartition(index.IndexTable, index.IndexKeyPrefix))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00047D60 File Offset: 0x00045F60
		public static SearchCriteria RewriteMessageSearchCriteria(Context context, Mailbox mailbox, SearchCriteria criteria, SortOrder sortOrder, bool conversation, bool mvExplosion, ExchangeId categorizedViewFolderId)
		{
			return criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				SearchCriteriaCompare searchCriteriaCompare = criterion as SearchCriteriaCompare;
				if (searchCriteriaCompare != null && (searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal || searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.NotEqual) && searchCriteriaCompare.Lhs is ExtendedPropertyColumn && searchCriteriaCompare.Rhs is ConstantColumn)
				{
					StorePropTag storePropTag = ((ExtendedPropertyColumn)searchCriteriaCompare.Lhs).StorePropTag;
					uint propTag = storePropTag.PropTag;
					MessageTable messageTable;
					int num;
					if (propTag <= 267976962U)
					{
						if (propTag <= 267780347U)
						{
							if (propTag != 235471099U && propTag != 235471106U && propTag != 267780347U)
							{
								return criterion;
							}
						}
						else if (propTag != 267780354U && propTag != 267976955U && propTag != 267976962U)
						{
							return criterion;
						}
					}
					else if (propTag <= 1709244674U)
					{
						if (propTag != 268370171U && propTag != 268370178U)
						{
							if (propTag != 1709244674U)
							{
								return criterion;
							}
							messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database);
							byte[] bytes = ((ConstantColumn)searchCriteriaCompare.Rhs).Value as byte[];
							ExchangeId exchangeId = ExchangeId.CreateFrom22ByteArray(context, mailbox.ReplidGuidMap, bytes);
							return Factory.CreateSearchCriteriaCompare(messageTable.FolderId, searchCriteriaCompare.RelOp, Factory.CreateConstantColumn(exchangeId.To26ByteArray(), messageTable.FolderId));
						}
					}
					else
					{
						if (propTag <= 1732902932U)
						{
							if (propTag != 1732771860U && propTag != 1732902932U)
							{
								return criterion;
							}
						}
						else if (propTag != 1733099540U)
						{
							if (propTag != 1733165059U)
							{
								return criterion;
							}
							num = (int)((ConstantColumn)searchCriteriaCompare.Rhs).Value;
							if (!mvExplosion)
							{
								if (num == 0 != (searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal))
								{
									return Factory.CreateSearchCriteriaFalse();
								}
								return Factory.CreateSearchCriteriaTrue();
							}
							else
							{
								if (num != 0 || !categorizedViewFolderId.IsNullOrZero)
								{
									return criterion;
								}
								if (searchCriteriaCompare.RelOp != SearchCriteriaCompare.SearchRelOp.Equal)
								{
									return Factory.CreateSearchCriteriaTrue();
								}
								return Factory.CreateSearchCriteriaFalse();
							}
						}
						messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database);
						long legacyId = (long)((ConstantColumn)searchCriteriaCompare.Rhs).Value;
						ExchangeId exchangeId = ExchangeId.CreateFromInt64(context, mailbox.ReplidGuidMap, legacyId);
						if (storePropTag.PropTag == 1732771860U)
						{
							return Factory.CreateSearchCriteriaCompare(messageTable.FolderId, searchCriteriaCompare.RelOp, Factory.CreateConstantColumn(exchangeId.To26ByteArray(), messageTable.FolderId));
						}
						if (storePropTag.PropTag == 1733099540U && exchangeId.Guid == ReplidGuidMap.ReservedGuidForCategorizedViews)
						{
							Column column = PropertySchema.MapToColumn(mailbox.Database, ObjectType.Message, PropTag.Message.CategID);
							return Factory.CreateSearchCriteriaCompare(column, searchCriteriaCompare.RelOp, Factory.CreateConstantColumn(exchangeId.ToLong(), column));
						}
						return Factory.CreateSearchCriteriaCompare(messageTable.MessageId, searchCriteriaCompare.RelOp, Factory.CreateConstantColumn(exchangeId.To26ByteArray(), messageTable.MessageId));
					}
					messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database);
					byte[] entryId = ((ConstantColumn)searchCriteriaCompare.Rhs).Value as byte[];
					bool flag = false;
					ExchangeId exchangeId2;
					ExchangeId messageId;
					if (EntryIdHelpers.ParseServerEntryId(context, mailbox.ReplidGuidMap, entryId, false, out exchangeId2, out messageId, out num))
					{
						List<SearchCriteria> list = new List<SearchCriteria>(3);
						uint propTag2 = storePropTag.PropTag;
						if (propTag2 <= 267780354U)
						{
							if (propTag2 <= 235471106U)
							{
								if (propTag2 == 235471099U || propTag2 == 235471106U)
								{
									list.Add(Factory.CreateSearchCriteriaCompare(messageTable.FolderId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(exchangeId2.To26ByteArray(), messageTable.FolderId)));
								}
							}
							else if (propTag2 == 267780347U || propTag2 == 267780354U)
							{
								if (messageId.Guid == ReplidGuidMap.ReservedGuidForCategorizedViews)
								{
									if (categorizedViewFolderId.IsValid && (exchangeId2.IsNullOrZero || exchangeId2 == categorizedViewFolderId) && num == 0)
									{
										Column column2 = PropertySchema.MapToColumn(mailbox.Database, ObjectType.Message, PropTag.Message.CategID);
										list.Add(Factory.CreateSearchCriteriaCompare(column2, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(messageId.ToLong(), column2)));
									}
									else
									{
										list.Add(Factory.CreateSearchCriteriaFalse());
									}
								}
								else if (!mvExplosion)
								{
									list.Add(Factory.CreateSearchCriteriaCompare(messageTable.FolderId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(exchangeId2.To26ByteArray(), messageTable.FolderId)));
									list.Add(Factory.CreateSearchCriteriaCompare(messageTable.MessageId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(messageId.To26ByteArray(), messageTable.MessageId)));
								}
								else
								{
									flag = true;
								}
							}
						}
						else
						{
							if (propTag2 <= 267976962U)
							{
								if (propTag2 != 267976955U && propTag2 != 267976962U)
								{
									goto IL_500;
								}
							}
							else
							{
								if (propTag2 != 268370171U && propTag2 != 268370178U)
								{
									goto IL_500;
								}
								if (!conversation)
								{
									if (!mvExplosion)
									{
										list.Add(Factory.CreateSearchCriteriaCompare(messageTable.FolderId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(exchangeId2.To26ByteArray(), messageTable.FolderId)));
										list.Add(Factory.CreateSearchCriteriaCompare(messageTable.MessageId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(messageId.To26ByteArray(), messageTable.MessageId)));
										goto IL_500;
									}
									flag = true;
									goto IL_500;
								}
							}
							list.Add(Factory.CreateSearchCriteriaCompare(messageTable.MessageId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(exchangeId2.To26ByteArray(), messageTable.MessageId)));
						}
						IL_500:
						if (flag)
						{
							using (TopMessage topMessage = TopMessage.OpenMessage(context, mailbox, exchangeId2, messageId))
							{
								if (topMessage == null)
								{
									list.Add(Factory.CreateSearchCriteriaFalse());
								}
								else if (num == 0)
								{
									int documentId = topMessage.GetDocumentId(context);
									list.Add(Factory.CreateSearchCriteriaCompare(messageTable.MessageDocumentId, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(documentId, messageTable.MessageDocumentId)));
								}
								else
								{
									Column col = PropertySchema.MapToColumn(mailbox.Database, ObjectType.Message, PropTag.Message.InstanceNum);
									for (int i = 0; i < sortOrder.Count; i++)
									{
										Column column3 = sortOrder[i].Column;
										if (column3 == col)
										{
											list.Add(Factory.CreateSearchCriteriaCompare(column3, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(num, column3)));
										}
										else if (column3 is ExtendedPropertyColumn && ((ExtendedPropertyColumn)column3).StorePropTag.IsMultiValueInstance)
										{
											StorePropTag storePropTag2 = ((ExtendedPropertyColumn)column3).StorePropTag;
											StorePropTag propertyTag = storePropTag2.ChangeType(storePropTag2.PropType & (PropertyType)57343);
											Column column4 = PropertySchema.MapToColumn(mailbox.Database, ObjectType.Message, propertyTag);
											object columnValue = topMessage.GetColumnValue(context, column4);
											Array array = (Array)columnValue;
											if (array != null && num > 0 && num - 1 < array.Length)
											{
												list.Add(Factory.CreateSearchCriteriaCompare(column3, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(LogicalIndex.TruncateValueAsNecessary(propertyTag.PropType & (PropertyType)61439, array.GetValue(num - 1)), column3)));
											}
											else if (columnValue == null && num == 1)
											{
												list.Add(Factory.CreateSearchCriteriaCompare(column3, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(null, column3)));
											}
											else
											{
												list.Add(Factory.CreateSearchCriteriaFalse());
											}
										}
										else
										{
											list.Add(Factory.CreateSearchCriteriaCompare(column3, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(LogicalIndex.TruncateValueAsNecessary(PropertyTypeHelper.PropTypeFromExtendedTypeCode(column3.ExtendedTypeCode), topMessage.GetColumnValue(context, column3)), column3)));
										}
									}
								}
							}
						}
						if (searchCriteriaCompare.RelOp != SearchCriteriaCompare.SearchRelOp.Equal)
						{
							return Factory.CreateSearchCriteriaNot(Factory.CreateSearchCriteriaAnd(list.ToArray()));
						}
						return Factory.CreateSearchCriteriaAnd(list.ToArray());
					}
				}
				return criterion;
			}, (context.Culture == null) ? null : context.Culture.CompareInfo, false);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00047DD3 File Offset: 0x00045FD3
		public override void SortTable(SortOrder sortOrder)
		{
			base.SortTable(sortOrder);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00047EFC File Offset: 0x000460FC
		public override void Restrict(Context context, SearchCriteria restrictCriteria)
		{
			this.superFilterColumn = null;
			this.filterFactorHints = null;
			base.Restrict(context, restrictCriteria);
			if (base.RestrictCriteria != null)
			{
				base.RestrictCriteria = base.RestrictCriteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
				{
					if (criterion is SearchCriteriaOr || criterion is SearchCriteriaNot)
					{
						return null;
					}
					if (criterion is SearchCriteriaCompare)
					{
						SearchCriteriaCompare searchCriteriaCompare = (SearchCriteriaCompare)criterion;
						if (searchCriteriaCompare.Rhs is ConstantColumn && searchCriteriaCompare.Lhs is ExtendedPropertyColumn && searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal)
						{
							ExtendedPropertyColumn extendedPropertyColumn = (ExtendedPropertyColumn)searchCriteriaCompare.Lhs;
							if (extendedPropertyColumn.StorePropTag == PropTag.Message.InternetMessageId)
							{
								Column column = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.InternetMessageIdHash);
								string text = (string)((ConstantColumn)searchCriteriaCompare.Rhs).Value;
								object value = (text == null) ? null : HashHelpers.GetInternetMessageIdHash(text);
								this.superFilterColumn = column;
								this.filterFactorHints = new Dictionary<Column, FilterFactorHint>(1)
								{
									{
										column,
										new FilterFactorHint
										{
											FilterFactor = 0.99
										}
									}
								};
								return Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
								{
									Factory.CreateSearchCriteriaCompare(column, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(value, column)),
									criterion
								});
							}
						}
					}
					return criterion;
				}, (context.Culture == null) ? null : context.Culture.CompareInfo, false);
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00047F64 File Offset: 0x00046164
		public override void SetColumns(Context context, IList<Column> columns, ViewSetColumnsFlag flags)
		{
			if (!this.SkipPropertiesPromotionValidation(context, flags))
			{
				PropertyPromotionHelper.ValidatePropertiesPromotion(context, base.Mailbox, this.folder.GetName(context), columns);
			}
			if (columns != null)
			{
				foreach (Column column in columns)
				{
					ExtendedPropertyColumn extendedPropertyColumn = column as ExtendedPropertyColumn;
					if (!(extendedPropertyColumn == null) && (extendedPropertyColumn.StorePropTag == PropTag.Message.Preview || extendedPropertyColumn.StorePropTag == PropTag.Message.PreviewUnread))
					{
						this.longValueColumnsToPreread = new List<Column>
						{
							this.messageTable.OffPagePropertyBlob
						};
						break;
					}
				}
			}
			if (!this.IsMailboxScopeView && this.searchFolder == null && (columns.Count < 50 || context.ClientType == ClientType.Migration || context.ClientType == ClientType.PublicFolderSystem) && this.hiddenItemView != null && !InTransitInfo.IsMoveSource(InTransitInfo.GetInTransitStatus(base.Mailbox.SharedState)))
			{
				Folder folder = Folder.OpenFolder(context, base.Mailbox, this.fid);
				if (folder != null)
				{
					folder.PromoteProperties(context, this.hiddenItemView.Value, columns);
				}
			}
			base.SetColumns(context, columns, flags);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000480B0 File Offset: 0x000462B0
		public override int GetRowCount(Context context)
		{
			if (this.IsViewEmpty(context))
			{
				return 0;
			}
			if (base.RowCountValid)
			{
				return base.RowCount;
			}
			if (base.MvExplosion || base.RestrictCriteria != null || base.IsCategorizedView)
			{
				return base.GetRowCount(context);
			}
			int num = 0;
			Folder folder = (this.searchFolder != null) ? this.searchFolder : Folder.OpenFolder(context, base.Mailbox, this.fid);
			if (folder != null)
			{
				if (this.hiddenItemView == null)
				{
					num += (int)folder.GetMessageCount(context) + (int)folder.GetHiddenItemCount(context);
				}
				else if (!this.hiddenItemView.Value)
				{
					num += (int)folder.GetMessageCount(context);
				}
				else
				{
					num += (int)folder.GetHiddenItemCount(context);
				}
			}
			return num;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00048170 File Offset: 0x00046370
		public override void Categorize(Context context, int categoryCount, int expandedCount, CategoryHeaderSortOverride[] categoryHeaderSortOverrides)
		{
			if (!this.IsCategorizationSupportEnabled(context))
			{
				return;
			}
			if (base.RestrictCriteria != null)
			{
				if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.TraceDebug<ExchangeId>(0L, "Folder {0}: Don't currently support categorizing a restricted view, so the current restriction will be removed before categorizing the view.", this.Fid);
				}
				this.Restrict(context, Factory.CreateSearchCriteriaTrue());
			}
			SortOrderBuilder sortOrderBuilder = new SortOrderBuilder();
			for (int i = 0; i < categoryCount; i++)
			{
				bool flag = false;
				if (i > 0 && !base.SortOrder.Ascending[i])
				{
					flag = true;
				}
				if (categoryHeaderSortOverrides[i] != null)
				{
					if (flag || (i > 0 && !categoryHeaderSortOverrides[i].Ascending))
					{
						Column column = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, LogicalIndex.CategoryHeaderLevelStubPropTagFromLevel(i));
						sortOrderBuilder.Add(column, false);
						flag = false;
					}
					Column column2 = LogicalIndex.CategoryHeaderAggregatePropFromLevel(categoryHeaderSortOverrides[i].Column, i);
					sortOrderBuilder.Add(column2, categoryHeaderSortOverrides[i].Ascending);
				}
				if (flag)
				{
					Column column3 = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, LogicalIndex.CategoryHeaderLevelStubPropTagFromLevel(i));
					sortOrderBuilder.Add(column3, false);
				}
				sortOrderBuilder.Add(base.SortOrder.Columns[i], base.SortOrder.Ascending[i]);
			}
			Column column4 = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.Depth);
			sortOrderBuilder.Add(column4, true);
			SortOrder categoryHeadersSortOrder = sortOrderBuilder.ToSortOrder();
			if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.TraceDebug(0L, "Folder {0} ({1}): Setting categorized view ({2} levels, with {3} levels initially expanded and {4} sort overrides): {5}", new object[]
				{
					this.Fid,
					Folder.GetName(context, base.Mailbox, this.Fid),
					categoryCount,
					expandedCount,
					CategoryHeaderSortOverride.NumberOfOverrides(categoryHeaderSortOverrides),
					categoryHeadersSortOrder.ToString()
				});
			}
			base.SetCategorizedView(categoryCount, expandedCount, categoryHeadersSortOrder, categoryHeaderSortOverrides);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00048360 File Offset: 0x00046560
		public override int CollapseRow(Context context, ExchangeId categoryId)
		{
			int num = 0;
			if (!this.IsCategorizationSupportEnabled(context))
			{
				throw new NotSupportedException((LID)45752U, "Don't currently support categorized views, so rows cannot be collapsed.");
			}
			if (this.IsViewEmpty(context))
			{
				throw new ObjectNotFoundException((LID)62136U, base.Mailbox.MailboxGuid, "View is empty, so category id cannot exist.");
			}
			LogicalIndex categoryHeaderViewLogicalIndex = this.GetCategoryHeaderViewLogicalIndex(context);
			if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.TraceDebug<ExchangeId, int>(0L, "CollapseRow: Generating query plan to find category id {0} in category headers logical index {1}.", categoryId, categoryHeaderViewLogicalIndex.LogicalIndexNumber);
			}
			SearchCriteria findRowCriteria = this.CreateCategoryIdCriterion(context, categoryHeaderViewLogicalIndex, categoryId.Counter, SearchCriteriaCompare.SearchRelOp.Equal);
			using (SimpleQueryOperator findCategoryHeaderRowOperator = this.GetFindCategoryHeaderRowOperator(context, categoryHeaderViewLogicalIndex, findRowCriteria, Bookmark.BOT, false))
			{
				using (Reader reader = findCategoryHeaderRowOperator.ExecuteReader(false))
				{
					if (!reader.Read())
					{
						if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.TraceDebug<ExchangeId, int>(0L, "Couldn't find category id {0} in category headers logical index {1}.", categoryId, categoryHeaderViewLogicalIndex.LogicalIndexNumber);
						}
						throw new ObjectNotFoundException((LID)37560U, base.Mailbox.MailboxGuid, "Category id does not exist.");
					}
					num = categoryHeaderViewLogicalIndex.CollapseCategoryHeader(context, reader, base.CollapseState);
					base.AdjustRowCountAfterExpandOrCollapse(-num);
				}
			}
			return num;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000484AC File Offset: 0x000466AC
		public override int ExpandRow(Context context, ExchangeId categoryId)
		{
			int num = 0;
			if (!this.IsCategorizationSupportEnabled(context))
			{
				throw new NotSupportedException((LID)53944U, "Don't currently support categorized views, so rows cannot be expanded.");
			}
			if (this.IsViewEmpty(context))
			{
				throw new ObjectNotFoundException((LID)41656U, base.Mailbox.MailboxGuid, "View is empty, so category id cannot exist.");
			}
			LogicalIndex categoryHeaderViewLogicalIndex = this.GetCategoryHeaderViewLogicalIndex(context);
			if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.TraceDebug<ExchangeId, int>(0L, "ExpandRow: Generating query plan to find category id {0} in category headers logical index {1}.", categoryId, categoryHeaderViewLogicalIndex.LogicalIndexNumber);
			}
			SearchCriteria findRowCriteria = this.CreateCategoryIdCriterion(context, categoryHeaderViewLogicalIndex, categoryId.Counter, SearchCriteriaCompare.SearchRelOp.Equal);
			using (SimpleQueryOperator findCategoryHeaderRowOperator = this.GetFindCategoryHeaderRowOperator(context, categoryHeaderViewLogicalIndex, findRowCriteria, Bookmark.BOT, false))
			{
				using (Reader reader = findCategoryHeaderRowOperator.ExecuteReader(false))
				{
					if (!reader.Read())
					{
						if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel.ExTraceGlobals.CategorizationsTracer.TraceDebug<ExchangeId, int>(0L, "Couldn't find category id {0} in category headers logical index {1}.", categoryId, categoryHeaderViewLogicalIndex.LogicalIndexNumber);
						}
						throw new ObjectNotFoundException((LID)34288U, base.Mailbox.MailboxGuid, "Category id does not exist.");
					}
					num = categoryHeaderViewLogicalIndex.ExpandCategoryHeader(context, reader, base.CollapseState);
					base.AdjustRowCountAfterExpandOrCollapse(num);
					this.ConstructCategorizedViewBookmarkFromHeaderRow(reader, false);
				}
			}
			return num;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00048600 File Offset: 0x00046800
		public override byte[] GetCollapseState(Context context, ExchangeId rowInstanceId, int rowInstanceNumber)
		{
			if (!this.IsCategorizationSupportEnabled(context))
			{
				throw new NotSupportedException((LID)65408U, "Don't currently support categorized views.");
			}
			if (base.CategoryCount == 0)
			{
				throw new NotSupportedException((LID)59264U, "Not supported for non-categorized views.");
			}
			base.SaveLastBookmark();
			byte[] serializedBookmark;
			try
			{
				Column column = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.InstanceId);
				Column column2 = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.InstanceNum);
				SearchCriteria criteria;
				if (rowInstanceId.Guid == ReplidGuidMap.ReservedGuidForCategorizedViews)
				{
					if (rowInstanceNumber == 0)
					{
						criteria = Factory.CreateSearchCriteriaCompare(column, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(rowInstanceId.ToLong(), column));
					}
					else
					{
						criteria = Factory.CreateSearchCriteriaFalse();
					}
				}
				else
				{
					criteria = Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
					{
						Factory.CreateSearchCriteriaCompare(column, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(rowInstanceId.ToLong(), column)),
						Factory.CreateSearchCriteriaCompare(column2, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(rowInstanceNumber, column2))
					});
				}
				bool flag;
				using (Reader reader = this.FindRow(context, criteria, ViewSeekOrigin.Beginning, null, false, out flag))
				{
					if (reader != null)
					{
						base.BookmarkCurrentRow(reader, true);
					}
					else
					{
						base.SeekRow(context, ViewSeekOrigin.Beginning, 0);
					}
					serializedBookmark = base.CreateExternalBookmark();
				}
			}
			finally
			{
				base.RevertToLastBookmark();
			}
			return MessageViewTable.SerializeCollapseState(base.GetSortOrderForView(), base.CollapseState, serializedBookmark);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00048770 File Offset: 0x00046970
		public override byte[] SetCollapseState(Context context, byte[] collapseState)
		{
			if (!this.IsCategorizationSupportEnabled(context))
			{
				throw new NotSupportedException((LID)38672U, "Don't currently support categorized views.");
			}
			if (base.CategoryCount == 0)
			{
				throw new NotSupportedException((LID)34688U, "Not supported for non-categorized views.");
			}
			byte[] bookmark = MessageViewTable.ParseCollapseState(base.GetSortOrderForView(), base.CollapseState, collapseState);
			bool flag;
			int num;
			bool flag2;
			this.SeekRow(context, ViewSeekOrigin.Bookmark, bookmark, 0, false, out flag, out num, false, out flag2);
			return base.CreateExternalBookmark();
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000487E3 File Offset: 0x000469E3
		public override void Reset()
		{
			base.Reset();
			this.SetDefaultSortOrder();
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000487F4 File Offset: 0x000469F4
		public override bool NeedIndexForPositionOrRowCount(Context context)
		{
			return (!this.IsViewEmpty(context) && !base.RowCountValid && (base.MvExplosion || base.RestrictCriteria != null || base.IsCategorizedView)) || !base.Bookmark.PositionValid;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00048840 File Offset: 0x00046A40
		public override IChunked PrepareIndexes(Context context, SearchCriteria findRowCriteria)
		{
			if (this.IsMailboxScopeView || this.IsViewEmpty(context) || !this.useLazyIndex || (LogicalIndex.IndexUseCallbackTestHook == null && this.GetFolderMessageCount(context) <= (long)ConfigurationSchema.ChunkedIndexPopulationFolderSizeThreshold.Value))
			{
				return null;
			}
			LogicalIndex logicalIndex;
			if (base.IsCategorizedView)
			{
				logicalIndex = this.GetBaseMessageViewLogicalIndexDoNotUpdate(context);
			}
			else
			{
				if (this.useCoveringIndex)
				{
					findRowCriteria = this.RewriteSearchCriteria(context, findRowCriteria);
				}
				IList<IIndex> viewIndexes = this.GetViewIndexes(context, findRowCriteria, true);
				logicalIndex = (LogicalIndex)viewIndexes[0];
			}
			bool arg;
			bool arg2;
			IChunked result = logicalIndex.PrepareIndex(context, new GenerateDataAccessOperatorCallback(this.GenerateDataAccessOperatorForIndexPopulation), out arg, out arg2);
			if (LogicalIndex.IndexUseCallbackTestHook != null)
			{
				LogicalIndex.IndexUseCallbackTestHook(logicalIndex, arg, arg2);
			}
			return result;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000488F0 File Offset: 0x00046AF0
		public SimpleQueryOperator.SimpleQueryOperatorDefinition GenerateDataAccessOperatorForIndexPopulation(Context context, LogicalIndex logicalIndex, IList<Column> columnsToFetch, out LogicalIndex baseViewIndex)
		{
			SearchCriteria searchCriteria = null;
			bool? associated = null;
			if (logicalIndex.ConditionalIndexColumn != null)
			{
				if (logicalIndex.ConditionalIndexColumn == this.messageTable.IsHidden)
				{
					associated = new bool?(logicalIndex.ConditionalIndexValue);
				}
				else
				{
					searchCriteria = Factory.CreateSearchCriteriaCompare(logicalIndex.ConditionalIndexColumn, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(logicalIndex.ConditionalIndexValue, logicalIndex.ConditionalIndexColumn));
				}
			}
			if (this.searchFolder != null)
			{
				int? logicalIndexNumber = this.searchFolder.GetLogicalIndexNumber(context);
				if (logicalIndexNumber != null)
				{
					baseViewIndex = LogicalIndexCache.GetLogicalIndex(context, base.Mailbox, this.searchFolder.GetId(context), logicalIndexNumber.Value);
					if (!baseViewIndex.IsStale)
					{
						return this.searchFolder.BaseViewOperatorDefinition(context, base.Mailbox, columnsToFetch, searchCriteria, associated);
					}
				}
			}
			baseViewIndex = null;
			StartStopKey startStopKey;
			if (associated != null)
			{
				startStopKey = new StartStopKey(true, new object[]
				{
					logicalIndex.MailboxPartitionNumber,
					logicalIndex.FolderId.To26ByteArray(),
					associated.Value
				});
			}
			else
			{
				startStopKey = new StartStopKey(true, new object[]
				{
					logicalIndex.MailboxPartitionNumber,
					logicalIndex.FolderId.To26ByteArray()
				});
			}
			FunctionColumn value = TopMessage.CreateVirtualParentDisplayFunctionColumn(this.messageTable, new Func<object[], object>(this.GetParentDisplayColumnFunction));
			return new TableOperator.TableOperatorDefinition(logicalIndex.IndexTable.Culture, this.messageTable.Table, this.messageTable.MessageUnique, columnsToFetch, null, searchCriteria, new Dictionary<Column, Column>(2)
			{
				{
					this.messageTable.VirtualIsRead,
					this.messageTable.IsRead
				},
				{
					this.messageTable.VirtualParentDisplay,
					value
				}
			}, 0, 0, new KeyRange[]
			{
				new KeyRange(startStopKey, startStopKey)
			}, false, true, true);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00048AE6 File Offset: 0x00046CE6
		internal static IDisposable SetSmallFolderThresholdForTest(int smallFolderThreshold)
		{
			return MessageViewTable.smallFolderThreshold.SetTestHook(smallFolderThreshold);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00048AF3 File Offset: 0x00046CF3
		internal static IDisposable SetFindCategorizedViewHeaderRowTestHook(Action action)
		{
			return MessageViewTable.findCategorizedViewHeaderRowTestHook.SetTestHook(action);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00048B00 File Offset: 0x00046D00
		internal static IDisposable SetFindCategorizedViewLeafRowTestHook(Action action)
		{
			return MessageViewTable.findCategorizedViewLeafRowTestHook.SetTestHook(action);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00048B0D File Offset: 0x00046D0D
		internal static IDisposable SetCategorizedViewFastFindRowOperatorTestHook(Action<bool, SimpleQueryOperator, int, int> action)
		{
			return MessageViewTable.categorizedViewFastFindRowOperatorTestHook.SetTestHook(action);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00048B1C File Offset: 0x00046D1C
		internal static byte[] SerializeCollapseState(SortOrder sortOrder, CategorizedTableCollapseState collapseState, byte[] serializedBookmark)
		{
			int num = collapseState.Serialize(null, 0);
			byte[] array = new byte[8 + num + serializedBookmark.Length];
			int num2 = 0;
			ParseSerialize.SerializeInt32(173302083, array, num2);
			num2 += 4;
			ParseSerialize.SerializeInt32(sortOrder.GetHashCode(), array, num2);
			num2 += 4;
			num2 += collapseState.Serialize(array, num2);
			Buffer.BlockCopy(serializedBookmark, 0, array, num2, serializedBookmark.Length);
			num2 += serializedBookmark.Length;
			return array;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00048B8C File Offset: 0x00046D8C
		internal static byte[] ParseCollapseState(SortOrder sortOrder, CategorizedTableCollapseState collapseState, byte[] collapseStateBlob)
		{
			if (collapseStateBlob == null || collapseStateBlob.Length < 12)
			{
				throw new StoreException((LID)46864U, ErrorCodeValue.InvalidCollapseState, "Invalid collapse state (1).");
			}
			int num = 0;
			int num2 = ParseSerialize.ParseInt32(collapseStateBlob, num);
			num += 4;
			if (num2 != 173302083)
			{
				throw new StoreException((LID)33152U, ErrorCodeValue.InvalidCollapseState, "Invalid collapse state (2).");
			}
			int num3 = ParseSerialize.ParseInt32(collapseStateBlob, num);
			num += 4;
			if (num3 != sortOrder.GetHashCode())
			{
				throw new StoreException((LID)49536U, ErrorCodeValue.InvalidCollapseState, "Invalid collapse state (3).");
			}
			num += collapseState.Deserialize(collapseStateBlob, num);
			if (collapseStateBlob.Length - num < 4)
			{
				throw new StoreException((LID)49024U, ErrorCodeValue.InvalidCollapseState, "Invalid collapse state (5).");
			}
			byte[] array = new byte[collapseStateBlob.Length - num];
			Buffer.BlockCopy(collapseStateBlob, num, array, 0, collapseStateBlob.Length - num);
			return array;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00048C6A File Offset: 0x00046E6A
		internal bool IsCategorizationSupportEnabled(Context context)
		{
			return context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00048C84 File Offset: 0x00046E84
		internal LogicalIndex GetBaseMessageViewLogicalIndex(Context context)
		{
			LogicalIndex baseMessageViewLogicalIndexDoNotUpdate = this.GetBaseMessageViewLogicalIndexDoNotUpdate(context);
			baseMessageViewLogicalIndexDoNotUpdate.UpdateIndex(context, new GenerateDataAccessOperatorCallback(this.GenerateDataAccessOperatorForIndexPopulation));
			return baseMessageViewLogicalIndexDoNotUpdate;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00048CB0 File Offset: 0x00046EB0
		internal LogicalIndex GetBaseMessageViewLogicalIndexDoNotUpdate(Context context)
		{
			List<Column> list = new List<Column>(CategoryHeaderSortOverride.NumberOfOverrides(base.CategoryHeaderSortOverrides) + 2);
			foreach (CategoryHeaderSortOverride categoryHeaderSortOverride in base.CategoryHeaderSortOverrides)
			{
				if (categoryHeaderSortOverride != null)
				{
					list.Add(categoryHeaderSortOverride.Column);
				}
			}
			if (!list.Contains(this.messageTable.IsRead))
			{
				list.Add(this.messageTable.IsRead);
			}
			if (base.Mailbox.SharedState.SupportsPerUserFeatures && !list.Contains(this.messageTable.LcnCurrent))
			{
				list.Add(this.messageTable.LcnCurrent);
			}
			Column[] nonKeyColumnsForPseudoIndex = this.GetNonKeyColumnsForPseudoIndex(base.ImplicitCriteria, null, null, list);
			return LogicalIndexCache.GetIndexToUse(context, base.Mailbox, this.fid, (this.searchFolder != null) ? LogicalIndexType.SearchFolderMessages : LogicalIndexType.Messages, this.messageTable.IsHidden, this.hiddenItemView.Value, base.SortOrder, nonKeyColumnsForPseudoIndex, null, base.Table);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00048DB4 File Offset: 0x00046FB4
		internal LogicalIndex GetCategoryHeaderViewLogicalIndex(Context context, LogicalIndex baseMessageViewLogicalIndex)
		{
			List<Column> list = new List<Column>(4);
			list.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.CategID));
			list.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.ContentCount));
			list.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.UnreadCountInt64));
			if (base.Mailbox.SharedState.SupportsPerUserFeatures)
			{
				list.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.CnsetIn));
			}
			Column[] array = new Column[base.CategoryCount];
			for (int i = 0; i < base.CategoryCount; i++)
			{
				array[i] = LogicalIndex.CheckForCategoryHeaderLevelStub(context.Database, base.SortOrder, i, base.CategoryHeaderSortOverrides);
			}
			bool baseMessageViewInReverseOrder;
			baseMessageViewLogicalIndex.IsMatchingSort(base.SortOrder, out baseMessageViewInReverseOrder);
			LogicalIndex indexToUse = LogicalIndexCache.GetIndexToUse(context, base.Mailbox, this.fid, LogicalIndexType.CategoryHeaders, this.messageTable.IsHidden, this.hiddenItemView.Value, base.CategoryHeadersSortOrder, list, new CategorizationInfo(baseMessageViewLogicalIndex.LogicalIndexNumber, baseMessageViewInReverseOrder, base.CategoryCount, base.CategoryHeaderSortOverrides), base.Table);
			indexToUse.UpdateCategoryHeaderView(context, baseMessageViewLogicalIndex, base.SortOrder, base.CategoryCount, base.CategoryHeaderSortOverrides, array);
			return indexToUse;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00048F04 File Offset: 0x00047104
		internal LogicalIndex GetCategoryHeaderViewLogicalIndex(Context context)
		{
			LogicalIndex baseMessageViewLogicalIndex = this.GetBaseMessageViewLogicalIndex(context);
			return this.GetCategoryHeaderViewLogicalIndex(context, baseMessageViewLogicalIndex);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00048F50 File Offset: 0x00047150
		internal IReadOnlyDictionary<Column, Column> GetHeaderRenames()
		{
			Dictionary<Column, Column> dictionary = new Dictionary<Column, Column>(6);
			Column column = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.CategID);
			Column key = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.InstanceId);
			dictionary[key] = column;
			Column column2 = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.InstanceNum);
			dictionary[column2] = Factory.CreateConstantColumn(0, column2);
			Column column3 = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.Fid);
			dictionary[column3] = Factory.CreateConstantColumn(this.Fid.ToLong(), column3);
			Column value = Factory.CreateFunctionColumn("HeaderInstanceKey", typeof(byte[]), PropertyTypeHelper.SizeFromPropType(PropertyType.Binary), PropertyTypeHelper.MaxLengthFromPropType(PropertyType.Binary), base.Table, (object[] argumentColumns) => ExchangeIdHelpers.BuildOursServerEntryId(this.Fid.ToLong(), (long)argumentColumns[0], 0), "ComputeHeaderInstanceKey", new Column[]
			{
				column
			});
			Column key2 = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.InstanceKey);
			Column key3 = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.InstanceKeySvrEid);
			dictionary[key2] = value;
			dictionary[key3] = value;
			if (base.Mailbox.SharedState.SupportsPerUserFeatures)
			{
				dictionary[this.messageTable.VirtualUnreadMessageCount] = Factory.CreateFunctionColumn("PerUserUnreadMessageCount", typeof(long), PropertyTypeHelper.SizeFromPropType(PropertyType.Int64), PropertyTypeHelper.MaxLengthFromPropType(PropertyType.Int64), base.Table, new Func<object[], object>(this.GetCategoryHeaderUnreadCountColumnFunction), "ComputePerUserUnreadMessageCount", new Column[]
				{
					PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.ContentCount),
					PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.UnreadCountInt64),
					PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.CnsetIn)
				});
			}
			else
			{
				dictionary[this.messageTable.VirtualUnreadMessageCount] = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.UnreadCountInt64);
			}
			return dictionary;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00049174 File Offset: 0x00047374
		internal Reader FindMessageDocumentId(Context context, int documentId, IList<Column> columnsToFetch)
		{
			bool flag = false;
			Reader reader = null;
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				base.Mailbox.MailboxPartitionNumber,
				documentId
			});
			TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, base.Table, base.Table.PrimaryKeyIndex, columnsToFetch, null, this.GetLeafRenames(), 0, 0, new KeyRange(startStopKey, startStopKey), false, true);
			try
			{
				reader = tableOperator.ExecuteReader(true);
				tableOperator = null;
				flag = reader.Read();
			}
			finally
			{
				if (tableOperator != null)
				{
					tableOperator.Dispose();
				}
				if (reader != null && !flag)
				{
					reader.Dispose();
					reader = null;
				}
			}
			return reader;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00049224 File Offset: 0x00047424
		internal Reader FindCategoryHeaderRowForMessage(Context context, SearchCriteria findRowCriteria)
		{
			return this.FindCategorizedViewRowForMessage(context, findRowCriteria, true, Bookmark.BOT, false);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00049238 File Offset: 0x00047438
		protected internal override IList<IIndex> GetInScopePseudoIndexes(Context context, SearchCriteria findRowCriteria, out IList<IIndex> masterIndexes)
		{
			masterIndexes = null;
			if (this.IsMailboxScopeView)
			{
				return null;
			}
			if (base.IsCategorizedView)
			{
				return new List<IIndex>(1)
				{
					this.GetCategoryHeaderViewLogicalIndex(context)
				};
			}
			bool flag = this.useLazyIndex;
			if (flag && !base.MvExplosion && !base.IsCategorizedView && !this.useIndexForEmptyFolder && (!this.IsSearchFolder || !this.SearchFolder.IsStaticSearch(context)) && this.GetFolderMessageCount(context) < (long)MessageViewTable.SmallFolderThreshold)
			{
				flag = false;
			}
			if (!flag)
			{
				List<IIndex> list = new List<IIndex>(1);
				if (this.searchFolder != null)
				{
					int value = this.searchFolder.GetLogicalIndexNumber(context).Value;
					LogicalIndex logicalIndex = LogicalIndexCache.GetLogicalIndex(context, base.Mailbox, this.Fid, value);
					list.Add(logicalIndex);
				}
				return list;
			}
			return this.GetViewIndexes(context, findRowCriteria, false);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0004930C File Offset: 0x0004750C
		private IList<IIndex> GetViewIndexes(Context context, SearchCriteria findRowCriteria, bool matchingOnly)
		{
			Column[] nonKeyColumnsForPseudoIndex = this.GetNonKeyColumnsForPseudoIndex(base.ImplicitCriteria, base.RestrictCriteria, findRowCriteria, null);
			SortOrder sortOrder = base.SortOrder;
			if (this.superFilterColumn != null)
			{
				SortOrderBuilder sortOrderBuilder = new SortOrderBuilder
				{
					{
						this.superFilterColumn,
						true
					}
				};
				IList<Column> columns = this.LogicalKeyIndex.Columns;
				for (int i = 0; i < columns.Count; i++)
				{
					sortOrderBuilder.Add(columns[i], true);
				}
				sortOrder = sortOrderBuilder.ToSortOrder();
			}
			return LogicalIndexCache.GetIndexesInScope(context, base.Mailbox, this.Fid, (this.searchFolder != null) ? LogicalIndexType.SearchFolderMessages : LogicalIndexType.Messages, this.messageTable.IsHidden, this.hiddenItemView.Value, sortOrder, nonKeyColumnsForPseudoIndex, null, base.Table, matchingOnly, false);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x000493D9 File Offset: 0x000475D9
		protected override bool IsViewEmpty(Context context)
		{
			return !this.IsMailboxScopeView && (this.UninitializedSearchFolder(context) || (!this.useIndexForEmptyFolder && this.GetFolderMessageCount(context) == 0L));
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00049405 File Offset: 0x00047605
		protected override IReadOnlyDictionary<Column, Column> GetColumnRenames(Context context)
		{
			if (base.IsCategorizedView)
			{
				return null;
			}
			return this.GetLeafRenames();
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00049418 File Offset: 0x00047618
		protected override CategorizedQueryParams GetCategorizedQueryParams(Context context)
		{
			if (!base.IsCategorizedView)
			{
				return null;
			}
			return new CategorizedQueryParams
			{
				HeaderRenameDictionary = this.GetHeaderRenames(),
				LeafRenameDictionary = this.GetLeafRenames(),
				CollapseState = base.CollapseState
			};
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000494D0 File Offset: 0x000476D0
		protected override Reader FindRow(Context context, SearchCriteria findRowCriteria, Bookmark startBookmark, bool backwards)
		{
			SearchCriteria searchCriteria = (base.IsCategorizedView && !this.IsViewEmpty(context) && findRowCriteria != null) ? this.RewriteSearchCriteria(context, findRowCriteria) : null;
			if (searchCriteria != null)
			{
				bool searchHeaderTableOnly = true;
				bool needCriteriaFixup = false;
				searchCriteria.EnumerateColumns(delegate(Column column, object state)
				{
					if (searchHeaderTableOnly && !(column is ConstantColumn) && !this.CategoryHeadersSortOrder.Columns.Contains(column))
					{
						if (column is ExtendedPropertyColumn && ((ExtendedPropertyColumn)column).StorePropTag.PropTag == 1732968468U)
						{
							needCriteriaFixup = true;
							return;
						}
						searchHeaderTableOnly = false;
					}
				}, null);
				if (searchHeaderTableOnly)
				{
					return this.FindCategorizedViewHeaderRow(context, searchCriteria, needCriteriaFixup, startBookmark, backwards);
				}
				if (this.IsFindRowCriteriaForFastFindLeafRow(searchCriteria))
				{
					FaultInjection.InjectFault(MessageViewTable.findCategorizedViewLeafRowTestHook);
					return this.FindCategorizedViewRowForMessage(context, searchCriteria, false, startBookmark, backwards);
				}
				if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug<SearchCriteria>(0L, "FindRow on categorized view: Unable to generate a 'fast' FindRow query plan. Falling back to 'slow' FindRow for criteria [{0}].", findRowCriteria);
				}
			}
			return base.FindRow(context, findRowCriteria, startBookmark, backwards);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000495A0 File Offset: 0x000477A0
		protected override SearchCriteria RewriteSearchCriteria(Context context, SearchCriteria criteria)
		{
			if (this.IsMailboxScopeView)
			{
				criteria = this.ConvertCriteriaEntryIdToDocumentId(context, criteria);
			}
			criteria = base.RewriteSearchCriteria(context, criteria);
			if (criteria == null)
			{
				return null;
			}
			return MessageViewTable.RewriteMessageSearchCriteria(context, base.Mailbox, criteria, base.SortOrder, false, base.MvExplosion, base.IsCategorizedView ? this.Fid : ExchangeId.Null);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00049600 File Offset: 0x00047800
		protected override void BringIndexesToCurrent(Context context, IList<IIndex> indexList, DataAccessOperator queryPlan)
		{
			if (indexList != null)
			{
				long folderMessageCount = this.GetFolderMessageCount(context);
				List<DataAccessOperator> planOperators = new List<DataAccessOperator>();
				queryPlan.GetDescendants(planOperators);
				for (int i = 0; i < indexList.Count; i++)
				{
					LogicalIndex logicalIndex = (LogicalIndex)indexList[i];
					if (MessageViewTable.PlanUsesIndex(context, planOperators, logicalIndex))
					{
						if (this.searchFolder != null)
						{
							int? logicalIndexNumber = this.searchFolder.GetLogicalIndexNumber(context);
							if (logicalIndex.LogicalIndexNumber == logicalIndexNumber)
							{
								if (!logicalIndex.IsStale)
								{
									logicalIndex.UpdateIndex(context, LogicalIndex.CannotRepopulate);
								}
								if (LogicalIndex.IndexUseCallbackTestHook != null)
								{
									LogicalIndex.IndexUseCallbackTestHook(logicalIndex, false, false);
									goto IL_CD;
								}
								goto IL_CD;
							}
						}
						bool arg;
						bool arg2;
						logicalIndex.UpdateIndex(context, new GenerateDataAccessOperatorCallback(this.GenerateDataAccessOperatorForIndexPopulation), folderMessageCount, out arg, out arg2);
						if (LogicalIndex.IndexUseCallbackTestHook != null)
						{
							LogicalIndex.IndexUseCallbackTestHook(logicalIndex, arg, arg2);
						}
					}
					IL_CD:;
				}
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000496EC File Offset: 0x000478EC
		private SearchCriteria CreateCategoryIdCriterion(Context context, LogicalIndex categoryHeaderViewLogicalIndex, ulong categoryIdCounterOnly, SearchCriteriaCompare.SearchRelOp relop)
		{
			Column key = PropertySchema.MapToColumn(context.Database, ObjectType.Message, PropTag.Message.CategID);
			Column argumentColumn = ((ConversionColumn)categoryHeaderViewLogicalIndex.RenameDictionary[key]).ArgumentColumn;
			return Factory.CreateSearchCriteriaCompare(argumentColumn, relop, Factory.CreateConstantColumn((long)categoryIdCounterOnly, argumentColumn));
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00049738 File Offset: 0x00047938
		private SimpleQueryOperator GetFindCategoryHeaderRowOperator(Context context, LogicalIndex categoryHeaderViewLogicalIndex, SearchCriteria findRowCriteria, Bookmark startBookmark, bool backwards)
		{
			Column item = PropertySchema.MapToColumn(context.Database, ObjectType.Message, PropTag.Message.CategID);
			Column item2 = PropertySchema.MapToColumn(context.Database, ObjectType.Message, PropTag.Message.ContentCount);
			IList<Column> list = new List<Column>(base.CategoryHeadersSortOrder.Columns);
			list.Add(item);
			list.Add(item2);
			if (!startBookmark.IsBOT && !startBookmark.IsEOT)
			{
				List<object> list2 = new List<object>(categoryHeaderViewLogicalIndex.SortOrder.Count);
				list2.Add(categoryHeaderViewLogicalIndex.MailboxPartitionNumber);
				list2.Add(categoryHeaderViewLogicalIndex.LogicalIndexNumber);
				for (int i = 0; i < base.CategoryHeadersSortOrder.Count; i++)
				{
					list2.Add(startBookmark.KeyValues[i]);
				}
				startBookmark = new Bookmark(list2, base.IsHeaderRowBookmark(startBookmark) && startBookmark.PositionedOn);
			}
			SearchCriteria searchCriteria = Factory.CreateSearchCriteriaCompare(categoryHeaderViewLogicalIndex.IndexTable.Columns[0], SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(categoryHeaderViewLogicalIndex.MailboxPartitionNumber, categoryHeaderViewLogicalIndex.IndexTable.Columns[0]));
			SearchCriteria searchCriteria2 = Factory.CreateSearchCriteriaCompare(categoryHeaderViewLogicalIndex.IndexTable.Columns[1], SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(categoryHeaderViewLogicalIndex.LogicalIndexNumber, categoryHeaderViewLogicalIndex.IndexTable.Columns[1]));
			QueryPlanner queryPlanner = new QueryPlanner(context, categoryHeaderViewLogicalIndex.IndexTable, null, Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
			{
				searchCriteria,
				searchCriteria2
			}), null, findRowCriteria, list, null, categoryHeaderViewLogicalIndex.RenameDictionary, null, null, categoryHeaderViewLogicalIndex.SortOrder, startBookmark, 0, 0, backwards, false, false, true, false, QueryPlanner.Hints.Empty);
			int arg;
			int arg2;
			SimpleQueryOperator simpleQueryOperator = queryPlanner.CreatePlan(out arg, out arg2);
			if (MessageViewTable.categorizedViewFastFindRowOperatorTestHook.Value != null)
			{
				MessageViewTable.categorizedViewFastFindRowOperatorTestHook.Value(true, simpleQueryOperator, arg, arg2);
			}
			return simpleQueryOperator;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0004991C File Offset: 0x00047B1C
		private IReadOnlyDictionary<Column, Column> GetLeafRenames()
		{
			Dictionary<Column, Column> dictionary = new Dictionary<Column, Column>(5);
			if (base.IsCategorizedView)
			{
				Column column = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.ContentCount);
				dictionary[column] = Factory.CreateConstantColumn(0, column);
				Column column2 = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.UnreadCountInt64);
				dictionary[column2] = Factory.CreateConstantColumn(0L, column2);
				dictionary[this.messageTable.VirtualUnreadMessageCount] = Factory.CreateConstantColumn(0L, this.messageTable.VirtualUnreadMessageCount);
			}
			else
			{
				dictionary[this.messageTable.VirtualUnreadMessageCount] = Factory.CreateConstantColumn(null, this.messageTable.VirtualUnreadMessageCount);
			}
			if (base.Mailbox.SharedState.SupportsPerUserFeatures)
			{
				dictionary[this.messageTable.VirtualIsRead] = Factory.CreateFunctionColumn("PerUserIsRead", typeof(bool), PropertyTypeHelper.SizeFromPropType(PropertyType.Boolean), PropertyTypeHelper.MaxLengthFromPropType(PropertyType.Boolean), base.Table, new Func<object[], object>(this.GetPerUserReadUnreadColumnFunction), "ComputePerUserIsRead", new Column[]
				{
					this.messageTable.FolderId,
					this.messageTable.IsRead,
					this.messageTable.LcnCurrent
				});
			}
			else
			{
				dictionary[this.messageTable.VirtualIsRead] = this.messageTable.IsRead;
			}
			dictionary[this.messageTable.VirtualParentDisplay] = TopMessage.CreateVirtualParentDisplayFunctionColumn(this.messageTable, new Func<object[], object>(this.GetParentDisplayColumnFunction));
			return dictionary;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00049AB0 File Offset: 0x00047CB0
		private object GetPerUserReadUnreadColumnFunction(object[] columnValues)
		{
			Context currentOperationContext = base.Mailbox.CurrentOperationContext;
			Folder folder;
			if (this.searchFolder != null || this.folder == null)
			{
				ExchangeId exchangeId = ExchangeId.CreateFrom26ByteArray(currentOperationContext, base.Mailbox.ReplidGuidMap, (byte[])columnValues[0]);
				if (exchangeId == ExchangeId.Zero)
				{
					return ((bool)columnValues[1]).GetBoxed();
				}
				folder = Folder.OpenFolder(currentOperationContext, base.Mailbox, exchangeId);
				if (folder == null)
				{
					return ((bool)columnValues[1]).GetBoxed();
				}
			}
			else
			{
				folder = this.folder;
			}
			ExchangeId changeNumber = ExchangeId.CreateFrom26ByteArray(currentOperationContext, base.Mailbox.ReplidGuidMap, (byte[])columnValues[2]);
			return TopMessage.GetPerUserReadUnreadColumnFunction(currentOperationContext, folder, (bool)columnValues[1], false, changeNumber).GetBoxed();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00049B64 File Offset: 0x00047D64
		private object GetCategoryHeaderUnreadCountColumnFunction(object[] columnValues)
		{
			Context currentOperationContext = base.Mailbox.CurrentOperationContext;
			if (!base.IsCategorizedView)
			{
				return columnValues[1];
			}
			if (this.searchFolder != null || this.folder == null)
			{
				return columnValues[1];
			}
			if (columnValues[2] == null)
			{
				return columnValues[1];
			}
			if (!this.folder.IsPerUserReadUnreadTrackingEnabled)
			{
				return columnValues[1];
			}
			if (currentOperationContext.UserIdentity == Guid.Empty)
			{
				return columnValues[1];
			}
			PerUser perUser = PerUser.LoadResident(currentOperationContext, base.Mailbox, currentOperationContext.UserIdentity, this.fid);
			if (perUser == null)
			{
				return (long)((int)columnValues[0]);
			}
			IdSet second = IdSet.Parse(currentOperationContext, (byte[])columnValues[2]);
			object result;
			using (LockManager.Lock(perUser, LockManager.LockType.PerUserShared, currentOperationContext.Diagnostics))
			{
				long countIds = (long)IdSet.Intersect(perUser.CNSet, second).CountIds;
				result = (long)((int)columnValues[0]) - countIds;
			}
			return result;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00049C60 File Offset: 0x00047E60
		private Reader FindCategorizedViewHeaderRow(Context context, SearchCriteria findRowCriteria, bool needCriteriaFixup, Bookmark startBookmark, bool backwards)
		{
			FaultInjection.InjectFault(MessageViewTable.findCategorizedViewHeaderRowTestHook);
			LogicalIndex categoryHeaderViewLogicalIndex = this.GetCategoryHeaderViewLogicalIndex(context);
			if (needCriteriaFixup)
			{
				findRowCriteria = this.RewriteSearchCriteriaForFindCategorizedViewHeaderRow(context, categoryHeaderViewLogicalIndex, findRowCriteria);
			}
			if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug<SearchCriteria>(0L, "FindRow on categorized view: Executing 'fast' FindRow on Header table for criteria [{0}].", findRowCriteria);
			}
			using (SimpleQueryOperator findCategoryHeaderRowOperator = this.GetFindCategoryHeaderRowOperator(context, categoryHeaderViewLogicalIndex, findRowCriteria, startBookmark, backwards))
			{
				if (findCategoryHeaderRowOperator != null)
				{
					using (Reader reader = findCategoryHeaderRowOperator.ExecuteReader(false))
					{
						while (reader.Read())
						{
							if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug(0L, "FindRow on Header table for categorized view: Succeeded.");
							}
							this.ConstructCategorizedViewBookmarkFromHeaderRow(reader, true);
							Reader findRowReaderForCategorizedView = this.GetFindRowReaderForCategorizedView(context, true, startBookmark, backwards);
							if (findRowReaderForCategorizedView != null)
							{
								return findRowReaderForCategorizedView;
							}
							if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug(0L, "Couldn't open categorized view row reader for found row (it must have been non-visible). Moving to next message in Header table.");
							}
						}
						if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug(0L, "FindRow on Header table for categorized view: Failed (no matches in Header table).");
						}
						goto IL_106;
					}
				}
				if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug(0L, "FindRow on Header table for categorized view: Failed (no operator).");
				}
				IL_106:;
			}
			return null;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00049E34 File Offset: 0x00048034
		private SearchCriteria RewriteSearchCriteriaForFindCategorizedViewHeaderRow(Context context, LogicalIndex categoryHeaderViewLogicalIndex, SearchCriteria findRowCriteria)
		{
			return findRowCriteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				SearchCriteriaCompare searchCriteriaCompare = criterion as SearchCriteriaCompare;
				if (searchCriteriaCompare != null && searchCriteriaCompare.Lhs is ExtendedPropertyColumn && searchCriteriaCompare.Rhs is ConstantColumn && ((ExtendedPropertyColumn)searchCriteriaCompare.Lhs).StorePropTag.PropTag == 1732968468U)
				{
					return this.CreateCategoryIdCriterion(context, categoryHeaderViewLogicalIndex, ExchangeIdHelpers.CounterFromLong((long)((ConstantColumn)searchCriteriaCompare.Rhs).Value), searchCriteriaCompare.RelOp);
				}
				return criterion;
			}, (context.Culture == null) ? null : context.Culture.CompareInfo, false);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00049E94 File Offset: 0x00048094
		private void ConstructCategorizedViewBookmarkFromHeaderRow(Reader reader, bool positionedOn)
		{
			List<object> list = new List<object>(base.CategoryHeadersSortOrder.Count + base.SortOrder.Count);
			for (int i = 0; i < base.CategoryHeadersSortOrder.Count; i++)
			{
				list.Add(reader.GetValue(base.CategoryHeadersSortOrder.Columns[i]));
			}
			for (int j = 0; j < base.SortOrder.Count; j++)
			{
				list.Add(null);
			}
			base.BookmarkRow(list, positionedOn);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00049F2C File Offset: 0x0004812C
		private Reader GetFindRowReaderForCategorizedView(Context context, bool isHeaderRow, Bookmark startBookmark, bool backwards)
		{
			SearchCriteria bookmarkCriteria = QueryPlanner.GetBookmarkCriteria(base.GetSortOrderForView(), backwards, startBookmark);
			Reader reader = this.QueryRows(context, 1, false);
			Reader result = null;
			if (reader != null)
			{
				try
				{
					if (reader.Read() && base.CheckBookmarkMatchesRow(context, reader, base.Bookmark, isHeaderRow) && (bookmarkCriteria == null || bookmarkCriteria.Evaluate(reader, context.Culture.CompareInfo)))
					{
						base.BookmarkCurrentRow(reader, true);
						result = reader;
						reader = null;
					}
				}
				finally
				{
					if (reader != null)
					{
						reader.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00049FB0 File Offset: 0x000481B0
		private Reader FindCategorizedViewRowForMessage(Context context, SearchCriteria findRowCriteria, bool headerRow, Bookmark startBookmark, bool backwards)
		{
			if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug<SearchCriteria>(0L, "FindRow on categorized view: Executing 'fast' FindRow on Message table for criteria [{0}].", findRowCriteria);
			}
			if ((startBookmark.IsBOT && backwards) || (startBookmark.IsEOT && !backwards))
			{
				if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug(0L, "FindRow on categorized view: FindRow trivially not found - searching backwards from BOT or forwards from EOT.");
				}
				return null;
			}
			bool flag = this.SearchFolder != null;
			IList<IIndex> list = new List<IIndex>(flag ? 2 : 1);
			list.Add(this.GetBaseMessageViewLogicalIndex(context));
			bool mustUseLazyIndex = false;
			if (flag)
			{
				IIndex baseViewLogicalIndex = this.SearchFolder.GetBaseViewLogicalIndex(context, true);
				if (baseViewLogicalIndex == null)
				{
					if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug<ExchangeId>(0L, "FindRow on categorized view: Failed (base view logical index for search folder {0} is missing or stale).", this.SearchFolder.GetId(context));
					}
					return null;
				}
				mustUseLazyIndex = true;
				if (!base.MvExplosion)
				{
					list.Add(baseViewLogicalIndex);
				}
			}
			else if (base.MvExplosion)
			{
				mustUseLazyIndex = true;
			}
			QueryPlanner queryPlanner = new QueryPlanner(context, base.Table, null, base.ImplicitCriteria, base.RestrictCriteria, findRowCriteria, base.SortOrder.Columns, null, this.GetLeafRenames(), null, list, SortOrder.Empty, Bookmark.BOT, 0, 0, false, mustUseLazyIndex, false, true, false, QueryPlanner.Hints.Empty);
			int arg;
			int arg2;
			using (SimpleQueryOperator simpleQueryOperator = queryPlanner.CreatePlan(out arg, out arg2))
			{
				if (MessageViewTable.categorizedViewFastFindRowOperatorTestHook.Value != null)
				{
					MessageViewTable.categorizedViewFastFindRowOperatorTestHook.Value(false, simpleQueryOperator, arg, arg2);
				}
				if (simpleQueryOperator != null)
				{
					using (Reader reader = simpleQueryOperator.ExecuteReader(false))
					{
						while (reader.Read())
						{
							if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug(0L, "FindRow on base message view for categorized view: Succeeded.");
							}
							this.ConstructCategorizedViewBookmarkFromMessage(context, reader, true, headerRow);
							Reader findRowReaderForCategorizedView = this.GetFindRowReaderForCategorizedView(context, headerRow, startBookmark, backwards);
							if (findRowReaderForCategorizedView != null)
							{
								return findRowReaderForCategorizedView;
							}
							if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug(0L, "Couldn't open categorized view row reader for found row (it must have been non-visible). Moving to next message in base message view.");
							}
						}
						if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug(0L, "FindRow on Message table for categorized view: Failed (no matches in base message view).");
						}
						goto IL_220;
					}
				}
				if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.ViewTableFindRowTracer.TraceDebug(0L, "FindRow on Message table for categorized view: Failed (no operator).");
				}
				IL_220:;
			}
			return null;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0004A20C File Offset: 0x0004840C
		private void ConstructCategorizedViewBookmarkFromMessage(Context context, Reader reader, bool positionedOn, bool headerBookmark)
		{
			List<object> list = new List<object>(base.CategoryHeadersSortOrder.Count + base.SortOrder.Count);
			List<object> list2 = new List<object>(base.SortOrder.Count);
			int num = CategoryHeaderSortOverride.NumberOfOverrides(base.CategoryHeaderSortOverrides);
			LogicalIndex logicalIndex = (num > 0) ? this.GetBaseMessageViewLogicalIndex(context) : null;
			foreach (Column column in base.SortOrder.Columns)
			{
				PropertyType propType = PropertyTypeHelper.PropTypeFromClrType(column.Type);
				object value = reader.GetValue(column);
				list2.Add(LogicalIndex.TruncateValueAsNecessary(propType, value));
			}
			for (int i = 0; i < base.CategoryCount; i++)
			{
				bool flag = i > 0 && !base.SortOrder.Ascending[i];
				if (flag)
				{
					list.Add(null);
				}
				if (base.CategoryHeaderSortOverrides[i] != null)
				{
					if (!flag && i > 0 && !base.CategoryHeaderSortOverrides[i].Ascending)
					{
						list.Add(null);
					}
					object columnValue = logicalIndex.ComputeAggregationWinner(context, i, list2, base.CategoryHeaderSortOverrides[i]);
					PropertyType propType2 = PropertyTypeHelper.PropTypeFromClrType(base.CategoryHeaderSortOverrides[i].Column.Type);
					list.Add(LogicalIndex.TruncateValueAsNecessary(propType2, columnValue));
				}
				list.Add(list2[i]);
			}
			if (!headerBookmark)
			{
				list.Add(base.CategoryCount);
				list.AddRange(list2);
			}
			else
			{
				list.Add(0);
				for (int j = 0; j < base.SortOrder.Count; j++)
				{
					list.Add(null);
				}
			}
			base.BookmarkRow(list, positionedOn);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0004A420 File Offset: 0x00048620
		private bool IsFindRowCriteriaForFastFindLeafRow(SearchCriteria findRowCriteria)
		{
			if (this.FindRowCriteriaMatchesSortOrder(findRowCriteria))
			{
				return true;
			}
			if (base.MvExplosion)
			{
				return false;
			}
			bool flag = false;
			bool sawFid = false;
			bool flag2 = false;
			base.ImplicitCriteria.EnumerateColumns(delegate(Column column, object state)
			{
				if (column == this.messageTable.FolderId)
				{
					sawFid = true;
				}
			}, null);
			SearchCriteria[] array = (findRowCriteria is SearchCriteriaAnd) ? ((SearchCriteriaAnd)findRowCriteria).NestedCriteria : new SearchCriteria[]
			{
				findRowCriteria
			};
			foreach (SearchCriteria searchCriterion in array)
			{
				Column col = this.CheckForCompareCriterionForFastFindLeafRow(searchCriterion);
				if (col == this.messageTable.MessageDocumentId)
				{
					flag = true;
				}
				else if (col == this.messageTable.FolderId)
				{
					sawFid = true;
				}
				else if (col == this.messageTable.MessageId)
				{
					flag2 = true;
				}
				else if (col != this.messageTable.IsHidden)
				{
					return false;
				}
			}
			return (sawFid && flag2) || flag;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0004A53C File Offset: 0x0004873C
		private bool FindRowCriteriaMatchesSortOrder(SearchCriteria findRowCriteria)
		{
			SearchCriteria[] array = (findRowCriteria is SearchCriteriaAnd) ? ((SearchCriteriaAnd)findRowCriteria).NestedCriteria : new SearchCriteria[]
			{
				findRowCriteria
			};
			bool flag = array != null && array.Length == base.SortOrder.Count;
			int num = 0;
			while (flag && num < array.Length)
			{
				SearchCriteriaCompare searchCriteriaCompare = array[num] as SearchCriteriaCompare;
				flag = (searchCriteriaCompare != null && searchCriteriaCompare.Rhs is ConstantColumn && searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal && searchCriteriaCompare.Lhs == base.SortOrder.Columns[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0004A5E0 File Offset: 0x000487E0
		private Column CheckForCompareCriterionForFastFindLeafRow(SearchCriteria searchCriterion)
		{
			SearchCriteriaCompare searchCriteriaCompare = searchCriterion as SearchCriteriaCompare;
			if (searchCriteriaCompare != null && searchCriteriaCompare.Rhs is ConstantColumn && searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal)
			{
				Column column = searchCriteriaCompare.Lhs;
				if (column is ExtendedPropertyColumn)
				{
					StorePropTag storePropTag = ((ExtendedPropertyColumn)searchCriteriaCompare.Lhs).StorePropTag;
					if (storePropTag.PropTag == 1746206723U)
					{
						column = this.messageTable.MessageDocumentId;
					}
					else if (storePropTag.PropTag == 1739194379U)
					{
						column = this.messageTable.IsHidden;
					}
				}
				return column;
			}
			return null;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0004A668 File Offset: 0x00048868
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

		// Token: 0x0600086B RID: 2155 RVA: 0x0004A6E0 File Offset: 0x000488E0
		private Column[] GetNonKeyColumnsForPseudoIndex(SearchCriteria implicitCriteria, SearchCriteria restrictCriteria, SearchCriteria findRowCriteria, IList<Column> explicitCoveringColumns)
		{
			HashSet<Column> hashSet = null;
			if (this.useCoveringIndex)
			{
				hashSet = ((base.ViewColumns != null) ? new HashSet<Column>(base.ViewColumns) : new HashSet<Column>());
				Action<Column, object> callback = delegate(Column c, object collection)
				{
					((ICollection<Column>)collection).Add(c);
				};
				if (implicitCriteria != null)
				{
					implicitCriteria.EnumerateColumns(callback, hashSet);
				}
				if (restrictCriteria != null)
				{
					restrictCriteria.EnumerateColumns(callback, hashSet);
				}
				if (findRowCriteria != null)
				{
					findRowCriteria.EnumerateColumns(callback, hashSet);
				}
				if (explicitCoveringColumns == null)
				{
					goto IL_AA;
				}
				using (IEnumerator<Column> enumerator = explicitCoveringColumns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Column item = enumerator.Current;
						((ICollection<Column>)hashSet).Add(item);
					}
					goto IL_AA;
				}
			}
			if (explicitCoveringColumns != null)
			{
				hashSet = new HashSet<Column>(explicitCoveringColumns);
			}
			IL_AA:
			Column[] result;
			if (hashSet != null)
			{
				if (base.Mailbox.SharedState.SupportsPerUserFeatures)
				{
					Column item2 = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.Read);
					Column item3 = PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.MessageFlags);
					if (hashSet.Contains(item2))
					{
						hashSet.Remove(item2);
						hashSet.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.FidBin));
						hashSet.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.ChangeNumber));
					}
					if (hashSet.Contains(item3))
					{
						hashSet.Remove(item3);
						hashSet.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.FidBin));
						hashSet.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.ChangeNumber));
						hashSet.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.HasAttach));
						hashSet.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.Associated));
						hashSet.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.MessageFlagsActual));
						hashSet.Add(PropertySchema.MapToColumn(base.Mailbox.Database, ObjectType.Message, PropTag.Message.MailFlags));
					}
				}
				hashSet.ExceptWith(base.SortOrder.Columns);
				hashSet.RemoveWhere((Column c) => c.ActualColumn is ConstantColumn);
				result = hashSet.ToArray<Column>();
			}
			else
			{
				result = Array<Column>.Empty;
			}
			return result;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0004AB6C File Offset: 0x00048D6C
		private SearchCriteria ConvertCriteriaEntryIdToDocumentId(Context context, SearchCriteria criteria)
		{
			List<KeyRange> keyRanges = null;
			criteria = criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				SearchCriteriaCompare searchCriteriaCompare = criterion as SearchCriteriaCompare;
				if (searchCriteriaCompare != null && searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal && searchCriteriaCompare.Lhs is ExtendedPropertyColumn && searchCriteriaCompare.Rhs is ConstantColumn && ((ExtendedPropertyColumn)searchCriteriaCompare.Lhs).StorePropTag.PropTag == 268370171U)
				{
					byte[] entryId = ((ConstantColumn)searchCriteriaCompare.Rhs).Value as byte[];
					ExchangeId exchangeId;
					ExchangeId exchangeId2;
					int num;
					if (EntryIdHelpers.ParseServerEntryId(context, this.Mailbox.ReplidGuidMap, entryId, false, out exchangeId, out exchangeId2, out num))
					{
						if (keyRanges == null)
						{
							keyRanges = new List<KeyRange>();
						}
						StartStopKey startStopKey = new StartStopKey(true, new object[]
						{
							this.Mailbox.MailboxPartitionNumber,
							exchangeId.To26ByteArray(),
							false,
							exchangeId2.To26ByteArray()
						});
						keyRanges.Add(new KeyRange(startStopKey, startStopKey));
					}
				}
				return criterion;
			}, (context.Culture == null) ? null : context.Culture.CompareInfo, false);
			Dictionary<FidMid, int> map = null;
			if (keyRanges != null)
			{
				ReplidGuidMap replidGuidMap = base.Mailbox.ReplidGuidMap;
				TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, this.messageTable.Table, this.messageTable.MessageUnique, new Column[]
				{
					this.messageTable.FolderId,
					this.messageTable.MessageId,
					this.messageTable.MessageDocumentId
				}, null, null, null, 0, 0, keyRanges, false, false, true);
				using (Reader reader = tableOperator.ExecuteReader(true))
				{
					while (reader.Read())
					{
						ExchangeId folderId = ExchangeId.CreateFrom26ByteArray(context, replidGuidMap, reader.GetBinary(this.messageTable.FolderId));
						ExchangeId messageId = ExchangeId.CreateFrom26ByteArray(context, replidGuidMap, reader.GetBinary(this.messageTable.MessageId));
						int @int = reader.GetInt32(this.messageTable.MessageDocumentId);
						if (map == null)
						{
							map = new Dictionary<FidMid, int>();
						}
						map.Add(new FidMid(folderId, messageId), @int);
					}
				}
			}
			criteria = criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
			{
				SearchCriteriaCompare searchCriteriaCompare = criterion as SearchCriteriaCompare;
				if (searchCriteriaCompare != null && searchCriteriaCompare.RelOp == SearchCriteriaCompare.SearchRelOp.Equal && searchCriteriaCompare.Lhs is ExtendedPropertyColumn && searchCriteriaCompare.Rhs is ConstantColumn && ((ExtendedPropertyColumn)searchCriteriaCompare.Lhs).StorePropTag.PropTag == 268370171U)
				{
					byte[] entryId = ((ConstantColumn)searchCriteriaCompare.Rhs).Value as byte[];
					ExchangeId folderId2;
					ExchangeId messageId2;
					int num;
					if (EntryIdHelpers.ParseServerEntryId(context, this.Mailbox.ReplidGuidMap, entryId, false, out folderId2, out messageId2, out num))
					{
						int num2;
						if (map == null || !map.TryGetValue(new FidMid(folderId2, messageId2), out num2))
						{
							return Factory.CreateSearchCriteriaFalse();
						}
						return Factory.CreateSearchCriteriaCompare(this.messageTable.MessageDocumentId, searchCriteriaCompare.RelOp, Factory.CreateConstantColumn(num2, this.messageTable.MessageDocumentId));
					}
				}
				return criterion;
			}, (context.Culture == null) ? null : context.Culture.CompareInfo, false);
			return criteria;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0004AD4C File Offset: 0x00048F4C
		private bool UninitializedSearchFolder(Context context)
		{
			return this.searchFolder != null && (this.searchFolder.IsDead || this.searchFolder.GetLogicalIndexNumber(context) == null);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0004AD8C File Offset: 0x00048F8C
		private long GetFolderMessageCount(Context context)
		{
			if (this.folder == null)
			{
				return 0L;
			}
			if (this.hiddenItemView == null)
			{
				return this.folder.GetMessageCount(context) + this.folder.GetHiddenItemCount(context);
			}
			if (!this.hiddenItemView.Value)
			{
				return this.folder.GetMessageCount(context);
			}
			return this.folder.GetHiddenItemCount(context);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0004ADF8 File Offset: 0x00048FF8
		private void SetDefaultSortOrder()
		{
			SortOrderBuilder sortOrderBuilder = new SortOrderBuilder
			{
				{
					this.messageTable.DateReceived,
					false
				}
			};
			this.SortTable(sortOrderBuilder.ToSortOrder());
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0004AE2C File Offset: 0x0004902C
		private bool SkipPropertiesPromotionValidation(Context context, ViewSetColumnsFlag flags)
		{
			return (flags & ViewSetColumnsFlag.NoColumnValidation) == ViewSetColumnsFlag.NoColumnValidation || (this.folder == null || this.searchFolder != null) || (this.hiddenItemView != null && this.hiddenItemView.Value) || ((!context.TestCaseId.IsNotNull || !MessageViewTable.viewValidationTestCases.Contains(context.TestCaseId)) && ViewTable.ClientTypeExcludedFromDefaultPromotedValidation(context.ClientType));
		}

		// Token: 0x04000412 RID: 1042
		public const int MaxNumberOfPropertiesToPromote = 50;

		// Token: 0x04000413 RID: 1043
		private const int MagicCollapseState = 173302083;

		// Token: 0x04000414 RID: 1044
		private static readonly TestCaseId[] viewValidationTestCases = new TestCaseId[0];

		// Token: 0x04000415 RID: 1045
		private static Hookable<int> smallFolderThreshold = Hookable<int>.Create(false, 7);

		// Token: 0x04000416 RID: 1046
		private static Hookable<Action> findCategorizedViewHeaderRowTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x04000417 RID: 1047
		private static Hookable<Action> findCategorizedViewLeafRowTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x04000418 RID: 1048
		private static Hookable<Action<bool, SimpleQueryOperator, int, int>> categorizedViewFastFindRowOperatorTestHook = Hookable<Action<bool, SimpleQueryOperator, int, int>>.Create(true, null);

		// Token: 0x04000419 RID: 1049
		private readonly ExchangeId fid;

		// Token: 0x0400041A RID: 1050
		private readonly Folder folder;

		// Token: 0x0400041B RID: 1051
		private readonly SearchFolder searchFolder;

		// Token: 0x0400041C RID: 1052
		private readonly bool? hiddenItemView;

		// Token: 0x0400041D RID: 1053
		private readonly MessageTable messageTable;

		// Token: 0x0400041E RID: 1054
		private readonly bool useLazyIndex;

		// Token: 0x0400041F RID: 1055
		private readonly bool useCoveringIndex;

		// Token: 0x04000420 RID: 1056
		private readonly bool useIndexForEmptyFolder;

		// Token: 0x04000421 RID: 1057
		private List<Column> longValueColumnsToPreread;

		// Token: 0x04000422 RID: 1058
		private Column superFilterColumn;

		// Token: 0x04000423 RID: 1059
		private Dictionary<Column, FilterFactorHint> filterFactorHints;
	}
}
