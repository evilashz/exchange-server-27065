using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D02 RID: 3330
	public class NewMailUserCommand : SyntheticCommandWithPipelineInputNoOutput<SecureString>
	{
		// Token: 0x0600AF61 RID: 44897 RVA: 0x000FD3EE File Offset: 0x000FB5EE
		private NewMailUserCommand() : base("New-MailUser")
		{
		}

		// Token: 0x0600AF62 RID: 44898 RVA: 0x000FD3FB File Offset: 0x000FB5FB
		public NewMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600AF63 RID: 44899 RVA: 0x000FD40A File Offset: 0x000FB60A
		public virtual NewMailUserCommand SetParameters(NewMailUserCommand.EnabledUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF64 RID: 44900 RVA: 0x000FD414 File Offset: 0x000FB614
		public virtual NewMailUserCommand SetParameters(NewMailUserCommand.MicrosoftOnlineServicesIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF65 RID: 44901 RVA: 0x000FD41E File Offset: 0x000FB61E
		public virtual NewMailUserCommand SetParameters(NewMailUserCommand.WindowsLiveIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF66 RID: 44902 RVA: 0x000FD428 File Offset: 0x000FB628
		public virtual NewMailUserCommand SetParameters(NewMailUserCommand.FederatedUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF67 RID: 44903 RVA: 0x000FD432 File Offset: 0x000FB632
		public virtual NewMailUserCommand SetParameters(NewMailUserCommand.WindowsLiveCustomDomainsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF68 RID: 44904 RVA: 0x000FD43C File Offset: 0x000FB63C
		public virtual NewMailUserCommand SetParameters(NewMailUserCommand.ImportLiveIdParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF69 RID: 44905 RVA: 0x000FD446 File Offset: 0x000FB646
		public virtual NewMailUserCommand SetParameters(NewMailUserCommand.DisabledUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF6A RID: 44906 RVA: 0x000FD450 File Offset: 0x000FB650
		public virtual NewMailUserCommand SetParameters(NewMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF6B RID: 44907 RVA: 0x000FD45A File Offset: 0x000FB65A
		public virtual NewMailUserCommand SetParameters(NewMailUserCommand.EnableRoomMailboxAccountParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AF6C RID: 44908 RVA: 0x000FD464 File Offset: 0x000FB664
		public virtual NewMailUserCommand SetParameters(NewMailUserCommand.MicrosoftOnlineServicesFederatedUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D03 RID: 3331
		public class EnabledUserParameters : ParametersBase
		{
			// Token: 0x17008116 RID: 33046
			// (set) Token: 0x0600AF6D RID: 44909 RVA: 0x000FD46E File Offset: 0x000FB66E
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17008117 RID: 33047
			// (set) Token: 0x0600AF6E RID: 44910 RVA: 0x000FD481 File Offset: 0x000FB681
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17008118 RID: 33048
			// (set) Token: 0x0600AF6F RID: 44911 RVA: 0x000FD494 File Offset: 0x000FB694
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17008119 RID: 33049
			// (set) Token: 0x0600AF70 RID: 44912 RVA: 0x000FD4A7 File Offset: 0x000FB6A7
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x1700811A RID: 33050
			// (set) Token: 0x0600AF71 RID: 44913 RVA: 0x000FD4BF File Offset: 0x000FB6BF
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x1700811B RID: 33051
			// (set) Token: 0x0600AF72 RID: 44914 RVA: 0x000FD4D7 File Offset: 0x000FB6D7
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x1700811C RID: 33052
			// (set) Token: 0x0600AF73 RID: 44915 RVA: 0x000FD4EF File Offset: 0x000FB6EF
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x1700811D RID: 33053
			// (set) Token: 0x0600AF74 RID: 44916 RVA: 0x000FD507 File Offset: 0x000FB707
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700811E RID: 33054
			// (set) Token: 0x0600AF75 RID: 44917 RVA: 0x000FD51A File Offset: 0x000FB71A
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700811F RID: 33055
			// (set) Token: 0x0600AF76 RID: 44918 RVA: 0x000FD532 File Offset: 0x000FB732
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008120 RID: 33056
			// (set) Token: 0x0600AF77 RID: 44919 RVA: 0x000FD545 File Offset: 0x000FB745
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008121 RID: 33057
			// (set) Token: 0x0600AF78 RID: 44920 RVA: 0x000FD558 File Offset: 0x000FB758
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008122 RID: 33058
			// (set) Token: 0x0600AF79 RID: 44921 RVA: 0x000FD56B File Offset: 0x000FB76B
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008123 RID: 33059
			// (set) Token: 0x0600AF7A RID: 44922 RVA: 0x000FD57E File Offset: 0x000FB77E
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008124 RID: 33060
			// (set) Token: 0x0600AF7B RID: 44923 RVA: 0x000FD591 File Offset: 0x000FB791
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008125 RID: 33061
			// (set) Token: 0x0600AF7C RID: 44924 RVA: 0x000FD5A4 File Offset: 0x000FB7A4
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008126 RID: 33062
			// (set) Token: 0x0600AF7D RID: 44925 RVA: 0x000FD5BC File Offset: 0x000FB7BC
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008127 RID: 33063
			// (set) Token: 0x0600AF7E RID: 44926 RVA: 0x000FD5CF File Offset: 0x000FB7CF
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008128 RID: 33064
			// (set) Token: 0x0600AF7F RID: 44927 RVA: 0x000FD5E7 File Offset: 0x000FB7E7
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008129 RID: 33065
			// (set) Token: 0x0600AF80 RID: 44928 RVA: 0x000FD5FA File Offset: 0x000FB7FA
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700812A RID: 33066
			// (set) Token: 0x0600AF81 RID: 44929 RVA: 0x000FD612 File Offset: 0x000FB812
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700812B RID: 33067
			// (set) Token: 0x0600AF82 RID: 44930 RVA: 0x000FD625 File Offset: 0x000FB825
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700812C RID: 33068
			// (set) Token: 0x0600AF83 RID: 44931 RVA: 0x000FD643 File Offset: 0x000FB843
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700812D RID: 33069
			// (set) Token: 0x0600AF84 RID: 44932 RVA: 0x000FD656 File Offset: 0x000FB856
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700812E RID: 33070
			// (set) Token: 0x0600AF85 RID: 44933 RVA: 0x000FD66E File Offset: 0x000FB86E
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700812F RID: 33071
			// (set) Token: 0x0600AF86 RID: 44934 RVA: 0x000FD686 File Offset: 0x000FB886
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008130 RID: 33072
			// (set) Token: 0x0600AF87 RID: 44935 RVA: 0x000FD69E File Offset: 0x000FB89E
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008131 RID: 33073
			// (set) Token: 0x0600AF88 RID: 44936 RVA: 0x000FD6B1 File Offset: 0x000FB8B1
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008132 RID: 33074
			// (set) Token: 0x0600AF89 RID: 44937 RVA: 0x000FD6C9 File Offset: 0x000FB8C9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008133 RID: 33075
			// (set) Token: 0x0600AF8A RID: 44938 RVA: 0x000FD6DC File Offset: 0x000FB8DC
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008134 RID: 33076
			// (set) Token: 0x0600AF8B RID: 44939 RVA: 0x000FD6EF File Offset: 0x000FB8EF
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008135 RID: 33077
			// (set) Token: 0x0600AF8C RID: 44940 RVA: 0x000FD70D File Offset: 0x000FB90D
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008136 RID: 33078
			// (set) Token: 0x0600AF8D RID: 44941 RVA: 0x000FD720 File Offset: 0x000FB920
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008137 RID: 33079
			// (set) Token: 0x0600AF8E RID: 44942 RVA: 0x000FD73E File Offset: 0x000FB93E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008138 RID: 33080
			// (set) Token: 0x0600AF8F RID: 44943 RVA: 0x000FD751 File Offset: 0x000FB951
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008139 RID: 33081
			// (set) Token: 0x0600AF90 RID: 44944 RVA: 0x000FD769 File Offset: 0x000FB969
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700813A RID: 33082
			// (set) Token: 0x0600AF91 RID: 44945 RVA: 0x000FD781 File Offset: 0x000FB981
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700813B RID: 33083
			// (set) Token: 0x0600AF92 RID: 44946 RVA: 0x000FD799 File Offset: 0x000FB999
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700813C RID: 33084
			// (set) Token: 0x0600AF93 RID: 44947 RVA: 0x000FD7B1 File Offset: 0x000FB9B1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D04 RID: 3332
		public class MicrosoftOnlineServicesIDParameters : ParametersBase
		{
			// Token: 0x1700813D RID: 33085
			// (set) Token: 0x0600AF95 RID: 44949 RVA: 0x000FD7D1 File Offset: 0x000FB9D1
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700813E RID: 33086
			// (set) Token: 0x0600AF96 RID: 44950 RVA: 0x000FD7E4 File Offset: 0x000FB9E4
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x1700813F RID: 33087
			// (set) Token: 0x0600AF97 RID: 44951 RVA: 0x000FD7F7 File Offset: 0x000FB9F7
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008140 RID: 33088
			// (set) Token: 0x0600AF98 RID: 44952 RVA: 0x000FD80A File Offset: 0x000FBA0A
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17008141 RID: 33089
			// (set) Token: 0x0600AF99 RID: 44953 RVA: 0x000FD81D File Offset: 0x000FBA1D
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008142 RID: 33090
			// (set) Token: 0x0600AF9A RID: 44954 RVA: 0x000FD835 File Offset: 0x000FBA35
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008143 RID: 33091
			// (set) Token: 0x0600AF9B RID: 44955 RVA: 0x000FD848 File Offset: 0x000FBA48
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008144 RID: 33092
			// (set) Token: 0x0600AF9C RID: 44956 RVA: 0x000FD85B File Offset: 0x000FBA5B
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008145 RID: 33093
			// (set) Token: 0x0600AF9D RID: 44957 RVA: 0x000FD86E File Offset: 0x000FBA6E
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008146 RID: 33094
			// (set) Token: 0x0600AF9E RID: 44958 RVA: 0x000FD881 File Offset: 0x000FBA81
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008147 RID: 33095
			// (set) Token: 0x0600AF9F RID: 44959 RVA: 0x000FD894 File Offset: 0x000FBA94
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008148 RID: 33096
			// (set) Token: 0x0600AFA0 RID: 44960 RVA: 0x000FD8A7 File Offset: 0x000FBAA7
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008149 RID: 33097
			// (set) Token: 0x0600AFA1 RID: 44961 RVA: 0x000FD8BF File Offset: 0x000FBABF
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700814A RID: 33098
			// (set) Token: 0x0600AFA2 RID: 44962 RVA: 0x000FD8D2 File Offset: 0x000FBAD2
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700814B RID: 33099
			// (set) Token: 0x0600AFA3 RID: 44963 RVA: 0x000FD8EA File Offset: 0x000FBAEA
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700814C RID: 33100
			// (set) Token: 0x0600AFA4 RID: 44964 RVA: 0x000FD8FD File Offset: 0x000FBAFD
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700814D RID: 33101
			// (set) Token: 0x0600AFA5 RID: 44965 RVA: 0x000FD915 File Offset: 0x000FBB15
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700814E RID: 33102
			// (set) Token: 0x0600AFA6 RID: 44966 RVA: 0x000FD928 File Offset: 0x000FBB28
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700814F RID: 33103
			// (set) Token: 0x0600AFA7 RID: 44967 RVA: 0x000FD946 File Offset: 0x000FBB46
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008150 RID: 33104
			// (set) Token: 0x0600AFA8 RID: 44968 RVA: 0x000FD959 File Offset: 0x000FBB59
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008151 RID: 33105
			// (set) Token: 0x0600AFA9 RID: 44969 RVA: 0x000FD971 File Offset: 0x000FBB71
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008152 RID: 33106
			// (set) Token: 0x0600AFAA RID: 44970 RVA: 0x000FD989 File Offset: 0x000FBB89
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008153 RID: 33107
			// (set) Token: 0x0600AFAB RID: 44971 RVA: 0x000FD9A1 File Offset: 0x000FBBA1
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008154 RID: 33108
			// (set) Token: 0x0600AFAC RID: 44972 RVA: 0x000FD9B4 File Offset: 0x000FBBB4
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008155 RID: 33109
			// (set) Token: 0x0600AFAD RID: 44973 RVA: 0x000FD9CC File Offset: 0x000FBBCC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008156 RID: 33110
			// (set) Token: 0x0600AFAE RID: 44974 RVA: 0x000FD9DF File Offset: 0x000FBBDF
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008157 RID: 33111
			// (set) Token: 0x0600AFAF RID: 44975 RVA: 0x000FD9F2 File Offset: 0x000FBBF2
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17008158 RID: 33112
			// (set) Token: 0x0600AFB0 RID: 44976 RVA: 0x000FDA10 File Offset: 0x000FBC10
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008159 RID: 33113
			// (set) Token: 0x0600AFB1 RID: 44977 RVA: 0x000FDA23 File Offset: 0x000FBC23
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700815A RID: 33114
			// (set) Token: 0x0600AFB2 RID: 44978 RVA: 0x000FDA41 File Offset: 0x000FBC41
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700815B RID: 33115
			// (set) Token: 0x0600AFB3 RID: 44979 RVA: 0x000FDA54 File Offset: 0x000FBC54
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700815C RID: 33116
			// (set) Token: 0x0600AFB4 RID: 44980 RVA: 0x000FDA6C File Offset: 0x000FBC6C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700815D RID: 33117
			// (set) Token: 0x0600AFB5 RID: 44981 RVA: 0x000FDA84 File Offset: 0x000FBC84
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700815E RID: 33118
			// (set) Token: 0x0600AFB6 RID: 44982 RVA: 0x000FDA9C File Offset: 0x000FBC9C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700815F RID: 33119
			// (set) Token: 0x0600AFB7 RID: 44983 RVA: 0x000FDAB4 File Offset: 0x000FBCB4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D05 RID: 3333
		public class WindowsLiveIDParameters : ParametersBase
		{
			// Token: 0x17008160 RID: 33120
			// (set) Token: 0x0600AFB9 RID: 44985 RVA: 0x000FDAD4 File Offset: 0x000FBCD4
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17008161 RID: 33121
			// (set) Token: 0x0600AFBA RID: 44986 RVA: 0x000FDAE7 File Offset: 0x000FBCE7
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17008162 RID: 33122
			// (set) Token: 0x0600AFBB RID: 44987 RVA: 0x000FDAFA File Offset: 0x000FBCFA
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008163 RID: 33123
			// (set) Token: 0x0600AFBC RID: 44988 RVA: 0x000FDB0D File Offset: 0x000FBD0D
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17008164 RID: 33124
			// (set) Token: 0x0600AFBD RID: 44989 RVA: 0x000FDB20 File Offset: 0x000FBD20
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x17008165 RID: 33125
			// (set) Token: 0x0600AFBE RID: 44990 RVA: 0x000FDB38 File Offset: 0x000FBD38
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008166 RID: 33126
			// (set) Token: 0x0600AFBF RID: 44991 RVA: 0x000FDB50 File Offset: 0x000FBD50
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008167 RID: 33127
			// (set) Token: 0x0600AFC0 RID: 44992 RVA: 0x000FDB63 File Offset: 0x000FBD63
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008168 RID: 33128
			// (set) Token: 0x0600AFC1 RID: 44993 RVA: 0x000FDB76 File Offset: 0x000FBD76
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008169 RID: 33129
			// (set) Token: 0x0600AFC2 RID: 44994 RVA: 0x000FDB89 File Offset: 0x000FBD89
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700816A RID: 33130
			// (set) Token: 0x0600AFC3 RID: 44995 RVA: 0x000FDB9C File Offset: 0x000FBD9C
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700816B RID: 33131
			// (set) Token: 0x0600AFC4 RID: 44996 RVA: 0x000FDBAF File Offset: 0x000FBDAF
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700816C RID: 33132
			// (set) Token: 0x0600AFC5 RID: 44997 RVA: 0x000FDBC2 File Offset: 0x000FBDC2
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700816D RID: 33133
			// (set) Token: 0x0600AFC6 RID: 44998 RVA: 0x000FDBDA File Offset: 0x000FBDDA
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700816E RID: 33134
			// (set) Token: 0x0600AFC7 RID: 44999 RVA: 0x000FDBED File Offset: 0x000FBDED
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700816F RID: 33135
			// (set) Token: 0x0600AFC8 RID: 45000 RVA: 0x000FDC05 File Offset: 0x000FBE05
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008170 RID: 33136
			// (set) Token: 0x0600AFC9 RID: 45001 RVA: 0x000FDC18 File Offset: 0x000FBE18
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008171 RID: 33137
			// (set) Token: 0x0600AFCA RID: 45002 RVA: 0x000FDC30 File Offset: 0x000FBE30
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008172 RID: 33138
			// (set) Token: 0x0600AFCB RID: 45003 RVA: 0x000FDC43 File Offset: 0x000FBE43
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008173 RID: 33139
			// (set) Token: 0x0600AFCC RID: 45004 RVA: 0x000FDC61 File Offset: 0x000FBE61
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008174 RID: 33140
			// (set) Token: 0x0600AFCD RID: 45005 RVA: 0x000FDC74 File Offset: 0x000FBE74
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008175 RID: 33141
			// (set) Token: 0x0600AFCE RID: 45006 RVA: 0x000FDC8C File Offset: 0x000FBE8C
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008176 RID: 33142
			// (set) Token: 0x0600AFCF RID: 45007 RVA: 0x000FDCA4 File Offset: 0x000FBEA4
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008177 RID: 33143
			// (set) Token: 0x0600AFD0 RID: 45008 RVA: 0x000FDCBC File Offset: 0x000FBEBC
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008178 RID: 33144
			// (set) Token: 0x0600AFD1 RID: 45009 RVA: 0x000FDCCF File Offset: 0x000FBECF
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008179 RID: 33145
			// (set) Token: 0x0600AFD2 RID: 45010 RVA: 0x000FDCE7 File Offset: 0x000FBEE7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700817A RID: 33146
			// (set) Token: 0x0600AFD3 RID: 45011 RVA: 0x000FDCFA File Offset: 0x000FBEFA
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700817B RID: 33147
			// (set) Token: 0x0600AFD4 RID: 45012 RVA: 0x000FDD0D File Offset: 0x000FBF0D
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700817C RID: 33148
			// (set) Token: 0x0600AFD5 RID: 45013 RVA: 0x000FDD2B File Offset: 0x000FBF2B
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700817D RID: 33149
			// (set) Token: 0x0600AFD6 RID: 45014 RVA: 0x000FDD3E File Offset: 0x000FBF3E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700817E RID: 33150
			// (set) Token: 0x0600AFD7 RID: 45015 RVA: 0x000FDD5C File Offset: 0x000FBF5C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700817F RID: 33151
			// (set) Token: 0x0600AFD8 RID: 45016 RVA: 0x000FDD6F File Offset: 0x000FBF6F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008180 RID: 33152
			// (set) Token: 0x0600AFD9 RID: 45017 RVA: 0x000FDD87 File Offset: 0x000FBF87
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008181 RID: 33153
			// (set) Token: 0x0600AFDA RID: 45018 RVA: 0x000FDD9F File Offset: 0x000FBF9F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008182 RID: 33154
			// (set) Token: 0x0600AFDB RID: 45019 RVA: 0x000FDDB7 File Offset: 0x000FBFB7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008183 RID: 33155
			// (set) Token: 0x0600AFDC RID: 45020 RVA: 0x000FDDCF File Offset: 0x000FBFCF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D06 RID: 3334
		public class FederatedUserParameters : ParametersBase
		{
			// Token: 0x17008184 RID: 33156
			// (set) Token: 0x0600AFDE RID: 45022 RVA: 0x000FDDEF File Offset: 0x000FBFEF
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17008185 RID: 33157
			// (set) Token: 0x0600AFDF RID: 45023 RVA: 0x000FDE02 File Offset: 0x000FC002
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17008186 RID: 33158
			// (set) Token: 0x0600AFE0 RID: 45024 RVA: 0x000FDE15 File Offset: 0x000FC015
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17008187 RID: 33159
			// (set) Token: 0x0600AFE1 RID: 45025 RVA: 0x000FDE28 File Offset: 0x000FC028
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x17008188 RID: 33160
			// (set) Token: 0x0600AFE2 RID: 45026 RVA: 0x000FDE40 File Offset: 0x000FC040
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17008189 RID: 33161
			// (set) Token: 0x0600AFE3 RID: 45027 RVA: 0x000FDE53 File Offset: 0x000FC053
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700818A RID: 33162
			// (set) Token: 0x0600AFE4 RID: 45028 RVA: 0x000FDE6B File Offset: 0x000FC06B
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700818B RID: 33163
			// (set) Token: 0x0600AFE5 RID: 45029 RVA: 0x000FDE7E File Offset: 0x000FC07E
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700818C RID: 33164
			// (set) Token: 0x0600AFE6 RID: 45030 RVA: 0x000FDE91 File Offset: 0x000FC091
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700818D RID: 33165
			// (set) Token: 0x0600AFE7 RID: 45031 RVA: 0x000FDEA4 File Offset: 0x000FC0A4
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700818E RID: 33166
			// (set) Token: 0x0600AFE8 RID: 45032 RVA: 0x000FDEB7 File Offset: 0x000FC0B7
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700818F RID: 33167
			// (set) Token: 0x0600AFE9 RID: 45033 RVA: 0x000FDECA File Offset: 0x000FC0CA
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008190 RID: 33168
			// (set) Token: 0x0600AFEA RID: 45034 RVA: 0x000FDEDD File Offset: 0x000FC0DD
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008191 RID: 33169
			// (set) Token: 0x0600AFEB RID: 45035 RVA: 0x000FDEF5 File Offset: 0x000FC0F5
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008192 RID: 33170
			// (set) Token: 0x0600AFEC RID: 45036 RVA: 0x000FDF08 File Offset: 0x000FC108
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008193 RID: 33171
			// (set) Token: 0x0600AFED RID: 45037 RVA: 0x000FDF20 File Offset: 0x000FC120
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008194 RID: 33172
			// (set) Token: 0x0600AFEE RID: 45038 RVA: 0x000FDF33 File Offset: 0x000FC133
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008195 RID: 33173
			// (set) Token: 0x0600AFEF RID: 45039 RVA: 0x000FDF4B File Offset: 0x000FC14B
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008196 RID: 33174
			// (set) Token: 0x0600AFF0 RID: 45040 RVA: 0x000FDF5E File Offset: 0x000FC15E
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008197 RID: 33175
			// (set) Token: 0x0600AFF1 RID: 45041 RVA: 0x000FDF7C File Offset: 0x000FC17C
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008198 RID: 33176
			// (set) Token: 0x0600AFF2 RID: 45042 RVA: 0x000FDF8F File Offset: 0x000FC18F
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008199 RID: 33177
			// (set) Token: 0x0600AFF3 RID: 45043 RVA: 0x000FDFA7 File Offset: 0x000FC1A7
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700819A RID: 33178
			// (set) Token: 0x0600AFF4 RID: 45044 RVA: 0x000FDFBF File Offset: 0x000FC1BF
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700819B RID: 33179
			// (set) Token: 0x0600AFF5 RID: 45045 RVA: 0x000FDFD7 File Offset: 0x000FC1D7
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700819C RID: 33180
			// (set) Token: 0x0600AFF6 RID: 45046 RVA: 0x000FDFEA File Offset: 0x000FC1EA
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700819D RID: 33181
			// (set) Token: 0x0600AFF7 RID: 45047 RVA: 0x000FE002 File Offset: 0x000FC202
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700819E RID: 33182
			// (set) Token: 0x0600AFF8 RID: 45048 RVA: 0x000FE015 File Offset: 0x000FC215
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700819F RID: 33183
			// (set) Token: 0x0600AFF9 RID: 45049 RVA: 0x000FE028 File Offset: 0x000FC228
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170081A0 RID: 33184
			// (set) Token: 0x0600AFFA RID: 45050 RVA: 0x000FE046 File Offset: 0x000FC246
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170081A1 RID: 33185
			// (set) Token: 0x0600AFFB RID: 45051 RVA: 0x000FE059 File Offset: 0x000FC259
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170081A2 RID: 33186
			// (set) Token: 0x0600AFFC RID: 45052 RVA: 0x000FE077 File Offset: 0x000FC277
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170081A3 RID: 33187
			// (set) Token: 0x0600AFFD RID: 45053 RVA: 0x000FE08A File Offset: 0x000FC28A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170081A4 RID: 33188
			// (set) Token: 0x0600AFFE RID: 45054 RVA: 0x000FE0A2 File Offset: 0x000FC2A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170081A5 RID: 33189
			// (set) Token: 0x0600AFFF RID: 45055 RVA: 0x000FE0BA File Offset: 0x000FC2BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170081A6 RID: 33190
			// (set) Token: 0x0600B000 RID: 45056 RVA: 0x000FE0D2 File Offset: 0x000FC2D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170081A7 RID: 33191
			// (set) Token: 0x0600B001 RID: 45057 RVA: 0x000FE0EA File Offset: 0x000FC2EA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D07 RID: 3335
		public class WindowsLiveCustomDomainsParameters : ParametersBase
		{
			// Token: 0x170081A8 RID: 33192
			// (set) Token: 0x0600B003 RID: 45059 RVA: 0x000FE10A File Offset: 0x000FC30A
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x170081A9 RID: 33193
			// (set) Token: 0x0600B004 RID: 45060 RVA: 0x000FE11D File Offset: 0x000FC31D
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170081AA RID: 33194
			// (set) Token: 0x0600B005 RID: 45061 RVA: 0x000FE130 File Offset: 0x000FC330
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x170081AB RID: 33195
			// (set) Token: 0x0600B006 RID: 45062 RVA: 0x000FE143 File Offset: 0x000FC343
			public virtual SwitchParameter UseExistingLiveId
			{
				set
				{
					base.PowerSharpParameters["UseExistingLiveId"] = value;
				}
			}

			// Token: 0x170081AC RID: 33196
			// (set) Token: 0x0600B007 RID: 45063 RVA: 0x000FE15B File Offset: 0x000FC35B
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x170081AD RID: 33197
			// (set) Token: 0x0600B008 RID: 45064 RVA: 0x000FE16E File Offset: 0x000FC36E
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x170081AE RID: 33198
			// (set) Token: 0x0600B009 RID: 45065 RVA: 0x000FE186 File Offset: 0x000FC386
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170081AF RID: 33199
			// (set) Token: 0x0600B00A RID: 45066 RVA: 0x000FE19E File Offset: 0x000FC39E
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170081B0 RID: 33200
			// (set) Token: 0x0600B00B RID: 45067 RVA: 0x000FE1B1 File Offset: 0x000FC3B1
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170081B1 RID: 33201
			// (set) Token: 0x0600B00C RID: 45068 RVA: 0x000FE1C4 File Offset: 0x000FC3C4
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170081B2 RID: 33202
			// (set) Token: 0x0600B00D RID: 45069 RVA: 0x000FE1D7 File Offset: 0x000FC3D7
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170081B3 RID: 33203
			// (set) Token: 0x0600B00E RID: 45070 RVA: 0x000FE1EA File Offset: 0x000FC3EA
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170081B4 RID: 33204
			// (set) Token: 0x0600B00F RID: 45071 RVA: 0x000FE1FD File Offset: 0x000FC3FD
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170081B5 RID: 33205
			// (set) Token: 0x0600B010 RID: 45072 RVA: 0x000FE210 File Offset: 0x000FC410
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170081B6 RID: 33206
			// (set) Token: 0x0600B011 RID: 45073 RVA: 0x000FE228 File Offset: 0x000FC428
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170081B7 RID: 33207
			// (set) Token: 0x0600B012 RID: 45074 RVA: 0x000FE23B File Offset: 0x000FC43B
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170081B8 RID: 33208
			// (set) Token: 0x0600B013 RID: 45075 RVA: 0x000FE253 File Offset: 0x000FC453
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170081B9 RID: 33209
			// (set) Token: 0x0600B014 RID: 45076 RVA: 0x000FE266 File Offset: 0x000FC466
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170081BA RID: 33210
			// (set) Token: 0x0600B015 RID: 45077 RVA: 0x000FE27E File Offset: 0x000FC47E
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170081BB RID: 33211
			// (set) Token: 0x0600B016 RID: 45078 RVA: 0x000FE291 File Offset: 0x000FC491
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170081BC RID: 33212
			// (set) Token: 0x0600B017 RID: 45079 RVA: 0x000FE2AF File Offset: 0x000FC4AF
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170081BD RID: 33213
			// (set) Token: 0x0600B018 RID: 45080 RVA: 0x000FE2C2 File Offset: 0x000FC4C2
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170081BE RID: 33214
			// (set) Token: 0x0600B019 RID: 45081 RVA: 0x000FE2DA File Offset: 0x000FC4DA
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170081BF RID: 33215
			// (set) Token: 0x0600B01A RID: 45082 RVA: 0x000FE2F2 File Offset: 0x000FC4F2
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170081C0 RID: 33216
			// (set) Token: 0x0600B01B RID: 45083 RVA: 0x000FE30A File Offset: 0x000FC50A
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170081C1 RID: 33217
			// (set) Token: 0x0600B01C RID: 45084 RVA: 0x000FE31D File Offset: 0x000FC51D
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170081C2 RID: 33218
			// (set) Token: 0x0600B01D RID: 45085 RVA: 0x000FE335 File Offset: 0x000FC535
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170081C3 RID: 33219
			// (set) Token: 0x0600B01E RID: 45086 RVA: 0x000FE348 File Offset: 0x000FC548
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170081C4 RID: 33220
			// (set) Token: 0x0600B01F RID: 45087 RVA: 0x000FE35B File Offset: 0x000FC55B
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170081C5 RID: 33221
			// (set) Token: 0x0600B020 RID: 45088 RVA: 0x000FE379 File Offset: 0x000FC579
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170081C6 RID: 33222
			// (set) Token: 0x0600B021 RID: 45089 RVA: 0x000FE38C File Offset: 0x000FC58C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170081C7 RID: 33223
			// (set) Token: 0x0600B022 RID: 45090 RVA: 0x000FE3AA File Offset: 0x000FC5AA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170081C8 RID: 33224
			// (set) Token: 0x0600B023 RID: 45091 RVA: 0x000FE3BD File Offset: 0x000FC5BD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170081C9 RID: 33225
			// (set) Token: 0x0600B024 RID: 45092 RVA: 0x000FE3D5 File Offset: 0x000FC5D5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170081CA RID: 33226
			// (set) Token: 0x0600B025 RID: 45093 RVA: 0x000FE3ED File Offset: 0x000FC5ED
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170081CB RID: 33227
			// (set) Token: 0x0600B026 RID: 45094 RVA: 0x000FE405 File Offset: 0x000FC605
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170081CC RID: 33228
			// (set) Token: 0x0600B027 RID: 45095 RVA: 0x000FE41D File Offset: 0x000FC61D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D08 RID: 3336
		public class ImportLiveIdParameters : ParametersBase
		{
			// Token: 0x170081CD RID: 33229
			// (set) Token: 0x0600B029 RID: 45097 RVA: 0x000FE43D File Offset: 0x000FC63D
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x170081CE RID: 33230
			// (set) Token: 0x0600B02A RID: 45098 RVA: 0x000FE450 File Offset: 0x000FC650
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170081CF RID: 33231
			// (set) Token: 0x0600B02B RID: 45099 RVA: 0x000FE463 File Offset: 0x000FC663
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x170081D0 RID: 33232
			// (set) Token: 0x0600B02C RID: 45100 RVA: 0x000FE476 File Offset: 0x000FC676
			public virtual SwitchParameter ImportLiveId
			{
				set
				{
					base.PowerSharpParameters["ImportLiveId"] = value;
				}
			}

			// Token: 0x170081D1 RID: 33233
			// (set) Token: 0x0600B02D RID: 45101 RVA: 0x000FE48E File Offset: 0x000FC68E
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170081D2 RID: 33234
			// (set) Token: 0x0600B02E RID: 45102 RVA: 0x000FE4A6 File Offset: 0x000FC6A6
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170081D3 RID: 33235
			// (set) Token: 0x0600B02F RID: 45103 RVA: 0x000FE4B9 File Offset: 0x000FC6B9
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170081D4 RID: 33236
			// (set) Token: 0x0600B030 RID: 45104 RVA: 0x000FE4CC File Offset: 0x000FC6CC
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170081D5 RID: 33237
			// (set) Token: 0x0600B031 RID: 45105 RVA: 0x000FE4DF File Offset: 0x000FC6DF
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170081D6 RID: 33238
			// (set) Token: 0x0600B032 RID: 45106 RVA: 0x000FE4F2 File Offset: 0x000FC6F2
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170081D7 RID: 33239
			// (set) Token: 0x0600B033 RID: 45107 RVA: 0x000FE505 File Offset: 0x000FC705
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170081D8 RID: 33240
			// (set) Token: 0x0600B034 RID: 45108 RVA: 0x000FE518 File Offset: 0x000FC718
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170081D9 RID: 33241
			// (set) Token: 0x0600B035 RID: 45109 RVA: 0x000FE530 File Offset: 0x000FC730
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170081DA RID: 33242
			// (set) Token: 0x0600B036 RID: 45110 RVA: 0x000FE543 File Offset: 0x000FC743
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170081DB RID: 33243
			// (set) Token: 0x0600B037 RID: 45111 RVA: 0x000FE55B File Offset: 0x000FC75B
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170081DC RID: 33244
			// (set) Token: 0x0600B038 RID: 45112 RVA: 0x000FE56E File Offset: 0x000FC76E
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170081DD RID: 33245
			// (set) Token: 0x0600B039 RID: 45113 RVA: 0x000FE586 File Offset: 0x000FC786
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170081DE RID: 33246
			// (set) Token: 0x0600B03A RID: 45114 RVA: 0x000FE599 File Offset: 0x000FC799
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170081DF RID: 33247
			// (set) Token: 0x0600B03B RID: 45115 RVA: 0x000FE5B7 File Offset: 0x000FC7B7
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170081E0 RID: 33248
			// (set) Token: 0x0600B03C RID: 45116 RVA: 0x000FE5CA File Offset: 0x000FC7CA
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170081E1 RID: 33249
			// (set) Token: 0x0600B03D RID: 45117 RVA: 0x000FE5E2 File Offset: 0x000FC7E2
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170081E2 RID: 33250
			// (set) Token: 0x0600B03E RID: 45118 RVA: 0x000FE5FA File Offset: 0x000FC7FA
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170081E3 RID: 33251
			// (set) Token: 0x0600B03F RID: 45119 RVA: 0x000FE612 File Offset: 0x000FC812
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170081E4 RID: 33252
			// (set) Token: 0x0600B040 RID: 45120 RVA: 0x000FE625 File Offset: 0x000FC825
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170081E5 RID: 33253
			// (set) Token: 0x0600B041 RID: 45121 RVA: 0x000FE63D File Offset: 0x000FC83D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170081E6 RID: 33254
			// (set) Token: 0x0600B042 RID: 45122 RVA: 0x000FE650 File Offset: 0x000FC850
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170081E7 RID: 33255
			// (set) Token: 0x0600B043 RID: 45123 RVA: 0x000FE663 File Offset: 0x000FC863
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170081E8 RID: 33256
			// (set) Token: 0x0600B044 RID: 45124 RVA: 0x000FE681 File Offset: 0x000FC881
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170081E9 RID: 33257
			// (set) Token: 0x0600B045 RID: 45125 RVA: 0x000FE694 File Offset: 0x000FC894
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170081EA RID: 33258
			// (set) Token: 0x0600B046 RID: 45126 RVA: 0x000FE6B2 File Offset: 0x000FC8B2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170081EB RID: 33259
			// (set) Token: 0x0600B047 RID: 45127 RVA: 0x000FE6C5 File Offset: 0x000FC8C5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170081EC RID: 33260
			// (set) Token: 0x0600B048 RID: 45128 RVA: 0x000FE6DD File Offset: 0x000FC8DD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170081ED RID: 33261
			// (set) Token: 0x0600B049 RID: 45129 RVA: 0x000FE6F5 File Offset: 0x000FC8F5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170081EE RID: 33262
			// (set) Token: 0x0600B04A RID: 45130 RVA: 0x000FE70D File Offset: 0x000FC90D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170081EF RID: 33263
			// (set) Token: 0x0600B04B RID: 45131 RVA: 0x000FE725 File Offset: 0x000FC925
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D09 RID: 3337
		public class DisabledUserParameters : ParametersBase
		{
			// Token: 0x170081F0 RID: 33264
			// (set) Token: 0x0600B04D RID: 45133 RVA: 0x000FE745 File Offset: 0x000FC945
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x170081F1 RID: 33265
			// (set) Token: 0x0600B04E RID: 45134 RVA: 0x000FE758 File Offset: 0x000FC958
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x170081F2 RID: 33266
			// (set) Token: 0x0600B04F RID: 45135 RVA: 0x000FE770 File Offset: 0x000FC970
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x170081F3 RID: 33267
			// (set) Token: 0x0600B050 RID: 45136 RVA: 0x000FE788 File Offset: 0x000FC988
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x170081F4 RID: 33268
			// (set) Token: 0x0600B051 RID: 45137 RVA: 0x000FE7A0 File Offset: 0x000FC9A0
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x170081F5 RID: 33269
			// (set) Token: 0x0600B052 RID: 45138 RVA: 0x000FE7B8 File Offset: 0x000FC9B8
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170081F6 RID: 33270
			// (set) Token: 0x0600B053 RID: 45139 RVA: 0x000FE7CB File Offset: 0x000FC9CB
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170081F7 RID: 33271
			// (set) Token: 0x0600B054 RID: 45140 RVA: 0x000FE7E3 File Offset: 0x000FC9E3
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170081F8 RID: 33272
			// (set) Token: 0x0600B055 RID: 45141 RVA: 0x000FE7F6 File Offset: 0x000FC9F6
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170081F9 RID: 33273
			// (set) Token: 0x0600B056 RID: 45142 RVA: 0x000FE809 File Offset: 0x000FCA09
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170081FA RID: 33274
			// (set) Token: 0x0600B057 RID: 45143 RVA: 0x000FE81C File Offset: 0x000FCA1C
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170081FB RID: 33275
			// (set) Token: 0x0600B058 RID: 45144 RVA: 0x000FE82F File Offset: 0x000FCA2F
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170081FC RID: 33276
			// (set) Token: 0x0600B059 RID: 45145 RVA: 0x000FE842 File Offset: 0x000FCA42
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170081FD RID: 33277
			// (set) Token: 0x0600B05A RID: 45146 RVA: 0x000FE855 File Offset: 0x000FCA55
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170081FE RID: 33278
			// (set) Token: 0x0600B05B RID: 45147 RVA: 0x000FE86D File Offset: 0x000FCA6D
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170081FF RID: 33279
			// (set) Token: 0x0600B05C RID: 45148 RVA: 0x000FE880 File Offset: 0x000FCA80
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008200 RID: 33280
			// (set) Token: 0x0600B05D RID: 45149 RVA: 0x000FE898 File Offset: 0x000FCA98
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008201 RID: 33281
			// (set) Token: 0x0600B05E RID: 45150 RVA: 0x000FE8AB File Offset: 0x000FCAAB
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008202 RID: 33282
			// (set) Token: 0x0600B05F RID: 45151 RVA: 0x000FE8C3 File Offset: 0x000FCAC3
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008203 RID: 33283
			// (set) Token: 0x0600B060 RID: 45152 RVA: 0x000FE8D6 File Offset: 0x000FCAD6
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008204 RID: 33284
			// (set) Token: 0x0600B061 RID: 45153 RVA: 0x000FE8F4 File Offset: 0x000FCAF4
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008205 RID: 33285
			// (set) Token: 0x0600B062 RID: 45154 RVA: 0x000FE907 File Offset: 0x000FCB07
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008206 RID: 33286
			// (set) Token: 0x0600B063 RID: 45155 RVA: 0x000FE91F File Offset: 0x000FCB1F
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008207 RID: 33287
			// (set) Token: 0x0600B064 RID: 45156 RVA: 0x000FE937 File Offset: 0x000FCB37
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008208 RID: 33288
			// (set) Token: 0x0600B065 RID: 45157 RVA: 0x000FE94F File Offset: 0x000FCB4F
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008209 RID: 33289
			// (set) Token: 0x0600B066 RID: 45158 RVA: 0x000FE962 File Offset: 0x000FCB62
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700820A RID: 33290
			// (set) Token: 0x0600B067 RID: 45159 RVA: 0x000FE97A File Offset: 0x000FCB7A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700820B RID: 33291
			// (set) Token: 0x0600B068 RID: 45160 RVA: 0x000FE98D File Offset: 0x000FCB8D
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700820C RID: 33292
			// (set) Token: 0x0600B069 RID: 45161 RVA: 0x000FE9A0 File Offset: 0x000FCBA0
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700820D RID: 33293
			// (set) Token: 0x0600B06A RID: 45162 RVA: 0x000FE9BE File Offset: 0x000FCBBE
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700820E RID: 33294
			// (set) Token: 0x0600B06B RID: 45163 RVA: 0x000FE9D1 File Offset: 0x000FCBD1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700820F RID: 33295
			// (set) Token: 0x0600B06C RID: 45164 RVA: 0x000FE9EF File Offset: 0x000FCBEF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008210 RID: 33296
			// (set) Token: 0x0600B06D RID: 45165 RVA: 0x000FEA02 File Offset: 0x000FCC02
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008211 RID: 33297
			// (set) Token: 0x0600B06E RID: 45166 RVA: 0x000FEA1A File Offset: 0x000FCC1A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008212 RID: 33298
			// (set) Token: 0x0600B06F RID: 45167 RVA: 0x000FEA32 File Offset: 0x000FCC32
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008213 RID: 33299
			// (set) Token: 0x0600B070 RID: 45168 RVA: 0x000FEA4A File Offset: 0x000FCC4A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008214 RID: 33300
			// (set) Token: 0x0600B071 RID: 45169 RVA: 0x000FEA62 File Offset: 0x000FCC62
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D0A RID: 3338
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008215 RID: 33301
			// (set) Token: 0x0600B073 RID: 45171 RVA: 0x000FEA82 File Offset: 0x000FCC82
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008216 RID: 33302
			// (set) Token: 0x0600B074 RID: 45172 RVA: 0x000FEA9A File Offset: 0x000FCC9A
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008217 RID: 33303
			// (set) Token: 0x0600B075 RID: 45173 RVA: 0x000FEAAD File Offset: 0x000FCCAD
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008218 RID: 33304
			// (set) Token: 0x0600B076 RID: 45174 RVA: 0x000FEAC0 File Offset: 0x000FCCC0
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008219 RID: 33305
			// (set) Token: 0x0600B077 RID: 45175 RVA: 0x000FEAD3 File Offset: 0x000FCCD3
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700821A RID: 33306
			// (set) Token: 0x0600B078 RID: 45176 RVA: 0x000FEAE6 File Offset: 0x000FCCE6
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700821B RID: 33307
			// (set) Token: 0x0600B079 RID: 45177 RVA: 0x000FEAF9 File Offset: 0x000FCCF9
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700821C RID: 33308
			// (set) Token: 0x0600B07A RID: 45178 RVA: 0x000FEB0C File Offset: 0x000FCD0C
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700821D RID: 33309
			// (set) Token: 0x0600B07B RID: 45179 RVA: 0x000FEB24 File Offset: 0x000FCD24
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700821E RID: 33310
			// (set) Token: 0x0600B07C RID: 45180 RVA: 0x000FEB37 File Offset: 0x000FCD37
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700821F RID: 33311
			// (set) Token: 0x0600B07D RID: 45181 RVA: 0x000FEB4F File Offset: 0x000FCD4F
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008220 RID: 33312
			// (set) Token: 0x0600B07E RID: 45182 RVA: 0x000FEB62 File Offset: 0x000FCD62
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008221 RID: 33313
			// (set) Token: 0x0600B07F RID: 45183 RVA: 0x000FEB7A File Offset: 0x000FCD7A
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008222 RID: 33314
			// (set) Token: 0x0600B080 RID: 45184 RVA: 0x000FEB8D File Offset: 0x000FCD8D
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008223 RID: 33315
			// (set) Token: 0x0600B081 RID: 45185 RVA: 0x000FEBAB File Offset: 0x000FCDAB
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008224 RID: 33316
			// (set) Token: 0x0600B082 RID: 45186 RVA: 0x000FEBBE File Offset: 0x000FCDBE
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008225 RID: 33317
			// (set) Token: 0x0600B083 RID: 45187 RVA: 0x000FEBD6 File Offset: 0x000FCDD6
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008226 RID: 33318
			// (set) Token: 0x0600B084 RID: 45188 RVA: 0x000FEBEE File Offset: 0x000FCDEE
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008227 RID: 33319
			// (set) Token: 0x0600B085 RID: 45189 RVA: 0x000FEC06 File Offset: 0x000FCE06
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008228 RID: 33320
			// (set) Token: 0x0600B086 RID: 45190 RVA: 0x000FEC19 File Offset: 0x000FCE19
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008229 RID: 33321
			// (set) Token: 0x0600B087 RID: 45191 RVA: 0x000FEC31 File Offset: 0x000FCE31
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700822A RID: 33322
			// (set) Token: 0x0600B088 RID: 45192 RVA: 0x000FEC44 File Offset: 0x000FCE44
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700822B RID: 33323
			// (set) Token: 0x0600B089 RID: 45193 RVA: 0x000FEC57 File Offset: 0x000FCE57
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700822C RID: 33324
			// (set) Token: 0x0600B08A RID: 45194 RVA: 0x000FEC75 File Offset: 0x000FCE75
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700822D RID: 33325
			// (set) Token: 0x0600B08B RID: 45195 RVA: 0x000FEC88 File Offset: 0x000FCE88
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700822E RID: 33326
			// (set) Token: 0x0600B08C RID: 45196 RVA: 0x000FECA6 File Offset: 0x000FCEA6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700822F RID: 33327
			// (set) Token: 0x0600B08D RID: 45197 RVA: 0x000FECB9 File Offset: 0x000FCEB9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008230 RID: 33328
			// (set) Token: 0x0600B08E RID: 45198 RVA: 0x000FECD1 File Offset: 0x000FCED1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008231 RID: 33329
			// (set) Token: 0x0600B08F RID: 45199 RVA: 0x000FECE9 File Offset: 0x000FCEE9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008232 RID: 33330
			// (set) Token: 0x0600B090 RID: 45200 RVA: 0x000FED01 File Offset: 0x000FCF01
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008233 RID: 33331
			// (set) Token: 0x0600B091 RID: 45201 RVA: 0x000FED19 File Offset: 0x000FCF19
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D0B RID: 3339
		public class EnableRoomMailboxAccountParameters : ParametersBase
		{
			// Token: 0x17008234 RID: 33332
			// (set) Token: 0x0600B093 RID: 45203 RVA: 0x000FED39 File Offset: 0x000FCF39
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17008235 RID: 33333
			// (set) Token: 0x0600B094 RID: 45204 RVA: 0x000FED4C File Offset: 0x000FCF4C
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008236 RID: 33334
			// (set) Token: 0x0600B095 RID: 45205 RVA: 0x000FED64 File Offset: 0x000FCF64
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008237 RID: 33335
			// (set) Token: 0x0600B096 RID: 45206 RVA: 0x000FED77 File Offset: 0x000FCF77
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008238 RID: 33336
			// (set) Token: 0x0600B097 RID: 45207 RVA: 0x000FED8A File Offset: 0x000FCF8A
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008239 RID: 33337
			// (set) Token: 0x0600B098 RID: 45208 RVA: 0x000FED9D File Offset: 0x000FCF9D
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700823A RID: 33338
			// (set) Token: 0x0600B099 RID: 45209 RVA: 0x000FEDB0 File Offset: 0x000FCFB0
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700823B RID: 33339
			// (set) Token: 0x0600B09A RID: 45210 RVA: 0x000FEDC3 File Offset: 0x000FCFC3
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700823C RID: 33340
			// (set) Token: 0x0600B09B RID: 45211 RVA: 0x000FEDD6 File Offset: 0x000FCFD6
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700823D RID: 33341
			// (set) Token: 0x0600B09C RID: 45212 RVA: 0x000FEDEE File Offset: 0x000FCFEE
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700823E RID: 33342
			// (set) Token: 0x0600B09D RID: 45213 RVA: 0x000FEE01 File Offset: 0x000FD001
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700823F RID: 33343
			// (set) Token: 0x0600B09E RID: 45214 RVA: 0x000FEE19 File Offset: 0x000FD019
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008240 RID: 33344
			// (set) Token: 0x0600B09F RID: 45215 RVA: 0x000FEE2C File Offset: 0x000FD02C
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008241 RID: 33345
			// (set) Token: 0x0600B0A0 RID: 45216 RVA: 0x000FEE44 File Offset: 0x000FD044
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008242 RID: 33346
			// (set) Token: 0x0600B0A1 RID: 45217 RVA: 0x000FEE57 File Offset: 0x000FD057
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008243 RID: 33347
			// (set) Token: 0x0600B0A2 RID: 45218 RVA: 0x000FEE75 File Offset: 0x000FD075
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008244 RID: 33348
			// (set) Token: 0x0600B0A3 RID: 45219 RVA: 0x000FEE88 File Offset: 0x000FD088
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008245 RID: 33349
			// (set) Token: 0x0600B0A4 RID: 45220 RVA: 0x000FEEA0 File Offset: 0x000FD0A0
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008246 RID: 33350
			// (set) Token: 0x0600B0A5 RID: 45221 RVA: 0x000FEEB8 File Offset: 0x000FD0B8
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008247 RID: 33351
			// (set) Token: 0x0600B0A6 RID: 45222 RVA: 0x000FEED0 File Offset: 0x000FD0D0
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008248 RID: 33352
			// (set) Token: 0x0600B0A7 RID: 45223 RVA: 0x000FEEE3 File Offset: 0x000FD0E3
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008249 RID: 33353
			// (set) Token: 0x0600B0A8 RID: 45224 RVA: 0x000FEEFB File Offset: 0x000FD0FB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700824A RID: 33354
			// (set) Token: 0x0600B0A9 RID: 45225 RVA: 0x000FEF0E File Offset: 0x000FD10E
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700824B RID: 33355
			// (set) Token: 0x0600B0AA RID: 45226 RVA: 0x000FEF21 File Offset: 0x000FD121
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700824C RID: 33356
			// (set) Token: 0x0600B0AB RID: 45227 RVA: 0x000FEF3F File Offset: 0x000FD13F
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700824D RID: 33357
			// (set) Token: 0x0600B0AC RID: 45228 RVA: 0x000FEF52 File Offset: 0x000FD152
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700824E RID: 33358
			// (set) Token: 0x0600B0AD RID: 45229 RVA: 0x000FEF70 File Offset: 0x000FD170
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700824F RID: 33359
			// (set) Token: 0x0600B0AE RID: 45230 RVA: 0x000FEF83 File Offset: 0x000FD183
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008250 RID: 33360
			// (set) Token: 0x0600B0AF RID: 45231 RVA: 0x000FEF9B File Offset: 0x000FD19B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008251 RID: 33361
			// (set) Token: 0x0600B0B0 RID: 45232 RVA: 0x000FEFB3 File Offset: 0x000FD1B3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008252 RID: 33362
			// (set) Token: 0x0600B0B1 RID: 45233 RVA: 0x000FEFCB File Offset: 0x000FD1CB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008253 RID: 33363
			// (set) Token: 0x0600B0B2 RID: 45234 RVA: 0x000FEFE3 File Offset: 0x000FD1E3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D0C RID: 3340
		public class MicrosoftOnlineServicesFederatedUserParameters : ParametersBase
		{
			// Token: 0x17008254 RID: 33364
			// (set) Token: 0x0600B0B4 RID: 45236 RVA: 0x000FF003 File Offset: 0x000FD203
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17008255 RID: 33365
			// (set) Token: 0x0600B0B5 RID: 45237 RVA: 0x000FF016 File Offset: 0x000FD216
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17008256 RID: 33366
			// (set) Token: 0x0600B0B6 RID: 45238 RVA: 0x000FF029 File Offset: 0x000FD229
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17008257 RID: 33367
			// (set) Token: 0x0600B0B7 RID: 45239 RVA: 0x000FF03C File Offset: 0x000FD23C
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008258 RID: 33368
			// (set) Token: 0x0600B0B8 RID: 45240 RVA: 0x000FF054 File Offset: 0x000FD254
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008259 RID: 33369
			// (set) Token: 0x0600B0B9 RID: 45241 RVA: 0x000FF067 File Offset: 0x000FD267
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700825A RID: 33370
			// (set) Token: 0x0600B0BA RID: 45242 RVA: 0x000FF07A File Offset: 0x000FD27A
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700825B RID: 33371
			// (set) Token: 0x0600B0BB RID: 45243 RVA: 0x000FF08D File Offset: 0x000FD28D
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700825C RID: 33372
			// (set) Token: 0x0600B0BC RID: 45244 RVA: 0x000FF0A0 File Offset: 0x000FD2A0
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700825D RID: 33373
			// (set) Token: 0x0600B0BD RID: 45245 RVA: 0x000FF0B3 File Offset: 0x000FD2B3
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700825E RID: 33374
			// (set) Token: 0x0600B0BE RID: 45246 RVA: 0x000FF0C6 File Offset: 0x000FD2C6
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700825F RID: 33375
			// (set) Token: 0x0600B0BF RID: 45247 RVA: 0x000FF0DE File Offset: 0x000FD2DE
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008260 RID: 33376
			// (set) Token: 0x0600B0C0 RID: 45248 RVA: 0x000FF0F1 File Offset: 0x000FD2F1
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008261 RID: 33377
			// (set) Token: 0x0600B0C1 RID: 45249 RVA: 0x000FF109 File Offset: 0x000FD309
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008262 RID: 33378
			// (set) Token: 0x0600B0C2 RID: 45250 RVA: 0x000FF11C File Offset: 0x000FD31C
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008263 RID: 33379
			// (set) Token: 0x0600B0C3 RID: 45251 RVA: 0x000FF134 File Offset: 0x000FD334
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17008264 RID: 33380
			// (set) Token: 0x0600B0C4 RID: 45252 RVA: 0x000FF147 File Offset: 0x000FD347
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008265 RID: 33381
			// (set) Token: 0x0600B0C5 RID: 45253 RVA: 0x000FF165 File Offset: 0x000FD365
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008266 RID: 33382
			// (set) Token: 0x0600B0C6 RID: 45254 RVA: 0x000FF178 File Offset: 0x000FD378
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008267 RID: 33383
			// (set) Token: 0x0600B0C7 RID: 45255 RVA: 0x000FF190 File Offset: 0x000FD390
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008268 RID: 33384
			// (set) Token: 0x0600B0C8 RID: 45256 RVA: 0x000FF1A8 File Offset: 0x000FD3A8
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008269 RID: 33385
			// (set) Token: 0x0600B0C9 RID: 45257 RVA: 0x000FF1C0 File Offset: 0x000FD3C0
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700826A RID: 33386
			// (set) Token: 0x0600B0CA RID: 45258 RVA: 0x000FF1D3 File Offset: 0x000FD3D3
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700826B RID: 33387
			// (set) Token: 0x0600B0CB RID: 45259 RVA: 0x000FF1EB File Offset: 0x000FD3EB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700826C RID: 33388
			// (set) Token: 0x0600B0CC RID: 45260 RVA: 0x000FF1FE File Offset: 0x000FD3FE
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700826D RID: 33389
			// (set) Token: 0x0600B0CD RID: 45261 RVA: 0x000FF211 File Offset: 0x000FD411
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700826E RID: 33390
			// (set) Token: 0x0600B0CE RID: 45262 RVA: 0x000FF22F File Offset: 0x000FD42F
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700826F RID: 33391
			// (set) Token: 0x0600B0CF RID: 45263 RVA: 0x000FF242 File Offset: 0x000FD442
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008270 RID: 33392
			// (set) Token: 0x0600B0D0 RID: 45264 RVA: 0x000FF260 File Offset: 0x000FD460
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008271 RID: 33393
			// (set) Token: 0x0600B0D1 RID: 45265 RVA: 0x000FF273 File Offset: 0x000FD473
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008272 RID: 33394
			// (set) Token: 0x0600B0D2 RID: 45266 RVA: 0x000FF28B File Offset: 0x000FD48B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008273 RID: 33395
			// (set) Token: 0x0600B0D3 RID: 45267 RVA: 0x000FF2A3 File Offset: 0x000FD4A3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008274 RID: 33396
			// (set) Token: 0x0600B0D4 RID: 45268 RVA: 0x000FF2BB File Offset: 0x000FD4BB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008275 RID: 33397
			// (set) Token: 0x0600B0D5 RID: 45269 RVA: 0x000FF2D3 File Offset: 0x000FD4D3
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
