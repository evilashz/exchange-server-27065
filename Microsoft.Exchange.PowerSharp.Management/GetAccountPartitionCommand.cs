using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200085D RID: 2141
	public class GetAccountPartitionCommand : SyntheticCommandWithPipelineInput<AccountPartition, AccountPartition>
	{
		// Token: 0x06006A56 RID: 27222 RVA: 0x000A1664 File Offset: 0x0009F864
		private GetAccountPartitionCommand() : base("Get-AccountPartition")
		{
		}

		// Token: 0x06006A57 RID: 27223 RVA: 0x000A1671 File Offset: 0x0009F871
		public GetAccountPartitionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006A58 RID: 27224 RVA: 0x000A1680 File Offset: 0x0009F880
		public virtual GetAccountPartitionCommand SetParameters(GetAccountPartitionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006A59 RID: 27225 RVA: 0x000A168A File Offset: 0x0009F88A
		public virtual GetAccountPartitionCommand SetParameters(GetAccountPartitionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200085E RID: 2142
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004555 RID: 17749
			// (set) Token: 0x06006A5A RID: 27226 RVA: 0x000A1694 File Offset: 0x0009F894
			public virtual SwitchParameter IncludeSecondaryPartitions
			{
				set
				{
					base.PowerSharpParameters["IncludeSecondaryPartitions"] = value;
				}
			}

			// Token: 0x17004556 RID: 17750
			// (set) Token: 0x06006A5B RID: 27227 RVA: 0x000A16AC File Offset: 0x0009F8AC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004557 RID: 17751
			// (set) Token: 0x06006A5C RID: 27228 RVA: 0x000A16BF File Offset: 0x0009F8BF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004558 RID: 17752
			// (set) Token: 0x06006A5D RID: 27229 RVA: 0x000A16D7 File Offset: 0x0009F8D7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004559 RID: 17753
			// (set) Token: 0x06006A5E RID: 27230 RVA: 0x000A16EF File Offset: 0x0009F8EF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700455A RID: 17754
			// (set) Token: 0x06006A5F RID: 27231 RVA: 0x000A1707 File Offset: 0x0009F907
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200085F RID: 2143
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700455B RID: 17755
			// (set) Token: 0x06006A61 RID: 27233 RVA: 0x000A1727 File Offset: 0x0009F927
			public virtual AccountPartitionIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700455C RID: 17756
			// (set) Token: 0x06006A62 RID: 27234 RVA: 0x000A173A File Offset: 0x0009F93A
			public virtual SwitchParameter IncludeSecondaryPartitions
			{
				set
				{
					base.PowerSharpParameters["IncludeSecondaryPartitions"] = value;
				}
			}

			// Token: 0x1700455D RID: 17757
			// (set) Token: 0x06006A63 RID: 27235 RVA: 0x000A1752 File Offset: 0x0009F952
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700455E RID: 17758
			// (set) Token: 0x06006A64 RID: 27236 RVA: 0x000A1765 File Offset: 0x0009F965
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700455F RID: 17759
			// (set) Token: 0x06006A65 RID: 27237 RVA: 0x000A177D File Offset: 0x0009F97D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004560 RID: 17760
			// (set) Token: 0x06006A66 RID: 27238 RVA: 0x000A1795 File Offset: 0x0009F995
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004561 RID: 17761
			// (set) Token: 0x06006A67 RID: 27239 RVA: 0x000A17AD File Offset: 0x0009F9AD
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
