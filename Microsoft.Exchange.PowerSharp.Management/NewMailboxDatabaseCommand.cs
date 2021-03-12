using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005F5 RID: 1525
	public class NewMailboxDatabaseCommand : SyntheticCommandWithPipelineInput<MailboxDatabase, MailboxDatabase>
	{
		// Token: 0x06004E4D RID: 20045 RVA: 0x0007CC7C File Offset: 0x0007AE7C
		private NewMailboxDatabaseCommand() : base("New-MailboxDatabase")
		{
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x0007CC89 File Offset: 0x0007AE89
		public NewMailboxDatabaseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x0007CC98 File Offset: 0x0007AE98
		public virtual NewMailboxDatabaseCommand SetParameters(NewMailboxDatabaseCommand.NonRecoveryParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x0007CCA2 File Offset: 0x0007AEA2
		public virtual NewMailboxDatabaseCommand SetParameters(NewMailboxDatabaseCommand.RecoveryParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x0007CCAC File Offset: 0x0007AEAC
		public virtual NewMailboxDatabaseCommand SetParameters(NewMailboxDatabaseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005F6 RID: 1526
		public class NonRecoveryParameters : ParametersBase
		{
			// Token: 0x17002E1C RID: 11804
			// (set) Token: 0x06004E52 RID: 20050 RVA: 0x0007CCB6 File Offset: 0x0007AEB6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002E1D RID: 11805
			// (set) Token: 0x06004E53 RID: 20051 RVA: 0x0007CCC9 File Offset: 0x0007AEC9
			public virtual DatabaseIdParameter PublicFolderDatabase
			{
				set
				{
					base.PowerSharpParameters["PublicFolderDatabase"] = value;
				}
			}

			// Token: 0x17002E1E RID: 11806
			// (set) Token: 0x06004E54 RID: 20052 RVA: 0x0007CCDC File Offset: 0x0007AEDC
			public virtual OfflineAddressBookIdParameter OfflineAddressBook
			{
				set
				{
					base.PowerSharpParameters["OfflineAddressBook"] = value;
				}
			}

			// Token: 0x17002E1F RID: 11807
			// (set) Token: 0x06004E55 RID: 20053 RVA: 0x0007CCEF File Offset: 0x0007AEEF
			public virtual bool IsExcludedFromProvisioning
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromProvisioning"] = value;
				}
			}

			// Token: 0x17002E20 RID: 11808
			// (set) Token: 0x06004E56 RID: 20054 RVA: 0x0007CD07 File Offset: 0x0007AF07
			public virtual bool IsSuspendedFromProvisioning
			{
				set
				{
					base.PowerSharpParameters["IsSuspendedFromProvisioning"] = value;
				}
			}

			// Token: 0x17002E21 RID: 11809
			// (set) Token: 0x06004E57 RID: 20055 RVA: 0x0007CD1F File Offset: 0x0007AF1F
			public virtual SwitchParameter IsExcludedFromInitialProvisioning
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromInitialProvisioning"] = value;
				}
			}

			// Token: 0x17002E22 RID: 11810
			// (set) Token: 0x06004E58 RID: 20056 RVA: 0x0007CD37 File Offset: 0x0007AF37
			public virtual bool AutoDagExcludeFromMonitoring
			{
				set
				{
					base.PowerSharpParameters["AutoDagExcludeFromMonitoring"] = value;
				}
			}

			// Token: 0x17002E23 RID: 11811
			// (set) Token: 0x06004E59 RID: 20057 RVA: 0x0007CD4F File Offset: 0x0007AF4F
			public virtual MailboxProvisioningAttributes MailboxProvisioningAttributes
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningAttributes"] = value;
				}
			}

			// Token: 0x17002E24 RID: 11812
			// (set) Token: 0x06004E5A RID: 20058 RVA: 0x0007CD62 File Offset: 0x0007AF62
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002E25 RID: 11813
			// (set) Token: 0x06004E5B RID: 20059 RVA: 0x0007CD75 File Offset: 0x0007AF75
			public virtual EdbFilePath EdbFilePath
			{
				set
				{
					base.PowerSharpParameters["EdbFilePath"] = value;
				}
			}

			// Token: 0x17002E26 RID: 11814
			// (set) Token: 0x06004E5C RID: 20060 RVA: 0x0007CD88 File Offset: 0x0007AF88
			public virtual NonRootLocalLongFullPath LogFolderPath
			{
				set
				{
					base.PowerSharpParameters["LogFolderPath"] = value;
				}
			}

			// Token: 0x17002E27 RID: 11815
			// (set) Token: 0x06004E5D RID: 20061 RVA: 0x0007CD9B File Offset: 0x0007AF9B
			public virtual SwitchParameter SkipDatabaseLogFolderCreation
			{
				set
				{
					base.PowerSharpParameters["SkipDatabaseLogFolderCreation"] = value;
				}
			}

			// Token: 0x17002E28 RID: 11816
			// (set) Token: 0x06004E5E RID: 20062 RVA: 0x0007CDB3 File Offset: 0x0007AFB3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E29 RID: 11817
			// (set) Token: 0x06004E5F RID: 20063 RVA: 0x0007CDC6 File Offset: 0x0007AFC6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002E2A RID: 11818
			// (set) Token: 0x06004E60 RID: 20064 RVA: 0x0007CDDE File Offset: 0x0007AFDE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002E2B RID: 11819
			// (set) Token: 0x06004E61 RID: 20065 RVA: 0x0007CDF6 File Offset: 0x0007AFF6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002E2C RID: 11820
			// (set) Token: 0x06004E62 RID: 20066 RVA: 0x0007CE0E File Offset: 0x0007B00E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002E2D RID: 11821
			// (set) Token: 0x06004E63 RID: 20067 RVA: 0x0007CE26 File Offset: 0x0007B026
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020005F7 RID: 1527
		public class RecoveryParameters : ParametersBase
		{
			// Token: 0x17002E2E RID: 11822
			// (set) Token: 0x06004E65 RID: 20069 RVA: 0x0007CE46 File Offset: 0x0007B046
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002E2F RID: 11823
			// (set) Token: 0x06004E66 RID: 20070 RVA: 0x0007CE59 File Offset: 0x0007B059
			public virtual SwitchParameter Recovery
			{
				set
				{
					base.PowerSharpParameters["Recovery"] = value;
				}
			}

			// Token: 0x17002E30 RID: 11824
			// (set) Token: 0x06004E67 RID: 20071 RVA: 0x0007CE71 File Offset: 0x0007B071
			public virtual MailboxProvisioningAttributes MailboxProvisioningAttributes
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningAttributes"] = value;
				}
			}

			// Token: 0x17002E31 RID: 11825
			// (set) Token: 0x06004E68 RID: 20072 RVA: 0x0007CE84 File Offset: 0x0007B084
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002E32 RID: 11826
			// (set) Token: 0x06004E69 RID: 20073 RVA: 0x0007CE97 File Offset: 0x0007B097
			public virtual EdbFilePath EdbFilePath
			{
				set
				{
					base.PowerSharpParameters["EdbFilePath"] = value;
				}
			}

			// Token: 0x17002E33 RID: 11827
			// (set) Token: 0x06004E6A RID: 20074 RVA: 0x0007CEAA File Offset: 0x0007B0AA
			public virtual NonRootLocalLongFullPath LogFolderPath
			{
				set
				{
					base.PowerSharpParameters["LogFolderPath"] = value;
				}
			}

			// Token: 0x17002E34 RID: 11828
			// (set) Token: 0x06004E6B RID: 20075 RVA: 0x0007CEBD File Offset: 0x0007B0BD
			public virtual SwitchParameter SkipDatabaseLogFolderCreation
			{
				set
				{
					base.PowerSharpParameters["SkipDatabaseLogFolderCreation"] = value;
				}
			}

			// Token: 0x17002E35 RID: 11829
			// (set) Token: 0x06004E6C RID: 20076 RVA: 0x0007CED5 File Offset: 0x0007B0D5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E36 RID: 11830
			// (set) Token: 0x06004E6D RID: 20077 RVA: 0x0007CEE8 File Offset: 0x0007B0E8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002E37 RID: 11831
			// (set) Token: 0x06004E6E RID: 20078 RVA: 0x0007CF00 File Offset: 0x0007B100
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002E38 RID: 11832
			// (set) Token: 0x06004E6F RID: 20079 RVA: 0x0007CF18 File Offset: 0x0007B118
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002E39 RID: 11833
			// (set) Token: 0x06004E70 RID: 20080 RVA: 0x0007CF30 File Offset: 0x0007B130
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002E3A RID: 11834
			// (set) Token: 0x06004E71 RID: 20081 RVA: 0x0007CF48 File Offset: 0x0007B148
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020005F8 RID: 1528
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002E3B RID: 11835
			// (set) Token: 0x06004E73 RID: 20083 RVA: 0x0007CF68 File Offset: 0x0007B168
			public virtual MailboxProvisioningAttributes MailboxProvisioningAttributes
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningAttributes"] = value;
				}
			}

			// Token: 0x17002E3C RID: 11836
			// (set) Token: 0x06004E74 RID: 20084 RVA: 0x0007CF7B File Offset: 0x0007B17B
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002E3D RID: 11837
			// (set) Token: 0x06004E75 RID: 20085 RVA: 0x0007CF8E File Offset: 0x0007B18E
			public virtual EdbFilePath EdbFilePath
			{
				set
				{
					base.PowerSharpParameters["EdbFilePath"] = value;
				}
			}

			// Token: 0x17002E3E RID: 11838
			// (set) Token: 0x06004E76 RID: 20086 RVA: 0x0007CFA1 File Offset: 0x0007B1A1
			public virtual NonRootLocalLongFullPath LogFolderPath
			{
				set
				{
					base.PowerSharpParameters["LogFolderPath"] = value;
				}
			}

			// Token: 0x17002E3F RID: 11839
			// (set) Token: 0x06004E77 RID: 20087 RVA: 0x0007CFB4 File Offset: 0x0007B1B4
			public virtual SwitchParameter SkipDatabaseLogFolderCreation
			{
				set
				{
					base.PowerSharpParameters["SkipDatabaseLogFolderCreation"] = value;
				}
			}

			// Token: 0x17002E40 RID: 11840
			// (set) Token: 0x06004E78 RID: 20088 RVA: 0x0007CFCC File Offset: 0x0007B1CC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002E41 RID: 11841
			// (set) Token: 0x06004E79 RID: 20089 RVA: 0x0007CFDF File Offset: 0x0007B1DF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002E42 RID: 11842
			// (set) Token: 0x06004E7A RID: 20090 RVA: 0x0007CFF7 File Offset: 0x0007B1F7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002E43 RID: 11843
			// (set) Token: 0x06004E7B RID: 20091 RVA: 0x0007D00F File Offset: 0x0007B20F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002E44 RID: 11844
			// (set) Token: 0x06004E7C RID: 20092 RVA: 0x0007D027 File Offset: 0x0007B227
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002E45 RID: 11845
			// (set) Token: 0x06004E7D RID: 20093 RVA: 0x0007D03F File Offset: 0x0007B23F
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
