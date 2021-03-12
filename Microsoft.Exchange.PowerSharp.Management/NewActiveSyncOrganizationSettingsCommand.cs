using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200004A RID: 74
	public class NewActiveSyncOrganizationSettingsCommand : SyntheticCommandWithPipelineInput<ActiveSyncOrganizationSettings, ActiveSyncOrganizationSettings>
	{
		// Token: 0x0600169C RID: 5788 RVA: 0x0003511C File Offset: 0x0003331C
		private NewActiveSyncOrganizationSettingsCommand() : base("New-ActiveSyncOrganizationSettings")
		{
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00035129 File Offset: 0x00033329
		public NewActiveSyncOrganizationSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00035138 File Offset: 0x00033338
		public virtual NewActiveSyncOrganizationSettingsCommand SetParameters(NewActiveSyncOrganizationSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200004B RID: 75
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170001C1 RID: 449
			// (set) Token: 0x0600169F RID: 5791 RVA: 0x00035142 File Offset: 0x00033342
			public virtual DeviceAccessLevel DefaultAccessLevel
			{
				set
				{
					base.PowerSharpParameters["DefaultAccessLevel"] = value;
				}
			}

			// Token: 0x170001C2 RID: 450
			// (set) Token: 0x060016A0 RID: 5792 RVA: 0x0003515A File Offset: 0x0003335A
			public virtual string UserMailInsert
			{
				set
				{
					base.PowerSharpParameters["UserMailInsert"] = value;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (set) Token: 0x060016A1 RID: 5793 RVA: 0x0003516D File Offset: 0x0003336D
			public virtual MultiValuedProperty<SmtpAddress> AdminMailRecipients
			{
				set
				{
					base.PowerSharpParameters["AdminMailRecipients"] = value;
				}
			}

			// Token: 0x170001C4 RID: 452
			// (set) Token: 0x060016A2 RID: 5794 RVA: 0x00035180 File Offset: 0x00033380
			public virtual string OtaNotificationMailInsert
			{
				set
				{
					base.PowerSharpParameters["OtaNotificationMailInsert"] = value;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (set) Token: 0x060016A3 RID: 5795 RVA: 0x00035193 File Offset: 0x00033393
			public virtual bool AllowAccessForUnSupportedPlatform
			{
				set
				{
					base.PowerSharpParameters["AllowAccessForUnSupportedPlatform"] = value;
				}
			}

			// Token: 0x170001C6 RID: 454
			// (set) Token: 0x060016A4 RID: 5796 RVA: 0x000351AB File Offset: 0x000333AB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170001C7 RID: 455
			// (set) Token: 0x060016A5 RID: 5797 RVA: 0x000351C9 File Offset: 0x000333C9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170001C8 RID: 456
			// (set) Token: 0x060016A6 RID: 5798 RVA: 0x000351DC File Offset: 0x000333DC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170001C9 RID: 457
			// (set) Token: 0x060016A7 RID: 5799 RVA: 0x000351F4 File Offset: 0x000333F4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170001CA RID: 458
			// (set) Token: 0x060016A8 RID: 5800 RVA: 0x0003520C File Offset: 0x0003340C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170001CB RID: 459
			// (set) Token: 0x060016A9 RID: 5801 RVA: 0x00035224 File Offset: 0x00033424
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170001CC RID: 460
			// (set) Token: 0x060016AA RID: 5802 RVA: 0x0003523C File Offset: 0x0003343C
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
