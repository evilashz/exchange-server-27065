using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000171 RID: 369
	internal interface ITextSinkEx : ITextSink
	{
		// Token: 0x06000FF7 RID: 4087
		void Write(string value);

		// Token: 0x06000FF8 RID: 4088
		void WriteNewLine();
	}
}
