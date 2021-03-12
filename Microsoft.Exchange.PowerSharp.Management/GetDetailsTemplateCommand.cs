using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000D8 RID: 216
	public class GetDetailsTemplateCommand : SyntheticCommandWithPipelineInput<DetailsTemplate, DetailsTemplate>
	{
		// Token: 0x06001D36 RID: 7478 RVA: 0x0003DA4D File Offset: 0x0003BC4D
		private GetDetailsTemplateCommand() : base("Get-DetailsTemplate")
		{
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0003DA5A File Offset: 0x0003BC5A
		public GetDetailsTemplateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0003DA69 File Offset: 0x0003BC69
		public virtual GetDetailsTemplateCommand SetParameters(GetDetailsTemplateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0003DA73 File Offset: 0x0003BC73
		public virtual GetDetailsTemplateCommand SetParameters(GetDetailsTemplateCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000D9 RID: 217
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700073F RID: 1855
			// (set) Token: 0x06001D3A RID: 7482 RVA: 0x0003DA7D File Offset: 0x0003BC7D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000740 RID: 1856
			// (set) Token: 0x06001D3B RID: 7483 RVA: 0x0003DA90 File Offset: 0x0003BC90
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000741 RID: 1857
			// (set) Token: 0x06001D3C RID: 7484 RVA: 0x0003DAA8 File Offset: 0x0003BCA8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000742 RID: 1858
			// (set) Token: 0x06001D3D RID: 7485 RVA: 0x0003DAC0 File Offset: 0x0003BCC0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000743 RID: 1859
			// (set) Token: 0x06001D3E RID: 7486 RVA: 0x0003DAD8 File Offset: 0x0003BCD8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000744 RID: 1860
			// (set) Token: 0x06001D3F RID: 7487 RVA: 0x0003DAF0 File Offset: 0x0003BCF0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000DA RID: 218
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000745 RID: 1861
			// (set) Token: 0x06001D41 RID: 7489 RVA: 0x0003DB10 File Offset: 0x0003BD10
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DetailsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x17000746 RID: 1862
			// (set) Token: 0x06001D42 RID: 7490 RVA: 0x0003DB2E File Offset: 0x0003BD2E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000747 RID: 1863
			// (set) Token: 0x06001D43 RID: 7491 RVA: 0x0003DB41 File Offset: 0x0003BD41
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000748 RID: 1864
			// (set) Token: 0x06001D44 RID: 7492 RVA: 0x0003DB59 File Offset: 0x0003BD59
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000749 RID: 1865
			// (set) Token: 0x06001D45 RID: 7493 RVA: 0x0003DB71 File Offset: 0x0003BD71
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700074A RID: 1866
			// (set) Token: 0x06001D46 RID: 7494 RVA: 0x0003DB89 File Offset: 0x0003BD89
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700074B RID: 1867
			// (set) Token: 0x06001D47 RID: 7495 RVA: 0x0003DBA1 File Offset: 0x0003BDA1
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
