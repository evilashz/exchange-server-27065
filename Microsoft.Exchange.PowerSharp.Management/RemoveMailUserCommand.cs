using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D0D RID: 3341
	public class RemoveMailUserCommand : SyntheticCommandWithPipelineInput<MailUserIdParameter, MailUserIdParameter>
	{
		// Token: 0x0600B0D7 RID: 45271 RVA: 0x000FF2F3 File Offset: 0x000FD4F3
		private RemoveMailUserCommand() : base("Remove-MailUser")
		{
		}

		// Token: 0x0600B0D8 RID: 45272 RVA: 0x000FF300 File Offset: 0x000FD500
		public RemoveMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B0D9 RID: 45273 RVA: 0x000FF30F File Offset: 0x000FD50F
		public virtual RemoveMailUserCommand SetParameters(RemoveMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B0DA RID: 45274 RVA: 0x000FF319 File Offset: 0x000FD519
		public virtual RemoveMailUserCommand SetParameters(RemoveMailUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D0E RID: 3342
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008276 RID: 33398
			// (set) Token: 0x0600B0DB RID: 45275 RVA: 0x000FF323 File Offset: 0x000FD523
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17008277 RID: 33399
			// (set) Token: 0x0600B0DC RID: 45276 RVA: 0x000FF33B File Offset: 0x000FD53B
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x17008278 RID: 33400
			// (set) Token: 0x0600B0DD RID: 45277 RVA: 0x000FF353 File Offset: 0x000FD553
			public virtual SwitchParameter KeepWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["KeepWindowsLiveID"] = value;
				}
			}

			// Token: 0x17008279 RID: 33401
			// (set) Token: 0x0600B0DE RID: 45278 RVA: 0x000FF36B File Offset: 0x000FD56B
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x1700827A RID: 33402
			// (set) Token: 0x0600B0DF RID: 45279 RVA: 0x000FF383 File Offset: 0x000FD583
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700827B RID: 33403
			// (set) Token: 0x0600B0E0 RID: 45280 RVA: 0x000FF39B File Offset: 0x000FD59B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700827C RID: 33404
			// (set) Token: 0x0600B0E1 RID: 45281 RVA: 0x000FF3AE File Offset: 0x000FD5AE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700827D RID: 33405
			// (set) Token: 0x0600B0E2 RID: 45282 RVA: 0x000FF3C6 File Offset: 0x000FD5C6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700827E RID: 33406
			// (set) Token: 0x0600B0E3 RID: 45283 RVA: 0x000FF3DE File Offset: 0x000FD5DE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700827F RID: 33407
			// (set) Token: 0x0600B0E4 RID: 45284 RVA: 0x000FF3F6 File Offset: 0x000FD5F6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008280 RID: 33408
			// (set) Token: 0x0600B0E5 RID: 45285 RVA: 0x000FF40E File Offset: 0x000FD60E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17008281 RID: 33409
			// (set) Token: 0x0600B0E6 RID: 45286 RVA: 0x000FF426 File Offset: 0x000FD626
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000D0F RID: 3343
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17008282 RID: 33410
			// (set) Token: 0x0600B0E8 RID: 45288 RVA: 0x000FF446 File Offset: 0x000FD646
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17008283 RID: 33411
			// (set) Token: 0x0600B0E9 RID: 45289 RVA: 0x000FF464 File Offset: 0x000FD664
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17008284 RID: 33412
			// (set) Token: 0x0600B0EA RID: 45290 RVA: 0x000FF47C File Offset: 0x000FD67C
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x17008285 RID: 33413
			// (set) Token: 0x0600B0EB RID: 45291 RVA: 0x000FF494 File Offset: 0x000FD694
			public virtual SwitchParameter KeepWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["KeepWindowsLiveID"] = value;
				}
			}

			// Token: 0x17008286 RID: 33414
			// (set) Token: 0x0600B0EC RID: 45292 RVA: 0x000FF4AC File Offset: 0x000FD6AC
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17008287 RID: 33415
			// (set) Token: 0x0600B0ED RID: 45293 RVA: 0x000FF4C4 File Offset: 0x000FD6C4
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008288 RID: 33416
			// (set) Token: 0x0600B0EE RID: 45294 RVA: 0x000FF4DC File Offset: 0x000FD6DC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008289 RID: 33417
			// (set) Token: 0x0600B0EF RID: 45295 RVA: 0x000FF4EF File Offset: 0x000FD6EF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700828A RID: 33418
			// (set) Token: 0x0600B0F0 RID: 45296 RVA: 0x000FF507 File Offset: 0x000FD707
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700828B RID: 33419
			// (set) Token: 0x0600B0F1 RID: 45297 RVA: 0x000FF51F File Offset: 0x000FD71F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700828C RID: 33420
			// (set) Token: 0x0600B0F2 RID: 45298 RVA: 0x000FF537 File Offset: 0x000FD737
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700828D RID: 33421
			// (set) Token: 0x0600B0F3 RID: 45299 RVA: 0x000FF54F File Offset: 0x000FD74F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700828E RID: 33422
			// (set) Token: 0x0600B0F4 RID: 45300 RVA: 0x000FF567 File Offset: 0x000FD767
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
