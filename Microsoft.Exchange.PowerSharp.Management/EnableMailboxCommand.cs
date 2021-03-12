using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C74 RID: 3188
	public class EnableMailboxCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06009C88 RID: 40072 RVA: 0x000E301F File Offset: 0x000E121F
		private EnableMailboxCommand() : base("Enable-Mailbox")
		{
		}

		// Token: 0x06009C89 RID: 40073 RVA: 0x000E302C File Offset: 0x000E122C
		public EnableMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009C8A RID: 40074 RVA: 0x000E303B File Offset: 0x000E123B
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C8B RID: 40075 RVA: 0x000E3045 File Offset: 0x000E1245
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.LinkedRoomMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C8C RID: 40076 RVA: 0x000E304F File Offset: 0x000E124F
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.AuditLogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C8D RID: 40077 RVA: 0x000E3059 File Offset: 0x000E1259
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.ArbitrationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C8E RID: 40078 RVA: 0x000E3063 File Offset: 0x000E1263
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.PublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C8F RID: 40079 RVA: 0x000E306D File Offset: 0x000E126D
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.DiscoveryParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C90 RID: 40080 RVA: 0x000E3077 File Offset: 0x000E1277
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.EquipmentParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C91 RID: 40081 RVA: 0x000E3081 File Offset: 0x000E1281
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.LinkedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C92 RID: 40082 RVA: 0x000E308B File Offset: 0x000E128B
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.RoomParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C93 RID: 40083 RVA: 0x000E3095 File Offset: 0x000E1295
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.SharedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C94 RID: 40084 RVA: 0x000E309F File Offset: 0x000E129F
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.UserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C95 RID: 40085 RVA: 0x000E30A9 File Offset: 0x000E12A9
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.WindowsLiveIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C96 RID: 40086 RVA: 0x000E30B3 File Offset: 0x000E12B3
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.ArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C97 RID: 40087 RVA: 0x000E30BD File Offset: 0x000E12BD
		public virtual EnableMailboxCommand SetParameters(EnableMailboxCommand.RemoteArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C75 RID: 3189
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006F59 RID: 28505
			// (set) Token: 0x06009C98 RID: 40088 RVA: 0x000E30C7 File Offset: 0x000E12C7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006F5A RID: 28506
			// (set) Token: 0x06009C99 RID: 40089 RVA: 0x000E30E5 File Offset: 0x000E12E5
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F5B RID: 28507
			// (set) Token: 0x06009C9A RID: 40090 RVA: 0x000E3103 File Offset: 0x000E1303
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006F5C RID: 28508
			// (set) Token: 0x06009C9B RID: 40091 RVA: 0x000E311B File Offset: 0x000E131B
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F5D RID: 28509
			// (set) Token: 0x06009C9C RID: 40092 RVA: 0x000E3139 File Offset: 0x000E1339
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F5E RID: 28510
			// (set) Token: 0x06009C9D RID: 40093 RVA: 0x000E3157 File Offset: 0x000E1357
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006F5F RID: 28511
			// (set) Token: 0x06009C9E RID: 40094 RVA: 0x000E316F File Offset: 0x000E136F
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006F60 RID: 28512
			// (set) Token: 0x06009C9F RID: 40095 RVA: 0x000E3182 File Offset: 0x000E1382
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006F61 RID: 28513
			// (set) Token: 0x06009CA0 RID: 40096 RVA: 0x000E3195 File Offset: 0x000E1395
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006F62 RID: 28514
			// (set) Token: 0x06009CA1 RID: 40097 RVA: 0x000E31AD File Offset: 0x000E13AD
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006F63 RID: 28515
			// (set) Token: 0x06009CA2 RID: 40098 RVA: 0x000E31C5 File Offset: 0x000E13C5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006F64 RID: 28516
			// (set) Token: 0x06009CA3 RID: 40099 RVA: 0x000E31D8 File Offset: 0x000E13D8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006F65 RID: 28517
			// (set) Token: 0x06009CA4 RID: 40100 RVA: 0x000E31F0 File Offset: 0x000E13F0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006F66 RID: 28518
			// (set) Token: 0x06009CA5 RID: 40101 RVA: 0x000E3208 File Offset: 0x000E1408
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006F67 RID: 28519
			// (set) Token: 0x06009CA6 RID: 40102 RVA: 0x000E3220 File Offset: 0x000E1420
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006F68 RID: 28520
			// (set) Token: 0x06009CA7 RID: 40103 RVA: 0x000E3238 File Offset: 0x000E1438
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C76 RID: 3190
		public class LinkedRoomMailboxParameters : ParametersBase
		{
			// Token: 0x17006F69 RID: 28521
			// (set) Token: 0x06009CA9 RID: 40105 RVA: 0x000E3258 File Offset: 0x000E1458
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006F6A RID: 28522
			// (set) Token: 0x06009CAA RID: 40106 RVA: 0x000E326B File Offset: 0x000E146B
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17006F6B RID: 28523
			// (set) Token: 0x06009CAB RID: 40107 RVA: 0x000E3283 File Offset: 0x000E1483
			public virtual SwitchParameter LinkedRoom
			{
				set
				{
					base.PowerSharpParameters["LinkedRoom"] = value;
				}
			}

			// Token: 0x17006F6C RID: 28524
			// (set) Token: 0x06009CAC RID: 40108 RVA: 0x000E329B File Offset: 0x000E149B
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006F6D RID: 28525
			// (set) Token: 0x06009CAD RID: 40109 RVA: 0x000E32B9 File Offset: 0x000E14B9
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17006F6E RID: 28526
			// (set) Token: 0x06009CAE RID: 40110 RVA: 0x000E32CC File Offset: 0x000E14CC
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17006F6F RID: 28527
			// (set) Token: 0x06009CAF RID: 40111 RVA: 0x000E32DF File Offset: 0x000E14DF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006F70 RID: 28528
			// (set) Token: 0x06009CB0 RID: 40112 RVA: 0x000E32FD File Offset: 0x000E14FD
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F71 RID: 28529
			// (set) Token: 0x06009CB1 RID: 40113 RVA: 0x000E331B File Offset: 0x000E151B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006F72 RID: 28530
			// (set) Token: 0x06009CB2 RID: 40114 RVA: 0x000E3333 File Offset: 0x000E1533
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F73 RID: 28531
			// (set) Token: 0x06009CB3 RID: 40115 RVA: 0x000E3351 File Offset: 0x000E1551
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F74 RID: 28532
			// (set) Token: 0x06009CB4 RID: 40116 RVA: 0x000E336F File Offset: 0x000E156F
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006F75 RID: 28533
			// (set) Token: 0x06009CB5 RID: 40117 RVA: 0x000E3387 File Offset: 0x000E1587
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006F76 RID: 28534
			// (set) Token: 0x06009CB6 RID: 40118 RVA: 0x000E339A File Offset: 0x000E159A
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006F77 RID: 28535
			// (set) Token: 0x06009CB7 RID: 40119 RVA: 0x000E33AD File Offset: 0x000E15AD
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006F78 RID: 28536
			// (set) Token: 0x06009CB8 RID: 40120 RVA: 0x000E33C5 File Offset: 0x000E15C5
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006F79 RID: 28537
			// (set) Token: 0x06009CB9 RID: 40121 RVA: 0x000E33DD File Offset: 0x000E15DD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006F7A RID: 28538
			// (set) Token: 0x06009CBA RID: 40122 RVA: 0x000E33F0 File Offset: 0x000E15F0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006F7B RID: 28539
			// (set) Token: 0x06009CBB RID: 40123 RVA: 0x000E3408 File Offset: 0x000E1608
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006F7C RID: 28540
			// (set) Token: 0x06009CBC RID: 40124 RVA: 0x000E3420 File Offset: 0x000E1620
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006F7D RID: 28541
			// (set) Token: 0x06009CBD RID: 40125 RVA: 0x000E3438 File Offset: 0x000E1638
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006F7E RID: 28542
			// (set) Token: 0x06009CBE RID: 40126 RVA: 0x000E3450 File Offset: 0x000E1650
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C77 RID: 3191
		public class AuditLogParameters : ParametersBase
		{
			// Token: 0x17006F7F RID: 28543
			// (set) Token: 0x06009CC0 RID: 40128 RVA: 0x000E3470 File Offset: 0x000E1670
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006F80 RID: 28544
			// (set) Token: 0x06009CC1 RID: 40129 RVA: 0x000E3483 File Offset: 0x000E1683
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17006F81 RID: 28545
			// (set) Token: 0x06009CC2 RID: 40130 RVA: 0x000E349B File Offset: 0x000E169B
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006F82 RID: 28546
			// (set) Token: 0x06009CC3 RID: 40131 RVA: 0x000E34B3 File Offset: 0x000E16B3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006F83 RID: 28547
			// (set) Token: 0x06009CC4 RID: 40132 RVA: 0x000E34D1 File Offset: 0x000E16D1
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F84 RID: 28548
			// (set) Token: 0x06009CC5 RID: 40133 RVA: 0x000E34EF File Offset: 0x000E16EF
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006F85 RID: 28549
			// (set) Token: 0x06009CC6 RID: 40134 RVA: 0x000E3507 File Offset: 0x000E1707
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F86 RID: 28550
			// (set) Token: 0x06009CC7 RID: 40135 RVA: 0x000E3525 File Offset: 0x000E1725
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F87 RID: 28551
			// (set) Token: 0x06009CC8 RID: 40136 RVA: 0x000E3543 File Offset: 0x000E1743
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006F88 RID: 28552
			// (set) Token: 0x06009CC9 RID: 40137 RVA: 0x000E355B File Offset: 0x000E175B
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006F89 RID: 28553
			// (set) Token: 0x06009CCA RID: 40138 RVA: 0x000E356E File Offset: 0x000E176E
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006F8A RID: 28554
			// (set) Token: 0x06009CCB RID: 40139 RVA: 0x000E3581 File Offset: 0x000E1781
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006F8B RID: 28555
			// (set) Token: 0x06009CCC RID: 40140 RVA: 0x000E3599 File Offset: 0x000E1799
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006F8C RID: 28556
			// (set) Token: 0x06009CCD RID: 40141 RVA: 0x000E35B1 File Offset: 0x000E17B1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006F8D RID: 28557
			// (set) Token: 0x06009CCE RID: 40142 RVA: 0x000E35C4 File Offset: 0x000E17C4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006F8E RID: 28558
			// (set) Token: 0x06009CCF RID: 40143 RVA: 0x000E35DC File Offset: 0x000E17DC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006F8F RID: 28559
			// (set) Token: 0x06009CD0 RID: 40144 RVA: 0x000E35F4 File Offset: 0x000E17F4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006F90 RID: 28560
			// (set) Token: 0x06009CD1 RID: 40145 RVA: 0x000E360C File Offset: 0x000E180C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006F91 RID: 28561
			// (set) Token: 0x06009CD2 RID: 40146 RVA: 0x000E3624 File Offset: 0x000E1824
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C78 RID: 3192
		public class ArbitrationParameters : ParametersBase
		{
			// Token: 0x17006F92 RID: 28562
			// (set) Token: 0x06009CD4 RID: 40148 RVA: 0x000E3644 File Offset: 0x000E1844
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006F93 RID: 28563
			// (set) Token: 0x06009CD5 RID: 40149 RVA: 0x000E3657 File Offset: 0x000E1857
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17006F94 RID: 28564
			// (set) Token: 0x06009CD6 RID: 40150 RVA: 0x000E366F File Offset: 0x000E186F
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17006F95 RID: 28565
			// (set) Token: 0x06009CD7 RID: 40151 RVA: 0x000E3687 File Offset: 0x000E1887
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006F96 RID: 28566
			// (set) Token: 0x06009CD8 RID: 40152 RVA: 0x000E36A5 File Offset: 0x000E18A5
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F97 RID: 28567
			// (set) Token: 0x06009CD9 RID: 40153 RVA: 0x000E36C3 File Offset: 0x000E18C3
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006F98 RID: 28568
			// (set) Token: 0x06009CDA RID: 40154 RVA: 0x000E36DB File Offset: 0x000E18DB
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F99 RID: 28569
			// (set) Token: 0x06009CDB RID: 40155 RVA: 0x000E36F9 File Offset: 0x000E18F9
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006F9A RID: 28570
			// (set) Token: 0x06009CDC RID: 40156 RVA: 0x000E3717 File Offset: 0x000E1917
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006F9B RID: 28571
			// (set) Token: 0x06009CDD RID: 40157 RVA: 0x000E372F File Offset: 0x000E192F
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006F9C RID: 28572
			// (set) Token: 0x06009CDE RID: 40158 RVA: 0x000E3742 File Offset: 0x000E1942
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006F9D RID: 28573
			// (set) Token: 0x06009CDF RID: 40159 RVA: 0x000E3755 File Offset: 0x000E1955
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006F9E RID: 28574
			// (set) Token: 0x06009CE0 RID: 40160 RVA: 0x000E376D File Offset: 0x000E196D
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006F9F RID: 28575
			// (set) Token: 0x06009CE1 RID: 40161 RVA: 0x000E3785 File Offset: 0x000E1985
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006FA0 RID: 28576
			// (set) Token: 0x06009CE2 RID: 40162 RVA: 0x000E3798 File Offset: 0x000E1998
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006FA1 RID: 28577
			// (set) Token: 0x06009CE3 RID: 40163 RVA: 0x000E37B0 File Offset: 0x000E19B0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006FA2 RID: 28578
			// (set) Token: 0x06009CE4 RID: 40164 RVA: 0x000E37C8 File Offset: 0x000E19C8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006FA3 RID: 28579
			// (set) Token: 0x06009CE5 RID: 40165 RVA: 0x000E37E0 File Offset: 0x000E19E0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006FA4 RID: 28580
			// (set) Token: 0x06009CE6 RID: 40166 RVA: 0x000E37F8 File Offset: 0x000E19F8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C79 RID: 3193
		public class PublicFolderParameters : ParametersBase
		{
			// Token: 0x17006FA5 RID: 28581
			// (set) Token: 0x06009CE8 RID: 40168 RVA: 0x000E3818 File Offset: 0x000E1A18
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006FA6 RID: 28582
			// (set) Token: 0x06009CE9 RID: 40169 RVA: 0x000E382B File Offset: 0x000E1A2B
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17006FA7 RID: 28583
			// (set) Token: 0x06009CEA RID: 40170 RVA: 0x000E3843 File Offset: 0x000E1A43
			public virtual SwitchParameter HoldForMigration
			{
				set
				{
					base.PowerSharpParameters["HoldForMigration"] = value;
				}
			}

			// Token: 0x17006FA8 RID: 28584
			// (set) Token: 0x06009CEB RID: 40171 RVA: 0x000E385B File Offset: 0x000E1A5B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006FA9 RID: 28585
			// (set) Token: 0x06009CEC RID: 40172 RVA: 0x000E3879 File Offset: 0x000E1A79
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FAA RID: 28586
			// (set) Token: 0x06009CED RID: 40173 RVA: 0x000E3897 File Offset: 0x000E1A97
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006FAB RID: 28587
			// (set) Token: 0x06009CEE RID: 40174 RVA: 0x000E38AF File Offset: 0x000E1AAF
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FAC RID: 28588
			// (set) Token: 0x06009CEF RID: 40175 RVA: 0x000E38CD File Offset: 0x000E1ACD
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FAD RID: 28589
			// (set) Token: 0x06009CF0 RID: 40176 RVA: 0x000E38EB File Offset: 0x000E1AEB
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006FAE RID: 28590
			// (set) Token: 0x06009CF1 RID: 40177 RVA: 0x000E3903 File Offset: 0x000E1B03
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006FAF RID: 28591
			// (set) Token: 0x06009CF2 RID: 40178 RVA: 0x000E3916 File Offset: 0x000E1B16
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006FB0 RID: 28592
			// (set) Token: 0x06009CF3 RID: 40179 RVA: 0x000E3929 File Offset: 0x000E1B29
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006FB1 RID: 28593
			// (set) Token: 0x06009CF4 RID: 40180 RVA: 0x000E3941 File Offset: 0x000E1B41
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006FB2 RID: 28594
			// (set) Token: 0x06009CF5 RID: 40181 RVA: 0x000E3959 File Offset: 0x000E1B59
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006FB3 RID: 28595
			// (set) Token: 0x06009CF6 RID: 40182 RVA: 0x000E396C File Offset: 0x000E1B6C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006FB4 RID: 28596
			// (set) Token: 0x06009CF7 RID: 40183 RVA: 0x000E3984 File Offset: 0x000E1B84
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006FB5 RID: 28597
			// (set) Token: 0x06009CF8 RID: 40184 RVA: 0x000E399C File Offset: 0x000E1B9C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006FB6 RID: 28598
			// (set) Token: 0x06009CF9 RID: 40185 RVA: 0x000E39B4 File Offset: 0x000E1BB4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006FB7 RID: 28599
			// (set) Token: 0x06009CFA RID: 40186 RVA: 0x000E39CC File Offset: 0x000E1BCC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C7A RID: 3194
		public class DiscoveryParameters : ParametersBase
		{
			// Token: 0x17006FB8 RID: 28600
			// (set) Token: 0x06009CFC RID: 40188 RVA: 0x000E39EC File Offset: 0x000E1BEC
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006FB9 RID: 28601
			// (set) Token: 0x06009CFD RID: 40189 RVA: 0x000E39FF File Offset: 0x000E1BFF
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17006FBA RID: 28602
			// (set) Token: 0x06009CFE RID: 40190 RVA: 0x000E3A17 File Offset: 0x000E1C17
			public virtual SwitchParameter Discovery
			{
				set
				{
					base.PowerSharpParameters["Discovery"] = value;
				}
			}

			// Token: 0x17006FBB RID: 28603
			// (set) Token: 0x06009CFF RID: 40191 RVA: 0x000E3A2F File Offset: 0x000E1C2F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006FBC RID: 28604
			// (set) Token: 0x06009D00 RID: 40192 RVA: 0x000E3A4D File Offset: 0x000E1C4D
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FBD RID: 28605
			// (set) Token: 0x06009D01 RID: 40193 RVA: 0x000E3A6B File Offset: 0x000E1C6B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006FBE RID: 28606
			// (set) Token: 0x06009D02 RID: 40194 RVA: 0x000E3A83 File Offset: 0x000E1C83
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FBF RID: 28607
			// (set) Token: 0x06009D03 RID: 40195 RVA: 0x000E3AA1 File Offset: 0x000E1CA1
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FC0 RID: 28608
			// (set) Token: 0x06009D04 RID: 40196 RVA: 0x000E3ABF File Offset: 0x000E1CBF
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006FC1 RID: 28609
			// (set) Token: 0x06009D05 RID: 40197 RVA: 0x000E3AD7 File Offset: 0x000E1CD7
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006FC2 RID: 28610
			// (set) Token: 0x06009D06 RID: 40198 RVA: 0x000E3AEA File Offset: 0x000E1CEA
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006FC3 RID: 28611
			// (set) Token: 0x06009D07 RID: 40199 RVA: 0x000E3AFD File Offset: 0x000E1CFD
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006FC4 RID: 28612
			// (set) Token: 0x06009D08 RID: 40200 RVA: 0x000E3B15 File Offset: 0x000E1D15
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006FC5 RID: 28613
			// (set) Token: 0x06009D09 RID: 40201 RVA: 0x000E3B2D File Offset: 0x000E1D2D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006FC6 RID: 28614
			// (set) Token: 0x06009D0A RID: 40202 RVA: 0x000E3B40 File Offset: 0x000E1D40
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006FC7 RID: 28615
			// (set) Token: 0x06009D0B RID: 40203 RVA: 0x000E3B58 File Offset: 0x000E1D58
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006FC8 RID: 28616
			// (set) Token: 0x06009D0C RID: 40204 RVA: 0x000E3B70 File Offset: 0x000E1D70
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006FC9 RID: 28617
			// (set) Token: 0x06009D0D RID: 40205 RVA: 0x000E3B88 File Offset: 0x000E1D88
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006FCA RID: 28618
			// (set) Token: 0x06009D0E RID: 40206 RVA: 0x000E3BA0 File Offset: 0x000E1DA0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C7B RID: 3195
		public class EquipmentParameters : ParametersBase
		{
			// Token: 0x17006FCB RID: 28619
			// (set) Token: 0x06009D10 RID: 40208 RVA: 0x000E3BC0 File Offset: 0x000E1DC0
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006FCC RID: 28620
			// (set) Token: 0x06009D11 RID: 40209 RVA: 0x000E3BD3 File Offset: 0x000E1DD3
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17006FCD RID: 28621
			// (set) Token: 0x06009D12 RID: 40210 RVA: 0x000E3BEB File Offset: 0x000E1DEB
			public virtual SwitchParameter Equipment
			{
				set
				{
					base.PowerSharpParameters["Equipment"] = value;
				}
			}

			// Token: 0x17006FCE RID: 28622
			// (set) Token: 0x06009D13 RID: 40211 RVA: 0x000E3C03 File Offset: 0x000E1E03
			public virtual SwitchParameter BypassModerationCheck
			{
				set
				{
					base.PowerSharpParameters["BypassModerationCheck"] = value;
				}
			}

			// Token: 0x17006FCF RID: 28623
			// (set) Token: 0x06009D14 RID: 40212 RVA: 0x000E3C1B File Offset: 0x000E1E1B
			public virtual bool? AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17006FD0 RID: 28624
			// (set) Token: 0x06009D15 RID: 40213 RVA: 0x000E3C33 File Offset: 0x000E1E33
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006FD1 RID: 28625
			// (set) Token: 0x06009D16 RID: 40214 RVA: 0x000E3C51 File Offset: 0x000E1E51
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FD2 RID: 28626
			// (set) Token: 0x06009D17 RID: 40215 RVA: 0x000E3C6F File Offset: 0x000E1E6F
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006FD3 RID: 28627
			// (set) Token: 0x06009D18 RID: 40216 RVA: 0x000E3C87 File Offset: 0x000E1E87
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FD4 RID: 28628
			// (set) Token: 0x06009D19 RID: 40217 RVA: 0x000E3CA5 File Offset: 0x000E1EA5
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FD5 RID: 28629
			// (set) Token: 0x06009D1A RID: 40218 RVA: 0x000E3CC3 File Offset: 0x000E1EC3
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006FD6 RID: 28630
			// (set) Token: 0x06009D1B RID: 40219 RVA: 0x000E3CDB File Offset: 0x000E1EDB
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006FD7 RID: 28631
			// (set) Token: 0x06009D1C RID: 40220 RVA: 0x000E3CEE File Offset: 0x000E1EEE
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006FD8 RID: 28632
			// (set) Token: 0x06009D1D RID: 40221 RVA: 0x000E3D01 File Offset: 0x000E1F01
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006FD9 RID: 28633
			// (set) Token: 0x06009D1E RID: 40222 RVA: 0x000E3D19 File Offset: 0x000E1F19
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006FDA RID: 28634
			// (set) Token: 0x06009D1F RID: 40223 RVA: 0x000E3D31 File Offset: 0x000E1F31
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006FDB RID: 28635
			// (set) Token: 0x06009D20 RID: 40224 RVA: 0x000E3D44 File Offset: 0x000E1F44
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006FDC RID: 28636
			// (set) Token: 0x06009D21 RID: 40225 RVA: 0x000E3D5C File Offset: 0x000E1F5C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006FDD RID: 28637
			// (set) Token: 0x06009D22 RID: 40226 RVA: 0x000E3D74 File Offset: 0x000E1F74
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006FDE RID: 28638
			// (set) Token: 0x06009D23 RID: 40227 RVA: 0x000E3D8C File Offset: 0x000E1F8C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006FDF RID: 28639
			// (set) Token: 0x06009D24 RID: 40228 RVA: 0x000E3DA4 File Offset: 0x000E1FA4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C7C RID: 3196
		public class LinkedParameters : ParametersBase
		{
			// Token: 0x17006FE0 RID: 28640
			// (set) Token: 0x06009D26 RID: 40230 RVA: 0x000E3DC4 File Offset: 0x000E1FC4
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006FE1 RID: 28641
			// (set) Token: 0x06009D27 RID: 40231 RVA: 0x000E3DD7 File Offset: 0x000E1FD7
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17006FE2 RID: 28642
			// (set) Token: 0x06009D28 RID: 40232 RVA: 0x000E3DEF File Offset: 0x000E1FEF
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006FE3 RID: 28643
			// (set) Token: 0x06009D29 RID: 40233 RVA: 0x000E3E0D File Offset: 0x000E200D
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17006FE4 RID: 28644
			// (set) Token: 0x06009D2A RID: 40234 RVA: 0x000E3E20 File Offset: 0x000E2020
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17006FE5 RID: 28645
			// (set) Token: 0x06009D2B RID: 40235 RVA: 0x000E3E33 File Offset: 0x000E2033
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006FE6 RID: 28646
			// (set) Token: 0x06009D2C RID: 40236 RVA: 0x000E3E51 File Offset: 0x000E2051
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FE7 RID: 28647
			// (set) Token: 0x06009D2D RID: 40237 RVA: 0x000E3E6F File Offset: 0x000E206F
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006FE8 RID: 28648
			// (set) Token: 0x06009D2E RID: 40238 RVA: 0x000E3E87 File Offset: 0x000E2087
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FE9 RID: 28649
			// (set) Token: 0x06009D2F RID: 40239 RVA: 0x000E3EA5 File Offset: 0x000E20A5
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FEA RID: 28650
			// (set) Token: 0x06009D30 RID: 40240 RVA: 0x000E3EC3 File Offset: 0x000E20C3
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17006FEB RID: 28651
			// (set) Token: 0x06009D31 RID: 40241 RVA: 0x000E3EDB File Offset: 0x000E20DB
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006FEC RID: 28652
			// (set) Token: 0x06009D32 RID: 40242 RVA: 0x000E3EEE File Offset: 0x000E20EE
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006FED RID: 28653
			// (set) Token: 0x06009D33 RID: 40243 RVA: 0x000E3F01 File Offset: 0x000E2101
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006FEE RID: 28654
			// (set) Token: 0x06009D34 RID: 40244 RVA: 0x000E3F19 File Offset: 0x000E2119
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17006FEF RID: 28655
			// (set) Token: 0x06009D35 RID: 40245 RVA: 0x000E3F31 File Offset: 0x000E2131
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006FF0 RID: 28656
			// (set) Token: 0x06009D36 RID: 40246 RVA: 0x000E3F44 File Offset: 0x000E2144
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006FF1 RID: 28657
			// (set) Token: 0x06009D37 RID: 40247 RVA: 0x000E3F5C File Offset: 0x000E215C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006FF2 RID: 28658
			// (set) Token: 0x06009D38 RID: 40248 RVA: 0x000E3F74 File Offset: 0x000E2174
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006FF3 RID: 28659
			// (set) Token: 0x06009D39 RID: 40249 RVA: 0x000E3F8C File Offset: 0x000E218C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006FF4 RID: 28660
			// (set) Token: 0x06009D3A RID: 40250 RVA: 0x000E3FA4 File Offset: 0x000E21A4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C7D RID: 3197
		public class RoomParameters : ParametersBase
		{
			// Token: 0x17006FF5 RID: 28661
			// (set) Token: 0x06009D3C RID: 40252 RVA: 0x000E3FC4 File Offset: 0x000E21C4
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006FF6 RID: 28662
			// (set) Token: 0x06009D3D RID: 40253 RVA: 0x000E3FD7 File Offset: 0x000E21D7
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17006FF7 RID: 28663
			// (set) Token: 0x06009D3E RID: 40254 RVA: 0x000E3FEF File Offset: 0x000E21EF
			public virtual SwitchParameter Room
			{
				set
				{
					base.PowerSharpParameters["Room"] = value;
				}
			}

			// Token: 0x17006FF8 RID: 28664
			// (set) Token: 0x06009D3F RID: 40255 RVA: 0x000E4007 File Offset: 0x000E2207
			public virtual SwitchParameter BypassModerationCheck
			{
				set
				{
					base.PowerSharpParameters["BypassModerationCheck"] = value;
				}
			}

			// Token: 0x17006FF9 RID: 28665
			// (set) Token: 0x06009D40 RID: 40256 RVA: 0x000E401F File Offset: 0x000E221F
			public virtual bool? AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17006FFA RID: 28666
			// (set) Token: 0x06009D41 RID: 40257 RVA: 0x000E4037 File Offset: 0x000E2237
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006FFB RID: 28667
			// (set) Token: 0x06009D42 RID: 40258 RVA: 0x000E4055 File Offset: 0x000E2255
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FFC RID: 28668
			// (set) Token: 0x06009D43 RID: 40259 RVA: 0x000E4073 File Offset: 0x000E2273
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006FFD RID: 28669
			// (set) Token: 0x06009D44 RID: 40260 RVA: 0x000E408B File Offset: 0x000E228B
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FFE RID: 28670
			// (set) Token: 0x06009D45 RID: 40261 RVA: 0x000E40A9 File Offset: 0x000E22A9
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006FFF RID: 28671
			// (set) Token: 0x06009D46 RID: 40262 RVA: 0x000E40C7 File Offset: 0x000E22C7
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17007000 RID: 28672
			// (set) Token: 0x06009D47 RID: 40263 RVA: 0x000E40DF File Offset: 0x000E22DF
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007001 RID: 28673
			// (set) Token: 0x06009D48 RID: 40264 RVA: 0x000E40F2 File Offset: 0x000E22F2
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007002 RID: 28674
			// (set) Token: 0x06009D49 RID: 40265 RVA: 0x000E4105 File Offset: 0x000E2305
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007003 RID: 28675
			// (set) Token: 0x06009D4A RID: 40266 RVA: 0x000E411D File Offset: 0x000E231D
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007004 RID: 28676
			// (set) Token: 0x06009D4B RID: 40267 RVA: 0x000E4135 File Offset: 0x000E2335
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007005 RID: 28677
			// (set) Token: 0x06009D4C RID: 40268 RVA: 0x000E4148 File Offset: 0x000E2348
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007006 RID: 28678
			// (set) Token: 0x06009D4D RID: 40269 RVA: 0x000E4160 File Offset: 0x000E2360
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007007 RID: 28679
			// (set) Token: 0x06009D4E RID: 40270 RVA: 0x000E4178 File Offset: 0x000E2378
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007008 RID: 28680
			// (set) Token: 0x06009D4F RID: 40271 RVA: 0x000E4190 File Offset: 0x000E2390
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007009 RID: 28681
			// (set) Token: 0x06009D50 RID: 40272 RVA: 0x000E41A8 File Offset: 0x000E23A8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C7E RID: 3198
		public class SharedParameters : ParametersBase
		{
			// Token: 0x1700700A RID: 28682
			// (set) Token: 0x06009D52 RID: 40274 RVA: 0x000E41C8 File Offset: 0x000E23C8
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700700B RID: 28683
			// (set) Token: 0x06009D53 RID: 40275 RVA: 0x000E41DB File Offset: 0x000E23DB
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x1700700C RID: 28684
			// (set) Token: 0x06009D54 RID: 40276 RVA: 0x000E41F3 File Offset: 0x000E23F3
			public virtual SwitchParameter Shared
			{
				set
				{
					base.PowerSharpParameters["Shared"] = value;
				}
			}

			// Token: 0x1700700D RID: 28685
			// (set) Token: 0x06009D55 RID: 40277 RVA: 0x000E420B File Offset: 0x000E240B
			public virtual SwitchParameter BypassModerationCheck
			{
				set
				{
					base.PowerSharpParameters["BypassModerationCheck"] = value;
				}
			}

			// Token: 0x1700700E RID: 28686
			// (set) Token: 0x06009D56 RID: 40278 RVA: 0x000E4223 File Offset: 0x000E2423
			public virtual bool? AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700700F RID: 28687
			// (set) Token: 0x06009D57 RID: 40279 RVA: 0x000E423B File Offset: 0x000E243B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17007010 RID: 28688
			// (set) Token: 0x06009D58 RID: 40280 RVA: 0x000E4259 File Offset: 0x000E2459
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007011 RID: 28689
			// (set) Token: 0x06009D59 RID: 40281 RVA: 0x000E4277 File Offset: 0x000E2477
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007012 RID: 28690
			// (set) Token: 0x06009D5A RID: 40282 RVA: 0x000E428F File Offset: 0x000E248F
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007013 RID: 28691
			// (set) Token: 0x06009D5B RID: 40283 RVA: 0x000E42AD File Offset: 0x000E24AD
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007014 RID: 28692
			// (set) Token: 0x06009D5C RID: 40284 RVA: 0x000E42CB File Offset: 0x000E24CB
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17007015 RID: 28693
			// (set) Token: 0x06009D5D RID: 40285 RVA: 0x000E42E3 File Offset: 0x000E24E3
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007016 RID: 28694
			// (set) Token: 0x06009D5E RID: 40286 RVA: 0x000E42F6 File Offset: 0x000E24F6
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007017 RID: 28695
			// (set) Token: 0x06009D5F RID: 40287 RVA: 0x000E4309 File Offset: 0x000E2509
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007018 RID: 28696
			// (set) Token: 0x06009D60 RID: 40288 RVA: 0x000E4321 File Offset: 0x000E2521
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007019 RID: 28697
			// (set) Token: 0x06009D61 RID: 40289 RVA: 0x000E4339 File Offset: 0x000E2539
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700701A RID: 28698
			// (set) Token: 0x06009D62 RID: 40290 RVA: 0x000E434C File Offset: 0x000E254C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700701B RID: 28699
			// (set) Token: 0x06009D63 RID: 40291 RVA: 0x000E4364 File Offset: 0x000E2564
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700701C RID: 28700
			// (set) Token: 0x06009D64 RID: 40292 RVA: 0x000E437C File Offset: 0x000E257C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700701D RID: 28701
			// (set) Token: 0x06009D65 RID: 40293 RVA: 0x000E4394 File Offset: 0x000E2594
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700701E RID: 28702
			// (set) Token: 0x06009D66 RID: 40294 RVA: 0x000E43AC File Offset: 0x000E25AC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C7F RID: 3199
		public class UserParameters : ParametersBase
		{
			// Token: 0x1700701F RID: 28703
			// (set) Token: 0x06009D68 RID: 40296 RVA: 0x000E43CC File Offset: 0x000E25CC
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007020 RID: 28704
			// (set) Token: 0x06009D69 RID: 40297 RVA: 0x000E43DF File Offset: 0x000E25DF
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007021 RID: 28705
			// (set) Token: 0x06009D6A RID: 40298 RVA: 0x000E43F7 File Offset: 0x000E25F7
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007022 RID: 28706
			// (set) Token: 0x06009D6B RID: 40299 RVA: 0x000E4415 File Offset: 0x000E2615
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007023 RID: 28707
			// (set) Token: 0x06009D6C RID: 40300 RVA: 0x000E4433 File Offset: 0x000E2633
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17007024 RID: 28708
			// (set) Token: 0x06009D6D RID: 40301 RVA: 0x000E444B File Offset: 0x000E264B
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17007025 RID: 28709
			// (set) Token: 0x06009D6E RID: 40302 RVA: 0x000E445E File Offset: 0x000E265E
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17007026 RID: 28710
			// (set) Token: 0x06009D6F RID: 40303 RVA: 0x000E4476 File Offset: 0x000E2676
			public virtual SwitchParameter BypassModerationCheck
			{
				set
				{
					base.PowerSharpParameters["BypassModerationCheck"] = value;
				}
			}

			// Token: 0x17007027 RID: 28711
			// (set) Token: 0x06009D70 RID: 40304 RVA: 0x000E448E File Offset: 0x000E268E
			public virtual bool? AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17007028 RID: 28712
			// (set) Token: 0x06009D71 RID: 40305 RVA: 0x000E44A6 File Offset: 0x000E26A6
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17007029 RID: 28713
			// (set) Token: 0x06009D72 RID: 40306 RVA: 0x000E44B9 File Offset: 0x000E26B9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700702A RID: 28714
			// (set) Token: 0x06009D73 RID: 40307 RVA: 0x000E44D7 File Offset: 0x000E26D7
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700702B RID: 28715
			// (set) Token: 0x06009D74 RID: 40308 RVA: 0x000E44F5 File Offset: 0x000E26F5
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700702C RID: 28716
			// (set) Token: 0x06009D75 RID: 40309 RVA: 0x000E450D File Offset: 0x000E270D
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700702D RID: 28717
			// (set) Token: 0x06009D76 RID: 40310 RVA: 0x000E452B File Offset: 0x000E272B
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700702E RID: 28718
			// (set) Token: 0x06009D77 RID: 40311 RVA: 0x000E4549 File Offset: 0x000E2749
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x1700702F RID: 28719
			// (set) Token: 0x06009D78 RID: 40312 RVA: 0x000E4561 File Offset: 0x000E2761
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007030 RID: 28720
			// (set) Token: 0x06009D79 RID: 40313 RVA: 0x000E4574 File Offset: 0x000E2774
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007031 RID: 28721
			// (set) Token: 0x06009D7A RID: 40314 RVA: 0x000E4587 File Offset: 0x000E2787
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007032 RID: 28722
			// (set) Token: 0x06009D7B RID: 40315 RVA: 0x000E459F File Offset: 0x000E279F
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007033 RID: 28723
			// (set) Token: 0x06009D7C RID: 40316 RVA: 0x000E45B7 File Offset: 0x000E27B7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007034 RID: 28724
			// (set) Token: 0x06009D7D RID: 40317 RVA: 0x000E45CA File Offset: 0x000E27CA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007035 RID: 28725
			// (set) Token: 0x06009D7E RID: 40318 RVA: 0x000E45E2 File Offset: 0x000E27E2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007036 RID: 28726
			// (set) Token: 0x06009D7F RID: 40319 RVA: 0x000E45FA File Offset: 0x000E27FA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007037 RID: 28727
			// (set) Token: 0x06009D80 RID: 40320 RVA: 0x000E4612 File Offset: 0x000E2812
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007038 RID: 28728
			// (set) Token: 0x06009D81 RID: 40321 RVA: 0x000E462A File Offset: 0x000E282A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C80 RID: 3200
		public class WindowsLiveIDParameters : ParametersBase
		{
			// Token: 0x17007039 RID: 28729
			// (set) Token: 0x06009D83 RID: 40323 RVA: 0x000E464A File Offset: 0x000E284A
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700703A RID: 28730
			// (set) Token: 0x06009D84 RID: 40324 RVA: 0x000E465D File Offset: 0x000E285D
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x1700703B RID: 28731
			// (set) Token: 0x06009D85 RID: 40325 RVA: 0x000E4675 File Offset: 0x000E2875
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700703C RID: 28732
			// (set) Token: 0x06009D86 RID: 40326 RVA: 0x000E4693 File Offset: 0x000E2893
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700703D RID: 28733
			// (set) Token: 0x06009D87 RID: 40327 RVA: 0x000E46AB File Offset: 0x000E28AB
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700703E RID: 28734
			// (set) Token: 0x06009D88 RID: 40328 RVA: 0x000E46BE File Offset: 0x000E28BE
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700703F RID: 28735
			// (set) Token: 0x06009D89 RID: 40329 RVA: 0x000E46D6 File Offset: 0x000E28D6
			public virtual SwitchParameter BypassModerationCheck
			{
				set
				{
					base.PowerSharpParameters["BypassModerationCheck"] = value;
				}
			}

			// Token: 0x17007040 RID: 28736
			// (set) Token: 0x06009D8A RID: 40330 RVA: 0x000E46EE File Offset: 0x000E28EE
			public virtual bool? AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x17007041 RID: 28737
			// (set) Token: 0x06009D8B RID: 40331 RVA: 0x000E4706 File Offset: 0x000E2906
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17007042 RID: 28738
			// (set) Token: 0x06009D8C RID: 40332 RVA: 0x000E4719 File Offset: 0x000E2919
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17007043 RID: 28739
			// (set) Token: 0x06009D8D RID: 40333 RVA: 0x000E4737 File Offset: 0x000E2937
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007044 RID: 28740
			// (set) Token: 0x06009D8E RID: 40334 RVA: 0x000E4755 File Offset: 0x000E2955
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007045 RID: 28741
			// (set) Token: 0x06009D8F RID: 40335 RVA: 0x000E476D File Offset: 0x000E296D
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007046 RID: 28742
			// (set) Token: 0x06009D90 RID: 40336 RVA: 0x000E478B File Offset: 0x000E298B
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007047 RID: 28743
			// (set) Token: 0x06009D91 RID: 40337 RVA: 0x000E47A9 File Offset: 0x000E29A9
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17007048 RID: 28744
			// (set) Token: 0x06009D92 RID: 40338 RVA: 0x000E47C1 File Offset: 0x000E29C1
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007049 RID: 28745
			// (set) Token: 0x06009D93 RID: 40339 RVA: 0x000E47D4 File Offset: 0x000E29D4
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700704A RID: 28746
			// (set) Token: 0x06009D94 RID: 40340 RVA: 0x000E47E7 File Offset: 0x000E29E7
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700704B RID: 28747
			// (set) Token: 0x06009D95 RID: 40341 RVA: 0x000E47FF File Offset: 0x000E29FF
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700704C RID: 28748
			// (set) Token: 0x06009D96 RID: 40342 RVA: 0x000E4817 File Offset: 0x000E2A17
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700704D RID: 28749
			// (set) Token: 0x06009D97 RID: 40343 RVA: 0x000E482A File Offset: 0x000E2A2A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700704E RID: 28750
			// (set) Token: 0x06009D98 RID: 40344 RVA: 0x000E4842 File Offset: 0x000E2A42
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700704F RID: 28751
			// (set) Token: 0x06009D99 RID: 40345 RVA: 0x000E485A File Offset: 0x000E2A5A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007050 RID: 28752
			// (set) Token: 0x06009D9A RID: 40346 RVA: 0x000E4872 File Offset: 0x000E2A72
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007051 RID: 28753
			// (set) Token: 0x06009D9B RID: 40347 RVA: 0x000E488A File Offset: 0x000E2A8A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C81 RID: 3201
		public class ArchiveParameters : ParametersBase
		{
			// Token: 0x17007052 RID: 28754
			// (set) Token: 0x06009D9D RID: 40349 RVA: 0x000E48AA File Offset: 0x000E2AAA
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007053 RID: 28755
			// (set) Token: 0x06009D9E RID: 40350 RVA: 0x000E48C2 File Offset: 0x000E2AC2
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17007054 RID: 28756
			// (set) Token: 0x06009D9F RID: 40351 RVA: 0x000E48DA File Offset: 0x000E2ADA
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007055 RID: 28757
			// (set) Token: 0x06009DA0 RID: 40352 RVA: 0x000E48ED File Offset: 0x000E2AED
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17007056 RID: 28758
			// (set) Token: 0x06009DA1 RID: 40353 RVA: 0x000E4900 File Offset: 0x000E2B00
			public virtual SwitchParameter BypassModerationCheck
			{
				set
				{
					base.PowerSharpParameters["BypassModerationCheck"] = value;
				}
			}

			// Token: 0x17007057 RID: 28759
			// (set) Token: 0x06009DA2 RID: 40354 RVA: 0x000E4918 File Offset: 0x000E2B18
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17007058 RID: 28760
			// (set) Token: 0x06009DA3 RID: 40355 RVA: 0x000E4936 File Offset: 0x000E2B36
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007059 RID: 28761
			// (set) Token: 0x06009DA4 RID: 40356 RVA: 0x000E4954 File Offset: 0x000E2B54
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700705A RID: 28762
			// (set) Token: 0x06009DA5 RID: 40357 RVA: 0x000E496C File Offset: 0x000E2B6C
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700705B RID: 28763
			// (set) Token: 0x06009DA6 RID: 40358 RVA: 0x000E498A File Offset: 0x000E2B8A
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700705C RID: 28764
			// (set) Token: 0x06009DA7 RID: 40359 RVA: 0x000E49A8 File Offset: 0x000E2BA8
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x1700705D RID: 28765
			// (set) Token: 0x06009DA8 RID: 40360 RVA: 0x000E49C0 File Offset: 0x000E2BC0
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700705E RID: 28766
			// (set) Token: 0x06009DA9 RID: 40361 RVA: 0x000E49D3 File Offset: 0x000E2BD3
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700705F RID: 28767
			// (set) Token: 0x06009DAA RID: 40362 RVA: 0x000E49E6 File Offset: 0x000E2BE6
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007060 RID: 28768
			// (set) Token: 0x06009DAB RID: 40363 RVA: 0x000E49FE File Offset: 0x000E2BFE
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007061 RID: 28769
			// (set) Token: 0x06009DAC RID: 40364 RVA: 0x000E4A16 File Offset: 0x000E2C16
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007062 RID: 28770
			// (set) Token: 0x06009DAD RID: 40365 RVA: 0x000E4A29 File Offset: 0x000E2C29
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007063 RID: 28771
			// (set) Token: 0x06009DAE RID: 40366 RVA: 0x000E4A41 File Offset: 0x000E2C41
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007064 RID: 28772
			// (set) Token: 0x06009DAF RID: 40367 RVA: 0x000E4A59 File Offset: 0x000E2C59
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007065 RID: 28773
			// (set) Token: 0x06009DB0 RID: 40368 RVA: 0x000E4A71 File Offset: 0x000E2C71
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007066 RID: 28774
			// (set) Token: 0x06009DB1 RID: 40369 RVA: 0x000E4A89 File Offset: 0x000E2C89
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C82 RID: 3202
		public class RemoteArchiveParameters : ParametersBase
		{
			// Token: 0x17007067 RID: 28775
			// (set) Token: 0x06009DB3 RID: 40371 RVA: 0x000E4AA9 File Offset: 0x000E2CA9
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x17007068 RID: 28776
			// (set) Token: 0x06009DB4 RID: 40372 RVA: 0x000E4AC1 File Offset: 0x000E2CC1
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x17007069 RID: 28777
			// (set) Token: 0x06009DB5 RID: 40373 RVA: 0x000E4AD4 File Offset: 0x000E2CD4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x1700706A RID: 28778
			// (set) Token: 0x06009DB6 RID: 40374 RVA: 0x000E4AF2 File Offset: 0x000E2CF2
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700706B RID: 28779
			// (set) Token: 0x06009DB7 RID: 40375 RVA: 0x000E4B10 File Offset: 0x000E2D10
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700706C RID: 28780
			// (set) Token: 0x06009DB8 RID: 40376 RVA: 0x000E4B28 File Offset: 0x000E2D28
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700706D RID: 28781
			// (set) Token: 0x06009DB9 RID: 40377 RVA: 0x000E4B46 File Offset: 0x000E2D46
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700706E RID: 28782
			// (set) Token: 0x06009DBA RID: 40378 RVA: 0x000E4B64 File Offset: 0x000E2D64
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x1700706F RID: 28783
			// (set) Token: 0x06009DBB RID: 40379 RVA: 0x000E4B7C File Offset: 0x000E2D7C
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007070 RID: 28784
			// (set) Token: 0x06009DBC RID: 40380 RVA: 0x000E4B8F File Offset: 0x000E2D8F
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007071 RID: 28785
			// (set) Token: 0x06009DBD RID: 40381 RVA: 0x000E4BA2 File Offset: 0x000E2DA2
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007072 RID: 28786
			// (set) Token: 0x06009DBE RID: 40382 RVA: 0x000E4BBA File Offset: 0x000E2DBA
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007073 RID: 28787
			// (set) Token: 0x06009DBF RID: 40383 RVA: 0x000E4BD2 File Offset: 0x000E2DD2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007074 RID: 28788
			// (set) Token: 0x06009DC0 RID: 40384 RVA: 0x000E4BE5 File Offset: 0x000E2DE5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007075 RID: 28789
			// (set) Token: 0x06009DC1 RID: 40385 RVA: 0x000E4BFD File Offset: 0x000E2DFD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007076 RID: 28790
			// (set) Token: 0x06009DC2 RID: 40386 RVA: 0x000E4C15 File Offset: 0x000E2E15
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007077 RID: 28791
			// (set) Token: 0x06009DC3 RID: 40387 RVA: 0x000E4C2D File Offset: 0x000E2E2D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007078 RID: 28792
			// (set) Token: 0x06009DC4 RID: 40388 RVA: 0x000E4C45 File Offset: 0x000E2E45
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
