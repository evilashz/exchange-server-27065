using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000BC RID: 188
	internal interface IGlobalInfo
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060009E7 RID: 2535
		// (set) Token: 0x060009E8 RID: 2536
		int? LastPolicyXMLHash { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060009E9 RID: 2537
		// (set) Token: 0x060009EA RID: 2538
		ExDateTime? NextTimeToClearMailboxLogs { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060009EB RID: 2539
		// (set) Token: 0x060009EC RID: 2540
		uint PolicyKeyNeeded { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060009ED RID: 2541
		// (set) Token: 0x060009EE RID: 2542
		uint PolicyKeyWaitingAck { get; set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060009EF RID: 2543
		// (set) Token: 0x060009F0 RID: 2544
		uint PolicyKeyOnDevice { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060009F1 RID: 2545
		// (set) Token: 0x060009F2 RID: 2546
		bool ProvisionSupported { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060009F3 RID: 2547
		// (set) Token: 0x060009F4 RID: 2548
		ExDateTime? LastPolicyTime { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060009F5 RID: 2549
		// (set) Token: 0x060009F6 RID: 2550
		ExDateTime? LastSyncAttemptTime { get; set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060009F7 RID: 2551
		// (set) Token: 0x060009F8 RID: 2552
		ExDateTime? LastSyncSuccessTime { get; set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060009F9 RID: 2553
		// (set) Token: 0x060009FA RID: 2554
		ExDateTime? RemoteWipeRequestedTime { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060009FB RID: 2555
		// (set) Token: 0x060009FC RID: 2556
		ExDateTime? RemoteWipeSentTime { get; set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060009FD RID: 2557
		// (set) Token: 0x060009FE RID: 2558
		ExDateTime? RemoteWipeAckTime { get; set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060009FF RID: 2559
		// (set) Token: 0x06000A00 RID: 2560
		string DeviceModel { get; set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000A01 RID: 2561
		// (set) Token: 0x06000A02 RID: 2562
		string DeviceImei { get; set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000A03 RID: 2563
		// (set) Token: 0x06000A04 RID: 2564
		string DeviceFriendlyName { get; set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000A05 RID: 2565
		// (set) Token: 0x06000A06 RID: 2566
		string DeviceOS { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000A07 RID: 2567
		// (set) Token: 0x06000A08 RID: 2568
		string DeviceOSLanguage { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000A09 RID: 2569
		// (set) Token: 0x06000A0A RID: 2570
		string DevicePhoneNumber { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000A0B RID: 2571
		// (set) Token: 0x06000A0C RID: 2572
		string UserAgent { get; set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000A0D RID: 2573
		// (set) Token: 0x06000A0E RID: 2574
		bool DeviceEnableOutboundSMS { get; set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000A0F RID: 2575
		// (set) Token: 0x06000A10 RID: 2576
		string DeviceMobileOperator { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000A11 RID: 2577
		// (set) Token: 0x06000A12 RID: 2578
		string RecoveryPassword { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000A13 RID: 2579
		// (set) Token: 0x06000A14 RID: 2580
		DeviceAccessState DeviceAccessState { get; set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000A15 RID: 2581
		// (set) Token: 0x06000A16 RID: 2582
		DeviceAccessStateReason DeviceAccessStateReason { get; set; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000A17 RID: 2583
		// (set) Token: 0x06000A18 RID: 2584
		DevicePolicyApplicationStatus DevicePolicyApplicationStatus { get; set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000A19 RID: 2585
		// (set) Token: 0x06000A1A RID: 2586
		ADObjectId DevicePolicyApplied { get; set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000A1B RID: 2587
		// (set) Token: 0x06000A1C RID: 2588
		ADObjectId DeviceAccessControlRule { get; set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000A1D RID: 2589
		// (set) Token: 0x06000A1E RID: 2590
		string LastDeviceWipeRequestor { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000A1F RID: 2591
		// (set) Token: 0x06000A20 RID: 2592
		string DeviceActiveSyncVersion { get; set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000A21 RID: 2593
		// (set) Token: 0x06000A22 RID: 2594
		string[] RemoteWipeConfirmationAddresses { get; set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000A23 RID: 2595
		// (set) Token: 0x06000A24 RID: 2596
		int? ADDeviceInfoHash { get; set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000A25 RID: 2597
		// (set) Token: 0x06000A26 RID: 2598
		bool HaveSentBoostrapMailForWM61 { get; set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000A27 RID: 2599
		// (set) Token: 0x06000A28 RID: 2600
		ExDateTime? BootstrapMailForWM61TriggeredTime { get; set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000A29 RID: 2601
		// (set) Token: 0x06000A2A RID: 2602
		bool DeviceInformationReceived { get; set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000A2B RID: 2603
		// (set) Token: 0x06000A2C RID: 2604
		ExDateTime? SyncStateUpgradeTime { get; set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000A2D RID: 2605
		// (set) Token: 0x06000A2E RID: 2606
		ExDateTime? ADCreationTime { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000A2F RID: 2607
		// (set) Token: 0x06000A30 RID: 2608
		ADObjectId DeviceADObjectId { get; set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000A31 RID: 2609
		// (set) Token: 0x06000A32 RID: 2610
		ADObjectId UserADObjectId { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000A33 RID: 2611
		bool IsSyncStateJustUpgraded { get; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000A34 RID: 2612
		// (set) Token: 0x06000A35 RID: 2613
		StoreObjectId ABQMailId { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000A36 RID: 2614
		// (set) Token: 0x06000A37 RID: 2615
		ABQMailState ABQMailState { get; set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000A38 RID: 2616
		// (set) Token: 0x06000A39 RID: 2617
		bool DeviceInformationPromoted { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000A3A RID: 2618
		// (set) Token: 0x06000A3B RID: 2619
		string DevicePhoneNumberForSms { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000A3C RID: 2620
		// (set) Token: 0x06000A3D RID: 2621
		bool SmsSearchFolderCreated { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000A3E RID: 2622
		// (set) Token: 0x06000A3F RID: 2623
		DeviceBehavior DeviceBehavior { get; set; }
	}
}
