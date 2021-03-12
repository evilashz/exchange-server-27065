using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B21 RID: 2849
	public class EnableUMMailboxCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x06008B64 RID: 35684 RVA: 0x000CCB1D File Offset: 0x000CAD1D
		private EnableUMMailboxCommand() : base("Enable-UMMailbox")
		{
		}

		// Token: 0x06008B65 RID: 35685 RVA: 0x000CCB2A File Offset: 0x000CAD2A
		public EnableUMMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008B66 RID: 35686 RVA: 0x000CCB39 File Offset: 0x000CAD39
		public virtual EnableUMMailboxCommand SetParameters(EnableUMMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008B67 RID: 35687 RVA: 0x000CCB43 File Offset: 0x000CAD43
		public virtual EnableUMMailboxCommand SetParameters(EnableUMMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B22 RID: 2850
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170060DB RID: 24795
			// (set) Token: 0x06008B68 RID: 35688 RVA: 0x000CCB4D File Offset: 0x000CAD4D
			public virtual MultiValuedProperty<string> Extensions
			{
				set
				{
					base.PowerSharpParameters["Extensions"] = value;
				}
			}

			// Token: 0x170060DC RID: 24796
			// (set) Token: 0x06008B69 RID: 35689 RVA: 0x000CCB60 File Offset: 0x000CAD60
			public virtual string UMMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["UMMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170060DD RID: 24797
			// (set) Token: 0x06008B6A RID: 35690 RVA: 0x000CCB7E File Offset: 0x000CAD7E
			public virtual string SIPResourceIdentifier
			{
				set
				{
					base.PowerSharpParameters["SIPResourceIdentifier"] = value;
				}
			}

			// Token: 0x170060DE RID: 24798
			// (set) Token: 0x06008B6B RID: 35691 RVA: 0x000CCB91 File Offset: 0x000CAD91
			public virtual string Pin
			{
				set
				{
					base.PowerSharpParameters["Pin"] = value;
				}
			}

			// Token: 0x170060DF RID: 24799
			// (set) Token: 0x06008B6C RID: 35692 RVA: 0x000CCBA4 File Offset: 0x000CADA4
			public virtual bool PinExpired
			{
				set
				{
					base.PowerSharpParameters["PinExpired"] = value;
				}
			}

			// Token: 0x170060E0 RID: 24800
			// (set) Token: 0x06008B6D RID: 35693 RVA: 0x000CCBBC File Offset: 0x000CADBC
			public virtual string NotifyEmail
			{
				set
				{
					base.PowerSharpParameters["NotifyEmail"] = value;
				}
			}

			// Token: 0x170060E1 RID: 24801
			// (set) Token: 0x06008B6E RID: 35694 RVA: 0x000CCBCF File Offset: 0x000CADCF
			public virtual string PilotNumber
			{
				set
				{
					base.PowerSharpParameters["PilotNumber"] = value;
				}
			}

			// Token: 0x170060E2 RID: 24802
			// (set) Token: 0x06008B6F RID: 35695 RVA: 0x000CCBE2 File Offset: 0x000CADE2
			public virtual bool AutomaticSpeechRecognitionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutomaticSpeechRecognitionEnabled"] = value;
				}
			}

			// Token: 0x170060E3 RID: 24803
			// (set) Token: 0x06008B70 RID: 35696 RVA: 0x000CCBFA File Offset: 0x000CADFA
			public virtual SwitchParameter ValidateOnly
			{
				set
				{
					base.PowerSharpParameters["ValidateOnly"] = value;
				}
			}

			// Token: 0x170060E4 RID: 24804
			// (set) Token: 0x06008B71 RID: 35697 RVA: 0x000CCC12 File Offset: 0x000CAE12
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170060E5 RID: 24805
			// (set) Token: 0x06008B72 RID: 35698 RVA: 0x000CCC2A File Offset: 0x000CAE2A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170060E6 RID: 24806
			// (set) Token: 0x06008B73 RID: 35699 RVA: 0x000CCC3D File Offset: 0x000CAE3D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170060E7 RID: 24807
			// (set) Token: 0x06008B74 RID: 35700 RVA: 0x000CCC55 File Offset: 0x000CAE55
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170060E8 RID: 24808
			// (set) Token: 0x06008B75 RID: 35701 RVA: 0x000CCC6D File Offset: 0x000CAE6D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170060E9 RID: 24809
			// (set) Token: 0x06008B76 RID: 35702 RVA: 0x000CCC85 File Offset: 0x000CAE85
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170060EA RID: 24810
			// (set) Token: 0x06008B77 RID: 35703 RVA: 0x000CCC9D File Offset: 0x000CAE9D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B23 RID: 2851
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170060EB RID: 24811
			// (set) Token: 0x06008B79 RID: 35705 RVA: 0x000CCCBD File Offset: 0x000CAEBD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170060EC RID: 24812
			// (set) Token: 0x06008B7A RID: 35706 RVA: 0x000CCCDB File Offset: 0x000CAEDB
			public virtual MultiValuedProperty<string> Extensions
			{
				set
				{
					base.PowerSharpParameters["Extensions"] = value;
				}
			}

			// Token: 0x170060ED RID: 24813
			// (set) Token: 0x06008B7B RID: 35707 RVA: 0x000CCCEE File Offset: 0x000CAEEE
			public virtual string UMMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["UMMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170060EE RID: 24814
			// (set) Token: 0x06008B7C RID: 35708 RVA: 0x000CCD0C File Offset: 0x000CAF0C
			public virtual string SIPResourceIdentifier
			{
				set
				{
					base.PowerSharpParameters["SIPResourceIdentifier"] = value;
				}
			}

			// Token: 0x170060EF RID: 24815
			// (set) Token: 0x06008B7D RID: 35709 RVA: 0x000CCD1F File Offset: 0x000CAF1F
			public virtual string Pin
			{
				set
				{
					base.PowerSharpParameters["Pin"] = value;
				}
			}

			// Token: 0x170060F0 RID: 24816
			// (set) Token: 0x06008B7E RID: 35710 RVA: 0x000CCD32 File Offset: 0x000CAF32
			public virtual bool PinExpired
			{
				set
				{
					base.PowerSharpParameters["PinExpired"] = value;
				}
			}

			// Token: 0x170060F1 RID: 24817
			// (set) Token: 0x06008B7F RID: 35711 RVA: 0x000CCD4A File Offset: 0x000CAF4A
			public virtual string NotifyEmail
			{
				set
				{
					base.PowerSharpParameters["NotifyEmail"] = value;
				}
			}

			// Token: 0x170060F2 RID: 24818
			// (set) Token: 0x06008B80 RID: 35712 RVA: 0x000CCD5D File Offset: 0x000CAF5D
			public virtual string PilotNumber
			{
				set
				{
					base.PowerSharpParameters["PilotNumber"] = value;
				}
			}

			// Token: 0x170060F3 RID: 24819
			// (set) Token: 0x06008B81 RID: 35713 RVA: 0x000CCD70 File Offset: 0x000CAF70
			public virtual bool AutomaticSpeechRecognitionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutomaticSpeechRecognitionEnabled"] = value;
				}
			}

			// Token: 0x170060F4 RID: 24820
			// (set) Token: 0x06008B82 RID: 35714 RVA: 0x000CCD88 File Offset: 0x000CAF88
			public virtual SwitchParameter ValidateOnly
			{
				set
				{
					base.PowerSharpParameters["ValidateOnly"] = value;
				}
			}

			// Token: 0x170060F5 RID: 24821
			// (set) Token: 0x06008B83 RID: 35715 RVA: 0x000CCDA0 File Offset: 0x000CAFA0
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170060F6 RID: 24822
			// (set) Token: 0x06008B84 RID: 35716 RVA: 0x000CCDB8 File Offset: 0x000CAFB8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170060F7 RID: 24823
			// (set) Token: 0x06008B85 RID: 35717 RVA: 0x000CCDCB File Offset: 0x000CAFCB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170060F8 RID: 24824
			// (set) Token: 0x06008B86 RID: 35718 RVA: 0x000CCDE3 File Offset: 0x000CAFE3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170060F9 RID: 24825
			// (set) Token: 0x06008B87 RID: 35719 RVA: 0x000CCDFB File Offset: 0x000CAFFB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170060FA RID: 24826
			// (set) Token: 0x06008B88 RID: 35720 RVA: 0x000CCE13 File Offset: 0x000CB013
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170060FB RID: 24827
			// (set) Token: 0x06008B89 RID: 35721 RVA: 0x000CCE2B File Offset: 0x000CB02B
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
