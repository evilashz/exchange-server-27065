using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Data.Storage
{
	// Token: 0x0200032E RID: 814
	public static class ExTraceGlobals
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0004CEE1 File Offset: 0x0004B0E1
		public static Trace StorageTracer
		{
			get
			{
				if (ExTraceGlobals.storageTracer == null)
				{
					ExTraceGlobals.storageTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.storageTracer;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0004CEFF File Offset: 0x0004B0FF
		public static Trace InteropTracer
		{
			get
			{
				if (ExTraceGlobals.interopTracer == null)
				{
					ExTraceGlobals.interopTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.interopTracer;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0004CF1D File Offset: 0x0004B11D
		public static Trace MeetingMessageTracer
		{
			get
			{
				if (ExTraceGlobals.meetingMessageTracer == null)
				{
					ExTraceGlobals.meetingMessageTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.meetingMessageTracer;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0004CF3B File Offset: 0x0004B13B
		public static Trace EventTracer
		{
			get
			{
				if (ExTraceGlobals.eventTracer == null)
				{
					ExTraceGlobals.eventTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.eventTracer;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0004CF59 File Offset: 0x0004B159
		public static Trace DisposeTracer
		{
			get
			{
				if (ExTraceGlobals.disposeTracer == null)
				{
					ExTraceGlobals.disposeTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.disposeTracer;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0004CF77 File Offset: 0x0004B177
		public static Trace ServiceDiscoveryTracer
		{
			get
			{
				if (ExTraceGlobals.serviceDiscoveryTracer == null)
				{
					ExTraceGlobals.serviceDiscoveryTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.serviceDiscoveryTracer;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0004CF95 File Offset: 0x0004B195
		public static Trace ContextTracer
		{
			get
			{
				if (ExTraceGlobals.contextTracer == null)
				{
					ExTraceGlobals.contextTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.contextTracer;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0004CFB3 File Offset: 0x0004B1B3
		public static Trace ContextShadowTracer
		{
			get
			{
				if (ExTraceGlobals.contextShadowTracer == null)
				{
					ExTraceGlobals.contextShadowTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.contextShadowTracer;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0004CFD1 File Offset: 0x0004B1D1
		public static Trace CcGenericTracer
		{
			get
			{
				if (ExTraceGlobals.ccGenericTracer == null)
				{
					ExTraceGlobals.ccGenericTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.ccGenericTracer;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0004CFEF File Offset: 0x0004B1EF
		public static Trace CcOleTracer
		{
			get
			{
				if (ExTraceGlobals.ccOleTracer == null)
				{
					ExTraceGlobals.ccOleTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.ccOleTracer;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0004D00E File Offset: 0x0004B20E
		public static Trace CcBodyTracer
		{
			get
			{
				if (ExTraceGlobals.ccBodyTracer == null)
				{
					ExTraceGlobals.ccBodyTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.ccBodyTracer;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x0004D02D File Offset: 0x0004B22D
		public static Trace CcInboundGenericTracer
		{
			get
			{
				if (ExTraceGlobals.ccInboundGenericTracer == null)
				{
					ExTraceGlobals.ccInboundGenericTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.ccInboundGenericTracer;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x0004D04C File Offset: 0x0004B24C
		public static Trace CcInboundMimeTracer
		{
			get
			{
				if (ExTraceGlobals.ccInboundMimeTracer == null)
				{
					ExTraceGlobals.ccInboundMimeTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.ccInboundMimeTracer;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0004D06B File Offset: 0x0004B26B
		public static Trace CcInboundTnefTracer
		{
			get
			{
				if (ExTraceGlobals.ccInboundTnefTracer == null)
				{
					ExTraceGlobals.ccInboundTnefTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.ccInboundTnefTracer;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x0004D08A File Offset: 0x0004B28A
		public static Trace CcOutboundGenericTracer
		{
			get
			{
				if (ExTraceGlobals.ccOutboundGenericTracer == null)
				{
					ExTraceGlobals.ccOutboundGenericTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.ccOutboundGenericTracer;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0004D0A9 File Offset: 0x0004B2A9
		public static Trace CcOutboundMimeTracer
		{
			get
			{
				if (ExTraceGlobals.ccOutboundMimeTracer == null)
				{
					ExTraceGlobals.ccOutboundMimeTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.ccOutboundMimeTracer;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0004D0C8 File Offset: 0x0004B2C8
		public static Trace CcOutboundTnefTracer
		{
			get
			{
				if (ExTraceGlobals.ccOutboundTnefTracer == null)
				{
					ExTraceGlobals.ccOutboundTnefTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.ccOutboundTnefTracer;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0004D0E7 File Offset: 0x0004B2E7
		public static Trace CcPFDTracer
		{
			get
			{
				if (ExTraceGlobals.ccPFDTracer == null)
				{
					ExTraceGlobals.ccPFDTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.ccPFDTracer;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0004D106 File Offset: 0x0004B306
		public static Trace SessionTracer
		{
			get
			{
				if (ExTraceGlobals.sessionTracer == null)
				{
					ExTraceGlobals.sessionTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.sessionTracer;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0004D125 File Offset: 0x0004B325
		public static Trace DefaultFoldersTracer
		{
			get
			{
				if (ExTraceGlobals.defaultFoldersTracer == null)
				{
					ExTraceGlobals.defaultFoldersTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.defaultFoldersTracer;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x0004D144 File Offset: 0x0004B344
		public static Trace UserConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.userConfigurationTracer == null)
				{
					ExTraceGlobals.userConfigurationTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.userConfigurationTracer;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0004D163 File Offset: 0x0004B363
		public static Trace PropertyBagTracer
		{
			get
			{
				if (ExTraceGlobals.propertyBagTracer == null)
				{
					ExTraceGlobals.propertyBagTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.propertyBagTracer;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0004D182 File Offset: 0x0004B382
		public static Trace TaskTracer
		{
			get
			{
				if (ExTraceGlobals.taskTracer == null)
				{
					ExTraceGlobals.taskTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.taskTracer;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0004D1A1 File Offset: 0x0004B3A1
		public static Trace RecurrenceTracer
		{
			get
			{
				if (ExTraceGlobals.recurrenceTracer == null)
				{
					ExTraceGlobals.recurrenceTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.recurrenceTracer;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0004D1C0 File Offset: 0x0004B3C0
		public static Trace WorkHoursTracer
		{
			get
			{
				if (ExTraceGlobals.workHoursTracer == null)
				{
					ExTraceGlobals.workHoursTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.workHoursTracer;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0004D1DF File Offset: 0x0004B3DF
		public static Trace SyncTracer
		{
			get
			{
				if (ExTraceGlobals.syncTracer == null)
				{
					ExTraceGlobals.syncTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.syncTracer;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0004D1FE File Offset: 0x0004B3FE
		public static Trace ICalTracer
		{
			get
			{
				if (ExTraceGlobals.iCalTracer == null)
				{
					ExTraceGlobals.iCalTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.iCalTracer;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0004D21D File Offset: 0x0004B41D
		public static Trace ActiveManagerClientTracer
		{
			get
			{
				if (ExTraceGlobals.activeManagerClientTracer == null)
				{
					ExTraceGlobals.activeManagerClientTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.activeManagerClientTracer;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x0004D23C File Offset: 0x0004B43C
		public static Trace CcOutboundVCardTracer
		{
			get
			{
				if (ExTraceGlobals.ccOutboundVCardTracer == null)
				{
					ExTraceGlobals.ccOutboundVCardTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.ccOutboundVCardTracer;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0004D25B File Offset: 0x0004B45B
		public static Trace CcInboundVCardTracer
		{
			get
			{
				if (ExTraceGlobals.ccInboundVCardTracer == null)
				{
					ExTraceGlobals.ccInboundVCardTracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.ccInboundVCardTracer;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x0004D27A File Offset: 0x0004B47A
		public static Trace SharingTracer
		{
			get
			{
				if (ExTraceGlobals.sharingTracer == null)
				{
					ExTraceGlobals.sharingTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.sharingTracer;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0004D299 File Offset: 0x0004B499
		public static Trace RightsManagementTracer
		{
			get
			{
				if (ExTraceGlobals.rightsManagementTracer == null)
				{
					ExTraceGlobals.rightsManagementTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.rightsManagementTracer;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0004D2B8 File Offset: 0x0004B4B8
		public static Trace DatabaseAvailabilityGroupTracer
		{
			get
			{
				if (ExTraceGlobals.databaseAvailabilityGroupTracer == null)
				{
					ExTraceGlobals.databaseAvailabilityGroupTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.databaseAvailabilityGroupTracer;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0004D2D7 File Offset: 0x0004B4D7
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0004D2F6 File Offset: 0x0004B4F6
		public static Trace SmtpServiceTracer
		{
			get
			{
				if (ExTraceGlobals.smtpServiceTracer == null)
				{
					ExTraceGlobals.smtpServiceTracer = new Trace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.smtpServiceTracer;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0004D315 File Offset: 0x0004B515
		public static Trace MapiConnectivityTracer
		{
			get
			{
				if (ExTraceGlobals.mapiConnectivityTracer == null)
				{
					ExTraceGlobals.mapiConnectivityTracer = new Trace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.mapiConnectivityTracer;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0004D334 File Offset: 0x0004B534
		public static Trace XtcTracer
		{
			get
			{
				if (ExTraceGlobals.xtcTracer == null)
				{
					ExTraceGlobals.xtcTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.xtcTracer;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0004D353 File Offset: 0x0004B553
		public static Trace CalendarLoggingTracer
		{
			get
			{
				if (ExTraceGlobals.calendarLoggingTracer == null)
				{
					ExTraceGlobals.calendarLoggingTracer = new Trace(ExTraceGlobals.componentGuid, 38);
				}
				return ExTraceGlobals.calendarLoggingTracer;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0004D372 File Offset: 0x0004B572
		public static Trace CalendarSeriesTracer
		{
			get
			{
				if (ExTraceGlobals.calendarSeriesTracer == null)
				{
					ExTraceGlobals.calendarSeriesTracer = new Trace(ExTraceGlobals.componentGuid, 39);
				}
				return ExTraceGlobals.calendarSeriesTracer;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0004D391 File Offset: 0x0004B591
		public static Trace BirthdayCalendarTracer
		{
			get
			{
				if (ExTraceGlobals.birthdayCalendarTracer == null)
				{
					ExTraceGlobals.birthdayCalendarTracer = new Trace(ExTraceGlobals.componentGuid, 40);
				}
				return ExTraceGlobals.birthdayCalendarTracer;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0004D3B0 File Offset: 0x0004B5B0
		public static Trace PropertyMappingTracer
		{
			get
			{
				if (ExTraceGlobals.propertyMappingTracer == null)
				{
					ExTraceGlobals.propertyMappingTracer = new Trace(ExTraceGlobals.componentGuid, 50);
				}
				return ExTraceGlobals.propertyMappingTracer;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0004D3CF File Offset: 0x0004B5CF
		public static Trace MdbResourceHealthTracer
		{
			get
			{
				if (ExTraceGlobals.mdbResourceHealthTracer == null)
				{
					ExTraceGlobals.mdbResourceHealthTracer = new Trace(ExTraceGlobals.componentGuid, 51);
				}
				return ExTraceGlobals.mdbResourceHealthTracer;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0004D3EE File Offset: 0x0004B5EE
		public static Trace ContactLinkingTracer
		{
			get
			{
				if (ExTraceGlobals.contactLinkingTracer == null)
				{
					ExTraceGlobals.contactLinkingTracer = new Trace(ExTraceGlobals.componentGuid, 52);
				}
				return ExTraceGlobals.contactLinkingTracer;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0004D40D File Offset: 0x0004B60D
		public static Trace UserPhotosTracer
		{
			get
			{
				if (ExTraceGlobals.userPhotosTracer == null)
				{
					ExTraceGlobals.userPhotosTracer = new Trace(ExTraceGlobals.componentGuid, 53);
				}
				return ExTraceGlobals.userPhotosTracer;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x0004D42C File Offset: 0x0004B62C
		public static Trace ContactFoldersEnumeratorTracer
		{
			get
			{
				if (ExTraceGlobals.contactFoldersEnumeratorTracer == null)
				{
					ExTraceGlobals.contactFoldersEnumeratorTracer = new Trace(ExTraceGlobals.componentGuid, 54);
				}
				return ExTraceGlobals.contactFoldersEnumeratorTracer;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0004D44B File Offset: 0x0004B64B
		public static Trace MyContactsFolderTracer
		{
			get
			{
				if (ExTraceGlobals.myContactsFolderTracer == null)
				{
					ExTraceGlobals.myContactsFolderTracer = new Trace(ExTraceGlobals.componentGuid, 55);
				}
				return ExTraceGlobals.myContactsFolderTracer;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0004D46A File Offset: 0x0004B66A
		public static Trace AggregationTracer
		{
			get
			{
				if (ExTraceGlobals.aggregationTracer == null)
				{
					ExTraceGlobals.aggregationTracer = new Trace(ExTraceGlobals.componentGuid, 56);
				}
				return ExTraceGlobals.aggregationTracer;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0004D489 File Offset: 0x0004B689
		public static Trace OutlookSocialConnectorInteropTracer
		{
			get
			{
				if (ExTraceGlobals.outlookSocialConnectorInteropTracer == null)
				{
					ExTraceGlobals.outlookSocialConnectorInteropTracer = new Trace(ExTraceGlobals.componentGuid, 57);
				}
				return ExTraceGlobals.outlookSocialConnectorInteropTracer;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0004D4A8 File Offset: 0x0004B6A8
		public static Trace PersonTracer
		{
			get
			{
				if (ExTraceGlobals.personTracer == null)
				{
					ExTraceGlobals.personTracer = new Trace(ExTraceGlobals.componentGuid, 58);
				}
				return ExTraceGlobals.personTracer;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x0004D4C7 File Offset: 0x0004B6C7
		public static Trace DatabasePingerTracer
		{
			get
			{
				if (ExTraceGlobals.databasePingerTracer == null)
				{
					ExTraceGlobals.databasePingerTracer = new Trace(ExTraceGlobals.componentGuid, 60);
				}
				return ExTraceGlobals.databasePingerTracer;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0004D4E6 File Offset: 0x0004B6E6
		public static Trace ContactsEnumeratorTracer
		{
			get
			{
				if (ExTraceGlobals.contactsEnumeratorTracer == null)
				{
					ExTraceGlobals.contactsEnumeratorTracer = new Trace(ExTraceGlobals.componentGuid, 62);
				}
				return ExTraceGlobals.contactsEnumeratorTracer;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0004D505 File Offset: 0x0004B705
		public static Trace ContactChangeLoggingTracer
		{
			get
			{
				if (ExTraceGlobals.contactChangeLoggingTracer == null)
				{
					ExTraceGlobals.contactChangeLoggingTracer = new Trace(ExTraceGlobals.componentGuid, 63);
				}
				return ExTraceGlobals.contactChangeLoggingTracer;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0004D524 File Offset: 0x0004B724
		public static Trace ContactExporterTracer
		{
			get
			{
				if (ExTraceGlobals.contactExporterTracer == null)
				{
					ExTraceGlobals.contactExporterTracer = new Trace(ExTraceGlobals.componentGuid, 64);
				}
				return ExTraceGlobals.contactExporterTracer;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0004D543 File Offset: 0x0004B743
		public static Trace SiteMailboxPermissionCheckTracer
		{
			get
			{
				if (ExTraceGlobals.siteMailboxPermissionCheckTracer == null)
				{
					ExTraceGlobals.siteMailboxPermissionCheckTracer = new Trace(ExTraceGlobals.componentGuid, 70);
				}
				return ExTraceGlobals.siteMailboxPermissionCheckTracer;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0004D562 File Offset: 0x0004B762
		public static Trace SiteMailboxDocumentSyncTracer
		{
			get
			{
				if (ExTraceGlobals.siteMailboxDocumentSyncTracer == null)
				{
					ExTraceGlobals.siteMailboxDocumentSyncTracer = new Trace(ExTraceGlobals.componentGuid, 71);
				}
				return ExTraceGlobals.siteMailboxDocumentSyncTracer;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0004D581 File Offset: 0x0004B781
		public static Trace SiteMailboxMembershipSyncTracer
		{
			get
			{
				if (ExTraceGlobals.siteMailboxMembershipSyncTracer == null)
				{
					ExTraceGlobals.siteMailboxMembershipSyncTracer = new Trace(ExTraceGlobals.componentGuid, 72);
				}
				return ExTraceGlobals.siteMailboxMembershipSyncTracer;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0004D5A0 File Offset: 0x0004B7A0
		public static Trace SiteMailboxClientOperationTracer
		{
			get
			{
				if (ExTraceGlobals.siteMailboxClientOperationTracer == null)
				{
					ExTraceGlobals.siteMailboxClientOperationTracer = new Trace(ExTraceGlobals.componentGuid, 73);
				}
				return ExTraceGlobals.siteMailboxClientOperationTracer;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0004D5BF File Offset: 0x0004B7BF
		public static Trace SiteMailboxMessageDedupTracer
		{
			get
			{
				if (ExTraceGlobals.siteMailboxMessageDedupTracer == null)
				{
					ExTraceGlobals.siteMailboxMessageDedupTracer = new Trace(ExTraceGlobals.componentGuid, 74);
				}
				return ExTraceGlobals.siteMailboxMessageDedupTracer;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0004D5DE File Offset: 0x0004B7DE
		public static Trace RemindersTracer
		{
			get
			{
				if (ExTraceGlobals.remindersTracer == null)
				{
					ExTraceGlobals.remindersTracer = new Trace(ExTraceGlobals.componentGuid, 81);
				}
				return ExTraceGlobals.remindersTracer;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0004D5FD File Offset: 0x0004B7FD
		public static Trace PeopleIKnowTracer
		{
			get
			{
				if (ExTraceGlobals.peopleIKnowTracer == null)
				{
					ExTraceGlobals.peopleIKnowTracer = new Trace(ExTraceGlobals.componentGuid, 82);
				}
				return ExTraceGlobals.peopleIKnowTracer;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0004D61C File Offset: 0x0004B81C
		public static Trace AggregatedConversationsTracer
		{
			get
			{
				if (ExTraceGlobals.aggregatedConversationsTracer == null)
				{
					ExTraceGlobals.aggregatedConversationsTracer = new Trace(ExTraceGlobals.componentGuid, 83);
				}
				return ExTraceGlobals.aggregatedConversationsTracer;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0004D63B File Offset: 0x0004B83B
		public static Trace DelegateTracer
		{
			get
			{
				if (ExTraceGlobals.delegateTracer == null)
				{
					ExTraceGlobals.delegateTracer = new Trace(ExTraceGlobals.componentGuid, 84);
				}
				return ExTraceGlobals.delegateTracer;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0004D65A File Offset: 0x0004B85A
		public static Trace GroupMailboxSessionTracer
		{
			get
			{
				if (ExTraceGlobals.groupMailboxSessionTracer == null)
				{
					ExTraceGlobals.groupMailboxSessionTracer = new Trace(ExTraceGlobals.componentGuid, 85);
				}
				return ExTraceGlobals.groupMailboxSessionTracer;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0004D679 File Offset: 0x0004B879
		public static Trace SyncProcessTracer
		{
			get
			{
				if (ExTraceGlobals.syncProcessTracer == null)
				{
					ExTraceGlobals.syncProcessTracer = new Trace(ExTraceGlobals.componentGuid, 86);
				}
				return ExTraceGlobals.syncProcessTracer;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0004D698 File Offset: 0x0004B898
		public static Trace ConversationTracer
		{
			get
			{
				if (ExTraceGlobals.conversationTracer == null)
				{
					ExTraceGlobals.conversationTracer = new Trace(ExTraceGlobals.componentGuid, 87);
				}
				return ExTraceGlobals.conversationTracer;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0004D6B7 File Offset: 0x0004B8B7
		public static Trace ReliableTimerTracer
		{
			get
			{
				if (ExTraceGlobals.reliableTimerTracer == null)
				{
					ExTraceGlobals.reliableTimerTracer = new Trace(ExTraceGlobals.componentGuid, 88);
				}
				return ExTraceGlobals.reliableTimerTracer;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0004D6D6 File Offset: 0x0004B8D6
		public static Trace FavoritePublicFoldersTracer
		{
			get
			{
				if (ExTraceGlobals.favoritePublicFoldersTracer == null)
				{
					ExTraceGlobals.favoritePublicFoldersTracer = new Trace(ExTraceGlobals.componentGuid, 89);
				}
				return ExTraceGlobals.favoritePublicFoldersTracer;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0004D6F5 File Offset: 0x0004B8F5
		public static Trace PublicFoldersTracer
		{
			get
			{
				if (ExTraceGlobals.publicFoldersTracer == null)
				{
					ExTraceGlobals.publicFoldersTracer = new Trace(ExTraceGlobals.componentGuid, 90);
				}
				return ExTraceGlobals.publicFoldersTracer;
			}
		}

		// Token: 0x04001620 RID: 5664
		private static Guid componentGuid = new Guid("6d031d1d-5908-457a-a6c4-cdd0f6e74d81");

		// Token: 0x04001621 RID: 5665
		private static Trace storageTracer = null;

		// Token: 0x04001622 RID: 5666
		private static Trace interopTracer = null;

		// Token: 0x04001623 RID: 5667
		private static Trace meetingMessageTracer = null;

		// Token: 0x04001624 RID: 5668
		private static Trace eventTracer = null;

		// Token: 0x04001625 RID: 5669
		private static Trace disposeTracer = null;

		// Token: 0x04001626 RID: 5670
		private static Trace serviceDiscoveryTracer = null;

		// Token: 0x04001627 RID: 5671
		private static Trace contextTracer = null;

		// Token: 0x04001628 RID: 5672
		private static Trace contextShadowTracer = null;

		// Token: 0x04001629 RID: 5673
		private static Trace ccGenericTracer = null;

		// Token: 0x0400162A RID: 5674
		private static Trace ccOleTracer = null;

		// Token: 0x0400162B RID: 5675
		private static Trace ccBodyTracer = null;

		// Token: 0x0400162C RID: 5676
		private static Trace ccInboundGenericTracer = null;

		// Token: 0x0400162D RID: 5677
		private static Trace ccInboundMimeTracer = null;

		// Token: 0x0400162E RID: 5678
		private static Trace ccInboundTnefTracer = null;

		// Token: 0x0400162F RID: 5679
		private static Trace ccOutboundGenericTracer = null;

		// Token: 0x04001630 RID: 5680
		private static Trace ccOutboundMimeTracer = null;

		// Token: 0x04001631 RID: 5681
		private static Trace ccOutboundTnefTracer = null;

		// Token: 0x04001632 RID: 5682
		private static Trace ccPFDTracer = null;

		// Token: 0x04001633 RID: 5683
		private static Trace sessionTracer = null;

		// Token: 0x04001634 RID: 5684
		private static Trace defaultFoldersTracer = null;

		// Token: 0x04001635 RID: 5685
		private static Trace userConfigurationTracer = null;

		// Token: 0x04001636 RID: 5686
		private static Trace propertyBagTracer = null;

		// Token: 0x04001637 RID: 5687
		private static Trace taskTracer = null;

		// Token: 0x04001638 RID: 5688
		private static Trace recurrenceTracer = null;

		// Token: 0x04001639 RID: 5689
		private static Trace workHoursTracer = null;

		// Token: 0x0400163A RID: 5690
		private static Trace syncTracer = null;

		// Token: 0x0400163B RID: 5691
		private static Trace iCalTracer = null;

		// Token: 0x0400163C RID: 5692
		private static Trace activeManagerClientTracer = null;

		// Token: 0x0400163D RID: 5693
		private static Trace ccOutboundVCardTracer = null;

		// Token: 0x0400163E RID: 5694
		private static Trace ccInboundVCardTracer = null;

		// Token: 0x0400163F RID: 5695
		private static Trace sharingTracer = null;

		// Token: 0x04001640 RID: 5696
		private static Trace rightsManagementTracer = null;

		// Token: 0x04001641 RID: 5697
		private static Trace databaseAvailabilityGroupTracer = null;

		// Token: 0x04001642 RID: 5698
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001643 RID: 5699
		private static Trace smtpServiceTracer = null;

		// Token: 0x04001644 RID: 5700
		private static Trace mapiConnectivityTracer = null;

		// Token: 0x04001645 RID: 5701
		private static Trace xtcTracer = null;

		// Token: 0x04001646 RID: 5702
		private static Trace calendarLoggingTracer = null;

		// Token: 0x04001647 RID: 5703
		private static Trace calendarSeriesTracer = null;

		// Token: 0x04001648 RID: 5704
		private static Trace birthdayCalendarTracer = null;

		// Token: 0x04001649 RID: 5705
		private static Trace propertyMappingTracer = null;

		// Token: 0x0400164A RID: 5706
		private static Trace mdbResourceHealthTracer = null;

		// Token: 0x0400164B RID: 5707
		private static Trace contactLinkingTracer = null;

		// Token: 0x0400164C RID: 5708
		private static Trace userPhotosTracer = null;

		// Token: 0x0400164D RID: 5709
		private static Trace contactFoldersEnumeratorTracer = null;

		// Token: 0x0400164E RID: 5710
		private static Trace myContactsFolderTracer = null;

		// Token: 0x0400164F RID: 5711
		private static Trace aggregationTracer = null;

		// Token: 0x04001650 RID: 5712
		private static Trace outlookSocialConnectorInteropTracer = null;

		// Token: 0x04001651 RID: 5713
		private static Trace personTracer = null;

		// Token: 0x04001652 RID: 5714
		private static Trace databasePingerTracer = null;

		// Token: 0x04001653 RID: 5715
		private static Trace contactsEnumeratorTracer = null;

		// Token: 0x04001654 RID: 5716
		private static Trace contactChangeLoggingTracer = null;

		// Token: 0x04001655 RID: 5717
		private static Trace contactExporterTracer = null;

		// Token: 0x04001656 RID: 5718
		private static Trace siteMailboxPermissionCheckTracer = null;

		// Token: 0x04001657 RID: 5719
		private static Trace siteMailboxDocumentSyncTracer = null;

		// Token: 0x04001658 RID: 5720
		private static Trace siteMailboxMembershipSyncTracer = null;

		// Token: 0x04001659 RID: 5721
		private static Trace siteMailboxClientOperationTracer = null;

		// Token: 0x0400165A RID: 5722
		private static Trace siteMailboxMessageDedupTracer = null;

		// Token: 0x0400165B RID: 5723
		private static Trace remindersTracer = null;

		// Token: 0x0400165C RID: 5724
		private static Trace peopleIKnowTracer = null;

		// Token: 0x0400165D RID: 5725
		private static Trace aggregatedConversationsTracer = null;

		// Token: 0x0400165E RID: 5726
		private static Trace delegateTracer = null;

		// Token: 0x0400165F RID: 5727
		private static Trace groupMailboxSessionTracer = null;

		// Token: 0x04001660 RID: 5728
		private static Trace syncProcessTracer = null;

		// Token: 0x04001661 RID: 5729
		private static Trace conversationTracer = null;

		// Token: 0x04001662 RID: 5730
		private static Trace reliableTimerTracer = null;

		// Token: 0x04001663 RID: 5731
		private static Trace favoritePublicFoldersTracer = null;

		// Token: 0x04001664 RID: 5732
		private static Trace publicFoldersTracer = null;
	}
}
