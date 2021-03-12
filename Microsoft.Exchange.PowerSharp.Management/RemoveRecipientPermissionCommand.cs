using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Permission;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000381 RID: 897
	public class RemoveRecipientPermissionCommand : SyntheticCommandWithPipelineInput<RecipientPermission, RecipientPermission>
	{
		// Token: 0x06003874 RID: 14452 RVA: 0x00061169 File Offset: 0x0005F369
		private RemoveRecipientPermissionCommand() : base("Remove-RecipientPermission")
		{
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x00061176 File Offset: 0x0005F376
		public RemoveRecipientPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x00061185 File Offset: 0x0005F385
		public virtual RemoveRecipientPermissionCommand SetParameters(RemoveRecipientPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x0006118F File Offset: 0x0005F38F
		public virtual RemoveRecipientPermissionCommand SetParameters(RemoveRecipientPermissionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000382 RID: 898
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001D2B RID: 7467
			// (set) Token: 0x06003878 RID: 14456 RVA: 0x00061199 File Offset: 0x0005F399
			public virtual string Trustee
			{
				set
				{
					base.PowerSharpParameters["Trustee"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17001D2C RID: 7468
			// (set) Token: 0x06003879 RID: 14457 RVA: 0x000611B7 File Offset: 0x0005F3B7
			public virtual MultiValuedProperty<RecipientAccessRight> AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17001D2D RID: 7469
			// (set) Token: 0x0600387A RID: 14458 RVA: 0x000611CA File Offset: 0x0005F3CA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D2E RID: 7470
			// (set) Token: 0x0600387B RID: 14459 RVA: 0x000611DD File Offset: 0x0005F3DD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D2F RID: 7471
			// (set) Token: 0x0600387C RID: 14460 RVA: 0x000611F5 File Offset: 0x0005F3F5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D30 RID: 7472
			// (set) Token: 0x0600387D RID: 14461 RVA: 0x0006120D File Offset: 0x0005F40D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D31 RID: 7473
			// (set) Token: 0x0600387E RID: 14462 RVA: 0x00061225 File Offset: 0x0005F425
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001D32 RID: 7474
			// (set) Token: 0x0600387F RID: 14463 RVA: 0x0006123D File Offset: 0x0005F43D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001D33 RID: 7475
			// (set) Token: 0x06003880 RID: 14464 RVA: 0x00061255 File Offset: 0x0005F455
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000383 RID: 899
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001D34 RID: 7476
			// (set) Token: 0x06003882 RID: 14466 RVA: 0x00061275 File Offset: 0x0005F475
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17001D35 RID: 7477
			// (set) Token: 0x06003883 RID: 14467 RVA: 0x00061293 File Offset: 0x0005F493
			public virtual string Trustee
			{
				set
				{
					base.PowerSharpParameters["Trustee"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17001D36 RID: 7478
			// (set) Token: 0x06003884 RID: 14468 RVA: 0x000612B1 File Offset: 0x0005F4B1
			public virtual MultiValuedProperty<RecipientAccessRight> AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17001D37 RID: 7479
			// (set) Token: 0x06003885 RID: 14469 RVA: 0x000612C4 File Offset: 0x0005F4C4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D38 RID: 7480
			// (set) Token: 0x06003886 RID: 14470 RVA: 0x000612D7 File Offset: 0x0005F4D7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D39 RID: 7481
			// (set) Token: 0x06003887 RID: 14471 RVA: 0x000612EF File Offset: 0x0005F4EF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D3A RID: 7482
			// (set) Token: 0x06003888 RID: 14472 RVA: 0x00061307 File Offset: 0x0005F507
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D3B RID: 7483
			// (set) Token: 0x06003889 RID: 14473 RVA: 0x0006131F File Offset: 0x0005F51F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001D3C RID: 7484
			// (set) Token: 0x0600388A RID: 14474 RVA: 0x00061337 File Offset: 0x0005F537
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001D3D RID: 7485
			// (set) Token: 0x0600388B RID: 14475 RVA: 0x0006134F File Offset: 0x0005F54F
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
