using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000558 RID: 1368
	public class RemoveDatabaseAvailabilityGroupCommand : SyntheticCommandWithPipelineInput<DatabaseAvailabilityGroup, DatabaseAvailabilityGroup>
	{
		// Token: 0x06004881 RID: 18561 RVA: 0x00075767 File Offset: 0x00073967
		private RemoveDatabaseAvailabilityGroupCommand() : base("Remove-DatabaseAvailabilityGroup")
		{
		}

		// Token: 0x06004882 RID: 18562 RVA: 0x00075774 File Offset: 0x00073974
		public RemoveDatabaseAvailabilityGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004883 RID: 18563 RVA: 0x00075783 File Offset: 0x00073983
		public virtual RemoveDatabaseAvailabilityGroupCommand SetParameters(RemoveDatabaseAvailabilityGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004884 RID: 18564 RVA: 0x0007578D File Offset: 0x0007398D
		public virtual RemoveDatabaseAvailabilityGroupCommand SetParameters(RemoveDatabaseAvailabilityGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000559 RID: 1369
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700298A RID: 10634
			// (set) Token: 0x06004885 RID: 18565 RVA: 0x00075797 File Offset: 0x00073997
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700298B RID: 10635
			// (set) Token: 0x06004886 RID: 18566 RVA: 0x000757AA File Offset: 0x000739AA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700298C RID: 10636
			// (set) Token: 0x06004887 RID: 18567 RVA: 0x000757C2 File Offset: 0x000739C2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700298D RID: 10637
			// (set) Token: 0x06004888 RID: 18568 RVA: 0x000757DA File Offset: 0x000739DA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700298E RID: 10638
			// (set) Token: 0x06004889 RID: 18569 RVA: 0x000757F2 File Offset: 0x000739F2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700298F RID: 10639
			// (set) Token: 0x0600488A RID: 18570 RVA: 0x0007580A File Offset: 0x00073A0A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002990 RID: 10640
			// (set) Token: 0x0600488B RID: 18571 RVA: 0x00075822 File Offset: 0x00073A22
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200055A RID: 1370
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002991 RID: 10641
			// (set) Token: 0x0600488D RID: 18573 RVA: 0x00075842 File Offset: 0x00073A42
			public virtual DatabaseAvailabilityGroupIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002992 RID: 10642
			// (set) Token: 0x0600488E RID: 18574 RVA: 0x00075855 File Offset: 0x00073A55
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002993 RID: 10643
			// (set) Token: 0x0600488F RID: 18575 RVA: 0x00075868 File Offset: 0x00073A68
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002994 RID: 10644
			// (set) Token: 0x06004890 RID: 18576 RVA: 0x00075880 File Offset: 0x00073A80
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002995 RID: 10645
			// (set) Token: 0x06004891 RID: 18577 RVA: 0x00075898 File Offset: 0x00073A98
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002996 RID: 10646
			// (set) Token: 0x06004892 RID: 18578 RVA: 0x000758B0 File Offset: 0x00073AB0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002997 RID: 10647
			// (set) Token: 0x06004893 RID: 18579 RVA: 0x000758C8 File Offset: 0x00073AC8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002998 RID: 10648
			// (set) Token: 0x06004894 RID: 18580 RVA: 0x000758E0 File Offset: 0x00073AE0
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
