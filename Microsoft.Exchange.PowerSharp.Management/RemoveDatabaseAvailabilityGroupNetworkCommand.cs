using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200055E RID: 1374
	public class RemoveDatabaseAvailabilityGroupNetworkCommand : SyntheticCommandWithPipelineInput<DatabaseAvailabilityGroupNetwork, DatabaseAvailabilityGroupNetwork>
	{
		// Token: 0x060048AB RID: 18603 RVA: 0x00075A99 File Offset: 0x00073C99
		private RemoveDatabaseAvailabilityGroupNetworkCommand() : base("Remove-DatabaseAvailabilityGroupNetwork")
		{
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x00075AA6 File Offset: 0x00073CA6
		public RemoveDatabaseAvailabilityGroupNetworkCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x00075AB5 File Offset: 0x00073CB5
		public virtual RemoveDatabaseAvailabilityGroupNetworkCommand SetParameters(RemoveDatabaseAvailabilityGroupNetworkCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x00075ABF File Offset: 0x00073CBF
		public virtual RemoveDatabaseAvailabilityGroupNetworkCommand SetParameters(RemoveDatabaseAvailabilityGroupNetworkCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200055F RID: 1375
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170029A8 RID: 10664
			// (set) Token: 0x060048AF RID: 18607 RVA: 0x00075AC9 File Offset: 0x00073CC9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170029A9 RID: 10665
			// (set) Token: 0x060048B0 RID: 18608 RVA: 0x00075ADC File Offset: 0x00073CDC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170029AA RID: 10666
			// (set) Token: 0x060048B1 RID: 18609 RVA: 0x00075AF4 File Offset: 0x00073CF4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170029AB RID: 10667
			// (set) Token: 0x060048B2 RID: 18610 RVA: 0x00075B0C File Offset: 0x00073D0C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170029AC RID: 10668
			// (set) Token: 0x060048B3 RID: 18611 RVA: 0x00075B24 File Offset: 0x00073D24
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170029AD RID: 10669
			// (set) Token: 0x060048B4 RID: 18612 RVA: 0x00075B3C File Offset: 0x00073D3C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170029AE RID: 10670
			// (set) Token: 0x060048B5 RID: 18613 RVA: 0x00075B54 File Offset: 0x00073D54
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000560 RID: 1376
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170029AF RID: 10671
			// (set) Token: 0x060048B7 RID: 18615 RVA: 0x00075B74 File Offset: 0x00073D74
			public virtual DatabaseAvailabilityGroupNetworkIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170029B0 RID: 10672
			// (set) Token: 0x060048B8 RID: 18616 RVA: 0x00075B87 File Offset: 0x00073D87
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170029B1 RID: 10673
			// (set) Token: 0x060048B9 RID: 18617 RVA: 0x00075B9A File Offset: 0x00073D9A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170029B2 RID: 10674
			// (set) Token: 0x060048BA RID: 18618 RVA: 0x00075BB2 File Offset: 0x00073DB2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170029B3 RID: 10675
			// (set) Token: 0x060048BB RID: 18619 RVA: 0x00075BCA File Offset: 0x00073DCA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170029B4 RID: 10676
			// (set) Token: 0x060048BC RID: 18620 RVA: 0x00075BE2 File Offset: 0x00073DE2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170029B5 RID: 10677
			// (set) Token: 0x060048BD RID: 18621 RVA: 0x00075BFA File Offset: 0x00073DFA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170029B6 RID: 10678
			// (set) Token: 0x060048BE RID: 18622 RVA: 0x00075C12 File Offset: 0x00073E12
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
