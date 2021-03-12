using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000434 RID: 1076
	public class SetNotificationCommand : SyntheticCommandWithPipelineInputNoOutput<AsyncOperationNotification>
	{
		// Token: 0x06003E8F RID: 16015 RVA: 0x00068F39 File Offset: 0x00067139
		private SetNotificationCommand() : base("Set-Notification")
		{
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x00068F46 File Offset: 0x00067146
		public SetNotificationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x00068F55 File Offset: 0x00067155
		public virtual SetNotificationCommand SetParameters(SetNotificationCommand.SettingsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x00068F5F File Offset: 0x0006715F
		public virtual SetNotificationCommand SetParameters(SetNotificationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x00068F69 File Offset: 0x00067169
		public virtual SetNotificationCommand SetParameters(SetNotificationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000435 RID: 1077
		public class SettingsParameters : ParametersBase
		{
			// Token: 0x170021E0 RID: 8672
			// (set) Token: 0x06003E94 RID: 16020 RVA: 0x00068F73 File Offset: 0x00067173
			public virtual AsyncOperationType ProcessType
			{
				set
				{
					base.PowerSharpParameters["ProcessType"] = value;
				}
			}

			// Token: 0x170021E1 RID: 8673
			// (set) Token: 0x06003E95 RID: 16021 RVA: 0x00068F8B File Offset: 0x0006718B
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x170021E2 RID: 8674
			// (set) Token: 0x06003E96 RID: 16022 RVA: 0x00068F9E File Offset: 0x0006719E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170021E3 RID: 8675
			// (set) Token: 0x06003E97 RID: 16023 RVA: 0x00068FB1 File Offset: 0x000671B1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021E4 RID: 8676
			// (set) Token: 0x06003E98 RID: 16024 RVA: 0x00068FC9 File Offset: 0x000671C9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021E5 RID: 8677
			// (set) Token: 0x06003E99 RID: 16025 RVA: 0x00068FE1 File Offset: 0x000671E1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021E6 RID: 8678
			// (set) Token: 0x06003E9A RID: 16026 RVA: 0x00068FF9 File Offset: 0x000671F9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170021E7 RID: 8679
			// (set) Token: 0x06003E9B RID: 16027 RVA: 0x00069011 File Offset: 0x00067211
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000436 RID: 1078
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170021E8 RID: 8680
			// (set) Token: 0x06003E9D RID: 16029 RVA: 0x00069031 File Offset: 0x00067231
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x170021E9 RID: 8681
			// (set) Token: 0x06003E9E RID: 16030 RVA: 0x00069044 File Offset: 0x00067244
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170021EA RID: 8682
			// (set) Token: 0x06003E9F RID: 16031 RVA: 0x00069062 File Offset: 0x00067262
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new EwsStoreObjectIdParameter(value) : null);
				}
			}

			// Token: 0x170021EB RID: 8683
			// (set) Token: 0x06003EA0 RID: 16032 RVA: 0x00069080 File Offset: 0x00067280
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170021EC RID: 8684
			// (set) Token: 0x06003EA1 RID: 16033 RVA: 0x00069093 File Offset: 0x00067293
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021ED RID: 8685
			// (set) Token: 0x06003EA2 RID: 16034 RVA: 0x000690AB File Offset: 0x000672AB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021EE RID: 8686
			// (set) Token: 0x06003EA3 RID: 16035 RVA: 0x000690C3 File Offset: 0x000672C3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021EF RID: 8687
			// (set) Token: 0x06003EA4 RID: 16036 RVA: 0x000690DB File Offset: 0x000672DB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170021F0 RID: 8688
			// (set) Token: 0x06003EA5 RID: 16037 RVA: 0x000690F3 File Offset: 0x000672F3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000437 RID: 1079
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170021F1 RID: 8689
			// (set) Token: 0x06003EA7 RID: 16039 RVA: 0x00069113 File Offset: 0x00067313
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170021F2 RID: 8690
			// (set) Token: 0x06003EA8 RID: 16040 RVA: 0x00069126 File Offset: 0x00067326
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021F3 RID: 8691
			// (set) Token: 0x06003EA9 RID: 16041 RVA: 0x0006913E File Offset: 0x0006733E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021F4 RID: 8692
			// (set) Token: 0x06003EAA RID: 16042 RVA: 0x00069156 File Offset: 0x00067356
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021F5 RID: 8693
			// (set) Token: 0x06003EAB RID: 16043 RVA: 0x0006916E File Offset: 0x0006736E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170021F6 RID: 8694
			// (set) Token: 0x06003EAC RID: 16044 RVA: 0x00069186 File Offset: 0x00067386
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
