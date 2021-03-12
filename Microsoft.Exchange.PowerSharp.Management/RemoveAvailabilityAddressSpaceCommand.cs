using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004F0 RID: 1264
	public class RemoveAvailabilityAddressSpaceCommand : SyntheticCommandWithPipelineInput<AvailabilityAddressSpace, AvailabilityAddressSpace>
	{
		// Token: 0x06004541 RID: 17729 RVA: 0x000716F9 File Offset: 0x0006F8F9
		private RemoveAvailabilityAddressSpaceCommand() : base("Remove-AvailabilityAddressSpace")
		{
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x00071706 File Offset: 0x0006F906
		public RemoveAvailabilityAddressSpaceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x00071715 File Offset: 0x0006F915
		public virtual RemoveAvailabilityAddressSpaceCommand SetParameters(RemoveAvailabilityAddressSpaceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x0007171F File Offset: 0x0006F91F
		public virtual RemoveAvailabilityAddressSpaceCommand SetParameters(RemoveAvailabilityAddressSpaceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004F1 RID: 1265
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700271A RID: 10010
			// (set) Token: 0x06004545 RID: 17733 RVA: 0x00071729 File Offset: 0x0006F929
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700271B RID: 10011
			// (set) Token: 0x06004546 RID: 17734 RVA: 0x0007173C File Offset: 0x0006F93C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700271C RID: 10012
			// (set) Token: 0x06004547 RID: 17735 RVA: 0x00071754 File Offset: 0x0006F954
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700271D RID: 10013
			// (set) Token: 0x06004548 RID: 17736 RVA: 0x0007176C File Offset: 0x0006F96C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700271E RID: 10014
			// (set) Token: 0x06004549 RID: 17737 RVA: 0x00071784 File Offset: 0x0006F984
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700271F RID: 10015
			// (set) Token: 0x0600454A RID: 17738 RVA: 0x0007179C File Offset: 0x0006F99C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002720 RID: 10016
			// (set) Token: 0x0600454B RID: 17739 RVA: 0x000717B4 File Offset: 0x0006F9B4
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020004F2 RID: 1266
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002721 RID: 10017
			// (set) Token: 0x0600454D RID: 17741 RVA: 0x000717D4 File Offset: 0x0006F9D4
			public virtual AvailabilityAddressSpaceIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002722 RID: 10018
			// (set) Token: 0x0600454E RID: 17742 RVA: 0x000717E7 File Offset: 0x0006F9E7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002723 RID: 10019
			// (set) Token: 0x0600454F RID: 17743 RVA: 0x000717FA File Offset: 0x0006F9FA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002724 RID: 10020
			// (set) Token: 0x06004550 RID: 17744 RVA: 0x00071812 File Offset: 0x0006FA12
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002725 RID: 10021
			// (set) Token: 0x06004551 RID: 17745 RVA: 0x0007182A File Offset: 0x0006FA2A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002726 RID: 10022
			// (set) Token: 0x06004552 RID: 17746 RVA: 0x00071842 File Offset: 0x0006FA42
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002727 RID: 10023
			// (set) Token: 0x06004553 RID: 17747 RVA: 0x0007185A File Offset: 0x0006FA5A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002728 RID: 10024
			// (set) Token: 0x06004554 RID: 17748 RVA: 0x00071872 File Offset: 0x0006FA72
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
