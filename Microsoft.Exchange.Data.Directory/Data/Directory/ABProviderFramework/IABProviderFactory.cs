using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IABProviderFactory
	{
		// Token: 0x0600009C RID: 156
		ABSession Create(IABSessionSettings addressBookSessionSettings);

		// Token: 0x0600009D RID: 157
		ABProviderCapabilities GetProviderCapabilities(IABSessionSettings addressBookSessionSettings);
	}
}
