using System;

namespace System.Text
{
	// Token: 0x02000A40 RID: 2624
	public sealed class EncoderExceptionFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x060066D4 RID: 26324 RVA: 0x0015A791 File Offset: 0x00158991
		public override bool Fallback(char charUnknown, int index)
		{
			throw new EncoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageConversionIndex", new object[]
			{
				(int)charUnknown,
				index
			}), charUnknown, index);
		}

		// Token: 0x060066D5 RID: 26325 RVA: 0x0015A7BC File Offset: 0x001589BC
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					55296,
					56319
				}));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					56320,
					57343
				}));
			}
			int num = char.ConvertToUtf32(charUnknownHigh, charUnknownLow);
			throw new EncoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageConversionIndex", new object[]
			{
				num,
				index
			}), charUnknownHigh, charUnknownLow, index);
		}

		// Token: 0x060066D6 RID: 26326 RVA: 0x0015A875 File Offset: 0x00158A75
		public override char GetNextChar()
		{
			return '\0';
		}

		// Token: 0x060066D7 RID: 26327 RVA: 0x0015A878 File Offset: 0x00158A78
		public override bool MovePrevious()
		{
			return false;
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x060066D8 RID: 26328 RVA: 0x0015A87B File Offset: 0x00158A7B
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}
	}
}
