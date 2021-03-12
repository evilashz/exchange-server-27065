using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000D5 RID: 213
	public class UpdateServicePlanCommand : SyntheticCommandWithPipelineInputNoOutput<OrganizationIdParameter>
	{
		// Token: 0x06001D12 RID: 7442 RVA: 0x0003D745 File Offset: 0x0003B945
		private UpdateServicePlanCommand() : base("Update-ServicePlan")
		{
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0003D752 File Offset: 0x0003B952
		public UpdateServicePlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0003D761 File Offset: 0x0003B961
		public virtual UpdateServicePlanCommand SetParameters(UpdateServicePlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0003D76B File Offset: 0x0003B96B
		public virtual UpdateServicePlanCommand SetParameters(UpdateServicePlanCommand.MigrateServicePlanParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000D6 RID: 214
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000721 RID: 1825
			// (set) Token: 0x06001D16 RID: 7446 RVA: 0x0003D775 File Offset: 0x0003B975
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000722 RID: 1826
			// (set) Token: 0x06001D17 RID: 7447 RVA: 0x0003D793 File Offset: 0x0003B993
			public virtual SwitchParameter ConfigOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigOnly"] = value;
				}
			}

			// Token: 0x17000723 RID: 1827
			// (set) Token: 0x06001D18 RID: 7448 RVA: 0x0003D7AB File Offset: 0x0003B9AB
			public virtual SwitchParameter Conservative
			{
				set
				{
					base.PowerSharpParameters["Conservative"] = value;
				}
			}

			// Token: 0x17000724 RID: 1828
			// (set) Token: 0x06001D19 RID: 7449 RVA: 0x0003D7C3 File Offset: 0x0003B9C3
			public virtual SwitchParameter IncludeUserUpdatePhase
			{
				set
				{
					base.PowerSharpParameters["IncludeUserUpdatePhase"] = value;
				}
			}

			// Token: 0x17000725 RID: 1829
			// (set) Token: 0x06001D1A RID: 7450 RVA: 0x0003D7DB File Offset: 0x0003B9DB
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x17000726 RID: 1830
			// (set) Token: 0x06001D1B RID: 7451 RVA: 0x0003D7F3 File Offset: 0x0003B9F3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000727 RID: 1831
			// (set) Token: 0x06001D1C RID: 7452 RVA: 0x0003D806 File Offset: 0x0003BA06
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x17000728 RID: 1832
			// (set) Token: 0x06001D1D RID: 7453 RVA: 0x0003D81E File Offset: 0x0003BA1E
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x17000729 RID: 1833
			// (set) Token: 0x06001D1E RID: 7454 RVA: 0x0003D836 File Offset: 0x0003BA36
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x1700072A RID: 1834
			// (set) Token: 0x06001D1F RID: 7455 RVA: 0x0003D84E File Offset: 0x0003BA4E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700072B RID: 1835
			// (set) Token: 0x06001D20 RID: 7456 RVA: 0x0003D866 File Offset: 0x0003BA66
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700072C RID: 1836
			// (set) Token: 0x06001D21 RID: 7457 RVA: 0x0003D87E File Offset: 0x0003BA7E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700072D RID: 1837
			// (set) Token: 0x06001D22 RID: 7458 RVA: 0x0003D896 File Offset: 0x0003BA96
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700072E RID: 1838
			// (set) Token: 0x06001D23 RID: 7459 RVA: 0x0003D8AE File Offset: 0x0003BAAE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000D7 RID: 215
		public class MigrateServicePlanParameterSetParameters : ParametersBase
		{
			// Token: 0x1700072F RID: 1839
			// (set) Token: 0x06001D25 RID: 7461 RVA: 0x0003D8CE File Offset: 0x0003BACE
			public virtual string ProgramId
			{
				set
				{
					base.PowerSharpParameters["ProgramId"] = value;
				}
			}

			// Token: 0x17000730 RID: 1840
			// (set) Token: 0x06001D26 RID: 7462 RVA: 0x0003D8E1 File Offset: 0x0003BAE1
			public virtual string OfferId
			{
				set
				{
					base.PowerSharpParameters["OfferId"] = value;
				}
			}

			// Token: 0x17000731 RID: 1841
			// (set) Token: 0x06001D27 RID: 7463 RVA: 0x0003D8F4 File Offset: 0x0003BAF4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000732 RID: 1842
			// (set) Token: 0x06001D28 RID: 7464 RVA: 0x0003D912 File Offset: 0x0003BB12
			public virtual SwitchParameter ConfigOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigOnly"] = value;
				}
			}

			// Token: 0x17000733 RID: 1843
			// (set) Token: 0x06001D29 RID: 7465 RVA: 0x0003D92A File Offset: 0x0003BB2A
			public virtual SwitchParameter Conservative
			{
				set
				{
					base.PowerSharpParameters["Conservative"] = value;
				}
			}

			// Token: 0x17000734 RID: 1844
			// (set) Token: 0x06001D2A RID: 7466 RVA: 0x0003D942 File Offset: 0x0003BB42
			public virtual SwitchParameter IncludeUserUpdatePhase
			{
				set
				{
					base.PowerSharpParameters["IncludeUserUpdatePhase"] = value;
				}
			}

			// Token: 0x17000735 RID: 1845
			// (set) Token: 0x06001D2B RID: 7467 RVA: 0x0003D95A File Offset: 0x0003BB5A
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x17000736 RID: 1846
			// (set) Token: 0x06001D2C RID: 7468 RVA: 0x0003D972 File Offset: 0x0003BB72
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000737 RID: 1847
			// (set) Token: 0x06001D2D RID: 7469 RVA: 0x0003D985 File Offset: 0x0003BB85
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x17000738 RID: 1848
			// (set) Token: 0x06001D2E RID: 7470 RVA: 0x0003D99D File Offset: 0x0003BB9D
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x17000739 RID: 1849
			// (set) Token: 0x06001D2F RID: 7471 RVA: 0x0003D9B5 File Offset: 0x0003BBB5
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x1700073A RID: 1850
			// (set) Token: 0x06001D30 RID: 7472 RVA: 0x0003D9CD File Offset: 0x0003BBCD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700073B RID: 1851
			// (set) Token: 0x06001D31 RID: 7473 RVA: 0x0003D9E5 File Offset: 0x0003BBE5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700073C RID: 1852
			// (set) Token: 0x06001D32 RID: 7474 RVA: 0x0003D9FD File Offset: 0x0003BBFD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700073D RID: 1853
			// (set) Token: 0x06001D33 RID: 7475 RVA: 0x0003DA15 File Offset: 0x0003BC15
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700073E RID: 1854
			// (set) Token: 0x06001D34 RID: 7476 RVA: 0x0003DA2D File Offset: 0x0003BC2D
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
