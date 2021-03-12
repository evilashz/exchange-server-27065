using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000043 RID: 67
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NoopMruDictionaryPerfCounters : IMruDictionaryPerfCounters
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00008F80 File Offset: 0x00007180
		private NoopMruDictionaryPerfCounters()
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008F88 File Offset: 0x00007188
		public void CacheHit()
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008F8A File Offset: 0x0000718A
		public void CacheMiss()
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008F8C File Offset: 0x0000718C
		public void CacheAdd(bool overwrite, bool remove)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008F8E File Offset: 0x0000718E
		public void CacheRemove()
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008F90 File Offset: 0x00007190
		public void FileRead(int count)
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008F92 File Offset: 0x00007192
		public void FileWrite(int count)
		{
		}

		// Token: 0x04000135 RID: 309
		public static NoopMruDictionaryPerfCounters Instance = new NoopMruDictionaryPerfCounters();
	}
}
