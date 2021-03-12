using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008AA RID: 2218
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ConversationPropertyAggregationContext : PropertyAggregationContext
	{
		// Token: 0x060052E2 RID: 21218 RVA: 0x0015A307 File Offset: 0x00158507
		public ConversationPropertyAggregationContext(IList<IStorePropertyBag> sources, string clientInfoString) : base(sources)
		{
			this.clientInfoString = clientInfoString;
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x060052E3 RID: 21219 RVA: 0x0015A317 File Offset: 0x00158517
		public string ClientInfoString
		{
			get
			{
				return this.clientInfoString;
			}
		}

		// Token: 0x04002D37 RID: 11575
		private readonly string clientInfoString;
	}
}
