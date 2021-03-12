using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000983 RID: 2435
	public class SetHostedEncryptionVirtualDirectoryCommand : SyntheticCommandWithPipelineInputNoOutput<ADE4eVirtualDirectory>
	{
		// Token: 0x06007983 RID: 31107 RVA: 0x000B5677 File Offset: 0x000B3877
		private SetHostedEncryptionVirtualDirectoryCommand() : base("Set-HostedEncryptionVirtualDirectory")
		{
		}

		// Token: 0x06007984 RID: 31108 RVA: 0x000B5684 File Offset: 0x000B3884
		public SetHostedEncryptionVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007985 RID: 31109 RVA: 0x000B5693 File Offset: 0x000B3893
		public virtual SetHostedEncryptionVirtualDirectoryCommand SetParameters(SetHostedEncryptionVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007986 RID: 31110 RVA: 0x000B569D File Offset: 0x000B389D
		public virtual SetHostedEncryptionVirtualDirectoryCommand SetParameters(SetHostedEncryptionVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000984 RID: 2436
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005236 RID: 21046
			// (set) Token: 0x06007987 RID: 31111 RVA: 0x000B56A7 File Offset: 0x000B38A7
			public virtual MultiValuedProperty<string> AllowedFileTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedFileTypes"] = value;
				}
			}

			// Token: 0x17005237 RID: 21047
			// (set) Token: 0x06007988 RID: 31112 RVA: 0x000B56BA File Offset: 0x000B38BA
			public virtual MultiValuedProperty<string> AllowedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedMimeTypes"] = value;
				}
			}

			// Token: 0x17005238 RID: 21048
			// (set) Token: 0x06007989 RID: 31113 RVA: 0x000B56CD File Offset: 0x000B38CD
			public virtual MultiValuedProperty<string> BlockedFileTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedFileTypes"] = value;
				}
			}

			// Token: 0x17005239 RID: 21049
			// (set) Token: 0x0600798A RID: 31114 RVA: 0x000B56E0 File Offset: 0x000B38E0
			public virtual MultiValuedProperty<string> BlockedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedMimeTypes"] = value;
				}
			}

			// Token: 0x1700523A RID: 21050
			// (set) Token: 0x0600798B RID: 31115 RVA: 0x000B56F3 File Offset: 0x000B38F3
			public virtual MultiValuedProperty<string> ForceSaveFileTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveFileTypes"] = value;
				}
			}

			// Token: 0x1700523B RID: 21051
			// (set) Token: 0x0600798C RID: 31116 RVA: 0x000B5706 File Offset: 0x000B3906
			public virtual MultiValuedProperty<string> ForceSaveMimeTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveMimeTypes"] = value;
				}
			}

			// Token: 0x1700523C RID: 21052
			// (set) Token: 0x0600798D RID: 31117 RVA: 0x000B5719 File Offset: 0x000B3919
			public virtual bool? AlwaysShowBcc
			{
				set
				{
					base.PowerSharpParameters["AlwaysShowBcc"] = value;
				}
			}

			// Token: 0x1700523D RID: 21053
			// (set) Token: 0x0600798E RID: 31118 RVA: 0x000B5731 File Offset: 0x000B3931
			public virtual bool? CheckForForgottenAttachments
			{
				set
				{
					base.PowerSharpParameters["CheckForForgottenAttachments"] = value;
				}
			}

			// Token: 0x1700523E RID: 21054
			// (set) Token: 0x0600798F RID: 31119 RVA: 0x000B5749 File Offset: 0x000B3949
			public virtual bool? HideMailTipsByDefault
			{
				set
				{
					base.PowerSharpParameters["HideMailTipsByDefault"] = value;
				}
			}

			// Token: 0x1700523F RID: 21055
			// (set) Token: 0x06007990 RID: 31120 RVA: 0x000B5761 File Offset: 0x000B3961
			public virtual uint? MailTipsLargeAudienceThreshold
			{
				set
				{
					base.PowerSharpParameters["MailTipsLargeAudienceThreshold"] = value;
				}
			}

			// Token: 0x17005240 RID: 21056
			// (set) Token: 0x06007991 RID: 31121 RVA: 0x000B5779 File Offset: 0x000B3979
			public virtual int? MaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x17005241 RID: 21057
			// (set) Token: 0x06007992 RID: 31122 RVA: 0x000B5791 File Offset: 0x000B3991
			public virtual int? MaxMessageSizeInKb
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSizeInKb"] = value;
				}
			}

			// Token: 0x17005242 RID: 21058
			// (set) Token: 0x06007993 RID: 31123 RVA: 0x000B57A9 File Offset: 0x000B39A9
			public virtual string ComposeFontColor
			{
				set
				{
					base.PowerSharpParameters["ComposeFontColor"] = value;
				}
			}

			// Token: 0x17005243 RID: 21059
			// (set) Token: 0x06007994 RID: 31124 RVA: 0x000B57BC File Offset: 0x000B39BC
			public virtual string ComposeFontName
			{
				set
				{
					base.PowerSharpParameters["ComposeFontName"] = value;
				}
			}

			// Token: 0x17005244 RID: 21060
			// (set) Token: 0x06007995 RID: 31125 RVA: 0x000B57CF File Offset: 0x000B39CF
			public virtual int? ComposeFontSize
			{
				set
				{
					base.PowerSharpParameters["ComposeFontSize"] = value;
				}
			}

			// Token: 0x17005245 RID: 21061
			// (set) Token: 0x06007996 RID: 31126 RVA: 0x000B57E7 File Offset: 0x000B39E7
			public virtual int? MaxImageSizeKB
			{
				set
				{
					base.PowerSharpParameters["MaxImageSizeKB"] = value;
				}
			}

			// Token: 0x17005246 RID: 21062
			// (set) Token: 0x06007997 RID: 31127 RVA: 0x000B57FF File Offset: 0x000B39FF
			public virtual int? MaxAttachmentSizeKB
			{
				set
				{
					base.PowerSharpParameters["MaxAttachmentSizeKB"] = value;
				}
			}

			// Token: 0x17005247 RID: 21063
			// (set) Token: 0x06007998 RID: 31128 RVA: 0x000B5817 File Offset: 0x000B3A17
			public virtual int? MaxEncryptedContentSizeKB
			{
				set
				{
					base.PowerSharpParameters["MaxEncryptedContentSizeKB"] = value;
				}
			}

			// Token: 0x17005248 RID: 21064
			// (set) Token: 0x06007999 RID: 31129 RVA: 0x000B582F File Offset: 0x000B3A2F
			public virtual int? MaxEmailStringSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailStringSize"] = value;
				}
			}

			// Token: 0x17005249 RID: 21065
			// (set) Token: 0x0600799A RID: 31130 RVA: 0x000B5847 File Offset: 0x000B3A47
			public virtual int? MaxPortalStringSize
			{
				set
				{
					base.PowerSharpParameters["MaxPortalStringSize"] = value;
				}
			}

			// Token: 0x1700524A RID: 21066
			// (set) Token: 0x0600799B RID: 31131 RVA: 0x000B585F File Offset: 0x000B3A5F
			public virtual int? MaxFwdAllowed
			{
				set
				{
					base.PowerSharpParameters["MaxFwdAllowed"] = value;
				}
			}

			// Token: 0x1700524B RID: 21067
			// (set) Token: 0x0600799C RID: 31132 RVA: 0x000B5877 File Offset: 0x000B3A77
			public virtual int? PortalInactivityTimeout
			{
				set
				{
					base.PowerSharpParameters["PortalInactivityTimeout"] = value;
				}
			}

			// Token: 0x1700524C RID: 21068
			// (set) Token: 0x0600799D RID: 31133 RVA: 0x000B588F File Offset: 0x000B3A8F
			public virtual int? TDSTimeOut
			{
				set
				{
					base.PowerSharpParameters["TDSTimeOut"] = value;
				}
			}

			// Token: 0x1700524D RID: 21069
			// (set) Token: 0x0600799E RID: 31134 RVA: 0x000B58A7 File Offset: 0x000B3AA7
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x1700524E RID: 21070
			// (set) Token: 0x0600799F RID: 31135 RVA: 0x000B58BF File Offset: 0x000B3ABF
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x1700524F RID: 21071
			// (set) Token: 0x060079A0 RID: 31136 RVA: 0x000B58D7 File Offset: 0x000B3AD7
			public virtual bool LiveIdAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdAuthentication"] = value;
				}
			}

			// Token: 0x17005250 RID: 21072
			// (set) Token: 0x060079A1 RID: 31137 RVA: 0x000B58EF File Offset: 0x000B3AEF
			public virtual GzipLevel GzipLevel
			{
				set
				{
					base.PowerSharpParameters["GzipLevel"] = value;
				}
			}

			// Token: 0x17005251 RID: 21073
			// (set) Token: 0x060079A2 RID: 31138 RVA: 0x000B5907 File Offset: 0x000B3B07
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005252 RID: 21074
			// (set) Token: 0x060079A3 RID: 31139 RVA: 0x000B591F File Offset: 0x000B3B1F
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17005253 RID: 21075
			// (set) Token: 0x060079A4 RID: 31140 RVA: 0x000B5932 File Offset: 0x000B3B32
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x17005254 RID: 21076
			// (set) Token: 0x060079A5 RID: 31141 RVA: 0x000B5945 File Offset: 0x000B3B45
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x17005255 RID: 21077
			// (set) Token: 0x060079A6 RID: 31142 RVA: 0x000B5958 File Offset: 0x000B3B58
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x17005256 RID: 21078
			// (set) Token: 0x060079A7 RID: 31143 RVA: 0x000B596B File Offset: 0x000B3B6B
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x17005257 RID: 21079
			// (set) Token: 0x060079A8 RID: 31144 RVA: 0x000B597E File Offset: 0x000B3B7E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005258 RID: 21080
			// (set) Token: 0x060079A9 RID: 31145 RVA: 0x000B5991 File Offset: 0x000B3B91
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005259 RID: 21081
			// (set) Token: 0x060079AA RID: 31146 RVA: 0x000B59A9 File Offset: 0x000B3BA9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700525A RID: 21082
			// (set) Token: 0x060079AB RID: 31147 RVA: 0x000B59C1 File Offset: 0x000B3BC1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700525B RID: 21083
			// (set) Token: 0x060079AC RID: 31148 RVA: 0x000B59D9 File Offset: 0x000B3BD9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700525C RID: 21084
			// (set) Token: 0x060079AD RID: 31149 RVA: 0x000B59F1 File Offset: 0x000B3BF1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000985 RID: 2437
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700525D RID: 21085
			// (set) Token: 0x060079AF RID: 31151 RVA: 0x000B5A11 File Offset: 0x000B3C11
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700525E RID: 21086
			// (set) Token: 0x060079B0 RID: 31152 RVA: 0x000B5A24 File Offset: 0x000B3C24
			public virtual MultiValuedProperty<string> AllowedFileTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedFileTypes"] = value;
				}
			}

			// Token: 0x1700525F RID: 21087
			// (set) Token: 0x060079B1 RID: 31153 RVA: 0x000B5A37 File Offset: 0x000B3C37
			public virtual MultiValuedProperty<string> AllowedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedMimeTypes"] = value;
				}
			}

			// Token: 0x17005260 RID: 21088
			// (set) Token: 0x060079B2 RID: 31154 RVA: 0x000B5A4A File Offset: 0x000B3C4A
			public virtual MultiValuedProperty<string> BlockedFileTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedFileTypes"] = value;
				}
			}

			// Token: 0x17005261 RID: 21089
			// (set) Token: 0x060079B3 RID: 31155 RVA: 0x000B5A5D File Offset: 0x000B3C5D
			public virtual MultiValuedProperty<string> BlockedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedMimeTypes"] = value;
				}
			}

			// Token: 0x17005262 RID: 21090
			// (set) Token: 0x060079B4 RID: 31156 RVA: 0x000B5A70 File Offset: 0x000B3C70
			public virtual MultiValuedProperty<string> ForceSaveFileTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveFileTypes"] = value;
				}
			}

			// Token: 0x17005263 RID: 21091
			// (set) Token: 0x060079B5 RID: 31157 RVA: 0x000B5A83 File Offset: 0x000B3C83
			public virtual MultiValuedProperty<string> ForceSaveMimeTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveMimeTypes"] = value;
				}
			}

			// Token: 0x17005264 RID: 21092
			// (set) Token: 0x060079B6 RID: 31158 RVA: 0x000B5A96 File Offset: 0x000B3C96
			public virtual bool? AlwaysShowBcc
			{
				set
				{
					base.PowerSharpParameters["AlwaysShowBcc"] = value;
				}
			}

			// Token: 0x17005265 RID: 21093
			// (set) Token: 0x060079B7 RID: 31159 RVA: 0x000B5AAE File Offset: 0x000B3CAE
			public virtual bool? CheckForForgottenAttachments
			{
				set
				{
					base.PowerSharpParameters["CheckForForgottenAttachments"] = value;
				}
			}

			// Token: 0x17005266 RID: 21094
			// (set) Token: 0x060079B8 RID: 31160 RVA: 0x000B5AC6 File Offset: 0x000B3CC6
			public virtual bool? HideMailTipsByDefault
			{
				set
				{
					base.PowerSharpParameters["HideMailTipsByDefault"] = value;
				}
			}

			// Token: 0x17005267 RID: 21095
			// (set) Token: 0x060079B9 RID: 31161 RVA: 0x000B5ADE File Offset: 0x000B3CDE
			public virtual uint? MailTipsLargeAudienceThreshold
			{
				set
				{
					base.PowerSharpParameters["MailTipsLargeAudienceThreshold"] = value;
				}
			}

			// Token: 0x17005268 RID: 21096
			// (set) Token: 0x060079BA RID: 31162 RVA: 0x000B5AF6 File Offset: 0x000B3CF6
			public virtual int? MaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x17005269 RID: 21097
			// (set) Token: 0x060079BB RID: 31163 RVA: 0x000B5B0E File Offset: 0x000B3D0E
			public virtual int? MaxMessageSizeInKb
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSizeInKb"] = value;
				}
			}

			// Token: 0x1700526A RID: 21098
			// (set) Token: 0x060079BC RID: 31164 RVA: 0x000B5B26 File Offset: 0x000B3D26
			public virtual string ComposeFontColor
			{
				set
				{
					base.PowerSharpParameters["ComposeFontColor"] = value;
				}
			}

			// Token: 0x1700526B RID: 21099
			// (set) Token: 0x060079BD RID: 31165 RVA: 0x000B5B39 File Offset: 0x000B3D39
			public virtual string ComposeFontName
			{
				set
				{
					base.PowerSharpParameters["ComposeFontName"] = value;
				}
			}

			// Token: 0x1700526C RID: 21100
			// (set) Token: 0x060079BE RID: 31166 RVA: 0x000B5B4C File Offset: 0x000B3D4C
			public virtual int? ComposeFontSize
			{
				set
				{
					base.PowerSharpParameters["ComposeFontSize"] = value;
				}
			}

			// Token: 0x1700526D RID: 21101
			// (set) Token: 0x060079BF RID: 31167 RVA: 0x000B5B64 File Offset: 0x000B3D64
			public virtual int? MaxImageSizeKB
			{
				set
				{
					base.PowerSharpParameters["MaxImageSizeKB"] = value;
				}
			}

			// Token: 0x1700526E RID: 21102
			// (set) Token: 0x060079C0 RID: 31168 RVA: 0x000B5B7C File Offset: 0x000B3D7C
			public virtual int? MaxAttachmentSizeKB
			{
				set
				{
					base.PowerSharpParameters["MaxAttachmentSizeKB"] = value;
				}
			}

			// Token: 0x1700526F RID: 21103
			// (set) Token: 0x060079C1 RID: 31169 RVA: 0x000B5B94 File Offset: 0x000B3D94
			public virtual int? MaxEncryptedContentSizeKB
			{
				set
				{
					base.PowerSharpParameters["MaxEncryptedContentSizeKB"] = value;
				}
			}

			// Token: 0x17005270 RID: 21104
			// (set) Token: 0x060079C2 RID: 31170 RVA: 0x000B5BAC File Offset: 0x000B3DAC
			public virtual int? MaxEmailStringSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailStringSize"] = value;
				}
			}

			// Token: 0x17005271 RID: 21105
			// (set) Token: 0x060079C3 RID: 31171 RVA: 0x000B5BC4 File Offset: 0x000B3DC4
			public virtual int? MaxPortalStringSize
			{
				set
				{
					base.PowerSharpParameters["MaxPortalStringSize"] = value;
				}
			}

			// Token: 0x17005272 RID: 21106
			// (set) Token: 0x060079C4 RID: 31172 RVA: 0x000B5BDC File Offset: 0x000B3DDC
			public virtual int? MaxFwdAllowed
			{
				set
				{
					base.PowerSharpParameters["MaxFwdAllowed"] = value;
				}
			}

			// Token: 0x17005273 RID: 21107
			// (set) Token: 0x060079C5 RID: 31173 RVA: 0x000B5BF4 File Offset: 0x000B3DF4
			public virtual int? PortalInactivityTimeout
			{
				set
				{
					base.PowerSharpParameters["PortalInactivityTimeout"] = value;
				}
			}

			// Token: 0x17005274 RID: 21108
			// (set) Token: 0x060079C6 RID: 31174 RVA: 0x000B5C0C File Offset: 0x000B3E0C
			public virtual int? TDSTimeOut
			{
				set
				{
					base.PowerSharpParameters["TDSTimeOut"] = value;
				}
			}

			// Token: 0x17005275 RID: 21109
			// (set) Token: 0x060079C7 RID: 31175 RVA: 0x000B5C24 File Offset: 0x000B3E24
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x17005276 RID: 21110
			// (set) Token: 0x060079C8 RID: 31176 RVA: 0x000B5C3C File Offset: 0x000B3E3C
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x17005277 RID: 21111
			// (set) Token: 0x060079C9 RID: 31177 RVA: 0x000B5C54 File Offset: 0x000B3E54
			public virtual bool LiveIdAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdAuthentication"] = value;
				}
			}

			// Token: 0x17005278 RID: 21112
			// (set) Token: 0x060079CA RID: 31178 RVA: 0x000B5C6C File Offset: 0x000B3E6C
			public virtual GzipLevel GzipLevel
			{
				set
				{
					base.PowerSharpParameters["GzipLevel"] = value;
				}
			}

			// Token: 0x17005279 RID: 21113
			// (set) Token: 0x060079CB RID: 31179 RVA: 0x000B5C84 File Offset: 0x000B3E84
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x1700527A RID: 21114
			// (set) Token: 0x060079CC RID: 31180 RVA: 0x000B5C9C File Offset: 0x000B3E9C
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x1700527B RID: 21115
			// (set) Token: 0x060079CD RID: 31181 RVA: 0x000B5CAF File Offset: 0x000B3EAF
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x1700527C RID: 21116
			// (set) Token: 0x060079CE RID: 31182 RVA: 0x000B5CC2 File Offset: 0x000B3EC2
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700527D RID: 21117
			// (set) Token: 0x060079CF RID: 31183 RVA: 0x000B5CD5 File Offset: 0x000B3ED5
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x1700527E RID: 21118
			// (set) Token: 0x060079D0 RID: 31184 RVA: 0x000B5CE8 File Offset: 0x000B3EE8
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x1700527F RID: 21119
			// (set) Token: 0x060079D1 RID: 31185 RVA: 0x000B5CFB File Offset: 0x000B3EFB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005280 RID: 21120
			// (set) Token: 0x060079D2 RID: 31186 RVA: 0x000B5D0E File Offset: 0x000B3F0E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005281 RID: 21121
			// (set) Token: 0x060079D3 RID: 31187 RVA: 0x000B5D26 File Offset: 0x000B3F26
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005282 RID: 21122
			// (set) Token: 0x060079D4 RID: 31188 RVA: 0x000B5D3E File Offset: 0x000B3F3E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005283 RID: 21123
			// (set) Token: 0x060079D5 RID: 31189 RVA: 0x000B5D56 File Offset: 0x000B3F56
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005284 RID: 21124
			// (set) Token: 0x060079D6 RID: 31190 RVA: 0x000B5D6E File Offset: 0x000B3F6E
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
