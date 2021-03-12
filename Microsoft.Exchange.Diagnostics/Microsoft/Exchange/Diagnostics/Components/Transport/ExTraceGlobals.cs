using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Transport
{
	// Token: 0x02000319 RID: 793
	public static class ExTraceGlobals
	{
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0004AF2D File Offset: 0x0004912D
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x0004AF4B File Offset: 0x0004914B
		public static Trace SmtpReceiveTracer
		{
			get
			{
				if (ExTraceGlobals.smtpReceiveTracer == null)
				{
					ExTraceGlobals.smtpReceiveTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.smtpReceiveTracer;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0004AF69 File Offset: 0x00049169
		public static Trace SmtpSendTracer
		{
			get
			{
				if (ExTraceGlobals.smtpSendTracer == null)
				{
					ExTraceGlobals.smtpSendTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.smtpSendTracer;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0004AF87 File Offset: 0x00049187
		public static Trace PickupTracer
		{
			get
			{
				if (ExTraceGlobals.pickupTracer == null)
				{
					ExTraceGlobals.pickupTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.pickupTracer;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x0004AFA5 File Offset: 0x000491A5
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0004AFC3 File Offset: 0x000491C3
		public static Trace QueuingTracer
		{
			get
			{
				if (ExTraceGlobals.queuingTracer == null)
				{
					ExTraceGlobals.queuingTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.queuingTracer;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0004AFE1 File Offset: 0x000491E1
		public static Trace DSNTracer
		{
			get
			{
				if (ExTraceGlobals.dSNTracer == null)
				{
					ExTraceGlobals.dSNTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.dSNTracer;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x0004AFFF File Offset: 0x000491FF
		public static Trace RoutingTracer
		{
			get
			{
				if (ExTraceGlobals.routingTracer == null)
				{
					ExTraceGlobals.routingTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.routingTracer;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x0004B01D File Offset: 0x0004921D
		public static Trace ResolverTracer
		{
			get
			{
				if (ExTraceGlobals.resolverTracer == null)
				{
					ExTraceGlobals.resolverTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.resolverTracer;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x0004B03B File Offset: 0x0004923B
		public static Trace ContentConversionTracer
		{
			get
			{
				if (ExTraceGlobals.contentConversionTracer == null)
				{
					ExTraceGlobals.contentConversionTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.contentConversionTracer;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x0004B05A File Offset: 0x0004925A
		public static Trace ExtensibilityTracer
		{
			get
			{
				if (ExTraceGlobals.extensibilityTracer == null)
				{
					ExTraceGlobals.extensibilityTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.extensibilityTracer;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x0004B079 File Offset: 0x00049279
		public static Trace SchedulerTracer
		{
			get
			{
				if (ExTraceGlobals.schedulerTracer == null)
				{
					ExTraceGlobals.schedulerTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.schedulerTracer;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0004B098 File Offset: 0x00049298
		public static Trace SecureMailTracer
		{
			get
			{
				if (ExTraceGlobals.secureMailTracer == null)
				{
					ExTraceGlobals.secureMailTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.secureMailTracer;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0004B0B7 File Offset: 0x000492B7
		public static Trace MessageTrackingTracer
		{
			get
			{
				if (ExTraceGlobals.messageTrackingTracer == null)
				{
					ExTraceGlobals.messageTrackingTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.messageTrackingTracer;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x0004B0D6 File Offset: 0x000492D6
		public static Trace ResourceManagerTracer
		{
			get
			{
				if (ExTraceGlobals.resourceManagerTracer == null)
				{
					ExTraceGlobals.resourceManagerTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.resourceManagerTracer;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0004B0F5 File Offset: 0x000492F5
		public static Trace ConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.configurationTracer == null)
				{
					ExTraceGlobals.configurationTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.configurationTracer;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0004B114 File Offset: 0x00049314
		public static Trace DumpsterTracer
		{
			get
			{
				if (ExTraceGlobals.dumpsterTracer == null)
				{
					ExTraceGlobals.dumpsterTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.dumpsterTracer;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0004B133 File Offset: 0x00049333
		public static Trace ExpoTracer
		{
			get
			{
				if (ExTraceGlobals.expoTracer == null)
				{
					ExTraceGlobals.expoTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.expoTracer;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x0004B152 File Offset: 0x00049352
		public static Trace CertificateTracer
		{
			get
			{
				if (ExTraceGlobals.certificateTracer == null)
				{
					ExTraceGlobals.certificateTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.certificateTracer;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x0004B171 File Offset: 0x00049371
		public static Trace OrarTracer
		{
			get
			{
				if (ExTraceGlobals.orarTracer == null)
				{
					ExTraceGlobals.orarTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.orarTracer;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x0004B190 File Offset: 0x00049390
		public static Trace ShadowRedundancyTracer
		{
			get
			{
				if (ExTraceGlobals.shadowRedundancyTracer == null)
				{
					ExTraceGlobals.shadowRedundancyTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.shadowRedundancyTracer;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x0004B1AF File Offset: 0x000493AF
		public static Trace ApprovalTracer
		{
			get
			{
				if (ExTraceGlobals.approvalTracer == null)
				{
					ExTraceGlobals.approvalTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.approvalTracer;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0004B1CE File Offset: 0x000493CE
		public static Trace TransportDumpsterTracer
		{
			get
			{
				if (ExTraceGlobals.transportDumpsterTracer == null)
				{
					ExTraceGlobals.transportDumpsterTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.transportDumpsterTracer;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x0004B1ED File Offset: 0x000493ED
		public static Trace TransportSettingsCacheTracer
		{
			get
			{
				if (ExTraceGlobals.transportSettingsCacheTracer == null)
				{
					ExTraceGlobals.transportSettingsCacheTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.transportSettingsCacheTracer;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0004B20C File Offset: 0x0004940C
		public static Trace TransportRulesCacheTracer
		{
			get
			{
				if (ExTraceGlobals.transportRulesCacheTracer == null)
				{
					ExTraceGlobals.transportRulesCacheTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.transportRulesCacheTracer;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x0004B22B File Offset: 0x0004942B
		public static Trace MicrosoftExchangeRecipientCacheTracer
		{
			get
			{
				if (ExTraceGlobals.microsoftExchangeRecipientCacheTracer == null)
				{
					ExTraceGlobals.microsoftExchangeRecipientCacheTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.microsoftExchangeRecipientCacheTracer;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0004B24A File Offset: 0x0004944A
		public static Trace RemoteDomainsCacheTracer
		{
			get
			{
				if (ExTraceGlobals.remoteDomainsCacheTracer == null)
				{
					ExTraceGlobals.remoteDomainsCacheTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.remoteDomainsCacheTracer;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x0004B269 File Offset: 0x00049469
		public static Trace JournalingRulesCacheTracer
		{
			get
			{
				if (ExTraceGlobals.journalingRulesCacheTracer == null)
				{
					ExTraceGlobals.journalingRulesCacheTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.journalingRulesCacheTracer;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0004B288 File Offset: 0x00049488
		public static Trace ResourcePoolTracer
		{
			get
			{
				if (ExTraceGlobals.resourcePoolTracer == null)
				{
					ExTraceGlobals.resourcePoolTracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.resourcePoolTracer;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x0004B2A7 File Offset: 0x000494A7
		public static Trace DeliveryAgentsTracer
		{
			get
			{
				if (ExTraceGlobals.deliveryAgentsTracer == null)
				{
					ExTraceGlobals.deliveryAgentsTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.deliveryAgentsTracer;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0004B2C6 File Offset: 0x000494C6
		public static Trace SupervisionTracer
		{
			get
			{
				if (ExTraceGlobals.supervisionTracer == null)
				{
					ExTraceGlobals.supervisionTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.supervisionTracer;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x0004B2E5 File Offset: 0x000494E5
		public static Trace RightsManagementTracer
		{
			get
			{
				if (ExTraceGlobals.rightsManagementTracer == null)
				{
					ExTraceGlobals.rightsManagementTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.rightsManagementTracer;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x0004B304 File Offset: 0x00049504
		public static Trace PerimeterSettingsCacheTracer
		{
			get
			{
				if (ExTraceGlobals.perimeterSettingsCacheTracer == null)
				{
					ExTraceGlobals.perimeterSettingsCacheTracer = new Trace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.perimeterSettingsCacheTracer;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x0004B323 File Offset: 0x00049523
		public static Trace PreviousHopLatencyTracer
		{
			get
			{
				if (ExTraceGlobals.previousHopLatencyTracer == null)
				{
					ExTraceGlobals.previousHopLatencyTracer = new Trace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.previousHopLatencyTracer;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x0004B342 File Offset: 0x00049542
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x0004B361 File Offset: 0x00049561
		public static Trace OrganizationSettingsCacheTracer
		{
			get
			{
				if (ExTraceGlobals.organizationSettingsCacheTracer == null)
				{
					ExTraceGlobals.organizationSettingsCacheTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.organizationSettingsCacheTracer;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x0004B380 File Offset: 0x00049580
		public static Trace AnonymousCertificateValidationResultCacheTracer
		{
			get
			{
				if (ExTraceGlobals.anonymousCertificateValidationResultCacheTracer == null)
				{
					ExTraceGlobals.anonymousCertificateValidationResultCacheTracer = new Trace(ExTraceGlobals.componentGuid, 37);
				}
				return ExTraceGlobals.anonymousCertificateValidationResultCacheTracer;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x0004B39F File Offset: 0x0004959F
		public static Trace AcceptedDomainsCacheTracer
		{
			get
			{
				if (ExTraceGlobals.acceptedDomainsCacheTracer == null)
				{
					ExTraceGlobals.acceptedDomainsCacheTracer = new Trace(ExTraceGlobals.componentGuid, 38);
				}
				return ExTraceGlobals.acceptedDomainsCacheTracer;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0004B3BE File Offset: 0x000495BE
		public static Trace ProxyHubSelectorTracer
		{
			get
			{
				if (ExTraceGlobals.proxyHubSelectorTracer == null)
				{
					ExTraceGlobals.proxyHubSelectorTracer = new Trace(ExTraceGlobals.componentGuid, 39);
				}
				return ExTraceGlobals.proxyHubSelectorTracer;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x0004B3DD File Offset: 0x000495DD
		public static Trace MessageResubmissionTracer
		{
			get
			{
				if (ExTraceGlobals.messageResubmissionTracer == null)
				{
					ExTraceGlobals.messageResubmissionTracer = new Trace(ExTraceGlobals.componentGuid, 40);
				}
				return ExTraceGlobals.messageResubmissionTracer;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0004B3FC File Offset: 0x000495FC
		public static Trace StorageTracer
		{
			get
			{
				if (ExTraceGlobals.storageTracer == null)
				{
					ExTraceGlobals.storageTracer = new Trace(ExTraceGlobals.componentGuid, 41);
				}
				return ExTraceGlobals.storageTracer;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x0004B41B File Offset: 0x0004961B
		public static Trace PoisonTracer
		{
			get
			{
				if (ExTraceGlobals.poisonTracer == null)
				{
					ExTraceGlobals.poisonTracer = new Trace(ExTraceGlobals.componentGuid, 42);
				}
				return ExTraceGlobals.poisonTracer;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0004B43A File Offset: 0x0004963A
		public static Trace HostedEncryptionTracer
		{
			get
			{
				if (ExTraceGlobals.hostedEncryptionTracer == null)
				{
					ExTraceGlobals.hostedEncryptionTracer = new Trace(ExTraceGlobals.componentGuid, 43);
				}
				return ExTraceGlobals.hostedEncryptionTracer;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0004B459 File Offset: 0x00049659
		public static Trace OutboundConnectorsCacheTracer
		{
			get
			{
				if (ExTraceGlobals.outboundConnectorsCacheTracer == null)
				{
					ExTraceGlobals.outboundConnectorsCacheTracer = new Trace(ExTraceGlobals.componentGuid, 44);
				}
				return ExTraceGlobals.outboundConnectorsCacheTracer;
			}
		}

		// Token: 0x04001539 RID: 5433
		private static Guid componentGuid = new Guid("c3ea5adf-c135-45e7-9dff-e1dc3bd67999");

		// Token: 0x0400153A RID: 5434
		private static Trace generalTracer = null;

		// Token: 0x0400153B RID: 5435
		private static Trace smtpReceiveTracer = null;

		// Token: 0x0400153C RID: 5436
		private static Trace smtpSendTracer = null;

		// Token: 0x0400153D RID: 5437
		private static Trace pickupTracer = null;

		// Token: 0x0400153E RID: 5438
		private static Trace serviceTracer = null;

		// Token: 0x0400153F RID: 5439
		private static Trace queuingTracer = null;

		// Token: 0x04001540 RID: 5440
		private static Trace dSNTracer = null;

		// Token: 0x04001541 RID: 5441
		private static Trace routingTracer = null;

		// Token: 0x04001542 RID: 5442
		private static Trace resolverTracer = null;

		// Token: 0x04001543 RID: 5443
		private static Trace contentConversionTracer = null;

		// Token: 0x04001544 RID: 5444
		private static Trace extensibilityTracer = null;

		// Token: 0x04001545 RID: 5445
		private static Trace schedulerTracer = null;

		// Token: 0x04001546 RID: 5446
		private static Trace secureMailTracer = null;

		// Token: 0x04001547 RID: 5447
		private static Trace messageTrackingTracer = null;

		// Token: 0x04001548 RID: 5448
		private static Trace resourceManagerTracer = null;

		// Token: 0x04001549 RID: 5449
		private static Trace configurationTracer = null;

		// Token: 0x0400154A RID: 5450
		private static Trace dumpsterTracer = null;

		// Token: 0x0400154B RID: 5451
		private static Trace expoTracer = null;

		// Token: 0x0400154C RID: 5452
		private static Trace certificateTracer = null;

		// Token: 0x0400154D RID: 5453
		private static Trace orarTracer = null;

		// Token: 0x0400154E RID: 5454
		private static Trace shadowRedundancyTracer = null;

		// Token: 0x0400154F RID: 5455
		private static Trace approvalTracer = null;

		// Token: 0x04001550 RID: 5456
		private static Trace transportDumpsterTracer = null;

		// Token: 0x04001551 RID: 5457
		private static Trace transportSettingsCacheTracer = null;

		// Token: 0x04001552 RID: 5458
		private static Trace transportRulesCacheTracer = null;

		// Token: 0x04001553 RID: 5459
		private static Trace microsoftExchangeRecipientCacheTracer = null;

		// Token: 0x04001554 RID: 5460
		private static Trace remoteDomainsCacheTracer = null;

		// Token: 0x04001555 RID: 5461
		private static Trace journalingRulesCacheTracer = null;

		// Token: 0x04001556 RID: 5462
		private static Trace resourcePoolTracer = null;

		// Token: 0x04001557 RID: 5463
		private static Trace deliveryAgentsTracer = null;

		// Token: 0x04001558 RID: 5464
		private static Trace supervisionTracer = null;

		// Token: 0x04001559 RID: 5465
		private static Trace rightsManagementTracer = null;

		// Token: 0x0400155A RID: 5466
		private static Trace perimeterSettingsCacheTracer = null;

		// Token: 0x0400155B RID: 5467
		private static Trace previousHopLatencyTracer = null;

		// Token: 0x0400155C RID: 5468
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x0400155D RID: 5469
		private static Trace organizationSettingsCacheTracer = null;

		// Token: 0x0400155E RID: 5470
		private static Trace anonymousCertificateValidationResultCacheTracer = null;

		// Token: 0x0400155F RID: 5471
		private static Trace acceptedDomainsCacheTracer = null;

		// Token: 0x04001560 RID: 5472
		private static Trace proxyHubSelectorTracer = null;

		// Token: 0x04001561 RID: 5473
		private static Trace messageResubmissionTracer = null;

		// Token: 0x04001562 RID: 5474
		private static Trace storageTracer = null;

		// Token: 0x04001563 RID: 5475
		private static Trace poisonTracer = null;

		// Token: 0x04001564 RID: 5476
		private static Trace hostedEncryptionTracer = null;

		// Token: 0x04001565 RID: 5477
		private static Trace outboundConnectorsCacheTracer = null;
	}
}
