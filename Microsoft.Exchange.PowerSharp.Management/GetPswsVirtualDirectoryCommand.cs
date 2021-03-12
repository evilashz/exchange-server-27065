using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000928 RID: 2344
	public class GetPswsVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADPswsVirtualDirectory, ADPswsVirtualDirectory>
	{
		// Token: 0x06007654 RID: 30292 RVA: 0x000B173E File Offset: 0x000AF93E
		private GetPswsVirtualDirectoryCommand() : base("Get-PswsVirtualDirectory")
		{
		}

		// Token: 0x06007655 RID: 30293 RVA: 0x000B174B File Offset: 0x000AF94B
		public GetPswsVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007656 RID: 30294 RVA: 0x000B175A File Offset: 0x000AF95A
		public virtual GetPswsVirtualDirectoryCommand SetParameters(GetPswsVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007657 RID: 30295 RVA: 0x000B1764 File Offset: 0x000AF964
		public virtual GetPswsVirtualDirectoryCommand SetParameters(GetPswsVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007658 RID: 30296 RVA: 0x000B176E File Offset: 0x000AF96E
		public virtual GetPswsVirtualDirectoryCommand SetParameters(GetPswsVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000929 RID: 2345
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004FBD RID: 20413
			// (set) Token: 0x06007659 RID: 30297 RVA: 0x000B1778 File Offset: 0x000AF978
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004FBE RID: 20414
			// (set) Token: 0x0600765A RID: 30298 RVA: 0x000B1790 File Offset: 0x000AF990
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FBF RID: 20415
			// (set) Token: 0x0600765B RID: 30299 RVA: 0x000B17A8 File Offset: 0x000AF9A8
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FC0 RID: 20416
			// (set) Token: 0x0600765C RID: 30300 RVA: 0x000B17C0 File Offset: 0x000AF9C0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004FC1 RID: 20417
			// (set) Token: 0x0600765D RID: 30301 RVA: 0x000B17D3 File Offset: 0x000AF9D3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FC2 RID: 20418
			// (set) Token: 0x0600765E RID: 30302 RVA: 0x000B17EB File Offset: 0x000AF9EB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004FC3 RID: 20419
			// (set) Token: 0x0600765F RID: 30303 RVA: 0x000B1803 File Offset: 0x000AFA03
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004FC4 RID: 20420
			// (set) Token: 0x06007660 RID: 30304 RVA: 0x000B181B File Offset: 0x000AFA1B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200092A RID: 2346
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004FC5 RID: 20421
			// (set) Token: 0x06007662 RID: 30306 RVA: 0x000B183B File Offset: 0x000AFA3B
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004FC6 RID: 20422
			// (set) Token: 0x06007663 RID: 30307 RVA: 0x000B184E File Offset: 0x000AFA4E
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004FC7 RID: 20423
			// (set) Token: 0x06007664 RID: 30308 RVA: 0x000B1866 File Offset: 0x000AFA66
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FC8 RID: 20424
			// (set) Token: 0x06007665 RID: 30309 RVA: 0x000B187E File Offset: 0x000AFA7E
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FC9 RID: 20425
			// (set) Token: 0x06007666 RID: 30310 RVA: 0x000B1896 File Offset: 0x000AFA96
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004FCA RID: 20426
			// (set) Token: 0x06007667 RID: 30311 RVA: 0x000B18A9 File Offset: 0x000AFAA9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FCB RID: 20427
			// (set) Token: 0x06007668 RID: 30312 RVA: 0x000B18C1 File Offset: 0x000AFAC1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004FCC RID: 20428
			// (set) Token: 0x06007669 RID: 30313 RVA: 0x000B18D9 File Offset: 0x000AFAD9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004FCD RID: 20429
			// (set) Token: 0x0600766A RID: 30314 RVA: 0x000B18F1 File Offset: 0x000AFAF1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200092B RID: 2347
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004FCE RID: 20430
			// (set) Token: 0x0600766C RID: 30316 RVA: 0x000B1911 File Offset: 0x000AFB11
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004FCF RID: 20431
			// (set) Token: 0x0600766D RID: 30317 RVA: 0x000B1924 File Offset: 0x000AFB24
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004FD0 RID: 20432
			// (set) Token: 0x0600766E RID: 30318 RVA: 0x000B193C File Offset: 0x000AFB3C
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FD1 RID: 20433
			// (set) Token: 0x0600766F RID: 30319 RVA: 0x000B1954 File Offset: 0x000AFB54
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FD2 RID: 20434
			// (set) Token: 0x06007670 RID: 30320 RVA: 0x000B196C File Offset: 0x000AFB6C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004FD3 RID: 20435
			// (set) Token: 0x06007671 RID: 30321 RVA: 0x000B197F File Offset: 0x000AFB7F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FD4 RID: 20436
			// (set) Token: 0x06007672 RID: 30322 RVA: 0x000B1997 File Offset: 0x000AFB97
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004FD5 RID: 20437
			// (set) Token: 0x06007673 RID: 30323 RVA: 0x000B19AF File Offset: 0x000AFBAF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004FD6 RID: 20438
			// (set) Token: 0x06007674 RID: 30324 RVA: 0x000B19C7 File Offset: 0x000AFBC7
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
