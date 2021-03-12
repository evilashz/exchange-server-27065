using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.TenantMonitoring;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B08 RID: 2824
	public class GetExchangeNotificationCommand : SyntheticCommandWithPipelineInput<Notification, Notification>
	{
		// Token: 0x06008AA7 RID: 35495 RVA: 0x000CBC26 File Offset: 0x000C9E26
		private GetExchangeNotificationCommand() : base("Get-ExchangeNotification")
		{
		}

		// Token: 0x06008AA8 RID: 35496 RVA: 0x000CBC33 File Offset: 0x000C9E33
		public GetExchangeNotificationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008AA9 RID: 35497 RVA: 0x000CBC42 File Offset: 0x000C9E42
		public virtual GetExchangeNotificationCommand SetParameters(GetExchangeNotificationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008AAA RID: 35498 RVA: 0x000CBC4C File Offset: 0x000C9E4C
		public virtual GetExchangeNotificationCommand SetParameters(GetExchangeNotificationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B09 RID: 2825
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006050 RID: 24656
			// (set) Token: 0x06008AAB RID: 35499 RVA: 0x000CBC56 File Offset: 0x000C9E56
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006051 RID: 24657
			// (set) Token: 0x06008AAC RID: 35500 RVA: 0x000CBC74 File Offset: 0x000C9E74
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006052 RID: 24658
			// (set) Token: 0x06008AAD RID: 35501 RVA: 0x000CBC8C File Offset: 0x000C9E8C
			public virtual SwitchParameter ShowDuplicates
			{
				set
				{
					base.PowerSharpParameters["ShowDuplicates"] = value;
				}
			}

			// Token: 0x17006053 RID: 24659
			// (set) Token: 0x06008AAE RID: 35502 RVA: 0x000CBCA4 File Offset: 0x000C9EA4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006054 RID: 24660
			// (set) Token: 0x06008AAF RID: 35503 RVA: 0x000CBCB7 File Offset: 0x000C9EB7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006055 RID: 24661
			// (set) Token: 0x06008AB0 RID: 35504 RVA: 0x000CBCCF File Offset: 0x000C9ECF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006056 RID: 24662
			// (set) Token: 0x06008AB1 RID: 35505 RVA: 0x000CBCE7 File Offset: 0x000C9EE7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006057 RID: 24663
			// (set) Token: 0x06008AB2 RID: 35506 RVA: 0x000CBCFF File Offset: 0x000C9EFF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B0A RID: 2826
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006058 RID: 24664
			// (set) Token: 0x06008AB4 RID: 35508 RVA: 0x000CBD1F File Offset: 0x000C9F1F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new NotificationIdParameter(value) : null);
				}
			}

			// Token: 0x17006059 RID: 24665
			// (set) Token: 0x06008AB5 RID: 35509 RVA: 0x000CBD3D File Offset: 0x000C9F3D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700605A RID: 24666
			// (set) Token: 0x06008AB6 RID: 35510 RVA: 0x000CBD5B File Offset: 0x000C9F5B
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700605B RID: 24667
			// (set) Token: 0x06008AB7 RID: 35511 RVA: 0x000CBD73 File Offset: 0x000C9F73
			public virtual SwitchParameter ShowDuplicates
			{
				set
				{
					base.PowerSharpParameters["ShowDuplicates"] = value;
				}
			}

			// Token: 0x1700605C RID: 24668
			// (set) Token: 0x06008AB8 RID: 35512 RVA: 0x000CBD8B File Offset: 0x000C9F8B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700605D RID: 24669
			// (set) Token: 0x06008AB9 RID: 35513 RVA: 0x000CBD9E File Offset: 0x000C9F9E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700605E RID: 24670
			// (set) Token: 0x06008ABA RID: 35514 RVA: 0x000CBDB6 File Offset: 0x000C9FB6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700605F RID: 24671
			// (set) Token: 0x06008ABB RID: 35515 RVA: 0x000CBDCE File Offset: 0x000C9FCE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006060 RID: 24672
			// (set) Token: 0x06008ABC RID: 35516 RVA: 0x000CBDE6 File Offset: 0x000C9FE6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
