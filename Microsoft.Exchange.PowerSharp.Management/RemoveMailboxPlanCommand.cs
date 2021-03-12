using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CD8 RID: 3288
	public class RemoveMailboxPlanCommand : SyntheticCommandWithPipelineInput<MailboxPlanIdParameter, MailboxPlanIdParameter>
	{
		// Token: 0x0600ACCA RID: 44234 RVA: 0x000F9D4F File Offset: 0x000F7F4F
		private RemoveMailboxPlanCommand() : base("Remove-MailboxPlan")
		{
		}

		// Token: 0x0600ACCB RID: 44235 RVA: 0x000F9D5C File Offset: 0x000F7F5C
		public RemoveMailboxPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600ACCC RID: 44236 RVA: 0x000F9D6B File Offset: 0x000F7F6B
		public virtual RemoveMailboxPlanCommand SetParameters(RemoveMailboxPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600ACCD RID: 44237 RVA: 0x000F9D75 File Offset: 0x000F7F75
		public virtual RemoveMailboxPlanCommand SetParameters(RemoveMailboxPlanCommand.StoreMailboxIdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600ACCE RID: 44238 RVA: 0x000F9D7F File Offset: 0x000F7F7F
		public virtual RemoveMailboxPlanCommand SetParameters(RemoveMailboxPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CD9 RID: 3289
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17007ED3 RID: 32467
			// (set) Token: 0x0600ACCF RID: 44239 RVA: 0x000F9D89 File Offset: 0x000F7F89
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x17007ED4 RID: 32468
			// (set) Token: 0x0600ACD0 RID: 44240 RVA: 0x000F9DA1 File Offset: 0x000F7FA1
			public virtual SwitchParameter KeepWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["KeepWindowsLiveID"] = value;
				}
			}

			// Token: 0x17007ED5 RID: 32469
			// (set) Token: 0x0600ACD1 RID: 44241 RVA: 0x000F9DB9 File Offset: 0x000F7FB9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007ED6 RID: 32470
			// (set) Token: 0x0600ACD2 RID: 44242 RVA: 0x000F9DD7 File Offset: 0x000F7FD7
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007ED7 RID: 32471
			// (set) Token: 0x0600ACD3 RID: 44243 RVA: 0x000F9DEF File Offset: 0x000F7FEF
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007ED8 RID: 32472
			// (set) Token: 0x0600ACD4 RID: 44244 RVA: 0x000F9E07 File Offset: 0x000F8007
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17007ED9 RID: 32473
			// (set) Token: 0x0600ACD5 RID: 44245 RVA: 0x000F9E1F File Offset: 0x000F801F
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17007EDA RID: 32474
			// (set) Token: 0x0600ACD6 RID: 44246 RVA: 0x000F9E37 File Offset: 0x000F8037
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17007EDB RID: 32475
			// (set) Token: 0x0600ACD7 RID: 44247 RVA: 0x000F9E4F File Offset: 0x000F804F
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007EDC RID: 32476
			// (set) Token: 0x0600ACD8 RID: 44248 RVA: 0x000F9E67 File Offset: 0x000F8067
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17007EDD RID: 32477
			// (set) Token: 0x0600ACD9 RID: 44249 RVA: 0x000F9E7F File Offset: 0x000F807F
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007EDE RID: 32478
			// (set) Token: 0x0600ACDA RID: 44250 RVA: 0x000F9E97 File Offset: 0x000F8097
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007EDF RID: 32479
			// (set) Token: 0x0600ACDB RID: 44251 RVA: 0x000F9EAF File Offset: 0x000F80AF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007EE0 RID: 32480
			// (set) Token: 0x0600ACDC RID: 44252 RVA: 0x000F9EC2 File Offset: 0x000F80C2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007EE1 RID: 32481
			// (set) Token: 0x0600ACDD RID: 44253 RVA: 0x000F9EDA File Offset: 0x000F80DA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007EE2 RID: 32482
			// (set) Token: 0x0600ACDE RID: 44254 RVA: 0x000F9EF2 File Offset: 0x000F80F2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007EE3 RID: 32483
			// (set) Token: 0x0600ACDF RID: 44255 RVA: 0x000F9F0A File Offset: 0x000F810A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007EE4 RID: 32484
			// (set) Token: 0x0600ACE0 RID: 44256 RVA: 0x000F9F22 File Offset: 0x000F8122
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007EE5 RID: 32485
			// (set) Token: 0x0600ACE1 RID: 44257 RVA: 0x000F9F3A File Offset: 0x000F813A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000CDA RID: 3290
		public class StoreMailboxIdentityParameters : ParametersBase
		{
			// Token: 0x17007EE6 RID: 32486
			// (set) Token: 0x0600ACE3 RID: 44259 RVA: 0x000F9F5A File Offset: 0x000F815A
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007EE7 RID: 32487
			// (set) Token: 0x0600ACE4 RID: 44260 RVA: 0x000F9F6D File Offset: 0x000F816D
			public virtual StoreMailboxIdParameter StoreMailboxIdentity
			{
				set
				{
					base.PowerSharpParameters["StoreMailboxIdentity"] = value;
				}
			}

			// Token: 0x17007EE8 RID: 32488
			// (set) Token: 0x0600ACE5 RID: 44261 RVA: 0x000F9F80 File Offset: 0x000F8180
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007EE9 RID: 32489
			// (set) Token: 0x0600ACE6 RID: 44262 RVA: 0x000F9F98 File Offset: 0x000F8198
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007EEA RID: 32490
			// (set) Token: 0x0600ACE7 RID: 44263 RVA: 0x000F9FB0 File Offset: 0x000F81B0
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17007EEB RID: 32491
			// (set) Token: 0x0600ACE8 RID: 44264 RVA: 0x000F9FC8 File Offset: 0x000F81C8
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17007EEC RID: 32492
			// (set) Token: 0x0600ACE9 RID: 44265 RVA: 0x000F9FE0 File Offset: 0x000F81E0
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17007EED RID: 32493
			// (set) Token: 0x0600ACEA RID: 44266 RVA: 0x000F9FF8 File Offset: 0x000F81F8
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007EEE RID: 32494
			// (set) Token: 0x0600ACEB RID: 44267 RVA: 0x000FA010 File Offset: 0x000F8210
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17007EEF RID: 32495
			// (set) Token: 0x0600ACEC RID: 44268 RVA: 0x000FA028 File Offset: 0x000F8228
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007EF0 RID: 32496
			// (set) Token: 0x0600ACED RID: 44269 RVA: 0x000FA040 File Offset: 0x000F8240
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007EF1 RID: 32497
			// (set) Token: 0x0600ACEE RID: 44270 RVA: 0x000FA058 File Offset: 0x000F8258
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007EF2 RID: 32498
			// (set) Token: 0x0600ACEF RID: 44271 RVA: 0x000FA06B File Offset: 0x000F826B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007EF3 RID: 32499
			// (set) Token: 0x0600ACF0 RID: 44272 RVA: 0x000FA083 File Offset: 0x000F8283
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007EF4 RID: 32500
			// (set) Token: 0x0600ACF1 RID: 44273 RVA: 0x000FA09B File Offset: 0x000F829B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007EF5 RID: 32501
			// (set) Token: 0x0600ACF2 RID: 44274 RVA: 0x000FA0B3 File Offset: 0x000F82B3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007EF6 RID: 32502
			// (set) Token: 0x0600ACF3 RID: 44275 RVA: 0x000FA0CB File Offset: 0x000F82CB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007EF7 RID: 32503
			// (set) Token: 0x0600ACF4 RID: 44276 RVA: 0x000FA0E3 File Offset: 0x000F82E3
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000CDB RID: 3291
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007EF8 RID: 32504
			// (set) Token: 0x0600ACF6 RID: 44278 RVA: 0x000FA103 File Offset: 0x000F8303
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007EF9 RID: 32505
			// (set) Token: 0x0600ACF7 RID: 44279 RVA: 0x000FA11B File Offset: 0x000F831B
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007EFA RID: 32506
			// (set) Token: 0x0600ACF8 RID: 44280 RVA: 0x000FA133 File Offset: 0x000F8333
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17007EFB RID: 32507
			// (set) Token: 0x0600ACF9 RID: 44281 RVA: 0x000FA14B File Offset: 0x000F834B
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17007EFC RID: 32508
			// (set) Token: 0x0600ACFA RID: 44282 RVA: 0x000FA163 File Offset: 0x000F8363
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17007EFD RID: 32509
			// (set) Token: 0x0600ACFB RID: 44283 RVA: 0x000FA17B File Offset: 0x000F837B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007EFE RID: 32510
			// (set) Token: 0x0600ACFC RID: 44284 RVA: 0x000FA193 File Offset: 0x000F8393
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17007EFF RID: 32511
			// (set) Token: 0x0600ACFD RID: 44285 RVA: 0x000FA1AB File Offset: 0x000F83AB
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007F00 RID: 32512
			// (set) Token: 0x0600ACFE RID: 44286 RVA: 0x000FA1C3 File Offset: 0x000F83C3
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007F01 RID: 32513
			// (set) Token: 0x0600ACFF RID: 44287 RVA: 0x000FA1DB File Offset: 0x000F83DB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F02 RID: 32514
			// (set) Token: 0x0600AD00 RID: 44288 RVA: 0x000FA1EE File Offset: 0x000F83EE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F03 RID: 32515
			// (set) Token: 0x0600AD01 RID: 44289 RVA: 0x000FA206 File Offset: 0x000F8406
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F04 RID: 32516
			// (set) Token: 0x0600AD02 RID: 44290 RVA: 0x000FA21E File Offset: 0x000F841E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F05 RID: 32517
			// (set) Token: 0x0600AD03 RID: 44291 RVA: 0x000FA236 File Offset: 0x000F8436
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F06 RID: 32518
			// (set) Token: 0x0600AD04 RID: 44292 RVA: 0x000FA24E File Offset: 0x000F844E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007F07 RID: 32519
			// (set) Token: 0x0600AD05 RID: 44293 RVA: 0x000FA266 File Offset: 0x000F8466
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
