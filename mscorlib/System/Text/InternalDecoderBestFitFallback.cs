using System;

namespace System.Text
{
	// Token: 0x02000A32 RID: 2610
	[Serializable]
	internal sealed class InternalDecoderBestFitFallback : DecoderFallback
	{
		// Token: 0x06006664 RID: 26212 RVA: 0x0015919D File Offset: 0x0015739D
		internal InternalDecoderBestFitFallback(Encoding encoding)
		{
			this.encoding = encoding;
			this.bIsMicrosoftBestFitFallback = true;
		}

		// Token: 0x06006665 RID: 26213 RVA: 0x001591BB File Offset: 0x001573BB
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalDecoderBestFitFallbackBuffer(this);
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06006666 RID: 26214 RVA: 0x001591C3 File Offset: 0x001573C3
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06006667 RID: 26215 RVA: 0x001591C8 File Offset: 0x001573C8
		public override bool Equals(object value)
		{
			InternalDecoderBestFitFallback internalDecoderBestFitFallback = value as InternalDecoderBestFitFallback;
			return internalDecoderBestFitFallback != null && this.encoding.CodePage == internalDecoderBestFitFallback.encoding.CodePage;
		}

		// Token: 0x06006668 RID: 26216 RVA: 0x001591F9 File Offset: 0x001573F9
		public override int GetHashCode()
		{
			return this.encoding.CodePage;
		}

		// Token: 0x04002D85 RID: 11653
		internal Encoding encoding;

		// Token: 0x04002D86 RID: 11654
		internal char[] arrayBestFit;

		// Token: 0x04002D87 RID: 11655
		internal char cReplacement = '?';
	}
}
