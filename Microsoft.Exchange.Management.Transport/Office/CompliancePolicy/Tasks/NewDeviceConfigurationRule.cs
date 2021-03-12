using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000110 RID: 272
	[Cmdlet("New", "DeviceConfigurationRule", SupportsShouldProcess = true)]
	public sealed class NewDeviceConfigurationRule : NewDeviceRuleBase
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x0002C6E5 File Offset: 0x0002A8E5
		public NewDeviceConfigurationRule() : base(PolicyScenario.DeviceSettings)
		{
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002C6EE File Offset: 0x0002A8EE
		protected override DeviceRuleBase CreateDeviceRule(RuleStorage ruleStorage)
		{
			return new DeviceConfigurationRule(ruleStorage);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002C6F6 File Offset: 0x0002A8F6
		protected override Exception GetDeviceRuleAlreadyExistsException(string name)
		{
			return new DeviceConfigurationRuleAlreadyExistsException(name);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002C6FE File Offset: 0x0002A8FE
		protected override bool GetDeviceRuleGuidFromWorkload(Workload workload, out Guid ruleGuid)
		{
			ruleGuid = default(Guid);
			return DevicePolicyUtility.GetConfigurationRuleGuidFromWorkload(workload, out ruleGuid);
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0002C70E File Offset: 0x0002A90E
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x0002C725 File Offset: 0x0002A925
		[Parameter(Mandatory = false)]
		public bool? PasswordRequired
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Password_Required];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_Required] = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0002C73D File Offset: 0x0002A93D
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x0002C754 File Offset: 0x0002A954
		[Parameter(Mandatory = false)]
		public bool? PhoneMemoryEncrypted
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Encryption_PhoneMemoryEncrypted];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Encryption_PhoneMemoryEncrypted] = value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0002C76C File Offset: 0x0002A96C
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x0002C783 File Offset: 0x0002A983
		[Parameter(Mandatory = false)]
		public TimeSpan? PasswordTimeout
		{
			get
			{
				return (TimeSpan?)base.Fields[DeviceConfigurationRule.Device_Password_Timeout];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_Timeout] = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0002C79B File Offset: 0x0002A99B
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0002C7B2 File Offset: 0x0002A9B2
		[Parameter(Mandatory = false)]
		public int? PasswordMinimumLength
		{
			get
			{
				return (int?)base.Fields[DeviceConfigurationRule.Device_Password_MinimumLength];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_MinimumLength] = value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0002C7CA File Offset: 0x0002A9CA
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0002C7E1 File Offset: 0x0002A9E1
		[Parameter(Mandatory = false)]
		public int? PasswordHistoryCount
		{
			get
			{
				return (int?)base.Fields[DeviceConfigurationRule.Device_Password_History];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_History] = value;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0002C7F9 File Offset: 0x0002A9F9
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0002C810 File Offset: 0x0002AA10
		[Parameter(Mandatory = false)]
		public int? PasswordExpirationDays
		{
			get
			{
				return (int?)base.Fields[DeviceConfigurationRule.Device_Password_Expiration];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_Expiration] = value;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0002C828 File Offset: 0x0002AA28
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0002C83F File Offset: 0x0002AA3F
		[Parameter(Mandatory = false)]
		public int? MaxPasswordAttemptsBeforeWipe
		{
			get
			{
				return (int?)base.Fields[DeviceConfigurationRule.Device_Password_MaxAttemptsBeforeWipe];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_MaxAttemptsBeforeWipe] = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0002C857 File Offset: 0x0002AA57
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x0002C86E File Offset: 0x0002AA6E
		[Parameter(Mandatory = false)]
		public int? PasswordMinComplexChars
		{
			get
			{
				return (int?)base.Fields[DeviceConfigurationRule.Device_Password_MinComplexChars];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_MinComplexChars] = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0002C886 File Offset: 0x0002AA86
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0002C89D File Offset: 0x0002AA9D
		[Parameter(Mandatory = false)]
		public bool? AllowSimplePassword
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Password_AllowSimplePassword];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_AllowSimplePassword] = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002C8B5 File Offset: 0x0002AAB5
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0002C8CC File Offset: 0x0002AACC
		[Parameter(Mandatory = false)]
		public bool? EnableRemovableStorage
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Security_EnableRemovableStorage];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Security_EnableRemovableStorage] = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002C8E4 File Offset: 0x0002AAE4
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0002C8FB File Offset: 0x0002AAFB
		[Parameter(Mandatory = false)]
		public bool? CameraEnabled
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Security_CameraEnabled];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Security_CameraEnabled] = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0002C913 File Offset: 0x0002AB13
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x0002C92A File Offset: 0x0002AB2A
		[Parameter(Mandatory = false)]
		public bool? BluetoothEnabled
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Security_BluetoothEnabled];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Security_BluetoothEnabled] = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0002C942 File Offset: 0x0002AB42
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x0002C959 File Offset: 0x0002AB59
		[Parameter(Mandatory = false)]
		public bool? ForceEncryptedBackup
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Cloud_ForceEncryptedBackup];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Cloud_ForceEncryptedBackup] = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0002C971 File Offset: 0x0002AB71
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x0002C988 File Offset: 0x0002AB88
		[Parameter(Mandatory = false)]
		public bool? AllowiCloudDocSync
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Cloud_AllowiCloudDocSync];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Cloud_AllowiCloudDocSync] = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0002C9A0 File Offset: 0x0002ABA0
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x0002C9B7 File Offset: 0x0002ABB7
		[Parameter(Mandatory = false)]
		public bool? AllowiCloudPhotoSync
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Cloud_AllowiCloudPhotoSync];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Cloud_AllowiCloudPhotoSync] = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002C9CF File Offset: 0x0002ABCF
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x0002C9E6 File Offset: 0x0002ABE6
		[Parameter(Mandatory = false)]
		public bool? AllowiCloudBackup
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Cloud_AllowiCloudBackup];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Cloud_AllowiCloudBackup] = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0002C9FE File Offset: 0x0002ABFE
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x0002CA15 File Offset: 0x0002AC15
		[Parameter(Mandatory = false)]
		public RatingRegionEntry? RegionRatings
		{
			get
			{
				return (RatingRegionEntry?)base.Fields[DeviceConfigurationRule.Device_Restrictions_RatingsRegion];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_RatingsRegion] = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002CA2D File Offset: 0x0002AC2D
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0002CA44 File Offset: 0x0002AC44
		[Parameter(Mandatory = false)]
		public RatingMovieEntry? MoviesRating
		{
			get
			{
				return (RatingMovieEntry?)base.Fields[DeviceConfigurationRule.Device_Restrictions_RatingMovies];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_RatingMovies] = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0002CA5C File Offset: 0x0002AC5C
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x0002CA73 File Offset: 0x0002AC73
		[Parameter(Mandatory = false)]
		public RatingTvShowEntry? TVShowsRating
		{
			get
			{
				return (RatingTvShowEntry?)base.Fields[DeviceConfigurationRule.Device_Restrictions_RatingTVShows];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_RatingTVShows] = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0002CA8B File Offset: 0x0002AC8B
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0002CAA2 File Offset: 0x0002ACA2
		[Parameter(Mandatory = false)]
		public RatingAppsEntry? AppsRating
		{
			get
			{
				return (RatingAppsEntry?)base.Fields[DeviceConfigurationRule.Device_Restrictions_RatingApps];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_RatingApps] = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0002CABA File Offset: 0x0002ACBA
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x0002CAD1 File Offset: 0x0002ACD1
		[Parameter(Mandatory = false)]
		public bool? AllowVoiceDialing
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowVoiceDialing];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowVoiceDialing] = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0002CAE9 File Offset: 0x0002ACE9
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x0002CB00 File Offset: 0x0002AD00
		[Parameter(Mandatory = false)]
		public bool? AllowVoiceAssistant
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowVoiceAssistant];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowVoiceAssistant] = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002CB18 File Offset: 0x0002AD18
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x0002CB2F File Offset: 0x0002AD2F
		[Parameter(Mandatory = false)]
		public bool? AllowAssistantWhileLocked
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowAssistantWhileLocked];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowAssistantWhileLocked] = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0002CB47 File Offset: 0x0002AD47
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x0002CB5E File Offset: 0x0002AD5E
		[Parameter(Mandatory = false)]
		public bool? AllowScreenshot
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowScreenshot];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowScreenshot] = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0002CB76 File Offset: 0x0002AD76
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0002CB8D File Offset: 0x0002AD8D
		[Parameter(Mandatory = false)]
		public bool? AllowVideoConferencing
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowVideoConferencing];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowVideoConferencing] = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0002CBA5 File Offset: 0x0002ADA5
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x0002CBBC File Offset: 0x0002ADBC
		[Parameter(Mandatory = false)]
		public bool? AllowPassbookWhileLocked
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowPassbookWhileLocked];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowPassbookWhileLocked] = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002CBD4 File Offset: 0x0002ADD4
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x0002CBEB File Offset: 0x0002ADEB
		[Parameter(Mandatory = false)]
		public bool? AllowDiagnosticSubmission
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowDiagnosticSubmission];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowDiagnosticSubmission] = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002CC03 File Offset: 0x0002AE03
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0002CC1A File Offset: 0x0002AE1A
		[Parameter(Mandatory = false)]
		public bool? AllowConvenienceLogon
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Password_AllowConvenienceLogon];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_AllowConvenienceLogon] = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0002CC32 File Offset: 0x0002AE32
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x0002CC49 File Offset: 0x0002AE49
		[Parameter(Mandatory = false)]
		public TimeSpan? MaxPasswordGracePeriod
		{
			get
			{
				return (TimeSpan?)base.Fields[DeviceConfigurationRule.Device_Password_MaxGracePeriod];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_MaxGracePeriod] = value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0002CC61 File Offset: 0x0002AE61
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x0002CC78 File Offset: 0x0002AE78
		[Parameter(Mandatory = false)]
		public int? PasswordQuality
		{
			get
			{
				return (int?)base.Fields[DeviceConfigurationRule.Device_Password_PasswordQuality];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_PasswordQuality] = value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0002CC90 File Offset: 0x0002AE90
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x0002CCA7 File Offset: 0x0002AEA7
		[Parameter(Mandatory = false)]
		public bool? AllowAppStore
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowAppStore];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_AllowAppStore] = value;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0002CCBF File Offset: 0x0002AEBF
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x0002CCD6 File Offset: 0x0002AED6
		[Parameter(Mandatory = false)]
		public bool? ForceAppStorePassword
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Restrictions_ForceAppStorePassword];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Restrictions_ForceAppStorePassword] = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0002CCEE File Offset: 0x0002AEEE
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x0002CD05 File Offset: 0x0002AF05
		[Parameter(Mandatory = false)]
		public bool? SystemSecurityTLS
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_SystemSecurity_TLS];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_SystemSecurity_TLS] = value;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0002CD1D File Offset: 0x0002AF1D
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x0002CD34 File Offset: 0x0002AF34
		[Parameter(Mandatory = false)]
		public UserAccountControlStatusEntry? UserAccountControlStatus
		{
			get
			{
				return (UserAccountControlStatusEntry?)base.Fields[DeviceConfigurationRule.Device_SystemSecurity_UserAccountControlStatus];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_SystemSecurity_UserAccountControlStatus] = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0002CD4C File Offset: 0x0002AF4C
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x0002CD63 File Offset: 0x0002AF63
		[Parameter(Mandatory = false)]
		public FirewallStatusEntry? FirewallStatus
		{
			get
			{
				return (FirewallStatusEntry?)base.Fields[DeviceConfigurationRule.Device_SystemSecurity_FirewallStatus];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_SystemSecurity_FirewallStatus] = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0002CD7B File Offset: 0x0002AF7B
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x0002CD92 File Offset: 0x0002AF92
		[Parameter(Mandatory = false)]
		public AutoUpdateStatusEntry? AutoUpdateStatus
		{
			get
			{
				return (AutoUpdateStatusEntry?)base.Fields[DeviceConfigurationRule.Device_SystemSecurity_AutoUpdateStatus];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_SystemSecurity_AutoUpdateStatus] = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0002CDAA File Offset: 0x0002AFAA
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x0002CDC1 File Offset: 0x0002AFC1
		[Parameter(Mandatory = false)]
		public string AntiVirusStatus
		{
			get
			{
				return (string)base.Fields[DeviceConfigurationRule.Device_SystemSecurity_AntiVirusStatus];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_SystemSecurity_AntiVirusStatus] = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002CDD4 File Offset: 0x0002AFD4
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x0002CDEB File Offset: 0x0002AFEB
		[Parameter(Mandatory = false)]
		public bool? AntiVirusSignatureStatus
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_SystemSecurity_AntiVirusSignatureStatus];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_SystemSecurity_AntiVirusSignatureStatus] = value;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0002CE03 File Offset: 0x0002B003
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x0002CE1A File Offset: 0x0002B01A
		[Parameter(Mandatory = false)]
		public bool? SmartScreenEnabled
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_InternetExplorer_SmartScreenEnabled];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_InternetExplorer_SmartScreenEnabled] = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0002CE32 File Offset: 0x0002B032
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0002CE49 File Offset: 0x0002B049
		[Parameter(Mandatory = false)]
		public string WorkFoldersSyncUrl
		{
			get
			{
				return (string)base.Fields[DeviceConfigurationRule.Device_WorkFolders_SyncUrl];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_WorkFolders_SyncUrl] = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0002CE5C File Offset: 0x0002B05C
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x0002CE73 File Offset: 0x0002B073
		[Parameter(Mandatory = false)]
		public string PasswordComplexity
		{
			get
			{
				return (string)base.Fields[DeviceConfigurationRule.Device_Password_Type];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Password_Type] = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0002CE86 File Offset: 0x0002B086
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0002CE9D File Offset: 0x0002B09D
		[Parameter(Mandatory = false)]
		public bool? WLANEnabled
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Device_Wireless_WLANEnabled];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Device_Wireless_WLANEnabled] = value;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0002CEB5 File Offset: 0x0002B0B5
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0002CECC File Offset: 0x0002B0CC
		[Parameter(Mandatory = false)]
		public string AccountName
		{
			get
			{
				return (string)base.Fields[DeviceConfigurationRule.Eas_AccountName];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_AccountName] = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0002CEDF File Offset: 0x0002B0DF
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0002CEF6 File Offset: 0x0002B0F6
		[Parameter(Mandatory = false)]
		public string AccountUserName
		{
			get
			{
				return (string)base.Fields[DeviceConfigurationRule.Eas_UserName];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_UserName] = value;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0002CF09 File Offset: 0x0002B109
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0002CF20 File Offset: 0x0002B120
		[Parameter(Mandatory = false)]
		public string ExchangeActiveSyncHost
		{
			get
			{
				return (string)base.Fields[DeviceConfigurationRule.Eas_Host];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_Host] = value;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0002CF33 File Offset: 0x0002B133
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x0002CF4A File Offset: 0x0002B14A
		[Parameter(Mandatory = false)]
		public string EmailAddress
		{
			get
			{
				return (string)base.Fields[DeviceConfigurationRule.Eas_EmailAddress];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_EmailAddress] = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0002CF5D File Offset: 0x0002B15D
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x0002CF74 File Offset: 0x0002B174
		[Parameter(Mandatory = false)]
		public bool? UseSSL
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Eas_UseSSL];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_UseSSL] = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0002CF8C File Offset: 0x0002B18C
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x0002CFA3 File Offset: 0x0002B1A3
		[Parameter(Mandatory = false)]
		public bool? AllowMove
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Eas_PreventAppSheet];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_PreventAppSheet] = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002CFBB File Offset: 0x0002B1BB
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0002CFD2 File Offset: 0x0002B1D2
		[Parameter(Mandatory = false)]
		public bool? AllowRecentAddressSyncing
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Eas_DisableMailRecentsSyncing];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_DisableMailRecentsSyncing] = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002CFEA File Offset: 0x0002B1EA
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0002D001 File Offset: 0x0002B201
		[Parameter(Mandatory = false)]
		public long? DaysToSync
		{
			get
			{
				return (long?)base.Fields[DeviceConfigurationRule.Eas_MailNumberOfPastDaysToSync];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_MailNumberOfPastDaysToSync] = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0002D019 File Offset: 0x0002B219
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0002D030 File Offset: 0x0002B230
		[Parameter(Mandatory = false)]
		public long? ContentType
		{
			get
			{
				return (long?)base.Fields[DeviceConfigurationRule.Eas_ContentTypeToSync];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_ContentTypeToSync] = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002D048 File Offset: 0x0002B248
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0002D05F File Offset: 0x0002B25F
		[Parameter(Mandatory = false)]
		public bool? UseSMIME
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Eas_UseSMIME];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_UseSMIME] = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0002D077 File Offset: 0x0002B277
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0002D08E File Offset: 0x0002B28E
		[Parameter(Mandatory = false)]
		public long? SyncSchedule
		{
			get
			{
				return (long?)base.Fields[DeviceConfigurationRule.Eas_SyncSchedule];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_SyncSchedule] = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0002D0A6 File Offset: 0x0002B2A6
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x0002D0BD File Offset: 0x0002B2BD
		[Parameter(Mandatory = false)]
		public bool? UseOnlyInEmail
		{
			get
			{
				return (bool?)base.Fields[DeviceConfigurationRule.Eas_PreventMove];
			}
			set
			{
				base.Fields[DeviceConfigurationRule.Eas_PreventMove] = value;
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002D0D8 File Offset: 0x0002B2D8
		protected override void SetPropsOnDeviceRule(DeviceRuleBase pdeviceRule)
		{
			DeviceConfigurationRule deviceConfigurationRule = (DeviceConfigurationRule)pdeviceRule;
			deviceConfigurationRule.PasswordRequired = this.PasswordRequired;
			deviceConfigurationRule.PhoneMemoryEncrypted = this.PhoneMemoryEncrypted;
			deviceConfigurationRule.PasswordTimeout = this.PasswordTimeout;
			deviceConfigurationRule.PasswordMinimumLength = this.PasswordMinimumLength;
			deviceConfigurationRule.PasswordHistoryCount = this.PasswordHistoryCount;
			deviceConfigurationRule.PasswordExpirationDays = this.PasswordExpirationDays;
			deviceConfigurationRule.MaxPasswordAttemptsBeforeWipe = this.MaxPasswordAttemptsBeforeWipe;
			deviceConfigurationRule.PasswordMinComplexChars = this.PasswordMinComplexChars;
			deviceConfigurationRule.AllowSimplePassword = this.AllowSimplePassword;
			deviceConfigurationRule.EnableRemovableStorage = this.EnableRemovableStorage;
			deviceConfigurationRule.CameraEnabled = this.CameraEnabled;
			deviceConfigurationRule.BluetoothEnabled = this.BluetoothEnabled;
			deviceConfigurationRule.ForceEncryptedBackup = this.ForceEncryptedBackup;
			deviceConfigurationRule.AllowiCloudDocSync = this.AllowiCloudDocSync;
			deviceConfigurationRule.AllowiCloudPhotoSync = this.AllowiCloudPhotoSync;
			deviceConfigurationRule.AllowiCloudBackup = this.AllowiCloudBackup;
			deviceConfigurationRule.RegionRatings = this.RegionRatings;
			deviceConfigurationRule.MoviesRating = this.MoviesRating;
			deviceConfigurationRule.TVShowsRating = this.TVShowsRating;
			deviceConfigurationRule.AppsRating = this.AppsRating;
			deviceConfigurationRule.AllowVoiceDialing = this.AllowVoiceDialing;
			deviceConfigurationRule.AllowVoiceAssistant = this.AllowVoiceAssistant;
			deviceConfigurationRule.AllowAssistantWhileLocked = this.AllowAssistantWhileLocked;
			deviceConfigurationRule.AllowScreenshot = this.AllowScreenshot;
			deviceConfigurationRule.AllowVideoConferencing = this.AllowVideoConferencing;
			deviceConfigurationRule.AllowPassbookWhileLocked = this.AllowPassbookWhileLocked;
			deviceConfigurationRule.AllowDiagnosticSubmission = this.AllowDiagnosticSubmission;
			deviceConfigurationRule.AllowConvenienceLogon = this.AllowConvenienceLogon;
			deviceConfigurationRule.MaxPasswordGracePeriod = this.MaxPasswordGracePeriod;
			deviceConfigurationRule.PasswordQuality = this.PasswordQuality;
			deviceConfigurationRule.AllowAppStore = this.AllowAppStore;
			deviceConfigurationRule.ForceAppStorePassword = this.ForceAppStorePassword;
			deviceConfigurationRule.SystemSecurityTLS = this.SystemSecurityTLS;
			deviceConfigurationRule.UserAccountControlStatus = this.UserAccountControlStatus;
			deviceConfigurationRule.FirewallStatus = this.FirewallStatus;
			deviceConfigurationRule.AutoUpdateStatus = this.AutoUpdateStatus;
			deviceConfigurationRule.AntiVirusStatus = this.AntiVirusStatus;
			deviceConfigurationRule.AntiVirusSignatureStatus = this.AntiVirusSignatureStatus;
			deviceConfigurationRule.SmartScreenEnabled = this.SmartScreenEnabled;
			deviceConfigurationRule.WorkFoldersSyncUrl = this.WorkFoldersSyncUrl;
			deviceConfigurationRule.PasswordComplexity = this.PasswordComplexity;
			deviceConfigurationRule.WLANEnabled = this.WLANEnabled;
			deviceConfigurationRule.AccountName = this.AccountName;
			deviceConfigurationRule.AccountUserName = this.AccountUserName;
			deviceConfigurationRule.ExchangeActiveSyncHost = this.ExchangeActiveSyncHost;
			deviceConfigurationRule.EmailAddress = this.EmailAddress;
			deviceConfigurationRule.UseSSL = this.UseSSL;
			deviceConfigurationRule.AllowMove = this.AllowMove;
			deviceConfigurationRule.AllowRecentAddressSyncing = this.AllowRecentAddressSyncing;
			deviceConfigurationRule.DaysToSync = this.DaysToSync;
			deviceConfigurationRule.ContentType = this.ContentType;
			deviceConfigurationRule.UseSMIME = this.UseSMIME;
			deviceConfigurationRule.SyncSchedule = this.SyncSchedule;
			deviceConfigurationRule.UseOnlyInEmail = this.UseOnlyInEmail;
		}
	}
}
