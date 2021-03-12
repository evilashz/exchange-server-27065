using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200019B RID: 411
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class Contact : DirectoryObject
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00022375 File Offset: 0x00020575
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x0002237D File Offset: 0x0002057D
		public DirectoryPropertyReferenceAddressListSingle Assistant
		{
			get
			{
				return this.assistantField;
			}
			set
			{
				this.assistantField = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00022386 File Offset: 0x00020586
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0002238E File Offset: 0x0002058E
		public DirectoryPropertyStringSingleLength1To3 C
		{
			get
			{
				return this.cField;
			}
			set
			{
				this.cField = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00022397 File Offset: 0x00020597
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x0002239F File Offset: 0x0002059F
		public DirectoryPropertyStringSingleLength1To2048 CloudLegacyExchangeDN
		{
			get
			{
				return this.cloudLegacyExchangeDNField;
			}
			set
			{
				this.cloudLegacyExchangeDNField = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x000223A8 File Offset: 0x000205A8
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x000223B0 File Offset: 0x000205B0
		public DirectoryPropertyBinarySingleLength1To4000 CloudMSExchBlockedSendersHash
		{
			get
			{
				return this.cloudMSExchBlockedSendersHashField;
			}
			set
			{
				this.cloudMSExchBlockedSendersHashField = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x000223B9 File Offset: 0x000205B9
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x000223C1 File Offset: 0x000205C1
		public DirectoryPropertyInt32Single CloudMSExchRecipientDisplayType
		{
			get
			{
				return this.cloudMSExchRecipientDisplayTypeField;
			}
			set
			{
				this.cloudMSExchRecipientDisplayTypeField = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x000223CA File Offset: 0x000205CA
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x000223D2 File Offset: 0x000205D2
		public DirectoryPropertyBinarySingleLength1To12000 CloudMSExchSafeRecipientsHash
		{
			get
			{
				return this.cloudMSExchSafeRecipientsHashField;
			}
			set
			{
				this.cloudMSExchSafeRecipientsHashField = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x000223DB File Offset: 0x000205DB
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x000223E3 File Offset: 0x000205E3
		public DirectoryPropertyBinarySingleLength1To32000 CloudMSExchSafeSendersHash
		{
			get
			{
				return this.cloudMSExchSafeSendersHashField;
			}
			set
			{
				this.cloudMSExchSafeSendersHashField = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x000223EC File Offset: 0x000205EC
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x000223F4 File Offset: 0x000205F4
		public DirectoryPropertyStringSingleLength1To128 Co
		{
			get
			{
				return this.coField;
			}
			set
			{
				this.coField = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x000223FD File Offset: 0x000205FD
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x00022405 File Offset: 0x00020605
		public DirectoryPropertyStringSingleLength1To64 Company
		{
			get
			{
				return this.companyField;
			}
			set
			{
				this.companyField = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0002240E File Offset: 0x0002060E
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x00022416 File Offset: 0x00020616
		public DirectoryPropertyInt32SingleMin0Max65535 CountryCode
		{
			get
			{
				return this.countryCodeField;
			}
			set
			{
				this.countryCodeField = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0002241F File Offset: 0x0002061F
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x00022427 File Offset: 0x00020627
		public DirectoryPropertyStringSingleLength1To64 Department
		{
			get
			{
				return this.departmentField;
			}
			set
			{
				this.departmentField = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00022430 File Offset: 0x00020630
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x00022438 File Offset: 0x00020638
		public DirectoryPropertyStringSingleLength1To1024 Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00022441 File Offset: 0x00020641
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x00022449 File Offset: 0x00020649
		public DirectoryPropertyBooleanSingle DirSyncEnabled
		{
			get
			{
				return this.dirSyncEnabledField;
			}
			set
			{
				this.dirSyncEnabledField = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00022452 File Offset: 0x00020652
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0002245A File Offset: 0x0002065A
		public DirectoryPropertyStringSingleLength1To256 DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x00022463 File Offset: 0x00020663
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x0002246B File Offset: 0x0002066B
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute1
		{
			get
			{
				return this.extensionAttribute1Field;
			}
			set
			{
				this.extensionAttribute1Field = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00022474 File Offset: 0x00020674
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0002247C File Offset: 0x0002067C
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute10
		{
			get
			{
				return this.extensionAttribute10Field;
			}
			set
			{
				this.extensionAttribute10Field = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00022485 File Offset: 0x00020685
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0002248D File Offset: 0x0002068D
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute11
		{
			get
			{
				return this.extensionAttribute11Field;
			}
			set
			{
				this.extensionAttribute11Field = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00022496 File Offset: 0x00020696
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0002249E File Offset: 0x0002069E
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute12
		{
			get
			{
				return this.extensionAttribute12Field;
			}
			set
			{
				this.extensionAttribute12Field = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x000224A7 File Offset: 0x000206A7
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x000224AF File Offset: 0x000206AF
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute13
		{
			get
			{
				return this.extensionAttribute13Field;
			}
			set
			{
				this.extensionAttribute13Field = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x000224B8 File Offset: 0x000206B8
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x000224C0 File Offset: 0x000206C0
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute14
		{
			get
			{
				return this.extensionAttribute14Field;
			}
			set
			{
				this.extensionAttribute14Field = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x000224C9 File Offset: 0x000206C9
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x000224D1 File Offset: 0x000206D1
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute15
		{
			get
			{
				return this.extensionAttribute15Field;
			}
			set
			{
				this.extensionAttribute15Field = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x000224DA File Offset: 0x000206DA
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x000224E2 File Offset: 0x000206E2
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute2
		{
			get
			{
				return this.extensionAttribute2Field;
			}
			set
			{
				this.extensionAttribute2Field = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x000224EB File Offset: 0x000206EB
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x000224F3 File Offset: 0x000206F3
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute3
		{
			get
			{
				return this.extensionAttribute3Field;
			}
			set
			{
				this.extensionAttribute3Field = value;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x000224FC File Offset: 0x000206FC
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x00022504 File Offset: 0x00020704
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute4
		{
			get
			{
				return this.extensionAttribute4Field;
			}
			set
			{
				this.extensionAttribute4Field = value;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0002250D File Offset: 0x0002070D
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x00022515 File Offset: 0x00020715
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute5
		{
			get
			{
				return this.extensionAttribute5Field;
			}
			set
			{
				this.extensionAttribute5Field = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002251E File Offset: 0x0002071E
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x00022526 File Offset: 0x00020726
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute6
		{
			get
			{
				return this.extensionAttribute6Field;
			}
			set
			{
				this.extensionAttribute6Field = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0002252F File Offset: 0x0002072F
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x00022537 File Offset: 0x00020737
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute7
		{
			get
			{
				return this.extensionAttribute7Field;
			}
			set
			{
				this.extensionAttribute7Field = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00022540 File Offset: 0x00020740
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x00022548 File Offset: 0x00020748
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute8
		{
			get
			{
				return this.extensionAttribute8Field;
			}
			set
			{
				this.extensionAttribute8Field = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00022551 File Offset: 0x00020751
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x00022559 File Offset: 0x00020759
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute9
		{
			get
			{
				return this.extensionAttribute9Field;
			}
			set
			{
				this.extensionAttribute9Field = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00022562 File Offset: 0x00020762
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x0002256A File Offset: 0x0002076A
		public DirectoryPropertyStringSingleLength1To64 FacsimileTelephoneNumber
		{
			get
			{
				return this.facsimileTelephoneNumberField;
			}
			set
			{
				this.facsimileTelephoneNumberField = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00022573 File Offset: 0x00020773
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x0002257B File Offset: 0x0002077B
		public DirectoryPropertyStringSingleLength1To64 GivenName
		{
			get
			{
				return this.givenNameField;
			}
			set
			{
				this.givenNameField = value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00022584 File Offset: 0x00020784
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x0002258C File Offset: 0x0002078C
		public DirectoryPropertyStringSingleLength1To64 HomePhone
		{
			get
			{
				return this.homePhoneField;
			}
			set
			{
				this.homePhoneField = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00022595 File Offset: 0x00020795
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x0002259D File Offset: 0x0002079D
		public DirectoryPropertyStringSingleLength1To1024 Info
		{
			get
			{
				return this.infoField;
			}
			set
			{
				this.infoField = value;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x000225A6 File Offset: 0x000207A6
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x000225AE File Offset: 0x000207AE
		public DirectoryPropertyStringSingleLength1To6 Initials
		{
			get
			{
				return this.initialsField;
			}
			set
			{
				this.initialsField = value;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x000225B7 File Offset: 0x000207B7
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x000225BF File Offset: 0x000207BF
		public DirectoryPropertyInt32Single InternetEncoding
		{
			get
			{
				return this.internetEncodingField;
			}
			set
			{
				this.internetEncodingField = value;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x000225C8 File Offset: 0x000207C8
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x000225D0 File Offset: 0x000207D0
		public DirectoryPropertyStringSingleLength1To64 IPPhone
		{
			get
			{
				return this.iPPhoneField;
			}
			set
			{
				this.iPPhoneField = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x000225D9 File Offset: 0x000207D9
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x000225E1 File Offset: 0x000207E1
		public DirectoryPropertyStringSingleLength1To128 L
		{
			get
			{
				return this.lField;
			}
			set
			{
				this.lField = value;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x000225EA File Offset: 0x000207EA
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x000225F2 File Offset: 0x000207F2
		public DirectoryPropertyDateTimeSingle LastDirSyncTime
		{
			get
			{
				return this.lastDirSyncTimeField;
			}
			set
			{
				this.lastDirSyncTimeField = value;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x000225FB File Offset: 0x000207FB
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x00022603 File Offset: 0x00020803
		public DirectoryPropertyStringSingleLength1To256 Mail
		{
			get
			{
				return this.mailField;
			}
			set
			{
				this.mailField = value;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0002260C File Offset: 0x0002080C
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x00022614 File Offset: 0x00020814
		public DirectoryPropertyStringSingleMailNickname MailNickname
		{
			get
			{
				return this.mailNicknameField;
			}
			set
			{
				this.mailNicknameField = value;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0002261D File Offset: 0x0002081D
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x00022625 File Offset: 0x00020825
		public DirectoryPropertyStringSingleLength1To64 MiddleName
		{
			get
			{
				return this.middleNameField;
			}
			set
			{
				this.middleNameField = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0002262E File Offset: 0x0002082E
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x00022636 File Offset: 0x00020836
		public DirectoryPropertyXmlMigrationDetail MigrationDetail
		{
			get
			{
				return this.migrationDetailField;
			}
			set
			{
				this.migrationDetailField = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0002263F File Offset: 0x0002083F
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x00022647 File Offset: 0x00020847
		public DirectoryPropertyStringSingleLength1To256 MigrationSourceAnchor
		{
			get
			{
				return this.migrationSourceAnchorField;
			}
			set
			{
				this.migrationSourceAnchorField = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00022650 File Offset: 0x00020850
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x00022658 File Offset: 0x00020858
		public DirectoryPropertyInt32SingleMin0 MigrationState
		{
			get
			{
				return this.migrationStateField;
			}
			set
			{
				this.migrationStateField = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x00022661 File Offset: 0x00020861
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x00022669 File Offset: 0x00020869
		public DirectoryPropertyStringSingleLength1To64 Mobile
		{
			get
			{
				return this.mobileField;
			}
			set
			{
				this.mobileField = value;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00022672 File Offset: 0x00020872
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x0002267A File Offset: 0x0002087A
		public DirectoryPropertyInt32Single MSDSHABSeniorityIndex
		{
			get
			{
				return this.mSDSHABSeniorityIndexField;
			}
			set
			{
				this.mSDSHABSeniorityIndexField = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00022683 File Offset: 0x00020883
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x0002268B File Offset: 0x0002088B
		public DirectoryPropertyStringSingleLength1To256 MSDSPhoneticDisplayName
		{
			get
			{
				return this.mSDSPhoneticDisplayNameField;
			}
			set
			{
				this.mSDSPhoneticDisplayNameField = value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00022694 File Offset: 0x00020894
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0002269C File Offset: 0x0002089C
		public DirectoryPropertyStringSingleLength1To256 MSExchAssistantName
		{
			get
			{
				return this.mSExchAssistantNameField;
			}
			set
			{
				this.mSExchAssistantNameField = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x000226A5 File Offset: 0x000208A5
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x000226AD File Offset: 0x000208AD
		public DirectoryPropertyBinarySingleLength1To4000 MSExchBlockedSendersHash
		{
			get
			{
				return this.mSExchBlockedSendersHashField;
			}
			set
			{
				this.mSExchBlockedSendersHashField = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x000226B6 File Offset: 0x000208B6
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x000226BE File Offset: 0x000208BE
		public DirectoryPropertyBooleanSingle MSExchEnableModeration
		{
			get
			{
				return this.mSExchEnableModerationField;
			}
			set
			{
				this.mSExchEnableModerationField = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x000226C7 File Offset: 0x000208C7
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x000226CF File Offset: 0x000208CF
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute1
		{
			get
			{
				return this.mSExchExtensionCustomAttribute1Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute1Field = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x000226D8 File Offset: 0x000208D8
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x000226E0 File Offset: 0x000208E0
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute2
		{
			get
			{
				return this.mSExchExtensionCustomAttribute2Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute2Field = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x000226E9 File Offset: 0x000208E9
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x000226F1 File Offset: 0x000208F1
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute3
		{
			get
			{
				return this.mSExchExtensionCustomAttribute3Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute3Field = value;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x000226FA File Offset: 0x000208FA
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x00022702 File Offset: 0x00020902
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute4
		{
			get
			{
				return this.mSExchExtensionCustomAttribute4Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute4Field = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0002270B File Offset: 0x0002090B
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x00022713 File Offset: 0x00020913
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute5
		{
			get
			{
				return this.mSExchExtensionCustomAttribute5Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute5Field = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0002271C File Offset: 0x0002091C
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x00022724 File Offset: 0x00020924
		public DirectoryPropertyBooleanSingle MSExchHideFromAddressLists
		{
			get
			{
				return this.mSExchHideFromAddressListsField;
			}
			set
			{
				this.mSExchHideFromAddressListsField = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0002272D File Offset: 0x0002092D
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x00022735 File Offset: 0x00020935
		public DirectoryPropertyDateTimeSingle MSExchLitigationHoldDate
		{
			get
			{
				return this.mSExchLitigationHoldDateField;
			}
			set
			{
				this.mSExchLitigationHoldDateField = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0002273E File Offset: 0x0002093E
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x00022746 File Offset: 0x00020946
		public DirectoryPropertyStringSingleLength1To1024 MSExchLitigationHoldOwner
		{
			get
			{
				return this.mSExchLitigationHoldOwnerField;
			}
			set
			{
				this.mSExchLitigationHoldOwnerField = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0002274F File Offset: 0x0002094F
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x00022757 File Offset: 0x00020957
		public DirectoryPropertyInt32Single MSExchModerationFlags
		{
			get
			{
				return this.mSExchModerationFlagsField;
			}
			set
			{
				this.mSExchModerationFlagsField = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00022760 File Offset: 0x00020960
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x00022768 File Offset: 0x00020968
		public DirectoryPropertyInt32Single MSExchRecipientDisplayType
		{
			get
			{
				return this.mSExchRecipientDisplayTypeField;
			}
			set
			{
				this.mSExchRecipientDisplayTypeField = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00022771 File Offset: 0x00020971
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x00022779 File Offset: 0x00020979
		public DirectoryPropertyInt64Single MSExchRecipientTypeDetails
		{
			get
			{
				return this.mSExchRecipientTypeDetailsField;
			}
			set
			{
				this.mSExchRecipientTypeDetailsField = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00022782 File Offset: 0x00020982
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0002278A File Offset: 0x0002098A
		public DirectoryPropertyBooleanSingle MSExchRequireAuthToSendTo
		{
			get
			{
				return this.mSExchRequireAuthToSendToField;
			}
			set
			{
				this.mSExchRequireAuthToSendToField = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00022793 File Offset: 0x00020993
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0002279B File Offset: 0x0002099B
		public DirectoryPropertyStringSingleLength1To1024 MSExchRetentionComment
		{
			get
			{
				return this.mSExchRetentionCommentField;
			}
			set
			{
				this.mSExchRetentionCommentField = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x000227A4 File Offset: 0x000209A4
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x000227AC File Offset: 0x000209AC
		public DirectoryPropertyStringSingleLength1To2048 MSExchRetentionUrl
		{
			get
			{
				return this.mSExchRetentionUrlField;
			}
			set
			{
				this.mSExchRetentionUrlField = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x000227B5 File Offset: 0x000209B5
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x000227BD File Offset: 0x000209BD
		public DirectoryPropertyBinarySingleLength1To12000 MSExchSafeRecipientsHash
		{
			get
			{
				return this.mSExchSafeRecipientsHashField;
			}
			set
			{
				this.mSExchSafeRecipientsHashField = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x000227C6 File Offset: 0x000209C6
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x000227CE File Offset: 0x000209CE
		public DirectoryPropertyBinarySingleLength1To32000 MSExchSafeSendersHash
		{
			get
			{
				return this.mSExchSafeSendersHashField;
			}
			set
			{
				this.mSExchSafeSendersHashField = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x000227D7 File Offset: 0x000209D7
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x000227DF File Offset: 0x000209DF
		public DirectoryPropertyStringLength2To500 MSExchSenderHintTranslations
		{
			get
			{
				return this.mSExchSenderHintTranslationsField;
			}
			set
			{
				this.mSExchSenderHintTranslationsField = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x000227E8 File Offset: 0x000209E8
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x000227F0 File Offset: 0x000209F0
		public DirectoryPropertyStringSingleLength1To256 MSRtcSipDeploymentLocator
		{
			get
			{
				return this.mSRtcSipDeploymentLocatorField;
			}
			set
			{
				this.mSRtcSipDeploymentLocatorField = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x000227F9 File Offset: 0x000209F9
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x00022801 File Offset: 0x00020A01
		public DirectoryPropertyStringSingleLength1To500 MSRtcSipLine
		{
			get
			{
				return this.mSRtcSipLineField;
			}
			set
			{
				this.mSRtcSipLineField = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0002280A File Offset: 0x00020A0A
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x00022812 File Offset: 0x00020A12
		public DirectoryPropertyInt32Single MSRtcSipOptionFlags
		{
			get
			{
				return this.mSRtcSipOptionFlagsField;
			}
			set
			{
				this.mSRtcSipOptionFlagsField = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0002281B File Offset: 0x00020A1B
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x00022823 File Offset: 0x00020A23
		public DirectoryPropertyStringSingleLength1To454 MSRtcSipPrimaryUserAddress
		{
			get
			{
				return this.mSRtcSipPrimaryUserAddressField;
			}
			set
			{
				this.mSRtcSipPrimaryUserAddressField = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0002282C File Offset: 0x00020A2C
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x00022834 File Offset: 0x00020A34
		public DirectoryPropertyBooleanSingle MSRtcSipUserEnabled
		{
			get
			{
				return this.mSRtcSipUserEnabledField;
			}
			set
			{
				this.mSRtcSipUserEnabledField = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0002283D File Offset: 0x00020A3D
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x00022845 File Offset: 0x00020A45
		public DirectoryPropertyStringLength1To64 OtherFacsimileTelephoneNumber
		{
			get
			{
				return this.otherFacsimileTelephoneNumberField;
			}
			set
			{
				this.otherFacsimileTelephoneNumberField = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0002284E File Offset: 0x00020A4E
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x00022856 File Offset: 0x00020A56
		public DirectoryPropertyStringLength1To64 OtherHomePhone
		{
			get
			{
				return this.otherHomePhoneField;
			}
			set
			{
				this.otherHomePhoneField = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0002285F File Offset: 0x00020A5F
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x00022867 File Offset: 0x00020A67
		public DirectoryPropertyStringLength1To512 OtherIPPhone
		{
			get
			{
				return this.otherIPPhoneField;
			}
			set
			{
				this.otherIPPhoneField = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00022870 File Offset: 0x00020A70
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x00022878 File Offset: 0x00020A78
		public DirectoryPropertyStringLength1To64 OtherMobile
		{
			get
			{
				return this.otherMobileField;
			}
			set
			{
				this.otherMobileField = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00022881 File Offset: 0x00020A81
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x00022889 File Offset: 0x00020A89
		public DirectoryPropertyStringLength1To64 OtherPager
		{
			get
			{
				return this.otherPagerField;
			}
			set
			{
				this.otherPagerField = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x00022892 File Offset: 0x00020A92
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x0002289A File Offset: 0x00020A9A
		public DirectoryPropertyStringLength1To64 OtherTelephone
		{
			get
			{
				return this.otherTelephoneField;
			}
			set
			{
				this.otherTelephoneField = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000228A3 File Offset: 0x00020AA3
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x000228AB File Offset: 0x00020AAB
		public DirectoryPropertyStringSingleLength1To64 Pager
		{
			get
			{
				return this.pagerField;
			}
			set
			{
				this.pagerField = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x000228B4 File Offset: 0x00020AB4
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x000228BC File Offset: 0x00020ABC
		public DirectoryPropertyStringSingleLength1To128 PhysicalDeliveryOfficeName
		{
			get
			{
				return this.physicalDeliveryOfficeNameField;
			}
			set
			{
				this.physicalDeliveryOfficeNameField = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000228C5 File Offset: 0x00020AC5
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x000228CD File Offset: 0x00020ACD
		public DirectoryPropertyStringSingleLength1To40 PostalCode
		{
			get
			{
				return this.postalCodeField;
			}
			set
			{
				this.postalCodeField = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x000228D6 File Offset: 0x00020AD6
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x000228DE File Offset: 0x00020ADE
		public DirectoryPropertyStringLength1To40 PostOfficeBox
		{
			get
			{
				return this.postOfficeBoxField;
			}
			set
			{
				this.postOfficeBoxField = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x000228E7 File Offset: 0x00020AE7
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x000228EF File Offset: 0x00020AEF
		public DirectoryPropertyProxyAddresses ProxyAddresses
		{
			get
			{
				return this.proxyAddressesField;
			}
			set
			{
				this.proxyAddressesField = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x000228F8 File Offset: 0x00020AF8
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x00022900 File Offset: 0x00020B00
		public DirectoryPropertyXmlServiceInfo ServiceInfo
		{
			get
			{
				return this.serviceInfoField;
			}
			set
			{
				this.serviceInfoField = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00022909 File Offset: 0x00020B09
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x00022911 File Offset: 0x00020B11
		public DirectoryPropertyStringSingleLength1To64 ShadowAlias
		{
			get
			{
				return this.shadowAliasField;
			}
			set
			{
				this.shadowAliasField = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0002291A File Offset: 0x00020B1A
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x00022922 File Offset: 0x00020B22
		public DirectoryPropertyStringSingleLength1To64 ShadowCommonName
		{
			get
			{
				return this.shadowCommonNameField;
			}
			set
			{
				this.shadowCommonNameField = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002292B File Offset: 0x00020B2B
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x00022933 File Offset: 0x00020B33
		public DirectoryPropertyStringSingleLength1To256 ShadowDisplayName
		{
			get
			{
				return this.shadowDisplayNameField;
			}
			set
			{
				this.shadowDisplayNameField = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0002293C File Offset: 0x00020B3C
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00022944 File Offset: 0x00020B44
		public DirectoryPropertyStringSingleLength1To2048 ShadowLegacyExchangeDN
		{
			get
			{
				return this.shadowLegacyExchangeDNField;
			}
			set
			{
				this.shadowLegacyExchangeDNField = value;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0002294D File Offset: 0x00020B4D
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00022955 File Offset: 0x00020B55
		public DirectoryPropertyStringSingleLength1To256 ShadowMail
		{
			get
			{
				return this.shadowMailField;
			}
			set
			{
				this.shadowMailField = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002295E File Offset: 0x00020B5E
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00022966 File Offset: 0x00020B66
		public DirectoryPropertyStringSingleLength1To64 ShadowMobile
		{
			get
			{
				return this.shadowMobileField;
			}
			set
			{
				this.shadowMobileField = value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002296F File Offset: 0x00020B6F
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00022977 File Offset: 0x00020B77
		public DirectoryPropertyStringLength1To1123 ShadowProxyAddresses
		{
			get
			{
				return this.shadowProxyAddressesField;
			}
			set
			{
				this.shadowProxyAddressesField = value;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00022980 File Offset: 0x00020B80
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x00022988 File Offset: 0x00020B88
		public DirectoryPropertyStringSingleLength1To1123 ShadowTargetAddress
		{
			get
			{
				return this.shadowTargetAddressField;
			}
			set
			{
				this.shadowTargetAddressField = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00022991 File Offset: 0x00020B91
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x00022999 File Offset: 0x00020B99
		public DirectoryPropertyStringSingleLength1To454 SipProxyAddress
		{
			get
			{
				return this.sipProxyAddressField;
			}
			set
			{
				this.sipProxyAddressField = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x000229A2 File Offset: 0x00020BA2
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x000229AA File Offset: 0x00020BAA
		public DirectoryPropertyStringSingleLength1To64 Sn
		{
			get
			{
				return this.snField;
			}
			set
			{
				this.snField = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x000229B3 File Offset: 0x00020BB3
		// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x000229BB File Offset: 0x00020BBB
		public DirectoryPropertyStringSingleLength1To256 SourceAnchor
		{
			get
			{
				return this.sourceAnchorField;
			}
			set
			{
				this.sourceAnchorField = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x000229C4 File Offset: 0x00020BC4
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x000229CC File Offset: 0x00020BCC
		public DirectoryPropertyStringSingleLength1To128 St
		{
			get
			{
				return this.stField;
			}
			set
			{
				this.stField = value;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x000229D5 File Offset: 0x00020BD5
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x000229DD File Offset: 0x00020BDD
		public DirectoryPropertyStringSingleLength1To1024 Street
		{
			get
			{
				return this.streetField;
			}
			set
			{
				this.streetField = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x000229E6 File Offset: 0x00020BE6
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x000229EE File Offset: 0x00020BEE
		public DirectoryPropertyStringSingleLength1To1024 StreetAddress
		{
			get
			{
				return this.streetAddressField;
			}
			set
			{
				this.streetAddressField = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x000229F7 File Offset: 0x00020BF7
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x000229FF File Offset: 0x00020BFF
		public DirectoryPropertyTargetAddress TargetAddress
		{
			get
			{
				return this.targetAddressField;
			}
			set
			{
				this.targetAddressField = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00022A08 File Offset: 0x00020C08
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x00022A10 File Offset: 0x00020C10
		public DirectoryPropertyStringSingleLength1To64 TelephoneAssistant
		{
			get
			{
				return this.telephoneAssistantField;
			}
			set
			{
				this.telephoneAssistantField = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00022A19 File Offset: 0x00020C19
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x00022A21 File Offset: 0x00020C21
		public DirectoryPropertyStringSingleLength1To64 TelephoneNumber
		{
			get
			{
				return this.telephoneNumberField;
			}
			set
			{
				this.telephoneNumberField = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00022A2A File Offset: 0x00020C2A
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x00022A32 File Offset: 0x00020C32
		public DirectoryPropertyBinarySingleLength1To102400 ThumbnailPhoto
		{
			get
			{
				return this.thumbnailPhotoField;
			}
			set
			{
				this.thumbnailPhotoField = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00022A3B File Offset: 0x00020C3B
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x00022A43 File Offset: 0x00020C43
		public DirectoryPropertyStringSingleLength1To128 Title
		{
			get
			{
				return this.titleField;
			}
			set
			{
				this.titleField = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00022A4C File Offset: 0x00020C4C
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x00022A54 File Offset: 0x00020C54
		public DirectoryPropertyStringLength1To1123 Url
		{
			get
			{
				return this.urlField;
			}
			set
			{
				this.urlField = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00022A5D File Offset: 0x00020C5D
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x00022A65 File Offset: 0x00020C65
		public DirectoryPropertyXmlValidationError ValidationError
		{
			get
			{
				return this.validationErrorField;
			}
			set
			{
				this.validationErrorField = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00022A6E File Offset: 0x00020C6E
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x00022A76 File Offset: 0x00020C76
		public DirectoryPropertyStringSingleLength1To2048 WwwHomepage
		{
			get
			{
				return this.wwwHomepageField;
			}
			set
			{
				this.wwwHomepageField = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00022A7F File Offset: 0x00020C7F
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x00022A87 File Offset: 0x00020C87
		[XmlArray(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/metadata/2010/01")]
		[XmlArrayItem("AttributeSet", IsNullable = false)]
		public AttributeSet[] SingleAuthorityMetadata
		{
			get
			{
				return this.singleAuthorityMetadataField;
			}
			set
			{
				this.singleAuthorityMetadataField = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00022A90 File Offset: 0x00020C90
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x00022A98 File Offset: 0x00020C98
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x040005F5 RID: 1525
		private DirectoryPropertyReferenceAddressListSingle assistantField;

		// Token: 0x040005F6 RID: 1526
		private DirectoryPropertyStringSingleLength1To3 cField;

		// Token: 0x040005F7 RID: 1527
		private DirectoryPropertyStringSingleLength1To2048 cloudLegacyExchangeDNField;

		// Token: 0x040005F8 RID: 1528
		private DirectoryPropertyBinarySingleLength1To4000 cloudMSExchBlockedSendersHashField;

		// Token: 0x040005F9 RID: 1529
		private DirectoryPropertyInt32Single cloudMSExchRecipientDisplayTypeField;

		// Token: 0x040005FA RID: 1530
		private DirectoryPropertyBinarySingleLength1To12000 cloudMSExchSafeRecipientsHashField;

		// Token: 0x040005FB RID: 1531
		private DirectoryPropertyBinarySingleLength1To32000 cloudMSExchSafeSendersHashField;

		// Token: 0x040005FC RID: 1532
		private DirectoryPropertyStringSingleLength1To128 coField;

		// Token: 0x040005FD RID: 1533
		private DirectoryPropertyStringSingleLength1To64 companyField;

		// Token: 0x040005FE RID: 1534
		private DirectoryPropertyInt32SingleMin0Max65535 countryCodeField;

		// Token: 0x040005FF RID: 1535
		private DirectoryPropertyStringSingleLength1To64 departmentField;

		// Token: 0x04000600 RID: 1536
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x04000601 RID: 1537
		private DirectoryPropertyBooleanSingle dirSyncEnabledField;

		// Token: 0x04000602 RID: 1538
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04000603 RID: 1539
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute1Field;

		// Token: 0x04000604 RID: 1540
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute10Field;

		// Token: 0x04000605 RID: 1541
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute11Field;

		// Token: 0x04000606 RID: 1542
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute12Field;

		// Token: 0x04000607 RID: 1543
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute13Field;

		// Token: 0x04000608 RID: 1544
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute14Field;

		// Token: 0x04000609 RID: 1545
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute15Field;

		// Token: 0x0400060A RID: 1546
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute2Field;

		// Token: 0x0400060B RID: 1547
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute3Field;

		// Token: 0x0400060C RID: 1548
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute4Field;

		// Token: 0x0400060D RID: 1549
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute5Field;

		// Token: 0x0400060E RID: 1550
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute6Field;

		// Token: 0x0400060F RID: 1551
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute7Field;

		// Token: 0x04000610 RID: 1552
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute8Field;

		// Token: 0x04000611 RID: 1553
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute9Field;

		// Token: 0x04000612 RID: 1554
		private DirectoryPropertyStringSingleLength1To64 facsimileTelephoneNumberField;

		// Token: 0x04000613 RID: 1555
		private DirectoryPropertyStringSingleLength1To64 givenNameField;

		// Token: 0x04000614 RID: 1556
		private DirectoryPropertyStringSingleLength1To64 homePhoneField;

		// Token: 0x04000615 RID: 1557
		private DirectoryPropertyStringSingleLength1To1024 infoField;

		// Token: 0x04000616 RID: 1558
		private DirectoryPropertyStringSingleLength1To6 initialsField;

		// Token: 0x04000617 RID: 1559
		private DirectoryPropertyInt32Single internetEncodingField;

		// Token: 0x04000618 RID: 1560
		private DirectoryPropertyStringSingleLength1To64 iPPhoneField;

		// Token: 0x04000619 RID: 1561
		private DirectoryPropertyStringSingleLength1To128 lField;

		// Token: 0x0400061A RID: 1562
		private DirectoryPropertyDateTimeSingle lastDirSyncTimeField;

		// Token: 0x0400061B RID: 1563
		private DirectoryPropertyStringSingleLength1To256 mailField;

		// Token: 0x0400061C RID: 1564
		private DirectoryPropertyStringSingleMailNickname mailNicknameField;

		// Token: 0x0400061D RID: 1565
		private DirectoryPropertyStringSingleLength1To64 middleNameField;

		// Token: 0x0400061E RID: 1566
		private DirectoryPropertyXmlMigrationDetail migrationDetailField;

		// Token: 0x0400061F RID: 1567
		private DirectoryPropertyStringSingleLength1To256 migrationSourceAnchorField;

		// Token: 0x04000620 RID: 1568
		private DirectoryPropertyInt32SingleMin0 migrationStateField;

		// Token: 0x04000621 RID: 1569
		private DirectoryPropertyStringSingleLength1To64 mobileField;

		// Token: 0x04000622 RID: 1570
		private DirectoryPropertyInt32Single mSDSHABSeniorityIndexField;

		// Token: 0x04000623 RID: 1571
		private DirectoryPropertyStringSingleLength1To256 mSDSPhoneticDisplayNameField;

		// Token: 0x04000624 RID: 1572
		private DirectoryPropertyStringSingleLength1To256 mSExchAssistantNameField;

		// Token: 0x04000625 RID: 1573
		private DirectoryPropertyBinarySingleLength1To4000 mSExchBlockedSendersHashField;

		// Token: 0x04000626 RID: 1574
		private DirectoryPropertyBooleanSingle mSExchEnableModerationField;

		// Token: 0x04000627 RID: 1575
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute1Field;

		// Token: 0x04000628 RID: 1576
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute2Field;

		// Token: 0x04000629 RID: 1577
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute3Field;

		// Token: 0x0400062A RID: 1578
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute4Field;

		// Token: 0x0400062B RID: 1579
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute5Field;

		// Token: 0x0400062C RID: 1580
		private DirectoryPropertyBooleanSingle mSExchHideFromAddressListsField;

		// Token: 0x0400062D RID: 1581
		private DirectoryPropertyDateTimeSingle mSExchLitigationHoldDateField;

		// Token: 0x0400062E RID: 1582
		private DirectoryPropertyStringSingleLength1To1024 mSExchLitigationHoldOwnerField;

		// Token: 0x0400062F RID: 1583
		private DirectoryPropertyInt32Single mSExchModerationFlagsField;

		// Token: 0x04000630 RID: 1584
		private DirectoryPropertyInt32Single mSExchRecipientDisplayTypeField;

		// Token: 0x04000631 RID: 1585
		private DirectoryPropertyInt64Single mSExchRecipientTypeDetailsField;

		// Token: 0x04000632 RID: 1586
		private DirectoryPropertyBooleanSingle mSExchRequireAuthToSendToField;

		// Token: 0x04000633 RID: 1587
		private DirectoryPropertyStringSingleLength1To1024 mSExchRetentionCommentField;

		// Token: 0x04000634 RID: 1588
		private DirectoryPropertyStringSingleLength1To2048 mSExchRetentionUrlField;

		// Token: 0x04000635 RID: 1589
		private DirectoryPropertyBinarySingleLength1To12000 mSExchSafeRecipientsHashField;

		// Token: 0x04000636 RID: 1590
		private DirectoryPropertyBinarySingleLength1To32000 mSExchSafeSendersHashField;

		// Token: 0x04000637 RID: 1591
		private DirectoryPropertyStringLength2To500 mSExchSenderHintTranslationsField;

		// Token: 0x04000638 RID: 1592
		private DirectoryPropertyStringSingleLength1To256 mSRtcSipDeploymentLocatorField;

		// Token: 0x04000639 RID: 1593
		private DirectoryPropertyStringSingleLength1To500 mSRtcSipLineField;

		// Token: 0x0400063A RID: 1594
		private DirectoryPropertyInt32Single mSRtcSipOptionFlagsField;

		// Token: 0x0400063B RID: 1595
		private DirectoryPropertyStringSingleLength1To454 mSRtcSipPrimaryUserAddressField;

		// Token: 0x0400063C RID: 1596
		private DirectoryPropertyBooleanSingle mSRtcSipUserEnabledField;

		// Token: 0x0400063D RID: 1597
		private DirectoryPropertyStringLength1To64 otherFacsimileTelephoneNumberField;

		// Token: 0x0400063E RID: 1598
		private DirectoryPropertyStringLength1To64 otherHomePhoneField;

		// Token: 0x0400063F RID: 1599
		private DirectoryPropertyStringLength1To512 otherIPPhoneField;

		// Token: 0x04000640 RID: 1600
		private DirectoryPropertyStringLength1To64 otherMobileField;

		// Token: 0x04000641 RID: 1601
		private DirectoryPropertyStringLength1To64 otherPagerField;

		// Token: 0x04000642 RID: 1602
		private DirectoryPropertyStringLength1To64 otherTelephoneField;

		// Token: 0x04000643 RID: 1603
		private DirectoryPropertyStringSingleLength1To64 pagerField;

		// Token: 0x04000644 RID: 1604
		private DirectoryPropertyStringSingleLength1To128 physicalDeliveryOfficeNameField;

		// Token: 0x04000645 RID: 1605
		private DirectoryPropertyStringSingleLength1To40 postalCodeField;

		// Token: 0x04000646 RID: 1606
		private DirectoryPropertyStringLength1To40 postOfficeBoxField;

		// Token: 0x04000647 RID: 1607
		private DirectoryPropertyProxyAddresses proxyAddressesField;

		// Token: 0x04000648 RID: 1608
		private DirectoryPropertyXmlServiceInfo serviceInfoField;

		// Token: 0x04000649 RID: 1609
		private DirectoryPropertyStringSingleLength1To64 shadowAliasField;

		// Token: 0x0400064A RID: 1610
		private DirectoryPropertyStringSingleLength1To64 shadowCommonNameField;

		// Token: 0x0400064B RID: 1611
		private DirectoryPropertyStringSingleLength1To256 shadowDisplayNameField;

		// Token: 0x0400064C RID: 1612
		private DirectoryPropertyStringSingleLength1To2048 shadowLegacyExchangeDNField;

		// Token: 0x0400064D RID: 1613
		private DirectoryPropertyStringSingleLength1To256 shadowMailField;

		// Token: 0x0400064E RID: 1614
		private DirectoryPropertyStringSingleLength1To64 shadowMobileField;

		// Token: 0x0400064F RID: 1615
		private DirectoryPropertyStringLength1To1123 shadowProxyAddressesField;

		// Token: 0x04000650 RID: 1616
		private DirectoryPropertyStringSingleLength1To1123 shadowTargetAddressField;

		// Token: 0x04000651 RID: 1617
		private DirectoryPropertyStringSingleLength1To454 sipProxyAddressField;

		// Token: 0x04000652 RID: 1618
		private DirectoryPropertyStringSingleLength1To64 snField;

		// Token: 0x04000653 RID: 1619
		private DirectoryPropertyStringSingleLength1To256 sourceAnchorField;

		// Token: 0x04000654 RID: 1620
		private DirectoryPropertyStringSingleLength1To128 stField;

		// Token: 0x04000655 RID: 1621
		private DirectoryPropertyStringSingleLength1To1024 streetField;

		// Token: 0x04000656 RID: 1622
		private DirectoryPropertyStringSingleLength1To1024 streetAddressField;

		// Token: 0x04000657 RID: 1623
		private DirectoryPropertyTargetAddress targetAddressField;

		// Token: 0x04000658 RID: 1624
		private DirectoryPropertyStringSingleLength1To64 telephoneAssistantField;

		// Token: 0x04000659 RID: 1625
		private DirectoryPropertyStringSingleLength1To64 telephoneNumberField;

		// Token: 0x0400065A RID: 1626
		private DirectoryPropertyBinarySingleLength1To102400 thumbnailPhotoField;

		// Token: 0x0400065B RID: 1627
		private DirectoryPropertyStringSingleLength1To128 titleField;

		// Token: 0x0400065C RID: 1628
		private DirectoryPropertyStringLength1To1123 urlField;

		// Token: 0x0400065D RID: 1629
		private DirectoryPropertyXmlValidationError validationErrorField;

		// Token: 0x0400065E RID: 1630
		private DirectoryPropertyStringSingleLength1To2048 wwwHomepageField;

		// Token: 0x0400065F RID: 1631
		private AttributeSet[] singleAuthorityMetadataField;

		// Token: 0x04000660 RID: 1632
		private XmlAttribute[] anyAttrField;
	}
}
