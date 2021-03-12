using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.MailboxRules;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Transport.MailboxRules
{
	// Token: 0x02000090 RID: 144
	internal class RecipientContext : RuleEvaluationContext
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x00019FC3 File Offset: 0x000181C3
		public RecipientContext(MessageContext messageContext, Recipient recipient) : base(messageContext)
		{
			this.recipient = recipient;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00019FD3 File Offset: 0x000181D3
		protected override IStorePropertyBag PropertyBag
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00019FDC File Offset: 0x000181DC
		public override bool CompareSingleValue(PropTag tag, Restriction.RelOp op, object messageValue, object ruleValue)
		{
			bool flag = base.CompareSingleValue(tag, op, messageValue, ruleValue);
			if (flag || tag != PropTag.SearchKey)
			{
				base.TraceDebug<bool>("RecipientContext.CompareSingleValue returning {0}.", flag);
				return flag;
			}
			return base.CompareAddresses(messageValue, ruleValue);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001A017 File Offset: 0x00018217
		protected override object CalculatePropertyValue(PropTag tag)
		{
			if (tag == PropTag.SearchKey)
			{
				return RuleUtil.SearchKeyFromParticipant(this.recipient.Participant);
			}
			return null;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001A033 File Offset: 0x00018233
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x040002BD RID: 701
		private Recipient recipient;
	}
}
