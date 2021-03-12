using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000149 RID: 329
	internal interface IMessageTraceVisitor
	{
		// Token: 0x06000C96 RID: 3222
		void Visit(MessageTraceEntityBase entity);

		// Token: 0x06000C97 RID: 3223
		void Visit(MessageTrace messageTrace);

		// Token: 0x06000C98 RID: 3224
		void Visit(MessageProperty messageProperty);

		// Token: 0x06000C99 RID: 3225
		void Visit(MessageEvent messageEvent);

		// Token: 0x06000C9A RID: 3226
		void Visit(MessageEventProperty messageEventProperty);

		// Token: 0x06000C9B RID: 3227
		void Visit(MessageEventRule messageEventRule);

		// Token: 0x06000C9C RID: 3228
		void Visit(MessageEventRuleProperty messageEventRuleProperty);

		// Token: 0x06000C9D RID: 3229
		void Visit(MessageEventRuleClassification messageEventRuleClassification);

		// Token: 0x06000C9E RID: 3230
		void Visit(MessageEventRuleClassificationProperty messageEventRuleClassificationProperty);

		// Token: 0x06000C9F RID: 3231
		void Visit(MessageEventSourceItem messageEventSourceItem);

		// Token: 0x06000CA0 RID: 3232
		void Visit(MessageEventSourceItemProperty messageEventSourceItemProperty);

		// Token: 0x06000CA1 RID: 3233
		void Visit(MessageClassification messageClassification);

		// Token: 0x06000CA2 RID: 3234
		void Visit(MessageClassificationProperty messageClassificationProperty);

		// Token: 0x06000CA3 RID: 3235
		void Visit(MessageClientInformation messageClientInformation);

		// Token: 0x06000CA4 RID: 3236
		void Visit(MessageClientInformationProperty messageClientInformationProperty);

		// Token: 0x06000CA5 RID: 3237
		void Visit(MessageRecipient messageRecipient);

		// Token: 0x06000CA6 RID: 3238
		void Visit(MessageRecipientProperty messageRecipientProperty);

		// Token: 0x06000CA7 RID: 3239
		void Visit(MessageRecipientStatus recipientStatus);

		// Token: 0x06000CA8 RID: 3240
		void Visit(MessageRecipientStatusProperty recipientStatusProperty);

		// Token: 0x06000CA9 RID: 3241
		void Visit(MessageAction messageAction);

		// Token: 0x06000CAA RID: 3242
		void Visit(MessageActionProperty messageActionProperty);
	}
}
