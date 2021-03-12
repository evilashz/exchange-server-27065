using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000017 RID: 23
	internal interface ISimpleBufferPool : IPool<SimpleBuffer>
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A3 RID: 163
		int BufferSize { get; }

		// Token: 0x060000A4 RID: 164
		SimpleBuffer TryGetObject(int bufSize);
	}
}
