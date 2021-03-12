using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000083 RID: 131
	internal struct ValueParser
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0001D2E6 File Offset: 0x0001B4E6
		public ValueParser(MimeStringList lines, bool allowUTF8)
		{
			this.lines = lines;
			this.allowUTF8 = allowUTF8;
			this.nextLine = 0;
			this.bytes = null;
			this.start = 0;
			this.end = 0;
			this.position = 0;
			this.ParseNextLine();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001D320 File Offset: 0x0001B520
		public ValueParser(MimeStringList lines, ValueParser valueParser)
		{
			this.lines = lines;
			this.allowUTF8 = valueParser.allowUTF8;
			this.nextLine = valueParser.nextLine;
			if (this.nextLine > 0 && this.nextLine <= this.lines.Count)
			{
				int num;
				this.bytes = this.lines[this.nextLine - 1].GetData(out this.start, out num);
				this.start = valueParser.start;
				this.position = valueParser.position;
				this.end = valueParser.end;
				return;
			}
			this.bytes = null;
			this.start = 0;
			this.end = 0;
			this.position = 0;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001D3D7 File Offset: 0x0001B5D7
		private bool Eof
		{
			get
			{
				return this.nextLine >= this.lines.Count;
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001D3EF File Offset: 0x0001B5EF
		public static int ParseToken(string value, int currentOffset, bool allowUTF8)
		{
			return MimeScan.FindEndOf(MimeScan.Token.Token, value, currentOffset, allowUTF8);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001D3FA File Offset: 0x0001B5FA
		public static int ParseToken(MimeString str, out int characterCount, bool allowUTF8)
		{
			return MimeScan.FindEndOf(MimeScan.Token.Token, str.Data, str.Offset, str.Length, out characterCount, allowUTF8);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001D41C File Offset: 0x0001B61C
		public bool ParseToDelimiter(bool ignoreNextByte, bool separateWithWhitespace, ref MimeStringList phrase)
		{
			bool result = false;
			int num = ignoreNextByte ? 1 : 0;
			for (;;)
			{
				int num2 = 0;
				num += MimeScan.FindEndOf(MimeScan.Token.Atom, this.bytes, this.position + num, this.end - this.position - num, out num2, this.allowUTF8);
				if (num != 0)
				{
					result = true;
					if (phrase.Length != 0 && separateWithWhitespace)
					{
						if (this.position == this.start || this.bytes[this.position - 1] != 32)
						{
							phrase.AppendFragment(ValueParser.SpaceLine);
						}
						else
						{
							this.position--;
							num++;
						}
					}
					separateWithWhitespace = false;
					phrase.AppendFragment(new MimeString(this.bytes, this.position, num));
					this.position += num;
				}
				if (this.position != this.end || !this.ParseNextLine())
				{
					break;
				}
				num = 0;
			}
			return result;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001D4FC File Offset: 0x0001B6FC
		public byte ParseGet()
		{
			if (this.position == this.end && !this.ParseNextLine())
			{
				return 0;
			}
			return this.bytes[this.position++];
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001D539 File Offset: 0x0001B739
		public void ParseUnget()
		{
			if (this.position == this.start)
			{
				this.ParseUngetLine();
			}
			this.position--;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001D560 File Offset: 0x0001B760
		public void ParseQString(bool save, ref MimeStringList phrase, bool handleISO2022)
		{
			bool flag = false;
			if (save)
			{
				phrase.AppendFragment(new MimeString(this.bytes, this.position, 1, 268435456U));
			}
			this.position++;
			bool flag2 = true;
			for (;;)
			{
				int num = MimeScan.ScanQuotedString(this.bytes, this.position, this.end - this.position, handleISO2022, ref flag);
				if (num != 0)
				{
					if (save)
					{
						phrase.AppendFragment(new MimeString(this.bytes, this.position, num));
					}
					this.position += num;
				}
				if (this.position != this.end)
				{
					if (this.bytes[this.position] == 14 || this.bytes[this.position] == 27)
					{
						this.ParseEscapedString(save, ref phrase, out flag2);
					}
					else
					{
						if (save)
						{
							phrase.AppendFragment(new MimeString(this.bytes, this.position, 1, 268435456U));
						}
						this.position++;
						if (this.bytes[this.position - 1] == 34)
						{
							break;
						}
						flag = true;
					}
				}
				else if (!this.ParseNextLine())
				{
					goto Block_8;
				}
			}
			return;
			Block_8:
			if (save && flag2)
			{
				phrase.AppendFragment(new MimeString(MimeString.DoubleQuote, 0, MimeString.DoubleQuote.Length, 268435456U));
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001D6A4 File Offset: 0x0001B8A4
		public void ParseComment(bool save, bool saveInnerOnly, ref MimeStringList comment, bool handleISO2022)
		{
			int num = 1;
			bool flag = false;
			int num2 = 0;
			if (save && !saveInnerOnly)
			{
				comment.AppendFragment(new MimeString(this.bytes, this.position, 1));
			}
			this.position++;
			for (;;)
			{
				int num3 = MimeScan.ScanComment(this.bytes, this.position, this.end - this.position, handleISO2022, ref num, ref flag);
				if (num3 != 0)
				{
					if (save)
					{
						if (num == 0 && saveInnerOnly)
						{
							num2 = 1;
						}
						comment.AppendFragment(new MimeString(this.bytes, this.position, num3 - num2));
					}
					this.position += num3;
					if (num == 0)
					{
						break;
					}
				}
				if (this.position != this.end && (this.bytes[this.position] == 14 || this.bytes[this.position] == 27))
				{
					bool flag2;
					this.ParseEscapedString(save, ref comment, out flag2);
				}
				else if (!this.ParseNextLine())
				{
					return;
				}
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001D78C File Offset: 0x0001B98C
		public bool ParseNextLine()
		{
			if (this.nextLine >= this.lines.Count)
			{
				return false;
			}
			int num;
			this.bytes = this.lines[this.nextLine].GetData(out this.start, out num);
			this.position = this.start;
			this.end = this.start + num;
			this.nextLine++;
			return true;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001D800 File Offset: 0x0001BA00
		public void ParseUngetLine()
		{
			int num;
			this.bytes = this.lines[this.nextLine - 2].GetData(out this.start, out num);
			this.position = (this.end = this.start + num);
			this.nextLine--;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001D85C File Offset: 0x0001BA5C
		public void ParseWhitespace(bool save, ref MimeStringList phrase)
		{
			for (;;)
			{
				int num = MimeScan.SkipLwsp(this.bytes, this.position, this.end - this.position);
				if (save && num != 0)
				{
					phrase.AppendFragment(new MimeString(this.bytes, this.position, num));
				}
				this.position += num;
				if (this.position != this.end)
				{
					break;
				}
				if (!this.ParseNextLine())
				{
					return;
				}
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001D8CC File Offset: 0x0001BACC
		public void ParseCFWS(bool save, ref MimeStringList phrase, bool handleISO2022)
		{
			for (;;)
			{
				int num = MimeScan.SkipLwsp(this.bytes, this.position, this.end - this.position);
				if (save && num != 0)
				{
					phrase.AppendFragment(new MimeString(this.bytes, this.position, num));
				}
				this.position += num;
				if (this.position != this.end)
				{
					if (this.bytes[this.position] != 40)
					{
						break;
					}
					this.ParseComment(save, false, ref phrase, handleISO2022);
				}
				else if (!this.ParseNextLine())
				{
					break;
				}
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001D958 File Offset: 0x0001BB58
		public void ParseSkipToNextDelimiterByte(byte delimiter)
		{
			MimeStringList mimeStringList = default(MimeStringList);
			for (;;)
			{
				if (this.position != this.end)
				{
					byte b = this.bytes[this.position];
					if (b == delimiter)
					{
						break;
					}
					if (b == 34)
					{
						this.ParseQString(false, ref mimeStringList, true);
					}
					else if (b == 40)
					{
						this.ParseComment(false, false, ref mimeStringList, true);
					}
					else
					{
						this.position++;
						this.ParseCFWS(false, ref mimeStringList, true);
						int num = 0;
						this.position += MimeScan.FindEndOf(MimeScan.Token.Atom, this.bytes, this.position, this.end - this.position, out num, this.allowUTF8);
					}
				}
				else if (!this.ParseNextLine())
				{
					return;
				}
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001DA13 File Offset: 0x0001BC13
		public MimeString ParseToken()
		{
			return this.ParseToken(MimeScan.Token.Token);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001DA1C File Offset: 0x0001BC1C
		public MimeString ParseToken(MimeScan.Token token)
		{
			MimeStringList mimeStringList = default(MimeStringList);
			while (this.position != this.end || this.ParseNextLine())
			{
				int num = 0;
				int num2 = MimeScan.FindEndOf(token, this.bytes, this.position, this.end - this.position, out num, this.allowUTF8);
				if (num2 == 0)
				{
					break;
				}
				mimeStringList.AppendFragment(new MimeString(this.bytes, this.position, num2));
				this.position += num2;
			}
			if (mimeStringList.Count == 0)
			{
				return default(MimeString);
			}
			if (mimeStringList.Count == 1)
			{
				return mimeStringList[0];
			}
			byte[] sz = mimeStringList.GetSz();
			return new MimeString(sz, 0, sz.Length);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001DAD8 File Offset: 0x0001BCD8
		public void ParseParameterValue(ref MimeStringList value, ref bool goodValue, bool handleISO2022)
		{
			MimeStringList mimeStringList = default(MimeStringList);
			goodValue = true;
			while (this.position != this.end || this.ParseNextLine())
			{
				byte b = this.bytes[this.position];
				if (b == 34)
				{
					value.Reset();
					mimeStringList.Reset();
					this.ParseQString(true, ref value, handleISO2022);
					return;
				}
				if (b == 40 || MimeScan.IsLWSP(b))
				{
					this.ParseCFWS(true, ref mimeStringList, handleISO2022);
				}
				else
				{
					if (b == 59)
					{
						return;
					}
					int num = this.position;
					do
					{
						int num2 = 1;
						if (!MimeScan.IsToken(b))
						{
							if (this.allowUTF8 && b >= 128)
							{
								if (!MimeScan.IsUTF8NonASCII(this.bytes, this.position, this.end, out num2))
								{
									num2 = 1;
									goodValue = false;
								}
							}
							else
							{
								goodValue = false;
							}
						}
						this.position += num2;
						if (this.position == this.end)
						{
							break;
						}
						b = this.bytes[this.position];
					}
					while (b != 59 && b != 40 && !MimeScan.IsLWSP(b));
					value.TakeOverAppend(ref mimeStringList);
					value.AppendFragment(new MimeString(this.bytes, num, this.position - num));
				}
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001DBFC File Offset: 0x0001BDFC
		public void ParseDomainLiteral(bool save, ref MimeStringList domain)
		{
			bool flag = false;
			int num = this.position;
			this.position++;
			for (;;)
			{
				if (this.position == this.end)
				{
					if (num != this.position && save)
					{
						domain.AppendFragment(new MimeString(this.bytes, num, this.position - num));
					}
					if (!this.ParseNextLine())
					{
						break;
					}
					num = this.position;
				}
				byte b = this.bytes[this.position++];
				if (flag)
				{
					flag = false;
				}
				else if (b == 92)
				{
					flag = true;
				}
				else if (b == 93)
				{
					goto IL_91;
				}
			}
			num = this.position;
			IL_91:
			if (num != this.position && save)
			{
				domain.AppendFragment(new MimeString(this.bytes, num, this.position - num));
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001DCC0 File Offset: 0x0001BEC0
		public void ParseToEnd(ref MimeStringList phrase)
		{
			if (this.position != this.end)
			{
				phrase.AppendFragment(new MimeString(this.bytes, this.position, this.end - this.position));
				this.position = this.end;
			}
			while (this.ParseNextLine())
			{
				phrase.AppendFragment(new MimeString(this.bytes, this.start, this.end - this.start));
				this.position = this.end;
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001DD45 File Offset: 0x0001BF45
		public void ParseAppendLastByte(ref MimeStringList phrase)
		{
			phrase.AppendFragment(new MimeString(this.bytes, this.position - 1, 1));
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001DD64 File Offset: 0x0001BF64
		public void ParseAppendSpace(ref MimeStringList phrase)
		{
			if (this.position == this.start || this.bytes[this.position - 1] != 32)
			{
				phrase.AppendFragment(ValueParser.SpaceLine);
				return;
			}
			phrase.AppendFragment(new MimeString(this.bytes, this.position - 1, 1));
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		private void ParseEscapedString(bool save, ref MimeStringList outStr, out bool singleByte)
		{
			bool flag = this.bytes[this.position] == 27;
			if (save)
			{
				outStr.AppendFragment(new MimeString(this.bytes, this.position, 1, 536870912U));
			}
			this.position++;
			if (flag && !this.ParseEscapeSequence(save, ref outStr))
			{
				singleByte = true;
				return;
			}
			singleByte = false;
			do
			{
				int num = MimeScan.ScanJISString(this.bytes, this.position, this.end - this.position, ref singleByte);
				if (save && num != 0)
				{
					outStr.AppendFragment(new MimeString(this.bytes, this.position, num, 536870912U));
				}
				this.position += num;
			}
			while (!singleByte && this.ParseNextLine());
			if (!flag && this.position != this.end && this.bytes[this.position] == 15)
			{
				if (save)
				{
					outStr.AppendFragment(new MimeString(this.bytes, this.position, 1, 536870912U));
				}
				this.position++;
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001DEC8 File Offset: 0x0001C0C8
		private bool ParseEscapeSequence(bool save, ref MimeStringList outStr)
		{
			byte b = this.ParseGet();
			byte b2 = this.ParseGet();
			byte b3 = this.ParseGet();
			if (b3 != 0)
			{
				this.ParseUnget();
			}
			if (b2 != 0)
			{
				this.ParseUnget();
			}
			if (b != 0)
			{
				this.ParseUnget();
			}
			int num = 0;
			bool result = false;
			byte b4 = b;
			if (b4 != 36)
			{
				if (b4 != 40)
				{
					switch (b4)
					{
					case 78:
					case 79:
						if (b2 >= 33)
						{
							num = 2;
							if (b3 >= 33)
							{
								num = 3;
							}
						}
						break;
					}
				}
				else if (b2 == 73)
				{
					result = true;
					num = 2;
				}
				else if (b2 == 66 || b2 == 74 || b2 == 72)
				{
					num = 2;
				}
			}
			else if (b2 == 66 || b2 == 65 || b2 == 64)
			{
				num = 2;
				result = true;
			}
			else if (b2 == 40 && (b3 == 67 || b3 == 68))
			{
				num = 3;
				result = true;
			}
			while (num-- != 0)
			{
				this.ParseGet();
				if (save)
				{
					outStr.AppendFragment(new MimeString(this.bytes, this.position - 1, 1, 536870912U));
				}
			}
			return result;
		}

		// Token: 0x040003D4 RID: 980
		private static readonly MimeString SpaceLine = new MimeString(" ");

		// Token: 0x040003D5 RID: 981
		private MimeStringList lines;

		// Token: 0x040003D6 RID: 982
		private int nextLine;

		// Token: 0x040003D7 RID: 983
		private byte[] bytes;

		// Token: 0x040003D8 RID: 984
		private int start;

		// Token: 0x040003D9 RID: 985
		private int end;

		// Token: 0x040003DA RID: 986
		private int position;

		// Token: 0x040003DB RID: 987
		private readonly bool allowUTF8;
	}
}
