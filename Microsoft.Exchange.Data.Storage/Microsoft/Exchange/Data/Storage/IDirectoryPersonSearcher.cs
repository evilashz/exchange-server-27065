using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004DE RID: 1246
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IDirectoryPersonSearcher
	{
		// Token: 0x0600364D RID: 13901
		bool TryFind(ContactInfoForLinking contactInfo, out ContactInfoForLinkingFromDirectory matchingContactInfo);
	}
}
