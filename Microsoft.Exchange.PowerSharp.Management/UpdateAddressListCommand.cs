using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004C4 RID: 1220
	public class UpdateAddressListCommand : SyntheticCommandWithPipelineInput<AddressListIdParameter, AddressListIdParameter>
	{
		// Token: 0x06004412 RID: 17426 RVA: 0x0006FFD1 File Offset: 0x0006E1D1
		private UpdateAddressListCommand() : base("Update-AddressList")
		{
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x0006FFDE File Offset: 0x0006E1DE
		public UpdateAddressListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x0006FFED File Offset: 0x0006E1ED
		public virtual UpdateAddressListCommand SetParameters(UpdateAddressListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x0006FFF7 File Offset: 0x0006E1F7
		public virtual UpdateAddressListCommand SetParameters(UpdateAddressListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004C5 RID: 1221
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002643 RID: 9795
			// (set) Token: 0x06004416 RID: 17430 RVA: 0x00070001 File Offset: 0x0006E201
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002644 RID: 9796
			// (set) Token: 0x06004417 RID: 17431 RVA: 0x00070014 File Offset: 0x0006E214
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002645 RID: 9797
			// (set) Token: 0x06004418 RID: 17432 RVA: 0x0007002C File Offset: 0x0006E22C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002646 RID: 9798
			// (set) Token: 0x06004419 RID: 17433 RVA: 0x00070044 File Offset: 0x0006E244
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002647 RID: 9799
			// (set) Token: 0x0600441A RID: 17434 RVA: 0x0007005C File Offset: 0x0006E25C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002648 RID: 9800
			// (set) Token: 0x0600441B RID: 17435 RVA: 0x00070074 File Offset: 0x0006E274
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004C6 RID: 1222
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002649 RID: 9801
			// (set) Token: 0x0600441D RID: 17437 RVA: 0x00070094 File Offset: 0x0006E294
			public virtual AddressListIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700264A RID: 9802
			// (set) Token: 0x0600441E RID: 17438 RVA: 0x000700A7 File Offset: 0x0006E2A7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700264B RID: 9803
			// (set) Token: 0x0600441F RID: 17439 RVA: 0x000700BA File Offset: 0x0006E2BA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700264C RID: 9804
			// (set) Token: 0x06004420 RID: 17440 RVA: 0x000700D2 File Offset: 0x0006E2D2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700264D RID: 9805
			// (set) Token: 0x06004421 RID: 17441 RVA: 0x000700EA File Offset: 0x0006E2EA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700264E RID: 9806
			// (set) Token: 0x06004422 RID: 17442 RVA: 0x00070102 File Offset: 0x0006E302
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700264F RID: 9807
			// (set) Token: 0x06004423 RID: 17443 RVA: 0x0007011A File Offset: 0x0006E31A
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
