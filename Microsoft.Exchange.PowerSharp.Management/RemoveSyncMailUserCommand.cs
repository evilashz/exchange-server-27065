using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DD4 RID: 3540
	public class RemoveSyncMailUserCommand : SyntheticCommandWithPipelineInput<MailUserIdParameter, MailUserIdParameter>
	{
		// Token: 0x0600D312 RID: 54034 RVA: 0x0012C49E File Offset: 0x0012A69E
		private RemoveSyncMailUserCommand() : base("Remove-SyncMailUser")
		{
		}

		// Token: 0x0600D313 RID: 54035 RVA: 0x0012C4AB File Offset: 0x0012A6AB
		public RemoveSyncMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D314 RID: 54036 RVA: 0x0012C4BA File Offset: 0x0012A6BA
		public virtual RemoveSyncMailUserCommand SetParameters(RemoveSyncMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D315 RID: 54037 RVA: 0x0012C4C4 File Offset: 0x0012A6C4
		public virtual RemoveSyncMailUserCommand SetParameters(RemoveSyncMailUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DD5 RID: 3541
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A323 RID: 41763
			// (set) Token: 0x0600D316 RID: 54038 RVA: 0x0012C4CE File Offset: 0x0012A6CE
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x1700A324 RID: 41764
			// (set) Token: 0x0600D317 RID: 54039 RVA: 0x0012C4E6 File Offset: 0x0012A6E6
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x1700A325 RID: 41765
			// (set) Token: 0x0600D318 RID: 54040 RVA: 0x0012C4FE File Offset: 0x0012A6FE
			public virtual SwitchParameter KeepWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["KeepWindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A326 RID: 41766
			// (set) Token: 0x0600D319 RID: 54041 RVA: 0x0012C516 File Offset: 0x0012A716
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x1700A327 RID: 41767
			// (set) Token: 0x0600D31A RID: 54042 RVA: 0x0012C52E File Offset: 0x0012A72E
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A328 RID: 41768
			// (set) Token: 0x0600D31B RID: 54043 RVA: 0x0012C546 File Offset: 0x0012A746
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A329 RID: 41769
			// (set) Token: 0x0600D31C RID: 54044 RVA: 0x0012C559 File Offset: 0x0012A759
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A32A RID: 41770
			// (set) Token: 0x0600D31D RID: 54045 RVA: 0x0012C571 File Offset: 0x0012A771
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A32B RID: 41771
			// (set) Token: 0x0600D31E RID: 54046 RVA: 0x0012C589 File Offset: 0x0012A789
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A32C RID: 41772
			// (set) Token: 0x0600D31F RID: 54047 RVA: 0x0012C5A1 File Offset: 0x0012A7A1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A32D RID: 41773
			// (set) Token: 0x0600D320 RID: 54048 RVA: 0x0012C5B9 File Offset: 0x0012A7B9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A32E RID: 41774
			// (set) Token: 0x0600D321 RID: 54049 RVA: 0x0012C5D1 File Offset: 0x0012A7D1
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000DD6 RID: 3542
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A32F RID: 41775
			// (set) Token: 0x0600D323 RID: 54051 RVA: 0x0012C5F1 File Offset: 0x0012A7F1
			public virtual SwitchParameter DisableWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["DisableWindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A330 RID: 41776
			// (set) Token: 0x0600D324 RID: 54052 RVA: 0x0012C609 File Offset: 0x0012A809
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A331 RID: 41777
			// (set) Token: 0x0600D325 RID: 54053 RVA: 0x0012C627 File Offset: 0x0012A827
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x1700A332 RID: 41778
			// (set) Token: 0x0600D326 RID: 54054 RVA: 0x0012C63F File Offset: 0x0012A83F
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x1700A333 RID: 41779
			// (set) Token: 0x0600D327 RID: 54055 RVA: 0x0012C657 File Offset: 0x0012A857
			public virtual SwitchParameter KeepWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["KeepWindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A334 RID: 41780
			// (set) Token: 0x0600D328 RID: 54056 RVA: 0x0012C66F File Offset: 0x0012A86F
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x1700A335 RID: 41781
			// (set) Token: 0x0600D329 RID: 54057 RVA: 0x0012C687 File Offset: 0x0012A887
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A336 RID: 41782
			// (set) Token: 0x0600D32A RID: 54058 RVA: 0x0012C69F File Offset: 0x0012A89F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A337 RID: 41783
			// (set) Token: 0x0600D32B RID: 54059 RVA: 0x0012C6B2 File Offset: 0x0012A8B2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A338 RID: 41784
			// (set) Token: 0x0600D32C RID: 54060 RVA: 0x0012C6CA File Offset: 0x0012A8CA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A339 RID: 41785
			// (set) Token: 0x0600D32D RID: 54061 RVA: 0x0012C6E2 File Offset: 0x0012A8E2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A33A RID: 41786
			// (set) Token: 0x0600D32E RID: 54062 RVA: 0x0012C6FA File Offset: 0x0012A8FA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A33B RID: 41787
			// (set) Token: 0x0600D32F RID: 54063 RVA: 0x0012C712 File Offset: 0x0012A912
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A33C RID: 41788
			// (set) Token: 0x0600D330 RID: 54064 RVA: 0x0012C72A File Offset: 0x0012A92A
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
