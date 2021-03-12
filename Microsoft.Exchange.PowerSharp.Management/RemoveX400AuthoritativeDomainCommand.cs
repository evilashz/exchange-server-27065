using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007B6 RID: 1974
	public class RemoveX400AuthoritativeDomainCommand : SyntheticCommandWithPipelineInput<X400AuthoritativeDomain, X400AuthoritativeDomain>
	{
		// Token: 0x060062E1 RID: 25313 RVA: 0x00097BF7 File Offset: 0x00095DF7
		private RemoveX400AuthoritativeDomainCommand() : base("Remove-X400AuthoritativeDomain")
		{
		}

		// Token: 0x060062E2 RID: 25314 RVA: 0x00097C04 File Offset: 0x00095E04
		public RemoveX400AuthoritativeDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060062E3 RID: 25315 RVA: 0x00097C13 File Offset: 0x00095E13
		public virtual RemoveX400AuthoritativeDomainCommand SetParameters(RemoveX400AuthoritativeDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060062E4 RID: 25316 RVA: 0x00097C1D File Offset: 0x00095E1D
		public virtual RemoveX400AuthoritativeDomainCommand SetParameters(RemoveX400AuthoritativeDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007B7 RID: 1975
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003F2E RID: 16174
			// (set) Token: 0x060062E5 RID: 25317 RVA: 0x00097C27 File Offset: 0x00095E27
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F2F RID: 16175
			// (set) Token: 0x060062E6 RID: 25318 RVA: 0x00097C3A File Offset: 0x00095E3A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F30 RID: 16176
			// (set) Token: 0x060062E7 RID: 25319 RVA: 0x00097C52 File Offset: 0x00095E52
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F31 RID: 16177
			// (set) Token: 0x060062E8 RID: 25320 RVA: 0x00097C6A File Offset: 0x00095E6A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F32 RID: 16178
			// (set) Token: 0x060062E9 RID: 25321 RVA: 0x00097C82 File Offset: 0x00095E82
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F33 RID: 16179
			// (set) Token: 0x060062EA RID: 25322 RVA: 0x00097C9A File Offset: 0x00095E9A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003F34 RID: 16180
			// (set) Token: 0x060062EB RID: 25323 RVA: 0x00097CB2 File Offset: 0x00095EB2
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020007B8 RID: 1976
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003F35 RID: 16181
			// (set) Token: 0x060062ED RID: 25325 RVA: 0x00097CD2 File Offset: 0x00095ED2
			public virtual X400AuthoritativeDomainIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003F36 RID: 16182
			// (set) Token: 0x060062EE RID: 25326 RVA: 0x00097CE5 File Offset: 0x00095EE5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F37 RID: 16183
			// (set) Token: 0x060062EF RID: 25327 RVA: 0x00097CF8 File Offset: 0x00095EF8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F38 RID: 16184
			// (set) Token: 0x060062F0 RID: 25328 RVA: 0x00097D10 File Offset: 0x00095F10
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F39 RID: 16185
			// (set) Token: 0x060062F1 RID: 25329 RVA: 0x00097D28 File Offset: 0x00095F28
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F3A RID: 16186
			// (set) Token: 0x060062F2 RID: 25330 RVA: 0x00097D40 File Offset: 0x00095F40
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F3B RID: 16187
			// (set) Token: 0x060062F3 RID: 25331 RVA: 0x00097D58 File Offset: 0x00095F58
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003F3C RID: 16188
			// (set) Token: 0x060062F4 RID: 25332 RVA: 0x00097D70 File Offset: 0x00095F70
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
