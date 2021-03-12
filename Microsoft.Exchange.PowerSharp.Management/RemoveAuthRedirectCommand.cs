using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004DD RID: 1245
	public class RemoveAuthRedirectCommand : SyntheticCommandWithPipelineInput<AuthRedirect, AuthRedirect>
	{
		// Token: 0x060044C1 RID: 17601 RVA: 0x00070D16 File Offset: 0x0006EF16
		private RemoveAuthRedirectCommand() : base("Remove-AuthRedirect")
		{
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x00070D23 File Offset: 0x0006EF23
		public RemoveAuthRedirectCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x00070D32 File Offset: 0x0006EF32
		public virtual RemoveAuthRedirectCommand SetParameters(RemoveAuthRedirectCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x00070D3C File Offset: 0x0006EF3C
		public virtual RemoveAuthRedirectCommand SetParameters(RemoveAuthRedirectCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004DE RID: 1246
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170026C0 RID: 9920
			// (set) Token: 0x060044C5 RID: 17605 RVA: 0x00070D46 File Offset: 0x0006EF46
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026C1 RID: 9921
			// (set) Token: 0x060044C6 RID: 17606 RVA: 0x00070D59 File Offset: 0x0006EF59
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026C2 RID: 9922
			// (set) Token: 0x060044C7 RID: 17607 RVA: 0x00070D71 File Offset: 0x0006EF71
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026C3 RID: 9923
			// (set) Token: 0x060044C8 RID: 17608 RVA: 0x00070D89 File Offset: 0x0006EF89
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026C4 RID: 9924
			// (set) Token: 0x060044C9 RID: 17609 RVA: 0x00070DA1 File Offset: 0x0006EFA1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170026C5 RID: 9925
			// (set) Token: 0x060044CA RID: 17610 RVA: 0x00070DB9 File Offset: 0x0006EFB9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170026C6 RID: 9926
			// (set) Token: 0x060044CB RID: 17611 RVA: 0x00070DD1 File Offset: 0x0006EFD1
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020004DF RID: 1247
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170026C7 RID: 9927
			// (set) Token: 0x060044CD RID: 17613 RVA: 0x00070DF1 File Offset: 0x0006EFF1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AuthRedirectIdParameter(value) : null);
				}
			}

			// Token: 0x170026C8 RID: 9928
			// (set) Token: 0x060044CE RID: 17614 RVA: 0x00070E0F File Offset: 0x0006F00F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026C9 RID: 9929
			// (set) Token: 0x060044CF RID: 17615 RVA: 0x00070E22 File Offset: 0x0006F022
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026CA RID: 9930
			// (set) Token: 0x060044D0 RID: 17616 RVA: 0x00070E3A File Offset: 0x0006F03A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026CB RID: 9931
			// (set) Token: 0x060044D1 RID: 17617 RVA: 0x00070E52 File Offset: 0x0006F052
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026CC RID: 9932
			// (set) Token: 0x060044D2 RID: 17618 RVA: 0x00070E6A File Offset: 0x0006F06A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170026CD RID: 9933
			// (set) Token: 0x060044D3 RID: 17619 RVA: 0x00070E82 File Offset: 0x0006F082
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170026CE RID: 9934
			// (set) Token: 0x060044D4 RID: 17620 RVA: 0x00070E9A File Offset: 0x0006F09A
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
