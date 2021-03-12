using System;
using System.Diagnostics;
using System.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000190 RID: 400
	internal class Token
	{
		// Token: 0x06001111 RID: 4369 RVA: 0x0007B972 File Offset: 0x00079B72
		public Token()
		{
			this.Reset();
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0007B980 File Offset: 0x00079B80
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x0007B988 File Offset: 0x00079B88
		public TokenId TokenId
		{
			get
			{
				return this.tokenId;
			}
			set
			{
				this.tokenId = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0007B991 File Offset: 0x00079B91
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x0007B999 File Offset: 0x00079B99
		public int Argument
		{
			get
			{
				return this.argument;
			}
			set
			{
				this.argument = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0007B9A2 File Offset: 0x00079BA2
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x0007B9AA File Offset: 0x00079BAA
		public Encoding TokenEncoding
		{
			get
			{
				return this.tokenEncoding;
			}
			set
			{
				this.tokenEncoding = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x0007B9B3 File Offset: 0x00079BB3
		public bool IsEmpty
		{
			get
			{
				return this.Whole.Tail == this.Whole.Head;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0007B9CD File Offset: 0x00079BCD
		public Token.RunEnumerator Runs
		{
			get
			{
				return new Token.RunEnumerator(this);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x0007B9D5 File Offset: 0x00079BD5
		public Token.TextReader Text
		{
			get
			{
				return new Token.TextReader(this);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0007B9DD File Offset: 0x00079BDD
		public bool IsWhitespaceOnly
		{
			get
			{
				return this.IsWhitespaceOnlyImp(ref this.Whole);
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0007B9EB File Offset: 0x00079BEB
		internal static int LiteralLength(int literal)
		{
			if (literal <= 65535)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0007B9F8 File Offset: 0x00079BF8
		internal static char LiteralFirstChar(int literal)
		{
			if (literal <= 65535)
			{
				return (char)literal;
			}
			return ParseSupport.HighSurrogateCharFromUcs4(literal);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0007BA0B File Offset: 0x00079C0B
		internal static char LiteralLastChar(int literal)
		{
			if (literal <= 65535)
			{
				return (char)literal;
			}
			return ParseSupport.LowSurrogateCharFromUcs4(literal);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0007BA20 File Offset: 0x00079C20
		protected internal bool IsWhitespaceOnlyImp(ref Token.Fragment fragment)
		{
			bool result = true;
			for (int num = fragment.Head; num != fragment.Tail; num++)
			{
				if (this.RunList[num].Type >= (RunType)2147483648U && this.RunList[num].TextType > RunTextType.UnusualWhitespace)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0007BA7C File Offset: 0x00079C7C
		protected internal int Read(ref Token.Fragment fragment, ref Token.FragmentPosition position, char[] buffer, int offset, int count)
		{
			int num = offset;
			int num2 = position.Run;
			if (num2 == fragment.Head - 1)
			{
				num2 = (position.Run = fragment.Head);
			}
			if (num2 != fragment.Tail)
			{
				int num3 = position.RunOffset;
				int num4 = position.RunDeltaOffset;
				int num6;
				for (;;)
				{
					Token.RunEntry runEntry = this.RunList[num2];
					if (runEntry.Type == (RunType)3221225472U)
					{
						int num5 = Token.LiteralLength(runEntry.Value);
						if (num4 != num5)
						{
							if (num5 == 1)
							{
								buffer[offset++] = (char)runEntry.Value;
								count--;
							}
							else if (num4 != 0)
							{
								buffer[offset++] = Token.LiteralLastChar(runEntry.Value);
								count--;
							}
							else
							{
								buffer[offset++] = Token.LiteralFirstChar(runEntry.Value);
								count--;
								if (count == 0)
								{
									break;
								}
								buffer[offset++] = Token.LiteralLastChar(runEntry.Value);
								count--;
							}
						}
					}
					else if (runEntry.Type == (RunType)2147483648U)
					{
						num6 = Math.Min(count, runEntry.Length - num4);
						System.Buffer.BlockCopy(this.Buffer, (num3 + num4) * 2, buffer, offset * 2, num6 * 2);
						offset += num6;
						count -= num6;
						if (num4 + num6 != runEntry.Length)
						{
							goto Block_9;
						}
					}
					num3 += runEntry.Length;
					num4 = 0;
					if (++num2 == fragment.Tail || count == 0)
					{
						goto IL_17D;
					}
				}
				num4 = 1;
				goto IL_17D;
				Block_9:
				num4 += num6;
				IL_17D:
				position.Run = num2;
				position.RunOffset = num3;
				position.RunDeltaOffset = num4;
			}
			return offset - num;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0007BC20 File Offset: 0x00079E20
		protected internal int ReadOriginal(ref Token.Fragment fragment, ref Token.FragmentPosition position, char[] buffer, int offset, int count)
		{
			int num = offset;
			int num2 = position.Run;
			if (num2 == fragment.Head - 1)
			{
				num2 = (position.Run = fragment.Head);
			}
			if (num2 != fragment.Tail)
			{
				int num3 = position.RunOffset;
				int num4 = position.RunDeltaOffset;
				int num5;
				for (;;)
				{
					Token.RunEntry runEntry = this.RunList[num2];
					if (runEntry.Type == (RunType)3221225472U || runEntry.Type == (RunType)2147483648U)
					{
						num5 = Math.Min(count, runEntry.Length - num4);
						System.Buffer.BlockCopy(this.Buffer, (num3 + num4) * 2, buffer, offset * 2, num5 * 2);
						offset += num5;
						count -= num5;
						if (num4 + num5 != runEntry.Length)
						{
							break;
						}
					}
					num3 += runEntry.Length;
					num4 = 0;
					if (++num2 == fragment.Tail || count == 0)
					{
						goto IL_DD;
					}
				}
				num4 += num5;
				IL_DD:
				position.Run = num2;
				position.RunOffset = num3;
				position.RunDeltaOffset = num4;
			}
			return offset - num;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0007BD24 File Offset: 0x00079F24
		protected internal int Read(Token.LexicalUnit unit, ref Token.FragmentPosition position, char[] buffer, int offset, int count)
		{
			int num = offset;
			if (unit.Head != -1)
			{
				uint majorKind = this.RunList[unit.Head].MajorKind;
				int num2 = position.Run;
				if (num2 == unit.Head - 1)
				{
					num2 = (position.Run = unit.Head);
				}
				Token.RunEntry runEntry = this.RunList[num2];
				if (num2 == unit.Head || runEntry.MajorKindPlusStartFlag == majorKind)
				{
					int num3 = position.RunOffset;
					int num4 = position.RunDeltaOffset;
					int num6;
					for (;;)
					{
						if (runEntry.Type == (RunType)3221225472U)
						{
							int num5 = Token.LiteralLength(runEntry.Value);
							if (num4 != num5)
							{
								if (num5 == 1)
								{
									buffer[offset++] = (char)runEntry.Value;
									count--;
								}
								else if (num4 != 0)
								{
									buffer[offset++] = Token.LiteralLastChar(runEntry.Value);
									count--;
								}
								else
								{
									buffer[offset++] = Token.LiteralFirstChar(runEntry.Value);
									count--;
									if (count == 0)
									{
										break;
									}
									buffer[offset++] = Token.LiteralLastChar(runEntry.Value);
									count--;
								}
							}
						}
						else if (runEntry.Type == (RunType)2147483648U)
						{
							num6 = Math.Min(count, runEntry.Length - num4);
							System.Buffer.BlockCopy(this.Buffer, (num3 + num4) * 2, buffer, offset * 2, num6 * 2);
							offset += num6;
							count -= num6;
							if (num4 + num6 != runEntry.Length)
							{
								goto Block_10;
							}
						}
						num3 += runEntry.Length;
						num4 = 0;
						runEntry = this.RunList[++num2];
						if (runEntry.MajorKindPlusStartFlag != majorKind || count == 0)
						{
							goto IL_1CF;
						}
					}
					num4 = 1;
					goto IL_1CF;
					Block_10:
					num4 += num6;
					IL_1CF:
					position.Run = num2;
					position.RunOffset = num3;
					position.RunDeltaOffset = num4;
				}
			}
			return offset - num;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0007BF1B File Offset: 0x0007A11B
		protected internal virtual void Rewind()
		{
			this.WholePosition.Rewind(this.Whole);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0007BF30 File Offset: 0x0007A130
		protected internal int GetLength(ref Token.Fragment fragment)
		{
			int num = fragment.Head;
			int num2 = 0;
			if (num != fragment.Tail)
			{
				do
				{
					Token.RunEntry runEntry = this.RunList[num];
					if (runEntry.Type == (RunType)2147483648U)
					{
						num2 += runEntry.Length;
					}
					else if (runEntry.Type == (RunType)3221225472U)
					{
						num2 += Token.LiteralLength(runEntry.Value);
					}
				}
				while (++num != fragment.Tail);
			}
			return num2;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0007BFA8 File Offset: 0x0007A1A8
		protected internal int GetOriginalLength(ref Token.Fragment fragment)
		{
			int num = fragment.Head;
			int num2 = 0;
			if (num != fragment.Tail)
			{
				do
				{
					Token.RunEntry runEntry = this.RunList[num];
					if (runEntry.Type == (RunType)2147483648U || runEntry.Type == (RunType)3221225472U)
					{
						num2 += runEntry.Length;
					}
				}
				while (++num != fragment.Tail);
			}
			return num2;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0007C010 File Offset: 0x0007A210
		protected internal int GetLength(Token.LexicalUnit unit)
		{
			int num = unit.Head;
			int num2 = 0;
			if (num != -1)
			{
				Token.RunEntry runEntry = this.RunList[num];
				uint majorKind = runEntry.MajorKind;
				do
				{
					if (runEntry.Type == (RunType)2147483648U)
					{
						num2 += runEntry.Length;
					}
					else if (runEntry.Type == (RunType)3221225472U)
					{
						num2 += Token.LiteralLength(runEntry.Value);
					}
					runEntry = this.RunList[++num];
				}
				while (runEntry.MajorKindPlusStartFlag == majorKind);
			}
			return num2;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0007C0A0 File Offset: 0x0007A2A0
		protected internal bool IsFragmentEmpty(ref Token.Fragment fragment)
		{
			int num = fragment.Head;
			if (num != fragment.Tail)
			{
				for (;;)
				{
					Token.RunEntry runEntry = this.RunList[num];
					if (runEntry.Type == (RunType)2147483648U || runEntry.Type == (RunType)3221225472U)
					{
						break;
					}
					if (++num == fragment.Tail)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0007C0FC File Offset: 0x0007A2FC
		protected internal bool IsFragmentEmpty(Token.LexicalUnit unit)
		{
			int num = unit.Head;
			if (num != -1)
			{
				Token.RunEntry runEntry = this.RunList[num];
				uint majorKind = runEntry.MajorKind;
				while (runEntry.Type != (RunType)2147483648U && runEntry.Type != (RunType)3221225472U)
				{
					runEntry = this.RunList[++num];
					if (runEntry.MajorKindPlusStartFlag != majorKind)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0007C16E File Offset: 0x0007A36E
		protected internal bool IsContiguous(ref Token.Fragment fragment)
		{
			return fragment.Head + 1 == fragment.Tail && this.RunList[fragment.Head].Type == (RunType)2147483648U;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0007C1A0 File Offset: 0x0007A3A0
		protected internal bool IsContiguous(Token.LexicalUnit unit)
		{
			return this.RunList[unit.Head].Type == (RunType)2147483648U && this.RunList[unit.Head].MajorKind != this.RunList[unit.Head + 1].MajorKindPlusStartFlag;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0007C204 File Offset: 0x0007A404
		protected internal int CalculateHashLowerCase(Token.Fragment fragment)
		{
			int num = fragment.Head;
			if (num == fragment.Tail)
			{
				return HashCode.CalculateEmptyHash();
			}
			int num2 = fragment.HeadOffset;
			if (num + 1 == fragment.Tail && this.RunList[num].Type == (RunType)2147483648U)
			{
				return HashCode.CalculateLowerCase(this.Buffer, num2, this.RunList[num].Length);
			}
			HashCode hashCode = new HashCode(true);
			do
			{
				Token.RunEntry runEntry = this.RunList[num];
				if (runEntry.Type == (RunType)2147483648U)
				{
					hashCode.AdvanceLowerCase(this.Buffer, num2, runEntry.Length);
				}
				else if (runEntry.Type == (RunType)3221225472U)
				{
					hashCode.AdvanceLowerCase(runEntry.Value);
				}
				num2 += runEntry.Length;
			}
			while (++num != fragment.Tail);
			return hashCode.FinalizeHash();
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0007C2F0 File Offset: 0x0007A4F0
		protected internal int CalculateHashLowerCase(Token.LexicalUnit unit)
		{
			int num = unit.Head;
			if (num == -1)
			{
				return HashCode.CalculateEmptyHash();
			}
			int num2 = unit.HeadOffset;
			Token.RunEntry runEntry = this.RunList[num];
			uint majorKind = runEntry.MajorKind;
			if (runEntry.Type == (RunType)2147483648U && majorKind != this.RunList[num + 1].MajorKindPlusStartFlag)
			{
				return HashCode.CalculateLowerCase(this.Buffer, num2, runEntry.Length);
			}
			HashCode hashCode = new HashCode(true);
			do
			{
				if (runEntry.Type == (RunType)2147483648U)
				{
					hashCode.AdvanceLowerCase(this.Buffer, num2, runEntry.Length);
				}
				else if (runEntry.Type == (RunType)3221225472U)
				{
					hashCode.AdvanceLowerCase(runEntry.Value);
				}
				num2 += runEntry.Length;
				runEntry = this.RunList[++num];
			}
			while (runEntry.MajorKindPlusStartFlag == majorKind);
			return hashCode.FinalizeHash();
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0007C3E8 File Offset: 0x0007A5E8
		protected internal int CalculateHash(Token.Fragment fragment)
		{
			int num = fragment.Head;
			if (num == fragment.Tail)
			{
				return HashCode.CalculateEmptyHash();
			}
			int num2 = fragment.HeadOffset;
			if (num + 1 == fragment.Tail && this.RunList[num].Type == (RunType)2147483648U)
			{
				return HashCode.Calculate(this.Buffer, num2, this.RunList[num].Length);
			}
			HashCode hashCode = new HashCode(true);
			do
			{
				Token.RunEntry runEntry = this.RunList[num];
				if (runEntry.Type == (RunType)2147483648U)
				{
					hashCode.Advance(this.Buffer, num2, runEntry.Length);
				}
				else if (runEntry.Type == (RunType)3221225472U)
				{
					hashCode.Advance(runEntry.Value);
				}
				num2 += runEntry.Length;
			}
			while (++num != fragment.Tail);
			return hashCode.FinalizeHash();
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0007C4D4 File Offset: 0x0007A6D4
		protected internal int CalculateHash(Token.LexicalUnit unit)
		{
			int num = unit.Head;
			if (num == -1)
			{
				return HashCode.CalculateEmptyHash();
			}
			int num2 = unit.HeadOffset;
			Token.RunEntry runEntry = this.RunList[num];
			uint majorKind = runEntry.MajorKind;
			if (runEntry.Type == (RunType)2147483648U && majorKind != this.RunList[num + 1].MajorKindPlusStartFlag)
			{
				return HashCode.Calculate(this.Buffer, num2, runEntry.Length);
			}
			HashCode hashCode = new HashCode(true);
			do
			{
				if (runEntry.Type == (RunType)2147483648U)
				{
					hashCode.Advance(this.Buffer, num2, runEntry.Length);
				}
				else if (runEntry.Type == (RunType)3221225472U)
				{
					hashCode.Advance(runEntry.Value);
				}
				num2 += runEntry.Length;
				runEntry = this.RunList[++num];
			}
			while (runEntry.MajorKindPlusStartFlag == majorKind);
			return hashCode.FinalizeHash();
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0007C5CC File Offset: 0x0007A7CC
		protected internal void WriteOriginalTo(ref Token.Fragment fragment, ITextSink sink)
		{
			int num = fragment.Head;
			if (num != fragment.Tail)
			{
				int num2 = fragment.HeadOffset;
				do
				{
					Token.RunEntry runEntry = this.RunList[num];
					if (runEntry.Type == (RunType)2147483648U || runEntry.Type == (RunType)3221225472U)
					{
						sink.Write(this.Buffer, num2, runEntry.Length);
					}
					num2 += runEntry.Length;
				}
				while (++num != fragment.Tail && !sink.IsEnough);
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0007C654 File Offset: 0x0007A854
		protected internal void WriteTo(ref Token.Fragment fragment, ITextSink sink)
		{
			int num = fragment.Head;
			if (num != fragment.Tail)
			{
				int num2 = fragment.HeadOffset;
				do
				{
					Token.RunEntry runEntry = this.RunList[num];
					if (runEntry.Type == (RunType)2147483648U)
					{
						sink.Write(this.Buffer, num2, runEntry.Length);
					}
					else if (runEntry.Type == (RunType)3221225472U)
					{
						sink.Write(runEntry.Value);
					}
					num2 += runEntry.Length;
				}
				while (++num != fragment.Tail && !sink.IsEnough);
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0007C6E8 File Offset: 0x0007A8E8
		protected internal void WriteTo(Token.LexicalUnit unit, ITextSink sink)
		{
			int num = unit.Head;
			if (num != -1)
			{
				int num2 = unit.HeadOffset;
				Token.RunEntry runEntry = this.RunList[num];
				uint majorKind = runEntry.MajorKind;
				do
				{
					if (runEntry.Type == (RunType)2147483648U)
					{
						sink.Write(this.Buffer, num2, runEntry.Length);
					}
					else if (runEntry.Type == (RunType)3221225472U)
					{
						sink.Write(runEntry.Value);
					}
					num2 += runEntry.Length;
					runEntry = this.RunList[++num];
				}
				while (runEntry.MajorKindPlusStartFlag == majorKind && !sink.IsEnough);
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0007C798 File Offset: 0x0007A998
		protected internal void WriteToAndCollapseWhitespace(ref Token.Fragment fragment, ITextSink sink, ref CollapseWhitespaceState collapseWhitespaceState)
		{
			int num = fragment.Head;
			if (num != fragment.Tail)
			{
				int num2 = fragment.HeadOffset;
				if (this.RunList[num].Type < (RunType)2147483648U)
				{
					this.SkipNonTextRuns(ref num, ref num2, fragment.Tail);
				}
				while (num != fragment.Tail && !sink.IsEnough)
				{
					if (this.RunList[num].TextType <= RunTextType.Nbsp)
					{
						if (this.RunList[num].TextType == RunTextType.NewLine)
						{
							collapseWhitespaceState = CollapseWhitespaceState.NewLine;
						}
						else if (collapseWhitespaceState == CollapseWhitespaceState.NonSpace)
						{
							collapseWhitespaceState = CollapseWhitespaceState.Whitespace;
						}
					}
					else
					{
						if (collapseWhitespaceState != CollapseWhitespaceState.NonSpace)
						{
							if (collapseWhitespaceState == CollapseWhitespaceState.NewLine)
							{
								sink.Write(Token.staticCollapseWhitespace, 1, 2);
							}
							else
							{
								sink.Write(Token.staticCollapseWhitespace, 0, 1);
							}
							collapseWhitespaceState = CollapseWhitespaceState.NonSpace;
						}
						if (this.RunList[num].Type == (RunType)3221225472U)
						{
							sink.Write(this.RunList[num].Value);
						}
						else
						{
							sink.Write(this.Buffer, num2, this.RunList[num].Length);
						}
					}
					num2 += this.RunList[num].Length;
					num++;
					if (num != fragment.Tail && this.RunList[num].Type < (RunType)2147483648U)
					{
						this.SkipNonTextRuns(ref num, ref num2, fragment.Tail);
					}
				}
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0007C904 File Offset: 0x0007AB04
		protected internal string GetString(ref Token.Fragment fragment, int maxLength)
		{
			if (fragment.Head == fragment.Tail)
			{
				return string.Empty;
			}
			if (this.IsContiguous(ref fragment))
			{
				return new string(this.Buffer, fragment.HeadOffset, this.GetLength(ref fragment));
			}
			if (this.IsFragmentEmpty(ref fragment))
			{
				return string.Empty;
			}
			if (this.stringBuildSink == null)
			{
				this.stringBuildSink = new StringBuildSink();
			}
			this.stringBuildSink.Reset(maxLength);
			this.WriteTo(ref fragment, this.stringBuildSink);
			return this.stringBuildSink.ToString();
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0007C990 File Offset: 0x0007AB90
		protected internal string GetString(Token.LexicalUnit unit, int maxLength)
		{
			if (this.IsFragmentEmpty(unit))
			{
				return string.Empty;
			}
			if (this.IsContiguous(unit))
			{
				return new string(this.Buffer, unit.HeadOffset, this.GetLength(unit));
			}
			if (this.stringBuildSink == null)
			{
				this.stringBuildSink = new StringBuildSink();
			}
			this.stringBuildSink.Reset(maxLength);
			this.WriteTo(unit, this.stringBuildSink);
			return this.stringBuildSink.ToString();
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0007CA06 File Offset: 0x0007AC06
		protected internal bool CaseInsensitiveCompareEqual(ref Token.Fragment fragment, string str)
		{
			if (this.compareSink == null)
			{
				this.compareSink = new Token.LowerCaseCompareSink();
			}
			this.compareSink.Reset(str);
			this.WriteTo(ref fragment, this.compareSink);
			return this.compareSink.IsEqual;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0007CA3F File Offset: 0x0007AC3F
		protected internal bool CaseInsensitiveCompareEqual(Token.LexicalUnit unit, string str)
		{
			if (this.compareSink == null)
			{
				this.compareSink = new Token.LowerCaseCompareSink();
			}
			this.compareSink.Reset(str);
			this.WriteTo(unit, this.compareSink);
			return this.compareSink.IsEqual;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0007CA78 File Offset: 0x0007AC78
		protected internal virtual bool CaseInsensitiveCompareRunEqual(int runOffset, string str, int strOffset)
		{
			int i = strOffset;
			while (i < str.Length)
			{
				if (ParseSupport.ToLowerCase(this.Buffer[runOffset++]) != str[i++])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0007CAB4 File Offset: 0x0007ACB4
		protected internal bool CaseInsensitiveContainsSubstring(ref Token.Fragment fragment, string str)
		{
			if (this.searchSink == null)
			{
				this.searchSink = new Token.LowerCaseSubstringSearchSink();
			}
			this.searchSink.Reset(str);
			this.WriteTo(ref fragment, this.searchSink);
			return this.searchSink.IsFound;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0007CAED File Offset: 0x0007ACED
		protected internal bool CaseInsensitiveContainsSubstring(Token.LexicalUnit unit, string str)
		{
			if (this.searchSink == null)
			{
				this.searchSink = new Token.LowerCaseSubstringSearchSink();
			}
			this.searchSink.Reset(str);
			this.WriteTo(unit, this.searchSink);
			return this.searchSink.IsFound;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0007CB28 File Offset: 0x0007AD28
		protected internal void StripLeadingWhitespace(ref Token.Fragment fragment)
		{
			int num = fragment.Head;
			if (num != fragment.Tail)
			{
				int num2 = fragment.HeadOffset;
				if (this.RunList[num].Type < (RunType)2147483648U)
				{
					this.SkipNonTextRuns(ref num, ref num2, fragment.Tail);
				}
				if (num == fragment.Tail)
				{
					return;
				}
				int i;
				do
				{
					if (this.RunList[num].Type == (RunType)3221225472U)
					{
						if (this.RunList[num].Value > 65535)
						{
							break;
						}
						CharClass charClass = ParseSupport.GetCharClass((char)this.RunList[num].Value);
						if (!ParseSupport.WhitespaceCharacter(charClass))
						{
							break;
						}
					}
					else
					{
						for (i = num2; i < num2 + this.RunList[num].Length; i++)
						{
							CharClass charClass = ParseSupport.GetCharClass(this.Buffer[i]);
							if (!ParseSupport.WhitespaceCharacter(charClass))
							{
								break;
							}
						}
						if (i < num2 + this.RunList[num].Length)
						{
							goto Block_8;
						}
					}
					num2 += this.RunList[num].Length;
					num++;
					if (num != fragment.Tail && this.RunList[num].Type < (RunType)2147483648U)
					{
						this.SkipNonTextRuns(ref num, ref num2, fragment.Tail);
					}
				}
				while (num != fragment.Tail);
				goto IL_162;
				Block_8:
				Token.RunEntry[] runList = this.RunList;
				int num3 = num;
				runList[num3].Length = runList[num3].Length - (i - num2);
				num2 = i;
				IL_162:
				fragment.Head = num;
				fragment.HeadOffset = num2;
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0007CCA8 File Offset: 0x0007AEA8
		protected internal bool SkipLeadingWhitespace(Token.LexicalUnit unit, ref Token.FragmentPosition position)
		{
			int num = unit.Head;
			if (num != -1)
			{
				int num2 = unit.HeadOffset;
				Token.RunEntry runEntry = this.RunList[num];
				uint majorKind = runEntry.MajorKind;
				int runDeltaOffset = 0;
				int i;
				do
				{
					if (runEntry.Type == (RunType)3221225472U)
					{
						if (runEntry.Value > 65535)
						{
							break;
						}
						CharClass charClass = ParseSupport.GetCharClass((char)runEntry.Value);
						if (!ParseSupport.WhitespaceCharacter(charClass))
						{
							break;
						}
					}
					else if (runEntry.Type == (RunType)2147483648U)
					{
						for (i = num2; i < num2 + runEntry.Length; i++)
						{
							CharClass charClass = ParseSupport.GetCharClass(this.Buffer[i]);
							if (!ParseSupport.WhitespaceCharacter(charClass))
							{
								break;
							}
						}
						if (i < num2 + runEntry.Length)
						{
							goto Block_7;
						}
					}
					num2 += runEntry.Length;
					runEntry = this.RunList[++num];
				}
				while (runEntry.MajorKindPlusStartFlag == majorKind);
				goto IL_EF;
				Block_7:
				runDeltaOffset = i - num2;
				IL_EF:
				position.Run = num;
				position.RunOffset = num2;
				position.RunDeltaOffset = runDeltaOffset;
				if (num == unit.Head || runEntry.MajorKindPlusStartFlag == majorKind)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0007CDD4 File Offset: 0x0007AFD4
		protected internal bool MoveToNextRun(ref Token.Fragment fragment, ref Token.FragmentPosition position, bool skipInvalid)
		{
			int num = position.Run;
			if (num != fragment.Tail)
			{
				if (num >= fragment.Head)
				{
					position.RunOffset += this.RunList[num].Length;
					position.RunDeltaOffset = 0;
				}
				num++;
				if (skipInvalid)
				{
					while (num != fragment.Tail && this.RunList[num].Type == RunType.Invalid)
					{
						position.RunOffset += this.RunList[num].Length;
						num++;
					}
				}
				position.Run = num;
				return num != fragment.Tail;
			}
			return false;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0007CE80 File Offset: 0x0007B080
		protected internal bool IsCurrentEof(ref Token.FragmentPosition position)
		{
			int run = position.Run;
			if (this.RunList[run].Type == (RunType)3221225472U)
			{
				return position.RunDeltaOffset == Token.LiteralLength(this.RunList[run].Value);
			}
			return position.RunDeltaOffset == this.RunList[run].Length;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0007CEE4 File Offset: 0x0007B0E4
		protected internal int ReadCurrent(ref Token.FragmentPosition position, char[] buffer, int offset, int count)
		{
			int run = position.Run;
			if (this.RunList[run].Type != (RunType)3221225472U)
			{
				int num = Math.Min(count, this.RunList[run].Length - position.RunDeltaOffset);
				if (num != 0)
				{
					System.Buffer.BlockCopy(this.Buffer, (position.RunOffset + position.RunDeltaOffset) * 2, buffer, offset * 2, num * 2);
					position.RunDeltaOffset += num;
				}
				return num;
			}
			int num2 = Token.LiteralLength(this.RunList[run].Value);
			if (position.RunDeltaOffset == num2)
			{
				return 0;
			}
			if (num2 == 1)
			{
				buffer[offset] = (char)this.RunList[run].Value;
				position.RunDeltaOffset++;
				return 1;
			}
			if (position.RunDeltaOffset != 0)
			{
				buffer[offset] = Token.LiteralLastChar(this.RunList[run].Value);
				position.RunDeltaOffset++;
				return 1;
			}
			buffer[offset++] = Token.LiteralFirstChar(this.RunList[run].Value);
			count--;
			position.RunDeltaOffset++;
			if (count == 0)
			{
				return 1;
			}
			buffer[offset] = Token.LiteralLastChar(this.RunList[run].Value);
			position.RunDeltaOffset++;
			return 2;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0007D043 File Offset: 0x0007B243
		internal void SkipNonTextRuns(ref int run, ref int runOffset, int tail)
		{
			do
			{
				runOffset += this.RunList[run].Length;
				run++;
			}
			while (run != tail && this.RunList[run].Type < (RunType)2147483648U);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0007D080 File Offset: 0x0007B280
		internal void Reset()
		{
			this.tokenId = TokenId.None;
			this.argument = 0;
			this.tokenEncoding = null;
			this.Whole.Reset();
			this.WholePosition.Reset();
		}

		// Token: 0x040011AD RID: 4525
		protected internal char[] Buffer;

		// Token: 0x040011AE RID: 4526
		protected internal Token.RunEntry[] RunList;

		// Token: 0x040011AF RID: 4527
		protected internal Token.Fragment Whole;

		// Token: 0x040011B0 RID: 4528
		protected internal Token.FragmentPosition WholePosition;

		// Token: 0x040011B1 RID: 4529
		private static char[] staticCollapseWhitespace = new char[]
		{
			' ',
			'\r',
			'\n'
		};

		// Token: 0x040011B2 RID: 4530
		private TokenId tokenId;

		// Token: 0x040011B3 RID: 4531
		private Encoding tokenEncoding;

		// Token: 0x040011B4 RID: 4532
		private int argument;

		// Token: 0x040011B5 RID: 4533
		private Token.LowerCaseCompareSink compareSink;

		// Token: 0x040011B6 RID: 4534
		private Token.LowerCaseSubstringSearchSink searchSink;

		// Token: 0x040011B7 RID: 4535
		private StringBuildSink stringBuildSink;

		// Token: 0x02000191 RID: 401
		public struct RunEnumerator
		{
			// Token: 0x06001142 RID: 4418 RVA: 0x0007D0CE File Offset: 0x0007B2CE
			internal RunEnumerator(Token token)
			{
				this.token = token;
			}

			// Token: 0x170004C0 RID: 1216
			// (get) Token: 0x06001143 RID: 4419 RVA: 0x0007D0D7 File Offset: 0x0007B2D7
			public TokenRun Current
			{
				get
				{
					return new TokenRun(this.token);
				}
			}

			// Token: 0x170004C1 RID: 1217
			// (get) Token: 0x06001144 RID: 4420 RVA: 0x0007D0E4 File Offset: 0x0007B2E4
			public bool IsValidPosition
			{
				get
				{
					return this.token.WholePosition.Run >= this.token.Whole.Head && this.token.WholePosition.Run < this.token.Whole.Tail;
				}
			}

			// Token: 0x170004C2 RID: 1218
			// (get) Token: 0x06001145 RID: 4421 RVA: 0x0007D137 File Offset: 0x0007B337
			public int CurrentIndex
			{
				get
				{
					return this.token.WholePosition.Run;
				}
			}

			// Token: 0x170004C3 RID: 1219
			// (get) Token: 0x06001146 RID: 4422 RVA: 0x0007D149 File Offset: 0x0007B349
			public int CurrentOffset
			{
				get
				{
					return this.token.WholePosition.RunOffset;
				}
			}

			// Token: 0x06001147 RID: 4423 RVA: 0x0007D15C File Offset: 0x0007B35C
			public bool MoveNext()
			{
				return this.token.MoveToNextRun(ref this.token.Whole, ref this.token.WholePosition, false);
			}

			// Token: 0x06001148 RID: 4424 RVA: 0x0007D190 File Offset: 0x0007B390
			public bool MoveNext(bool skipInvalid)
			{
				return this.token.MoveToNextRun(ref this.token.Whole, ref this.token.WholePosition, skipInvalid);
			}

			// Token: 0x06001149 RID: 4425 RVA: 0x0007D1C1 File Offset: 0x0007B3C1
			public void Rewind()
			{
				this.token.WholePosition.Rewind(this.token.Whole);
			}

			// Token: 0x0600114A RID: 4426 RVA: 0x0007D1DE File Offset: 0x0007B3DE
			public Token.RunEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x0600114B RID: 4427 RVA: 0x0007D1E6 File Offset: 0x0007B3E6
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x040011B8 RID: 4536
			private Token token;
		}

		// Token: 0x02000192 RID: 402
		public struct TextReader
		{
			// Token: 0x0600114C RID: 4428 RVA: 0x0007D1E8 File Offset: 0x0007B3E8
			internal TextReader(Token token)
			{
				this.token = token;
			}

			// Token: 0x170004C4 RID: 1220
			// (get) Token: 0x0600114D RID: 4429 RVA: 0x0007D1F1 File Offset: 0x0007B3F1
			public int Length
			{
				get
				{
					return this.token.GetLength(ref this.token.Whole);
				}
			}

			// Token: 0x170004C5 RID: 1221
			// (get) Token: 0x0600114E RID: 4430 RVA: 0x0007D209 File Offset: 0x0007B409
			public int OriginalLength
			{
				get
				{
					return this.token.GetOriginalLength(ref this.token.Whole);
				}
			}

			// Token: 0x0600114F RID: 4431 RVA: 0x0007D224 File Offset: 0x0007B424
			public int Read(char[] buffer, int offset, int count)
			{
				return this.token.Read(ref this.token.Whole, ref this.token.WholePosition, buffer, offset, count);
			}

			// Token: 0x06001150 RID: 4432 RVA: 0x0007D257 File Offset: 0x0007B457
			public void Rewind()
			{
				this.token.WholePosition.Rewind(this.token.Whole);
			}

			// Token: 0x06001151 RID: 4433 RVA: 0x0007D274 File Offset: 0x0007B474
			public void WriteTo(ITextSink sink)
			{
				this.token.WriteTo(ref this.token.Whole, sink);
			}

			// Token: 0x06001152 RID: 4434 RVA: 0x0007D28D File Offset: 0x0007B48D
			public void WriteToAndCollapseWhitespace(ITextSink sink, ref CollapseWhitespaceState collapseWhitespaceState)
			{
				this.token.WriteToAndCollapseWhitespace(ref this.token.Whole, sink, ref collapseWhitespaceState);
			}

			// Token: 0x06001153 RID: 4435 RVA: 0x0007D2A7 File Offset: 0x0007B4A7
			public void StripLeadingWhitespace()
			{
				this.token.StripLeadingWhitespace(ref this.token.Whole);
				this.Rewind();
			}

			// Token: 0x06001154 RID: 4436 RVA: 0x0007D2C8 File Offset: 0x0007B4C8
			public int ReadOriginal(char[] buffer, int offset, int count)
			{
				return this.token.ReadOriginal(ref this.token.Whole, ref this.token.WholePosition, buffer, offset, count);
			}

			// Token: 0x06001155 RID: 4437 RVA: 0x0007D2FB File Offset: 0x0007B4FB
			public void WriteOriginalTo(ITextSink sink)
			{
				this.token.WriteOriginalTo(ref this.token.Whole, sink);
			}

			// Token: 0x06001156 RID: 4438 RVA: 0x0007D314 File Offset: 0x0007B514
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x040011B9 RID: 4537
			private Token token;
		}

		// Token: 0x02000193 RID: 403
		internal struct RunEntry
		{
			// Token: 0x170004C6 RID: 1222
			// (get) Token: 0x06001157 RID: 4439 RVA: 0x0007D316 File Offset: 0x0007B516
			public RunType Type
			{
				get
				{
					return (RunType)(this.lengthAndType & 3221225472U);
				}
			}

			// Token: 0x170004C7 RID: 1223
			// (get) Token: 0x06001158 RID: 4440 RVA: 0x0007D324 File Offset: 0x0007B524
			public RunTextType TextType
			{
				get
				{
					return (RunTextType)(this.lengthAndType & 939524096U);
				}
			}

			// Token: 0x170004C8 RID: 1224
			// (get) Token: 0x06001159 RID: 4441 RVA: 0x0007D332 File Offset: 0x0007B532
			// (set) Token: 0x0600115A RID: 4442 RVA: 0x0007D340 File Offset: 0x0007B540
			public int Length
			{
				get
				{
					return (int)(this.lengthAndType & 16777215U);
				}
				set
				{
					this.lengthAndType = (uint)(value | (int)(this.lengthAndType & 4278190080U));
				}
			}

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x0600115B RID: 4443 RVA: 0x0007D356 File Offset: 0x0007B556
			public uint Kind
			{
				get
				{
					return this.valueAndKind & 4278190080U;
				}
			}

			// Token: 0x170004CA RID: 1226
			// (get) Token: 0x0600115C RID: 4444 RVA: 0x0007D364 File Offset: 0x0007B564
			public uint MajorKindPlusStartFlag
			{
				get
				{
					return this.valueAndKind & 4227858432U;
				}
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x0600115D RID: 4445 RVA: 0x0007D372 File Offset: 0x0007B572
			public uint MajorKind
			{
				get
				{
					return this.valueAndKind & 2080374784U;
				}
			}

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x0600115E RID: 4446 RVA: 0x0007D380 File Offset: 0x0007B580
			public int Value
			{
				get
				{
					return (int)(this.valueAndKind & 16777215U);
				}
			}

			// Token: 0x0600115F RID: 4447 RVA: 0x0007D38E File Offset: 0x0007B58E
			public void Initialize(RunType type, RunTextType textType, uint kind, int length, int value)
			{
				this.lengthAndType = (uint)(length | (int)type | (int)textType);
				this.valueAndKind = (uint)(value | (int)kind);
			}

			// Token: 0x06001160 RID: 4448 RVA: 0x0007D3A6 File Offset: 0x0007B5A6
			public void InitializeSentinel()
			{
				this.valueAndKind = 2147483648U;
			}

			// Token: 0x06001161 RID: 4449 RVA: 0x0007D3B4 File Offset: 0x0007B5B4
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					this.Type.ToString(),
					" - ",
					this.TextType.ToString(),
					" - ",
					((this.Kind & 2147483647U) >> 26).ToString(),
					"/",
					(this.Kind >> 24 & 3U).ToString(),
					" (",
					this.Length,
					") = ",
					this.Value.ToString("X6")
				});
			}

			// Token: 0x040011BA RID: 4538
			internal const int MaxRunLength = 134217727;

			// Token: 0x040011BB RID: 4539
			internal const int MaxRunValue = 16777215;

			// Token: 0x040011BC RID: 4540
			private uint lengthAndType;

			// Token: 0x040011BD RID: 4541
			private uint valueAndKind;
		}

		// Token: 0x02000194 RID: 404
		internal struct LexicalUnit
		{
			// Token: 0x06001162 RID: 4450 RVA: 0x0007D473 File Offset: 0x0007B673
			public void Reset()
			{
				this.Head = -1;
				this.HeadOffset = 0;
			}

			// Token: 0x06001163 RID: 4451 RVA: 0x0007D483 File Offset: 0x0007B683
			public void Initialize(int run, int offset)
			{
				this.Head = run;
				this.HeadOffset = offset;
			}

			// Token: 0x06001164 RID: 4452 RVA: 0x0007D493 File Offset: 0x0007B693
			public override string ToString()
			{
				return this.Head.ToString("X") + " / " + this.HeadOffset.ToString("X");
			}

			// Token: 0x040011BE RID: 4542
			public int Head;

			// Token: 0x040011BF RID: 4543
			public int HeadOffset;
		}

		// Token: 0x02000195 RID: 405
		internal struct Fragment
		{
			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x06001165 RID: 4453 RVA: 0x0007D4BF File Offset: 0x0007B6BF
			public bool IsEmpty
			{
				get
				{
					return this.Head == this.Tail;
				}
			}

			// Token: 0x06001166 RID: 4454 RVA: 0x0007D4D0 File Offset: 0x0007B6D0
			public void Reset()
			{
				this.Head = (this.Tail = (this.HeadOffset = 0));
			}

			// Token: 0x06001167 RID: 4455 RVA: 0x0007D4F8 File Offset: 0x0007B6F8
			public void Initialize(int run, int offset)
			{
				this.Tail = run;
				this.Head = run;
				this.HeadOffset = offset;
			}

			// Token: 0x06001168 RID: 4456 RVA: 0x0007D51C File Offset: 0x0007B71C
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					this.Head.ToString("X"),
					" - ",
					this.Tail.ToString("X"),
					" / ",
					this.HeadOffset.ToString("X")
				});
			}

			// Token: 0x040011C0 RID: 4544
			public int Head;

			// Token: 0x040011C1 RID: 4545
			public int Tail;

			// Token: 0x040011C2 RID: 4546
			public int HeadOffset;
		}

		// Token: 0x02000196 RID: 406
		internal struct FragmentPosition
		{
			// Token: 0x06001169 RID: 4457 RVA: 0x0007D57F File Offset: 0x0007B77F
			public void Reset()
			{
				this.Run = -2;
				this.RunOffset = 0;
				this.RunDeltaOffset = 0;
			}

			// Token: 0x0600116A RID: 4458 RVA: 0x0007D597 File Offset: 0x0007B797
			public void Rewind(Token.LexicalUnit unit)
			{
				this.Run = unit.Head - 1;
				this.RunOffset = unit.HeadOffset;
				this.RunDeltaOffset = 0;
			}

			// Token: 0x0600116B RID: 4459 RVA: 0x0007D5BC File Offset: 0x0007B7BC
			public void Rewind(Token.Fragment fragment)
			{
				this.Run = fragment.Head - 1;
				this.RunOffset = fragment.HeadOffset;
				this.RunDeltaOffset = 0;
			}

			// Token: 0x0600116C RID: 4460 RVA: 0x0007D5E1 File Offset: 0x0007B7E1
			public bool SameAs(Token.FragmentPosition pos2)
			{
				return this.Run == pos2.Run && this.RunOffset == pos2.RunOffset && this.RunDeltaOffset == pos2.RunDeltaOffset;
			}

			// Token: 0x0600116D RID: 4461 RVA: 0x0007D614 File Offset: 0x0007B814
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					this.Run.ToString("X"),
					" / ",
					this.RunOffset.ToString("X"),
					" + ",
					this.RunDeltaOffset.ToString("X")
				});
			}

			// Token: 0x040011C3 RID: 4547
			public int Run;

			// Token: 0x040011C4 RID: 4548
			public int RunOffset;

			// Token: 0x040011C5 RID: 4549
			public int RunDeltaOffset;
		}

		// Token: 0x02000197 RID: 407
		private class LowerCaseCompareSink : ITextSink
		{
			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x0600116F RID: 4463 RVA: 0x0007D67F File Offset: 0x0007B87F
			public bool IsEqual
			{
				get
				{
					return !this.definitelyNotEqual && this.strIndex == this.str.Length;
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x06001170 RID: 4464 RVA: 0x0007D69E File Offset: 0x0007B89E
			public bool IsEnough
			{
				get
				{
					return this.definitelyNotEqual;
				}
			}

			// Token: 0x06001171 RID: 4465 RVA: 0x0007D6A6 File Offset: 0x0007B8A6
			public void Reset(string str)
			{
				this.str = str;
				this.strIndex = 0;
				this.definitelyNotEqual = false;
			}

			// Token: 0x06001172 RID: 4466 RVA: 0x0007D6C0 File Offset: 0x0007B8C0
			public void Write(char[] buffer, int offset, int count)
			{
				int num = offset + count;
				while (offset < num)
				{
					if (this.strIndex == 0)
					{
						if (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(buffer[offset])))
						{
							offset++;
							continue;
						}
					}
					else if (this.strIndex == this.str.Length)
					{
						if (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(buffer[offset])))
						{
							offset++;
							continue;
						}
						this.definitelyNotEqual = true;
						return;
					}
					if (ParseSupport.ToLowerCase(buffer[offset]) != this.str[this.strIndex])
					{
						this.definitelyNotEqual = true;
						return;
					}
					offset++;
					this.strIndex++;
				}
			}

			// Token: 0x06001173 RID: 4467 RVA: 0x0007D764 File Offset: 0x0007B964
			public void Write(int ucs32Char)
			{
				if (Token.LiteralLength(ucs32Char) != 1)
				{
					this.definitelyNotEqual = true;
					return;
				}
				if (this.strIndex == 0)
				{
					if (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass((char)ucs32Char)))
					{
						return;
					}
				}
				else if (this.strIndex == this.str.Length)
				{
					if (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass((char)ucs32Char)))
					{
						return;
					}
					this.definitelyNotEqual = true;
					return;
				}
				if (this.str[this.strIndex] != ParseSupport.ToLowerCase((char)ucs32Char))
				{
					this.definitelyNotEqual = true;
					return;
				}
				this.strIndex++;
			}

			// Token: 0x040011C6 RID: 4550
			private bool definitelyNotEqual;

			// Token: 0x040011C7 RID: 4551
			private int strIndex;

			// Token: 0x040011C8 RID: 4552
			private string str;
		}

		// Token: 0x02000198 RID: 408
		private class LowerCaseSubstringSearchSink : ITextSink
		{
			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x06001175 RID: 4469 RVA: 0x0007D7FB File Offset: 0x0007B9FB
			public bool IsFound
			{
				get
				{
					return this.found;
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x06001176 RID: 4470 RVA: 0x0007D803 File Offset: 0x0007BA03
			public bool IsEnough
			{
				get
				{
					return this.found;
				}
			}

			// Token: 0x06001177 RID: 4471 RVA: 0x0007D80B File Offset: 0x0007BA0B
			public void Reset(string str)
			{
				this.str = str;
				this.strIndex = 0;
				this.found = false;
			}

			// Token: 0x06001178 RID: 4472 RVA: 0x0007D824 File Offset: 0x0007BA24
			public void Write(char[] buffer, int offset, int count)
			{
				int num = offset + count;
				while (offset < num && this.strIndex < this.str.Length)
				{
					if (ParseSupport.ToLowerCase(buffer[offset]) == this.str[this.strIndex])
					{
						this.strIndex++;
					}
					else
					{
						this.strIndex = 0;
					}
					offset++;
				}
				if (this.strIndex == this.str.Length)
				{
					this.found = true;
				}
			}

			// Token: 0x06001179 RID: 4473 RVA: 0x0007D8A0 File Offset: 0x0007BAA0
			public void Write(int ucs32Char)
			{
				if (Token.LiteralLength(ucs32Char) != 1 || this.str[this.strIndex] != ParseSupport.ToLowerCase((char)ucs32Char))
				{
					this.strIndex = 0;
					return;
				}
				this.strIndex++;
				if (this.strIndex == this.str.Length)
				{
					this.found = true;
				}
			}

			// Token: 0x040011C9 RID: 4553
			private bool found;

			// Token: 0x040011CA RID: 4554
			private int strIndex;

			// Token: 0x040011CB RID: 4555
			private string str;
		}
	}
}
