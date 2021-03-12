using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000043 RID: 67
	internal static class AnnotationStrings
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00007DA0 File Offset: 0x00005FA0
		static AnnotationStrings()
		{
			AnnotationStrings.stringIDs.Add(2599184111U, "Title_CsActiveUserDaily");
			AnnotationStrings.stringIDs.Add(916893256U, "Title_TrafficReport");
			AnnotationStrings.stringIDs.Add(2457455276U, "Title_ExternalActivityWeekly");
			AnnotationStrings.stringIDs.Add(1460033953U, "Title_StaleMailboxDetail");
			AnnotationStrings.stringIDs.Add(919006352U, "Title_ConnectionByClientTypeDetailYearly");
			AnnotationStrings.stringIDs.Add(2728899750U, "Title_MailboxUsage");
			AnnotationStrings.stringIDs.Add(4227251602U, "Title_SPOSkyDriveProDeployedMonthly");
			AnnotationStrings.stringIDs.Add(3697180487U, "Title_MailboxUsageDetail");
			AnnotationStrings.stringIDs.Add(2447679595U, "Title_CsActiveUserMonthly");
			AnnotationStrings.stringIDs.Add(1719022488U, "Title_CsAVConferenceTimeDaily");
			AnnotationStrings.stringIDs.Add(4092464326U, "Title_CsP2PAVTimeWeekly");
			AnnotationStrings.stringIDs.Add(548826438U, "Title_ClientOSReport");
			AnnotationStrings.stringIDs.Add(1607544303U, "Title_SPOActiveUserWeekly");
			AnnotationStrings.stringIDs.Add(4116169389U, "Minutes");
			AnnotationStrings.stringIDs.Add(2007411791U, "Title_CsClientDeviceMonthly");
			AnnotationStrings.stringIDs.Add(1173626453U, "Title_ClientBrowserDetailReport");
			AnnotationStrings.stringIDs.Add(4086740914U, "Title_SpamMessageDetail");
			AnnotationStrings.stringIDs.Add(873044688U, "Title_ConnectionByClientTypeDaily");
			AnnotationStrings.stringIDs.Add(2102105265U, "Title_SummaryReport");
			AnnotationStrings.stringIDs.Add(3778010946U, "Title_ExternalActivityByUserWeekly");
			AnnotationStrings.stringIDs.Add(4124840583U, "Title_ConnectionByClientTypeDetailWeekly");
			AnnotationStrings.stringIDs.Add(3104710592U, "Title_MessageTraceDetail");
			AnnotationStrings.stringIDs.Add(3127208035U, "Title_SPOTenantStorageMetricWeekly");
			AnnotationStrings.stringIDs.Add(3560391903U, "NA");
			AnnotationStrings.stringIDs.Add(4116066136U, "SizeMB");
			AnnotationStrings.stringIDs.Add(3142174497U, "Title_ConnectionByClientTypeYearly");
			AnnotationStrings.stringIDs.Add(2449782919U, "Title_SPOSkyDriveProStorageWeekly");
			AnnotationStrings.stringIDs.Add(4117984753U, "Title_MxRecordReport");
			AnnotationStrings.stringIDs.Add(3288860276U, "Title_FilterValueList");
			AnnotationStrings.stringIDs.Add(3807172670U, "Title_SPOSkyDriveProDeployedWeekly");
			AnnotationStrings.stringIDs.Add(3489420090U, "Title_ScorecardClientDevice");
			AnnotationStrings.stringIDs.Add(2483434450U, "Title_CsP2PSessionDaily");
			AnnotationStrings.stringIDs.Add(1637029953U, "Title_MessageTrace");
			AnnotationStrings.stringIDs.Add(2188089486U, "Title_CsConferenceDaily");
			AnnotationStrings.stringIDs.Add(3696480905U, "Title_SPOODBFileActivity");
			AnnotationStrings.stringIDs.Add(1466734612U, "Title_ScorecardMetrics");
			AnnotationStrings.stringIDs.Add(2474312838U, "Title_GroupActivityMonthly");
			AnnotationStrings.stringIDs.Add(3490604815U, "Title_SPOTenantStorageMetricMonthly");
			AnnotationStrings.stringIDs.Add(3438387218U, "Title_StaleMailbox");
			AnnotationStrings.stringIDs.Add(2626245559U, "Title_CsActiveUserWeekly");
			AnnotationStrings.stringIDs.Add(2699807741U, "Title_DeviceDashboardSummaryReport");
			AnnotationStrings.stringIDs.Add(3798591559U, "Title_SPOActiveUserDaily");
			AnnotationStrings.stringIDs.Add(1566437488U, "Title_ClientBrowserReport");
			AnnotationStrings.stringIDs.Add(1406692960U, "Title_MailboxActivityYearly");
			AnnotationStrings.stringIDs.Add(1816522614U, "Title_GroupActivityDaily");
			AnnotationStrings.stringIDs.Add(369410763U, "Title_ScorecardClientOutlook");
			AnnotationStrings.stringIDs.Add(444818027U, "Title_MailboxActivityMonthly");
			AnnotationStrings.stringIDs.Add(2167864011U, "Title_DeviceDetailsReport");
			AnnotationStrings.stringIDs.Add(2938655794U, "Title_CsP2PAVTimeMonthly");
			AnnotationStrings.stringIDs.Add(31883008U, "Title_CsActiveUserYearly");
			AnnotationStrings.stringIDs.Add(1167376766U, "Title_OutboundConnectorReport");
			AnnotationStrings.stringIDs.Add(4159483935U, "Title_MailboxActivityDaily");
			AnnotationStrings.stringIDs.Add(4136517238U, "Title_CsConferenceMonthly");
			AnnotationStrings.stringIDs.Add(4055793796U, "CountOfMailboxes");
			AnnotationStrings.stringIDs.Add(2411970335U, "Title_TopTrafficReport");
			AnnotationStrings.stringIDs.Add(1441569752U, "Title_ConnectionByClientTypeWeekly");
			AnnotationStrings.stringIDs.Add(2900754885U, "Title_SPOTeamSiteStorageMonthly");
			AnnotationStrings.stringIDs.Add(3750041507U, "Title_SPOSkyDriveProStorageMonthly");
			AnnotationStrings.stringIDs.Add(3134462204U, "Title_CsAVConferenceTimeMonthly");
			AnnotationStrings.stringIDs.Add(4032142024U, "Title_ExternalActivityMonthly");
			AnnotationStrings.stringIDs.Add(664019258U, "Title_MalwareMessageDetail");
			AnnotationStrings.stringIDs.Add(1554779067U, "Count");
			AnnotationStrings.stringIDs.Add(2391708396U, "Title_ConnectionByClientTypeMonthly");
			AnnotationStrings.stringIDs.Add(3789075813U, "Title_ClientOSDetailReport");
			AnnotationStrings.stringIDs.Add(1491838153U, "Title_PartnerClientExpiringSubscription");
			AnnotationStrings.stringIDs.Add(4073762434U, "Title_ServiceDeliveryReport");
			AnnotationStrings.stringIDs.Add(1120340038U, "Title_LicenseVsUsageSummary");
			AnnotationStrings.stringIDs.Add(2269495169U, "Title_DlpMessageDetail");
			AnnotationStrings.stringIDs.Add(2874211435U, "Title_SPOTenantStorageMetricDaily");
			AnnotationStrings.stringIDs.Add(3703323930U, "Title_PartnerCustomerUser");
			AnnotationStrings.stringIDs.Add(2188740169U, "Title_ExternalActivityByDomainDaily");
			AnnotationStrings.stringIDs.Add(3901694418U, "Title_PolicyTrafficReport");
			AnnotationStrings.stringIDs.Add(95966325U, "Title_PolicyMessageDetail");
			AnnotationStrings.stringIDs.Add(2331474543U, "Title_MessageDetailReport");
			AnnotationStrings.stringIDs.Add(3348137534U, "Title_ExternalActivityByUserDaily");
			AnnotationStrings.stringIDs.Add(878571178U, "Title_ExternalActivitySummaryWeekly");
			AnnotationStrings.stringIDs.Add(1703063467U, "Title_SPOActiveUserMonthly");
			AnnotationStrings.stringIDs.Add(2654343028U, "Title_ExternalActivityDaily");
			AnnotationStrings.stringIDs.Add(203909934U, "Title_ExternalActivitySummaryMonthly");
			AnnotationStrings.stringIDs.Add(1942412854U, "Title_SPOODBUserStatistics");
			AnnotationStrings.stringIDs.Add(2115947028U, "CountOfGroups");
			AnnotationStrings.stringIDs.Add(2328219770U, "Date");
			AnnotationStrings.stringIDs.Add(3914274640U, "Title_SPOTeamSiteDeployedMonthly");
			AnnotationStrings.stringIDs.Add(1964655722U, "Title_GroupActivityWeekly");
			AnnotationStrings.stringIDs.Add(3192449161U, "Title_ExternalActivityByDomainMonthly");
			AnnotationStrings.stringIDs.Add(2465396958U, "Title_ExternalActivityByUserMonthly");
			AnnotationStrings.stringIDs.Add(719677643U, "Title_ConnectionByClientTypeDetailMonthly");
			AnnotationStrings.stringIDs.Add(4178626464U, "Title_CsAVConferenceTimeWeekly");
			AnnotationStrings.stringIDs.Add(330021932U, "Title_SPOTeamSiteDeployedWeekly");
			AnnotationStrings.stringIDs.Add(1681059041U, "Title_SPOTeamSiteStorageWeekly");
			AnnotationStrings.stringIDs.Add(1464124746U, "Title_CsConferenceWeekly");
			AnnotationStrings.stringIDs.Add(849685939U, "Title_GroupActivityYearly");
			AnnotationStrings.stringIDs.Add(4557591U, "Title_MailboxActivityWeekly");
			AnnotationStrings.stringIDs.Add(3439325998U, "Title_CsP2PAVTimeDaily");
			AnnotationStrings.stringIDs.Add(1215094758U, "ActiveUserCount");
			AnnotationStrings.stringIDs.Add(3177100050U, "Title_ExternalActivitySummaryDaily");
			AnnotationStrings.stringIDs.Add(4215466264U, "CountOfAccounts");
			AnnotationStrings.stringIDs.Add(3807350126U, "Title_CsP2PSessionMonthly");
			AnnotationStrings.stringIDs.Add(1382414506U, "Title_CsP2PSessionWeekly");
			AnnotationStrings.stringIDs.Add(4205497791U, "Title_ConnectionByClientTypeDetailDaily");
			AnnotationStrings.stringIDs.Add(386864173U, "Title_ExternalActivityByDomainWeekly");
			AnnotationStrings.stringIDs.Add(3075209078U, "Title_ScorecardClientOS");
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000085D4 File Offset: 0x000067D4
		public static LocalizedString Title_CsActiveUserDaily
		{
			get
			{
				return new LocalizedString("Title_CsActiveUserDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000085EB File Offset: 0x000067EB
		public static LocalizedString Title_TrafficReport
		{
			get
			{
				return new LocalizedString("Title_TrafficReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00008602 File Offset: 0x00006802
		public static LocalizedString Title_ExternalActivityWeekly
		{
			get
			{
				return new LocalizedString("Title_ExternalActivityWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00008619 File Offset: 0x00006819
		public static LocalizedString Title_StaleMailboxDetail
		{
			get
			{
				return new LocalizedString("Title_StaleMailboxDetail", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00008630 File Offset: 0x00006830
		public static LocalizedString Title_ConnectionByClientTypeDetailYearly
		{
			get
			{
				return new LocalizedString("Title_ConnectionByClientTypeDetailYearly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00008647 File Offset: 0x00006847
		public static LocalizedString Title_MailboxUsage
		{
			get
			{
				return new LocalizedString("Title_MailboxUsage", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000865E File Offset: 0x0000685E
		public static LocalizedString Title_SPOSkyDriveProDeployedMonthly
		{
			get
			{
				return new LocalizedString("Title_SPOSkyDriveProDeployedMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00008675 File Offset: 0x00006875
		public static LocalizedString Title_MailboxUsageDetail
		{
			get
			{
				return new LocalizedString("Title_MailboxUsageDetail", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000868C File Offset: 0x0000688C
		public static LocalizedString Title_CsActiveUserMonthly
		{
			get
			{
				return new LocalizedString("Title_CsActiveUserMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000086A3 File Offset: 0x000068A3
		public static LocalizedString Title_CsAVConferenceTimeDaily
		{
			get
			{
				return new LocalizedString("Title_CsAVConferenceTimeDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000086BA File Offset: 0x000068BA
		public static LocalizedString Title_CsP2PAVTimeWeekly
		{
			get
			{
				return new LocalizedString("Title_CsP2PAVTimeWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000086D1 File Offset: 0x000068D1
		public static LocalizedString Title_ClientOSReport
		{
			get
			{
				return new LocalizedString("Title_ClientOSReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000086E8 File Offset: 0x000068E8
		public static LocalizedString Title_SPOActiveUserWeekly
		{
			get
			{
				return new LocalizedString("Title_SPOActiveUserWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000086FF File Offset: 0x000068FF
		public static LocalizedString Minutes
		{
			get
			{
				return new LocalizedString("Minutes", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00008716 File Offset: 0x00006916
		public static LocalizedString Title_CsClientDeviceMonthly
		{
			get
			{
				return new LocalizedString("Title_CsClientDeviceMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000872D File Offset: 0x0000692D
		public static LocalizedString Title_ClientBrowserDetailReport
		{
			get
			{
				return new LocalizedString("Title_ClientBrowserDetailReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00008744 File Offset: 0x00006944
		public static LocalizedString Title_SpamMessageDetail
		{
			get
			{
				return new LocalizedString("Title_SpamMessageDetail", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000875B File Offset: 0x0000695B
		public static LocalizedString Title_ConnectionByClientTypeDaily
		{
			get
			{
				return new LocalizedString("Title_ConnectionByClientTypeDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00008772 File Offset: 0x00006972
		public static LocalizedString Title_SummaryReport
		{
			get
			{
				return new LocalizedString("Title_SummaryReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00008789 File Offset: 0x00006989
		public static LocalizedString Title_ExternalActivityByUserWeekly
		{
			get
			{
				return new LocalizedString("Title_ExternalActivityByUserWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000087A0 File Offset: 0x000069A0
		public static LocalizedString Title_ConnectionByClientTypeDetailWeekly
		{
			get
			{
				return new LocalizedString("Title_ConnectionByClientTypeDetailWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000087B7 File Offset: 0x000069B7
		public static LocalizedString Title_MessageTraceDetail
		{
			get
			{
				return new LocalizedString("Title_MessageTraceDetail", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000087CE File Offset: 0x000069CE
		public static LocalizedString Title_SPOTenantStorageMetricWeekly
		{
			get
			{
				return new LocalizedString("Title_SPOTenantStorageMetricWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000087E5 File Offset: 0x000069E5
		public static LocalizedString NA
		{
			get
			{
				return new LocalizedString("NA", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000087FC File Offset: 0x000069FC
		public static LocalizedString SizeMB
		{
			get
			{
				return new LocalizedString("SizeMB", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00008813 File Offset: 0x00006A13
		public static LocalizedString Title_ConnectionByClientTypeYearly
		{
			get
			{
				return new LocalizedString("Title_ConnectionByClientTypeYearly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000882A File Offset: 0x00006A2A
		public static LocalizedString Title_SPOSkyDriveProStorageWeekly
		{
			get
			{
				return new LocalizedString("Title_SPOSkyDriveProStorageWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00008841 File Offset: 0x00006A41
		public static LocalizedString Title_MxRecordReport
		{
			get
			{
				return new LocalizedString("Title_MxRecordReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00008858 File Offset: 0x00006A58
		public static LocalizedString Title_FilterValueList
		{
			get
			{
				return new LocalizedString("Title_FilterValueList", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000886F File Offset: 0x00006A6F
		public static LocalizedString Title_SPOSkyDriveProDeployedWeekly
		{
			get
			{
				return new LocalizedString("Title_SPOSkyDriveProDeployedWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00008886 File Offset: 0x00006A86
		public static LocalizedString Title_ScorecardClientDevice
		{
			get
			{
				return new LocalizedString("Title_ScorecardClientDevice", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000889D File Offset: 0x00006A9D
		public static LocalizedString Title_CsP2PSessionDaily
		{
			get
			{
				return new LocalizedString("Title_CsP2PSessionDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000088B4 File Offset: 0x00006AB4
		public static LocalizedString Title_MessageTrace
		{
			get
			{
				return new LocalizedString("Title_MessageTrace", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000088CB File Offset: 0x00006ACB
		public static LocalizedString Title_CsConferenceDaily
		{
			get
			{
				return new LocalizedString("Title_CsConferenceDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000088E2 File Offset: 0x00006AE2
		public static LocalizedString Title_SPOODBFileActivity
		{
			get
			{
				return new LocalizedString("Title_SPOODBFileActivity", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000088F9 File Offset: 0x00006AF9
		public static LocalizedString Title_ScorecardMetrics
		{
			get
			{
				return new LocalizedString("Title_ScorecardMetrics", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00008910 File Offset: 0x00006B10
		public static LocalizedString Title_GroupActivityMonthly
		{
			get
			{
				return new LocalizedString("Title_GroupActivityMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008927 File Offset: 0x00006B27
		public static LocalizedString Title_SPOTenantStorageMetricMonthly
		{
			get
			{
				return new LocalizedString("Title_SPOTenantStorageMetricMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000893E File Offset: 0x00006B3E
		public static LocalizedString Title_StaleMailbox
		{
			get
			{
				return new LocalizedString("Title_StaleMailbox", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00008955 File Offset: 0x00006B55
		public static LocalizedString Title_CsActiveUserWeekly
		{
			get
			{
				return new LocalizedString("Title_CsActiveUserWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000896C File Offset: 0x00006B6C
		public static LocalizedString Title_DeviceDashboardSummaryReport
		{
			get
			{
				return new LocalizedString("Title_DeviceDashboardSummaryReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00008983 File Offset: 0x00006B83
		public static LocalizedString Title_SPOActiveUserDaily
		{
			get
			{
				return new LocalizedString("Title_SPOActiveUserDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000899A File Offset: 0x00006B9A
		public static LocalizedString Title_ClientBrowserReport
		{
			get
			{
				return new LocalizedString("Title_ClientBrowserReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000089B1 File Offset: 0x00006BB1
		public static LocalizedString Title_MailboxActivityYearly
		{
			get
			{
				return new LocalizedString("Title_MailboxActivityYearly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000089C8 File Offset: 0x00006BC8
		public static LocalizedString Title_GroupActivityDaily
		{
			get
			{
				return new LocalizedString("Title_GroupActivityDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000089DF File Offset: 0x00006BDF
		public static LocalizedString Title_ScorecardClientOutlook
		{
			get
			{
				return new LocalizedString("Title_ScorecardClientOutlook", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000089F6 File Offset: 0x00006BF6
		public static LocalizedString Title_MailboxActivityMonthly
		{
			get
			{
				return new LocalizedString("Title_MailboxActivityMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008A0D File Offset: 0x00006C0D
		public static LocalizedString Title_DeviceDetailsReport
		{
			get
			{
				return new LocalizedString("Title_DeviceDetailsReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008A24 File Offset: 0x00006C24
		public static LocalizedString Title_CsP2PAVTimeMonthly
		{
			get
			{
				return new LocalizedString("Title_CsP2PAVTimeMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008A3B File Offset: 0x00006C3B
		public static LocalizedString Title_CsActiveUserYearly
		{
			get
			{
				return new LocalizedString("Title_CsActiveUserYearly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00008A52 File Offset: 0x00006C52
		public static LocalizedString Title_OutboundConnectorReport
		{
			get
			{
				return new LocalizedString("Title_OutboundConnectorReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008A69 File Offset: 0x00006C69
		public static LocalizedString Title_MailboxActivityDaily
		{
			get
			{
				return new LocalizedString("Title_MailboxActivityDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00008A80 File Offset: 0x00006C80
		public static LocalizedString Title_CsConferenceMonthly
		{
			get
			{
				return new LocalizedString("Title_CsConferenceMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008A97 File Offset: 0x00006C97
		public static LocalizedString CountOfMailboxes
		{
			get
			{
				return new LocalizedString("CountOfMailboxes", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00008AAE File Offset: 0x00006CAE
		public static LocalizedString Title_TopTrafficReport
		{
			get
			{
				return new LocalizedString("Title_TopTrafficReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00008AC5 File Offset: 0x00006CC5
		public static LocalizedString Title_ConnectionByClientTypeWeekly
		{
			get
			{
				return new LocalizedString("Title_ConnectionByClientTypeWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00008ADC File Offset: 0x00006CDC
		public static LocalizedString Title_SPOTeamSiteStorageMonthly
		{
			get
			{
				return new LocalizedString("Title_SPOTeamSiteStorageMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00008AF3 File Offset: 0x00006CF3
		public static LocalizedString Title_SPOSkyDriveProStorageMonthly
		{
			get
			{
				return new LocalizedString("Title_SPOSkyDriveProStorageMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00008B0A File Offset: 0x00006D0A
		public static LocalizedString Title_CsAVConferenceTimeMonthly
		{
			get
			{
				return new LocalizedString("Title_CsAVConferenceTimeMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00008B21 File Offset: 0x00006D21
		public static LocalizedString Title_ExternalActivityMonthly
		{
			get
			{
				return new LocalizedString("Title_ExternalActivityMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00008B38 File Offset: 0x00006D38
		public static LocalizedString Title_MalwareMessageDetail
		{
			get
			{
				return new LocalizedString("Title_MalwareMessageDetail", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008B4F File Offset: 0x00006D4F
		public static LocalizedString Count
		{
			get
			{
				return new LocalizedString("Count", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00008B66 File Offset: 0x00006D66
		public static LocalizedString Title_ConnectionByClientTypeMonthly
		{
			get
			{
				return new LocalizedString("Title_ConnectionByClientTypeMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008B7D File Offset: 0x00006D7D
		public static LocalizedString Title_ClientOSDetailReport
		{
			get
			{
				return new LocalizedString("Title_ClientOSDetailReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00008B94 File Offset: 0x00006D94
		public static LocalizedString Title_PartnerClientExpiringSubscription
		{
			get
			{
				return new LocalizedString("Title_PartnerClientExpiringSubscription", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008BAB File Offset: 0x00006DAB
		public static LocalizedString Title_ServiceDeliveryReport
		{
			get
			{
				return new LocalizedString("Title_ServiceDeliveryReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00008BC2 File Offset: 0x00006DC2
		public static LocalizedString Title_LicenseVsUsageSummary
		{
			get
			{
				return new LocalizedString("Title_LicenseVsUsageSummary", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008BD9 File Offset: 0x00006DD9
		public static LocalizedString Title_DlpMessageDetail
		{
			get
			{
				return new LocalizedString("Title_DlpMessageDetail", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00008BF0 File Offset: 0x00006DF0
		public static LocalizedString Title_SPOTenantStorageMetricDaily
		{
			get
			{
				return new LocalizedString("Title_SPOTenantStorageMetricDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008C07 File Offset: 0x00006E07
		public static LocalizedString Title_PartnerCustomerUser
		{
			get
			{
				return new LocalizedString("Title_PartnerCustomerUser", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00008C1E File Offset: 0x00006E1E
		public static LocalizedString Title_ExternalActivityByDomainDaily
		{
			get
			{
				return new LocalizedString("Title_ExternalActivityByDomainDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008C35 File Offset: 0x00006E35
		public static LocalizedString Title_PolicyTrafficReport
		{
			get
			{
				return new LocalizedString("Title_PolicyTrafficReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00008C4C File Offset: 0x00006E4C
		public static LocalizedString Title_PolicyMessageDetail
		{
			get
			{
				return new LocalizedString("Title_PolicyMessageDetail", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00008C63 File Offset: 0x00006E63
		public static LocalizedString Title_MessageDetailReport
		{
			get
			{
				return new LocalizedString("Title_MessageDetailReport", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00008C7A File Offset: 0x00006E7A
		public static LocalizedString Title_ExternalActivityByUserDaily
		{
			get
			{
				return new LocalizedString("Title_ExternalActivityByUserDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00008C91 File Offset: 0x00006E91
		public static LocalizedString Title_ExternalActivitySummaryWeekly
		{
			get
			{
				return new LocalizedString("Title_ExternalActivitySummaryWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00008CA8 File Offset: 0x00006EA8
		public static LocalizedString Title_SPOActiveUserMonthly
		{
			get
			{
				return new LocalizedString("Title_SPOActiveUserMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00008CBF File Offset: 0x00006EBF
		public static LocalizedString Title_ExternalActivityDaily
		{
			get
			{
				return new LocalizedString("Title_ExternalActivityDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00008CD6 File Offset: 0x00006ED6
		public static LocalizedString Title_ExternalActivitySummaryMonthly
		{
			get
			{
				return new LocalizedString("Title_ExternalActivitySummaryMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008CED File Offset: 0x00006EED
		public static LocalizedString Title_SPOODBUserStatistics
		{
			get
			{
				return new LocalizedString("Title_SPOODBUserStatistics", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00008D04 File Offset: 0x00006F04
		public static LocalizedString CountOfGroups
		{
			get
			{
				return new LocalizedString("CountOfGroups", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00008D1B File Offset: 0x00006F1B
		public static LocalizedString Date
		{
			get
			{
				return new LocalizedString("Date", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00008D32 File Offset: 0x00006F32
		public static LocalizedString Title_SPOTeamSiteDeployedMonthly
		{
			get
			{
				return new LocalizedString("Title_SPOTeamSiteDeployedMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00008D49 File Offset: 0x00006F49
		public static LocalizedString Title_GroupActivityWeekly
		{
			get
			{
				return new LocalizedString("Title_GroupActivityWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00008D60 File Offset: 0x00006F60
		public static LocalizedString Title_ExternalActivityByDomainMonthly
		{
			get
			{
				return new LocalizedString("Title_ExternalActivityByDomainMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00008D77 File Offset: 0x00006F77
		public static LocalizedString Title_ExternalActivityByUserMonthly
		{
			get
			{
				return new LocalizedString("Title_ExternalActivityByUserMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00008D8E File Offset: 0x00006F8E
		public static LocalizedString Title_ConnectionByClientTypeDetailMonthly
		{
			get
			{
				return new LocalizedString("Title_ConnectionByClientTypeDetailMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00008DA5 File Offset: 0x00006FA5
		public static LocalizedString Title_CsAVConferenceTimeWeekly
		{
			get
			{
				return new LocalizedString("Title_CsAVConferenceTimeWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00008DBC File Offset: 0x00006FBC
		public static LocalizedString Title_SPOTeamSiteDeployedWeekly
		{
			get
			{
				return new LocalizedString("Title_SPOTeamSiteDeployedWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008DD3 File Offset: 0x00006FD3
		public static LocalizedString Title_SPOTeamSiteStorageWeekly
		{
			get
			{
				return new LocalizedString("Title_SPOTeamSiteStorageWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00008DEA File Offset: 0x00006FEA
		public static LocalizedString Title_CsConferenceWeekly
		{
			get
			{
				return new LocalizedString("Title_CsConferenceWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008E01 File Offset: 0x00007001
		public static LocalizedString Title_GroupActivityYearly
		{
			get
			{
				return new LocalizedString("Title_GroupActivityYearly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008E18 File Offset: 0x00007018
		public static LocalizedString Title_MailboxActivityWeekly
		{
			get
			{
				return new LocalizedString("Title_MailboxActivityWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00008E2F File Offset: 0x0000702F
		public static LocalizedString Title_CsP2PAVTimeDaily
		{
			get
			{
				return new LocalizedString("Title_CsP2PAVTimeDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008E46 File Offset: 0x00007046
		public static LocalizedString ActiveUserCount
		{
			get
			{
				return new LocalizedString("ActiveUserCount", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008E5D File Offset: 0x0000705D
		public static LocalizedString Title_ExternalActivitySummaryDaily
		{
			get
			{
				return new LocalizedString("Title_ExternalActivitySummaryDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00008E74 File Offset: 0x00007074
		public static LocalizedString CountOfAccounts
		{
			get
			{
				return new LocalizedString("CountOfAccounts", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00008E8B File Offset: 0x0000708B
		public static LocalizedString Title_CsP2PSessionMonthly
		{
			get
			{
				return new LocalizedString("Title_CsP2PSessionMonthly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00008EA2 File Offset: 0x000070A2
		public static LocalizedString Title_CsP2PSessionWeekly
		{
			get
			{
				return new LocalizedString("Title_CsP2PSessionWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00008EB9 File Offset: 0x000070B9
		public static LocalizedString Title_ConnectionByClientTypeDetailDaily
		{
			get
			{
				return new LocalizedString("Title_ConnectionByClientTypeDetailDaily", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008ED0 File Offset: 0x000070D0
		public static LocalizedString Title_ExternalActivityByDomainWeekly
		{
			get
			{
				return new LocalizedString("Title_ExternalActivityByDomainWeekly", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00008EE7 File Offset: 0x000070E7
		public static LocalizedString Title_ScorecardClientOS
		{
			get
			{
				return new LocalizedString("Title_ScorecardClientOS", AnnotationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008EFE File Offset: 0x000070FE
		public static LocalizedString GetLocalizedString(AnnotationStrings.IDs key)
		{
			return new LocalizedString(AnnotationStrings.stringIDs[(uint)key], AnnotationStrings.ResourceManager, new object[0]);
		}

		// Token: 0x040000D0 RID: 208
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(102);

		// Token: 0x040000D1 RID: 209
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.ReportingWebService.AnnotationStrings", typeof(AnnotationStrings).GetTypeInfo().Assembly);

		// Token: 0x02000044 RID: 68
		public enum IDs : uint
		{
			// Token: 0x040000D3 RID: 211
			Title_CsActiveUserDaily = 2599184111U,
			// Token: 0x040000D4 RID: 212
			Title_TrafficReport = 916893256U,
			// Token: 0x040000D5 RID: 213
			Title_ExternalActivityWeekly = 2457455276U,
			// Token: 0x040000D6 RID: 214
			Title_StaleMailboxDetail = 1460033953U,
			// Token: 0x040000D7 RID: 215
			Title_ConnectionByClientTypeDetailYearly = 919006352U,
			// Token: 0x040000D8 RID: 216
			Title_MailboxUsage = 2728899750U,
			// Token: 0x040000D9 RID: 217
			Title_SPOSkyDriveProDeployedMonthly = 4227251602U,
			// Token: 0x040000DA RID: 218
			Title_MailboxUsageDetail = 3697180487U,
			// Token: 0x040000DB RID: 219
			Title_CsActiveUserMonthly = 2447679595U,
			// Token: 0x040000DC RID: 220
			Title_CsAVConferenceTimeDaily = 1719022488U,
			// Token: 0x040000DD RID: 221
			Title_CsP2PAVTimeWeekly = 4092464326U,
			// Token: 0x040000DE RID: 222
			Title_ClientOSReport = 548826438U,
			// Token: 0x040000DF RID: 223
			Title_SPOActiveUserWeekly = 1607544303U,
			// Token: 0x040000E0 RID: 224
			Minutes = 4116169389U,
			// Token: 0x040000E1 RID: 225
			Title_CsClientDeviceMonthly = 2007411791U,
			// Token: 0x040000E2 RID: 226
			Title_ClientBrowserDetailReport = 1173626453U,
			// Token: 0x040000E3 RID: 227
			Title_SpamMessageDetail = 4086740914U,
			// Token: 0x040000E4 RID: 228
			Title_ConnectionByClientTypeDaily = 873044688U,
			// Token: 0x040000E5 RID: 229
			Title_SummaryReport = 2102105265U,
			// Token: 0x040000E6 RID: 230
			Title_ExternalActivityByUserWeekly = 3778010946U,
			// Token: 0x040000E7 RID: 231
			Title_ConnectionByClientTypeDetailWeekly = 4124840583U,
			// Token: 0x040000E8 RID: 232
			Title_MessageTraceDetail = 3104710592U,
			// Token: 0x040000E9 RID: 233
			Title_SPOTenantStorageMetricWeekly = 3127208035U,
			// Token: 0x040000EA RID: 234
			NA = 3560391903U,
			// Token: 0x040000EB RID: 235
			SizeMB = 4116066136U,
			// Token: 0x040000EC RID: 236
			Title_ConnectionByClientTypeYearly = 3142174497U,
			// Token: 0x040000ED RID: 237
			Title_SPOSkyDriveProStorageWeekly = 2449782919U,
			// Token: 0x040000EE RID: 238
			Title_MxRecordReport = 4117984753U,
			// Token: 0x040000EF RID: 239
			Title_FilterValueList = 3288860276U,
			// Token: 0x040000F0 RID: 240
			Title_SPOSkyDriveProDeployedWeekly = 3807172670U,
			// Token: 0x040000F1 RID: 241
			Title_ScorecardClientDevice = 3489420090U,
			// Token: 0x040000F2 RID: 242
			Title_CsP2PSessionDaily = 2483434450U,
			// Token: 0x040000F3 RID: 243
			Title_MessageTrace = 1637029953U,
			// Token: 0x040000F4 RID: 244
			Title_CsConferenceDaily = 2188089486U,
			// Token: 0x040000F5 RID: 245
			Title_SPOODBFileActivity = 3696480905U,
			// Token: 0x040000F6 RID: 246
			Title_ScorecardMetrics = 1466734612U,
			// Token: 0x040000F7 RID: 247
			Title_GroupActivityMonthly = 2474312838U,
			// Token: 0x040000F8 RID: 248
			Title_SPOTenantStorageMetricMonthly = 3490604815U,
			// Token: 0x040000F9 RID: 249
			Title_StaleMailbox = 3438387218U,
			// Token: 0x040000FA RID: 250
			Title_CsActiveUserWeekly = 2626245559U,
			// Token: 0x040000FB RID: 251
			Title_DeviceDashboardSummaryReport = 2699807741U,
			// Token: 0x040000FC RID: 252
			Title_SPOActiveUserDaily = 3798591559U,
			// Token: 0x040000FD RID: 253
			Title_ClientBrowserReport = 1566437488U,
			// Token: 0x040000FE RID: 254
			Title_MailboxActivityYearly = 1406692960U,
			// Token: 0x040000FF RID: 255
			Title_GroupActivityDaily = 1816522614U,
			// Token: 0x04000100 RID: 256
			Title_ScorecardClientOutlook = 369410763U,
			// Token: 0x04000101 RID: 257
			Title_MailboxActivityMonthly = 444818027U,
			// Token: 0x04000102 RID: 258
			Title_DeviceDetailsReport = 2167864011U,
			// Token: 0x04000103 RID: 259
			Title_CsP2PAVTimeMonthly = 2938655794U,
			// Token: 0x04000104 RID: 260
			Title_CsActiveUserYearly = 31883008U,
			// Token: 0x04000105 RID: 261
			Title_OutboundConnectorReport = 1167376766U,
			// Token: 0x04000106 RID: 262
			Title_MailboxActivityDaily = 4159483935U,
			// Token: 0x04000107 RID: 263
			Title_CsConferenceMonthly = 4136517238U,
			// Token: 0x04000108 RID: 264
			CountOfMailboxes = 4055793796U,
			// Token: 0x04000109 RID: 265
			Title_TopTrafficReport = 2411970335U,
			// Token: 0x0400010A RID: 266
			Title_ConnectionByClientTypeWeekly = 1441569752U,
			// Token: 0x0400010B RID: 267
			Title_SPOTeamSiteStorageMonthly = 2900754885U,
			// Token: 0x0400010C RID: 268
			Title_SPOSkyDriveProStorageMonthly = 3750041507U,
			// Token: 0x0400010D RID: 269
			Title_CsAVConferenceTimeMonthly = 3134462204U,
			// Token: 0x0400010E RID: 270
			Title_ExternalActivityMonthly = 4032142024U,
			// Token: 0x0400010F RID: 271
			Title_MalwareMessageDetail = 664019258U,
			// Token: 0x04000110 RID: 272
			Count = 1554779067U,
			// Token: 0x04000111 RID: 273
			Title_ConnectionByClientTypeMonthly = 2391708396U,
			// Token: 0x04000112 RID: 274
			Title_ClientOSDetailReport = 3789075813U,
			// Token: 0x04000113 RID: 275
			Title_PartnerClientExpiringSubscription = 1491838153U,
			// Token: 0x04000114 RID: 276
			Title_ServiceDeliveryReport = 4073762434U,
			// Token: 0x04000115 RID: 277
			Title_LicenseVsUsageSummary = 1120340038U,
			// Token: 0x04000116 RID: 278
			Title_DlpMessageDetail = 2269495169U,
			// Token: 0x04000117 RID: 279
			Title_SPOTenantStorageMetricDaily = 2874211435U,
			// Token: 0x04000118 RID: 280
			Title_PartnerCustomerUser = 3703323930U,
			// Token: 0x04000119 RID: 281
			Title_ExternalActivityByDomainDaily = 2188740169U,
			// Token: 0x0400011A RID: 282
			Title_PolicyTrafficReport = 3901694418U,
			// Token: 0x0400011B RID: 283
			Title_PolicyMessageDetail = 95966325U,
			// Token: 0x0400011C RID: 284
			Title_MessageDetailReport = 2331474543U,
			// Token: 0x0400011D RID: 285
			Title_ExternalActivityByUserDaily = 3348137534U,
			// Token: 0x0400011E RID: 286
			Title_ExternalActivitySummaryWeekly = 878571178U,
			// Token: 0x0400011F RID: 287
			Title_SPOActiveUserMonthly = 1703063467U,
			// Token: 0x04000120 RID: 288
			Title_ExternalActivityDaily = 2654343028U,
			// Token: 0x04000121 RID: 289
			Title_ExternalActivitySummaryMonthly = 203909934U,
			// Token: 0x04000122 RID: 290
			Title_SPOODBUserStatistics = 1942412854U,
			// Token: 0x04000123 RID: 291
			CountOfGroups = 2115947028U,
			// Token: 0x04000124 RID: 292
			Date = 2328219770U,
			// Token: 0x04000125 RID: 293
			Title_SPOTeamSiteDeployedMonthly = 3914274640U,
			// Token: 0x04000126 RID: 294
			Title_GroupActivityWeekly = 1964655722U,
			// Token: 0x04000127 RID: 295
			Title_ExternalActivityByDomainMonthly = 3192449161U,
			// Token: 0x04000128 RID: 296
			Title_ExternalActivityByUserMonthly = 2465396958U,
			// Token: 0x04000129 RID: 297
			Title_ConnectionByClientTypeDetailMonthly = 719677643U,
			// Token: 0x0400012A RID: 298
			Title_CsAVConferenceTimeWeekly = 4178626464U,
			// Token: 0x0400012B RID: 299
			Title_SPOTeamSiteDeployedWeekly = 330021932U,
			// Token: 0x0400012C RID: 300
			Title_SPOTeamSiteStorageWeekly = 1681059041U,
			// Token: 0x0400012D RID: 301
			Title_CsConferenceWeekly = 1464124746U,
			// Token: 0x0400012E RID: 302
			Title_GroupActivityYearly = 849685939U,
			// Token: 0x0400012F RID: 303
			Title_MailboxActivityWeekly = 4557591U,
			// Token: 0x04000130 RID: 304
			Title_CsP2PAVTimeDaily = 3439325998U,
			// Token: 0x04000131 RID: 305
			ActiveUserCount = 1215094758U,
			// Token: 0x04000132 RID: 306
			Title_ExternalActivitySummaryDaily = 3177100050U,
			// Token: 0x04000133 RID: 307
			CountOfAccounts = 4215466264U,
			// Token: 0x04000134 RID: 308
			Title_CsP2PSessionMonthly = 3807350126U,
			// Token: 0x04000135 RID: 309
			Title_CsP2PSessionWeekly = 1382414506U,
			// Token: 0x04000136 RID: 310
			Title_ConnectionByClientTypeDetailDaily = 4205497791U,
			// Token: 0x04000137 RID: 311
			Title_ExternalActivityByDomainWeekly = 386864173U,
			// Token: 0x04000138 RID: 312
			Title_ScorecardClientOS = 3075209078U
		}
	}
}
