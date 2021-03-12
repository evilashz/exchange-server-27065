using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004F3 RID: 1267
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IOscContactSourcesForContactParser
	{
		// Token: 0x060036F6 RID: 14070
		OscNetworkProperties ReadOscContactSource(byte[] property);
	}
}
