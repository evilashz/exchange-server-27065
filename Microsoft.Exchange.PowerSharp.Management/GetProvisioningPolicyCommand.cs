using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AEF RID: 2799
	public class GetProvisioningPolicyCommand : SyntheticCommandWithPipelineInput<ADProvisioningPolicy, ADProvisioningPolicy>
	{
		// Token: 0x060089E5 RID: 35301 RVA: 0x000CAC74 File Offset: 0x000C8E74
		private GetProvisioningPolicyCommand() : base("Get-ProvisioningPolicy")
		{
		}

		// Token: 0x060089E6 RID: 35302 RVA: 0x000CAC81 File Offset: 0x000C8E81
		public GetProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060089E7 RID: 35303 RVA: 0x000CAC90 File Offset: 0x000C8E90
		public virtual GetProvisioningPolicyCommand SetParameters(GetProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060089E8 RID: 35304 RVA: 0x000CAC9A File Offset: 0x000C8E9A
		public virtual GetProvisioningPolicyCommand SetParameters(GetProvisioningPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AF0 RID: 2800
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005FC0 RID: 24512
			// (set) Token: 0x060089E9 RID: 35305 RVA: 0x000CACA4 File Offset: 0x000C8EA4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005FC1 RID: 24513
			// (set) Token: 0x060089EA RID: 35306 RVA: 0x000CACC2 File Offset: 0x000C8EC2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FC2 RID: 24514
			// (set) Token: 0x060089EB RID: 35307 RVA: 0x000CACD5 File Offset: 0x000C8ED5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FC3 RID: 24515
			// (set) Token: 0x060089EC RID: 35308 RVA: 0x000CACED File Offset: 0x000C8EED
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FC4 RID: 24516
			// (set) Token: 0x060089ED RID: 35309 RVA: 0x000CAD05 File Offset: 0x000C8F05
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FC5 RID: 24517
			// (set) Token: 0x060089EE RID: 35310 RVA: 0x000CAD1D File Offset: 0x000C8F1D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AF1 RID: 2801
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005FC6 RID: 24518
			// (set) Token: 0x060089F0 RID: 35312 RVA: 0x000CAD3D File Offset: 0x000C8F3D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ProvisioningPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17005FC7 RID: 24519
			// (set) Token: 0x060089F1 RID: 35313 RVA: 0x000CAD5B File Offset: 0x000C8F5B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005FC8 RID: 24520
			// (set) Token: 0x060089F2 RID: 35314 RVA: 0x000CAD79 File Offset: 0x000C8F79
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FC9 RID: 24521
			// (set) Token: 0x060089F3 RID: 35315 RVA: 0x000CAD8C File Offset: 0x000C8F8C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FCA RID: 24522
			// (set) Token: 0x060089F4 RID: 35316 RVA: 0x000CADA4 File Offset: 0x000C8FA4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FCB RID: 24523
			// (set) Token: 0x060089F5 RID: 35317 RVA: 0x000CADBC File Offset: 0x000C8FBC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FCC RID: 24524
			// (set) Token: 0x060089F6 RID: 35318 RVA: 0x000CADD4 File Offset: 0x000C8FD4
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
