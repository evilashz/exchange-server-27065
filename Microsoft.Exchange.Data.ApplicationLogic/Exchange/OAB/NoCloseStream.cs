using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x0200014D RID: 333
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NoCloseStream : BaseStream
	{
		// Token: 0x06000D72 RID: 3442 RVA: 0x0003886B File Offset: 0x00036A6B
		public NoCloseStream(Stream stream) : base(stream)
		{
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00038874 File Offset: 0x00036A74
		public override void Close()
		{
		}
	}
}
