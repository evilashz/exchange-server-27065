using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.TenantMonitoring;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B0B RID: 2827
	public class NewExchangeNotificationCommand : SyntheticCommandWithPipelineInput<Notification, Notification>
	{
		// Token: 0x06008ABE RID: 35518 RVA: 0x000CBE06 File Offset: 0x000CA006
		private NewExchangeNotificationCommand() : base("New-ExchangeNotification")
		{
		}

		// Token: 0x06008ABF RID: 35519 RVA: 0x000CBE13 File Offset: 0x000CA013
		public NewExchangeNotificationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008AC0 RID: 35520 RVA: 0x000CBE22 File Offset: 0x000CA022
		public virtual NewExchangeNotificationCommand SetParameters(NewExchangeNotificationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B0C RID: 2828
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006061 RID: 24673
			// (set) Token: 0x06008AC1 RID: 35521 RVA: 0x000CBE2C File Offset: 0x000CA02C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006062 RID: 24674
			// (set) Token: 0x06008AC2 RID: 35522 RVA: 0x000CBE4A File Offset: 0x000CA04A
			public virtual uint EventInstanceId
			{
				set
				{
					base.PowerSharpParameters["EventInstanceId"] = value;
				}
			}

			// Token: 0x17006063 RID: 24675
			// (set) Token: 0x06008AC3 RID: 35523 RVA: 0x000CBE62 File Offset: 0x000CA062
			public virtual string EventSource
			{
				set
				{
					base.PowerSharpParameters["EventSource"] = value;
				}
			}

			// Token: 0x17006064 RID: 24676
			// (set) Token: 0x06008AC4 RID: 35524 RVA: 0x000CBE75 File Offset: 0x000CA075
			public virtual int EventCategoryId
			{
				set
				{
					base.PowerSharpParameters["EventCategoryId"] = value;
				}
			}

			// Token: 0x17006065 RID: 24677
			// (set) Token: 0x06008AC5 RID: 35525 RVA: 0x000CBE8D File Offset: 0x000CA08D
			public virtual ExDateTime EventTime
			{
				set
				{
					base.PowerSharpParameters["EventTime"] = value;
				}
			}

			// Token: 0x17006066 RID: 24678
			// (set) Token: 0x06008AC6 RID: 35526 RVA: 0x000CBEA5 File Offset: 0x000CA0A5
			public virtual string InsertionStrings
			{
				set
				{
					base.PowerSharpParameters["InsertionStrings"] = value;
				}
			}

			// Token: 0x17006067 RID: 24679
			// (set) Token: 0x06008AC7 RID: 35527 RVA: 0x000CBEB8 File Offset: 0x000CA0B8
			public virtual RecipientIdParameter NotificationRecipients
			{
				set
				{
					base.PowerSharpParameters["NotificationRecipients"] = value;
				}
			}

			// Token: 0x17006068 RID: 24680
			// (set) Token: 0x06008AC8 RID: 35528 RVA: 0x000CBECB File Offset: 0x000CA0CB
			public virtual ExDateTime CreationTime
			{
				set
				{
					base.PowerSharpParameters["CreationTime"] = value;
				}
			}

			// Token: 0x17006069 RID: 24681
			// (set) Token: 0x06008AC9 RID: 35529 RVA: 0x000CBEE3 File Offset: 0x000CA0E3
			public virtual string PeriodicKey
			{
				set
				{
					base.PowerSharpParameters["PeriodicKey"] = value;
				}
			}

			// Token: 0x1700606A RID: 24682
			// (set) Token: 0x06008ACA RID: 35530 RVA: 0x000CBEF6 File Offset: 0x000CA0F6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700606B RID: 24683
			// (set) Token: 0x06008ACB RID: 35531 RVA: 0x000CBF09 File Offset: 0x000CA109
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700606C RID: 24684
			// (set) Token: 0x06008ACC RID: 35532 RVA: 0x000CBF21 File Offset: 0x000CA121
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700606D RID: 24685
			// (set) Token: 0x06008ACD RID: 35533 RVA: 0x000CBF39 File Offset: 0x000CA139
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700606E RID: 24686
			// (set) Token: 0x06008ACE RID: 35534 RVA: 0x000CBF51 File Offset: 0x000CA151
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700606F RID: 24687
			// (set) Token: 0x06008ACF RID: 35535 RVA: 0x000CBF69 File Offset: 0x000CA169
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
