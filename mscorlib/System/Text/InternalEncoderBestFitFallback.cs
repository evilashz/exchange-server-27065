using System;

namespace System.Text
{
	// Token: 0x02000A3D RID: 2621
	[Serializable]
	internal class InternalEncoderBestFitFallback : EncoderFallback
	{
		// Token: 0x060066C0 RID: 26304 RVA: 0x0015A441 File Offset: 0x00158641
		internal InternalEncoderBestFitFallback(Encoding encoding)
		{
			this.encoding = encoding;
			this.bIsMicrosoftBestFitFallback = true;
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x0015A457 File Offset: 0x00158657
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalEncoderBestFitFallbackBuffer(this);
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x060066C2 RID: 26306 RVA: 0x0015A45F File Offset: 0x0015865F
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x0015A464 File Offset: 0x00158664
		public override bool Equals(object value)
		{
			InternalEncoderBestFitFallback internalEncoderBestFitFallback = value as InternalEncoderBestFitFallback;
			return internalEncoderBestFitFallback != null && this.encoding.CodePage == internalEncoderBestFitFallback.encoding.CodePage;
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x0015A495 File Offset: 0x00158695
		public override int GetHashCode()
		{
			return this.encoding.CodePage;
		}

		// Token: 0x04002DA0 RID: 11680
		internal Encoding encoding;

		// Token: 0x04002DA1 RID: 11681
		internal char[] arrayBestFit;
	}
}
