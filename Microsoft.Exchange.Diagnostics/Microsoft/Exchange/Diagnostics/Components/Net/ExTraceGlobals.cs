using System;

namespace Microsoft.Exchange.Diagnostics.Components.Net
{
	// Token: 0x0200037F RID: 895
	public static class ExTraceGlobals
	{
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0005534B File Offset: 0x0005354B
		public static Trace DNSTracer
		{
			get
			{
				if (ExTraceGlobals.dNSTracer == null)
				{
					ExTraceGlobals.dNSTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.dNSTracer;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00055369 File Offset: 0x00053569
		public static Trace NetworkTracer
		{
			get
			{
				if (ExTraceGlobals.networkTracer == null)
				{
					ExTraceGlobals.networkTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.networkTracer;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x00055387 File Offset: 0x00053587
		public static Trace AuthenticationTracer
		{
			get
			{
				if (ExTraceGlobals.authenticationTracer == null)
				{
					ExTraceGlobals.authenticationTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.authenticationTracer;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x000553A5 File Offset: 0x000535A5
		public static Trace CertificateTracer
		{
			get
			{
				if (ExTraceGlobals.certificateTracer == null)
				{
					ExTraceGlobals.certificateTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.certificateTracer;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x000553C3 File Offset: 0x000535C3
		public static Trace DirectoryServicesTracer
		{
			get
			{
				if (ExTraceGlobals.directoryServicesTracer == null)
				{
					ExTraceGlobals.directoryServicesTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.directoryServicesTracer;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x000553E1 File Offset: 0x000535E1
		public static Trace ProcessManagerTracer
		{
			get
			{
				if (ExTraceGlobals.processManagerTracer == null)
				{
					ExTraceGlobals.processManagerTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.processManagerTracer;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x000553FF File Offset: 0x000535FF
		public static Trace HttpClientTracer
		{
			get
			{
				if (ExTraceGlobals.httpClientTracer == null)
				{
					ExTraceGlobals.httpClientTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.httpClientTracer;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0005541D File Offset: 0x0005361D
		public static Trace ProtocolLogTracer
		{
			get
			{
				if (ExTraceGlobals.protocolLogTracer == null)
				{
					ExTraceGlobals.protocolLogTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.protocolLogTracer;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0005543B File Offset: 0x0005363B
		public static Trace RightsManagementTracer
		{
			get
			{
				if (ExTraceGlobals.rightsManagementTracer == null)
				{
					ExTraceGlobals.rightsManagementTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.rightsManagementTracer;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00055459 File Offset: 0x00053659
		public static Trace LiveIDAuthenticationClientTracer
		{
			get
			{
				if (ExTraceGlobals.liveIDAuthenticationClientTracer == null)
				{
					ExTraceGlobals.liveIDAuthenticationClientTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.liveIDAuthenticationClientTracer;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x00055478 File Offset: 0x00053678
		public static Trace DeltaSyncClientTracer
		{
			get
			{
				if (ExTraceGlobals.deltaSyncClientTracer == null)
				{
					ExTraceGlobals.deltaSyncClientTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.deltaSyncClientTracer;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x00055497 File Offset: 0x00053697
		public static Trace DeltaSyncResponseHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.deltaSyncResponseHandlerTracer == null)
				{
					ExTraceGlobals.deltaSyncResponseHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.deltaSyncResponseHandlerTracer;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x000554B6 File Offset: 0x000536B6
		public static Trace LanguagePackInfoTracer
		{
			get
			{
				if (ExTraceGlobals.languagePackInfoTracer == null)
				{
					ExTraceGlobals.languagePackInfoTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.languagePackInfoTracer;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x000554D5 File Offset: 0x000536D5
		public static Trace WSTrustTracer
		{
			get
			{
				if (ExTraceGlobals.wSTrustTracer == null)
				{
					ExTraceGlobals.wSTrustTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.wSTrustTracer;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x000554F4 File Offset: 0x000536F4
		public static Trace EwsClientTracer
		{
			get
			{
				if (ExTraceGlobals.ewsClientTracer == null)
				{
					ExTraceGlobals.ewsClientTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.ewsClientTracer;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x00055513 File Offset: 0x00053713
		public static Trace ConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.configurationTracer == null)
				{
					ExTraceGlobals.configurationTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.configurationTracer;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x00055532 File Offset: 0x00053732
		public static Trace SmtpClientTracer
		{
			get
			{
				if (ExTraceGlobals.smtpClientTracer == null)
				{
					ExTraceGlobals.smtpClientTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.smtpClientTracer;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x00055551 File Offset: 0x00053751
		public static Trace XropServiceClientTracer
		{
			get
			{
				if (ExTraceGlobals.xropServiceClientTracer == null)
				{
					ExTraceGlobals.xropServiceClientTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.xropServiceClientTracer;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x00055570 File Offset: 0x00053770
		public static Trace XropServiceServerTracer
		{
			get
			{
				if (ExTraceGlobals.xropServiceServerTracer == null)
				{
					ExTraceGlobals.xropServiceServerTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.xropServiceServerTracer;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0005558F File Offset: 0x0005378F
		public static Trace ClaimTracer
		{
			get
			{
				if (ExTraceGlobals.claimTracer == null)
				{
					ExTraceGlobals.claimTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.claimTracer;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x000555AE File Offset: 0x000537AE
		public static Trace FacebookTracer
		{
			get
			{
				if (ExTraceGlobals.facebookTracer == null)
				{
					ExTraceGlobals.facebookTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.facebookTracer;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x000555CD File Offset: 0x000537CD
		public static Trace LinkedInTracer
		{
			get
			{
				if (ExTraceGlobals.linkedInTracer == null)
				{
					ExTraceGlobals.linkedInTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.linkedInTracer;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x000555EC File Offset: 0x000537EC
		public static Trace MonitoringWebClientTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringWebClientTracer == null)
				{
					ExTraceGlobals.monitoringWebClientTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.monitoringWebClientTracer;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0005560B File Offset: 0x0005380B
		public static Trace RulesBasedHttpModuleTracer
		{
			get
			{
				if (ExTraceGlobals.rulesBasedHttpModuleTracer == null)
				{
					ExTraceGlobals.rulesBasedHttpModuleTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.rulesBasedHttpModuleTracer;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x0005562A File Offset: 0x0005382A
		public static Trace AADClientTracer
		{
			get
			{
				if (ExTraceGlobals.aADClientTracer == null)
				{
					ExTraceGlobals.aADClientTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.aADClientTracer;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x00055649 File Offset: 0x00053849
		public static Trace AppSettingsTracer
		{
			get
			{
				if (ExTraceGlobals.appSettingsTracer == null)
				{
					ExTraceGlobals.appSettingsTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.appSettingsTracer;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00055668 File Offset: 0x00053868
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x040019E1 RID: 6625
		private static Guid componentGuid = new Guid("351632BC-3F4E-4C79-A368-F8E54DCE4A2E");

		// Token: 0x040019E2 RID: 6626
		private static Trace dNSTracer = null;

		// Token: 0x040019E3 RID: 6627
		private static Trace networkTracer = null;

		// Token: 0x040019E4 RID: 6628
		private static Trace authenticationTracer = null;

		// Token: 0x040019E5 RID: 6629
		private static Trace certificateTracer = null;

		// Token: 0x040019E6 RID: 6630
		private static Trace directoryServicesTracer = null;

		// Token: 0x040019E7 RID: 6631
		private static Trace processManagerTracer = null;

		// Token: 0x040019E8 RID: 6632
		private static Trace httpClientTracer = null;

		// Token: 0x040019E9 RID: 6633
		private static Trace protocolLogTracer = null;

		// Token: 0x040019EA RID: 6634
		private static Trace rightsManagementTracer = null;

		// Token: 0x040019EB RID: 6635
		private static Trace liveIDAuthenticationClientTracer = null;

		// Token: 0x040019EC RID: 6636
		private static Trace deltaSyncClientTracer = null;

		// Token: 0x040019ED RID: 6637
		private static Trace deltaSyncResponseHandlerTracer = null;

		// Token: 0x040019EE RID: 6638
		private static Trace languagePackInfoTracer = null;

		// Token: 0x040019EF RID: 6639
		private static Trace wSTrustTracer = null;

		// Token: 0x040019F0 RID: 6640
		private static Trace ewsClientTracer = null;

		// Token: 0x040019F1 RID: 6641
		private static Trace configurationTracer = null;

		// Token: 0x040019F2 RID: 6642
		private static Trace smtpClientTracer = null;

		// Token: 0x040019F3 RID: 6643
		private static Trace xropServiceClientTracer = null;

		// Token: 0x040019F4 RID: 6644
		private static Trace xropServiceServerTracer = null;

		// Token: 0x040019F5 RID: 6645
		private static Trace claimTracer = null;

		// Token: 0x040019F6 RID: 6646
		private static Trace facebookTracer = null;

		// Token: 0x040019F7 RID: 6647
		private static Trace linkedInTracer = null;

		// Token: 0x040019F8 RID: 6648
		private static Trace monitoringWebClientTracer = null;

		// Token: 0x040019F9 RID: 6649
		private static Trace rulesBasedHttpModuleTracer = null;

		// Token: 0x040019FA RID: 6650
		private static Trace aADClientTracer = null;

		// Token: 0x040019FB RID: 6651
		private static Trace appSettingsTracer = null;

		// Token: 0x040019FC RID: 6652
		private static Trace commonTracer = null;
	}
}
