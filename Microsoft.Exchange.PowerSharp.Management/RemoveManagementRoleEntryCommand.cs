using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000361 RID: 865
	public class RemoveManagementRoleEntryCommand : SyntheticCommandWithPipelineInputNoOutput<RoleEntryIdParameter>
	{
		// Token: 0x06003748 RID: 14152 RVA: 0x0005F96E File Offset: 0x0005DB6E
		private RemoveManagementRoleEntryCommand() : base("Remove-ManagementRoleEntry")
		{
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x0005F97B File Offset: 0x0005DB7B
		public RemoveManagementRoleEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x0005F98A File Offset: 0x0005DB8A
		public virtual RemoveManagementRoleEntryCommand SetParameters(RemoveManagementRoleEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x0005F994 File Offset: 0x0005DB94
		public virtual RemoveManagementRoleEntryCommand SetParameters(RemoveManagementRoleEntryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000362 RID: 866
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001C3F RID: 7231
			// (set) Token: 0x0600374C RID: 14156 RVA: 0x0005F99E File Offset: 0x0005DB9E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C40 RID: 7232
			// (set) Token: 0x0600374D RID: 14157 RVA: 0x0005F9B6 File Offset: 0x0005DBB6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C41 RID: 7233
			// (set) Token: 0x0600374E RID: 14158 RVA: 0x0005F9C9 File Offset: 0x0005DBC9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C42 RID: 7234
			// (set) Token: 0x0600374F RID: 14159 RVA: 0x0005F9E1 File Offset: 0x0005DBE1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C43 RID: 7235
			// (set) Token: 0x06003750 RID: 14160 RVA: 0x0005F9F9 File Offset: 0x0005DBF9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C44 RID: 7236
			// (set) Token: 0x06003751 RID: 14161 RVA: 0x0005FA11 File Offset: 0x0005DC11
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C45 RID: 7237
			// (set) Token: 0x06003752 RID: 14162 RVA: 0x0005FA29 File Offset: 0x0005DC29
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001C46 RID: 7238
			// (set) Token: 0x06003753 RID: 14163 RVA: 0x0005FA41 File Offset: 0x0005DC41
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000363 RID: 867
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001C47 RID: 7239
			// (set) Token: 0x06003755 RID: 14165 RVA: 0x0005FA61 File Offset: 0x0005DC61
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleEntryIdParameter(value) : null);
				}
			}

			// Token: 0x17001C48 RID: 7240
			// (set) Token: 0x06003756 RID: 14166 RVA: 0x0005FA7F File Offset: 0x0005DC7F
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C49 RID: 7241
			// (set) Token: 0x06003757 RID: 14167 RVA: 0x0005FA97 File Offset: 0x0005DC97
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C4A RID: 7242
			// (set) Token: 0x06003758 RID: 14168 RVA: 0x0005FAAA File Offset: 0x0005DCAA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C4B RID: 7243
			// (set) Token: 0x06003759 RID: 14169 RVA: 0x0005FAC2 File Offset: 0x0005DCC2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C4C RID: 7244
			// (set) Token: 0x0600375A RID: 14170 RVA: 0x0005FADA File Offset: 0x0005DCDA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C4D RID: 7245
			// (set) Token: 0x0600375B RID: 14171 RVA: 0x0005FAF2 File Offset: 0x0005DCF2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C4E RID: 7246
			// (set) Token: 0x0600375C RID: 14172 RVA: 0x0005FB0A File Offset: 0x0005DD0A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001C4F RID: 7247
			// (set) Token: 0x0600375D RID: 14173 RVA: 0x0005FB22 File Offset: 0x0005DD22
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
