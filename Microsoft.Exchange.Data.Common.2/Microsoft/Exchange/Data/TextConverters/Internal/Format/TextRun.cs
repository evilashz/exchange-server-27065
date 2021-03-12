using System;
using System.Text;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002A5 RID: 677
	internal struct TextRun
	{
		// Token: 0x06001B12 RID: 6930 RVA: 0x000D1EAD File Offset: 0x000D00AD
		internal TextRun(FormatStore.TextStore text, uint position)
		{
			this.isImmutable = false;
			this.text = text;
			this.position = position;
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x000D1EC4 File Offset: 0x000D00C4
		public uint Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x000D1ECC File Offset: 0x000D00CC
		public TextRunType Type
		{
			get
			{
				return FormatStore.TextStore.TypeFromRunHeader(this.text.Pick(this.position));
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x000D1EE4 File Offset: 0x000D00E4
		public int EffectiveLength
		{
			get
			{
				return FormatStore.TextStore.LengthFromRunHeader(this.text.Pick(this.position));
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x000D1EFC File Offset: 0x000D00FC
		public int Length
		{
			get
			{
				char c = this.text.Pick(this.position);
				if (c < '\u3000')
				{
					return FormatStore.TextStore.LengthFromRunHeader(c) + 1;
				}
				return 1;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x000D1F30 File Offset: 0x000D0130
		public int WordLength
		{
			get
			{
				int num = 0;
				TextRun textRun = this;
				while (!textRun.IsEnd() && textRun.Type == TextRunType.NonSpace && num < 1024)
				{
					num += textRun.EffectiveLength;
					textRun = textRun.GetNext();
				}
				return num;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x000D1F7A File Offset: 0x000D017A
		private bool IsLong
		{
			get
			{
				return this.Type < TextRunType.FirstShort;
			}
		}

		// Token: 0x17000712 RID: 1810
		public char this[int index]
		{
			get
			{
				return this.text.Plane(this.position)[this.text.Index(this.position) + 1 + index];
			}
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x000D1FB4 File Offset: 0x000D01B4
		public char GetWordChar(int index)
		{
			int effectiveLength = this.EffectiveLength;
			if (index < effectiveLength)
			{
				return this[index];
			}
			TextRun next = this.GetNext();
			index -= effectiveLength;
			while (!next.IsEnd())
			{
				effectiveLength = next.EffectiveLength;
				if (index < effectiveLength)
				{
					return next[index];
				}
				if (next.Type != TextRunType.NonSpace)
				{
					break;
				}
				index -= effectiveLength;
				next = next.GetNext();
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000D2025 File Offset: 0x000D0225
		public void MoveNext()
		{
			if (this.isImmutable)
			{
				throw new InvalidOperationException("This run is immutable");
			}
			this.position += (uint)this.Length;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000D204D File Offset: 0x000D024D
		public void SkipInvalid()
		{
			if (this.isImmutable)
			{
				throw new InvalidOperationException("This run is immutable");
			}
			while (!this.IsEnd() && this.Type == TextRunType.Invalid)
			{
				this.MoveNext();
			}
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000D2078 File Offset: 0x000D0278
		public bool IsEnd()
		{
			return this.position >= this.text.CurrentPosition;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000D2090 File Offset: 0x000D0290
		public TextRun GetNext()
		{
			return new TextRun(this.text, this.position + (uint)this.Length);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000D20AA File Offset: 0x000D02AA
		public void GetChunk(int start, out char[] buffer, out int offset, out int count)
		{
			buffer = this.text.Plane(this.position);
			offset = this.text.Index(this.position) + 1 + start;
			count = this.EffectiveLength - start;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000D20E4 File Offset: 0x000D02E4
		public int AppendFragment(int start, ref ScratchBuffer scratchBuffer, int maxLength)
		{
			int offset = this.text.Index(this.position) + 1 + start;
			int num = Math.Min(this.EffectiveLength - start, maxLength);
			if (num != 0)
			{
				scratchBuffer.Append(this.text.Plane(this.position), offset, num);
			}
			return num;
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000D2134 File Offset: 0x000D0334
		public void ConvertToInvalid()
		{
			this.text.ConvertToInvalid(this.position);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000D2147 File Offset: 0x000D0347
		public void ConvertToInvalid(int count)
		{
			this.text.ConvertToInvalid(this.position, count);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000D215B File Offset: 0x000D035B
		public void ConvertShort(TextRunType type, int newEffectiveLength)
		{
			this.text.ConvertShortRun(this.position, type, newEffectiveLength);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000D2170 File Offset: 0x000D0370
		public override string ToString()
		{
			int wordLength = this.WordLength;
			StringBuilder stringBuilder = new StringBuilder(wordLength);
			for (int i = 0; i < wordLength; i++)
			{
				stringBuilder.Append(this.GetWordChar(i));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000D21AB File Offset: 0x000D03AB
		internal void MakeImmutable()
		{
			this.isImmutable = true;
		}

		// Token: 0x040020A8 RID: 8360
		public const int MaxEffectiveLength = 4095;

		// Token: 0x040020A9 RID: 8361
		public static readonly TextRun Invalid = default(TextRun);

		// Token: 0x040020AA RID: 8362
		private FormatStore.TextStore text;

		// Token: 0x040020AB RID: 8363
		private uint position;

		// Token: 0x040020AC RID: 8364
		private bool isImmutable;
	}
}
