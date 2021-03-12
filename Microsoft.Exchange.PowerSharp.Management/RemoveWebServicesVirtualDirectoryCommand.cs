using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200097A RID: 2426
	public class RemoveWebServicesVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADWebServicesVirtualDirectory, ADWebServicesVirtualDirectory>
	{
		// Token: 0x06007910 RID: 30992 RVA: 0x000B4D26 File Offset: 0x000B2F26
		private RemoveWebServicesVirtualDirectoryCommand() : base("Remove-WebServicesVirtualDirectory")
		{
		}

		// Token: 0x06007911 RID: 30993 RVA: 0x000B4D33 File Offset: 0x000B2F33
		public RemoveWebServicesVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007912 RID: 30994 RVA: 0x000B4D42 File Offset: 0x000B2F42
		public virtual RemoveWebServicesVirtualDirectoryCommand SetParameters(RemoveWebServicesVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007913 RID: 30995 RVA: 0x000B4D4C File Offset: 0x000B2F4C
		public virtual RemoveWebServicesVirtualDirectoryCommand SetParameters(RemoveWebServicesVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200097B RID: 2427
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170051D5 RID: 20949
			// (set) Token: 0x06007914 RID: 30996 RVA: 0x000B4D56 File Offset: 0x000B2F56
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170051D6 RID: 20950
			// (set) Token: 0x06007915 RID: 30997 RVA: 0x000B4D6E File Offset: 0x000B2F6E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170051D7 RID: 20951
			// (set) Token: 0x06007916 RID: 30998 RVA: 0x000B4D81 File Offset: 0x000B2F81
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170051D8 RID: 20952
			// (set) Token: 0x06007917 RID: 30999 RVA: 0x000B4D99 File Offset: 0x000B2F99
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170051D9 RID: 20953
			// (set) Token: 0x06007918 RID: 31000 RVA: 0x000B4DB1 File Offset: 0x000B2FB1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170051DA RID: 20954
			// (set) Token: 0x06007919 RID: 31001 RVA: 0x000B4DC9 File Offset: 0x000B2FC9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170051DB RID: 20955
			// (set) Token: 0x0600791A RID: 31002 RVA: 0x000B4DE1 File Offset: 0x000B2FE1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170051DC RID: 20956
			// (set) Token: 0x0600791B RID: 31003 RVA: 0x000B4DF9 File Offset: 0x000B2FF9
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200097C RID: 2428
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170051DD RID: 20957
			// (set) Token: 0x0600791D RID: 31005 RVA: 0x000B4E19 File Offset: 0x000B3019
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170051DE RID: 20958
			// (set) Token: 0x0600791E RID: 31006 RVA: 0x000B4E2C File Offset: 0x000B302C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170051DF RID: 20959
			// (set) Token: 0x0600791F RID: 31007 RVA: 0x000B4E44 File Offset: 0x000B3044
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170051E0 RID: 20960
			// (set) Token: 0x06007920 RID: 31008 RVA: 0x000B4E57 File Offset: 0x000B3057
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170051E1 RID: 20961
			// (set) Token: 0x06007921 RID: 31009 RVA: 0x000B4E6F File Offset: 0x000B306F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170051E2 RID: 20962
			// (set) Token: 0x06007922 RID: 31010 RVA: 0x000B4E87 File Offset: 0x000B3087
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170051E3 RID: 20963
			// (set) Token: 0x06007923 RID: 31011 RVA: 0x000B4E9F File Offset: 0x000B309F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170051E4 RID: 20964
			// (set) Token: 0x06007924 RID: 31012 RVA: 0x000B4EB7 File Offset: 0x000B30B7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170051E5 RID: 20965
			// (set) Token: 0x06007925 RID: 31013 RVA: 0x000B4ECF File Offset: 0x000B30CF
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
