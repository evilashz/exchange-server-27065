using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000606 RID: 1542
	public class UpdateDatabaseSchemaCommand : SyntheticCommandWithPipelineInput<Database, Database>
	{
		// Token: 0x06004F2B RID: 20267 RVA: 0x0007DEA1 File Offset: 0x0007C0A1
		private UpdateDatabaseSchemaCommand() : base("Update-DatabaseSchema")
		{
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x0007DEAE File Offset: 0x0007C0AE
		public UpdateDatabaseSchemaCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x0007DEBD File Offset: 0x0007C0BD
		public virtual UpdateDatabaseSchemaCommand SetParameters(UpdateDatabaseSchemaCommand.VersionsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x0007DEC7 File Offset: 0x0007C0C7
		public virtual UpdateDatabaseSchemaCommand SetParameters(UpdateDatabaseSchemaCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x0007DED1 File Offset: 0x0007C0D1
		public virtual UpdateDatabaseSchemaCommand SetParameters(UpdateDatabaseSchemaCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000607 RID: 1543
		public class VersionsParameters : ParametersBase
		{
			// Token: 0x17002ED8 RID: 11992
			// (set) Token: 0x06004F30 RID: 20272 RVA: 0x0007DEDB File Offset: 0x0007C0DB
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002ED9 RID: 11993
			// (set) Token: 0x06004F31 RID: 20273 RVA: 0x0007DEEE File Offset: 0x0007C0EE
			public virtual ushort MajorVersion
			{
				set
				{
					base.PowerSharpParameters["MajorVersion"] = value;
				}
			}

			// Token: 0x17002EDA RID: 11994
			// (set) Token: 0x06004F32 RID: 20274 RVA: 0x0007DF06 File Offset: 0x0007C106
			public virtual ushort MinorVersion
			{
				set
				{
					base.PowerSharpParameters["MinorVersion"] = value;
				}
			}

			// Token: 0x17002EDB RID: 11995
			// (set) Token: 0x06004F33 RID: 20275 RVA: 0x0007DF1E File Offset: 0x0007C11E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002EDC RID: 11996
			// (set) Token: 0x06004F34 RID: 20276 RVA: 0x0007DF31 File Offset: 0x0007C131
			public virtual bool AllowFileRestore
			{
				set
				{
					base.PowerSharpParameters["AllowFileRestore"] = value;
				}
			}

			// Token: 0x17002EDD RID: 11997
			// (set) Token: 0x06004F35 RID: 20277 RVA: 0x0007DF49 File Offset: 0x0007C149
			public virtual bool BackgroundDatabaseMaintenance
			{
				set
				{
					base.PowerSharpParameters["BackgroundDatabaseMaintenance"] = value;
				}
			}

			// Token: 0x17002EDE RID: 11998
			// (set) Token: 0x06004F36 RID: 20278 RVA: 0x0007DF61 File Offset: 0x0007C161
			public virtual EnhancedTimeSpan DeletedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DeletedItemRetention"] = value;
				}
			}

			// Token: 0x17002EDF RID: 11999
			// (set) Token: 0x06004F37 RID: 20279 RVA: 0x0007DF79 File Offset: 0x0007C179
			public virtual Schedule MaintenanceSchedule
			{
				set
				{
					base.PowerSharpParameters["MaintenanceSchedule"] = value;
				}
			}

			// Token: 0x17002EE0 RID: 12000
			// (set) Token: 0x06004F38 RID: 20280 RVA: 0x0007DF8C File Offset: 0x0007C18C
			public virtual bool MountAtStartup
			{
				set
				{
					base.PowerSharpParameters["MountAtStartup"] = value;
				}
			}

			// Token: 0x17002EE1 RID: 12001
			// (set) Token: 0x06004F39 RID: 20281 RVA: 0x0007DFA4 File Offset: 0x0007C1A4
			public virtual Schedule QuotaNotificationSchedule
			{
				set
				{
					base.PowerSharpParameters["QuotaNotificationSchedule"] = value;
				}
			}

			// Token: 0x17002EE2 RID: 12002
			// (set) Token: 0x06004F3A RID: 20282 RVA: 0x0007DFB7 File Offset: 0x0007C1B7
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x17002EE3 RID: 12003
			// (set) Token: 0x06004F3B RID: 20283 RVA: 0x0007DFCF File Offset: 0x0007C1CF
			public virtual bool AutoDagExcludeFromMonitoring
			{
				set
				{
					base.PowerSharpParameters["AutoDagExcludeFromMonitoring"] = value;
				}
			}

			// Token: 0x17002EE4 RID: 12004
			// (set) Token: 0x06004F3C RID: 20284 RVA: 0x0007DFE7 File Offset: 0x0007C1E7
			public virtual AutoDatabaseMountDial AutoDatabaseMountDial
			{
				set
				{
					base.PowerSharpParameters["AutoDatabaseMountDial"] = value;
				}
			}

			// Token: 0x17002EE5 RID: 12005
			// (set) Token: 0x06004F3D RID: 20285 RVA: 0x0007DFFF File Offset: 0x0007C1FF
			public virtual string DatabaseGroup
			{
				set
				{
					base.PowerSharpParameters["DatabaseGroup"] = value;
				}
			}

			// Token: 0x17002EE6 RID: 12006
			// (set) Token: 0x06004F3E RID: 20286 RVA: 0x0007E012 File Offset: 0x0007C212
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17002EE7 RID: 12007
			// (set) Token: 0x06004F3F RID: 20287 RVA: 0x0007E02A File Offset: 0x0007C22A
			public virtual EnhancedTimeSpan EventHistoryRetentionPeriod
			{
				set
				{
					base.PowerSharpParameters["EventHistoryRetentionPeriod"] = value;
				}
			}

			// Token: 0x17002EE8 RID: 12008
			// (set) Token: 0x06004F40 RID: 20288 RVA: 0x0007E042 File Offset: 0x0007C242
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002EE9 RID: 12009
			// (set) Token: 0x06004F41 RID: 20289 RVA: 0x0007E055 File Offset: 0x0007C255
			public virtual bool CircularLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["CircularLoggingEnabled"] = value;
				}
			}

			// Token: 0x17002EEA RID: 12010
			// (set) Token: 0x06004F42 RID: 20290 RVA: 0x0007E06D File Offset: 0x0007C26D
			public virtual DataMoveReplicationConstraintParameter DataMoveReplicationConstraint
			{
				set
				{
					base.PowerSharpParameters["DataMoveReplicationConstraint"] = value;
				}
			}

			// Token: 0x17002EEB RID: 12011
			// (set) Token: 0x06004F43 RID: 20291 RVA: 0x0007E085 File Offset: 0x0007C285
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002EEC RID: 12012
			// (set) Token: 0x06004F44 RID: 20292 RVA: 0x0007E09D File Offset: 0x0007C29D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002EED RID: 12013
			// (set) Token: 0x06004F45 RID: 20293 RVA: 0x0007E0B5 File Offset: 0x0007C2B5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002EEE RID: 12014
			// (set) Token: 0x06004F46 RID: 20294 RVA: 0x0007E0CD File Offset: 0x0007C2CD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002EEF RID: 12015
			// (set) Token: 0x06004F47 RID: 20295 RVA: 0x0007E0E5 File Offset: 0x0007C2E5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000608 RID: 1544
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002EF0 RID: 12016
			// (set) Token: 0x06004F49 RID: 20297 RVA: 0x0007E105 File Offset: 0x0007C305
			public virtual DatabaseIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002EF1 RID: 12017
			// (set) Token: 0x06004F4A RID: 20298 RVA: 0x0007E118 File Offset: 0x0007C318
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002EF2 RID: 12018
			// (set) Token: 0x06004F4B RID: 20299 RVA: 0x0007E12B File Offset: 0x0007C32B
			public virtual bool AllowFileRestore
			{
				set
				{
					base.PowerSharpParameters["AllowFileRestore"] = value;
				}
			}

			// Token: 0x17002EF3 RID: 12019
			// (set) Token: 0x06004F4C RID: 20300 RVA: 0x0007E143 File Offset: 0x0007C343
			public virtual bool BackgroundDatabaseMaintenance
			{
				set
				{
					base.PowerSharpParameters["BackgroundDatabaseMaintenance"] = value;
				}
			}

			// Token: 0x17002EF4 RID: 12020
			// (set) Token: 0x06004F4D RID: 20301 RVA: 0x0007E15B File Offset: 0x0007C35B
			public virtual EnhancedTimeSpan DeletedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DeletedItemRetention"] = value;
				}
			}

			// Token: 0x17002EF5 RID: 12021
			// (set) Token: 0x06004F4E RID: 20302 RVA: 0x0007E173 File Offset: 0x0007C373
			public virtual Schedule MaintenanceSchedule
			{
				set
				{
					base.PowerSharpParameters["MaintenanceSchedule"] = value;
				}
			}

			// Token: 0x17002EF6 RID: 12022
			// (set) Token: 0x06004F4F RID: 20303 RVA: 0x0007E186 File Offset: 0x0007C386
			public virtual bool MountAtStartup
			{
				set
				{
					base.PowerSharpParameters["MountAtStartup"] = value;
				}
			}

			// Token: 0x17002EF7 RID: 12023
			// (set) Token: 0x06004F50 RID: 20304 RVA: 0x0007E19E File Offset: 0x0007C39E
			public virtual Schedule QuotaNotificationSchedule
			{
				set
				{
					base.PowerSharpParameters["QuotaNotificationSchedule"] = value;
				}
			}

			// Token: 0x17002EF8 RID: 12024
			// (set) Token: 0x06004F51 RID: 20305 RVA: 0x0007E1B1 File Offset: 0x0007C3B1
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x17002EF9 RID: 12025
			// (set) Token: 0x06004F52 RID: 20306 RVA: 0x0007E1C9 File Offset: 0x0007C3C9
			public virtual bool AutoDagExcludeFromMonitoring
			{
				set
				{
					base.PowerSharpParameters["AutoDagExcludeFromMonitoring"] = value;
				}
			}

			// Token: 0x17002EFA RID: 12026
			// (set) Token: 0x06004F53 RID: 20307 RVA: 0x0007E1E1 File Offset: 0x0007C3E1
			public virtual AutoDatabaseMountDial AutoDatabaseMountDial
			{
				set
				{
					base.PowerSharpParameters["AutoDatabaseMountDial"] = value;
				}
			}

			// Token: 0x17002EFB RID: 12027
			// (set) Token: 0x06004F54 RID: 20308 RVA: 0x0007E1F9 File Offset: 0x0007C3F9
			public virtual string DatabaseGroup
			{
				set
				{
					base.PowerSharpParameters["DatabaseGroup"] = value;
				}
			}

			// Token: 0x17002EFC RID: 12028
			// (set) Token: 0x06004F55 RID: 20309 RVA: 0x0007E20C File Offset: 0x0007C40C
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17002EFD RID: 12029
			// (set) Token: 0x06004F56 RID: 20310 RVA: 0x0007E224 File Offset: 0x0007C424
			public virtual EnhancedTimeSpan EventHistoryRetentionPeriod
			{
				set
				{
					base.PowerSharpParameters["EventHistoryRetentionPeriod"] = value;
				}
			}

			// Token: 0x17002EFE RID: 12030
			// (set) Token: 0x06004F57 RID: 20311 RVA: 0x0007E23C File Offset: 0x0007C43C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002EFF RID: 12031
			// (set) Token: 0x06004F58 RID: 20312 RVA: 0x0007E24F File Offset: 0x0007C44F
			public virtual bool CircularLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["CircularLoggingEnabled"] = value;
				}
			}

			// Token: 0x17002F00 RID: 12032
			// (set) Token: 0x06004F59 RID: 20313 RVA: 0x0007E267 File Offset: 0x0007C467
			public virtual DataMoveReplicationConstraintParameter DataMoveReplicationConstraint
			{
				set
				{
					base.PowerSharpParameters["DataMoveReplicationConstraint"] = value;
				}
			}

			// Token: 0x17002F01 RID: 12033
			// (set) Token: 0x06004F5A RID: 20314 RVA: 0x0007E27F File Offset: 0x0007C47F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F02 RID: 12034
			// (set) Token: 0x06004F5B RID: 20315 RVA: 0x0007E297 File Offset: 0x0007C497
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F03 RID: 12035
			// (set) Token: 0x06004F5C RID: 20316 RVA: 0x0007E2AF File Offset: 0x0007C4AF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F04 RID: 12036
			// (set) Token: 0x06004F5D RID: 20317 RVA: 0x0007E2C7 File Offset: 0x0007C4C7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002F05 RID: 12037
			// (set) Token: 0x06004F5E RID: 20318 RVA: 0x0007E2DF File Offset: 0x0007C4DF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000609 RID: 1545
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002F06 RID: 12038
			// (set) Token: 0x06004F60 RID: 20320 RVA: 0x0007E2FF File Offset: 0x0007C4FF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F07 RID: 12039
			// (set) Token: 0x06004F61 RID: 20321 RVA: 0x0007E312 File Offset: 0x0007C512
			public virtual bool AllowFileRestore
			{
				set
				{
					base.PowerSharpParameters["AllowFileRestore"] = value;
				}
			}

			// Token: 0x17002F08 RID: 12040
			// (set) Token: 0x06004F62 RID: 20322 RVA: 0x0007E32A File Offset: 0x0007C52A
			public virtual bool BackgroundDatabaseMaintenance
			{
				set
				{
					base.PowerSharpParameters["BackgroundDatabaseMaintenance"] = value;
				}
			}

			// Token: 0x17002F09 RID: 12041
			// (set) Token: 0x06004F63 RID: 20323 RVA: 0x0007E342 File Offset: 0x0007C542
			public virtual EnhancedTimeSpan DeletedItemRetention
			{
				set
				{
					base.PowerSharpParameters["DeletedItemRetention"] = value;
				}
			}

			// Token: 0x17002F0A RID: 12042
			// (set) Token: 0x06004F64 RID: 20324 RVA: 0x0007E35A File Offset: 0x0007C55A
			public virtual Schedule MaintenanceSchedule
			{
				set
				{
					base.PowerSharpParameters["MaintenanceSchedule"] = value;
				}
			}

			// Token: 0x17002F0B RID: 12043
			// (set) Token: 0x06004F65 RID: 20325 RVA: 0x0007E36D File Offset: 0x0007C56D
			public virtual bool MountAtStartup
			{
				set
				{
					base.PowerSharpParameters["MountAtStartup"] = value;
				}
			}

			// Token: 0x17002F0C RID: 12044
			// (set) Token: 0x06004F66 RID: 20326 RVA: 0x0007E385 File Offset: 0x0007C585
			public virtual Schedule QuotaNotificationSchedule
			{
				set
				{
					base.PowerSharpParameters["QuotaNotificationSchedule"] = value;
				}
			}

			// Token: 0x17002F0D RID: 12045
			// (set) Token: 0x06004F67 RID: 20327 RVA: 0x0007E398 File Offset: 0x0007C598
			public virtual bool RetainDeletedItemsUntilBackup
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsUntilBackup"] = value;
				}
			}

			// Token: 0x17002F0E RID: 12046
			// (set) Token: 0x06004F68 RID: 20328 RVA: 0x0007E3B0 File Offset: 0x0007C5B0
			public virtual bool AutoDagExcludeFromMonitoring
			{
				set
				{
					base.PowerSharpParameters["AutoDagExcludeFromMonitoring"] = value;
				}
			}

			// Token: 0x17002F0F RID: 12047
			// (set) Token: 0x06004F69 RID: 20329 RVA: 0x0007E3C8 File Offset: 0x0007C5C8
			public virtual AutoDatabaseMountDial AutoDatabaseMountDial
			{
				set
				{
					base.PowerSharpParameters["AutoDatabaseMountDial"] = value;
				}
			}

			// Token: 0x17002F10 RID: 12048
			// (set) Token: 0x06004F6A RID: 20330 RVA: 0x0007E3E0 File Offset: 0x0007C5E0
			public virtual string DatabaseGroup
			{
				set
				{
					base.PowerSharpParameters["DatabaseGroup"] = value;
				}
			}

			// Token: 0x17002F11 RID: 12049
			// (set) Token: 0x06004F6B RID: 20331 RVA: 0x0007E3F3 File Offset: 0x0007C5F3
			public virtual Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17002F12 RID: 12050
			// (set) Token: 0x06004F6C RID: 20332 RVA: 0x0007E40B File Offset: 0x0007C60B
			public virtual EnhancedTimeSpan EventHistoryRetentionPeriod
			{
				set
				{
					base.PowerSharpParameters["EventHistoryRetentionPeriod"] = value;
				}
			}

			// Token: 0x17002F13 RID: 12051
			// (set) Token: 0x06004F6D RID: 20333 RVA: 0x0007E423 File Offset: 0x0007C623
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002F14 RID: 12052
			// (set) Token: 0x06004F6E RID: 20334 RVA: 0x0007E436 File Offset: 0x0007C636
			public virtual bool CircularLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["CircularLoggingEnabled"] = value;
				}
			}

			// Token: 0x17002F15 RID: 12053
			// (set) Token: 0x06004F6F RID: 20335 RVA: 0x0007E44E File Offset: 0x0007C64E
			public virtual DataMoveReplicationConstraintParameter DataMoveReplicationConstraint
			{
				set
				{
					base.PowerSharpParameters["DataMoveReplicationConstraint"] = value;
				}
			}

			// Token: 0x17002F16 RID: 12054
			// (set) Token: 0x06004F70 RID: 20336 RVA: 0x0007E466 File Offset: 0x0007C666
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F17 RID: 12055
			// (set) Token: 0x06004F71 RID: 20337 RVA: 0x0007E47E File Offset: 0x0007C67E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F18 RID: 12056
			// (set) Token: 0x06004F72 RID: 20338 RVA: 0x0007E496 File Offset: 0x0007C696
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F19 RID: 12057
			// (set) Token: 0x06004F73 RID: 20339 RVA: 0x0007E4AE File Offset: 0x0007C6AE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002F1A RID: 12058
			// (set) Token: 0x06004F74 RID: 20340 RVA: 0x0007E4C6 File Offset: 0x0007C6C6
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
