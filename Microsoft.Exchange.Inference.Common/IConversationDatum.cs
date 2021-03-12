using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000014 RID: 20
	internal interface IConversationDatum
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000080 RID: 128
		// (set) Token: 0x06000081 RID: 129
		IList<IMessageRecipient> Participants { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000082 RID: 130
		// (set) Token: 0x06000083 RID: 131
		string[] TopicWords { get; set; }
	}
}
