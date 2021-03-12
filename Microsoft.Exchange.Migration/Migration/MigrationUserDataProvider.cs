using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000CD RID: 205
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationUserDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002E004 File Offset: 0x0002C204
		protected MigrationUserDataProvider(MigrationDataProvider dataProvider, string executingUserId, bool cleanSessionOnDispose = true) : base(dataProvider.MailboxSession)
		{
			this.dataProvider = dataProvider;
			this.diagnosticEnabled = false;
			this.IncludeReport = false;
			this.jobCache = new MigrationJobObjectCache(dataProvider);
			this.ExecutingUserId = executingUserId;
			this.cleanSessionOnDispose = cleanSessionOnDispose;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0002E041 File Offset: 0x0002C241
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x0002E049 File Offset: 0x0002C249
		public int? LimitSkippedItemsTo { get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0002E052 File Offset: 0x0002C252
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x0002E05A File Offset: 0x0002C25A
		public bool ForceRemoval { get; set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0002E063 File Offset: 0x0002C263
		public IMigrationDataProvider MailboxProvider
		{
			get
			{
				return this.dataProvider;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0002E06B File Offset: 0x0002C26B
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x0002E073 File Offset: 0x0002C273
		public LocalizedException LastError { get; private set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0002E07C File Offset: 0x0002C27C
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x0002E084 File Offset: 0x0002C284
		public bool IncludeReport { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0002E08D File Offset: 0x0002C28D
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x0002E095 File Offset: 0x0002C295
		public string ExecutingUserId { get; private set; }

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002E09E File Offset: 0x0002C29E
		public static MigrationUserDataProvider CreateDataProvider(MigrationDataProvider dataProvider, string executingUserId = null)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			return new MigrationUserDataProvider(dataProvider, executingUserId, false);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
		public static MigrationUserDataProvider CreateDataProvider(string action, IRecipientSession recipientSession, ADUser partitionMailbox, string executingUserId = null)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(action, "action");
			MigrationUtil.ThrowOnNullArgument(recipientSession, "recipientSession");
			MigrationUserDataProvider result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MigrationDataProvider disposable = MigrationDataProvider.CreateProviderForMigrationMailbox(action, recipientSession, partitionMailbox);
				disposeGuard.Add<MigrationDataProvider>(disposable);
				MigrationUserDataProvider migrationUserDataProvider = new MigrationUserDataProvider(disposable, executingUserId, true);
				disposeGuard.Success();
				result = migrationUserDataProvider;
			}
			return result;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002E124 File Offset: 0x0002C324
		public MigrationJob GetJob(MigrationBatchId batch)
		{
			return this.jobCache.GetJob(batch);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002E132 File Offset: 0x0002C332
		public void EnableDiagnostics(string argument)
		{
			this.diagnosticEnabled = true;
			this.diagnosticArgument = new MigrationDiagnosticArgument(argument);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002E147 File Offset: 0x0002C347
		public IEnumerable<MigrationUser> GetByUserId(MigrationUserId userId, int pageSize)
		{
			return this.InternalFindPaged<MigrationUser>(null, userId, false, null, pageSize);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002E418 File Offset: 0x0002C618
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy unused, int pageSize)
		{
			SortBy[] sort = null;
			QueryFilter[] internalFilters = null;
			this.DetermineInternalFiltering(filter, rootId, ref pageSize, out sort, out internalFilters);
			foreach (QueryFilter internalFilter in internalFilters)
			{
				foreach (object[] row in this.MailboxProvider.QueryRows(internalFilter, sort, MigrationUser.PropertyDefinitions, pageSize))
				{
					MigrationJobItemSummary summary = MigrationJobItemSummary.LoadFromRow(row);
					this.LastError = null;
					yield return (T)((object)this.ConvertStoreObjectToPresentationObject<T>(summary));
				}
			}
			yield break;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002E44C File Offset: 0x0002C64C
		protected override void InternalSave(ConfigurableObject instance)
		{
			ObjectState objectState = instance.ObjectState;
			if (objectState != ObjectState.Deleted)
			{
				throw new NotSupportedException(string.Format("Save: MigrationUserDataProvider(objectState:{0})", instance.ObjectState));
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002E67C File Offset: 0x0002C87C
		protected override void InternalDelete(ConfigurableObject instance)
		{
			MigrationUser user = (MigrationUser)instance;
			MigrationJob job = null;
			bool isPAW = false;
			MigrationHelper.RunUpdateOperation(delegate
			{
				job = this.GetJob(user.BatchId);
				if (job == null && !this.ForceRemoval)
				{
					throw new CannotRemoveUserWithoutBatchException(user.Identity.ToString());
				}
				MigrationJobItem byGuid = MigrationJobItem.GetByGuid(this.MailboxProvider, user.Identity.JobItemGuid);
				if (byGuid == null)
				{
					throw new MigrationUserAlreadyRemovedException(user.Identity.JobItemGuid.ToString());
				}
				isPAW = byGuid.IsPAW;
				if (isPAW && !this.ForceRemoval)
				{
					byGuid.SetMigrationFlags(this.MailboxProvider, MigrationFlags.Remove);
					return;
				}
				if (!this.ForceRemoval)
				{
					MigrationUtil.AssertOrThrow(!isPAW && job != null, "We should have thrown above if this is not the case!", new object[0]);
					MigrationUtil.AssertOrThrow(byGuid.MigrationJobId == job.JobId, "The job should be the owner of the job item.", new object[0]);
					using (ILegacySubscriptionHandler legacySubscriptionHandler = LegacySubscriptionHandlerBase.CreateSubscriptionHandler(this.MailboxProvider, job))
					{
						MigrationJobRemovingProcessor.RemoveJobItemSubscription(byGuid, legacySubscriptionHandler);
					}
				}
				byGuid.Delete(this.MailboxProvider);
			});
			if (!isPAW && job != null)
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					if (!this.ForceRemoval)
					{
						job.IncrementRemovedUserCount(this.MailboxProvider);
					}
					job.ReportData.Append(Strings.MigrationReportJobItemRemoved(this.ExecutingUserId, user.Identity.ToString()));
					this.MailboxProvider.FlushReport(job.ReportData);
				}, null);
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002E6EC File Offset: 0x0002C8EC
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.cleanSessionOnDispose && this.dataProvider != null)
					{
						this.dataProvider.Dispose();
					}
					this.dataProvider = null;
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002E738 File Offset: 0x0002C938
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationUserDataProvider>(this);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002E740 File Offset: 0x0002C940
		private T ConvertStoreObjectToPresentationObject<T>(MigrationJobItemSummary summary)
		{
			MigrationUser migrationUser;
			if (typeof(T).Equals(typeof(MigrationUserStatistics)))
			{
				migrationUser = new MigrationUserStatistics();
			}
			else
			{
				migrationUser = new MigrationUser();
			}
			if (summary.Identifier != null || summary.JobItemGuid != null)
			{
				migrationUser.Identity = new MigrationUserId(summary.Identifier, summary.JobItemGuid ?? Guid.Empty);
			}
			migrationUser.EmailAddress = SmtpAddress.Empty;
			SmtpAddress emailAddress;
			if (!string.IsNullOrEmpty(summary.LocalMailboxIdentifier))
			{
				migrationUser.EmailAddress = (SmtpAddress)summary.LocalMailboxIdentifier;
			}
			else if (MigrationServiceHelper.TryParseSmtpAddress(summary.Identifier, out emailAddress))
			{
				migrationUser.EmailAddress = emailAddress;
			}
			migrationUser.RecipientType = summary.RecipientType;
			migrationUser.SkippedItemCount = summary.ItemsSkipped;
			migrationUser.SyncedItemCount = summary.ItemsSynced;
			migrationUser.MailboxLegacyDN = summary.MailboxLegacyDN;
			ExTimeZone timeZone = null;
			MigrationJobSummary jobSummary = this.jobCache.GetJobSummary(summary.BatchGuid);
			if (jobSummary != null)
			{
				migrationUser.BatchId = jobSummary.BatchId;
				timeZone = jobSummary.UserTimeZone;
			}
			ExDateTime? universalDateTime = MigrationHelper.GetUniversalDateTime(summary.LastSubscriptionCheckTime);
			migrationUser.LastSubscriptionCheckTime = (DateTime?)MigrationHelper.GetLocalizedDateTime(universalDateTime, timeZone);
			ExDateTime? universalDateTime2 = MigrationHelper.GetUniversalDateTime(summary.LastSuccessfulSyncTime);
			migrationUser.LastSuccessfulSyncTime = (DateTime?)MigrationHelper.GetLocalizedDateTime(universalDateTime2, timeZone);
			if (summary.MailboxGuid != null)
			{
				migrationUser.MailboxGuid = summary.MailboxGuid.Value;
			}
			if (summary.MrsId != null)
			{
				migrationUser.RequestGuid = summary.MrsId.Value;
			}
			if (summary.Status != null)
			{
				migrationUser.Status = summary.Status.Value;
			}
			if (migrationUser is MigrationUserStatistics)
			{
				this.PopulateStatistics((MigrationUserStatistics)migrationUser);
			}
			return (T)((object)migrationUser);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002E928 File Offset: 0x0002CB28
		private void PopulateStatistics(MigrationUserStatistics user)
		{
			MigrationUtil.ThrowOnNullArgument(user, "user");
			MigrationUtil.ThrowOnNullArgument(user.Identity, "Identity");
			MigrationUtil.ThrowOnNullArgument(this.MailboxProvider, "dataProvider");
			MigrationSession argument = MigrationSession.Get(this.MailboxProvider, false);
			MigrationUtil.ThrowOnNullArgument(argument, "session");
			MigrationJobItem migrationJobItem = null;
			if (user.Identity != null && user.Identity.JobItemGuid != Guid.Empty)
			{
				migrationJobItem = MigrationJobItem.GetByGuid(this.MailboxProvider, user.Identity.JobItemGuid);
			}
			if (migrationJobItem == null)
			{
				return;
			}
			if (migrationJobItem.StatusData != null)
			{
				user.Error = migrationJobItem.StatusData.FailureRecord;
			}
			user.MigrationType = migrationJobItem.MigrationType;
			if (this.diagnosticEnabled)
			{
				XElement diagnosticInfo = migrationJobItem.GetDiagnosticInfo(this.MailboxProvider, this.diagnosticArgument);
				if (diagnosticInfo != null)
				{
					user.DiagnosticInfo = diagnosticInfo.ToString();
				}
			}
			if (migrationJobItem.SubscriptionId != null)
			{
				SubscriptionSnapshot subscriptionSnapshot = null;
				try
				{
					SubscriptionAccessorBase subscriptionAccessorBase = SubscriptionAccessorBase.CreateAccessor(this.MailboxProvider, user.MigrationType, migrationJobItem.IsPAW);
					subscriptionAccessorBase.IncludeReport = true;
					subscriptionSnapshot = subscriptionAccessorBase.RetrieveSubscriptionSnapshot(migrationJobItem.SubscriptionId);
				}
				catch (LocalizedException lastError)
				{
					this.LastError = lastError;
				}
				if (subscriptionSnapshot != null)
				{
					user.TotalQueuedDuration = subscriptionSnapshot.TotalQueuedDuration;
					user.TotalInProgressDuration = subscriptionSnapshot.TotalInProgressDuration;
					user.TotalSyncedDuration = subscriptionSnapshot.TotalSyncedDuration;
					user.TotalStalledDuration = subscriptionSnapshot.TotalStalledDuration;
					user.EstimatedTotalTransferSize = subscriptionSnapshot.EstimatedTotalTransferSize;
					user.EstimatedTotalTransferCount = subscriptionSnapshot.EstimatedTotalTransferCount;
					user.BytesTransferred = subscriptionSnapshot.BytesTransferred;
					user.AverageBytesTransferredPerHour = subscriptionSnapshot.AverageBytesTransferredPerHour;
					user.CurrentBytesTransferredPerMinute = subscriptionSnapshot.CurrentBytesTransferredPerMinute;
					user.SyncedItemCount = subscriptionSnapshot.NumItemsSynced;
					user.TotalItemsInSourceMailboxCount = subscriptionSnapshot.NumTotalItemsInMailbox;
					user.SkippedItemCount = subscriptionSnapshot.NumItemsSkipped;
					user.PercentageComplete = subscriptionSnapshot.PercentageComplete;
					if (subscriptionSnapshot.Report != null)
					{
						user.SkippedItems = new MultiValuedProperty<MigrationUserSkippedItem>();
						this.LoadSkippedItems(user.SkippedItems, subscriptionSnapshot.Report.BadItems);
						this.LoadSkippedItems(user.SkippedItems, subscriptionSnapshot.Report.LargeItems);
						if (this.IncludeReport)
						{
							user.Report = subscriptionSnapshot.Report;
						}
					}
				}
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002EB54 File Offset: 0x0002CD54
		private void LoadSkippedItems(MultiValuedProperty<MigrationUserSkippedItem> skippedItems, List<BadMessageRec> badMessages)
		{
			int num = skippedItems.Count;
			if ((this.LimitSkippedItemsTo != null && this.LimitSkippedItemsTo.Value - num <= 0) || badMessages == null)
			{
				return;
			}
			foreach (BadMessageRec rec in badMessages)
			{
				skippedItems.Add(this.BadRecToSkippedItem(rec));
				if (this.LimitSkippedItemsTo != null && ++num >= this.LimitSkippedItemsTo.Value)
				{
					break;
				}
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002EC00 File Offset: 0x0002CE00
		private MigrationUserSkippedItem BadRecToSkippedItem(BadMessageRec rec)
		{
			MigrationUtil.ThrowOnNullArgument(rec, "rec");
			return new MigrationUserSkippedItem
			{
				Kind = rec.Kind.ToString(),
				FolderName = rec.FolderName,
				Sender = rec.Sender,
				Recipient = rec.Recipient,
				Subject = rec.Subject,
				MessageClass = rec.MessageClass,
				MessageSize = rec.MessageSize,
				DateSent = rec.DateSent,
				DateReceived = rec.DateReceived,
				Failure = ((rec.Failure != null) ? rec.Failure.ToString() : null)
			};
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002ECB4 File Offset: 0x0002CEB4
		private void ParsePresentationFilter(QueryFilter presentationFilter, out Guid? mailboxGuid, out Guid? batchGuid, out MigrationUserStatus[] statuses)
		{
			statuses = null;
			mailboxGuid = null;
			batchGuid = null;
			List<QueryFilter> list = new List<QueryFilter>();
			if (presentationFilter != null)
			{
				list.Add(presentationFilter);
			}
			while (list.Count > 0)
			{
				QueryFilter queryFilter = list[0];
				list.RemoveAt(0);
				if (queryFilter is CompositeFilter)
				{
					list.AddRange(((CompositeFilter)queryFilter).Filters);
				}
				else if (queryFilter is ComparisonFilter)
				{
					ComparisonFilter comparisonFilter = (ComparisonFilter)queryFilter;
					PropertyDefinition property = comparisonFilter.Property;
					object propertyValue = comparisonFilter.PropertyValue;
					if (property == MigrationUserSchema.BatchId)
					{
						MigrationBatchId batchId = (MigrationBatchId)propertyValue;
						batchGuid = new Guid?(this.jobCache.GetBatchGuidById(batchId));
					}
					else if (property == MigrationUserSchema.Status)
					{
						statuses = new MigrationUserStatus[]
						{
							(MigrationUserStatus)propertyValue
						};
					}
					else if (property == MigrationUserSchema.StatusSummary)
					{
						statuses = MigrationUser.MapFromSummaryToStatus[(MigrationUserStatusSummary)propertyValue];
					}
					else if (property == MigrationUserSchema.MailboxGuid)
					{
						mailboxGuid = new Guid?((Guid)propertyValue);
					}
				}
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002EDCC File Offset: 0x0002CFCC
		private void DetermineInternalFiltering(QueryFilter filter, ObjectId rootId, ref int pageSize, out SortBy[] sort, out QueryFilter[] internalFilters)
		{
			if (filter != null)
			{
				Guid? guid = null;
				Guid? guid2 = null;
				MigrationUserStatus[] array = null;
				this.ParsePresentationFilter(filter, out guid, out guid2, out array);
				if (guid != null)
				{
					internalFilters = new QueryFilter[]
					{
						QueryFilter.AndTogether(new QueryFilter[]
						{
							MigrationJobItem.MessageClassEqualityFilter.Filter,
							new ComparisonFilter(ComparisonOperator.Equal, MigrationUser.MailboxGuidPropertyDefinition, guid.Value)
						})
					};
					sort = new SortBy[]
					{
						new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
						new SortBy(MigrationUser.MailboxGuidPropertyDefinition, SortOrder.Ascending)
					};
					pageSize = 2;
					return;
				}
				if (guid2 != null)
				{
					sort = new SortBy[]
					{
						new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
						new SortBy(MigrationUser.BatchIdPropertyDefinition, SortOrder.Ascending),
						new SortBy(MigrationUser.StatusPropertyDefinition, SortOrder.Ascending)
					};
				}
				else
				{
					sort = new SortBy[]
					{
						new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
						new SortBy(MigrationUser.StatusPropertyDefinition, SortOrder.Ascending)
					};
				}
				if (array != null)
				{
					internalFilters = new QueryFilter[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						MigrationUserStatus migrationUserStatus = array[i];
						if (guid2 != null)
						{
							internalFilters[i] = QueryFilter.AndTogether(new QueryFilter[]
							{
								MigrationJobItem.MessageClassEqualityFilter.Filter,
								new ComparisonFilter(ComparisonOperator.Equal, MigrationUser.BatchIdPropertyDefinition, guid2.Value),
								new ComparisonFilter(ComparisonOperator.Equal, MigrationUser.StatusPropertyDefinition, (int)migrationUserStatus)
							});
						}
						else
						{
							internalFilters[i] = QueryFilter.AndTogether(new QueryFilter[]
							{
								MigrationJobItem.MessageClassEqualityFilter.Filter,
								new ComparisonFilter(ComparisonOperator.Equal, MigrationUser.StatusPropertyDefinition, (int)migrationUserStatus)
							});
						}
					}
					return;
				}
				if (guid2 != null)
				{
					internalFilters = new QueryFilter[]
					{
						QueryFilter.AndTogether(new QueryFilter[]
						{
							MigrationJobItem.MessageClassEqualityFilter.Filter,
							new ComparisonFilter(ComparisonOperator.Equal, MigrationUser.BatchIdPropertyDefinition, guid2.Value)
						})
					};
					return;
				}
				sort = null;
			}
			MigrationUserId migrationUserId = rootId as MigrationUserId;
			if (migrationUserId != null)
			{
				if (migrationUserId.JobItemGuid != Guid.Empty)
				{
					internalFilters = new QueryFilter[]
					{
						QueryFilter.AndTogether(new QueryFilter[]
						{
							MigrationJobItem.MessageClassEqualityFilter.Filter,
							new ComparisonFilter(ComparisonOperator.Equal, MigrationUser.IdPropertyDefinition, migrationUserId.JobItemGuid)
						})
					};
					sort = MigrationUserDataProvider.JobItemIdSort;
				}
				else
				{
					internalFilters = new QueryFilter[]
					{
						QueryFilter.AndTogether(new QueryFilter[]
						{
							MigrationJobItem.MessageClassEqualityFilter.Filter,
							new ComparisonFilter(ComparisonOperator.Equal, MigrationUser.IdentifierPropertyDefinition, migrationUserId.Id)
						})
					};
					sort = MigrationUserDataProvider.DefaultSort;
				}
				pageSize = 2;
				return;
			}
			internalFilters = new QueryFilter[]
			{
				MigrationJobItem.MessageClassEqualityFilter.Filter
			};
			sort = MigrationUserDataProvider.DefaultSort;
		}

		// Token: 0x04000428 RID: 1064
		public static readonly SortBy[] DefaultSort = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(MigrationUser.IdentifierPropertyDefinition, SortOrder.Ascending)
		};

		// Token: 0x04000429 RID: 1065
		public static readonly SortBy[] JobItemIdSort = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(MigrationUser.IdPropertyDefinition, SortOrder.Ascending)
		};

		// Token: 0x0400042A RID: 1066
		private readonly MigrationJobObjectCache jobCache;

		// Token: 0x0400042B RID: 1067
		private MigrationDataProvider dataProvider;

		// Token: 0x0400042C RID: 1068
		private bool diagnosticEnabled;

		// Token: 0x0400042D RID: 1069
		private MigrationDiagnosticArgument diagnosticArgument;

		// Token: 0x0400042E RID: 1070
		private readonly bool cleanSessionOnDispose;
	}
}
