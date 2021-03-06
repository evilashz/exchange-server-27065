using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BDC RID: 3036
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DelegateWorkItem : WorkItem
	{
		// Token: 0x06006BDD RID: 27613 RVA: 0x001CE51F File Offset: 0x001CC71F
		public DelegateWorkItem(IRuleEvaluationContext context, AdrEntry[] recipients, int actionIndex) : base(context, actionIndex)
		{
			if (RuleUtil.IsNullOrEmpty(recipients))
			{
				throw new ArgumentException("Delegate recipient list is empty");
			}
			this.recipients = recipients;
		}

		// Token: 0x17001D4A RID: 7498
		// (get) Token: 0x06006BDE RID: 27614 RVA: 0x001CE543 File Offset: 0x001CC743
		public override ExecutionStage Stage
		{
			get
			{
				return ExecutionStage.OnPromotedMessage | ExecutionStage.OnDeliveredMessage;
			}
		}

		// Token: 0x06006BDF RID: 27615 RVA: 0x001CE548 File Offset: 0x001CC748
		public override void Execute()
		{
			if (!this.ShouldExecuteOnThisStage)
			{
				return;
			}
			if (base.Context.DetectLoop())
			{
				string[] valueOrDefault = base.Context.Message.GetValueOrDefault<string[]>(MessageItemSchema.XLoop, null);
				if (valueOrDefault == null || valueOrDefault.Length != 1)
				{
					return;
				}
				base.Context.TraceDebug("Sending to Delegates even though loop was detected due to 1 XLoop header in the message.");
			}
			base.Context.TraceDebug("Delegate action: Creating message to forward.");
			using (MessageItem messageItem = RuleMessageUtils.CreateDelegateForward(base.Context.Message, base.Context.StoreSession.PreferedCulture, base.Context.DefaultDomainName, base.Context.XLoopValue, base.Context))
			{
				if (this.ShouldSubmit(messageItem))
				{
					this.SetDelegateProperties(messageItem);
					if (base.Context.PropertiesForDelegateForward != null)
					{
						foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in base.Context.PropertiesForDelegateForward)
						{
							base.Context.TraceDebug<PropertyDefinition, object>("Delegate action: setting {0} to {1}", keyValuePair.Key, keyValuePair.Value ?? "(null)");
							messageItem.SetOrDeleteProperty(keyValuePair.Key, keyValuePair.Value);
						}
					}
					messageItem.AutoResponseSuppress = AutoResponseSuppress.All;
					RuleUtil.SetRecipients(base.Context, messageItem, null, this.recipients, true);
					base.SubmitMessage(messageItem);
				}
			}
		}

		// Token: 0x06006BE0 RID: 27616 RVA: 0x001CE6C8 File Offset: 0x001CC8C8
		private void SetDelegateProperties(MessageItem newMessage)
		{
			newMessage[ItemSchema.DelegatedByRule] = true;
			if (newMessage.GetValueOrDefault<object>(MessageItemSchema.ReceivedRepresentingEntryId, null) != null)
			{
				base.Context.TraceFunction("Delegate action: Message already has received-representing property.");
				return;
			}
			Result<ADRawEntry> result = base.Context.RecipientCache.FindAndCacheRecipient(base.Context.Recipient);
			ADRawEntry data = result.Data;
			if (data == null)
			{
				base.Context.TraceFunction<ProviderError>("Delegate action: Unable to read recipient data from AD: {0}", result.Error);
				return;
			}
			base.Context.TraceFunction("Delegate action: Setting received-representing properties.");
			newMessage[MessageItemSchema.ReceivedRepresentingDisplayName] = data[ADRecipientSchema.DisplayName];
			newMessage[MessageItemSchema.ReceivedRepresentingAddressType] = "EX";
			newMessage[MessageItemSchema.ReceivedRepresentingEmailAddress] = data[ADRecipientSchema.LegacyExchangeDN];
			newMessage[MessageItemSchema.ReceivedRepresentingSmtpAddress] = data[ADRecipientSchema.PrimarySmtpAddress].ToString();
			newMessage[MessageItemSchema.ReceivedRepresentingSearchKey] = RuleUtil.SearchKeyFromAddress("EX", (string)data[ADRecipientSchema.LegacyExchangeDN]);
			newMessage[MessageItemSchema.ReceivedRepresentingEntryId] = base.Context.StoreSession.Mailbox[StoreObjectSchema.EntryId];
		}

		// Token: 0x06006BE1 RID: 27617 RVA: 0x001CE7F8 File Offset: 0x001CC9F8
		private bool ShouldSubmit(MessageItem message)
		{
			if (this.IsScheduleMessage(message))
			{
				base.Context.TraceDebug("Delegate action: Message will be forwarded.");
				return true;
			}
			ProxyAddress originalSender = RuleUtil.GetOriginalSender(base.Context.Message);
			if (originalSender == null || originalSender is InvalidProxyAddress)
			{
				base.Context.TraceDebug("Delegate action: Sent-Representing properties are not valid, message will not be forwarded.");
				return false;
			}
			base.Context.TraceDebug<ProxyAddress>("Delegate action: Message will be sent representing {0}.", originalSender);
			Result<ADRawEntry> result = base.Context.RecipientCache.FindAndCacheRecipient(originalSender);
			if (result.Data == null)
			{
				if (ProviderError.NotFound == result.Error)
				{
					base.Context.TraceError("Delegate action: Sender doesn't exist in AD.");
				}
				else
				{
					base.Context.TraceError<ProviderError>("Delegate action: Sender look up failed: {0}", result.Error);
				}
				base.Context.TraceDebug("Delegate action: Message will not be forwarded.");
				return false;
			}
			base.Context.TraceDebug("Delegate action: Message will be forwarded.");
			return true;
		}

		// Token: 0x06006BE2 RID: 27618 RVA: 0x001CE8DC File Offset: 0x001CCADC
		private bool IsScheduleMessage(MessageItem message)
		{
			if (message.ClassName.StartsWith("IPM.Schedule.", StringComparison.OrdinalIgnoreCase) || message.ClassName.Equals("IPM.SchedulePlus.FreeBusy.BinaryData", StringComparison.OrdinalIgnoreCase))
			{
				base.Context.TraceDebug("Delegate action: This is a schedule message.");
				return true;
			}
			base.Context.TraceDebug("Delegate action: This is not a schedule message.");
			return false;
		}

		// Token: 0x04003DB2 RID: 15794
		private const string FreeBusyDataMessageClass = "IPM.SchedulePlus.FreeBusy.BinaryData";

		// Token: 0x04003DB3 RID: 15795
		private const string ScheduleMessageClassPrefix = "IPM.Schedule.";

		// Token: 0x04003DB4 RID: 15796
		private AdrEntry[] recipients;
	}
}
