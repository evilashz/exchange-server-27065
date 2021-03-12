using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B2A RID: 2858
	public class EnableUMAutoAttendantCommand : SyntheticCommandWithPipelineInput<UMAutoAttendant, UMAutoAttendant>
	{
		// Token: 0x06008BC5 RID: 35781 RVA: 0x000CD2E2 File Offset: 0x000CB4E2
		private EnableUMAutoAttendantCommand() : base("Enable-UMAutoAttendant")
		{
		}

		// Token: 0x06008BC6 RID: 35782 RVA: 0x000CD2EF File Offset: 0x000CB4EF
		public EnableUMAutoAttendantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008BC7 RID: 35783 RVA: 0x000CD2FE File Offset: 0x000CB4FE
		public virtual EnableUMAutoAttendantCommand SetParameters(EnableUMAutoAttendantCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008BC8 RID: 35784 RVA: 0x000CD308 File Offset: 0x000CB508
		public virtual EnableUMAutoAttendantCommand SetParameters(EnableUMAutoAttendantCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B2B RID: 2859
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700612A RID: 24874
			// (set) Token: 0x06008BC9 RID: 35785 RVA: 0x000CD312 File Offset: 0x000CB512
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700612B RID: 24875
			// (set) Token: 0x06008BCA RID: 35786 RVA: 0x000CD325 File Offset: 0x000CB525
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700612C RID: 24876
			// (set) Token: 0x06008BCB RID: 35787 RVA: 0x000CD33D File Offset: 0x000CB53D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700612D RID: 24877
			// (set) Token: 0x06008BCC RID: 35788 RVA: 0x000CD355 File Offset: 0x000CB555
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700612E RID: 24878
			// (set) Token: 0x06008BCD RID: 35789 RVA: 0x000CD36D File Offset: 0x000CB56D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700612F RID: 24879
			// (set) Token: 0x06008BCE RID: 35790 RVA: 0x000CD385 File Offset: 0x000CB585
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B2C RID: 2860
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006130 RID: 24880
			// (set) Token: 0x06008BD0 RID: 35792 RVA: 0x000CD3A5 File Offset: 0x000CB5A5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x17006131 RID: 24881
			// (set) Token: 0x06008BD1 RID: 35793 RVA: 0x000CD3C3 File Offset: 0x000CB5C3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006132 RID: 24882
			// (set) Token: 0x06008BD2 RID: 35794 RVA: 0x000CD3D6 File Offset: 0x000CB5D6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006133 RID: 24883
			// (set) Token: 0x06008BD3 RID: 35795 RVA: 0x000CD3EE File Offset: 0x000CB5EE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006134 RID: 24884
			// (set) Token: 0x06008BD4 RID: 35796 RVA: 0x000CD406 File Offset: 0x000CB606
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006135 RID: 24885
			// (set) Token: 0x06008BD5 RID: 35797 RVA: 0x000CD41E File Offset: 0x000CB61E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006136 RID: 24886
			// (set) Token: 0x06008BD6 RID: 35798 RVA: 0x000CD436 File Offset: 0x000CB636
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
