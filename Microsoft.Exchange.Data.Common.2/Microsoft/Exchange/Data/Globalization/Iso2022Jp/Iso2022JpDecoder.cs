using System;
using System.Text;

namespace Microsoft.Exchange.Data.Globalization.Iso2022Jp
{
	// Token: 0x0200012B RID: 299
	internal class Iso2022JpDecoder : Decoder
	{
		// Token: 0x06000BC9 RID: 3017 RVA: 0x0006AB78 File Offset: 0x00068D78
		internal Iso2022JpDecoder(Iso2022JpEncoding parent)
		{
			this.killSwitch = Iso2022JpEncoding.InternalReadKillSwitch();
			this.parent = parent;
			switch (this.killSwitch)
			{
			case Iso2022DecodingMode.Default:
				this.defaultDecoder = parent.DefaultEncoding.GetDecoder();
				break;
			case Iso2022DecodingMode.Override:
				this.subDecoders = DecodeToCp932.GetStandardOrder();
				this.cp932Decoder = Encoding.GetEncoding(932).GetDecoder();
				this.currentSubDecoder = -1;
				this.lastChanceDecoderIndex = DecodeToCp932.LastChanceDecoderIndex;
				this.leftovers = null;
				this.escape = new Escape();
				break;
			case Iso2022DecodingMode.Throw:
				break;
			default:
				throw new InvalidOperationException("Invalid Iso2022DecodingMode!");
			}
			this.sbSummary = new StringBuilder(32);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0006AC29 File Offset: 0x00068E29
		public unsafe override void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0006AC30 File Offset: 0x00068E30
		public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			switch (this.killSwitch)
			{
			case Iso2022DecodingMode.Default:
				this.defaultDecoder.Convert(bytes, byteIndex, byteCount, chars, charIndex, charCount, flush, out bytesUsed, out charsUsed, out completed);
				return;
			case Iso2022DecodingMode.Override:
			{
				bytesUsed = 0;
				charsUsed = 0;
				completed = false;
				int num = 0;
				int num2 = 0;
				bool flag = false;
				if (this.leftovers != null)
				{
					this.ConvertBuffer(this.leftovers, 0, this.leftovers.Length, chars, charIndex, charCount, flush, out num, out num2, out flag);
					if (!flag)
					{
						throw new ArgumentException("Caller did not provide enough space for previously unprocessed data!", "chars");
					}
					charIndex += num2;
					charCount -= num2;
					bytesUsed += num;
					this.leftovers = null;
				}
				if (byteCount == 0)
				{
					completed = true;
					return;
				}
				this.ConvertBuffer(bytes, byteIndex, byteCount, chars, charIndex, charCount, flush, out bytesUsed, out charsUsed, out completed);
				int num3 = byteCount - (bytesUsed - num);
				int num4 = byteIndex + (byteCount - num3);
				if (num3 > 0 && bytes[num4] != 0)
				{
					this.leftovers = new byte[num3];
					Array.Copy(bytes, num4, this.leftovers, 0, num3);
				}
				return;
			}
			case Iso2022DecodingMode.Throw:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0006AD44 File Offset: 0x00068F44
		private void ConvertBuffer(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			bytesUsed = 0;
			charsUsed = 0;
			completed = false;
			int num = 0;
			while (byteCount > 0)
			{
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6;
				this.GetNextRun(bytes, byteIndex, byteCount, out num2, out num6);
				if (num2 == 0 && bytes[byteIndex] == 0)
				{
					if (charCount > 0)
					{
						chars[charIndex] = ' ';
						charsUsed++;
						charIndex++;
						charCount--;
					}
					bytesUsed++;
					byteIndex++;
					byteCount--;
					if (byteCount == 0)
					{
						completed = true;
						return;
					}
				}
				else
				{
					byte[] array = new byte[num6];
					bool flag;
					this.subDecoders[this.currentSubDecoder].ConvertToCp932(bytes, byteIndex, num2, array, 0, num6, flush, this.escape, out num3, out num, out flag);
					bytesUsed += num3;
					byteIndex += num3;
					byteCount -= num3;
					if (num2 != num3)
					{
						string message = string.Format("{2}.GetNextRun input length {0} does not match ConvertToCp932 length {1}", num2, num3, this.subDecoders[this.currentSubDecoder].GetType());
						throw new InvalidOperationException(message);
					}
					if (num != num6)
					{
						string message2 = string.Format("{2}.GetNextRun output length {0} does not match ConvertToCp932 length {1}", num6, num, this.subDecoders[this.currentSubDecoder].GetType());
						throw new InvalidOperationException(message2);
					}
					bool flag2 = byteCount > 0 && bytes[byteIndex] == 0;
					if (num6 == 0)
					{
						if (byteCount <= 0 || flag2)
						{
							completed = true;
							return;
						}
					}
					else
					{
						int num7 = num6;
						int i = this.cp932Decoder.GetCharCount(array, 0, num6, flush);
						while (i > charCount)
						{
							num7--;
							i = this.cp932Decoder.GetCharCount(array, 0, num7, flush);
							flag = false;
						}
						this.cp932Decoder.Convert(array, 0, num, chars, charIndex, charCount, flush, out num4, out num5, out completed);
						charsUsed += num5;
						charIndex += num5;
						charCount -= num5;
						completed = (flag2 || (completed && flag));
					}
				}
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0006AF24 File Offset: 0x00069124
		private void GetNextRun(byte[] bytes, int byteIndex, int byteCount, out int bytesConsumed, out int cp932ByteCount)
		{
			bytesConsumed = 0;
			cp932ByteCount = 0;
			DecodeToCp932.MapEscape(bytes, byteIndex, byteCount, this.escape);
			if (this.escape.Sequence == EscapeSequence.Incomplete)
			{
				bytesConsumed += this.escape.BytesInCurrentBuffer;
				this.currentSubDecoder = this.lastChanceDecoderIndex;
				this.sbSummary.Append("|");
				return;
			}
			if (this.currentSubDecoder != -1 && !this.escape.IsValidEscapeSequence)
			{
				int num;
				int num2;
				ValidationResult runLength = this.subDecoders[this.currentSubDecoder].GetRunLength(bytes, byteIndex, byteCount, this.escape, out num, out num2);
				if (runLength == ValidationResult.Valid && num > 0)
				{
					cp932ByteCount = num2;
					bytesConsumed = num;
					this.sbSummary.AppendFormat("|{0}", bytesConsumed);
					return;
				}
			}
			this.runCount++;
			if (this.escape.IsValidEscapeSequence)
			{
				int i = 0;
				while (i < this.subDecoders.Length)
				{
					if (this.subDecoders[i].IsEscapeSequenceHandled(this.escape))
					{
						int num;
						int num2;
						ValidationResult runLength = this.subDecoders[i].GetRunLength(bytes, byteIndex, byteCount, this.escape, out num, out num2);
						if (runLength != ValidationResult.Valid)
						{
							string message = string.Format("{0}.IsEscapeSequenceCovered and GetRunLength must agree on validity", this.subDecoders[i].GetType().Name);
							throw new InvalidOperationException(message);
						}
						cp932ByteCount = num2;
						bytesConsumed = num;
						this.currentSubDecoder = i;
						this.sbSummary.AppendFormat("E{0}{1}", this.escape.Abbreviation, bytesConsumed);
						return;
					}
					else
					{
						i++;
					}
				}
			}
			else
			{
				for (int i = 0; i < this.subDecoders.Length; i++)
				{
					int num;
					int num2;
					ValidationResult runLength = this.subDecoders[i].GetRunLength(bytes, byteIndex, byteCount, this.escape, out num, out num2);
					if (runLength == ValidationResult.Valid && (num > 0 || (num < byteCount && bytes[byteIndex + num] == 0)))
					{
						this.currentSubDecoder = i;
						cp932ByteCount = num2;
						bytesConsumed = num;
						this.sbSummary.AppendFormat("{0}{1}", this.subDecoders[i].Abbreviation, bytesConsumed);
						return;
					}
				}
			}
			throw new InvalidOperationException("What happened to the last-chance decoder?");
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0006B136 File Offset: 0x00069336
		public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0006B13D File Offset: 0x0006933D
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(bytes, index, count, false);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0006B14C File Offset: 0x0006934C
		public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			switch (this.killSwitch)
			{
			case Iso2022DecodingMode.Default:
				return this.defaultDecoder.GetCharCount(bytes, index, count, flush);
			case Iso2022DecodingMode.Override:
			{
				char[] array = new char[this.parent.GetMaxCharCount(count)];
				int num;
				int result;
				bool flag;
				this.Convert(bytes, index, count, array, 0, array.Length, flush, out num, out result, out flag);
				this.Reset();
				return result;
			}
			case Iso2022DecodingMode.Throw:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0006B1C2 File Offset: 0x000693C2
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0006B1C9 File Offset: 0x000693C9
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0006B1DC File Offset: 0x000693DC
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
		{
			switch (this.killSwitch)
			{
			case Iso2022DecodingMode.Default:
				return this.defaultDecoder.GetChars(bytes, byteIndex, byteCount, chars, charIndex, flush);
			case Iso2022DecodingMode.Override:
			{
				int num;
				int result;
				bool flag;
				this.Convert(bytes, byteIndex, byteCount, chars, charIndex, chars.Length - charIndex, flush, out num, out result, out flag);
				if (!flag)
				{
					throw new ArgumentException("insufficient capacity", "chars");
				}
				return result;
			}
			case Iso2022DecodingMode.Throw:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0006B258 File Offset: 0x00069458
		public override void Reset()
		{
			switch (this.killSwitch)
			{
			case Iso2022DecodingMode.Default:
				this.defaultDecoder.Reset();
				return;
			case Iso2022DecodingMode.Override:
				foreach (DecodeToCp932 decodeToCp in this.subDecoders)
				{
					decodeToCp.Reset();
				}
				this.currentSubDecoder = -1;
				this.leftovers = null;
				this.escape.Reset();
				this.runCount = 0;
				this.sbSummary = new StringBuilder(32);
				return;
			case Iso2022DecodingMode.Throw:
				throw new NotImplementedException();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0006B2E4 File Offset: 0x000694E4
		public override string ToString()
		{
			return string.Format("{0} runs; {1}", this.runCount, this.sbSummary.ToString());
		}

		// Token: 0x04000E8B RID: 3723
		private const int SummarySize = 32;

		// Token: 0x04000E8C RID: 3724
		private Iso2022DecodingMode killSwitch;

		// Token: 0x04000E8D RID: 3725
		private Decoder defaultDecoder;

		// Token: 0x04000E8E RID: 3726
		private Decoder cp932Decoder;

		// Token: 0x04000E8F RID: 3727
		private DecodeToCp932[] subDecoders;

		// Token: 0x04000E90 RID: 3728
		private int currentSubDecoder;

		// Token: 0x04000E91 RID: 3729
		private readonly int lastChanceDecoderIndex;

		// Token: 0x04000E92 RID: 3730
		private Iso2022JpEncoding parent;

		// Token: 0x04000E93 RID: 3731
		private byte[] leftovers;

		// Token: 0x04000E94 RID: 3732
		private Escape escape;

		// Token: 0x04000E95 RID: 3733
		private int runCount;

		// Token: 0x04000E96 RID: 3734
		private StringBuilder sbSummary;
	}
}
