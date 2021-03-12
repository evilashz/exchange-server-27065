using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000FD RID: 253
	public class StartManagedFolderAssistantCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06001EA8 RID: 7848 RVA: 0x0003F857 File Offset: 0x0003DA57
		private StartManagedFolderAssistantCommand() : base("Start-ManagedFolderAssistant")
		{
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x0003F864 File Offset: 0x0003DA64
		public StartManagedFolderAssistantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0003F873 File Offset: 0x0003DA73
		public virtual StartManagedFolderAssistantCommand SetParameters(StartManagedFolderAssistantCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0003F87D File Offset: 0x0003DA7D
		public virtual StartManagedFolderAssistantCommand SetParameters(StartManagedFolderAssistantCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000FE RID: 254
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000867 RID: 2151
			// (set) Token: 0x06001EAC RID: 7852 RVA: 0x0003F887 File Offset: 0x0003DA87
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17000868 RID: 2152
			// (set) Token: 0x06001EAD RID: 7853 RVA: 0x0003F8A5 File Offset: 0x0003DAA5
			public virtual SwitchParameter HoldCleanup
			{
				set
				{
					base.PowerSharpParameters["HoldCleanup"] = value;
				}
			}

			// Token: 0x17000869 RID: 2153
			// (set) Token: 0x06001EAE RID: 7854 RVA: 0x0003F8BD File Offset: 0x0003DABD
			public virtual SwitchParameter EHAHiddenFolderCleanup
			{
				set
				{
					base.PowerSharpParameters["EHAHiddenFolderCleanup"] = value;
				}
			}

			// Token: 0x1700086A RID: 2154
			// (set) Token: 0x06001EAF RID: 7855 RVA: 0x0003F8D5 File Offset: 0x0003DAD5
			public virtual SwitchParameter InactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["InactiveMailbox"] = value;
				}
			}

			// Token: 0x1700086B RID: 2155
			// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x0003F8ED File Offset: 0x0003DAED
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700086C RID: 2156
			// (set) Token: 0x06001EB1 RID: 7857 RVA: 0x0003F900 File Offset: 0x0003DB00
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700086D RID: 2157
			// (set) Token: 0x06001EB2 RID: 7858 RVA: 0x0003F918 File Offset: 0x0003DB18
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700086E RID: 2158
			// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x0003F930 File Offset: 0x0003DB30
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700086F RID: 2159
			// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x0003F948 File Offset: 0x0003DB48
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000870 RID: 2160
			// (set) Token: 0x06001EB5 RID: 7861 RVA: 0x0003F960 File Offset: 0x0003DB60
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000FF RID: 255
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000871 RID: 2161
			// (set) Token: 0x06001EB7 RID: 7863 RVA: 0x0003F980 File Offset: 0x0003DB80
			public virtual SwitchParameter HoldCleanup
			{
				set
				{
					base.PowerSharpParameters["HoldCleanup"] = value;
				}
			}

			// Token: 0x17000872 RID: 2162
			// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x0003F998 File Offset: 0x0003DB98
			public virtual SwitchParameter EHAHiddenFolderCleanup
			{
				set
				{
					base.PowerSharpParameters["EHAHiddenFolderCleanup"] = value;
				}
			}

			// Token: 0x17000873 RID: 2163
			// (set) Token: 0x06001EB9 RID: 7865 RVA: 0x0003F9B0 File Offset: 0x0003DBB0
			public virtual SwitchParameter InactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["InactiveMailbox"] = value;
				}
			}

			// Token: 0x17000874 RID: 2164
			// (set) Token: 0x06001EBA RID: 7866 RVA: 0x0003F9C8 File Offset: 0x0003DBC8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000875 RID: 2165
			// (set) Token: 0x06001EBB RID: 7867 RVA: 0x0003F9DB File Offset: 0x0003DBDB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000876 RID: 2166
			// (set) Token: 0x06001EBC RID: 7868 RVA: 0x0003F9F3 File Offset: 0x0003DBF3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000877 RID: 2167
			// (set) Token: 0x06001EBD RID: 7869 RVA: 0x0003FA0B File Offset: 0x0003DC0B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000878 RID: 2168
			// (set) Token: 0x06001EBE RID: 7870 RVA: 0x0003FA23 File Offset: 0x0003DC23
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000879 RID: 2169
			// (set) Token: 0x06001EBF RID: 7871 RVA: 0x0003FA3B File Offset: 0x0003DC3B
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
