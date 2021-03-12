using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Notification;
using Microsoft.Exchange.Transport.Sync.Common.SyncHealthLog;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class SyncHealthLogManager
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public static bool TryConfigure(SyncHealthLogConfiguration syncHealthLogConfiguration)
		{
			bool result;
			lock (SyncHealthLogManager.syncObject)
			{
				if (SyncHealthLogManager.initialized)
				{
					SyncHealthLogManager.syncHealthLog.Configure(syncHealthLogConfiguration);
					result = true;
				}
				else
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)227UL, SyncHealthLogManager.Tracer, "TryConfigure failed due to shutdown.", new object[0]);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000B3C4 File Offset: 0x000095C4
		public static bool TryLogSubscriptionCompletion(string mailboxServerName, string tenantGuid, string userMailboxGuid, string subscriptionGuid, string incomingServerName, string subscriptionType, string aggregationType, string processedBy, TimeSpan syncDuration, bool moreDataAvailable)
		{
			return SyncHealthLogManager.Log(delegate
			{
				SyncHealthLogManager.syncHealthLog.LogSubscriptionCompletion(mailboxServerName, tenantGuid, userMailboxGuid, subscriptionGuid, incomingServerName, subscriptionType, aggregationType, processedBy, syncDuration, moreDataAvailable);
			});
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000B4A4 File Offset: 0x000096A4
		public static bool TryLogSubscriptionCreation(string mailboxServerName, string tenantGuid, string userMailboxGuid, string subscriptionGuid, string subscriptionType, string creationType, string emailDomain, string incomingServerName, int port, string authenticationType, string encryptionType, DateTime creationTime, string aggregationType)
		{
			return SyncHealthLogManager.Log(delegate
			{
				SyncHealthLogManager.syncHealthLog.LogSubscriptionCreation(mailboxServerName, tenantGuid, userMailboxGuid, subscriptionGuid, subscriptionType, creationType, emailDomain, incomingServerName, port, authenticationType, encryptionType, creationTime, aggregationType);
			});
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000B56C File Offset: 0x0000976C
		public static bool TryLogDatabaseDiscovery(ExDateTime dbPollingTimeStamp, string dbPollingSource, int totalDBCount, int enabledDBCount, string databaseId, string databaseEvent, string currentDatabaseState)
		{
			return SyncHealthLogManager.Log(delegate
			{
				SyncHealthLogManager.syncHealthLog.LogDatabaseDiscovery(dbPollingTimeStamp, dbPollingSource, totalDBCount, enabledDBCount, databaseId, databaseEvent, currentDatabaseState);
			});
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000B5C4 File Offset: 0x000097C4
		public static bool TryLogMailboxNotification(Guid mailboxGuid, Guid mdbGuid, SubscriptionNotificationRpcMethodCode notificationType)
		{
			bool result;
			lock (SyncHealthLogManager.syncObject)
			{
				if (SyncHealthLogManager.initialized)
				{
					SyncHealthLogManager.syncHealthLog.LogMailboxNotification(mailboxGuid, mdbGuid, notificationType);
					result = true;
				}
				else
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)518UL, SyncHealthLogManager.Tracer, "TryLogMailboxNotification failed due to shutdown.", new object[0]);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000B690 File Offset: 0x00009890
		public static bool TryLogSubscriptionDeletion(string mailboxServerName, string tenantGuid, string userMailboxGuid, string subscriptionGuid, string subscriptionType, string incomingServerName, bool wasSubscriptionDeleted, string aggregationType)
		{
			return SyncHealthLogManager.Log(delegate
			{
				SyncHealthLogManager.syncHealthLog.LogSubscriptionDeletion(mailboxServerName, tenantGuid, userMailboxGuid, subscriptionGuid, subscriptionType, incomingServerName, wasSubscriptionDeleted, aggregationType);
			});
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000B778 File Offset: 0x00009978
		public static bool TryLogSubscriptionDispatch(string mailboxServerName, string tenantGuid, string userMailboxGuid, string subscriptionGuid, string incomingServerName, string subscriptionType, string aggregationType, string dispatchedTo, bool isSuccessful, bool isPermanentError, bool isTransientError, string dispatchError, bool isBeyondSyncPollingFrequency, int secondsBeyondPollingFrequency, string workType, string dispatchTrigger, string databaseGuid)
		{
			return SyncHealthLogManager.Log(delegate
			{
				SyncHealthLogManager.syncHealthLog.LogSubscriptionDispatch(mailboxServerName, tenantGuid, userMailboxGuid, subscriptionGuid, incomingServerName, subscriptionType, aggregationType, dispatchedTo, isSuccessful, isPermanentError, isTransientError, dispatchError, isBeyondSyncPollingFrequency, secondsBeyondPollingFrequency, workType, dispatchTrigger, databaseGuid);
			});
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000B83C File Offset: 0x00009A3C
		public static void LogWorkTypeBudgets(KeyValuePair<string, object>[] eventData)
		{
			SyncHealthLogManager.Log(delegate
			{
				SyncHealthLogManager.syncHealthLog.LogWorkTypeBudgets(eventData);
			});
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000B868 File Offset: 0x00009A68
		public static void Start()
		{
			lock (SyncHealthLogManager.syncObject)
			{
				if (!SyncHealthLogManager.initialized)
				{
					ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)233UL, SyncHealthLogManager.Tracer, "SyncHealthLogManager.Start: Called.", new object[0]);
					SyncHealthLogManager.syncHealthLog = new SyncHealthLog(ContentAggregationConfig.SyncMailboxHealthLogConfiguration);
					SyncHealthLogManager.initialized = true;
				}
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public static void Shutdown()
		{
			lock (SyncHealthLogManager.syncObject)
			{
				if (SyncHealthLogManager.initialized)
				{
					ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)234UL, SyncHealthLogManager.Tracer, "SyncHealthLogManager.Shutdown: Called.", new object[0]);
					SyncHealthLogManager.syncHealthLog.Dispose();
					SyncHealthLogManager.syncHealthLog = null;
					SyncHealthLogManager.initialized = false;
				}
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000B964 File Offset: 0x00009B64
		private static bool Log(Action action)
		{
			bool result;
			lock (SyncHealthLogManager.syncObject)
			{
				if (SyncHealthLogManager.initialized)
				{
					action();
					result = true;
				}
				else
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)231UL, SyncHealthLogManager.Tracer, "Log failed due to shutdown.", new object[0]);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0400010E RID: 270
		private static readonly Trace Tracer = ExTraceGlobals.SyncHealthLogManagerTracer;

		// Token: 0x0400010F RID: 271
		private static readonly object syncObject = new object();

		// Token: 0x04000110 RID: 272
		private static bool initialized;

		// Token: 0x04000111 RID: 273
		private static SyncHealthLog syncHealthLog;
	}
}
