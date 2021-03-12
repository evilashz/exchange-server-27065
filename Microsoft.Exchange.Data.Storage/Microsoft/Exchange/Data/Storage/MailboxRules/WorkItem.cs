using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BD8 RID: 3032
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class WorkItem
	{
		// Token: 0x06006BB9 RID: 27577 RVA: 0x001CD790 File Offset: 0x001CB990
		public WorkItem(IRuleEvaluationContext context, int actionIndex)
		{
			this.context = context;
			this.rule = this.context.CurrentRule;
			this.actionIndex = actionIndex;
		}

		// Token: 0x17001D3B RID: 7483
		// (get) Token: 0x06006BBA RID: 27578 RVA: 0x001CD7B7 File Offset: 0x001CB9B7
		public IRuleEvaluationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17001D3C RID: 7484
		// (get) Token: 0x06006BBB RID: 27579 RVA: 0x001CD7BF File Offset: 0x001CB9BF
		public Rule Rule
		{
			get
			{
				return this.rule;
			}
		}

		// Token: 0x17001D3D RID: 7485
		// (get) Token: 0x06006BBC RID: 27580 RVA: 0x001CD7C7 File Offset: 0x001CB9C7
		public int ActionIndex
		{
			get
			{
				return this.actionIndex;
			}
		}

		// Token: 0x17001D3E RID: 7486
		// (get) Token: 0x06006BBD RID: 27581 RVA: 0x001CD7CF File Offset: 0x001CB9CF
		public virtual bool ShouldExecuteOnThisStage
		{
			get
			{
				return (this.context.ExecutionStage == ExecutionStage.OnPromotedMessage && this.context.DeliveryFolder == null) || (this.context.ExecutionStage == ExecutionStage.OnDeliveredMessage && this.context.DeliveryFolder != null);
			}
		}

		// Token: 0x17001D3F RID: 7487
		// (get) Token: 0x06006BBE RID: 27582
		public abstract ExecutionStage Stage { get; }

		// Token: 0x06006BBF RID: 27583
		public abstract void Execute();

		// Token: 0x06006BC0 RID: 27584 RVA: 0x001CD80C File Offset: 0x001CBA0C
		protected MessageItem OpenMessage(byte[] messageEntryId)
		{
			if (messageEntryId == null)
			{
				throw new InvalidRuleException(string.Format("Rule {0} is invalid since its message template id is null.", this.rule.Name));
			}
			StoreId itemId = StoreObjectId.FromProviderSpecificId(messageEntryId);
			return Item.BindAsMessage(this.Context.StoreSession, itemId, StoreObjectSchema.ContentConversionProperties);
		}

		// Token: 0x06006BC1 RID: 27585 RVA: 0x001CD854 File Offset: 0x001CBA54
		protected void SetRecipientsResponsibility(MessageItem message)
		{
			foreach (Recipient recipient in message.Recipients)
			{
				recipient[ItemSchema.Responsibility] = true;
			}
		}

		// Token: 0x06006BC2 RID: 27586 RVA: 0x001CD8AC File Offset: 0x001CBAAC
		protected void SubmitMessage(MessageItem message)
		{
			this.context.TraceDebug<string>("Submitting message with subject {0}", message.Subject);
			this.AppendRuleHistory(message);
			using (ISubmissionItem submissionItem = this.Context.GenerateSubmissionItem(message, this))
			{
				submissionItem.Submit();
			}
		}

		// Token: 0x06006BC3 RID: 27587 RVA: 0x001CD908 File Offset: 0x001CBB08
		protected void SubmitMessage(MessageItem message, ProxyAddress sender, IEnumerable<Participant> recipients)
		{
			this.context.TraceDebug<string>("Submitting message with subject {0}", message.Subject);
			this.AppendRuleHistory(message);
			using (ISubmissionItem submissionItem = this.Context.GenerateSubmissionItem(message, this))
			{
				submissionItem.Submit(sender, recipients);
			}
		}

		// Token: 0x06006BC4 RID: 27588 RVA: 0x001CD964 File Offset: 0x001CBB64
		protected IList<ProxyAddress> GetSenderProxyAddresses()
		{
			if (this.Context.Sender == null)
			{
				return Array<ProxyAddress>.Empty;
			}
			Result<ADRawEntry> result = this.Context.RecipientCache.FindAndCacheRecipient(this.Context.Sender);
			if (result.Data == null)
			{
				return new ProxyAddress[]
				{
					this.Context.Sender
				};
			}
			List<ProxyAddress> list = new List<ProxyAddress>((IList<ProxyAddress>)result.Data[ADRecipientSchema.EmailAddresses]);
			try
			{
				list.Add(new CustomProxyAddress((CustomProxyAddressPrefix)ProxyAddressPrefix.LegacyDN, (string)result.Data[ADRecipientSchema.LegacyExchangeDN], true));
			}
			catch (ArgumentOutOfRangeException argument)
			{
				this.Context.TraceError<ArgumentOutOfRangeException>("Invalid LegacyDN. Exception: {0}", argument);
			}
			return list;
		}

		// Token: 0x06006BC5 RID: 27589 RVA: 0x001CDA38 File Offset: 0x001CBC38
		private void AppendRuleHistory(MessageItem message)
		{
			byte[] array = this.Context.Message.TryGetProperty(ItemSchema.RuleTriggerHistory) as byte[];
			if (array != null)
			{
				message[ItemSchema.RuleTriggerHistory] = array;
			}
			RuleHistory ruleHistory = message.GetRuleHistory(this.Context.StoreSession);
			ruleHistory.Add(this.rule.ID);
			ruleHistory.Save();
		}

		// Token: 0x04003DA6 RID: 15782
		private IRuleEvaluationContext context;

		// Token: 0x04003DA7 RID: 15783
		private Rule rule;

		// Token: 0x04003DA8 RID: 15784
		private int actionIndex;
	}
}
