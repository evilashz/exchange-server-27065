using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DAC RID: 3500
	public class UndoSyncSoftDeletedMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxIdParameter>
	{
		// Token: 0x0600C968 RID: 51560 RVA: 0x0011FAC8 File Offset: 0x0011DCC8
		private UndoSyncSoftDeletedMailboxCommand() : base("Undo-SyncSoftDeletedMailbox")
		{
		}

		// Token: 0x0600C969 RID: 51561 RVA: 0x0011FAD5 File Offset: 0x0011DCD5
		public UndoSyncSoftDeletedMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600C96A RID: 51562 RVA: 0x0011FAE4 File Offset: 0x0011DCE4
		public virtual UndoSyncSoftDeletedMailboxCommand SetParameters(UndoSyncSoftDeletedMailboxCommand.SoftDeletedMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C96B RID: 51563 RVA: 0x0011FAEE File Offset: 0x0011DCEE
		public virtual UndoSyncSoftDeletedMailboxCommand SetParameters(UndoSyncSoftDeletedMailboxCommand.EnableRoomMailboxAccountParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C96C RID: 51564 RVA: 0x0011FAF8 File Offset: 0x0011DCF8
		public virtual UndoSyncSoftDeletedMailboxCommand SetParameters(UndoSyncSoftDeletedMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C96D RID: 51565 RVA: 0x0011FB02 File Offset: 0x0011DD02
		public virtual UndoSyncSoftDeletedMailboxCommand SetParameters(UndoSyncSoftDeletedMailboxCommand.LinkedRoomMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C96E RID: 51566 RVA: 0x0011FB0C File Offset: 0x0011DD0C
		public virtual UndoSyncSoftDeletedMailboxCommand SetParameters(UndoSyncSoftDeletedMailboxCommand.AuxMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C96F RID: 51567 RVA: 0x0011FB16 File Offset: 0x0011DD16
		public virtual UndoSyncSoftDeletedMailboxCommand SetParameters(UndoSyncSoftDeletedMailboxCommand.PublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C970 RID: 51568 RVA: 0x0011FB20 File Offset: 0x0011DD20
		public virtual UndoSyncSoftDeletedMailboxCommand SetParameters(UndoSyncSoftDeletedMailboxCommand.AuditLogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DAD RID: 3501
		public class SoftDeletedMailboxParameters : ParametersBase
		{
			// Token: 0x170099C9 RID: 39369
			// (set) Token: 0x0600C971 RID: 51569 RVA: 0x0011FB2A File Offset: 0x0011DD2A
			public virtual string SoftDeletedObject
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedObject"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170099CA RID: 39370
			// (set) Token: 0x0600C972 RID: 51570 RVA: 0x0011FB48 File Offset: 0x0011DD48
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170099CB RID: 39371
			// (set) Token: 0x0600C973 RID: 51571 RVA: 0x0011FB5B File Offset: 0x0011DD5B
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x170099CC RID: 39372
			// (set) Token: 0x0600C974 RID: 51572 RVA: 0x0011FB6E File Offset: 0x0011DD6E
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170099CD RID: 39373
			// (set) Token: 0x0600C975 RID: 51573 RVA: 0x0011FB81 File Offset: 0x0011DD81
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x170099CE RID: 39374
			// (set) Token: 0x0600C976 RID: 51574 RVA: 0x0011FB99 File Offset: 0x0011DD99
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170099CF RID: 39375
			// (set) Token: 0x0600C977 RID: 51575 RVA: 0x0011FBAC File Offset: 0x0011DDAC
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170099D0 RID: 39376
			// (set) Token: 0x0600C978 RID: 51576 RVA: 0x0011FBBF File Offset: 0x0011DDBF
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170099D1 RID: 39377
			// (set) Token: 0x0600C979 RID: 51577 RVA: 0x0011FBD2 File Offset: 0x0011DDD2
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170099D2 RID: 39378
			// (set) Token: 0x0600C97A RID: 51578 RVA: 0x0011FBE5 File Offset: 0x0011DDE5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170099D3 RID: 39379
			// (set) Token: 0x0600C97B RID: 51579 RVA: 0x0011FC03 File Offset: 0x0011DE03
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170099D4 RID: 39380
			// (set) Token: 0x0600C97C RID: 51580 RVA: 0x0011FC16 File Offset: 0x0011DE16
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170099D5 RID: 39381
			// (set) Token: 0x0600C97D RID: 51581 RVA: 0x0011FC2E File Offset: 0x0011DE2E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170099D6 RID: 39382
			// (set) Token: 0x0600C97E RID: 51582 RVA: 0x0011FC46 File Offset: 0x0011DE46
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170099D7 RID: 39383
			// (set) Token: 0x0600C97F RID: 51583 RVA: 0x0011FC5E File Offset: 0x0011DE5E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170099D8 RID: 39384
			// (set) Token: 0x0600C980 RID: 51584 RVA: 0x0011FC76 File Offset: 0x0011DE76
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DAE RID: 3502
		public class EnableRoomMailboxAccountParameters : ParametersBase
		{
			// Token: 0x170099D9 RID: 39385
			// (set) Token: 0x0600C982 RID: 51586 RVA: 0x0011FC96 File Offset: 0x0011DE96
			public virtual SecureString RoomMailboxPassword
			{
				set
				{
					base.PowerSharpParameters["RoomMailboxPassword"] = value;
				}
			}

			// Token: 0x170099DA RID: 39386
			// (set) Token: 0x0600C983 RID: 51587 RVA: 0x0011FCA9 File Offset: 0x0011DEA9
			public virtual bool EnableRoomMailboxAccount
			{
				set
				{
					base.PowerSharpParameters["EnableRoomMailboxAccount"] = value;
				}
			}

			// Token: 0x170099DB RID: 39387
			// (set) Token: 0x0600C984 RID: 51588 RVA: 0x0011FCC1 File Offset: 0x0011DEC1
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170099DC RID: 39388
			// (set) Token: 0x0600C985 RID: 51589 RVA: 0x0011FCD4 File Offset: 0x0011DED4
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170099DD RID: 39389
			// (set) Token: 0x0600C986 RID: 51590 RVA: 0x0011FCE7 File Offset: 0x0011DEE7
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170099DE RID: 39390
			// (set) Token: 0x0600C987 RID: 51591 RVA: 0x0011FCFA File Offset: 0x0011DEFA
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170099DF RID: 39391
			// (set) Token: 0x0600C988 RID: 51592 RVA: 0x0011FD0D File Offset: 0x0011DF0D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170099E0 RID: 39392
			// (set) Token: 0x0600C989 RID: 51593 RVA: 0x0011FD2B File Offset: 0x0011DF2B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170099E1 RID: 39393
			// (set) Token: 0x0600C98A RID: 51594 RVA: 0x0011FD3E File Offset: 0x0011DF3E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170099E2 RID: 39394
			// (set) Token: 0x0600C98B RID: 51595 RVA: 0x0011FD56 File Offset: 0x0011DF56
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170099E3 RID: 39395
			// (set) Token: 0x0600C98C RID: 51596 RVA: 0x0011FD6E File Offset: 0x0011DF6E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170099E4 RID: 39396
			// (set) Token: 0x0600C98D RID: 51597 RVA: 0x0011FD86 File Offset: 0x0011DF86
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170099E5 RID: 39397
			// (set) Token: 0x0600C98E RID: 51598 RVA: 0x0011FD9E File Offset: 0x0011DF9E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DAF RID: 3503
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170099E6 RID: 39398
			// (set) Token: 0x0600C990 RID: 51600 RVA: 0x0011FDBE File Offset: 0x0011DFBE
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170099E7 RID: 39399
			// (set) Token: 0x0600C991 RID: 51601 RVA: 0x0011FDD1 File Offset: 0x0011DFD1
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170099E8 RID: 39400
			// (set) Token: 0x0600C992 RID: 51602 RVA: 0x0011FDE4 File Offset: 0x0011DFE4
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170099E9 RID: 39401
			// (set) Token: 0x0600C993 RID: 51603 RVA: 0x0011FDF7 File Offset: 0x0011DFF7
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170099EA RID: 39402
			// (set) Token: 0x0600C994 RID: 51604 RVA: 0x0011FE0A File Offset: 0x0011E00A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170099EB RID: 39403
			// (set) Token: 0x0600C995 RID: 51605 RVA: 0x0011FE28 File Offset: 0x0011E028
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170099EC RID: 39404
			// (set) Token: 0x0600C996 RID: 51606 RVA: 0x0011FE3B File Offset: 0x0011E03B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170099ED RID: 39405
			// (set) Token: 0x0600C997 RID: 51607 RVA: 0x0011FE53 File Offset: 0x0011E053
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170099EE RID: 39406
			// (set) Token: 0x0600C998 RID: 51608 RVA: 0x0011FE6B File Offset: 0x0011E06B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170099EF RID: 39407
			// (set) Token: 0x0600C999 RID: 51609 RVA: 0x0011FE83 File Offset: 0x0011E083
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170099F0 RID: 39408
			// (set) Token: 0x0600C99A RID: 51610 RVA: 0x0011FE9B File Offset: 0x0011E09B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DB0 RID: 3504
		public class LinkedRoomMailboxParameters : ParametersBase
		{
			// Token: 0x170099F1 RID: 39409
			// (set) Token: 0x0600C99C RID: 51612 RVA: 0x0011FEBB File Offset: 0x0011E0BB
			public virtual SwitchParameter LinkedRoom
			{
				set
				{
					base.PowerSharpParameters["LinkedRoom"] = value;
				}
			}

			// Token: 0x170099F2 RID: 39410
			// (set) Token: 0x0600C99D RID: 51613 RVA: 0x0011FED3 File Offset: 0x0011E0D3
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170099F3 RID: 39411
			// (set) Token: 0x0600C99E RID: 51614 RVA: 0x0011FEE6 File Offset: 0x0011E0E6
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170099F4 RID: 39412
			// (set) Token: 0x0600C99F RID: 51615 RVA: 0x0011FEF9 File Offset: 0x0011E0F9
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170099F5 RID: 39413
			// (set) Token: 0x0600C9A0 RID: 51616 RVA: 0x0011FF0C File Offset: 0x0011E10C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170099F6 RID: 39414
			// (set) Token: 0x0600C9A1 RID: 51617 RVA: 0x0011FF1F File Offset: 0x0011E11F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170099F7 RID: 39415
			// (set) Token: 0x0600C9A2 RID: 51618 RVA: 0x0011FF3D File Offset: 0x0011E13D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170099F8 RID: 39416
			// (set) Token: 0x0600C9A3 RID: 51619 RVA: 0x0011FF50 File Offset: 0x0011E150
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170099F9 RID: 39417
			// (set) Token: 0x0600C9A4 RID: 51620 RVA: 0x0011FF68 File Offset: 0x0011E168
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170099FA RID: 39418
			// (set) Token: 0x0600C9A5 RID: 51621 RVA: 0x0011FF80 File Offset: 0x0011E180
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170099FB RID: 39419
			// (set) Token: 0x0600C9A6 RID: 51622 RVA: 0x0011FF98 File Offset: 0x0011E198
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170099FC RID: 39420
			// (set) Token: 0x0600C9A7 RID: 51623 RVA: 0x0011FFB0 File Offset: 0x0011E1B0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DB1 RID: 3505
		public class AuxMailboxParameters : ParametersBase
		{
			// Token: 0x170099FD RID: 39421
			// (set) Token: 0x0600C9A9 RID: 51625 RVA: 0x0011FFD0 File Offset: 0x0011E1D0
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x170099FE RID: 39422
			// (set) Token: 0x0600C9AA RID: 51626 RVA: 0x0011FFE8 File Offset: 0x0011E1E8
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170099FF RID: 39423
			// (set) Token: 0x0600C9AB RID: 51627 RVA: 0x0011FFFB File Offset: 0x0011E1FB
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009A00 RID: 39424
			// (set) Token: 0x0600C9AC RID: 51628 RVA: 0x0012000E File Offset: 0x0011E20E
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009A01 RID: 39425
			// (set) Token: 0x0600C9AD RID: 51629 RVA: 0x00120021 File Offset: 0x0011E221
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009A02 RID: 39426
			// (set) Token: 0x0600C9AE RID: 51630 RVA: 0x00120034 File Offset: 0x0011E234
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009A03 RID: 39427
			// (set) Token: 0x0600C9AF RID: 51631 RVA: 0x00120052 File Offset: 0x0011E252
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009A04 RID: 39428
			// (set) Token: 0x0600C9B0 RID: 51632 RVA: 0x00120065 File Offset: 0x0011E265
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009A05 RID: 39429
			// (set) Token: 0x0600C9B1 RID: 51633 RVA: 0x0012007D File Offset: 0x0011E27D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009A06 RID: 39430
			// (set) Token: 0x0600C9B2 RID: 51634 RVA: 0x00120095 File Offset: 0x0011E295
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009A07 RID: 39431
			// (set) Token: 0x0600C9B3 RID: 51635 RVA: 0x001200AD File Offset: 0x0011E2AD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009A08 RID: 39432
			// (set) Token: 0x0600C9B4 RID: 51636 RVA: 0x001200C5 File Offset: 0x0011E2C5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DB2 RID: 3506
		public class PublicFolderParameters : ParametersBase
		{
			// Token: 0x17009A09 RID: 39433
			// (set) Token: 0x0600C9B6 RID: 51638 RVA: 0x001200E5 File Offset: 0x0011E2E5
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x17009A0A RID: 39434
			// (set) Token: 0x0600C9B7 RID: 51639 RVA: 0x001200FD File Offset: 0x0011E2FD
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009A0B RID: 39435
			// (set) Token: 0x0600C9B8 RID: 51640 RVA: 0x00120110 File Offset: 0x0011E310
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009A0C RID: 39436
			// (set) Token: 0x0600C9B9 RID: 51641 RVA: 0x00120123 File Offset: 0x0011E323
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009A0D RID: 39437
			// (set) Token: 0x0600C9BA RID: 51642 RVA: 0x00120136 File Offset: 0x0011E336
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009A0E RID: 39438
			// (set) Token: 0x0600C9BB RID: 51643 RVA: 0x00120149 File Offset: 0x0011E349
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009A0F RID: 39439
			// (set) Token: 0x0600C9BC RID: 51644 RVA: 0x00120167 File Offset: 0x0011E367
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009A10 RID: 39440
			// (set) Token: 0x0600C9BD RID: 51645 RVA: 0x0012017A File Offset: 0x0011E37A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009A11 RID: 39441
			// (set) Token: 0x0600C9BE RID: 51646 RVA: 0x00120192 File Offset: 0x0011E392
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009A12 RID: 39442
			// (set) Token: 0x0600C9BF RID: 51647 RVA: 0x001201AA File Offset: 0x0011E3AA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009A13 RID: 39443
			// (set) Token: 0x0600C9C0 RID: 51648 RVA: 0x001201C2 File Offset: 0x0011E3C2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009A14 RID: 39444
			// (set) Token: 0x0600C9C1 RID: 51649 RVA: 0x001201DA File Offset: 0x0011E3DA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DB3 RID: 3507
		public class AuditLogParameters : ParametersBase
		{
			// Token: 0x17009A15 RID: 39445
			// (set) Token: 0x0600C9C3 RID: 51651 RVA: 0x001201FA File Offset: 0x0011E3FA
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17009A16 RID: 39446
			// (set) Token: 0x0600C9C4 RID: 51652 RVA: 0x00120212 File Offset: 0x0011E412
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17009A17 RID: 39447
			// (set) Token: 0x0600C9C5 RID: 51653 RVA: 0x00120225 File Offset: 0x0011E425
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17009A18 RID: 39448
			// (set) Token: 0x0600C9C6 RID: 51654 RVA: 0x00120238 File Offset: 0x0011E438
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17009A19 RID: 39449
			// (set) Token: 0x0600C9C7 RID: 51655 RVA: 0x0012024B File Offset: 0x0011E44B
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17009A1A RID: 39450
			// (set) Token: 0x0600C9C8 RID: 51656 RVA: 0x0012025E File Offset: 0x0011E45E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17009A1B RID: 39451
			// (set) Token: 0x0600C9C9 RID: 51657 RVA: 0x0012027C File Offset: 0x0011E47C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009A1C RID: 39452
			// (set) Token: 0x0600C9CA RID: 51658 RVA: 0x0012028F File Offset: 0x0011E48F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009A1D RID: 39453
			// (set) Token: 0x0600C9CB RID: 51659 RVA: 0x001202A7 File Offset: 0x0011E4A7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009A1E RID: 39454
			// (set) Token: 0x0600C9CC RID: 51660 RVA: 0x001202BF File Offset: 0x0011E4BF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009A1F RID: 39455
			// (set) Token: 0x0600C9CD RID: 51661 RVA: 0x001202D7 File Offset: 0x0011E4D7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009A20 RID: 39456
			// (set) Token: 0x0600C9CE RID: 51662 RVA: 0x001202EF File Offset: 0x0011E4EF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
