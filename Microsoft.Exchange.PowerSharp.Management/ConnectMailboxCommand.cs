using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000213 RID: 531
	public class ConnectMailboxCommand : SyntheticCommandWithPipelineInput<MailboxStatistics, MailboxStatistics>
	{
		// Token: 0x06002990 RID: 10640 RVA: 0x0004DB1F File Offset: 0x0004BD1F
		private ConnectMailboxCommand() : base("Connect-Mailbox")
		{
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0004DB2C File Offset: 0x0004BD2C
		public ConnectMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0004DB3B File Offset: 0x0004BD3B
		public virtual ConnectMailboxCommand SetParameters(ConnectMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x0004DB45 File Offset: 0x0004BD45
		public virtual ConnectMailboxCommand SetParameters(ConnectMailboxCommand.ValidateOnlyParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0004DB4F File Offset: 0x0004BD4F
		public virtual ConnectMailboxCommand SetParameters(ConnectMailboxCommand.LinkedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0004DB59 File Offset: 0x0004BD59
		public virtual ConnectMailboxCommand SetParameters(ConnectMailboxCommand.EquipmentParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x0004DB63 File Offset: 0x0004BD63
		public virtual ConnectMailboxCommand SetParameters(ConnectMailboxCommand.SharedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x0004DB6D File Offset: 0x0004BD6D
		public virtual ConnectMailboxCommand SetParameters(ConnectMailboxCommand.RoomParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0004DB77 File Offset: 0x0004BD77
		public virtual ConnectMailboxCommand SetParameters(ConnectMailboxCommand.UserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000214 RID: 532
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001123 RID: 4387
			// (set) Token: 0x06002999 RID: 10649 RVA: 0x0004DB81 File Offset: 0x0004BD81
			public virtual StoreMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001124 RID: 4388
			// (set) Token: 0x0600299A RID: 10650 RVA: 0x0004DB94 File Offset: 0x0004BD94
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17001125 RID: 4389
			// (set) Token: 0x0600299B RID: 10651 RVA: 0x0004DBA7 File Offset: 0x0004BDA7
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001126 RID: 4390
			// (set) Token: 0x0600299C RID: 10652 RVA: 0x0004DBBF File Offset: 0x0004BDBF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001127 RID: 4391
			// (set) Token: 0x0600299D RID: 10653 RVA: 0x0004DBD2 File Offset: 0x0004BDD2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001128 RID: 4392
			// (set) Token: 0x0600299E RID: 10654 RVA: 0x0004DBEA File Offset: 0x0004BDEA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001129 RID: 4393
			// (set) Token: 0x0600299F RID: 10655 RVA: 0x0004DC02 File Offset: 0x0004BE02
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700112A RID: 4394
			// (set) Token: 0x060029A0 RID: 10656 RVA: 0x0004DC1A File Offset: 0x0004BE1A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700112B RID: 4395
			// (set) Token: 0x060029A1 RID: 10657 RVA: 0x0004DC32 File Offset: 0x0004BE32
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000215 RID: 533
		public class ValidateOnlyParameters : ParametersBase
		{
			// Token: 0x1700112C RID: 4396
			// (set) Token: 0x060029A3 RID: 10659 RVA: 0x0004DC52 File Offset: 0x0004BE52
			public virtual SwitchParameter ValidateOnly
			{
				set
				{
					base.PowerSharpParameters["ValidateOnly"] = value;
				}
			}

			// Token: 0x1700112D RID: 4397
			// (set) Token: 0x060029A4 RID: 10660 RVA: 0x0004DC6A File Offset: 0x0004BE6A
			public virtual StoreMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700112E RID: 4398
			// (set) Token: 0x060029A5 RID: 10661 RVA: 0x0004DC7D File Offset: 0x0004BE7D
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700112F RID: 4399
			// (set) Token: 0x060029A6 RID: 10662 RVA: 0x0004DC90 File Offset: 0x0004BE90
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001130 RID: 4400
			// (set) Token: 0x060029A7 RID: 10663 RVA: 0x0004DCA8 File Offset: 0x0004BEA8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001131 RID: 4401
			// (set) Token: 0x060029A8 RID: 10664 RVA: 0x0004DCBB File Offset: 0x0004BEBB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001132 RID: 4402
			// (set) Token: 0x060029A9 RID: 10665 RVA: 0x0004DCD3 File Offset: 0x0004BED3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001133 RID: 4403
			// (set) Token: 0x060029AA RID: 10666 RVA: 0x0004DCEB File Offset: 0x0004BEEB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001134 RID: 4404
			// (set) Token: 0x060029AB RID: 10667 RVA: 0x0004DD03 File Offset: 0x0004BF03
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001135 RID: 4405
			// (set) Token: 0x060029AC RID: 10668 RVA: 0x0004DD1B File Offset: 0x0004BF1B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000216 RID: 534
		public class LinkedParameters : ParametersBase
		{
			// Token: 0x17001136 RID: 4406
			// (set) Token: 0x060029AE RID: 10670 RVA: 0x0004DD3B File Offset: 0x0004BF3B
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17001137 RID: 4407
			// (set) Token: 0x060029AF RID: 10671 RVA: 0x0004DD59 File Offset: 0x0004BF59
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17001138 RID: 4408
			// (set) Token: 0x060029B0 RID: 10672 RVA: 0x0004DD6C File Offset: 0x0004BF6C
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17001139 RID: 4409
			// (set) Token: 0x060029B1 RID: 10673 RVA: 0x0004DD8A File Offset: 0x0004BF8A
			public virtual Fqdn LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x1700113A RID: 4410
			// (set) Token: 0x060029B2 RID: 10674 RVA: 0x0004DD9D File Offset: 0x0004BF9D
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x1700113B RID: 4411
			// (set) Token: 0x060029B3 RID: 10675 RVA: 0x0004DDB0 File Offset: 0x0004BFB0
			public virtual string ManagedFolderMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700113C RID: 4412
			// (set) Token: 0x060029B4 RID: 10676 RVA: 0x0004DDCE File Offset: 0x0004BFCE
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700113D RID: 4413
			// (set) Token: 0x060029B5 RID: 10677 RVA: 0x0004DDEC File Offset: 0x0004BFEC
			public virtual SwitchParameter ManagedFolderMailboxPolicyAllowed
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderMailboxPolicyAllowed"] = value;
				}
			}

			// Token: 0x1700113E RID: 4414
			// (set) Token: 0x060029B6 RID: 10678 RVA: 0x0004DE04 File Offset: 0x0004C004
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700113F RID: 4415
			// (set) Token: 0x060029B7 RID: 10679 RVA: 0x0004DE22 File Offset: 0x0004C022
			public virtual StoreMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001140 RID: 4416
			// (set) Token: 0x060029B8 RID: 10680 RVA: 0x0004DE35 File Offset: 0x0004C035
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17001141 RID: 4417
			// (set) Token: 0x060029B9 RID: 10681 RVA: 0x0004DE48 File Offset: 0x0004C048
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001142 RID: 4418
			// (set) Token: 0x060029BA RID: 10682 RVA: 0x0004DE60 File Offset: 0x0004C060
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001143 RID: 4419
			// (set) Token: 0x060029BB RID: 10683 RVA: 0x0004DE73 File Offset: 0x0004C073
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001144 RID: 4420
			// (set) Token: 0x060029BC RID: 10684 RVA: 0x0004DE8B File Offset: 0x0004C08B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001145 RID: 4421
			// (set) Token: 0x060029BD RID: 10685 RVA: 0x0004DEA3 File Offset: 0x0004C0A3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001146 RID: 4422
			// (set) Token: 0x060029BE RID: 10686 RVA: 0x0004DEBB File Offset: 0x0004C0BB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001147 RID: 4423
			// (set) Token: 0x060029BF RID: 10687 RVA: 0x0004DED3 File Offset: 0x0004C0D3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000217 RID: 535
		public class EquipmentParameters : ParametersBase
		{
			// Token: 0x17001148 RID: 4424
			// (set) Token: 0x060029C1 RID: 10689 RVA: 0x0004DEF3 File Offset: 0x0004C0F3
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17001149 RID: 4425
			// (set) Token: 0x060029C2 RID: 10690 RVA: 0x0004DF11 File Offset: 0x0004C111
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700114A RID: 4426
			// (set) Token: 0x060029C3 RID: 10691 RVA: 0x0004DF24 File Offset: 0x0004C124
			public virtual SwitchParameter Equipment
			{
				set
				{
					base.PowerSharpParameters["Equipment"] = value;
				}
			}

			// Token: 0x1700114B RID: 4427
			// (set) Token: 0x060029C4 RID: 10692 RVA: 0x0004DF3C File Offset: 0x0004C13C
			public virtual string ManagedFolderMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700114C RID: 4428
			// (set) Token: 0x060029C5 RID: 10693 RVA: 0x0004DF5A File Offset: 0x0004C15A
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700114D RID: 4429
			// (set) Token: 0x060029C6 RID: 10694 RVA: 0x0004DF78 File Offset: 0x0004C178
			public virtual SwitchParameter ManagedFolderMailboxPolicyAllowed
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderMailboxPolicyAllowed"] = value;
				}
			}

			// Token: 0x1700114E RID: 4430
			// (set) Token: 0x060029C7 RID: 10695 RVA: 0x0004DF90 File Offset: 0x0004C190
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700114F RID: 4431
			// (set) Token: 0x060029C8 RID: 10696 RVA: 0x0004DFAE File Offset: 0x0004C1AE
			public virtual StoreMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001150 RID: 4432
			// (set) Token: 0x060029C9 RID: 10697 RVA: 0x0004DFC1 File Offset: 0x0004C1C1
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17001151 RID: 4433
			// (set) Token: 0x060029CA RID: 10698 RVA: 0x0004DFD4 File Offset: 0x0004C1D4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001152 RID: 4434
			// (set) Token: 0x060029CB RID: 10699 RVA: 0x0004DFEC File Offset: 0x0004C1EC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001153 RID: 4435
			// (set) Token: 0x060029CC RID: 10700 RVA: 0x0004DFFF File Offset: 0x0004C1FF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001154 RID: 4436
			// (set) Token: 0x060029CD RID: 10701 RVA: 0x0004E017 File Offset: 0x0004C217
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001155 RID: 4437
			// (set) Token: 0x060029CE RID: 10702 RVA: 0x0004E02F File Offset: 0x0004C22F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001156 RID: 4438
			// (set) Token: 0x060029CF RID: 10703 RVA: 0x0004E047 File Offset: 0x0004C247
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001157 RID: 4439
			// (set) Token: 0x060029D0 RID: 10704 RVA: 0x0004E05F File Offset: 0x0004C25F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000218 RID: 536
		public class SharedParameters : ParametersBase
		{
			// Token: 0x17001158 RID: 4440
			// (set) Token: 0x060029D2 RID: 10706 RVA: 0x0004E07F File Offset: 0x0004C27F
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17001159 RID: 4441
			// (set) Token: 0x060029D3 RID: 10707 RVA: 0x0004E09D File Offset: 0x0004C29D
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700115A RID: 4442
			// (set) Token: 0x060029D4 RID: 10708 RVA: 0x0004E0B0 File Offset: 0x0004C2B0
			public virtual SwitchParameter Shared
			{
				set
				{
					base.PowerSharpParameters["Shared"] = value;
				}
			}

			// Token: 0x1700115B RID: 4443
			// (set) Token: 0x060029D5 RID: 10709 RVA: 0x0004E0C8 File Offset: 0x0004C2C8
			public virtual string ManagedFolderMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700115C RID: 4444
			// (set) Token: 0x060029D6 RID: 10710 RVA: 0x0004E0E6 File Offset: 0x0004C2E6
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700115D RID: 4445
			// (set) Token: 0x060029D7 RID: 10711 RVA: 0x0004E104 File Offset: 0x0004C304
			public virtual SwitchParameter ManagedFolderMailboxPolicyAllowed
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderMailboxPolicyAllowed"] = value;
				}
			}

			// Token: 0x1700115E RID: 4446
			// (set) Token: 0x060029D8 RID: 10712 RVA: 0x0004E11C File Offset: 0x0004C31C
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700115F RID: 4447
			// (set) Token: 0x060029D9 RID: 10713 RVA: 0x0004E13A File Offset: 0x0004C33A
			public virtual StoreMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001160 RID: 4448
			// (set) Token: 0x060029DA RID: 10714 RVA: 0x0004E14D File Offset: 0x0004C34D
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17001161 RID: 4449
			// (set) Token: 0x060029DB RID: 10715 RVA: 0x0004E160 File Offset: 0x0004C360
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001162 RID: 4450
			// (set) Token: 0x060029DC RID: 10716 RVA: 0x0004E178 File Offset: 0x0004C378
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001163 RID: 4451
			// (set) Token: 0x060029DD RID: 10717 RVA: 0x0004E18B File Offset: 0x0004C38B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001164 RID: 4452
			// (set) Token: 0x060029DE RID: 10718 RVA: 0x0004E1A3 File Offset: 0x0004C3A3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001165 RID: 4453
			// (set) Token: 0x060029DF RID: 10719 RVA: 0x0004E1BB File Offset: 0x0004C3BB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001166 RID: 4454
			// (set) Token: 0x060029E0 RID: 10720 RVA: 0x0004E1D3 File Offset: 0x0004C3D3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001167 RID: 4455
			// (set) Token: 0x060029E1 RID: 10721 RVA: 0x0004E1EB File Offset: 0x0004C3EB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000219 RID: 537
		public class RoomParameters : ParametersBase
		{
			// Token: 0x17001168 RID: 4456
			// (set) Token: 0x060029E3 RID: 10723 RVA: 0x0004E20B File Offset: 0x0004C40B
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17001169 RID: 4457
			// (set) Token: 0x060029E4 RID: 10724 RVA: 0x0004E229 File Offset: 0x0004C429
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700116A RID: 4458
			// (set) Token: 0x060029E5 RID: 10725 RVA: 0x0004E23C File Offset: 0x0004C43C
			public virtual SwitchParameter Room
			{
				set
				{
					base.PowerSharpParameters["Room"] = value;
				}
			}

			// Token: 0x1700116B RID: 4459
			// (set) Token: 0x060029E6 RID: 10726 RVA: 0x0004E254 File Offset: 0x0004C454
			public virtual string ManagedFolderMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700116C RID: 4460
			// (set) Token: 0x060029E7 RID: 10727 RVA: 0x0004E272 File Offset: 0x0004C472
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700116D RID: 4461
			// (set) Token: 0x060029E8 RID: 10728 RVA: 0x0004E290 File Offset: 0x0004C490
			public virtual SwitchParameter ManagedFolderMailboxPolicyAllowed
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderMailboxPolicyAllowed"] = value;
				}
			}

			// Token: 0x1700116E RID: 4462
			// (set) Token: 0x060029E9 RID: 10729 RVA: 0x0004E2A8 File Offset: 0x0004C4A8
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700116F RID: 4463
			// (set) Token: 0x060029EA RID: 10730 RVA: 0x0004E2C6 File Offset: 0x0004C4C6
			public virtual StoreMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001170 RID: 4464
			// (set) Token: 0x060029EB RID: 10731 RVA: 0x0004E2D9 File Offset: 0x0004C4D9
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17001171 RID: 4465
			// (set) Token: 0x060029EC RID: 10732 RVA: 0x0004E2EC File Offset: 0x0004C4EC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001172 RID: 4466
			// (set) Token: 0x060029ED RID: 10733 RVA: 0x0004E304 File Offset: 0x0004C504
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001173 RID: 4467
			// (set) Token: 0x060029EE RID: 10734 RVA: 0x0004E317 File Offset: 0x0004C517
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001174 RID: 4468
			// (set) Token: 0x060029EF RID: 10735 RVA: 0x0004E32F File Offset: 0x0004C52F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001175 RID: 4469
			// (set) Token: 0x060029F0 RID: 10736 RVA: 0x0004E347 File Offset: 0x0004C547
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001176 RID: 4470
			// (set) Token: 0x060029F1 RID: 10737 RVA: 0x0004E35F File Offset: 0x0004C55F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001177 RID: 4471
			// (set) Token: 0x060029F2 RID: 10738 RVA: 0x0004E377 File Offset: 0x0004C577
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200021A RID: 538
		public class UserParameters : ParametersBase
		{
			// Token: 0x17001178 RID: 4472
			// (set) Token: 0x060029F4 RID: 10740 RVA: 0x0004E397 File Offset: 0x0004C597
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17001179 RID: 4473
			// (set) Token: 0x060029F5 RID: 10741 RVA: 0x0004E3B5 File Offset: 0x0004C5B5
			public virtual SwitchParameter AllowLegacyDNMismatch
			{
				set
				{
					base.PowerSharpParameters["AllowLegacyDNMismatch"] = value;
				}
			}

			// Token: 0x1700117A RID: 4474
			// (set) Token: 0x060029F6 RID: 10742 RVA: 0x0004E3CD File Offset: 0x0004C5CD
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700117B RID: 4475
			// (set) Token: 0x060029F7 RID: 10743 RVA: 0x0004E3E0 File Offset: 0x0004C5E0
			public virtual string ManagedFolderMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700117C RID: 4476
			// (set) Token: 0x060029F8 RID: 10744 RVA: 0x0004E3FE File Offset: 0x0004C5FE
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700117D RID: 4477
			// (set) Token: 0x060029F9 RID: 10745 RVA: 0x0004E41C File Offset: 0x0004C61C
			public virtual SwitchParameter ManagedFolderMailboxPolicyAllowed
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderMailboxPolicyAllowed"] = value;
				}
			}

			// Token: 0x1700117E RID: 4478
			// (set) Token: 0x060029FA RID: 10746 RVA: 0x0004E434 File Offset: 0x0004C634
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700117F RID: 4479
			// (set) Token: 0x060029FB RID: 10747 RVA: 0x0004E452 File Offset: 0x0004C652
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17001180 RID: 4480
			// (set) Token: 0x060029FC RID: 10748 RVA: 0x0004E470 File Offset: 0x0004C670
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17001181 RID: 4481
			// (set) Token: 0x060029FD RID: 10749 RVA: 0x0004E488 File Offset: 0x0004C688
			public virtual StoreMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001182 RID: 4482
			// (set) Token: 0x060029FE RID: 10750 RVA: 0x0004E49B File Offset: 0x0004C69B
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17001183 RID: 4483
			// (set) Token: 0x060029FF RID: 10751 RVA: 0x0004E4AE File Offset: 0x0004C6AE
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001184 RID: 4484
			// (set) Token: 0x06002A00 RID: 10752 RVA: 0x0004E4C6 File Offset: 0x0004C6C6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001185 RID: 4485
			// (set) Token: 0x06002A01 RID: 10753 RVA: 0x0004E4D9 File Offset: 0x0004C6D9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001186 RID: 4486
			// (set) Token: 0x06002A02 RID: 10754 RVA: 0x0004E4F1 File Offset: 0x0004C6F1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001187 RID: 4487
			// (set) Token: 0x06002A03 RID: 10755 RVA: 0x0004E509 File Offset: 0x0004C709
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001188 RID: 4488
			// (set) Token: 0x06002A04 RID: 10756 RVA: 0x0004E521 File Offset: 0x0004C721
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001189 RID: 4489
			// (set) Token: 0x06002A05 RID: 10757 RVA: 0x0004E539 File Offset: 0x0004C739
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
