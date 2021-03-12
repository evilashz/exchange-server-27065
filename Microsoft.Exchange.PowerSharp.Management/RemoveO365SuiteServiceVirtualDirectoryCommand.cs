using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000965 RID: 2405
	public class RemoveO365SuiteServiceVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADO365SuiteServiceVirtualDirectory, ADO365SuiteServiceVirtualDirectory>
	{
		// Token: 0x0600787B RID: 30843 RVA: 0x000B41C7 File Offset: 0x000B23C7
		private RemoveO365SuiteServiceVirtualDirectoryCommand() : base("Remove-O365SuiteServiceVirtualDirectory")
		{
		}

		// Token: 0x0600787C RID: 30844 RVA: 0x000B41D4 File Offset: 0x000B23D4
		public RemoveO365SuiteServiceVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600787D RID: 30845 RVA: 0x000B41E3 File Offset: 0x000B23E3
		public virtual RemoveO365SuiteServiceVirtualDirectoryCommand SetParameters(RemoveO365SuiteServiceVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600787E RID: 30846 RVA: 0x000B41ED File Offset: 0x000B23ED
		public virtual RemoveO365SuiteServiceVirtualDirectoryCommand SetParameters(RemoveO365SuiteServiceVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000966 RID: 2406
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700516A RID: 20842
			// (set) Token: 0x0600787F RID: 30847 RVA: 0x000B41F7 File Offset: 0x000B23F7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700516B RID: 20843
			// (set) Token: 0x06007880 RID: 30848 RVA: 0x000B420A File Offset: 0x000B240A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700516C RID: 20844
			// (set) Token: 0x06007881 RID: 30849 RVA: 0x000B4222 File Offset: 0x000B2422
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700516D RID: 20845
			// (set) Token: 0x06007882 RID: 30850 RVA: 0x000B423A File Offset: 0x000B243A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700516E RID: 20846
			// (set) Token: 0x06007883 RID: 30851 RVA: 0x000B4252 File Offset: 0x000B2452
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700516F RID: 20847
			// (set) Token: 0x06007884 RID: 30852 RVA: 0x000B426A File Offset: 0x000B246A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005170 RID: 20848
			// (set) Token: 0x06007885 RID: 30853 RVA: 0x000B4282 File Offset: 0x000B2482
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000967 RID: 2407
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005171 RID: 20849
			// (set) Token: 0x06007887 RID: 30855 RVA: 0x000B42A2 File Offset: 0x000B24A2
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005172 RID: 20850
			// (set) Token: 0x06007888 RID: 30856 RVA: 0x000B42B5 File Offset: 0x000B24B5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005173 RID: 20851
			// (set) Token: 0x06007889 RID: 30857 RVA: 0x000B42C8 File Offset: 0x000B24C8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005174 RID: 20852
			// (set) Token: 0x0600788A RID: 30858 RVA: 0x000B42E0 File Offset: 0x000B24E0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005175 RID: 20853
			// (set) Token: 0x0600788B RID: 30859 RVA: 0x000B42F8 File Offset: 0x000B24F8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005176 RID: 20854
			// (set) Token: 0x0600788C RID: 30860 RVA: 0x000B4310 File Offset: 0x000B2510
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005177 RID: 20855
			// (set) Token: 0x0600788D RID: 30861 RVA: 0x000B4328 File Offset: 0x000B2528
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005178 RID: 20856
			// (set) Token: 0x0600788E RID: 30862 RVA: 0x000B4340 File Offset: 0x000B2540
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
