using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200089C RID: 2204
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ConversationAggregationExtension : AggregationExtension
	{
		// Token: 0x0600526E RID: 21102 RVA: 0x00158D22 File Offset: 0x00156F22
		public ConversationAggregationExtension(MailboxSession mailboxSession)
		{
			this.clientInfoString = mailboxSession.ClientInfoString;
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x00158D36 File Offset: 0x00156F36
		public override PropertyAggregationContext GetPropertyAggregationContext(IList<IStorePropertyBag> sources)
		{
			return new ConversationPropertyAggregationContext(sources, this.clientInfoString);
		}

		// Token: 0x04002CDE RID: 11486
		private readonly string clientInfoString;
	}
}
