using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E23 RID: 3619
	public class SetUMCallAnsweringRuleCommand : SyntheticCommandWithPipelineInputNoOutput<UMCallAnsweringRule>
	{
		// Token: 0x0600D6F6 RID: 55030 RVA: 0x00131652 File Offset: 0x0012F852
		private SetUMCallAnsweringRuleCommand() : base("Set-UMCallAnsweringRule")
		{
		}

		// Token: 0x0600D6F7 RID: 55031 RVA: 0x0013165F File Offset: 0x0012F85F
		public SetUMCallAnsweringRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D6F8 RID: 55032 RVA: 0x0013166E File Offset: 0x0012F86E
		public virtual SetUMCallAnsweringRuleCommand SetParameters(SetUMCallAnsweringRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D6F9 RID: 55033 RVA: 0x00131678 File Offset: 0x0012F878
		public virtual SetUMCallAnsweringRuleCommand SetParameters(SetUMCallAnsweringRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E24 RID: 3620
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A669 RID: 42601
			// (set) Token: 0x0600D6FA RID: 55034 RVA: 0x00131682 File Offset: 0x0012F882
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A66A RID: 42602
			// (set) Token: 0x0600D6FB RID: 55035 RVA: 0x001316A0 File Offset: 0x0012F8A0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A66B RID: 42603
			// (set) Token: 0x0600D6FC RID: 55036 RVA: 0x001316B3 File Offset: 0x0012F8B3
			public virtual MultiValuedProperty<CallerIdItem> CallerIds
			{
				set
				{
					base.PowerSharpParameters["CallerIds"] = value;
				}
			}

			// Token: 0x1700A66C RID: 42604
			// (set) Token: 0x0600D6FD RID: 55037 RVA: 0x001316C6 File Offset: 0x0012F8C6
			public virtual bool CallersCanInterruptGreeting
			{
				set
				{
					base.PowerSharpParameters["CallersCanInterruptGreeting"] = value;
				}
			}

			// Token: 0x1700A66D RID: 42605
			// (set) Token: 0x0600D6FE RID: 55038 RVA: 0x001316DE File Offset: 0x0012F8DE
			public virtual bool CheckAutomaticReplies
			{
				set
				{
					base.PowerSharpParameters["CheckAutomaticReplies"] = value;
				}
			}

			// Token: 0x1700A66E RID: 42606
			// (set) Token: 0x0600D6FF RID: 55039 RVA: 0x001316F6 File Offset: 0x0012F8F6
			public virtual MultiValuedProperty<string> ExtensionsDialed
			{
				set
				{
					base.PowerSharpParameters["ExtensionsDialed"] = value;
				}
			}

			// Token: 0x1700A66F RID: 42607
			// (set) Token: 0x0600D700 RID: 55040 RVA: 0x00131709 File Offset: 0x0012F909
			public virtual MultiValuedProperty<KeyMapping> KeyMappings
			{
				set
				{
					base.PowerSharpParameters["KeyMappings"] = value;
				}
			}

			// Token: 0x1700A670 RID: 42608
			// (set) Token: 0x0600D701 RID: 55041 RVA: 0x0013171C File Offset: 0x0012F91C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A671 RID: 42609
			// (set) Token: 0x0600D702 RID: 55042 RVA: 0x0013172F File Offset: 0x0012F92F
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x1700A672 RID: 42610
			// (set) Token: 0x0600D703 RID: 55043 RVA: 0x00131747 File Offset: 0x0012F947
			public virtual int ScheduleStatus
			{
				set
				{
					base.PowerSharpParameters["ScheduleStatus"] = value;
				}
			}

			// Token: 0x1700A673 RID: 42611
			// (set) Token: 0x0600D704 RID: 55044 RVA: 0x0013175F File Offset: 0x0012F95F
			public virtual TimeOfDay TimeOfDay
			{
				set
				{
					base.PowerSharpParameters["TimeOfDay"] = value;
				}
			}

			// Token: 0x1700A674 RID: 42612
			// (set) Token: 0x0600D705 RID: 55045 RVA: 0x00131772 File Offset: 0x0012F972
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A675 RID: 42613
			// (set) Token: 0x0600D706 RID: 55046 RVA: 0x0013178A File Offset: 0x0012F98A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A676 RID: 42614
			// (set) Token: 0x0600D707 RID: 55047 RVA: 0x001317A2 File Offset: 0x0012F9A2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A677 RID: 42615
			// (set) Token: 0x0600D708 RID: 55048 RVA: 0x001317BA File Offset: 0x0012F9BA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A678 RID: 42616
			// (set) Token: 0x0600D709 RID: 55049 RVA: 0x001317D2 File Offset: 0x0012F9D2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E25 RID: 3621
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A679 RID: 42617
			// (set) Token: 0x0600D70B RID: 55051 RVA: 0x001317F2 File Offset: 0x0012F9F2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMCallAnsweringRuleIdParameter(value) : null);
				}
			}

			// Token: 0x1700A67A RID: 42618
			// (set) Token: 0x0600D70C RID: 55052 RVA: 0x00131810 File Offset: 0x0012FA10
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A67B RID: 42619
			// (set) Token: 0x0600D70D RID: 55053 RVA: 0x0013182E File Offset: 0x0012FA2E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A67C RID: 42620
			// (set) Token: 0x0600D70E RID: 55054 RVA: 0x00131841 File Offset: 0x0012FA41
			public virtual MultiValuedProperty<CallerIdItem> CallerIds
			{
				set
				{
					base.PowerSharpParameters["CallerIds"] = value;
				}
			}

			// Token: 0x1700A67D RID: 42621
			// (set) Token: 0x0600D70F RID: 55055 RVA: 0x00131854 File Offset: 0x0012FA54
			public virtual bool CallersCanInterruptGreeting
			{
				set
				{
					base.PowerSharpParameters["CallersCanInterruptGreeting"] = value;
				}
			}

			// Token: 0x1700A67E RID: 42622
			// (set) Token: 0x0600D710 RID: 55056 RVA: 0x0013186C File Offset: 0x0012FA6C
			public virtual bool CheckAutomaticReplies
			{
				set
				{
					base.PowerSharpParameters["CheckAutomaticReplies"] = value;
				}
			}

			// Token: 0x1700A67F RID: 42623
			// (set) Token: 0x0600D711 RID: 55057 RVA: 0x00131884 File Offset: 0x0012FA84
			public virtual MultiValuedProperty<string> ExtensionsDialed
			{
				set
				{
					base.PowerSharpParameters["ExtensionsDialed"] = value;
				}
			}

			// Token: 0x1700A680 RID: 42624
			// (set) Token: 0x0600D712 RID: 55058 RVA: 0x00131897 File Offset: 0x0012FA97
			public virtual MultiValuedProperty<KeyMapping> KeyMappings
			{
				set
				{
					base.PowerSharpParameters["KeyMappings"] = value;
				}
			}

			// Token: 0x1700A681 RID: 42625
			// (set) Token: 0x0600D713 RID: 55059 RVA: 0x001318AA File Offset: 0x0012FAAA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A682 RID: 42626
			// (set) Token: 0x0600D714 RID: 55060 RVA: 0x001318BD File Offset: 0x0012FABD
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x1700A683 RID: 42627
			// (set) Token: 0x0600D715 RID: 55061 RVA: 0x001318D5 File Offset: 0x0012FAD5
			public virtual int ScheduleStatus
			{
				set
				{
					base.PowerSharpParameters["ScheduleStatus"] = value;
				}
			}

			// Token: 0x1700A684 RID: 42628
			// (set) Token: 0x0600D716 RID: 55062 RVA: 0x001318ED File Offset: 0x0012FAED
			public virtual TimeOfDay TimeOfDay
			{
				set
				{
					base.PowerSharpParameters["TimeOfDay"] = value;
				}
			}

			// Token: 0x1700A685 RID: 42629
			// (set) Token: 0x0600D717 RID: 55063 RVA: 0x00131900 File Offset: 0x0012FB00
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A686 RID: 42630
			// (set) Token: 0x0600D718 RID: 55064 RVA: 0x00131918 File Offset: 0x0012FB18
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A687 RID: 42631
			// (set) Token: 0x0600D719 RID: 55065 RVA: 0x00131930 File Offset: 0x0012FB30
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A688 RID: 42632
			// (set) Token: 0x0600D71A RID: 55066 RVA: 0x00131948 File Offset: 0x0012FB48
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A689 RID: 42633
			// (set) Token: 0x0600D71B RID: 55067 RVA: 0x00131960 File Offset: 0x0012FB60
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
