using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.HygieneRules
{
	// Token: 0x02000008 RID: 8
	internal sealed class HygieneTransportRulesEvaluator : RulesEvaluator
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000239C File Offset: 0x0000059C
		public HygieneTransportRulesEvaluator(HygieneTransportRulesEvaluationContext context) : base(context)
		{
			this.context = context;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023AC File Offset: 0x000005AC
		protected override ExecutionControl EnterRule()
		{
			HygieneTransportRule hygieneTransportRule = (HygieneTransportRule)this.context.CurrentRule;
			RuleCollection rules = this.context.Rules;
			if (hygieneTransportRule.Fork != null && hygieneTransportRule.Fork.Count > 0)
			{
				MailItem mailItem = this.context.MailItem;
				List<EnvelopeRecipient> list = null;
				foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
				{
					string recipient = envelopeRecipient.Address.ToString();
					if (this.RecipientMatchesForkInfo(hygieneTransportRule, recipient, this.context.Server))
					{
						if (list == null)
						{
							list = new List<EnvelopeRecipient>();
						}
						list.Add(envelopeRecipient);
					}
				}
				if (list == null)
				{
					this.ExitRule();
					return ExecutionControl.SkipThis;
				}
				if (mailItem.Recipients.Count != list.Count)
				{
					this.context.MatchedRecipients = list;
				}
			}
			return ExecutionControl.Execute;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024AC File Offset: 0x000006AC
		protected override bool EnterRuleActionBlock()
		{
			if (this.context.MatchedRecipients != null && this.context.MatchedRecipients.Count > 0)
			{
				if (this.context.EventSource != null)
				{
					this.context.EventSource.Fork(this.context.MatchedRecipients);
				}
				this.context.MatchedRecipients = null;
			}
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002510 File Offset: 0x00000710
		private bool RecipientMatchesForkInfo(HygieneTransportRule rule, string recipient, SmtpServer server)
		{
			bool flag = true;
			for (int i = 0; i < rule.Fork.Count; i++)
			{
				if (!rule.Fork[i].Exception)
				{
					BifurcationInfo bifInfo = rule.Fork[i];
					if (!this.MatchesSingleBifurcationInfo(recipient, bifInfo, server))
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				for (int j = 0; j < rule.Fork.Count; j++)
				{
					if (rule.Fork[j].Exception)
					{
						BifurcationInfo bifInfo2 = rule.Fork[j];
						if (this.MatchesSingleBifurcationInfo(recipient, bifInfo2, server))
						{
							flag = false;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025AC File Offset: 0x000007AC
		private bool MatchesSingleBifurcationInfo(string recipient, BifurcationInfo bifInfo, SmtpServer server)
		{
			foreach (string x in bifInfo.Recipients)
			{
				if (this.context.UserComparer.Equals(x, recipient))
				{
					return true;
				}
			}
			foreach (string text in bifInfo.Lists)
			{
				if (recipient.Equals(text, StringComparison.InvariantCultureIgnoreCase) || this.context.MembershipChecker.Equals(recipient, text))
				{
					return true;
				}
			}
			foreach (string domain in bifInfo.RecipientDomainIs)
			{
				string domain2 = new SmtpAddress(recipient).Domain;
				if (!string.IsNullOrEmpty(domain2) && DomainIsPredicate.IsSubdomainOf(domain, domain2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000014 RID: 20
		private readonly HygieneTransportRulesEvaluationContext context;
	}
}
