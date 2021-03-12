using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000066 RID: 102
	public class NewMailboxAuditLogSearchCommand : SyntheticCommandWithPipelineInput<MailboxAuditLogSearch, MailboxAuditLogSearch>
	{
		// Token: 0x06001799 RID: 6041 RVA: 0x0003653B File Offset: 0x0003473B
		private NewMailboxAuditLogSearchCommand() : base("New-MailboxAuditLogSearch")
		{
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00036548 File Offset: 0x00034748
		public NewMailboxAuditLogSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00036557 File Offset: 0x00034757
		public virtual NewMailboxAuditLogSearchCommand SetParameters(NewMailboxAuditLogSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00036561 File Offset: 0x00034761
		public virtual NewMailboxAuditLogSearchCommand SetParameters(NewMailboxAuditLogSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000067 RID: 103
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000286 RID: 646
			// (set) Token: 0x0600179D RID: 6045 RVA: 0x0003656B File Offset: 0x0003476B
			public virtual MultiValuedProperty<MailboxIdParameter> Mailboxes
			{
				set
				{
					base.PowerSharpParameters["Mailboxes"] = value;
				}
			}

			// Token: 0x17000287 RID: 647
			// (set) Token: 0x0600179E RID: 6046 RVA: 0x0003657E File Offset: 0x0003477E
			public virtual MultiValuedProperty<AuditScopes> LogonTypes
			{
				set
				{
					base.PowerSharpParameters["LogonTypes"] = value;
				}
			}

			// Token: 0x17000288 RID: 648
			// (set) Token: 0x0600179F RID: 6047 RVA: 0x00036591 File Offset: 0x00034791
			public virtual MultiValuedProperty<MailboxAuditOperations> Operations
			{
				set
				{
					base.PowerSharpParameters["Operations"] = value;
				}
			}

			// Token: 0x17000289 RID: 649
			// (set) Token: 0x060017A0 RID: 6048 RVA: 0x000365A4 File Offset: 0x000347A4
			public virtual SwitchParameter ShowDetails
			{
				set
				{
					base.PowerSharpParameters["ShowDetails"] = value;
				}
			}

			// Token: 0x1700028A RID: 650
			// (set) Token: 0x060017A1 RID: 6049 RVA: 0x000365BC File Offset: 0x000347BC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700028B RID: 651
			// (set) Token: 0x060017A2 RID: 6050 RVA: 0x000365CF File Offset: 0x000347CF
			public virtual ExDateTime StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x1700028C RID: 652
			// (set) Token: 0x060017A3 RID: 6051 RVA: 0x000365E7 File Offset: 0x000347E7
			public virtual ExDateTime EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x1700028D RID: 653
			// (set) Token: 0x060017A4 RID: 6052 RVA: 0x000365FF File Offset: 0x000347FF
			public virtual bool? ExternalAccess
			{
				set
				{
					base.PowerSharpParameters["ExternalAccess"] = value;
				}
			}

			// Token: 0x1700028E RID: 654
			// (set) Token: 0x060017A5 RID: 6053 RVA: 0x00036617 File Offset: 0x00034817
			public virtual MultiValuedProperty<SmtpAddress> StatusMailRecipients
			{
				set
				{
					base.PowerSharpParameters["StatusMailRecipients"] = value;
				}
			}

			// Token: 0x1700028F RID: 655
			// (set) Token: 0x060017A6 RID: 6054 RVA: 0x0003662A File Offset: 0x0003482A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000290 RID: 656
			// (set) Token: 0x060017A7 RID: 6055 RVA: 0x0003663D File Offset: 0x0003483D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000291 RID: 657
			// (set) Token: 0x060017A8 RID: 6056 RVA: 0x00036655 File Offset: 0x00034855
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000292 RID: 658
			// (set) Token: 0x060017A9 RID: 6057 RVA: 0x0003666D File Offset: 0x0003486D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000293 RID: 659
			// (set) Token: 0x060017AA RID: 6058 RVA: 0x00036685 File Offset: 0x00034885
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000294 RID: 660
			// (set) Token: 0x060017AB RID: 6059 RVA: 0x0003669D File Offset: 0x0003489D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000068 RID: 104
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000295 RID: 661
			// (set) Token: 0x060017AD RID: 6061 RVA: 0x000366BD File Offset: 0x000348BD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000296 RID: 662
			// (set) Token: 0x060017AE RID: 6062 RVA: 0x000366DB File Offset: 0x000348DB
			public virtual MultiValuedProperty<MailboxIdParameter> Mailboxes
			{
				set
				{
					base.PowerSharpParameters["Mailboxes"] = value;
				}
			}

			// Token: 0x17000297 RID: 663
			// (set) Token: 0x060017AF RID: 6063 RVA: 0x000366EE File Offset: 0x000348EE
			public virtual MultiValuedProperty<AuditScopes> LogonTypes
			{
				set
				{
					base.PowerSharpParameters["LogonTypes"] = value;
				}
			}

			// Token: 0x17000298 RID: 664
			// (set) Token: 0x060017B0 RID: 6064 RVA: 0x00036701 File Offset: 0x00034901
			public virtual MultiValuedProperty<MailboxAuditOperations> Operations
			{
				set
				{
					base.PowerSharpParameters["Operations"] = value;
				}
			}

			// Token: 0x17000299 RID: 665
			// (set) Token: 0x060017B1 RID: 6065 RVA: 0x00036714 File Offset: 0x00034914
			public virtual SwitchParameter ShowDetails
			{
				set
				{
					base.PowerSharpParameters["ShowDetails"] = value;
				}
			}

			// Token: 0x1700029A RID: 666
			// (set) Token: 0x060017B2 RID: 6066 RVA: 0x0003672C File Offset: 0x0003492C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700029B RID: 667
			// (set) Token: 0x060017B3 RID: 6067 RVA: 0x0003673F File Offset: 0x0003493F
			public virtual ExDateTime StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x1700029C RID: 668
			// (set) Token: 0x060017B4 RID: 6068 RVA: 0x00036757 File Offset: 0x00034957
			public virtual ExDateTime EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x1700029D RID: 669
			// (set) Token: 0x060017B5 RID: 6069 RVA: 0x0003676F File Offset: 0x0003496F
			public virtual bool? ExternalAccess
			{
				set
				{
					base.PowerSharpParameters["ExternalAccess"] = value;
				}
			}

			// Token: 0x1700029E RID: 670
			// (set) Token: 0x060017B6 RID: 6070 RVA: 0x00036787 File Offset: 0x00034987
			public virtual MultiValuedProperty<SmtpAddress> StatusMailRecipients
			{
				set
				{
					base.PowerSharpParameters["StatusMailRecipients"] = value;
				}
			}

			// Token: 0x1700029F RID: 671
			// (set) Token: 0x060017B7 RID: 6071 RVA: 0x0003679A File Offset: 0x0003499A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170002A0 RID: 672
			// (set) Token: 0x060017B8 RID: 6072 RVA: 0x000367AD File Offset: 0x000349AD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170002A1 RID: 673
			// (set) Token: 0x060017B9 RID: 6073 RVA: 0x000367C5 File Offset: 0x000349C5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170002A2 RID: 674
			// (set) Token: 0x060017BA RID: 6074 RVA: 0x000367DD File Offset: 0x000349DD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170002A3 RID: 675
			// (set) Token: 0x060017BB RID: 6075 RVA: 0x000367F5 File Offset: 0x000349F5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170002A4 RID: 676
			// (set) Token: 0x060017BC RID: 6076 RVA: 0x0003680D File Offset: 0x00034A0D
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
