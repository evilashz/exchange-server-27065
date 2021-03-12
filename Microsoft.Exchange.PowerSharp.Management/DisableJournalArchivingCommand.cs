using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C58 RID: 3160
	public class DisableJournalArchivingCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x06009AFB RID: 39675 RVA: 0x000E0FA2 File Offset: 0x000DF1A2
		private DisableJournalArchivingCommand() : base("Disable-JournalArchiving")
		{
		}

		// Token: 0x06009AFC RID: 39676 RVA: 0x000E0FAF File Offset: 0x000DF1AF
		public DisableJournalArchivingCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009AFD RID: 39677 RVA: 0x000E0FBE File Offset: 0x000DF1BE
		public virtual DisableJournalArchivingCommand SetParameters(DisableJournalArchivingCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009AFE RID: 39678 RVA: 0x000E0FC8 File Offset: 0x000DF1C8
		public virtual DisableJournalArchivingCommand SetParameters(DisableJournalArchivingCommand.StoreMailboxIdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009AFF RID: 39679 RVA: 0x000E0FD2 File Offset: 0x000DF1D2
		public virtual DisableJournalArchivingCommand SetParameters(DisableJournalArchivingCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C59 RID: 3161
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006E04 RID: 28164
			// (set) Token: 0x06009B00 RID: 39680 RVA: 0x000E0FDC File Offset: 0x000DF1DC
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x17006E05 RID: 28165
			// (set) Token: 0x06009B01 RID: 39681 RVA: 0x000E0FF4 File Offset: 0x000DF1F4
			public virtual SwitchParameter KeepWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["KeepWindowsLiveID"] = value;
				}
			}

			// Token: 0x17006E06 RID: 28166
			// (set) Token: 0x06009B02 RID: 39682 RVA: 0x000E100C File Offset: 0x000DF20C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006E07 RID: 28167
			// (set) Token: 0x06009B03 RID: 39683 RVA: 0x000E102A File Offset: 0x000DF22A
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17006E08 RID: 28168
			// (set) Token: 0x06009B04 RID: 39684 RVA: 0x000E1042 File Offset: 0x000DF242
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17006E09 RID: 28169
			// (set) Token: 0x06009B05 RID: 39685 RVA: 0x000E105A File Offset: 0x000DF25A
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17006E0A RID: 28170
			// (set) Token: 0x06009B06 RID: 39686 RVA: 0x000E1072 File Offset: 0x000DF272
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17006E0B RID: 28171
			// (set) Token: 0x06009B07 RID: 39687 RVA: 0x000E108A File Offset: 0x000DF28A
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17006E0C RID: 28172
			// (set) Token: 0x06009B08 RID: 39688 RVA: 0x000E10A2 File Offset: 0x000DF2A2
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006E0D RID: 28173
			// (set) Token: 0x06009B09 RID: 39689 RVA: 0x000E10BA File Offset: 0x000DF2BA
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17006E0E RID: 28174
			// (set) Token: 0x06009B0A RID: 39690 RVA: 0x000E10D2 File Offset: 0x000DF2D2
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006E0F RID: 28175
			// (set) Token: 0x06009B0B RID: 39691 RVA: 0x000E10EA File Offset: 0x000DF2EA
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006E10 RID: 28176
			// (set) Token: 0x06009B0C RID: 39692 RVA: 0x000E1102 File Offset: 0x000DF302
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006E11 RID: 28177
			// (set) Token: 0x06009B0D RID: 39693 RVA: 0x000E1115 File Offset: 0x000DF315
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006E12 RID: 28178
			// (set) Token: 0x06009B0E RID: 39694 RVA: 0x000E112D File Offset: 0x000DF32D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006E13 RID: 28179
			// (set) Token: 0x06009B0F RID: 39695 RVA: 0x000E1145 File Offset: 0x000DF345
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006E14 RID: 28180
			// (set) Token: 0x06009B10 RID: 39696 RVA: 0x000E115D File Offset: 0x000DF35D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006E15 RID: 28181
			// (set) Token: 0x06009B11 RID: 39697 RVA: 0x000E1175 File Offset: 0x000DF375
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006E16 RID: 28182
			// (set) Token: 0x06009B12 RID: 39698 RVA: 0x000E118D File Offset: 0x000DF38D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C5A RID: 3162
		public class StoreMailboxIdentityParameters : ParametersBase
		{
			// Token: 0x17006E17 RID: 28183
			// (set) Token: 0x06009B14 RID: 39700 RVA: 0x000E11AD File Offset: 0x000DF3AD
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006E18 RID: 28184
			// (set) Token: 0x06009B15 RID: 39701 RVA: 0x000E11C0 File Offset: 0x000DF3C0
			public virtual StoreMailboxIdParameter StoreMailboxIdentity
			{
				set
				{
					base.PowerSharpParameters["StoreMailboxIdentity"] = value;
				}
			}

			// Token: 0x17006E19 RID: 28185
			// (set) Token: 0x06009B16 RID: 39702 RVA: 0x000E11D3 File Offset: 0x000DF3D3
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17006E1A RID: 28186
			// (set) Token: 0x06009B17 RID: 39703 RVA: 0x000E11EB File Offset: 0x000DF3EB
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17006E1B RID: 28187
			// (set) Token: 0x06009B18 RID: 39704 RVA: 0x000E1203 File Offset: 0x000DF403
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17006E1C RID: 28188
			// (set) Token: 0x06009B19 RID: 39705 RVA: 0x000E121B File Offset: 0x000DF41B
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17006E1D RID: 28189
			// (set) Token: 0x06009B1A RID: 39706 RVA: 0x000E1233 File Offset: 0x000DF433
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17006E1E RID: 28190
			// (set) Token: 0x06009B1B RID: 39707 RVA: 0x000E124B File Offset: 0x000DF44B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006E1F RID: 28191
			// (set) Token: 0x06009B1C RID: 39708 RVA: 0x000E1263 File Offset: 0x000DF463
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17006E20 RID: 28192
			// (set) Token: 0x06009B1D RID: 39709 RVA: 0x000E127B File Offset: 0x000DF47B
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006E21 RID: 28193
			// (set) Token: 0x06009B1E RID: 39710 RVA: 0x000E1293 File Offset: 0x000DF493
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006E22 RID: 28194
			// (set) Token: 0x06009B1F RID: 39711 RVA: 0x000E12AB File Offset: 0x000DF4AB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006E23 RID: 28195
			// (set) Token: 0x06009B20 RID: 39712 RVA: 0x000E12BE File Offset: 0x000DF4BE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006E24 RID: 28196
			// (set) Token: 0x06009B21 RID: 39713 RVA: 0x000E12D6 File Offset: 0x000DF4D6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006E25 RID: 28197
			// (set) Token: 0x06009B22 RID: 39714 RVA: 0x000E12EE File Offset: 0x000DF4EE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006E26 RID: 28198
			// (set) Token: 0x06009B23 RID: 39715 RVA: 0x000E1306 File Offset: 0x000DF506
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006E27 RID: 28199
			// (set) Token: 0x06009B24 RID: 39716 RVA: 0x000E131E File Offset: 0x000DF51E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006E28 RID: 28200
			// (set) Token: 0x06009B25 RID: 39717 RVA: 0x000E1336 File Offset: 0x000DF536
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C5B RID: 3163
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006E29 RID: 28201
			// (set) Token: 0x06009B27 RID: 39719 RVA: 0x000E1356 File Offset: 0x000DF556
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17006E2A RID: 28202
			// (set) Token: 0x06009B28 RID: 39720 RVA: 0x000E136E File Offset: 0x000DF56E
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17006E2B RID: 28203
			// (set) Token: 0x06009B29 RID: 39721 RVA: 0x000E1386 File Offset: 0x000DF586
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17006E2C RID: 28204
			// (set) Token: 0x06009B2A RID: 39722 RVA: 0x000E139E File Offset: 0x000DF59E
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17006E2D RID: 28205
			// (set) Token: 0x06009B2B RID: 39723 RVA: 0x000E13B6 File Offset: 0x000DF5B6
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17006E2E RID: 28206
			// (set) Token: 0x06009B2C RID: 39724 RVA: 0x000E13CE File Offset: 0x000DF5CE
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006E2F RID: 28207
			// (set) Token: 0x06009B2D RID: 39725 RVA: 0x000E13E6 File Offset: 0x000DF5E6
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17006E30 RID: 28208
			// (set) Token: 0x06009B2E RID: 39726 RVA: 0x000E13FE File Offset: 0x000DF5FE
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006E31 RID: 28209
			// (set) Token: 0x06009B2F RID: 39727 RVA: 0x000E1416 File Offset: 0x000DF616
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006E32 RID: 28210
			// (set) Token: 0x06009B30 RID: 39728 RVA: 0x000E142E File Offset: 0x000DF62E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006E33 RID: 28211
			// (set) Token: 0x06009B31 RID: 39729 RVA: 0x000E1441 File Offset: 0x000DF641
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006E34 RID: 28212
			// (set) Token: 0x06009B32 RID: 39730 RVA: 0x000E1459 File Offset: 0x000DF659
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006E35 RID: 28213
			// (set) Token: 0x06009B33 RID: 39731 RVA: 0x000E1471 File Offset: 0x000DF671
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006E36 RID: 28214
			// (set) Token: 0x06009B34 RID: 39732 RVA: 0x000E1489 File Offset: 0x000DF689
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006E37 RID: 28215
			// (set) Token: 0x06009B35 RID: 39733 RVA: 0x000E14A1 File Offset: 0x000DF6A1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006E38 RID: 28216
			// (set) Token: 0x06009B36 RID: 39734 RVA: 0x000E14B9 File Offset: 0x000DF6B9
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
