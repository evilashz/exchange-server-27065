using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200033F RID: 831
	public class RemoveRoleGroupCommand : SyntheticCommandWithPipelineInput<ADGroup, ADGroup>
	{
		// Token: 0x06003616 RID: 13846 RVA: 0x0005E07C File Offset: 0x0005C27C
		private RemoveRoleGroupCommand() : base("Remove-RoleGroup")
		{
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x0005E089 File Offset: 0x0005C289
		public RemoveRoleGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x0005E098 File Offset: 0x0005C298
		public virtual RemoveRoleGroupCommand SetParameters(RemoveRoleGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x0005E0A2 File Offset: 0x0005C2A2
		public virtual RemoveRoleGroupCommand SetParameters(RemoveRoleGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000340 RID: 832
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001B51 RID: 6993
			// (set) Token: 0x0600361A RID: 13850 RVA: 0x0005E0AC File Offset: 0x0005C2AC
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17001B52 RID: 6994
			// (set) Token: 0x0600361B RID: 13851 RVA: 0x0005E0C4 File Offset: 0x0005C2C4
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17001B53 RID: 6995
			// (set) Token: 0x0600361C RID: 13852 RVA: 0x0005E0DC File Offset: 0x0005C2DC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001B54 RID: 6996
			// (set) Token: 0x0600361D RID: 13853 RVA: 0x0005E0F4 File Offset: 0x0005C2F4
			public virtual SwitchParameter RemoveWellKnownObjectGuid
			{
				set
				{
					base.PowerSharpParameters["RemoveWellKnownObjectGuid"] = value;
				}
			}

			// Token: 0x17001B55 RID: 6997
			// (set) Token: 0x0600361E RID: 13854 RVA: 0x0005E10C File Offset: 0x0005C30C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B56 RID: 6998
			// (set) Token: 0x0600361F RID: 13855 RVA: 0x0005E11F File Offset: 0x0005C31F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B57 RID: 6999
			// (set) Token: 0x06003620 RID: 13856 RVA: 0x0005E137 File Offset: 0x0005C337
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B58 RID: 7000
			// (set) Token: 0x06003621 RID: 13857 RVA: 0x0005E14F File Offset: 0x0005C34F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B59 RID: 7001
			// (set) Token: 0x06003622 RID: 13858 RVA: 0x0005E167 File Offset: 0x0005C367
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001B5A RID: 7002
			// (set) Token: 0x06003623 RID: 13859 RVA: 0x0005E17F File Offset: 0x0005C37F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001B5B RID: 7003
			// (set) Token: 0x06003624 RID: 13860 RVA: 0x0005E197 File Offset: 0x0005C397
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000341 RID: 833
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001B5C RID: 7004
			// (set) Token: 0x06003626 RID: 13862 RVA: 0x0005E1B7 File Offset: 0x0005C3B7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17001B5D RID: 7005
			// (set) Token: 0x06003627 RID: 13863 RVA: 0x0005E1D5 File Offset: 0x0005C3D5
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17001B5E RID: 7006
			// (set) Token: 0x06003628 RID: 13864 RVA: 0x0005E1ED File Offset: 0x0005C3ED
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17001B5F RID: 7007
			// (set) Token: 0x06003629 RID: 13865 RVA: 0x0005E205 File Offset: 0x0005C405
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001B60 RID: 7008
			// (set) Token: 0x0600362A RID: 13866 RVA: 0x0005E21D File Offset: 0x0005C41D
			public virtual SwitchParameter RemoveWellKnownObjectGuid
			{
				set
				{
					base.PowerSharpParameters["RemoveWellKnownObjectGuid"] = value;
				}
			}

			// Token: 0x17001B61 RID: 7009
			// (set) Token: 0x0600362B RID: 13867 RVA: 0x0005E235 File Offset: 0x0005C435
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B62 RID: 7010
			// (set) Token: 0x0600362C RID: 13868 RVA: 0x0005E248 File Offset: 0x0005C448
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B63 RID: 7011
			// (set) Token: 0x0600362D RID: 13869 RVA: 0x0005E260 File Offset: 0x0005C460
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B64 RID: 7012
			// (set) Token: 0x0600362E RID: 13870 RVA: 0x0005E278 File Offset: 0x0005C478
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B65 RID: 7013
			// (set) Token: 0x0600362F RID: 13871 RVA: 0x0005E290 File Offset: 0x0005C490
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001B66 RID: 7014
			// (set) Token: 0x06003630 RID: 13872 RVA: 0x0005E2A8 File Offset: 0x0005C4A8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001B67 RID: 7015
			// (set) Token: 0x06003631 RID: 13873 RVA: 0x0005E2C0 File Offset: 0x0005C4C0
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
