using System;
using System.IO;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000065 RID: 101
	internal abstract class StreamOnDataStorage : Stream
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003B5 RID: 949
		public abstract DataStorage Storage { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003B6 RID: 950
		public abstract long Start { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003B7 RID: 951
		public abstract long End { get; }

		// Token: 0x060003B8 RID: 952 RVA: 0x00015D0F File Offset: 0x00013F0F
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
