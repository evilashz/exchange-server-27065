using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000897 RID: 2199
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IReplyAllExtractor
	{
		// Token: 0x06005231 RID: 21041
		bool TryRetrieveReplyAllDisplayNames(IStorePropertyBag propertyBag, out HashSet<string> displayNames);

		// Token: 0x06005232 RID: 21042
		HashSet<string> RetrieveReplyAllDisplayNames(ICorePropertyBag propertyBag);

		// Token: 0x06005233 RID: 21043
		ParticipantSet RetrieveReplyAllParticipants(ICorePropertyBag propertyBag);

		// Token: 0x06005234 RID: 21044
		ParticipantSet RetrieveReplyAllParticipants(IStorePropertyBag propertyBag);
	}
}
