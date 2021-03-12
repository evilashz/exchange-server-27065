using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000904 RID: 2308
	public class GetAutodiscoverVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADAutodiscoverVirtualDirectory, ADAutodiscoverVirtualDirectory>
	{
		// Token: 0x06007522 RID: 29986 RVA: 0x000AFF4D File Offset: 0x000AE14D
		private GetAutodiscoverVirtualDirectoryCommand() : base("Get-AutodiscoverVirtualDirectory")
		{
		}

		// Token: 0x06007523 RID: 29987 RVA: 0x000AFF5A File Offset: 0x000AE15A
		public GetAutodiscoverVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007524 RID: 29988 RVA: 0x000AFF69 File Offset: 0x000AE169
		public virtual GetAutodiscoverVirtualDirectoryCommand SetParameters(GetAutodiscoverVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007525 RID: 29989 RVA: 0x000AFF73 File Offset: 0x000AE173
		public virtual GetAutodiscoverVirtualDirectoryCommand SetParameters(GetAutodiscoverVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007526 RID: 29990 RVA: 0x000AFF7D File Offset: 0x000AE17D
		public virtual GetAutodiscoverVirtualDirectoryCommand SetParameters(GetAutodiscoverVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000905 RID: 2309
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004ED3 RID: 20179
			// (set) Token: 0x06007527 RID: 29991 RVA: 0x000AFF87 File Offset: 0x000AE187
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004ED4 RID: 20180
			// (set) Token: 0x06007528 RID: 29992 RVA: 0x000AFF9F File Offset: 0x000AE19F
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004ED5 RID: 20181
			// (set) Token: 0x06007529 RID: 29993 RVA: 0x000AFFB7 File Offset: 0x000AE1B7
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004ED6 RID: 20182
			// (set) Token: 0x0600752A RID: 29994 RVA: 0x000AFFCF File Offset: 0x000AE1CF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004ED7 RID: 20183
			// (set) Token: 0x0600752B RID: 29995 RVA: 0x000AFFE2 File Offset: 0x000AE1E2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004ED8 RID: 20184
			// (set) Token: 0x0600752C RID: 29996 RVA: 0x000AFFFA File Offset: 0x000AE1FA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004ED9 RID: 20185
			// (set) Token: 0x0600752D RID: 29997 RVA: 0x000B0012 File Offset: 0x000AE212
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004EDA RID: 20186
			// (set) Token: 0x0600752E RID: 29998 RVA: 0x000B002A File Offset: 0x000AE22A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000906 RID: 2310
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004EDB RID: 20187
			// (set) Token: 0x06007530 RID: 30000 RVA: 0x000B004A File Offset: 0x000AE24A
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004EDC RID: 20188
			// (set) Token: 0x06007531 RID: 30001 RVA: 0x000B005D File Offset: 0x000AE25D
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004EDD RID: 20189
			// (set) Token: 0x06007532 RID: 30002 RVA: 0x000B0075 File Offset: 0x000AE275
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004EDE RID: 20190
			// (set) Token: 0x06007533 RID: 30003 RVA: 0x000B008D File Offset: 0x000AE28D
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004EDF RID: 20191
			// (set) Token: 0x06007534 RID: 30004 RVA: 0x000B00A5 File Offset: 0x000AE2A5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004EE0 RID: 20192
			// (set) Token: 0x06007535 RID: 30005 RVA: 0x000B00B8 File Offset: 0x000AE2B8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004EE1 RID: 20193
			// (set) Token: 0x06007536 RID: 30006 RVA: 0x000B00D0 File Offset: 0x000AE2D0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004EE2 RID: 20194
			// (set) Token: 0x06007537 RID: 30007 RVA: 0x000B00E8 File Offset: 0x000AE2E8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004EE3 RID: 20195
			// (set) Token: 0x06007538 RID: 30008 RVA: 0x000B0100 File Offset: 0x000AE300
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000907 RID: 2311
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004EE4 RID: 20196
			// (set) Token: 0x0600753A RID: 30010 RVA: 0x000B0120 File Offset: 0x000AE320
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004EE5 RID: 20197
			// (set) Token: 0x0600753B RID: 30011 RVA: 0x000B0133 File Offset: 0x000AE333
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004EE6 RID: 20198
			// (set) Token: 0x0600753C RID: 30012 RVA: 0x000B014B File Offset: 0x000AE34B
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004EE7 RID: 20199
			// (set) Token: 0x0600753D RID: 30013 RVA: 0x000B0163 File Offset: 0x000AE363
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004EE8 RID: 20200
			// (set) Token: 0x0600753E RID: 30014 RVA: 0x000B017B File Offset: 0x000AE37B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004EE9 RID: 20201
			// (set) Token: 0x0600753F RID: 30015 RVA: 0x000B018E File Offset: 0x000AE38E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004EEA RID: 20202
			// (set) Token: 0x06007540 RID: 30016 RVA: 0x000B01A6 File Offset: 0x000AE3A6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004EEB RID: 20203
			// (set) Token: 0x06007541 RID: 30017 RVA: 0x000B01BE File Offset: 0x000AE3BE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004EEC RID: 20204
			// (set) Token: 0x06007542 RID: 30018 RVA: 0x000B01D6 File Offset: 0x000AE3D6
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
