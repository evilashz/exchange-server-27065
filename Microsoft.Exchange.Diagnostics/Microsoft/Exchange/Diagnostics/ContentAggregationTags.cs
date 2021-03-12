using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000219 RID: 537
	public struct ContentAggregationTags
	{
		// Token: 0x04000BC7 RID: 3015
		public const int Common = 0;

		// Token: 0x04000BC8 RID: 3016
		public const int Pop3Client = 1;

		// Token: 0x04000BC9 RID: 3017
		public const int DeltaSyncStorageProvider = 2;

		// Token: 0x04000BCA RID: 3018
		public const int FeedParser = 3;

		// Token: 0x04000BCB RID: 3019
		public const int ContentGeneration = 4;

		// Token: 0x04000BCC RID: 3020
		public const int SubscriptionSubmissionRpc = 5;

		// Token: 0x04000BCD RID: 3021
		public const int RssServerLock = 6;

		// Token: 0x04000BCE RID: 3022
		public const int SubscriptionSubmit = 7;

		// Token: 0x04000BCF RID: 3023
		public const int SubscriptionSubmissionServer = 8;

		// Token: 0x04000BD0 RID: 3024
		public const int Scheduler = 9;

		// Token: 0x04000BD1 RID: 3025
		public const int SubscriptionManager = 10;

		// Token: 0x04000BD2 RID: 3026
		public const int WebFeedProtocolHandler = 11;

		// Token: 0x04000BD3 RID: 3027
		public const int DeliveryAgent = 12;

		// Token: 0x04000BD4 RID: 3028
		public const int HtmlFixer = 13;

		// Token: 0x04000BD5 RID: 3029
		public const int Pop3ProtocolHandler = 14;

		// Token: 0x04000BD6 RID: 3030
		public const int Pop3StorageProvider = 15;

		// Token: 0x04000BD7 RID: 3031
		public const int SubscriptionTask = 17;

		// Token: 0x04000BD8 RID: 3032
		public const int SyncEngine = 18;

		// Token: 0x04000BD9 RID: 3033
		public const int TransportSyncStorageProvider = 19;

		// Token: 0x04000BDA RID: 3034
		public const int StateStorage = 20;

		// Token: 0x04000BDB RID: 3035
		public const int SyncLog = 21;

		// Token: 0x04000BDC RID: 3036
		public const int XSOSyncStorageProvider = 22;

		// Token: 0x04000BDD RID: 3037
		public const int SubscriptionEventbasedAssistant = 23;

		// Token: 0x04000BDE RID: 3038
		public const int CacheManager = 24;

		// Token: 0x04000BDF RID: 3039
		public const int CacheManagerLookup = 25;

		// Token: 0x04000BE0 RID: 3040
		public const int TokenManager = 26;

		// Token: 0x04000BE1 RID: 3041
		public const int SubscriptionCompletionServer = 27;

		// Token: 0x04000BE2 RID: 3042
		public const int SubscriptionCompletionClient = 28;

		// Token: 0x04000BE3 RID: 3043
		public const int EventLog = 29;

		// Token: 0x04000BE4 RID: 3044
		public const int ProtocolHandler = 30;

		// Token: 0x04000BE5 RID: 3045
		public const int AggregationComponent = 31;

		// Token: 0x04000BE6 RID: 3046
		public const int IMAPSyncStorageProvider = 32;

		// Token: 0x04000BE7 RID: 3047
		public const int IMAPClient = 33;

		// Token: 0x04000BE8 RID: 3048
		public const int FaultInjection = 34;

		// Token: 0x04000BE9 RID: 3049
		public const int DavClient = 35;

		// Token: 0x04000BEA RID: 3050
		public const int DavSyncStorageProvider = 36;

		// Token: 0x04000BEB RID: 3051
		public const int SyncPoisonHandler = 37;

		// Token: 0x04000BEC RID: 3052
		public const int NativeSyncStorageProvider = 38;

		// Token: 0x04000BED RID: 3053
		public const int SendAs = 39;

		// Token: 0x04000BEE RID: 3054
		public const int StatefulHubPicker = 40;

		// Token: 0x04000BEF RID: 3055
		public const int RemoteAccountPolicy = 41;

		// Token: 0x04000BF0 RID: 3056
		public const int DataAccessLayer = 42;

		// Token: 0x04000BF1 RID: 3057
		public const int SystemMailboxSessionPool = 43;

		// Token: 0x04000BF2 RID: 3058
		public const int SubscriptionCacheMessage = 44;

		// Token: 0x04000BF3 RID: 3059
		public const int SubscriptionQueue = 45;

		// Token: 0x04000BF4 RID: 3060
		public const int SubscriptionCacheRpcServer = 46;

		// Token: 0x04000BF5 RID: 3061
		public const int ContentAggregationConfig = 47;

		// Token: 0x04000BF6 RID: 3062
		public const int AggregationConfiguration = 48;

		// Token: 0x04000BF7 RID: 3063
		public const int SubscriptionAgentManager = 49;

		// Token: 0x04000BF8 RID: 3064
		public const int SyncHealthLogManager = 50;

		// Token: 0x04000BF9 RID: 3065
		public const int TransportSyncManagerSvc = 51;

		// Token: 0x04000BFA RID: 3066
		public const int GlobalDatabaseHandler = 52;

		// Token: 0x04000BFB RID: 3067
		public const int DatabaseManager = 53;

		// Token: 0x04000BFC RID: 3068
		public const int MailboxManager = 54;

		// Token: 0x04000BFD RID: 3069
		public const int MailboxTableManager = 55;

		// Token: 0x04000BFE RID: 3070
		public const int SubscriptionNotificationServer = 56;

		// Token: 0x04000BFF RID: 3071
		public const int SubscriptionNotificationClient = 57;

		// Token: 0x04000C00 RID: 3072
		public const int FacebookProvider = 58;

		// Token: 0x04000C01 RID: 3073
		public const int LinkedInProvider = 59;

		// Token: 0x04000C02 RID: 3074
		public const int SubscriptionRemove = 60;

		// Token: 0x04000C03 RID: 3075
		public static Guid guid = new Guid("B29C4959-0C49-4bfa-BDDD-9B6E961420AC");
	}
}
