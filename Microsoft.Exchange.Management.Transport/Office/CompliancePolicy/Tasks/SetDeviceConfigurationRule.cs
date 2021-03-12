using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000111 RID: 273
	[Cmdlet("Set", "DeviceConfigurationRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDeviceConfigurationRule : SetDeviceRuleBase
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x0002D374 File Offset: 0x0002B574
		public SetDeviceConfigurationRule() : base(PolicyScenario.DeviceSettings)
		{
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002D37D File Offset: 0x0002B57D
		protected override DeviceRuleBase CreateDeviceRule(RuleStorage dataObject)
		{
			return new DeviceConfigurationRule(dataObject);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002D388 File Offset: 0x0002B588
		protected override void ValidateUnacceptableParameter()
		{
			if (this.Identity != null && !DevicePolicyUtility.IsDeviceConfigurationRule(this.Identity.ToString()))
			{
				base.WriteError(new ArgumentException(Strings.CanOnlyManipulateDeviceConfigurationRule), ErrorCategory.InvalidArgument, null);
			}
			if (DevicePolicyUtility.IsPropertySpecified(base.DynamicParametersInstance, ADObjectSchema.Name))
			{
				base.WriteError(new ArgumentException(Strings.CannotChangeDeviceConfigurationRuleName), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0002D3EF File Offset: 0x0002B5EF
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x0002D406 File Offset: 0x0002B606
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

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0002D41E File Offset: 0x0002B61E
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x0002D435 File Offset: 0x0002B635
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

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0002D44D File Offset: 0x0002B64D
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x0002D464 File Offset: 0x0002B664
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

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0002D47C File Offset: 0x0002B67C
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x0002D493 File Offset: 0x0002B693
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

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0002D4AB File Offset: 0x0002B6AB
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x0002D4C2 File Offset: 0x0002B6C2
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

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0002D4DA File Offset: 0x0002B6DA
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x0002D4F1 File Offset: 0x0002B6F1
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

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0002D509 File Offset: 0x0002B709
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x0002D520 File Offset: 0x0002B720
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

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0002D538 File Offset: 0x0002B738
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x0002D54F File Offset: 0x0002B74F
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

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0002D567 File Offset: 0x0002B767
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x0002D57E File Offset: 0x0002B77E
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

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0002D596 File Offset: 0x0002B796
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x0002D5AD File Offset: 0x0002B7AD
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

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0002D5C5 File Offset: 0x0002B7C5
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x0002D5DC File Offset: 0x0002B7DC
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

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0002D5F4 File Offset: 0x0002B7F4
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x0002D60B File Offset: 0x0002B80B
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

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0002D623 File Offset: 0x0002B823
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x0002D63A File Offset: 0x0002B83A
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

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0002D652 File Offset: 0x0002B852
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x0002D669 File Offset: 0x0002B869
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

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0002D681 File Offset: 0x0002B881
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x0002D698 File Offset: 0x0002B898
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

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0002D6B0 File Offset: 0x0002B8B0
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x0002D6C7 File Offset: 0x0002B8C7
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

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0002D6DF File Offset: 0x0002B8DF
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x0002D6F6 File Offset: 0x0002B8F6
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

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0002D70E File Offset: 0x0002B90E
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0002D725 File Offset: 0x0002B925
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

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0002D73D File Offset: 0x0002B93D
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x0002D754 File Offset: 0x0002B954
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

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0002D76C File Offset: 0x0002B96C
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x0002D783 File Offset: 0x0002B983
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

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0002D79B File Offset: 0x0002B99B
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x0002D7B2 File Offset: 0x0002B9B2
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

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0002D7CA File Offset: 0x0002B9CA
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x0002D7E1 File Offset: 0x0002B9E1
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

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0002D7F9 File Offset: 0x0002B9F9
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x0002D810 File Offset: 0x0002BA10
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

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0002D828 File Offset: 0x0002BA28
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x0002D83F File Offset: 0x0002BA3F
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

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0002D857 File Offset: 0x0002BA57
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x0002D86E File Offset: 0x0002BA6E
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

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0002D886 File Offset: 0x0002BA86
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x0002D89D File Offset: 0x0002BA9D
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

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0002D8B5 File Offset: 0x0002BAB5
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x0002D8CC File Offset: 0x0002BACC
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

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0002D8E4 File Offset: 0x0002BAE4
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x0002D8FB File Offset: 0x0002BAFB
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

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0002D913 File Offset: 0x0002BB13
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x0002D92A File Offset: 0x0002BB2A
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

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0002D942 File Offset: 0x0002BB42
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x0002D959 File Offset: 0x0002BB59
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

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0002D971 File Offset: 0x0002BB71
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x0002D988 File Offset: 0x0002BB88
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

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0002D9A0 File Offset: 0x0002BBA0
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0002D9B7 File Offset: 0x0002BBB7
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

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0002D9CF File Offset: 0x0002BBCF
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0002D9E6 File Offset: 0x0002BBE6
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

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0002D9FE File Offset: 0x0002BBFE
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x0002DA15 File Offset: 0x0002BC15
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

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0002DA2D File Offset: 0x0002BC2D
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x0002DA44 File Offset: 0x0002BC44
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

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0002DA5C File Offset: 0x0002BC5C
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x0002DA73 File Offset: 0x0002BC73
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

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0002DA8B File Offset: 0x0002BC8B
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x0002DAA2 File Offset: 0x0002BCA2
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

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0002DAB5 File Offset: 0x0002BCB5
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x0002DACC File Offset: 0x0002BCCC
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

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002DAE4 File Offset: 0x0002BCE4
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x0002DAFB File Offset: 0x0002BCFB
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

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0002DB13 File Offset: 0x0002BD13
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x0002DB2A File Offset: 0x0002BD2A
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

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0002DB3D File Offset: 0x0002BD3D
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x0002DB54 File Offset: 0x0002BD54
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

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0002DB67 File Offset: 0x0002BD67
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x0002DB7E File Offset: 0x0002BD7E
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

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0002DB96 File Offset: 0x0002BD96
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0002DBAD File Offset: 0x0002BDAD
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

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0002DBC0 File Offset: 0x0002BDC0
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x0002DBD7 File Offset: 0x0002BDD7
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

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0002DBEA File Offset: 0x0002BDEA
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x0002DC01 File Offset: 0x0002BE01
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

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0002DC14 File Offset: 0x0002BE14
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x0002DC2B File Offset: 0x0002BE2B
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

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002DC3E File Offset: 0x0002BE3E
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x0002DC55 File Offset: 0x0002BE55
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

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0002DC6D File Offset: 0x0002BE6D
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x0002DC84 File Offset: 0x0002BE84
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

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0002DC9C File Offset: 0x0002BE9C
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x0002DCB3 File Offset: 0x0002BEB3
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

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0002DCCB File Offset: 0x0002BECB
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x0002DCE2 File Offset: 0x0002BEE2
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

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0002DCFA File Offset: 0x0002BEFA
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x0002DD11 File Offset: 0x0002BF11
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

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0002DD29 File Offset: 0x0002BF29
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0002DD40 File Offset: 0x0002BF40
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

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0002DD58 File Offset: 0x0002BF58
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x0002DD6F File Offset: 0x0002BF6F
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

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0002DD87 File Offset: 0x0002BF87
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x0002DD9E File Offset: 0x0002BF9E
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

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002DDB8 File Offset: 0x0002BFB8
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
