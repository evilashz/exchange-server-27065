using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E72 RID: 3698
	public class NewPopSubscriptionCommand : SyntheticCommandWithPipelineInput<PopSubscriptionProxy, PopSubscriptionProxy>
	{
		// Token: 0x0600DA5B RID: 55899 RVA: 0x00135D23 File Offset: 0x00133F23
		private NewPopSubscriptionCommand() : base("New-PopSubscription")
		{
		}

		// Token: 0x0600DA5C RID: 55900 RVA: 0x00135D30 File Offset: 0x00133F30
		public NewPopSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DA5D RID: 55901 RVA: 0x00135D3F File Offset: 0x00133F3F
		public virtual NewPopSubscriptionCommand SetParameters(NewPopSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E73 RID: 3699
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A930 RID: 43312
			// (set) Token: 0x0600DA5E RID: 55902 RVA: 0x00135D49 File Offset: 0x00133F49
			public virtual Fqdn IncomingServer
			{
				set
				{
					base.PowerSharpParameters["IncomingServer"] = value;
				}
			}

			// Token: 0x1700A931 RID: 43313
			// (set) Token: 0x0600DA5F RID: 55903 RVA: 0x00135D5C File Offset: 0x00133F5C
			public virtual int IncomingPort
			{
				set
				{
					base.PowerSharpParameters["IncomingPort"] = value;
				}
			}

			// Token: 0x1700A932 RID: 43314
			// (set) Token: 0x0600DA60 RID: 55904 RVA: 0x00135D74 File Offset: 0x00133F74
			public virtual string IncomingUserName
			{
				set
				{
					base.PowerSharpParameters["IncomingUserName"] = value;
				}
			}

			// Token: 0x1700A933 RID: 43315
			// (set) Token: 0x0600DA61 RID: 55905 RVA: 0x00135D87 File Offset: 0x00133F87
			public virtual SecureString IncomingPassword
			{
				set
				{
					base.PowerSharpParameters["IncomingPassword"] = value;
				}
			}

			// Token: 0x1700A934 RID: 43316
			// (set) Token: 0x0600DA62 RID: 55906 RVA: 0x00135D9A File Offset: 0x00133F9A
			public virtual AuthenticationMechanism IncomingAuth
			{
				set
				{
					base.PowerSharpParameters["IncomingAuth"] = value;
				}
			}

			// Token: 0x1700A935 RID: 43317
			// (set) Token: 0x0600DA63 RID: 55907 RVA: 0x00135DB2 File Offset: 0x00133FB2
			public virtual SecurityMechanism IncomingSecurity
			{
				set
				{
					base.PowerSharpParameters["IncomingSecurity"] = value;
				}
			}

			// Token: 0x1700A936 RID: 43318
			// (set) Token: 0x0600DA64 RID: 55908 RVA: 0x00135DCA File Offset: 0x00133FCA
			public virtual bool LeaveOnServer
			{
				set
				{
					base.PowerSharpParameters["LeaveOnServer"] = value;
				}
			}

			// Token: 0x1700A937 RID: 43319
			// (set) Token: 0x0600DA65 RID: 55909 RVA: 0x00135DE2 File Offset: 0x00133FE2
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700A938 RID: 43320
			// (set) Token: 0x0600DA66 RID: 55910 RVA: 0x00135DFA File Offset: 0x00133FFA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A939 RID: 43321
			// (set) Token: 0x0600DA67 RID: 55911 RVA: 0x00135E0D File Offset: 0x0013400D
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A93A RID: 43322
			// (set) Token: 0x0600DA68 RID: 55912 RVA: 0x00135E2B File Offset: 0x0013402B
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A93B RID: 43323
			// (set) Token: 0x0600DA69 RID: 55913 RVA: 0x00135E3E File Offset: 0x0013403E
			public virtual SmtpAddress EmailAddress
			{
				set
				{
					base.PowerSharpParameters["EmailAddress"] = value;
				}
			}

			// Token: 0x1700A93C RID: 43324
			// (set) Token: 0x0600DA6A RID: 55914 RVA: 0x00135E56 File Offset: 0x00134056
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A93D RID: 43325
			// (set) Token: 0x0600DA6B RID: 55915 RVA: 0x00135E69 File Offset: 0x00134069
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A93E RID: 43326
			// (set) Token: 0x0600DA6C RID: 55916 RVA: 0x00135E81 File Offset: 0x00134081
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A93F RID: 43327
			// (set) Token: 0x0600DA6D RID: 55917 RVA: 0x00135E99 File Offset: 0x00134099
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A940 RID: 43328
			// (set) Token: 0x0600DA6E RID: 55918 RVA: 0x00135EB1 File Offset: 0x001340B1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A941 RID: 43329
			// (set) Token: 0x0600DA6F RID: 55919 RVA: 0x00135EC9 File Offset: 0x001340C9
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
