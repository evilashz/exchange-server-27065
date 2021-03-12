using System;

namespace Microsoft.Exchange.Data.Globalization.Iso2022Jp
{
	// Token: 0x02000127 RID: 295
	internal class DecodeUsAsciiToCp932 : DecodeToCp932
	{
		// Token: 0x06000BA0 RID: 2976 RVA: 0x0006A28E File Offset: 0x0006848E
		public override bool IsEscapeSequenceHandled(Escape escape)
		{
			return escape.Sequence == EscapeSequence.Iso646Irv;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0006A29C File Offset: 0x0006849C
		public override ValidationResult GetRunLength(byte[] dataIn, int offsetIn, int lengthIn, Escape escape, out int usedIn, out int usedOut)
		{
			usedIn = 0;
			usedOut = 0;
			int i = offsetIn;
			int num = 0;
			bool flag = false;
			bool isValidEscapeSequence = escape.IsValidEscapeSequence;
			int num2 = 0;
			int limit = this.CalculateLoopCountLimit(lengthIn);
			if (isValidEscapeSequence)
			{
				if (!this.IsEscapeSequenceHandled(escape))
				{
					throw new InvalidOperationException(string.Format("unhandled escape sequence: {0}", escape.Sequence));
				}
				i += escape.BytesInCurrentBuffer;
			}
			while (i < offsetIn + lengthIn)
			{
				this.CheckLoopCount(ref num2, limit);
				byte b = dataIn[i];
				if (b == 27 || b == 15 || b == 14 || (b > 127 && !isValidEscapeSequence) || b == 0)
				{
					break;
				}
				if ((b < 32 || b > 127) && b != 9 && b != 10 && b != 11 && b != 12 && b != 13)
				{
					flag = true;
				}
				i++;
				num++;
			}
			usedIn = i - offsetIn;
			usedOut = num;
			if (!flag || isValidEscapeSequence)
			{
				return ValidationResult.Valid;
			}
			return ValidationResult.Invalid;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0006A380 File Offset: 0x00068580
		public override void ConvertToCp932(byte[] dataIn, int offsetIn, int lengthIn, byte[] dataOut, int offsetOut, int lengthOut, bool flush, Escape escape, out int usedIn, out int usedOut, out bool complete)
		{
			usedIn = 0;
			usedOut = 0;
			int i = offsetIn;
			int num = offsetOut;
			int num2 = 0;
			int limit = this.CalculateLoopCountLimit(lengthIn);
			if (escape.IsValidEscapeSequence)
			{
				if (!this.IsEscapeSequenceHandled(escape))
				{
					throw new InvalidOperationException(string.Format("unhandled escape sequence: {0}", escape.Sequence));
				}
				i += escape.BytesInCurrentBuffer;
			}
			while (i < offsetIn + lengthIn)
			{
				this.CheckLoopCount(ref num2, limit);
				byte b = dataIn[i];
				if (b == 27 || b == 15 || b == 14 || b == 0)
				{
					break;
				}
				if (num + 1 > offsetOut + lengthOut)
				{
					string message = string.Format("DecodeUsAsciiToCp932.ConvertToCp932: output buffer overrun, offset {0}, length {1}", offsetOut, lengthOut);
					throw new InvalidOperationException(message);
				}
				dataOut[num++] = dataIn[i++];
			}
			complete = (i == offsetIn + lengthIn);
			usedIn = i - offsetIn;
			usedOut = num - offsetOut;
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0006A45D File Offset: 0x0006865D
		public override char Abbreviation
		{
			get
			{
				return 'a';
			}
		}
	}
}
