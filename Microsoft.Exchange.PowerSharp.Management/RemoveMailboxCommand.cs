using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CD0 RID: 3280
	public class RemoveMailboxCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x0600AC4D RID: 44109 RVA: 0x000F9299 File Offset: 0x000F7499
		private RemoveMailboxCommand() : base("Remove-Mailbox")
		{
		}

		// Token: 0x0600AC4E RID: 44110 RVA: 0x000F92A6 File Offset: 0x000F74A6
		public RemoveMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600AC4F RID: 44111 RVA: 0x000F92B5 File Offset: 0x000F74B5
		public virtual RemoveMailboxCommand SetParameters(RemoveMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AC50 RID: 44112 RVA: 0x000F92BF File Offset: 0x000F74BF
		public virtual RemoveMailboxCommand SetParameters(RemoveMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AC51 RID: 44113 RVA: 0x000F92C9 File Offset: 0x000F74C9
		public virtual RemoveMailboxCommand SetParameters(RemoveMailboxCommand.StoreMailboxIdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CD1 RID: 3281
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007E66 RID: 32358
			// (set) Token: 0x0600AC52 RID: 44114 RVA: 0x000F92D3 File Offset: 0x000F74D3
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17007E67 RID: 32359
			// (set) Token: 0x0600AC53 RID: 44115 RVA: 0x000F92EB File Offset: 0x000F74EB
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007E68 RID: 32360
			// (set) Token: 0x0600AC54 RID: 44116 RVA: 0x000F9303 File Offset: 0x000F7503
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007E69 RID: 32361
			// (set) Token: 0x0600AC55 RID: 44117 RVA: 0x000F931B File Offset: 0x000F751B
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17007E6A RID: 32362
			// (set) Token: 0x0600AC56 RID: 44118 RVA: 0x000F9333 File Offset: 0x000F7533
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17007E6B RID: 32363
			// (set) Token: 0x0600AC57 RID: 44119 RVA: 0x000F934B File Offset: 0x000F754B
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17007E6C RID: 32364
			// (set) Token: 0x0600AC58 RID: 44120 RVA: 0x000F9363 File Offset: 0x000F7563
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007E6D RID: 32365
			// (set) Token: 0x0600AC59 RID: 44121 RVA: 0x000F937B File Offset: 0x000F757B
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17007E6E RID: 32366
			// (set) Token: 0x0600AC5A RID: 44122 RVA: 0x000F9393 File Offset: 0x000F7593
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007E6F RID: 32367
			// (set) Token: 0x0600AC5B RID: 44123 RVA: 0x000F93AB File Offset: 0x000F75AB
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007E70 RID: 32368
			// (set) Token: 0x0600AC5C RID: 44124 RVA: 0x000F93C3 File Offset: 0x000F75C3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007E71 RID: 32369
			// (set) Token: 0x0600AC5D RID: 44125 RVA: 0x000F93D6 File Offset: 0x000F75D6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007E72 RID: 32370
			// (set) Token: 0x0600AC5E RID: 44126 RVA: 0x000F93EE File Offset: 0x000F75EE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007E73 RID: 32371
			// (set) Token: 0x0600AC5F RID: 44127 RVA: 0x000F9406 File Offset: 0x000F7606
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007E74 RID: 32372
			// (set) Token: 0x0600AC60 RID: 44128 RVA: 0x000F941E File Offset: 0x000F761E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007E75 RID: 32373
			// (set) Token: 0x0600AC61 RID: 44129 RVA: 0x000F9436 File Offset: 0x000F7636
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007E76 RID: 32374
			// (set) Token: 0x0600AC62 RID: 44130 RVA: 0x000F944E File Offset: 0x000F764E
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000CD2 RID: 3282
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17007E77 RID: 32375
			// (set) Token: 0x0600AC64 RID: 44132 RVA: 0x000F946E File Offset: 0x000F766E
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x17007E78 RID: 32376
			// (set) Token: 0x0600AC65 RID: 44133 RVA: 0x000F9486 File Offset: 0x000F7686
			public virtual SwitchParameter KeepWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["KeepWindowsLiveID"] = value;
				}
			}

			// Token: 0x17007E79 RID: 32377
			// (set) Token: 0x0600AC66 RID: 44134 RVA: 0x000F949E File Offset: 0x000F769E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007E7A RID: 32378
			// (set) Token: 0x0600AC67 RID: 44135 RVA: 0x000F94BC File Offset: 0x000F76BC
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17007E7B RID: 32379
			// (set) Token: 0x0600AC68 RID: 44136 RVA: 0x000F94D4 File Offset: 0x000F76D4
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007E7C RID: 32380
			// (set) Token: 0x0600AC69 RID: 44137 RVA: 0x000F94EC File Offset: 0x000F76EC
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007E7D RID: 32381
			// (set) Token: 0x0600AC6A RID: 44138 RVA: 0x000F9504 File Offset: 0x000F7704
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17007E7E RID: 32382
			// (set) Token: 0x0600AC6B RID: 44139 RVA: 0x000F951C File Offset: 0x000F771C
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17007E7F RID: 32383
			// (set) Token: 0x0600AC6C RID: 44140 RVA: 0x000F9534 File Offset: 0x000F7734
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17007E80 RID: 32384
			// (set) Token: 0x0600AC6D RID: 44141 RVA: 0x000F954C File Offset: 0x000F774C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007E81 RID: 32385
			// (set) Token: 0x0600AC6E RID: 44142 RVA: 0x000F9564 File Offset: 0x000F7764
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17007E82 RID: 32386
			// (set) Token: 0x0600AC6F RID: 44143 RVA: 0x000F957C File Offset: 0x000F777C
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007E83 RID: 32387
			// (set) Token: 0x0600AC70 RID: 44144 RVA: 0x000F9594 File Offset: 0x000F7794
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007E84 RID: 32388
			// (set) Token: 0x0600AC71 RID: 44145 RVA: 0x000F95AC File Offset: 0x000F77AC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007E85 RID: 32389
			// (set) Token: 0x0600AC72 RID: 44146 RVA: 0x000F95BF File Offset: 0x000F77BF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007E86 RID: 32390
			// (set) Token: 0x0600AC73 RID: 44147 RVA: 0x000F95D7 File Offset: 0x000F77D7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007E87 RID: 32391
			// (set) Token: 0x0600AC74 RID: 44148 RVA: 0x000F95EF File Offset: 0x000F77EF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007E88 RID: 32392
			// (set) Token: 0x0600AC75 RID: 44149 RVA: 0x000F9607 File Offset: 0x000F7807
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007E89 RID: 32393
			// (set) Token: 0x0600AC76 RID: 44150 RVA: 0x000F961F File Offset: 0x000F781F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007E8A RID: 32394
			// (set) Token: 0x0600AC77 RID: 44151 RVA: 0x000F9637 File Offset: 0x000F7837
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000CD3 RID: 3283
		public class StoreMailboxIdentityParameters : ParametersBase
		{
			// Token: 0x17007E8B RID: 32395
			// (set) Token: 0x0600AC79 RID: 44153 RVA: 0x000F9657 File Offset: 0x000F7857
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007E8C RID: 32396
			// (set) Token: 0x0600AC7A RID: 44154 RVA: 0x000F966A File Offset: 0x000F786A
			public virtual StoreMailboxIdParameter StoreMailboxIdentity
			{
				set
				{
					base.PowerSharpParameters["StoreMailboxIdentity"] = value;
				}
			}

			// Token: 0x17007E8D RID: 32397
			// (set) Token: 0x0600AC7B RID: 44155 RVA: 0x000F967D File Offset: 0x000F787D
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17007E8E RID: 32398
			// (set) Token: 0x0600AC7C RID: 44156 RVA: 0x000F9695 File Offset: 0x000F7895
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007E8F RID: 32399
			// (set) Token: 0x0600AC7D RID: 44157 RVA: 0x000F96AD File Offset: 0x000F78AD
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007E90 RID: 32400
			// (set) Token: 0x0600AC7E RID: 44158 RVA: 0x000F96C5 File Offset: 0x000F78C5
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17007E91 RID: 32401
			// (set) Token: 0x0600AC7F RID: 44159 RVA: 0x000F96DD File Offset: 0x000F78DD
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17007E92 RID: 32402
			// (set) Token: 0x0600AC80 RID: 44160 RVA: 0x000F96F5 File Offset: 0x000F78F5
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17007E93 RID: 32403
			// (set) Token: 0x0600AC81 RID: 44161 RVA: 0x000F970D File Offset: 0x000F790D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007E94 RID: 32404
			// (set) Token: 0x0600AC82 RID: 44162 RVA: 0x000F9725 File Offset: 0x000F7925
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17007E95 RID: 32405
			// (set) Token: 0x0600AC83 RID: 44163 RVA: 0x000F973D File Offset: 0x000F793D
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007E96 RID: 32406
			// (set) Token: 0x0600AC84 RID: 44164 RVA: 0x000F9755 File Offset: 0x000F7955
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007E97 RID: 32407
			// (set) Token: 0x0600AC85 RID: 44165 RVA: 0x000F976D File Offset: 0x000F796D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007E98 RID: 32408
			// (set) Token: 0x0600AC86 RID: 44166 RVA: 0x000F9780 File Offset: 0x000F7980
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007E99 RID: 32409
			// (set) Token: 0x0600AC87 RID: 44167 RVA: 0x000F9798 File Offset: 0x000F7998
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007E9A RID: 32410
			// (set) Token: 0x0600AC88 RID: 44168 RVA: 0x000F97B0 File Offset: 0x000F79B0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007E9B RID: 32411
			// (set) Token: 0x0600AC89 RID: 44169 RVA: 0x000F97C8 File Offset: 0x000F79C8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007E9C RID: 32412
			// (set) Token: 0x0600AC8A RID: 44170 RVA: 0x000F97E0 File Offset: 0x000F79E0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007E9D RID: 32413
			// (set) Token: 0x0600AC8B RID: 44171 RVA: 0x000F97F8 File Offset: 0x000F79F8
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
