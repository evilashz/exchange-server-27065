using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000DE RID: 222
	public class SetDetailsTemplateCommand : SyntheticCommandWithPipelineInputNoOutput<DetailsTemplate>
	{
		// Token: 0x06001D5E RID: 7518 RVA: 0x0003DD65 File Offset: 0x0003BF65
		private SetDetailsTemplateCommand() : base("Set-DetailsTemplate")
		{
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0003DD72 File Offset: 0x0003BF72
		public SetDetailsTemplateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0003DD81 File Offset: 0x0003BF81
		public virtual SetDetailsTemplateCommand SetParameters(SetDetailsTemplateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0003DD8B File Offset: 0x0003BF8B
		public virtual SetDetailsTemplateCommand SetParameters(SetDetailsTemplateCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000DF RID: 223
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700075B RID: 1883
			// (set) Token: 0x06001D62 RID: 7522 RVA: 0x0003DD95 File Offset: 0x0003BF95
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700075C RID: 1884
			// (set) Token: 0x06001D63 RID: 7523 RVA: 0x0003DDA8 File Offset: 0x0003BFA8
			public virtual MultiValuedProperty<Page> Pages
			{
				set
				{
					base.PowerSharpParameters["Pages"] = value;
				}
			}

			// Token: 0x1700075D RID: 1885
			// (set) Token: 0x06001D64 RID: 7524 RVA: 0x0003DDBB File Offset: 0x0003BFBB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700075E RID: 1886
			// (set) Token: 0x06001D65 RID: 7525 RVA: 0x0003DDD3 File Offset: 0x0003BFD3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700075F RID: 1887
			// (set) Token: 0x06001D66 RID: 7526 RVA: 0x0003DDEB File Offset: 0x0003BFEB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000760 RID: 1888
			// (set) Token: 0x06001D67 RID: 7527 RVA: 0x0003DE03 File Offset: 0x0003C003
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000761 RID: 1889
			// (set) Token: 0x06001D68 RID: 7528 RVA: 0x0003DE1B File Offset: 0x0003C01B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000E0 RID: 224
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000762 RID: 1890
			// (set) Token: 0x06001D6A RID: 7530 RVA: 0x0003DE3B File Offset: 0x0003C03B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DetailsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x17000763 RID: 1891
			// (set) Token: 0x06001D6B RID: 7531 RVA: 0x0003DE59 File Offset: 0x0003C059
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000764 RID: 1892
			// (set) Token: 0x06001D6C RID: 7532 RVA: 0x0003DE6C File Offset: 0x0003C06C
			public virtual MultiValuedProperty<Page> Pages
			{
				set
				{
					base.PowerSharpParameters["Pages"] = value;
				}
			}

			// Token: 0x17000765 RID: 1893
			// (set) Token: 0x06001D6D RID: 7533 RVA: 0x0003DE7F File Offset: 0x0003C07F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000766 RID: 1894
			// (set) Token: 0x06001D6E RID: 7534 RVA: 0x0003DE97 File Offset: 0x0003C097
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000767 RID: 1895
			// (set) Token: 0x06001D6F RID: 7535 RVA: 0x0003DEAF File Offset: 0x0003C0AF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000768 RID: 1896
			// (set) Token: 0x06001D70 RID: 7536 RVA: 0x0003DEC7 File Offset: 0x0003C0C7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000769 RID: 1897
			// (set) Token: 0x06001D71 RID: 7537 RVA: 0x0003DEDF File Offset: 0x0003C0DF
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
