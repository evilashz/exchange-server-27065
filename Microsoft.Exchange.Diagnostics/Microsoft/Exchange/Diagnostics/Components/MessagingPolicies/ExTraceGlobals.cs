using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.MessagingPolicies
{
	// Token: 0x0200032C RID: 812
	public static class ExTraceGlobals
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0004CAD6 File Offset: 0x0004ACD6
		public static Trace TransportRulesEngineTracer
		{
			get
			{
				if (ExTraceGlobals.transportRulesEngineTracer == null)
				{
					ExTraceGlobals.transportRulesEngineTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.transportRulesEngineTracer;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0004CAF4 File Offset: 0x0004ACF4
		public static Trace CodeGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.codeGeneratorTracer == null)
				{
					ExTraceGlobals.codeGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.codeGeneratorTracer;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0004CB12 File Offset: 0x0004AD12
		public static Trace JournalingTracer
		{
			get
			{
				if (ExTraceGlobals.journalingTracer == null)
				{
					ExTraceGlobals.journalingTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.journalingTracer;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0004CB30 File Offset: 0x0004AD30
		public static Trace AttachmentFilteringTracer
		{
			get
			{
				if (ExTraceGlobals.attachmentFilteringTracer == null)
				{
					ExTraceGlobals.attachmentFilteringTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.attachmentFilteringTracer;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0004CB4E File Offset: 0x0004AD4E
		public static Trace AddressRewritingTracer
		{
			get
			{
				if (ExTraceGlobals.addressRewritingTracer == null)
				{
					ExTraceGlobals.addressRewritingTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.addressRewritingTracer;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0004CB6C File Offset: 0x0004AD6C
		public static Trace RmSvcAgentTracer
		{
			get
			{
				if (ExTraceGlobals.rmSvcAgentTracer == null)
				{
					ExTraceGlobals.rmSvcAgentTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.rmSvcAgentTracer;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0004CB8A File Offset: 0x0004AD8A
		public static Trace RulesEngineTracer
		{
			get
			{
				if (ExTraceGlobals.rulesEngineTracer == null)
				{
					ExTraceGlobals.rulesEngineTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.rulesEngineTracer;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0004CBA8 File Offset: 0x0004ADA8
		public static Trace ReconciliationTracer
		{
			get
			{
				if (ExTraceGlobals.reconciliationTracer == null)
				{
					ExTraceGlobals.reconciliationTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.reconciliationTracer;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0004CBC6 File Offset: 0x0004ADC6
		public static Trace CrossPremiseTracer
		{
			get
			{
				if (ExTraceGlobals.crossPremiseTracer == null)
				{
					ExTraceGlobals.crossPremiseTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.crossPremiseTracer;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0004CBE4 File Offset: 0x0004ADE4
		public static Trace OpenDomainRoutingAgentTracer
		{
			get
			{
				if (ExTraceGlobals.openDomainRoutingAgentTracer == null)
				{
					ExTraceGlobals.openDomainRoutingAgentTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.openDomainRoutingAgentTracer;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0004CC03 File Offset: 0x0004AE03
		public static Trace JrdAgentTracer
		{
			get
			{
				if (ExTraceGlobals.jrdAgentTracer == null)
				{
					ExTraceGlobals.jrdAgentTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.jrdAgentTracer;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0004CC22 File Offset: 0x0004AE22
		public static Trace PreLicensingAgentTracer
		{
			get
			{
				if (ExTraceGlobals.preLicensingAgentTracer == null)
				{
					ExTraceGlobals.preLicensingAgentTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.preLicensingAgentTracer;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0004CC41 File Offset: 0x0004AE41
		public static Trace OutlookProtectionRulesTracer
		{
			get
			{
				if (ExTraceGlobals.outlookProtectionRulesTracer == null)
				{
					ExTraceGlobals.outlookProtectionRulesTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.outlookProtectionRulesTracer;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0004CC60 File Offset: 0x0004AE60
		public static Trace EhfOutboundRoutingAgentTracer
		{
			get
			{
				if (ExTraceGlobals.ehfOutboundRoutingAgentTracer == null)
				{
					ExTraceGlobals.ehfOutboundRoutingAgentTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.ehfOutboundRoutingAgentTracer;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0004CC7F File Offset: 0x0004AE7F
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0004CC9E File Offset: 0x0004AE9E
		public static Trace RedirectionAgentTracer
		{
			get
			{
				if (ExTraceGlobals.redirectionAgentTracer == null)
				{
					ExTraceGlobals.redirectionAgentTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.redirectionAgentTracer;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0004CCBD File Offset: 0x0004AEBD
		public static Trace InboundTrustAgentTracer
		{
			get
			{
				if (ExTraceGlobals.inboundTrustAgentTracer == null)
				{
					ExTraceGlobals.inboundTrustAgentTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.inboundTrustAgentTracer;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0004CCDC File Offset: 0x0004AEDC
		public static Trace OutboundTrustAgentTracer
		{
			get
			{
				if (ExTraceGlobals.outboundTrustAgentTracer == null)
				{
					ExTraceGlobals.outboundTrustAgentTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.outboundTrustAgentTracer;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0004CCFB File Offset: 0x0004AEFB
		public static Trace CentralizedMailFlowAgentTracer
		{
			get
			{
				if (ExTraceGlobals.centralizedMailFlowAgentTracer == null)
				{
					ExTraceGlobals.centralizedMailFlowAgentTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.centralizedMailFlowAgentTracer;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0004CD1A File Offset: 0x0004AF1A
		public static Trace InterceptorAgentTracer
		{
			get
			{
				if (ExTraceGlobals.interceptorAgentTracer == null)
				{
					ExTraceGlobals.interceptorAgentTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.interceptorAgentTracer;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0004CD39 File Offset: 0x0004AF39
		public static Trace OutlookPolicyNudgeRulesTracer
		{
			get
			{
				if (ExTraceGlobals.outlookPolicyNudgeRulesTracer == null)
				{
					ExTraceGlobals.outlookPolicyNudgeRulesTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.outlookPolicyNudgeRulesTracer;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0004CD58 File Offset: 0x0004AF58
		public static Trace ClassificationDefinitionsTracer
		{
			get
			{
				if (ExTraceGlobals.classificationDefinitionsTracer == null)
				{
					ExTraceGlobals.classificationDefinitionsTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.classificationDefinitionsTracer;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0004CD77 File Offset: 0x0004AF77
		public static Trace FfoFrontendProxyAgentTracer
		{
			get
			{
				if (ExTraceGlobals.ffoFrontendProxyAgentTracer == null)
				{
					ExTraceGlobals.ffoFrontendProxyAgentTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.ffoFrontendProxyAgentTracer;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0004CD96 File Offset: 0x0004AF96
		public static Trace FrontendProxyAgentTracer
		{
			get
			{
				if (ExTraceGlobals.frontendProxyAgentTracer == null)
				{
					ExTraceGlobals.frontendProxyAgentTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.frontendProxyAgentTracer;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0004CDB5 File Offset: 0x0004AFB5
		public static Trace AddressBookPolicyRoutingAgentTracer
		{
			get
			{
				if (ExTraceGlobals.addressBookPolicyRoutingAgentTracer == null)
				{
					ExTraceGlobals.addressBookPolicyRoutingAgentTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.addressBookPolicyRoutingAgentTracer;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x0004CDD4 File Offset: 0x0004AFD4
		public static Trace OwaRulesEngineTracer
		{
			get
			{
				if (ExTraceGlobals.owaRulesEngineTracer == null)
				{
					ExTraceGlobals.owaRulesEngineTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.owaRulesEngineTracer;
			}
		}

		// Token: 0x04001603 RID: 5635
		private static Guid componentGuid = new Guid("D368698B-B84F-402e-A300-FA98CC39020F");

		// Token: 0x04001604 RID: 5636
		private static Trace transportRulesEngineTracer = null;

		// Token: 0x04001605 RID: 5637
		private static Trace codeGeneratorTracer = null;

		// Token: 0x04001606 RID: 5638
		private static Trace journalingTracer = null;

		// Token: 0x04001607 RID: 5639
		private static Trace attachmentFilteringTracer = null;

		// Token: 0x04001608 RID: 5640
		private static Trace addressRewritingTracer = null;

		// Token: 0x04001609 RID: 5641
		private static Trace rmSvcAgentTracer = null;

		// Token: 0x0400160A RID: 5642
		private static Trace rulesEngineTracer = null;

		// Token: 0x0400160B RID: 5643
		private static Trace reconciliationTracer = null;

		// Token: 0x0400160C RID: 5644
		private static Trace crossPremiseTracer = null;

		// Token: 0x0400160D RID: 5645
		private static Trace openDomainRoutingAgentTracer = null;

		// Token: 0x0400160E RID: 5646
		private static Trace jrdAgentTracer = null;

		// Token: 0x0400160F RID: 5647
		private static Trace preLicensingAgentTracer = null;

		// Token: 0x04001610 RID: 5648
		private static Trace outlookProtectionRulesTracer = null;

		// Token: 0x04001611 RID: 5649
		private static Trace ehfOutboundRoutingAgentTracer = null;

		// Token: 0x04001612 RID: 5650
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001613 RID: 5651
		private static Trace redirectionAgentTracer = null;

		// Token: 0x04001614 RID: 5652
		private static Trace inboundTrustAgentTracer = null;

		// Token: 0x04001615 RID: 5653
		private static Trace outboundTrustAgentTracer = null;

		// Token: 0x04001616 RID: 5654
		private static Trace centralizedMailFlowAgentTracer = null;

		// Token: 0x04001617 RID: 5655
		private static Trace interceptorAgentTracer = null;

		// Token: 0x04001618 RID: 5656
		private static Trace outlookPolicyNudgeRulesTracer = null;

		// Token: 0x04001619 RID: 5657
		private static Trace classificationDefinitionsTracer = null;

		// Token: 0x0400161A RID: 5658
		private static Trace ffoFrontendProxyAgentTracer = null;

		// Token: 0x0400161B RID: 5659
		private static Trace frontendProxyAgentTracer = null;

		// Token: 0x0400161C RID: 5660
		private static Trace addressBookPolicyRoutingAgentTracer = null;

		// Token: 0x0400161D RID: 5661
		private static Trace owaRulesEngineTracer = null;
	}
}
