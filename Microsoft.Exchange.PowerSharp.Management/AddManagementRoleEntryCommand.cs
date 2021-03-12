using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200035B RID: 859
	public class AddManagementRoleEntryCommand : SyntheticCommandWithPipelineInputNoOutput<RoleEntryIdParameter>
	{
		// Token: 0x06003713 RID: 14099 RVA: 0x0005F526 File Offset: 0x0005D726
		private AddManagementRoleEntryCommand() : base("Add-ManagementRoleEntry")
		{
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x0005F533 File Offset: 0x0005D733
		public AddManagementRoleEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x0005F542 File Offset: 0x0005D742
		public virtual AddManagementRoleEntryCommand SetParameters(AddManagementRoleEntryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x0005F54C File Offset: 0x0005D74C
		public virtual AddManagementRoleEntryCommand SetParameters(AddManagementRoleEntryCommand.ParentRoleEntryParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x0005F556 File Offset: 0x0005D756
		public virtual AddManagementRoleEntryCommand SetParameters(AddManagementRoleEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200035C RID: 860
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001C16 RID: 7190
			// (set) Token: 0x06003718 RID: 14104 RVA: 0x0005F560 File Offset: 0x0005D760
			public virtual SwitchParameter UnScopedTopLevel
			{
				set
				{
					base.PowerSharpParameters["UnScopedTopLevel"] = value;
				}
			}

			// Token: 0x17001C17 RID: 7191
			// (set) Token: 0x06003719 RID: 14105 RVA: 0x0005F578 File Offset: 0x0005D778
			public virtual SwitchParameter SkipScriptExistenceCheck
			{
				set
				{
					base.PowerSharpParameters["SkipScriptExistenceCheck"] = value;
				}
			}

			// Token: 0x17001C18 RID: 7192
			// (set) Token: 0x0600371A RID: 14106 RVA: 0x0005F590 File Offset: 0x0005D790
			public virtual string Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x17001C19 RID: 7193
			// (set) Token: 0x0600371B RID: 14107 RVA: 0x0005F5A3 File Offset: 0x0005D7A3
			public virtual string PSSnapinName
			{
				set
				{
					base.PowerSharpParameters["PSSnapinName"] = value;
				}
			}

			// Token: 0x17001C1A RID: 7194
			// (set) Token: 0x0600371C RID: 14108 RVA: 0x0005F5B6 File Offset: 0x0005D7B6
			public virtual ManagementRoleEntryType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17001C1B RID: 7195
			// (set) Token: 0x0600371D RID: 14109 RVA: 0x0005F5CE File Offset: 0x0005D7CE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleEntryIdParameter(value) : null);
				}
			}

			// Token: 0x17001C1C RID: 7196
			// (set) Token: 0x0600371E RID: 14110 RVA: 0x0005F5EC File Offset: 0x0005D7EC
			public virtual SwitchParameter Overwrite
			{
				set
				{
					base.PowerSharpParameters["Overwrite"] = value;
				}
			}

			// Token: 0x17001C1D RID: 7197
			// (set) Token: 0x0600371F RID: 14111 RVA: 0x0005F604 File Offset: 0x0005D804
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C1E RID: 7198
			// (set) Token: 0x06003720 RID: 14112 RVA: 0x0005F61C File Offset: 0x0005D81C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C1F RID: 7199
			// (set) Token: 0x06003721 RID: 14113 RVA: 0x0005F62F File Offset: 0x0005D82F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C20 RID: 7200
			// (set) Token: 0x06003722 RID: 14114 RVA: 0x0005F647 File Offset: 0x0005D847
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C21 RID: 7201
			// (set) Token: 0x06003723 RID: 14115 RVA: 0x0005F65F File Offset: 0x0005D85F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C22 RID: 7202
			// (set) Token: 0x06003724 RID: 14116 RVA: 0x0005F677 File Offset: 0x0005D877
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C23 RID: 7203
			// (set) Token: 0x06003725 RID: 14117 RVA: 0x0005F68F File Offset: 0x0005D88F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200035D RID: 861
		public class ParentRoleEntryParameters : ParametersBase
		{
			// Token: 0x17001C24 RID: 7204
			// (set) Token: 0x06003727 RID: 14119 RVA: 0x0005F6AF File Offset: 0x0005D8AF
			public virtual string ParentRoleEntry
			{
				set
				{
					base.PowerSharpParameters["ParentRoleEntry"] = ((value != null) ? new RoleEntryIdParameter(value) : null);
				}
			}

			// Token: 0x17001C25 RID: 7205
			// (set) Token: 0x06003728 RID: 14120 RVA: 0x0005F6CD File Offset: 0x0005D8CD
			public virtual string Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001C26 RID: 7206
			// (set) Token: 0x06003729 RID: 14121 RVA: 0x0005F6EB File Offset: 0x0005D8EB
			public virtual SwitchParameter Overwrite
			{
				set
				{
					base.PowerSharpParameters["Overwrite"] = value;
				}
			}

			// Token: 0x17001C27 RID: 7207
			// (set) Token: 0x0600372A RID: 14122 RVA: 0x0005F703 File Offset: 0x0005D903
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C28 RID: 7208
			// (set) Token: 0x0600372B RID: 14123 RVA: 0x0005F71B File Offset: 0x0005D91B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C29 RID: 7209
			// (set) Token: 0x0600372C RID: 14124 RVA: 0x0005F72E File Offset: 0x0005D92E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C2A RID: 7210
			// (set) Token: 0x0600372D RID: 14125 RVA: 0x0005F746 File Offset: 0x0005D946
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C2B RID: 7211
			// (set) Token: 0x0600372E RID: 14126 RVA: 0x0005F75E File Offset: 0x0005D95E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C2C RID: 7212
			// (set) Token: 0x0600372F RID: 14127 RVA: 0x0005F776 File Offset: 0x0005D976
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C2D RID: 7213
			// (set) Token: 0x06003730 RID: 14128 RVA: 0x0005F78E File Offset: 0x0005D98E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200035E RID: 862
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001C2E RID: 7214
			// (set) Token: 0x06003732 RID: 14130 RVA: 0x0005F7AE File Offset: 0x0005D9AE
			public virtual SwitchParameter Overwrite
			{
				set
				{
					base.PowerSharpParameters["Overwrite"] = value;
				}
			}

			// Token: 0x17001C2F RID: 7215
			// (set) Token: 0x06003733 RID: 14131 RVA: 0x0005F7C6 File Offset: 0x0005D9C6
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C30 RID: 7216
			// (set) Token: 0x06003734 RID: 14132 RVA: 0x0005F7DE File Offset: 0x0005D9DE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C31 RID: 7217
			// (set) Token: 0x06003735 RID: 14133 RVA: 0x0005F7F1 File Offset: 0x0005D9F1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C32 RID: 7218
			// (set) Token: 0x06003736 RID: 14134 RVA: 0x0005F809 File Offset: 0x0005DA09
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C33 RID: 7219
			// (set) Token: 0x06003737 RID: 14135 RVA: 0x0005F821 File Offset: 0x0005DA21
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C34 RID: 7220
			// (set) Token: 0x06003738 RID: 14136 RVA: 0x0005F839 File Offset: 0x0005DA39
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C35 RID: 7221
			// (set) Token: 0x06003739 RID: 14137 RVA: 0x0005F851 File Offset: 0x0005DA51
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
