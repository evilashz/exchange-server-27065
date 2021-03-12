using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BF1 RID: 3057
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PFMessageContext : PFRuleEvaluationContext
	{
		// Token: 0x06006D17 RID: 27927 RVA: 0x001D22F9 File Offset: 0x001D04F9
		public PFMessageContext(Folder folder, ICoreItem message, StoreSession session, ProxyAddress recipientAddress, IADRecipientCache recipientCache, long mimeSize) : base(folder, message, session, recipientAddress, recipientCache, mimeSize)
		{
		}

		// Token: 0x06006D18 RID: 27928 RVA: 0x001D230A File Offset: 0x001D050A
		protected PFMessageContext(PFRuleEvaluationContext parentContext) : base(parentContext)
		{
		}

		// Token: 0x17001DB6 RID: 7606
		// (get) Token: 0x06006D19 RID: 27929 RVA: 0x001D2313 File Offset: 0x001D0513
		protected override IStorePropertyBag PropertyBag
		{
			get
			{
				return base.Message;
			}
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x001D231B File Offset: 0x001D051B
		public override IRuleEvaluationContext GetAttachmentContext(Attachment attachment)
		{
			return new PFAttachmentContext(this, attachment);
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x001D2324 File Offset: 0x001D0524
		public override IRuleEvaluationContext GetRecipientContext(Recipient recipient)
		{
			return new PFRecipientContext(this, recipient);
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x001D2330 File Offset: 0x001D0530
		public override bool CompareSingleValue(PropTag tag, Restriction.RelOp op, object messageValue, object ruleValue)
		{
			bool flag = base.CompareSingleValue(tag, op, messageValue, ruleValue);
			if (flag || tag != PropTag.SenderSearchKey)
			{
				return flag;
			}
			return base.CompareAddresses(messageValue, ruleValue);
		}

		// Token: 0x06006D1D RID: 27933 RVA: 0x001D2360 File Offset: 0x001D0560
		protected override object CalculatePropertyValue(PropTag tag)
		{
			if (tag <= PropTag.MessageRecipMe)
			{
				if (tag <= PropTag.Sensitivity)
				{
					if (tag == PropTag.Importance)
					{
						object propertyValue = base.GetPropertyValue(tag);
						return propertyValue ?? 1;
					}
					if (tag == PropTag.Sensitivity)
					{
						object propertyValue2 = base.GetPropertyValue(tag);
						return propertyValue2 ?? 0;
					}
				}
				else
				{
					if (tag == PropTag.MessageToMe)
					{
						return this.GetRecipientType() == RecipientItemType.To;
					}
					if (tag == PropTag.MessageCcMe)
					{
						return this.GetRecipientType() == RecipientItemType.Cc;
					}
					if (tag == PropTag.MessageRecipMe)
					{
						return this.GetRecipientType() != RecipientItemType.Unknown;
					}
				}
			}
			else if (tag <= PropTag.MessageSize)
			{
				if (tag == PropTag.SenderSearchKey)
				{
					return RuleUtil.SearchKeyFromParticipant(base.Message.Sender);
				}
				if (tag == PropTag.MessageSize)
				{
					if (base.MimeSize > 2147483647L)
					{
						return int.MaxValue;
					}
					return (int)base.MimeSize;
				}
			}
			else
			{
				if (tag == PropTag.MessageSizeExtended)
				{
					return base.MimeSize;
				}
				if (tag == PropTag.Body)
				{
					return this.GetMessageBody();
				}
				if (tag == (PropTag)1716650242U)
				{
					return base.Message.PropertyBag.TryGetProperty(InternalSchema.SenderEntryId);
				}
			}
			return null;
		}

		// Token: 0x06006D1E RID: 27934 RVA: 0x001D24BC File Offset: 0x001D06BC
		private string GetMessageBody()
		{
			if (this.body == null)
			{
				this.body = RuleEvaluationContextBase.GetMessageBody(this);
			}
			return this.body;
		}

		// Token: 0x06006D1F RID: 27935 RVA: 0x001D24D8 File Offset: 0x001D06D8
		private RecipientItemType GetRecipientType()
		{
			if (this.recipientType == null)
			{
				this.recipientType = new RecipientItemType?(RuleEvaluationContextBase.GetRecipientType(this));
			}
			return this.recipientType.Value;
		}

		// Token: 0x04003E22 RID: 15906
		private const PropTag TagActiveUserEntryId = (PropTag)1716650242U;

		// Token: 0x04003E23 RID: 15907
		private string body;

		// Token: 0x04003E24 RID: 15908
		private RecipientItemType? recipientType;
	}
}
