using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000452 RID: 1106
	public class EnableMailboxQuarantineCommand : SyntheticCommandWithPipelineInputNoOutput<GeneralMailboxIdParameter>
	{
		// Token: 0x06003FD9 RID: 16345 RVA: 0x0006AA37 File Offset: 0x00068C37
		private EnableMailboxQuarantineCommand() : base("Enable-MailboxQuarantine")
		{
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x0006AA44 File Offset: 0x00068C44
		public EnableMailboxQuarantineCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x0006AA53 File Offset: 0x00068C53
		public virtual EnableMailboxQuarantineCommand SetParameters(EnableMailboxQuarantineCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x0006AA5D File Offset: 0x00068C5D
		public virtual EnableMailboxQuarantineCommand SetParameters(EnableMailboxQuarantineCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000453 RID: 1107
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170022EE RID: 8942
			// (set) Token: 0x06003FDD RID: 16349 RVA: 0x0006AA67 File Offset: 0x00068C67
			public virtual EnhancedTimeSpan? Duration
			{
				set
				{
					base.PowerSharpParameters["Duration"] = value;
				}
			}

			// Token: 0x170022EF RID: 8943
			// (set) Token: 0x06003FDE RID: 16350 RVA: 0x0006AA7F File Offset: 0x00068C7F
			public virtual SwitchParameter AllowMigration
			{
				set
				{
					base.PowerSharpParameters["AllowMigration"] = value;
				}
			}

			// Token: 0x170022F0 RID: 8944
			// (set) Token: 0x06003FDF RID: 16351 RVA: 0x0006AA97 File Offset: 0x00068C97
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170022F1 RID: 8945
			// (set) Token: 0x06003FE0 RID: 16352 RVA: 0x0006AAAF File Offset: 0x00068CAF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170022F2 RID: 8946
			// (set) Token: 0x06003FE1 RID: 16353 RVA: 0x0006AAC7 File Offset: 0x00068CC7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170022F3 RID: 8947
			// (set) Token: 0x06003FE2 RID: 16354 RVA: 0x0006AADF File Offset: 0x00068CDF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170022F4 RID: 8948
			// (set) Token: 0x06003FE3 RID: 16355 RVA: 0x0006AAF7 File Offset: 0x00068CF7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170022F5 RID: 8949
			// (set) Token: 0x06003FE4 RID: 16356 RVA: 0x0006AB0F File Offset: 0x00068D0F
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000454 RID: 1108
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170022F6 RID: 8950
			// (set) Token: 0x06003FE6 RID: 16358 RVA: 0x0006AB2F File Offset: 0x00068D2F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new GeneralMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170022F7 RID: 8951
			// (set) Token: 0x06003FE7 RID: 16359 RVA: 0x0006AB4D File Offset: 0x00068D4D
			public virtual EnhancedTimeSpan? Duration
			{
				set
				{
					base.PowerSharpParameters["Duration"] = value;
				}
			}

			// Token: 0x170022F8 RID: 8952
			// (set) Token: 0x06003FE8 RID: 16360 RVA: 0x0006AB65 File Offset: 0x00068D65
			public virtual SwitchParameter AllowMigration
			{
				set
				{
					base.PowerSharpParameters["AllowMigration"] = value;
				}
			}

			// Token: 0x170022F9 RID: 8953
			// (set) Token: 0x06003FE9 RID: 16361 RVA: 0x0006AB7D File Offset: 0x00068D7D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170022FA RID: 8954
			// (set) Token: 0x06003FEA RID: 16362 RVA: 0x0006AB95 File Offset: 0x00068D95
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170022FB RID: 8955
			// (set) Token: 0x06003FEB RID: 16363 RVA: 0x0006ABAD File Offset: 0x00068DAD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170022FC RID: 8956
			// (set) Token: 0x06003FEC RID: 16364 RVA: 0x0006ABC5 File Offset: 0x00068DC5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170022FD RID: 8957
			// (set) Token: 0x06003FED RID: 16365 RVA: 0x0006ABDD File Offset: 0x00068DDD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170022FE RID: 8958
			// (set) Token: 0x06003FEE RID: 16366 RVA: 0x0006ABF5 File Offset: 0x00068DF5
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
