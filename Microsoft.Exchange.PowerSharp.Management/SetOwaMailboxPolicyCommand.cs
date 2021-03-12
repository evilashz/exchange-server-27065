using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001D9 RID: 473
	public class SetOwaMailboxPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<OwaMailboxPolicy>
	{
		// Token: 0x060026BF RID: 9919 RVA: 0x00049F03 File Offset: 0x00048103
		private SetOwaMailboxPolicyCommand() : base("Set-OwaMailboxPolicy")
		{
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x00049F10 File Offset: 0x00048110
		public SetOwaMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x00049F1F File Offset: 0x0004811F
		public virtual SetOwaMailboxPolicyCommand SetParameters(SetOwaMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x00049F29 File Offset: 0x00048129
		public virtual SetOwaMailboxPolicyCommand SetParameters(SetOwaMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001DA RID: 474
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000EC6 RID: 3782
			// (set) Token: 0x060026C3 RID: 9923 RVA: 0x00049F33 File Offset: 0x00048133
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000EC7 RID: 3783
			// (set) Token: 0x060026C4 RID: 9924 RVA: 0x00049F4B File Offset: 0x0004814B
			public virtual SwitchParameter DisableFacebook
			{
				set
				{
					base.PowerSharpParameters["DisableFacebook"] = value;
				}
			}

			// Token: 0x17000EC8 RID: 3784
			// (set) Token: 0x060026C5 RID: 9925 RVA: 0x00049F63 File Offset: 0x00048163
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000EC9 RID: 3785
			// (set) Token: 0x060026C6 RID: 9926 RVA: 0x00049F76 File Offset: 0x00048176
			public virtual bool DirectFileAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["DirectFileAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17000ECA RID: 3786
			// (set) Token: 0x060026C7 RID: 9927 RVA: 0x00049F8E File Offset: 0x0004818E
			public virtual bool DirectFileAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["DirectFileAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17000ECB RID: 3787
			// (set) Token: 0x060026C8 RID: 9928 RVA: 0x00049FA6 File Offset: 0x000481A6
			public virtual bool WebReadyDocumentViewingOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17000ECC RID: 3788
			// (set) Token: 0x060026C9 RID: 9929 RVA: 0x00049FBE File Offset: 0x000481BE
			public virtual bool WebReadyDocumentViewingOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17000ECD RID: 3789
			// (set) Token: 0x060026CA RID: 9930 RVA: 0x00049FD6 File Offset: 0x000481D6
			public virtual bool ForceWebReadyDocumentViewingFirstOnPublicComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWebReadyDocumentViewingFirstOnPublicComputers"] = value;
				}
			}

			// Token: 0x17000ECE RID: 3790
			// (set) Token: 0x060026CB RID: 9931 RVA: 0x00049FEE File Offset: 0x000481EE
			public virtual bool ForceWebReadyDocumentViewingFirstOnPrivateComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWebReadyDocumentViewingFirstOnPrivateComputers"] = value;
				}
			}

			// Token: 0x17000ECF RID: 3791
			// (set) Token: 0x060026CC RID: 9932 RVA: 0x0004A006 File Offset: 0x00048206
			public virtual bool WacViewingOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WacViewingOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17000ED0 RID: 3792
			// (set) Token: 0x060026CD RID: 9933 RVA: 0x0004A01E File Offset: 0x0004821E
			public virtual bool WacViewingOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WacViewingOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17000ED1 RID: 3793
			// (set) Token: 0x060026CE RID: 9934 RVA: 0x0004A036 File Offset: 0x00048236
			public virtual bool ForceWacViewingFirstOnPublicComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWacViewingFirstOnPublicComputers"] = value;
				}
			}

			// Token: 0x17000ED2 RID: 3794
			// (set) Token: 0x060026CF RID: 9935 RVA: 0x0004A04E File Offset: 0x0004824E
			public virtual bool ForceWacViewingFirstOnPrivateComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWacViewingFirstOnPrivateComputers"] = value;
				}
			}

			// Token: 0x17000ED3 RID: 3795
			// (set) Token: 0x060026D0 RID: 9936 RVA: 0x0004A066 File Offset: 0x00048266
			public virtual AttachmentBlockingActions ActionForUnknownFileAndMIMETypes
			{
				set
				{
					base.PowerSharpParameters["ActionForUnknownFileAndMIMETypes"] = value;
				}
			}

			// Token: 0x17000ED4 RID: 3796
			// (set) Token: 0x060026D1 RID: 9937 RVA: 0x0004A07E File Offset: 0x0004827E
			public virtual MultiValuedProperty<string> WebReadyFileTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyFileTypes"] = value;
				}
			}

			// Token: 0x17000ED5 RID: 3797
			// (set) Token: 0x060026D2 RID: 9938 RVA: 0x0004A091 File Offset: 0x00048291
			public virtual MultiValuedProperty<string> WebReadyMimeTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyMimeTypes"] = value;
				}
			}

			// Token: 0x17000ED6 RID: 3798
			// (set) Token: 0x060026D3 RID: 9939 RVA: 0x0004A0A4 File Offset: 0x000482A4
			public virtual bool WebReadyDocumentViewingForAllSupportedTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingForAllSupportedTypes"] = value;
				}
			}

			// Token: 0x17000ED7 RID: 3799
			// (set) Token: 0x060026D4 RID: 9940 RVA: 0x0004A0BC File Offset: 0x000482BC
			public virtual MultiValuedProperty<string> WebReadyDocumentViewingSupportedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingSupportedMimeTypes"] = value;
				}
			}

			// Token: 0x17000ED8 RID: 3800
			// (set) Token: 0x060026D5 RID: 9941 RVA: 0x0004A0CF File Offset: 0x000482CF
			public virtual MultiValuedProperty<string> WebReadyDocumentViewingSupportedFileTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingSupportedFileTypes"] = value;
				}
			}

			// Token: 0x17000ED9 RID: 3801
			// (set) Token: 0x060026D6 RID: 9942 RVA: 0x0004A0E2 File Offset: 0x000482E2
			public virtual MultiValuedProperty<string> AllowedFileTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedFileTypes"] = value;
				}
			}

			// Token: 0x17000EDA RID: 3802
			// (set) Token: 0x060026D7 RID: 9943 RVA: 0x0004A0F5 File Offset: 0x000482F5
			public virtual MultiValuedProperty<string> AllowedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedMimeTypes"] = value;
				}
			}

			// Token: 0x17000EDB RID: 3803
			// (set) Token: 0x060026D8 RID: 9944 RVA: 0x0004A108 File Offset: 0x00048308
			public virtual MultiValuedProperty<string> ForceSaveFileTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveFileTypes"] = value;
				}
			}

			// Token: 0x17000EDC RID: 3804
			// (set) Token: 0x060026D9 RID: 9945 RVA: 0x0004A11B File Offset: 0x0004831B
			public virtual MultiValuedProperty<string> ForceSaveMimeTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveMimeTypes"] = value;
				}
			}

			// Token: 0x17000EDD RID: 3805
			// (set) Token: 0x060026DA RID: 9946 RVA: 0x0004A12E File Offset: 0x0004832E
			public virtual MultiValuedProperty<string> BlockedFileTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedFileTypes"] = value;
				}
			}

			// Token: 0x17000EDE RID: 3806
			// (set) Token: 0x060026DB RID: 9947 RVA: 0x0004A141 File Offset: 0x00048341
			public virtual MultiValuedProperty<string> BlockedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedMimeTypes"] = value;
				}
			}

			// Token: 0x17000EDF RID: 3807
			// (set) Token: 0x060026DC RID: 9948 RVA: 0x0004A154 File Offset: 0x00048354
			public virtual bool PhoneticSupportEnabled
			{
				set
				{
					base.PowerSharpParameters["PhoneticSupportEnabled"] = value;
				}
			}

			// Token: 0x17000EE0 RID: 3808
			// (set) Token: 0x060026DD RID: 9949 RVA: 0x0004A16C File Offset: 0x0004836C
			public virtual string DefaultTheme
			{
				set
				{
					base.PowerSharpParameters["DefaultTheme"] = value;
				}
			}

			// Token: 0x17000EE1 RID: 3809
			// (set) Token: 0x060026DE RID: 9950 RVA: 0x0004A17F File Offset: 0x0004837F
			public virtual int DefaultClientLanguage
			{
				set
				{
					base.PowerSharpParameters["DefaultClientLanguage"] = value;
				}
			}

			// Token: 0x17000EE2 RID: 3810
			// (set) Token: 0x060026DF RID: 9951 RVA: 0x0004A197 File Offset: 0x00048397
			public virtual int LogonAndErrorLanguage
			{
				set
				{
					base.PowerSharpParameters["LogonAndErrorLanguage"] = value;
				}
			}

			// Token: 0x17000EE3 RID: 3811
			// (set) Token: 0x060026E0 RID: 9952 RVA: 0x0004A1AF File Offset: 0x000483AF
			public virtual bool UseGB18030
			{
				set
				{
					base.PowerSharpParameters["UseGB18030"] = value;
				}
			}

			// Token: 0x17000EE4 RID: 3812
			// (set) Token: 0x060026E1 RID: 9953 RVA: 0x0004A1C7 File Offset: 0x000483C7
			public virtual bool UseISO885915
			{
				set
				{
					base.PowerSharpParameters["UseISO885915"] = value;
				}
			}

			// Token: 0x17000EE5 RID: 3813
			// (set) Token: 0x060026E2 RID: 9954 RVA: 0x0004A1DF File Offset: 0x000483DF
			public virtual OutboundCharsetOptions OutboundCharset
			{
				set
				{
					base.PowerSharpParameters["OutboundCharset"] = value;
				}
			}

			// Token: 0x17000EE6 RID: 3814
			// (set) Token: 0x060026E3 RID: 9955 RVA: 0x0004A1F7 File Offset: 0x000483F7
			public virtual bool GlobalAddressListEnabled
			{
				set
				{
					base.PowerSharpParameters["GlobalAddressListEnabled"] = value;
				}
			}

			// Token: 0x17000EE7 RID: 3815
			// (set) Token: 0x060026E4 RID: 9956 RVA: 0x0004A20F File Offset: 0x0004840F
			public virtual bool OrganizationEnabled
			{
				set
				{
					base.PowerSharpParameters["OrganizationEnabled"] = value;
				}
			}

			// Token: 0x17000EE8 RID: 3816
			// (set) Token: 0x060026E5 RID: 9957 RVA: 0x0004A227 File Offset: 0x00048427
			public virtual bool ExplicitLogonEnabled
			{
				set
				{
					base.PowerSharpParameters["ExplicitLogonEnabled"] = value;
				}
			}

			// Token: 0x17000EE9 RID: 3817
			// (set) Token: 0x060026E6 RID: 9958 RVA: 0x0004A23F File Offset: 0x0004843F
			public virtual bool OWALightEnabled
			{
				set
				{
					base.PowerSharpParameters["OWALightEnabled"] = value;
				}
			}

			// Token: 0x17000EEA RID: 3818
			// (set) Token: 0x060026E7 RID: 9959 RVA: 0x0004A257 File Offset: 0x00048457
			public virtual bool DelegateAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["DelegateAccessEnabled"] = value;
				}
			}

			// Token: 0x17000EEB RID: 3819
			// (set) Token: 0x060026E8 RID: 9960 RVA: 0x0004A26F File Offset: 0x0004846F
			public virtual bool IRMEnabled
			{
				set
				{
					base.PowerSharpParameters["IRMEnabled"] = value;
				}
			}

			// Token: 0x17000EEC RID: 3820
			// (set) Token: 0x060026E9 RID: 9961 RVA: 0x0004A287 File Offset: 0x00048487
			public virtual bool CalendarEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarEnabled"] = value;
				}
			}

			// Token: 0x17000EED RID: 3821
			// (set) Token: 0x060026EA RID: 9962 RVA: 0x0004A29F File Offset: 0x0004849F
			public virtual bool ContactsEnabled
			{
				set
				{
					base.PowerSharpParameters["ContactsEnabled"] = value;
				}
			}

			// Token: 0x17000EEE RID: 3822
			// (set) Token: 0x060026EB RID: 9963 RVA: 0x0004A2B7 File Offset: 0x000484B7
			public virtual bool TasksEnabled
			{
				set
				{
					base.PowerSharpParameters["TasksEnabled"] = value;
				}
			}

			// Token: 0x17000EEF RID: 3823
			// (set) Token: 0x060026EC RID: 9964 RVA: 0x0004A2CF File Offset: 0x000484CF
			public virtual bool JournalEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalEnabled"] = value;
				}
			}

			// Token: 0x17000EF0 RID: 3824
			// (set) Token: 0x060026ED RID: 9965 RVA: 0x0004A2E7 File Offset: 0x000484E7
			public virtual bool NotesEnabled
			{
				set
				{
					base.PowerSharpParameters["NotesEnabled"] = value;
				}
			}

			// Token: 0x17000EF1 RID: 3825
			// (set) Token: 0x060026EE RID: 9966 RVA: 0x0004A2FF File Offset: 0x000484FF
			public virtual bool RemindersAndNotificationsEnabled
			{
				set
				{
					base.PowerSharpParameters["RemindersAndNotificationsEnabled"] = value;
				}
			}

			// Token: 0x17000EF2 RID: 3826
			// (set) Token: 0x060026EF RID: 9967 RVA: 0x0004A317 File Offset: 0x00048517
			public virtual bool PremiumClientEnabled
			{
				set
				{
					base.PowerSharpParameters["PremiumClientEnabled"] = value;
				}
			}

			// Token: 0x17000EF3 RID: 3827
			// (set) Token: 0x060026F0 RID: 9968 RVA: 0x0004A32F File Offset: 0x0004852F
			public virtual bool SpellCheckerEnabled
			{
				set
				{
					base.PowerSharpParameters["SpellCheckerEnabled"] = value;
				}
			}

			// Token: 0x17000EF4 RID: 3828
			// (set) Token: 0x060026F1 RID: 9969 RVA: 0x0004A347 File Offset: 0x00048547
			public virtual bool SearchFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["SearchFoldersEnabled"] = value;
				}
			}

			// Token: 0x17000EF5 RID: 3829
			// (set) Token: 0x060026F2 RID: 9970 RVA: 0x0004A35F File Offset: 0x0004855F
			public virtual bool SignaturesEnabled
			{
				set
				{
					base.PowerSharpParameters["SignaturesEnabled"] = value;
				}
			}

			// Token: 0x17000EF6 RID: 3830
			// (set) Token: 0x060026F3 RID: 9971 RVA: 0x0004A377 File Offset: 0x00048577
			public virtual bool ThemeSelectionEnabled
			{
				set
				{
					base.PowerSharpParameters["ThemeSelectionEnabled"] = value;
				}
			}

			// Token: 0x17000EF7 RID: 3831
			// (set) Token: 0x060026F4 RID: 9972 RVA: 0x0004A38F File Offset: 0x0004858F
			public virtual bool JunkEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["JunkEmailEnabled"] = value;
				}
			}

			// Token: 0x17000EF8 RID: 3832
			// (set) Token: 0x060026F5 RID: 9973 RVA: 0x0004A3A7 File Offset: 0x000485A7
			public virtual bool UMIntegrationEnabled
			{
				set
				{
					base.PowerSharpParameters["UMIntegrationEnabled"] = value;
				}
			}

			// Token: 0x17000EF9 RID: 3833
			// (set) Token: 0x060026F6 RID: 9974 RVA: 0x0004A3BF File Offset: 0x000485BF
			public virtual bool WSSAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17000EFA RID: 3834
			// (set) Token: 0x060026F7 RID: 9975 RVA: 0x0004A3D7 File Offset: 0x000485D7
			public virtual bool WSSAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17000EFB RID: 3835
			// (set) Token: 0x060026F8 RID: 9976 RVA: 0x0004A3EF File Offset: 0x000485EF
			public virtual bool ChangePasswordEnabled
			{
				set
				{
					base.PowerSharpParameters["ChangePasswordEnabled"] = value;
				}
			}

			// Token: 0x17000EFC RID: 3836
			// (set) Token: 0x060026F9 RID: 9977 RVA: 0x0004A407 File Offset: 0x00048607
			public virtual bool UNCAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17000EFD RID: 3837
			// (set) Token: 0x060026FA RID: 9978 RVA: 0x0004A41F File Offset: 0x0004861F
			public virtual bool UNCAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17000EFE RID: 3838
			// (set) Token: 0x060026FB RID: 9979 RVA: 0x0004A437 File Offset: 0x00048637
			public virtual bool ActiveSyncIntegrationEnabled
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncIntegrationEnabled"] = value;
				}
			}

			// Token: 0x17000EFF RID: 3839
			// (set) Token: 0x060026FC RID: 9980 RVA: 0x0004A44F File Offset: 0x0004864F
			public virtual bool AllAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["AllAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17000F00 RID: 3840
			// (set) Token: 0x060026FD RID: 9981 RVA: 0x0004A467 File Offset: 0x00048667
			public virtual bool RulesEnabled
			{
				set
				{
					base.PowerSharpParameters["RulesEnabled"] = value;
				}
			}

			// Token: 0x17000F01 RID: 3841
			// (set) Token: 0x060026FE RID: 9982 RVA: 0x0004A47F File Offset: 0x0004867F
			public virtual bool PublicFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersEnabled"] = value;
				}
			}

			// Token: 0x17000F02 RID: 3842
			// (set) Token: 0x060026FF RID: 9983 RVA: 0x0004A497 File Offset: 0x00048697
			public virtual bool SMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["SMimeEnabled"] = value;
				}
			}

			// Token: 0x17000F03 RID: 3843
			// (set) Token: 0x06002700 RID: 9984 RVA: 0x0004A4AF File Offset: 0x000486AF
			public virtual bool RecoverDeletedItemsEnabled
			{
				set
				{
					base.PowerSharpParameters["RecoverDeletedItemsEnabled"] = value;
				}
			}

			// Token: 0x17000F04 RID: 3844
			// (set) Token: 0x06002701 RID: 9985 RVA: 0x0004A4C7 File Offset: 0x000486C7
			public virtual bool InstantMessagingEnabled
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingEnabled"] = value;
				}
			}

			// Token: 0x17000F05 RID: 3845
			// (set) Token: 0x06002702 RID: 9986 RVA: 0x0004A4DF File Offset: 0x000486DF
			public virtual bool TextMessagingEnabled
			{
				set
				{
					base.PowerSharpParameters["TextMessagingEnabled"] = value;
				}
			}

			// Token: 0x17000F06 RID: 3846
			// (set) Token: 0x06002703 RID: 9987 RVA: 0x0004A4F7 File Offset: 0x000486F7
			public virtual bool ForceSaveAttachmentFilteringEnabled
			{
				set
				{
					base.PowerSharpParameters["ForceSaveAttachmentFilteringEnabled"] = value;
				}
			}

			// Token: 0x17000F07 RID: 3847
			// (set) Token: 0x06002704 RID: 9988 RVA: 0x0004A50F File Offset: 0x0004870F
			public virtual bool SilverlightEnabled
			{
				set
				{
					base.PowerSharpParameters["SilverlightEnabled"] = value;
				}
			}

			// Token: 0x17000F08 RID: 3848
			// (set) Token: 0x06002705 RID: 9989 RVA: 0x0004A527 File Offset: 0x00048727
			public virtual InstantMessagingTypeOptions? InstantMessagingType
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingType"] = value;
				}
			}

			// Token: 0x17000F09 RID: 3849
			// (set) Token: 0x06002706 RID: 9990 RVA: 0x0004A53F File Offset: 0x0004873F
			public virtual bool DisplayPhotosEnabled
			{
				set
				{
					base.PowerSharpParameters["DisplayPhotosEnabled"] = value;
				}
			}

			// Token: 0x17000F0A RID: 3850
			// (set) Token: 0x06002707 RID: 9991 RVA: 0x0004A557 File Offset: 0x00048757
			public virtual bool SetPhotoEnabled
			{
				set
				{
					base.PowerSharpParameters["SetPhotoEnabled"] = value;
				}
			}

			// Token: 0x17000F0B RID: 3851
			// (set) Token: 0x06002708 RID: 9992 RVA: 0x0004A56F File Offset: 0x0004876F
			public virtual AllowOfflineOnEnum AllowOfflineOn
			{
				set
				{
					base.PowerSharpParameters["AllowOfflineOn"] = value;
				}
			}

			// Token: 0x17000F0C RID: 3852
			// (set) Token: 0x06002709 RID: 9993 RVA: 0x0004A587 File Offset: 0x00048787
			public virtual string SetPhotoURL
			{
				set
				{
					base.PowerSharpParameters["SetPhotoURL"] = value;
				}
			}

			// Token: 0x17000F0D RID: 3853
			// (set) Token: 0x0600270A RID: 9994 RVA: 0x0004A59A File Offset: 0x0004879A
			public virtual bool PlacesEnabled
			{
				set
				{
					base.PowerSharpParameters["PlacesEnabled"] = value;
				}
			}

			// Token: 0x17000F0E RID: 3854
			// (set) Token: 0x0600270B RID: 9995 RVA: 0x0004A5B2 File Offset: 0x000487B2
			public virtual bool WeatherEnabled
			{
				set
				{
					base.PowerSharpParameters["WeatherEnabled"] = value;
				}
			}

			// Token: 0x17000F0F RID: 3855
			// (set) Token: 0x0600270C RID: 9996 RVA: 0x0004A5CA File Offset: 0x000487CA
			public virtual bool AllowCopyContactsToDeviceAddressBook
			{
				set
				{
					base.PowerSharpParameters["AllowCopyContactsToDeviceAddressBook"] = value;
				}
			}

			// Token: 0x17000F10 RID: 3856
			// (set) Token: 0x0600270D RID: 9997 RVA: 0x0004A5E2 File Offset: 0x000487E2
			public virtual bool PredictedActionsEnabled
			{
				set
				{
					base.PowerSharpParameters["PredictedActionsEnabled"] = value;
				}
			}

			// Token: 0x17000F11 RID: 3857
			// (set) Token: 0x0600270E RID: 9998 RVA: 0x0004A5FA File Offset: 0x000487FA
			public virtual bool UserDiagnosticEnabled
			{
				set
				{
					base.PowerSharpParameters["UserDiagnosticEnabled"] = value;
				}
			}

			// Token: 0x17000F12 RID: 3858
			// (set) Token: 0x0600270F RID: 9999 RVA: 0x0004A612 File Offset: 0x00048812
			public virtual bool FacebookEnabled
			{
				set
				{
					base.PowerSharpParameters["FacebookEnabled"] = value;
				}
			}

			// Token: 0x17000F13 RID: 3859
			// (set) Token: 0x06002710 RID: 10000 RVA: 0x0004A62A File Offset: 0x0004882A
			public virtual bool LinkedInEnabled
			{
				set
				{
					base.PowerSharpParameters["LinkedInEnabled"] = value;
				}
			}

			// Token: 0x17000F14 RID: 3860
			// (set) Token: 0x06002711 RID: 10001 RVA: 0x0004A642 File Offset: 0x00048842
			public virtual bool WacExternalServicesEnabled
			{
				set
				{
					base.PowerSharpParameters["WacExternalServicesEnabled"] = value;
				}
			}

			// Token: 0x17000F15 RID: 3861
			// (set) Token: 0x06002712 RID: 10002 RVA: 0x0004A65A File Offset: 0x0004885A
			public virtual bool WacOMEXEnabled
			{
				set
				{
					base.PowerSharpParameters["WacOMEXEnabled"] = value;
				}
			}

			// Token: 0x17000F16 RID: 3862
			// (set) Token: 0x06002713 RID: 10003 RVA: 0x0004A672 File Offset: 0x00048872
			public virtual bool ReportJunkEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportJunkEmailEnabled"] = value;
				}
			}

			// Token: 0x17000F17 RID: 3863
			// (set) Token: 0x06002714 RID: 10004 RVA: 0x0004A68A File Offset: 0x0004888A
			public virtual bool GroupCreationEnabled
			{
				set
				{
					base.PowerSharpParameters["GroupCreationEnabled"] = value;
				}
			}

			// Token: 0x17000F18 RID: 3864
			// (set) Token: 0x06002715 RID: 10005 RVA: 0x0004A6A2 File Offset: 0x000488A2
			public virtual bool SkipCreateUnifiedGroupCustomSharepointClassification
			{
				set
				{
					base.PowerSharpParameters["SkipCreateUnifiedGroupCustomSharepointClassification"] = value;
				}
			}

			// Token: 0x17000F19 RID: 3865
			// (set) Token: 0x06002716 RID: 10006 RVA: 0x0004A6BA File Offset: 0x000488BA
			public virtual WebPartsFrameOptions WebPartsFrameOptionsType
			{
				set
				{
					base.PowerSharpParameters["WebPartsFrameOptionsType"] = value;
				}
			}

			// Token: 0x17000F1A RID: 3866
			// (set) Token: 0x06002717 RID: 10007 RVA: 0x0004A6D2 File Offset: 0x000488D2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000F1B RID: 3867
			// (set) Token: 0x06002718 RID: 10008 RVA: 0x0004A6E5 File Offset: 0x000488E5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000F1C RID: 3868
			// (set) Token: 0x06002719 RID: 10009 RVA: 0x0004A6FD File Offset: 0x000488FD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000F1D RID: 3869
			// (set) Token: 0x0600271A RID: 10010 RVA: 0x0004A715 File Offset: 0x00048915
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000F1E RID: 3870
			// (set) Token: 0x0600271B RID: 10011 RVA: 0x0004A72D File Offset: 0x0004892D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000F1F RID: 3871
			// (set) Token: 0x0600271C RID: 10012 RVA: 0x0004A745 File Offset: 0x00048945
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020001DB RID: 475
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000F20 RID: 3872
			// (set) Token: 0x0600271E RID: 10014 RVA: 0x0004A765 File Offset: 0x00048965
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000F21 RID: 3873
			// (set) Token: 0x0600271F RID: 10015 RVA: 0x0004A783 File Offset: 0x00048983
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000F22 RID: 3874
			// (set) Token: 0x06002720 RID: 10016 RVA: 0x0004A79B File Offset: 0x0004899B
			public virtual SwitchParameter DisableFacebook
			{
				set
				{
					base.PowerSharpParameters["DisableFacebook"] = value;
				}
			}

			// Token: 0x17000F23 RID: 3875
			// (set) Token: 0x06002721 RID: 10017 RVA: 0x0004A7B3 File Offset: 0x000489B3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000F24 RID: 3876
			// (set) Token: 0x06002722 RID: 10018 RVA: 0x0004A7C6 File Offset: 0x000489C6
			public virtual bool DirectFileAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["DirectFileAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17000F25 RID: 3877
			// (set) Token: 0x06002723 RID: 10019 RVA: 0x0004A7DE File Offset: 0x000489DE
			public virtual bool DirectFileAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["DirectFileAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17000F26 RID: 3878
			// (set) Token: 0x06002724 RID: 10020 RVA: 0x0004A7F6 File Offset: 0x000489F6
			public virtual bool WebReadyDocumentViewingOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17000F27 RID: 3879
			// (set) Token: 0x06002725 RID: 10021 RVA: 0x0004A80E File Offset: 0x00048A0E
			public virtual bool WebReadyDocumentViewingOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17000F28 RID: 3880
			// (set) Token: 0x06002726 RID: 10022 RVA: 0x0004A826 File Offset: 0x00048A26
			public virtual bool ForceWebReadyDocumentViewingFirstOnPublicComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWebReadyDocumentViewingFirstOnPublicComputers"] = value;
				}
			}

			// Token: 0x17000F29 RID: 3881
			// (set) Token: 0x06002727 RID: 10023 RVA: 0x0004A83E File Offset: 0x00048A3E
			public virtual bool ForceWebReadyDocumentViewingFirstOnPrivateComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWebReadyDocumentViewingFirstOnPrivateComputers"] = value;
				}
			}

			// Token: 0x17000F2A RID: 3882
			// (set) Token: 0x06002728 RID: 10024 RVA: 0x0004A856 File Offset: 0x00048A56
			public virtual bool WacViewingOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WacViewingOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17000F2B RID: 3883
			// (set) Token: 0x06002729 RID: 10025 RVA: 0x0004A86E File Offset: 0x00048A6E
			public virtual bool WacViewingOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WacViewingOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17000F2C RID: 3884
			// (set) Token: 0x0600272A RID: 10026 RVA: 0x0004A886 File Offset: 0x00048A86
			public virtual bool ForceWacViewingFirstOnPublicComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWacViewingFirstOnPublicComputers"] = value;
				}
			}

			// Token: 0x17000F2D RID: 3885
			// (set) Token: 0x0600272B RID: 10027 RVA: 0x0004A89E File Offset: 0x00048A9E
			public virtual bool ForceWacViewingFirstOnPrivateComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWacViewingFirstOnPrivateComputers"] = value;
				}
			}

			// Token: 0x17000F2E RID: 3886
			// (set) Token: 0x0600272C RID: 10028 RVA: 0x0004A8B6 File Offset: 0x00048AB6
			public virtual AttachmentBlockingActions ActionForUnknownFileAndMIMETypes
			{
				set
				{
					base.PowerSharpParameters["ActionForUnknownFileAndMIMETypes"] = value;
				}
			}

			// Token: 0x17000F2F RID: 3887
			// (set) Token: 0x0600272D RID: 10029 RVA: 0x0004A8CE File Offset: 0x00048ACE
			public virtual MultiValuedProperty<string> WebReadyFileTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyFileTypes"] = value;
				}
			}

			// Token: 0x17000F30 RID: 3888
			// (set) Token: 0x0600272E RID: 10030 RVA: 0x0004A8E1 File Offset: 0x00048AE1
			public virtual MultiValuedProperty<string> WebReadyMimeTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyMimeTypes"] = value;
				}
			}

			// Token: 0x17000F31 RID: 3889
			// (set) Token: 0x0600272F RID: 10031 RVA: 0x0004A8F4 File Offset: 0x00048AF4
			public virtual bool WebReadyDocumentViewingForAllSupportedTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingForAllSupportedTypes"] = value;
				}
			}

			// Token: 0x17000F32 RID: 3890
			// (set) Token: 0x06002730 RID: 10032 RVA: 0x0004A90C File Offset: 0x00048B0C
			public virtual MultiValuedProperty<string> WebReadyDocumentViewingSupportedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingSupportedMimeTypes"] = value;
				}
			}

			// Token: 0x17000F33 RID: 3891
			// (set) Token: 0x06002731 RID: 10033 RVA: 0x0004A91F File Offset: 0x00048B1F
			public virtual MultiValuedProperty<string> WebReadyDocumentViewingSupportedFileTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingSupportedFileTypes"] = value;
				}
			}

			// Token: 0x17000F34 RID: 3892
			// (set) Token: 0x06002732 RID: 10034 RVA: 0x0004A932 File Offset: 0x00048B32
			public virtual MultiValuedProperty<string> AllowedFileTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedFileTypes"] = value;
				}
			}

			// Token: 0x17000F35 RID: 3893
			// (set) Token: 0x06002733 RID: 10035 RVA: 0x0004A945 File Offset: 0x00048B45
			public virtual MultiValuedProperty<string> AllowedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedMimeTypes"] = value;
				}
			}

			// Token: 0x17000F36 RID: 3894
			// (set) Token: 0x06002734 RID: 10036 RVA: 0x0004A958 File Offset: 0x00048B58
			public virtual MultiValuedProperty<string> ForceSaveFileTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveFileTypes"] = value;
				}
			}

			// Token: 0x17000F37 RID: 3895
			// (set) Token: 0x06002735 RID: 10037 RVA: 0x0004A96B File Offset: 0x00048B6B
			public virtual MultiValuedProperty<string> ForceSaveMimeTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveMimeTypes"] = value;
				}
			}

			// Token: 0x17000F38 RID: 3896
			// (set) Token: 0x06002736 RID: 10038 RVA: 0x0004A97E File Offset: 0x00048B7E
			public virtual MultiValuedProperty<string> BlockedFileTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedFileTypes"] = value;
				}
			}

			// Token: 0x17000F39 RID: 3897
			// (set) Token: 0x06002737 RID: 10039 RVA: 0x0004A991 File Offset: 0x00048B91
			public virtual MultiValuedProperty<string> BlockedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedMimeTypes"] = value;
				}
			}

			// Token: 0x17000F3A RID: 3898
			// (set) Token: 0x06002738 RID: 10040 RVA: 0x0004A9A4 File Offset: 0x00048BA4
			public virtual bool PhoneticSupportEnabled
			{
				set
				{
					base.PowerSharpParameters["PhoneticSupportEnabled"] = value;
				}
			}

			// Token: 0x17000F3B RID: 3899
			// (set) Token: 0x06002739 RID: 10041 RVA: 0x0004A9BC File Offset: 0x00048BBC
			public virtual string DefaultTheme
			{
				set
				{
					base.PowerSharpParameters["DefaultTheme"] = value;
				}
			}

			// Token: 0x17000F3C RID: 3900
			// (set) Token: 0x0600273A RID: 10042 RVA: 0x0004A9CF File Offset: 0x00048BCF
			public virtual int DefaultClientLanguage
			{
				set
				{
					base.PowerSharpParameters["DefaultClientLanguage"] = value;
				}
			}

			// Token: 0x17000F3D RID: 3901
			// (set) Token: 0x0600273B RID: 10043 RVA: 0x0004A9E7 File Offset: 0x00048BE7
			public virtual int LogonAndErrorLanguage
			{
				set
				{
					base.PowerSharpParameters["LogonAndErrorLanguage"] = value;
				}
			}

			// Token: 0x17000F3E RID: 3902
			// (set) Token: 0x0600273C RID: 10044 RVA: 0x0004A9FF File Offset: 0x00048BFF
			public virtual bool UseGB18030
			{
				set
				{
					base.PowerSharpParameters["UseGB18030"] = value;
				}
			}

			// Token: 0x17000F3F RID: 3903
			// (set) Token: 0x0600273D RID: 10045 RVA: 0x0004AA17 File Offset: 0x00048C17
			public virtual bool UseISO885915
			{
				set
				{
					base.PowerSharpParameters["UseISO885915"] = value;
				}
			}

			// Token: 0x17000F40 RID: 3904
			// (set) Token: 0x0600273E RID: 10046 RVA: 0x0004AA2F File Offset: 0x00048C2F
			public virtual OutboundCharsetOptions OutboundCharset
			{
				set
				{
					base.PowerSharpParameters["OutboundCharset"] = value;
				}
			}

			// Token: 0x17000F41 RID: 3905
			// (set) Token: 0x0600273F RID: 10047 RVA: 0x0004AA47 File Offset: 0x00048C47
			public virtual bool GlobalAddressListEnabled
			{
				set
				{
					base.PowerSharpParameters["GlobalAddressListEnabled"] = value;
				}
			}

			// Token: 0x17000F42 RID: 3906
			// (set) Token: 0x06002740 RID: 10048 RVA: 0x0004AA5F File Offset: 0x00048C5F
			public virtual bool OrganizationEnabled
			{
				set
				{
					base.PowerSharpParameters["OrganizationEnabled"] = value;
				}
			}

			// Token: 0x17000F43 RID: 3907
			// (set) Token: 0x06002741 RID: 10049 RVA: 0x0004AA77 File Offset: 0x00048C77
			public virtual bool ExplicitLogonEnabled
			{
				set
				{
					base.PowerSharpParameters["ExplicitLogonEnabled"] = value;
				}
			}

			// Token: 0x17000F44 RID: 3908
			// (set) Token: 0x06002742 RID: 10050 RVA: 0x0004AA8F File Offset: 0x00048C8F
			public virtual bool OWALightEnabled
			{
				set
				{
					base.PowerSharpParameters["OWALightEnabled"] = value;
				}
			}

			// Token: 0x17000F45 RID: 3909
			// (set) Token: 0x06002743 RID: 10051 RVA: 0x0004AAA7 File Offset: 0x00048CA7
			public virtual bool DelegateAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["DelegateAccessEnabled"] = value;
				}
			}

			// Token: 0x17000F46 RID: 3910
			// (set) Token: 0x06002744 RID: 10052 RVA: 0x0004AABF File Offset: 0x00048CBF
			public virtual bool IRMEnabled
			{
				set
				{
					base.PowerSharpParameters["IRMEnabled"] = value;
				}
			}

			// Token: 0x17000F47 RID: 3911
			// (set) Token: 0x06002745 RID: 10053 RVA: 0x0004AAD7 File Offset: 0x00048CD7
			public virtual bool CalendarEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarEnabled"] = value;
				}
			}

			// Token: 0x17000F48 RID: 3912
			// (set) Token: 0x06002746 RID: 10054 RVA: 0x0004AAEF File Offset: 0x00048CEF
			public virtual bool ContactsEnabled
			{
				set
				{
					base.PowerSharpParameters["ContactsEnabled"] = value;
				}
			}

			// Token: 0x17000F49 RID: 3913
			// (set) Token: 0x06002747 RID: 10055 RVA: 0x0004AB07 File Offset: 0x00048D07
			public virtual bool TasksEnabled
			{
				set
				{
					base.PowerSharpParameters["TasksEnabled"] = value;
				}
			}

			// Token: 0x17000F4A RID: 3914
			// (set) Token: 0x06002748 RID: 10056 RVA: 0x0004AB1F File Offset: 0x00048D1F
			public virtual bool JournalEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalEnabled"] = value;
				}
			}

			// Token: 0x17000F4B RID: 3915
			// (set) Token: 0x06002749 RID: 10057 RVA: 0x0004AB37 File Offset: 0x00048D37
			public virtual bool NotesEnabled
			{
				set
				{
					base.PowerSharpParameters["NotesEnabled"] = value;
				}
			}

			// Token: 0x17000F4C RID: 3916
			// (set) Token: 0x0600274A RID: 10058 RVA: 0x0004AB4F File Offset: 0x00048D4F
			public virtual bool RemindersAndNotificationsEnabled
			{
				set
				{
					base.PowerSharpParameters["RemindersAndNotificationsEnabled"] = value;
				}
			}

			// Token: 0x17000F4D RID: 3917
			// (set) Token: 0x0600274B RID: 10059 RVA: 0x0004AB67 File Offset: 0x00048D67
			public virtual bool PremiumClientEnabled
			{
				set
				{
					base.PowerSharpParameters["PremiumClientEnabled"] = value;
				}
			}

			// Token: 0x17000F4E RID: 3918
			// (set) Token: 0x0600274C RID: 10060 RVA: 0x0004AB7F File Offset: 0x00048D7F
			public virtual bool SpellCheckerEnabled
			{
				set
				{
					base.PowerSharpParameters["SpellCheckerEnabled"] = value;
				}
			}

			// Token: 0x17000F4F RID: 3919
			// (set) Token: 0x0600274D RID: 10061 RVA: 0x0004AB97 File Offset: 0x00048D97
			public virtual bool SearchFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["SearchFoldersEnabled"] = value;
				}
			}

			// Token: 0x17000F50 RID: 3920
			// (set) Token: 0x0600274E RID: 10062 RVA: 0x0004ABAF File Offset: 0x00048DAF
			public virtual bool SignaturesEnabled
			{
				set
				{
					base.PowerSharpParameters["SignaturesEnabled"] = value;
				}
			}

			// Token: 0x17000F51 RID: 3921
			// (set) Token: 0x0600274F RID: 10063 RVA: 0x0004ABC7 File Offset: 0x00048DC7
			public virtual bool ThemeSelectionEnabled
			{
				set
				{
					base.PowerSharpParameters["ThemeSelectionEnabled"] = value;
				}
			}

			// Token: 0x17000F52 RID: 3922
			// (set) Token: 0x06002750 RID: 10064 RVA: 0x0004ABDF File Offset: 0x00048DDF
			public virtual bool JunkEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["JunkEmailEnabled"] = value;
				}
			}

			// Token: 0x17000F53 RID: 3923
			// (set) Token: 0x06002751 RID: 10065 RVA: 0x0004ABF7 File Offset: 0x00048DF7
			public virtual bool UMIntegrationEnabled
			{
				set
				{
					base.PowerSharpParameters["UMIntegrationEnabled"] = value;
				}
			}

			// Token: 0x17000F54 RID: 3924
			// (set) Token: 0x06002752 RID: 10066 RVA: 0x0004AC0F File Offset: 0x00048E0F
			public virtual bool WSSAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17000F55 RID: 3925
			// (set) Token: 0x06002753 RID: 10067 RVA: 0x0004AC27 File Offset: 0x00048E27
			public virtual bool WSSAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17000F56 RID: 3926
			// (set) Token: 0x06002754 RID: 10068 RVA: 0x0004AC3F File Offset: 0x00048E3F
			public virtual bool ChangePasswordEnabled
			{
				set
				{
					base.PowerSharpParameters["ChangePasswordEnabled"] = value;
				}
			}

			// Token: 0x17000F57 RID: 3927
			// (set) Token: 0x06002755 RID: 10069 RVA: 0x0004AC57 File Offset: 0x00048E57
			public virtual bool UNCAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17000F58 RID: 3928
			// (set) Token: 0x06002756 RID: 10070 RVA: 0x0004AC6F File Offset: 0x00048E6F
			public virtual bool UNCAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17000F59 RID: 3929
			// (set) Token: 0x06002757 RID: 10071 RVA: 0x0004AC87 File Offset: 0x00048E87
			public virtual bool ActiveSyncIntegrationEnabled
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncIntegrationEnabled"] = value;
				}
			}

			// Token: 0x17000F5A RID: 3930
			// (set) Token: 0x06002758 RID: 10072 RVA: 0x0004AC9F File Offset: 0x00048E9F
			public virtual bool AllAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["AllAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17000F5B RID: 3931
			// (set) Token: 0x06002759 RID: 10073 RVA: 0x0004ACB7 File Offset: 0x00048EB7
			public virtual bool RulesEnabled
			{
				set
				{
					base.PowerSharpParameters["RulesEnabled"] = value;
				}
			}

			// Token: 0x17000F5C RID: 3932
			// (set) Token: 0x0600275A RID: 10074 RVA: 0x0004ACCF File Offset: 0x00048ECF
			public virtual bool PublicFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersEnabled"] = value;
				}
			}

			// Token: 0x17000F5D RID: 3933
			// (set) Token: 0x0600275B RID: 10075 RVA: 0x0004ACE7 File Offset: 0x00048EE7
			public virtual bool SMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["SMimeEnabled"] = value;
				}
			}

			// Token: 0x17000F5E RID: 3934
			// (set) Token: 0x0600275C RID: 10076 RVA: 0x0004ACFF File Offset: 0x00048EFF
			public virtual bool RecoverDeletedItemsEnabled
			{
				set
				{
					base.PowerSharpParameters["RecoverDeletedItemsEnabled"] = value;
				}
			}

			// Token: 0x17000F5F RID: 3935
			// (set) Token: 0x0600275D RID: 10077 RVA: 0x0004AD17 File Offset: 0x00048F17
			public virtual bool InstantMessagingEnabled
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingEnabled"] = value;
				}
			}

			// Token: 0x17000F60 RID: 3936
			// (set) Token: 0x0600275E RID: 10078 RVA: 0x0004AD2F File Offset: 0x00048F2F
			public virtual bool TextMessagingEnabled
			{
				set
				{
					base.PowerSharpParameters["TextMessagingEnabled"] = value;
				}
			}

			// Token: 0x17000F61 RID: 3937
			// (set) Token: 0x0600275F RID: 10079 RVA: 0x0004AD47 File Offset: 0x00048F47
			public virtual bool ForceSaveAttachmentFilteringEnabled
			{
				set
				{
					base.PowerSharpParameters["ForceSaveAttachmentFilteringEnabled"] = value;
				}
			}

			// Token: 0x17000F62 RID: 3938
			// (set) Token: 0x06002760 RID: 10080 RVA: 0x0004AD5F File Offset: 0x00048F5F
			public virtual bool SilverlightEnabled
			{
				set
				{
					base.PowerSharpParameters["SilverlightEnabled"] = value;
				}
			}

			// Token: 0x17000F63 RID: 3939
			// (set) Token: 0x06002761 RID: 10081 RVA: 0x0004AD77 File Offset: 0x00048F77
			public virtual InstantMessagingTypeOptions? InstantMessagingType
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingType"] = value;
				}
			}

			// Token: 0x17000F64 RID: 3940
			// (set) Token: 0x06002762 RID: 10082 RVA: 0x0004AD8F File Offset: 0x00048F8F
			public virtual bool DisplayPhotosEnabled
			{
				set
				{
					base.PowerSharpParameters["DisplayPhotosEnabled"] = value;
				}
			}

			// Token: 0x17000F65 RID: 3941
			// (set) Token: 0x06002763 RID: 10083 RVA: 0x0004ADA7 File Offset: 0x00048FA7
			public virtual bool SetPhotoEnabled
			{
				set
				{
					base.PowerSharpParameters["SetPhotoEnabled"] = value;
				}
			}

			// Token: 0x17000F66 RID: 3942
			// (set) Token: 0x06002764 RID: 10084 RVA: 0x0004ADBF File Offset: 0x00048FBF
			public virtual AllowOfflineOnEnum AllowOfflineOn
			{
				set
				{
					base.PowerSharpParameters["AllowOfflineOn"] = value;
				}
			}

			// Token: 0x17000F67 RID: 3943
			// (set) Token: 0x06002765 RID: 10085 RVA: 0x0004ADD7 File Offset: 0x00048FD7
			public virtual string SetPhotoURL
			{
				set
				{
					base.PowerSharpParameters["SetPhotoURL"] = value;
				}
			}

			// Token: 0x17000F68 RID: 3944
			// (set) Token: 0x06002766 RID: 10086 RVA: 0x0004ADEA File Offset: 0x00048FEA
			public virtual bool PlacesEnabled
			{
				set
				{
					base.PowerSharpParameters["PlacesEnabled"] = value;
				}
			}

			// Token: 0x17000F69 RID: 3945
			// (set) Token: 0x06002767 RID: 10087 RVA: 0x0004AE02 File Offset: 0x00049002
			public virtual bool WeatherEnabled
			{
				set
				{
					base.PowerSharpParameters["WeatherEnabled"] = value;
				}
			}

			// Token: 0x17000F6A RID: 3946
			// (set) Token: 0x06002768 RID: 10088 RVA: 0x0004AE1A File Offset: 0x0004901A
			public virtual bool AllowCopyContactsToDeviceAddressBook
			{
				set
				{
					base.PowerSharpParameters["AllowCopyContactsToDeviceAddressBook"] = value;
				}
			}

			// Token: 0x17000F6B RID: 3947
			// (set) Token: 0x06002769 RID: 10089 RVA: 0x0004AE32 File Offset: 0x00049032
			public virtual bool PredictedActionsEnabled
			{
				set
				{
					base.PowerSharpParameters["PredictedActionsEnabled"] = value;
				}
			}

			// Token: 0x17000F6C RID: 3948
			// (set) Token: 0x0600276A RID: 10090 RVA: 0x0004AE4A File Offset: 0x0004904A
			public virtual bool UserDiagnosticEnabled
			{
				set
				{
					base.PowerSharpParameters["UserDiagnosticEnabled"] = value;
				}
			}

			// Token: 0x17000F6D RID: 3949
			// (set) Token: 0x0600276B RID: 10091 RVA: 0x0004AE62 File Offset: 0x00049062
			public virtual bool FacebookEnabled
			{
				set
				{
					base.PowerSharpParameters["FacebookEnabled"] = value;
				}
			}

			// Token: 0x17000F6E RID: 3950
			// (set) Token: 0x0600276C RID: 10092 RVA: 0x0004AE7A File Offset: 0x0004907A
			public virtual bool LinkedInEnabled
			{
				set
				{
					base.PowerSharpParameters["LinkedInEnabled"] = value;
				}
			}

			// Token: 0x17000F6F RID: 3951
			// (set) Token: 0x0600276D RID: 10093 RVA: 0x0004AE92 File Offset: 0x00049092
			public virtual bool WacExternalServicesEnabled
			{
				set
				{
					base.PowerSharpParameters["WacExternalServicesEnabled"] = value;
				}
			}

			// Token: 0x17000F70 RID: 3952
			// (set) Token: 0x0600276E RID: 10094 RVA: 0x0004AEAA File Offset: 0x000490AA
			public virtual bool WacOMEXEnabled
			{
				set
				{
					base.PowerSharpParameters["WacOMEXEnabled"] = value;
				}
			}

			// Token: 0x17000F71 RID: 3953
			// (set) Token: 0x0600276F RID: 10095 RVA: 0x0004AEC2 File Offset: 0x000490C2
			public virtual bool ReportJunkEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportJunkEmailEnabled"] = value;
				}
			}

			// Token: 0x17000F72 RID: 3954
			// (set) Token: 0x06002770 RID: 10096 RVA: 0x0004AEDA File Offset: 0x000490DA
			public virtual bool GroupCreationEnabled
			{
				set
				{
					base.PowerSharpParameters["GroupCreationEnabled"] = value;
				}
			}

			// Token: 0x17000F73 RID: 3955
			// (set) Token: 0x06002771 RID: 10097 RVA: 0x0004AEF2 File Offset: 0x000490F2
			public virtual bool SkipCreateUnifiedGroupCustomSharepointClassification
			{
				set
				{
					base.PowerSharpParameters["SkipCreateUnifiedGroupCustomSharepointClassification"] = value;
				}
			}

			// Token: 0x17000F74 RID: 3956
			// (set) Token: 0x06002772 RID: 10098 RVA: 0x0004AF0A File Offset: 0x0004910A
			public virtual WebPartsFrameOptions WebPartsFrameOptionsType
			{
				set
				{
					base.PowerSharpParameters["WebPartsFrameOptionsType"] = value;
				}
			}

			// Token: 0x17000F75 RID: 3957
			// (set) Token: 0x06002773 RID: 10099 RVA: 0x0004AF22 File Offset: 0x00049122
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000F76 RID: 3958
			// (set) Token: 0x06002774 RID: 10100 RVA: 0x0004AF35 File Offset: 0x00049135
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000F77 RID: 3959
			// (set) Token: 0x06002775 RID: 10101 RVA: 0x0004AF4D File Offset: 0x0004914D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000F78 RID: 3960
			// (set) Token: 0x06002776 RID: 10102 RVA: 0x0004AF65 File Offset: 0x00049165
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000F79 RID: 3961
			// (set) Token: 0x06002777 RID: 10103 RVA: 0x0004AF7D File Offset: 0x0004917D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000F7A RID: 3962
			// (set) Token: 0x06002778 RID: 10104 RVA: 0x0004AF95 File Offset: 0x00049195
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
