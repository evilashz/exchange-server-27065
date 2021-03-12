using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000763 RID: 1891
	public class GetIPBlockListProviderCommand : SyntheticCommandWithPipelineInput<IPBlockListProvider, IPBlockListProvider>
	{
		// Token: 0x06006023 RID: 24611 RVA: 0x0009446C File Offset: 0x0009266C
		private GetIPBlockListProviderCommand() : base("Get-IPBlockListProvider")
		{
		}

		// Token: 0x06006024 RID: 24612 RVA: 0x00094479 File Offset: 0x00092679
		public GetIPBlockListProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x00094488 File Offset: 0x00092688
		public virtual GetIPBlockListProviderCommand SetParameters(GetIPBlockListProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x00094492 File Offset: 0x00092692
		public virtual GetIPBlockListProviderCommand SetParameters(GetIPBlockListProviderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000764 RID: 1892
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003D16 RID: 15638
			// (set) Token: 0x06006027 RID: 24615 RVA: 0x0009449C File Offset: 0x0009269C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003D17 RID: 15639
			// (set) Token: 0x06006028 RID: 24616 RVA: 0x000944AF File Offset: 0x000926AF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D18 RID: 15640
			// (set) Token: 0x06006029 RID: 24617 RVA: 0x000944C7 File Offset: 0x000926C7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D19 RID: 15641
			// (set) Token: 0x0600602A RID: 24618 RVA: 0x000944DF File Offset: 0x000926DF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D1A RID: 15642
			// (set) Token: 0x0600602B RID: 24619 RVA: 0x000944F7 File Offset: 0x000926F7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000765 RID: 1893
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003D1B RID: 15643
			// (set) Token: 0x0600602D RID: 24621 RVA: 0x00094517 File Offset: 0x00092717
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new IPBlockListProviderIdParameter(value) : null);
				}
			}

			// Token: 0x17003D1C RID: 15644
			// (set) Token: 0x0600602E RID: 24622 RVA: 0x00094535 File Offset: 0x00092735
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003D1D RID: 15645
			// (set) Token: 0x0600602F RID: 24623 RVA: 0x00094548 File Offset: 0x00092748
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D1E RID: 15646
			// (set) Token: 0x06006030 RID: 24624 RVA: 0x00094560 File Offset: 0x00092760
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D1F RID: 15647
			// (set) Token: 0x06006031 RID: 24625 RVA: 0x00094578 File Offset: 0x00092778
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D20 RID: 15648
			// (set) Token: 0x06006032 RID: 24626 RVA: 0x00094590 File Offset: 0x00092790
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
