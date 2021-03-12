using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000977 RID: 2423
	public class RemoveSnackyServiceVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADSnackyServiceVirtualDirectory, ADSnackyServiceVirtualDirectory>
	{
		// Token: 0x060078FB RID: 30971 RVA: 0x000B4B8D File Offset: 0x000B2D8D
		private RemoveSnackyServiceVirtualDirectoryCommand() : base("Remove-SnackyServiceVirtualDirectory")
		{
		}

		// Token: 0x060078FC RID: 30972 RVA: 0x000B4B9A File Offset: 0x000B2D9A
		public RemoveSnackyServiceVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060078FD RID: 30973 RVA: 0x000B4BA9 File Offset: 0x000B2DA9
		public virtual RemoveSnackyServiceVirtualDirectoryCommand SetParameters(RemoveSnackyServiceVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060078FE RID: 30974 RVA: 0x000B4BB3 File Offset: 0x000B2DB3
		public virtual RemoveSnackyServiceVirtualDirectoryCommand SetParameters(RemoveSnackyServiceVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000978 RID: 2424
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170051C6 RID: 20934
			// (set) Token: 0x060078FF RID: 30975 RVA: 0x000B4BBD File Offset: 0x000B2DBD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170051C7 RID: 20935
			// (set) Token: 0x06007900 RID: 30976 RVA: 0x000B4BD0 File Offset: 0x000B2DD0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170051C8 RID: 20936
			// (set) Token: 0x06007901 RID: 30977 RVA: 0x000B4BE8 File Offset: 0x000B2DE8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170051C9 RID: 20937
			// (set) Token: 0x06007902 RID: 30978 RVA: 0x000B4C00 File Offset: 0x000B2E00
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170051CA RID: 20938
			// (set) Token: 0x06007903 RID: 30979 RVA: 0x000B4C18 File Offset: 0x000B2E18
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170051CB RID: 20939
			// (set) Token: 0x06007904 RID: 30980 RVA: 0x000B4C30 File Offset: 0x000B2E30
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170051CC RID: 20940
			// (set) Token: 0x06007905 RID: 30981 RVA: 0x000B4C48 File Offset: 0x000B2E48
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000979 RID: 2425
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170051CD RID: 20941
			// (set) Token: 0x06007907 RID: 30983 RVA: 0x000B4C68 File Offset: 0x000B2E68
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170051CE RID: 20942
			// (set) Token: 0x06007908 RID: 30984 RVA: 0x000B4C7B File Offset: 0x000B2E7B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170051CF RID: 20943
			// (set) Token: 0x06007909 RID: 30985 RVA: 0x000B4C8E File Offset: 0x000B2E8E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170051D0 RID: 20944
			// (set) Token: 0x0600790A RID: 30986 RVA: 0x000B4CA6 File Offset: 0x000B2EA6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170051D1 RID: 20945
			// (set) Token: 0x0600790B RID: 30987 RVA: 0x000B4CBE File Offset: 0x000B2EBE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170051D2 RID: 20946
			// (set) Token: 0x0600790C RID: 30988 RVA: 0x000B4CD6 File Offset: 0x000B2ED6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170051D3 RID: 20947
			// (set) Token: 0x0600790D RID: 30989 RVA: 0x000B4CEE File Offset: 0x000B2EEE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170051D4 RID: 20948
			// (set) Token: 0x0600790E RID: 30990 RVA: 0x000B4D06 File Offset: 0x000B2F06
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
