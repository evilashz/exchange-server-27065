using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E1E RID: 3614
	public class NewUMCallAnsweringRuleCommand : SyntheticCommandWithPipelineInput<UMCallAnsweringRule, UMCallAnsweringRule>
	{
		// Token: 0x0600D6CA RID: 54986 RVA: 0x001312BE File Offset: 0x0012F4BE
		private NewUMCallAnsweringRuleCommand() : base("New-UMCallAnsweringRule")
		{
		}

		// Token: 0x0600D6CB RID: 54987 RVA: 0x001312CB File Offset: 0x0012F4CB
		public NewUMCallAnsweringRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D6CC RID: 54988 RVA: 0x001312DA File Offset: 0x0012F4DA
		public virtual NewUMCallAnsweringRuleCommand SetParameters(NewUMCallAnsweringRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E1F RID: 3615
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A647 RID: 42567
			// (set) Token: 0x0600D6CD RID: 54989 RVA: 0x001312E4 File Offset: 0x0012F4E4
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A648 RID: 42568
			// (set) Token: 0x0600D6CE RID: 54990 RVA: 0x00131302 File Offset: 0x0012F502
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A649 RID: 42569
			// (set) Token: 0x0600D6CF RID: 54991 RVA: 0x00131320 File Offset: 0x0012F520
			public virtual MultiValuedProperty<CallerIdItem> CallerIds
			{
				set
				{
					base.PowerSharpParameters["CallerIds"] = value;
				}
			}

			// Token: 0x1700A64A RID: 42570
			// (set) Token: 0x0600D6D0 RID: 54992 RVA: 0x00131333 File Offset: 0x0012F533
			public virtual bool CallersCanInterruptGreeting
			{
				set
				{
					base.PowerSharpParameters["CallersCanInterruptGreeting"] = value;
				}
			}

			// Token: 0x1700A64B RID: 42571
			// (set) Token: 0x0600D6D1 RID: 54993 RVA: 0x0013134B File Offset: 0x0012F54B
			public virtual bool CheckAutomaticReplies
			{
				set
				{
					base.PowerSharpParameters["CheckAutomaticReplies"] = value;
				}
			}

			// Token: 0x1700A64C RID: 42572
			// (set) Token: 0x0600D6D2 RID: 54994 RVA: 0x00131363 File Offset: 0x0012F563
			public virtual MultiValuedProperty<string> ExtensionsDialed
			{
				set
				{
					base.PowerSharpParameters["ExtensionsDialed"] = value;
				}
			}

			// Token: 0x1700A64D RID: 42573
			// (set) Token: 0x0600D6D3 RID: 54995 RVA: 0x00131376 File Offset: 0x0012F576
			public virtual MultiValuedProperty<KeyMapping> KeyMappings
			{
				set
				{
					base.PowerSharpParameters["KeyMappings"] = value;
				}
			}

			// Token: 0x1700A64E RID: 42574
			// (set) Token: 0x0600D6D4 RID: 54996 RVA: 0x00131389 File Offset: 0x0012F589
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A64F RID: 42575
			// (set) Token: 0x0600D6D5 RID: 54997 RVA: 0x0013139C File Offset: 0x0012F59C
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x1700A650 RID: 42576
			// (set) Token: 0x0600D6D6 RID: 54998 RVA: 0x001313B4 File Offset: 0x0012F5B4
			public virtual int ScheduleStatus
			{
				set
				{
					base.PowerSharpParameters["ScheduleStatus"] = value;
				}
			}

			// Token: 0x1700A651 RID: 42577
			// (set) Token: 0x0600D6D7 RID: 54999 RVA: 0x001313CC File Offset: 0x0012F5CC
			public virtual TimeOfDay TimeOfDay
			{
				set
				{
					base.PowerSharpParameters["TimeOfDay"] = value;
				}
			}

			// Token: 0x1700A652 RID: 42578
			// (set) Token: 0x0600D6D8 RID: 55000 RVA: 0x001313DF File Offset: 0x0012F5DF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A653 RID: 42579
			// (set) Token: 0x0600D6D9 RID: 55001 RVA: 0x001313F2 File Offset: 0x0012F5F2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A654 RID: 42580
			// (set) Token: 0x0600D6DA RID: 55002 RVA: 0x0013140A File Offset: 0x0012F60A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A655 RID: 42581
			// (set) Token: 0x0600D6DB RID: 55003 RVA: 0x00131422 File Offset: 0x0012F622
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A656 RID: 42582
			// (set) Token: 0x0600D6DC RID: 55004 RVA: 0x0013143A File Offset: 0x0012F63A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A657 RID: 42583
			// (set) Token: 0x0600D6DD RID: 55005 RVA: 0x00131452 File Offset: 0x0012F652
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
