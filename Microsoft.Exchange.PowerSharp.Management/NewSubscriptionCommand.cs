using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E74 RID: 3700
	public class NewSubscriptionCommand : SyntheticCommandWithPipelineInput<PimSubscriptionProxy, PimSubscriptionProxy>
	{
		// Token: 0x0600DA71 RID: 55921 RVA: 0x00135EE9 File Offset: 0x001340E9
		private NewSubscriptionCommand() : base("New-Subscription")
		{
		}

		// Token: 0x0600DA72 RID: 55922 RVA: 0x00135EF6 File Offset: 0x001340F6
		public NewSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DA73 RID: 55923 RVA: 0x00135F05 File Offset: 0x00134105
		public virtual NewSubscriptionCommand SetParameters(NewSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E75 RID: 3701
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A942 RID: 43330
			// (set) Token: 0x0600DA74 RID: 55924 RVA: 0x00135F0F File Offset: 0x0013410F
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700A943 RID: 43331
			// (set) Token: 0x0600DA75 RID: 55925 RVA: 0x00135F22 File Offset: 0x00134122
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700A944 RID: 43332
			// (set) Token: 0x0600DA76 RID: 55926 RVA: 0x00135F3A File Offset: 0x0013413A
			public virtual SwitchParameter Hotmail
			{
				set
				{
					base.PowerSharpParameters["Hotmail"] = value;
				}
			}

			// Token: 0x1700A945 RID: 43333
			// (set) Token: 0x0600DA77 RID: 55927 RVA: 0x00135F52 File Offset: 0x00134152
			public virtual SwitchParameter Imap
			{
				set
				{
					base.PowerSharpParameters["Imap"] = value;
				}
			}

			// Token: 0x1700A946 RID: 43334
			// (set) Token: 0x0600DA78 RID: 55928 RVA: 0x00135F6A File Offset: 0x0013416A
			public virtual SwitchParameter Pop
			{
				set
				{
					base.PowerSharpParameters["Pop"] = value;
				}
			}

			// Token: 0x1700A947 RID: 43335
			// (set) Token: 0x0600DA79 RID: 55929 RVA: 0x00135F82 File Offset: 0x00134182
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A948 RID: 43336
			// (set) Token: 0x0600DA7A RID: 55930 RVA: 0x00135F95 File Offset: 0x00134195
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A949 RID: 43337
			// (set) Token: 0x0600DA7B RID: 55931 RVA: 0x00135FB3 File Offset: 0x001341B3
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A94A RID: 43338
			// (set) Token: 0x0600DA7C RID: 55932 RVA: 0x00135FC6 File Offset: 0x001341C6
			public virtual SmtpAddress EmailAddress
			{
				set
				{
					base.PowerSharpParameters["EmailAddress"] = value;
				}
			}

			// Token: 0x1700A94B RID: 43339
			// (set) Token: 0x0600DA7D RID: 55933 RVA: 0x00135FDE File Offset: 0x001341DE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A94C RID: 43340
			// (set) Token: 0x0600DA7E RID: 55934 RVA: 0x00135FF1 File Offset: 0x001341F1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A94D RID: 43341
			// (set) Token: 0x0600DA7F RID: 55935 RVA: 0x00136009 File Offset: 0x00134209
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A94E RID: 43342
			// (set) Token: 0x0600DA80 RID: 55936 RVA: 0x00136021 File Offset: 0x00134221
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A94F RID: 43343
			// (set) Token: 0x0600DA81 RID: 55937 RVA: 0x00136039 File Offset: 0x00134239
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A950 RID: 43344
			// (set) Token: 0x0600DA82 RID: 55938 RVA: 0x00136051 File Offset: 0x00134251
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
