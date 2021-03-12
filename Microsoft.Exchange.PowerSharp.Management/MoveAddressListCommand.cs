using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004CA RID: 1226
	public class MoveAddressListCommand : SyntheticCommandWithPipelineInput<AddressBookBase, AddressBookBase>
	{
		// Token: 0x06004438 RID: 17464 RVA: 0x000702A3 File Offset: 0x0006E4A3
		private MoveAddressListCommand() : base("Move-AddressList")
		{
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x000702B0 File Offset: 0x0006E4B0
		public MoveAddressListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x000702BF File Offset: 0x0006E4BF
		public virtual MoveAddressListCommand SetParameters(MoveAddressListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x000702C9 File Offset: 0x0006E4C9
		public virtual MoveAddressListCommand SetParameters(MoveAddressListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004CB RID: 1227
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700265D RID: 9821
			// (set) Token: 0x0600443C RID: 17468 RVA: 0x000702D3 File Offset: 0x0006E4D3
			public virtual AddressListIdParameter Target
			{
				set
				{
					base.PowerSharpParameters["Target"] = value;
				}
			}

			// Token: 0x1700265E RID: 9822
			// (set) Token: 0x0600443D RID: 17469 RVA: 0x000702E6 File Offset: 0x0006E4E6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700265F RID: 9823
			// (set) Token: 0x0600443E RID: 17470 RVA: 0x000702F9 File Offset: 0x0006E4F9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002660 RID: 9824
			// (set) Token: 0x0600443F RID: 17471 RVA: 0x00070311 File Offset: 0x0006E511
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002661 RID: 9825
			// (set) Token: 0x06004440 RID: 17472 RVA: 0x00070329 File Offset: 0x0006E529
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002662 RID: 9826
			// (set) Token: 0x06004441 RID: 17473 RVA: 0x00070341 File Offset: 0x0006E541
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002663 RID: 9827
			// (set) Token: 0x06004442 RID: 17474 RVA: 0x00070359 File Offset: 0x0006E559
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002664 RID: 9828
			// (set) Token: 0x06004443 RID: 17475 RVA: 0x00070371 File Offset: 0x0006E571
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020004CC RID: 1228
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002665 RID: 9829
			// (set) Token: 0x06004445 RID: 17477 RVA: 0x00070391 File Offset: 0x0006E591
			public virtual AddressListIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002666 RID: 9830
			// (set) Token: 0x06004446 RID: 17478 RVA: 0x000703A4 File Offset: 0x0006E5A4
			public virtual AddressListIdParameter Target
			{
				set
				{
					base.PowerSharpParameters["Target"] = value;
				}
			}

			// Token: 0x17002667 RID: 9831
			// (set) Token: 0x06004447 RID: 17479 RVA: 0x000703B7 File Offset: 0x0006E5B7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002668 RID: 9832
			// (set) Token: 0x06004448 RID: 17480 RVA: 0x000703CA File Offset: 0x0006E5CA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002669 RID: 9833
			// (set) Token: 0x06004449 RID: 17481 RVA: 0x000703E2 File Offset: 0x0006E5E2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700266A RID: 9834
			// (set) Token: 0x0600444A RID: 17482 RVA: 0x000703FA File Offset: 0x0006E5FA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700266B RID: 9835
			// (set) Token: 0x0600444B RID: 17483 RVA: 0x00070412 File Offset: 0x0006E612
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700266C RID: 9836
			// (set) Token: 0x0600444C RID: 17484 RVA: 0x0007042A File Offset: 0x0006E62A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700266D RID: 9837
			// (set) Token: 0x0600444D RID: 17485 RVA: 0x00070442 File Offset: 0x0006E642
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
