using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D22 RID: 3362
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SearchEventLogger
	{
		// Token: 0x17001EF4 RID: 7924
		// (get) Token: 0x060073FB RID: 29691 RVA: 0x00202900 File Offset: 0x00200B00
		public static SearchEventLogger Instance
		{
			get
			{
				return SearchEventLogger.instance;
			}
		}

		// Token: 0x060073FC RID: 29692 RVA: 0x00202907 File Offset: 0x00200B07
		public SearchEventLogger()
		{
			this.logger = new ExEventLog(new Guid("{8E4F12B2-E72A-42b4-816C-30462241203A}"), "MSExchange Mid-Tier Storage");
		}

		// Token: 0x060073FD RID: 29693 RVA: 0x0020292C File Offset: 0x00200B2C
		public void LogSearchObjectSavedEvent(SearchEventLogger.PropertyLogData logData)
		{
			this.logger.LogEvent(StorageEventLogConstants.Tuple_SearchObjectSaved, null, new object[]
			{
				logData
			});
		}

		// Token: 0x060073FE RID: 29694 RVA: 0x00202958 File Offset: 0x00200B58
		public void LogSearchStatusSavedEvent(SearchEventLogger.PropertyLogData logData)
		{
			this.logger.LogEvent(StorageEventLogConstants.Tuple_SearchStatusSaved, null, new object[]
			{
				logData
			});
		}

		// Token: 0x060073FF RID: 29695 RVA: 0x00202984 File Offset: 0x00200B84
		public void LogSearchErrorEvent(string searchId, string errorMsg)
		{
			this.logger.LogEvent(StorageEventLogConstants.Tuple_SearchStatusError, null, new object[]
			{
				searchId,
				errorMsg
			});
		}

		// Token: 0x06007400 RID: 29696 RVA: 0x002029B4 File Offset: 0x00200BB4
		public void LogDiscoveryAndHoldSavedEvent(MailboxDiscoverySearch obj)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty(EwsStoreObjectSchema.Identity.Name, obj.Identity);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.StatisticsOnly.Name, obj.StatisticsOnly);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.IncludeUnsearchableItems.Name, obj.IncludeUnsearchableItems);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.IncludeKeywordStatistics.Name, obj.IncludeKeywordStatistics);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.LogLevel.Name, obj.LogLevel);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.Query.Name, (obj.Query != null) ? obj.Query.Length : 0);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.Language.Name, obj.Language);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.CreatedTime.Name, obj.CreatedTime);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.LastModifiedTime.Name, obj.LastModifiedTime);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ExcludeDuplicateMessages.Name, obj.ExcludeDuplicateMessages);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.InPlaceHoldEnabled.Name, obj.InPlaceHoldEnabled);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ItemHoldPeriod.Name, obj.ItemHoldPeriod);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.InPlaceHoldIdentity.Name, obj.InPlaceHoldIdentity);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.KeywordHits.Name, (obj.KeywordHits != null) ? obj.KeywordHits.Count : 0);
			propertyLogData.AddOrganization(obj.ManagedByOrganization);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoveryAndHoldSaved, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007401 RID: 29697 RVA: 0x00202B7C File Offset: 0x00200D7C
		public void LogDiscoverySearchRpcServerRestartedEvent(Exception exception)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("Exception", exception);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchRpcServerRestarted, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007402 RID: 29698 RVA: 0x00202BBC File Offset: 0x00200DBC
		public void LogDiscoverySearchPendingWorkItemsChangedEvent(string description, string searchName, string arbitrationMailbox, int pendingItemsCount)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("Description", description);
			propertyLogData.AddProperty("ArbitrationMailbox", arbitrationMailbox);
			propertyLogData.AddProperty("PendingWorkItemsCount", pendingItemsCount);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchWorkItemQueueChanged, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007403 RID: 29699 RVA: 0x00202C18 File Offset: 0x00200E18
		public void LogDiscoverySearchWorkItemQueueChangedEvent(string description, string searchName, string arbitrationMailbox, string workItemAction, bool isEstimateOnly, int queueLength, int runningWorkItemsCount, int copySearchesInProgressCount, int semaphoreCount = 0)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("Description", description);
			propertyLogData.AddProperty("WorkItemAction", workItemAction);
			propertyLogData.AddProperty("IsEstimateOnly", isEstimateOnly);
			propertyLogData.AddProperty("ArbitrationMailbox", arbitrationMailbox);
			propertyLogData.AddProperty("QueueLength", queueLength);
			propertyLogData.AddProperty("RunningWorkItemsCount", runningWorkItemsCount);
			propertyLogData.AddProperty("CopySearchesInProgressCount", copySearchesInProgressCount);
			propertyLogData.AddProperty("SemaphoreCount", semaphoreCount);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchWorkItemQueueChanged, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007404 RID: 29700 RVA: 0x00202CC8 File Offset: 0x00200EC8
		public void LogDiscoverySearchWorkItemQueueNotProcessedEvent(string searchName, string arbitrationMailbox, string workItemAction, bool isEstimateOnly, int queueLength, int runningWorkItemsCount, int copySearchesInProgressCount, int maxRunningCopySearches)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("WorkItemAction", workItemAction);
			propertyLogData.AddProperty("IsEstimateOnly", isEstimateOnly);
			propertyLogData.AddProperty("ArbitrationMailbox", arbitrationMailbox);
			propertyLogData.AddProperty("QueueLength", queueLength);
			propertyLogData.AddProperty("RunningWorkItemsCount", runningWorkItemsCount);
			propertyLogData.AddProperty("CopySearchesInProgressCount", copySearchesInProgressCount);
			propertyLogData.AddProperty("MaxRunningCopySearches", maxRunningCopySearches);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchWorkItemQueueNotProcessed, searchName + arbitrationMailbox, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007405 RID: 29701 RVA: 0x00202D74 File Offset: 0x00200F74
		public void LogDiscoverySearchStatusChangedEvent(MailboxDiscoverySearch obj, string parentCallerInfo, [CallerMemberName] string callerMember = null)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("CallerInfo", parentCallerInfo + ": " + callerMember);
			propertyLogData.AddProperty(EwsStoreObjectSchema.Identity.Name, obj.Identity);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.Status.Name, obj.Status);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.StatisticsOnly.Name, obj.StatisticsOnly);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.IncludeUnsearchableItems.Name, obj.IncludeUnsearchableItems);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.IncludeKeywordStatistics.Name, obj.IncludeKeywordStatistics);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.Query.Name, (obj.Query != null) ? obj.Query.Length : 0);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.Language.Name, obj.Language);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.Sources.Name, obj.Sources);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.CreatedTime.Name, obj.CreatedTime);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.LastModifiedTime.Name, obj.LastModifiedTime);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ExcludeDuplicateMessages.Name, obj.ExcludeDuplicateMessages);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.InPlaceHoldEnabled.Name, obj.InPlaceHoldEnabled);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ItemHoldPeriod.Name, obj.ItemHoldPeriod);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.InPlaceHoldIdentity.Name, obj.InPlaceHoldIdentity);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.FailedToHoldMailboxes.Name, obj.FailedToHoldMailboxes);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.KeywordHits.Name, (obj.KeywordHits != null) ? obj.KeywordHits.Count : 0);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ScenarioId.Name, obj.ScenarioId);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.Resume.Name, obj.Resume);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.LastStartTime.Name, obj.LastStartTime);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.LastEndTime.Name, obj.LastEndTime);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.PercentComplete.Name, obj.PercentComplete);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.NumberOfMailboxes.Name, obj.NumberOfMailboxes);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ResultItemCountCopied.Name, obj.ResultItemCountCopied);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ResultItemCountEstimate.Name, obj.ResultItemCountEstimate);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ResultSizeCopied.Name, obj.ResultSizeCopied);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ResultSizeEstimate.Name, obj.ResultSizeEstimate);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ResultsPath.Name, obj.ResultsPath);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ResultsLink.Name, obj.ResultsLink);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.CompletedMailboxes.Name, obj.CompletedMailboxes);
			propertyLogData.AddOrganization(obj.ManagedByOrganization);
			if (obj.Errors != null && obj.Errors.Count > 0)
			{
				propertyLogData.AddProperty(MailboxDiscoverySearchSchema.Errors.Name, string.Join(",", obj.Errors.ToArray()));
			}
			if (obj.IsFeatureFlighted("SearchStatsFlighted") && obj.SearchStatistics != null && obj.SearchStatistics.Count > 0 && obj.SearchStatistics[0] != null)
			{
				propertyLogData.AddProperty(MailboxDiscoverySearchSchema.SearchStatistics.Name, obj.SearchStatistics[0].ToString());
			}
			propertyLogData.AddProperty("Flighted Features", obj.FlightedFeatures);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchStatusChanged, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007406 RID: 29702 RVA: 0x00203168 File Offset: 0x00201368
		public void LogInPlaceHoldSettingsSynchronizedEvent(MailboxDiscoverySearch obj)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.InPlaceHoldEnabled.Name, obj.InPlaceHoldEnabled);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.ItemHoldPeriod.Name, obj.ItemHoldPeriod);
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.InPlaceHoldErrors.Name, obj.InPlaceHoldErrors);
			propertyLogData.AddOrganization(obj.ManagedByOrganization);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_InPlaceHoldSettingsSynchronized, null, new object[]
			{
				propertyLogData
			});
			if (obj.InPlaceHoldErrors != null && obj.InPlaceHoldErrors.Count > 0)
			{
				foreach (string stringToEscape in obj.InPlaceHoldErrors)
				{
					string value = Uri.EscapeDataString(stringToEscape);
					SearchEventLogger.PropertyLogData propertyLogData2 = new SearchEventLogger.PropertyLogData();
					propertyLogData2.AddOrganization(obj.ManagedByOrganization);
					propertyLogData2.AddProperty("Error", value);
					this.logger.LogEvent(StorageEventLogConstants.Tuple_SynchronizeInPlaceHoldError, null, new object[]
					{
						propertyLogData2
					});
				}
			}
		}

		// Token: 0x06007407 RID: 29703 RVA: 0x00203294 File Offset: 0x00201494
		public void LogDiscoverySearchStartRequestedEvent(MailboxDiscoverySearch obj, string rbacContext)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty(MailboxDiscoverySearchSchema.Resume.Name, obj.Resume);
			propertyLogData.AddProperty("RbacContext", rbacContext);
			propertyLogData.AddOrganization(obj.ManagedByOrganization);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchStartRequested, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007408 RID: 29704 RVA: 0x002032F8 File Offset: 0x002014F8
		public void LogDiscoverySearchStopRequestedEvent(MailboxDiscoverySearch obj, string rbacContext)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("RbacContext", rbacContext);
			propertyLogData.AddOrganization(obj.ManagedByOrganization);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchStopRequested, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007409 RID: 29705 RVA: 0x00203344 File Offset: 0x00201544
		public void LogDiscoverySearchRemoveRequestedEvent(MailboxDiscoverySearch obj, string rbacContext)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("RbacContext", rbacContext);
			propertyLogData.AddOrganization(obj.ManagedByOrganization);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchRemoveRequested, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x0600740A RID: 29706 RVA: 0x00203390 File Offset: 0x00201590
		public void LogDiscoverySearchRequestPickedUpEvent(string name, string actionType, string rbacContext, string orgId)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("RbacContext", rbacContext);
			propertyLogData.AddOrganization(orgId);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchRequestPickedUp, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x0600740B RID: 29707 RVA: 0x002033D8 File Offset: 0x002015D8
		public void LogDiscoverySearchStartRequestProcessedEvent(string name, string rbacContext, string orgId)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("RbacContext", rbacContext);
			propertyLogData.AddOrganization(orgId);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchStartRequestProcessed, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x0600740C RID: 29708 RVA: 0x0020341C File Offset: 0x0020161C
		public void LogDiscoverySearchStopRequestProcessedEvent(string name, string rbacContext, string orgId)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("RbacContext", rbacContext);
			propertyLogData.AddOrganization(orgId);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchStopRequestProcessed, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x0600740D RID: 29709 RVA: 0x00203460 File Offset: 0x00201660
		public void LogDiscoverySearchRemoveRequestProcessedEvent(string name, string rbacContext, string orgId)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("RbacContext", rbacContext);
			propertyLogData.AddOrganization(orgId);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchRemoveRequestProcessed, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x0600740E RID: 29710 RVA: 0x002034A4 File Offset: 0x002016A4
		public void LogDiscoverySearchServerErrorEvent(string description, string name, string arbitrationMailbox, string errorMsg)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("Description", description);
			propertyLogData.AddProperty("ArbitrationMailbox", arbitrationMailbox);
			propertyLogData.AddProperty("Error", errorMsg);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchServerError, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x0600740F RID: 29711 RVA: 0x002034FC File Offset: 0x002016FC
		public void LogDiscoverySearchServerErrorEvent(string description, string name, string arbitrationMailbox, Exception exception)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("Description", description);
			propertyLogData.AddProperty("ArbitrationMailbox", arbitrationMailbox);
			propertyLogData.AddProperty("Exception", exception);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchServerError, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007410 RID: 29712 RVA: 0x00203554 File Offset: 0x00201754
		public void LogDiscoverySearchTaskErrorEvent(string name, string organizationHint, string errorMsg)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("OrganizationHint", organizationHint);
			propertyLogData.AddProperty("Error", errorMsg);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchTaskError, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007411 RID: 29713 RVA: 0x002035A0 File Offset: 0x002017A0
		public void LogDiscoverySearchTaskErrorEvent(string name, string organizationHint, Exception exception)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("OrganizationHint", organizationHint);
			propertyLogData.AddProperty("Exception", exception);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchTaskError, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007412 RID: 29714 RVA: 0x002035EC File Offset: 0x002017EC
		public void LogDiscoverySearchTaskStartedEvent(string name, string organization)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddOrganization(organization);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchTaskStarted, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007413 RID: 29715 RVA: 0x00203624 File Offset: 0x00201824
		public void LogDiscoverySearchTaskCompletedEvent(string name, string organization, string status)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("CompletedStatus", status);
			propertyLogData.AddOrganization(organization);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_DiscoverySearchTaskCompleted, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007414 RID: 29716 RVA: 0x00203668 File Offset: 0x00201868
		public void LogFailedToSyncDiscoveryHoldToExchangeOnlineEvent(object e)
		{
			this.logger.LogEvent(StorageEventLogConstants.Tuple_FailedToSyncDiscoveryHoldToExchangeOnline, null, new object[]
			{
				e
			});
		}

		// Token: 0x06007415 RID: 29717 RVA: 0x00203694 File Offset: 0x00201894
		public void LogSyncDiscoveryHoldToExchangeOnlineStartEvent(string mailbox)
		{
			this.logger.LogEvent(StorageEventLogConstants.Tuple_SyncDiscoveryHoldToExchangeOnlineStart, null, new object[]
			{
				mailbox
			});
		}

		// Token: 0x06007416 RID: 29718 RVA: 0x002036C0 File Offset: 0x002018C0
		public void LogSyncDiscoveryHoldToExchangeOnlineDetailsEvent(int numOfHolds, string remoteAddress)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("NumberOfHolds", numOfHolds);
			propertyLogData.AddProperty("RemoteAddress", remoteAddress);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_SyncDiscoveryHoldToExchangeOnlineDetails, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x06007417 RID: 29719 RVA: 0x00203710 File Offset: 0x00201910
		public void LogSingleFailureSyncDiscoveryHoldToExchangeOnlineEvent(string searchId, string holdAction, string query, string inPlaceHoldIdentity, string errorCode, string errorMessage)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			propertyLogData.AddProperty("HoldAction", holdAction);
			propertyLogData.AddProperty("InPlaceHoldIdentity", inPlaceHoldIdentity);
			propertyLogData.AddProperty("ErrorCode", errorCode);
			propertyLogData.AddProperty("ErrorMessage", errorMessage);
			this.logger.LogEvent(StorageEventLogConstants.Tuple_SingleFailureSyncDiscoveryHoldToExchangeOnline, null, new object[]
			{
				propertyLogData
			});
		}

		// Token: 0x0400512A RID: 20778
		public const string EventLogSourceName = "MSExchange Mid-Tier Storage";

		// Token: 0x0400512B RID: 20779
		private static SearchEventLogger instance = new SearchEventLogger();

		// Token: 0x0400512C RID: 20780
		private ExEventLog logger;

		// Token: 0x02000D23 RID: 3363
		public class PropertyLogData
		{
			// Token: 0x06007419 RID: 29721 RVA: 0x00203780 File Offset: 0x00201980
			public PropertyLogData()
			{
				this.propertyBag = new Dictionary<string, string>();
			}

			// Token: 0x0600741A RID: 29722 RVA: 0x00203794 File Offset: 0x00201994
			public void AddProperty(string name, object value)
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new ArgumentNullException("name");
				}
				string text = string.Empty;
				if (value != null)
				{
					if (value is ICollection)
					{
						text = ((ICollection)value).Count.ToString();
					}
					else
					{
						text = value.ToString();
					}
				}
				if (text.Length > 10000)
				{
					text = text.Substring(0, 10000);
				}
				if (this.propertyBag.ContainsKey(name))
				{
					this.propertyBag[name] = text;
					return;
				}
				this.propertyBag.Add(name, text);
			}

			// Token: 0x0600741B RID: 29723 RVA: 0x00203828 File Offset: 0x00201A28
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, string> keyValuePair in this.propertyBag)
				{
					stringBuilder.Append('&');
					stringBuilder.Append(keyValuePair.Key);
					stringBuilder.Append('=');
					stringBuilder.Append((keyValuePair.Value.IndexOfAny(new char[]
					{
						'&',
						'='
					}) >= 0) ? Uri.EscapeDataString(keyValuePair.Value) : keyValuePair.Value);
				}
				int num = (stringBuilder.Length > 0) ? 1 : 0;
				return stringBuilder.ToString(num, (stringBuilder.Length >= 32767) ? 32766 : (stringBuilder.Length - num));
			}

			// Token: 0x0600741C RID: 29724 RVA: 0x0020390C File Offset: 0x00201B0C
			public void AddOrganization(string org)
			{
				this.AddProperty("Organization", org);
			}

			// Token: 0x0600741D RID: 29725 RVA: 0x0020391C File Offset: 0x00201B1C
			public void AddSearchObject(SearchObject obj)
			{
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				this.AddProperty(SearchObjectBaseSchema.Id.Name, obj.Id);
				this.AddProperty(SearchObjectSchema.Language.Name, obj.Language);
				this.AddProperty(SearchObjectSchema.StartDate.Name, obj.StartDate);
				this.AddProperty(SearchObjectSchema.EndDate.Name, obj.EndDate);
				this.AddProperty(SearchObjectSchema.MessageTypes.Name, obj.MessageTypes);
				this.AddProperty(SearchObjectSchema.SearchDumpster.Name, obj.SearchDumpster);
				this.AddProperty(SearchObjectSchema.LogLevel.Name, obj.LogLevel);
				this.AddProperty(SearchObjectSchema.IncludeUnsearchableItems.Name, obj.IncludeUnsearchableItems);
				this.AddProperty(SearchObjectSchema.IncludePersonalArchive.Name, obj.IncludePersonalArchive);
				this.AddProperty(SearchObjectSchema.IncludeRemoteAccounts.Name, obj.IncludeRemoteAccounts);
				this.AddProperty(SearchObjectSchema.StatusMailRecipients.Name, obj.StatusMailRecipients);
				this.AddProperty(SearchObjectSchema.EstimateOnly.Name, obj.EstimateOnly);
				this.AddProperty(SearchObjectSchema.ExcludeDuplicateMessages.Name, obj.ExcludeDuplicateMessages);
				this.AddProperty(SearchObjectSchema.Resume.Name, obj.Resume);
				this.AddProperty(SearchObjectSchema.SearchQuery.Name, obj.SearchQuery.Length);
				this.AddProperty(SearchObjectSchema.IncludeKeywordStatistics.Name, obj.IncludeKeywordStatistics);
			}

			// Token: 0x0600741E RID: 29726 RVA: 0x00203AD8 File Offset: 0x00201CD8
			public void AddSearchStatus(SearchStatus status)
			{
				if (status == null)
				{
					throw new ArgumentNullException("status");
				}
				this.AddProperty(SearchStatusSchema.Status.Name, status.Status);
				this.AddProperty(SearchStatusSchema.LastStartTime.Name, status.LastStartTime);
				this.AddProperty(SearchStatusSchema.LastEndTime.Name, status.LastEndTime);
				this.AddProperty(SearchStatusSchema.NumberMailboxesToSearch.Name, status.NumberMailboxesToSearch);
				this.AddProperty(SearchStatusSchema.PercentComplete.Name, status.PercentComplete);
				this.AddProperty(SearchStatusSchema.ResultNumber.Name, status.ResultNumber);
				this.AddProperty(SearchStatusSchema.ResultSize.Name, status.ResultSize);
				this.AddProperty(SearchStatusSchema.ResultSizeEstimate.Name, status.ResultSizeEstimate);
				this.AddProperty(SearchStatusSchema.ResultSizeCopied.Name, status.ResultSizeCopied);
				this.AddProperty(SearchStatusSchema.Errors.Name, status.Errors);
				this.AddProperty(SearchStatusSchema.KeywordHits.Name, status.KeywordHits);
				this.AddProperty(SearchStatusSchema.CompletedMailboxes.Name, status.CompletedMailboxes);
			}

			// Token: 0x0400512D RID: 20781
			private Dictionary<string, string> propertyBag;
		}
	}
}
