using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004C7 RID: 1223
	public class UpdateGlobalAddressListCommand : SyntheticCommandWithPipelineInput<GlobalAddressListIdParameter, GlobalAddressListIdParameter>
	{
		// Token: 0x06004425 RID: 17445 RVA: 0x0007013A File Offset: 0x0006E33A
		private UpdateGlobalAddressListCommand() : base("Update-GlobalAddressList")
		{
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x00070147 File Offset: 0x0006E347
		public UpdateGlobalAddressListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x00070156 File Offset: 0x0006E356
		public virtual UpdateGlobalAddressListCommand SetParameters(UpdateGlobalAddressListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x00070160 File Offset: 0x0006E360
		public virtual UpdateGlobalAddressListCommand SetParameters(UpdateGlobalAddressListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004C8 RID: 1224
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002650 RID: 9808
			// (set) Token: 0x06004429 RID: 17449 RVA: 0x0007016A File Offset: 0x0006E36A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002651 RID: 9809
			// (set) Token: 0x0600442A RID: 17450 RVA: 0x0007017D File Offset: 0x0006E37D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002652 RID: 9810
			// (set) Token: 0x0600442B RID: 17451 RVA: 0x00070195 File Offset: 0x0006E395
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002653 RID: 9811
			// (set) Token: 0x0600442C RID: 17452 RVA: 0x000701AD File Offset: 0x0006E3AD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002654 RID: 9812
			// (set) Token: 0x0600442D RID: 17453 RVA: 0x000701C5 File Offset: 0x0006E3C5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002655 RID: 9813
			// (set) Token: 0x0600442E RID: 17454 RVA: 0x000701DD File Offset: 0x0006E3DD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004C9 RID: 1225
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002656 RID: 9814
			// (set) Token: 0x06004430 RID: 17456 RVA: 0x000701FD File Offset: 0x0006E3FD
			public virtual GlobalAddressListIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002657 RID: 9815
			// (set) Token: 0x06004431 RID: 17457 RVA: 0x00070210 File Offset: 0x0006E410
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002658 RID: 9816
			// (set) Token: 0x06004432 RID: 17458 RVA: 0x00070223 File Offset: 0x0006E423
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002659 RID: 9817
			// (set) Token: 0x06004433 RID: 17459 RVA: 0x0007023B File Offset: 0x0006E43B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700265A RID: 9818
			// (set) Token: 0x06004434 RID: 17460 RVA: 0x00070253 File Offset: 0x0006E453
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700265B RID: 9819
			// (set) Token: 0x06004435 RID: 17461 RVA: 0x0007026B File Offset: 0x0006E46B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700265C RID: 9820
			// (set) Token: 0x06004436 RID: 17462 RVA: 0x00070283 File Offset: 0x0006E483
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
