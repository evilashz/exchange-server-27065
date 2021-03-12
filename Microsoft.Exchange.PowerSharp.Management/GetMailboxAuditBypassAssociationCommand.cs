using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000193 RID: 403
	public class GetMailboxAuditBypassAssociationCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x06002373 RID: 9075 RVA: 0x000457CC File Offset: 0x000439CC
		private GetMailboxAuditBypassAssociationCommand() : base("Get-MailboxAuditBypassAssociation")
		{
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000457D9 File Offset: 0x000439D9
		public GetMailboxAuditBypassAssociationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000457E8 File Offset: 0x000439E8
		public virtual GetMailboxAuditBypassAssociationCommand SetParameters(GetMailboxAuditBypassAssociationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000457F2 File Offset: 0x000439F2
		public virtual GetMailboxAuditBypassAssociationCommand SetParameters(GetMailboxAuditBypassAssociationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000194 RID: 404
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000C06 RID: 3078
			// (set) Token: 0x06002377 RID: 9079 RVA: 0x000457FC File Offset: 0x000439FC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000C07 RID: 3079
			// (set) Token: 0x06002378 RID: 9080 RVA: 0x0004581A File Offset: 0x00043A1A
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17000C08 RID: 3080
			// (set) Token: 0x06002379 RID: 9081 RVA: 0x00045832 File Offset: 0x00043A32
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C09 RID: 3081
			// (set) Token: 0x0600237A RID: 9082 RVA: 0x00045845 File Offset: 0x00043A45
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000C0A RID: 3082
			// (set) Token: 0x0600237B RID: 9083 RVA: 0x0004585D File Offset: 0x00043A5D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000C0B RID: 3083
			// (set) Token: 0x0600237C RID: 9084 RVA: 0x00045875 File Offset: 0x00043A75
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000C0C RID: 3084
			// (set) Token: 0x0600237D RID: 9085 RVA: 0x0004588D File Offset: 0x00043A8D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000195 RID: 405
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000C0D RID: 3085
			// (set) Token: 0x0600237F RID: 9087 RVA: 0x000458AD File Offset: 0x00043AAD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxAuditBypassAssociationIdParameter(value) : null);
				}
			}

			// Token: 0x17000C0E RID: 3086
			// (set) Token: 0x06002380 RID: 9088 RVA: 0x000458CB File Offset: 0x00043ACB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000C0F RID: 3087
			// (set) Token: 0x06002381 RID: 9089 RVA: 0x000458E9 File Offset: 0x00043AE9
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17000C10 RID: 3088
			// (set) Token: 0x06002382 RID: 9090 RVA: 0x00045901 File Offset: 0x00043B01
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C11 RID: 3089
			// (set) Token: 0x06002383 RID: 9091 RVA: 0x00045914 File Offset: 0x00043B14
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000C12 RID: 3090
			// (set) Token: 0x06002384 RID: 9092 RVA: 0x0004592C File Offset: 0x00043B2C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000C13 RID: 3091
			// (set) Token: 0x06002385 RID: 9093 RVA: 0x00045944 File Offset: 0x00043B44
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000C14 RID: 3092
			// (set) Token: 0x06002386 RID: 9094 RVA: 0x0004595C File Offset: 0x00043B5C
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
