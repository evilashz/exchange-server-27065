using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000022 RID: 34
	public class ClearMobileDeviceCommand : SyntheticCommandWithPipelineInput<MobileDevice, MobileDevice>
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x000335BB File Offset: 0x000317BB
		private ClearMobileDeviceCommand() : base("Clear-MobileDevice")
		{
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000335C8 File Offset: 0x000317C8
		public ClearMobileDeviceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000335D7 File Offset: 0x000317D7
		public virtual ClearMobileDeviceCommand SetParameters(ClearMobileDeviceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x000335E1 File Offset: 0x000317E1
		public virtual ClearMobileDeviceCommand SetParameters(ClearMobileDeviceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000023 RID: 35
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170000BD RID: 189
			// (set) Token: 0x0600154C RID: 5452 RVA: 0x000335EB File Offset: 0x000317EB
			public virtual MultiValuedProperty<string> NotificationEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["NotificationEmailAddresses"] = value;
				}
			}

			// Token: 0x170000BE RID: 190
			// (set) Token: 0x0600154D RID: 5453 RVA: 0x000335FE File Offset: 0x000317FE
			public virtual SwitchParameter Cancel
			{
				set
				{
					base.PowerSharpParameters["Cancel"] = value;
				}
			}

			// Token: 0x170000BF RID: 191
			// (set) Token: 0x0600154E RID: 5454 RVA: 0x00033616 File Offset: 0x00031816
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170000C0 RID: 192
			// (set) Token: 0x0600154F RID: 5455 RVA: 0x00033629 File Offset: 0x00031829
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170000C1 RID: 193
			// (set) Token: 0x06001550 RID: 5456 RVA: 0x00033641 File Offset: 0x00031841
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170000C2 RID: 194
			// (set) Token: 0x06001551 RID: 5457 RVA: 0x00033659 File Offset: 0x00031859
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (set) Token: 0x06001552 RID: 5458 RVA: 0x00033671 File Offset: 0x00031871
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (set) Token: 0x06001553 RID: 5459 RVA: 0x00033689 File Offset: 0x00031889
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (set) Token: 0x06001554 RID: 5460 RVA: 0x000336A1 File Offset: 0x000318A1
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000024 RID: 36
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170000C6 RID: 198
			// (set) Token: 0x06001556 RID: 5462 RVA: 0x000336C1 File Offset: 0x000318C1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MobileDeviceIdParameter(value) : null);
				}
			}

			// Token: 0x170000C7 RID: 199
			// (set) Token: 0x06001557 RID: 5463 RVA: 0x000336DF File Offset: 0x000318DF
			public virtual MultiValuedProperty<string> NotificationEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["NotificationEmailAddresses"] = value;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (set) Token: 0x06001558 RID: 5464 RVA: 0x000336F2 File Offset: 0x000318F2
			public virtual SwitchParameter Cancel
			{
				set
				{
					base.PowerSharpParameters["Cancel"] = value;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (set) Token: 0x06001559 RID: 5465 RVA: 0x0003370A File Offset: 0x0003190A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170000CA RID: 202
			// (set) Token: 0x0600155A RID: 5466 RVA: 0x0003371D File Offset: 0x0003191D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170000CB RID: 203
			// (set) Token: 0x0600155B RID: 5467 RVA: 0x00033735 File Offset: 0x00031935
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170000CC RID: 204
			// (set) Token: 0x0600155C RID: 5468 RVA: 0x0003374D File Offset: 0x0003194D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170000CD RID: 205
			// (set) Token: 0x0600155D RID: 5469 RVA: 0x00033765 File Offset: 0x00031965
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170000CE RID: 206
			// (set) Token: 0x0600155E RID: 5470 RVA: 0x0003377D File Offset: 0x0003197D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170000CF RID: 207
			// (set) Token: 0x0600155F RID: 5471 RVA: 0x00033795 File Offset: 0x00031995
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
