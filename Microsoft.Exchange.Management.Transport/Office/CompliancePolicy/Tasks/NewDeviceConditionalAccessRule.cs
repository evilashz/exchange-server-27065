using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000100 RID: 256
	[Cmdlet("New", "DeviceConditionalAccessRule", SupportsShouldProcess = true)]
	public sealed class NewDeviceConditionalAccessRule : NewDeviceRuleBase
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x00029DBE File Offset: 0x00027FBE
		public NewDeviceConditionalAccessRule() : base(PolicyScenario.DeviceConditionalAccess)
		{
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00029DC7 File Offset: 0x00027FC7
		protected override DeviceRuleBase CreateDeviceRule(RuleStorage ruleStorage)
		{
			return new DeviceConditionalAccessRule(ruleStorage);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00029DCF File Offset: 0x00027FCF
		protected override Exception GetDeviceRuleAlreadyExistsException(string name)
		{
			return new DeviceConditionalAccessRuleAlreadyExistsException(name);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00029DD7 File Offset: 0x00027FD7
		protected override bool GetDeviceRuleGuidFromWorkload(Workload workload, out Guid ruleGuid)
		{
			ruleGuid = default(Guid);
			return DevicePolicyUtility.GetConditionalAccessRuleGuidFromWorkload(workload, out ruleGuid);
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00029DE7 File Offset: 0x00027FE7
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00029DFE File Offset: 0x00027FFE
		[Parameter(Mandatory = false)]
		public bool? AllowJailbroken
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Security_Jailbroken];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Security_Jailbroken] = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00029E16 File Offset: 0x00028016
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x00029E2D File Offset: 0x0002802D
		[Parameter(Mandatory = false)]
		public bool? PasswordRequired
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Password_Required];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_Required] = value;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00029E45 File Offset: 0x00028045
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00029E5C File Offset: 0x0002805C
		[Parameter(Mandatory = false)]
		public bool? PhoneMemoryEncrypted
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Encryption_PhoneMemoryEncrypted];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Encryption_PhoneMemoryEncrypted] = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00029E74 File Offset: 0x00028074
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x00029E8B File Offset: 0x0002808B
		[Parameter(Mandatory = false)]
		public TimeSpan? PasswordTimeout
		{
			get
			{
				return (TimeSpan?)base.Fields[DeviceConditionalAccessRule.Device_Password_Timeout];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_Timeout] = value;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00029EA3 File Offset: 0x000280A3
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x00029EBA File Offset: 0x000280BA
		[Parameter(Mandatory = false)]
		public int? PasswordMinimumLength
		{
			get
			{
				return (int?)base.Fields[DeviceConditionalAccessRule.Device_Password_MinimumLength];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_MinimumLength] = value;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00029ED2 File Offset: 0x000280D2
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x00029EE9 File Offset: 0x000280E9
		[Parameter(Mandatory = false)]
		public int? PasswordHistoryCount
		{
			get
			{
				return (int?)base.Fields[DeviceConditionalAccessRule.Device_Password_History];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_History] = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00029F01 File Offset: 0x00028101
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x00029F18 File Offset: 0x00028118
		[Parameter(Mandatory = false)]
		public int? PasswordExpirationDays
		{
			get
			{
				return (int?)base.Fields[DeviceConditionalAccessRule.Device_Password_Expiration];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_Expiration] = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00029F30 File Offset: 0x00028130
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00029F47 File Offset: 0x00028147
		[Parameter(Mandatory = false)]
		public int? PasswordMinComplexChars
		{
			get
			{
				return (int?)base.Fields[DeviceConditionalAccessRule.Device_Password_MinComplexChars];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_MinComplexChars] = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00029F5F File Offset: 0x0002815F
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00029F76 File Offset: 0x00028176
		[Parameter(Mandatory = false)]
		public bool? AllowSimplePassword
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Password_AllowSimplePassword];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_AllowSimplePassword] = value;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00029F8E File Offset: 0x0002818E
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x00029FA5 File Offset: 0x000281A5
		[Parameter(Mandatory = false)]
		public int? PasswordQuality
		{
			get
			{
				return (int?)base.Fields[DeviceConditionalAccessRule.Device_Password_PasswordQuality];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_PasswordQuality] = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00029FBD File Offset: 0x000281BD
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x00029FD4 File Offset: 0x000281D4
		[Parameter(Mandatory = false)]
		public int? MaxPasswordAttemptsBeforeWipe
		{
			get
			{
				return (int?)base.Fields[DeviceConditionalAccessRule.Device_Password_MaxAttemptsBeforeWipe];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_MaxAttemptsBeforeWipe] = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00029FEC File Offset: 0x000281EC
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x0002A003 File Offset: 0x00028203
		[Parameter(Mandatory = false)]
		public bool? EnableRemovableStorage
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Security_EnableRemovableStorage];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Security_EnableRemovableStorage] = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0002A01B File Offset: 0x0002821B
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x0002A032 File Offset: 0x00028232
		[Parameter(Mandatory = false)]
		public bool? CameraEnabled
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Security_CameraEnabled];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Security_CameraEnabled] = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002A04A File Offset: 0x0002824A
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x0002A061 File Offset: 0x00028261
		[Parameter(Mandatory = false)]
		public bool? BluetoothEnabled
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Security_BluetoothEnabled];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Security_BluetoothEnabled] = value;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002A079 File Offset: 0x00028279
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x0002A090 File Offset: 0x00028290
		[Parameter(Mandatory = false)]
		public bool? ForceEncryptedBackup
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Cloud_ForceEncryptedBackup];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Cloud_ForceEncryptedBackup] = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0002A0A8 File Offset: 0x000282A8
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x0002A0BF File Offset: 0x000282BF
		[Parameter(Mandatory = false)]
		public bool? AllowiCloudDocSync
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Cloud_AllowiCloudDocSync];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Cloud_AllowiCloudDocSync] = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0002A0D7 File Offset: 0x000282D7
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0002A0EE File Offset: 0x000282EE
		[Parameter(Mandatory = false)]
		public bool? AllowiCloudPhotoSync
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Cloud_AllowiCloudPhotoSync];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Cloud_AllowiCloudPhotoSync] = value;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0002A106 File Offset: 0x00028306
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x0002A11D File Offset: 0x0002831D
		[Parameter(Mandatory = false)]
		public bool? AllowiCloudBackup
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Cloud_AllowiCloudBackup];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Cloud_AllowiCloudBackup] = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0002A135 File Offset: 0x00028335
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x0002A14C File Offset: 0x0002834C
		[Parameter(Mandatory = false)]
		public CARatingRegionEntry? RegionRatings
		{
			get
			{
				return (CARatingRegionEntry?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_RatingsRegion];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_RatingsRegion] = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0002A164 File Offset: 0x00028364
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x0002A17B File Offset: 0x0002837B
		[Parameter(Mandatory = false)]
		public CARatingMovieEntry? MoviesRating
		{
			get
			{
				return (CARatingMovieEntry?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_RatingMovies];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_RatingMovies] = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0002A193 File Offset: 0x00028393
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0002A1AA File Offset: 0x000283AA
		[Parameter(Mandatory = false)]
		public CARatingTvShowEntry? TVShowsRating
		{
			get
			{
				return (CARatingTvShowEntry?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_RatingTVShows];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_RatingTVShows] = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0002A1C2 File Offset: 0x000283C2
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x0002A1D9 File Offset: 0x000283D9
		[Parameter(Mandatory = false)]
		public CARatingAppsEntry? AppsRating
		{
			get
			{
				return (CARatingAppsEntry?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_RatingApps];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_RatingApps] = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0002A1F1 File Offset: 0x000283F1
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x0002A208 File Offset: 0x00028408
		[Parameter(Mandatory = false)]
		public bool? AllowVoiceDialing
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowVoiceDialing];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowVoiceDialing] = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0002A220 File Offset: 0x00028420
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x0002A237 File Offset: 0x00028437
		[Parameter(Mandatory = false)]
		public bool? AllowVoiceAssistant
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowVoiceAssistant];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowVoiceAssistant] = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0002A24F File Offset: 0x0002844F
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x0002A266 File Offset: 0x00028466
		[Parameter(Mandatory = false)]
		public bool? AllowAssistantWhileLocked
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowAssistantWhileLocked];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowAssistantWhileLocked] = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0002A27E File Offset: 0x0002847E
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x0002A295 File Offset: 0x00028495
		[Parameter(Mandatory = false)]
		public bool? AllowScreenshot
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowScreenshot];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowScreenshot] = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0002A2AD File Offset: 0x000284AD
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x0002A2C4 File Offset: 0x000284C4
		[Parameter(Mandatory = false)]
		public bool? AllowVideoConferencing
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowVideoConferencing];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowVideoConferencing] = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0002A2DC File Offset: 0x000284DC
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0002A2F3 File Offset: 0x000284F3
		[Parameter(Mandatory = false)]
		public bool? AllowPassbookWhileLocked
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowPassbookWhileLocked];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowPassbookWhileLocked] = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0002A30B File Offset: 0x0002850B
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0002A322 File Offset: 0x00028522
		[Parameter(Mandatory = false)]
		public bool? AllowDiagnosticSubmission
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowDiagnosticSubmission];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowDiagnosticSubmission] = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0002A33A File Offset: 0x0002853A
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x0002A351 File Offset: 0x00028551
		[Parameter(Mandatory = false)]
		public bool? AllowConvenienceLogon
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Password_AllowConvenienceLogon];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_AllowConvenienceLogon] = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0002A369 File Offset: 0x00028569
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x0002A380 File Offset: 0x00028580
		[Parameter(Mandatory = false)]
		public TimeSpan? MaxPasswordGracePeriod
		{
			get
			{
				return (TimeSpan?)base.Fields[DeviceConditionalAccessRule.Device_Password_MaxGracePeriod];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_MaxGracePeriod] = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002A398 File Offset: 0x00028598
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x0002A3AF File Offset: 0x000285AF
		[Parameter(Mandatory = false)]
		public bool? AllowAppStore
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowAppStore];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_AllowAppStore] = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0002A3C7 File Offset: 0x000285C7
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x0002A3DE File Offset: 0x000285DE
		[Parameter(Mandatory = false)]
		public bool? ForceAppStorePassword
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Restrictions_ForceAppStorePassword];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Restrictions_ForceAppStorePassword] = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0002A3F6 File Offset: 0x000285F6
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x0002A40D File Offset: 0x0002860D
		[Parameter(Mandatory = false)]
		public bool? SystemSecurityTLS
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_TLS];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_TLS] = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002A425 File Offset: 0x00028625
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0002A43C File Offset: 0x0002863C
		[Parameter(Mandatory = false)]
		public CAUserAccountControlStatusEntry? UserAccountControlStatus
		{
			get
			{
				return (CAUserAccountControlStatusEntry?)base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_UserAccountControlStatus];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_UserAccountControlStatus] = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002A454 File Offset: 0x00028654
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x0002A46B File Offset: 0x0002866B
		[Parameter(Mandatory = false)]
		public CAFirewallStatusEntry? FirewallStatus
		{
			get
			{
				return (CAFirewallStatusEntry?)base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_FirewallStatus];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_FirewallStatus] = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0002A483 File Offset: 0x00028683
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0002A49A File Offset: 0x0002869A
		[Parameter(Mandatory = false)]
		public CAAutoUpdateStatusEntry? AutoUpdateStatus
		{
			get
			{
				return (CAAutoUpdateStatusEntry?)base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_AutoUpdateStatus];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_AutoUpdateStatus] = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0002A4B2 File Offset: 0x000286B2
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x0002A4C9 File Offset: 0x000286C9
		[Parameter(Mandatory = false)]
		public string AntiVirusStatus
		{
			get
			{
				return (string)base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_AntiVirusStatus];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_AntiVirusStatus] = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002A4DC File Offset: 0x000286DC
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x0002A4F3 File Offset: 0x000286F3
		[Parameter(Mandatory = false)]
		public bool? AntiVirusSignatureStatus
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_AntiVirusSignatureStatus];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_SystemSecurity_AntiVirusSignatureStatus] = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002A50B File Offset: 0x0002870B
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x0002A522 File Offset: 0x00028722
		[Parameter(Mandatory = false)]
		public bool? SmartScreenEnabled
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_InternetExplorer_SmartScreenEnabled];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_InternetExplorer_SmartScreenEnabled] = value;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0002A53A File Offset: 0x0002873A
		// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x0002A551 File Offset: 0x00028751
		[Parameter(Mandatory = false)]
		public string WorkFoldersSyncUrl
		{
			get
			{
				return (string)base.Fields[DeviceConditionalAccessRule.Device_WorkFolders_SyncUrl];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_WorkFolders_SyncUrl] = value;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0002A564 File Offset: 0x00028764
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x0002A57B File Offset: 0x0002877B
		[Parameter(Mandatory = false)]
		public string PasswordComplexity
		{
			get
			{
				return (string)base.Fields[DeviceConditionalAccessRule.Device_Password_Type];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Password_Type] = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0002A58E File Offset: 0x0002878E
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0002A5A5 File Offset: 0x000287A5
		[Parameter(Mandatory = false)]
		public bool? WLANEnabled
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Device_Wireless_WLANEnabled];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Device_Wireless_WLANEnabled] = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0002A5BD File Offset: 0x000287BD
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x0002A5D4 File Offset: 0x000287D4
		[Parameter(Mandatory = false)]
		public string AccountName
		{
			get
			{
				return (string)base.Fields[DeviceConditionalAccessRule.Eas_AccountName];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_AccountName] = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0002A5E7 File Offset: 0x000287E7
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x0002A5FE File Offset: 0x000287FE
		[Parameter(Mandatory = false)]
		public string AccountUserName
		{
			get
			{
				return (string)base.Fields[DeviceConditionalAccessRule.Eas_UserName];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_UserName] = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0002A611 File Offset: 0x00028811
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0002A628 File Offset: 0x00028828
		[Parameter(Mandatory = false)]
		public string ExchangeActiveSyncHost
		{
			get
			{
				return (string)base.Fields[DeviceConditionalAccessRule.Eas_Host];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_Host] = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002A63B File Offset: 0x0002883B
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0002A652 File Offset: 0x00028852
		[Parameter(Mandatory = false)]
		public string EmailAddress
		{
			get
			{
				return (string)base.Fields[DeviceConditionalAccessRule.Eas_EmailAddress];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_EmailAddress] = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0002A665 File Offset: 0x00028865
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0002A67C File Offset: 0x0002887C
		[Parameter(Mandatory = false)]
		public bool? UseSSL
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Eas_UseSSL];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_UseSSL] = value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0002A694 File Offset: 0x00028894
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x0002A6AB File Offset: 0x000288AB
		[Parameter(Mandatory = false)]
		public bool? AllowMove
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Eas_PreventAppSheet];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_PreventAppSheet] = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002A6C3 File Offset: 0x000288C3
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0002A6DA File Offset: 0x000288DA
		[Parameter(Mandatory = false)]
		public bool? AllowRecentAddressSyncing
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Eas_DisableMailRecentsSyncing];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_DisableMailRecentsSyncing] = value;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002A6F2 File Offset: 0x000288F2
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0002A709 File Offset: 0x00028909
		[Parameter(Mandatory = false)]
		public long? DaysToSync
		{
			get
			{
				return (long?)base.Fields[DeviceConditionalAccessRule.Eas_MailNumberOfPastDaysToSync];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_MailNumberOfPastDaysToSync] = value;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002A721 File Offset: 0x00028921
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0002A738 File Offset: 0x00028938
		[Parameter(Mandatory = false)]
		public long? ContentType
		{
			get
			{
				return (long?)base.Fields[DeviceConditionalAccessRule.Eas_ContentTypeToSync];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_ContentTypeToSync] = value;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002A750 File Offset: 0x00028950
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x0002A767 File Offset: 0x00028967
		[Parameter(Mandatory = false)]
		public bool? UseSMIME
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Eas_UseSMIME];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_UseSMIME] = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0002A77F File Offset: 0x0002897F
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0002A796 File Offset: 0x00028996
		[Parameter(Mandatory = false)]
		public long? SyncSchedule
		{
			get
			{
				return (long?)base.Fields[DeviceConditionalAccessRule.Eas_SyncSchedule];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_SyncSchedule] = value;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0002A7AE File Offset: 0x000289AE
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0002A7C5 File Offset: 0x000289C5
		[Parameter(Mandatory = false)]
		public bool? UseOnlyInEmail
		{
			get
			{
				return (bool?)base.Fields[DeviceConditionalAccessRule.Eas_PreventMove];
			}
			set
			{
				base.Fields[DeviceConditionalAccessRule.Eas_PreventMove] = value;
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002A7E0 File Offset: 0x000289E0
		protected override void SetPropsOnDeviceRule(DeviceRuleBase pdeviceRule)
		{
			DeviceConditionalAccessRule deviceConditionalAccessRule = (DeviceConditionalAccessRule)pdeviceRule;
			deviceConditionalAccessRule.AllowJailbroken = this.AllowJailbroken;
			deviceConditionalAccessRule.PasswordRequired = this.PasswordRequired;
			deviceConditionalAccessRule.PhoneMemoryEncrypted = this.PhoneMemoryEncrypted;
			deviceConditionalAccessRule.PasswordTimeout = this.PasswordTimeout;
			deviceConditionalAccessRule.PasswordMinimumLength = this.PasswordMinimumLength;
			deviceConditionalAccessRule.PasswordHistoryCount = this.PasswordHistoryCount;
			deviceConditionalAccessRule.PasswordExpirationDays = this.PasswordExpirationDays;
			deviceConditionalAccessRule.PasswordMinComplexChars = this.PasswordMinComplexChars;
			deviceConditionalAccessRule.AllowSimplePassword = this.AllowSimplePassword;
			deviceConditionalAccessRule.PasswordQuality = this.PasswordQuality;
			deviceConditionalAccessRule.MaxPasswordAttemptsBeforeWipe = this.MaxPasswordAttemptsBeforeWipe;
			deviceConditionalAccessRule.EnableRemovableStorage = this.EnableRemovableStorage;
			deviceConditionalAccessRule.CameraEnabled = this.CameraEnabled;
			deviceConditionalAccessRule.BluetoothEnabled = this.BluetoothEnabled;
			deviceConditionalAccessRule.ForceEncryptedBackup = this.ForceEncryptedBackup;
			deviceConditionalAccessRule.AllowiCloudDocSync = this.AllowiCloudDocSync;
			deviceConditionalAccessRule.AllowiCloudPhotoSync = this.AllowiCloudPhotoSync;
			deviceConditionalAccessRule.AllowiCloudBackup = this.AllowiCloudBackup;
			deviceConditionalAccessRule.RegionRatings = this.RegionRatings;
			deviceConditionalAccessRule.MoviesRating = this.MoviesRating;
			deviceConditionalAccessRule.TVShowsRating = this.TVShowsRating;
			deviceConditionalAccessRule.AppsRating = this.AppsRating;
			deviceConditionalAccessRule.AllowVoiceDialing = this.AllowVoiceDialing;
			deviceConditionalAccessRule.AllowVoiceAssistant = this.AllowVoiceAssistant;
			deviceConditionalAccessRule.AllowAssistantWhileLocked = this.AllowAssistantWhileLocked;
			deviceConditionalAccessRule.AllowScreenshot = this.AllowScreenshot;
			deviceConditionalAccessRule.AllowVideoConferencing = this.AllowVideoConferencing;
			deviceConditionalAccessRule.AllowPassbookWhileLocked = this.AllowPassbookWhileLocked;
			deviceConditionalAccessRule.AllowDiagnosticSubmission = this.AllowDiagnosticSubmission;
			deviceConditionalAccessRule.AllowConvenienceLogon = this.AllowConvenienceLogon;
			deviceConditionalAccessRule.MaxPasswordGracePeriod = this.MaxPasswordGracePeriod;
			deviceConditionalAccessRule.AllowAppStore = this.AllowAppStore;
			deviceConditionalAccessRule.ForceAppStorePassword = this.ForceAppStorePassword;
			deviceConditionalAccessRule.SystemSecurityTLS = this.SystemSecurityTLS;
			deviceConditionalAccessRule.UserAccountControlStatus = this.UserAccountControlStatus;
			deviceConditionalAccessRule.FirewallStatus = this.FirewallStatus;
			deviceConditionalAccessRule.AutoUpdateStatus = this.AutoUpdateStatus;
			deviceConditionalAccessRule.AntiVirusStatus = this.AntiVirusStatus;
			deviceConditionalAccessRule.AntiVirusSignatureStatus = this.AntiVirusSignatureStatus;
			deviceConditionalAccessRule.SmartScreenEnabled = this.SmartScreenEnabled;
			deviceConditionalAccessRule.WorkFoldersSyncUrl = this.WorkFoldersSyncUrl;
			deviceConditionalAccessRule.PasswordComplexity = this.PasswordComplexity;
			deviceConditionalAccessRule.WLANEnabled = this.WLANEnabled;
			deviceConditionalAccessRule.AccountName = this.AccountName;
			deviceConditionalAccessRule.AccountUserName = this.AccountUserName;
			deviceConditionalAccessRule.ExchangeActiveSyncHost = this.ExchangeActiveSyncHost;
			deviceConditionalAccessRule.EmailAddress = this.EmailAddress;
			deviceConditionalAccessRule.UseSSL = this.UseSSL;
			deviceConditionalAccessRule.AllowMove = this.AllowMove;
			deviceConditionalAccessRule.AllowRecentAddressSyncing = this.AllowRecentAddressSyncing;
			deviceConditionalAccessRule.DaysToSync = this.DaysToSync;
			deviceConditionalAccessRule.ContentType = this.ContentType;
			deviceConditionalAccessRule.UseSMIME = this.UseSMIME;
			deviceConditionalAccessRule.SyncSchedule = this.SyncSchedule;
			deviceConditionalAccessRule.UseOnlyInEmail = this.UseOnlyInEmail;
		}
	}
}
