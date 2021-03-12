using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D13 RID: 3347
	public class GetMessageCategoryCommand : SyntheticCommandWithPipelineInput<MessageCategory, MessageCategory>
	{
		// Token: 0x0600B1D1 RID: 45521 RVA: 0x001007A3 File Offset: 0x000FE9A3
		private GetMessageCategoryCommand() : base("Get-MessageCategory")
		{
		}

		// Token: 0x0600B1D2 RID: 45522 RVA: 0x001007B0 File Offset: 0x000FE9B0
		public GetMessageCategoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B1D3 RID: 45523 RVA: 0x001007BF File Offset: 0x000FE9BF
		public virtual GetMessageCategoryCommand SetParameters(GetMessageCategoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B1D4 RID: 45524 RVA: 0x001007C9 File Offset: 0x000FE9C9
		public virtual GetMessageCategoryCommand SetParameters(GetMessageCategoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D14 RID: 3348
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008364 RID: 33636
			// (set) Token: 0x0600B1D5 RID: 45525 RVA: 0x001007D3 File Offset: 0x000FE9D3
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008365 RID: 33637
			// (set) Token: 0x0600B1D6 RID: 45526 RVA: 0x001007F1 File Offset: 0x000FE9F1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008366 RID: 33638
			// (set) Token: 0x0600B1D7 RID: 45527 RVA: 0x00100804 File Offset: 0x000FEA04
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008367 RID: 33639
			// (set) Token: 0x0600B1D8 RID: 45528 RVA: 0x0010081C File Offset: 0x000FEA1C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008368 RID: 33640
			// (set) Token: 0x0600B1D9 RID: 45529 RVA: 0x00100834 File Offset: 0x000FEA34
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008369 RID: 33641
			// (set) Token: 0x0600B1DA RID: 45530 RVA: 0x0010084C File Offset: 0x000FEA4C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D15 RID: 3349
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700836A RID: 33642
			// (set) Token: 0x0600B1DC RID: 45532 RVA: 0x0010086C File Offset: 0x000FEA6C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MessageCategoryIdParameter(value) : null);
				}
			}

			// Token: 0x1700836B RID: 33643
			// (set) Token: 0x0600B1DD RID: 45533 RVA: 0x0010088A File Offset: 0x000FEA8A
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700836C RID: 33644
			// (set) Token: 0x0600B1DE RID: 45534 RVA: 0x001008A8 File Offset: 0x000FEAA8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700836D RID: 33645
			// (set) Token: 0x0600B1DF RID: 45535 RVA: 0x001008BB File Offset: 0x000FEABB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700836E RID: 33646
			// (set) Token: 0x0600B1E0 RID: 45536 RVA: 0x001008D3 File Offset: 0x000FEAD3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700836F RID: 33647
			// (set) Token: 0x0600B1E1 RID: 45537 RVA: 0x001008EB File Offset: 0x000FEAEB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008370 RID: 33648
			// (set) Token: 0x0600B1E2 RID: 45538 RVA: 0x00100903 File Offset: 0x000FEB03
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
