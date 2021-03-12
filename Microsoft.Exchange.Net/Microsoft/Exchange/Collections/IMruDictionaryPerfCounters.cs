using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000042 RID: 66
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMruDictionaryPerfCounters
	{
		// Token: 0x060001BD RID: 445
		void CacheHit();

		// Token: 0x060001BE RID: 446
		void CacheMiss();

		// Token: 0x060001BF RID: 447
		void CacheAdd(bool overwrite, bool remove);

		// Token: 0x060001C0 RID: 448
		void CacheRemove();

		// Token: 0x060001C1 RID: 449
		void FileRead(int count);

		// Token: 0x060001C2 RID: 450
		void FileWrite(int count);
	}
}
