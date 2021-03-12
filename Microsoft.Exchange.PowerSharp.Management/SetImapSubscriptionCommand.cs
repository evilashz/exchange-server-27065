using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E8B RID: 3723
	public class SetImapSubscriptionCommand : SyntheticCommandWithPipelineInputNoOutput<IMAPSubscriptionProxy>
	{
		// Token: 0x0600DB36 RID: 56118 RVA: 0x00136EDC File Offset: 0x001350DC
		private SetImapSubscriptionCommand() : base("Set-ImapSubscription")
		{
		}

		// Token: 0x0600DB37 RID: 56119 RVA: 0x00136EE9 File Offset: 0x001350E9
		public SetImapSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DB38 RID: 56120 RVA: 0x00136EF8 File Offset: 0x001350F8
		public virtual SetImapSubscriptionCommand SetParameters(SetImapSubscriptionCommand.SubscriptionModificationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB39 RID: 56121 RVA: 0x00136F02 File Offset: 0x00135102
		public virtual SetImapSubscriptionCommand SetParameters(SetImapSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB3A RID: 56122 RVA: 0x00136F0C File Offset: 0x0013510C
		public virtual SetImapSubscriptionCommand SetParameters(SetImapSubscriptionCommand.DisableSubscriptionAsPoisonParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB3B RID: 56123 RVA: 0x00136F16 File Offset: 0x00135116
		public virtual SetImapSubscriptionCommand SetParameters(SetImapSubscriptionCommand.ResendVerificationEmailParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB3C RID: 56124 RVA: 0x00136F20 File Offset: 0x00135120
		public virtual SetImapSubscriptionCommand SetParameters(SetImapSubscriptionCommand.ValidateSendAsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB3D RID: 56125 RVA: 0x00136F2A File Offset: 0x0013512A
		public virtual SetImapSubscriptionCommand SetParameters(SetImapSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E8C RID: 3724
		public class SubscriptionModificationParameters : ParametersBase
		{
			// Token: 0x1700A9D9 RID: 43481
			// (set) Token: 0x0600DB3E RID: 56126 RVA: 0x00136F34 File Offset: 0x00135134
			public virtual IMAPAuthenticationMechanism IncomingAuth
			{
				set
				{
					base.PowerSharpParameters["IncomingAuth"] = value;
				}
			}

			// Token: 0x1700A9DA RID: 43482
			// (set) Token: 0x0600DB3F RID: 56127 RVA: 0x00136F4C File Offset: 0x0013514C
			public virtual IMAPSecurityMechanism IncomingSecurity
			{
				set
				{
					base.PowerSharpParameters["IncomingSecurity"] = value;
				}
			}

			// Token: 0x1700A9DB RID: 43483
			// (set) Token: 0x0600DB40 RID: 56128 RVA: 0x00136F64 File Offset: 0x00135164
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9DC RID: 43484
			// (set) Token: 0x0600DB41 RID: 56129 RVA: 0x00136F82 File Offset: 0x00135182
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9DD RID: 43485
			// (set) Token: 0x0600DB42 RID: 56130 RVA: 0x00136FA0 File Offset: 0x001351A0
			public virtual SmtpAddress EmailAddress
			{
				set
				{
					base.PowerSharpParameters["EmailAddress"] = value;
				}
			}

			// Token: 0x1700A9DE RID: 43486
			// (set) Token: 0x0600DB43 RID: 56131 RVA: 0x00136FB8 File Offset: 0x001351B8
			public virtual string IncomingUserName
			{
				set
				{
					base.PowerSharpParameters["IncomingUserName"] = value;
				}
			}

			// Token: 0x1700A9DF RID: 43487
			// (set) Token: 0x0600DB44 RID: 56132 RVA: 0x00136FCB File Offset: 0x001351CB
			public virtual SecureString IncomingPassword
			{
				set
				{
					base.PowerSharpParameters["IncomingPassword"] = value;
				}
			}

			// Token: 0x1700A9E0 RID: 43488
			// (set) Token: 0x0600DB45 RID: 56133 RVA: 0x00136FDE File Offset: 0x001351DE
			public virtual Fqdn IncomingServer
			{
				set
				{
					base.PowerSharpParameters["IncomingServer"] = value;
				}
			}

			// Token: 0x1700A9E1 RID: 43489
			// (set) Token: 0x0600DB46 RID: 56134 RVA: 0x00136FF1 File Offset: 0x001351F1
			public virtual int IncomingPort
			{
				set
				{
					base.PowerSharpParameters["IncomingPort"] = value;
				}
			}

			// Token: 0x1700A9E2 RID: 43490
			// (set) Token: 0x0600DB47 RID: 56135 RVA: 0x00137009 File Offset: 0x00135209
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700A9E3 RID: 43491
			// (set) Token: 0x0600DB48 RID: 56136 RVA: 0x00137021 File Offset: 0x00135221
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A9E4 RID: 43492
			// (set) Token: 0x0600DB49 RID: 56137 RVA: 0x00137034 File Offset: 0x00135234
			public virtual SwitchParameter EnablePoisonSubscription
			{
				set
				{
					base.PowerSharpParameters["EnablePoisonSubscription"] = value;
				}
			}

			// Token: 0x1700A9E5 RID: 43493
			// (set) Token: 0x0600DB4A RID: 56138 RVA: 0x0013704C File Offset: 0x0013524C
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700A9E6 RID: 43494
			// (set) Token: 0x0600DB4B RID: 56139 RVA: 0x00137064 File Offset: 0x00135264
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A9E7 RID: 43495
			// (set) Token: 0x0600DB4C RID: 56140 RVA: 0x00137077 File Offset: 0x00135277
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A9E8 RID: 43496
			// (set) Token: 0x0600DB4D RID: 56141 RVA: 0x0013708F File Offset: 0x0013528F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A9E9 RID: 43497
			// (set) Token: 0x0600DB4E RID: 56142 RVA: 0x001370A7 File Offset: 0x001352A7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A9EA RID: 43498
			// (set) Token: 0x0600DB4F RID: 56143 RVA: 0x001370BF File Offset: 0x001352BF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A9EB RID: 43499
			// (set) Token: 0x0600DB50 RID: 56144 RVA: 0x001370D7 File Offset: 0x001352D7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E8D RID: 3725
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A9EC RID: 43500
			// (set) Token: 0x0600DB52 RID: 56146 RVA: 0x001370F7 File Offset: 0x001352F7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9ED RID: 43501
			// (set) Token: 0x0600DB53 RID: 56147 RVA: 0x00137115 File Offset: 0x00135315
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9EE RID: 43502
			// (set) Token: 0x0600DB54 RID: 56148 RVA: 0x00137133 File Offset: 0x00135333
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A9EF RID: 43503
			// (set) Token: 0x0600DB55 RID: 56149 RVA: 0x00137146 File Offset: 0x00135346
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A9F0 RID: 43504
			// (set) Token: 0x0600DB56 RID: 56150 RVA: 0x0013715E File Offset: 0x0013535E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A9F1 RID: 43505
			// (set) Token: 0x0600DB57 RID: 56151 RVA: 0x00137176 File Offset: 0x00135376
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A9F2 RID: 43506
			// (set) Token: 0x0600DB58 RID: 56152 RVA: 0x0013718E File Offset: 0x0013538E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A9F3 RID: 43507
			// (set) Token: 0x0600DB59 RID: 56153 RVA: 0x001371A6 File Offset: 0x001353A6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E8E RID: 3726
		public class DisableSubscriptionAsPoisonParameters : ParametersBase
		{
			// Token: 0x1700A9F4 RID: 43508
			// (set) Token: 0x0600DB5B RID: 56155 RVA: 0x001371C6 File Offset: 0x001353C6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9F5 RID: 43509
			// (set) Token: 0x0600DB5C RID: 56156 RVA: 0x001371E4 File Offset: 0x001353E4
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9F6 RID: 43510
			// (set) Token: 0x0600DB5D RID: 56157 RVA: 0x00137202 File Offset: 0x00135402
			public virtual SwitchParameter DisableAsPoison
			{
				set
				{
					base.PowerSharpParameters["DisableAsPoison"] = value;
				}
			}

			// Token: 0x1700A9F7 RID: 43511
			// (set) Token: 0x0600DB5E RID: 56158 RVA: 0x0013721A File Offset: 0x0013541A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A9F8 RID: 43512
			// (set) Token: 0x0600DB5F RID: 56159 RVA: 0x0013722D File Offset: 0x0013542D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A9F9 RID: 43513
			// (set) Token: 0x0600DB60 RID: 56160 RVA: 0x00137245 File Offset: 0x00135445
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A9FA RID: 43514
			// (set) Token: 0x0600DB61 RID: 56161 RVA: 0x0013725D File Offset: 0x0013545D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A9FB RID: 43515
			// (set) Token: 0x0600DB62 RID: 56162 RVA: 0x00137275 File Offset: 0x00135475
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A9FC RID: 43516
			// (set) Token: 0x0600DB63 RID: 56163 RVA: 0x0013728D File Offset: 0x0013548D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E8F RID: 3727
		public class ResendVerificationEmailParameters : ParametersBase
		{
			// Token: 0x1700A9FD RID: 43517
			// (set) Token: 0x0600DB65 RID: 56165 RVA: 0x001372AD File Offset: 0x001354AD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9FE RID: 43518
			// (set) Token: 0x0600DB66 RID: 56166 RVA: 0x001372CB File Offset: 0x001354CB
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9FF RID: 43519
			// (set) Token: 0x0600DB67 RID: 56167 RVA: 0x001372E9 File Offset: 0x001354E9
			public virtual SwitchParameter ResendVerification
			{
				set
				{
					base.PowerSharpParameters["ResendVerification"] = value;
				}
			}

			// Token: 0x1700AA00 RID: 43520
			// (set) Token: 0x0600DB68 RID: 56168 RVA: 0x00137301 File Offset: 0x00135501
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA01 RID: 43521
			// (set) Token: 0x0600DB69 RID: 56169 RVA: 0x00137314 File Offset: 0x00135514
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA02 RID: 43522
			// (set) Token: 0x0600DB6A RID: 56170 RVA: 0x0013732C File Offset: 0x0013552C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA03 RID: 43523
			// (set) Token: 0x0600DB6B RID: 56171 RVA: 0x00137344 File Offset: 0x00135544
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA04 RID: 43524
			// (set) Token: 0x0600DB6C RID: 56172 RVA: 0x0013735C File Offset: 0x0013555C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA05 RID: 43525
			// (set) Token: 0x0600DB6D RID: 56173 RVA: 0x00137374 File Offset: 0x00135574
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E90 RID: 3728
		public class ValidateSendAsParameters : ParametersBase
		{
			// Token: 0x1700AA06 RID: 43526
			// (set) Token: 0x0600DB6F RID: 56175 RVA: 0x00137394 File Offset: 0x00135594
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA07 RID: 43527
			// (set) Token: 0x0600DB70 RID: 56176 RVA: 0x001373B2 File Offset: 0x001355B2
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA08 RID: 43528
			// (set) Token: 0x0600DB71 RID: 56177 RVA: 0x001373D0 File Offset: 0x001355D0
			public virtual string ValidateSecret
			{
				set
				{
					base.PowerSharpParameters["ValidateSecret"] = value;
				}
			}

			// Token: 0x1700AA09 RID: 43529
			// (set) Token: 0x0600DB72 RID: 56178 RVA: 0x001373E3 File Offset: 0x001355E3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA0A RID: 43530
			// (set) Token: 0x0600DB73 RID: 56179 RVA: 0x001373F6 File Offset: 0x001355F6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA0B RID: 43531
			// (set) Token: 0x0600DB74 RID: 56180 RVA: 0x0013740E File Offset: 0x0013560E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA0C RID: 43532
			// (set) Token: 0x0600DB75 RID: 56181 RVA: 0x00137426 File Offset: 0x00135626
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA0D RID: 43533
			// (set) Token: 0x0600DB76 RID: 56182 RVA: 0x0013743E File Offset: 0x0013563E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA0E RID: 43534
			// (set) Token: 0x0600DB77 RID: 56183 RVA: 0x00137456 File Offset: 0x00135656
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E91 RID: 3729
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700AA0F RID: 43535
			// (set) Token: 0x0600DB79 RID: 56185 RVA: 0x00137476 File Offset: 0x00135676
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA10 RID: 43536
			// (set) Token: 0x0600DB7A RID: 56186 RVA: 0x00137489 File Offset: 0x00135689
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA11 RID: 43537
			// (set) Token: 0x0600DB7B RID: 56187 RVA: 0x001374A1 File Offset: 0x001356A1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA12 RID: 43538
			// (set) Token: 0x0600DB7C RID: 56188 RVA: 0x001374B9 File Offset: 0x001356B9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA13 RID: 43539
			// (set) Token: 0x0600DB7D RID: 56189 RVA: 0x001374D1 File Offset: 0x001356D1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA14 RID: 43540
			// (set) Token: 0x0600DB7E RID: 56190 RVA: 0x001374E9 File Offset: 0x001356E9
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
