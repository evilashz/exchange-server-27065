using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000918 RID: 2328
	public class GetO365SuiteServiceVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADO365SuiteServiceVirtualDirectory, ADO365SuiteServiceVirtualDirectory>
	{
		// Token: 0x060075CC RID: 30156 RVA: 0x000B0C9A File Offset: 0x000AEE9A
		private GetO365SuiteServiceVirtualDirectoryCommand() : base("Get-O365SuiteServiceVirtualDirectory")
		{
		}

		// Token: 0x060075CD RID: 30157 RVA: 0x000B0CA7 File Offset: 0x000AEEA7
		public GetO365SuiteServiceVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060075CE RID: 30158 RVA: 0x000B0CB6 File Offset: 0x000AEEB6
		public virtual GetO365SuiteServiceVirtualDirectoryCommand SetParameters(GetO365SuiteServiceVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060075CF RID: 30159 RVA: 0x000B0CC0 File Offset: 0x000AEEC0
		public virtual GetO365SuiteServiceVirtualDirectoryCommand SetParameters(GetO365SuiteServiceVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060075D0 RID: 30160 RVA: 0x000B0CCA File Offset: 0x000AEECA
		public virtual GetO365SuiteServiceVirtualDirectoryCommand SetParameters(GetO365SuiteServiceVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000919 RID: 2329
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004F55 RID: 20309
			// (set) Token: 0x060075D1 RID: 30161 RVA: 0x000B0CD4 File Offset: 0x000AEED4
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F56 RID: 20310
			// (set) Token: 0x060075D2 RID: 30162 RVA: 0x000B0CEC File Offset: 0x000AEEEC
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F57 RID: 20311
			// (set) Token: 0x060075D3 RID: 30163 RVA: 0x000B0D04 File Offset: 0x000AEF04
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F58 RID: 20312
			// (set) Token: 0x060075D4 RID: 30164 RVA: 0x000B0D1C File Offset: 0x000AEF1C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F59 RID: 20313
			// (set) Token: 0x060075D5 RID: 30165 RVA: 0x000B0D2F File Offset: 0x000AEF2F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F5A RID: 20314
			// (set) Token: 0x060075D6 RID: 30166 RVA: 0x000B0D47 File Offset: 0x000AEF47
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F5B RID: 20315
			// (set) Token: 0x060075D7 RID: 30167 RVA: 0x000B0D5F File Offset: 0x000AEF5F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F5C RID: 20316
			// (set) Token: 0x060075D8 RID: 30168 RVA: 0x000B0D77 File Offset: 0x000AEF77
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200091A RID: 2330
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004F5D RID: 20317
			// (set) Token: 0x060075DA RID: 30170 RVA: 0x000B0D97 File Offset: 0x000AEF97
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004F5E RID: 20318
			// (set) Token: 0x060075DB RID: 30171 RVA: 0x000B0DAA File Offset: 0x000AEFAA
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F5F RID: 20319
			// (set) Token: 0x060075DC RID: 30172 RVA: 0x000B0DC2 File Offset: 0x000AEFC2
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F60 RID: 20320
			// (set) Token: 0x060075DD RID: 30173 RVA: 0x000B0DDA File Offset: 0x000AEFDA
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F61 RID: 20321
			// (set) Token: 0x060075DE RID: 30174 RVA: 0x000B0DF2 File Offset: 0x000AEFF2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F62 RID: 20322
			// (set) Token: 0x060075DF RID: 30175 RVA: 0x000B0E05 File Offset: 0x000AF005
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F63 RID: 20323
			// (set) Token: 0x060075E0 RID: 30176 RVA: 0x000B0E1D File Offset: 0x000AF01D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F64 RID: 20324
			// (set) Token: 0x060075E1 RID: 30177 RVA: 0x000B0E35 File Offset: 0x000AF035
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F65 RID: 20325
			// (set) Token: 0x060075E2 RID: 30178 RVA: 0x000B0E4D File Offset: 0x000AF04D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200091B RID: 2331
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004F66 RID: 20326
			// (set) Token: 0x060075E4 RID: 30180 RVA: 0x000B0E6D File Offset: 0x000AF06D
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004F67 RID: 20327
			// (set) Token: 0x060075E5 RID: 30181 RVA: 0x000B0E80 File Offset: 0x000AF080
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F68 RID: 20328
			// (set) Token: 0x060075E6 RID: 30182 RVA: 0x000B0E98 File Offset: 0x000AF098
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F69 RID: 20329
			// (set) Token: 0x060075E7 RID: 30183 RVA: 0x000B0EB0 File Offset: 0x000AF0B0
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F6A RID: 20330
			// (set) Token: 0x060075E8 RID: 30184 RVA: 0x000B0EC8 File Offset: 0x000AF0C8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F6B RID: 20331
			// (set) Token: 0x060075E9 RID: 30185 RVA: 0x000B0EDB File Offset: 0x000AF0DB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F6C RID: 20332
			// (set) Token: 0x060075EA RID: 30186 RVA: 0x000B0EF3 File Offset: 0x000AF0F3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F6D RID: 20333
			// (set) Token: 0x060075EB RID: 30187 RVA: 0x000B0F0B File Offset: 0x000AF10B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F6E RID: 20334
			// (set) Token: 0x060075EC RID: 30188 RVA: 0x000B0F23 File Offset: 0x000AF123
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
