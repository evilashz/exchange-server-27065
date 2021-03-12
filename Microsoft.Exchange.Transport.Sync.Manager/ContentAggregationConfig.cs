using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.SyncHealthLog;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ContentAggregationConfig
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000C6 RID: 198 RVA: 0x00007794 File Offset: 0x00005994
		// (remove) Token: 0x060000C7 RID: 199 RVA: 0x000077D8 File Offset: 0x000059D8
		internal static event ContentAggregationConfig.ConfigurationChangedEventHandler OnConfigurationChanged
		{
			add
			{
				lock (ContentAggregationConfig.eventHandlers)
				{
					ContentAggregationConfig.eventHandlers.Add(value);
				}
			}
			remove
			{
				lock (ContentAggregationConfig.eventHandlers)
				{
					if (!ContentAggregationConfig.eventHandlers.Remove(value))
					{
						throw new ArgumentException("Event handler is not registered", "value");
					}
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00007830 File Offset: 0x00005A30
		internal static Server LocalServer
		{
			get
			{
				return ContentAggregationConfig.localServer;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00007839 File Offset: 0x00005A39
		internal static ExEventLog EventLogger
		{
			get
			{
				return ContentAggregationConfig.eventLogger;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00007840 File Offset: 0x00005A40
		internal static SyncLog SyncLog
		{
			get
			{
				return ContentAggregationConfig.syncLog;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00007847 File Offset: 0x00005A47
		internal static GlobalSyncLogSession SyncLogSession
		{
			get
			{
				return ContentAggregationConfig.syncLogSession;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000784E File Offset: 0x00005A4E
		internal static TimeSpan AggregationIncrementalSyncInterval
		{
			get
			{
				return ContentAggregationConfig.aggregationIncrementalSyncInterval;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00007855 File Offset: 0x00005A55
		internal static TimeSpan MigrationIncrementalSyncInterval
		{
			get
			{
				return ContentAggregationConfig.migrationIncrementalSyncInterval;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000785C File Offset: 0x00005A5C
		internal static TimeSpan PeopleConnectionInitialSyncInterval
		{
			get
			{
				return ContentAggregationConfig.peopleConnectionInitialSyncInterval;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00007863 File Offset: 0x00005A63
		internal static TimeSpan PeopleConnectionTriggeredSyncInterval
		{
			get
			{
				return ContentAggregationConfig.peopleConnectionTriggeredSyncInterval;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000786A File Offset: 0x00005A6A
		internal static TimeSpan PeopleConnectionIncrementalSyncInterval
		{
			get
			{
				return ContentAggregationConfig.peopleConnectionIncrementalSyncInterval;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00007871 File Offset: 0x00005A71
		internal static TimeSpan OwaMailboxPolicyInducedDeleteInterval
		{
			get
			{
				return ContentAggregationConfig.owaMailboxPolicyInducedDeleteInterval;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00007878 File Offset: 0x00005A78
		internal static TimeSpan OwaMailboxPolicyProbeInterval
		{
			get
			{
				return ContentAggregationConfig.owaMailboxPolicyProbeInterval;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000787F File Offset: 0x00005A7F
		internal static byte AggregationSubscriptionSavedSyncWeight
		{
			get
			{
				return ContentAggregationConfig.aggregationSubscriptionSavedSyncWeight;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00007886 File Offset: 0x00005A86
		internal static byte AggregationIncrementalSyncWeight
		{
			get
			{
				return ContentAggregationConfig.aggregationIncrementalSyncWeight;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000788D File Offset: 0x00005A8D
		internal static byte OwaLogonTriggeredSyncWeight
		{
			get
			{
				return ContentAggregationConfig.owaLogonTriggeredSyncWeight;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00007894 File Offset: 0x00005A94
		internal static byte OwaRefreshButtonTriggeredSyncWeight
		{
			get
			{
				return ContentAggregationConfig.owaRefreshButtonTriggeredSyncWeight;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000789B File Offset: 0x00005A9B
		internal static byte OwaSessionTriggeredSyncWeight
		{
			get
			{
				return ContentAggregationConfig.owaSessionTriggeredSyncWeight;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000078A2 File Offset: 0x00005AA2
		internal static byte MigrationInitialSyncWeight
		{
			get
			{
				return ContentAggregationConfig.migrationInitialSyncWeight;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000078A9 File Offset: 0x00005AA9
		internal static byte AggregationInitialSyncWeight
		{
			get
			{
				return ContentAggregationConfig.aggregationInitialSyncWeight;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000078B0 File Offset: 0x00005AB0
		internal static byte MigrationFinalizationSyncWeight
		{
			get
			{
				return ContentAggregationConfig.migrationFinalizationSyncWeight;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000078B7 File Offset: 0x00005AB7
		internal static byte MigrationIncrementalSyncWeight
		{
			get
			{
				return ContentAggregationConfig.migrationIncrementalSyncWeight;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000078BE File Offset: 0x00005ABE
		internal static byte PeopleConnectionInitialSyncWeight
		{
			get
			{
				return ContentAggregationConfig.peopleConnectionInitialSyncWeight;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000078C5 File Offset: 0x00005AC5
		internal static byte PeopleConnectionTriggeredSyncWeight
		{
			get
			{
				return ContentAggregationConfig.peopleConnectionTriggeredSyncWeight;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000078CC File Offset: 0x00005ACC
		internal static byte PeopleConnectionIncrementalSyncWeight
		{
			get
			{
				return ContentAggregationConfig.peopleConnectionIncrementalSyncWeight;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000078D3 File Offset: 0x00005AD3
		internal static byte OwaMailboxPolicyInducedDeleteWeight
		{
			get
			{
				return ContentAggregationConfig.owaMailboxPolicyInducedDeleteWeight;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000078DA File Offset: 0x00005ADA
		internal static TimeSpan DispatchEntryExpirationTime
		{
			get
			{
				return ContentAggregationConfig.dispatchEntryExpirationTime;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000078E1 File Offset: 0x00005AE1
		internal static TimeSpan DispatchEntryExpirationCheckFrequency
		{
			get
			{
				return ContentAggregationConfig.dispatchEntryExpirationCheckFrequency;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000078E8 File Offset: 0x00005AE8
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000078EF File Offset: 0x00005AEF
		internal static TimeSpan DatabasePollingInterval
		{
			get
			{
				return ContentAggregationConfig.databasePollingInterval;
			}
			set
			{
				ContentAggregationConfig.databasePollingInterval = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000078F7 File Offset: 0x00005AF7
		internal static TimeSpan PrimingDispatchTime
		{
			get
			{
				return ContentAggregationConfig.primingDispatchTime;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000078FE File Offset: 0x00005AFE
		internal static TimeSpan MigrationInitialSyncInterval
		{
			get
			{
				return ContentAggregationConfig.migrationInitialSyncInterval;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00007905 File Offset: 0x00005B05
		internal static TimeSpan AggregationInitialSyncInterval
		{
			get
			{
				return ContentAggregationConfig.aggregationInitialSyncInterval;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000790C File Offset: 0x00005B0C
		internal static TimeSpan SyncNowTime
		{
			get
			{
				return ContentAggregationConfig.syncNowTime;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00007913 File Offset: 0x00005B13
		internal static TimeSpan OwaTriggeredSyncNowTime
		{
			get
			{
				return ContentAggregationConfig.owaTriggeredSyncNowTime;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000791A File Offset: 0x00005B1A
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00007921 File Offset: 0x00005B21
		internal static TimeSpan MailboxTablePollingInterval
		{
			get
			{
				return ContentAggregationConfig.mailboxTablePollingInterval;
			}
			set
			{
				ContentAggregationConfig.mailboxTablePollingInterval = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007929 File Offset: 0x00005B29
		internal static TimeSpan MailboxTableRetryPollingInterval
		{
			get
			{
				return ContentAggregationConfig.mailboxTableRetryPollingInterval;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00007930 File Offset: 0x00005B30
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00007937 File Offset: 0x00005B37
		internal static TimeSpan MailboxTableTwoWayPollingInterval
		{
			get
			{
				return ContentAggregationConfig.mailboxTableTwoWayPollingInterval;
			}
			set
			{
				ContentAggregationConfig.mailboxTableTwoWayPollingInterval = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000793F File Offset: 0x00005B3F
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00007946 File Offset: 0x00005B46
		internal static TimeSpan DelayBeforeMailboxTablePollingStarts
		{
			get
			{
				return ContentAggregationConfig.delayBeforeMailboxTablePollingStarts;
			}
			set
			{
				ContentAggregationConfig.delayBeforeMailboxTablePollingStarts = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000794E File Offset: 0x00005B4E
		internal static bool PopAggregationEnabled
		{
			get
			{
				return ContentAggregationConfig.popAggregationEnabled;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00007955 File Offset: 0x00005B55
		internal static bool ImapAggregationEnabled
		{
			get
			{
				return ContentAggregationConfig.imapAggregationEnabled;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000795C File Offset: 0x00005B5C
		internal static TimeSpan DispatcherDatabaseRefreshFrequency
		{
			get
			{
				return ContentAggregationConfig.dispatcherDatabaseRefreshFrequency;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00007963 File Offset: 0x00005B63
		internal static bool FacebookAggregationEnabled
		{
			get
			{
				return ContentAggregationConfig.facebookAggregationEnabled;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000796A File Offset: 0x00005B6A
		internal static bool DeltaSyncAggregationEnabled
		{
			get
			{
				return ContentAggregationConfig.deltaSyncAggregationEnabled;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00007971 File Offset: 0x00005B71
		internal static bool LinkedInAggregationEnabled
		{
			get
			{
				return ContentAggregationConfig.linkedInAggregationEnabled;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00007978 File Offset: 0x00005B78
		internal static bool OwaMailboxPolicyConstraintEnabled
		{
			get
			{
				return ContentAggregationConfig.owaMailboxPolicyConstraintEnabled;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00007980 File Offset: 0x00005B80
		internal static AggregationSubscriptionType SubscriptionTypesAllowed
		{
			get
			{
				AggregationSubscriptionType aggregationSubscriptionType = AggregationSubscriptionType.All;
				if (!ContentAggregationConfig.PopAggregationEnabled)
				{
					aggregationSubscriptionType &= ~AggregationSubscriptionType.Pop;
				}
				if (!ContentAggregationConfig.ImapAggregationEnabled)
				{
					aggregationSubscriptionType &= ~AggregationSubscriptionType.IMAP;
				}
				if (!ContentAggregationConfig.DeltaSyncAggregationEnabled)
				{
					aggregationSubscriptionType &= ~AggregationSubscriptionType.DeltaSyncMail;
				}
				if (!ContentAggregationConfig.FacebookAggregationEnabled)
				{
					aggregationSubscriptionType &= ~AggregationSubscriptionType.Facebook;
				}
				if (!ContentAggregationConfig.LinkedInAggregationEnabled)
				{
					aggregationSubscriptionType &= ~AggregationSubscriptionType.LinkedIn;
				}
				return aggregationSubscriptionType;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000079D0 File Offset: 0x00005BD0
		internal static TimeSpan HubBusyPeriod
		{
			get
			{
				return ContentAggregationConfig.hubBusyPeriod;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000079D7 File Offset: 0x00005BD7
		internal static TimeSpan HubSubscriptionTypeNotSupportedPeriod
		{
			get
			{
				return ContentAggregationConfig.hubSubscriptionTypeNotSupportedPeriod;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000079DE File Offset: 0x00005BDE
		internal static TimeSpan DatabaseBackoffTime
		{
			get
			{
				return ContentAggregationConfig.databaseBackoffTime;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000079E5 File Offset: 0x00005BE5
		internal static TimeSpan MinimumDispatchWaitForFailedSync
		{
			get
			{
				return ContentAggregationConfig.minimumDispatchWaitForFailedSync;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000079EC File Offset: 0x00005BEC
		internal static TimeSpan WorkTypeBudgetManagerSlidingWindowLength
		{
			get
			{
				return ContentAggregationConfig.workTypeBudgetManagerSlidingWindowLength;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000079F3 File Offset: 0x00005BF3
		internal static TimeSpan WorkTypeBudgetManagerSlidingBucketLength
		{
			get
			{
				return ContentAggregationConfig.workTypeBudgetManagerSlidingBucketLength;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000079FA File Offset: 0x00005BFA
		internal static TimeSpan WorkTypeBudgetManagerSampleDispatchedWorkFrequency
		{
			get
			{
				return ContentAggregationConfig.workTypeBudgetManagerSampleDispatchedWorkFrequency;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00007A01 File Offset: 0x00005C01
		internal static TimeSpan HubInactivityPeriod
		{
			get
			{
				return ContentAggregationConfig.hubInactivityPeriod;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00007A08 File Offset: 0x00005C08
		internal static int MaxCompletionThreads
		{
			get
			{
				return ContentAggregationConfig.maxCompletionThreads;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00007A0F File Offset: 0x00005C0F
		internal static int MaxCacheRpcThreads
		{
			get
			{
				return ContentAggregationConfig.maxCacheRpcThreads;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00007A16 File Offset: 0x00005C16
		internal static int MaxNotificationThreads
		{
			get
			{
				return ContentAggregationConfig.maxNotificationThreads;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00007A1D File Offset: 0x00005C1D
		internal static int MaxManualResetEventsInResourcePool
		{
			get
			{
				return ContentAggregationConfig.maxManualResetEventsInResourcePool;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00007A24 File Offset: 0x00005C24
		internal static int MaxMailboxSessionsInResourcePool
		{
			get
			{
				return ContentAggregationConfig.maxMailboxSessionsInResourcePool;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00007A2B File Offset: 0x00005C2B
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00007A32 File Offset: 0x00005C32
		internal static TimeSpan TokenWaitTimeOutInterval
		{
			get
			{
				return ContentAggregationConfig.tokenWaitTimeOutInterval;
			}
			set
			{
				ContentAggregationConfig.tokenWaitTimeOutInterval = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00007A3A File Offset: 0x00005C3A
		internal static TimeSpan DispatchOutageThreshold
		{
			get
			{
				return ContentAggregationConfig.dispatchOutageThreshold;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00007A41 File Offset: 0x00005C41
		internal static TimeSpan PoolBackOffTimeInterval
		{
			get
			{
				return ContentAggregationConfig.poolBackOffTimeInterval;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00007A48 File Offset: 0x00005C48
		internal static TimeSpan PoolExpiryCheckInterval
		{
			get
			{
				return ContentAggregationConfig.poolExpiryCheckInterval;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00007A4F File Offset: 0x00005C4F
		internal static TimeSpan MaxSystemMailboxSessionsUnusedPeriod
		{
			get
			{
				return ContentAggregationConfig.maxSystemMailboxSessionsUnusedPeriod;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00007A56 File Offset: 0x00005C56
		internal static int DispatcherBackOffTimeInSeconds
		{
			get
			{
				return ContentAggregationConfig.dispatcherBackOffTimeInSeconds;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00007A5D File Offset: 0x00005C5D
		internal static int MaxNumberOfAttemptsBeforePoolBackOff
		{
			get
			{
				return ContentAggregationConfig.maxNumberOfAttemptsBeforePoolBackOff;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00007A64 File Offset: 0x00005C64
		internal static TimeSpan SLAPerfCounterUpdateInterval
		{
			get
			{
				return ContentAggregationConfig.sLAPerfCounterUpdateInterval;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00007A6B File Offset: 0x00005C6B
		internal static int SLAExpiryBuckets
		{
			get
			{
				return ContentAggregationConfig.slaExpiryBuckets;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00007A72 File Offset: 0x00005C72
		internal static int SLADataBuckets
		{
			get
			{
				return ContentAggregationConfig.slaDataBuckets;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00007A79 File Offset: 0x00005C79
		internal static TimeSpan PCExpiryInterval
		{
			get
			{
				return ContentAggregationConfig.pCExpiryInterval;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00007A80 File Offset: 0x00005C80
		internal static int MaxSyncsPerDB
		{
			get
			{
				return ContentAggregationConfig.maxSyncsPerDB;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00007A87 File Offset: 0x00005C87
		internal static bool CacheRepairEnabled
		{
			get
			{
				return ContentAggregationConfig.cacheRepairEnabled;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00007A8E File Offset: 0x00005C8E
		internal static int MaxCacheMessageRepairAttempts
		{
			get
			{
				return ContentAggregationConfig.maxCacheMessageRepairAttempts;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00007A95 File Offset: 0x00005C95
		internal static TimeSpan DelayBeforeRepairThreadStarts
		{
			get
			{
				return ContentAggregationConfig.delayBeforeRepairThreadStarts;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00007A9C File Offset: 0x00005C9C
		internal static TimeSpan DelayBetweenDispatchQueueBuilds
		{
			get
			{
				return ContentAggregationConfig.delayBetweenDispatchQueueBuilds;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00007AA3 File Offset: 0x00005CA3
		internal static bool AggregationSubscriptionsEnabled
		{
			get
			{
				return ContentAggregationConfig.aggregationSubscriptionsEnabled;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00007AAA File Offset: 0x00005CAA
		internal static bool MigrationSubscriptionsEnabled
		{
			get
			{
				return ContentAggregationConfig.migrationSubscriptionsEnabled;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00007AB1 File Offset: 0x00005CB1
		internal static bool PeopleConnectionSubscriptionsEnabled
		{
			get
			{
				return ContentAggregationConfig.peopleConnectionSubscriptionsEnabled;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00007AB8 File Offset: 0x00005CB8
		internal static bool IsDatacenterMode
		{
			get
			{
				if (!ContentAggregationConfig.checkedDatacenterMode)
				{
					try
					{
						ContentAggregationConfig.datacenterMode = (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled || SyncUtilities.IsEnabledInEnterprise());
					}
					catch (CannotDetermineExchangeModeException ex)
					{
						ContentAggregationConfig.Tracer.TraceError<string>(0L, "Failed to determine the datacenter mode. Will Defaulting to Enterprise mode: {0}", ex.Message);
					}
					ContentAggregationConfig.checkedDatacenterMode = true;
				}
				return ContentAggregationConfig.datacenterMode;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00007B30 File Offset: 0x00005D30
		internal static int MaxDispatcherThreads
		{
			get
			{
				if (ContentAggregationConfig.localServer != null)
				{
					return ContentAggregationConfig.localServer.MaxTransportSyncDispatchers;
				}
				return 5;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00007B49 File Offset: 0x00005D49
		internal static bool TransportSyncDispatchEnabled
		{
			get
			{
				return ContentAggregationConfig.IsDatacenterMode && (ContentAggregationConfig.localServer == null || ContentAggregationConfig.localServer.TransportSyncDispatchEnabled);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00007B6B File Offset: 0x00005D6B
		internal static SyncHealthLogConfiguration SyncMailboxHealthLogConfiguration
		{
			get
			{
				if (ContentAggregationConfig.localServer != null)
				{
					return SyncHealthLogConfiguration.CreateSyncMailboxHealthLogConfiguration(ContentAggregationConfig.localServer);
				}
				return ContentAggregationConfig.defaultSyncMailboxHealthLogConfiguration;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00007B88 File Offset: 0x00005D88
		private static bool SyncLogEnabled
		{
			get
			{
				if (ContentAggregationConfig.localServer != null)
				{
					return ContentAggregationConfig.localServer.TransportSyncMailboxLogEnabled;
				}
				return ContentAggregationConfig.defaultSyncLogEnabled;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00007BA5 File Offset: 0x00005DA5
		private static SyncLoggingLevel SyncLogLoggingLevel
		{
			get
			{
				if (ContentAggregationConfig.localServer != null)
				{
					return ContentAggregationConfig.localServer.TransportSyncMailboxLogLoggingLevel;
				}
				return ContentAggregationConfig.defaultSyncLoggingLevel;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00007BC4 File Offset: 0x00005DC4
		private static string SyncLogFilePath
		{
			get
			{
				if (ContentAggregationConfig.localServer == null || ContentAggregationConfig.localServer.TransportSyncMailboxLogFilePath == null)
				{
					return ContentAggregationConfig.defaultRelativeSyncLogPath;
				}
				string text = ContentAggregationConfig.localServer.TransportSyncMailboxLogFilePath.ToString();
				if (string.IsNullOrEmpty(text))
				{
					return ContentAggregationConfig.defaultRelativeSyncLogPath;
				}
				return text;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00007C18 File Offset: 0x00005E18
		private static long SyncLogMaxAgeInHours
		{
			get
			{
				if (ContentAggregationConfig.localServer != null)
				{
					return (long)ContentAggregationConfig.localServer.TransportSyncMailboxLogMaxAge.TotalHours;
				}
				return 720L;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00007C4C File Offset: 0x00005E4C
		private static long SyncLogMaxDirectorySizeInKb
		{
			get
			{
				if (ContentAggregationConfig.localServer != null)
				{
					return (long)ContentAggregationConfig.localServer.TransportSyncMailboxLogMaxDirectorySize.ToKB();
				}
				return (long)ByteQuantifiedSize.FromGB(10UL).ToKB();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00007C88 File Offset: 0x00005E88
		private static long SyncLogMaxFileSizeInKb
		{
			get
			{
				if (ContentAggregationConfig.localServer != null)
				{
					return (long)ContentAggregationConfig.localServer.TransportSyncMailboxLogMaxFileSize.ToKB();
				}
				return 10240L;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007CBC File Offset: 0x00005EBC
		internal static bool Start(bool includeADConfig)
		{
			bool result;
			lock (ContentAggregationConfig.syncRoot)
			{
				ContentAggregationConfig.Tracer.TraceDebug(0L, "Loading initial configuration for subscription manager.");
				string exePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Microsoft.Exchange.TransportSyncManagerSvc.exe");
				ContentAggregationConfig.configuration = ConfigurationManager.OpenExeConfiguration(exePath);
				if (includeADConfig)
				{
					Exception ex;
					if (!ContentAggregationConfig.TryLoad(true, out ex))
					{
						ContentAggregationConfig.Tracer.TraceError<Exception>(0L, "Failed to load initial AD configuration for subscription manager {0}.", ex);
						ContentAggregationConfig.LogEvent(TransportSyncManagerEventLogConstants.Tuple_SyncManagerConfigLoadFailed, null, new object[]
						{
							ex
						});
						return false;
					}
					ContentAggregationConfig.Tracer.TraceDebug(0L, "Successfully loaded initial AD configuration for subscription manager.");
					ContentAggregationConfig.LogEvent(TransportSyncManagerEventLogConstants.Tuple_SyncManagerConfigLoadSucceeded, null, new object[0]);
				}
				ContentAggregationConfig.OpenGlobalLogSession();
				CommonLoggingHelper.SyncLogSession = ContentAggregationConfig.SyncLogSession;
				ContentAggregationConfig.LoadConfigSettings();
				ContentAggregationConfig.LogADConfigDetails();
				result = true;
			}
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007DAC File Offset: 0x00005FAC
		internal static void Shutdown()
		{
			lock (ContentAggregationConfig.eventHandlers)
			{
				ContentAggregationConfig.eventHandlers.Clear();
			}
			lock (ContentAggregationConfig.syncRoot)
			{
				if (ContentAggregationConfig.notificationCookie != null)
				{
					ADNotificationAdapter.UnregisterChangeNotification(ContentAggregationConfig.notificationCookie);
					ContentAggregationConfig.notificationCookie = null;
				}
			}
			ContentAggregationConfig.syncLogSession.LogDebugging((TSLID)81UL, ContentAggregationConfig.Tracer, 0L, "Shutdown completed for subscription manager configuration.", new object[0]);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007E54 File Offset: 0x00006054
		internal static void LogEvent(EventLogEntry eventLogEntry)
		{
			SyncUtilities.ThrowIfArgumentNull("eventLogEntry", eventLogEntry);
			ContentAggregationConfig.LogEvent(eventLogEntry.Tuple, eventLogEntry.PeriodicKey, eventLogEntry.MessageArgs);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007E7C File Offset: 0x0000607C
		internal static bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			if (ContentAggregationConfig.SyncLogSession != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (messageArgs != null && messageArgs.Length > 0)
				{
					foreach (object value in messageArgs)
					{
						stringBuilder.Append(value);
						stringBuilder.Append(';');
					}
				}
				uint num = tuple.EventId & 65535U;
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)82UL, "Logged {0} event {1} with periodic key [{2}], args: [{3}]", new object[]
				{
					tuple.EntryType,
					num,
					periodicKey,
					stringBuilder.ToString()
				});
			}
			return ContentAggregationConfig.eventLogger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007F34 File Offset: 0x00006134
		internal static void LoadConfigSettings()
		{
			ContentAggregationConfig.popAggregationEnabled = ContentAggregationConfig.GetConfigBool("PopAggregationEnabled", true);
			ContentAggregationConfig.deltaSyncAggregationEnabled = ContentAggregationConfig.GetConfigBool("DeltaSyncAggregationEnabled", true);
			ContentAggregationConfig.imapAggregationEnabled = ContentAggregationConfig.GetConfigBool("ImapAggregationEnabled", true);
			ContentAggregationConfig.facebookAggregationEnabled = ContentAggregationConfig.GetConfigBool("FacebookAggregationEnabled", true);
			ContentAggregationConfig.linkedInAggregationEnabled = ContentAggregationConfig.GetConfigBool("LinkedInAggregationEnabled", true);
			ContentAggregationConfig.owaMailboxPolicyConstraintEnabled = ContentAggregationConfig.GetConfigBool("OwaMailboxPolicyConstraintEnabled", true);
			ContentAggregationConfig.aggregationIncrementalSyncInterval = ContentAggregationConfig.GetConfigTimeSpan("AggregationIncrementalSyncInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(7.0), TimeSpan.FromHours(1.0));
			ContentAggregationConfig.migrationIncrementalSyncInterval = ContentAggregationConfig.GetConfigTimeSpan("MigrationIncrementalSyncInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(100.0), TimeSpan.FromDays(1.0));
			ContentAggregationConfig.peopleConnectionInitialSyncInterval = ContentAggregationConfig.GetConfigTimeSpan("PeopleConnectionInitialSyncInterval", TimeSpan.FromSeconds(0.0), TimeSpan.FromDays(100.0), TimeSpan.FromSeconds(0.0));
			ContentAggregationConfig.peopleConnectionTriggeredSyncInterval = ContentAggregationConfig.GetConfigTimeSpan("PeopleConnectionTriggeredSyncInterval", TimeSpan.FromSeconds(0.0), TimeSpan.FromDays(100.0), TimeSpan.FromSeconds(0.0));
			ContentAggregationConfig.peopleConnectionIncrementalSyncInterval = ContentAggregationConfig.GetConfigTimeSpan("PeopleConnectionIncrementalSyncInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(15.0), TimeSpan.FromHours(12.0));
			ContentAggregationConfig.owaMailboxPolicyInducedDeleteInterval = ContentAggregationConfig.GetConfigTimeSpan("OwaMailboxPolicyInducedDeleteInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(7.0), TimeSpan.FromHours(1.0));
			ContentAggregationConfig.owaMailboxPolicyProbeInterval = ContentAggregationConfig.GetConfigTimeSpan("OwaMailboxPolicyProbeInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(365.0), TimeSpan.FromDays(14.0));
			ContentAggregationConfig.dispatchEntryExpirationTime = ContentAggregationConfig.GetConfigTimeSpan("DispatchEntryExpirationTime", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(7.0), TimeSpan.FromMinutes(30.0));
			ContentAggregationConfig.dispatchEntryExpirationCheckFrequency = ContentAggregationConfig.GetConfigTimeSpan("DispatchEntryExpirationCheckFrequency", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(5.0));
			ContentAggregationConfig.primingDispatchTime = ContentAggregationConfig.GetConfigTimeSpan("PrimingDispatchTime", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(1.0), TimeSpan.FromSeconds(5.0));
			ContentAggregationConfig.migrationInitialSyncInterval = ContentAggregationConfig.GetConfigTimeSpan("MigrationInitialSyncInterval", TimeSpan.FromSeconds(0.0), TimeSpan.FromHours(24.0), TimeSpan.FromSeconds(0.0));
			ContentAggregationConfig.aggregationInitialSyncInterval = ContentAggregationConfig.GetConfigTimeSpan("AggregationInitialSyncInterval", TimeSpan.FromSeconds(0.0), TimeSpan.FromHours(24.0), TimeSpan.FromSeconds(0.0));
			ContentAggregationConfig.aggregationInitialSyncWeight = ContentAggregationConfig.GetConfigByte("AggregationInitialSyncWeight", 1, 100, 5);
			ContentAggregationConfig.aggregationSubscriptionSavedSyncWeight = ContentAggregationConfig.GetConfigByte("AggregationSubscriptionSavedSyncWeight", 1, 100, 2);
			ContentAggregationConfig.aggregationIncrementalSyncWeight = ContentAggregationConfig.GetConfigByte("AggregationIncrementalSyncWeight", 1, 100, 18);
			ContentAggregationConfig.migrationInitialSyncWeight = ContentAggregationConfig.GetConfigByte("MigrationInitialSyncWeight", 1, 100, 16);
			ContentAggregationConfig.migrationIncrementalSyncWeight = ContentAggregationConfig.GetConfigByte("MigrationIncrementalSyncWeight", 1, 100, 1);
			ContentAggregationConfig.migrationFinalizationSyncWeight = ContentAggregationConfig.GetConfigByte("MigrationFinalizationSyncWeight", 1, 100, 17);
			ContentAggregationConfig.owaLogonTriggeredSyncWeight = ContentAggregationConfig.GetConfigByte("OwaLogonTriggeredSyncWeight", 1, 100, 2);
			ContentAggregationConfig.owaRefreshButtonTriggeredSyncWeight = ContentAggregationConfig.GetConfigByte("OwaRefreshButtonTriggeredSyncWeight", 1, 100, 4);
			ContentAggregationConfig.owaSessionTriggeredSyncWeight = ContentAggregationConfig.GetConfigByte("OwaSessionTriggeredSyncWeight", 1, 100, 2);
			ContentAggregationConfig.owaTriggeredSyncNowTime = ContentAggregationConfig.GetConfigTimeSpan("OwaTriggeredSyncNowTime", TimeSpan.FromSeconds(0.0), TimeSpan.FromHours(24.0), TimeSpan.FromSeconds(0.0));
			ContentAggregationConfig.peopleConnectionInitialSyncWeight = ContentAggregationConfig.GetConfigByte("PeopleConnectionInitialSyncWeight", 1, 100, 20);
			ContentAggregationConfig.peopleConnectionTriggeredSyncWeight = ContentAggregationConfig.GetConfigByte("PeopleConnectionTriggerSyncWeight", 1, 100, 3);
			ContentAggregationConfig.peopleConnectionIncrementalSyncWeight = ContentAggregationConfig.GetConfigByte("PeopleConnectionIncrementalSyncWeight", 1, 100, 8);
			ContentAggregationConfig.owaMailboxPolicyInducedDeleteWeight = ContentAggregationConfig.GetConfigByte("OwaMailboxPolicyInducedDeleteWeight", 1, 100, 2);
			ContentAggregationConfig.syncNowTime = ContentAggregationConfig.GetConfigTimeSpan("SyncNowTime", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(24.0), TimeSpan.FromSeconds(1.0));
			ContentAggregationConfig.databasePollingInterval = ContentAggregationConfig.GetConfigTimeSpan("DatabasePollingInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(5.0));
			ContentAggregationConfig.mailboxTablePollingInterval = ContentAggregationConfig.GetConfigTimeSpan("MailboxTablePollingInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(45.0));
			ContentAggregationConfig.mailboxTableRetryPollingInterval = ContentAggregationConfig.GetConfigTimeSpan("MailboxTableRetryPollingInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(5.0));
			ContentAggregationConfig.mailboxTableTwoWayPollingInterval = ContentAggregationConfig.GetConfigTimeSpan("MailboxTableTwoWayPollingInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(1.0), TimeSpan.FromHours(24.0));
			ContentAggregationConfig.DelayBeforeMailboxTablePollingStarts = ContentAggregationConfig.GetConfigTimeSpan("DelayBeforeMailboxTablePollingStarts", TimeSpan.FromSeconds(0.0), TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(15.0));
			ContentAggregationConfig.hubBusyPeriod = ContentAggregationConfig.GetConfigTimeSpan("HubBusyPeriod", TimeSpan.Zero, TimeSpan.FromDays(1.0), TimeSpan.FromSeconds(5.0));
			ContentAggregationConfig.hubInactivityPeriod = ContentAggregationConfig.GetConfigTimeSpan("HubInactivityPeriod", TimeSpan.Zero, TimeSpan.FromDays(1.0), TimeSpan.FromSeconds(15.0));
			ContentAggregationConfig.hubSubscriptionTypeNotSupportedPeriod = ContentAggregationConfig.GetConfigTimeSpan("HubSubscriptionTypeNotSupportedPeriod", TimeSpan.Zero, TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(1.0));
			ContentAggregationConfig.databaseBackoffTime = ContentAggregationConfig.GetConfigTimeSpan("DatabaseBackoffTime", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(1.0));
			ContentAggregationConfig.minimumDispatchWaitForFailedSync = ContentAggregationConfig.GetConfigTimeSpan("MinimumDispatchWaitForFailedSync", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(7.0), TimeSpan.FromMinutes(5.0));
			ContentAggregationConfig.workTypeBudgetManagerSlidingWindowLength = ContentAggregationConfig.GetConfigTimeSpan("WorkTypeBudgetManagerSlidingWindowLength", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(24.0), TimeSpan.FromMinutes(10.0));
			ContentAggregationConfig.workTypeBudgetManagerSlidingBucketLength = ContentAggregationConfig.GetConfigTimeSpan("WorkTypeBudgetManagerSlidingBucketLength", TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(60.0), TimeSpan.FromSeconds(5.0));
			ContentAggregationConfig.workTypeBudgetManagerSampleDispatchedWorkFrequency = ContentAggregationConfig.GetConfigTimeSpan("WorkTypeBudgetManagerSampleDispatchedWorkFrequency", TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(60.0), TimeSpan.FromSeconds(1.0));
			ContentAggregationConfig.maxCompletionThreads = ContentAggregationConfig.GetConfigInt("MaxCompletionThreads", 1, 1024, 16);
			ContentAggregationConfig.maxCacheRpcThreads = ContentAggregationConfig.GetConfigInt("MaxCacheRpcThreads", 1, 128, 4);
			ContentAggregationConfig.maxNotificationThreads = ContentAggregationConfig.GetConfigInt("MaxNotificationThreads", 1, 128, 4);
			ContentAggregationConfig.maxManualResetEventsInResourcePool = ContentAggregationConfig.GetConfigInt("MaxManualResetEventsInResourcePool", 0, 262144, 1024);
			ContentAggregationConfig.maxMailboxSessionsInResourcePool = ContentAggregationConfig.GetConfigInt("MaxMailboxSessionsInResourcePool", 0, 262144, 1024);
			ContentAggregationConfig.tokenWaitTimeOutInterval = ContentAggregationConfig.GetConfigTimeSpan("TokenWaitTimeOutInterval", TimeSpan.Zero, TimeSpan.FromDays(365.0), TimeSpan.FromMinutes(30.0));
			ContentAggregationConfig.dispatchOutageThreshold = ContentAggregationConfig.GetConfigTimeSpan("DispatchOutageThreshold", TimeSpan.Zero, TimeSpan.FromDays(365.0), TimeSpan.FromHours(1.0));
			ContentAggregationConfig.poolBackOffTimeInterval = ContentAggregationConfig.GetConfigTimeSpan("PoolBackOffTimeInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(5.0));
			ContentAggregationConfig.poolExpiryCheckInterval = ContentAggregationConfig.GetConfigTimeSpan("PoolExpiryCheckInterval", TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(1.0));
			ContentAggregationConfig.maxSystemMailboxSessionsUnusedPeriod = ContentAggregationConfig.GetConfigTimeSpan("MaxSystemMailboxSessionsUnusedPeriod", TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(5.0));
			ContentAggregationConfig.dispatcherBackOffTimeInSeconds = ContentAggregationConfig.GetConfigInt("DispatcherBackOffTimeInSeconds", 1, 60, 5);
			ContentAggregationConfig.maxNumberOfAttemptsBeforePoolBackOff = ContentAggregationConfig.GetConfigInt("MaxNumberOfAttemptsBeforePoolBackOff", 1, 60, 3);
			ContentAggregationConfig.sLAPerfCounterUpdateInterval = ContentAggregationConfig.GetConfigTimeSpan("SLAPerfCounterUpdateInterval", TimeSpan.FromMinutes(1.0), TimeSpan.FromDays(7.0), TimeSpan.FromMinutes(1.0));
			ContentAggregationConfig.slaExpiryBuckets = ContentAggregationConfig.GetConfigInt("SLAExpiryBuckets", 1, 50, 5);
			ContentAggregationConfig.slaDataBuckets = ContentAggregationConfig.GetConfigInt("SLADataBuckets", 1, 1000, 100);
			ContentAggregationConfig.pCExpiryInterval = ContentAggregationConfig.GetConfigTimeSpan("PercentileCountersExpiryInterval", TimeSpan.FromMinutes(1.0), TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(5.0));
			ContentAggregationConfig.maxSyncsPerDB = ContentAggregationConfig.GetConfigInt("MaxSyncsPerDB", 0, 1000, 40);
			ContentAggregationConfig.cacheRepairEnabled = ContentAggregationConfig.GetConfigBool("CacheRepairEnabled", true);
			ContentAggregationConfig.maxCacheMessageRepairAttempts = ContentAggregationConfig.GetConfigInt("MaxCacheMessageRepairAttempts", 1, 1000, 5);
			ContentAggregationConfig.delayBeforeRepairThreadStarts = ContentAggregationConfig.GetConfigTimeSpan("DelayBeforeRepairThreadStarts", TimeSpan.FromMilliseconds(1.0), TimeSpan.FromDays(7.0), TimeSpan.FromSeconds(30.0));
			ContentAggregationConfig.delayBetweenDispatchQueueBuilds = ContentAggregationConfig.GetConfigTimeSpan("DelayBetweenDispatchQueueBuilds", TimeSpan.FromMilliseconds(1.0), TimeSpan.FromDays(7.0), TimeSpan.FromSeconds(30.0));
			ContentAggregationConfig.dispatcherDatabaseRefreshFrequency = ContentAggregationConfig.GetConfigTimeSpan("DispatcherDatabaseRefreshFrequency", TimeSpan.FromMilliseconds(1.0), TimeSpan.FromDays(7.0), TimeSpan.FromMinutes(1.0));
			ContentAggregationConfig.aggregationSubscriptionsEnabled = ContentAggregationConfig.GetConfigBool("AggregationSubscriptionsEnabled", true);
			ContentAggregationConfig.migrationSubscriptionsEnabled = ContentAggregationConfig.GetConfigBool("MigrationSubscriptionsEnabled", true);
			ContentAggregationConfig.peopleConnectionSubscriptionsEnabled = ContentAggregationConfig.GetConfigBool("PeopleConnectionSubscriptionsEnabled", true);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000089E0 File Offset: 0x00006BE0
		private static void LogADConfigDetails()
		{
			ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)464UL, "Config {0}:{1}.", new object[]
			{
				ServerSchema.TransportSyncDispatchEnabled.Name,
				ContentAggregationConfig.TransportSyncDispatchEnabled.ToString()
			});
			ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)516UL, "Config {0}:{1}.", new object[]
			{
				ServerSchema.MaxTransportSyncDispatchers.Name,
				ContentAggregationConfig.MaxDispatcherThreads.ToString()
			});
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008A6C File Offset: 0x00006C6C
		private static void LogSetting<T>(string label, T min, T max, T @default, T actual)
		{
			ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)83UL, "Config {0}:{1}.", new object[]
			{
				label,
				actual
			});
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008AA8 File Offset: 0x00006CA8
		private static void LogSetting<T>(string label, T @default, T actual)
		{
			ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)84UL, "Config {0}:{1}.", new object[]
			{
				label,
				actual
			});
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008AE4 File Offset: 0x00006CE4
		private static string GetAppSetting(string label)
		{
			if (ContentAggregationConfig.configuration.AppSettings.Settings[label] == null)
			{
				return null;
			}
			return ContentAggregationConfig.configuration.AppSettings.Settings[label].Value;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00008B26 File Offset: 0x00006D26
		private static byte GetConfigByte(string label, byte min, byte max, byte defaultValue)
		{
			return (byte)ContentAggregationConfig.GetConfigInt(label, (int)min, (int)max, (int)defaultValue);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00008B34 File Offset: 0x00006D34
		private static int GetConfigInt(string label, int min, int max, int defaultValue)
		{
			string text = null;
			try
			{
				text = ContentAggregationConfig.GetAppSetting(label);
			}
			catch (ConfigurationErrorsException)
			{
			}
			int num = defaultValue;
			if (string.IsNullOrEmpty(text) || !int.TryParse(text, out num) || num < min || num > max)
			{
				num = defaultValue;
			}
			ContentAggregationConfig.LogSetting<int>(label, min, max, defaultValue, num);
			return num;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008B88 File Offset: 0x00006D88
		private static decimal GetConfigDecimal(string label, decimal min, decimal max, decimal defaultValue)
		{
			string text = null;
			try
			{
				text = ContentAggregationConfig.GetAppSetting(label);
			}
			catch (ConfigurationErrorsException)
			{
			}
			decimal num = defaultValue;
			if (string.IsNullOrEmpty(text) || !decimal.TryParse(text, out num) || num < min || num > max)
			{
				num = defaultValue;
			}
			ContentAggregationConfig.LogSetting<decimal>(label, min, max, defaultValue, num);
			return num;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00008BE8 File Offset: 0x00006DE8
		private static bool GetConfigBool(string label, bool defaultValue)
		{
			string value = null;
			try
			{
				value = ContentAggregationConfig.GetAppSetting(label);
			}
			catch (ConfigurationErrorsException)
			{
			}
			bool flag = defaultValue;
			if (string.IsNullOrEmpty(value) || !bool.TryParse(value, out flag))
			{
				flag = defaultValue;
			}
			ContentAggregationConfig.LogSetting<bool>(label, defaultValue, flag);
			return flag;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00008C34 File Offset: 0x00006E34
		private static TimeSpan GetConfigTimeSpan(string label, TimeSpan min, TimeSpan max, TimeSpan defaultValue)
		{
			string text = null;
			try
			{
				text = ContentAggregationConfig.GetAppSetting(label);
			}
			catch (ConfigurationErrorsException)
			{
			}
			TimeSpan timeSpan = defaultValue;
			if (string.IsNullOrEmpty(text) || !TimeSpan.TryParse(text, out timeSpan) || timeSpan < min || timeSpan > max)
			{
				timeSpan = defaultValue;
			}
			ContentAggregationConfig.LogSetting<TimeSpan>(label, min, max, defaultValue, timeSpan);
			return timeSpan;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00008C94 File Offset: 0x00006E94
		private static float GetConfigFloat(string label, float min, float max, float defaultValue)
		{
			string text = null;
			try
			{
				text = ContentAggregationConfig.GetAppSetting(label);
			}
			catch (ConfigurationErrorsException)
			{
			}
			float num = defaultValue;
			if (string.IsNullOrEmpty(text) || !float.TryParse(text, out num) || num < min || num > max)
			{
				num = defaultValue;
			}
			ContentAggregationConfig.LogSetting<float>(label, min, max, defaultValue, num);
			return num;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00008CE8 File Offset: 0x00006EE8
		private static string GetConfigString(string label, string defaultValue)
		{
			string text = null;
			try
			{
				text = ContentAggregationConfig.GetAppSetting(label);
			}
			catch (ConfigurationErrorsException)
			{
			}
			if (string.IsNullOrEmpty(text))
			{
				text = defaultValue;
			}
			ContentAggregationConfig.LogSetting<string>(label, defaultValue, text);
			return text;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00008DB4 File Offset: 0x00006FB4
		private static bool TryLoad(bool subscribeForNotifications, out Exception exception)
		{
			exception = null;
			Server tmpLocalServer = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 2685, "TryLoad", "f:\\15.00.1497\\sources\\dev\\transportSync\\src\\Manager\\ContentAggregationConfig.cs");
				tmpLocalServer = topologyConfigurationSession.FindLocalServer();
				ContentAggregationConfig.Tracer.TraceDebug(0L, "Loaded local server object.");
				if (subscribeForNotifications)
				{
					ContentAggregationConfig.notificationCookie = ADNotificationAdapter.RegisterChangeNotification<Server>(tmpLocalServer.Id, new ADNotificationCallback(ContentAggregationConfig.HandleConfigurationChange));
					ContentAggregationConfig.Tracer.TraceDebug(0L, "Subscribed to AD change notifications for local server object.");
				}
			});
			if (adoperationResult.Succeeded)
			{
				ContentAggregationConfig.localServer = tmpLocalServer;
				return true;
			}
			exception = adoperationResult.Exception;
			ContentAggregationConfig.Tracer.TraceError(0L, "AD operation failed; details: {0}", new object[]
			{
				adoperationResult.Exception ?? "<null>"
			});
			return false;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00008E38 File Offset: 0x00007038
		private static void HandleConfigurationChange(ADNotificationEventArgs args)
		{
			Exception ex;
			bool flag2;
			lock (ContentAggregationConfig.syncRoot)
			{
				if (ContentAggregationConfig.eventHandlers == null)
				{
					return;
				}
				ContentAggregationConfig.syncLogSession.LogDebugging((TSLID)85UL, ContentAggregationConfig.Tracer, "Detected configuration change.", new object[0]);
				flag2 = ContentAggregationConfig.TryLoad(false, out ex);
				if (flag2)
				{
					ContentAggregationConfig.UpdateConfiguration();
				}
			}
			if (flag2)
			{
				ContentAggregationConfig.syncLogSession.LogDebugging((TSLID)86UL, ContentAggregationConfig.Tracer, "Successfully updated configuration; invoking custom handlers.", new object[0]);
				ContentAggregationConfig.InvokeEventHandlers();
				ContentAggregationConfig.LogEvent(TransportSyncManagerEventLogConstants.Tuple_SyncManagerConfigUpdateSucceeded, null, new object[0]);
				return;
			}
			ContentAggregationConfig.syncLogSession.LogError((TSLID)87UL, ContentAggregationConfig.Tracer, "Failed to updated configuration; continue using previous version or defaults. {0}", new object[]
			{
				ex
			});
			ContentAggregationConfig.LogEvent(TransportSyncManagerEventLogConstants.Tuple_SyncManagerConfigUpdateFailed, null, new object[0]);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00008F2C File Offset: 0x0000712C
		private static void InvokeEventHandlers()
		{
			ContentAggregationConfig.ConfigurationChangedEventHandler[] array;
			lock (ContentAggregationConfig.eventHandlers)
			{
				array = ContentAggregationConfig.eventHandlers.ToArray();
			}
			foreach (ContentAggregationConfig.ConfigurationChangedEventHandler configurationChangedEventHandler in array)
			{
				configurationChangedEventHandler();
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008F94 File Offset: 0x00007194
		private static void OpenGlobalLogSession()
		{
			if (ContentAggregationConfig.syncLog == null)
			{
				SyncLogConfiguration syncLogConfiguration = ContentAggregationConfig.LoadSyncLogConfig();
				ContentAggregationConfig.syncLog = new SyncLog(syncLogConfiguration);
				ContentAggregationConfig.syncLogSession = ContentAggregationConfig.syncLog.OpenGlobalSession();
				return;
			}
			ContentAggregationConfig.UpdateConfiguration();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00008FD0 File Offset: 0x000071D0
		private static SyncLogConfiguration LoadSyncLogConfig()
		{
			return new SyncLogConfiguration
			{
				Enabled = (ContentAggregationConfig.IsDatacenterMode && ContentAggregationConfig.SyncLogEnabled),
				LogFilePath = ContentAggregationConfig.SyncLogFilePath,
				AgeQuotaInHours = ContentAggregationConfig.SyncLogMaxAgeInHours,
				DirectorySizeQuota = ContentAggregationConfig.SyncLogMaxDirectorySizeInKb,
				PerFileSizeQuota = ContentAggregationConfig.SyncLogMaxFileSizeInKb,
				SyncLoggingLevel = ContentAggregationConfig.SyncLogLoggingLevel
			};
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009030 File Offset: 0x00007230
		private static void UpdateConfiguration()
		{
			ContentAggregationConfig.syncLog.ConfigureLog(ContentAggregationConfig.IsDatacenterMode && ContentAggregationConfig.SyncLogEnabled, ContentAggregationConfig.SyncLogFilePath, ContentAggregationConfig.SyncLogMaxAgeInHours, ContentAggregationConfig.SyncLogMaxDirectorySizeInKb, ContentAggregationConfig.SyncLogMaxFileSizeInKb, ContentAggregationConfig.SyncLogLoggingLevel);
		}

		// Token: 0x04000062 RID: 98
		public const string PopAggregationEnabledConfig = "PopAggregationEnabled";

		// Token: 0x04000063 RID: 99
		public const string DeltaSyncAggregationEnabledConfig = "DeltaSyncAggregationEnabled";

		// Token: 0x04000064 RID: 100
		public const string ImapAggregationEnabledConfig = "ImapAggregationEnabled";

		// Token: 0x04000065 RID: 101
		public const string FacebookAggregationEnabledConfig = "FacebookAggregationEnabled";

		// Token: 0x04000066 RID: 102
		public const string LinkedInAggregationEnabledConfig = "LinkedInAggregationEnabled";

		// Token: 0x04000067 RID: 103
		public const string OwaMailboxPolicyConstraintEnabledConfig = "OwaMailboxPolicyConstraintEnabled";

		// Token: 0x04000068 RID: 104
		public const string AggregationIncrementalSyncIntervalConfig = "AggregationIncrementalSyncInterval";

		// Token: 0x04000069 RID: 105
		public const string MigrationIncrementalSyncIntervalConfig = "MigrationIncrementalSyncInterval";

		// Token: 0x0400006A RID: 106
		public const string PeopleConnectionInitialSyncIntervalConfig = "PeopleConnectionInitialSyncInterval";

		// Token: 0x0400006B RID: 107
		public const string PeopleConnectionTriggeredSyncIntervalConfig = "PeopleConnectionTriggeredSyncInterval";

		// Token: 0x0400006C RID: 108
		public const string PeopleConnectionIncrementalSyncIntervalConfig = "PeopleConnectionIncrementalSyncInterval";

		// Token: 0x0400006D RID: 109
		public const string OwaMailboxPolicyInducedDeleteIntervalConfig = "OwaMailboxPolicyInducedDeleteInterval";

		// Token: 0x0400006E RID: 110
		public const string OwaMailboxPolicyProbeIntervalConfig = "OwaMailboxPolicyProbeInterval";

		// Token: 0x0400006F RID: 111
		public const string DispatchEntryExpirationTimeConfig = "DispatchEntryExpirationTime";

		// Token: 0x04000070 RID: 112
		public const string DispatchEntryExpirationCheckFrequencyConfig = "DispatchEntryExpirationCheckFrequency";

		// Token: 0x04000071 RID: 113
		public const string PrimingDispatchTimeConfig = "PrimingDispatchTime";

		// Token: 0x04000072 RID: 114
		public const string MigrationInitialSyncIntervalConfig = "MigrationInitialSyncInterval";

		// Token: 0x04000073 RID: 115
		public const string AggregationInitialSyncIntervalConfig = "AggregationInitialSyncInterval";

		// Token: 0x04000074 RID: 116
		public const string AggregationInitialSyncWeightConfig = "AggregationInitialSyncWeight";

		// Token: 0x04000075 RID: 117
		public const string AggregationSubscriptionSavedSyncWeightConfig = "AggregationSubscriptionSavedSyncWeight";

		// Token: 0x04000076 RID: 118
		public const string AggregationIncrementalSyncWeightConfig = "AggregationIncrementalSyncWeight";

		// Token: 0x04000077 RID: 119
		public const string MigrationInitialSyncWeightConfig = "MigrationInitialSyncWeight";

		// Token: 0x04000078 RID: 120
		public const string MigrationIncrementalSyncWeightConfig = "MigrationIncrementalSyncWeight";

		// Token: 0x04000079 RID: 121
		public const string MigrationFinalizationSyncWeightConfig = "MigrationFinalizationSyncWeight";

		// Token: 0x0400007A RID: 122
		public const string SyncNowTimeConfig = "SyncNowTime";

		// Token: 0x0400007B RID: 123
		public const string DatabasePollingIntervalConfig = "DatabasePollingInterval";

		// Token: 0x0400007C RID: 124
		public const string MailboxTablePollingIntervalConfig = "MailboxTablePollingInterval";

		// Token: 0x0400007D RID: 125
		public const string MailboxTableRetryPollingIntervalConfig = "MailboxTableRetryPollingInterval";

		// Token: 0x0400007E RID: 126
		public const string MailboxTableTwoWayPollingIntervalConfig = "MailboxTableTwoWayPollingInterval";

		// Token: 0x0400007F RID: 127
		public const string DelayBeforeMailboxTablePollingStartsConfig = "DelayBeforeMailboxTablePollingStarts";

		// Token: 0x04000080 RID: 128
		public const string SubscriptionSyncTimeOutIntervalConfig = "SubscriptionSyncTimeOutInterval";

		// Token: 0x04000081 RID: 129
		public const string HubBusyPeriodConfig = "HubBusyPeriod";

		// Token: 0x04000082 RID: 130
		public const string HubInactivityPeriodConfig = "HubInactivityPeriod";

		// Token: 0x04000083 RID: 131
		public const string HubSubscriptionTypeNotSupportedPeriodConfig = "HubSubscriptionTypeNotSupportedPeriod";

		// Token: 0x04000084 RID: 132
		public const string DatabaseBackoffTimeConfig = "DatabaseBackoffTime";

		// Token: 0x04000085 RID: 133
		public const string MinimumDispatchWaitForFailedSyncConfig = "MinimumDispatchWaitForFailedSync";

		// Token: 0x04000086 RID: 134
		public const string WorkTypeBudgetManagerSlidingWindowLengthConfig = "WorkTypeBudgetManagerSlidingWindowLength";

		// Token: 0x04000087 RID: 135
		public const string WorkTypeBudgetManagerSlidingBucketLengthConfig = "WorkTypeBudgetManagerSlidingBucketLength";

		// Token: 0x04000088 RID: 136
		public const string WorkTypeBudgetManagerSampleDispatchedWorkFrequencyConfig = "WorkTypeBudgetManagerSampleDispatchedWorkFrequency";

		// Token: 0x04000089 RID: 137
		public const string MaxCompletionThreadsConfig = "MaxCompletionThreads";

		// Token: 0x0400008A RID: 138
		public const string MaxCacheRpcThreadsConfig = "MaxCacheRpcThreads";

		// Token: 0x0400008B RID: 139
		public const string MaxNotificationThreadsConfig = "MaxNotificationThreads";

		// Token: 0x0400008C RID: 140
		public const string MaxManualResetEventsInResourcePoolConfig = "MaxManualResetEventsInResourcePool";

		// Token: 0x0400008D RID: 141
		public const string MaxMailboxSessionsInResourcePoolConfig = "MaxMailboxSessionsInResourcePool";

		// Token: 0x0400008E RID: 142
		public const string TokenWaitTimeOutIntervalConfig = "TokenWaitTimeOutInterval";

		// Token: 0x0400008F RID: 143
		public const string DispatchOutageThresholdConfig = "DispatchOutageThreshold";

		// Token: 0x04000090 RID: 144
		public const string PoolBackOffTimeIntervalConfig = "PoolBackOffTimeInterval";

		// Token: 0x04000091 RID: 145
		public const string PoolExpiryCheckIntervalConfig = "PoolExpiryCheckInterval";

		// Token: 0x04000092 RID: 146
		public const string MaxSystemMailboxSessionsUnusedPeriodConfig = "MaxSystemMailboxSessionsUnusedPeriod";

		// Token: 0x04000093 RID: 147
		public const string DispatcherBackOffTimeInSecondsConfig = "DispatcherBackOffTimeInSeconds";

		// Token: 0x04000094 RID: 148
		public const string MaxNumberOfAttemptsBeforePoolBackOffConfig = "MaxNumberOfAttemptsBeforePoolBackOff";

		// Token: 0x04000095 RID: 149
		public const string SLAPerfCounterUpdateIntervalConfig = "SLAPerfCounterUpdateInterval";

		// Token: 0x04000096 RID: 150
		public const string SlaExpiryBucketsConfig = "SLAExpiryBuckets";

		// Token: 0x04000097 RID: 151
		public const string SlaDataBucketsConfig = "SLADataBuckets";

		// Token: 0x04000098 RID: 152
		public const string PCExpiryIntervalConfig = "PercentileCountersExpiryInterval";

		// Token: 0x04000099 RID: 153
		public const string MaxSyncsPerDBConfig = "MaxSyncsPerDB";

		// Token: 0x0400009A RID: 154
		public const string CacheRepairEnabledConfig = "CacheRepairEnabled";

		// Token: 0x0400009B RID: 155
		public const string MaxCacheMessageRepairAttemptsConfig = "MaxCacheMessageRepairAttempts";

		// Token: 0x0400009C RID: 156
		public const string DelayBeforeRepairThreadStartsConfig = "DelayBeforeRepairThreadStarts";

		// Token: 0x0400009D RID: 157
		public const string DelayBetweenDispatchQueueBuildsConfig = "DelayBetweenDispatchQueueBuilds";

		// Token: 0x0400009E RID: 158
		public const string DispatcherDatabaseRefreshFrequencyConfig = "DispatcherDatabaseRefreshFrequency";

		// Token: 0x0400009F RID: 159
		private const string ConfigDetailFormat = "Config {0}:{1}.";

		// Token: 0x040000A0 RID: 160
		private const string ServiceFileName = "Microsoft.Exchange.TransportSyncManagerSvc.exe";

		// Token: 0x040000A1 RID: 161
		private static readonly Trace Tracer = ExTraceGlobals.ContentAggregationConfigTracer;

		// Token: 0x040000A2 RID: 162
		private static readonly ExEventLog eventLogger = new ExEventLog(new Guid("{DF4B5565-53E9-4776-A824-185F22FB3CA6}"), "MSExchangeTransportSyncManager");

		// Token: 0x040000A3 RID: 163
		private static readonly string defaultRelativeSyncLogPath = "TransportRoles\\Logs\\SyncLog\\Mailbox";

		// Token: 0x040000A4 RID: 164
		private static readonly bool defaultSyncLogEnabled = false;

		// Token: 0x040000A5 RID: 165
		private static readonly SyncLoggingLevel defaultSyncLoggingLevel = SyncLoggingLevel.None;

		// Token: 0x040000A6 RID: 166
		private static readonly SyncHealthLogConfiguration defaultSyncMailboxHealthLogConfiguration = new SyncHealthLogConfiguration();

		// Token: 0x040000A7 RID: 167
		private static TimeSpan migrationInitialSyncInterval;

		// Token: 0x040000A8 RID: 168
		private static TimeSpan aggregationInitialSyncInterval;

		// Token: 0x040000A9 RID: 169
		private static TimeSpan aggregationIncrementalSyncInterval;

		// Token: 0x040000AA RID: 170
		private static TimeSpan migrationIncrementalSyncInterval;

		// Token: 0x040000AB RID: 171
		private static TimeSpan peopleConnectionInitialSyncInterval;

		// Token: 0x040000AC RID: 172
		private static TimeSpan peopleConnectionTriggeredSyncInterval;

		// Token: 0x040000AD RID: 173
		private static TimeSpan peopleConnectionIncrementalSyncInterval;

		// Token: 0x040000AE RID: 174
		private static TimeSpan owaMailboxPolicyInducedDeleteInterval;

		// Token: 0x040000AF RID: 175
		private static TimeSpan owaMailboxPolicyProbeInterval;

		// Token: 0x040000B0 RID: 176
		private static byte aggregationSubscriptionSavedSyncWeight;

		// Token: 0x040000B1 RID: 177
		private static byte aggregationIncrementalSyncWeight;

		// Token: 0x040000B2 RID: 178
		private static byte migrationInitialSyncWeight;

		// Token: 0x040000B3 RID: 179
		private static byte aggregationInitialSyncWeight;

		// Token: 0x040000B4 RID: 180
		private static byte migrationFinalizationSyncWeight;

		// Token: 0x040000B5 RID: 181
		private static byte migrationIncrementalSyncWeight;

		// Token: 0x040000B6 RID: 182
		private static byte owaLogonTriggeredSyncWeight;

		// Token: 0x040000B7 RID: 183
		private static byte owaRefreshButtonTriggeredSyncWeight;

		// Token: 0x040000B8 RID: 184
		private static byte owaSessionTriggeredSyncWeight;

		// Token: 0x040000B9 RID: 185
		private static byte peopleConnectionInitialSyncWeight;

		// Token: 0x040000BA RID: 186
		private static byte peopleConnectionTriggeredSyncWeight;

		// Token: 0x040000BB RID: 187
		private static byte peopleConnectionIncrementalSyncWeight;

		// Token: 0x040000BC RID: 188
		private static byte owaMailboxPolicyInducedDeleteWeight;

		// Token: 0x040000BD RID: 189
		private static TimeSpan dispatchEntryExpirationTime;

		// Token: 0x040000BE RID: 190
		private static TimeSpan dispatchEntryExpirationCheckFrequency;

		// Token: 0x040000BF RID: 191
		private static TimeSpan primingDispatchTime;

		// Token: 0x040000C0 RID: 192
		private static TimeSpan syncNowTime;

		// Token: 0x040000C1 RID: 193
		private static TimeSpan owaTriggeredSyncNowTime;

		// Token: 0x040000C2 RID: 194
		private static TimeSpan databasePollingInterval;

		// Token: 0x040000C3 RID: 195
		private static TimeSpan mailboxTablePollingInterval;

		// Token: 0x040000C4 RID: 196
		private static TimeSpan mailboxTableRetryPollingInterval;

		// Token: 0x040000C5 RID: 197
		private static TimeSpan mailboxTableTwoWayPollingInterval;

		// Token: 0x040000C6 RID: 198
		private static TimeSpan delayBeforeMailboxTablePollingStarts;

		// Token: 0x040000C7 RID: 199
		private static TimeSpan hubBusyPeriod;

		// Token: 0x040000C8 RID: 200
		private static TimeSpan hubInactivityPeriod;

		// Token: 0x040000C9 RID: 201
		private static TimeSpan hubSubscriptionTypeNotSupportedPeriod;

		// Token: 0x040000CA RID: 202
		private static TimeSpan databaseBackoffTime;

		// Token: 0x040000CB RID: 203
		private static TimeSpan minimumDispatchWaitForFailedSync;

		// Token: 0x040000CC RID: 204
		private static TimeSpan workTypeBudgetManagerSlidingWindowLength;

		// Token: 0x040000CD RID: 205
		private static TimeSpan workTypeBudgetManagerSlidingBucketLength;

		// Token: 0x040000CE RID: 206
		private static TimeSpan workTypeBudgetManagerSampleDispatchedWorkFrequency;

		// Token: 0x040000CF RID: 207
		private static int maxCompletionThreads;

		// Token: 0x040000D0 RID: 208
		private static int maxCacheRpcThreads;

		// Token: 0x040000D1 RID: 209
		private static int maxNotificationThreads;

		// Token: 0x040000D2 RID: 210
		private static int maxManualResetEventsInResourcePool;

		// Token: 0x040000D3 RID: 211
		private static int maxMailboxSessionsInResourcePool;

		// Token: 0x040000D4 RID: 212
		private static TimeSpan tokenWaitTimeOutInterval;

		// Token: 0x040000D5 RID: 213
		private static TimeSpan dispatchOutageThreshold;

		// Token: 0x040000D6 RID: 214
		private static TimeSpan poolBackOffTimeInterval;

		// Token: 0x040000D7 RID: 215
		private static TimeSpan poolExpiryCheckInterval;

		// Token: 0x040000D8 RID: 216
		private static TimeSpan maxSystemMailboxSessionsUnusedPeriod;

		// Token: 0x040000D9 RID: 217
		private static int dispatcherBackOffTimeInSeconds;

		// Token: 0x040000DA RID: 218
		private static int maxNumberOfAttemptsBeforePoolBackOff;

		// Token: 0x040000DB RID: 219
		private static TimeSpan sLAPerfCounterUpdateInterval;

		// Token: 0x040000DC RID: 220
		private static int slaExpiryBuckets;

		// Token: 0x040000DD RID: 221
		private static int slaDataBuckets;

		// Token: 0x040000DE RID: 222
		private static TimeSpan pCExpiryInterval;

		// Token: 0x040000DF RID: 223
		private static int maxSyncsPerDB;

		// Token: 0x040000E0 RID: 224
		private static bool cacheRepairEnabled;

		// Token: 0x040000E1 RID: 225
		private static int maxCacheMessageRepairAttempts;

		// Token: 0x040000E2 RID: 226
		private static TimeSpan delayBeforeRepairThreadStarts;

		// Token: 0x040000E3 RID: 227
		private static TimeSpan delayBetweenDispatchQueueBuilds;

		// Token: 0x040000E4 RID: 228
		private static bool popAggregationEnabled;

		// Token: 0x040000E5 RID: 229
		private static bool deltaSyncAggregationEnabled;

		// Token: 0x040000E6 RID: 230
		private static bool imapAggregationEnabled;

		// Token: 0x040000E7 RID: 231
		private static bool facebookAggregationEnabled;

		// Token: 0x040000E8 RID: 232
		private static bool linkedInAggregationEnabled;

		// Token: 0x040000E9 RID: 233
		private static bool owaMailboxPolicyConstraintEnabled;

		// Token: 0x040000EA RID: 234
		private static TimeSpan dispatcherDatabaseRefreshFrequency;

		// Token: 0x040000EB RID: 235
		private static bool aggregationSubscriptionsEnabled;

		// Token: 0x040000EC RID: 236
		private static bool migrationSubscriptionsEnabled;

		// Token: 0x040000ED RID: 237
		private static bool peopleConnectionSubscriptionsEnabled;

		// Token: 0x040000EE RID: 238
		private static bool datacenterMode;

		// Token: 0x040000EF RID: 239
		private static bool checkedDatacenterMode;

		// Token: 0x040000F0 RID: 240
		private static volatile Server localServer;

		// Token: 0x040000F1 RID: 241
		private static ADNotificationRequestCookie notificationCookie;

		// Token: 0x040000F2 RID: 242
		private static List<ContentAggregationConfig.ConfigurationChangedEventHandler> eventHandlers = new List<ContentAggregationConfig.ConfigurationChangedEventHandler>();

		// Token: 0x040000F3 RID: 243
		private static object syncRoot = new object();

		// Token: 0x040000F4 RID: 244
		private static SyncLog syncLog;

		// Token: 0x040000F5 RID: 245
		private static GlobalSyncLogSession syncLogSession;

		// Token: 0x040000F6 RID: 246
		private static Configuration configuration = null;

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x0600013B RID: 315
		internal delegate void ConfigurationChangedEventHandler();
	}
}
