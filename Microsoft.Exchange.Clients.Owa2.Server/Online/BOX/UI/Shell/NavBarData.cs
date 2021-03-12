using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.UI.Shell
{
	// Token: 0x0200007B RID: 123
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "NavBarData", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.UI.Shell")]
	public class NavBarData : IExtensibleDataObject
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000E176 File Offset: 0x0000C376
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x0000E17E File Offset: 0x0000C37E
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000E187 File Offset: 0x0000C387
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0000E18F File Offset: 0x0000C38F
		[DataMember]
		public NavBarLinkData AboutMeLink
		{
			get
			{
				return this.AboutMeLinkField;
			}
			set
			{
				this.AboutMeLinkField = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000E198 File Offset: 0x0000C398
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		[DataMember]
		public NavBarLinkData AdminLink
		{
			get
			{
				return this.AdminLinkField;
			}
			set
			{
				this.AdminLinkField = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000E1A9 File Offset: 0x0000C3A9
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0000E1B1 File Offset: 0x0000C3B1
		[DataMember]
		public NavBarImageData AppsImage
		{
			get
			{
				return this.AppsImageField;
			}
			set
			{
				this.AppsImageField = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000E1BA File Offset: 0x0000C3BA
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000E1C2 File Offset: 0x0000C3C2
		[DataMember]
		public NavBarLinkData[] AppsLinks
		{
			get
			{
				return this.AppsLinksField;
			}
			set
			{
				this.AppsLinksField = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000E1CB File Offset: 0x0000C3CB
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000E1D3 File Offset: 0x0000C3D3
		[DataMember]
		public string ClientData
		{
			get
			{
				return this.ClientDataField;
			}
			set
			{
				this.ClientDataField = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		[DataMember]
		public NavBarLinkData CommunityLink
		{
			get
			{
				return this.CommunityLinkField;
			}
			set
			{
				this.CommunityLinkField = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000E1ED File Offset: 0x0000C3ED
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0000E1F5 File Offset: 0x0000C3F5
		[DataMember]
		public string CompanyDisplayName
		{
			get
			{
				return this.CompanyDisplayNameField;
			}
			set
			{
				this.CompanyDisplayNameField = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000E1FE File Offset: 0x0000C3FE
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0000E206 File Offset: 0x0000C406
		[DataMember]
		public string CorrelationID
		{
			get
			{
				return this.CorrelationIDField;
			}
			set
			{
				this.CorrelationIDField = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000E20F File Offset: 0x0000C40F
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0000E217 File Offset: 0x0000C417
		[DataMember]
		public string CultureName
		{
			get
			{
				return this.CultureNameField;
			}
			set
			{
				this.CultureNameField = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000E220 File Offset: 0x0000C420
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x0000E228 File Offset: 0x0000C428
		[DataMember]
		public string CurrentMainLinkElementID
		{
			get
			{
				return this.CurrentMainLinkElementIDField;
			}
			set
			{
				this.CurrentMainLinkElementIDField = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000E231 File Offset: 0x0000C431
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000E239 File Offset: 0x0000C439
		[DataMember]
		public NavBarLinkData[] CurrentWorkloadHelpSubLinks
		{
			get
			{
				return this.CurrentWorkloadHelpSubLinksField;
			}
			set
			{
				this.CurrentWorkloadHelpSubLinksField = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000E242 File Offset: 0x0000C442
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000E24A File Offset: 0x0000C44A
		[DataMember]
		public NavBarLinkData CurrentWorkloadSettingsLink
		{
			get
			{
				return this.CurrentWorkloadSettingsLinkField;
			}
			set
			{
				this.CurrentWorkloadSettingsLinkField = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000E253 File Offset: 0x0000C453
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000E25B File Offset: 0x0000C45B
		[DataMember]
		public NavBarLinkData[] CurrentWorkloadSettingsSubLinks
		{
			get
			{
				return this.CurrentWorkloadSettingsSubLinksField;
			}
			set
			{
				this.CurrentWorkloadSettingsSubLinksField = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000E264 File Offset: 0x0000C464
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000E26C File Offset: 0x0000C46C
		[DataMember]
		public NavBarLinkData[] CurrentWorkloadUserSubLinks
		{
			get
			{
				return this.CurrentWorkloadUserSubLinksField;
			}
			set
			{
				this.CurrentWorkloadUserSubLinksField = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000E275 File Offset: 0x0000C475
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000E27D File Offset: 0x0000C47D
		[DataMember]
		public ShellDimensions Dimensions
		{
			get
			{
				return this.DimensionsField;
			}
			set
			{
				this.DimensionsField = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000E286 File Offset: 0x0000C486
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000E28E File Offset: 0x0000C48E
		[DataMember]
		public NavBarImageData DownArrowImage
		{
			get
			{
				return this.DownArrowImageField;
			}
			set
			{
				this.DownArrowImageField = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000E297 File Offset: 0x0000C497
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000E29F File Offset: 0x0000C49F
		[DataMember]
		public NavBarImageData DownWhiteArrowImage
		{
			get
			{
				return this.DownWhiteArrowImageField;
			}
			set
			{
				this.DownWhiteArrowImageField = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000E2A8 File Offset: 0x0000C4A8
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		[DataMember]
		public NavBarLinkData FeedbackLink
		{
			get
			{
				return this.FeedbackLinkField;
			}
			set
			{
				this.FeedbackLinkField = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000E2B9 File Offset: 0x0000C4B9
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000E2C1 File Offset: 0x0000C4C1
		[DataMember]
		public string FlightName
		{
			get
			{
				return this.FlightNameField;
			}
			set
			{
				this.FlightNameField = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000E2CA File Offset: 0x0000C4CA
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000E2D2 File Offset: 0x0000C4D2
		[DataMember]
		public bool FlipHelpIcon
		{
			get
			{
				return this.FlipHelpIconField;
			}
			set
			{
				this.FlipHelpIconField = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000E2DB File Offset: 0x0000C4DB
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000E2E3 File Offset: 0x0000C4E3
		[DataMember]
		public string FooterCopyrightLogoTitle
		{
			get
			{
				return this.FooterCopyrightLogoTitleField;
			}
			set
			{
				this.FooterCopyrightLogoTitleField = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		[DataMember]
		public string FooterCopyrightText
		{
			get
			{
				return this.FooterCopyrightTextField;
			}
			set
			{
				this.FooterCopyrightTextField = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000E2FD File Offset: 0x0000C4FD
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x0000E305 File Offset: 0x0000C505
		[DataMember]
		public NavBarLinkData FooterICPLink
		{
			get
			{
				return this.FooterICPLinkField;
			}
			set
			{
				this.FooterICPLinkField = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000E30E File Offset: 0x0000C50E
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0000E316 File Offset: 0x0000C516
		[DataMember]
		public NavBarImageData FooterLogoImage
		{
			get
			{
				return this.FooterLogoImageField;
			}
			set
			{
				this.FooterLogoImageField = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000E31F File Offset: 0x0000C51F
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0000E327 File Offset: 0x0000C527
		[DataMember]
		public bool HasTenantBranding
		{
			get
			{
				return this.HasTenantBrandingField;
			}
			set
			{
				this.HasTenantBrandingField = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000E330 File Offset: 0x0000C530
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000E338 File Offset: 0x0000C538
		[DataMember]
		public NavBarImageData HelpImage
		{
			get
			{
				return this.HelpImageField;
			}
			set
			{
				this.HelpImageField = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000E341 File Offset: 0x0000C541
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000E349 File Offset: 0x0000C549
		[DataMember]
		public NavBarLinkData HelpLink
		{
			get
			{
				return this.HelpLinkField;
			}
			set
			{
				this.HelpLinkField = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000E352 File Offset: 0x0000C552
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000E35A File Offset: 0x0000C55A
		[DataMember]
		public string IPv6Text
		{
			get
			{
				return this.IPv6TextField;
			}
			set
			{
				this.IPv6TextField = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000E363 File Offset: 0x0000C563
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0000E36B File Offset: 0x0000C56B
		[DataMember]
		public string ImageClusterUrl
		{
			get
			{
				return this.ImageClusterUrlField;
			}
			set
			{
				this.ImageClusterUrlField = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000E374 File Offset: 0x0000C574
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0000E37C File Offset: 0x0000C57C
		[DataMember]
		public bool IsAuthenticated
		{
			get
			{
				return this.IsAuthenticatedField;
			}
			set
			{
				this.IsAuthenticatedField = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000E385 File Offset: 0x0000C585
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000E38D File Offset: 0x0000C58D
		[DataMember]
		public NavBarLinkData LegalLink
		{
			get
			{
				return this.LegalLinkField;
			}
			set
			{
				this.LegalLinkField = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000E396 File Offset: 0x0000C596
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000E39E File Offset: 0x0000C59E
		[DataMember]
		public string LogoIconID
		{
			get
			{
				return this.LogoIconIDField;
			}
			set
			{
				this.LogoIconIDField = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000E3A7 File Offset: 0x0000C5A7
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0000E3AF File Offset: 0x0000C5AF
		[DataMember]
		public NavBarImageData LogoImage
		{
			get
			{
				return this.LogoImageField;
			}
			set
			{
				this.LogoImageField = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000E3C0 File Offset: 0x0000C5C0
		[DataMember]
		public string LogoNavigationUrl
		{
			get
			{
				return this.LogoNavigationUrlField;
			}
			set
			{
				this.LogoNavigationUrlField = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000E3C9 File Offset: 0x0000C5C9
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000E3D1 File Offset: 0x0000C5D1
		[DataMember]
		public NavBarImageData LogoThemeableImage
		{
			get
			{
				return this.LogoThemeableImageField;
			}
			set
			{
				this.LogoThemeableImageField = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000E3DA File Offset: 0x0000C5DA
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000E3E2 File Offset: 0x0000C5E2
		[DataMember]
		public string MenuTitleText
		{
			get
			{
				return this.MenuTitleTextField;
			}
			set
			{
				this.MenuTitleTextField = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000E3EB File Offset: 0x0000C5EB
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000E3F3 File Offset: 0x0000C5F3
		[DataMember]
		public string MyProfileUrl
		{
			get
			{
				return this.MyProfileUrlField;
			}
			set
			{
				this.MyProfileUrlField = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0000E404 File Offset: 0x0000C604
		[DataMember]
		public string NavBarAriaLabel
		{
			get
			{
				return this.NavBarAriaLabelField;
			}
			set
			{
				this.NavBarAriaLabelField = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000E40D File Offset: 0x0000C60D
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000E415 File Offset: 0x0000C615
		[DataMember]
		public NavBarImageData NotificationsBellIconImage
		{
			get
			{
				return this.NotificationsBellIconImageField;
			}
			set
			{
				this.NotificationsBellIconImageField = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000E41E File Offset: 0x0000C61E
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000E426 File Offset: 0x0000C626
		[DataMember]
		public string NotificationsCountLabelText
		{
			get
			{
				return this.NotificationsCountLabelTextField;
			}
			set
			{
				this.NotificationsCountLabelTextField = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000E42F File Offset: 0x0000C62F
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000E437 File Offset: 0x0000C637
		[DataMember]
		public NavBarImageData NotificationsHighIconImage
		{
			get
			{
				return this.NotificationsHighIconImageField;
			}
			set
			{
				this.NotificationsHighIconImageField = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000E440 File Offset: 0x0000C640
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000E448 File Offset: 0x0000C648
		[DataMember]
		public NavBarImageData NotificationsLowIconImage
		{
			get
			{
				return this.NotificationsLowIconImageField;
			}
			set
			{
				this.NotificationsLowIconImageField = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000E451 File Offset: 0x0000C651
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000E459 File Offset: 0x0000C659
		[DataMember]
		public NavBarImageData NotificationsMediumIconImage
		{
			get
			{
				return this.NotificationsMediumIconImageField;
			}
			set
			{
				this.NotificationsMediumIconImageField = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000E462 File Offset: 0x0000C662
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000E46A File Offset: 0x0000C66A
		[DataMember]
		public string NotificationsPopupHeaderText
		{
			get
			{
				return this.NotificationsPopupHeaderTextField;
			}
			set
			{
				this.NotificationsPopupHeaderTextField = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000E473 File Offset: 0x0000C673
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0000E47B File Offset: 0x0000C67B
		[DataMember]
		public NavBarUnclusteredImageData NotificationsProgressIconImage
		{
			get
			{
				return this.NotificationsProgressIconImageField;
			}
			set
			{
				this.NotificationsProgressIconImageField = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000E484 File Offset: 0x0000C684
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x0000E48C File Offset: 0x0000C68C
		[DataMember]
		public NavBarLinkData O365SettingsLink
		{
			get
			{
				return this.O365SettingsLinkField;
			}
			set
			{
				this.O365SettingsLinkField = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000E495 File Offset: 0x0000C695
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0000E49D File Offset: 0x0000C69D
		[DataMember]
		public NavBarLinkData PartnerLink
		{
			get
			{
				return this.PartnerLinkField;
			}
			set
			{
				this.PartnerLinkField = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000E4A6 File Offset: 0x0000C6A6
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0000E4AE File Offset: 0x0000C6AE
		[DataMember]
		public string PoweredByText
		{
			get
			{
				return this.PoweredByTextField;
			}
			set
			{
				this.PoweredByTextField = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000E4B7 File Offset: 0x0000C6B7
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000E4BF File Offset: 0x0000C6BF
		[DataMember]
		public NavBarLinkData PrivacyLink
		{
			get
			{
				return this.PrivacyLinkField;
			}
			set
			{
				this.PrivacyLinkField = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		[DataMember]
		public string SessionID
		{
			get
			{
				return this.SessionIDField;
			}
			set
			{
				this.SessionIDField = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000E4D9 File Offset: 0x0000C6D9
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000E4E1 File Offset: 0x0000C6E1
		[DataMember]
		public NavBarImageData SettingsImage
		{
			get
			{
				return this.SettingsImageField;
			}
			set
			{
				this.SettingsImageField = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000E4EA File Offset: 0x0000C6EA
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000E4F2 File Offset: 0x0000C6F2
		[DataMember]
		public NavBarLinkData SignOutLink
		{
			get
			{
				return this.SignOutLinkField;
			}
			set
			{
				this.SignOutLinkField = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000E4FB File Offset: 0x0000C6FB
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0000E503 File Offset: 0x0000C703
		[DataMember]
		public NavBarLinkData TenantBackgroundImageUrl
		{
			get
			{
				return this.TenantBackgroundImageUrlField;
			}
			set
			{
				this.TenantBackgroundImageUrlField = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000E50C File Offset: 0x0000C70C
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000E514 File Offset: 0x0000C714
		[DataMember]
		public NavBarLinkData TenantLogoNavigationUrl
		{
			get
			{
				return this.TenantLogoNavigationUrlField;
			}
			set
			{
				this.TenantLogoNavigationUrlField = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000E51D File Offset: 0x0000C71D
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000E525 File Offset: 0x0000C725
		[DataMember]
		public NavBarLinkData TenantLogoUrl
		{
			get
			{
				return this.TenantLogoUrlField;
			}
			set
			{
				this.TenantLogoUrlField = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000E52E File Offset: 0x0000C72E
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0000E536 File Offset: 0x0000C736
		[DataMember]
		public string[] ThemeColors
		{
			get
			{
				return this.ThemeColorsField;
			}
			set
			{
				this.ThemeColorsField = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000E53F File Offset: 0x0000C73F
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0000E547 File Offset: 0x0000C747
		[DataMember]
		public string TransparentImageUrl
		{
			get
			{
				return this.TransparentImageUrlField;
			}
			set
			{
				this.TransparentImageUrlField = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000E550 File Offset: 0x0000C750
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0000E558 File Offset: 0x0000C758
		[DataMember]
		public string TruncatedUserDisplayName
		{
			get
			{
				return this.TruncatedUserDisplayNameField;
			}
			set
			{
				this.TruncatedUserDisplayNameField = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000E561 File Offset: 0x0000C761
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x0000E569 File Offset: 0x0000C769
		[DataMember]
		public NavBarImageData UpArrowImage
		{
			get
			{
				return this.UpArrowImageField;
			}
			set
			{
				this.UpArrowImageField = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000E572 File Offset: 0x0000C772
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0000E57A File Offset: 0x0000C77A
		[DataMember]
		public bool UseSPOBehaviors
		{
			get
			{
				return this.UseSPOBehaviorsField;
			}
			set
			{
				this.UseSPOBehaviorsField = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000E583 File Offset: 0x0000C783
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0000E58B File Offset: 0x0000C78B
		[DataMember]
		public string UserDisplayName
		{
			get
			{
				return this.UserDisplayNameField;
			}
			set
			{
				this.UserDisplayNameField = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000E594 File Offset: 0x0000C794
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0000E59C File Offset: 0x0000C79C
		[DataMember]
		public NavBarLinkData[] WorkloadLinks
		{
			get
			{
				return this.WorkloadLinksField;
			}
			set
			{
				this.WorkloadLinksField = value;
			}
		}

		// Token: 0x04000244 RID: 580
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000245 RID: 581
		private NavBarLinkData AboutMeLinkField;

		// Token: 0x04000246 RID: 582
		private NavBarLinkData AdminLinkField;

		// Token: 0x04000247 RID: 583
		private NavBarImageData AppsImageField;

		// Token: 0x04000248 RID: 584
		private NavBarLinkData[] AppsLinksField;

		// Token: 0x04000249 RID: 585
		private string ClientDataField;

		// Token: 0x0400024A RID: 586
		private NavBarLinkData CommunityLinkField;

		// Token: 0x0400024B RID: 587
		private string CompanyDisplayNameField;

		// Token: 0x0400024C RID: 588
		private string CorrelationIDField;

		// Token: 0x0400024D RID: 589
		private string CultureNameField;

		// Token: 0x0400024E RID: 590
		private string CurrentMainLinkElementIDField;

		// Token: 0x0400024F RID: 591
		private NavBarLinkData[] CurrentWorkloadHelpSubLinksField;

		// Token: 0x04000250 RID: 592
		private NavBarLinkData CurrentWorkloadSettingsLinkField;

		// Token: 0x04000251 RID: 593
		private NavBarLinkData[] CurrentWorkloadSettingsSubLinksField;

		// Token: 0x04000252 RID: 594
		private NavBarLinkData[] CurrentWorkloadUserSubLinksField;

		// Token: 0x04000253 RID: 595
		private ShellDimensions DimensionsField;

		// Token: 0x04000254 RID: 596
		private NavBarImageData DownArrowImageField;

		// Token: 0x04000255 RID: 597
		private NavBarImageData DownWhiteArrowImageField;

		// Token: 0x04000256 RID: 598
		private NavBarLinkData FeedbackLinkField;

		// Token: 0x04000257 RID: 599
		private string FlightNameField;

		// Token: 0x04000258 RID: 600
		private bool FlipHelpIconField;

		// Token: 0x04000259 RID: 601
		private string FooterCopyrightLogoTitleField;

		// Token: 0x0400025A RID: 602
		private string FooterCopyrightTextField;

		// Token: 0x0400025B RID: 603
		private NavBarLinkData FooterICPLinkField;

		// Token: 0x0400025C RID: 604
		private NavBarImageData FooterLogoImageField;

		// Token: 0x0400025D RID: 605
		private bool HasTenantBrandingField;

		// Token: 0x0400025E RID: 606
		private NavBarImageData HelpImageField;

		// Token: 0x0400025F RID: 607
		private NavBarLinkData HelpLinkField;

		// Token: 0x04000260 RID: 608
		private string IPv6TextField;

		// Token: 0x04000261 RID: 609
		private string ImageClusterUrlField;

		// Token: 0x04000262 RID: 610
		private bool IsAuthenticatedField;

		// Token: 0x04000263 RID: 611
		private NavBarLinkData LegalLinkField;

		// Token: 0x04000264 RID: 612
		private string LogoIconIDField;

		// Token: 0x04000265 RID: 613
		private NavBarImageData LogoImageField;

		// Token: 0x04000266 RID: 614
		private string LogoNavigationUrlField;

		// Token: 0x04000267 RID: 615
		private NavBarImageData LogoThemeableImageField;

		// Token: 0x04000268 RID: 616
		private string MenuTitleTextField;

		// Token: 0x04000269 RID: 617
		private string MyProfileUrlField;

		// Token: 0x0400026A RID: 618
		private string NavBarAriaLabelField;

		// Token: 0x0400026B RID: 619
		private NavBarImageData NotificationsBellIconImageField;

		// Token: 0x0400026C RID: 620
		private string NotificationsCountLabelTextField;

		// Token: 0x0400026D RID: 621
		private NavBarImageData NotificationsHighIconImageField;

		// Token: 0x0400026E RID: 622
		private NavBarImageData NotificationsLowIconImageField;

		// Token: 0x0400026F RID: 623
		private NavBarImageData NotificationsMediumIconImageField;

		// Token: 0x04000270 RID: 624
		private string NotificationsPopupHeaderTextField;

		// Token: 0x04000271 RID: 625
		private NavBarUnclusteredImageData NotificationsProgressIconImageField;

		// Token: 0x04000272 RID: 626
		private NavBarLinkData O365SettingsLinkField;

		// Token: 0x04000273 RID: 627
		private NavBarLinkData PartnerLinkField;

		// Token: 0x04000274 RID: 628
		private string PoweredByTextField;

		// Token: 0x04000275 RID: 629
		private NavBarLinkData PrivacyLinkField;

		// Token: 0x04000276 RID: 630
		private string SessionIDField;

		// Token: 0x04000277 RID: 631
		private NavBarImageData SettingsImageField;

		// Token: 0x04000278 RID: 632
		private NavBarLinkData SignOutLinkField;

		// Token: 0x04000279 RID: 633
		private NavBarLinkData TenantBackgroundImageUrlField;

		// Token: 0x0400027A RID: 634
		private NavBarLinkData TenantLogoNavigationUrlField;

		// Token: 0x0400027B RID: 635
		private NavBarLinkData TenantLogoUrlField;

		// Token: 0x0400027C RID: 636
		private string[] ThemeColorsField;

		// Token: 0x0400027D RID: 637
		private string TransparentImageUrlField;

		// Token: 0x0400027E RID: 638
		private string TruncatedUserDisplayNameField;

		// Token: 0x0400027F RID: 639
		private NavBarImageData UpArrowImageField;

		// Token: 0x04000280 RID: 640
		private bool UseSPOBehaviorsField;

		// Token: 0x04000281 RID: 641
		private string UserDisplayNameField;

		// Token: 0x04000282 RID: 642
		private NavBarLinkData[] WorkloadLinksField;
	}
}
