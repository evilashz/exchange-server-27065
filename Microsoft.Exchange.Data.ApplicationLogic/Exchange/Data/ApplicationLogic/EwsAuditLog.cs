using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200009D RID: 157
	internal class EwsAuditLog : IAuditLog
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x0001868F File Offset: 0x0001688F
		public EwsAuditLog(EwsAuditClient ewsClient, FolderIdType auditFolderId, DateTime logRangeStart, DateTime logRangeEnd)
		{
			this.ewsClient = ewsClient;
			this.auditFolderId = auditFolderId;
			this.EstimatedLogStartTime = logRangeStart;
			this.EstimatedLogEndTime = logRangeEnd;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x000186B4 File Offset: 0x000168B4
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x000186BC File Offset: 0x000168BC
		public DateTime EstimatedLogStartTime { get; private set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x000186C5 File Offset: 0x000168C5
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x000186CD File Offset: 0x000168CD
		public DateTime EstimatedLogEndTime { get; private set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000186D6 File Offset: 0x000168D6
		public bool IsAsynchronous
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000186DC File Offset: 0x000168DC
		public IAuditQueryContext<TFilter> CreateAuditQueryContext<TFilter>()
		{
			if (typeof(TFilter) != typeof(RestrictionType) && typeof(TFilter) != typeof(QueryStringType))
			{
				throw new NotSupportedException();
			}
			return (IAuditQueryContext<TFilter>)new EwsAuditLog.EwsAuditLogQueryContext(this);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00018730 File Offset: 0x00016930
		public int WriteAuditRecord(IAuditLogRecord auditRecord)
		{
			this.ewsClient.RefreshUrl(false);
			int result = 0;
			CreateItemType createItemType = EwsAuditLog.GetCreateItemType(auditRecord, this.auditFolderId, out result);
			this.ewsClient.CreateItem(createItemType);
			return result;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00018768 File Offset: 0x00016968
		private static CreateItemType GetCreateItemType(IAuditLogRecord auditRecord, BaseFolderIdType targetFolderId, out int recordSize)
		{
			string asString = AuditLogParseSerialize.GetAsString(auditRecord);
			string text = string.Format("{0} : {1}", auditRecord.UserId, auditRecord.Operation);
			string text2 = string.Format("{0}{1}", auditRecord.UserId, "audit");
			string text3 = string.Format("{0}{1}", auditRecord.ObjectId, "audit");
			recordSize = Encoding.Unicode.GetByteCount(text) + Encoding.Unicode.GetByteCount(asString) + Encoding.Unicode.GetByteCount(text2) + Encoding.Unicode.GetByteCount(text3);
			return new CreateItemType
			{
				MessageDisposition = MessageDispositionType.SaveOnly,
				MessageDispositionSpecified = true,
				SavedItemFolderId = new TargetFolderIdType
				{
					Item = targetFolderId
				},
				Items = new NonEmptyArrayOfAllItemsType
				{
					Items = new ItemType[]
					{
						new MessageType
						{
							ItemClass = "IPM.AuditLog",
							Subject = text,
							Body = new BodyType
							{
								Value = asString,
								BodyType1 = BodyTypeType.Text
							},
							From = new SingleRecipientType
							{
								Item = new EmailAddressType
								{
									Name = text2
								}
							},
							ToRecipients = new EmailAddressType[]
							{
								new EmailAddressType
								{
									Name = text3
								}
							}
						}
					}
				}
			};
		}

		// Token: 0x040002FE RID: 766
		private EwsAuditClient ewsClient;

		// Token: 0x040002FF RID: 767
		private FolderIdType auditFolderId;

		// Token: 0x0200009E RID: 158
		private class EwsAuditLogQueryContext : DisposableObject, IAuditQueryContext<RestrictionType>, IAuditQueryContext<QueryStringType>, IDisposable
		{
			// Token: 0x060006C5 RID: 1733 RVA: 0x000188D5 File Offset: 0x00016AD5
			public EwsAuditLogQueryContext(EwsAuditLog auditLog)
			{
				this.auditLog = auditLog;
				this.pendingAsyncResult = null;
			}

			// Token: 0x060006C6 RID: 1734 RVA: 0x000188EB File Offset: 0x00016AEB
			public IAsyncResult BeginAuditLogQuery(RestrictionType queryFilter, int maximumResultsCount)
			{
				if (this.pendingAsyncResult != null)
				{
					throw new InvalidOperationException("Asynchronous query is already pending.");
				}
				this.pendingAsyncResult = new CompletedAsyncResult();
				this.queryFilter = queryFilter;
				return this.pendingAsyncResult;
			}

			// Token: 0x060006C7 RID: 1735 RVA: 0x00018918 File Offset: 0x00016B18
			public IAsyncResult BeginAuditLogQuery(QueryStringType queryString, int maximumResultsCount)
			{
				if (this.pendingAsyncResult != null)
				{
					throw new InvalidOperationException("Asynchronous query is already pending.");
				}
				this.pendingAsyncResult = new CompletedAsyncResult();
				this.queryString = queryString;
				return this.pendingAsyncResult;
			}

			// Token: 0x060006C8 RID: 1736 RVA: 0x00018B30 File Offset: 0x00016D30
			public IEnumerable<T> EndAuditLogQuery<T>(IAsyncResult asyncResult, IAuditQueryStrategy<T> queryStrategy)
			{
				foreach (ItemType item in this.FindItemsPaged())
				{
					EwsAuditLog.EwsItemPropertyBag itemAsPropertyBag = new EwsAuditLog.EwsItemPropertyBag(item);
					bool stopNow;
					bool match = queryStrategy.RecordFilter(itemAsPropertyBag, out stopNow);
					if (stopNow)
					{
						break;
					}
					if (match)
					{
						yield return queryStrategy.Convert(itemAsPropertyBag);
					}
				}
				yield break;
			}

			// Token: 0x060006C9 RID: 1737 RVA: 0x00018F74 File Offset: 0x00017174
			private IEnumerable<ItemType> FindItemsPaged()
			{
				QueryStringType searchQueryStringToUse = this.queryString;
				bool keepLooking;
				do
				{
					keepLooking = false;
					ItemType[] items;
					if (this.queryString != null)
					{
						items = this.auditLog.ewsClient.FindItemsWithFAST(this.auditLog.auditFolderId, null, EwsAuditLog.EwsAuditLogQueryContext.SortByReceivedTime, searchQueryStringToUse);
					}
					else
					{
						items = this.auditLog.ewsClient.FindItems(this.auditLog.auditFolderId, null, EwsAuditLog.EwsAuditLogQueryContext.SortByReceivedTime, this.queryFilter);
					}
					DateTime? earliestReceivedTime = null;
					bool useLessOrEqualFilter = false;
					if (this.IsTraceEnabled(TraceType.DebugTrace))
					{
						this.Tracer.TraceDebug<int>(0L, "Query results returned: {0}.", items.Length);
					}
					if (EwsAuditLog.EwsAuditLogQueryContext.IsSearchResultLimitedByFast(this.queryString, items))
					{
						if (this.IsTraceEnabled(TraceType.DebugTrace))
						{
							this.Tracer.TraceDebug(0L, "Query results may be limited by FAST.");
						}
						if (items[items.Length - 1].DateTimeReceivedSpecified)
						{
							earliestReceivedTime = new DateTime?(items[items.Length - 1].DateTimeReceived);
							useLessOrEqualFilter = (items[0].DateTimeReceived != earliestReceivedTime.Value);
							keepLooking = true;
							if (this.IsTraceEnabled(TraceType.DebugTrace))
							{
								this.Tracer.TraceDebug<DateTime?, bool>(0L, "Earliest item seen=[{0}] useLessOrEqualFilter=[{1}].", earliestReceivedTime, useLessOrEqualFilter);
							}
						}
					}
					foreach (ItemType item in items)
					{
						yield return item;
					}
					if (keepLooking)
					{
						searchQueryStringToUse = null;
						if (this.queryString != null && this.queryString.Value != null && earliestReceivedTime != null)
						{
							ExDateTime exDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, earliestReceivedTime.Value);
							StringBuilder stringBuilder = new StringBuilder(this.queryString.Value);
							AqsQueryBuilder.AppendDateClause(stringBuilder, PropertyKeyword.Received, useLessOrEqualFilter ? DateRangeQueryOperation.LessThanOrEqual : DateRangeQueryOperation.LessThan, exDateTime.UniversalTime);
							searchQueryStringToUse = new QueryStringType
							{
								Value = stringBuilder.ToString()
							};
						}
						keepLooking = (searchQueryStringToUse != null);
					}
					if (this.IsTraceEnabled(TraceType.DebugTrace))
					{
						this.Tracer.TraceDebug<bool>(0L, "Make another query: [{0}]", keepLooking);
					}
				}
				while (keepLooking);
				yield break;
			}

			// Token: 0x060006CA RID: 1738 RVA: 0x00018F91 File Offset: 0x00017191
			private static bool IsSearchResultLimitedByFast(QueryStringType queryFilter, ItemType[] searchResult)
			{
				return queryFilter != null && queryFilter.Value != null && searchResult.Length == 250;
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x060006CB RID: 1739 RVA: 0x00018FAA File Offset: 0x000171AA
			private Trace Tracer
			{
				get
				{
					return this.auditLog.ewsClient.Tracer;
				}
			}

			// Token: 0x060006CC RID: 1740 RVA: 0x00018FBC File Offset: 0x000171BC
			private bool IsTraceEnabled(TraceType traceType)
			{
				return this.Tracer != null && this.Tracer.IsTraceEnabled(traceType);
			}

			// Token: 0x060006CD RID: 1741 RVA: 0x00018FD4 File Offset: 0x000171D4
			protected override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<EwsAuditLog.EwsAuditLogQueryContext>(this);
			}

			// Token: 0x04000302 RID: 770
			private static readonly FieldOrderType[] SortByReceivedTime = new FieldOrderType[]
			{
				new FieldOrderType
				{
					Item = new PathToUnindexedFieldType
					{
						FieldURI = UnindexedFieldURIType.itemDateTimeReceived
					},
					Order = SortDirectionType.Descending
				}
			};

			// Token: 0x04000303 RID: 771
			private EwsAuditLog auditLog;

			// Token: 0x04000304 RID: 772
			private IAsyncResult pendingAsyncResult;

			// Token: 0x04000305 RID: 773
			private RestrictionType queryFilter;

			// Token: 0x04000306 RID: 774
			private QueryStringType queryString;
		}

		// Token: 0x0200009F RID: 159
		private class EwsItemPropertyBag : IReadOnlyPropertyBag
		{
			// Token: 0x060006CF RID: 1743 RVA: 0x0001901C File Offset: 0x0001721C
			public EwsItemPropertyBag(ItemType ewsItem)
			{
				this.ewsItem = ewsItem;
			}

			// Token: 0x170001C1 RID: 449
			public object this[PropertyDefinition propertyDefinition]
			{
				get
				{
					object obj = null;
					StorePropertyDefinition storePropertyDefinition = propertyDefinition as StorePropertyDefinition;
					if (storePropertyDefinition != null)
					{
						if (ItemSchema.TextBody.CompareTo(storePropertyDefinition) == 0)
						{
							if (this.ewsItem.ExtendedProperty != null && this.ewsItem.ExtendedProperty.Length > 0)
							{
								foreach (ExtendedPropertyType extendedPropertyType in this.ewsItem.ExtendedProperty)
								{
									if (string.Equals("0x1000", extendedPropertyType.ExtendedFieldURI.PropertyTag))
									{
										obj = (extendedPropertyType.Item as string);
									}
								}
							}
						}
						else if (ItemSchema.Id.CompareTo(storePropertyDefinition) == 0)
						{
							obj = new ConfigObjectId(this.ewsItem.ItemId.Id);
						}
						else if (StoreObjectSchema.CreationTime.CompareTo(storePropertyDefinition) == 0 && this.ewsItem.DateTimeCreatedSpecified)
						{
							obj = new ExDateTime(ExTimeZone.UtcTimeZone, this.ewsItem.DateTimeCreated);
						}
					}
					if (obj == null)
					{
						obj = new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
					}
					return obj;
				}
			}

			// Token: 0x060006D1 RID: 1745 RVA: 0x00019126 File Offset: 0x00017326
			public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04000307 RID: 775
			private const string BodyPropertyTag = "0x1000";

			// Token: 0x04000308 RID: 776
			private ItemType ewsItem;
		}
	}
}
