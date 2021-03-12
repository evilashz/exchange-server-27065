using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E2D RID: 3629
	public class UpdateSafeListCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x0600D7FA RID: 55290 RVA: 0x00132BB5 File Offset: 0x00130DB5
		private UpdateSafeListCommand() : base("Update-SafeList")
		{
		}

		// Token: 0x0600D7FB RID: 55291 RVA: 0x00132BC2 File Offset: 0x00130DC2
		public UpdateSafeListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D7FC RID: 55292 RVA: 0x00132BD1 File Offset: 0x00130DD1
		public virtual UpdateSafeListCommand SetParameters(UpdateSafeListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D7FD RID: 55293 RVA: 0x00132BDB File Offset: 0x00130DDB
		public virtual UpdateSafeListCommand SetParameters(UpdateSafeListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E2E RID: 3630
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A759 RID: 42841
			// (set) Token: 0x0600D7FE RID: 55294 RVA: 0x00132BE5 File Offset: 0x00130DE5
			public virtual UpdateType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x1700A75A RID: 42842
			// (set) Token: 0x0600D7FF RID: 55295 RVA: 0x00132BFD File Offset: 0x00130DFD
			public virtual SwitchParameter IncludeDomains
			{
				set
				{
					base.PowerSharpParameters["IncludeDomains"] = value;
				}
			}

			// Token: 0x1700A75B RID: 42843
			// (set) Token: 0x0600D800 RID: 55296 RVA: 0x00132C15 File Offset: 0x00130E15
			public virtual SwitchParameter EnsureJunkEmailRule
			{
				set
				{
					base.PowerSharpParameters["EnsureJunkEmailRule"] = value;
				}
			}

			// Token: 0x1700A75C RID: 42844
			// (set) Token: 0x0600D801 RID: 55297 RVA: 0x00132C2D File Offset: 0x00130E2D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A75D RID: 42845
			// (set) Token: 0x0600D802 RID: 55298 RVA: 0x00132C40 File Offset: 0x00130E40
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A75E RID: 42846
			// (set) Token: 0x0600D803 RID: 55299 RVA: 0x00132C58 File Offset: 0x00130E58
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A75F RID: 42847
			// (set) Token: 0x0600D804 RID: 55300 RVA: 0x00132C70 File Offset: 0x00130E70
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A760 RID: 42848
			// (set) Token: 0x0600D805 RID: 55301 RVA: 0x00132C88 File Offset: 0x00130E88
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A761 RID: 42849
			// (set) Token: 0x0600D806 RID: 55302 RVA: 0x00132CA0 File Offset: 0x00130EA0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E2F RID: 3631
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A762 RID: 42850
			// (set) Token: 0x0600D808 RID: 55304 RVA: 0x00132CC0 File Offset: 0x00130EC0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A763 RID: 42851
			// (set) Token: 0x0600D809 RID: 55305 RVA: 0x00132CDE File Offset: 0x00130EDE
			public virtual UpdateType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x1700A764 RID: 42852
			// (set) Token: 0x0600D80A RID: 55306 RVA: 0x00132CF6 File Offset: 0x00130EF6
			public virtual SwitchParameter IncludeDomains
			{
				set
				{
					base.PowerSharpParameters["IncludeDomains"] = value;
				}
			}

			// Token: 0x1700A765 RID: 42853
			// (set) Token: 0x0600D80B RID: 55307 RVA: 0x00132D0E File Offset: 0x00130F0E
			public virtual SwitchParameter EnsureJunkEmailRule
			{
				set
				{
					base.PowerSharpParameters["EnsureJunkEmailRule"] = value;
				}
			}

			// Token: 0x1700A766 RID: 42854
			// (set) Token: 0x0600D80C RID: 55308 RVA: 0x00132D26 File Offset: 0x00130F26
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A767 RID: 42855
			// (set) Token: 0x0600D80D RID: 55309 RVA: 0x00132D39 File Offset: 0x00130F39
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A768 RID: 42856
			// (set) Token: 0x0600D80E RID: 55310 RVA: 0x00132D51 File Offset: 0x00130F51
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A769 RID: 42857
			// (set) Token: 0x0600D80F RID: 55311 RVA: 0x00132D69 File Offset: 0x00130F69
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A76A RID: 42858
			// (set) Token: 0x0600D810 RID: 55312 RVA: 0x00132D81 File Offset: 0x00130F81
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A76B RID: 42859
			// (set) Token: 0x0600D811 RID: 55313 RVA: 0x00132D99 File Offset: 0x00130F99
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
