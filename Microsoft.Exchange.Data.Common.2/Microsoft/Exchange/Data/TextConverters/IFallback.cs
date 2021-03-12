using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200017C RID: 380
	internal interface IFallback
	{
		// Token: 0x0600105A RID: 4186
		byte[] GetUnsafeAsciiMap(out byte unsafeAsciiMask);

		// Token: 0x0600105B RID: 4187
		bool HasUnsafeUnicode();

		// Token: 0x0600105C RID: 4188
		bool TreatNonAsciiAsUnsafe(string charset);

		// Token: 0x0600105D RID: 4189
		bool IsUnsafeUnicode(char ch, bool isFirstChar);

		// Token: 0x0600105E RID: 4190
		bool FallBackChar(char ch, char[] outputBuffer, ref int outputBufferCount, int lineBufferEnd);
	}
}
