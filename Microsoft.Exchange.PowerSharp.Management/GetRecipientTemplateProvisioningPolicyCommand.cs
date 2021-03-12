using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AF5 RID: 2805
	public class GetRecipientTemplateProvisioningPolicyCommand : SyntheticCommandWithPipelineInput<RecipientTemplateProvisioningPolicy, RecipientTemplateProvisioningPolicy>
	{
		// Token: 0x06008A0F RID: 35343 RVA: 0x000CAFD4 File Offset: 0x000C91D4
		private GetRecipientTemplateProvisioningPolicyCommand() : base("Get-RecipientTemplateProvisioningPolicy")
		{
		}

		// Token: 0x06008A10 RID: 35344 RVA: 0x000CAFE1 File Offset: 0x000C91E1
		public GetRecipientTemplateProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008A11 RID: 35345 RVA: 0x000CAFF0 File Offset: 0x000C91F0
		public virtual GetRecipientTemplateProvisioningPolicyCommand SetParameters(GetRecipientTemplateProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008A12 RID: 35346 RVA: 0x000CAFFA File Offset: 0x000C91FA
		public virtual GetRecipientTemplateProvisioningPolicyCommand SetParameters(GetRecipientTemplateProvisioningPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AF6 RID: 2806
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005FDE RID: 24542
			// (set) Token: 0x06008A13 RID: 35347 RVA: 0x000CB004 File Offset: 0x000C9204
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005FDF RID: 24543
			// (set) Token: 0x06008A14 RID: 35348 RVA: 0x000CB022 File Offset: 0x000C9222
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FE0 RID: 24544
			// (set) Token: 0x06008A15 RID: 35349 RVA: 0x000CB035 File Offset: 0x000C9235
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FE1 RID: 24545
			// (set) Token: 0x06008A16 RID: 35350 RVA: 0x000CB04D File Offset: 0x000C924D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FE2 RID: 24546
			// (set) Token: 0x06008A17 RID: 35351 RVA: 0x000CB065 File Offset: 0x000C9265
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FE3 RID: 24547
			// (set) Token: 0x06008A18 RID: 35352 RVA: 0x000CB07D File Offset: 0x000C927D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AF7 RID: 2807
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005FE4 RID: 24548
			// (set) Token: 0x06008A1A RID: 35354 RVA: 0x000CB09D File Offset: 0x000C929D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ProvisioningPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17005FE5 RID: 24549
			// (set) Token: 0x06008A1B RID: 35355 RVA: 0x000CB0BB File Offset: 0x000C92BB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005FE6 RID: 24550
			// (set) Token: 0x06008A1C RID: 35356 RVA: 0x000CB0D9 File Offset: 0x000C92D9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FE7 RID: 24551
			// (set) Token: 0x06008A1D RID: 35357 RVA: 0x000CB0EC File Offset: 0x000C92EC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FE8 RID: 24552
			// (set) Token: 0x06008A1E RID: 35358 RVA: 0x000CB104 File Offset: 0x000C9304
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FE9 RID: 24553
			// (set) Token: 0x06008A1F RID: 35359 RVA: 0x000CB11C File Offset: 0x000C931C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FEA RID: 24554
			// (set) Token: 0x06008A20 RID: 35360 RVA: 0x000CB134 File Offset: 0x000C9334
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
