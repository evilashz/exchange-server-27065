using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BDE RID: 3038
	public class GetConsumerMailboxCommand : SyntheticCommandWithPipelineInput<ConsumerMailbox, ConsumerMailbox>
	{
		// Token: 0x060092FB RID: 37627 RVA: 0x000D6909 File Offset: 0x000D4B09
		private GetConsumerMailboxCommand() : base("Get-ConsumerMailbox")
		{
		}

		// Token: 0x060092FC RID: 37628 RVA: 0x000D6916 File Offset: 0x000D4B16
		public GetConsumerMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060092FD RID: 37629 RVA: 0x000D6925 File Offset: 0x000D4B25
		public virtual GetConsumerMailboxCommand SetParameters(GetConsumerMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060092FE RID: 37630 RVA: 0x000D692F File Offset: 0x000D4B2F
		public virtual GetConsumerMailboxCommand SetParameters(GetConsumerMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BDF RID: 3039
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170066F8 RID: 26360
			// (set) Token: 0x060092FF RID: 37631 RVA: 0x000D6939 File Offset: 0x000D4B39
			public virtual SwitchParameter MservDataOnly
			{
				set
				{
					base.PowerSharpParameters["MservDataOnly"] = value;
				}
			}

			// Token: 0x170066F9 RID: 26361
			// (set) Token: 0x06009300 RID: 37632 RVA: 0x000D6951 File Offset: 0x000D4B51
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170066FA RID: 26362
			// (set) Token: 0x06009301 RID: 37633 RVA: 0x000D6969 File Offset: 0x000D4B69
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170066FB RID: 26363
			// (set) Token: 0x06009302 RID: 37634 RVA: 0x000D6981 File Offset: 0x000D4B81
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170066FC RID: 26364
			// (set) Token: 0x06009303 RID: 37635 RVA: 0x000D6999 File Offset: 0x000D4B99
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BE0 RID: 3040
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170066FD RID: 26365
			// (set) Token: 0x06009305 RID: 37637 RVA: 0x000D69B9 File Offset: 0x000D4BB9
			public virtual ConsumerMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170066FE RID: 26366
			// (set) Token: 0x06009306 RID: 37638 RVA: 0x000D69CC File Offset: 0x000D4BCC
			public virtual SwitchParameter MservDataOnly
			{
				set
				{
					base.PowerSharpParameters["MservDataOnly"] = value;
				}
			}

			// Token: 0x170066FF RID: 26367
			// (set) Token: 0x06009307 RID: 37639 RVA: 0x000D69E4 File Offset: 0x000D4BE4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006700 RID: 26368
			// (set) Token: 0x06009308 RID: 37640 RVA: 0x000D69FC File Offset: 0x000D4BFC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006701 RID: 26369
			// (set) Token: 0x06009309 RID: 37641 RVA: 0x000D6A14 File Offset: 0x000D4C14
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006702 RID: 26370
			// (set) Token: 0x0600930A RID: 37642 RVA: 0x000D6A2C File Offset: 0x000D4C2C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
