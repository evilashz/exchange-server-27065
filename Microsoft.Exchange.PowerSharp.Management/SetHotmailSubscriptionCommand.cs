using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E86 RID: 3718
	public class SetHotmailSubscriptionCommand : SyntheticCommandWithPipelineInputNoOutput<HotmailSubscriptionProxy>
	{
		// Token: 0x0600DB09 RID: 56073 RVA: 0x00136B2A File Offset: 0x00134D2A
		private SetHotmailSubscriptionCommand() : base("Set-HotmailSubscription")
		{
		}

		// Token: 0x0600DB0A RID: 56074 RVA: 0x00136B37 File Offset: 0x00134D37
		public SetHotmailSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DB0B RID: 56075 RVA: 0x00136B46 File Offset: 0x00134D46
		public virtual SetHotmailSubscriptionCommand SetParameters(SetHotmailSubscriptionCommand.SubscriptionModificationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB0C RID: 56076 RVA: 0x00136B50 File Offset: 0x00134D50
		public virtual SetHotmailSubscriptionCommand SetParameters(SetHotmailSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB0D RID: 56077 RVA: 0x00136B5A File Offset: 0x00134D5A
		public virtual SetHotmailSubscriptionCommand SetParameters(SetHotmailSubscriptionCommand.DisableSubscriptionAsPoisonParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB0E RID: 56078 RVA: 0x00136B64 File Offset: 0x00134D64
		public virtual SetHotmailSubscriptionCommand SetParameters(SetHotmailSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E87 RID: 3719
		public class SubscriptionModificationParameters : ParametersBase
		{
			// Token: 0x1700A9B6 RID: 43446
			// (set) Token: 0x0600DB0F RID: 56079 RVA: 0x00136B6E File Offset: 0x00134D6E
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700A9B7 RID: 43447
			// (set) Token: 0x0600DB10 RID: 56080 RVA: 0x00136B81 File Offset: 0x00134D81
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9B8 RID: 43448
			// (set) Token: 0x0600DB11 RID: 56081 RVA: 0x00136B9F File Offset: 0x00134D9F
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9B9 RID: 43449
			// (set) Token: 0x0600DB12 RID: 56082 RVA: 0x00136BBD File Offset: 0x00134DBD
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A9BA RID: 43450
			// (set) Token: 0x0600DB13 RID: 56083 RVA: 0x00136BD0 File Offset: 0x00134DD0
			public virtual SwitchParameter EnablePoisonSubscription
			{
				set
				{
					base.PowerSharpParameters["EnablePoisonSubscription"] = value;
				}
			}

			// Token: 0x1700A9BB RID: 43451
			// (set) Token: 0x0600DB14 RID: 56084 RVA: 0x00136BE8 File Offset: 0x00134DE8
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700A9BC RID: 43452
			// (set) Token: 0x0600DB15 RID: 56085 RVA: 0x00136C00 File Offset: 0x00134E00
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A9BD RID: 43453
			// (set) Token: 0x0600DB16 RID: 56086 RVA: 0x00136C13 File Offset: 0x00134E13
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A9BE RID: 43454
			// (set) Token: 0x0600DB17 RID: 56087 RVA: 0x00136C2B File Offset: 0x00134E2B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A9BF RID: 43455
			// (set) Token: 0x0600DB18 RID: 56088 RVA: 0x00136C43 File Offset: 0x00134E43
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A9C0 RID: 43456
			// (set) Token: 0x0600DB19 RID: 56089 RVA: 0x00136C5B File Offset: 0x00134E5B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A9C1 RID: 43457
			// (set) Token: 0x0600DB1A RID: 56090 RVA: 0x00136C73 File Offset: 0x00134E73
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E88 RID: 3720
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A9C2 RID: 43458
			// (set) Token: 0x0600DB1C RID: 56092 RVA: 0x00136C93 File Offset: 0x00134E93
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9C3 RID: 43459
			// (set) Token: 0x0600DB1D RID: 56093 RVA: 0x00136CB1 File Offset: 0x00134EB1
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9C4 RID: 43460
			// (set) Token: 0x0600DB1E RID: 56094 RVA: 0x00136CCF File Offset: 0x00134ECF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A9C5 RID: 43461
			// (set) Token: 0x0600DB1F RID: 56095 RVA: 0x00136CE2 File Offset: 0x00134EE2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A9C6 RID: 43462
			// (set) Token: 0x0600DB20 RID: 56096 RVA: 0x00136CFA File Offset: 0x00134EFA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A9C7 RID: 43463
			// (set) Token: 0x0600DB21 RID: 56097 RVA: 0x00136D12 File Offset: 0x00134F12
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A9C8 RID: 43464
			// (set) Token: 0x0600DB22 RID: 56098 RVA: 0x00136D2A File Offset: 0x00134F2A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A9C9 RID: 43465
			// (set) Token: 0x0600DB23 RID: 56099 RVA: 0x00136D42 File Offset: 0x00134F42
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E89 RID: 3721
		public class DisableSubscriptionAsPoisonParameters : ParametersBase
		{
			// Token: 0x1700A9CA RID: 43466
			// (set) Token: 0x0600DB25 RID: 56101 RVA: 0x00136D62 File Offset: 0x00134F62
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9CB RID: 43467
			// (set) Token: 0x0600DB26 RID: 56102 RVA: 0x00136D80 File Offset: 0x00134F80
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9CC RID: 43468
			// (set) Token: 0x0600DB27 RID: 56103 RVA: 0x00136D9E File Offset: 0x00134F9E
			public virtual SwitchParameter DisableAsPoison
			{
				set
				{
					base.PowerSharpParameters["DisableAsPoison"] = value;
				}
			}

			// Token: 0x1700A9CD RID: 43469
			// (set) Token: 0x0600DB28 RID: 56104 RVA: 0x00136DB6 File Offset: 0x00134FB6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A9CE RID: 43470
			// (set) Token: 0x0600DB29 RID: 56105 RVA: 0x00136DC9 File Offset: 0x00134FC9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A9CF RID: 43471
			// (set) Token: 0x0600DB2A RID: 56106 RVA: 0x00136DE1 File Offset: 0x00134FE1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A9D0 RID: 43472
			// (set) Token: 0x0600DB2B RID: 56107 RVA: 0x00136DF9 File Offset: 0x00134FF9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A9D1 RID: 43473
			// (set) Token: 0x0600DB2C RID: 56108 RVA: 0x00136E11 File Offset: 0x00135011
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A9D2 RID: 43474
			// (set) Token: 0x0600DB2D RID: 56109 RVA: 0x00136E29 File Offset: 0x00135029
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E8A RID: 3722
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A9D3 RID: 43475
			// (set) Token: 0x0600DB2F RID: 56111 RVA: 0x00136E49 File Offset: 0x00135049
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A9D4 RID: 43476
			// (set) Token: 0x0600DB30 RID: 56112 RVA: 0x00136E5C File Offset: 0x0013505C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A9D5 RID: 43477
			// (set) Token: 0x0600DB31 RID: 56113 RVA: 0x00136E74 File Offset: 0x00135074
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A9D6 RID: 43478
			// (set) Token: 0x0600DB32 RID: 56114 RVA: 0x00136E8C File Offset: 0x0013508C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A9D7 RID: 43479
			// (set) Token: 0x0600DB33 RID: 56115 RVA: 0x00136EA4 File Offset: 0x001350A4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A9D8 RID: 43480
			// (set) Token: 0x0600DB34 RID: 56116 RVA: 0x00136EBC File Offset: 0x001350BC
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
