using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.IsMemberOfProvider
{
	// Token: 0x020001C4 RID: 452
	internal interface IIsMemberOfResolver<in TGroupKeyType> : IDisposable
	{
		// Token: 0x06001277 RID: 4727
		void ClearCache();

		// Token: 0x06001278 RID: 4728
		bool IsMemberOf(IRecipientSession session, ADObjectId recipientId, TGroupKeyType groupKey);

		// Token: 0x06001279 RID: 4729
		bool IsMemberOf(IRecipientSession session, Guid recipientObjectGuid, TGroupKeyType groupKey);
	}
}
