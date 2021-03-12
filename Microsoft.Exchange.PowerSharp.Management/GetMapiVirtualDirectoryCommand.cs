using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000910 RID: 2320
	public class GetMapiVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADMapiVirtualDirectory, ADMapiVirtualDirectory>
	{
		// Token: 0x06007588 RID: 30088 RVA: 0x000B0748 File Offset: 0x000AE948
		private GetMapiVirtualDirectoryCommand() : base("Get-MapiVirtualDirectory")
		{
		}

		// Token: 0x06007589 RID: 30089 RVA: 0x000B0755 File Offset: 0x000AE955
		public GetMapiVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600758A RID: 30090 RVA: 0x000B0764 File Offset: 0x000AE964
		public virtual GetMapiVirtualDirectoryCommand SetParameters(GetMapiVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600758B RID: 30091 RVA: 0x000B076E File Offset: 0x000AE96E
		public virtual GetMapiVirtualDirectoryCommand SetParameters(GetMapiVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600758C RID: 30092 RVA: 0x000B0778 File Offset: 0x000AE978
		public virtual GetMapiVirtualDirectoryCommand SetParameters(GetMapiVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000911 RID: 2321
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004F21 RID: 20257
			// (set) Token: 0x0600758D RID: 30093 RVA: 0x000B0782 File Offset: 0x000AE982
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F22 RID: 20258
			// (set) Token: 0x0600758E RID: 30094 RVA: 0x000B079A File Offset: 0x000AE99A
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F23 RID: 20259
			// (set) Token: 0x0600758F RID: 30095 RVA: 0x000B07B2 File Offset: 0x000AE9B2
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F24 RID: 20260
			// (set) Token: 0x06007590 RID: 30096 RVA: 0x000B07CA File Offset: 0x000AE9CA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F25 RID: 20261
			// (set) Token: 0x06007591 RID: 30097 RVA: 0x000B07DD File Offset: 0x000AE9DD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F26 RID: 20262
			// (set) Token: 0x06007592 RID: 30098 RVA: 0x000B07F5 File Offset: 0x000AE9F5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F27 RID: 20263
			// (set) Token: 0x06007593 RID: 30099 RVA: 0x000B080D File Offset: 0x000AEA0D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F28 RID: 20264
			// (set) Token: 0x06007594 RID: 30100 RVA: 0x000B0825 File Offset: 0x000AEA25
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000912 RID: 2322
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004F29 RID: 20265
			// (set) Token: 0x06007596 RID: 30102 RVA: 0x000B0845 File Offset: 0x000AEA45
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004F2A RID: 20266
			// (set) Token: 0x06007597 RID: 30103 RVA: 0x000B0858 File Offset: 0x000AEA58
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F2B RID: 20267
			// (set) Token: 0x06007598 RID: 30104 RVA: 0x000B0870 File Offset: 0x000AEA70
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F2C RID: 20268
			// (set) Token: 0x06007599 RID: 30105 RVA: 0x000B0888 File Offset: 0x000AEA88
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F2D RID: 20269
			// (set) Token: 0x0600759A RID: 30106 RVA: 0x000B08A0 File Offset: 0x000AEAA0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F2E RID: 20270
			// (set) Token: 0x0600759B RID: 30107 RVA: 0x000B08B3 File Offset: 0x000AEAB3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F2F RID: 20271
			// (set) Token: 0x0600759C RID: 30108 RVA: 0x000B08CB File Offset: 0x000AEACB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F30 RID: 20272
			// (set) Token: 0x0600759D RID: 30109 RVA: 0x000B08E3 File Offset: 0x000AEAE3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F31 RID: 20273
			// (set) Token: 0x0600759E RID: 30110 RVA: 0x000B08FB File Offset: 0x000AEAFB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000913 RID: 2323
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004F32 RID: 20274
			// (set) Token: 0x060075A0 RID: 30112 RVA: 0x000B091B File Offset: 0x000AEB1B
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004F33 RID: 20275
			// (set) Token: 0x060075A1 RID: 30113 RVA: 0x000B092E File Offset: 0x000AEB2E
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F34 RID: 20276
			// (set) Token: 0x060075A2 RID: 30114 RVA: 0x000B0946 File Offset: 0x000AEB46
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F35 RID: 20277
			// (set) Token: 0x060075A3 RID: 30115 RVA: 0x000B095E File Offset: 0x000AEB5E
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F36 RID: 20278
			// (set) Token: 0x060075A4 RID: 30116 RVA: 0x000B0976 File Offset: 0x000AEB76
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F37 RID: 20279
			// (set) Token: 0x060075A5 RID: 30117 RVA: 0x000B0989 File Offset: 0x000AEB89
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F38 RID: 20280
			// (set) Token: 0x060075A6 RID: 30118 RVA: 0x000B09A1 File Offset: 0x000AEBA1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F39 RID: 20281
			// (set) Token: 0x060075A7 RID: 30119 RVA: 0x000B09B9 File Offset: 0x000AEBB9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F3A RID: 20282
			// (set) Token: 0x060075A8 RID: 30120 RVA: 0x000B09D1 File Offset: 0x000AEBD1
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
