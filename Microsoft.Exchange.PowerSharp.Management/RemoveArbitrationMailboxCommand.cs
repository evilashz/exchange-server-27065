using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CD4 RID: 3284
	public class RemoveArbitrationMailboxCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x0600AC8D RID: 44173 RVA: 0x000F9818 File Offset: 0x000F7A18
		private RemoveArbitrationMailboxCommand() : base("Remove-ArbitrationMailbox")
		{
		}

		// Token: 0x0600AC8E RID: 44174 RVA: 0x000F9825 File Offset: 0x000F7A25
		public RemoveArbitrationMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600AC8F RID: 44175 RVA: 0x000F9834 File Offset: 0x000F7A34
		public virtual RemoveArbitrationMailboxCommand SetParameters(RemoveArbitrationMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AC90 RID: 44176 RVA: 0x000F983E File Offset: 0x000F7A3E
		public virtual RemoveArbitrationMailboxCommand SetParameters(RemoveArbitrationMailboxCommand.StoreMailboxIdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AC91 RID: 44177 RVA: 0x000F9848 File Offset: 0x000F7A48
		public virtual RemoveArbitrationMailboxCommand SetParameters(RemoveArbitrationMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CD5 RID: 3285
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17007E9E RID: 32414
			// (set) Token: 0x0600AC92 RID: 44178 RVA: 0x000F9852 File Offset: 0x000F7A52
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x17007E9F RID: 32415
			// (set) Token: 0x0600AC93 RID: 44179 RVA: 0x000F986A File Offset: 0x000F7A6A
			public virtual SwitchParameter KeepWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["KeepWindowsLiveID"] = value;
				}
			}

			// Token: 0x17007EA0 RID: 32416
			// (set) Token: 0x0600AC94 RID: 44180 RVA: 0x000F9882 File Offset: 0x000F7A82
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007EA1 RID: 32417
			// (set) Token: 0x0600AC95 RID: 44181 RVA: 0x000F98A0 File Offset: 0x000F7AA0
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007EA2 RID: 32418
			// (set) Token: 0x0600AC96 RID: 44182 RVA: 0x000F98B8 File Offset: 0x000F7AB8
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007EA3 RID: 32419
			// (set) Token: 0x0600AC97 RID: 44183 RVA: 0x000F98D0 File Offset: 0x000F7AD0
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17007EA4 RID: 32420
			// (set) Token: 0x0600AC98 RID: 44184 RVA: 0x000F98E8 File Offset: 0x000F7AE8
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17007EA5 RID: 32421
			// (set) Token: 0x0600AC99 RID: 44185 RVA: 0x000F9900 File Offset: 0x000F7B00
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17007EA6 RID: 32422
			// (set) Token: 0x0600AC9A RID: 44186 RVA: 0x000F9918 File Offset: 0x000F7B18
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007EA7 RID: 32423
			// (set) Token: 0x0600AC9B RID: 44187 RVA: 0x000F9930 File Offset: 0x000F7B30
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17007EA8 RID: 32424
			// (set) Token: 0x0600AC9C RID: 44188 RVA: 0x000F9948 File Offset: 0x000F7B48
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007EA9 RID: 32425
			// (set) Token: 0x0600AC9D RID: 44189 RVA: 0x000F9960 File Offset: 0x000F7B60
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007EAA RID: 32426
			// (set) Token: 0x0600AC9E RID: 44190 RVA: 0x000F9978 File Offset: 0x000F7B78
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007EAB RID: 32427
			// (set) Token: 0x0600AC9F RID: 44191 RVA: 0x000F998B File Offset: 0x000F7B8B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007EAC RID: 32428
			// (set) Token: 0x0600ACA0 RID: 44192 RVA: 0x000F99A3 File Offset: 0x000F7BA3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007EAD RID: 32429
			// (set) Token: 0x0600ACA1 RID: 44193 RVA: 0x000F99BB File Offset: 0x000F7BBB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007EAE RID: 32430
			// (set) Token: 0x0600ACA2 RID: 44194 RVA: 0x000F99D3 File Offset: 0x000F7BD3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007EAF RID: 32431
			// (set) Token: 0x0600ACA3 RID: 44195 RVA: 0x000F99EB File Offset: 0x000F7BEB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007EB0 RID: 32432
			// (set) Token: 0x0600ACA4 RID: 44196 RVA: 0x000F9A03 File Offset: 0x000F7C03
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000CD6 RID: 3286
		public class StoreMailboxIdentityParameters : ParametersBase
		{
			// Token: 0x17007EB1 RID: 32433
			// (set) Token: 0x0600ACA6 RID: 44198 RVA: 0x000F9A23 File Offset: 0x000F7C23
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007EB2 RID: 32434
			// (set) Token: 0x0600ACA7 RID: 44199 RVA: 0x000F9A36 File Offset: 0x000F7C36
			public virtual StoreMailboxIdParameter StoreMailboxIdentity
			{
				set
				{
					base.PowerSharpParameters["StoreMailboxIdentity"] = value;
				}
			}

			// Token: 0x17007EB3 RID: 32435
			// (set) Token: 0x0600ACA8 RID: 44200 RVA: 0x000F9A49 File Offset: 0x000F7C49
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007EB4 RID: 32436
			// (set) Token: 0x0600ACA9 RID: 44201 RVA: 0x000F9A61 File Offset: 0x000F7C61
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007EB5 RID: 32437
			// (set) Token: 0x0600ACAA RID: 44202 RVA: 0x000F9A79 File Offset: 0x000F7C79
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17007EB6 RID: 32438
			// (set) Token: 0x0600ACAB RID: 44203 RVA: 0x000F9A91 File Offset: 0x000F7C91
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17007EB7 RID: 32439
			// (set) Token: 0x0600ACAC RID: 44204 RVA: 0x000F9AA9 File Offset: 0x000F7CA9
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17007EB8 RID: 32440
			// (set) Token: 0x0600ACAD RID: 44205 RVA: 0x000F9AC1 File Offset: 0x000F7CC1
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007EB9 RID: 32441
			// (set) Token: 0x0600ACAE RID: 44206 RVA: 0x000F9AD9 File Offset: 0x000F7CD9
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17007EBA RID: 32442
			// (set) Token: 0x0600ACAF RID: 44207 RVA: 0x000F9AF1 File Offset: 0x000F7CF1
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007EBB RID: 32443
			// (set) Token: 0x0600ACB0 RID: 44208 RVA: 0x000F9B09 File Offset: 0x000F7D09
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007EBC RID: 32444
			// (set) Token: 0x0600ACB1 RID: 44209 RVA: 0x000F9B21 File Offset: 0x000F7D21
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007EBD RID: 32445
			// (set) Token: 0x0600ACB2 RID: 44210 RVA: 0x000F9B34 File Offset: 0x000F7D34
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007EBE RID: 32446
			// (set) Token: 0x0600ACB3 RID: 44211 RVA: 0x000F9B4C File Offset: 0x000F7D4C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007EBF RID: 32447
			// (set) Token: 0x0600ACB4 RID: 44212 RVA: 0x000F9B64 File Offset: 0x000F7D64
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007EC0 RID: 32448
			// (set) Token: 0x0600ACB5 RID: 44213 RVA: 0x000F9B7C File Offset: 0x000F7D7C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007EC1 RID: 32449
			// (set) Token: 0x0600ACB6 RID: 44214 RVA: 0x000F9B94 File Offset: 0x000F7D94
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007EC2 RID: 32450
			// (set) Token: 0x0600ACB7 RID: 44215 RVA: 0x000F9BAC File Offset: 0x000F7DAC
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000CD7 RID: 3287
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007EC3 RID: 32451
			// (set) Token: 0x0600ACB9 RID: 44217 RVA: 0x000F9BCC File Offset: 0x000F7DCC
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17007EC4 RID: 32452
			// (set) Token: 0x0600ACBA RID: 44218 RVA: 0x000F9BE4 File Offset: 0x000F7DE4
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17007EC5 RID: 32453
			// (set) Token: 0x0600ACBB RID: 44219 RVA: 0x000F9BFC File Offset: 0x000F7DFC
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17007EC6 RID: 32454
			// (set) Token: 0x0600ACBC RID: 44220 RVA: 0x000F9C14 File Offset: 0x000F7E14
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17007EC7 RID: 32455
			// (set) Token: 0x0600ACBD RID: 44221 RVA: 0x000F9C2C File Offset: 0x000F7E2C
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17007EC8 RID: 32456
			// (set) Token: 0x0600ACBE RID: 44222 RVA: 0x000F9C44 File Offset: 0x000F7E44
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007EC9 RID: 32457
			// (set) Token: 0x0600ACBF RID: 44223 RVA: 0x000F9C5C File Offset: 0x000F7E5C
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17007ECA RID: 32458
			// (set) Token: 0x0600ACC0 RID: 44224 RVA: 0x000F9C74 File Offset: 0x000F7E74
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007ECB RID: 32459
			// (set) Token: 0x0600ACC1 RID: 44225 RVA: 0x000F9C8C File Offset: 0x000F7E8C
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17007ECC RID: 32460
			// (set) Token: 0x0600ACC2 RID: 44226 RVA: 0x000F9CA4 File Offset: 0x000F7EA4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007ECD RID: 32461
			// (set) Token: 0x0600ACC3 RID: 44227 RVA: 0x000F9CB7 File Offset: 0x000F7EB7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007ECE RID: 32462
			// (set) Token: 0x0600ACC4 RID: 44228 RVA: 0x000F9CCF File Offset: 0x000F7ECF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007ECF RID: 32463
			// (set) Token: 0x0600ACC5 RID: 44229 RVA: 0x000F9CE7 File Offset: 0x000F7EE7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007ED0 RID: 32464
			// (set) Token: 0x0600ACC6 RID: 44230 RVA: 0x000F9CFF File Offset: 0x000F7EFF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007ED1 RID: 32465
			// (set) Token: 0x0600ACC7 RID: 44231 RVA: 0x000F9D17 File Offset: 0x000F7F17
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17007ED2 RID: 32466
			// (set) Token: 0x0600ACC8 RID: 44232 RVA: 0x000F9D2F File Offset: 0x000F7F2F
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
