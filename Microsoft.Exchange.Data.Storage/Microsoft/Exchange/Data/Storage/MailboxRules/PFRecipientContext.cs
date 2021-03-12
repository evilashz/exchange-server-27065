using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BF2 RID: 3058
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PFRecipientContext : PFRuleEvaluationContext
	{
		// Token: 0x06006D20 RID: 27936 RVA: 0x001D2503 File Offset: 0x001D0703
		public PFRecipientContext(PFMessageContext messageContext, Recipient recipient) : base(messageContext)
		{
			this.recipient = recipient;
		}

		// Token: 0x17001DB7 RID: 7607
		// (get) Token: 0x06006D21 RID: 27937 RVA: 0x001D2513 File Offset: 0x001D0713
		protected override IStorePropertyBag PropertyBag
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x06006D22 RID: 27938 RVA: 0x001D251C File Offset: 0x001D071C
		public override bool CompareSingleValue(PropTag tag, Restriction.RelOp op, object messageValue, object ruleValue)
		{
			bool flag = base.CompareSingleValue(tag, op, messageValue, ruleValue);
			if (flag || tag != PropTag.SearchKey)
			{
				base.TraceDebug<bool>("PFRecipientContext.CompareSingleValue returning {0}.", flag);
				return flag;
			}
			return base.CompareAddresses(messageValue, ruleValue);
		}

		// Token: 0x06006D23 RID: 27939 RVA: 0x001D2557 File Offset: 0x001D0757
		protected override object CalculatePropertyValue(PropTag tag)
		{
			if (tag == PropTag.SearchKey)
			{
				return RuleUtil.SearchKeyFromParticipant(this.recipient.Participant);
			}
			return null;
		}

		// Token: 0x06006D24 RID: 27940 RVA: 0x001D2573 File Offset: 0x001D0773
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x04003E25 RID: 15909
		private Recipient recipient;
	}
}
