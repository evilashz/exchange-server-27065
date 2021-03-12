using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000476 RID: 1142
	public class GetMailboxAutoReplyConfigurationCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x060040E2 RID: 16610 RVA: 0x0006BF1C File Offset: 0x0006A11C
		private GetMailboxAutoReplyConfigurationCommand() : base("Get-MailboxAutoReplyConfiguration")
		{
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x0006BF29 File Offset: 0x0006A129
		public GetMailboxAutoReplyConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x0006BF38 File Offset: 0x0006A138
		public virtual GetMailboxAutoReplyConfigurationCommand SetParameters(GetMailboxAutoReplyConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x0006BF42 File Offset: 0x0006A142
		public virtual GetMailboxAutoReplyConfigurationCommand SetParameters(GetMailboxAutoReplyConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000477 RID: 1143
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170023AF RID: 9135
			// (set) Token: 0x060040E6 RID: 16614 RVA: 0x0006BF4C File Offset: 0x0006A14C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170023B0 RID: 9136
			// (set) Token: 0x060040E7 RID: 16615 RVA: 0x0006BF6A File Offset: 0x0006A16A
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170023B1 RID: 9137
			// (set) Token: 0x060040E8 RID: 16616 RVA: 0x0006BF7D File Offset: 0x0006A17D
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170023B2 RID: 9138
			// (set) Token: 0x060040E9 RID: 16617 RVA: 0x0006BF95 File Offset: 0x0006A195
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170023B3 RID: 9139
			// (set) Token: 0x060040EA RID: 16618 RVA: 0x0006BFAD File Offset: 0x0006A1AD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170023B4 RID: 9140
			// (set) Token: 0x060040EB RID: 16619 RVA: 0x0006BFC0 File Offset: 0x0006A1C0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170023B5 RID: 9141
			// (set) Token: 0x060040EC RID: 16620 RVA: 0x0006BFD8 File Offset: 0x0006A1D8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170023B6 RID: 9142
			// (set) Token: 0x060040ED RID: 16621 RVA: 0x0006BFF0 File Offset: 0x0006A1F0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170023B7 RID: 9143
			// (set) Token: 0x060040EE RID: 16622 RVA: 0x0006C008 File Offset: 0x0006A208
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000478 RID: 1144
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170023B8 RID: 9144
			// (set) Token: 0x060040F0 RID: 16624 RVA: 0x0006C028 File Offset: 0x0006A228
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170023B9 RID: 9145
			// (set) Token: 0x060040F1 RID: 16625 RVA: 0x0006C03B File Offset: 0x0006A23B
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170023BA RID: 9146
			// (set) Token: 0x060040F2 RID: 16626 RVA: 0x0006C053 File Offset: 0x0006A253
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170023BB RID: 9147
			// (set) Token: 0x060040F3 RID: 16627 RVA: 0x0006C06B File Offset: 0x0006A26B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170023BC RID: 9148
			// (set) Token: 0x060040F4 RID: 16628 RVA: 0x0006C07E File Offset: 0x0006A27E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170023BD RID: 9149
			// (set) Token: 0x060040F5 RID: 16629 RVA: 0x0006C096 File Offset: 0x0006A296
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170023BE RID: 9150
			// (set) Token: 0x060040F6 RID: 16630 RVA: 0x0006C0AE File Offset: 0x0006A2AE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170023BF RID: 9151
			// (set) Token: 0x060040F7 RID: 16631 RVA: 0x0006C0C6 File Offset: 0x0006A2C6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
