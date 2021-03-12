using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ContentAggregation
{
	// Token: 0x0200031B RID: 795
	public static class ExTraceGlobals
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0004B5F5 File Offset: 0x000497F5
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0004B613 File Offset: 0x00049813
		public static Trace Pop3ClientTracer
		{
			get
			{
				if (ExTraceGlobals.pop3ClientTracer == null)
				{
					ExTraceGlobals.pop3ClientTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.pop3ClientTracer;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0004B631 File Offset: 0x00049831
		public static Trace DeltaSyncStorageProviderTracer
		{
			get
			{
				if (ExTraceGlobals.deltaSyncStorageProviderTracer == null)
				{
					ExTraceGlobals.deltaSyncStorageProviderTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.deltaSyncStorageProviderTracer;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0004B64F File Offset: 0x0004984F
		public static Trace FeedParserTracer
		{
			get
			{
				if (ExTraceGlobals.feedParserTracer == null)
				{
					ExTraceGlobals.feedParserTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.feedParserTracer;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0004B66D File Offset: 0x0004986D
		public static Trace ContentGenerationTracer
		{
			get
			{
				if (ExTraceGlobals.contentGenerationTracer == null)
				{
					ExTraceGlobals.contentGenerationTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.contentGenerationTracer;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x0004B68B File Offset: 0x0004988B
		public static Trace SubscriptionSubmissionRpcTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionSubmissionRpcTracer == null)
				{
					ExTraceGlobals.subscriptionSubmissionRpcTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.subscriptionSubmissionRpcTracer;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0004B6A9 File Offset: 0x000498A9
		public static Trace RssServerLockTracer
		{
			get
			{
				if (ExTraceGlobals.rssServerLockTracer == null)
				{
					ExTraceGlobals.rssServerLockTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.rssServerLockTracer;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x0004B6C7 File Offset: 0x000498C7
		public static Trace SubscriptionSubmitTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionSubmitTracer == null)
				{
					ExTraceGlobals.subscriptionSubmitTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.subscriptionSubmitTracer;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0004B6E5 File Offset: 0x000498E5
		public static Trace SubscriptionSubmissionServerTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionSubmissionServerTracer == null)
				{
					ExTraceGlobals.subscriptionSubmissionServerTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.subscriptionSubmissionServerTracer;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0004B703 File Offset: 0x00049903
		public static Trace SchedulerTracer
		{
			get
			{
				if (ExTraceGlobals.schedulerTracer == null)
				{
					ExTraceGlobals.schedulerTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.schedulerTracer;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0004B722 File Offset: 0x00049922
		public static Trace SubscriptionManagerTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionManagerTracer == null)
				{
					ExTraceGlobals.subscriptionManagerTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.subscriptionManagerTracer;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0004B741 File Offset: 0x00049941
		public static Trace WebFeedProtocolHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.webFeedProtocolHandlerTracer == null)
				{
					ExTraceGlobals.webFeedProtocolHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.webFeedProtocolHandlerTracer;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0004B760 File Offset: 0x00049960
		public static Trace DeliveryAgentTracer
		{
			get
			{
				if (ExTraceGlobals.deliveryAgentTracer == null)
				{
					ExTraceGlobals.deliveryAgentTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.deliveryAgentTracer;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x0004B77F File Offset: 0x0004997F
		public static Trace HtmlFixerTracer
		{
			get
			{
				if (ExTraceGlobals.htmlFixerTracer == null)
				{
					ExTraceGlobals.htmlFixerTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.htmlFixerTracer;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x0004B79E File Offset: 0x0004999E
		public static Trace Pop3ProtocolHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.pop3ProtocolHandlerTracer == null)
				{
					ExTraceGlobals.pop3ProtocolHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.pop3ProtocolHandlerTracer;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x0004B7BD File Offset: 0x000499BD
		public static Trace Pop3StorageProviderTracer
		{
			get
			{
				if (ExTraceGlobals.pop3StorageProviderTracer == null)
				{
					ExTraceGlobals.pop3StorageProviderTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.pop3StorageProviderTracer;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0004B7DC File Offset: 0x000499DC
		public static Trace SubscriptionTaskTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionTaskTracer == null)
				{
					ExTraceGlobals.subscriptionTaskTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.subscriptionTaskTracer;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x0004B7FB File Offset: 0x000499FB
		public static Trace SyncEngineTracer
		{
			get
			{
				if (ExTraceGlobals.syncEngineTracer == null)
				{
					ExTraceGlobals.syncEngineTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.syncEngineTracer;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0004B81A File Offset: 0x00049A1A
		public static Trace TransportSyncStorageProviderTracer
		{
			get
			{
				if (ExTraceGlobals.transportSyncStorageProviderTracer == null)
				{
					ExTraceGlobals.transportSyncStorageProviderTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.transportSyncStorageProviderTracer;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0004B839 File Offset: 0x00049A39
		public static Trace StateStorageTracer
		{
			get
			{
				if (ExTraceGlobals.stateStorageTracer == null)
				{
					ExTraceGlobals.stateStorageTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.stateStorageTracer;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0004B858 File Offset: 0x00049A58
		public static Trace SyncLogTracer
		{
			get
			{
				if (ExTraceGlobals.syncLogTracer == null)
				{
					ExTraceGlobals.syncLogTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.syncLogTracer;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0004B877 File Offset: 0x00049A77
		public static Trace XSOSyncStorageProviderTracer
		{
			get
			{
				if (ExTraceGlobals.xSOSyncStorageProviderTracer == null)
				{
					ExTraceGlobals.xSOSyncStorageProviderTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.xSOSyncStorageProviderTracer;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x0004B896 File Offset: 0x00049A96
		public static Trace SubscriptionEventbasedAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionEventbasedAssistantTracer == null)
				{
					ExTraceGlobals.subscriptionEventbasedAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.subscriptionEventbasedAssistantTracer;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x0004B8B5 File Offset: 0x00049AB5
		public static Trace CacheManagerTracer
		{
			get
			{
				if (ExTraceGlobals.cacheManagerTracer == null)
				{
					ExTraceGlobals.cacheManagerTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.cacheManagerTracer;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0004B8D4 File Offset: 0x00049AD4
		public static Trace CacheManagerLookupTracer
		{
			get
			{
				if (ExTraceGlobals.cacheManagerLookupTracer == null)
				{
					ExTraceGlobals.cacheManagerLookupTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.cacheManagerLookupTracer;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0004B8F3 File Offset: 0x00049AF3
		public static Trace TokenManagerTracer
		{
			get
			{
				if (ExTraceGlobals.tokenManagerTracer == null)
				{
					ExTraceGlobals.tokenManagerTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.tokenManagerTracer;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0004B912 File Offset: 0x00049B12
		public static Trace SubscriptionCompletionServerTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionCompletionServerTracer == null)
				{
					ExTraceGlobals.subscriptionCompletionServerTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.subscriptionCompletionServerTracer;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x0004B931 File Offset: 0x00049B31
		public static Trace SubscriptionCompletionClientTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionCompletionClientTracer == null)
				{
					ExTraceGlobals.subscriptionCompletionClientTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.subscriptionCompletionClientTracer;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0004B950 File Offset: 0x00049B50
		public static Trace EventLogTracer
		{
			get
			{
				if (ExTraceGlobals.eventLogTracer == null)
				{
					ExTraceGlobals.eventLogTracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.eventLogTracer;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0004B96F File Offset: 0x00049B6F
		public static Trace ProtocolHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.protocolHandlerTracer == null)
				{
					ExTraceGlobals.protocolHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.protocolHandlerTracer;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0004B98E File Offset: 0x00049B8E
		public static Trace AggregationComponentTracer
		{
			get
			{
				if (ExTraceGlobals.aggregationComponentTracer == null)
				{
					ExTraceGlobals.aggregationComponentTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.aggregationComponentTracer;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0004B9AD File Offset: 0x00049BAD
		public static Trace IMAPSyncStorageProviderTracer
		{
			get
			{
				if (ExTraceGlobals.iMAPSyncStorageProviderTracer == null)
				{
					ExTraceGlobals.iMAPSyncStorageProviderTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.iMAPSyncStorageProviderTracer;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x0004B9CC File Offset: 0x00049BCC
		public static Trace IMAPClientTracer
		{
			get
			{
				if (ExTraceGlobals.iMAPClientTracer == null)
				{
					ExTraceGlobals.iMAPClientTracer = new Trace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.iMAPClientTracer;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0004B9EB File Offset: 0x00049BEB
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x0004BA0A File Offset: 0x00049C0A
		public static Trace DavClientTracer
		{
			get
			{
				if (ExTraceGlobals.davClientTracer == null)
				{
					ExTraceGlobals.davClientTracer = new Trace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.davClientTracer;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0004BA29 File Offset: 0x00049C29
		public static Trace DavSyncStorageProviderTracer
		{
			get
			{
				if (ExTraceGlobals.davSyncStorageProviderTracer == null)
				{
					ExTraceGlobals.davSyncStorageProviderTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.davSyncStorageProviderTracer;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0004BA48 File Offset: 0x00049C48
		public static Trace SyncPoisonHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.syncPoisonHandlerTracer == null)
				{
					ExTraceGlobals.syncPoisonHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 37);
				}
				return ExTraceGlobals.syncPoisonHandlerTracer;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x0004BA67 File Offset: 0x00049C67
		public static Trace NativeSyncStorageProviderTracer
		{
			get
			{
				if (ExTraceGlobals.nativeSyncStorageProviderTracer == null)
				{
					ExTraceGlobals.nativeSyncStorageProviderTracer = new Trace(ExTraceGlobals.componentGuid, 38);
				}
				return ExTraceGlobals.nativeSyncStorageProviderTracer;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0004BA86 File Offset: 0x00049C86
		public static Trace SendAsTracer
		{
			get
			{
				if (ExTraceGlobals.sendAsTracer == null)
				{
					ExTraceGlobals.sendAsTracer = new Trace(ExTraceGlobals.componentGuid, 39);
				}
				return ExTraceGlobals.sendAsTracer;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0004BAA5 File Offset: 0x00049CA5
		public static Trace StatefulHubPickerTracer
		{
			get
			{
				if (ExTraceGlobals.statefulHubPickerTracer == null)
				{
					ExTraceGlobals.statefulHubPickerTracer = new Trace(ExTraceGlobals.componentGuid, 40);
				}
				return ExTraceGlobals.statefulHubPickerTracer;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0004BAC4 File Offset: 0x00049CC4
		public static Trace RemoteAccountPolicyTracer
		{
			get
			{
				if (ExTraceGlobals.remoteAccountPolicyTracer == null)
				{
					ExTraceGlobals.remoteAccountPolicyTracer = new Trace(ExTraceGlobals.componentGuid, 41);
				}
				return ExTraceGlobals.remoteAccountPolicyTracer;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x0004BAE3 File Offset: 0x00049CE3
		public static Trace DataAccessLayerTracer
		{
			get
			{
				if (ExTraceGlobals.dataAccessLayerTracer == null)
				{
					ExTraceGlobals.dataAccessLayerTracer = new Trace(ExTraceGlobals.componentGuid, 42);
				}
				return ExTraceGlobals.dataAccessLayerTracer;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0004BB02 File Offset: 0x00049D02
		public static Trace SystemMailboxSessionPoolTracer
		{
			get
			{
				if (ExTraceGlobals.systemMailboxSessionPoolTracer == null)
				{
					ExTraceGlobals.systemMailboxSessionPoolTracer = new Trace(ExTraceGlobals.componentGuid, 43);
				}
				return ExTraceGlobals.systemMailboxSessionPoolTracer;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0004BB21 File Offset: 0x00049D21
		public static Trace SubscriptionCacheMessageTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionCacheMessageTracer == null)
				{
					ExTraceGlobals.subscriptionCacheMessageTracer = new Trace(ExTraceGlobals.componentGuid, 44);
				}
				return ExTraceGlobals.subscriptionCacheMessageTracer;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0004BB40 File Offset: 0x00049D40
		public static Trace SubscriptionQueueTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionQueueTracer == null)
				{
					ExTraceGlobals.subscriptionQueueTracer = new Trace(ExTraceGlobals.componentGuid, 45);
				}
				return ExTraceGlobals.subscriptionQueueTracer;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0004BB5F File Offset: 0x00049D5F
		public static Trace SubscriptionCacheRpcServerTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionCacheRpcServerTracer == null)
				{
					ExTraceGlobals.subscriptionCacheRpcServerTracer = new Trace(ExTraceGlobals.componentGuid, 46);
				}
				return ExTraceGlobals.subscriptionCacheRpcServerTracer;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0004BB7E File Offset: 0x00049D7E
		public static Trace ContentAggregationConfigTracer
		{
			get
			{
				if (ExTraceGlobals.contentAggregationConfigTracer == null)
				{
					ExTraceGlobals.contentAggregationConfigTracer = new Trace(ExTraceGlobals.componentGuid, 47);
				}
				return ExTraceGlobals.contentAggregationConfigTracer;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x0004BB9D File Offset: 0x00049D9D
		public static Trace AggregationConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.aggregationConfigurationTracer == null)
				{
					ExTraceGlobals.aggregationConfigurationTracer = new Trace(ExTraceGlobals.componentGuid, 48);
				}
				return ExTraceGlobals.aggregationConfigurationTracer;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0004BBBC File Offset: 0x00049DBC
		public static Trace SubscriptionAgentManagerTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionAgentManagerTracer == null)
				{
					ExTraceGlobals.subscriptionAgentManagerTracer = new Trace(ExTraceGlobals.componentGuid, 49);
				}
				return ExTraceGlobals.subscriptionAgentManagerTracer;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0004BBDB File Offset: 0x00049DDB
		public static Trace SyncHealthLogManagerTracer
		{
			get
			{
				if (ExTraceGlobals.syncHealthLogManagerTracer == null)
				{
					ExTraceGlobals.syncHealthLogManagerTracer = new Trace(ExTraceGlobals.componentGuid, 50);
				}
				return ExTraceGlobals.syncHealthLogManagerTracer;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0004BBFA File Offset: 0x00049DFA
		public static Trace TransportSyncManagerSvcTracer
		{
			get
			{
				if (ExTraceGlobals.transportSyncManagerSvcTracer == null)
				{
					ExTraceGlobals.transportSyncManagerSvcTracer = new Trace(ExTraceGlobals.componentGuid, 51);
				}
				return ExTraceGlobals.transportSyncManagerSvcTracer;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0004BC19 File Offset: 0x00049E19
		public static Trace GlobalDatabaseHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.globalDatabaseHandlerTracer == null)
				{
					ExTraceGlobals.globalDatabaseHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 52);
				}
				return ExTraceGlobals.globalDatabaseHandlerTracer;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0004BC38 File Offset: 0x00049E38
		public static Trace DatabaseManagerTracer
		{
			get
			{
				if (ExTraceGlobals.databaseManagerTracer == null)
				{
					ExTraceGlobals.databaseManagerTracer = new Trace(ExTraceGlobals.componentGuid, 53);
				}
				return ExTraceGlobals.databaseManagerTracer;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0004BC57 File Offset: 0x00049E57
		public static Trace MailboxManagerTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxManagerTracer == null)
				{
					ExTraceGlobals.mailboxManagerTracer = new Trace(ExTraceGlobals.componentGuid, 54);
				}
				return ExTraceGlobals.mailboxManagerTracer;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0004BC76 File Offset: 0x00049E76
		public static Trace MailboxTableManagerTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxTableManagerTracer == null)
				{
					ExTraceGlobals.mailboxTableManagerTracer = new Trace(ExTraceGlobals.componentGuid, 55);
				}
				return ExTraceGlobals.mailboxTableManagerTracer;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0004BC95 File Offset: 0x00049E95
		public static Trace SubscriptionNotificationServerTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionNotificationServerTracer == null)
				{
					ExTraceGlobals.subscriptionNotificationServerTracer = new Trace(ExTraceGlobals.componentGuid, 56);
				}
				return ExTraceGlobals.subscriptionNotificationServerTracer;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0004BCB4 File Offset: 0x00049EB4
		public static Trace SubscriptionNotificationClientTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionNotificationClientTracer == null)
				{
					ExTraceGlobals.subscriptionNotificationClientTracer = new Trace(ExTraceGlobals.componentGuid, 57);
				}
				return ExTraceGlobals.subscriptionNotificationClientTracer;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0004BCD3 File Offset: 0x00049ED3
		public static Trace FacebookProviderTracer
		{
			get
			{
				if (ExTraceGlobals.facebookProviderTracer == null)
				{
					ExTraceGlobals.facebookProviderTracer = new Trace(ExTraceGlobals.componentGuid, 58);
				}
				return ExTraceGlobals.facebookProviderTracer;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0004BCF2 File Offset: 0x00049EF2
		public static Trace LinkedInProviderTracer
		{
			get
			{
				if (ExTraceGlobals.linkedInProviderTracer == null)
				{
					ExTraceGlobals.linkedInProviderTracer = new Trace(ExTraceGlobals.componentGuid, 59);
				}
				return ExTraceGlobals.linkedInProviderTracer;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0004BD11 File Offset: 0x00049F11
		public static Trace SubscriptionRemoveTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionRemoveTracer == null)
				{
					ExTraceGlobals.subscriptionRemoveTracer = new Trace(ExTraceGlobals.componentGuid, 60);
				}
				return ExTraceGlobals.subscriptionRemoveTracer;
			}
		}

		// Token: 0x04001569 RID: 5481
		private static Guid componentGuid = new Guid("B29C4959-0C49-4bfa-BDDD-9B6E961420AC");

		// Token: 0x0400156A RID: 5482
		private static Trace commonTracer = null;

		// Token: 0x0400156B RID: 5483
		private static Trace pop3ClientTracer = null;

		// Token: 0x0400156C RID: 5484
		private static Trace deltaSyncStorageProviderTracer = null;

		// Token: 0x0400156D RID: 5485
		private static Trace feedParserTracer = null;

		// Token: 0x0400156E RID: 5486
		private static Trace contentGenerationTracer = null;

		// Token: 0x0400156F RID: 5487
		private static Trace subscriptionSubmissionRpcTracer = null;

		// Token: 0x04001570 RID: 5488
		private static Trace rssServerLockTracer = null;

		// Token: 0x04001571 RID: 5489
		private static Trace subscriptionSubmitTracer = null;

		// Token: 0x04001572 RID: 5490
		private static Trace subscriptionSubmissionServerTracer = null;

		// Token: 0x04001573 RID: 5491
		private static Trace schedulerTracer = null;

		// Token: 0x04001574 RID: 5492
		private static Trace subscriptionManagerTracer = null;

		// Token: 0x04001575 RID: 5493
		private static Trace webFeedProtocolHandlerTracer = null;

		// Token: 0x04001576 RID: 5494
		private static Trace deliveryAgentTracer = null;

		// Token: 0x04001577 RID: 5495
		private static Trace htmlFixerTracer = null;

		// Token: 0x04001578 RID: 5496
		private static Trace pop3ProtocolHandlerTracer = null;

		// Token: 0x04001579 RID: 5497
		private static Trace pop3StorageProviderTracer = null;

		// Token: 0x0400157A RID: 5498
		private static Trace subscriptionTaskTracer = null;

		// Token: 0x0400157B RID: 5499
		private static Trace syncEngineTracer = null;

		// Token: 0x0400157C RID: 5500
		private static Trace transportSyncStorageProviderTracer = null;

		// Token: 0x0400157D RID: 5501
		private static Trace stateStorageTracer = null;

		// Token: 0x0400157E RID: 5502
		private static Trace syncLogTracer = null;

		// Token: 0x0400157F RID: 5503
		private static Trace xSOSyncStorageProviderTracer = null;

		// Token: 0x04001580 RID: 5504
		private static Trace subscriptionEventbasedAssistantTracer = null;

		// Token: 0x04001581 RID: 5505
		private static Trace cacheManagerTracer = null;

		// Token: 0x04001582 RID: 5506
		private static Trace cacheManagerLookupTracer = null;

		// Token: 0x04001583 RID: 5507
		private static Trace tokenManagerTracer = null;

		// Token: 0x04001584 RID: 5508
		private static Trace subscriptionCompletionServerTracer = null;

		// Token: 0x04001585 RID: 5509
		private static Trace subscriptionCompletionClientTracer = null;

		// Token: 0x04001586 RID: 5510
		private static Trace eventLogTracer = null;

		// Token: 0x04001587 RID: 5511
		private static Trace protocolHandlerTracer = null;

		// Token: 0x04001588 RID: 5512
		private static Trace aggregationComponentTracer = null;

		// Token: 0x04001589 RID: 5513
		private static Trace iMAPSyncStorageProviderTracer = null;

		// Token: 0x0400158A RID: 5514
		private static Trace iMAPClientTracer = null;

		// Token: 0x0400158B RID: 5515
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x0400158C RID: 5516
		private static Trace davClientTracer = null;

		// Token: 0x0400158D RID: 5517
		private static Trace davSyncStorageProviderTracer = null;

		// Token: 0x0400158E RID: 5518
		private static Trace syncPoisonHandlerTracer = null;

		// Token: 0x0400158F RID: 5519
		private static Trace nativeSyncStorageProviderTracer = null;

		// Token: 0x04001590 RID: 5520
		private static Trace sendAsTracer = null;

		// Token: 0x04001591 RID: 5521
		private static Trace statefulHubPickerTracer = null;

		// Token: 0x04001592 RID: 5522
		private static Trace remoteAccountPolicyTracer = null;

		// Token: 0x04001593 RID: 5523
		private static Trace dataAccessLayerTracer = null;

		// Token: 0x04001594 RID: 5524
		private static Trace systemMailboxSessionPoolTracer = null;

		// Token: 0x04001595 RID: 5525
		private static Trace subscriptionCacheMessageTracer = null;

		// Token: 0x04001596 RID: 5526
		private static Trace subscriptionQueueTracer = null;

		// Token: 0x04001597 RID: 5527
		private static Trace subscriptionCacheRpcServerTracer = null;

		// Token: 0x04001598 RID: 5528
		private static Trace contentAggregationConfigTracer = null;

		// Token: 0x04001599 RID: 5529
		private static Trace aggregationConfigurationTracer = null;

		// Token: 0x0400159A RID: 5530
		private static Trace subscriptionAgentManagerTracer = null;

		// Token: 0x0400159B RID: 5531
		private static Trace syncHealthLogManagerTracer = null;

		// Token: 0x0400159C RID: 5532
		private static Trace transportSyncManagerSvcTracer = null;

		// Token: 0x0400159D RID: 5533
		private static Trace globalDatabaseHandlerTracer = null;

		// Token: 0x0400159E RID: 5534
		private static Trace databaseManagerTracer = null;

		// Token: 0x0400159F RID: 5535
		private static Trace mailboxManagerTracer = null;

		// Token: 0x040015A0 RID: 5536
		private static Trace mailboxTableManagerTracer = null;

		// Token: 0x040015A1 RID: 5537
		private static Trace subscriptionNotificationServerTracer = null;

		// Token: 0x040015A2 RID: 5538
		private static Trace subscriptionNotificationClientTracer = null;

		// Token: 0x040015A3 RID: 5539
		private static Trace facebookProviderTracer = null;

		// Token: 0x040015A4 RID: 5540
		private static Trace linkedInProviderTracer = null;

		// Token: 0x040015A5 RID: 5541
		private static Trace subscriptionRemoveTracer = null;
	}
}
