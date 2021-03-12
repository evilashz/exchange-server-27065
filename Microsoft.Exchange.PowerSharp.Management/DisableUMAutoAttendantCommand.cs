using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B1B RID: 2843
	public class DisableUMAutoAttendantCommand : SyntheticCommandWithPipelineInput<UMAutoAttendant, UMAutoAttendant>
	{
		// Token: 0x06008B3C RID: 35644 RVA: 0x000CC805 File Offset: 0x000CAA05
		private DisableUMAutoAttendantCommand() : base("Disable-UMAutoAttendant")
		{
		}

		// Token: 0x06008B3D RID: 35645 RVA: 0x000CC812 File Offset: 0x000CAA12
		public DisableUMAutoAttendantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008B3E RID: 35646 RVA: 0x000CC821 File Offset: 0x000CAA21
		public virtual DisableUMAutoAttendantCommand SetParameters(DisableUMAutoAttendantCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008B3F RID: 35647 RVA: 0x000CC82B File Offset: 0x000CAA2B
		public virtual DisableUMAutoAttendantCommand SetParameters(DisableUMAutoAttendantCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B1C RID: 2844
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170060BF RID: 24767
			// (set) Token: 0x06008B40 RID: 35648 RVA: 0x000CC835 File Offset: 0x000CAA35
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170060C0 RID: 24768
			// (set) Token: 0x06008B41 RID: 35649 RVA: 0x000CC848 File Offset: 0x000CAA48
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170060C1 RID: 24769
			// (set) Token: 0x06008B42 RID: 35650 RVA: 0x000CC860 File Offset: 0x000CAA60
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170060C2 RID: 24770
			// (set) Token: 0x06008B43 RID: 35651 RVA: 0x000CC878 File Offset: 0x000CAA78
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170060C3 RID: 24771
			// (set) Token: 0x06008B44 RID: 35652 RVA: 0x000CC890 File Offset: 0x000CAA90
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170060C4 RID: 24772
			// (set) Token: 0x06008B45 RID: 35653 RVA: 0x000CC8A8 File Offset: 0x000CAAA8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170060C5 RID: 24773
			// (set) Token: 0x06008B46 RID: 35654 RVA: 0x000CC8C0 File Offset: 0x000CAAC0
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B1D RID: 2845
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170060C6 RID: 24774
			// (set) Token: 0x06008B48 RID: 35656 RVA: 0x000CC8E0 File Offset: 0x000CAAE0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x170060C7 RID: 24775
			// (set) Token: 0x06008B49 RID: 35657 RVA: 0x000CC8FE File Offset: 0x000CAAFE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170060C8 RID: 24776
			// (set) Token: 0x06008B4A RID: 35658 RVA: 0x000CC911 File Offset: 0x000CAB11
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170060C9 RID: 24777
			// (set) Token: 0x06008B4B RID: 35659 RVA: 0x000CC929 File Offset: 0x000CAB29
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170060CA RID: 24778
			// (set) Token: 0x06008B4C RID: 35660 RVA: 0x000CC941 File Offset: 0x000CAB41
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170060CB RID: 24779
			// (set) Token: 0x06008B4D RID: 35661 RVA: 0x000CC959 File Offset: 0x000CAB59
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170060CC RID: 24780
			// (set) Token: 0x06008B4E RID: 35662 RVA: 0x000CC971 File Offset: 0x000CAB71
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170060CD RID: 24781
			// (set) Token: 0x06008B4F RID: 35663 RVA: 0x000CC989 File Offset: 0x000CAB89
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
