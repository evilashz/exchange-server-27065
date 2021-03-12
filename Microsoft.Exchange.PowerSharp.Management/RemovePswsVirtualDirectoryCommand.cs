using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000971 RID: 2417
	public class RemovePswsVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADPswsVirtualDirectory, ADPswsVirtualDirectory>
	{
		// Token: 0x060078D1 RID: 30929 RVA: 0x000B485B File Offset: 0x000B2A5B
		private RemovePswsVirtualDirectoryCommand() : base("Remove-PswsVirtualDirectory")
		{
		}

		// Token: 0x060078D2 RID: 30930 RVA: 0x000B4868 File Offset: 0x000B2A68
		public RemovePswsVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060078D3 RID: 30931 RVA: 0x000B4877 File Offset: 0x000B2A77
		public virtual RemovePswsVirtualDirectoryCommand SetParameters(RemovePswsVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060078D4 RID: 30932 RVA: 0x000B4881 File Offset: 0x000B2A81
		public virtual RemovePswsVirtualDirectoryCommand SetParameters(RemovePswsVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000972 RID: 2418
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170051A8 RID: 20904
			// (set) Token: 0x060078D5 RID: 30933 RVA: 0x000B488B File Offset: 0x000B2A8B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170051A9 RID: 20905
			// (set) Token: 0x060078D6 RID: 30934 RVA: 0x000B489E File Offset: 0x000B2A9E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170051AA RID: 20906
			// (set) Token: 0x060078D7 RID: 30935 RVA: 0x000B48B6 File Offset: 0x000B2AB6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170051AB RID: 20907
			// (set) Token: 0x060078D8 RID: 30936 RVA: 0x000B48CE File Offset: 0x000B2ACE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170051AC RID: 20908
			// (set) Token: 0x060078D9 RID: 30937 RVA: 0x000B48E6 File Offset: 0x000B2AE6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170051AD RID: 20909
			// (set) Token: 0x060078DA RID: 30938 RVA: 0x000B48FE File Offset: 0x000B2AFE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170051AE RID: 20910
			// (set) Token: 0x060078DB RID: 30939 RVA: 0x000B4916 File Offset: 0x000B2B16
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000973 RID: 2419
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170051AF RID: 20911
			// (set) Token: 0x060078DD RID: 30941 RVA: 0x000B4936 File Offset: 0x000B2B36
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170051B0 RID: 20912
			// (set) Token: 0x060078DE RID: 30942 RVA: 0x000B4949 File Offset: 0x000B2B49
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170051B1 RID: 20913
			// (set) Token: 0x060078DF RID: 30943 RVA: 0x000B495C File Offset: 0x000B2B5C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170051B2 RID: 20914
			// (set) Token: 0x060078E0 RID: 30944 RVA: 0x000B4974 File Offset: 0x000B2B74
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170051B3 RID: 20915
			// (set) Token: 0x060078E1 RID: 30945 RVA: 0x000B498C File Offset: 0x000B2B8C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170051B4 RID: 20916
			// (set) Token: 0x060078E2 RID: 30946 RVA: 0x000B49A4 File Offset: 0x000B2BA4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170051B5 RID: 20917
			// (set) Token: 0x060078E3 RID: 30947 RVA: 0x000B49BC File Offset: 0x000B2BBC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170051B6 RID: 20918
			// (set) Token: 0x060078E4 RID: 30948 RVA: 0x000B49D4 File Offset: 0x000B2BD4
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
