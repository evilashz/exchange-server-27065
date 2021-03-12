using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x0200009A RID: 154
	internal interface IJetRecordCounter
	{
		// Token: 0x06000670 RID: 1648
		int GetCount();

		// Token: 0x06000671 RID: 1649
		int GetOrdinalPosition(SortOrder sortOrder, StartStopKey stopKey, CompareInfo compareInfo);
	}
}
