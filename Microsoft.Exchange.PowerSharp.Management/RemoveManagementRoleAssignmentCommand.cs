using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000328 RID: 808
	public class RemoveManagementRoleAssignmentCommand : SyntheticCommandWithPipelineInput<ExchangeRoleAssignment, ExchangeRoleAssignment>
	{
		// Token: 0x060034FB RID: 13563 RVA: 0x0005C97E File Offset: 0x0005AB7E
		private RemoveManagementRoleAssignmentCommand() : base("Remove-ManagementRoleAssignment")
		{
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x0005C98B File Offset: 0x0005AB8B
		public RemoveManagementRoleAssignmentCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x0005C99A File Offset: 0x0005AB9A
		public virtual RemoveManagementRoleAssignmentCommand SetParameters(RemoveManagementRoleAssignmentCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x0005C9A4 File Offset: 0x0005ABA4
		public virtual RemoveManagementRoleAssignmentCommand SetParameters(RemoveManagementRoleAssignmentCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000329 RID: 809
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001A64 RID: 6756
			// (set) Token: 0x060034FF RID: 13567 RVA: 0x0005C9AE File Offset: 0x0005ABAE
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001A65 RID: 6757
			// (set) Token: 0x06003500 RID: 13568 RVA: 0x0005C9C6 File Offset: 0x0005ABC6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A66 RID: 6758
			// (set) Token: 0x06003501 RID: 13569 RVA: 0x0005C9D9 File Offset: 0x0005ABD9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A67 RID: 6759
			// (set) Token: 0x06003502 RID: 13570 RVA: 0x0005C9F1 File Offset: 0x0005ABF1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A68 RID: 6760
			// (set) Token: 0x06003503 RID: 13571 RVA: 0x0005CA09 File Offset: 0x0005AC09
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A69 RID: 6761
			// (set) Token: 0x06003504 RID: 13572 RVA: 0x0005CA21 File Offset: 0x0005AC21
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001A6A RID: 6762
			// (set) Token: 0x06003505 RID: 13573 RVA: 0x0005CA39 File Offset: 0x0005AC39
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001A6B RID: 6763
			// (set) Token: 0x06003506 RID: 13574 RVA: 0x0005CA51 File Offset: 0x0005AC51
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200032A RID: 810
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001A6C RID: 6764
			// (set) Token: 0x06003508 RID: 13576 RVA: 0x0005CA71 File Offset: 0x0005AC71
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleAssignmentIdParameter(value) : null);
				}
			}

			// Token: 0x17001A6D RID: 6765
			// (set) Token: 0x06003509 RID: 13577 RVA: 0x0005CA8F File Offset: 0x0005AC8F
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001A6E RID: 6766
			// (set) Token: 0x0600350A RID: 13578 RVA: 0x0005CAA7 File Offset: 0x0005ACA7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A6F RID: 6767
			// (set) Token: 0x0600350B RID: 13579 RVA: 0x0005CABA File Offset: 0x0005ACBA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A70 RID: 6768
			// (set) Token: 0x0600350C RID: 13580 RVA: 0x0005CAD2 File Offset: 0x0005ACD2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A71 RID: 6769
			// (set) Token: 0x0600350D RID: 13581 RVA: 0x0005CAEA File Offset: 0x0005ACEA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A72 RID: 6770
			// (set) Token: 0x0600350E RID: 13582 RVA: 0x0005CB02 File Offset: 0x0005AD02
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001A73 RID: 6771
			// (set) Token: 0x0600350F RID: 13583 RVA: 0x0005CB1A File Offset: 0x0005AD1A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001A74 RID: 6772
			// (set) Token: 0x06003510 RID: 13584 RVA: 0x0005CB32 File Offset: 0x0005AD32
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
