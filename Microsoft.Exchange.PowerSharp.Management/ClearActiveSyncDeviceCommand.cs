using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000025 RID: 37
	public class ClearActiveSyncDeviceCommand : SyntheticCommandWithPipelineInputNoOutput<MobileDeviceIdParameter>
	{
		// Token: 0x06001561 RID: 5473 RVA: 0x000337B5 File Offset: 0x000319B5
		private ClearActiveSyncDeviceCommand() : base("Clear-ActiveSyncDevice")
		{
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x000337C2 File Offset: 0x000319C2
		public ClearActiveSyncDeviceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x000337D1 File Offset: 0x000319D1
		public virtual ClearActiveSyncDeviceCommand SetParameters(ClearActiveSyncDeviceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x000337DB File Offset: 0x000319DB
		public virtual ClearActiveSyncDeviceCommand SetParameters(ClearActiveSyncDeviceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000026 RID: 38
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170000D0 RID: 208
			// (set) Token: 0x06001565 RID: 5477 RVA: 0x000337E5 File Offset: 0x000319E5
			public virtual MultiValuedProperty<string> NotificationEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["NotificationEmailAddresses"] = value;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (set) Token: 0x06001566 RID: 5478 RVA: 0x000337F8 File Offset: 0x000319F8
			public virtual SwitchParameter Cancel
			{
				set
				{
					base.PowerSharpParameters["Cancel"] = value;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (set) Token: 0x06001567 RID: 5479 RVA: 0x00033810 File Offset: 0x00031A10
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (set) Token: 0x06001568 RID: 5480 RVA: 0x00033823 File Offset: 0x00031A23
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170000D4 RID: 212
			// (set) Token: 0x06001569 RID: 5481 RVA: 0x0003383B File Offset: 0x00031A3B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (set) Token: 0x0600156A RID: 5482 RVA: 0x00033853 File Offset: 0x00031A53
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170000D6 RID: 214
			// (set) Token: 0x0600156B RID: 5483 RVA: 0x0003386B File Offset: 0x00031A6B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170000D7 RID: 215
			// (set) Token: 0x0600156C RID: 5484 RVA: 0x00033883 File Offset: 0x00031A83
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170000D8 RID: 216
			// (set) Token: 0x0600156D RID: 5485 RVA: 0x0003389B File Offset: 0x00031A9B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000027 RID: 39
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170000D9 RID: 217
			// (set) Token: 0x0600156F RID: 5487 RVA: 0x000338BB File Offset: 0x00031ABB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MobileDeviceIdParameter(value) : null);
				}
			}

			// Token: 0x170000DA RID: 218
			// (set) Token: 0x06001570 RID: 5488 RVA: 0x000338D9 File Offset: 0x00031AD9
			public virtual MultiValuedProperty<string> NotificationEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["NotificationEmailAddresses"] = value;
				}
			}

			// Token: 0x170000DB RID: 219
			// (set) Token: 0x06001571 RID: 5489 RVA: 0x000338EC File Offset: 0x00031AEC
			public virtual SwitchParameter Cancel
			{
				set
				{
					base.PowerSharpParameters["Cancel"] = value;
				}
			}

			// Token: 0x170000DC RID: 220
			// (set) Token: 0x06001572 RID: 5490 RVA: 0x00033904 File Offset: 0x00031B04
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170000DD RID: 221
			// (set) Token: 0x06001573 RID: 5491 RVA: 0x00033917 File Offset: 0x00031B17
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170000DE RID: 222
			// (set) Token: 0x06001574 RID: 5492 RVA: 0x0003392F File Offset: 0x00031B2F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170000DF RID: 223
			// (set) Token: 0x06001575 RID: 5493 RVA: 0x00033947 File Offset: 0x00031B47
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170000E0 RID: 224
			// (set) Token: 0x06001576 RID: 5494 RVA: 0x0003395F File Offset: 0x00031B5F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170000E1 RID: 225
			// (set) Token: 0x06001577 RID: 5495 RVA: 0x00033977 File Offset: 0x00031B77
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170000E2 RID: 226
			// (set) Token: 0x06001578 RID: 5496 RVA: 0x0003398F File Offset: 0x00031B8F
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
