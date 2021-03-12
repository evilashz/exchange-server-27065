using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007B3 RID: 1971
	public class RemoveAcceptedDomainCommand : SyntheticCommandWithPipelineInput<AcceptedDomain, AcceptedDomain>
	{
		// Token: 0x060062CC RID: 25292 RVA: 0x00097A5E File Offset: 0x00095C5E
		private RemoveAcceptedDomainCommand() : base("Remove-AcceptedDomain")
		{
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x00097A6B File Offset: 0x00095C6B
		public RemoveAcceptedDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x00097A7A File Offset: 0x00095C7A
		public virtual RemoveAcceptedDomainCommand SetParameters(RemoveAcceptedDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x00097A84 File Offset: 0x00095C84
		public virtual RemoveAcceptedDomainCommand SetParameters(RemoveAcceptedDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007B4 RID: 1972
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003F1F RID: 16159
			// (set) Token: 0x060062D0 RID: 25296 RVA: 0x00097A8E File Offset: 0x00095C8E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F20 RID: 16160
			// (set) Token: 0x060062D1 RID: 25297 RVA: 0x00097AA1 File Offset: 0x00095CA1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F21 RID: 16161
			// (set) Token: 0x060062D2 RID: 25298 RVA: 0x00097AB9 File Offset: 0x00095CB9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F22 RID: 16162
			// (set) Token: 0x060062D3 RID: 25299 RVA: 0x00097AD1 File Offset: 0x00095CD1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F23 RID: 16163
			// (set) Token: 0x060062D4 RID: 25300 RVA: 0x00097AE9 File Offset: 0x00095CE9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F24 RID: 16164
			// (set) Token: 0x060062D5 RID: 25301 RVA: 0x00097B01 File Offset: 0x00095D01
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003F25 RID: 16165
			// (set) Token: 0x060062D6 RID: 25302 RVA: 0x00097B19 File Offset: 0x00095D19
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020007B5 RID: 1973
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003F26 RID: 16166
			// (set) Token: 0x060062D8 RID: 25304 RVA: 0x00097B39 File Offset: 0x00095D39
			public virtual AcceptedDomainIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003F27 RID: 16167
			// (set) Token: 0x060062D9 RID: 25305 RVA: 0x00097B4C File Offset: 0x00095D4C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F28 RID: 16168
			// (set) Token: 0x060062DA RID: 25306 RVA: 0x00097B5F File Offset: 0x00095D5F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F29 RID: 16169
			// (set) Token: 0x060062DB RID: 25307 RVA: 0x00097B77 File Offset: 0x00095D77
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F2A RID: 16170
			// (set) Token: 0x060062DC RID: 25308 RVA: 0x00097B8F File Offset: 0x00095D8F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F2B RID: 16171
			// (set) Token: 0x060062DD RID: 25309 RVA: 0x00097BA7 File Offset: 0x00095DA7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F2C RID: 16172
			// (set) Token: 0x060062DE RID: 25310 RVA: 0x00097BBF File Offset: 0x00095DBF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003F2D RID: 16173
			// (set) Token: 0x060062DF RID: 25311 RVA: 0x00097BD7 File Offset: 0x00095DD7
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
