using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000914 RID: 2324
	public class GetActiveSyncVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADMobileVirtualDirectory, ADMobileVirtualDirectory>
	{
		// Token: 0x060075AA RID: 30122 RVA: 0x000B09F1 File Offset: 0x000AEBF1
		private GetActiveSyncVirtualDirectoryCommand() : base("Get-ActiveSyncVirtualDirectory")
		{
		}

		// Token: 0x060075AB RID: 30123 RVA: 0x000B09FE File Offset: 0x000AEBFE
		public GetActiveSyncVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060075AC RID: 30124 RVA: 0x000B0A0D File Offset: 0x000AEC0D
		public virtual GetActiveSyncVirtualDirectoryCommand SetParameters(GetActiveSyncVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060075AD RID: 30125 RVA: 0x000B0A17 File Offset: 0x000AEC17
		public virtual GetActiveSyncVirtualDirectoryCommand SetParameters(GetActiveSyncVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060075AE RID: 30126 RVA: 0x000B0A21 File Offset: 0x000AEC21
		public virtual GetActiveSyncVirtualDirectoryCommand SetParameters(GetActiveSyncVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000915 RID: 2325
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004F3B RID: 20283
			// (set) Token: 0x060075AF RID: 30127 RVA: 0x000B0A2B File Offset: 0x000AEC2B
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F3C RID: 20284
			// (set) Token: 0x060075B0 RID: 30128 RVA: 0x000B0A43 File Offset: 0x000AEC43
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F3D RID: 20285
			// (set) Token: 0x060075B1 RID: 30129 RVA: 0x000B0A5B File Offset: 0x000AEC5B
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F3E RID: 20286
			// (set) Token: 0x060075B2 RID: 30130 RVA: 0x000B0A73 File Offset: 0x000AEC73
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F3F RID: 20287
			// (set) Token: 0x060075B3 RID: 30131 RVA: 0x000B0A86 File Offset: 0x000AEC86
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F40 RID: 20288
			// (set) Token: 0x060075B4 RID: 30132 RVA: 0x000B0A9E File Offset: 0x000AEC9E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F41 RID: 20289
			// (set) Token: 0x060075B5 RID: 30133 RVA: 0x000B0AB6 File Offset: 0x000AECB6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F42 RID: 20290
			// (set) Token: 0x060075B6 RID: 30134 RVA: 0x000B0ACE File Offset: 0x000AECCE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000916 RID: 2326
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004F43 RID: 20291
			// (set) Token: 0x060075B8 RID: 30136 RVA: 0x000B0AEE File Offset: 0x000AECEE
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004F44 RID: 20292
			// (set) Token: 0x060075B9 RID: 30137 RVA: 0x000B0B01 File Offset: 0x000AED01
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F45 RID: 20293
			// (set) Token: 0x060075BA RID: 30138 RVA: 0x000B0B19 File Offset: 0x000AED19
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F46 RID: 20294
			// (set) Token: 0x060075BB RID: 30139 RVA: 0x000B0B31 File Offset: 0x000AED31
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F47 RID: 20295
			// (set) Token: 0x060075BC RID: 30140 RVA: 0x000B0B49 File Offset: 0x000AED49
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F48 RID: 20296
			// (set) Token: 0x060075BD RID: 30141 RVA: 0x000B0B5C File Offset: 0x000AED5C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F49 RID: 20297
			// (set) Token: 0x060075BE RID: 30142 RVA: 0x000B0B74 File Offset: 0x000AED74
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F4A RID: 20298
			// (set) Token: 0x060075BF RID: 30143 RVA: 0x000B0B8C File Offset: 0x000AED8C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F4B RID: 20299
			// (set) Token: 0x060075C0 RID: 30144 RVA: 0x000B0BA4 File Offset: 0x000AEDA4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000917 RID: 2327
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004F4C RID: 20300
			// (set) Token: 0x060075C2 RID: 30146 RVA: 0x000B0BC4 File Offset: 0x000AEDC4
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004F4D RID: 20301
			// (set) Token: 0x060075C3 RID: 30147 RVA: 0x000B0BD7 File Offset: 0x000AEDD7
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F4E RID: 20302
			// (set) Token: 0x060075C4 RID: 30148 RVA: 0x000B0BEF File Offset: 0x000AEDEF
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F4F RID: 20303
			// (set) Token: 0x060075C5 RID: 30149 RVA: 0x000B0C07 File Offset: 0x000AEE07
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F50 RID: 20304
			// (set) Token: 0x060075C6 RID: 30150 RVA: 0x000B0C1F File Offset: 0x000AEE1F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F51 RID: 20305
			// (set) Token: 0x060075C7 RID: 30151 RVA: 0x000B0C32 File Offset: 0x000AEE32
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F52 RID: 20306
			// (set) Token: 0x060075C8 RID: 30152 RVA: 0x000B0C4A File Offset: 0x000AEE4A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F53 RID: 20307
			// (set) Token: 0x060075C9 RID: 30153 RVA: 0x000B0C62 File Offset: 0x000AEE62
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F54 RID: 20308
			// (set) Token: 0x060075CA RID: 30154 RVA: 0x000B0C7A File Offset: 0x000AEE7A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
