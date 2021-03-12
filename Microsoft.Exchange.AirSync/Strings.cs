using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000006 RID: 6
	public static class Strings
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000374C File Offset: 0x0000194C
		static Strings()
		{
			Strings.stringIDs.Add(3901821180U, "MDMQuarantinedMailBodyStep3");
			Strings.stringIDs.Add(617896851U, "ABQMailBodyDeviceUserAgent");
			Strings.stringIDs.Add(119192957U, "ReportItemFolder");
			Strings.stringIDs.Add(3727360630U, "UserName");
			Strings.stringIDs.Add(3163280220U, "ReportPrefix");
			Strings.stringIDs.Add(740650552U, "ReportForward5");
			Strings.stringIDs.Add(1084313144U, "ReportMailboxInfo");
			Strings.stringIDs.Add(1143935079U, "ReportForward6");
			Strings.stringIDs.Add(282544891U, "ABQMailBodyDeviceInformation");
			Strings.stringIDs.Add(4003050722U, "ABQMailBodyDeviceModel");
			Strings.stringIDs.Add(166564534U, "MailboxLogReportBody");
			Strings.stringIDs.Add(4140072557U, "RemoteWipeConfirmationMessageBody2");
			Strings.stringIDs.Add(3901821178U, "MDMQuarantinedMailBodyStep1");
			Strings.stringIDs.Add(2910600975U, "RemoteWipeConfirmationMessageSubject");
			Strings.stringIDs.Add(1023822817U, "BootstrapMailForWM61Subject");
			Strings.stringIDs.Add(2910997761U, "DeviceStatisticsTaskMailboxLogAttachmentNote");
			Strings.stringIDs.Add(4060027620U, "BootstrapMailForWM61Body3");
			Strings.stringIDs.Add(2048398875U, "ReportDeviceId");
			Strings.stringIDs.Add(4060027610U, "BootstrapMailForWM61Body9");
			Strings.stringIDs.Add(137157223U, "UnfamiliarLocationAccountTeam");
			Strings.stringIDs.Add(872007324U, "ReportAssemblyInfo");
			Strings.stringIDs.Add(4060027616U, "BootstrapMailForWM61Body7");
			Strings.stringIDs.Add(4060027615U, "BootstrapMailForWM61Body6");
			Strings.stringIDs.Add(1008143499U, "UnfamiliarLocationTitle");
			Strings.stringIDs.Add(3323369056U, "DeviceType");
			Strings.stringIDs.Add(4060027609U, "BootstrapMailForWM61Body8");
			Strings.stringIDs.Add(940098429U, "MDMNonCompliantMailSubject");
			Strings.stringIDs.Add(2136772851U, "ReportItemType");
			Strings.stringIDs.Add(4049839131U, "ABQMailBodyDeviceOS");
			Strings.stringIDs.Add(4227611602U, "BootstrapMailForWM61Body10");
			Strings.stringIDs.Add(3066249380U, "ReportForward1");
			Strings.stringIDs.Add(3469533907U, "ReportForward2");
			Strings.stringIDs.Add(802699486U, "ABQMailBodyEASVersion");
			Strings.stringIDs.Add(3872818434U, "ReportForward7");
			Strings.stringIDs.Add(1411189202U, "RemoteWipeConfirmationMessageBody3");
			Strings.stringIDs.Add(1844802661U, "DeviceId");
			Strings.stringIDs.Add(38328364U, "ABQMailBodyDeviceAccessState");
			Strings.stringIDs.Add(2769639219U, "UnfamiliarLocationParagraph2");
			Strings.stringIDs.Add(981673616U, "DeviceStatisticsTaskMailboxLogSubject");
			Strings.stringIDs.Add(3272201393U, "UnfamiliarLocationButton");
			Strings.stringIDs.Add(2910205670U, "MDMQuarantinedMailBasicRetryText");
			Strings.stringIDs.Add(2092201661U, "AccessBlockedMailSubject");
			Strings.stringIDs.Add(1349060152U, "AutoBlockedMailSubject");
			Strings.stringIDs.Add(172566471U, "AutoBlockedMailBody1");
			Strings.stringIDs.Add(194232551U, "ReportSyncInfo");
			Strings.stringIDs.Add(566036320U, "ABQMailBodyDeviceAccessStateReason");
			Strings.stringIDs.Add(2491124383U, "IRMCorruptProtectedMessageBodyText");
			Strings.stringIDs.Add(2465427168U, "DeviceDiscoveryMailBody1");
			Strings.stringIDs.Add(3901821179U, "MDMQuarantinedMailBodyStep2");
			Strings.stringIDs.Add(2887039700U, "ABQMailBodyDeviceID");
			Strings.stringIDs.Add(4239215784U, "ReportAndLogSender");
			Strings.stringIDs.Add(579459593U, "ReportStackTrace");
			Strings.stringIDs.Add(3415785961U, "ReportDebugInfo");
			Strings.stringIDs.Add(1903449966U, "ReportForward3");
			Strings.stringIDs.Add(620933251U, "UnfamiliarLocationSubject");
			Strings.stringIDs.Add(2904427437U, "MDMNonCompliantMailBody");
			Strings.stringIDs.Add(2306734493U, "ReportForward4");
			Strings.stringIDs.Add(2951832077U, "MailboxLogReportHeader");
			Strings.stringIDs.Add(2687744413U, "MDMQuarantinedMailBodyEnrollLink");
			Strings.stringIDs.Add(3303701745U, "SMIMENotSupportedBodyText");
			Strings.stringIDs.Add(2987669051U, "ABQMailBodyDeviceIMEI");
			Strings.stringIDs.Add(886329592U, "ReportSubject");
			Strings.stringIDs.Add(55458219U, "MDMQuarantinedMailBody");
			Strings.stringIDs.Add(1248113741U, "ReportCASInfo");
			Strings.stringIDs.Add(118380958U, "MDMNonCompliantMailBodyLinkText");
			Strings.stringIDs.Add(310873002U, "MailboxLogReportSubject");
			Strings.stringIDs.Add(70812490U, "IRMNoViewRightsBodyText");
			Strings.stringIDs.Add(1986464879U, "ReportItemSubject");
			Strings.stringIDs.Add(4060027613U, "BootstrapMailForWM61Body4");
			Strings.stringIDs.Add(1862974783U, "MDMQuarantinedMailSubject");
			Strings.stringIDs.Add(3713119782U, "ReportUnknown");
			Strings.stringIDs.Add(3889310586U, "AccessBlockedMailBody1");
			Strings.stringIDs.Add(2315459723U, "RemoteWipeConfirmationMessageBody1Owa");
			Strings.stringIDs.Add(715035320U, "IRMServerNotAvailableBodyText");
			Strings.stringIDs.Add(2510964059U, "UnfamiliarLocationSubTitle");
			Strings.stringIDs.Add(2727787888U, "RemoteWipeConfirmationMessageHeader");
			Strings.stringIDs.Add(2328219770U, "Date");
			Strings.stringIDs.Add(2710599231U, "DeviceDiscoveryMailSubject");
			Strings.stringIDs.Add(3200123789U, "MaxDevicesExceededMailSubject");
			Strings.stringIDs.Add(2870296499U, "ReportItemCreated");
			Strings.stringIDs.Add(251622507U, "ABQMailBodyDeviceType");
			Strings.stringIDs.Add(4019774947U, "RemoteWipeConfirmationMessageBody1Task");
			Strings.stringIDs.Add(3997627200U, "UnfamiliarLocationClosing");
			Strings.stringIDs.Add(2817448987U, "IRMLicenseExpiredBodyText");
			Strings.stringIDs.Add(3675706513U, "SmsSearchFolder");
			Strings.stringIDs.Add(3017151488U, "IRMPreLicensingFailureBodyText");
			Strings.stringIDs.Add(4060027618U, "BootstrapMailForWM61Body1");
			Strings.stringIDs.Add(4060027619U, "BootstrapMailForWM61Body2");
			Strings.stringIDs.Add(3889721364U, "MDMQuarantinedMailBodyActivateLink");
			Strings.stringIDs.Add(2439737583U, "QuarantinedMailSubject");
			Strings.stringIDs.Add(3714305626U, "UnfamiliarLocationFromName");
			Strings.stringIDs.Add(4060027614U, "BootstrapMailForWM61Body5");
			Strings.stringIDs.Add(586931743U, "MDMQuarantinedMailBodyRetryLink");
			Strings.stringIDs.Add(3715154655U, "IRMServerNotConfiguredBodyText");
			Strings.stringIDs.Add(765518032U, "QuarantinedMailBody1");
			Strings.stringIDs.Add(2153172051U, "IRMReachNotConfiguredBodyText");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00003F08 File Offset: 0x00002108
		public static LocalizedString MDMQuarantinedMailBodyStep3
		{
			get
			{
				return new LocalizedString("MDMQuarantinedMailBodyStep3", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00003F26 File Offset: 0x00002126
		public static LocalizedString ABQMailBodyDeviceUserAgent
		{
			get
			{
				return new LocalizedString("ABQMailBodyDeviceUserAgent", "Ex23A244", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00003F44 File Offset: 0x00002144
		public static LocalizedString ReportItemFolder
		{
			get
			{
				return new LocalizedString("ReportItemFolder", "Ex549398", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00003F62 File Offset: 0x00002162
		public static LocalizedString UserName
		{
			get
			{
				return new LocalizedString("UserName", "Ex819B3C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00003F80 File Offset: 0x00002180
		public static LocalizedString ReportPrefix
		{
			get
			{
				return new LocalizedString("ReportPrefix", "Ex65ACB8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00003F9E File Offset: 0x0000219E
		public static LocalizedString ReportForward5
		{
			get
			{
				return new LocalizedString("ReportForward5", "ExF77AF9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00003FBC File Offset: 0x000021BC
		public static LocalizedString ReportMailboxInfo
		{
			get
			{
				return new LocalizedString("ReportMailboxInfo", "ExD21FCF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00003FDA File Offset: 0x000021DA
		public static LocalizedString ReportForward6
		{
			get
			{
				return new LocalizedString("ReportForward6", "Ex8F26E5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00003FF8 File Offset: 0x000021F8
		public static LocalizedString ABQMailBodyDeviceInformation
		{
			get
			{
				return new LocalizedString("ABQMailBodyDeviceInformation", "Ex9B4560", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00004016 File Offset: 0x00002216
		public static LocalizedString ABQMailBodyDeviceModel
		{
			get
			{
				return new LocalizedString("ABQMailBodyDeviceModel", "Ex0DA9E3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00004034 File Offset: 0x00002234
		public static LocalizedString MailboxLogReportBody
		{
			get
			{
				return new LocalizedString("MailboxLogReportBody", "Ex5D698F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00004052 File Offset: 0x00002252
		public static LocalizedString RemoteWipeConfirmationMessageBody2
		{
			get
			{
				return new LocalizedString("RemoteWipeConfirmationMessageBody2", "Ex9C72C2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00004070 File Offset: 0x00002270
		public static LocalizedString MDMQuarantinedMailBodyStep1
		{
			get
			{
				return new LocalizedString("MDMQuarantinedMailBodyStep1", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000408E File Offset: 0x0000228E
		public static LocalizedString RemoteWipeConfirmationMessageSubject
		{
			get
			{
				return new LocalizedString("RemoteWipeConfirmationMessageSubject", "Ex4C0A92", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000040AC File Offset: 0x000022AC
		public static LocalizedString BootstrapMailForWM61Subject
		{
			get
			{
				return new LocalizedString("BootstrapMailForWM61Subject", "Ex0DF123", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000040CA File Offset: 0x000022CA
		public static LocalizedString DeviceStatisticsTaskMailboxLogAttachmentNote
		{
			get
			{
				return new LocalizedString("DeviceStatisticsTaskMailboxLogAttachmentNote", "Ex350069", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000040E8 File Offset: 0x000022E8
		public static LocalizedString BootstrapMailForWM61Body3
		{
			get
			{
				return new LocalizedString("BootstrapMailForWM61Body3", "ExC3537C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00004106 File Offset: 0x00002306
		public static LocalizedString ReportDeviceId
		{
			get
			{
				return new LocalizedString("ReportDeviceId", "ExEE4F83", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00004124 File Offset: 0x00002324
		public static LocalizedString BootstrapMailForWM61Body9
		{
			get
			{
				return new LocalizedString("BootstrapMailForWM61Body9", "Ex30C07A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00004142 File Offset: 0x00002342
		public static LocalizedString UnfamiliarLocationAccountTeam
		{
			get
			{
				return new LocalizedString("UnfamiliarLocationAccountTeam", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00004160 File Offset: 0x00002360
		public static LocalizedString ReportAssemblyInfo
		{
			get
			{
				return new LocalizedString("ReportAssemblyInfo", "Ex1D1D3C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000417E File Offset: 0x0000237E
		public static LocalizedString BootstrapMailForWM61Body7
		{
			get
			{
				return new LocalizedString("BootstrapMailForWM61Body7", "Ex42AB62", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000419C File Offset: 0x0000239C
		public static LocalizedString BootstrapMailForWM61Body6
		{
			get
			{
				return new LocalizedString("BootstrapMailForWM61Body6", "ExFDE36F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000041BA File Offset: 0x000023BA
		public static LocalizedString UnfamiliarLocationTitle
		{
			get
			{
				return new LocalizedString("UnfamiliarLocationTitle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000041D8 File Offset: 0x000023D8
		public static LocalizedString DeviceType
		{
			get
			{
				return new LocalizedString("DeviceType", "Ex6E6370", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000041F6 File Offset: 0x000023F6
		public static LocalizedString BootstrapMailForWM61Body8
		{
			get
			{
				return new LocalizedString("BootstrapMailForWM61Body8", "Ex3D14BD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00004214 File Offset: 0x00002414
		public static LocalizedString MDMNonCompliantMailSubject
		{
			get
			{
				return new LocalizedString("MDMNonCompliantMailSubject", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00004232 File Offset: 0x00002432
		public static LocalizedString ReportItemType
		{
			get
			{
				return new LocalizedString("ReportItemType", "ExA8C906", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00004250 File Offset: 0x00002450
		public static LocalizedString ABQMailBodyDeviceOS
		{
			get
			{
				return new LocalizedString("ABQMailBodyDeviceOS", "Ex9BB95A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000426E File Offset: 0x0000246E
		public static LocalizedString BootstrapMailForWM61Body10
		{
			get
			{
				return new LocalizedString("BootstrapMailForWM61Body10", "Ex003B35", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000428C File Offset: 0x0000248C
		public static LocalizedString ReportForward1
		{
			get
			{
				return new LocalizedString("ReportForward1", "Ex4178A1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000042AA File Offset: 0x000024AA
		public static LocalizedString ReportForward2
		{
			get
			{
				return new LocalizedString("ReportForward2", "Ex25FC20", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000042C8 File Offset: 0x000024C8
		public static LocalizedString ABQMailBodyEASVersion
		{
			get
			{
				return new LocalizedString("ABQMailBodyEASVersion", "Ex49B0E4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000042E6 File Offset: 0x000024E6
		public static LocalizedString ReportForward7
		{
			get
			{
				return new LocalizedString("ReportForward7", "ExBE9861", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00004304 File Offset: 0x00002504
		public static LocalizedString RemoteWipeConfirmationMessageBody3
		{
			get
			{
				return new LocalizedString("RemoteWipeConfirmationMessageBody3", "Ex4339C1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00004322 File Offset: 0x00002522
		public static LocalizedString DeviceId
		{
			get
			{
				return new LocalizedString("DeviceId", "ExDB714E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00004340 File Offset: 0x00002540
		public static LocalizedString ABQMailBodyDeviceAccessState
		{
			get
			{
				return new LocalizedString("ABQMailBodyDeviceAccessState", "Ex29B825", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000435E File Offset: 0x0000255E
		public static LocalizedString UnfamiliarLocationParagraph2
		{
			get
			{
				return new LocalizedString("UnfamiliarLocationParagraph2", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000437C File Offset: 0x0000257C
		public static LocalizedString CanceledDelegatedSubjectPrefix(string displayName)
		{
			return new LocalizedString("CanceledDelegatedSubjectPrefix", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName
			});
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000043AC File Offset: 0x000025AC
		public static LocalizedString UnfamiliarLocationParagraph1(string address)
		{
			return new LocalizedString("UnfamiliarLocationParagraph1", "", false, false, Strings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000043DB File Offset: 0x000025DB
		public static LocalizedString DeviceStatisticsTaskMailboxLogSubject
		{
			get
			{
				return new LocalizedString("DeviceStatisticsTaskMailboxLogSubject", "ExD59891", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000043F9 File Offset: 0x000025F9
		public static LocalizedString UnfamiliarLocationButton
		{
			get
			{
				return new LocalizedString("UnfamiliarLocationButton", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00004417 File Offset: 0x00002617
		public static LocalizedString MDMQuarantinedMailBasicRetryText
		{
			get
			{
				return new LocalizedString("MDMQuarantinedMailBasicRetryText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00004435 File Offset: 0x00002635
		public static LocalizedString AccessBlockedMailSubject
		{
			get
			{
				return new LocalizedString("AccessBlockedMailSubject", "Ex2F80BF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00004453 File Offset: 0x00002653
		public static LocalizedString AutoBlockedMailSubject
		{
			get
			{
				return new LocalizedString("AutoBlockedMailSubject", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00004471 File Offset: 0x00002671
		public static LocalizedString AutoBlockedMailBody1
		{
			get
			{
				return new LocalizedString("AutoBlockedMailBody1", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000448F File Offset: 0x0000268F
		public static LocalizedString ReportSyncInfo
		{
			get
			{
				return new LocalizedString("ReportSyncInfo", "ExA4FA6B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000044AD File Offset: 0x000026AD
		public static LocalizedString ABQMailBodyDeviceAccessStateReason
		{
			get
			{
				return new LocalizedString("ABQMailBodyDeviceAccessStateReason", "Ex19E5D5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000044CB File Offset: 0x000026CB
		public static LocalizedString IRMCorruptProtectedMessageBodyText
		{
			get
			{
				return new LocalizedString("IRMCorruptProtectedMessageBodyText", "ExC2EE82", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000044E9 File Offset: 0x000026E9
		public static LocalizedString DeviceDiscoveryMailBody1
		{
			get
			{
				return new LocalizedString("DeviceDiscoveryMailBody1", "Ex936CB2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00004507 File Offset: 0x00002707
		public static LocalizedString MDMQuarantinedMailBodyStep2
		{
			get
			{
				return new LocalizedString("MDMQuarantinedMailBodyStep2", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00004525 File Offset: 0x00002725
		public static LocalizedString ABQMailBodyDeviceID
		{
			get
			{
				return new LocalizedString("ABQMailBodyDeviceID", "Ex0DDECE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00004543 File Offset: 0x00002743
		public static LocalizedString ReportAndLogSender
		{
			get
			{
				return new LocalizedString("ReportAndLogSender", "Ex4A6291", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00004561 File Offset: 0x00002761
		public static LocalizedString ReportStackTrace
		{
			get
			{
				return new LocalizedString("ReportStackTrace", "Ex31A5A2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000457F File Offset: 0x0000277F
		public static LocalizedString ReportDebugInfo
		{
			get
			{
				return new LocalizedString("ReportDebugInfo", "Ex07E341", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000459D File Offset: 0x0000279D
		public static LocalizedString ReportForward3
		{
			get
			{
				return new LocalizedString("ReportForward3", "Ex627990", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000045BB File Offset: 0x000027BB
		public static LocalizedString UnfamiliarLocationSubject
		{
			get
			{
				return new LocalizedString("UnfamiliarLocationSubject", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000045D9 File Offset: 0x000027D9
		public static LocalizedString MDMNonCompliantMailBody
		{
			get
			{
				return new LocalizedString("MDMNonCompliantMailBody", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000045F7 File Offset: 0x000027F7
		public static LocalizedString ReportForward4
		{
			get
			{
				return new LocalizedString("ReportForward4", "ExCA7500", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00004615 File Offset: 0x00002815
		public static LocalizedString MailboxLogReportHeader
		{
			get
			{
				return new LocalizedString("MailboxLogReportHeader", "Ex7D0CC0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00004633 File Offset: 0x00002833
		public static LocalizedString MDMQuarantinedMailBodyEnrollLink
		{
			get
			{
				return new LocalizedString("MDMQuarantinedMailBodyEnrollLink", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00004651 File Offset: 0x00002851
		public static LocalizedString SMIMENotSupportedBodyText
		{
			get
			{
				return new LocalizedString("SMIMENotSupportedBodyText", "ExDBF215", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004670 File Offset: 0x00002870
		public static LocalizedString MaxDevicesExceededMailBody(int deviceCount, uint maxDevicesLimit)
		{
			return new LocalizedString("MaxDevicesExceededMailBody", "Ex1A6E84", false, true, Strings.ResourceManager, new object[]
			{
				deviceCount,
				maxDevicesLimit
			});
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000046AD File Offset: 0x000028AD
		public static LocalizedString ABQMailBodyDeviceIMEI
		{
			get
			{
				return new LocalizedString("ABQMailBodyDeviceIMEI", "Ex8CD364", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000046CB File Offset: 0x000028CB
		public static LocalizedString ReportSubject
		{
			get
			{
				return new LocalizedString("ReportSubject", "ExA8B473", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000046E9 File Offset: 0x000028E9
		public static LocalizedString MDMQuarantinedMailBody
		{
			get
			{
				return new LocalizedString("MDMQuarantinedMailBody", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00004707 File Offset: 0x00002907
		public static LocalizedString ReportCASInfo
		{
			get
			{
				return new LocalizedString("ReportCASInfo", "Ex72C054", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00004725 File Offset: 0x00002925
		public static LocalizedString MDMNonCompliantMailBodyLinkText
		{
			get
			{
				return new LocalizedString("MDMNonCompliantMailBodyLinkText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00004743 File Offset: 0x00002943
		public static LocalizedString MailboxLogReportSubject
		{
			get
			{
				return new LocalizedString("MailboxLogReportSubject", "ExBF791D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00004761 File Offset: 0x00002961
		public static LocalizedString IRMNoViewRightsBodyText
		{
			get
			{
				return new LocalizedString("IRMNoViewRightsBodyText", "Ex1F0166", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000477F File Offset: 0x0000297F
		public static LocalizedString ReportItemSubject
		{
			get
			{
				return new LocalizedString("ReportItemSubject", "Ex813431", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000479D File Offset: 0x0000299D
		public static LocalizedString BootstrapMailForWM61Body4
		{
			get
			{
				return new LocalizedString("BootstrapMailForWM61Body4", "ExDAB65E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000047BB File Offset: 0x000029BB
		public static LocalizedString MDMQuarantinedMailSubject
		{
			get
			{
				return new LocalizedString("MDMQuarantinedMailSubject", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000047D9 File Offset: 0x000029D9
		public static LocalizedString ReportUnknown
		{
			get
			{
				return new LocalizedString("ReportUnknown", "Ex5C525C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000047F7 File Offset: 0x000029F7
		public static LocalizedString AccessBlockedMailBody1
		{
			get
			{
				return new LocalizedString("AccessBlockedMailBody1", "ExCB10C3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00004815 File Offset: 0x00002A15
		public static LocalizedString RemoteWipeConfirmationMessageBody1Owa
		{
			get
			{
				return new LocalizedString("RemoteWipeConfirmationMessageBody1Owa", "ExD6AD11", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00004833 File Offset: 0x00002A33
		public static LocalizedString IRMServerNotAvailableBodyText
		{
			get
			{
				return new LocalizedString("IRMServerNotAvailableBodyText", "ExC2B231", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00004851 File Offset: 0x00002A51
		public static LocalizedString UnfamiliarLocationSubTitle
		{
			get
			{
				return new LocalizedString("UnfamiliarLocationSubTitle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000486F File Offset: 0x00002A6F
		public static LocalizedString RemoteWipeConfirmationMessageHeader
		{
			get
			{
				return new LocalizedString("RemoteWipeConfirmationMessageHeader", "Ex65A4C0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000488D File Offset: 0x00002A8D
		public static LocalizedString Date
		{
			get
			{
				return new LocalizedString("Date", "ExCB2187", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000048AB File Offset: 0x00002AAB
		public static LocalizedString DeviceDiscoveryMailSubject
		{
			get
			{
				return new LocalizedString("DeviceDiscoveryMailSubject", "ExC9E961", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000048C9 File Offset: 0x00002AC9
		public static LocalizedString MaxDevicesExceededMailSubject
		{
			get
			{
				return new LocalizedString("MaxDevicesExceededMailSubject", "Ex4E91F8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000048E7 File Offset: 0x00002AE7
		public static LocalizedString ReportItemCreated
		{
			get
			{
				return new LocalizedString("ReportItemCreated", "Ex80CA2B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004905 File Offset: 0x00002B05
		public static LocalizedString ABQMailBodyDeviceType
		{
			get
			{
				return new LocalizedString("ABQMailBodyDeviceType", "ExA64CA8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004923 File Offset: 0x00002B23
		public static LocalizedString RemoteWipeConfirmationMessageBody1Task
		{
			get
			{
				return new LocalizedString("RemoteWipeConfirmationMessageBody1Task", "Ex2DCEEF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004941 File Offset: 0x00002B41
		public static LocalizedString UnfamiliarLocationClosing
		{
			get
			{
				return new LocalizedString("UnfamiliarLocationClosing", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000495F File Offset: 0x00002B5F
		public static LocalizedString IRMLicenseExpiredBodyText
		{
			get
			{
				return new LocalizedString("IRMLicenseExpiredBodyText", "Ex0D4AAF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000497D File Offset: 0x00002B7D
		public static LocalizedString SmsSearchFolder
		{
			get
			{
				return new LocalizedString("SmsSearchFolder", "ExFE7AB9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000499B File Offset: 0x00002B9B
		public static LocalizedString IRMPreLicensingFailureBodyText
		{
			get
			{
				return new LocalizedString("IRMPreLicensingFailureBodyText", "ExF14228", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000049B9 File Offset: 0x00002BB9
		public static LocalizedString BootstrapMailForWM61Body1
		{
			get
			{
				return new LocalizedString("BootstrapMailForWM61Body1", "Ex892220", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000049D7 File Offset: 0x00002BD7
		public static LocalizedString BootstrapMailForWM61Body2
		{
			get
			{
				return new LocalizedString("BootstrapMailForWM61Body2", "ExE766BC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000049F8 File Offset: 0x00002BF8
		public static LocalizedString ABQMailBodySentAt(string dateTime, string recipientsSMTP)
		{
			return new LocalizedString("ABQMailBodySentAt", "Ex9B1624", false, true, Strings.ResourceManager, new object[]
			{
				dateTime,
				recipientsSMTP
			});
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00004A2B File Offset: 0x00002C2B
		public static LocalizedString MDMQuarantinedMailBodyActivateLink
		{
			get
			{
				return new LocalizedString("MDMQuarantinedMailBodyActivateLink", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00004A49 File Offset: 0x00002C49
		public static LocalizedString QuarantinedMailSubject
		{
			get
			{
				return new LocalizedString("QuarantinedMailSubject", "Ex11B316", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004A67 File Offset: 0x00002C67
		public static LocalizedString UnfamiliarLocationFromName
		{
			get
			{
				return new LocalizedString("UnfamiliarLocationFromName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00004A85 File Offset: 0x00002C85
		public static LocalizedString BootstrapMailForWM61Body5
		{
			get
			{
				return new LocalizedString("BootstrapMailForWM61Body5", "Ex4EC313", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004AA3 File Offset: 0x00002CA3
		public static LocalizedString MDMQuarantinedMailBodyRetryLink
		{
			get
			{
				return new LocalizedString("MDMQuarantinedMailBodyRetryLink", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004AC1 File Offset: 0x00002CC1
		public static LocalizedString IRMServerNotConfiguredBodyText
		{
			get
			{
				return new LocalizedString("IRMServerNotConfiguredBodyText", "Ex04E16A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public static LocalizedString DelegatedSubjectPrefix(string displayName)
		{
			return new LocalizedString("DelegatedSubjectPrefix", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName
			});
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004B0F File Offset: 0x00002D0F
		public static LocalizedString QuarantinedMailBody1
		{
			get
			{
				return new LocalizedString("QuarantinedMailBody1", "ExBED365", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004B2D File Offset: 0x00002D2D
		public static LocalizedString IRMReachNotConfiguredBodyText
		{
			get
			{
				return new LocalizedString("IRMReachNotConfiguredBodyText", "Ex5527F8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004B4B File Offset: 0x00002D4B
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040000EC RID: 236
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(96);

		// Token: 0x040000ED RID: 237
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.AirSync.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000007 RID: 7
		public enum IDs : uint
		{
			// Token: 0x040000EF RID: 239
			MDMQuarantinedMailBodyStep3 = 3901821180U,
			// Token: 0x040000F0 RID: 240
			ABQMailBodyDeviceUserAgent = 617896851U,
			// Token: 0x040000F1 RID: 241
			ReportItemFolder = 119192957U,
			// Token: 0x040000F2 RID: 242
			UserName = 3727360630U,
			// Token: 0x040000F3 RID: 243
			ReportPrefix = 3163280220U,
			// Token: 0x040000F4 RID: 244
			ReportForward5 = 740650552U,
			// Token: 0x040000F5 RID: 245
			ReportMailboxInfo = 1084313144U,
			// Token: 0x040000F6 RID: 246
			ReportForward6 = 1143935079U,
			// Token: 0x040000F7 RID: 247
			ABQMailBodyDeviceInformation = 282544891U,
			// Token: 0x040000F8 RID: 248
			ABQMailBodyDeviceModel = 4003050722U,
			// Token: 0x040000F9 RID: 249
			MailboxLogReportBody = 166564534U,
			// Token: 0x040000FA RID: 250
			RemoteWipeConfirmationMessageBody2 = 4140072557U,
			// Token: 0x040000FB RID: 251
			MDMQuarantinedMailBodyStep1 = 3901821178U,
			// Token: 0x040000FC RID: 252
			RemoteWipeConfirmationMessageSubject = 2910600975U,
			// Token: 0x040000FD RID: 253
			BootstrapMailForWM61Subject = 1023822817U,
			// Token: 0x040000FE RID: 254
			DeviceStatisticsTaskMailboxLogAttachmentNote = 2910997761U,
			// Token: 0x040000FF RID: 255
			BootstrapMailForWM61Body3 = 4060027620U,
			// Token: 0x04000100 RID: 256
			ReportDeviceId = 2048398875U,
			// Token: 0x04000101 RID: 257
			BootstrapMailForWM61Body9 = 4060027610U,
			// Token: 0x04000102 RID: 258
			UnfamiliarLocationAccountTeam = 137157223U,
			// Token: 0x04000103 RID: 259
			ReportAssemblyInfo = 872007324U,
			// Token: 0x04000104 RID: 260
			BootstrapMailForWM61Body7 = 4060027616U,
			// Token: 0x04000105 RID: 261
			BootstrapMailForWM61Body6 = 4060027615U,
			// Token: 0x04000106 RID: 262
			UnfamiliarLocationTitle = 1008143499U,
			// Token: 0x04000107 RID: 263
			DeviceType = 3323369056U,
			// Token: 0x04000108 RID: 264
			BootstrapMailForWM61Body8 = 4060027609U,
			// Token: 0x04000109 RID: 265
			MDMNonCompliantMailSubject = 940098429U,
			// Token: 0x0400010A RID: 266
			ReportItemType = 2136772851U,
			// Token: 0x0400010B RID: 267
			ABQMailBodyDeviceOS = 4049839131U,
			// Token: 0x0400010C RID: 268
			BootstrapMailForWM61Body10 = 4227611602U,
			// Token: 0x0400010D RID: 269
			ReportForward1 = 3066249380U,
			// Token: 0x0400010E RID: 270
			ReportForward2 = 3469533907U,
			// Token: 0x0400010F RID: 271
			ABQMailBodyEASVersion = 802699486U,
			// Token: 0x04000110 RID: 272
			ReportForward7 = 3872818434U,
			// Token: 0x04000111 RID: 273
			RemoteWipeConfirmationMessageBody3 = 1411189202U,
			// Token: 0x04000112 RID: 274
			DeviceId = 1844802661U,
			// Token: 0x04000113 RID: 275
			ABQMailBodyDeviceAccessState = 38328364U,
			// Token: 0x04000114 RID: 276
			UnfamiliarLocationParagraph2 = 2769639219U,
			// Token: 0x04000115 RID: 277
			DeviceStatisticsTaskMailboxLogSubject = 981673616U,
			// Token: 0x04000116 RID: 278
			UnfamiliarLocationButton = 3272201393U,
			// Token: 0x04000117 RID: 279
			MDMQuarantinedMailBasicRetryText = 2910205670U,
			// Token: 0x04000118 RID: 280
			AccessBlockedMailSubject = 2092201661U,
			// Token: 0x04000119 RID: 281
			AutoBlockedMailSubject = 1349060152U,
			// Token: 0x0400011A RID: 282
			AutoBlockedMailBody1 = 172566471U,
			// Token: 0x0400011B RID: 283
			ReportSyncInfo = 194232551U,
			// Token: 0x0400011C RID: 284
			ABQMailBodyDeviceAccessStateReason = 566036320U,
			// Token: 0x0400011D RID: 285
			IRMCorruptProtectedMessageBodyText = 2491124383U,
			// Token: 0x0400011E RID: 286
			DeviceDiscoveryMailBody1 = 2465427168U,
			// Token: 0x0400011F RID: 287
			MDMQuarantinedMailBodyStep2 = 3901821179U,
			// Token: 0x04000120 RID: 288
			ABQMailBodyDeviceID = 2887039700U,
			// Token: 0x04000121 RID: 289
			ReportAndLogSender = 4239215784U,
			// Token: 0x04000122 RID: 290
			ReportStackTrace = 579459593U,
			// Token: 0x04000123 RID: 291
			ReportDebugInfo = 3415785961U,
			// Token: 0x04000124 RID: 292
			ReportForward3 = 1903449966U,
			// Token: 0x04000125 RID: 293
			UnfamiliarLocationSubject = 620933251U,
			// Token: 0x04000126 RID: 294
			MDMNonCompliantMailBody = 2904427437U,
			// Token: 0x04000127 RID: 295
			ReportForward4 = 2306734493U,
			// Token: 0x04000128 RID: 296
			MailboxLogReportHeader = 2951832077U,
			// Token: 0x04000129 RID: 297
			MDMQuarantinedMailBodyEnrollLink = 2687744413U,
			// Token: 0x0400012A RID: 298
			SMIMENotSupportedBodyText = 3303701745U,
			// Token: 0x0400012B RID: 299
			ABQMailBodyDeviceIMEI = 2987669051U,
			// Token: 0x0400012C RID: 300
			ReportSubject = 886329592U,
			// Token: 0x0400012D RID: 301
			MDMQuarantinedMailBody = 55458219U,
			// Token: 0x0400012E RID: 302
			ReportCASInfo = 1248113741U,
			// Token: 0x0400012F RID: 303
			MDMNonCompliantMailBodyLinkText = 118380958U,
			// Token: 0x04000130 RID: 304
			MailboxLogReportSubject = 310873002U,
			// Token: 0x04000131 RID: 305
			IRMNoViewRightsBodyText = 70812490U,
			// Token: 0x04000132 RID: 306
			ReportItemSubject = 1986464879U,
			// Token: 0x04000133 RID: 307
			BootstrapMailForWM61Body4 = 4060027613U,
			// Token: 0x04000134 RID: 308
			MDMQuarantinedMailSubject = 1862974783U,
			// Token: 0x04000135 RID: 309
			ReportUnknown = 3713119782U,
			// Token: 0x04000136 RID: 310
			AccessBlockedMailBody1 = 3889310586U,
			// Token: 0x04000137 RID: 311
			RemoteWipeConfirmationMessageBody1Owa = 2315459723U,
			// Token: 0x04000138 RID: 312
			IRMServerNotAvailableBodyText = 715035320U,
			// Token: 0x04000139 RID: 313
			UnfamiliarLocationSubTitle = 2510964059U,
			// Token: 0x0400013A RID: 314
			RemoteWipeConfirmationMessageHeader = 2727787888U,
			// Token: 0x0400013B RID: 315
			Date = 2328219770U,
			// Token: 0x0400013C RID: 316
			DeviceDiscoveryMailSubject = 2710599231U,
			// Token: 0x0400013D RID: 317
			MaxDevicesExceededMailSubject = 3200123789U,
			// Token: 0x0400013E RID: 318
			ReportItemCreated = 2870296499U,
			// Token: 0x0400013F RID: 319
			ABQMailBodyDeviceType = 251622507U,
			// Token: 0x04000140 RID: 320
			RemoteWipeConfirmationMessageBody1Task = 4019774947U,
			// Token: 0x04000141 RID: 321
			UnfamiliarLocationClosing = 3997627200U,
			// Token: 0x04000142 RID: 322
			IRMLicenseExpiredBodyText = 2817448987U,
			// Token: 0x04000143 RID: 323
			SmsSearchFolder = 3675706513U,
			// Token: 0x04000144 RID: 324
			IRMPreLicensingFailureBodyText = 3017151488U,
			// Token: 0x04000145 RID: 325
			BootstrapMailForWM61Body1 = 4060027618U,
			// Token: 0x04000146 RID: 326
			BootstrapMailForWM61Body2,
			// Token: 0x04000147 RID: 327
			MDMQuarantinedMailBodyActivateLink = 3889721364U,
			// Token: 0x04000148 RID: 328
			QuarantinedMailSubject = 2439737583U,
			// Token: 0x04000149 RID: 329
			UnfamiliarLocationFromName = 3714305626U,
			// Token: 0x0400014A RID: 330
			BootstrapMailForWM61Body5 = 4060027614U,
			// Token: 0x0400014B RID: 331
			MDMQuarantinedMailBodyRetryLink = 586931743U,
			// Token: 0x0400014C RID: 332
			IRMServerNotConfiguredBodyText = 3715154655U,
			// Token: 0x0400014D RID: 333
			QuarantinedMailBody1 = 765518032U,
			// Token: 0x0400014E RID: 334
			IRMReachNotConfiguredBodyText = 2153172051U
		}

		// Token: 0x02000008 RID: 8
		private enum ParamIDs
		{
			// Token: 0x04000150 RID: 336
			CanceledDelegatedSubjectPrefix,
			// Token: 0x04000151 RID: 337
			UnfamiliarLocationParagraph1,
			// Token: 0x04000152 RID: 338
			MaxDevicesExceededMailBody,
			// Token: 0x04000153 RID: 339
			ABQMailBodySentAt,
			// Token: 0x04000154 RID: 340
			DelegatedSubjectPrefix
		}
	}
}
