using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E70 RID: 3696
	public class NewImapSubscriptionCommand : SyntheticCommandWithPipelineInput<IMAPSubscriptionProxy, IMAPSubscriptionProxy>
	{
		// Token: 0x0600DA46 RID: 55878 RVA: 0x00135B75 File Offset: 0x00133D75
		private NewImapSubscriptionCommand() : base("New-ImapSubscription")
		{
		}

		// Token: 0x0600DA47 RID: 55879 RVA: 0x00135B82 File Offset: 0x00133D82
		public NewImapSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DA48 RID: 55880 RVA: 0x00135B91 File Offset: 0x00133D91
		public virtual NewImapSubscriptionCommand SetParameters(NewImapSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E71 RID: 3697
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A91F RID: 43295
			// (set) Token: 0x0600DA49 RID: 55881 RVA: 0x00135B9B File Offset: 0x00133D9B
			public virtual Fqdn IncomingServer
			{
				set
				{
					base.PowerSharpParameters["IncomingServer"] = value;
				}
			}

			// Token: 0x1700A920 RID: 43296
			// (set) Token: 0x0600DA4A RID: 55882 RVA: 0x00135BAE File Offset: 0x00133DAE
			public virtual int IncomingPort
			{
				set
				{
					base.PowerSharpParameters["IncomingPort"] = value;
				}
			}

			// Token: 0x1700A921 RID: 43297
			// (set) Token: 0x0600DA4B RID: 55883 RVA: 0x00135BC6 File Offset: 0x00133DC6
			public virtual string IncomingUserName
			{
				set
				{
					base.PowerSharpParameters["IncomingUserName"] = value;
				}
			}

			// Token: 0x1700A922 RID: 43298
			// (set) Token: 0x0600DA4C RID: 55884 RVA: 0x00135BD9 File Offset: 0x00133DD9
			public virtual SecureString IncomingPassword
			{
				set
				{
					base.PowerSharpParameters["IncomingPassword"] = value;
				}
			}

			// Token: 0x1700A923 RID: 43299
			// (set) Token: 0x0600DA4D RID: 55885 RVA: 0x00135BEC File Offset: 0x00133DEC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700A924 RID: 43300
			// (set) Token: 0x0600DA4E RID: 55886 RVA: 0x00135C04 File Offset: 0x00133E04
			public virtual IMAPAuthenticationMechanism IncomingAuth
			{
				set
				{
					base.PowerSharpParameters["IncomingAuth"] = value;
				}
			}

			// Token: 0x1700A925 RID: 43301
			// (set) Token: 0x0600DA4F RID: 55887 RVA: 0x00135C1C File Offset: 0x00133E1C
			public virtual IMAPSecurityMechanism IncomingSecurity
			{
				set
				{
					base.PowerSharpParameters["IncomingSecurity"] = value;
				}
			}

			// Token: 0x1700A926 RID: 43302
			// (set) Token: 0x0600DA50 RID: 55888 RVA: 0x00135C34 File Offset: 0x00133E34
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A927 RID: 43303
			// (set) Token: 0x0600DA51 RID: 55889 RVA: 0x00135C47 File Offset: 0x00133E47
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A928 RID: 43304
			// (set) Token: 0x0600DA52 RID: 55890 RVA: 0x00135C65 File Offset: 0x00133E65
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A929 RID: 43305
			// (set) Token: 0x0600DA53 RID: 55891 RVA: 0x00135C78 File Offset: 0x00133E78
			public virtual SmtpAddress EmailAddress
			{
				set
				{
					base.PowerSharpParameters["EmailAddress"] = value;
				}
			}

			// Token: 0x1700A92A RID: 43306
			// (set) Token: 0x0600DA54 RID: 55892 RVA: 0x00135C90 File Offset: 0x00133E90
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A92B RID: 43307
			// (set) Token: 0x0600DA55 RID: 55893 RVA: 0x00135CA3 File Offset: 0x00133EA3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A92C RID: 43308
			// (set) Token: 0x0600DA56 RID: 55894 RVA: 0x00135CBB File Offset: 0x00133EBB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A92D RID: 43309
			// (set) Token: 0x0600DA57 RID: 55895 RVA: 0x00135CD3 File Offset: 0x00133ED3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A92E RID: 43310
			// (set) Token: 0x0600DA58 RID: 55896 RVA: 0x00135CEB File Offset: 0x00133EEB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A92F RID: 43311
			// (set) Token: 0x0600DA59 RID: 55897 RVA: 0x00135D03 File Offset: 0x00133F03
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
