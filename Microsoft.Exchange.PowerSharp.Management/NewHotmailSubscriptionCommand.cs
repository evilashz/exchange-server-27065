using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E6D RID: 3693
	public class NewHotmailSubscriptionCommand : SyntheticCommandWithPipelineInput<HotmailSubscriptionProxy, HotmailSubscriptionProxy>
	{
		// Token: 0x0600DA2D RID: 55853 RVA: 0x0013597F File Offset: 0x00133B7F
		private NewHotmailSubscriptionCommand() : base("New-HotmailSubscription")
		{
		}

		// Token: 0x0600DA2E RID: 55854 RVA: 0x0013598C File Offset: 0x00133B8C
		public NewHotmailSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DA2F RID: 55855 RVA: 0x0013599B File Offset: 0x00133B9B
		public virtual NewHotmailSubscriptionCommand SetParameters(NewHotmailSubscriptionCommand.AggregationParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DA30 RID: 55856 RVA: 0x001359A5 File Offset: 0x00133BA5
		public virtual NewHotmailSubscriptionCommand SetParameters(NewHotmailSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E6E RID: 3694
		public class AggregationParameterSetParameters : ParametersBase
		{
			// Token: 0x1700A90C RID: 43276
			// (set) Token: 0x0600DA31 RID: 55857 RVA: 0x001359AF File Offset: 0x00133BAF
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700A90D RID: 43277
			// (set) Token: 0x0600DA32 RID: 55858 RVA: 0x001359C2 File Offset: 0x00133BC2
			public virtual SmtpAddress EmailAddress
			{
				set
				{
					base.PowerSharpParameters["EmailAddress"] = value;
				}
			}

			// Token: 0x1700A90E RID: 43278
			// (set) Token: 0x0600DA33 RID: 55859 RVA: 0x001359DA File Offset: 0x00133BDA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A90F RID: 43279
			// (set) Token: 0x0600DA34 RID: 55860 RVA: 0x001359ED File Offset: 0x00133BED
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A910 RID: 43280
			// (set) Token: 0x0600DA35 RID: 55861 RVA: 0x00135A0B File Offset: 0x00133C0B
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A911 RID: 43281
			// (set) Token: 0x0600DA36 RID: 55862 RVA: 0x00135A1E File Offset: 0x00133C1E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A912 RID: 43282
			// (set) Token: 0x0600DA37 RID: 55863 RVA: 0x00135A31 File Offset: 0x00133C31
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A913 RID: 43283
			// (set) Token: 0x0600DA38 RID: 55864 RVA: 0x00135A49 File Offset: 0x00133C49
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A914 RID: 43284
			// (set) Token: 0x0600DA39 RID: 55865 RVA: 0x00135A61 File Offset: 0x00133C61
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A915 RID: 43285
			// (set) Token: 0x0600DA3A RID: 55866 RVA: 0x00135A79 File Offset: 0x00133C79
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A916 RID: 43286
			// (set) Token: 0x0600DA3B RID: 55867 RVA: 0x00135A91 File Offset: 0x00133C91
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E6F RID: 3695
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A917 RID: 43287
			// (set) Token: 0x0600DA3D RID: 55869 RVA: 0x00135AB1 File Offset: 0x00133CB1
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A918 RID: 43288
			// (set) Token: 0x0600DA3E RID: 55870 RVA: 0x00135ACF File Offset: 0x00133CCF
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A919 RID: 43289
			// (set) Token: 0x0600DA3F RID: 55871 RVA: 0x00135AE2 File Offset: 0x00133CE2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A91A RID: 43290
			// (set) Token: 0x0600DA40 RID: 55872 RVA: 0x00135AF5 File Offset: 0x00133CF5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A91B RID: 43291
			// (set) Token: 0x0600DA41 RID: 55873 RVA: 0x00135B0D File Offset: 0x00133D0D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A91C RID: 43292
			// (set) Token: 0x0600DA42 RID: 55874 RVA: 0x00135B25 File Offset: 0x00133D25
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A91D RID: 43293
			// (set) Token: 0x0600DA43 RID: 55875 RVA: 0x00135B3D File Offset: 0x00133D3D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A91E RID: 43294
			// (set) Token: 0x0600DA44 RID: 55876 RVA: 0x00135B55 File Offset: 0x00133D55
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
