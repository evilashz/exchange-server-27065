using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000184 RID: 388
	[XmlInclude(typeof(BaseResponseMessageType))]
	[XmlInclude(typeof(BaseFolderType))]
	[XmlInclude(typeof(BaseRequestType))]
	[CLSCompliant(false)]
	[XmlInclude(typeof(BaseItemIdType))]
	[XmlInclude(typeof(BaseEmailAddressType))]
	[XmlInclude(typeof(BaseFolderIdType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "ExchangeServiceBinding", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(AttendeeConflictData))]
	[XmlInclude(typeof(ServiceConfiguration))]
	[XmlInclude(typeof(DirectoryEntryType))]
	[XmlInclude(typeof(BaseCalendarItemStateDefinitionType))]
	[XmlInclude(typeof(RuleOperationType))]
	[XmlInclude(typeof(BaseSubscriptionRequestType))]
	[XmlInclude(typeof(MailboxLocatorType))]
	[XmlInclude(typeof(BaseGroupByType))]
	[XmlInclude(typeof(RecurrenceRangeBaseType))]
	[XmlInclude(typeof(RecurrencePatternBaseType))]
	[XmlInclude(typeof(AttachmentType))]
	[XmlInclude(typeof(ChangeDescriptionType))]
	[XmlInclude(typeof(BasePagingType))]
	[XmlInclude(typeof(BasePermissionType))]
	public class ExchangeServiceBinding : CustomSoapHttpClientProtocol, IExchangeService
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x0001A87C File Offset: 0x00018A7C
		public ExchangeServiceBinding()
		{
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000950 RID: 2384 RVA: 0x0001A884 File Offset: 0x00018A84
		// (remove) Token: 0x06000951 RID: 2385 RVA: 0x0001A8BC File Offset: 0x00018ABC
		public event ResolveNamesCompletedEventHandler ResolveNamesCompleted;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000952 RID: 2386 RVA: 0x0001A8F4 File Offset: 0x00018AF4
		// (remove) Token: 0x06000953 RID: 2387 RVA: 0x0001A92C File Offset: 0x00018B2C
		public event ExpandDLCompletedEventHandler ExpandDLCompleted;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000954 RID: 2388 RVA: 0x0001A964 File Offset: 0x00018B64
		// (remove) Token: 0x06000955 RID: 2389 RVA: 0x0001A99C File Offset: 0x00018B9C
		public event GetServerTimeZonesCompletedEventHandler GetServerTimeZonesCompleted;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000956 RID: 2390 RVA: 0x0001A9D4 File Offset: 0x00018BD4
		// (remove) Token: 0x06000957 RID: 2391 RVA: 0x0001AA0C File Offset: 0x00018C0C
		public event FindFolderCompletedEventHandler FindFolderCompleted;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000958 RID: 2392 RVA: 0x0001AA44 File Offset: 0x00018C44
		// (remove) Token: 0x06000959 RID: 2393 RVA: 0x0001AA7C File Offset: 0x00018C7C
		public event FindItemCompletedEventHandler FindItemCompleted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600095A RID: 2394 RVA: 0x0001AAB4 File Offset: 0x00018CB4
		// (remove) Token: 0x0600095B RID: 2395 RVA: 0x0001AAEC File Offset: 0x00018CEC
		public event GetFolderCompletedEventHandler GetFolderCompleted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600095C RID: 2396 RVA: 0x0001AB24 File Offset: 0x00018D24
		// (remove) Token: 0x0600095D RID: 2397 RVA: 0x0001AB5C File Offset: 0x00018D5C
		public event UploadItemsCompletedEventHandler UploadItemsCompleted;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600095E RID: 2398 RVA: 0x0001AB94 File Offset: 0x00018D94
		// (remove) Token: 0x0600095F RID: 2399 RVA: 0x0001ABCC File Offset: 0x00018DCC
		public event ExportItemsCompletedEventHandler ExportItemsCompleted;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000960 RID: 2400 RVA: 0x0001AC04 File Offset: 0x00018E04
		// (remove) Token: 0x06000961 RID: 2401 RVA: 0x0001AC3C File Offset: 0x00018E3C
		public event ConvertIdCompletedEventHandler ConvertIdCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000962 RID: 2402 RVA: 0x0001AC74 File Offset: 0x00018E74
		// (remove) Token: 0x06000963 RID: 2403 RVA: 0x0001ACAC File Offset: 0x00018EAC
		public event CreateFolderCompletedEventHandler CreateFolderCompleted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000964 RID: 2404 RVA: 0x0001ACE4 File Offset: 0x00018EE4
		// (remove) Token: 0x06000965 RID: 2405 RVA: 0x0001AD1C File Offset: 0x00018F1C
		public event CreateFolderPathCompletedEventHandler CreateFolderPathCompleted;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000966 RID: 2406 RVA: 0x0001AD54 File Offset: 0x00018F54
		// (remove) Token: 0x06000967 RID: 2407 RVA: 0x0001AD8C File Offset: 0x00018F8C
		public event DeleteFolderCompletedEventHandler DeleteFolderCompleted;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000968 RID: 2408 RVA: 0x0001ADC4 File Offset: 0x00018FC4
		// (remove) Token: 0x06000969 RID: 2409 RVA: 0x0001ADFC File Offset: 0x00018FFC
		public event EmptyFolderCompletedEventHandler EmptyFolderCompleted;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600096A RID: 2410 RVA: 0x0001AE34 File Offset: 0x00019034
		// (remove) Token: 0x0600096B RID: 2411 RVA: 0x0001AE6C File Offset: 0x0001906C
		public event UpdateFolderCompletedEventHandler UpdateFolderCompleted;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600096C RID: 2412 RVA: 0x0001AEA4 File Offset: 0x000190A4
		// (remove) Token: 0x0600096D RID: 2413 RVA: 0x0001AEDC File Offset: 0x000190DC
		public event MoveFolderCompletedEventHandler MoveFolderCompleted;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600096E RID: 2414 RVA: 0x0001AF14 File Offset: 0x00019114
		// (remove) Token: 0x0600096F RID: 2415 RVA: 0x0001AF4C File Offset: 0x0001914C
		public event CopyFolderCompletedEventHandler CopyFolderCompleted;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000970 RID: 2416 RVA: 0x0001AF84 File Offset: 0x00019184
		// (remove) Token: 0x06000971 RID: 2417 RVA: 0x0001AFBC File Offset: 0x000191BC
		public event SubscribeCompletedEventHandler SubscribeCompleted;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000972 RID: 2418 RVA: 0x0001AFF4 File Offset: 0x000191F4
		// (remove) Token: 0x06000973 RID: 2419 RVA: 0x0001B02C File Offset: 0x0001922C
		public event UnsubscribeCompletedEventHandler UnsubscribeCompleted;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000974 RID: 2420 RVA: 0x0001B064 File Offset: 0x00019264
		// (remove) Token: 0x06000975 RID: 2421 RVA: 0x0001B09C File Offset: 0x0001929C
		public event GetEventsCompletedEventHandler GetEventsCompleted;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000976 RID: 2422 RVA: 0x0001B0D4 File Offset: 0x000192D4
		// (remove) Token: 0x06000977 RID: 2423 RVA: 0x0001B10C File Offset: 0x0001930C
		public event GetStreamingEventsCompletedEventHandler GetStreamingEventsCompleted;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000978 RID: 2424 RVA: 0x0001B144 File Offset: 0x00019344
		// (remove) Token: 0x06000979 RID: 2425 RVA: 0x0001B17C File Offset: 0x0001937C
		public event SyncFolderHierarchyCompletedEventHandler SyncFolderHierarchyCompleted;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x0600097A RID: 2426 RVA: 0x0001B1B4 File Offset: 0x000193B4
		// (remove) Token: 0x0600097B RID: 2427 RVA: 0x0001B1EC File Offset: 0x000193EC
		public event SyncFolderItemsCompletedEventHandler SyncFolderItemsCompleted;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600097C RID: 2428 RVA: 0x0001B224 File Offset: 0x00019424
		// (remove) Token: 0x0600097D RID: 2429 RVA: 0x0001B25C File Offset: 0x0001945C
		public event CreateManagedFolderCompletedEventHandler CreateManagedFolderCompleted;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600097E RID: 2430 RVA: 0x0001B294 File Offset: 0x00019494
		// (remove) Token: 0x0600097F RID: 2431 RVA: 0x0001B2CC File Offset: 0x000194CC
		public event GetItemCompletedEventHandler GetItemCompleted;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000980 RID: 2432 RVA: 0x0001B304 File Offset: 0x00019504
		// (remove) Token: 0x06000981 RID: 2433 RVA: 0x0001B33C File Offset: 0x0001953C
		public event CreateItemCompletedEventHandler CreateItemCompleted;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000982 RID: 2434 RVA: 0x0001B374 File Offset: 0x00019574
		// (remove) Token: 0x06000983 RID: 2435 RVA: 0x0001B3AC File Offset: 0x000195AC
		public event DeleteItemCompletedEventHandler DeleteItemCompleted;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000984 RID: 2436 RVA: 0x0001B3E4 File Offset: 0x000195E4
		// (remove) Token: 0x06000985 RID: 2437 RVA: 0x0001B41C File Offset: 0x0001961C
		public event UpdateItemCompletedEventHandler UpdateItemCompleted;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000986 RID: 2438 RVA: 0x0001B454 File Offset: 0x00019654
		// (remove) Token: 0x06000987 RID: 2439 RVA: 0x0001B48C File Offset: 0x0001968C
		public event UpdateItemInRecoverableItemsCompletedEventHandler UpdateItemInRecoverableItemsCompleted;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000988 RID: 2440 RVA: 0x0001B4C4 File Offset: 0x000196C4
		// (remove) Token: 0x06000989 RID: 2441 RVA: 0x0001B4FC File Offset: 0x000196FC
		public event SendItemCompletedEventHandler SendItemCompleted;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600098A RID: 2442 RVA: 0x0001B534 File Offset: 0x00019734
		// (remove) Token: 0x0600098B RID: 2443 RVA: 0x0001B56C File Offset: 0x0001976C
		public event MoveItemCompletedEventHandler MoveItemCompleted;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600098C RID: 2444 RVA: 0x0001B5A4 File Offset: 0x000197A4
		// (remove) Token: 0x0600098D RID: 2445 RVA: 0x0001B5DC File Offset: 0x000197DC
		public event CopyItemCompletedEventHandler CopyItemCompleted;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600098E RID: 2446 RVA: 0x0001B614 File Offset: 0x00019814
		// (remove) Token: 0x0600098F RID: 2447 RVA: 0x0001B64C File Offset: 0x0001984C
		public event ArchiveItemCompletedEventHandler ArchiveItemCompleted;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000990 RID: 2448 RVA: 0x0001B684 File Offset: 0x00019884
		// (remove) Token: 0x06000991 RID: 2449 RVA: 0x0001B6BC File Offset: 0x000198BC
		public event CreateAttachmentCompletedEventHandler CreateAttachmentCompleted;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000992 RID: 2450 RVA: 0x0001B6F4 File Offset: 0x000198F4
		// (remove) Token: 0x06000993 RID: 2451 RVA: 0x0001B72C File Offset: 0x0001992C
		public event DeleteAttachmentCompletedEventHandler DeleteAttachmentCompleted;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000994 RID: 2452 RVA: 0x0001B764 File Offset: 0x00019964
		// (remove) Token: 0x06000995 RID: 2453 RVA: 0x0001B79C File Offset: 0x0001999C
		public event GetAttachmentCompletedEventHandler GetAttachmentCompleted;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000996 RID: 2454 RVA: 0x0001B7D4 File Offset: 0x000199D4
		// (remove) Token: 0x06000997 RID: 2455 RVA: 0x0001B80C File Offset: 0x00019A0C
		public event GetClientAccessTokenCompletedEventHandler GetClientAccessTokenCompleted;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000998 RID: 2456 RVA: 0x0001B844 File Offset: 0x00019A44
		// (remove) Token: 0x06000999 RID: 2457 RVA: 0x0001B87C File Offset: 0x00019A7C
		public event GetDelegateCompletedEventHandler GetDelegateCompleted;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600099A RID: 2458 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		// (remove) Token: 0x0600099B RID: 2459 RVA: 0x0001B8EC File Offset: 0x00019AEC
		public event AddDelegateCompletedEventHandler AddDelegateCompleted;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600099C RID: 2460 RVA: 0x0001B924 File Offset: 0x00019B24
		// (remove) Token: 0x0600099D RID: 2461 RVA: 0x0001B95C File Offset: 0x00019B5C
		public event RemoveDelegateCompletedEventHandler RemoveDelegateCompleted;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600099E RID: 2462 RVA: 0x0001B994 File Offset: 0x00019B94
		// (remove) Token: 0x0600099F RID: 2463 RVA: 0x0001B9CC File Offset: 0x00019BCC
		public event UpdateDelegateCompletedEventHandler UpdateDelegateCompleted;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060009A0 RID: 2464 RVA: 0x0001BA04 File Offset: 0x00019C04
		// (remove) Token: 0x060009A1 RID: 2465 RVA: 0x0001BA3C File Offset: 0x00019C3C
		public event CreateUserConfigurationCompletedEventHandler CreateUserConfigurationCompleted;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060009A2 RID: 2466 RVA: 0x0001BA74 File Offset: 0x00019C74
		// (remove) Token: 0x060009A3 RID: 2467 RVA: 0x0001BAAC File Offset: 0x00019CAC
		public event DeleteUserConfigurationCompletedEventHandler DeleteUserConfigurationCompleted;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060009A4 RID: 2468 RVA: 0x0001BAE4 File Offset: 0x00019CE4
		// (remove) Token: 0x060009A5 RID: 2469 RVA: 0x0001BB1C File Offset: 0x00019D1C
		public event GetUserConfigurationCompletedEventHandler GetUserConfigurationCompleted;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060009A6 RID: 2470 RVA: 0x0001BB54 File Offset: 0x00019D54
		// (remove) Token: 0x060009A7 RID: 2471 RVA: 0x0001BB8C File Offset: 0x00019D8C
		public event UpdateUserConfigurationCompletedEventHandler UpdateUserConfigurationCompleted;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060009A8 RID: 2472 RVA: 0x0001BBC4 File Offset: 0x00019DC4
		// (remove) Token: 0x060009A9 RID: 2473 RVA: 0x0001BBFC File Offset: 0x00019DFC
		public event GetUserAvailabilityCompletedEventHandler GetUserAvailabilityCompleted;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060009AA RID: 2474 RVA: 0x0001BC34 File Offset: 0x00019E34
		// (remove) Token: 0x060009AB RID: 2475 RVA: 0x0001BC6C File Offset: 0x00019E6C
		public event GetUserOofSettingsCompletedEventHandler GetUserOofSettingsCompleted;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060009AC RID: 2476 RVA: 0x0001BCA4 File Offset: 0x00019EA4
		// (remove) Token: 0x060009AD RID: 2477 RVA: 0x0001BCDC File Offset: 0x00019EDC
		public event SetUserOofSettingsCompletedEventHandler SetUserOofSettingsCompleted;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060009AE RID: 2478 RVA: 0x0001BD14 File Offset: 0x00019F14
		// (remove) Token: 0x060009AF RID: 2479 RVA: 0x0001BD4C File Offset: 0x00019F4C
		public event GetServiceConfigurationCompletedEventHandler GetServiceConfigurationCompleted;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060009B0 RID: 2480 RVA: 0x0001BD84 File Offset: 0x00019F84
		// (remove) Token: 0x060009B1 RID: 2481 RVA: 0x0001BDBC File Offset: 0x00019FBC
		public event GetMailTipsCompletedEventHandler GetMailTipsCompleted;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060009B2 RID: 2482 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		// (remove) Token: 0x060009B3 RID: 2483 RVA: 0x0001BE2C File Offset: 0x0001A02C
		public event PlayOnPhoneCompletedEventHandler PlayOnPhoneCompleted;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060009B4 RID: 2484 RVA: 0x0001BE64 File Offset: 0x0001A064
		// (remove) Token: 0x060009B5 RID: 2485 RVA: 0x0001BE9C File Offset: 0x0001A09C
		public event GetPhoneCallInformationCompletedEventHandler GetPhoneCallInformationCompleted;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060009B6 RID: 2486 RVA: 0x0001BED4 File Offset: 0x0001A0D4
		// (remove) Token: 0x060009B7 RID: 2487 RVA: 0x0001BF0C File Offset: 0x0001A10C
		public event DisconnectPhoneCallCompletedEventHandler DisconnectPhoneCallCompleted;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060009B8 RID: 2488 RVA: 0x0001BF44 File Offset: 0x0001A144
		// (remove) Token: 0x060009B9 RID: 2489 RVA: 0x0001BF7C File Offset: 0x0001A17C
		public event GetSharingMetadataCompletedEventHandler GetSharingMetadataCompleted;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060009BA RID: 2490 RVA: 0x0001BFB4 File Offset: 0x0001A1B4
		// (remove) Token: 0x060009BB RID: 2491 RVA: 0x0001BFEC File Offset: 0x0001A1EC
		public event RefreshSharingFolderCompletedEventHandler RefreshSharingFolderCompleted;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x060009BC RID: 2492 RVA: 0x0001C024 File Offset: 0x0001A224
		// (remove) Token: 0x060009BD RID: 2493 RVA: 0x0001C05C File Offset: 0x0001A25C
		public event GetSharingFolderCompletedEventHandler GetSharingFolderCompleted;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060009BE RID: 2494 RVA: 0x0001C094 File Offset: 0x0001A294
		// (remove) Token: 0x060009BF RID: 2495 RVA: 0x0001C0CC File Offset: 0x0001A2CC
		public event SetTeamMailboxCompletedEventHandler SetTeamMailboxCompleted;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x060009C0 RID: 2496 RVA: 0x0001C104 File Offset: 0x0001A304
		// (remove) Token: 0x060009C1 RID: 2497 RVA: 0x0001C13C File Offset: 0x0001A33C
		public event UnpinTeamMailboxCompletedEventHandler UnpinTeamMailboxCompleted;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060009C2 RID: 2498 RVA: 0x0001C174 File Offset: 0x0001A374
		// (remove) Token: 0x060009C3 RID: 2499 RVA: 0x0001C1AC File Offset: 0x0001A3AC
		public event GetRoomListsCompletedEventHandler GetRoomListsCompleted;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060009C4 RID: 2500 RVA: 0x0001C1E4 File Offset: 0x0001A3E4
		// (remove) Token: 0x060009C5 RID: 2501 RVA: 0x0001C21C File Offset: 0x0001A41C
		public event GetRoomsCompletedEventHandler GetRoomsCompleted;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060009C6 RID: 2502 RVA: 0x0001C254 File Offset: 0x0001A454
		// (remove) Token: 0x060009C7 RID: 2503 RVA: 0x0001C28C File Offset: 0x0001A48C
		public event FindMessageTrackingReportCompletedEventHandler FindMessageTrackingReportCompleted;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060009C8 RID: 2504 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		// (remove) Token: 0x060009C9 RID: 2505 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		public event GetMessageTrackingReportCompletedEventHandler GetMessageTrackingReportCompleted;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060009CA RID: 2506 RVA: 0x0001C334 File Offset: 0x0001A534
		// (remove) Token: 0x060009CB RID: 2507 RVA: 0x0001C36C File Offset: 0x0001A56C
		public event FindConversationCompletedEventHandler FindConversationCompleted;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060009CC RID: 2508 RVA: 0x0001C3A4 File Offset: 0x0001A5A4
		// (remove) Token: 0x060009CD RID: 2509 RVA: 0x0001C3DC File Offset: 0x0001A5DC
		public event ApplyConversationActionCompletedEventHandler ApplyConversationActionCompleted;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060009CE RID: 2510 RVA: 0x0001C414 File Offset: 0x0001A614
		// (remove) Token: 0x060009CF RID: 2511 RVA: 0x0001C44C File Offset: 0x0001A64C
		public event GetConversationItemsCompletedEventHandler GetConversationItemsCompleted;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060009D0 RID: 2512 RVA: 0x0001C484 File Offset: 0x0001A684
		// (remove) Token: 0x060009D1 RID: 2513 RVA: 0x0001C4BC File Offset: 0x0001A6BC
		public event FindPeopleCompletedEventHandler FindPeopleCompleted;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x060009D2 RID: 2514 RVA: 0x0001C4F4 File Offset: 0x0001A6F4
		// (remove) Token: 0x060009D3 RID: 2515 RVA: 0x0001C52C File Offset: 0x0001A72C
		public event GetPersonaCompletedEventHandler GetPersonaCompleted;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x060009D4 RID: 2516 RVA: 0x0001C564 File Offset: 0x0001A764
		// (remove) Token: 0x060009D5 RID: 2517 RVA: 0x0001C59C File Offset: 0x0001A79C
		public event GetInboxRulesCompletedEventHandler GetInboxRulesCompleted;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x060009D6 RID: 2518 RVA: 0x0001C5D4 File Offset: 0x0001A7D4
		// (remove) Token: 0x060009D7 RID: 2519 RVA: 0x0001C60C File Offset: 0x0001A80C
		public event UpdateInboxRulesCompletedEventHandler UpdateInboxRulesCompleted;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060009D8 RID: 2520 RVA: 0x0001C644 File Offset: 0x0001A844
		// (remove) Token: 0x060009D9 RID: 2521 RVA: 0x0001C67C File Offset: 0x0001A87C
		public event GetPasswordExpirationDateCompletedEventHandler GetPasswordExpirationDateCompleted;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060009DA RID: 2522 RVA: 0x0001C6B4 File Offset: 0x0001A8B4
		// (remove) Token: 0x060009DB RID: 2523 RVA: 0x0001C6EC File Offset: 0x0001A8EC
		public event GetSearchableMailboxesCompletedEventHandler GetSearchableMailboxesCompleted;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060009DC RID: 2524 RVA: 0x0001C724 File Offset: 0x0001A924
		// (remove) Token: 0x060009DD RID: 2525 RVA: 0x0001C75C File Offset: 0x0001A95C
		public event SearchMailboxesCompletedEventHandler SearchMailboxesCompleted;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060009DE RID: 2526 RVA: 0x0001C794 File Offset: 0x0001A994
		// (remove) Token: 0x060009DF RID: 2527 RVA: 0x0001C7CC File Offset: 0x0001A9CC
		public event GetDiscoverySearchConfigurationCompletedEventHandler GetDiscoverySearchConfigurationCompleted;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060009E0 RID: 2528 RVA: 0x0001C804 File Offset: 0x0001AA04
		// (remove) Token: 0x060009E1 RID: 2529 RVA: 0x0001C83C File Offset: 0x0001AA3C
		public event GetHoldOnMailboxesCompletedEventHandler GetHoldOnMailboxesCompleted;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060009E2 RID: 2530 RVA: 0x0001C874 File Offset: 0x0001AA74
		// (remove) Token: 0x060009E3 RID: 2531 RVA: 0x0001C8AC File Offset: 0x0001AAAC
		public event SetHoldOnMailboxesCompletedEventHandler SetHoldOnMailboxesCompleted;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060009E4 RID: 2532 RVA: 0x0001C8E4 File Offset: 0x0001AAE4
		// (remove) Token: 0x060009E5 RID: 2533 RVA: 0x0001C91C File Offset: 0x0001AB1C
		public event GetNonIndexableItemStatisticsCompletedEventHandler GetNonIndexableItemStatisticsCompleted;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x060009E6 RID: 2534 RVA: 0x0001C954 File Offset: 0x0001AB54
		// (remove) Token: 0x060009E7 RID: 2535 RVA: 0x0001C98C File Offset: 0x0001AB8C
		public event GetNonIndexableItemDetailsCompletedEventHandler GetNonIndexableItemDetailsCompleted;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060009E8 RID: 2536 RVA: 0x0001C9C4 File Offset: 0x0001ABC4
		// (remove) Token: 0x060009E9 RID: 2537 RVA: 0x0001C9FC File Offset: 0x0001ABFC
		public event MarkAllItemsAsReadCompletedEventHandler MarkAllItemsAsReadCompleted;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060009EA RID: 2538 RVA: 0x0001CA34 File Offset: 0x0001AC34
		// (remove) Token: 0x060009EB RID: 2539 RVA: 0x0001CA6C File Offset: 0x0001AC6C
		public event MarkAsJunkCompletedEventHandler MarkAsJunkCompleted;

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060009EC RID: 2540 RVA: 0x0001CAA4 File Offset: 0x0001ACA4
		// (remove) Token: 0x060009ED RID: 2541 RVA: 0x0001CADC File Offset: 0x0001ACDC
		public event GetAppManifestsCompletedEventHandler GetAppManifestsCompleted;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x060009EE RID: 2542 RVA: 0x0001CB14 File Offset: 0x0001AD14
		// (remove) Token: 0x060009EF RID: 2543 RVA: 0x0001CB4C File Offset: 0x0001AD4C
		public event AddNewImContactToGroupCompletedEventHandler AddNewImContactToGroupCompleted;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060009F0 RID: 2544 RVA: 0x0001CB84 File Offset: 0x0001AD84
		// (remove) Token: 0x060009F1 RID: 2545 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		public event AddNewTelUriContactToGroupCompletedEventHandler AddNewTelUriContactToGroupCompleted;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060009F2 RID: 2546 RVA: 0x0001CBF4 File Offset: 0x0001ADF4
		// (remove) Token: 0x060009F3 RID: 2547 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		public event AddImContactToGroupCompletedEventHandler AddImContactToGroupCompleted;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060009F4 RID: 2548 RVA: 0x0001CC64 File Offset: 0x0001AE64
		// (remove) Token: 0x060009F5 RID: 2549 RVA: 0x0001CC9C File Offset: 0x0001AE9C
		public event RemoveImContactFromGroupCompletedEventHandler RemoveImContactFromGroupCompleted;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x060009F6 RID: 2550 RVA: 0x0001CCD4 File Offset: 0x0001AED4
		// (remove) Token: 0x060009F7 RID: 2551 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		public event AddImGroupCompletedEventHandler AddImGroupCompleted;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060009F8 RID: 2552 RVA: 0x0001CD44 File Offset: 0x0001AF44
		// (remove) Token: 0x060009F9 RID: 2553 RVA: 0x0001CD7C File Offset: 0x0001AF7C
		public event AddDistributionGroupToImListCompletedEventHandler AddDistributionGroupToImListCompleted;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060009FA RID: 2554 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
		// (remove) Token: 0x060009FB RID: 2555 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		public event GetImItemListCompletedEventHandler GetImItemListCompleted;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060009FC RID: 2556 RVA: 0x0001CE24 File Offset: 0x0001B024
		// (remove) Token: 0x060009FD RID: 2557 RVA: 0x0001CE5C File Offset: 0x0001B05C
		public event GetImItemsCompletedEventHandler GetImItemsCompleted;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x060009FE RID: 2558 RVA: 0x0001CE94 File Offset: 0x0001B094
		// (remove) Token: 0x060009FF RID: 2559 RVA: 0x0001CECC File Offset: 0x0001B0CC
		public event RemoveContactFromImListCompletedEventHandler RemoveContactFromImListCompleted;

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06000A00 RID: 2560 RVA: 0x0001CF04 File Offset: 0x0001B104
		// (remove) Token: 0x06000A01 RID: 2561 RVA: 0x0001CF3C File Offset: 0x0001B13C
		public event RemoveDistributionGroupFromImListCompletedEventHandler RemoveDistributionGroupFromImListCompleted;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06000A02 RID: 2562 RVA: 0x0001CF74 File Offset: 0x0001B174
		// (remove) Token: 0x06000A03 RID: 2563 RVA: 0x0001CFAC File Offset: 0x0001B1AC
		public event RemoveImGroupCompletedEventHandler RemoveImGroupCompleted;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06000A04 RID: 2564 RVA: 0x0001CFE4 File Offset: 0x0001B1E4
		// (remove) Token: 0x06000A05 RID: 2565 RVA: 0x0001D01C File Offset: 0x0001B21C
		public event SetImGroupCompletedEventHandler SetImGroupCompleted;

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06000A06 RID: 2566 RVA: 0x0001D054 File Offset: 0x0001B254
		// (remove) Token: 0x06000A07 RID: 2567 RVA: 0x0001D08C File Offset: 0x0001B28C
		public event SetImListMigrationCompletedCompletedEventHandler SetImListMigrationCompletedCompleted;

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06000A08 RID: 2568 RVA: 0x0001D0C4 File Offset: 0x0001B2C4
		// (remove) Token: 0x06000A09 RID: 2569 RVA: 0x0001D0FC File Offset: 0x0001B2FC
		public event GetUserRetentionPolicyTagsCompletedEventHandler GetUserRetentionPolicyTagsCompleted;

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06000A0A RID: 2570 RVA: 0x0001D134 File Offset: 0x0001B334
		// (remove) Token: 0x06000A0B RID: 2571 RVA: 0x0001D16C File Offset: 0x0001B36C
		public event InstallAppCompletedEventHandler InstallAppCompleted;

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06000A0C RID: 2572 RVA: 0x0001D1A4 File Offset: 0x0001B3A4
		// (remove) Token: 0x06000A0D RID: 2573 RVA: 0x0001D1DC File Offset: 0x0001B3DC
		public event UninstallAppCompletedEventHandler UninstallAppCompleted;

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06000A0E RID: 2574 RVA: 0x0001D214 File Offset: 0x0001B414
		// (remove) Token: 0x06000A0F RID: 2575 RVA: 0x0001D24C File Offset: 0x0001B44C
		public event DisableAppCompletedEventHandler DisableAppCompleted;

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06000A10 RID: 2576 RVA: 0x0001D284 File Offset: 0x0001B484
		// (remove) Token: 0x06000A11 RID: 2577 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		public event GetAppMarketplaceUrlCompletedEventHandler GetAppMarketplaceUrlCompleted;

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06000A12 RID: 2578 RVA: 0x0001D2F4 File Offset: 0x0001B4F4
		// (remove) Token: 0x06000A13 RID: 2579 RVA: 0x0001D32C File Offset: 0x0001B52C
		public event GetUserPhotoCompletedEventHandler GetUserPhotoCompleted;

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001D364 File Offset: 0x0001B564
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ResolveNames", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("ResolveNamesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ResolveNamesResponseType ResolveNames([XmlElement("ResolveNames", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ResolveNamesType ResolveNames1)
		{
			object[] array = this.Invoke("ResolveNames", new object[]
			{
				ResolveNames1
			});
			return (ResolveNamesResponseType)array[0];
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001D394 File Offset: 0x0001B594
		public IAsyncResult BeginResolveNames(ResolveNamesType ResolveNames1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ResolveNames", new object[]
			{
				ResolveNames1
			}, callback, asyncState);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0001D3BC File Offset: 0x0001B5BC
		public ResolveNamesResponseType EndResolveNames(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ResolveNamesResponseType)array[0];
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001D3D9 File Offset: 0x0001B5D9
		public void ResolveNamesAsync(ResolveNamesType ResolveNames1)
		{
			this.ResolveNamesAsync(ResolveNames1, null);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		public void ResolveNamesAsync(ResolveNamesType ResolveNames1, object userState)
		{
			if (this.ResolveNamesOperationCompleted == null)
			{
				this.ResolveNamesOperationCompleted = new SendOrPostCallback(this.OnResolveNamesOperationCompleted);
			}
			base.InvokeAsync("ResolveNames", new object[]
			{
				ResolveNames1
			}, this.ResolveNamesOperationCompleted, userState);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001D42C File Offset: 0x0001B62C
		private void OnResolveNamesOperationCompleted(object arg)
		{
			if (this.ResolveNamesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ResolveNamesCompleted(this, new ResolveNamesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001D474 File Offset: 0x0001B674
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ExpandDL", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("ExpandDLResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ExpandDLResponseType ExpandDL([XmlElement("ExpandDL", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ExpandDLType ExpandDL1)
		{
			object[] array = this.Invoke("ExpandDL", new object[]
			{
				ExpandDL1
			});
			return (ExpandDLResponseType)array[0];
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
		public IAsyncResult BeginExpandDL(ExpandDLType ExpandDL1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ExpandDL", new object[]
			{
				ExpandDL1
			}, callback, asyncState);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001D4CC File Offset: 0x0001B6CC
		public ExpandDLResponseType EndExpandDL(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ExpandDLResponseType)array[0];
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001D4E9 File Offset: 0x0001B6E9
		public void ExpandDLAsync(ExpandDLType ExpandDL1)
		{
			this.ExpandDLAsync(ExpandDL1, null);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
		public void ExpandDLAsync(ExpandDLType ExpandDL1, object userState)
		{
			if (this.ExpandDLOperationCompleted == null)
			{
				this.ExpandDLOperationCompleted = new SendOrPostCallback(this.OnExpandDLOperationCompleted);
			}
			base.InvokeAsync("ExpandDL", new object[]
			{
				ExpandDL1
			}, this.ExpandDLOperationCompleted, userState);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001D53C File Offset: 0x0001B73C
		private void OnExpandDLOperationCompleted(object arg)
		{
			if (this.ExpandDLCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ExpandDLCompleted(this, new ExpandDLCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001D584 File Offset: 0x0001B784
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetServerTimeZones", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("GetServerTimeZonesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetServerTimeZonesResponseType GetServerTimeZones([XmlElement("GetServerTimeZones", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetServerTimeZonesType GetServerTimeZones1)
		{
			object[] array = this.Invoke("GetServerTimeZones", new object[]
			{
				GetServerTimeZones1
			});
			return (GetServerTimeZonesResponseType)array[0];
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001D5B4 File Offset: 0x0001B7B4
		public IAsyncResult BeginGetServerTimeZones(GetServerTimeZonesType GetServerTimeZones1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetServerTimeZones", new object[]
			{
				GetServerTimeZones1
			}, callback, asyncState);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001D5DC File Offset: 0x0001B7DC
		public GetServerTimeZonesResponseType EndGetServerTimeZones(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetServerTimeZonesResponseType)array[0];
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001D5F9 File Offset: 0x0001B7F9
		public void GetServerTimeZonesAsync(GetServerTimeZonesType GetServerTimeZones1)
		{
			this.GetServerTimeZonesAsync(GetServerTimeZones1, null);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001D604 File Offset: 0x0001B804
		public void GetServerTimeZonesAsync(GetServerTimeZonesType GetServerTimeZones1, object userState)
		{
			if (this.GetServerTimeZonesOperationCompleted == null)
			{
				this.GetServerTimeZonesOperationCompleted = new SendOrPostCallback(this.OnGetServerTimeZonesOperationCompleted);
			}
			base.InvokeAsync("GetServerTimeZones", new object[]
			{
				GetServerTimeZones1
			}, this.GetServerTimeZonesOperationCompleted, userState);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001D64C File Offset: 0x0001B84C
		private void OnGetServerTimeZonesOperationCompleted(object arg)
		{
			if (this.GetServerTimeZonesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetServerTimeZonesCompleted(this, new GetServerTimeZonesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001D694 File Offset: 0x0001B894
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ManagementRole")]
		[SoapHeader("TimeZoneContext")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("FindFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindFolderResponseType FindFolder([XmlElement("FindFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindFolderType FindFolder1)
		{
			object[] array = this.Invoke("FindFolder", new object[]
			{
				FindFolder1
			});
			return (FindFolderResponseType)array[0];
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
		public IAsyncResult BeginFindFolder(FindFolderType FindFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindFolder", new object[]
			{
				FindFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001D6EC File Offset: 0x0001B8EC
		public FindFolderResponseType EndFindFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FindFolderResponseType)array[0];
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001D709 File Offset: 0x0001B909
		public void FindFolderAsync(FindFolderType FindFolder1)
		{
			this.FindFolderAsync(FindFolder1, null);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001D714 File Offset: 0x0001B914
		public void FindFolderAsync(FindFolderType FindFolder1, object userState)
		{
			if (this.FindFolderOperationCompleted == null)
			{
				this.FindFolderOperationCompleted = new SendOrPostCallback(this.OnFindFolderOperationCompleted);
			}
			base.InvokeAsync("FindFolder", new object[]
			{
				FindFolder1
			}, this.FindFolderOperationCompleted, userState);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001D75C File Offset: 0x0001B95C
		private void OnFindFolderOperationCompleted(object arg)
		{
			if (this.FindFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindFolderCompleted(this, new FindFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0001D7A4 File Offset: 0x0001B9A4
		[SoapHeader("ManagementRole")]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("DateTimePrecision")]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("FindItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindItemResponseType FindItem([XmlElement("FindItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindItemType FindItem1)
		{
			object[] array = this.Invoke("FindItem", new object[]
			{
				FindItem1
			});
			return (FindItemResponseType)array[0];
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001D7D4 File Offset: 0x0001B9D4
		public IAsyncResult BeginFindItem(FindItemType FindItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindItem", new object[]
			{
				FindItem1
			}, callback, asyncState);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001D7FC File Offset: 0x0001B9FC
		public FindItemResponseType EndFindItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FindItemResponseType)array[0];
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001D819 File Offset: 0x0001BA19
		public void FindItemAsync(FindItemType FindItem1)
		{
			this.FindItemAsync(FindItem1, null);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001D824 File Offset: 0x0001BA24
		public void FindItemAsync(FindItemType FindItem1, object userState)
		{
			if (this.FindItemOperationCompleted == null)
			{
				this.FindItemOperationCompleted = new SendOrPostCallback(this.OnFindItemOperationCompleted);
			}
			base.InvokeAsync("FindItem", new object[]
			{
				FindItem1
			}, this.FindItemOperationCompleted, userState);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001D86C File Offset: 0x0001BA6C
		private void OnFindItemOperationCompleted(object arg)
		{
			if (this.FindItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindItemCompleted(this, new FindItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001D8B4 File Offset: 0x0001BAB4
		[SoapHeader("ManagementRole")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetFolderResponseType GetFolder([XmlElement("GetFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetFolderType GetFolder1)
		{
			object[] array = this.Invoke("GetFolder", new object[]
			{
				GetFolder1
			});
			return (GetFolderResponseType)array[0];
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001D8E4 File Offset: 0x0001BAE4
		public IAsyncResult BeginGetFolder(GetFolderType GetFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetFolder", new object[]
			{
				GetFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001D90C File Offset: 0x0001BB0C
		public GetFolderResponseType EndGetFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetFolderResponseType)array[0];
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001D929 File Offset: 0x0001BB29
		public void GetFolderAsync(GetFolderType GetFolder1)
		{
			this.GetFolderAsync(GetFolder1, null);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001D934 File Offset: 0x0001BB34
		public void GetFolderAsync(GetFolderType GetFolder1, object userState)
		{
			if (this.GetFolderOperationCompleted == null)
			{
				this.GetFolderOperationCompleted = new SendOrPostCallback(this.OnGetFolderOperationCompleted);
			}
			base.InvokeAsync("GetFolder", new object[]
			{
				GetFolder1
			}, this.GetFolderOperationCompleted, userState);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001D97C File Offset: 0x0001BB7C
		private void OnGetFolderOperationCompleted(object arg)
		{
			if (this.GetFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetFolderCompleted(this, new GetFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001D9C4 File Offset: 0x0001BBC4
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UploadItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("UploadItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UploadItemsResponseType UploadItems([XmlElement("UploadItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UploadItemsType UploadItems1)
		{
			object[] array = this.Invoke("UploadItems", new object[]
			{
				UploadItems1
			});
			return (UploadItemsResponseType)array[0];
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001D9F4 File Offset: 0x0001BBF4
		public IAsyncResult BeginUploadItems(UploadItemsType UploadItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UploadItems", new object[]
			{
				UploadItems1
			}, callback, asyncState);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001DA1C File Offset: 0x0001BC1C
		public UploadItemsResponseType EndUploadItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UploadItemsResponseType)array[0];
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001DA39 File Offset: 0x0001BC39
		public void UploadItemsAsync(UploadItemsType UploadItems1)
		{
			this.UploadItemsAsync(UploadItems1, null);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001DA44 File Offset: 0x0001BC44
		public void UploadItemsAsync(UploadItemsType UploadItems1, object userState)
		{
			if (this.UploadItemsOperationCompleted == null)
			{
				this.UploadItemsOperationCompleted = new SendOrPostCallback(this.OnUploadItemsOperationCompleted);
			}
			base.InvokeAsync("UploadItems", new object[]
			{
				UploadItems1
			}, this.UploadItemsOperationCompleted, userState);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001DA8C File Offset: 0x0001BC8C
		private void OnUploadItemsOperationCompleted(object arg)
		{
			if (this.UploadItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UploadItemsCompleted(this, new UploadItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001DAD4 File Offset: 0x0001BCD4
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ManagementRole")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ExportItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("ExportItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ExportItemsResponseType ExportItems([XmlElement("ExportItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ExportItemsType ExportItems1)
		{
			object[] array = this.Invoke("ExportItems", new object[]
			{
				ExportItems1
			});
			return (ExportItemsResponseType)array[0];
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001DB04 File Offset: 0x0001BD04
		public IAsyncResult BeginExportItems(ExportItemsType ExportItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ExportItems", new object[]
			{
				ExportItems1
			}, callback, asyncState);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		public ExportItemsResponseType EndExportItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ExportItemsResponseType)array[0];
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001DB49 File Offset: 0x0001BD49
		public void ExportItemsAsync(ExportItemsType ExportItems1)
		{
			this.ExportItemsAsync(ExportItems1, null);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001DB54 File Offset: 0x0001BD54
		public void ExportItemsAsync(ExportItemsType ExportItems1, object userState)
		{
			if (this.ExportItemsOperationCompleted == null)
			{
				this.ExportItemsOperationCompleted = new SendOrPostCallback(this.OnExportItemsOperationCompleted);
			}
			base.InvokeAsync("ExportItems", new object[]
			{
				ExportItems1
			}, this.ExportItemsOperationCompleted, userState);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001DB9C File Offset: 0x0001BD9C
		private void OnExportItemsOperationCompleted(object arg)
		{
			if (this.ExportItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ExportItemsCompleted(this, new ExportItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001DBE4 File Offset: 0x0001BDE4
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ConvertId", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("ConvertIdResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ConvertIdResponseType ConvertId([XmlElement("ConvertId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ConvertIdType ConvertId1)
		{
			object[] array = this.Invoke("ConvertId", new object[]
			{
				ConvertId1
			});
			return (ConvertIdResponseType)array[0];
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001DC14 File Offset: 0x0001BE14
		public IAsyncResult BeginConvertId(ConvertIdType ConvertId1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ConvertId", new object[]
			{
				ConvertId1
			}, callback, asyncState);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001DC3C File Offset: 0x0001BE3C
		public ConvertIdResponseType EndConvertId(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ConvertIdResponseType)array[0];
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001DC59 File Offset: 0x0001BE59
		public void ConvertIdAsync(ConvertIdType ConvertId1)
		{
			this.ConvertIdAsync(ConvertId1, null);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001DC64 File Offset: 0x0001BE64
		public void ConvertIdAsync(ConvertIdType ConvertId1, object userState)
		{
			if (this.ConvertIdOperationCompleted == null)
			{
				this.ConvertIdOperationCompleted = new SendOrPostCallback(this.OnConvertIdOperationCompleted);
			}
			base.InvokeAsync("ConvertId", new object[]
			{
				ConvertId1
			}, this.ConvertIdOperationCompleted, userState);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001DCAC File Offset: 0x0001BEAC
		private void OnConvertIdOperationCompleted(object arg)
		{
			if (this.ConvertIdCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ConvertIdCompleted(this, new ConvertIdCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001DCF4 File Offset: 0x0001BEF4
		[SoapHttpClientTraceExtension]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("CreateFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateFolderResponseType CreateFolder([XmlElement("CreateFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateFolderType CreateFolder1)
		{
			object[] array = this.Invoke("CreateFolder", new object[]
			{
				CreateFolder1
			});
			return (CreateFolderResponseType)array[0];
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001DD24 File Offset: 0x0001BF24
		public IAsyncResult BeginCreateFolder(CreateFolderType CreateFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateFolder", new object[]
			{
				CreateFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001DD4C File Offset: 0x0001BF4C
		public CreateFolderResponseType EndCreateFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateFolderResponseType)array[0];
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001DD69 File Offset: 0x0001BF69
		public void CreateFolderAsync(CreateFolderType CreateFolder1)
		{
			this.CreateFolderAsync(CreateFolder1, null);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001DD74 File Offset: 0x0001BF74
		public void CreateFolderAsync(CreateFolderType CreateFolder1, object userState)
		{
			if (this.CreateFolderOperationCompleted == null)
			{
				this.CreateFolderOperationCompleted = new SendOrPostCallback(this.OnCreateFolderOperationCompleted);
			}
			base.InvokeAsync("CreateFolder", new object[]
			{
				CreateFolder1
			}, this.CreateFolderOperationCompleted, userState);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001DDBC File Offset: 0x0001BFBC
		private void OnCreateFolderOperationCompleted(object arg)
		{
			if (this.CreateFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateFolderCompleted(this, new CreateFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001DE04 File Offset: 0x0001C004
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateFolderPath", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("CreateFolderPathResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateFolderPathResponseType CreateFolderPath([XmlElement("CreateFolderPath", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateFolderPathType CreateFolderPath1)
		{
			object[] array = this.Invoke("CreateFolderPath", new object[]
			{
				CreateFolderPath1
			});
			return (CreateFolderPathResponseType)array[0];
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001DE34 File Offset: 0x0001C034
		public IAsyncResult BeginCreateFolderPath(CreateFolderPathType CreateFolderPath1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateFolderPath", new object[]
			{
				CreateFolderPath1
			}, callback, asyncState);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001DE5C File Offset: 0x0001C05C
		public CreateFolderPathResponseType EndCreateFolderPath(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateFolderPathResponseType)array[0];
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001DE79 File Offset: 0x0001C079
		public void CreateFolderPathAsync(CreateFolderPathType CreateFolderPath1)
		{
			this.CreateFolderPathAsync(CreateFolderPath1, null);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001DE84 File Offset: 0x0001C084
		public void CreateFolderPathAsync(CreateFolderPathType CreateFolderPath1, object userState)
		{
			if (this.CreateFolderPathOperationCompleted == null)
			{
				this.CreateFolderPathOperationCompleted = new SendOrPostCallback(this.OnCreateFolderPathOperationCompleted);
			}
			base.InvokeAsync("CreateFolderPath", new object[]
			{
				CreateFolderPath1
			}, this.CreateFolderPathOperationCompleted, userState);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001DECC File Offset: 0x0001C0CC
		private void OnCreateFolderPathOperationCompleted(object arg)
		{
			if (this.CreateFolderPathCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateFolderPathCompleted(this, new CreateFolderPathCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001DF14 File Offset: 0x0001C114
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("DeleteFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DeleteFolderResponseType DeleteFolder([XmlElement("DeleteFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteFolderType DeleteFolder1)
		{
			object[] array = this.Invoke("DeleteFolder", new object[]
			{
				DeleteFolder1
			});
			return (DeleteFolderResponseType)array[0];
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001DF44 File Offset: 0x0001C144
		public IAsyncResult BeginDeleteFolder(DeleteFolderType DeleteFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteFolder", new object[]
			{
				DeleteFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001DF6C File Offset: 0x0001C16C
		public DeleteFolderResponseType EndDeleteFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DeleteFolderResponseType)array[0];
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001DF89 File Offset: 0x0001C189
		public void DeleteFolderAsync(DeleteFolderType DeleteFolder1)
		{
			this.DeleteFolderAsync(DeleteFolder1, null);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001DF94 File Offset: 0x0001C194
		public void DeleteFolderAsync(DeleteFolderType DeleteFolder1, object userState)
		{
			if (this.DeleteFolderOperationCompleted == null)
			{
				this.DeleteFolderOperationCompleted = new SendOrPostCallback(this.OnDeleteFolderOperationCompleted);
			}
			base.InvokeAsync("DeleteFolder", new object[]
			{
				DeleteFolder1
			}, this.DeleteFolderOperationCompleted, userState);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001DFDC File Offset: 0x0001C1DC
		private void OnDeleteFolderOperationCompleted(object arg)
		{
			if (this.DeleteFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteFolderCompleted(this, new DeleteFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001E024 File Offset: 0x0001C224
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/EmptyFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("EmptyFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public EmptyFolderResponseType EmptyFolder([XmlElement("EmptyFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] EmptyFolderType EmptyFolder1)
		{
			object[] array = this.Invoke("EmptyFolder", new object[]
			{
				EmptyFolder1
			});
			return (EmptyFolderResponseType)array[0];
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001E054 File Offset: 0x0001C254
		public IAsyncResult BeginEmptyFolder(EmptyFolderType EmptyFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EmptyFolder", new object[]
			{
				EmptyFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001E07C File Offset: 0x0001C27C
		public EmptyFolderResponseType EndEmptyFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (EmptyFolderResponseType)array[0];
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001E099 File Offset: 0x0001C299
		public void EmptyFolderAsync(EmptyFolderType EmptyFolder1)
		{
			this.EmptyFolderAsync(EmptyFolder1, null);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001E0A4 File Offset: 0x0001C2A4
		public void EmptyFolderAsync(EmptyFolderType EmptyFolder1, object userState)
		{
			if (this.EmptyFolderOperationCompleted == null)
			{
				this.EmptyFolderOperationCompleted = new SendOrPostCallback(this.OnEmptyFolderOperationCompleted);
			}
			base.InvokeAsync("EmptyFolder", new object[]
			{
				EmptyFolder1
			}, this.EmptyFolderOperationCompleted, userState);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		private void OnEmptyFolderOperationCompleted(object arg)
		{
			if (this.EmptyFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EmptyFolderCompleted(this, new EmptyFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001E134 File Offset: 0x0001C334
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("UpdateFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateFolderResponseType UpdateFolder([XmlElement("UpdateFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateFolderType UpdateFolder1)
		{
			object[] array = this.Invoke("UpdateFolder", new object[]
			{
				UpdateFolder1
			});
			return (UpdateFolderResponseType)array[0];
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001E164 File Offset: 0x0001C364
		public IAsyncResult BeginUpdateFolder(UpdateFolderType UpdateFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateFolder", new object[]
			{
				UpdateFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001E18C File Offset: 0x0001C38C
		public UpdateFolderResponseType EndUpdateFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateFolderResponseType)array[0];
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001E1A9 File Offset: 0x0001C3A9
		public void UpdateFolderAsync(UpdateFolderType UpdateFolder1)
		{
			this.UpdateFolderAsync(UpdateFolder1, null);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001E1B4 File Offset: 0x0001C3B4
		public void UpdateFolderAsync(UpdateFolderType UpdateFolder1, object userState)
		{
			if (this.UpdateFolderOperationCompleted == null)
			{
				this.UpdateFolderOperationCompleted = new SendOrPostCallback(this.OnUpdateFolderOperationCompleted);
			}
			base.InvokeAsync("UpdateFolder", new object[]
			{
				UpdateFolder1
			}, this.UpdateFolderOperationCompleted, userState);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001E1FC File Offset: 0x0001C3FC
		private void OnUpdateFolderOperationCompleted(object arg)
		{
			if (this.UpdateFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateFolderCompleted(this, new UpdateFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001E244 File Offset: 0x0001C444
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MoveFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("MoveFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public MoveFolderResponseType MoveFolder([XmlElement("MoveFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MoveFolderType MoveFolder1)
		{
			object[] array = this.Invoke("MoveFolder", new object[]
			{
				MoveFolder1
			});
			return (MoveFolderResponseType)array[0];
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001E274 File Offset: 0x0001C474
		public IAsyncResult BeginMoveFolder(MoveFolderType MoveFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MoveFolder", new object[]
			{
				MoveFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001E29C File Offset: 0x0001C49C
		public MoveFolderResponseType EndMoveFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MoveFolderResponseType)array[0];
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001E2B9 File Offset: 0x0001C4B9
		public void MoveFolderAsync(MoveFolderType MoveFolder1)
		{
			this.MoveFolderAsync(MoveFolder1, null);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001E2C4 File Offset: 0x0001C4C4
		public void MoveFolderAsync(MoveFolderType MoveFolder1, object userState)
		{
			if (this.MoveFolderOperationCompleted == null)
			{
				this.MoveFolderOperationCompleted = new SendOrPostCallback(this.OnMoveFolderOperationCompleted);
			}
			base.InvokeAsync("MoveFolder", new object[]
			{
				MoveFolder1
			}, this.MoveFolderOperationCompleted, userState);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001E30C File Offset: 0x0001C50C
		private void OnMoveFolderOperationCompleted(object arg)
		{
			if (this.MoveFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MoveFolderCompleted(this, new MoveFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001E354 File Offset: 0x0001C554
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CopyFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("CopyFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CopyFolderResponseType CopyFolder([XmlElement("CopyFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CopyFolderType CopyFolder1)
		{
			object[] array = this.Invoke("CopyFolder", new object[]
			{
				CopyFolder1
			});
			return (CopyFolderResponseType)array[0];
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001E384 File Offset: 0x0001C584
		public IAsyncResult BeginCopyFolder(CopyFolderType CopyFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CopyFolder", new object[]
			{
				CopyFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		public CopyFolderResponseType EndCopyFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CopyFolderResponseType)array[0];
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001E3C9 File Offset: 0x0001C5C9
		public void CopyFolderAsync(CopyFolderType CopyFolder1)
		{
			this.CopyFolderAsync(CopyFolder1, null);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001E3D4 File Offset: 0x0001C5D4
		public void CopyFolderAsync(CopyFolderType CopyFolder1, object userState)
		{
			if (this.CopyFolderOperationCompleted == null)
			{
				this.CopyFolderOperationCompleted = new SendOrPostCallback(this.OnCopyFolderOperationCompleted);
			}
			base.InvokeAsync("CopyFolder", new object[]
			{
				CopyFolder1
			}, this.CopyFolderOperationCompleted, userState);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001E41C File Offset: 0x0001C61C
		private void OnCopyFolderOperationCompleted(object arg)
		{
			if (this.CopyFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CopyFolderCompleted(this, new CopyFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0001E464 File Offset: 0x0001C664
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/Subscribe", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("SubscribeResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SubscribeResponseType Subscribe([XmlElement("Subscribe", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SubscribeType Subscribe1)
		{
			object[] array = this.Invoke("Subscribe", new object[]
			{
				Subscribe1
			});
			return (SubscribeResponseType)array[0];
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0001E494 File Offset: 0x0001C694
		public IAsyncResult BeginSubscribe(SubscribeType Subscribe1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Subscribe", new object[]
			{
				Subscribe1
			}, callback, asyncState);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001E4BC File Offset: 0x0001C6BC
		public SubscribeResponseType EndSubscribe(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SubscribeResponseType)array[0];
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001E4D9 File Offset: 0x0001C6D9
		public void SubscribeAsync(SubscribeType Subscribe1)
		{
			this.SubscribeAsync(Subscribe1, null);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
		public void SubscribeAsync(SubscribeType Subscribe1, object userState)
		{
			if (this.SubscribeOperationCompleted == null)
			{
				this.SubscribeOperationCompleted = new SendOrPostCallback(this.OnSubscribeOperationCompleted);
			}
			base.InvokeAsync("Subscribe", new object[]
			{
				Subscribe1
			}, this.SubscribeOperationCompleted, userState);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001E52C File Offset: 0x0001C72C
		private void OnSubscribeOperationCompleted(object arg)
		{
			if (this.SubscribeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SubscribeCompleted(this, new SubscribeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001E574 File Offset: 0x0001C774
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/Unsubscribe", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("UnsubscribeResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UnsubscribeResponseType Unsubscribe([XmlElement("Unsubscribe", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UnsubscribeType Unsubscribe1)
		{
			object[] array = this.Invoke("Unsubscribe", new object[]
			{
				Unsubscribe1
			});
			return (UnsubscribeResponseType)array[0];
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001E5A4 File Offset: 0x0001C7A4
		public IAsyncResult BeginUnsubscribe(UnsubscribeType Unsubscribe1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Unsubscribe", new object[]
			{
				Unsubscribe1
			}, callback, asyncState);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001E5CC File Offset: 0x0001C7CC
		public UnsubscribeResponseType EndUnsubscribe(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UnsubscribeResponseType)array[0];
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001E5E9 File Offset: 0x0001C7E9
		public void UnsubscribeAsync(UnsubscribeType Unsubscribe1)
		{
			this.UnsubscribeAsync(Unsubscribe1, null);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001E5F4 File Offset: 0x0001C7F4
		public void UnsubscribeAsync(UnsubscribeType Unsubscribe1, object userState)
		{
			if (this.UnsubscribeOperationCompleted == null)
			{
				this.UnsubscribeOperationCompleted = new SendOrPostCallback(this.OnUnsubscribeOperationCompleted);
			}
			base.InvokeAsync("Unsubscribe", new object[]
			{
				Unsubscribe1
			}, this.UnsubscribeOperationCompleted, userState);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001E63C File Offset: 0x0001C83C
		private void OnUnsubscribeOperationCompleted(object arg)
		{
			if (this.UnsubscribeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UnsubscribeCompleted(this, new UnsubscribeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001E684 File Offset: 0x0001C884
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetEvents", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetEventsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetEventsResponseType GetEvents([XmlElement("GetEvents", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetEventsType GetEvents1)
		{
			object[] array = this.Invoke("GetEvents", new object[]
			{
				GetEvents1
			});
			return (GetEventsResponseType)array[0];
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001E6B4 File Offset: 0x0001C8B4
		public IAsyncResult BeginGetEvents(GetEventsType GetEvents1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetEvents", new object[]
			{
				GetEvents1
			}, callback, asyncState);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001E6DC File Offset: 0x0001C8DC
		public GetEventsResponseType EndGetEvents(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetEventsResponseType)array[0];
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001E6F9 File Offset: 0x0001C8F9
		public void GetEventsAsync(GetEventsType GetEvents1)
		{
			this.GetEventsAsync(GetEvents1, null);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001E704 File Offset: 0x0001C904
		public void GetEventsAsync(GetEventsType GetEvents1, object userState)
		{
			if (this.GetEventsOperationCompleted == null)
			{
				this.GetEventsOperationCompleted = new SendOrPostCallback(this.OnGetEventsOperationCompleted);
			}
			base.InvokeAsync("GetEvents", new object[]
			{
				GetEvents1
			}, this.GetEventsOperationCompleted, userState);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001E74C File Offset: 0x0001C94C
		private void OnGetEventsOperationCompleted(object arg)
		{
			if (this.GetEventsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetEventsCompleted(this, new GetEventsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001E794 File Offset: 0x0001C994
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetEvents", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("GetStreamingEventsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetStreamingEventsResponseType GetStreamingEvents([XmlElement("GetStreamingEvents", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetStreamingEventsType GetStreamingEvents1)
		{
			object[] array = this.Invoke("GetStreamingEvents", new object[]
			{
				GetStreamingEvents1
			});
			return (GetStreamingEventsResponseType)array[0];
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001E7C4 File Offset: 0x0001C9C4
		public IAsyncResult BeginGetStreamingEvents(GetStreamingEventsType GetStreamingEvents1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetStreamingEvents", new object[]
			{
				GetStreamingEvents1
			}, callback, asyncState);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001E7EC File Offset: 0x0001C9EC
		public GetStreamingEventsResponseType EndGetStreamingEvents(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetStreamingEventsResponseType)array[0];
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001E809 File Offset: 0x0001CA09
		public void GetStreamingEventsAsync(GetStreamingEventsType GetStreamingEvents1)
		{
			this.GetStreamingEventsAsync(GetStreamingEvents1, null);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001E814 File Offset: 0x0001CA14
		public void GetStreamingEventsAsync(GetStreamingEventsType GetStreamingEvents1, object userState)
		{
			if (this.GetStreamingEventsOperationCompleted == null)
			{
				this.GetStreamingEventsOperationCompleted = new SendOrPostCallback(this.OnGetStreamingEventsOperationCompleted);
			}
			base.InvokeAsync("GetStreamingEvents", new object[]
			{
				GetStreamingEvents1
			}, this.GetStreamingEventsOperationCompleted, userState);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001E85C File Offset: 0x0001CA5C
		private void OnGetStreamingEventsOperationCompleted(object arg)
		{
			if (this.GetStreamingEventsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetStreamingEventsCompleted(this, new GetStreamingEventsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SyncFolderHierarchy", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("SyncFolderHierarchyResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SyncFolderHierarchyResponseType SyncFolderHierarchy([XmlElement("SyncFolderHierarchy", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SyncFolderHierarchyType SyncFolderHierarchy1)
		{
			object[] array = this.Invoke("SyncFolderHierarchy", new object[]
			{
				SyncFolderHierarchy1
			});
			return (SyncFolderHierarchyResponseType)array[0];
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001E8D4 File Offset: 0x0001CAD4
		public IAsyncResult BeginSyncFolderHierarchy(SyncFolderHierarchyType SyncFolderHierarchy1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SyncFolderHierarchy", new object[]
			{
				SyncFolderHierarchy1
			}, callback, asyncState);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001E8FC File Offset: 0x0001CAFC
		public SyncFolderHierarchyResponseType EndSyncFolderHierarchy(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SyncFolderHierarchyResponseType)array[0];
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001E919 File Offset: 0x0001CB19
		public void SyncFolderHierarchyAsync(SyncFolderHierarchyType SyncFolderHierarchy1)
		{
			this.SyncFolderHierarchyAsync(SyncFolderHierarchy1, null);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001E924 File Offset: 0x0001CB24
		public void SyncFolderHierarchyAsync(SyncFolderHierarchyType SyncFolderHierarchy1, object userState)
		{
			if (this.SyncFolderHierarchyOperationCompleted == null)
			{
				this.SyncFolderHierarchyOperationCompleted = new SendOrPostCallback(this.OnSyncFolderHierarchyOperationCompleted);
			}
			base.InvokeAsync("SyncFolderHierarchy", new object[]
			{
				SyncFolderHierarchy1
			}, this.SyncFolderHierarchyOperationCompleted, userState);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001E96C File Offset: 0x0001CB6C
		private void OnSyncFolderHierarchyOperationCompleted(object arg)
		{
			if (this.SyncFolderHierarchyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SyncFolderHierarchyCompleted(this, new SyncFolderHierarchyCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001E9B4 File Offset: 0x0001CBB4
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SyncFolderItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("SyncFolderItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SyncFolderItemsResponseType SyncFolderItems([XmlElement("SyncFolderItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SyncFolderItemsType SyncFolderItems1)
		{
			object[] array = this.Invoke("SyncFolderItems", new object[]
			{
				SyncFolderItems1
			});
			return (SyncFolderItemsResponseType)array[0];
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001E9E4 File Offset: 0x0001CBE4
		public IAsyncResult BeginSyncFolderItems(SyncFolderItemsType SyncFolderItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SyncFolderItems", new object[]
			{
				SyncFolderItems1
			}, callback, asyncState);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001EA0C File Offset: 0x0001CC0C
		public SyncFolderItemsResponseType EndSyncFolderItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SyncFolderItemsResponseType)array[0];
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001EA29 File Offset: 0x0001CC29
		public void SyncFolderItemsAsync(SyncFolderItemsType SyncFolderItems1)
		{
			this.SyncFolderItemsAsync(SyncFolderItems1, null);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001EA34 File Offset: 0x0001CC34
		public void SyncFolderItemsAsync(SyncFolderItemsType SyncFolderItems1, object userState)
		{
			if (this.SyncFolderItemsOperationCompleted == null)
			{
				this.SyncFolderItemsOperationCompleted = new SendOrPostCallback(this.OnSyncFolderItemsOperationCompleted);
			}
			base.InvokeAsync("SyncFolderItems", new object[]
			{
				SyncFolderItems1
			}, this.SyncFolderItemsOperationCompleted, userState);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		private void OnSyncFolderItemsOperationCompleted(object arg)
		{
			if (this.SyncFolderItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SyncFolderItemsCompleted(this, new SyncFolderItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001EAC4 File Offset: 0x0001CCC4
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateManagedFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("CreateManagedFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateManagedFolderResponseType CreateManagedFolder([XmlElement("CreateManagedFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateManagedFolderRequestType CreateManagedFolder1)
		{
			object[] array = this.Invoke("CreateManagedFolder", new object[]
			{
				CreateManagedFolder1
			});
			return (CreateManagedFolderResponseType)array[0];
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001EAF4 File Offset: 0x0001CCF4
		public IAsyncResult BeginCreateManagedFolder(CreateManagedFolderRequestType CreateManagedFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateManagedFolder", new object[]
			{
				CreateManagedFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001EB1C File Offset: 0x0001CD1C
		public CreateManagedFolderResponseType EndCreateManagedFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateManagedFolderResponseType)array[0];
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0001EB39 File Offset: 0x0001CD39
		public void CreateManagedFolderAsync(CreateManagedFolderRequestType CreateManagedFolder1)
		{
			this.CreateManagedFolderAsync(CreateManagedFolder1, null);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0001EB44 File Offset: 0x0001CD44
		public void CreateManagedFolderAsync(CreateManagedFolderRequestType CreateManagedFolder1, object userState)
		{
			if (this.CreateManagedFolderOperationCompleted == null)
			{
				this.CreateManagedFolderOperationCompleted = new SendOrPostCallback(this.OnCreateManagedFolderOperationCompleted);
			}
			base.InvokeAsync("CreateManagedFolder", new object[]
			{
				CreateManagedFolder1
			}, this.CreateManagedFolderOperationCompleted, userState);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0001EB8C File Offset: 0x0001CD8C
		private void OnCreateManagedFolderOperationCompleted(object arg)
		{
			if (this.CreateManagedFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateManagedFolderCompleted(this, new CreateManagedFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0001EBD4 File Offset: 0x0001CDD4
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ManagementRole")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("DateTimePrecision")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("GetItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetItemResponseType GetItem([XmlElement("GetItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetItemType GetItem1)
		{
			object[] array = this.Invoke("GetItem", new object[]
			{
				GetItem1
			});
			return (GetItemResponseType)array[0];
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0001EC04 File Offset: 0x0001CE04
		public IAsyncResult BeginGetItem(GetItemType GetItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItem", new object[]
			{
				GetItem1
			}, callback, asyncState);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001EC2C File Offset: 0x0001CE2C
		public GetItemResponseType EndGetItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetItemResponseType)array[0];
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001EC49 File Offset: 0x0001CE49
		public void GetItemAsync(GetItemType GetItem1)
		{
			this.GetItemAsync(GetItem1, null);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001EC54 File Offset: 0x0001CE54
		public void GetItemAsync(GetItemType GetItem1, object userState)
		{
			if (this.GetItemOperationCompleted == null)
			{
				this.GetItemOperationCompleted = new SendOrPostCallback(this.OnGetItemOperationCompleted);
			}
			base.InvokeAsync("GetItem", new object[]
			{
				GetItem1
			}, this.GetItemOperationCompleted, userState);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001EC9C File Offset: 0x0001CE9C
		private void OnGetItemOperationCompleted(object arg)
		{
			if (this.GetItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemCompleted(this, new GetItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001ECE4 File Offset: 0x0001CEE4
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("CreateItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateItemResponseType CreateItem([XmlElement("CreateItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateItemType CreateItem1)
		{
			object[] array = this.Invoke("CreateItem", new object[]
			{
				CreateItem1
			});
			return (CreateItemResponseType)array[0];
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001ED14 File Offset: 0x0001CF14
		public IAsyncResult BeginCreateItem(CreateItemType CreateItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateItem", new object[]
			{
				CreateItem1
			}, callback, asyncState);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001ED3C File Offset: 0x0001CF3C
		public CreateItemResponseType EndCreateItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateItemResponseType)array[0];
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001ED59 File Offset: 0x0001CF59
		public void CreateItemAsync(CreateItemType CreateItem1)
		{
			this.CreateItemAsync(CreateItem1, null);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001ED64 File Offset: 0x0001CF64
		public void CreateItemAsync(CreateItemType CreateItem1, object userState)
		{
			if (this.CreateItemOperationCompleted == null)
			{
				this.CreateItemOperationCompleted = new SendOrPostCallback(this.OnCreateItemOperationCompleted);
			}
			base.InvokeAsync("CreateItem", new object[]
			{
				CreateItem1
			}, this.CreateItemOperationCompleted, userState);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001EDAC File Offset: 0x0001CFAC
		private void OnCreateItemOperationCompleted(object arg)
		{
			if (this.CreateItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateItemCompleted(this, new CreateItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0001EDF4 File Offset: 0x0001CFF4
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("DeleteItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DeleteItemResponseType DeleteItem([XmlElement("DeleteItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteItemType DeleteItem1)
		{
			object[] array = this.Invoke("DeleteItem", new object[]
			{
				DeleteItem1
			});
			return (DeleteItemResponseType)array[0];
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001EE24 File Offset: 0x0001D024
		public IAsyncResult BeginDeleteItem(DeleteItemType DeleteItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteItem", new object[]
			{
				DeleteItem1
			}, callback, asyncState);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001EE4C File Offset: 0x0001D04C
		public DeleteItemResponseType EndDeleteItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DeleteItemResponseType)array[0];
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0001EE69 File Offset: 0x0001D069
		public void DeleteItemAsync(DeleteItemType DeleteItem1)
		{
			this.DeleteItemAsync(DeleteItem1, null);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001EE74 File Offset: 0x0001D074
		public void DeleteItemAsync(DeleteItemType DeleteItem1, object userState)
		{
			if (this.DeleteItemOperationCompleted == null)
			{
				this.DeleteItemOperationCompleted = new SendOrPostCallback(this.OnDeleteItemOperationCompleted);
			}
			base.InvokeAsync("DeleteItem", new object[]
			{
				DeleteItem1
			}, this.DeleteItemOperationCompleted, userState);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0001EEBC File Offset: 0x0001D0BC
		private void OnDeleteItemOperationCompleted(object arg)
		{
			if (this.DeleteItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteItemCompleted(this, new DeleteItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001EF04 File Offset: 0x0001D104
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("TimeZoneContext")]
		[return: XmlElement("UpdateItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateItemResponseType UpdateItem([XmlElement("UpdateItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateItemType UpdateItem1)
		{
			object[] array = this.Invoke("UpdateItem", new object[]
			{
				UpdateItem1
			});
			return (UpdateItemResponseType)array[0];
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001EF34 File Offset: 0x0001D134
		public IAsyncResult BeginUpdateItem(UpdateItemType UpdateItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateItem", new object[]
			{
				UpdateItem1
			}, callback, asyncState);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001EF5C File Offset: 0x0001D15C
		public UpdateItemResponseType EndUpdateItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateItemResponseType)array[0];
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001EF79 File Offset: 0x0001D179
		public void UpdateItemAsync(UpdateItemType UpdateItem1)
		{
			this.UpdateItemAsync(UpdateItem1, null);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001EF84 File Offset: 0x0001D184
		public void UpdateItemAsync(UpdateItemType UpdateItem1, object userState)
		{
			if (this.UpdateItemOperationCompleted == null)
			{
				this.UpdateItemOperationCompleted = new SendOrPostCallback(this.OnUpdateItemOperationCompleted);
			}
			base.InvokeAsync("UpdateItem", new object[]
			{
				UpdateItem1
			}, this.UpdateItemOperationCompleted, userState);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		private void OnUpdateItemOperationCompleted(object arg)
		{
			if (this.UpdateItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateItemCompleted(this, new UpdateItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001F014 File Offset: 0x0001D214
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ManagementRole")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateItemInRecoverableItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("UpdateItemInRecoverableItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateItemInRecoverableItemsResponseType UpdateItemInRecoverableItems([XmlElement("UpdateItemInRecoverableItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateItemInRecoverableItemsType UpdateItemInRecoverableItems1)
		{
			object[] array = this.Invoke("UpdateItemInRecoverableItems", new object[]
			{
				UpdateItemInRecoverableItems1
			});
			return (UpdateItemInRecoverableItemsResponseType)array[0];
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001F044 File Offset: 0x0001D244
		public IAsyncResult BeginUpdateItemInRecoverableItems(UpdateItemInRecoverableItemsType UpdateItemInRecoverableItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateItemInRecoverableItems", new object[]
			{
				UpdateItemInRecoverableItems1
			}, callback, asyncState);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001F06C File Offset: 0x0001D26C
		public UpdateItemInRecoverableItemsResponseType EndUpdateItemInRecoverableItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateItemInRecoverableItemsResponseType)array[0];
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001F089 File Offset: 0x0001D289
		public void UpdateItemInRecoverableItemsAsync(UpdateItemInRecoverableItemsType UpdateItemInRecoverableItems1)
		{
			this.UpdateItemInRecoverableItemsAsync(UpdateItemInRecoverableItems1, null);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0001F094 File Offset: 0x0001D294
		public void UpdateItemInRecoverableItemsAsync(UpdateItemInRecoverableItemsType UpdateItemInRecoverableItems1, object userState)
		{
			if (this.UpdateItemInRecoverableItemsOperationCompleted == null)
			{
				this.UpdateItemInRecoverableItemsOperationCompleted = new SendOrPostCallback(this.OnUpdateItemInRecoverableItemsOperationCompleted);
			}
			base.InvokeAsync("UpdateItemInRecoverableItems", new object[]
			{
				UpdateItemInRecoverableItems1
			}, this.UpdateItemInRecoverableItemsOperationCompleted, userState);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001F0DC File Offset: 0x0001D2DC
		private void OnUpdateItemInRecoverableItemsOperationCompleted(object arg)
		{
			if (this.UpdateItemInRecoverableItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateItemInRecoverableItemsCompleted(this, new UpdateItemInRecoverableItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001F124 File Offset: 0x0001D324
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SendItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("SendItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SendItemResponseType SendItem([XmlElement("SendItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SendItemType SendItem1)
		{
			object[] array = this.Invoke("SendItem", new object[]
			{
				SendItem1
			});
			return (SendItemResponseType)array[0];
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001F154 File Offset: 0x0001D354
		public IAsyncResult BeginSendItem(SendItemType SendItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SendItem", new object[]
			{
				SendItem1
			}, callback, asyncState);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001F17C File Offset: 0x0001D37C
		public SendItemResponseType EndSendItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SendItemResponseType)array[0];
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001F199 File Offset: 0x0001D399
		public void SendItemAsync(SendItemType SendItem1)
		{
			this.SendItemAsync(SendItem1, null);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001F1A4 File Offset: 0x0001D3A4
		public void SendItemAsync(SendItemType SendItem1, object userState)
		{
			if (this.SendItemOperationCompleted == null)
			{
				this.SendItemOperationCompleted = new SendOrPostCallback(this.OnSendItemOperationCompleted);
			}
			base.InvokeAsync("SendItem", new object[]
			{
				SendItem1
			}, this.SendItemOperationCompleted, userState);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001F1EC File Offset: 0x0001D3EC
		private void OnSendItemOperationCompleted(object arg)
		{
			if (this.SendItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SendItemCompleted(this, new SendItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001F234 File Offset: 0x0001D434
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MoveItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("MoveItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public MoveItemResponseType MoveItem([XmlElement("MoveItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MoveItemType MoveItem1)
		{
			object[] array = this.Invoke("MoveItem", new object[]
			{
				MoveItem1
			});
			return (MoveItemResponseType)array[0];
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001F264 File Offset: 0x0001D464
		public IAsyncResult BeginMoveItem(MoveItemType MoveItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MoveItem", new object[]
			{
				MoveItem1
			}, callback, asyncState);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001F28C File Offset: 0x0001D48C
		public MoveItemResponseType EndMoveItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MoveItemResponseType)array[0];
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001F2A9 File Offset: 0x0001D4A9
		public void MoveItemAsync(MoveItemType MoveItem1)
		{
			this.MoveItemAsync(MoveItem1, null);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
		public void MoveItemAsync(MoveItemType MoveItem1, object userState)
		{
			if (this.MoveItemOperationCompleted == null)
			{
				this.MoveItemOperationCompleted = new SendOrPostCallback(this.OnMoveItemOperationCompleted);
			}
			base.InvokeAsync("MoveItem", new object[]
			{
				MoveItem1
			}, this.MoveItemOperationCompleted, userState);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001F2FC File Offset: 0x0001D4FC
		private void OnMoveItemOperationCompleted(object arg)
		{
			if (this.MoveItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MoveItemCompleted(this, new MoveItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001F344 File Offset: 0x0001D544
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CopyItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("CopyItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CopyItemResponseType CopyItem([XmlElement("CopyItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CopyItemType CopyItem1)
		{
			object[] array = this.Invoke("CopyItem", new object[]
			{
				CopyItem1
			});
			return (CopyItemResponseType)array[0];
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001F374 File Offset: 0x0001D574
		public IAsyncResult BeginCopyItem(CopyItemType CopyItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CopyItem", new object[]
			{
				CopyItem1
			}, callback, asyncState);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001F39C File Offset: 0x0001D59C
		public CopyItemResponseType EndCopyItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CopyItemResponseType)array[0];
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001F3B9 File Offset: 0x0001D5B9
		public void CopyItemAsync(CopyItemType CopyItem1)
		{
			this.CopyItemAsync(CopyItem1, null);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001F3C4 File Offset: 0x0001D5C4
		public void CopyItemAsync(CopyItemType CopyItem1, object userState)
		{
			if (this.CopyItemOperationCompleted == null)
			{
				this.CopyItemOperationCompleted = new SendOrPostCallback(this.OnCopyItemOperationCompleted);
			}
			base.InvokeAsync("CopyItem", new object[]
			{
				CopyItem1
			}, this.CopyItemOperationCompleted, userState);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001F40C File Offset: 0x0001D60C
		private void OnCopyItemOperationCompleted(object arg)
		{
			if (this.CopyItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CopyItemCompleted(this, new CopyItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0001F454 File Offset: 0x0001D654
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ArchiveItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("ArchiveItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ArchiveItemResponseType ArchiveItem([XmlElement("ArchiveItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ArchiveItemType ArchiveItem1)
		{
			object[] array = this.Invoke("ArchiveItem", new object[]
			{
				ArchiveItem1
			});
			return (ArchiveItemResponseType)array[0];
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0001F484 File Offset: 0x0001D684
		public IAsyncResult BeginArchiveItem(ArchiveItemType ArchiveItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ArchiveItem", new object[]
			{
				ArchiveItem1
			}, callback, asyncState);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001F4AC File Offset: 0x0001D6AC
		public ArchiveItemResponseType EndArchiveItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ArchiveItemResponseType)array[0];
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0001F4C9 File Offset: 0x0001D6C9
		public void ArchiveItemAsync(ArchiveItemType ArchiveItem1)
		{
			this.ArchiveItemAsync(ArchiveItem1, null);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001F4D4 File Offset: 0x0001D6D4
		public void ArchiveItemAsync(ArchiveItemType ArchiveItem1, object userState)
		{
			if (this.ArchiveItemOperationCompleted == null)
			{
				this.ArchiveItemOperationCompleted = new SendOrPostCallback(this.OnArchiveItemOperationCompleted);
			}
			base.InvokeAsync("ArchiveItem", new object[]
			{
				ArchiveItem1
			}, this.ArchiveItemOperationCompleted, userState);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001F51C File Offset: 0x0001D71C
		private void OnArchiveItemOperationCompleted(object arg)
		{
			if (this.ArchiveItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ArchiveItemCompleted(this, new ArchiveItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001F564 File Offset: 0x0001D764
		[SoapHttpClientTraceExtension]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateAttachment", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("CreateAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateAttachmentResponseType CreateAttachment([XmlElement("CreateAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateAttachmentType CreateAttachment1)
		{
			object[] array = this.Invoke("CreateAttachment", new object[]
			{
				CreateAttachment1
			});
			return (CreateAttachmentResponseType)array[0];
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001F594 File Offset: 0x0001D794
		public IAsyncResult BeginCreateAttachment(CreateAttachmentType CreateAttachment1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateAttachment", new object[]
			{
				CreateAttachment1
			}, callback, asyncState);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0001F5BC File Offset: 0x0001D7BC
		public CreateAttachmentResponseType EndCreateAttachment(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateAttachmentResponseType)array[0];
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001F5D9 File Offset: 0x0001D7D9
		public void CreateAttachmentAsync(CreateAttachmentType CreateAttachment1)
		{
			this.CreateAttachmentAsync(CreateAttachment1, null);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0001F5E4 File Offset: 0x0001D7E4
		public void CreateAttachmentAsync(CreateAttachmentType CreateAttachment1, object userState)
		{
			if (this.CreateAttachmentOperationCompleted == null)
			{
				this.CreateAttachmentOperationCompleted = new SendOrPostCallback(this.OnCreateAttachmentOperationCompleted);
			}
			base.InvokeAsync("CreateAttachment", new object[]
			{
				CreateAttachment1
			}, this.CreateAttachmentOperationCompleted, userState);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0001F62C File Offset: 0x0001D82C
		private void OnCreateAttachmentOperationCompleted(object arg)
		{
			if (this.CreateAttachmentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateAttachmentCompleted(this, new CreateAttachmentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001F674 File Offset: 0x0001D874
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteAttachment", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("DeleteAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DeleteAttachmentResponseType DeleteAttachment([XmlElement("DeleteAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteAttachmentType DeleteAttachment1)
		{
			object[] array = this.Invoke("DeleteAttachment", new object[]
			{
				DeleteAttachment1
			});
			return (DeleteAttachmentResponseType)array[0];
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0001F6A4 File Offset: 0x0001D8A4
		public IAsyncResult BeginDeleteAttachment(DeleteAttachmentType DeleteAttachment1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteAttachment", new object[]
			{
				DeleteAttachment1
			}, callback, asyncState);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0001F6CC File Offset: 0x0001D8CC
		public DeleteAttachmentResponseType EndDeleteAttachment(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DeleteAttachmentResponseType)array[0];
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0001F6E9 File Offset: 0x0001D8E9
		public void DeleteAttachmentAsync(DeleteAttachmentType DeleteAttachment1)
		{
			this.DeleteAttachmentAsync(DeleteAttachment1, null);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		public void DeleteAttachmentAsync(DeleteAttachmentType DeleteAttachment1, object userState)
		{
			if (this.DeleteAttachmentOperationCompleted == null)
			{
				this.DeleteAttachmentOperationCompleted = new SendOrPostCallback(this.OnDeleteAttachmentOperationCompleted);
			}
			base.InvokeAsync("DeleteAttachment", new object[]
			{
				DeleteAttachment1
			}, this.DeleteAttachmentOperationCompleted, userState);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001F73C File Offset: 0x0001D93C
		private void OnDeleteAttachmentOperationCompleted(object arg)
		{
			if (this.DeleteAttachmentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteAttachmentCompleted(this, new DeleteAttachmentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001F784 File Offset: 0x0001D984
		[SoapHeader("MailboxCulture")]
		[SoapHeader("TimeZoneContext")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetAttachment", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetAttachmentResponseType GetAttachment([XmlElement("GetAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetAttachmentType GetAttachment1)
		{
			object[] array = this.Invoke("GetAttachment", new object[]
			{
				GetAttachment1
			});
			return (GetAttachmentResponseType)array[0];
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
		public IAsyncResult BeginGetAttachment(GetAttachmentType GetAttachment1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAttachment", new object[]
			{
				GetAttachment1
			}, callback, asyncState);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001F7DC File Offset: 0x0001D9DC
		public GetAttachmentResponseType EndGetAttachment(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetAttachmentResponseType)array[0];
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001F7F9 File Offset: 0x0001D9F9
		public void GetAttachmentAsync(GetAttachmentType GetAttachment1)
		{
			this.GetAttachmentAsync(GetAttachment1, null);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001F804 File Offset: 0x0001DA04
		public void GetAttachmentAsync(GetAttachmentType GetAttachment1, object userState)
		{
			if (this.GetAttachmentOperationCompleted == null)
			{
				this.GetAttachmentOperationCompleted = new SendOrPostCallback(this.OnGetAttachmentOperationCompleted);
			}
			base.InvokeAsync("GetAttachment", new object[]
			{
				GetAttachment1
			}, this.GetAttachmentOperationCompleted, userState);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001F84C File Offset: 0x0001DA4C
		private void OnGetAttachmentOperationCompleted(object arg)
		{
			if (this.GetAttachmentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAttachmentCompleted(this, new GetAttachmentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001F894 File Offset: 0x0001DA94
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetClientAccessToken", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetClientAccessTokenResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetClientAccessTokenResponseType GetClientAccessToken([XmlElement("GetClientAccessToken", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetClientAccessTokenType GetClientAccessToken1)
		{
			object[] array = this.Invoke("GetClientAccessToken", new object[]
			{
				GetClientAccessToken1
			});
			return (GetClientAccessTokenResponseType)array[0];
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001F8C4 File Offset: 0x0001DAC4
		public IAsyncResult BeginGetClientAccessToken(GetClientAccessTokenType GetClientAccessToken1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetClientAccessToken", new object[]
			{
				GetClientAccessToken1
			}, callback, asyncState);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001F8EC File Offset: 0x0001DAEC
		public GetClientAccessTokenResponseType EndGetClientAccessToken(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetClientAccessTokenResponseType)array[0];
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001F909 File Offset: 0x0001DB09
		public void GetClientAccessTokenAsync(GetClientAccessTokenType GetClientAccessToken1)
		{
			this.GetClientAccessTokenAsync(GetClientAccessToken1, null);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001F914 File Offset: 0x0001DB14
		public void GetClientAccessTokenAsync(GetClientAccessTokenType GetClientAccessToken1, object userState)
		{
			if (this.GetClientAccessTokenOperationCompleted == null)
			{
				this.GetClientAccessTokenOperationCompleted = new SendOrPostCallback(this.OnGetClientAccessTokenOperationCompleted);
			}
			base.InvokeAsync("GetClientAccessToken", new object[]
			{
				GetClientAccessToken1
			}, this.GetClientAccessTokenOperationCompleted, userState);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001F95C File Offset: 0x0001DB5C
		private void OnGetClientAccessTokenOperationCompleted(object arg)
		{
			if (this.GetClientAccessTokenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetClientAccessTokenCompleted(this, new GetClientAccessTokenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001F9A4 File Offset: 0x0001DBA4
		[SoapHttpClientTraceExtension]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetDelegateResponseMessageType GetDelegate([XmlElement("GetDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetDelegateType GetDelegate1)
		{
			object[] array = this.Invoke("GetDelegate", new object[]
			{
				GetDelegate1
			});
			return (GetDelegateResponseMessageType)array[0];
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
		public IAsyncResult BeginGetDelegate(GetDelegateType GetDelegate1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDelegate", new object[]
			{
				GetDelegate1
			}, callback, asyncState);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001F9FC File Offset: 0x0001DBFC
		public GetDelegateResponseMessageType EndGetDelegate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetDelegateResponseMessageType)array[0];
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0001FA19 File Offset: 0x0001DC19
		public void GetDelegateAsync(GetDelegateType GetDelegate1)
		{
			this.GetDelegateAsync(GetDelegate1, null);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001FA24 File Offset: 0x0001DC24
		public void GetDelegateAsync(GetDelegateType GetDelegate1, object userState)
		{
			if (this.GetDelegateOperationCompleted == null)
			{
				this.GetDelegateOperationCompleted = new SendOrPostCallback(this.OnGetDelegateOperationCompleted);
			}
			base.InvokeAsync("GetDelegate", new object[]
			{
				GetDelegate1
			}, this.GetDelegateOperationCompleted, userState);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001FA6C File Offset: 0x0001DC6C
		private void OnGetDelegateOperationCompleted(object arg)
		{
			if (this.GetDelegateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDelegateCompleted(this, new GetDelegateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001FAB4 File Offset: 0x0001DCB4
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("AddDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddDelegateResponseMessageType AddDelegate([XmlElement("AddDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddDelegateType AddDelegate1)
		{
			object[] array = this.Invoke("AddDelegate", new object[]
			{
				AddDelegate1
			});
			return (AddDelegateResponseMessageType)array[0];
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
		public IAsyncResult BeginAddDelegate(AddDelegateType AddDelegate1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddDelegate", new object[]
			{
				AddDelegate1
			}, callback, asyncState);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001FB0C File Offset: 0x0001DD0C
		public AddDelegateResponseMessageType EndAddDelegate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddDelegateResponseMessageType)array[0];
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001FB29 File Offset: 0x0001DD29
		public void AddDelegateAsync(AddDelegateType AddDelegate1)
		{
			this.AddDelegateAsync(AddDelegate1, null);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001FB34 File Offset: 0x0001DD34
		public void AddDelegateAsync(AddDelegateType AddDelegate1, object userState)
		{
			if (this.AddDelegateOperationCompleted == null)
			{
				this.AddDelegateOperationCompleted = new SendOrPostCallback(this.OnAddDelegateOperationCompleted);
			}
			base.InvokeAsync("AddDelegate", new object[]
			{
				AddDelegate1
			}, this.AddDelegateOperationCompleted, userState);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0001FB7C File Offset: 0x0001DD7C
		private void OnAddDelegateOperationCompleted(object arg)
		{
			if (this.AddDelegateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddDelegateCompleted(this, new AddDelegateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0001FBC4 File Offset: 0x0001DDC4
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("RemoveDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RemoveDelegateResponseMessageType RemoveDelegate([XmlElement("RemoveDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveDelegateType RemoveDelegate1)
		{
			object[] array = this.Invoke("RemoveDelegate", new object[]
			{
				RemoveDelegate1
			});
			return (RemoveDelegateResponseMessageType)array[0];
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001FBF4 File Offset: 0x0001DDF4
		public IAsyncResult BeginRemoveDelegate(RemoveDelegateType RemoveDelegate1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveDelegate", new object[]
			{
				RemoveDelegate1
			}, callback, asyncState);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0001FC1C File Offset: 0x0001DE1C
		public RemoveDelegateResponseMessageType EndRemoveDelegate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RemoveDelegateResponseMessageType)array[0];
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001FC39 File Offset: 0x0001DE39
		public void RemoveDelegateAsync(RemoveDelegateType RemoveDelegate1)
		{
			this.RemoveDelegateAsync(RemoveDelegate1, null);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001FC44 File Offset: 0x0001DE44
		public void RemoveDelegateAsync(RemoveDelegateType RemoveDelegate1, object userState)
		{
			if (this.RemoveDelegateOperationCompleted == null)
			{
				this.RemoveDelegateOperationCompleted = new SendOrPostCallback(this.OnRemoveDelegateOperationCompleted);
			}
			base.InvokeAsync("RemoveDelegate", new object[]
			{
				RemoveDelegate1
			}, this.RemoveDelegateOperationCompleted, userState);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0001FC8C File Offset: 0x0001DE8C
		private void OnRemoveDelegateOperationCompleted(object arg)
		{
			if (this.RemoveDelegateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveDelegateCompleted(this, new RemoveDelegateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0001FCD4 File Offset: 0x0001DED4
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("UpdateDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateDelegateResponseMessageType UpdateDelegate([XmlElement("UpdateDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateDelegateType UpdateDelegate1)
		{
			object[] array = this.Invoke("UpdateDelegate", new object[]
			{
				UpdateDelegate1
			});
			return (UpdateDelegateResponseMessageType)array[0];
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001FD04 File Offset: 0x0001DF04
		public IAsyncResult BeginUpdateDelegate(UpdateDelegateType UpdateDelegate1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateDelegate", new object[]
			{
				UpdateDelegate1
			}, callback, asyncState);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		public UpdateDelegateResponseMessageType EndUpdateDelegate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateDelegateResponseMessageType)array[0];
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0001FD49 File Offset: 0x0001DF49
		public void UpdateDelegateAsync(UpdateDelegateType UpdateDelegate1)
		{
			this.UpdateDelegateAsync(UpdateDelegate1, null);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0001FD54 File Offset: 0x0001DF54
		public void UpdateDelegateAsync(UpdateDelegateType UpdateDelegate1, object userState)
		{
			if (this.UpdateDelegateOperationCompleted == null)
			{
				this.UpdateDelegateOperationCompleted = new SendOrPostCallback(this.OnUpdateDelegateOperationCompleted);
			}
			base.InvokeAsync("UpdateDelegate", new object[]
			{
				UpdateDelegate1
			}, this.UpdateDelegateOperationCompleted, userState);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		private void OnUpdateDelegateOperationCompleted(object arg)
		{
			if (this.UpdateDelegateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateDelegateCompleted(this, new UpdateDelegateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0001FDE4 File Offset: 0x0001DFE4
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("CreateUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateUserConfigurationResponseType CreateUserConfiguration([XmlElement("CreateUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateUserConfigurationType CreateUserConfiguration1)
		{
			object[] array = this.Invoke("CreateUserConfiguration", new object[]
			{
				CreateUserConfiguration1
			});
			return (CreateUserConfigurationResponseType)array[0];
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001FE14 File Offset: 0x0001E014
		public IAsyncResult BeginCreateUserConfiguration(CreateUserConfigurationType CreateUserConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateUserConfiguration", new object[]
			{
				CreateUserConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0001FE3C File Offset: 0x0001E03C
		public CreateUserConfigurationResponseType EndCreateUserConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateUserConfigurationResponseType)array[0];
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0001FE59 File Offset: 0x0001E059
		public void CreateUserConfigurationAsync(CreateUserConfigurationType CreateUserConfiguration1)
		{
			this.CreateUserConfigurationAsync(CreateUserConfiguration1, null);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001FE64 File Offset: 0x0001E064
		public void CreateUserConfigurationAsync(CreateUserConfigurationType CreateUserConfiguration1, object userState)
		{
			if (this.CreateUserConfigurationOperationCompleted == null)
			{
				this.CreateUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnCreateUserConfigurationOperationCompleted);
			}
			base.InvokeAsync("CreateUserConfiguration", new object[]
			{
				CreateUserConfiguration1
			}, this.CreateUserConfigurationOperationCompleted, userState);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0001FEAC File Offset: 0x0001E0AC
		private void OnCreateUserConfigurationOperationCompleted(object arg)
		{
			if (this.CreateUserConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateUserConfigurationCompleted(this, new CreateUserConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001FEF4 File Offset: 0x0001E0F4
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("DeleteUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DeleteUserConfigurationResponseType DeleteUserConfiguration([XmlElement("DeleteUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteUserConfigurationType DeleteUserConfiguration1)
		{
			object[] array = this.Invoke("DeleteUserConfiguration", new object[]
			{
				DeleteUserConfiguration1
			});
			return (DeleteUserConfigurationResponseType)array[0];
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0001FF24 File Offset: 0x0001E124
		public IAsyncResult BeginDeleteUserConfiguration(DeleteUserConfigurationType DeleteUserConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteUserConfiguration", new object[]
			{
				DeleteUserConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001FF4C File Offset: 0x0001E14C
		public DeleteUserConfigurationResponseType EndDeleteUserConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DeleteUserConfigurationResponseType)array[0];
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001FF69 File Offset: 0x0001E169
		public void DeleteUserConfigurationAsync(DeleteUserConfigurationType DeleteUserConfiguration1)
		{
			this.DeleteUserConfigurationAsync(DeleteUserConfiguration1, null);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0001FF74 File Offset: 0x0001E174
		public void DeleteUserConfigurationAsync(DeleteUserConfigurationType DeleteUserConfiguration1, object userState)
		{
			if (this.DeleteUserConfigurationOperationCompleted == null)
			{
				this.DeleteUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnDeleteUserConfigurationOperationCompleted);
			}
			base.InvokeAsync("DeleteUserConfiguration", new object[]
			{
				DeleteUserConfiguration1
			}, this.DeleteUserConfigurationOperationCompleted, userState);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0001FFBC File Offset: 0x0001E1BC
		private void OnDeleteUserConfigurationOperationCompleted(object arg)
		{
			if (this.DeleteUserConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteUserConfigurationCompleted(this, new DeleteUserConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00020004 File Offset: 0x0001E204
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserConfigurationResponseType GetUserConfiguration([XmlElement("GetUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserConfigurationType GetUserConfiguration1)
		{
			object[] array = this.Invoke("GetUserConfiguration", new object[]
			{
				GetUserConfiguration1
			});
			return (GetUserConfigurationResponseType)array[0];
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00020034 File Offset: 0x0001E234
		public IAsyncResult BeginGetUserConfiguration(GetUserConfigurationType GetUserConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserConfiguration", new object[]
			{
				GetUserConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002005C File Offset: 0x0001E25C
		public GetUserConfigurationResponseType EndGetUserConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserConfigurationResponseType)array[0];
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00020079 File Offset: 0x0001E279
		public void GetUserConfigurationAsync(GetUserConfigurationType GetUserConfiguration1)
		{
			this.GetUserConfigurationAsync(GetUserConfiguration1, null);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00020084 File Offset: 0x0001E284
		public void GetUserConfigurationAsync(GetUserConfigurationType GetUserConfiguration1, object userState)
		{
			if (this.GetUserConfigurationOperationCompleted == null)
			{
				this.GetUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnGetUserConfigurationOperationCompleted);
			}
			base.InvokeAsync("GetUserConfiguration", new object[]
			{
				GetUserConfiguration1
			}, this.GetUserConfigurationOperationCompleted, userState);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000200CC File Offset: 0x0001E2CC
		private void OnGetUserConfigurationOperationCompleted(object arg)
		{
			if (this.GetUserConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserConfigurationCompleted(this, new GetUserConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00020114 File Offset: 0x0001E314
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("UpdateUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateUserConfigurationResponseType UpdateUserConfiguration([XmlElement("UpdateUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateUserConfigurationType UpdateUserConfiguration1)
		{
			object[] array = this.Invoke("UpdateUserConfiguration", new object[]
			{
				UpdateUserConfiguration1
			});
			return (UpdateUserConfigurationResponseType)array[0];
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00020144 File Offset: 0x0001E344
		public IAsyncResult BeginUpdateUserConfiguration(UpdateUserConfigurationType UpdateUserConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateUserConfiguration", new object[]
			{
				UpdateUserConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002016C File Offset: 0x0001E36C
		public UpdateUserConfigurationResponseType EndUpdateUserConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateUserConfigurationResponseType)array[0];
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00020189 File Offset: 0x0001E389
		public void UpdateUserConfigurationAsync(UpdateUserConfigurationType UpdateUserConfiguration1)
		{
			this.UpdateUserConfigurationAsync(UpdateUserConfiguration1, null);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00020194 File Offset: 0x0001E394
		public void UpdateUserConfigurationAsync(UpdateUserConfigurationType UpdateUserConfiguration1, object userState)
		{
			if (this.UpdateUserConfigurationOperationCompleted == null)
			{
				this.UpdateUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnUpdateUserConfigurationOperationCompleted);
			}
			base.InvokeAsync("UpdateUserConfiguration", new object[]
			{
				UpdateUserConfiguration1
			}, this.UpdateUserConfigurationOperationCompleted, userState);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000201DC File Offset: 0x0001E3DC
		private void OnUpdateUserConfigurationOperationCompleted(object arg)
		{
			if (this.UpdateUserConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateUserConfigurationCompleted(this, new UpdateUserConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00020224 File Offset: 0x0001E424
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserAvailability", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetUserAvailabilityResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserAvailabilityResponseType GetUserAvailability([XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserAvailabilityRequestType GetUserAvailabilityRequest)
		{
			object[] array = this.Invoke("GetUserAvailability", new object[]
			{
				GetUserAvailabilityRequest
			});
			return (GetUserAvailabilityResponseType)array[0];
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00020254 File Offset: 0x0001E454
		public IAsyncResult BeginGetUserAvailability(GetUserAvailabilityRequestType GetUserAvailabilityRequest, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserAvailability", new object[]
			{
				GetUserAvailabilityRequest
			}, callback, asyncState);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002027C File Offset: 0x0001E47C
		public GetUserAvailabilityResponseType EndGetUserAvailability(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserAvailabilityResponseType)array[0];
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00020299 File Offset: 0x0001E499
		public void GetUserAvailabilityAsync(GetUserAvailabilityRequestType GetUserAvailabilityRequest)
		{
			this.GetUserAvailabilityAsync(GetUserAvailabilityRequest, null);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000202A4 File Offset: 0x0001E4A4
		public void GetUserAvailabilityAsync(GetUserAvailabilityRequestType GetUserAvailabilityRequest, object userState)
		{
			if (this.GetUserAvailabilityOperationCompleted == null)
			{
				this.GetUserAvailabilityOperationCompleted = new SendOrPostCallback(this.OnGetUserAvailabilityOperationCompleted);
			}
			base.InvokeAsync("GetUserAvailability", new object[]
			{
				GetUserAvailabilityRequest
			}, this.GetUserAvailabilityOperationCompleted, userState);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000202EC File Offset: 0x0001E4EC
		private void OnGetUserAvailabilityOperationCompleted(object arg)
		{
			if (this.GetUserAvailabilityCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserAvailabilityCompleted(this, new GetUserAvailabilityCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00020334 File Offset: 0x0001E534
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserOofSettings", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetUserOofSettingsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserOofSettingsResponse GetUserOofSettings([XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserOofSettingsRequest GetUserOofSettingsRequest)
		{
			object[] array = this.Invoke("GetUserOofSettings", new object[]
			{
				GetUserOofSettingsRequest
			});
			return (GetUserOofSettingsResponse)array[0];
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00020364 File Offset: 0x0001E564
		public IAsyncResult BeginGetUserOofSettings(GetUserOofSettingsRequest GetUserOofSettingsRequest, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserOofSettings", new object[]
			{
				GetUserOofSettingsRequest
			}, callback, asyncState);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002038C File Offset: 0x0001E58C
		public GetUserOofSettingsResponse EndGetUserOofSettings(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserOofSettingsResponse)array[0];
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000203A9 File Offset: 0x0001E5A9
		public void GetUserOofSettingsAsync(GetUserOofSettingsRequest GetUserOofSettingsRequest)
		{
			this.GetUserOofSettingsAsync(GetUserOofSettingsRequest, null);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000203B4 File Offset: 0x0001E5B4
		public void GetUserOofSettingsAsync(GetUserOofSettingsRequest GetUserOofSettingsRequest, object userState)
		{
			if (this.GetUserOofSettingsOperationCompleted == null)
			{
				this.GetUserOofSettingsOperationCompleted = new SendOrPostCallback(this.OnGetUserOofSettingsOperationCompleted);
			}
			base.InvokeAsync("GetUserOofSettings", new object[]
			{
				GetUserOofSettingsRequest
			}, this.GetUserOofSettingsOperationCompleted, userState);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000203FC File Offset: 0x0001E5FC
		private void OnGetUserOofSettingsOperationCompleted(object arg)
		{
			if (this.GetUserOofSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserOofSettingsCompleted(this, new GetUserOofSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00020444 File Offset: 0x0001E644
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetUserOofSettings", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("SetUserOofSettingsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SetUserOofSettingsResponse SetUserOofSettings([XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetUserOofSettingsRequest SetUserOofSettingsRequest)
		{
			object[] array = this.Invoke("SetUserOofSettings", new object[]
			{
				SetUserOofSettingsRequest
			});
			return (SetUserOofSettingsResponse)array[0];
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00020474 File Offset: 0x0001E674
		public IAsyncResult BeginSetUserOofSettings(SetUserOofSettingsRequest SetUserOofSettingsRequest, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetUserOofSettings", new object[]
			{
				SetUserOofSettingsRequest
			}, callback, asyncState);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002049C File Offset: 0x0001E69C
		public SetUserOofSettingsResponse EndSetUserOofSettings(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SetUserOofSettingsResponse)array[0];
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000204B9 File Offset: 0x0001E6B9
		public void SetUserOofSettingsAsync(SetUserOofSettingsRequest SetUserOofSettingsRequest)
		{
			this.SetUserOofSettingsAsync(SetUserOofSettingsRequest, null);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000204C4 File Offset: 0x0001E6C4
		public void SetUserOofSettingsAsync(SetUserOofSettingsRequest SetUserOofSettingsRequest, object userState)
		{
			if (this.SetUserOofSettingsOperationCompleted == null)
			{
				this.SetUserOofSettingsOperationCompleted = new SendOrPostCallback(this.OnSetUserOofSettingsOperationCompleted);
			}
			base.InvokeAsync("SetUserOofSettings", new object[]
			{
				SetUserOofSettingsRequest
			}, this.SetUserOofSettingsOperationCompleted, userState);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002050C File Offset: 0x0001E70C
		private void OnSetUserOofSettingsOperationCompleted(object arg)
		{
			if (this.SetUserOofSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetUserOofSettingsCompleted(this, new SetUserOofSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00020554 File Offset: 0x0001E754
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetServiceConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetServiceConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetServiceConfigurationResponseMessageType GetServiceConfiguration([XmlElement("GetServiceConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetServiceConfigurationType GetServiceConfiguration1)
		{
			object[] array = this.Invoke("GetServiceConfiguration", new object[]
			{
				GetServiceConfiguration1
			});
			return (GetServiceConfigurationResponseMessageType)array[0];
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00020584 File Offset: 0x0001E784
		public IAsyncResult BeginGetServiceConfiguration(GetServiceConfigurationType GetServiceConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetServiceConfiguration", new object[]
			{
				GetServiceConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000205AC File Offset: 0x0001E7AC
		public GetServiceConfigurationResponseMessageType EndGetServiceConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetServiceConfigurationResponseMessageType)array[0];
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000205C9 File Offset: 0x0001E7C9
		public void GetServiceConfigurationAsync(GetServiceConfigurationType GetServiceConfiguration1)
		{
			this.GetServiceConfigurationAsync(GetServiceConfiguration1, null);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000205D4 File Offset: 0x0001E7D4
		public void GetServiceConfigurationAsync(GetServiceConfigurationType GetServiceConfiguration1, object userState)
		{
			if (this.GetServiceConfigurationOperationCompleted == null)
			{
				this.GetServiceConfigurationOperationCompleted = new SendOrPostCallback(this.OnGetServiceConfigurationOperationCompleted);
			}
			base.InvokeAsync("GetServiceConfiguration", new object[]
			{
				GetServiceConfiguration1
			}, this.GetServiceConfigurationOperationCompleted, userState);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002061C File Offset: 0x0001E81C
		private void OnGetServiceConfigurationOperationCompleted(object arg)
		{
			if (this.GetServiceConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetServiceConfigurationCompleted(this, new GetServiceConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00020664 File Offset: 0x0001E864
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetMailTips", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("GetMailTipsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetMailTipsResponseMessageType GetMailTips([XmlElement("GetMailTips", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetMailTipsType GetMailTips1)
		{
			object[] array = this.Invoke("GetMailTips", new object[]
			{
				GetMailTips1
			});
			return (GetMailTipsResponseMessageType)array[0];
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00020694 File Offset: 0x0001E894
		public IAsyncResult BeginGetMailTips(GetMailTipsType GetMailTips1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMailTips", new object[]
			{
				GetMailTips1
			}, callback, asyncState);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000206BC File Offset: 0x0001E8BC
		public GetMailTipsResponseMessageType EndGetMailTips(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetMailTipsResponseMessageType)array[0];
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000206D9 File Offset: 0x0001E8D9
		public void GetMailTipsAsync(GetMailTipsType GetMailTips1)
		{
			this.GetMailTipsAsync(GetMailTips1, null);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000206E4 File Offset: 0x0001E8E4
		public void GetMailTipsAsync(GetMailTipsType GetMailTips1, object userState)
		{
			if (this.GetMailTipsOperationCompleted == null)
			{
				this.GetMailTipsOperationCompleted = new SendOrPostCallback(this.OnGetMailTipsOperationCompleted);
			}
			base.InvokeAsync("GetMailTips", new object[]
			{
				GetMailTips1
			}, this.GetMailTipsOperationCompleted, userState);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002072C File Offset: 0x0001E92C
		private void OnGetMailTipsOperationCompleted(object arg)
		{
			if (this.GetMailTipsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMailTipsCompleted(this, new GetMailTipsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00020774 File Offset: 0x0001E974
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/PlayOnPhone", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("PlayOnPhoneResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public PlayOnPhoneResponseMessageType PlayOnPhone([XmlElement("PlayOnPhone", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] PlayOnPhoneType PlayOnPhone1)
		{
			object[] array = this.Invoke("PlayOnPhone", new object[]
			{
				PlayOnPhone1
			});
			return (PlayOnPhoneResponseMessageType)array[0];
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000207A4 File Offset: 0x0001E9A4
		public IAsyncResult BeginPlayOnPhone(PlayOnPhoneType PlayOnPhone1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("PlayOnPhone", new object[]
			{
				PlayOnPhone1
			}, callback, asyncState);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000207CC File Offset: 0x0001E9CC
		public PlayOnPhoneResponseMessageType EndPlayOnPhone(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (PlayOnPhoneResponseMessageType)array[0];
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000207E9 File Offset: 0x0001E9E9
		public void PlayOnPhoneAsync(PlayOnPhoneType PlayOnPhone1)
		{
			this.PlayOnPhoneAsync(PlayOnPhone1, null);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000207F4 File Offset: 0x0001E9F4
		public void PlayOnPhoneAsync(PlayOnPhoneType PlayOnPhone1, object userState)
		{
			if (this.PlayOnPhoneOperationCompleted == null)
			{
				this.PlayOnPhoneOperationCompleted = new SendOrPostCallback(this.OnPlayOnPhoneOperationCompleted);
			}
			base.InvokeAsync("PlayOnPhone", new object[]
			{
				PlayOnPhone1
			}, this.PlayOnPhoneOperationCompleted, userState);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002083C File Offset: 0x0001EA3C
		private void OnPlayOnPhoneOperationCompleted(object arg)
		{
			if (this.PlayOnPhoneCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.PlayOnPhoneCompleted(this, new PlayOnPhoneCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00020884 File Offset: 0x0001EA84
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetPhoneCallInformation", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetPhoneCallInformationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetPhoneCallInformationResponseMessageType GetPhoneCallInformation([XmlElement("GetPhoneCallInformation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetPhoneCallInformationType GetPhoneCallInformation1)
		{
			object[] array = this.Invoke("GetPhoneCallInformation", new object[]
			{
				GetPhoneCallInformation1
			});
			return (GetPhoneCallInformationResponseMessageType)array[0];
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x000208B4 File Offset: 0x0001EAB4
		public IAsyncResult BeginGetPhoneCallInformation(GetPhoneCallInformationType GetPhoneCallInformation1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPhoneCallInformation", new object[]
			{
				GetPhoneCallInformation1
			}, callback, asyncState);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000208DC File Offset: 0x0001EADC
		public GetPhoneCallInformationResponseMessageType EndGetPhoneCallInformation(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetPhoneCallInformationResponseMessageType)array[0];
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000208F9 File Offset: 0x0001EAF9
		public void GetPhoneCallInformationAsync(GetPhoneCallInformationType GetPhoneCallInformation1)
		{
			this.GetPhoneCallInformationAsync(GetPhoneCallInformation1, null);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00020904 File Offset: 0x0001EB04
		public void GetPhoneCallInformationAsync(GetPhoneCallInformationType GetPhoneCallInformation1, object userState)
		{
			if (this.GetPhoneCallInformationOperationCompleted == null)
			{
				this.GetPhoneCallInformationOperationCompleted = new SendOrPostCallback(this.OnGetPhoneCallInformationOperationCompleted);
			}
			base.InvokeAsync("GetPhoneCallInformation", new object[]
			{
				GetPhoneCallInformation1
			}, this.GetPhoneCallInformationOperationCompleted, userState);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002094C File Offset: 0x0001EB4C
		private void OnGetPhoneCallInformationOperationCompleted(object arg)
		{
			if (this.GetPhoneCallInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPhoneCallInformationCompleted(this, new GetPhoneCallInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00020994 File Offset: 0x0001EB94
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DisconnectPhoneCall", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("DisconnectPhoneCallResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DisconnectPhoneCallResponseMessageType DisconnectPhoneCall([XmlElement("DisconnectPhoneCall", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DisconnectPhoneCallType DisconnectPhoneCall1)
		{
			object[] array = this.Invoke("DisconnectPhoneCall", new object[]
			{
				DisconnectPhoneCall1
			});
			return (DisconnectPhoneCallResponseMessageType)array[0];
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000209C4 File Offset: 0x0001EBC4
		public IAsyncResult BeginDisconnectPhoneCall(DisconnectPhoneCallType DisconnectPhoneCall1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DisconnectPhoneCall", new object[]
			{
				DisconnectPhoneCall1
			}, callback, asyncState);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000209EC File Offset: 0x0001EBEC
		public DisconnectPhoneCallResponseMessageType EndDisconnectPhoneCall(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DisconnectPhoneCallResponseMessageType)array[0];
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00020A09 File Offset: 0x0001EC09
		public void DisconnectPhoneCallAsync(DisconnectPhoneCallType DisconnectPhoneCall1)
		{
			this.DisconnectPhoneCallAsync(DisconnectPhoneCall1, null);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00020A14 File Offset: 0x0001EC14
		public void DisconnectPhoneCallAsync(DisconnectPhoneCallType DisconnectPhoneCall1, object userState)
		{
			if (this.DisconnectPhoneCallOperationCompleted == null)
			{
				this.DisconnectPhoneCallOperationCompleted = new SendOrPostCallback(this.OnDisconnectPhoneCallOperationCompleted);
			}
			base.InvokeAsync("DisconnectPhoneCall", new object[]
			{
				DisconnectPhoneCall1
			}, this.DisconnectPhoneCallOperationCompleted, userState);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00020A5C File Offset: 0x0001EC5C
		private void OnDisconnectPhoneCallOperationCompleted(object arg)
		{
			if (this.DisconnectPhoneCallCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DisconnectPhoneCallCompleted(this, new DisconnectPhoneCallCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00020AA4 File Offset: 0x0001ECA4
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetSharingMetadata", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetSharingMetadataResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetSharingMetadataResponseMessageType GetSharingMetadata([XmlElement("GetSharingMetadata", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetSharingMetadataType GetSharingMetadata1)
		{
			object[] array = this.Invoke("GetSharingMetadata", new object[]
			{
				GetSharingMetadata1
			});
			return (GetSharingMetadataResponseMessageType)array[0];
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00020AD4 File Offset: 0x0001ECD4
		public IAsyncResult BeginGetSharingMetadata(GetSharingMetadataType GetSharingMetadata1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSharingMetadata", new object[]
			{
				GetSharingMetadata1
			}, callback, asyncState);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00020AFC File Offset: 0x0001ECFC
		public GetSharingMetadataResponseMessageType EndGetSharingMetadata(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetSharingMetadataResponseMessageType)array[0];
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00020B19 File Offset: 0x0001ED19
		public void GetSharingMetadataAsync(GetSharingMetadataType GetSharingMetadata1)
		{
			this.GetSharingMetadataAsync(GetSharingMetadata1, null);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00020B24 File Offset: 0x0001ED24
		public void GetSharingMetadataAsync(GetSharingMetadataType GetSharingMetadata1, object userState)
		{
			if (this.GetSharingMetadataOperationCompleted == null)
			{
				this.GetSharingMetadataOperationCompleted = new SendOrPostCallback(this.OnGetSharingMetadataOperationCompleted);
			}
			base.InvokeAsync("GetSharingMetadata", new object[]
			{
				GetSharingMetadata1
			}, this.GetSharingMetadataOperationCompleted, userState);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00020B6C File Offset: 0x0001ED6C
		private void OnGetSharingMetadataOperationCompleted(object arg)
		{
			if (this.GetSharingMetadataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSharingMetadataCompleted(this, new GetSharingMetadataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00020BB4 File Offset: 0x0001EDB4
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RefreshSharingFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("RefreshSharingFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RefreshSharingFolderResponseMessageType RefreshSharingFolder([XmlElement("RefreshSharingFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RefreshSharingFolderType RefreshSharingFolder1)
		{
			object[] array = this.Invoke("RefreshSharingFolder", new object[]
			{
				RefreshSharingFolder1
			});
			return (RefreshSharingFolderResponseMessageType)array[0];
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00020BE4 File Offset: 0x0001EDE4
		public IAsyncResult BeginRefreshSharingFolder(RefreshSharingFolderType RefreshSharingFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RefreshSharingFolder", new object[]
			{
				RefreshSharingFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00020C0C File Offset: 0x0001EE0C
		public RefreshSharingFolderResponseMessageType EndRefreshSharingFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RefreshSharingFolderResponseMessageType)array[0];
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00020C29 File Offset: 0x0001EE29
		public void RefreshSharingFolderAsync(RefreshSharingFolderType RefreshSharingFolder1)
		{
			this.RefreshSharingFolderAsync(RefreshSharingFolder1, null);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00020C34 File Offset: 0x0001EE34
		public void RefreshSharingFolderAsync(RefreshSharingFolderType RefreshSharingFolder1, object userState)
		{
			if (this.RefreshSharingFolderOperationCompleted == null)
			{
				this.RefreshSharingFolderOperationCompleted = new SendOrPostCallback(this.OnRefreshSharingFolderOperationCompleted);
			}
			base.InvokeAsync("RefreshSharingFolder", new object[]
			{
				RefreshSharingFolder1
			}, this.RefreshSharingFolderOperationCompleted, userState);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00020C7C File Offset: 0x0001EE7C
		private void OnRefreshSharingFolderOperationCompleted(object arg)
		{
			if (this.RefreshSharingFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RefreshSharingFolderCompleted(this, new RefreshSharingFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00020CC4 File Offset: 0x0001EEC4
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetSharingFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetSharingFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetSharingFolderResponseMessageType GetSharingFolder([XmlElement("GetSharingFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetSharingFolderType GetSharingFolder1)
		{
			object[] array = this.Invoke("GetSharingFolder", new object[]
			{
				GetSharingFolder1
			});
			return (GetSharingFolderResponseMessageType)array[0];
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00020CF4 File Offset: 0x0001EEF4
		public IAsyncResult BeginGetSharingFolder(GetSharingFolderType GetSharingFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSharingFolder", new object[]
			{
				GetSharingFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00020D1C File Offset: 0x0001EF1C
		public GetSharingFolderResponseMessageType EndGetSharingFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetSharingFolderResponseMessageType)array[0];
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00020D39 File Offset: 0x0001EF39
		public void GetSharingFolderAsync(GetSharingFolderType GetSharingFolder1)
		{
			this.GetSharingFolderAsync(GetSharingFolder1, null);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00020D44 File Offset: 0x0001EF44
		public void GetSharingFolderAsync(GetSharingFolderType GetSharingFolder1, object userState)
		{
			if (this.GetSharingFolderOperationCompleted == null)
			{
				this.GetSharingFolderOperationCompleted = new SendOrPostCallback(this.OnGetSharingFolderOperationCompleted);
			}
			base.InvokeAsync("GetSharingFolder", new object[]
			{
				GetSharingFolder1
			}, this.GetSharingFolderOperationCompleted, userState);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00020D8C File Offset: 0x0001EF8C
		private void OnGetSharingFolderOperationCompleted(object arg)
		{
			if (this.GetSharingFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSharingFolderCompleted(this, new GetSharingFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00020DD4 File Offset: 0x0001EFD4
		[SoapHttpClientTraceExtension]
		[SoapHeader("ManagementRole")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetTeamMailbox", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("SetTeamMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SetTeamMailboxResponseMessageType SetTeamMailbox([XmlElement("SetTeamMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetTeamMailboxRequestType SetTeamMailbox1)
		{
			object[] array = this.Invoke("SetTeamMailbox", new object[]
			{
				SetTeamMailbox1
			});
			return (SetTeamMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00020E04 File Offset: 0x0001F004
		public IAsyncResult BeginSetTeamMailbox(SetTeamMailboxRequestType SetTeamMailbox1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetTeamMailbox", new object[]
			{
				SetTeamMailbox1
			}, callback, asyncState);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00020E2C File Offset: 0x0001F02C
		public SetTeamMailboxResponseMessageType EndSetTeamMailbox(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SetTeamMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00020E49 File Offset: 0x0001F049
		public void SetTeamMailboxAsync(SetTeamMailboxRequestType SetTeamMailbox1)
		{
			this.SetTeamMailboxAsync(SetTeamMailbox1, null);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00020E54 File Offset: 0x0001F054
		public void SetTeamMailboxAsync(SetTeamMailboxRequestType SetTeamMailbox1, object userState)
		{
			if (this.SetTeamMailboxOperationCompleted == null)
			{
				this.SetTeamMailboxOperationCompleted = new SendOrPostCallback(this.OnSetTeamMailboxOperationCompleted);
			}
			base.InvokeAsync("SetTeamMailbox", new object[]
			{
				SetTeamMailbox1
			}, this.SetTeamMailboxOperationCompleted, userState);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00020E9C File Offset: 0x0001F09C
		private void OnSetTeamMailboxOperationCompleted(object arg)
		{
			if (this.SetTeamMailboxCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetTeamMailboxCompleted(this, new SetTeamMailboxCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00020EE4 File Offset: 0x0001F0E4
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UnpinTeamMailbox", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("UnpinTeamMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UnpinTeamMailboxResponseMessageType UnpinTeamMailbox([XmlElement("UnpinTeamMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UnpinTeamMailboxRequestType UnpinTeamMailbox1)
		{
			object[] array = this.Invoke("UnpinTeamMailbox", new object[]
			{
				UnpinTeamMailbox1
			});
			return (UnpinTeamMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00020F14 File Offset: 0x0001F114
		public IAsyncResult BeginUnpinTeamMailbox(UnpinTeamMailboxRequestType UnpinTeamMailbox1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UnpinTeamMailbox", new object[]
			{
				UnpinTeamMailbox1
			}, callback, asyncState);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00020F3C File Offset: 0x0001F13C
		public UnpinTeamMailboxResponseMessageType EndUnpinTeamMailbox(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UnpinTeamMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00020F59 File Offset: 0x0001F159
		public void UnpinTeamMailboxAsync(UnpinTeamMailboxRequestType UnpinTeamMailbox1)
		{
			this.UnpinTeamMailboxAsync(UnpinTeamMailbox1, null);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00020F64 File Offset: 0x0001F164
		public void UnpinTeamMailboxAsync(UnpinTeamMailboxRequestType UnpinTeamMailbox1, object userState)
		{
			if (this.UnpinTeamMailboxOperationCompleted == null)
			{
				this.UnpinTeamMailboxOperationCompleted = new SendOrPostCallback(this.OnUnpinTeamMailboxOperationCompleted);
			}
			base.InvokeAsync("UnpinTeamMailbox", new object[]
			{
				UnpinTeamMailbox1
			}, this.UnpinTeamMailboxOperationCompleted, userState);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00020FAC File Offset: 0x0001F1AC
		private void OnUnpinTeamMailboxOperationCompleted(object arg)
		{
			if (this.UnpinTeamMailboxCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UnpinTeamMailboxCompleted(this, new UnpinTeamMailboxCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00020FF4 File Offset: 0x0001F1F4
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetRoomLists", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetRoomListsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetRoomListsResponseMessageType GetRoomLists([XmlElement("GetRoomLists", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetRoomListsType GetRoomLists1)
		{
			object[] array = this.Invoke("GetRoomLists", new object[]
			{
				GetRoomLists1
			});
			return (GetRoomListsResponseMessageType)array[0];
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00021024 File Offset: 0x0001F224
		public IAsyncResult BeginGetRoomLists(GetRoomListsType GetRoomLists1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetRoomLists", new object[]
			{
				GetRoomLists1
			}, callback, asyncState);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002104C File Offset: 0x0001F24C
		public GetRoomListsResponseMessageType EndGetRoomLists(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetRoomListsResponseMessageType)array[0];
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00021069 File Offset: 0x0001F269
		public void GetRoomListsAsync(GetRoomListsType GetRoomLists1)
		{
			this.GetRoomListsAsync(GetRoomLists1, null);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00021074 File Offset: 0x0001F274
		public void GetRoomListsAsync(GetRoomListsType GetRoomLists1, object userState)
		{
			if (this.GetRoomListsOperationCompleted == null)
			{
				this.GetRoomListsOperationCompleted = new SendOrPostCallback(this.OnGetRoomListsOperationCompleted);
			}
			base.InvokeAsync("GetRoomLists", new object[]
			{
				GetRoomLists1
			}, this.GetRoomListsOperationCompleted, userState);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000210BC File Offset: 0x0001F2BC
		private void OnGetRoomListsOperationCompleted(object arg)
		{
			if (this.GetRoomListsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRoomListsCompleted(this, new GetRoomListsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00021104 File Offset: 0x0001F304
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetRooms", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetRoomsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetRoomsResponseMessageType GetRooms([XmlElement("GetRooms", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetRoomsType GetRooms1)
		{
			object[] array = this.Invoke("GetRooms", new object[]
			{
				GetRooms1
			});
			return (GetRoomsResponseMessageType)array[0];
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00021134 File Offset: 0x0001F334
		public IAsyncResult BeginGetRooms(GetRoomsType GetRooms1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetRooms", new object[]
			{
				GetRooms1
			}, callback, asyncState);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002115C File Offset: 0x0001F35C
		public GetRoomsResponseMessageType EndGetRooms(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetRoomsResponseMessageType)array[0];
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00021179 File Offset: 0x0001F379
		public void GetRoomsAsync(GetRoomsType GetRooms1)
		{
			this.GetRoomsAsync(GetRooms1, null);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00021184 File Offset: 0x0001F384
		public void GetRoomsAsync(GetRoomsType GetRooms1, object userState)
		{
			if (this.GetRoomsOperationCompleted == null)
			{
				this.GetRoomsOperationCompleted = new SendOrPostCallback(this.OnGetRoomsOperationCompleted);
			}
			base.InvokeAsync("GetRooms", new object[]
			{
				GetRooms1
			}, this.GetRoomsOperationCompleted, userState);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000211CC File Offset: 0x0001F3CC
		private void OnGetRoomsOperationCompleted(object arg)
		{
			if (this.GetRoomsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRoomsCompleted(this, new GetRoomsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00021214 File Offset: 0x0001F414
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindMessageTrackingReport", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("FindMessageTrackingReportResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindMessageTrackingReportResponseMessageType FindMessageTrackingReport([XmlElement("FindMessageTrackingReport", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindMessageTrackingReportRequestType FindMessageTrackingReport1)
		{
			object[] array = this.Invoke("FindMessageTrackingReport", new object[]
			{
				FindMessageTrackingReport1
			});
			return (FindMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00021244 File Offset: 0x0001F444
		public IAsyncResult BeginFindMessageTrackingReport(FindMessageTrackingReportRequestType FindMessageTrackingReport1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindMessageTrackingReport", new object[]
			{
				FindMessageTrackingReport1
			}, callback, asyncState);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002126C File Offset: 0x0001F46C
		public FindMessageTrackingReportResponseMessageType EndFindMessageTrackingReport(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FindMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00021289 File Offset: 0x0001F489
		public void FindMessageTrackingReportAsync(FindMessageTrackingReportRequestType FindMessageTrackingReport1)
		{
			this.FindMessageTrackingReportAsync(FindMessageTrackingReport1, null);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00021294 File Offset: 0x0001F494
		public void FindMessageTrackingReportAsync(FindMessageTrackingReportRequestType FindMessageTrackingReport1, object userState)
		{
			if (this.FindMessageTrackingReportOperationCompleted == null)
			{
				this.FindMessageTrackingReportOperationCompleted = new SendOrPostCallback(this.OnFindMessageTrackingReportOperationCompleted);
			}
			base.InvokeAsync("FindMessageTrackingReport", new object[]
			{
				FindMessageTrackingReport1
			}, this.FindMessageTrackingReportOperationCompleted, userState);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000212DC File Offset: 0x0001F4DC
		private void OnFindMessageTrackingReportOperationCompleted(object arg)
		{
			if (this.FindMessageTrackingReportCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindMessageTrackingReportCompleted(this, new FindMessageTrackingReportCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00021324 File Offset: 0x0001F524
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetMessageTrackingReport", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetMessageTrackingReportResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetMessageTrackingReportResponseMessageType GetMessageTrackingReport([XmlElement("GetMessageTrackingReport", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetMessageTrackingReportRequestType GetMessageTrackingReport1)
		{
			object[] array = this.Invoke("GetMessageTrackingReport", new object[]
			{
				GetMessageTrackingReport1
			});
			return (GetMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00021354 File Offset: 0x0001F554
		public IAsyncResult BeginGetMessageTrackingReport(GetMessageTrackingReportRequestType GetMessageTrackingReport1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMessageTrackingReport", new object[]
			{
				GetMessageTrackingReport1
			}, callback, asyncState);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002137C File Offset: 0x0001F57C
		public GetMessageTrackingReportResponseMessageType EndGetMessageTrackingReport(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00021399 File Offset: 0x0001F599
		public void GetMessageTrackingReportAsync(GetMessageTrackingReportRequestType GetMessageTrackingReport1)
		{
			this.GetMessageTrackingReportAsync(GetMessageTrackingReport1, null);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000213A4 File Offset: 0x0001F5A4
		public void GetMessageTrackingReportAsync(GetMessageTrackingReportRequestType GetMessageTrackingReport1, object userState)
		{
			if (this.GetMessageTrackingReportOperationCompleted == null)
			{
				this.GetMessageTrackingReportOperationCompleted = new SendOrPostCallback(this.OnGetMessageTrackingReportOperationCompleted);
			}
			base.InvokeAsync("GetMessageTrackingReport", new object[]
			{
				GetMessageTrackingReport1
			}, this.GetMessageTrackingReportOperationCompleted, userState);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000213EC File Offset: 0x0001F5EC
		private void OnGetMessageTrackingReportOperationCompleted(object arg)
		{
			if (this.GetMessageTrackingReportCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMessageTrackingReportCompleted(this, new GetMessageTrackingReportCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00021434 File Offset: 0x0001F634
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindConversation", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("FindConversationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindConversationResponseMessageType FindConversation([XmlElement("FindConversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindConversationType FindConversation1)
		{
			object[] array = this.Invoke("FindConversation", new object[]
			{
				FindConversation1
			});
			return (FindConversationResponseMessageType)array[0];
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00021464 File Offset: 0x0001F664
		public IAsyncResult BeginFindConversation(FindConversationType FindConversation1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindConversation", new object[]
			{
				FindConversation1
			}, callback, asyncState);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002148C File Offset: 0x0001F68C
		public FindConversationResponseMessageType EndFindConversation(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FindConversationResponseMessageType)array[0];
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000214A9 File Offset: 0x0001F6A9
		public void FindConversationAsync(FindConversationType FindConversation1)
		{
			this.FindConversationAsync(FindConversation1, null);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000214B4 File Offset: 0x0001F6B4
		public void FindConversationAsync(FindConversationType FindConversation1, object userState)
		{
			if (this.FindConversationOperationCompleted == null)
			{
				this.FindConversationOperationCompleted = new SendOrPostCallback(this.OnFindConversationOperationCompleted);
			}
			base.InvokeAsync("FindConversation", new object[]
			{
				FindConversation1
			}, this.FindConversationOperationCompleted, userState);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000214FC File Offset: 0x0001F6FC
		private void OnFindConversationOperationCompleted(object arg)
		{
			if (this.FindConversationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindConversationCompleted(this, new FindConversationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00021544 File Offset: 0x0001F744
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ApplyConversationAction", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("ApplyConversationActionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ApplyConversationActionResponseType ApplyConversationAction([XmlElement("ApplyConversationAction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ApplyConversationActionType ApplyConversationAction1)
		{
			object[] array = this.Invoke("ApplyConversationAction", new object[]
			{
				ApplyConversationAction1
			});
			return (ApplyConversationActionResponseType)array[0];
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00021574 File Offset: 0x0001F774
		public IAsyncResult BeginApplyConversationAction(ApplyConversationActionType ApplyConversationAction1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ApplyConversationAction", new object[]
			{
				ApplyConversationAction1
			}, callback, asyncState);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002159C File Offset: 0x0001F79C
		public ApplyConversationActionResponseType EndApplyConversationAction(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ApplyConversationActionResponseType)array[0];
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000215B9 File Offset: 0x0001F7B9
		public void ApplyConversationActionAsync(ApplyConversationActionType ApplyConversationAction1)
		{
			this.ApplyConversationActionAsync(ApplyConversationAction1, null);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000215C4 File Offset: 0x0001F7C4
		public void ApplyConversationActionAsync(ApplyConversationActionType ApplyConversationAction1, object userState)
		{
			if (this.ApplyConversationActionOperationCompleted == null)
			{
				this.ApplyConversationActionOperationCompleted = new SendOrPostCallback(this.OnApplyConversationActionOperationCompleted);
			}
			base.InvokeAsync("ApplyConversationAction", new object[]
			{
				ApplyConversationAction1
			}, this.ApplyConversationActionOperationCompleted, userState);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002160C File Offset: 0x0001F80C
		private void OnApplyConversationActionOperationCompleted(object arg)
		{
			if (this.ApplyConversationActionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ApplyConversationActionCompleted(this, new ApplyConversationActionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00021654 File Offset: 0x0001F854
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetConversationItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetConversationItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetConversationItemsResponseType GetConversationItems([XmlElement("GetConversationItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetConversationItemsType GetConversationItems1)
		{
			object[] array = this.Invoke("GetConversationItems", new object[]
			{
				GetConversationItems1
			});
			return (GetConversationItemsResponseType)array[0];
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00021684 File Offset: 0x0001F884
		public IAsyncResult BeginGetConversationItems(GetConversationItemsType GetConversationItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetConversationItems", new object[]
			{
				GetConversationItems1
			}, callback, asyncState);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000216AC File Offset: 0x0001F8AC
		public GetConversationItemsResponseType EndGetConversationItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetConversationItemsResponseType)array[0];
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000216C9 File Offset: 0x0001F8C9
		public void GetConversationItemsAsync(GetConversationItemsType GetConversationItems1)
		{
			this.GetConversationItemsAsync(GetConversationItems1, null);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000216D4 File Offset: 0x0001F8D4
		public void GetConversationItemsAsync(GetConversationItemsType GetConversationItems1, object userState)
		{
			if (this.GetConversationItemsOperationCompleted == null)
			{
				this.GetConversationItemsOperationCompleted = new SendOrPostCallback(this.OnGetConversationItemsOperationCompleted);
			}
			base.InvokeAsync("GetConversationItems", new object[]
			{
				GetConversationItems1
			}, this.GetConversationItemsOperationCompleted, userState);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002171C File Offset: 0x0001F91C
		private void OnGetConversationItemsOperationCompleted(object arg)
		{
			if (this.GetConversationItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetConversationItemsCompleted(this, new GetConversationItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00021764 File Offset: 0x0001F964
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindPeople", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("FindPeopleResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindPeopleResponseMessageType FindPeople([XmlElement("FindPeople", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindPeopleType FindPeople1)
		{
			object[] array = this.Invoke("FindPeople", new object[]
			{
				FindPeople1
			});
			return (FindPeopleResponseMessageType)array[0];
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00021794 File Offset: 0x0001F994
		public IAsyncResult BeginFindPeople(FindPeopleType FindPeople1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindPeople", new object[]
			{
				FindPeople1
			}, callback, asyncState);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000217BC File Offset: 0x0001F9BC
		public FindPeopleResponseMessageType EndFindPeople(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FindPeopleResponseMessageType)array[0];
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000217D9 File Offset: 0x0001F9D9
		public void FindPeopleAsync(FindPeopleType FindPeople1)
		{
			this.FindPeopleAsync(FindPeople1, null);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000217E4 File Offset: 0x0001F9E4
		public void FindPeopleAsync(FindPeopleType FindPeople1, object userState)
		{
			if (this.FindPeopleOperationCompleted == null)
			{
				this.FindPeopleOperationCompleted = new SendOrPostCallback(this.OnFindPeopleOperationCompleted);
			}
			base.InvokeAsync("FindPeople", new object[]
			{
				FindPeople1
			}, this.FindPeopleOperationCompleted, userState);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002182C File Offset: 0x0001FA2C
		private void OnFindPeopleOperationCompleted(object arg)
		{
			if (this.FindPeopleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindPeopleCompleted(this, new FindPeopleCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00021874 File Offset: 0x0001FA74
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetPersona", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetPersonaResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetPersonaResponseMessageType GetPersona([XmlElement("GetPersona", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetPersonaType GetPersona1)
		{
			object[] array = this.Invoke("GetPersona", new object[]
			{
				GetPersona1
			});
			return (GetPersonaResponseMessageType)array[0];
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000218A4 File Offset: 0x0001FAA4
		public IAsyncResult BeginGetPersona(GetPersonaType GetPersona1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPersona", new object[]
			{
				GetPersona1
			}, callback, asyncState);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000218CC File Offset: 0x0001FACC
		public GetPersonaResponseMessageType EndGetPersona(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetPersonaResponseMessageType)array[0];
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000218E9 File Offset: 0x0001FAE9
		public void GetPersonaAsync(GetPersonaType GetPersona1)
		{
			this.GetPersonaAsync(GetPersona1, null);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000218F4 File Offset: 0x0001FAF4
		public void GetPersonaAsync(GetPersonaType GetPersona1, object userState)
		{
			if (this.GetPersonaOperationCompleted == null)
			{
				this.GetPersonaOperationCompleted = new SendOrPostCallback(this.OnGetPersonaOperationCompleted);
			}
			base.InvokeAsync("GetPersona", new object[]
			{
				GetPersona1
			}, this.GetPersonaOperationCompleted, userState);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002193C File Offset: 0x0001FB3C
		private void OnGetPersonaOperationCompleted(object arg)
		{
			if (this.GetPersonaCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPersonaCompleted(this, new GetPersonaCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00021984 File Offset: 0x0001FB84
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetInboxRules", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("TimeZoneContext")]
		[return: XmlElement("GetInboxRulesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetInboxRulesResponseType GetInboxRules([XmlElement("GetInboxRules", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetInboxRulesRequestType GetInboxRules1)
		{
			object[] array = this.Invoke("GetInboxRules", new object[]
			{
				GetInboxRules1
			});
			return (GetInboxRulesResponseType)array[0];
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x000219B4 File Offset: 0x0001FBB4
		public IAsyncResult BeginGetInboxRules(GetInboxRulesRequestType GetInboxRules1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetInboxRules", new object[]
			{
				GetInboxRules1
			}, callback, asyncState);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000219DC File Offset: 0x0001FBDC
		public GetInboxRulesResponseType EndGetInboxRules(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetInboxRulesResponseType)array[0];
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000219F9 File Offset: 0x0001FBF9
		public void GetInboxRulesAsync(GetInboxRulesRequestType GetInboxRules1)
		{
			this.GetInboxRulesAsync(GetInboxRules1, null);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00021A04 File Offset: 0x0001FC04
		public void GetInboxRulesAsync(GetInboxRulesRequestType GetInboxRules1, object userState)
		{
			if (this.GetInboxRulesOperationCompleted == null)
			{
				this.GetInboxRulesOperationCompleted = new SendOrPostCallback(this.OnGetInboxRulesOperationCompleted);
			}
			base.InvokeAsync("GetInboxRules", new object[]
			{
				GetInboxRules1
			}, this.GetInboxRulesOperationCompleted, userState);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00021A4C File Offset: 0x0001FC4C
		private void OnGetInboxRulesOperationCompleted(object arg)
		{
			if (this.GetInboxRulesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetInboxRulesCompleted(this, new GetInboxRulesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00021A94 File Offset: 0x0001FC94
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateInboxRules", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("UpdateInboxRulesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateInboxRulesResponseType UpdateInboxRules([XmlElement("UpdateInboxRules", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateInboxRulesRequestType UpdateInboxRules1)
		{
			object[] array = this.Invoke("UpdateInboxRules", new object[]
			{
				UpdateInboxRules1
			});
			return (UpdateInboxRulesResponseType)array[0];
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00021AC4 File Offset: 0x0001FCC4
		public IAsyncResult BeginUpdateInboxRules(UpdateInboxRulesRequestType UpdateInboxRules1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateInboxRules", new object[]
			{
				UpdateInboxRules1
			}, callback, asyncState);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00021AEC File Offset: 0x0001FCEC
		public UpdateInboxRulesResponseType EndUpdateInboxRules(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateInboxRulesResponseType)array[0];
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00021B09 File Offset: 0x0001FD09
		public void UpdateInboxRulesAsync(UpdateInboxRulesRequestType UpdateInboxRules1)
		{
			this.UpdateInboxRulesAsync(UpdateInboxRules1, null);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00021B14 File Offset: 0x0001FD14
		public void UpdateInboxRulesAsync(UpdateInboxRulesRequestType UpdateInboxRules1, object userState)
		{
			if (this.UpdateInboxRulesOperationCompleted == null)
			{
				this.UpdateInboxRulesOperationCompleted = new SendOrPostCallback(this.OnUpdateInboxRulesOperationCompleted);
			}
			base.InvokeAsync("UpdateInboxRules", new object[]
			{
				UpdateInboxRules1
			}, this.UpdateInboxRulesOperationCompleted, userState);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00021B5C File Offset: 0x0001FD5C
		private void OnUpdateInboxRulesOperationCompleted(object arg)
		{
			if (this.UpdateInboxRulesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateInboxRulesCompleted(this, new UpdateInboxRulesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00021BA4 File Offset: 0x0001FDA4
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetPasswordExpirationDate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("GetPasswordExpirationDateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetPasswordExpirationDateResponseMessageType GetPasswordExpirationDate([XmlElement("GetPasswordExpirationDate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetPasswordExpirationDateType GetPasswordExpirationDate1)
		{
			object[] array = this.Invoke("GetPasswordExpirationDate", new object[]
			{
				GetPasswordExpirationDate1
			});
			return (GetPasswordExpirationDateResponseMessageType)array[0];
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00021BD4 File Offset: 0x0001FDD4
		public IAsyncResult BeginGetPasswordExpirationDate(GetPasswordExpirationDateType GetPasswordExpirationDate1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPasswordExpirationDate", new object[]
			{
				GetPasswordExpirationDate1
			}, callback, asyncState);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00021BFC File Offset: 0x0001FDFC
		public GetPasswordExpirationDateResponseMessageType EndGetPasswordExpirationDate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetPasswordExpirationDateResponseMessageType)array[0];
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00021C19 File Offset: 0x0001FE19
		public void GetPasswordExpirationDateAsync(GetPasswordExpirationDateType GetPasswordExpirationDate1)
		{
			this.GetPasswordExpirationDateAsync(GetPasswordExpirationDate1, null);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00021C24 File Offset: 0x0001FE24
		public void GetPasswordExpirationDateAsync(GetPasswordExpirationDateType GetPasswordExpirationDate1, object userState)
		{
			if (this.GetPasswordExpirationDateOperationCompleted == null)
			{
				this.GetPasswordExpirationDateOperationCompleted = new SendOrPostCallback(this.OnGetPasswordExpirationDateOperationCompleted);
			}
			base.InvokeAsync("GetPasswordExpirationDate", new object[]
			{
				GetPasswordExpirationDate1
			}, this.GetPasswordExpirationDateOperationCompleted, userState);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00021C6C File Offset: 0x0001FE6C
		private void OnGetPasswordExpirationDateOperationCompleted(object arg)
		{
			if (this.GetPasswordExpirationDateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPasswordExpirationDateCompleted(this, new GetPasswordExpirationDateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00021CB4 File Offset: 0x0001FEB4
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetSearchableMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ManagementRole")]
		[return: XmlElement("GetSearchableMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetSearchableMailboxesResponseMessageType GetSearchableMailboxes([XmlElement("GetSearchableMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetSearchableMailboxesType GetSearchableMailboxes1)
		{
			object[] array = this.Invoke("GetSearchableMailboxes", new object[]
			{
				GetSearchableMailboxes1
			});
			return (GetSearchableMailboxesResponseMessageType)array[0];
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00021CE4 File Offset: 0x0001FEE4
		public IAsyncResult BeginGetSearchableMailboxes(GetSearchableMailboxesType GetSearchableMailboxes1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSearchableMailboxes", new object[]
			{
				GetSearchableMailboxes1
			}, callback, asyncState);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00021D0C File Offset: 0x0001FF0C
		public GetSearchableMailboxesResponseMessageType EndGetSearchableMailboxes(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetSearchableMailboxesResponseMessageType)array[0];
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00021D29 File Offset: 0x0001FF29
		public void GetSearchableMailboxesAsync(GetSearchableMailboxesType GetSearchableMailboxes1)
		{
			this.GetSearchableMailboxesAsync(GetSearchableMailboxes1, null);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00021D34 File Offset: 0x0001FF34
		public void GetSearchableMailboxesAsync(GetSearchableMailboxesType GetSearchableMailboxes1, object userState)
		{
			if (this.GetSearchableMailboxesOperationCompleted == null)
			{
				this.GetSearchableMailboxesOperationCompleted = new SendOrPostCallback(this.OnGetSearchableMailboxesOperationCompleted);
			}
			base.InvokeAsync("GetSearchableMailboxes", new object[]
			{
				GetSearchableMailboxes1
			}, this.GetSearchableMailboxesOperationCompleted, userState);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00021D7C File Offset: 0x0001FF7C
		private void OnGetSearchableMailboxesOperationCompleted(object arg)
		{
			if (this.GetSearchableMailboxesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSearchableMailboxesCompleted(this, new GetSearchableMailboxesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00021DC4 File Offset: 0x0001FFC4
		[SoapHeader("ManagementRole")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SearchMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("SearchMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SearchMailboxesResponseType SearchMailboxes([XmlElement("SearchMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SearchMailboxesType SearchMailboxes1)
		{
			object[] array = this.Invoke("SearchMailboxes", new object[]
			{
				SearchMailboxes1
			});
			return (SearchMailboxesResponseType)array[0];
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00021DF4 File Offset: 0x0001FFF4
		public IAsyncResult BeginSearchMailboxes(SearchMailboxesType SearchMailboxes1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SearchMailboxes", new object[]
			{
				SearchMailboxes1
			}, callback, asyncState);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00021E1C File Offset: 0x0002001C
		public SearchMailboxesResponseType EndSearchMailboxes(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SearchMailboxesResponseType)array[0];
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00021E39 File Offset: 0x00020039
		public void SearchMailboxesAsync(SearchMailboxesType SearchMailboxes1)
		{
			this.SearchMailboxesAsync(SearchMailboxes1, null);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00021E44 File Offset: 0x00020044
		public void SearchMailboxesAsync(SearchMailboxesType SearchMailboxes1, object userState)
		{
			if (this.SearchMailboxesOperationCompleted == null)
			{
				this.SearchMailboxesOperationCompleted = new SendOrPostCallback(this.OnSearchMailboxesOperationCompleted);
			}
			base.InvokeAsync("SearchMailboxes", new object[]
			{
				SearchMailboxes1
			}, this.SearchMailboxesOperationCompleted, userState);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00021E8C File Offset: 0x0002008C
		private void OnSearchMailboxesOperationCompleted(object arg)
		{
			if (this.SearchMailboxesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SearchMailboxesCompleted(this, new SearchMailboxesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00021ED4 File Offset: 0x000200D4
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ManagementRole")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetDiscoverySearchConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetDiscoverySearchConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetDiscoverySearchConfigurationResponseMessageType GetDiscoverySearchConfiguration([XmlElement("GetDiscoverySearchConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetDiscoverySearchConfigurationType GetDiscoverySearchConfiguration1)
		{
			object[] array = this.Invoke("GetDiscoverySearchConfiguration", new object[]
			{
				GetDiscoverySearchConfiguration1
			});
			return (GetDiscoverySearchConfigurationResponseMessageType)array[0];
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00021F04 File Offset: 0x00020104
		public IAsyncResult BeginGetDiscoverySearchConfiguration(GetDiscoverySearchConfigurationType GetDiscoverySearchConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDiscoverySearchConfiguration", new object[]
			{
				GetDiscoverySearchConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00021F2C File Offset: 0x0002012C
		public GetDiscoverySearchConfigurationResponseMessageType EndGetDiscoverySearchConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetDiscoverySearchConfigurationResponseMessageType)array[0];
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00021F49 File Offset: 0x00020149
		public void GetDiscoverySearchConfigurationAsync(GetDiscoverySearchConfigurationType GetDiscoverySearchConfiguration1)
		{
			this.GetDiscoverySearchConfigurationAsync(GetDiscoverySearchConfiguration1, null);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00021F54 File Offset: 0x00020154
		public void GetDiscoverySearchConfigurationAsync(GetDiscoverySearchConfigurationType GetDiscoverySearchConfiguration1, object userState)
		{
			if (this.GetDiscoverySearchConfigurationOperationCompleted == null)
			{
				this.GetDiscoverySearchConfigurationOperationCompleted = new SendOrPostCallback(this.OnGetDiscoverySearchConfigurationOperationCompleted);
			}
			base.InvokeAsync("GetDiscoverySearchConfiguration", new object[]
			{
				GetDiscoverySearchConfiguration1
			}, this.GetDiscoverySearchConfigurationOperationCompleted, userState);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00021F9C File Offset: 0x0002019C
		private void OnGetDiscoverySearchConfigurationOperationCompleted(object arg)
		{
			if (this.GetDiscoverySearchConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDiscoverySearchConfigurationCompleted(this, new GetDiscoverySearchConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00021FE4 File Offset: 0x000201E4
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetHoldOnMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ManagementRole")]
		[return: XmlElement("GetHoldOnMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetHoldOnMailboxesResponseMessageType GetHoldOnMailboxes([XmlElement("GetHoldOnMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetHoldOnMailboxesType GetHoldOnMailboxes1)
		{
			object[] array = this.Invoke("GetHoldOnMailboxes", new object[]
			{
				GetHoldOnMailboxes1
			});
			return (GetHoldOnMailboxesResponseMessageType)array[0];
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00022014 File Offset: 0x00020214
		public IAsyncResult BeginGetHoldOnMailboxes(GetHoldOnMailboxesType GetHoldOnMailboxes1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetHoldOnMailboxes", new object[]
			{
				GetHoldOnMailboxes1
			}, callback, asyncState);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002203C File Offset: 0x0002023C
		public GetHoldOnMailboxesResponseMessageType EndGetHoldOnMailboxes(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetHoldOnMailboxesResponseMessageType)array[0];
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00022059 File Offset: 0x00020259
		public void GetHoldOnMailboxesAsync(GetHoldOnMailboxesType GetHoldOnMailboxes1)
		{
			this.GetHoldOnMailboxesAsync(GetHoldOnMailboxes1, null);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00022064 File Offset: 0x00020264
		public void GetHoldOnMailboxesAsync(GetHoldOnMailboxesType GetHoldOnMailboxes1, object userState)
		{
			if (this.GetHoldOnMailboxesOperationCompleted == null)
			{
				this.GetHoldOnMailboxesOperationCompleted = new SendOrPostCallback(this.OnGetHoldOnMailboxesOperationCompleted);
			}
			base.InvokeAsync("GetHoldOnMailboxes", new object[]
			{
				GetHoldOnMailboxes1
			}, this.GetHoldOnMailboxesOperationCompleted, userState);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000220AC File Offset: 0x000202AC
		private void OnGetHoldOnMailboxesOperationCompleted(object arg)
		{
			if (this.GetHoldOnMailboxesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetHoldOnMailboxesCompleted(this, new GetHoldOnMailboxesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000220F4 File Offset: 0x000202F4
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetHoldOnMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ManagementRole")]
		[return: XmlElement("SetHoldOnMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SetHoldOnMailboxesResponseMessageType SetHoldOnMailboxes([XmlElement("SetHoldOnMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetHoldOnMailboxesType SetHoldOnMailboxes1)
		{
			object[] array = this.Invoke("SetHoldOnMailboxes", new object[]
			{
				SetHoldOnMailboxes1
			});
			return (SetHoldOnMailboxesResponseMessageType)array[0];
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00022124 File Offset: 0x00020324
		public IAsyncResult BeginSetHoldOnMailboxes(SetHoldOnMailboxesType SetHoldOnMailboxes1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetHoldOnMailboxes", new object[]
			{
				SetHoldOnMailboxes1
			}, callback, asyncState);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002214C File Offset: 0x0002034C
		public SetHoldOnMailboxesResponseMessageType EndSetHoldOnMailboxes(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SetHoldOnMailboxesResponseMessageType)array[0];
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00022169 File Offset: 0x00020369
		public void SetHoldOnMailboxesAsync(SetHoldOnMailboxesType SetHoldOnMailboxes1)
		{
			this.SetHoldOnMailboxesAsync(SetHoldOnMailboxes1, null);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00022174 File Offset: 0x00020374
		public void SetHoldOnMailboxesAsync(SetHoldOnMailboxesType SetHoldOnMailboxes1, object userState)
		{
			if (this.SetHoldOnMailboxesOperationCompleted == null)
			{
				this.SetHoldOnMailboxesOperationCompleted = new SendOrPostCallback(this.OnSetHoldOnMailboxesOperationCompleted);
			}
			base.InvokeAsync("SetHoldOnMailboxes", new object[]
			{
				SetHoldOnMailboxes1
			}, this.SetHoldOnMailboxesOperationCompleted, userState);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000221BC File Offset: 0x000203BC
		private void OnSetHoldOnMailboxesOperationCompleted(object arg)
		{
			if (this.SetHoldOnMailboxesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetHoldOnMailboxesCompleted(this, new SetHoldOnMailboxesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00022204 File Offset: 0x00020404
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetNonIndexableItemStatistics", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ManagementRole")]
		[return: XmlElement("GetNonIndexableItemStatisticsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetNonIndexableItemStatisticsResponseMessageType GetNonIndexableItemStatistics([XmlElement("GetNonIndexableItemStatistics", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetNonIndexableItemStatisticsType GetNonIndexableItemStatistics1)
		{
			object[] array = this.Invoke("GetNonIndexableItemStatistics", new object[]
			{
				GetNonIndexableItemStatistics1
			});
			return (GetNonIndexableItemStatisticsResponseMessageType)array[0];
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00022234 File Offset: 0x00020434
		public IAsyncResult BeginGetNonIndexableItemStatistics(GetNonIndexableItemStatisticsType GetNonIndexableItemStatistics1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetNonIndexableItemStatistics", new object[]
			{
				GetNonIndexableItemStatistics1
			}, callback, asyncState);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002225C File Offset: 0x0002045C
		public GetNonIndexableItemStatisticsResponseMessageType EndGetNonIndexableItemStatistics(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetNonIndexableItemStatisticsResponseMessageType)array[0];
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00022279 File Offset: 0x00020479
		public void GetNonIndexableItemStatisticsAsync(GetNonIndexableItemStatisticsType GetNonIndexableItemStatistics1)
		{
			this.GetNonIndexableItemStatisticsAsync(GetNonIndexableItemStatistics1, null);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00022284 File Offset: 0x00020484
		public void GetNonIndexableItemStatisticsAsync(GetNonIndexableItemStatisticsType GetNonIndexableItemStatistics1, object userState)
		{
			if (this.GetNonIndexableItemStatisticsOperationCompleted == null)
			{
				this.GetNonIndexableItemStatisticsOperationCompleted = new SendOrPostCallback(this.OnGetNonIndexableItemStatisticsOperationCompleted);
			}
			base.InvokeAsync("GetNonIndexableItemStatistics", new object[]
			{
				GetNonIndexableItemStatistics1
			}, this.GetNonIndexableItemStatisticsOperationCompleted, userState);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000222CC File Offset: 0x000204CC
		private void OnGetNonIndexableItemStatisticsOperationCompleted(object arg)
		{
			if (this.GetNonIndexableItemStatisticsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetNonIndexableItemStatisticsCompleted(this, new GetNonIndexableItemStatisticsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00022314 File Offset: 0x00020514
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ManagementRole")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetNonIndexableItemDetails", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetNonIndexableItemDetailsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetNonIndexableItemDetailsResponseMessageType GetNonIndexableItemDetails([XmlElement("GetNonIndexableItemDetails", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetNonIndexableItemDetailsType GetNonIndexableItemDetails1)
		{
			object[] array = this.Invoke("GetNonIndexableItemDetails", new object[]
			{
				GetNonIndexableItemDetails1
			});
			return (GetNonIndexableItemDetailsResponseMessageType)array[0];
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00022344 File Offset: 0x00020544
		public IAsyncResult BeginGetNonIndexableItemDetails(GetNonIndexableItemDetailsType GetNonIndexableItemDetails1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetNonIndexableItemDetails", new object[]
			{
				GetNonIndexableItemDetails1
			}, callback, asyncState);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002236C File Offset: 0x0002056C
		public GetNonIndexableItemDetailsResponseMessageType EndGetNonIndexableItemDetails(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetNonIndexableItemDetailsResponseMessageType)array[0];
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00022389 File Offset: 0x00020589
		public void GetNonIndexableItemDetailsAsync(GetNonIndexableItemDetailsType GetNonIndexableItemDetails1)
		{
			this.GetNonIndexableItemDetailsAsync(GetNonIndexableItemDetails1, null);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00022394 File Offset: 0x00020594
		public void GetNonIndexableItemDetailsAsync(GetNonIndexableItemDetailsType GetNonIndexableItemDetails1, object userState)
		{
			if (this.GetNonIndexableItemDetailsOperationCompleted == null)
			{
				this.GetNonIndexableItemDetailsOperationCompleted = new SendOrPostCallback(this.OnGetNonIndexableItemDetailsOperationCompleted);
			}
			base.InvokeAsync("GetNonIndexableItemDetails", new object[]
			{
				GetNonIndexableItemDetails1
			}, this.GetNonIndexableItemDetailsOperationCompleted, userState);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000223DC File Offset: 0x000205DC
		private void OnGetNonIndexableItemDetailsOperationCompleted(object arg)
		{
			if (this.GetNonIndexableItemDetailsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetNonIndexableItemDetailsCompleted(this, new GetNonIndexableItemDetailsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00022424 File Offset: 0x00020624
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MarkAllItemsAsRead", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("MarkAllItemsAsReadResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public MarkAllItemsAsReadResponseType MarkAllItemsAsRead([XmlElement("MarkAllItemsAsRead", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MarkAllItemsAsReadType MarkAllItemsAsRead1)
		{
			object[] array = this.Invoke("MarkAllItemsAsRead", new object[]
			{
				MarkAllItemsAsRead1
			});
			return (MarkAllItemsAsReadResponseType)array[0];
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00022454 File Offset: 0x00020654
		public IAsyncResult BeginMarkAllItemsAsRead(MarkAllItemsAsReadType MarkAllItemsAsRead1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MarkAllItemsAsRead", new object[]
			{
				MarkAllItemsAsRead1
			}, callback, asyncState);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002247C File Offset: 0x0002067C
		public MarkAllItemsAsReadResponseType EndMarkAllItemsAsRead(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MarkAllItemsAsReadResponseType)array[0];
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00022499 File Offset: 0x00020699
		public void MarkAllItemsAsReadAsync(MarkAllItemsAsReadType MarkAllItemsAsRead1)
		{
			this.MarkAllItemsAsReadAsync(MarkAllItemsAsRead1, null);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x000224A4 File Offset: 0x000206A4
		public void MarkAllItemsAsReadAsync(MarkAllItemsAsReadType MarkAllItemsAsRead1, object userState)
		{
			if (this.MarkAllItemsAsReadOperationCompleted == null)
			{
				this.MarkAllItemsAsReadOperationCompleted = new SendOrPostCallback(this.OnMarkAllItemsAsReadOperationCompleted);
			}
			base.InvokeAsync("MarkAllItemsAsRead", new object[]
			{
				MarkAllItemsAsRead1
			}, this.MarkAllItemsAsReadOperationCompleted, userState);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000224EC File Offset: 0x000206EC
		private void OnMarkAllItemsAsReadOperationCompleted(object arg)
		{
			if (this.MarkAllItemsAsReadCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MarkAllItemsAsReadCompleted(this, new MarkAllItemsAsReadCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00022534 File Offset: 0x00020734
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MarkAsJunk", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("MarkAsJunkResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public MarkAsJunkResponseType MarkAsJunk([XmlElement("MarkAsJunk", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MarkAsJunkType MarkAsJunk1)
		{
			object[] array = this.Invoke("MarkAsJunk", new object[]
			{
				MarkAsJunk1
			});
			return (MarkAsJunkResponseType)array[0];
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00022564 File Offset: 0x00020764
		public IAsyncResult BeginMarkAsJunk(MarkAsJunkType MarkAsJunk1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MarkAsJunk", new object[]
			{
				MarkAsJunk1
			}, callback, asyncState);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002258C File Offset: 0x0002078C
		public MarkAsJunkResponseType EndMarkAsJunk(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MarkAsJunkResponseType)array[0];
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x000225A9 File Offset: 0x000207A9
		public void MarkAsJunkAsync(MarkAsJunkType MarkAsJunk1)
		{
			this.MarkAsJunkAsync(MarkAsJunk1, null);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000225B4 File Offset: 0x000207B4
		public void MarkAsJunkAsync(MarkAsJunkType MarkAsJunk1, object userState)
		{
			if (this.MarkAsJunkOperationCompleted == null)
			{
				this.MarkAsJunkOperationCompleted = new SendOrPostCallback(this.OnMarkAsJunkOperationCompleted);
			}
			base.InvokeAsync("MarkAsJunk", new object[]
			{
				MarkAsJunk1
			}, this.MarkAsJunkOperationCompleted, userState);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000225FC File Offset: 0x000207FC
		private void OnMarkAsJunkOperationCompleted(object arg)
		{
			if (this.MarkAsJunkCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MarkAsJunkCompleted(this, new MarkAsJunkCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00022644 File Offset: 0x00020844
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetAppManifests", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetAppManifestsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetAppManifestsResponseType GetAppManifests([XmlElement("GetAppManifests", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetAppManifestsType GetAppManifests1)
		{
			object[] array = this.Invoke("GetAppManifests", new object[]
			{
				GetAppManifests1
			});
			return (GetAppManifestsResponseType)array[0];
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00022674 File Offset: 0x00020874
		public IAsyncResult BeginGetAppManifests(GetAppManifestsType GetAppManifests1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAppManifests", new object[]
			{
				GetAppManifests1
			}, callback, asyncState);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002269C File Offset: 0x0002089C
		public GetAppManifestsResponseType EndGetAppManifests(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetAppManifestsResponseType)array[0];
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000226B9 File Offset: 0x000208B9
		public void GetAppManifestsAsync(GetAppManifestsType GetAppManifests1)
		{
			this.GetAppManifestsAsync(GetAppManifests1, null);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000226C4 File Offset: 0x000208C4
		public void GetAppManifestsAsync(GetAppManifestsType GetAppManifests1, object userState)
		{
			if (this.GetAppManifestsOperationCompleted == null)
			{
				this.GetAppManifestsOperationCompleted = new SendOrPostCallback(this.OnGetAppManifestsOperationCompleted);
			}
			base.InvokeAsync("GetAppManifests", new object[]
			{
				GetAppManifests1
			}, this.GetAppManifestsOperationCompleted, userState);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002270C File Offset: 0x0002090C
		private void OnGetAppManifestsOperationCompleted(object arg)
		{
			if (this.GetAppManifestsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAppManifestsCompleted(this, new GetAppManifestsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00022754 File Offset: 0x00020954
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddNewImContactToGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("AddNewImContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddNewImContactToGroupResponseMessageType AddNewImContactToGroup([XmlElement("AddNewImContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddNewImContactToGroupType AddNewImContactToGroup1)
		{
			object[] array = this.Invoke("AddNewImContactToGroup", new object[]
			{
				AddNewImContactToGroup1
			});
			return (AddNewImContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00022784 File Offset: 0x00020984
		public IAsyncResult BeginAddNewImContactToGroup(AddNewImContactToGroupType AddNewImContactToGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddNewImContactToGroup", new object[]
			{
				AddNewImContactToGroup1
			}, callback, asyncState);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x000227AC File Offset: 0x000209AC
		public AddNewImContactToGroupResponseMessageType EndAddNewImContactToGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddNewImContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000227C9 File Offset: 0x000209C9
		public void AddNewImContactToGroupAsync(AddNewImContactToGroupType AddNewImContactToGroup1)
		{
			this.AddNewImContactToGroupAsync(AddNewImContactToGroup1, null);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000227D4 File Offset: 0x000209D4
		public void AddNewImContactToGroupAsync(AddNewImContactToGroupType AddNewImContactToGroup1, object userState)
		{
			if (this.AddNewImContactToGroupOperationCompleted == null)
			{
				this.AddNewImContactToGroupOperationCompleted = new SendOrPostCallback(this.OnAddNewImContactToGroupOperationCompleted);
			}
			base.InvokeAsync("AddNewImContactToGroup", new object[]
			{
				AddNewImContactToGroup1
			}, this.AddNewImContactToGroupOperationCompleted, userState);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002281C File Offset: 0x00020A1C
		private void OnAddNewImContactToGroupOperationCompleted(object arg)
		{
			if (this.AddNewImContactToGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddNewImContactToGroupCompleted(this, new AddNewImContactToGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00022864 File Offset: 0x00020A64
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddNewTelUriContactToGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("AddNewTelUriContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddNewTelUriContactToGroupResponseMessageType AddNewTelUriContactToGroup([XmlElement("AddNewTelUriContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddNewTelUriContactToGroupType AddNewTelUriContactToGroup1)
		{
			object[] array = this.Invoke("AddNewTelUriContactToGroup", new object[]
			{
				AddNewTelUriContactToGroup1
			});
			return (AddNewTelUriContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00022894 File Offset: 0x00020A94
		public IAsyncResult BeginAddNewTelUriContactToGroup(AddNewTelUriContactToGroupType AddNewTelUriContactToGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddNewTelUriContactToGroup", new object[]
			{
				AddNewTelUriContactToGroup1
			}, callback, asyncState);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x000228BC File Offset: 0x00020ABC
		public AddNewTelUriContactToGroupResponseMessageType EndAddNewTelUriContactToGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddNewTelUriContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000228D9 File Offset: 0x00020AD9
		public void AddNewTelUriContactToGroupAsync(AddNewTelUriContactToGroupType AddNewTelUriContactToGroup1)
		{
			this.AddNewTelUriContactToGroupAsync(AddNewTelUriContactToGroup1, null);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000228E4 File Offset: 0x00020AE4
		public void AddNewTelUriContactToGroupAsync(AddNewTelUriContactToGroupType AddNewTelUriContactToGroup1, object userState)
		{
			if (this.AddNewTelUriContactToGroupOperationCompleted == null)
			{
				this.AddNewTelUriContactToGroupOperationCompleted = new SendOrPostCallback(this.OnAddNewTelUriContactToGroupOperationCompleted);
			}
			base.InvokeAsync("AddNewTelUriContactToGroup", new object[]
			{
				AddNewTelUriContactToGroup1
			}, this.AddNewTelUriContactToGroupOperationCompleted, userState);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002292C File Offset: 0x00020B2C
		private void OnAddNewTelUriContactToGroupOperationCompleted(object arg)
		{
			if (this.AddNewTelUriContactToGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddNewTelUriContactToGroupCompleted(this, new AddNewTelUriContactToGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00022974 File Offset: 0x00020B74
		[SoapHeader("MailboxCulture")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddImContactToGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("AddImContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddImContactToGroupResponseMessageType AddImContactToGroup([XmlElement("AddImContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddImContactToGroupType AddImContactToGroup1)
		{
			object[] array = this.Invoke("AddImContactToGroup", new object[]
			{
				AddImContactToGroup1
			});
			return (AddImContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000229A4 File Offset: 0x00020BA4
		public IAsyncResult BeginAddImContactToGroup(AddImContactToGroupType AddImContactToGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddImContactToGroup", new object[]
			{
				AddImContactToGroup1
			}, callback, asyncState);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000229CC File Offset: 0x00020BCC
		public AddImContactToGroupResponseMessageType EndAddImContactToGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddImContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000229E9 File Offset: 0x00020BE9
		public void AddImContactToGroupAsync(AddImContactToGroupType AddImContactToGroup1)
		{
			this.AddImContactToGroupAsync(AddImContactToGroup1, null);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x000229F4 File Offset: 0x00020BF4
		public void AddImContactToGroupAsync(AddImContactToGroupType AddImContactToGroup1, object userState)
		{
			if (this.AddImContactToGroupOperationCompleted == null)
			{
				this.AddImContactToGroupOperationCompleted = new SendOrPostCallback(this.OnAddImContactToGroupOperationCompleted);
			}
			base.InvokeAsync("AddImContactToGroup", new object[]
			{
				AddImContactToGroup1
			}, this.AddImContactToGroupOperationCompleted, userState);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00022A3C File Offset: 0x00020C3C
		private void OnAddImContactToGroupOperationCompleted(object arg)
		{
			if (this.AddImContactToGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddImContactToGroupCompleted(this, new AddImContactToGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00022A84 File Offset: 0x00020C84
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveImContactFromGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("RemoveImContactFromGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RemoveImContactFromGroupResponseMessageType RemoveImContactFromGroup([XmlElement("RemoveImContactFromGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveImContactFromGroupType RemoveImContactFromGroup1)
		{
			object[] array = this.Invoke("RemoveImContactFromGroup", new object[]
			{
				RemoveImContactFromGroup1
			});
			return (RemoveImContactFromGroupResponseMessageType)array[0];
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00022AB4 File Offset: 0x00020CB4
		public IAsyncResult BeginRemoveImContactFromGroup(RemoveImContactFromGroupType RemoveImContactFromGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveImContactFromGroup", new object[]
			{
				RemoveImContactFromGroup1
			}, callback, asyncState);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00022ADC File Offset: 0x00020CDC
		public RemoveImContactFromGroupResponseMessageType EndRemoveImContactFromGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RemoveImContactFromGroupResponseMessageType)array[0];
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00022AF9 File Offset: 0x00020CF9
		public void RemoveImContactFromGroupAsync(RemoveImContactFromGroupType RemoveImContactFromGroup1)
		{
			this.RemoveImContactFromGroupAsync(RemoveImContactFromGroup1, null);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00022B04 File Offset: 0x00020D04
		public void RemoveImContactFromGroupAsync(RemoveImContactFromGroupType RemoveImContactFromGroup1, object userState)
		{
			if (this.RemoveImContactFromGroupOperationCompleted == null)
			{
				this.RemoveImContactFromGroupOperationCompleted = new SendOrPostCallback(this.OnRemoveImContactFromGroupOperationCompleted);
			}
			base.InvokeAsync("RemoveImContactFromGroup", new object[]
			{
				RemoveImContactFromGroup1
			}, this.RemoveImContactFromGroupOperationCompleted, userState);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00022B4C File Offset: 0x00020D4C
		private void OnRemoveImContactFromGroupOperationCompleted(object arg)
		{
			if (this.RemoveImContactFromGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveImContactFromGroupCompleted(this, new RemoveImContactFromGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00022B94 File Offset: 0x00020D94
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddImGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("AddImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddImGroupResponseMessageType AddImGroup([XmlElement("AddImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddImGroupType AddImGroup1)
		{
			object[] array = this.Invoke("AddImGroup", new object[]
			{
				AddImGroup1
			});
			return (AddImGroupResponseMessageType)array[0];
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00022BC4 File Offset: 0x00020DC4
		public IAsyncResult BeginAddImGroup(AddImGroupType AddImGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddImGroup", new object[]
			{
				AddImGroup1
			}, callback, asyncState);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00022BEC File Offset: 0x00020DEC
		public AddImGroupResponseMessageType EndAddImGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddImGroupResponseMessageType)array[0];
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00022C09 File Offset: 0x00020E09
		public void AddImGroupAsync(AddImGroupType AddImGroup1)
		{
			this.AddImGroupAsync(AddImGroup1, null);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00022C14 File Offset: 0x00020E14
		public void AddImGroupAsync(AddImGroupType AddImGroup1, object userState)
		{
			if (this.AddImGroupOperationCompleted == null)
			{
				this.AddImGroupOperationCompleted = new SendOrPostCallback(this.OnAddImGroupOperationCompleted);
			}
			base.InvokeAsync("AddImGroup", new object[]
			{
				AddImGroup1
			}, this.AddImGroupOperationCompleted, userState);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00022C5C File Offset: 0x00020E5C
		private void OnAddImGroupOperationCompleted(object arg)
		{
			if (this.AddImGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddImGroupCompleted(this, new AddImGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00022CA4 File Offset: 0x00020EA4
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddDistributionGroupToImList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("AddDistributionGroupToImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddDistributionGroupToImListResponseMessageType AddDistributionGroupToImList([XmlElement("AddDistributionGroupToImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddDistributionGroupToImListType AddDistributionGroupToImList1)
		{
			object[] array = this.Invoke("AddDistributionGroupToImList", new object[]
			{
				AddDistributionGroupToImList1
			});
			return (AddDistributionGroupToImListResponseMessageType)array[0];
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00022CD4 File Offset: 0x00020ED4
		public IAsyncResult BeginAddDistributionGroupToImList(AddDistributionGroupToImListType AddDistributionGroupToImList1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddDistributionGroupToImList", new object[]
			{
				AddDistributionGroupToImList1
			}, callback, asyncState);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00022CFC File Offset: 0x00020EFC
		public AddDistributionGroupToImListResponseMessageType EndAddDistributionGroupToImList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddDistributionGroupToImListResponseMessageType)array[0];
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00022D19 File Offset: 0x00020F19
		public void AddDistributionGroupToImListAsync(AddDistributionGroupToImListType AddDistributionGroupToImList1)
		{
			this.AddDistributionGroupToImListAsync(AddDistributionGroupToImList1, null);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00022D24 File Offset: 0x00020F24
		public void AddDistributionGroupToImListAsync(AddDistributionGroupToImListType AddDistributionGroupToImList1, object userState)
		{
			if (this.AddDistributionGroupToImListOperationCompleted == null)
			{
				this.AddDistributionGroupToImListOperationCompleted = new SendOrPostCallback(this.OnAddDistributionGroupToImListOperationCompleted);
			}
			base.InvokeAsync("AddDistributionGroupToImList", new object[]
			{
				AddDistributionGroupToImList1
			}, this.AddDistributionGroupToImListOperationCompleted, userState);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00022D6C File Offset: 0x00020F6C
		private void OnAddDistributionGroupToImListOperationCompleted(object arg)
		{
			if (this.AddDistributionGroupToImListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddDistributionGroupToImListCompleted(this, new AddDistributionGroupToImListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00022DB4 File Offset: 0x00020FB4
		[SoapHeader("RequestServerVersionValue")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetImItemList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetImItemListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetImItemListResponseMessageType GetImItemList([XmlElement("GetImItemList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetImItemListType GetImItemList1)
		{
			object[] array = this.Invoke("GetImItemList", new object[]
			{
				GetImItemList1
			});
			return (GetImItemListResponseMessageType)array[0];
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00022DE4 File Offset: 0x00020FE4
		public IAsyncResult BeginGetImItemList(GetImItemListType GetImItemList1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetImItemList", new object[]
			{
				GetImItemList1
			}, callback, asyncState);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00022E0C File Offset: 0x0002100C
		public GetImItemListResponseMessageType EndGetImItemList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetImItemListResponseMessageType)array[0];
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00022E29 File Offset: 0x00021029
		public void GetImItemListAsync(GetImItemListType GetImItemList1)
		{
			this.GetImItemListAsync(GetImItemList1, null);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00022E34 File Offset: 0x00021034
		public void GetImItemListAsync(GetImItemListType GetImItemList1, object userState)
		{
			if (this.GetImItemListOperationCompleted == null)
			{
				this.GetImItemListOperationCompleted = new SendOrPostCallback(this.OnGetImItemListOperationCompleted);
			}
			base.InvokeAsync("GetImItemList", new object[]
			{
				GetImItemList1
			}, this.GetImItemListOperationCompleted, userState);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00022E7C File Offset: 0x0002107C
		private void OnGetImItemListOperationCompleted(object arg)
		{
			if (this.GetImItemListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetImItemListCompleted(this, new GetImItemListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00022EC4 File Offset: 0x000210C4
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetImItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetImItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetImItemsResponseMessageType GetImItems([XmlElement("GetImItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetImItemsType GetImItems1)
		{
			object[] array = this.Invoke("GetImItems", new object[]
			{
				GetImItems1
			});
			return (GetImItemsResponseMessageType)array[0];
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00022EF4 File Offset: 0x000210F4
		public IAsyncResult BeginGetImItems(GetImItemsType GetImItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetImItems", new object[]
			{
				GetImItems1
			}, callback, asyncState);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00022F1C File Offset: 0x0002111C
		public GetImItemsResponseMessageType EndGetImItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetImItemsResponseMessageType)array[0];
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00022F39 File Offset: 0x00021139
		public void GetImItemsAsync(GetImItemsType GetImItems1)
		{
			this.GetImItemsAsync(GetImItems1, null);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00022F44 File Offset: 0x00021144
		public void GetImItemsAsync(GetImItemsType GetImItems1, object userState)
		{
			if (this.GetImItemsOperationCompleted == null)
			{
				this.GetImItemsOperationCompleted = new SendOrPostCallback(this.OnGetImItemsOperationCompleted);
			}
			base.InvokeAsync("GetImItems", new object[]
			{
				GetImItems1
			}, this.GetImItemsOperationCompleted, userState);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00022F8C File Offset: 0x0002118C
		private void OnGetImItemsOperationCompleted(object arg)
		{
			if (this.GetImItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetImItemsCompleted(this, new GetImItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00022FD4 File Offset: 0x000211D4
		[SoapHeader("MailboxCulture")]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveContactFromImList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("RemoveContactFromImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RemoveContactFromImListResponseMessageType RemoveContactFromImList([XmlElement("RemoveContactFromImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveContactFromImListType RemoveContactFromImList1)
		{
			object[] array = this.Invoke("RemoveContactFromImList", new object[]
			{
				RemoveContactFromImList1
			});
			return (RemoveContactFromImListResponseMessageType)array[0];
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00023004 File Offset: 0x00021204
		public IAsyncResult BeginRemoveContactFromImList(RemoveContactFromImListType RemoveContactFromImList1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveContactFromImList", new object[]
			{
				RemoveContactFromImList1
			}, callback, asyncState);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002302C File Offset: 0x0002122C
		public RemoveContactFromImListResponseMessageType EndRemoveContactFromImList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RemoveContactFromImListResponseMessageType)array[0];
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00023049 File Offset: 0x00021249
		public void RemoveContactFromImListAsync(RemoveContactFromImListType RemoveContactFromImList1)
		{
			this.RemoveContactFromImListAsync(RemoveContactFromImList1, null);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00023054 File Offset: 0x00021254
		public void RemoveContactFromImListAsync(RemoveContactFromImListType RemoveContactFromImList1, object userState)
		{
			if (this.RemoveContactFromImListOperationCompleted == null)
			{
				this.RemoveContactFromImListOperationCompleted = new SendOrPostCallback(this.OnRemoveContactFromImListOperationCompleted);
			}
			base.InvokeAsync("RemoveContactFromImList", new object[]
			{
				RemoveContactFromImList1
			}, this.RemoveContactFromImListOperationCompleted, userState);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002309C File Offset: 0x0002129C
		private void OnRemoveContactFromImListOperationCompleted(object arg)
		{
			if (this.RemoveContactFromImListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveContactFromImListCompleted(this, new RemoveContactFromImListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000230E4 File Offset: 0x000212E4
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveDistributionGroupFromImList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("RemoveDistributionGroupFromImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RemoveDistributionGroupFromImListResponseMessageType RemoveDistributionGroupFromImList([XmlElement("RemoveDistributionGroupFromImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveDistributionGroupFromImListType RemoveDistributionGroupFromImList1)
		{
			object[] array = this.Invoke("RemoveDistributionGroupFromImList", new object[]
			{
				RemoveDistributionGroupFromImList1
			});
			return (RemoveDistributionGroupFromImListResponseMessageType)array[0];
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00023114 File Offset: 0x00021314
		public IAsyncResult BeginRemoveDistributionGroupFromImList(RemoveDistributionGroupFromImListType RemoveDistributionGroupFromImList1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveDistributionGroupFromImList", new object[]
			{
				RemoveDistributionGroupFromImList1
			}, callback, asyncState);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002313C File Offset: 0x0002133C
		public RemoveDistributionGroupFromImListResponseMessageType EndRemoveDistributionGroupFromImList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RemoveDistributionGroupFromImListResponseMessageType)array[0];
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00023159 File Offset: 0x00021359
		public void RemoveDistributionGroupFromImListAsync(RemoveDistributionGroupFromImListType RemoveDistributionGroupFromImList1)
		{
			this.RemoveDistributionGroupFromImListAsync(RemoveDistributionGroupFromImList1, null);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00023164 File Offset: 0x00021364
		public void RemoveDistributionGroupFromImListAsync(RemoveDistributionGroupFromImListType RemoveDistributionGroupFromImList1, object userState)
		{
			if (this.RemoveDistributionGroupFromImListOperationCompleted == null)
			{
				this.RemoveDistributionGroupFromImListOperationCompleted = new SendOrPostCallback(this.OnRemoveDistributionGroupFromImListOperationCompleted);
			}
			base.InvokeAsync("RemoveDistributionGroupFromImList", new object[]
			{
				RemoveDistributionGroupFromImList1
			}, this.RemoveDistributionGroupFromImListOperationCompleted, userState);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000231AC File Offset: 0x000213AC
		private void OnRemoveDistributionGroupFromImListOperationCompleted(object arg)
		{
			if (this.RemoveDistributionGroupFromImListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveDistributionGroupFromImListCompleted(this, new RemoveDistributionGroupFromImListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000231F4 File Offset: 0x000213F4
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveImGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("RemoveImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RemoveImGroupResponseMessageType RemoveImGroup([XmlElement("RemoveImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveImGroupType RemoveImGroup1)
		{
			object[] array = this.Invoke("RemoveImGroup", new object[]
			{
				RemoveImGroup1
			});
			return (RemoveImGroupResponseMessageType)array[0];
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00023224 File Offset: 0x00021424
		public IAsyncResult BeginRemoveImGroup(RemoveImGroupType RemoveImGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveImGroup", new object[]
			{
				RemoveImGroup1
			}, callback, asyncState);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002324C File Offset: 0x0002144C
		public RemoveImGroupResponseMessageType EndRemoveImGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RemoveImGroupResponseMessageType)array[0];
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00023269 File Offset: 0x00021469
		public void RemoveImGroupAsync(RemoveImGroupType RemoveImGroup1)
		{
			this.RemoveImGroupAsync(RemoveImGroup1, null);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00023274 File Offset: 0x00021474
		public void RemoveImGroupAsync(RemoveImGroupType RemoveImGroup1, object userState)
		{
			if (this.RemoveImGroupOperationCompleted == null)
			{
				this.RemoveImGroupOperationCompleted = new SendOrPostCallback(this.OnRemoveImGroupOperationCompleted);
			}
			base.InvokeAsync("RemoveImGroup", new object[]
			{
				RemoveImGroup1
			}, this.RemoveImGroupOperationCompleted, userState);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000232BC File Offset: 0x000214BC
		private void OnRemoveImGroupOperationCompleted(object arg)
		{
			if (this.RemoveImGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveImGroupCompleted(this, new RemoveImGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00023304 File Offset: 0x00021504
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetImGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("SetImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SetImGroupResponseMessageType SetImGroup([XmlElement("SetImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetImGroupType SetImGroup1)
		{
			object[] array = this.Invoke("SetImGroup", new object[]
			{
				SetImGroup1
			});
			return (SetImGroupResponseMessageType)array[0];
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00023334 File Offset: 0x00021534
		public IAsyncResult BeginSetImGroup(SetImGroupType SetImGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetImGroup", new object[]
			{
				SetImGroup1
			}, callback, asyncState);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002335C File Offset: 0x0002155C
		public SetImGroupResponseMessageType EndSetImGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SetImGroupResponseMessageType)array[0];
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00023379 File Offset: 0x00021579
		public void SetImGroupAsync(SetImGroupType SetImGroup1)
		{
			this.SetImGroupAsync(SetImGroup1, null);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00023384 File Offset: 0x00021584
		public void SetImGroupAsync(SetImGroupType SetImGroup1, object userState)
		{
			if (this.SetImGroupOperationCompleted == null)
			{
				this.SetImGroupOperationCompleted = new SendOrPostCallback(this.OnSetImGroupOperationCompleted);
			}
			base.InvokeAsync("SetImGroup", new object[]
			{
				SetImGroup1
			}, this.SetImGroupOperationCompleted, userState);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000233CC File Offset: 0x000215CC
		private void OnSetImGroupOperationCompleted(object arg)
		{
			if (this.SetImGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetImGroupCompleted(this, new SetImGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00023414 File Offset: 0x00021614
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetImListMigrationCompleted", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("SetImListMigrationCompletedResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SetImListMigrationCompletedResponseMessageType SetImListMigrationCompleted([XmlElement("SetImListMigrationCompleted", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetImListMigrationCompletedType SetImListMigrationCompleted1)
		{
			object[] array = this.Invoke("SetImListMigrationCompleted", new object[]
			{
				SetImListMigrationCompleted1
			});
			return (SetImListMigrationCompletedResponseMessageType)array[0];
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00023444 File Offset: 0x00021644
		public IAsyncResult BeginSetImListMigrationCompleted(SetImListMigrationCompletedType SetImListMigrationCompleted1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetImListMigrationCompleted", new object[]
			{
				SetImListMigrationCompleted1
			}, callback, asyncState);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002346C File Offset: 0x0002166C
		public SetImListMigrationCompletedResponseMessageType EndSetImListMigrationCompleted(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SetImListMigrationCompletedResponseMessageType)array[0];
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00023489 File Offset: 0x00021689
		public void SetImListMigrationCompletedAsync(SetImListMigrationCompletedType SetImListMigrationCompleted1)
		{
			this.SetImListMigrationCompletedAsync(SetImListMigrationCompleted1, null);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00023494 File Offset: 0x00021694
		public void SetImListMigrationCompletedAsync(SetImListMigrationCompletedType SetImListMigrationCompleted1, object userState)
		{
			if (this.SetImListMigrationCompletedOperationCompleted == null)
			{
				this.SetImListMigrationCompletedOperationCompleted = new SendOrPostCallback(this.OnSetImListMigrationCompletedOperationCompleted);
			}
			base.InvokeAsync("SetImListMigrationCompleted", new object[]
			{
				SetImListMigrationCompleted1
			}, this.SetImListMigrationCompletedOperationCompleted, userState);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000234DC File Offset: 0x000216DC
		private void OnSetImListMigrationCompletedOperationCompleted(object arg)
		{
			if (this.SetImListMigrationCompletedCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetImListMigrationCompletedCompleted(this, new SetImListMigrationCompletedCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00023524 File Offset: 0x00021724
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserRetentionPolicyTags", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("GetUserRetentionPolicyTagsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserRetentionPolicyTagsResponseMessageType GetUserRetentionPolicyTags([XmlElement("GetUserRetentionPolicyTags", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserRetentionPolicyTagsType GetUserRetentionPolicyTags1)
		{
			object[] array = this.Invoke("GetUserRetentionPolicyTags", new object[]
			{
				GetUserRetentionPolicyTags1
			});
			return (GetUserRetentionPolicyTagsResponseMessageType)array[0];
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00023554 File Offset: 0x00021754
		public IAsyncResult BeginGetUserRetentionPolicyTags(GetUserRetentionPolicyTagsType GetUserRetentionPolicyTags1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserRetentionPolicyTags", new object[]
			{
				GetUserRetentionPolicyTags1
			}, callback, asyncState);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002357C File Offset: 0x0002177C
		public GetUserRetentionPolicyTagsResponseMessageType EndGetUserRetentionPolicyTags(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserRetentionPolicyTagsResponseMessageType)array[0];
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00023599 File Offset: 0x00021799
		public void GetUserRetentionPolicyTagsAsync(GetUserRetentionPolicyTagsType GetUserRetentionPolicyTags1)
		{
			this.GetUserRetentionPolicyTagsAsync(GetUserRetentionPolicyTags1, null);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000235A4 File Offset: 0x000217A4
		public void GetUserRetentionPolicyTagsAsync(GetUserRetentionPolicyTagsType GetUserRetentionPolicyTags1, object userState)
		{
			if (this.GetUserRetentionPolicyTagsOperationCompleted == null)
			{
				this.GetUserRetentionPolicyTagsOperationCompleted = new SendOrPostCallback(this.OnGetUserRetentionPolicyTagsOperationCompleted);
			}
			base.InvokeAsync("GetUserRetentionPolicyTags", new object[]
			{
				GetUserRetentionPolicyTags1
			}, this.GetUserRetentionPolicyTagsOperationCompleted, userState);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000235EC File Offset: 0x000217EC
		private void OnGetUserRetentionPolicyTagsOperationCompleted(object arg)
		{
			if (this.GetUserRetentionPolicyTagsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserRetentionPolicyTagsCompleted(this, new GetUserRetentionPolicyTagsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00023634 File Offset: 0x00021834
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/InstallApp", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("InstallAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public InstallAppResponseType InstallApp([XmlElement("InstallApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] InstallAppType InstallApp1)
		{
			object[] array = this.Invoke("InstallApp", new object[]
			{
				InstallApp1
			});
			return (InstallAppResponseType)array[0];
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00023664 File Offset: 0x00021864
		public IAsyncResult BeginInstallApp(InstallAppType InstallApp1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("InstallApp", new object[]
			{
				InstallApp1
			}, callback, asyncState);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002368C File Offset: 0x0002188C
		public InstallAppResponseType EndInstallApp(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (InstallAppResponseType)array[0];
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000236A9 File Offset: 0x000218A9
		public void InstallAppAsync(InstallAppType InstallApp1)
		{
			this.InstallAppAsync(InstallApp1, null);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000236B4 File Offset: 0x000218B4
		public void InstallAppAsync(InstallAppType InstallApp1, object userState)
		{
			if (this.InstallAppOperationCompleted == null)
			{
				this.InstallAppOperationCompleted = new SendOrPostCallback(this.OnInstallAppOperationCompleted);
			}
			base.InvokeAsync("InstallApp", new object[]
			{
				InstallApp1
			}, this.InstallAppOperationCompleted, userState);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000236FC File Offset: 0x000218FC
		private void OnInstallAppOperationCompleted(object arg)
		{
			if (this.InstallAppCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.InstallAppCompleted(this, new InstallAppCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00023744 File Offset: 0x00021944
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UninstallApp", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("UninstallAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UninstallAppResponseType UninstallApp([XmlElement("UninstallApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UninstallAppType UninstallApp1)
		{
			object[] array = this.Invoke("UninstallApp", new object[]
			{
				UninstallApp1
			});
			return (UninstallAppResponseType)array[0];
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00023774 File Offset: 0x00021974
		public IAsyncResult BeginUninstallApp(UninstallAppType UninstallApp1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UninstallApp", new object[]
			{
				UninstallApp1
			}, callback, asyncState);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002379C File Offset: 0x0002199C
		public UninstallAppResponseType EndUninstallApp(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UninstallAppResponseType)array[0];
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000237B9 File Offset: 0x000219B9
		public void UninstallAppAsync(UninstallAppType UninstallApp1)
		{
			this.UninstallAppAsync(UninstallApp1, null);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000237C4 File Offset: 0x000219C4
		public void UninstallAppAsync(UninstallAppType UninstallApp1, object userState)
		{
			if (this.UninstallAppOperationCompleted == null)
			{
				this.UninstallAppOperationCompleted = new SendOrPostCallback(this.OnUninstallAppOperationCompleted);
			}
			base.InvokeAsync("UninstallApp", new object[]
			{
				UninstallApp1
			}, this.UninstallAppOperationCompleted, userState);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002380C File Offset: 0x00021A0C
		private void OnUninstallAppOperationCompleted(object arg)
		{
			if (this.UninstallAppCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UninstallAppCompleted(this, new UninstallAppCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00023854 File Offset: 0x00021A54
		[SoapHttpClientTraceExtension]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DisableApp", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("DisableAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DisableAppResponseType DisableApp([XmlElement("DisableApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DisableAppType DisableApp1)
		{
			object[] array = this.Invoke("DisableApp", new object[]
			{
				DisableApp1
			});
			return (DisableAppResponseType)array[0];
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00023884 File Offset: 0x00021A84
		public IAsyncResult BeginDisableApp(DisableAppType DisableApp1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DisableApp", new object[]
			{
				DisableApp1
			}, callback, asyncState);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x000238AC File Offset: 0x00021AAC
		public DisableAppResponseType EndDisableApp(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DisableAppResponseType)array[0];
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000238C9 File Offset: 0x00021AC9
		public void DisableAppAsync(DisableAppType DisableApp1)
		{
			this.DisableAppAsync(DisableApp1, null);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000238D4 File Offset: 0x00021AD4
		public void DisableAppAsync(DisableAppType DisableApp1, object userState)
		{
			if (this.DisableAppOperationCompleted == null)
			{
				this.DisableAppOperationCompleted = new SendOrPostCallback(this.OnDisableAppOperationCompleted);
			}
			base.InvokeAsync("DisableApp", new object[]
			{
				DisableApp1
			}, this.DisableAppOperationCompleted, userState);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002391C File Offset: 0x00021B1C
		private void OnDisableAppOperationCompleted(object arg)
		{
			if (this.DisableAppCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DisableAppCompleted(this, new DisableAppCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00023964 File Offset: 0x00021B64
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetAppMarketplaceUrl", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHttpClientTraceExtension]
		[return: XmlElement("GetAppMarketplaceUrlResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetAppMarketplaceUrlResponseMessageType GetAppMarketplaceUrl([XmlElement("GetAppMarketplaceUrl", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetAppMarketplaceUrlType GetAppMarketplaceUrl1)
		{
			object[] array = this.Invoke("GetAppMarketplaceUrl", new object[]
			{
				GetAppMarketplaceUrl1
			});
			return (GetAppMarketplaceUrlResponseMessageType)array[0];
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00023994 File Offset: 0x00021B94
		public IAsyncResult BeginGetAppMarketplaceUrl(GetAppMarketplaceUrlType GetAppMarketplaceUrl1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAppMarketplaceUrl", new object[]
			{
				GetAppMarketplaceUrl1
			}, callback, asyncState);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x000239BC File Offset: 0x00021BBC
		public GetAppMarketplaceUrlResponseMessageType EndGetAppMarketplaceUrl(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetAppMarketplaceUrlResponseMessageType)array[0];
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000239D9 File Offset: 0x00021BD9
		public void GetAppMarketplaceUrlAsync(GetAppMarketplaceUrlType GetAppMarketplaceUrl1)
		{
			this.GetAppMarketplaceUrlAsync(GetAppMarketplaceUrl1, null);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000239E4 File Offset: 0x00021BE4
		public void GetAppMarketplaceUrlAsync(GetAppMarketplaceUrlType GetAppMarketplaceUrl1, object userState)
		{
			if (this.GetAppMarketplaceUrlOperationCompleted == null)
			{
				this.GetAppMarketplaceUrlOperationCompleted = new SendOrPostCallback(this.OnGetAppMarketplaceUrlOperationCompleted);
			}
			base.InvokeAsync("GetAppMarketplaceUrl", new object[]
			{
				GetAppMarketplaceUrl1
			}, this.GetAppMarketplaceUrlOperationCompleted, userState);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00023A2C File Offset: 0x00021C2C
		private void OnGetAppMarketplaceUrlOperationCompleted(object arg)
		{
			if (this.GetAppMarketplaceUrlCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAppMarketplaceUrlCompleted(this, new GetAppMarketplaceUrlCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00023A74 File Offset: 0x00021C74
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHttpClientTraceExtension]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserPhoto", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetUserPhotoResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserPhotoResponseMessageType GetUserPhoto([XmlElement("GetUserPhoto", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserPhotoType GetUserPhoto1)
		{
			object[] array = this.Invoke("GetUserPhoto", new object[]
			{
				GetUserPhoto1
			});
			return (GetUserPhotoResponseMessageType)array[0];
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00023AA4 File Offset: 0x00021CA4
		public IAsyncResult BeginGetUserPhoto(GetUserPhotoType GetUserPhoto1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserPhoto", new object[]
			{
				GetUserPhoto1
			}, callback, asyncState);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00023ACC File Offset: 0x00021CCC
		public GetUserPhotoResponseMessageType EndGetUserPhoto(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserPhotoResponseMessageType)array[0];
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00023AE9 File Offset: 0x00021CE9
		public void GetUserPhotoAsync(GetUserPhotoType GetUserPhoto1)
		{
			this.GetUserPhotoAsync(GetUserPhoto1, null);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00023AF4 File Offset: 0x00021CF4
		public void GetUserPhotoAsync(GetUserPhotoType GetUserPhoto1, object userState)
		{
			if (this.GetUserPhotoOperationCompleted == null)
			{
				this.GetUserPhotoOperationCompleted = new SendOrPostCallback(this.OnGetUserPhotoOperationCompleted);
			}
			base.InvokeAsync("GetUserPhoto", new object[]
			{
				GetUserPhoto1
			}, this.GetUserPhotoOperationCompleted, userState);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00023B3C File Offset: 0x00021D3C
		private void OnGetUserPhotoOperationCompleted(object arg)
		{
			if (this.GetUserPhotoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserPhotoCompleted(this, new GetUserPhotoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00023B81 File Offset: 0x00021D81
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00023B8A File Offset: 0x00021D8A
		internal override XmlNamespaceDefinition[] PredefinedNamespaces
		{
			get
			{
				return Constants.EwsNamespaces;
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00023B91 File Offset: 0x00021D91
		public ExchangeServiceBinding(string componentId, RemoteCertificateValidationCallback remoteCertificateValidationCallback) : base(remoteCertificateValidationCallback, true)
		{
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00023B9B File Offset: 0x00021D9B
		public ExchangeServiceBinding(string componentId, RemoteCertificateValidationCallback remoteCertificateValidationCallback, bool normalization) : base(remoteCertificateValidationCallback, normalization)
		{
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00023BA5 File Offset: 0x00021DA5
		public ExchangeServiceBinding(RemoteCertificateValidationCallback remoteCertificateValidationCallback) : base(remoteCertificateValidationCallback, true)
		{
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00023BAF File Offset: 0x00021DAF
		public ExchangeServiceBinding(RemoteCertificateValidationCallback remoteCertificateValidationCallback, bool normalization) : base(remoteCertificateValidationCallback, normalization)
		{
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00023BB9 File Offset: 0x00021DB9
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00023BC1 File Offset: 0x00021DC1
		string IExchangeService.Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				base.Url = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00023BCA File Offset: 0x00021DCA
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x00023BD2 File Offset: 0x00021DD2
		int IExchangeService.Timeout
		{
			get
			{
				return base.Timeout;
			}
			set
			{
				base.Timeout = value;
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00023BDC File Offset: 0x00021DDC
		protected override WebRequest GetWebRequest(Uri uri)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)base.GetWebRequest(uri);
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null)
			{
				currentActivityScope.SerializeTo(httpWebRequest);
			}
			return httpWebRequest;
		}

		// Token: 0x040006B5 RID: 1717
		public ExchangeImpersonationType ExchangeImpersonation;

		// Token: 0x040006B6 RID: 1718
		public MailboxCultureType MailboxCulture;

		// Token: 0x040006B7 RID: 1719
		public RequestServerVersion RequestServerVersionValue;

		// Token: 0x040006B8 RID: 1720
		public ServerVersionInfo ServerVersionInfoValue;

		// Token: 0x040006B9 RID: 1721
		private SendOrPostCallback ResolveNamesOperationCompleted;

		// Token: 0x040006BA RID: 1722
		private SendOrPostCallback ExpandDLOperationCompleted;

		// Token: 0x040006BB RID: 1723
		private SendOrPostCallback GetServerTimeZonesOperationCompleted;

		// Token: 0x040006BC RID: 1724
		public TimeZoneContextType TimeZoneContext;

		// Token: 0x040006BD RID: 1725
		public ManagementRoleType ManagementRole;

		// Token: 0x040006BE RID: 1726
		private SendOrPostCallback FindFolderOperationCompleted;

		// Token: 0x040006BF RID: 1727
		public DateTimePrecisionType DateTimePrecision;

		// Token: 0x040006C0 RID: 1728
		private SendOrPostCallback FindItemOperationCompleted;

		// Token: 0x040006C1 RID: 1729
		private SendOrPostCallback GetFolderOperationCompleted;

		// Token: 0x040006C2 RID: 1730
		private SendOrPostCallback UploadItemsOperationCompleted;

		// Token: 0x040006C3 RID: 1731
		private SendOrPostCallback ExportItemsOperationCompleted;

		// Token: 0x040006C4 RID: 1732
		private SendOrPostCallback ConvertIdOperationCompleted;

		// Token: 0x040006C5 RID: 1733
		private SendOrPostCallback CreateFolderOperationCompleted;

		// Token: 0x040006C6 RID: 1734
		private SendOrPostCallback CreateFolderPathOperationCompleted;

		// Token: 0x040006C7 RID: 1735
		private SendOrPostCallback DeleteFolderOperationCompleted;

		// Token: 0x040006C8 RID: 1736
		private SendOrPostCallback EmptyFolderOperationCompleted;

		// Token: 0x040006C9 RID: 1737
		private SendOrPostCallback UpdateFolderOperationCompleted;

		// Token: 0x040006CA RID: 1738
		private SendOrPostCallback MoveFolderOperationCompleted;

		// Token: 0x040006CB RID: 1739
		private SendOrPostCallback CopyFolderOperationCompleted;

		// Token: 0x040006CC RID: 1740
		private SendOrPostCallback SubscribeOperationCompleted;

		// Token: 0x040006CD RID: 1741
		private SendOrPostCallback UnsubscribeOperationCompleted;

		// Token: 0x040006CE RID: 1742
		private SendOrPostCallback GetEventsOperationCompleted;

		// Token: 0x040006CF RID: 1743
		private SendOrPostCallback GetStreamingEventsOperationCompleted;

		// Token: 0x040006D0 RID: 1744
		private SendOrPostCallback SyncFolderHierarchyOperationCompleted;

		// Token: 0x040006D1 RID: 1745
		private SendOrPostCallback SyncFolderItemsOperationCompleted;

		// Token: 0x040006D2 RID: 1746
		private SendOrPostCallback CreateManagedFolderOperationCompleted;

		// Token: 0x040006D3 RID: 1747
		private SendOrPostCallback GetItemOperationCompleted;

		// Token: 0x040006D4 RID: 1748
		private SendOrPostCallback CreateItemOperationCompleted;

		// Token: 0x040006D5 RID: 1749
		private SendOrPostCallback DeleteItemOperationCompleted;

		// Token: 0x040006D6 RID: 1750
		private SendOrPostCallback UpdateItemOperationCompleted;

		// Token: 0x040006D7 RID: 1751
		private SendOrPostCallback UpdateItemInRecoverableItemsOperationCompleted;

		// Token: 0x040006D8 RID: 1752
		private SendOrPostCallback SendItemOperationCompleted;

		// Token: 0x040006D9 RID: 1753
		private SendOrPostCallback MoveItemOperationCompleted;

		// Token: 0x040006DA RID: 1754
		private SendOrPostCallback CopyItemOperationCompleted;

		// Token: 0x040006DB RID: 1755
		private SendOrPostCallback ArchiveItemOperationCompleted;

		// Token: 0x040006DC RID: 1756
		private SendOrPostCallback CreateAttachmentOperationCompleted;

		// Token: 0x040006DD RID: 1757
		private SendOrPostCallback DeleteAttachmentOperationCompleted;

		// Token: 0x040006DE RID: 1758
		private SendOrPostCallback GetAttachmentOperationCompleted;

		// Token: 0x040006DF RID: 1759
		private SendOrPostCallback GetClientAccessTokenOperationCompleted;

		// Token: 0x040006E0 RID: 1760
		private SendOrPostCallback GetDelegateOperationCompleted;

		// Token: 0x040006E1 RID: 1761
		private SendOrPostCallback AddDelegateOperationCompleted;

		// Token: 0x040006E2 RID: 1762
		private SendOrPostCallback RemoveDelegateOperationCompleted;

		// Token: 0x040006E3 RID: 1763
		private SendOrPostCallback UpdateDelegateOperationCompleted;

		// Token: 0x040006E4 RID: 1764
		private SendOrPostCallback CreateUserConfigurationOperationCompleted;

		// Token: 0x040006E5 RID: 1765
		private SendOrPostCallback DeleteUserConfigurationOperationCompleted;

		// Token: 0x040006E6 RID: 1766
		private SendOrPostCallback GetUserConfigurationOperationCompleted;

		// Token: 0x040006E7 RID: 1767
		private SendOrPostCallback UpdateUserConfigurationOperationCompleted;

		// Token: 0x040006E8 RID: 1768
		private SendOrPostCallback GetUserAvailabilityOperationCompleted;

		// Token: 0x040006E9 RID: 1769
		private SendOrPostCallback GetUserOofSettingsOperationCompleted;

		// Token: 0x040006EA RID: 1770
		private SendOrPostCallback SetUserOofSettingsOperationCompleted;

		// Token: 0x040006EB RID: 1771
		private SendOrPostCallback GetServiceConfigurationOperationCompleted;

		// Token: 0x040006EC RID: 1772
		private SendOrPostCallback GetMailTipsOperationCompleted;

		// Token: 0x040006ED RID: 1773
		private SendOrPostCallback PlayOnPhoneOperationCompleted;

		// Token: 0x040006EE RID: 1774
		private SendOrPostCallback GetPhoneCallInformationOperationCompleted;

		// Token: 0x040006EF RID: 1775
		private SendOrPostCallback DisconnectPhoneCallOperationCompleted;

		// Token: 0x040006F0 RID: 1776
		private SendOrPostCallback GetSharingMetadataOperationCompleted;

		// Token: 0x040006F1 RID: 1777
		private SendOrPostCallback RefreshSharingFolderOperationCompleted;

		// Token: 0x040006F2 RID: 1778
		private SendOrPostCallback GetSharingFolderOperationCompleted;

		// Token: 0x040006F3 RID: 1779
		private SendOrPostCallback SetTeamMailboxOperationCompleted;

		// Token: 0x040006F4 RID: 1780
		private SendOrPostCallback UnpinTeamMailboxOperationCompleted;

		// Token: 0x040006F5 RID: 1781
		private SendOrPostCallback GetRoomListsOperationCompleted;

		// Token: 0x040006F6 RID: 1782
		private SendOrPostCallback GetRoomsOperationCompleted;

		// Token: 0x040006F7 RID: 1783
		private SendOrPostCallback FindMessageTrackingReportOperationCompleted;

		// Token: 0x040006F8 RID: 1784
		private SendOrPostCallback GetMessageTrackingReportOperationCompleted;

		// Token: 0x040006F9 RID: 1785
		private SendOrPostCallback FindConversationOperationCompleted;

		// Token: 0x040006FA RID: 1786
		private SendOrPostCallback ApplyConversationActionOperationCompleted;

		// Token: 0x040006FB RID: 1787
		private SendOrPostCallback GetConversationItemsOperationCompleted;

		// Token: 0x040006FC RID: 1788
		private SendOrPostCallback FindPeopleOperationCompleted;

		// Token: 0x040006FD RID: 1789
		private SendOrPostCallback GetPersonaOperationCompleted;

		// Token: 0x040006FE RID: 1790
		private SendOrPostCallback GetInboxRulesOperationCompleted;

		// Token: 0x040006FF RID: 1791
		private SendOrPostCallback UpdateInboxRulesOperationCompleted;

		// Token: 0x04000700 RID: 1792
		private SendOrPostCallback GetPasswordExpirationDateOperationCompleted;

		// Token: 0x04000701 RID: 1793
		private SendOrPostCallback GetSearchableMailboxesOperationCompleted;

		// Token: 0x04000702 RID: 1794
		private SendOrPostCallback SearchMailboxesOperationCompleted;

		// Token: 0x04000703 RID: 1795
		private SendOrPostCallback GetDiscoverySearchConfigurationOperationCompleted;

		// Token: 0x04000704 RID: 1796
		private SendOrPostCallback GetHoldOnMailboxesOperationCompleted;

		// Token: 0x04000705 RID: 1797
		private SendOrPostCallback SetHoldOnMailboxesOperationCompleted;

		// Token: 0x04000706 RID: 1798
		private SendOrPostCallback GetNonIndexableItemStatisticsOperationCompleted;

		// Token: 0x04000707 RID: 1799
		private SendOrPostCallback GetNonIndexableItemDetailsOperationCompleted;

		// Token: 0x04000708 RID: 1800
		private SendOrPostCallback MarkAllItemsAsReadOperationCompleted;

		// Token: 0x04000709 RID: 1801
		private SendOrPostCallback MarkAsJunkOperationCompleted;

		// Token: 0x0400070A RID: 1802
		private SendOrPostCallback GetAppManifestsOperationCompleted;

		// Token: 0x0400070B RID: 1803
		private SendOrPostCallback AddNewImContactToGroupOperationCompleted;

		// Token: 0x0400070C RID: 1804
		private SendOrPostCallback AddNewTelUriContactToGroupOperationCompleted;

		// Token: 0x0400070D RID: 1805
		private SendOrPostCallback AddImContactToGroupOperationCompleted;

		// Token: 0x0400070E RID: 1806
		private SendOrPostCallback RemoveImContactFromGroupOperationCompleted;

		// Token: 0x0400070F RID: 1807
		private SendOrPostCallback AddImGroupOperationCompleted;

		// Token: 0x04000710 RID: 1808
		private SendOrPostCallback AddDistributionGroupToImListOperationCompleted;

		// Token: 0x04000711 RID: 1809
		private SendOrPostCallback GetImItemListOperationCompleted;

		// Token: 0x04000712 RID: 1810
		private SendOrPostCallback GetImItemsOperationCompleted;

		// Token: 0x04000713 RID: 1811
		private SendOrPostCallback RemoveContactFromImListOperationCompleted;

		// Token: 0x04000714 RID: 1812
		private SendOrPostCallback RemoveDistributionGroupFromImListOperationCompleted;

		// Token: 0x04000715 RID: 1813
		private SendOrPostCallback RemoveImGroupOperationCompleted;

		// Token: 0x04000716 RID: 1814
		private SendOrPostCallback SetImGroupOperationCompleted;

		// Token: 0x04000717 RID: 1815
		private SendOrPostCallback SetImListMigrationCompletedOperationCompleted;

		// Token: 0x04000718 RID: 1816
		private SendOrPostCallback GetUserRetentionPolicyTagsOperationCompleted;

		// Token: 0x04000719 RID: 1817
		private SendOrPostCallback InstallAppOperationCompleted;

		// Token: 0x0400071A RID: 1818
		private SendOrPostCallback UninstallAppOperationCompleted;

		// Token: 0x0400071B RID: 1819
		private SendOrPostCallback DisableAppOperationCompleted;

		// Token: 0x0400071C RID: 1820
		private SendOrPostCallback GetAppMarketplaceUrlOperationCompleted;

		// Token: 0x0400071D RID: 1821
		private SendOrPostCallback GetUserPhotoOperationCompleted;
	}
}
