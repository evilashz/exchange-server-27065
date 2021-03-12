using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000103 RID: 259
	[Cmdlet("Set", "DeviceConditionalAccessRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDeviceConditionalAccessRule : SetDeviceRuleBase
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x0002B0DB File Offset: 0x000292DB
		public SetDeviceConditionalAccessRule() : base(PolicyScenario.DeviceConditionalAccess)
		{
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002B0E4 File Offset: 0x000292E4
		protected override DeviceRuleBase CreateDeviceRule(RuleStorage dataObject)
		{
			return new DeviceConditionalAccessRule(dataObject);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002B0EC File Offset: 0x000292EC
		protected override void ValidateUnacceptableParameter()
		{
			if (this.Identity != null && !DevicePolicyUtility.IsDeviceConditionalAccessRule(this.Identity.ToString()))
			{
				base.WriteError(new ArgumentException(Strings.CanOnlyManipulateDeviceConditionalAccessRule), ErrorCategory.InvalidArgument, null);
			}
			if (DevicePolicyUtility.IsPropertySpecified(base.DynamicParametersInstance, ADObjectSchema.Name))
			{
				base.WriteError(new ArgumentException(Strings.CannotChangeDeviceConditionalAccessRuleName), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0002B153 File Offset: 0x00029353
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x0002B16A File Offset: 0x0002936A
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

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0002B182 File Offset: 0x00029382
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x0002B199 File Offset: 0x00029399
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

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0002B1B1 File Offset: 0x000293B1
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x0002B1C8 File Offset: 0x000293C8
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

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0002B1E0 File Offset: 0x000293E0
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x0002B1F7 File Offset: 0x000293F7
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

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0002B20F File Offset: 0x0002940F
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x0002B226 File Offset: 0x00029426
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

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0002B23E File Offset: 0x0002943E
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x0002B255 File Offset: 0x00029455
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

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0002B26D File Offset: 0x0002946D
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x0002B284 File Offset: 0x00029484
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

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002B29C File Offset: 0x0002949C
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x0002B2B3 File Offset: 0x000294B3
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

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0002B2CB File Offset: 0x000294CB
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x0002B2E2 File Offset: 0x000294E2
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

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0002B2FA File Offset: 0x000294FA
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0002B311 File Offset: 0x00029511
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

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0002B329 File Offset: 0x00029529
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0002B340 File Offset: 0x00029540
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

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0002B358 File Offset: 0x00029558
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x0002B36F File Offset: 0x0002956F
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

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0002B387 File Offset: 0x00029587
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0002B39E File Offset: 0x0002959E
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

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0002B3B6 File Offset: 0x000295B6
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x0002B3CD File Offset: 0x000295CD
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

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0002B3E5 File Offset: 0x000295E5
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x0002B3FC File Offset: 0x000295FC
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

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0002B414 File Offset: 0x00029614
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x0002B42B File Offset: 0x0002962B
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

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0002B443 File Offset: 0x00029643
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x0002B45A File Offset: 0x0002965A
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

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0002B472 File Offset: 0x00029672
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x0002B489 File Offset: 0x00029689
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

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0002B4A1 File Offset: 0x000296A1
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x0002B4B8 File Offset: 0x000296B8
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

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0002B4D0 File Offset: 0x000296D0
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0002B4E7 File Offset: 0x000296E7
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

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0002B4FF File Offset: 0x000296FF
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x0002B516 File Offset: 0x00029716
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

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0002B52E File Offset: 0x0002972E
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x0002B545 File Offset: 0x00029745
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

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0002B55D File Offset: 0x0002975D
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x0002B574 File Offset: 0x00029774
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

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0002B58C File Offset: 0x0002978C
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x0002B5A3 File Offset: 0x000297A3
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

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0002B5BB File Offset: 0x000297BB
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x0002B5D2 File Offset: 0x000297D2
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

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0002B5EA File Offset: 0x000297EA
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x0002B601 File Offset: 0x00029801
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0002B619 File Offset: 0x00029819
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x0002B630 File Offset: 0x00029830
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

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0002B648 File Offset: 0x00029848
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x0002B65F File Offset: 0x0002985F
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

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0002B677 File Offset: 0x00029877
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x0002B68E File Offset: 0x0002988E
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

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0002B6A6 File Offset: 0x000298A6
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x0002B6BD File Offset: 0x000298BD
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

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0002B6D5 File Offset: 0x000298D5
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x0002B6EC File Offset: 0x000298EC
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

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002B704 File Offset: 0x00029904
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0002B71B File Offset: 0x0002991B
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

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002B733 File Offset: 0x00029933
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x0002B74A File Offset: 0x0002994A
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

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0002B762 File Offset: 0x00029962
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0002B779 File Offset: 0x00029979
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

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002B791 File Offset: 0x00029991
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0002B7A8 File Offset: 0x000299A8
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

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0002B7C0 File Offset: 0x000299C0
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x0002B7D7 File Offset: 0x000299D7
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

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0002B7EF File Offset: 0x000299EF
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x0002B806 File Offset: 0x00029A06
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

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0002B81E File Offset: 0x00029A1E
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x0002B835 File Offset: 0x00029A35
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

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0002B848 File Offset: 0x00029A48
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x0002B85F File Offset: 0x00029A5F
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

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0002B877 File Offset: 0x00029A77
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x0002B88E File Offset: 0x00029A8E
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

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0002B8A6 File Offset: 0x00029AA6
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x0002B8BD File Offset: 0x00029ABD
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

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0002B8D0 File Offset: 0x00029AD0
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x0002B8E7 File Offset: 0x00029AE7
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

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0002B8FA File Offset: 0x00029AFA
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x0002B911 File Offset: 0x00029B11
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

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0002B929 File Offset: 0x00029B29
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x0002B940 File Offset: 0x00029B40
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

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0002B953 File Offset: 0x00029B53
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x0002B96A File Offset: 0x00029B6A
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

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0002B97D File Offset: 0x00029B7D
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0002B994 File Offset: 0x00029B94
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

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0002B9A7 File Offset: 0x00029BA7
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0002B9BE File Offset: 0x00029BBE
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

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0002B9D1 File Offset: 0x00029BD1
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x0002B9E8 File Offset: 0x00029BE8
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

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002BA00 File Offset: 0x00029C00
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x0002BA17 File Offset: 0x00029C17
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

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002BA2F File Offset: 0x00029C2F
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x0002BA46 File Offset: 0x00029C46
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

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0002BA5E File Offset: 0x00029C5E
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x0002BA75 File Offset: 0x00029C75
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

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002BA8D File Offset: 0x00029C8D
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x0002BAA4 File Offset: 0x00029CA4
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

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002BABC File Offset: 0x00029CBC
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x0002BAD3 File Offset: 0x00029CD3
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

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0002BAEB File Offset: 0x00029CEB
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x0002BB02 File Offset: 0x00029D02
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

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0002BB1A File Offset: 0x00029D1A
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x0002BB31 File Offset: 0x00029D31
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

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002BB4C File Offset: 0x00029D4C
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
