using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000170 RID: 368
	internal interface ITextSink
	{
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000FF4 RID: 4084
		bool IsEnough { get; }

		// Token: 0x06000FF5 RID: 4085
		void Write(char[] buffer, int offset, int count);

		// Token: 0x06000FF6 RID: 4086
		void Write(int ucs32Char);
	}
}
