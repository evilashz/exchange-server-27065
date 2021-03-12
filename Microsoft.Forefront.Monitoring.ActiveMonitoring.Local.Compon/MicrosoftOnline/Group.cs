using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000198 RID: 408
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class Group : DirectoryObject
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00021DEB File Offset: 0x0001FFEB
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00021DF3 File Offset: 0x0001FFF3
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

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00021DFC File Offset: 0x0001FFFC
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x00021E04 File Offset: 0x00020004
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

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00021E0D File Offset: 0x0002000D
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00021E15 File Offset: 0x00020015
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

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00021E1E File Offset: 0x0002001E
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x00021E26 File Offset: 0x00020026
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

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00021E2F File Offset: 0x0002002F
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x00021E37 File Offset: 0x00020037
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

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00021E40 File Offset: 0x00020040
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x00021E48 File Offset: 0x00020048
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

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00021E51 File Offset: 0x00020051
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x00021E59 File Offset: 0x00020059
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

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00021E62 File Offset: 0x00020062
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00021E6A File Offset: 0x0002006A
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

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00021E73 File Offset: 0x00020073
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x00021E7B File Offset: 0x0002007B
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

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00021E84 File Offset: 0x00020084
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x00021E8C File Offset: 0x0002008C
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

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00021E95 File Offset: 0x00020095
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x00021E9D File Offset: 0x0002009D
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

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00021EA6 File Offset: 0x000200A6
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x00021EAE File Offset: 0x000200AE
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

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00021EB7 File Offset: 0x000200B7
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x00021EBF File Offset: 0x000200BF
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

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00021EC8 File Offset: 0x000200C8
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x00021ED0 File Offset: 0x000200D0
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

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00021ED9 File Offset: 0x000200D9
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x00021EE1 File Offset: 0x000200E1
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

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00021EEA File Offset: 0x000200EA
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x00021EF2 File Offset: 0x000200F2
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

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00021EFB File Offset: 0x000200FB
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x00021F03 File Offset: 0x00020103
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

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x00021F0C File Offset: 0x0002010C
		// (set) Token: 0x06000B81 RID: 2945 RVA: 0x00021F14 File Offset: 0x00020114
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

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00021F1D File Offset: 0x0002011D
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x00021F25 File Offset: 0x00020125
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

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00021F2E File Offset: 0x0002012E
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x00021F36 File Offset: 0x00020136
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

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00021F3F File Offset: 0x0002013F
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x00021F47 File Offset: 0x00020147
		public DirectoryPropertyBooleanSingle HideDLMembership
		{
			get
			{
				return this.hideDLMembershipField;
			}
			set
			{
				this.hideDLMembershipField = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00021F50 File Offset: 0x00020150
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x00021F58 File Offset: 0x00020158
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

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00021F61 File Offset: 0x00020161
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x00021F69 File Offset: 0x00020169
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

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x00021F72 File Offset: 0x00020172
		// (set) Token: 0x06000B8D RID: 2957 RVA: 0x00021F7A File Offset: 0x0002017A
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

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00021F83 File Offset: 0x00020183
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x00021F8B File Offset: 0x0002018B
		public DirectoryPropertyBooleanSingle MailEnabled
		{
			get
			{
				return this.mailEnabledField;
			}
			set
			{
				this.mailEnabledField = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00021F94 File Offset: 0x00020194
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x00021F9C File Offset: 0x0002019C
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

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00021FA5 File Offset: 0x000201A5
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x00021FAD File Offset: 0x000201AD
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

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00021FB6 File Offset: 0x000201B6
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x00021FBE File Offset: 0x000201BE
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

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x00021FC7 File Offset: 0x000201C7
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x00021FCF File Offset: 0x000201CF
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

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00021FD8 File Offset: 0x000201D8
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x00021FE0 File Offset: 0x000201E0
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

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00021FE9 File Offset: 0x000201E9
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x00021FF1 File Offset: 0x000201F1
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

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00021FFA File Offset: 0x000201FA
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x00022002 File Offset: 0x00020202
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

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0002200B File Offset: 0x0002020B
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x00022013 File Offset: 0x00020213
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

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0002201C File Offset: 0x0002021C
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x00022024 File Offset: 0x00020224
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

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002202D File Offset: 0x0002022D
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x00022035 File Offset: 0x00020235
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

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0002203E File Offset: 0x0002023E
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x00022046 File Offset: 0x00020246
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

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0002204F File Offset: 0x0002024F
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x00022057 File Offset: 0x00020257
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

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00022060 File Offset: 0x00020260
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x00022068 File Offset: 0x00020268
		public DirectoryPropertyInt32Single MSExchGroupDepartRestriction
		{
			get
			{
				return this.mSExchGroupDepartRestrictionField;
			}
			set
			{
				this.mSExchGroupDepartRestrictionField = value;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00022071 File Offset: 0x00020271
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x00022079 File Offset: 0x00020279
		public DirectoryPropertyInt32Single MSExchGroupJoinRestriction
		{
			get
			{
				return this.mSExchGroupJoinRestrictionField;
			}
			set
			{
				this.mSExchGroupJoinRestrictionField = value;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00022082 File Offset: 0x00020282
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x0002208A File Offset: 0x0002028A
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

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00022093 File Offset: 0x00020293
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x0002209B File Offset: 0x0002029B
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

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x000220A4 File Offset: 0x000202A4
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x000220AC File Offset: 0x000202AC
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

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x000220B5 File Offset: 0x000202B5
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x000220BD File Offset: 0x000202BD
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

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x000220C6 File Offset: 0x000202C6
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x000220CE File Offset: 0x000202CE
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

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x000220D7 File Offset: 0x000202D7
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x000220DF File Offset: 0x000202DF
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

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x000220E8 File Offset: 0x000202E8
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x000220F0 File Offset: 0x000202F0
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

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x000220F9 File Offset: 0x000202F9
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x00022101 File Offset: 0x00020301
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

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0002210A File Offset: 0x0002030A
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x00022112 File Offset: 0x00020312
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

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0002211B File Offset: 0x0002031B
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x00022123 File Offset: 0x00020323
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

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0002212C File Offset: 0x0002032C
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x00022134 File Offset: 0x00020334
		public DirectoryPropertyBooleanSingle MSOrgIsOrganizational
		{
			get
			{
				return this.mSOrgIsOrganizationalField;
			}
			set
			{
				this.mSOrgIsOrganizationalField = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0002213D File Offset: 0x0002033D
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x00022145 File Offset: 0x00020345
		public DirectoryPropertyBinarySingleLength1To128 OnPremiseSecurityIdentifier
		{
			get
			{
				return this.onPremiseSecurityIdentifierField;
			}
			set
			{
				this.onPremiseSecurityIdentifierField = value;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0002214E File Offset: 0x0002034E
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x00022156 File Offset: 0x00020356
		public DirectoryPropertyBooleanSingle OofReplyToOriginator
		{
			get
			{
				return this.oofReplyToOriginatorField;
			}
			set
			{
				this.oofReplyToOriginatorField = value;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0002215F File Offset: 0x0002035F
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x00022167 File Offset: 0x00020367
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

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00022170 File Offset: 0x00020370
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x00022178 File Offset: 0x00020378
		public DirectoryPropertyBooleanSingle ReportToOriginator
		{
			get
			{
				return this.reportToOriginatorField;
			}
			set
			{
				this.reportToOriginatorField = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00022181 File Offset: 0x00020381
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x00022189 File Offset: 0x00020389
		public DirectoryPropertyBooleanSingle ReportToOwner
		{
			get
			{
				return this.reportToOwnerField;
			}
			set
			{
				this.reportToOwnerField = value;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00022192 File Offset: 0x00020392
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0002219A File Offset: 0x0002039A
		public DirectoryPropertyBooleanSingle SecurityEnabled
		{
			get
			{
				return this.securityEnabledField;
			}
			set
			{
				this.securityEnabledField = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x000221A3 File Offset: 0x000203A3
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x000221AB File Offset: 0x000203AB
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

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x000221B4 File Offset: 0x000203B4
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x000221BC File Offset: 0x000203BC
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

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x000221C5 File Offset: 0x000203C5
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x000221CD File Offset: 0x000203CD
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

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x000221D6 File Offset: 0x000203D6
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x000221DE File Offset: 0x000203DE
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

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x000221E7 File Offset: 0x000203E7
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x000221EF File Offset: 0x000203EF
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

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x000221F8 File Offset: 0x000203F8
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x00022200 File Offset: 0x00020400
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

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00022209 File Offset: 0x00020409
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x00022211 File Offset: 0x00020411
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

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0002221A File Offset: 0x0002041A
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x00022222 File Offset: 0x00020422
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

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0002222B File Offset: 0x0002042B
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x00022233 File Offset: 0x00020433
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

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0002223C File Offset: 0x0002043C
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x00022244 File Offset: 0x00020444
		public DirectoryPropertyStringSingleLength1To40 WellKnownObject
		{
			get
			{
				return this.wellKnownObjectField;
			}
			set
			{
				this.wellKnownObjectField = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0002224D File Offset: 0x0002044D
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x00022255 File Offset: 0x00020455
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

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0002225E File Offset: 0x0002045E
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x00022266 File Offset: 0x00020466
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

		// Token: 0x040005A3 RID: 1443
		private DirectoryPropertyStringSingleLength1To2048 cloudLegacyExchangeDNField;

		// Token: 0x040005A4 RID: 1444
		private DirectoryPropertyInt32Single cloudMSExchRecipientDisplayTypeField;

		// Token: 0x040005A5 RID: 1445
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x040005A6 RID: 1446
		private DirectoryPropertyBooleanSingle dirSyncEnabledField;

		// Token: 0x040005A7 RID: 1447
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x040005A8 RID: 1448
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute1Field;

		// Token: 0x040005A9 RID: 1449
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute10Field;

		// Token: 0x040005AA RID: 1450
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute11Field;

		// Token: 0x040005AB RID: 1451
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute12Field;

		// Token: 0x040005AC RID: 1452
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute13Field;

		// Token: 0x040005AD RID: 1453
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute14Field;

		// Token: 0x040005AE RID: 1454
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute15Field;

		// Token: 0x040005AF RID: 1455
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute2Field;

		// Token: 0x040005B0 RID: 1456
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute3Field;

		// Token: 0x040005B1 RID: 1457
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute4Field;

		// Token: 0x040005B2 RID: 1458
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute5Field;

		// Token: 0x040005B3 RID: 1459
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute6Field;

		// Token: 0x040005B4 RID: 1460
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute7Field;

		// Token: 0x040005B5 RID: 1461
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute8Field;

		// Token: 0x040005B6 RID: 1462
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute9Field;

		// Token: 0x040005B7 RID: 1463
		private DirectoryPropertyBooleanSingle hideDLMembershipField;

		// Token: 0x040005B8 RID: 1464
		private DirectoryPropertyStringSingleLength1To1024 infoField;

		// Token: 0x040005B9 RID: 1465
		private DirectoryPropertyDateTimeSingle lastDirSyncTimeField;

		// Token: 0x040005BA RID: 1466
		private DirectoryPropertyStringSingleLength1To256 mailField;

		// Token: 0x040005BB RID: 1467
		private DirectoryPropertyBooleanSingle mailEnabledField;

		// Token: 0x040005BC RID: 1468
		private DirectoryPropertyStringSingleMailNickname mailNicknameField;

		// Token: 0x040005BD RID: 1469
		private DirectoryPropertyXmlMigrationDetail migrationDetailField;

		// Token: 0x040005BE RID: 1470
		private DirectoryPropertyStringSingleLength1To256 migrationSourceAnchorField;

		// Token: 0x040005BF RID: 1471
		private DirectoryPropertyInt32SingleMin0 migrationStateField;

		// Token: 0x040005C0 RID: 1472
		private DirectoryPropertyInt32Single mSDSHABSeniorityIndexField;

		// Token: 0x040005C1 RID: 1473
		private DirectoryPropertyStringSingleLength1To256 mSDSPhoneticDisplayNameField;

		// Token: 0x040005C2 RID: 1474
		private DirectoryPropertyBooleanSingle mSExchEnableModerationField;

		// Token: 0x040005C3 RID: 1475
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute1Field;

		// Token: 0x040005C4 RID: 1476
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute2Field;

		// Token: 0x040005C5 RID: 1477
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute3Field;

		// Token: 0x040005C6 RID: 1478
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute4Field;

		// Token: 0x040005C7 RID: 1479
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute5Field;

		// Token: 0x040005C8 RID: 1480
		private DirectoryPropertyInt32Single mSExchGroupDepartRestrictionField;

		// Token: 0x040005C9 RID: 1481
		private DirectoryPropertyInt32Single mSExchGroupJoinRestrictionField;

		// Token: 0x040005CA RID: 1482
		private DirectoryPropertyBooleanSingle mSExchHideFromAddressListsField;

		// Token: 0x040005CB RID: 1483
		private DirectoryPropertyDateTimeSingle mSExchLitigationHoldDateField;

		// Token: 0x040005CC RID: 1484
		private DirectoryPropertyStringSingleLength1To1024 mSExchLitigationHoldOwnerField;

		// Token: 0x040005CD RID: 1485
		private DirectoryPropertyInt32Single mSExchModerationFlagsField;

		// Token: 0x040005CE RID: 1486
		private DirectoryPropertyInt32Single mSExchRecipientDisplayTypeField;

		// Token: 0x040005CF RID: 1487
		private DirectoryPropertyInt64Single mSExchRecipientTypeDetailsField;

		// Token: 0x040005D0 RID: 1488
		private DirectoryPropertyBooleanSingle mSExchRequireAuthToSendToField;

		// Token: 0x040005D1 RID: 1489
		private DirectoryPropertyStringSingleLength1To1024 mSExchRetentionCommentField;

		// Token: 0x040005D2 RID: 1490
		private DirectoryPropertyStringSingleLength1To2048 mSExchRetentionUrlField;

		// Token: 0x040005D3 RID: 1491
		private DirectoryPropertyStringLength2To500 mSExchSenderHintTranslationsField;

		// Token: 0x040005D4 RID: 1492
		private DirectoryPropertyBooleanSingle mSOrgIsOrganizationalField;

		// Token: 0x040005D5 RID: 1493
		private DirectoryPropertyBinarySingleLength1To128 onPremiseSecurityIdentifierField;

		// Token: 0x040005D6 RID: 1494
		private DirectoryPropertyBooleanSingle oofReplyToOriginatorField;

		// Token: 0x040005D7 RID: 1495
		private DirectoryPropertyProxyAddresses proxyAddressesField;

		// Token: 0x040005D8 RID: 1496
		private DirectoryPropertyBooleanSingle reportToOriginatorField;

		// Token: 0x040005D9 RID: 1497
		private DirectoryPropertyBooleanSingle reportToOwnerField;

		// Token: 0x040005DA RID: 1498
		private DirectoryPropertyBooleanSingle securityEnabledField;

		// Token: 0x040005DB RID: 1499
		private DirectoryPropertyXmlServiceInfo serviceInfoField;

		// Token: 0x040005DC RID: 1500
		private DirectoryPropertyStringSingleLength1To64 shadowAliasField;

		// Token: 0x040005DD RID: 1501
		private DirectoryPropertyStringSingleLength1To64 shadowCommonNameField;

		// Token: 0x040005DE RID: 1502
		private DirectoryPropertyStringSingleLength1To256 shadowDisplayNameField;

		// Token: 0x040005DF RID: 1503
		private DirectoryPropertyStringSingleLength1To2048 shadowLegacyExchangeDNField;

		// Token: 0x040005E0 RID: 1504
		private DirectoryPropertyStringSingleLength1To256 shadowMailField;

		// Token: 0x040005E1 RID: 1505
		private DirectoryPropertyStringLength1To1123 shadowProxyAddressesField;

		// Token: 0x040005E2 RID: 1506
		private DirectoryPropertyStringSingleLength1To256 sourceAnchorField;

		// Token: 0x040005E3 RID: 1507
		private DirectoryPropertyXmlValidationError validationErrorField;

		// Token: 0x040005E4 RID: 1508
		private DirectoryPropertyStringSingleLength1To40 wellKnownObjectField;

		// Token: 0x040005E5 RID: 1509
		private AttributeSet[] singleAuthorityMetadataField;

		// Token: 0x040005E6 RID: 1510
		private XmlAttribute[] anyAttrField;
	}
}
