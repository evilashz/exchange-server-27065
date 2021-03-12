using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004BB RID: 1211
	public class RemoveGlobalAddressListCommand : SyntheticCommandWithPipelineInput<GlobalAddressListIdParameter, GlobalAddressListIdParameter>
	{
		// Token: 0x06004379 RID: 17273 RVA: 0x0006F418 File Offset: 0x0006D618
		private RemoveGlobalAddressListCommand() : base("Remove-GlobalAddressList")
		{
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x0006F425 File Offset: 0x0006D625
		public RemoveGlobalAddressListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x0006F434 File Offset: 0x0006D634
		public virtual RemoveGlobalAddressListCommand SetParameters(RemoveGlobalAddressListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x0006F43E File Offset: 0x0006D63E
		public virtual RemoveGlobalAddressListCommand SetParameters(RemoveGlobalAddressListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004BC RID: 1212
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170025BC RID: 9660
			// (set) Token: 0x0600437D RID: 17277 RVA: 0x0006F448 File Offset: 0x0006D648
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170025BD RID: 9661
			// (set) Token: 0x0600437E RID: 17278 RVA: 0x0006F45B File Offset: 0x0006D65B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170025BE RID: 9662
			// (set) Token: 0x0600437F RID: 17279 RVA: 0x0006F473 File Offset: 0x0006D673
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170025BF RID: 9663
			// (set) Token: 0x06004380 RID: 17280 RVA: 0x0006F48B File Offset: 0x0006D68B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170025C0 RID: 9664
			// (set) Token: 0x06004381 RID: 17281 RVA: 0x0006F4A3 File Offset: 0x0006D6A3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170025C1 RID: 9665
			// (set) Token: 0x06004382 RID: 17282 RVA: 0x0006F4BB File Offset: 0x0006D6BB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170025C2 RID: 9666
			// (set) Token: 0x06004383 RID: 17283 RVA: 0x0006F4D3 File Offset: 0x0006D6D3
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020004BD RID: 1213
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170025C3 RID: 9667
			// (set) Token: 0x06004385 RID: 17285 RVA: 0x0006F4F3 File Offset: 0x0006D6F3
			public virtual GlobalAddressListIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170025C4 RID: 9668
			// (set) Token: 0x06004386 RID: 17286 RVA: 0x0006F506 File Offset: 0x0006D706
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170025C5 RID: 9669
			// (set) Token: 0x06004387 RID: 17287 RVA: 0x0006F519 File Offset: 0x0006D719
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170025C6 RID: 9670
			// (set) Token: 0x06004388 RID: 17288 RVA: 0x0006F531 File Offset: 0x0006D731
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170025C7 RID: 9671
			// (set) Token: 0x06004389 RID: 17289 RVA: 0x0006F549 File Offset: 0x0006D749
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170025C8 RID: 9672
			// (set) Token: 0x0600438A RID: 17290 RVA: 0x0006F561 File Offset: 0x0006D761
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170025C9 RID: 9673
			// (set) Token: 0x0600438B RID: 17291 RVA: 0x0006F579 File Offset: 0x0006D779
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170025CA RID: 9674
			// (set) Token: 0x0600438C RID: 17292 RVA: 0x0006F591 File Offset: 0x0006D791
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
