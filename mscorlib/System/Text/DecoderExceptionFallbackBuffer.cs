using System;
using System.Globalization;

namespace System.Text
{
	// Token: 0x02000A35 RID: 2613
	public sealed class DecoderExceptionFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x06006677 RID: 26231 RVA: 0x0015949D File Offset: 0x0015769D
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this.Throw(bytesUnknown, index);
			return true;
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x001594A8 File Offset: 0x001576A8
		public override char GetNextChar()
		{
			return '\0';
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x001594AB File Offset: 0x001576AB
		public override bool MovePrevious()
		{
			return false;
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x0600667A RID: 26234 RVA: 0x001594AE File Offset: 0x001576AE
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600667B RID: 26235 RVA: 0x001594B4 File Offset: 0x001576B4
		private void Throw(byte[] bytesUnknown, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				stringBuilder.Append("[");
				stringBuilder.Append(bytesUnknown[num].ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append("]");
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new DecoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageBytesIndex", new object[]
			{
				stringBuilder,
				index
			}), bytesUnknown, index);
		}
	}
}
