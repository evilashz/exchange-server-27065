using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000DB RID: 219
	public class RestoreDetailsTemplateCommand : SyntheticCommandWithPipelineInput<DetailsTemplate, DetailsTemplate>
	{
		// Token: 0x06001D49 RID: 7497 RVA: 0x0003DBC1 File Offset: 0x0003BDC1
		private RestoreDetailsTemplateCommand() : base("Restore-DetailsTemplate")
		{
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0003DBCE File Offset: 0x0003BDCE
		public RestoreDetailsTemplateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0003DBDD File Offset: 0x0003BDDD
		public virtual RestoreDetailsTemplateCommand SetParameters(RestoreDetailsTemplateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0003DBE7 File Offset: 0x0003BDE7
		public virtual RestoreDetailsTemplateCommand SetParameters(RestoreDetailsTemplateCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000DC RID: 220
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700074C RID: 1868
			// (set) Token: 0x06001D4D RID: 7501 RVA: 0x0003DBF1 File Offset: 0x0003BDF1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700074D RID: 1869
			// (set) Token: 0x06001D4E RID: 7502 RVA: 0x0003DC04 File Offset: 0x0003BE04
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700074E RID: 1870
			// (set) Token: 0x06001D4F RID: 7503 RVA: 0x0003DC1C File Offset: 0x0003BE1C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700074F RID: 1871
			// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0003DC34 File Offset: 0x0003BE34
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000750 RID: 1872
			// (set) Token: 0x06001D51 RID: 7505 RVA: 0x0003DC4C File Offset: 0x0003BE4C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000751 RID: 1873
			// (set) Token: 0x06001D52 RID: 7506 RVA: 0x0003DC64 File Offset: 0x0003BE64
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000752 RID: 1874
			// (set) Token: 0x06001D53 RID: 7507 RVA: 0x0003DC7C File Offset: 0x0003BE7C
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020000DD RID: 221
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000753 RID: 1875
			// (set) Token: 0x06001D55 RID: 7509 RVA: 0x0003DC9C File Offset: 0x0003BE9C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DetailsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x17000754 RID: 1876
			// (set) Token: 0x06001D56 RID: 7510 RVA: 0x0003DCBA File Offset: 0x0003BEBA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000755 RID: 1877
			// (set) Token: 0x06001D57 RID: 7511 RVA: 0x0003DCCD File Offset: 0x0003BECD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000756 RID: 1878
			// (set) Token: 0x06001D58 RID: 7512 RVA: 0x0003DCE5 File Offset: 0x0003BEE5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000757 RID: 1879
			// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0003DCFD File Offset: 0x0003BEFD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000758 RID: 1880
			// (set) Token: 0x06001D5A RID: 7514 RVA: 0x0003DD15 File Offset: 0x0003BF15
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000759 RID: 1881
			// (set) Token: 0x06001D5B RID: 7515 RVA: 0x0003DD2D File Offset: 0x0003BF2D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700075A RID: 1882
			// (set) Token: 0x06001D5C RID: 7516 RVA: 0x0003DD45 File Offset: 0x0003BF45
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
