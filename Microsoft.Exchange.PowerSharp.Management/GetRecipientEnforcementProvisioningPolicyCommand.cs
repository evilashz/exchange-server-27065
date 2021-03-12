using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AF2 RID: 2802
	public class GetRecipientEnforcementProvisioningPolicyCommand : SyntheticCommandWithPipelineInput<RecipientEnforcementProvisioningPolicy, RecipientEnforcementProvisioningPolicy>
	{
		// Token: 0x060089F8 RID: 35320 RVA: 0x000CADF4 File Offset: 0x000C8FF4
		private GetRecipientEnforcementProvisioningPolicyCommand() : base("Get-RecipientEnforcementProvisioningPolicy")
		{
		}

		// Token: 0x060089F9 RID: 35321 RVA: 0x000CAE01 File Offset: 0x000C9001
		public GetRecipientEnforcementProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060089FA RID: 35322 RVA: 0x000CAE10 File Offset: 0x000C9010
		public virtual GetRecipientEnforcementProvisioningPolicyCommand SetParameters(GetRecipientEnforcementProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060089FB RID: 35323 RVA: 0x000CAE1A File Offset: 0x000C901A
		public virtual GetRecipientEnforcementProvisioningPolicyCommand SetParameters(GetRecipientEnforcementProvisioningPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AF3 RID: 2803
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005FCD RID: 24525
			// (set) Token: 0x060089FC RID: 35324 RVA: 0x000CAE24 File Offset: 0x000C9024
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17005FCE RID: 24526
			// (set) Token: 0x060089FD RID: 35325 RVA: 0x000CAE3C File Offset: 0x000C903C
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17005FCF RID: 24527
			// (set) Token: 0x060089FE RID: 35326 RVA: 0x000CAE54 File Offset: 0x000C9054
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005FD0 RID: 24528
			// (set) Token: 0x060089FF RID: 35327 RVA: 0x000CAE72 File Offset: 0x000C9072
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FD1 RID: 24529
			// (set) Token: 0x06008A00 RID: 35328 RVA: 0x000CAE85 File Offset: 0x000C9085
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FD2 RID: 24530
			// (set) Token: 0x06008A01 RID: 35329 RVA: 0x000CAE9D File Offset: 0x000C909D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FD3 RID: 24531
			// (set) Token: 0x06008A02 RID: 35330 RVA: 0x000CAEB5 File Offset: 0x000C90B5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FD4 RID: 24532
			// (set) Token: 0x06008A03 RID: 35331 RVA: 0x000CAECD File Offset: 0x000C90CD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AF4 RID: 2804
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005FD5 RID: 24533
			// (set) Token: 0x06008A05 RID: 35333 RVA: 0x000CAEED File Offset: 0x000C90ED
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientEnforcementProvisioningPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17005FD6 RID: 24534
			// (set) Token: 0x06008A06 RID: 35334 RVA: 0x000CAF0B File Offset: 0x000C910B
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17005FD7 RID: 24535
			// (set) Token: 0x06008A07 RID: 35335 RVA: 0x000CAF23 File Offset: 0x000C9123
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17005FD8 RID: 24536
			// (set) Token: 0x06008A08 RID: 35336 RVA: 0x000CAF3B File Offset: 0x000C913B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005FD9 RID: 24537
			// (set) Token: 0x06008A09 RID: 35337 RVA: 0x000CAF59 File Offset: 0x000C9159
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FDA RID: 24538
			// (set) Token: 0x06008A0A RID: 35338 RVA: 0x000CAF6C File Offset: 0x000C916C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FDB RID: 24539
			// (set) Token: 0x06008A0B RID: 35339 RVA: 0x000CAF84 File Offset: 0x000C9184
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FDC RID: 24540
			// (set) Token: 0x06008A0C RID: 35340 RVA: 0x000CAF9C File Offset: 0x000C919C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FDD RID: 24541
			// (set) Token: 0x06008A0D RID: 35341 RVA: 0x000CAFB4 File Offset: 0x000C91B4
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
