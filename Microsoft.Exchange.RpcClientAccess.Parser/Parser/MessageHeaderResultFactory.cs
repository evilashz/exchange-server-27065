using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000FB RID: 251
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MessageHeaderResultFactory : StandardResultFactory
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x0000F71D File Offset: 0x0000D91D
		internal MessageHeaderResultFactory(RopId ropId) : base(ropId)
		{
		}

		// Token: 0x06000514 RID: 1300
		public abstract RecipientCollector CreateRecipientCollector(MessageHeader messageHeader, PropertyTag[] extraPropertyTags, Encoding string8Encoding);
	}
}
