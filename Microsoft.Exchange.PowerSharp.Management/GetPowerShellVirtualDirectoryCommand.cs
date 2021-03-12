using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000924 RID: 2340
	public class GetPowerShellVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADPowerShellVirtualDirectory, ADPowerShellVirtualDirectory>
	{
		// Token: 0x06007632 RID: 30258 RVA: 0x000B1495 File Offset: 0x000AF695
		private GetPowerShellVirtualDirectoryCommand() : base("Get-PowerShellVirtualDirectory")
		{
		}

		// Token: 0x06007633 RID: 30259 RVA: 0x000B14A2 File Offset: 0x000AF6A2
		public GetPowerShellVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007634 RID: 30260 RVA: 0x000B14B1 File Offset: 0x000AF6B1
		public virtual GetPowerShellVirtualDirectoryCommand SetParameters(GetPowerShellVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007635 RID: 30261 RVA: 0x000B14BB File Offset: 0x000AF6BB
		public virtual GetPowerShellVirtualDirectoryCommand SetParameters(GetPowerShellVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007636 RID: 30262 RVA: 0x000B14C5 File Offset: 0x000AF6C5
		public virtual GetPowerShellVirtualDirectoryCommand SetParameters(GetPowerShellVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000925 RID: 2341
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004FA3 RID: 20387
			// (set) Token: 0x06007637 RID: 30263 RVA: 0x000B14CF File Offset: 0x000AF6CF
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004FA4 RID: 20388
			// (set) Token: 0x06007638 RID: 30264 RVA: 0x000B14E7 File Offset: 0x000AF6E7
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FA5 RID: 20389
			// (set) Token: 0x06007639 RID: 30265 RVA: 0x000B14FF File Offset: 0x000AF6FF
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FA6 RID: 20390
			// (set) Token: 0x0600763A RID: 30266 RVA: 0x000B1517 File Offset: 0x000AF717
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004FA7 RID: 20391
			// (set) Token: 0x0600763B RID: 30267 RVA: 0x000B152A File Offset: 0x000AF72A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FA8 RID: 20392
			// (set) Token: 0x0600763C RID: 30268 RVA: 0x000B1542 File Offset: 0x000AF742
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004FA9 RID: 20393
			// (set) Token: 0x0600763D RID: 30269 RVA: 0x000B155A File Offset: 0x000AF75A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004FAA RID: 20394
			// (set) Token: 0x0600763E RID: 30270 RVA: 0x000B1572 File Offset: 0x000AF772
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000926 RID: 2342
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004FAB RID: 20395
			// (set) Token: 0x06007640 RID: 30272 RVA: 0x000B1592 File Offset: 0x000AF792
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004FAC RID: 20396
			// (set) Token: 0x06007641 RID: 30273 RVA: 0x000B15A5 File Offset: 0x000AF7A5
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004FAD RID: 20397
			// (set) Token: 0x06007642 RID: 30274 RVA: 0x000B15BD File Offset: 0x000AF7BD
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FAE RID: 20398
			// (set) Token: 0x06007643 RID: 30275 RVA: 0x000B15D5 File Offset: 0x000AF7D5
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FAF RID: 20399
			// (set) Token: 0x06007644 RID: 30276 RVA: 0x000B15ED File Offset: 0x000AF7ED
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004FB0 RID: 20400
			// (set) Token: 0x06007645 RID: 30277 RVA: 0x000B1600 File Offset: 0x000AF800
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FB1 RID: 20401
			// (set) Token: 0x06007646 RID: 30278 RVA: 0x000B1618 File Offset: 0x000AF818
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004FB2 RID: 20402
			// (set) Token: 0x06007647 RID: 30279 RVA: 0x000B1630 File Offset: 0x000AF830
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004FB3 RID: 20403
			// (set) Token: 0x06007648 RID: 30280 RVA: 0x000B1648 File Offset: 0x000AF848
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000927 RID: 2343
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004FB4 RID: 20404
			// (set) Token: 0x0600764A RID: 30282 RVA: 0x000B1668 File Offset: 0x000AF868
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004FB5 RID: 20405
			// (set) Token: 0x0600764B RID: 30283 RVA: 0x000B167B File Offset: 0x000AF87B
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004FB6 RID: 20406
			// (set) Token: 0x0600764C RID: 30284 RVA: 0x000B1693 File Offset: 0x000AF893
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FB7 RID: 20407
			// (set) Token: 0x0600764D RID: 30285 RVA: 0x000B16AB File Offset: 0x000AF8AB
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FB8 RID: 20408
			// (set) Token: 0x0600764E RID: 30286 RVA: 0x000B16C3 File Offset: 0x000AF8C3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004FB9 RID: 20409
			// (set) Token: 0x0600764F RID: 30287 RVA: 0x000B16D6 File Offset: 0x000AF8D6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FBA RID: 20410
			// (set) Token: 0x06007650 RID: 30288 RVA: 0x000B16EE File Offset: 0x000AF8EE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004FBB RID: 20411
			// (set) Token: 0x06007651 RID: 30289 RVA: 0x000B1706 File Offset: 0x000AF906
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004FBC RID: 20412
			// (set) Token: 0x06007652 RID: 30290 RVA: 0x000B171E File Offset: 0x000AF91E
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
