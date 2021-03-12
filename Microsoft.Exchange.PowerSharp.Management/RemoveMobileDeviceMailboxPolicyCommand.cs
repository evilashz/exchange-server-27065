using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001B5 RID: 437
	public class RemoveMobileDeviceMailboxPolicyCommand : SyntheticCommandWithPipelineInput<MobileMailboxPolicy, MobileMailboxPolicy>
	{
		// Token: 0x0600253D RID: 9533 RVA: 0x00047EEE File Offset: 0x000460EE
		private RemoveMobileDeviceMailboxPolicyCommand() : base("Remove-MobileDeviceMailboxPolicy")
		{
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x00047EFB File Offset: 0x000460FB
		public RemoveMobileDeviceMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00047F0A File Offset: 0x0004610A
		public virtual RemoveMobileDeviceMailboxPolicyCommand SetParameters(RemoveMobileDeviceMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x00047F14 File Offset: 0x00046114
		public virtual RemoveMobileDeviceMailboxPolicyCommand SetParameters(RemoveMobileDeviceMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001B6 RID: 438
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000D8C RID: 3468
			// (set) Token: 0x06002541 RID: 9537 RVA: 0x00047F1E File Offset: 0x0004611E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000D8D RID: 3469
			// (set) Token: 0x06002542 RID: 9538 RVA: 0x00047F36 File Offset: 0x00046136
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D8E RID: 3470
			// (set) Token: 0x06002543 RID: 9539 RVA: 0x00047F49 File Offset: 0x00046149
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D8F RID: 3471
			// (set) Token: 0x06002544 RID: 9540 RVA: 0x00047F61 File Offset: 0x00046161
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D90 RID: 3472
			// (set) Token: 0x06002545 RID: 9541 RVA: 0x00047F79 File Offset: 0x00046179
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D91 RID: 3473
			// (set) Token: 0x06002546 RID: 9542 RVA: 0x00047F91 File Offset: 0x00046191
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000D92 RID: 3474
			// (set) Token: 0x06002547 RID: 9543 RVA: 0x00047FA9 File Offset: 0x000461A9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000D93 RID: 3475
			// (set) Token: 0x06002548 RID: 9544 RVA: 0x00047FC1 File Offset: 0x000461C1
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020001B7 RID: 439
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000D94 RID: 3476
			// (set) Token: 0x0600254A RID: 9546 RVA: 0x00047FE1 File Offset: 0x000461E1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000D95 RID: 3477
			// (set) Token: 0x0600254B RID: 9547 RVA: 0x00047FFF File Offset: 0x000461FF
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000D96 RID: 3478
			// (set) Token: 0x0600254C RID: 9548 RVA: 0x00048017 File Offset: 0x00046217
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D97 RID: 3479
			// (set) Token: 0x0600254D RID: 9549 RVA: 0x0004802A File Offset: 0x0004622A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D98 RID: 3480
			// (set) Token: 0x0600254E RID: 9550 RVA: 0x00048042 File Offset: 0x00046242
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D99 RID: 3481
			// (set) Token: 0x0600254F RID: 9551 RVA: 0x0004805A File Offset: 0x0004625A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D9A RID: 3482
			// (set) Token: 0x06002550 RID: 9552 RVA: 0x00048072 File Offset: 0x00046272
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000D9B RID: 3483
			// (set) Token: 0x06002551 RID: 9553 RVA: 0x0004808A File Offset: 0x0004628A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000D9C RID: 3484
			// (set) Token: 0x06002552 RID: 9554 RVA: 0x000480A2 File Offset: 0x000462A2
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
