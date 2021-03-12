using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009FA RID: 2554
	public class SuspendMergeRequestCommand : SyntheticCommandWithPipelineInput<MergeRequestIdParameter, MergeRequestIdParameter>
	{
		// Token: 0x06008025 RID: 32805 RVA: 0x000BE2B4 File Offset: 0x000BC4B4
		private SuspendMergeRequestCommand() : base("Suspend-MergeRequest")
		{
		}

		// Token: 0x06008026 RID: 32806 RVA: 0x000BE2C1 File Offset: 0x000BC4C1
		public SuspendMergeRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008027 RID: 32807 RVA: 0x000BE2D0 File Offset: 0x000BC4D0
		public virtual SuspendMergeRequestCommand SetParameters(SuspendMergeRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008028 RID: 32808 RVA: 0x000BE2DA File Offset: 0x000BC4DA
		public virtual SuspendMergeRequestCommand SetParameters(SuspendMergeRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009FB RID: 2555
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170057EA RID: 22506
			// (set) Token: 0x06008029 RID: 32809 RVA: 0x000BE2E4 File Offset: 0x000BC4E4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MergeRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170057EB RID: 22507
			// (set) Token: 0x0600802A RID: 32810 RVA: 0x000BE302 File Offset: 0x000BC502
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x170057EC RID: 22508
			// (set) Token: 0x0600802B RID: 32811 RVA: 0x000BE315 File Offset: 0x000BC515
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170057ED RID: 22509
			// (set) Token: 0x0600802C RID: 32812 RVA: 0x000BE328 File Offset: 0x000BC528
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170057EE RID: 22510
			// (set) Token: 0x0600802D RID: 32813 RVA: 0x000BE340 File Offset: 0x000BC540
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170057EF RID: 22511
			// (set) Token: 0x0600802E RID: 32814 RVA: 0x000BE358 File Offset: 0x000BC558
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170057F0 RID: 22512
			// (set) Token: 0x0600802F RID: 32815 RVA: 0x000BE370 File Offset: 0x000BC570
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170057F1 RID: 22513
			// (set) Token: 0x06008030 RID: 32816 RVA: 0x000BE388 File Offset: 0x000BC588
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170057F2 RID: 22514
			// (set) Token: 0x06008031 RID: 32817 RVA: 0x000BE3A0 File Offset: 0x000BC5A0
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020009FC RID: 2556
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170057F3 RID: 22515
			// (set) Token: 0x06008033 RID: 32819 RVA: 0x000BE3C0 File Offset: 0x000BC5C0
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x170057F4 RID: 22516
			// (set) Token: 0x06008034 RID: 32820 RVA: 0x000BE3D3 File Offset: 0x000BC5D3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170057F5 RID: 22517
			// (set) Token: 0x06008035 RID: 32821 RVA: 0x000BE3E6 File Offset: 0x000BC5E6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170057F6 RID: 22518
			// (set) Token: 0x06008036 RID: 32822 RVA: 0x000BE3FE File Offset: 0x000BC5FE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170057F7 RID: 22519
			// (set) Token: 0x06008037 RID: 32823 RVA: 0x000BE416 File Offset: 0x000BC616
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170057F8 RID: 22520
			// (set) Token: 0x06008038 RID: 32824 RVA: 0x000BE42E File Offset: 0x000BC62E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170057F9 RID: 22521
			// (set) Token: 0x06008039 RID: 32825 RVA: 0x000BE446 File Offset: 0x000BC646
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170057FA RID: 22522
			// (set) Token: 0x0600803A RID: 32826 RVA: 0x000BE45E File Offset: 0x000BC65E
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
