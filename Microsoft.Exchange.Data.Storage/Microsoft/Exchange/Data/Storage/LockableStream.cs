using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AF0 RID: 2800
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class LockableStream : Stream
	{
		// Token: 0x060065AF RID: 26031 RVA: 0x001B065B File Offset: 0x001AE85B
		public virtual void LockRegion(long offset, long cb, int lockType)
		{
		}

		// Token: 0x060065B0 RID: 26032 RVA: 0x001B065D File Offset: 0x001AE85D
		public virtual void UnlockRegion(long offset, long cb, int lockType)
		{
		}
	}
}
