using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000696 RID: 1686
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ITestBridgeDelegateSessionCache
	{
		// Token: 0x060044FD RID: 17661
		int GetSize(int currentSize);

		// Token: 0x060044FE RID: 17662
		void Removed(string mailboxLegacyDn);

		// Token: 0x060044FF RID: 17663
		void Added(string mailboxLegacyDn);
	}
}
