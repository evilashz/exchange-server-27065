using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B24 RID: 2852
	public class EnableUMMailboxPlanCommand : SyntheticCommandWithPipelineInput<MailboxPlanIdParameter, MailboxPlanIdParameter>
	{
		// Token: 0x06008B8B RID: 35723 RVA: 0x000CCE4B File Offset: 0x000CB04B
		private EnableUMMailboxPlanCommand() : base("Enable-UMMailboxPlan")
		{
		}

		// Token: 0x06008B8C RID: 35724 RVA: 0x000CCE58 File Offset: 0x000CB058
		public EnableUMMailboxPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008B8D RID: 35725 RVA: 0x000CCE67 File Offset: 0x000CB067
		public virtual EnableUMMailboxPlanCommand SetParameters(EnableUMMailboxPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008B8E RID: 35726 RVA: 0x000CCE71 File Offset: 0x000CB071
		public virtual EnableUMMailboxPlanCommand SetParameters(EnableUMMailboxPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B25 RID: 2853
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170060FC RID: 24828
			// (set) Token: 0x06008B8F RID: 35727 RVA: 0x000CCE7B File Offset: 0x000CB07B
			public virtual MultiValuedProperty<string> Extensions
			{
				set
				{
					base.PowerSharpParameters["Extensions"] = value;
				}
			}

			// Token: 0x170060FD RID: 24829
			// (set) Token: 0x06008B90 RID: 35728 RVA: 0x000CCE8E File Offset: 0x000CB08E
			public virtual string UMMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["UMMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170060FE RID: 24830
			// (set) Token: 0x06008B91 RID: 35729 RVA: 0x000CCEAC File Offset: 0x000CB0AC
			public virtual string SIPResourceIdentifier
			{
				set
				{
					base.PowerSharpParameters["SIPResourceIdentifier"] = value;
				}
			}

			// Token: 0x170060FF RID: 24831
			// (set) Token: 0x06008B92 RID: 35730 RVA: 0x000CCEBF File Offset: 0x000CB0BF
			public virtual string Pin
			{
				set
				{
					base.PowerSharpParameters["Pin"] = value;
				}
			}

			// Token: 0x17006100 RID: 24832
			// (set) Token: 0x06008B93 RID: 35731 RVA: 0x000CCED2 File Offset: 0x000CB0D2
			public virtual bool PinExpired
			{
				set
				{
					base.PowerSharpParameters["PinExpired"] = value;
				}
			}

			// Token: 0x17006101 RID: 24833
			// (set) Token: 0x06008B94 RID: 35732 RVA: 0x000CCEEA File Offset: 0x000CB0EA
			public virtual string NotifyEmail
			{
				set
				{
					base.PowerSharpParameters["NotifyEmail"] = value;
				}
			}

			// Token: 0x17006102 RID: 24834
			// (set) Token: 0x06008B95 RID: 35733 RVA: 0x000CCEFD File Offset: 0x000CB0FD
			public virtual string PilotNumber
			{
				set
				{
					base.PowerSharpParameters["PilotNumber"] = value;
				}
			}

			// Token: 0x17006103 RID: 24835
			// (set) Token: 0x06008B96 RID: 35734 RVA: 0x000CCF10 File Offset: 0x000CB110
			public virtual bool AutomaticSpeechRecognitionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutomaticSpeechRecognitionEnabled"] = value;
				}
			}

			// Token: 0x17006104 RID: 24836
			// (set) Token: 0x06008B97 RID: 35735 RVA: 0x000CCF28 File Offset: 0x000CB128
			public virtual SwitchParameter ValidateOnly
			{
				set
				{
					base.PowerSharpParameters["ValidateOnly"] = value;
				}
			}

			// Token: 0x17006105 RID: 24837
			// (set) Token: 0x06008B98 RID: 35736 RVA: 0x000CCF40 File Offset: 0x000CB140
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006106 RID: 24838
			// (set) Token: 0x06008B99 RID: 35737 RVA: 0x000CCF58 File Offset: 0x000CB158
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006107 RID: 24839
			// (set) Token: 0x06008B9A RID: 35738 RVA: 0x000CCF6B File Offset: 0x000CB16B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006108 RID: 24840
			// (set) Token: 0x06008B9B RID: 35739 RVA: 0x000CCF83 File Offset: 0x000CB183
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006109 RID: 24841
			// (set) Token: 0x06008B9C RID: 35740 RVA: 0x000CCF9B File Offset: 0x000CB19B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700610A RID: 24842
			// (set) Token: 0x06008B9D RID: 35741 RVA: 0x000CCFB3 File Offset: 0x000CB1B3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700610B RID: 24843
			// (set) Token: 0x06008B9E RID: 35742 RVA: 0x000CCFCB File Offset: 0x000CB1CB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B26 RID: 2854
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700610C RID: 24844
			// (set) Token: 0x06008BA0 RID: 35744 RVA: 0x000CCFEB File Offset: 0x000CB1EB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700610D RID: 24845
			// (set) Token: 0x06008BA1 RID: 35745 RVA: 0x000CD009 File Offset: 0x000CB209
			public virtual MultiValuedProperty<string> Extensions
			{
				set
				{
					base.PowerSharpParameters["Extensions"] = value;
				}
			}

			// Token: 0x1700610E RID: 24846
			// (set) Token: 0x06008BA2 RID: 35746 RVA: 0x000CD01C File Offset: 0x000CB21C
			public virtual string UMMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["UMMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700610F RID: 24847
			// (set) Token: 0x06008BA3 RID: 35747 RVA: 0x000CD03A File Offset: 0x000CB23A
			public virtual string SIPResourceIdentifier
			{
				set
				{
					base.PowerSharpParameters["SIPResourceIdentifier"] = value;
				}
			}

			// Token: 0x17006110 RID: 24848
			// (set) Token: 0x06008BA4 RID: 35748 RVA: 0x000CD04D File Offset: 0x000CB24D
			public virtual string Pin
			{
				set
				{
					base.PowerSharpParameters["Pin"] = value;
				}
			}

			// Token: 0x17006111 RID: 24849
			// (set) Token: 0x06008BA5 RID: 35749 RVA: 0x000CD060 File Offset: 0x000CB260
			public virtual bool PinExpired
			{
				set
				{
					base.PowerSharpParameters["PinExpired"] = value;
				}
			}

			// Token: 0x17006112 RID: 24850
			// (set) Token: 0x06008BA6 RID: 35750 RVA: 0x000CD078 File Offset: 0x000CB278
			public virtual string NotifyEmail
			{
				set
				{
					base.PowerSharpParameters["NotifyEmail"] = value;
				}
			}

			// Token: 0x17006113 RID: 24851
			// (set) Token: 0x06008BA7 RID: 35751 RVA: 0x000CD08B File Offset: 0x000CB28B
			public virtual string PilotNumber
			{
				set
				{
					base.PowerSharpParameters["PilotNumber"] = value;
				}
			}

			// Token: 0x17006114 RID: 24852
			// (set) Token: 0x06008BA8 RID: 35752 RVA: 0x000CD09E File Offset: 0x000CB29E
			public virtual bool AutomaticSpeechRecognitionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutomaticSpeechRecognitionEnabled"] = value;
				}
			}

			// Token: 0x17006115 RID: 24853
			// (set) Token: 0x06008BA9 RID: 35753 RVA: 0x000CD0B6 File Offset: 0x000CB2B6
			public virtual SwitchParameter ValidateOnly
			{
				set
				{
					base.PowerSharpParameters["ValidateOnly"] = value;
				}
			}

			// Token: 0x17006116 RID: 24854
			// (set) Token: 0x06008BAA RID: 35754 RVA: 0x000CD0CE File Offset: 0x000CB2CE
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006117 RID: 24855
			// (set) Token: 0x06008BAB RID: 35755 RVA: 0x000CD0E6 File Offset: 0x000CB2E6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006118 RID: 24856
			// (set) Token: 0x06008BAC RID: 35756 RVA: 0x000CD0F9 File Offset: 0x000CB2F9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006119 RID: 24857
			// (set) Token: 0x06008BAD RID: 35757 RVA: 0x000CD111 File Offset: 0x000CB311
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700611A RID: 24858
			// (set) Token: 0x06008BAE RID: 35758 RVA: 0x000CD129 File Offset: 0x000CB329
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700611B RID: 24859
			// (set) Token: 0x06008BAF RID: 35759 RVA: 0x000CD141 File Offset: 0x000CB341
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700611C RID: 24860
			// (set) Token: 0x06008BB0 RID: 35760 RVA: 0x000CD159 File Offset: 0x000CB359
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
