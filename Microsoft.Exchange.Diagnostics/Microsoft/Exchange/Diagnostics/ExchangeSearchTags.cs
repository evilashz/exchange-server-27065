using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000261 RID: 609
	public struct ExchangeSearchTags
	{
		// Token: 0x04000FCC RID: 4044
		public const int General = 0;

		// Token: 0x04000FCD RID: 4045
		public const int CatchUpNotificationCrawler = 1;

		// Token: 0x04000FCE RID: 4046
		public const int ChunkSourceFunctions = 2;

		// Token: 0x04000FCF RID: 4047
		public const int Crawler = 3;

		// Token: 0x04000FD0 RID: 4048
		public const int CSrchProject = 4;

		// Token: 0x04000FD1 RID: 4049
		public const int DataSourceFunctions = 5;

		// Token: 0x04000FD2 RID: 4050
		public const int Driver = 6;

		// Token: 0x04000FD3 RID: 4051
		public const int FilterEnumerator = 7;

		// Token: 0x04000FD4 RID: 4052
		public const int FTEAdminComInterop = 8;

		// Token: 0x04000FD5 RID: 4053
		public const int IndexablePropertyCache = 9;

		// Token: 0x04000FD6 RID: 4054
		public const int MapiIterator = 10;

		// Token: 0x04000FD7 RID: 4055
		public const int NotificationProcessing = 11;

		// Token: 0x04000FD8 RID: 4056
		public const int NotificationQueue = 12;

		// Token: 0x04000FD9 RID: 4057
		public const int NotificationWatcher = 13;

		// Token: 0x04000FDA RID: 4058
		public const int PHFunctions = 14;

		// Token: 0x04000FDB RID: 4059
		public const int RetryEngine = 16;

		// Token: 0x04000FDC RID: 4060
		public const int ThrottleController = 17;

		// Token: 0x04000FDD RID: 4061
		public const int PropertyStoreCache = 18;

		// Token: 0x04000FDE RID: 4062
		public const int ActiveManager = 19;

		// Token: 0x04000FDF RID: 4063
		public const int CatalogHealth = 20;

		// Token: 0x04000FE0 RID: 4064
		public const int SearchCatalogClient = 21;

		// Token: 0x04000FE1 RID: 4065
		public const int SearchCatalogServer = 22;

		// Token: 0x04000FE2 RID: 4066
		public const int MailboxDeletion = 23;

		// Token: 0x04000FE3 RID: 4067
		public const int PropertyStore = 24;

		// Token: 0x04000FE4 RID: 4068
		public const int StoreMonitor = 25;

		// Token: 0x04000FE5 RID: 4069
		public const int FaultInjection = 26;

		// Token: 0x04000FE6 RID: 4070
		public const int MailboxIndexingHelper = 27;

		// Token: 0x04000FE7 RID: 4071
		public const int CatalogState = 28;

		// Token: 0x04000FE8 RID: 4072
		public const int FileExtensionCache = 29;

		// Token: 0x04000FE9 RID: 4073
		public const int MsFteSqlMonitor = 30;

		// Token: 0x04000FEA RID: 4074
		public const int ServerConnections = 31;

		// Token: 0x04000FEB RID: 4075
		public const int LogonCache = 32;

		// Token: 0x04000FEC RID: 4076
		public const int Logon = 33;

		// Token: 0x04000FED RID: 4077
		public const int CatalogReconciler = 34;

		// Token: 0x04000FEE RID: 4078
		public const int CatalogReconcileResult = 35;

		// Token: 0x04000FEF RID: 4079
		public const int AllCatalogReconciler = 36;

		// Token: 0x04000FF0 RID: 4080
		public const int MailboxReconcileResult = 37;

		// Token: 0x04000FF1 RID: 4081
		public const int NewFilterMonitor = 38;

		// Token: 0x04000FF2 RID: 4082
		public const int InMemoryDefault = 39;

		// Token: 0x04000FF3 RID: 4083
		public const int TestExchangeSearch = 40;

		// Token: 0x04000FF4 RID: 4084
		public const int BatchThrottler = 41;

		// Token: 0x04000FF5 RID: 4085
		public const int ThrottleParameters = 42;

		// Token: 0x04000FF6 RID: 4086
		public const int ThrottleDataProvider = 43;

		// Token: 0x04000FF7 RID: 4087
		public const int RegistryParameter = 44;

		// Token: 0x04000FF8 RID: 4088
		public const int LatencySampler = 45;

		// Token: 0x04000FF9 RID: 4089
		public const int MovingAverage = 46;

		// Token: 0x04000FFA RID: 4090
		public const int CoreComponent = 50;

		// Token: 0x04000FFB RID: 4091
		public const int CoreComponentRegistry = 51;

		// Token: 0x04000FFC RID: 4092
		public const int CoreGeneral = 52;

		// Token: 0x04000FFD RID: 4093
		public const int FastFeeder = 53;

		// Token: 0x04000FFE RID: 4094
		public const int MdbNotificationsFeeder = 54;

		// Token: 0x04000FFF RID: 4095
		public const int Service = 55;

		// Token: 0x04001000 RID: 4096
		public const int Engine = 56;

		// Token: 0x04001001 RID: 4097
		public const int MdbFeedingController = 57;

		// Token: 0x04001002 RID: 4098
		public const int IndexManagement = 58;

		// Token: 0x04001003 RID: 4099
		public const int CoreFailureMonitor = 59;

		// Token: 0x04001004 RID: 4100
		public const int MdbCrawlerFeeder = 60;

		// Token: 0x04001005 RID: 4101
		public const int MdbDocumentAdapter = 61;

		// Token: 0x04001006 RID: 4102
		public const int CoreDocumentModel = 62;

		// Token: 0x04001007 RID: 4103
		public const int PipelineLoader = 63;

		// Token: 0x04001008 RID: 4104
		public const int CorePipeline = 64;

		// Token: 0x04001009 RID: 4105
		public const int QueueManager = 65;

		// Token: 0x0400100A RID: 4106
		public const int CrawlerWatermarkManager = 66;

		// Token: 0x0400100B RID: 4107
		public const int FailedItemStorage = 67;

		// Token: 0x0400100C RID: 4108
		public const int MdbWatcher = 68;

		// Token: 0x0400100D RID: 4109
		public const int MdbRetryFeeder = 69;

		// Token: 0x0400100E RID: 4110
		public const int MdbSessionCache = 70;

		// Token: 0x0400100F RID: 4111
		public const int RetrieverOperator = 71;

		// Token: 0x04001010 RID: 4112
		public const int StreamManager = 72;

		// Token: 0x04001011 RID: 4113
		public const int StreamChannel = 73;

		// Token: 0x04001012 RID: 4114
		public const int AnnotationToken = 74;

		// Token: 0x04001013 RID: 4115
		public const int TransportOperator = 75;

		// Token: 0x04001014 RID: 4116
		public const int IndexRoutingAgent = 76;

		// Token: 0x04001015 RID: 4117
		public const int IndexDeliveryAgent = 77;

		// Token: 0x04001016 RID: 4118
		public const int TransportFlowFeeder = 78;

		// Token: 0x04001017 RID: 4119
		public const int QueryExecutor = 79;

		// Token: 0x04001018 RID: 4120
		public const int ErrorOperator = 80;

		// Token: 0x04001019 RID: 4121
		public const int NotificationsWatermarkManager = 81;

		// Token: 0x0400101A RID: 4122
		public const int IndexStatusStore = 82;

		// Token: 0x0400101B RID: 4123
		public const int IndexStatusProvider = 83;

		// Token: 0x0400101C RID: 4124
		public const int FastIoExtension = 84;

		// Token: 0x0400101D RID: 4125
		public const int XSOMailboxSession = 85;

		// Token: 0x0400101E RID: 4126
		public const int PostDocParserOperator = 86;

		// Token: 0x0400101F RID: 4127
		public const int RecordManagerOperator = 87;

		// Token: 0x04001020 RID: 4128
		public const int OperatorDiagnostics = 88;

		// Token: 0x04001021 RID: 4129
		public const int SearchRpcClient = 89;

		// Token: 0x04001022 RID: 4130
		public const int SearchRpcServer = 90;

		// Token: 0x04001023 RID: 4131
		public const int DocumentTrackerOperator = 91;

		// Token: 0x04001024 RID: 4132
		public const int ErrorBypassOperator = 92;

		// Token: 0x04001025 RID: 4133
		public const int FeederThrottling = 93;

		// Token: 0x04001026 RID: 4134
		public const int WatermarkStorage = 94;

		// Token: 0x04001027 RID: 4135
		public const int DiagnosticOperator = 95;

		// Token: 0x04001028 RID: 4136
		public const int InstantSearch = 96;

		// Token: 0x04001029 RID: 4137
		public const int TopNManagementClient = 97;

		// Token: 0x0400102A RID: 4138
		public const int SearchDictionary = 98;

		// Token: 0x0400102B RID: 4139
		public static Guid guid = new Guid("c3ea5adf-c135-45e7-9dff-e1dc3bd67123");
	}
}
