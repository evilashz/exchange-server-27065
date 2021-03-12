using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C6B RID: 3179
	public class DisableMailboxCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06009C24 RID: 39972 RVA: 0x000E27B8 File Offset: 0x000E09B8
		private DisableMailboxCommand() : base("Disable-Mailbox")
		{
		}

		// Token: 0x06009C25 RID: 39973 RVA: 0x000E27C5 File Offset: 0x000E09C5
		public DisableMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009C26 RID: 39974 RVA: 0x000E27D4 File Offset: 0x000E09D4
		public virtual DisableMailboxCommand SetParameters(DisableMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C27 RID: 39975 RVA: 0x000E27DE File Offset: 0x000E09DE
		public virtual DisableMailboxCommand SetParameters(DisableMailboxCommand.ArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C28 RID: 39976 RVA: 0x000E27E8 File Offset: 0x000E09E8
		public virtual DisableMailboxCommand SetParameters(DisableMailboxCommand.RemoteArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C29 RID: 39977 RVA: 0x000E27F2 File Offset: 0x000E09F2
		public virtual DisableMailboxCommand SetParameters(DisableMailboxCommand.ArbitrationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C2A RID: 39978 RVA: 0x000E27FC File Offset: 0x000E09FC
		public virtual DisableMailboxCommand SetParameters(DisableMailboxCommand.PublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C6C RID: 3180
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006F07 RID: 28423
			// (set) Token: 0x06009C2B RID: 39979 RVA: 0x000E2806 File Offset: 0x000E0A06
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006F08 RID: 28424
			// (set) Token: 0x06009C2C RID: 39980 RVA: 0x000E2824 File Offset: 0x000E0A24
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006F09 RID: 28425
			// (set) Token: 0x06009C2D RID: 39981 RVA: 0x000E283C File Offset: 0x000E0A3C
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17006F0A RID: 28426
			// (set) Token: 0x06009C2E RID: 39982 RVA: 0x000E2854 File Offset: 0x000E0A54
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006F0B RID: 28427
			// (set) Token: 0x06009C2F RID: 39983 RVA: 0x000E286C File Offset: 0x000E0A6C
			public virtual SwitchParameter PreserveEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["PreserveEmailAddresses"] = value;
				}
			}

			// Token: 0x17006F0C RID: 28428
			// (set) Token: 0x06009C30 RID: 39984 RVA: 0x000E2884 File Offset: 0x000E0A84
			public virtual SwitchParameter PreventRecordingPreviousDatabase
			{
				set
				{
					base.PowerSharpParameters["PreventRecordingPreviousDatabase"] = value;
				}
			}

			// Token: 0x17006F0D RID: 28429
			// (set) Token: 0x06009C31 RID: 39985 RVA: 0x000E289C File Offset: 0x000E0A9C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006F0E RID: 28430
			// (set) Token: 0x06009C32 RID: 39986 RVA: 0x000E28AF File Offset: 0x000E0AAF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006F0F RID: 28431
			// (set) Token: 0x06009C33 RID: 39987 RVA: 0x000E28C7 File Offset: 0x000E0AC7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006F10 RID: 28432
			// (set) Token: 0x06009C34 RID: 39988 RVA: 0x000E28DF File Offset: 0x000E0ADF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006F11 RID: 28433
			// (set) Token: 0x06009C35 RID: 39989 RVA: 0x000E28F7 File Offset: 0x000E0AF7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006F12 RID: 28434
			// (set) Token: 0x06009C36 RID: 39990 RVA: 0x000E290F File Offset: 0x000E0B0F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006F13 RID: 28435
			// (set) Token: 0x06009C37 RID: 39991 RVA: 0x000E2927 File Offset: 0x000E0B27
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C6D RID: 3181
		public class ArchiveParameters : ParametersBase
		{
			// Token: 0x17006F14 RID: 28436
			// (set) Token: 0x06009C39 RID: 39993 RVA: 0x000E2947 File Offset: 0x000E0B47
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17006F15 RID: 28437
			// (set) Token: 0x06009C3A RID: 39994 RVA: 0x000E295F File Offset: 0x000E0B5F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006F16 RID: 28438
			// (set) Token: 0x06009C3B RID: 39995 RVA: 0x000E297D File Offset: 0x000E0B7D
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006F17 RID: 28439
			// (set) Token: 0x06009C3C RID: 39996 RVA: 0x000E2995 File Offset: 0x000E0B95
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17006F18 RID: 28440
			// (set) Token: 0x06009C3D RID: 39997 RVA: 0x000E29AD File Offset: 0x000E0BAD
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006F19 RID: 28441
			// (set) Token: 0x06009C3E RID: 39998 RVA: 0x000E29C5 File Offset: 0x000E0BC5
			public virtual SwitchParameter PreserveEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["PreserveEmailAddresses"] = value;
				}
			}

			// Token: 0x17006F1A RID: 28442
			// (set) Token: 0x06009C3F RID: 39999 RVA: 0x000E29DD File Offset: 0x000E0BDD
			public virtual SwitchParameter PreventRecordingPreviousDatabase
			{
				set
				{
					base.PowerSharpParameters["PreventRecordingPreviousDatabase"] = value;
				}
			}

			// Token: 0x17006F1B RID: 28443
			// (set) Token: 0x06009C40 RID: 40000 RVA: 0x000E29F5 File Offset: 0x000E0BF5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006F1C RID: 28444
			// (set) Token: 0x06009C41 RID: 40001 RVA: 0x000E2A08 File Offset: 0x000E0C08
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006F1D RID: 28445
			// (set) Token: 0x06009C42 RID: 40002 RVA: 0x000E2A20 File Offset: 0x000E0C20
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006F1E RID: 28446
			// (set) Token: 0x06009C43 RID: 40003 RVA: 0x000E2A38 File Offset: 0x000E0C38
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006F1F RID: 28447
			// (set) Token: 0x06009C44 RID: 40004 RVA: 0x000E2A50 File Offset: 0x000E0C50
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006F20 RID: 28448
			// (set) Token: 0x06009C45 RID: 40005 RVA: 0x000E2A68 File Offset: 0x000E0C68
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006F21 RID: 28449
			// (set) Token: 0x06009C46 RID: 40006 RVA: 0x000E2A80 File Offset: 0x000E0C80
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C6E RID: 3182
		public class RemoteArchiveParameters : ParametersBase
		{
			// Token: 0x17006F22 RID: 28450
			// (set) Token: 0x06009C48 RID: 40008 RVA: 0x000E2AA0 File Offset: 0x000E0CA0
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x17006F23 RID: 28451
			// (set) Token: 0x06009C49 RID: 40009 RVA: 0x000E2AB8 File Offset: 0x000E0CB8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006F24 RID: 28452
			// (set) Token: 0x06009C4A RID: 40010 RVA: 0x000E2AD6 File Offset: 0x000E0CD6
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006F25 RID: 28453
			// (set) Token: 0x06009C4B RID: 40011 RVA: 0x000E2AEE File Offset: 0x000E0CEE
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17006F26 RID: 28454
			// (set) Token: 0x06009C4C RID: 40012 RVA: 0x000E2B06 File Offset: 0x000E0D06
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006F27 RID: 28455
			// (set) Token: 0x06009C4D RID: 40013 RVA: 0x000E2B1E File Offset: 0x000E0D1E
			public virtual SwitchParameter PreserveEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["PreserveEmailAddresses"] = value;
				}
			}

			// Token: 0x17006F28 RID: 28456
			// (set) Token: 0x06009C4E RID: 40014 RVA: 0x000E2B36 File Offset: 0x000E0D36
			public virtual SwitchParameter PreventRecordingPreviousDatabase
			{
				set
				{
					base.PowerSharpParameters["PreventRecordingPreviousDatabase"] = value;
				}
			}

			// Token: 0x17006F29 RID: 28457
			// (set) Token: 0x06009C4F RID: 40015 RVA: 0x000E2B4E File Offset: 0x000E0D4E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006F2A RID: 28458
			// (set) Token: 0x06009C50 RID: 40016 RVA: 0x000E2B61 File Offset: 0x000E0D61
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006F2B RID: 28459
			// (set) Token: 0x06009C51 RID: 40017 RVA: 0x000E2B79 File Offset: 0x000E0D79
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006F2C RID: 28460
			// (set) Token: 0x06009C52 RID: 40018 RVA: 0x000E2B91 File Offset: 0x000E0D91
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006F2D RID: 28461
			// (set) Token: 0x06009C53 RID: 40019 RVA: 0x000E2BA9 File Offset: 0x000E0DA9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006F2E RID: 28462
			// (set) Token: 0x06009C54 RID: 40020 RVA: 0x000E2BC1 File Offset: 0x000E0DC1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006F2F RID: 28463
			// (set) Token: 0x06009C55 RID: 40021 RVA: 0x000E2BD9 File Offset: 0x000E0DD9
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C6F RID: 3183
		public class ArbitrationParameters : ParametersBase
		{
			// Token: 0x17006F30 RID: 28464
			// (set) Token: 0x06009C57 RID: 40023 RVA: 0x000E2BF9 File Offset: 0x000E0DF9
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17006F31 RID: 28465
			// (set) Token: 0x06009C58 RID: 40024 RVA: 0x000E2C11 File Offset: 0x000E0E11
			public virtual SwitchParameter DisableLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["DisableLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17006F32 RID: 28466
			// (set) Token: 0x06009C59 RID: 40025 RVA: 0x000E2C29 File Offset: 0x000E0E29
			public virtual SwitchParameter DisableArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["DisableArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17006F33 RID: 28467
			// (set) Token: 0x06009C5A RID: 40026 RVA: 0x000E2C41 File Offset: 0x000E0E41
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006F34 RID: 28468
			// (set) Token: 0x06009C5B RID: 40027 RVA: 0x000E2C5F File Offset: 0x000E0E5F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006F35 RID: 28469
			// (set) Token: 0x06009C5C RID: 40028 RVA: 0x000E2C77 File Offset: 0x000E0E77
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17006F36 RID: 28470
			// (set) Token: 0x06009C5D RID: 40029 RVA: 0x000E2C8F File Offset: 0x000E0E8F
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006F37 RID: 28471
			// (set) Token: 0x06009C5E RID: 40030 RVA: 0x000E2CA7 File Offset: 0x000E0EA7
			public virtual SwitchParameter PreserveEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["PreserveEmailAddresses"] = value;
				}
			}

			// Token: 0x17006F38 RID: 28472
			// (set) Token: 0x06009C5F RID: 40031 RVA: 0x000E2CBF File Offset: 0x000E0EBF
			public virtual SwitchParameter PreventRecordingPreviousDatabase
			{
				set
				{
					base.PowerSharpParameters["PreventRecordingPreviousDatabase"] = value;
				}
			}

			// Token: 0x17006F39 RID: 28473
			// (set) Token: 0x06009C60 RID: 40032 RVA: 0x000E2CD7 File Offset: 0x000E0ED7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006F3A RID: 28474
			// (set) Token: 0x06009C61 RID: 40033 RVA: 0x000E2CEA File Offset: 0x000E0EEA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006F3B RID: 28475
			// (set) Token: 0x06009C62 RID: 40034 RVA: 0x000E2D02 File Offset: 0x000E0F02
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006F3C RID: 28476
			// (set) Token: 0x06009C63 RID: 40035 RVA: 0x000E2D1A File Offset: 0x000E0F1A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006F3D RID: 28477
			// (set) Token: 0x06009C64 RID: 40036 RVA: 0x000E2D32 File Offset: 0x000E0F32
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006F3E RID: 28478
			// (set) Token: 0x06009C65 RID: 40037 RVA: 0x000E2D4A File Offset: 0x000E0F4A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006F3F RID: 28479
			// (set) Token: 0x06009C66 RID: 40038 RVA: 0x000E2D62 File Offset: 0x000E0F62
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C70 RID: 3184
		public class PublicFolderParameters : ParametersBase
		{
			// Token: 0x17006F40 RID: 28480
			// (set) Token: 0x06009C68 RID: 40040 RVA: 0x000E2D82 File Offset: 0x000E0F82
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17006F41 RID: 28481
			// (set) Token: 0x06009C69 RID: 40041 RVA: 0x000E2D9A File Offset: 0x000E0F9A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006F42 RID: 28482
			// (set) Token: 0x06009C6A RID: 40042 RVA: 0x000E2DB8 File Offset: 0x000E0FB8
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006F43 RID: 28483
			// (set) Token: 0x06009C6B RID: 40043 RVA: 0x000E2DD0 File Offset: 0x000E0FD0
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17006F44 RID: 28484
			// (set) Token: 0x06009C6C RID: 40044 RVA: 0x000E2DE8 File Offset: 0x000E0FE8
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006F45 RID: 28485
			// (set) Token: 0x06009C6D RID: 40045 RVA: 0x000E2E00 File Offset: 0x000E1000
			public virtual SwitchParameter PreserveEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["PreserveEmailAddresses"] = value;
				}
			}

			// Token: 0x17006F46 RID: 28486
			// (set) Token: 0x06009C6E RID: 40046 RVA: 0x000E2E18 File Offset: 0x000E1018
			public virtual SwitchParameter PreventRecordingPreviousDatabase
			{
				set
				{
					base.PowerSharpParameters["PreventRecordingPreviousDatabase"] = value;
				}
			}

			// Token: 0x17006F47 RID: 28487
			// (set) Token: 0x06009C6F RID: 40047 RVA: 0x000E2E30 File Offset: 0x000E1030
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006F48 RID: 28488
			// (set) Token: 0x06009C70 RID: 40048 RVA: 0x000E2E43 File Offset: 0x000E1043
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006F49 RID: 28489
			// (set) Token: 0x06009C71 RID: 40049 RVA: 0x000E2E5B File Offset: 0x000E105B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006F4A RID: 28490
			// (set) Token: 0x06009C72 RID: 40050 RVA: 0x000E2E73 File Offset: 0x000E1073
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006F4B RID: 28491
			// (set) Token: 0x06009C73 RID: 40051 RVA: 0x000E2E8B File Offset: 0x000E108B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006F4C RID: 28492
			// (set) Token: 0x06009C74 RID: 40052 RVA: 0x000E2EA3 File Offset: 0x000E10A3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006F4D RID: 28493
			// (set) Token: 0x06009C75 RID: 40053 RVA: 0x000E2EBB File Offset: 0x000E10BB
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
