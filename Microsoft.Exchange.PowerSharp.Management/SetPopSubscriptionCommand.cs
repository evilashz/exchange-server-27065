using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E92 RID: 3730
	public class SetPopSubscriptionCommand : SyntheticCommandWithPipelineInputNoOutput<PopSubscriptionProxy>
	{
		// Token: 0x0600DB80 RID: 56192 RVA: 0x00137509 File Offset: 0x00135709
		private SetPopSubscriptionCommand() : base("Set-PopSubscription")
		{
		}

		// Token: 0x0600DB81 RID: 56193 RVA: 0x00137516 File Offset: 0x00135716
		public SetPopSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DB82 RID: 56194 RVA: 0x00137525 File Offset: 0x00135725
		public virtual SetPopSubscriptionCommand SetParameters(SetPopSubscriptionCommand.SubscriptionModificationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB83 RID: 56195 RVA: 0x0013752F File Offset: 0x0013572F
		public virtual SetPopSubscriptionCommand SetParameters(SetPopSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB84 RID: 56196 RVA: 0x00137539 File Offset: 0x00135739
		public virtual SetPopSubscriptionCommand SetParameters(SetPopSubscriptionCommand.DisableSubscriptionAsPoisonParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB85 RID: 56197 RVA: 0x00137543 File Offset: 0x00135743
		public virtual SetPopSubscriptionCommand SetParameters(SetPopSubscriptionCommand.ResendVerificationEmailParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB86 RID: 56198 RVA: 0x0013754D File Offset: 0x0013574D
		public virtual SetPopSubscriptionCommand SetParameters(SetPopSubscriptionCommand.ValidateSendAsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DB87 RID: 56199 RVA: 0x00137557 File Offset: 0x00135757
		public virtual SetPopSubscriptionCommand SetParameters(SetPopSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E93 RID: 3731
		public class SubscriptionModificationParameters : ParametersBase
		{
			// Token: 0x1700AA15 RID: 43541
			// (set) Token: 0x0600DB88 RID: 56200 RVA: 0x00137561 File Offset: 0x00135761
			public virtual AuthenticationMechanism IncomingAuth
			{
				set
				{
					base.PowerSharpParameters["IncomingAuth"] = value;
				}
			}

			// Token: 0x1700AA16 RID: 43542
			// (set) Token: 0x0600DB89 RID: 56201 RVA: 0x00137579 File Offset: 0x00135779
			public virtual SecurityMechanism IncomingSecurity
			{
				set
				{
					base.PowerSharpParameters["IncomingSecurity"] = value;
				}
			}

			// Token: 0x1700AA17 RID: 43543
			// (set) Token: 0x0600DB8A RID: 56202 RVA: 0x00137591 File Offset: 0x00135791
			public virtual bool LeaveOnServer
			{
				set
				{
					base.PowerSharpParameters["LeaveOnServer"] = value;
				}
			}

			// Token: 0x1700AA18 RID: 43544
			// (set) Token: 0x0600DB8B RID: 56203 RVA: 0x001375A9 File Offset: 0x001357A9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA19 RID: 43545
			// (set) Token: 0x0600DB8C RID: 56204 RVA: 0x001375C7 File Offset: 0x001357C7
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA1A RID: 43546
			// (set) Token: 0x0600DB8D RID: 56205 RVA: 0x001375E5 File Offset: 0x001357E5
			public virtual SmtpAddress EmailAddress
			{
				set
				{
					base.PowerSharpParameters["EmailAddress"] = value;
				}
			}

			// Token: 0x1700AA1B RID: 43547
			// (set) Token: 0x0600DB8E RID: 56206 RVA: 0x001375FD File Offset: 0x001357FD
			public virtual string IncomingUserName
			{
				set
				{
					base.PowerSharpParameters["IncomingUserName"] = value;
				}
			}

			// Token: 0x1700AA1C RID: 43548
			// (set) Token: 0x0600DB8F RID: 56207 RVA: 0x00137610 File Offset: 0x00135810
			public virtual SecureString IncomingPassword
			{
				set
				{
					base.PowerSharpParameters["IncomingPassword"] = value;
				}
			}

			// Token: 0x1700AA1D RID: 43549
			// (set) Token: 0x0600DB90 RID: 56208 RVA: 0x00137623 File Offset: 0x00135823
			public virtual Fqdn IncomingServer
			{
				set
				{
					base.PowerSharpParameters["IncomingServer"] = value;
				}
			}

			// Token: 0x1700AA1E RID: 43550
			// (set) Token: 0x0600DB91 RID: 56209 RVA: 0x00137636 File Offset: 0x00135836
			public virtual int IncomingPort
			{
				set
				{
					base.PowerSharpParameters["IncomingPort"] = value;
				}
			}

			// Token: 0x1700AA1F RID: 43551
			// (set) Token: 0x0600DB92 RID: 56210 RVA: 0x0013764E File Offset: 0x0013584E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700AA20 RID: 43552
			// (set) Token: 0x0600DB93 RID: 56211 RVA: 0x00137666 File Offset: 0x00135866
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AA21 RID: 43553
			// (set) Token: 0x0600DB94 RID: 56212 RVA: 0x00137679 File Offset: 0x00135879
			public virtual SwitchParameter EnablePoisonSubscription
			{
				set
				{
					base.PowerSharpParameters["EnablePoisonSubscription"] = value;
				}
			}

			// Token: 0x1700AA22 RID: 43554
			// (set) Token: 0x0600DB95 RID: 56213 RVA: 0x00137691 File Offset: 0x00135891
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AA23 RID: 43555
			// (set) Token: 0x0600DB96 RID: 56214 RVA: 0x001376A9 File Offset: 0x001358A9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA24 RID: 43556
			// (set) Token: 0x0600DB97 RID: 56215 RVA: 0x001376BC File Offset: 0x001358BC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA25 RID: 43557
			// (set) Token: 0x0600DB98 RID: 56216 RVA: 0x001376D4 File Offset: 0x001358D4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA26 RID: 43558
			// (set) Token: 0x0600DB99 RID: 56217 RVA: 0x001376EC File Offset: 0x001358EC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA27 RID: 43559
			// (set) Token: 0x0600DB9A RID: 56218 RVA: 0x00137704 File Offset: 0x00135904
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA28 RID: 43560
			// (set) Token: 0x0600DB9B RID: 56219 RVA: 0x0013771C File Offset: 0x0013591C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E94 RID: 3732
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700AA29 RID: 43561
			// (set) Token: 0x0600DB9D RID: 56221 RVA: 0x0013773C File Offset: 0x0013593C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA2A RID: 43562
			// (set) Token: 0x0600DB9E RID: 56222 RVA: 0x0013775A File Offset: 0x0013595A
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA2B RID: 43563
			// (set) Token: 0x0600DB9F RID: 56223 RVA: 0x00137778 File Offset: 0x00135978
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA2C RID: 43564
			// (set) Token: 0x0600DBA0 RID: 56224 RVA: 0x0013778B File Offset: 0x0013598B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA2D RID: 43565
			// (set) Token: 0x0600DBA1 RID: 56225 RVA: 0x001377A3 File Offset: 0x001359A3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA2E RID: 43566
			// (set) Token: 0x0600DBA2 RID: 56226 RVA: 0x001377BB File Offset: 0x001359BB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA2F RID: 43567
			// (set) Token: 0x0600DBA3 RID: 56227 RVA: 0x001377D3 File Offset: 0x001359D3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA30 RID: 43568
			// (set) Token: 0x0600DBA4 RID: 56228 RVA: 0x001377EB File Offset: 0x001359EB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E95 RID: 3733
		public class DisableSubscriptionAsPoisonParameters : ParametersBase
		{
			// Token: 0x1700AA31 RID: 43569
			// (set) Token: 0x0600DBA6 RID: 56230 RVA: 0x0013780B File Offset: 0x00135A0B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA32 RID: 43570
			// (set) Token: 0x0600DBA7 RID: 56231 RVA: 0x00137829 File Offset: 0x00135A29
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA33 RID: 43571
			// (set) Token: 0x0600DBA8 RID: 56232 RVA: 0x00137847 File Offset: 0x00135A47
			public virtual SwitchParameter DisableAsPoison
			{
				set
				{
					base.PowerSharpParameters["DisableAsPoison"] = value;
				}
			}

			// Token: 0x1700AA34 RID: 43572
			// (set) Token: 0x0600DBA9 RID: 56233 RVA: 0x0013785F File Offset: 0x00135A5F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA35 RID: 43573
			// (set) Token: 0x0600DBAA RID: 56234 RVA: 0x00137872 File Offset: 0x00135A72
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA36 RID: 43574
			// (set) Token: 0x0600DBAB RID: 56235 RVA: 0x0013788A File Offset: 0x00135A8A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA37 RID: 43575
			// (set) Token: 0x0600DBAC RID: 56236 RVA: 0x001378A2 File Offset: 0x00135AA2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA38 RID: 43576
			// (set) Token: 0x0600DBAD RID: 56237 RVA: 0x001378BA File Offset: 0x00135ABA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA39 RID: 43577
			// (set) Token: 0x0600DBAE RID: 56238 RVA: 0x001378D2 File Offset: 0x00135AD2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E96 RID: 3734
		public class ResendVerificationEmailParameters : ParametersBase
		{
			// Token: 0x1700AA3A RID: 43578
			// (set) Token: 0x0600DBB0 RID: 56240 RVA: 0x001378F2 File Offset: 0x00135AF2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA3B RID: 43579
			// (set) Token: 0x0600DBB1 RID: 56241 RVA: 0x00137910 File Offset: 0x00135B10
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA3C RID: 43580
			// (set) Token: 0x0600DBB2 RID: 56242 RVA: 0x0013792E File Offset: 0x00135B2E
			public virtual SwitchParameter ResendVerification
			{
				set
				{
					base.PowerSharpParameters["ResendVerification"] = value;
				}
			}

			// Token: 0x1700AA3D RID: 43581
			// (set) Token: 0x0600DBB3 RID: 56243 RVA: 0x00137946 File Offset: 0x00135B46
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA3E RID: 43582
			// (set) Token: 0x0600DBB4 RID: 56244 RVA: 0x00137959 File Offset: 0x00135B59
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA3F RID: 43583
			// (set) Token: 0x0600DBB5 RID: 56245 RVA: 0x00137971 File Offset: 0x00135B71
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA40 RID: 43584
			// (set) Token: 0x0600DBB6 RID: 56246 RVA: 0x00137989 File Offset: 0x00135B89
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA41 RID: 43585
			// (set) Token: 0x0600DBB7 RID: 56247 RVA: 0x001379A1 File Offset: 0x00135BA1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA42 RID: 43586
			// (set) Token: 0x0600DBB8 RID: 56248 RVA: 0x001379B9 File Offset: 0x00135BB9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E97 RID: 3735
		public class ValidateSendAsParameters : ParametersBase
		{
			// Token: 0x1700AA43 RID: 43587
			// (set) Token: 0x0600DBBA RID: 56250 RVA: 0x001379D9 File Offset: 0x00135BD9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA44 RID: 43588
			// (set) Token: 0x0600DBBB RID: 56251 RVA: 0x001379F7 File Offset: 0x00135BF7
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA45 RID: 43589
			// (set) Token: 0x0600DBBC RID: 56252 RVA: 0x00137A15 File Offset: 0x00135C15
			public virtual string ValidateSecret
			{
				set
				{
					base.PowerSharpParameters["ValidateSecret"] = value;
				}
			}

			// Token: 0x1700AA46 RID: 43590
			// (set) Token: 0x0600DBBD RID: 56253 RVA: 0x00137A28 File Offset: 0x00135C28
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA47 RID: 43591
			// (set) Token: 0x0600DBBE RID: 56254 RVA: 0x00137A3B File Offset: 0x00135C3B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA48 RID: 43592
			// (set) Token: 0x0600DBBF RID: 56255 RVA: 0x00137A53 File Offset: 0x00135C53
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA49 RID: 43593
			// (set) Token: 0x0600DBC0 RID: 56256 RVA: 0x00137A6B File Offset: 0x00135C6B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA4A RID: 43594
			// (set) Token: 0x0600DBC1 RID: 56257 RVA: 0x00137A83 File Offset: 0x00135C83
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA4B RID: 43595
			// (set) Token: 0x0600DBC2 RID: 56258 RVA: 0x00137A9B File Offset: 0x00135C9B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E98 RID: 3736
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700AA4C RID: 43596
			// (set) Token: 0x0600DBC4 RID: 56260 RVA: 0x00137ABB File Offset: 0x00135CBB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA4D RID: 43597
			// (set) Token: 0x0600DBC5 RID: 56261 RVA: 0x00137ACE File Offset: 0x00135CCE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA4E RID: 43598
			// (set) Token: 0x0600DBC6 RID: 56262 RVA: 0x00137AE6 File Offset: 0x00135CE6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA4F RID: 43599
			// (set) Token: 0x0600DBC7 RID: 56263 RVA: 0x00137AFE File Offset: 0x00135CFE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA50 RID: 43600
			// (set) Token: 0x0600DBC8 RID: 56264 RVA: 0x00137B16 File Offset: 0x00135D16
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA51 RID: 43601
			// (set) Token: 0x0600DBC9 RID: 56265 RVA: 0x00137B2E File Offset: 0x00135D2E
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
