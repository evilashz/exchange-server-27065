using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009AF RID: 2479
	public class SetOrganizationalUnitCommand : SyntheticCommandWithPipelineInputNoOutput<ExtendedOrganizationalUnit>
	{
		// Token: 0x06007C9A RID: 31898 RVA: 0x000B97E8 File Offset: 0x000B79E8
		private SetOrganizationalUnitCommand() : base("Set-OrganizationalUnit")
		{
		}

		// Token: 0x06007C9B RID: 31899 RVA: 0x000B97F5 File Offset: 0x000B79F5
		public SetOrganizationalUnitCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007C9C RID: 31900 RVA: 0x000B9804 File Offset: 0x000B7A04
		public virtual SetOrganizationalUnitCommand SetParameters(SetOrganizationalUnitCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007C9D RID: 31901 RVA: 0x000B980E File Offset: 0x000B7A0E
		public virtual SetOrganizationalUnitCommand SetParameters(SetOrganizationalUnitCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009B0 RID: 2480
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170054F5 RID: 21749
			// (set) Token: 0x06007C9E RID: 31902 RVA: 0x000B9818 File Offset: 0x000B7A18
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170054F6 RID: 21750
			// (set) Token: 0x06007C9F RID: 31903 RVA: 0x000B982B File Offset: 0x000B7A2B
			public virtual MultiValuedProperty<string> DirSyncStatusAck
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatusAck"] = value;
				}
			}

			// Token: 0x170054F7 RID: 21751
			// (set) Token: 0x06007CA0 RID: 31904 RVA: 0x000B983E File Offset: 0x000B7A3E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170054F8 RID: 21752
			// (set) Token: 0x06007CA1 RID: 31905 RVA: 0x000B9851 File Offset: 0x000B7A51
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170054F9 RID: 21753
			// (set) Token: 0x06007CA2 RID: 31906 RVA: 0x000B9869 File Offset: 0x000B7A69
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170054FA RID: 21754
			// (set) Token: 0x06007CA3 RID: 31907 RVA: 0x000B9881 File Offset: 0x000B7A81
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170054FB RID: 21755
			// (set) Token: 0x06007CA4 RID: 31908 RVA: 0x000B9899 File Offset: 0x000B7A99
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170054FC RID: 21756
			// (set) Token: 0x06007CA5 RID: 31909 RVA: 0x000B98B1 File Offset: 0x000B7AB1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009B1 RID: 2481
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170054FD RID: 21757
			// (set) Token: 0x06007CA7 RID: 31911 RVA: 0x000B98D1 File Offset: 0x000B7AD1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExtendedOrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170054FE RID: 21758
			// (set) Token: 0x06007CA8 RID: 31912 RVA: 0x000B98EF File Offset: 0x000B7AEF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170054FF RID: 21759
			// (set) Token: 0x06007CA9 RID: 31913 RVA: 0x000B9902 File Offset: 0x000B7B02
			public virtual MultiValuedProperty<string> DirSyncStatusAck
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatusAck"] = value;
				}
			}

			// Token: 0x17005500 RID: 21760
			// (set) Token: 0x06007CAA RID: 31914 RVA: 0x000B9915 File Offset: 0x000B7B15
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005501 RID: 21761
			// (set) Token: 0x06007CAB RID: 31915 RVA: 0x000B9928 File Offset: 0x000B7B28
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005502 RID: 21762
			// (set) Token: 0x06007CAC RID: 31916 RVA: 0x000B9940 File Offset: 0x000B7B40
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005503 RID: 21763
			// (set) Token: 0x06007CAD RID: 31917 RVA: 0x000B9958 File Offset: 0x000B7B58
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005504 RID: 21764
			// (set) Token: 0x06007CAE RID: 31918 RVA: 0x000B9970 File Offset: 0x000B7B70
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005505 RID: 21765
			// (set) Token: 0x06007CAF RID: 31919 RVA: 0x000B9988 File Offset: 0x000B7B88
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
