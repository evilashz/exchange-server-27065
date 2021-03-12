using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Permission;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200037B RID: 891
	public class AddRecipientPermissionCommand : SyntheticCommandWithPipelineInput<RecipientPermission, RecipientPermission>
	{
		// Token: 0x06003842 RID: 14402 RVA: 0x00060D5D File Offset: 0x0005EF5D
		private AddRecipientPermissionCommand() : base("Add-RecipientPermission")
		{
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x00060D6A File Offset: 0x0005EF6A
		public AddRecipientPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x00060D79 File Offset: 0x0005EF79
		public virtual AddRecipientPermissionCommand SetParameters(AddRecipientPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x00060D83 File Offset: 0x0005EF83
		public virtual AddRecipientPermissionCommand SetParameters(AddRecipientPermissionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200037C RID: 892
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001D05 RID: 7429
			// (set) Token: 0x06003846 RID: 14406 RVA: 0x00060D8D File Offset: 0x0005EF8D
			public virtual string Trustee
			{
				set
				{
					base.PowerSharpParameters["Trustee"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17001D06 RID: 7430
			// (set) Token: 0x06003847 RID: 14407 RVA: 0x00060DAB File Offset: 0x0005EFAB
			public virtual MultiValuedProperty<RecipientAccessRight> AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17001D07 RID: 7431
			// (set) Token: 0x06003848 RID: 14408 RVA: 0x00060DBE File Offset: 0x0005EFBE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D08 RID: 7432
			// (set) Token: 0x06003849 RID: 14409 RVA: 0x00060DD1 File Offset: 0x0005EFD1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D09 RID: 7433
			// (set) Token: 0x0600384A RID: 14410 RVA: 0x00060DE9 File Offset: 0x0005EFE9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D0A RID: 7434
			// (set) Token: 0x0600384B RID: 14411 RVA: 0x00060E01 File Offset: 0x0005F001
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D0B RID: 7435
			// (set) Token: 0x0600384C RID: 14412 RVA: 0x00060E19 File Offset: 0x0005F019
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001D0C RID: 7436
			// (set) Token: 0x0600384D RID: 14413 RVA: 0x00060E31 File Offset: 0x0005F031
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001D0D RID: 7437
			// (set) Token: 0x0600384E RID: 14414 RVA: 0x00060E49 File Offset: 0x0005F049
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200037D RID: 893
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001D0E RID: 7438
			// (set) Token: 0x06003850 RID: 14416 RVA: 0x00060E69 File Offset: 0x0005F069
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17001D0F RID: 7439
			// (set) Token: 0x06003851 RID: 14417 RVA: 0x00060E87 File Offset: 0x0005F087
			public virtual string Trustee
			{
				set
				{
					base.PowerSharpParameters["Trustee"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17001D10 RID: 7440
			// (set) Token: 0x06003852 RID: 14418 RVA: 0x00060EA5 File Offset: 0x0005F0A5
			public virtual MultiValuedProperty<RecipientAccessRight> AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17001D11 RID: 7441
			// (set) Token: 0x06003853 RID: 14419 RVA: 0x00060EB8 File Offset: 0x0005F0B8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D12 RID: 7442
			// (set) Token: 0x06003854 RID: 14420 RVA: 0x00060ECB File Offset: 0x0005F0CB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D13 RID: 7443
			// (set) Token: 0x06003855 RID: 14421 RVA: 0x00060EE3 File Offset: 0x0005F0E3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D14 RID: 7444
			// (set) Token: 0x06003856 RID: 14422 RVA: 0x00060EFB File Offset: 0x0005F0FB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D15 RID: 7445
			// (set) Token: 0x06003857 RID: 14423 RVA: 0x00060F13 File Offset: 0x0005F113
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001D16 RID: 7446
			// (set) Token: 0x06003858 RID: 14424 RVA: 0x00060F2B File Offset: 0x0005F12B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001D17 RID: 7447
			// (set) Token: 0x06003859 RID: 14425 RVA: 0x00060F43 File Offset: 0x0005F143
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
