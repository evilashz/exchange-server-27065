using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.MailboxRules;
using Microsoft.Exchange.MailboxTransport.StoreDriverDelivery;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Transport.MailboxRules
{
	// Token: 0x0200008F RID: 143
	internal class MessageContext : RuleEvaluationContext
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x00019D99 File Offset: 0x00017F99
		public MessageContext(Folder folder, MessageItem message, StoreSession session, ProxyAddress recipientAddress, ADRecipientCache<TransportMiniRecipient> recipientCache, long mimeSize, MailItemDeliver mailItemDeliver) : base(folder, message, session, recipientAddress, recipientCache, mimeSize, mailItemDeliver)
		{
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00019DAC File Offset: 0x00017FAC
		protected MessageContext(RuleEvaluationContext parentContext) : base(parentContext)
		{
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00019DB5 File Offset: 0x00017FB5
		protected override IStorePropertyBag PropertyBag
		{
			get
			{
				return base.Message;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00019DBD File Offset: 0x00017FBD
		public override IRuleEvaluationContext GetAttachmentContext(Attachment attachment)
		{
			return new AttachmentContext(this, attachment);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00019DC6 File Offset: 0x00017FC6
		public override IRuleEvaluationContext GetRecipientContext(Recipient recipient)
		{
			return new RecipientContext(this, recipient);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00019DD0 File Offset: 0x00017FD0
		public override bool CompareSingleValue(PropTag tag, Restriction.RelOp op, object messageValue, object ruleValue)
		{
			bool flag = base.CompareSingleValue(tag, op, messageValue, ruleValue);
			if (flag || tag != PropTag.SenderSearchKey)
			{
				return flag;
			}
			return base.CompareAddresses(messageValue, ruleValue);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00019DFF File Offset: 0x00017FFF
		internal override void SetRecipient(ProxyAddress recipient)
		{
			base.SetRecipient(recipient);
			this.recipientType = null;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00019E14 File Offset: 0x00018014
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
			else if (tag <= PropTag.MessageFlags)
			{
				if (tag == PropTag.SenderSearchKey)
				{
					return RuleUtil.SearchKeyFromParticipant(base.Message.Sender);
				}
				if (tag == PropTag.MessageFlags)
				{
					if (base.Message.AttachmentCollection.Count > 0)
					{
						return 16;
					}
					return 0;
				}
			}
			else if (tag != PropTag.MessageSize)
			{
				if (tag == PropTag.MessageSizeExtended)
				{
					return base.MimeSize;
				}
				if (tag == PropTag.Body)
				{
					return this.GetMessageBody();
				}
			}
			else
			{
				if (base.MimeSize > 2147483647L)
				{
					return int.MaxValue;
				}
				return (int)base.MimeSize;
			}
			return null;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00019F7C File Offset: 0x0001817C
		private string GetMessageBody()
		{
			if (this.body == null)
			{
				this.body = RuleEvaluationContextBase.GetMessageBody(this);
			}
			return this.body;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00019F98 File Offset: 0x00018198
		private RecipientItemType GetRecipientType()
		{
			if (this.recipientType == null)
			{
				this.recipientType = new RecipientItemType?(RuleEvaluationContextBase.GetRecipientType(this));
			}
			return this.recipientType.Value;
		}

		// Token: 0x040002BB RID: 699
		private string body;

		// Token: 0x040002BC RID: 700
		private RecipientItemType? recipientType;
	}
}
