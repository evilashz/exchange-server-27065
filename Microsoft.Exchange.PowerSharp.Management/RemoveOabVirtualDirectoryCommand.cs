using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000968 RID: 2408
	public class RemoveOabVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADOabVirtualDirectory, ADOabVirtualDirectory>
	{
		// Token: 0x06007890 RID: 30864 RVA: 0x000B4360 File Offset: 0x000B2560
		private RemoveOabVirtualDirectoryCommand() : base("Remove-OabVirtualDirectory")
		{
		}

		// Token: 0x06007891 RID: 30865 RVA: 0x000B436D File Offset: 0x000B256D
		public RemoveOabVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x000B437C File Offset: 0x000B257C
		public virtual RemoveOabVirtualDirectoryCommand SetParameters(RemoveOabVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007893 RID: 30867 RVA: 0x000B4386 File Offset: 0x000B2586
		public virtual RemoveOabVirtualDirectoryCommand SetParameters(RemoveOabVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000969 RID: 2409
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005179 RID: 20857
			// (set) Token: 0x06007894 RID: 30868 RVA: 0x000B4390 File Offset: 0x000B2590
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700517A RID: 20858
			// (set) Token: 0x06007895 RID: 30869 RVA: 0x000B43A8 File Offset: 0x000B25A8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700517B RID: 20859
			// (set) Token: 0x06007896 RID: 30870 RVA: 0x000B43BB File Offset: 0x000B25BB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700517C RID: 20860
			// (set) Token: 0x06007897 RID: 30871 RVA: 0x000B43D3 File Offset: 0x000B25D3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700517D RID: 20861
			// (set) Token: 0x06007898 RID: 30872 RVA: 0x000B43EB File Offset: 0x000B25EB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700517E RID: 20862
			// (set) Token: 0x06007899 RID: 30873 RVA: 0x000B4403 File Offset: 0x000B2603
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700517F RID: 20863
			// (set) Token: 0x0600789A RID: 30874 RVA: 0x000B441B File Offset: 0x000B261B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005180 RID: 20864
			// (set) Token: 0x0600789B RID: 30875 RVA: 0x000B4433 File Offset: 0x000B2633
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200096A RID: 2410
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005181 RID: 20865
			// (set) Token: 0x0600789D RID: 30877 RVA: 0x000B4453 File Offset: 0x000B2653
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005182 RID: 20866
			// (set) Token: 0x0600789E RID: 30878 RVA: 0x000B4466 File Offset: 0x000B2666
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17005183 RID: 20867
			// (set) Token: 0x0600789F RID: 30879 RVA: 0x000B447E File Offset: 0x000B267E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005184 RID: 20868
			// (set) Token: 0x060078A0 RID: 30880 RVA: 0x000B4491 File Offset: 0x000B2691
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005185 RID: 20869
			// (set) Token: 0x060078A1 RID: 30881 RVA: 0x000B44A9 File Offset: 0x000B26A9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005186 RID: 20870
			// (set) Token: 0x060078A2 RID: 30882 RVA: 0x000B44C1 File Offset: 0x000B26C1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005187 RID: 20871
			// (set) Token: 0x060078A3 RID: 30883 RVA: 0x000B44D9 File Offset: 0x000B26D9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005188 RID: 20872
			// (set) Token: 0x060078A4 RID: 30884 RVA: 0x000B44F1 File Offset: 0x000B26F1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005189 RID: 20873
			// (set) Token: 0x060078A5 RID: 30885 RVA: 0x000B4509 File Offset: 0x000B2709
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
