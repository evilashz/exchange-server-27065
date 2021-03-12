using System;

namespace Microsoft.Exchange.Data.Globalization.Iso2022Jp
{
	// Token: 0x02000128 RID: 296
	internal class DecodeLastChanceToCp932 : DecodeToCp932
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x0006A469 File Offset: 0x00068669
		public override bool IsEscapeSequenceHandled(Escape escape)
		{
			return true;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0006A46C File Offset: 0x0006866C
		public override ValidationResult GetRunLength(byte[] dataIn, int offsetIn, int lengthIn, Escape escape, out int usedIn, out int usedOut)
		{
			usedIn = 0;
			usedOut = 0;
			int i = offsetIn;
			int num = 0;
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
			else if (escape.Sequence == EscapeSequence.NotRecognized)
			{
				i += escape.BytesInCurrentBuffer;
				num += escape.BytesInCurrentBuffer;
			}
			while (i < offsetIn + lengthIn)
			{
				this.CheckLoopCount(ref num2, limit);
				byte b = dataIn[i];
				if (b == 27 || b == 15 || b == 14 || b == 0)
				{
					break;
				}
				if (b == 160)
				{
					i++;
					num++;
				}
				else
				{
					if ((b >= 129 && b <= 159) || (b >= 224 && b <= 252))
					{
						i++;
						num++;
						if (i >= offsetIn + lengthIn || dataIn[i] == 0)
						{
							break;
						}
					}
					i++;
					num++;
				}
			}
			usedIn = i - offsetIn;
			usedOut = num;
			return ValidationResult.Valid;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0006A578 File Offset: 0x00068778
		public override void ConvertToCp932(byte[] dataIn, int offsetIn, int lengthIn, byte[] dataOut, int offsetOut, int lengthOut, bool flush, Escape escape, out int usedIn, out int usedOut, out bool complete)
		{
			usedIn = 0;
			usedOut = 0;
			complete = false;
			int i = offsetIn;
			int num = offsetOut;
			int num2 = 0;
			int num3 = 0;
			int limit = this.CalculateLoopCountLimit(lengthIn);
			if (escape.IsValidEscapeSequence)
			{
				if (!this.IsEscapeSequenceHandled(escape))
				{
					throw new InvalidOperationException(string.Format("unhandled escape sequence: {0}", escape.Sequence));
				}
				i += escape.BytesInCurrentBuffer;
			}
			else if (escape.Sequence == EscapeSequence.NotRecognized)
			{
				int num4 = 0;
				while (num4 < escape.BytesInCurrentBuffer && num4 < lengthIn)
				{
					if (num < offsetOut + lengthOut)
					{
						dataOut[num++] = dataIn[i + num4];
						num2++;
					}
					num4++;
				}
				i += escape.BytesInCurrentBuffer;
			}
			else if (escape.Sequence == EscapeSequence.Incomplete)
			{
				i += escape.BytesInCurrentBuffer;
			}
			while (i < offsetIn + lengthIn)
			{
				this.CheckLoopCount(ref num3, limit);
				byte b = dataIn[i];
				if (b == 27 || b == 15 || b == 14 || b == 0)
				{
					break;
				}
				if (b == 160)
				{
					if (num + 1 > offsetOut + lengthOut)
					{
						break;
					}
					dataOut[num++] = 32;
					i++;
					num2++;
				}
				else if (((b >= 129 && b <= 159) || (b >= 224 && b <= 252)) && i + 2 <= offsetIn + lengthIn && num + 2 <= offsetOut + lengthOut)
				{
					dataOut[num++] = dataIn[i++];
					num2++;
					byte b2 = dataIn[i];
					if (b2 == 0)
					{
						break;
					}
					dataOut[num++] = dataIn[i++];
					num2++;
				}
				else
				{
					if (num + 1 > offsetOut + lengthOut)
					{
						break;
					}
					dataOut[num++] = dataIn[i++];
					num2++;
				}
			}
			usedIn = i - offsetIn;
			usedOut = num2;
			complete = (i == offsetIn + lengthIn);
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0006A749 File Offset: 0x00068949
		public override char Abbreviation
		{
			get
			{
				return 'L';
			}
		}
	}
}
