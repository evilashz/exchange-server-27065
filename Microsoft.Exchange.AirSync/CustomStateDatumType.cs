using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000059 RID: 89
	internal struct CustomStateDatumType
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001D84C File Offset: 0x0001BA4C
		internal static string SupportedTags
		{
			get
			{
				return CustomStateDatumType.GetStaticString("SupportedTags");
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0001D858 File Offset: 0x0001BA58
		internal static string IdMapping
		{
			get
			{
				return CustomStateDatumType.GetStaticString("IdMapping");
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0001D864 File Offset: 0x0001BA64
		internal static string SyncKey
		{
			get
			{
				return CustomStateDatumType.GetStaticString("SyncKey");
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0001D870 File Offset: 0x0001BA70
		internal static string RecoverySyncKey
		{
			get
			{
				return CustomStateDatumType.GetStaticString("RecoverySyncKey");
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001D87C File Offset: 0x0001BA7C
		internal static string FilterType
		{
			get
			{
				return CustomStateDatumType.GetStaticString("FilterType");
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001D888 File Offset: 0x0001BA88
		internal static string MaxItems
		{
			get
			{
				return CustomStateDatumType.GetStaticString("MaxItems");
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001D894 File Offset: 0x0001BA94
		internal static string ConversationMode
		{
			get
			{
				return CustomStateDatumType.GetStaticString("ConversationMode");
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		internal static string CalendarSyncState
		{
			get
			{
				return CustomStateDatumType.GetStaticString("CalendarSyncState");
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001D8AC File Offset: 0x0001BAAC
		internal static string RecoveryCalendarSyncState
		{
			get
			{
				return CustomStateDatumType.GetStaticString("RecoveryCalendarSyncState");
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001D8B8 File Offset: 0x0001BAB8
		internal static string CalendarMasterItems
		{
			get
			{
				return CustomStateDatumType.GetStaticString("CalendarMasterItems");
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0001D8C4 File Offset: 0x0001BAC4
		internal static string CustomCalendarSyncFilter
		{
			get
			{
				return CustomStateDatumType.GetStaticString("CustomCalendarSyncFilter");
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001D8D0 File Offset: 0x0001BAD0
		internal static string AirSyncProtocolVersion
		{
			get
			{
				return CustomStateDatumType.GetStaticString("AirSyncProtocolVersion");
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0001D8DC File Offset: 0x0001BADC
		internal static string AirSyncClassType
		{
			get
			{
				return CustomStateDatumType.GetStaticString("AirSyncClassType");
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001D8E8 File Offset: 0x0001BAE8
		internal static string CachedOptionsNode
		{
			get
			{
				return CustomStateDatumType.GetStaticString("CachedOptionsNode");
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		internal static string WipeRequestTime
		{
			get
			{
				return CustomStateDatumType.GetStaticString("WipeRequestTime");
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0001D900 File Offset: 0x0001BB00
		internal static string WipeSendTime
		{
			get
			{
				return CustomStateDatumType.GetStaticString("WipeSentTime");
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001D90C File Offset: 0x0001BB0C
		internal static string WipeAckTime
		{
			get
			{
				return CustomStateDatumType.GetStaticString("WipeAckTime");
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0001D918 File Offset: 0x0001BB18
		internal static string WipeConfirmationAddresses
		{
			get
			{
				return CustomStateDatumType.GetStaticString("WipeConfirmationAddresses");
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001D924 File Offset: 0x0001BB24
		internal static string LastSyncAttemptTime
		{
			get
			{
				return CustomStateDatumType.GetStaticString("LastSyncAttemptTime");
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0001D930 File Offset: 0x0001BB30
		internal static string LastSyncSuccessTime
		{
			get
			{
				return CustomStateDatumType.GetStaticString("LastSyncSuccessTime");
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001D93C File Offset: 0x0001BB3C
		internal static string UserAgent
		{
			get
			{
				return CustomStateDatumType.GetStaticString("UserAgent");
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0001D948 File Offset: 0x0001BB48
		internal static string LastPingHeartbeat
		{
			get
			{
				return CustomStateDatumType.GetStaticString("LastPingHeartbeat");
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001D954 File Offset: 0x0001BB54
		internal static string DeviceModel
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceModel");
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001D960 File Offset: 0x0001BB60
		internal static string DeviceImei
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceIMEI");
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0001D96C File Offset: 0x0001BB6C
		internal static string DeviceFriendlyName
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceFriendlyName");
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001D978 File Offset: 0x0001BB78
		internal static string DeviceOS
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceOS");
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001D984 File Offset: 0x0001BB84
		internal static string DeviceOSLanguage
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceOSLanguage");
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0001D990 File Offset: 0x0001BB90
		internal static string DevicePhoneNumber
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DevicePhoneNumber");
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001D99C File Offset: 0x0001BB9C
		internal static string DeviceEnableOutboundSMS
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceEnableOutboundSMS");
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		internal static string DeviceMobileOperator
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceMobileOperator");
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001D9B4 File Offset: 0x0001BBB4
		internal static string DPFolderList
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DPFolderList");
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		internal static string LastPolicyXMLHash
		{
			get
			{
				return CustomStateDatumType.GetStaticString("LastPolicyXMLHash");
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001D9CC File Offset: 0x0001BBCC
		internal static string LastPolicyTime
		{
			get
			{
				return CustomStateDatumType.GetStaticString("LastPolicyTime");
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001D9D8 File Offset: 0x0001BBD8
		internal static string NextTimeToClearMailboxLogs
		{
			get
			{
				return CustomStateDatumType.GetStaticString("NextTimeToClearMailboxLogs");
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001D9E4 File Offset: 0x0001BBE4
		internal static string PolicyKeyNeeded
		{
			get
			{
				return CustomStateDatumType.GetStaticString("PolicyKeyNeeded");
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001D9F0 File Offset: 0x0001BBF0
		internal static string PolicyKeyWaitingAck
		{
			get
			{
				return CustomStateDatumType.GetStaticString("PolicyKeyWaitingAck");
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0001D9FC File Offset: 0x0001BBFC
		internal static string PolicyKeyOnDevice
		{
			get
			{
				return CustomStateDatumType.GetStaticString("PolicyKeyOnDevice");
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0001DA08 File Offset: 0x0001BC08
		internal static string RecoveryPassword
		{
			get
			{
				return CustomStateDatumType.GetStaticString("RecoveryPassword");
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0001DA14 File Offset: 0x0001BC14
		internal static string LastCachableWbxmlDocument
		{
			get
			{
				return CustomStateDatumType.GetStaticString("LastCachableWbxmlDocument");
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0001DA20 File Offset: 0x0001BC20
		internal static string ClientCanSendUpEmptyRequests
		{
			get
			{
				return CustomStateDatumType.GetStaticString("ClientCanSendUpEmptyRequests");
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001DA2C File Offset: 0x0001BC2C
		internal static string LastSyncRequestRandomNumber
		{
			get
			{
				return CustomStateDatumType.GetStaticString("LastSyncRequestRandomString");
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0001DA38 File Offset: 0x0001BC38
		internal static string LastClientIdsSent
		{
			get
			{
				return CustomStateDatumType.GetStaticString("LastClientIdsSent");
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001DA44 File Offset: 0x0001BC44
		internal static string ProvisionSupported
		{
			get
			{
				return CustomStateDatumType.GetStaticString("ProvisionSupported");
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001DA50 File Offset: 0x0001BC50
		internal static string Permissions
		{
			get
			{
				return CustomStateDatumType.GetStaticString("Permissions");
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		internal static string FullFolderTree
		{
			get
			{
				return CustomStateDatumType.GetStaticString("FullFolderTree");
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001DA68 File Offset: 0x0001BC68
		internal static string RecoveryFullFolderTree
		{
			get
			{
				return CustomStateDatumType.GetStaticString("RecoveryFullFolderTree");
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001DA74 File Offset: 0x0001BC74
		internal static string ClientCategoryList
		{
			get
			{
				return CustomStateDatumType.GetStaticString("ClientCategoryList");
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001DA80 File Offset: 0x0001BC80
		internal static string SSUpgradeDateTime
		{
			get
			{
				return CustomStateDatumType.GetStaticString("SSUpgradeDateTime");
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0001DA8C File Offset: 0x0001BC8C
		internal static string HaveSentBoostrapMailForWM61
		{
			get
			{
				return CustomStateDatumType.GetStaticString("HaveSentBoostrapMailForWM61");
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001DA98 File Offset: 0x0001BC98
		internal static string BootstrapMailForWM61TriggeredTime
		{
			get
			{
				return CustomStateDatumType.GetStaticString("BootstrapMailForWM61TriggeredTime");
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0001DAA4 File Offset: 0x0001BCA4
		internal static string DeviceAccessState
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceAccessState");
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
		internal static string DeviceAccessStateReason
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceAccessStateReason");
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001DABC File Offset: 0x0001BCBC
		internal static string DeviceAccessControlRule
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceAccessControlRule");
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001DAC8 File Offset: 0x0001BCC8
		internal static string DevicePolicyApplied
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DevicePolicyApplied");
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0001DAD4 File Offset: 0x0001BCD4
		internal static string DevicePolicyApplicationStatus
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DevicePolicyApplicationStatus");
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0001DAE0 File Offset: 0x0001BCE0
		internal static string LastDeviceWipeRequestor
		{
			get
			{
				return CustomStateDatumType.GetStaticString("LastDeviceWipeRequestor");
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0001DAEC File Offset: 0x0001BCEC
		internal static string DeviceActiveSyncVersion
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceActiveSyncVersion");
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		internal static string ADDeviceInfoHash
		{
			get
			{
				return CustomStateDatumType.GetStaticString("ADDeviceInfoHash");
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0001DB04 File Offset: 0x0001BD04
		internal static string DeviceInformationReceived
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceInformationReceived");
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001DB10 File Offset: 0x0001BD10
		internal static string ADCreationTime
		{
			get
			{
				return CustomStateDatumType.GetStaticString("ADCreationTime");
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001DB1C File Offset: 0x0001BD1C
		internal static string DeviceADObjectId
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceADObjectId");
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0001DB28 File Offset: 0x0001BD28
		internal static string UserADObjectId
		{
			get
			{
				return CustomStateDatumType.GetStaticString("UserADObjectId");
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0001DB34 File Offset: 0x0001BD34
		internal static string ABQMailId
		{
			get
			{
				return CustomStateDatumType.GetStaticString("ABQMailId");
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0001DB40 File Offset: 0x0001BD40
		internal static string ABQMailState
		{
			get
			{
				return CustomStateDatumType.GetStaticString("ABQMailState");
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001DB4C File Offset: 0x0001BD4C
		internal static string DeviceInformationPromoted
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceInformationPromoted");
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001DB58 File Offset: 0x0001BD58
		internal static string DevicePhoneNumberForSms
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DevicePhoneNumberForSms");
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0001DB64 File Offset: 0x0001BD64
		internal static string SmsSearchFolderCreated
		{
			get
			{
				return CustomStateDatumType.GetStaticString("SmsSearchFolderCreated");
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0001DB70 File Offset: 0x0001BD70
		internal static string LastMaxDevicesExceededMailSentTime
		{
			get
			{
				return CustomStateDatumType.GetStaticString("LastMaxDevicesExceededMailSentTime");
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0001DB7C File Offset: 0x0001BD7C
		internal static string DeviceBehavior
		{
			get
			{
				return CustomStateDatumType.GetStaticString("DeviceBehavior");
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001DB88 File Offset: 0x0001BD88
		internal static string MeetingOrganizerInfo
		{
			get
			{
				return CustomStateDatumType.GetStaticString("MeetingOrganizerInfo");
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001DB94 File Offset: 0x0001BD94
		private static void Initialize()
		{
			lock (CustomStateDatumType.syncRoot)
			{
				if (!CustomStateDatumType.initialized)
				{
					StaticStringPool.Instance.Intern("SupportedTags");
					StaticStringPool.Instance.Intern("IdMapping");
					StaticStringPool.Instance.Intern("SyncKey");
					StaticStringPool.Instance.Intern("RecoverySyncKey");
					StaticStringPool.Instance.Intern("FilterType");
					StaticStringPool.Instance.Intern("MaxItems");
					StaticStringPool.Instance.Intern("ConversationMode");
					StaticStringPool.Instance.Intern("CustomCalendarSyncFilter");
					StaticStringPool.Instance.Intern("AirSyncProtocolVersion");
					StaticStringPool.Instance.Intern("AirSyncClassType");
					StaticStringPool.Instance.Intern("CachedOptionsNode");
					StaticStringPool.Instance.Intern("Permissions");
					StaticStringPool.Instance.Intern("WipeRequestTime");
					StaticStringPool.Instance.Intern("WipeSentTime");
					StaticStringPool.Instance.Intern("WipeAckTime");
					StaticStringPool.Instance.Intern("WipeConfirmationAddresses");
					StaticStringPool.Instance.Intern("LastSyncAttemptTime");
					StaticStringPool.Instance.Intern("LastSyncSuccessTime");
					StaticStringPool.Instance.Intern("UserAgent");
					StaticStringPool.Instance.Intern("LastPingHeartbeat");
					StaticStringPool.Instance.Intern("DeviceModel");
					StaticStringPool.Instance.Intern("DeviceIMEI");
					StaticStringPool.Instance.Intern("DeviceFriendlyName");
					StaticStringPool.Instance.Intern("DeviceOS");
					StaticStringPool.Instance.Intern("DeviceOSLanguage");
					StaticStringPool.Instance.Intern("DevicePhoneNumber");
					StaticStringPool.Instance.Intern("DPFolderList");
					StaticStringPool.Instance.Intern("LastPolicyXMLHash");
					StaticStringPool.Instance.Intern("LastPolicyTime");
					StaticStringPool.Instance.Intern("NextTimeToClearMailboxLogs");
					StaticStringPool.Instance.Intern("PolicyKeyNeeded");
					StaticStringPool.Instance.Intern("PolicyKeyWaitingAck");
					StaticStringPool.Instance.Intern("PolicyKeyOnDevice");
					StaticStringPool.Instance.Intern("RecoveryPassword");
					StaticStringPool.Instance.Intern("LastCachableWbxmlDocument");
					StaticStringPool.Instance.Intern("ClientCanSendUpEmptyRequests");
					StaticStringPool.Instance.Intern("LastSyncRequestRandomString");
					StaticStringPool.Instance.Intern("LastClientIdsSent");
					StaticStringPool.Instance.Intern("ProvisionSupported");
					StaticStringPool.Instance.Intern("DeviceEnableOutboundSMS");
					StaticStringPool.Instance.Intern("DeviceMobileOperator");
					StaticStringPool.Instance.Intern("FullFolderTree");
					StaticStringPool.Instance.Intern("RecoveryFullFolderTree");
					StaticStringPool.Instance.Intern("ClientCategoryList");
					StaticStringPool.Instance.Intern("SSUpgradeDateTime");
					StaticStringPool.Instance.Intern("HaveSentBoostrapMailForWM61");
					StaticStringPool.Instance.Intern("BootstrapMailForWM61TriggeredTime");
					StaticStringPool.Instance.Intern("DeviceAccessState");
					StaticStringPool.Instance.Intern("DeviceAccessStateReason");
					StaticStringPool.Instance.Intern("DevicePolicyApplied");
					StaticStringPool.Instance.Intern("DevicePolicyApplicationStatus");
					StaticStringPool.Instance.Intern("DeviceAccessControlRule");
					StaticStringPool.Instance.Intern("LastDeviceWipeRequestor");
					StaticStringPool.Instance.Intern("DeviceActiveSyncVersion");
					StaticStringPool.Instance.Intern("ADDeviceInfoHash");
					StaticStringPool.Instance.Intern("DeviceInformationReceived");
					StaticStringPool.Instance.Intern("ADCreationTime");
					StaticStringPool.Instance.Intern("DeviceADObjectId");
					StaticStringPool.Instance.Intern("UserADObjectId");
					StaticStringPool.Instance.Intern("ABQMailId");
					StaticStringPool.Instance.Intern("ABQMailState");
					StaticStringPool.Instance.Intern("DeviceInformationPromoted");
					StaticStringPool.Instance.Intern("DevicePhoneNumberForSms");
					StaticStringPool.Instance.Intern("SmsSearchFolderCreated");
					StaticStringPool.Instance.Intern("LastMaxDevicesExceededMailSentTime");
					StaticStringPool.Instance.Intern("DeviceBehavior");
					StaticStringPool.Instance.Intern("MeetingOrganizerInfo");
					StaticStringPool.Instance.Intern("CalendarSyncState");
					StaticStringPool.Instance.Intern("RecoveryCalendarSyncState");
					CustomStateDatumType.initialized = true;
				}
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001E03C File Offset: 0x0001C23C
		private static string GetStaticString(string key)
		{
			if (!CustomStateDatumType.initialized)
			{
				CustomStateDatumType.Initialize();
			}
			return key;
		}

		// Token: 0x0400039E RID: 926
		private static object syncRoot = new object();

		// Token: 0x0400039F RID: 927
		private static bool initialized;
	}
}
