using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x0200001A RID: 26
	internal class PeopleRelevanceSchema : DocumentSchema
	{
		// Token: 0x04000053 RID: 83
		public static readonly PropertyDefinition ContactList = new SimplePropertyDefinition("ContactList", typeof(IDictionary<string, IInferenceRecipient>), PropertyFlag.None);

		// Token: 0x04000054 RID: 84
		public static readonly PropertyDefinition SentItemsTrainingSubDocument = new SimplePropertyDefinition("SentItemsTrainingSubDocument", typeof(IDocument), PropertyFlag.None);

		// Token: 0x04000055 RID: 85
		public static readonly PropertyDefinition CurrentTimeWindowStartTime = new SimplePropertyDefinition("CurrentTimeWindowStartTime", typeof(ExDateTime), PropertyFlag.None);

		// Token: 0x04000056 RID: 86
		public static readonly PropertyDefinition CurrentTimeWindowNumber = new SimplePropertyDefinition("CurrentTimeWindowNumber", typeof(long), PropertyFlag.None);

		// Token: 0x04000057 RID: 87
		public static readonly PropertyDefinition TimeWindowNumberAtLastRun = new SimplePropertyDefinition("TimeWindowNumberAtLastRun", typeof(long), PropertyFlag.None);

		// Token: 0x04000058 RID: 88
		public static readonly PropertyDefinition TopRankedContacts = new SimplePropertyDefinition("TopRankedContacts", typeof(List<string>), PropertyFlag.None);

		// Token: 0x04000059 RID: 89
		public static readonly PropertyDefinition PeopleModelVersion = new SimplePropertyDefinition("PeopleModelVersion", typeof(Version), PropertyFlag.None);

		// Token: 0x0400005A RID: 90
		public static readonly PropertyDefinition LastRecipientCacheValidationTime = new SimplePropertyDefinition("LastRecipientCacheValidationTime", typeof(ExDateTime), PropertyFlag.None);

		// Token: 0x0400005B RID: 91
		public static readonly PropertyDefinition MailboxOwner = new SimplePropertyDefinition("MailboxOwner", typeof(IMessageRecipient), PropertyFlag.None);

		// Token: 0x0400005C RID: 92
		public static readonly PropertyDefinition MailboxAlias = new SimplePropertyDefinition("MailboxAlias", typeof(string), PropertyFlag.None);

		// Token: 0x0400005D RID: 93
		public static readonly PropertyDefinition SentTime = new SimplePropertyDefinition("SentTime", typeof(ExDateTime), PropertyFlag.None);

		// Token: 0x0400005E RID: 94
		public static readonly PropertyDefinition RecipientsTo = new SimplePropertyDefinition("RecipientsTo", typeof(IList<IMessageRecipient>), PropertyFlag.None);

		// Token: 0x0400005F RID: 95
		public static readonly PropertyDefinition RecipientsCc = new SimplePropertyDefinition("RecipientsCc", typeof(IList<IMessageRecipient>), PropertyFlag.None);

		// Token: 0x04000060 RID: 96
		public static readonly PropertyDefinition IsReply = new SimplePropertyDefinition("IsReply", typeof(bool), PropertyFlag.None);

		// Token: 0x04000061 RID: 97
		public static readonly PropertyDefinition IsBasedOnRecipientInfoData = new SimplePropertyDefinition("IsBasedOnRecipientInfoData", typeof(bool), PropertyFlag.None);

		// Token: 0x04000062 RID: 98
		public static readonly PropertyDefinition RecipientInfoEnumerable = new SimplePropertyDefinition("RecipientInfoEnumerable", typeof(IEnumerable<IRecipientInfo>), PropertyFlag.None);

		// Token: 0x04000063 RID: 99
		public static readonly PropertyDefinition LastProcessedMessageSentTime = new SimplePropertyDefinition("LastProcessedMessageSentTime", typeof(ExDateTime), PropertyFlag.None);
	}
}
