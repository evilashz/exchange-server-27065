using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000920 RID: 2336
	public class GetOwaVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADOwaVirtualDirectory, ADOwaVirtualDirectory>
	{
		// Token: 0x06007610 RID: 30224 RVA: 0x000B11EC File Offset: 0x000AF3EC
		private GetOwaVirtualDirectoryCommand() : base("Get-OwaVirtualDirectory")
		{
		}

		// Token: 0x06007611 RID: 30225 RVA: 0x000B11F9 File Offset: 0x000AF3F9
		public GetOwaVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007612 RID: 30226 RVA: 0x000B1208 File Offset: 0x000AF408
		public virtual GetOwaVirtualDirectoryCommand SetParameters(GetOwaVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007613 RID: 30227 RVA: 0x000B1212 File Offset: 0x000AF412
		public virtual GetOwaVirtualDirectoryCommand SetParameters(GetOwaVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007614 RID: 30228 RVA: 0x000B121C File Offset: 0x000AF41C
		public virtual GetOwaVirtualDirectoryCommand SetParameters(GetOwaVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000921 RID: 2337
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004F89 RID: 20361
			// (set) Token: 0x06007615 RID: 30229 RVA: 0x000B1226 File Offset: 0x000AF426
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F8A RID: 20362
			// (set) Token: 0x06007616 RID: 30230 RVA: 0x000B123E File Offset: 0x000AF43E
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F8B RID: 20363
			// (set) Token: 0x06007617 RID: 30231 RVA: 0x000B1256 File Offset: 0x000AF456
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F8C RID: 20364
			// (set) Token: 0x06007618 RID: 30232 RVA: 0x000B126E File Offset: 0x000AF46E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F8D RID: 20365
			// (set) Token: 0x06007619 RID: 30233 RVA: 0x000B1281 File Offset: 0x000AF481
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F8E RID: 20366
			// (set) Token: 0x0600761A RID: 30234 RVA: 0x000B1299 File Offset: 0x000AF499
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F8F RID: 20367
			// (set) Token: 0x0600761B RID: 30235 RVA: 0x000B12B1 File Offset: 0x000AF4B1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F90 RID: 20368
			// (set) Token: 0x0600761C RID: 30236 RVA: 0x000B12C9 File Offset: 0x000AF4C9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000922 RID: 2338
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004F91 RID: 20369
			// (set) Token: 0x0600761E RID: 30238 RVA: 0x000B12E9 File Offset: 0x000AF4E9
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004F92 RID: 20370
			// (set) Token: 0x0600761F RID: 30239 RVA: 0x000B12FC File Offset: 0x000AF4FC
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F93 RID: 20371
			// (set) Token: 0x06007620 RID: 30240 RVA: 0x000B1314 File Offset: 0x000AF514
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F94 RID: 20372
			// (set) Token: 0x06007621 RID: 30241 RVA: 0x000B132C File Offset: 0x000AF52C
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F95 RID: 20373
			// (set) Token: 0x06007622 RID: 30242 RVA: 0x000B1344 File Offset: 0x000AF544
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F96 RID: 20374
			// (set) Token: 0x06007623 RID: 30243 RVA: 0x000B1357 File Offset: 0x000AF557
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F97 RID: 20375
			// (set) Token: 0x06007624 RID: 30244 RVA: 0x000B136F File Offset: 0x000AF56F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F98 RID: 20376
			// (set) Token: 0x06007625 RID: 30245 RVA: 0x000B1387 File Offset: 0x000AF587
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F99 RID: 20377
			// (set) Token: 0x06007626 RID: 30246 RVA: 0x000B139F File Offset: 0x000AF59F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000923 RID: 2339
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004F9A RID: 20378
			// (set) Token: 0x06007628 RID: 30248 RVA: 0x000B13BF File Offset: 0x000AF5BF
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004F9B RID: 20379
			// (set) Token: 0x06007629 RID: 30249 RVA: 0x000B13D2 File Offset: 0x000AF5D2
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F9C RID: 20380
			// (set) Token: 0x0600762A RID: 30250 RVA: 0x000B13EA File Offset: 0x000AF5EA
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F9D RID: 20381
			// (set) Token: 0x0600762B RID: 30251 RVA: 0x000B1402 File Offset: 0x000AF602
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F9E RID: 20382
			// (set) Token: 0x0600762C RID: 30252 RVA: 0x000B141A File Offset: 0x000AF61A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F9F RID: 20383
			// (set) Token: 0x0600762D RID: 30253 RVA: 0x000B142D File Offset: 0x000AF62D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FA0 RID: 20384
			// (set) Token: 0x0600762E RID: 30254 RVA: 0x000B1445 File Offset: 0x000AF645
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004FA1 RID: 20385
			// (set) Token: 0x0600762F RID: 30255 RVA: 0x000B145D File Offset: 0x000AF65D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004FA2 RID: 20386
			// (set) Token: 0x06007630 RID: 30256 RVA: 0x000B1475 File Offset: 0x000AF675
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
