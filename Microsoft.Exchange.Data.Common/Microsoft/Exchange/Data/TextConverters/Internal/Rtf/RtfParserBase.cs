using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000252 RID: 594
	internal class RtfParserBase
	{
		// Token: 0x0600187F RID: 6271 RVA: 0x000C4560 File Offset: 0x000C2760
		public RtfParserBase(int inputBufferSize, bool testBoundaryConditions, IReportBytes reportBytes)
		{
			this.parseBuffer = new byte[1 + (testBoundaryConditions ? 133 : Math.Min(32767, inputBufferSize))];
			this.reportBytes = reportBytes;
			this.InitializeBase();
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x000C4598 File Offset: 0x000C2798
		public RtfParserBase(int inputBufferSize, bool testBoundaryConditions, RtfParserBase previewParser, IReportBytes reportBytes)
		{
			int num = 1 + (testBoundaryConditions ? 133 : Math.Min(32767, inputBufferSize));
			if (previewParser.ParseBuffer.Length < num)
			{
				this.parseBuffer = new byte[1 + (testBoundaryConditions ? 133 : Math.Min(32767, inputBufferSize))];
				Buffer.BlockCopy(previewParser.ParseBuffer, 0, this.parseBuffer, 0, previewParser.ParseEnd);
			}
			else
			{
				this.parseBuffer = previewParser.ParseBuffer;
			}
			this.parseEnd = previewParser.ParseEnd;
			this.endOfFileVisible = previewParser.EndOfFileVisible;
			this.reportBytes = reportBytes;
			this.InitializeBase();
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x000C463D File Offset: 0x000C283D
		public byte[] ParseBuffer
		{
			get
			{
				return this.parseBuffer;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x000C4645 File Offset: 0x000C2845
		public int ParseStart
		{
			get
			{
				return this.parseStart;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x000C464D File Offset: 0x000C284D
		public int ParseOffset
		{
			get
			{
				return this.parseOffset;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x000C4655 File Offset: 0x000C2855
		public int ParseEnd
		{
			get
			{
				return this.parseEnd;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x000C465D File Offset: 0x000C285D
		public bool ParseBufferFull
		{
			get
			{
				return this.parseEnd + 128 >= this.parseBuffer.Length;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x000C4678 File Offset: 0x000C2878
		public bool EndOfFile
		{
			get
			{
				return this.endOfFileVisible && this.parseOffset == this.parseEnd;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x000C4692 File Offset: 0x000C2892
		public bool EndOfFileVisible
		{
			get
			{
				return this.endOfFileVisible;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x000C469A File Offset: 0x000C289A
		public bool ParseBufferNeedsRefill
		{
			get
			{
				return this.parseEnd - this.parseOffset < 128;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x000C46B0 File Offset: 0x000C28B0
		public RtfRunKind RunKind
		{
			get
			{
				return this.run.Kind;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x000C46BD File Offset: 0x000C28BD
		public short KeywordId
		{
			get
			{
				return this.run.KeywordId;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x000C46CA File Offset: 0x000C28CA
		public int KeywordValue
		{
			get
			{
				return this.run.Value;
			}
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x000C46D8 File Offset: 0x000C28D8
		public int GetBufferSpace(bool compact, out int offset)
		{
			if (compact && this.parseStart != 0 && (this.parseEnd - this.parseStart < 128 || this.parseEnd + 128 > this.parseBuffer.Length - 1))
			{
				if (this.parseEnd != this.parseStart)
				{
					Buffer.BlockCopy(this.parseBuffer, this.parseStart, this.parseBuffer, 0, this.parseEnd - this.parseStart);
				}
				this.parseOffset -= this.parseStart;
				this.parseEnd -= this.parseStart;
				this.parseStart = 0;
			}
			offset = this.parseEnd;
			return this.parseBuffer.Length - 1 - this.parseEnd;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x000C479B File Offset: 0x000C299B
		public void ReportMoreDataAvailable(int length, bool endOfFileVisible)
		{
			this.parseEnd += length;
			this.parseBuffer[this.parseEnd] = 0;
			this.endOfFileVisible = endOfFileVisible;
			if (this.reportBytes != null)
			{
				this.reportBytes.ReportBytesRead(length);
			}
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x000C47D4 File Offset: 0x000C29D4
		public bool ParseRun()
		{
			if (this.parseEnd == this.parseOffset)
			{
				if (this.endOfFileVisible)
				{
					this.run.Initialize(RtfRunKind.EndOfFile, 0, 0);
					return true;
				}
				return false;
			}
			else
			{
				if (this.binLength != 0)
				{
					int num = Math.Min(this.parseEnd - this.parseOffset, this.binLength);
					this.run.Initialize(RtfRunKind.Binary, num, 0, 0 != this.bytesToSkip, false);
					this.binLength -= num;
					this.parseOffset += num;
					if (this.binLength == 0 && this.bytesToSkip != 0)
					{
						this.bytesToSkip--;
					}
					return true;
				}
				int num2 = this.parseOffset;
				byte b = this.parseBuffer[num2];
				byte b2 = b;
				switch (b2)
				{
				case 9:
					if (!this.lastLeadByte)
					{
						this.run.InitializeKeyword(126, 0, 1, this.SkipIfNecessary(1), this.firstKeyword);
						this.parseOffset++;
						this.firstKeyword = false;
						return true;
					}
					break;
				case 10:
				case 13:
					do
					{
						b = this.parseBuffer[++num2];
					}
					while (b == 13 || b == 10);
					this.run.Initialize(RtfRunKind.Ignore, num2 - this.parseOffset, 0, false, false);
					this.parseOffset = num2;
					return true;
				case 11:
				case 12:
					break;
				default:
					if (b2 != 92)
					{
						switch (b2)
						{
						case 123:
							if (!this.lastLeadByte)
							{
								this.run.Initialize(RtfRunKind.Begin, 1, 0);
								this.parseOffset++;
								this.firstKeyword = true;
								this.bytesToSkip = 0;
								return true;
							}
							break;
						case 125:
							if (!this.lastLeadByte)
							{
								this.run.Initialize(RtfRunKind.End, 1, 0);
								this.parseOffset++;
								this.firstKeyword = false;
								this.bytesToSkip = 0;
								return true;
							}
							break;
						}
					}
					else
					{
						if (this.ParseKeywordRun())
						{
							this.firstKeyword = false;
							return true;
						}
						return false;
					}
					break;
				}
				this.EnsureCodePage();
				this.firstKeyword = false;
				return this.ParseTextRun();
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x000C49E8 File Offset: 0x000C2BE8
		protected void ReportConsumed(int length)
		{
			this.parseStart += length;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x000C49F8 File Offset: 0x000C2BF8
		protected void InitializeBase()
		{
			this.parseStart = 0;
			this.parseOffset = 0;
			this.bytesSkipForUnicodeEscape = 1;
			this.firstKeyword = false;
			this.bytesToSkip = 0;
			this.binLength = 0;
			this.defaultCodePage = 0;
			this.currentCodePage = 0;
			this.currentTextMapping = TextMapping.Unicode;
			this.leadMask = (DbcsLeadBits)0;
			this.lastLeadByte = false;
			this.currentCharRep = RtfSupport.CharRep.DEFAULT_INDEX;
			this.run.Reset();
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x000C4A65 File Offset: 0x000C2C65
		protected void SetCodePage(ushort codePage, TextMapping textMapping)
		{
			if (codePage != this.currentCodePage)
			{
				this.currentCodePage = codePage;
				this.leadMask = ParseSupport.GetCodePageLeadMask((int)codePage);
			}
			if (textMapping != this.currentTextMapping)
			{
				this.currentTextMapping = textMapping;
			}
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x000C4A93 File Offset: 0x000C2C93
		private void EnsureCodePage()
		{
			if (this.currentCodePage == 0)
			{
				this.SetCodePage((this.defaultCodePage == 0) ? 1252 : this.defaultCodePage, TextMapping.Unicode);
			}
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000C4ABC File Offset: 0x000C2CBC
		private bool ParseKeywordRun()
		{
			if (1 != this.parseEnd - this.parseOffset)
			{
				byte b = this.parseBuffer[this.parseOffset + 1];
				char c = (char)b;
				int num;
				if (c > '-')
				{
					if (c <= '\\')
					{
						if (c == ':')
						{
							this.lastLeadByte = false;
							this.run.InitializeKeyword(3, 0, 2, this.SkipIfNecessary(1), this.firstKeyword);
							this.parseOffset += 2;
							return true;
						}
						if (c != '\\')
						{
							goto IL_CE;
						}
					}
					else
					{
						if (c != '_')
						{
							switch (c)
							{
							case '{':
							case '}':
								goto IL_EA;
							case '|':
								this.lastLeadByte = false;
								this.run.InitializeKeyword(2, 0, 2, this.SkipIfNecessary(1), this.firstKeyword);
								this.parseOffset += 2;
								return true;
							case '~':
								num = 160;
								goto IL_155;
							}
							goto IL_CE;
						}
						num = 8209;
						goto IL_155;
					}
					IL_EA:
					num = (int)b;
					goto IL_EC;
				}
				if (c <= '\'')
				{
					switch (c)
					{
					case '\t':
						this.lastLeadByte = false;
						this.run.InitializeKeyword(126, 0, 2, this.SkipIfNecessary(1), this.firstKeyword);
						this.parseOffset += 2;
						return true;
					case '\n':
					case '\r':
						this.lastLeadByte = false;
						this.run.InitializeKeyword(68, 0, 2, this.SkipIfNecessary(1), this.firstKeyword);
						this.parseOffset += 2;
						return true;
					case '\v':
					case '\f':
						break;
					default:
						if (c == '\'')
						{
							this.EnsureCodePage();
							if (this.parseEnd - this.parseOffset >= 4)
							{
								num = RtfSupport.Unescape(this.parseBuffer[this.parseOffset + 2], this.parseBuffer[this.parseOffset + 3]);
								if (num > 255)
								{
									if (this.lastLeadByte)
									{
										this.lastLeadByte = false;
										this.run.Initialize(RtfRunKind.Text, 1, 0, this.SkipIfNecessary(1), false);
										this.parseOffset++;
										return true;
									}
									num = 63;
								}
								else
								{
									if ((num == 13 || num == 10) && !this.lastLeadByte)
									{
										this.run.InitializeKeyword(68, 0, 4, this.SkipIfNecessary(1), this.firstKeyword);
										this.parseOffset += 4;
										return true;
									}
									if (num == 0)
									{
										num = 32;
									}
									this.lastLeadByte = (!this.lastLeadByte && ParseSupport.IsLeadByte((byte)num, this.leadMask));
								}
								this.run.Initialize(RtfRunKind.Escape, 4, num, this.SkipIfNecessary(1), this.lastLeadByte);
								this.parseOffset += 4;
								return true;
							}
							if (this.endOfFileVisible)
							{
								this.run.Initialize(RtfRunKind.Text, 1, 0, this.SkipIfNecessary(1), false);
								this.parseOffset++;
								this.lastLeadByte = false;
								return true;
							}
							return false;
						}
						break;
					}
				}
				else
				{
					if (c == '*')
					{
						this.lastLeadByte = false;
						this.run.InitializeKeyword(1, 0, 2, this.SkipIfNecessary(1), this.firstKeyword);
						this.parseOffset += 2;
						return true;
					}
					if (c == '-')
					{
						num = 173;
						goto IL_155;
					}
				}
				IL_CE:
				CharClass charClass = ParseSupport.GetCharClass(b);
				if (ParseSupport.AlphaCharacter(charClass))
				{
					this.lastLeadByte = false;
					return this.ParseKeyword(b);
				}
				num = (int)b;
				if (num == 0)
				{
					num = 32;
				}
				IL_EC:
				this.EnsureCodePage();
				this.lastLeadByte = (!this.lastLeadByte && ParseSupport.IsLeadByte((byte)num, this.leadMask));
				this.run.Initialize(RtfRunKind.Escape, 2, num, this.SkipIfNecessary(1), this.lastLeadByte);
				this.parseOffset += 2;
				return true;
				IL_155:
				this.EnsureCodePage();
				this.lastLeadByte = false;
				this.run.Initialize(RtfRunKind.Unicode, 2, num, this.SkipIfNecessary(1), false);
				this.parseOffset += 2;
				return true;
			}
			if (this.endOfFileVisible)
			{
				this.run.Initialize(RtfRunKind.Text, 1, 0, this.SkipIfNecessary(1), false);
				this.parseOffset++;
				return true;
			}
			return false;
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000C4EAC File Offset: 0x000C30AC
		private bool ParseKeyword(byte ch)
		{
			int num = this.parseOffset + 1;
			short hash = 0;
			do
			{
				hash = RTFData.AddHash(hash, ch);
				ch = this.parseBuffer[++num];
			}
			while ((ch | 32) - 97 <= 25);
			int num2 = num - (this.parseOffset + 1);
			bool flag = false;
			bool flag2 = false;
			int num3 = 0;
			if (ch == 45)
			{
				flag = true;
				flag2 = true;
				num++;
				ch = this.parseBuffer[num];
			}
			if (ch - 48 <= 9)
			{
				flag = true;
				do
				{
					num3 = num3 * 10 + (int)(ch - 48);
					ch = this.parseBuffer[++num];
				}
				while (ch - 48 <= 9);
				if (flag2)
				{
					num3 = -num3;
				}
			}
			if (num > this.parseOffset + 128 - 1)
			{
				num = this.parseOffset + 128 - 1;
				ch = this.parseBuffer[num];
				num3 = 0;
				num2 = Math.Min(num2, num - (this.parseOffset + 1));
			}
			if (ch == 32)
			{
				num++;
			}
			else if (ch == 0 && num == this.parseEnd && !this.endOfFileVisible)
			{
				return false;
			}
			int num4 = 0;
			if (num2 != 1 || (this.parseBuffer[this.parseOffset + 1] | 32) != 117)
			{
				short num5 = this.LookupKeyword(hash, this.parseOffset + 1, num2);
				if (RTFData.keywords[(int)num5].character == '\0')
				{
					if (!flag)
					{
						num3 = (int)RTFData.keywords[(int)num5].defaultValue;
					}
					if (num5 == 47)
					{
						this.binLength = ((num3 > 0) ? num3 : 0);
					}
					bool skip = (num5 != 47 || this.binLength == 0) && this.SkipIfNecessary(1);
					this.run.InitializeKeyword(num5, num3, num - this.parseOffset, skip, this.firstKeyword);
					this.parseOffset = num;
					return true;
				}
				num3 = (int)RTFData.keywords[(int)num5].character;
			}
			else
			{
				num4 = (int)this.bytesSkipForUnicodeEscape;
				if (num3 < 0)
				{
					num3 &= 65535;
				}
				else if (num3 > 1114111)
				{
					num3 = 63;
				}
				if (this.currentCharRep == RtfSupport.CharRep.SYMBOL_INDEX && 61440 <= num3 && num3 <= 61695)
				{
					num3 -= 61440;
				}
				if (num3 == 0)
				{
					num3 = 32;
				}
			}
			this.run.Initialize(RtfRunKind.Unicode, num - this.parseOffset, num3, this.SkipIfNecessary(1), ParseSupport.IsHighSurrogate((char)num3));
			this.parseOffset = num;
			this.bytesToSkip += num4;
			return true;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x000C5100 File Offset: 0x000C3300
		private bool ParseTextRun()
		{
			int num = this.parseOffset;
			int num2 = this.parseEnd;
			byte b = this.parseBuffer[num];
			if (this.bytesToSkip != 0)
			{
				num2 = Math.Min(num2, num + this.bytesToSkip);
			}
			bool skip;
			RtfRunKind kind;
			if (b == 0)
			{
				this.lastLeadByte = false;
				do
				{
					b = this.parseBuffer[++num];
				}
				while (b == 0 && num != num2);
				CharClass charClass = ParseSupport.GetCharClass(b);
				skip = this.SkipIfNecessary(num - this.parseOffset);
				kind = RtfRunKind.Zero;
			}
			else if (this.leadMask == (DbcsLeadBits)0)
			{
				this.lastLeadByte = false;
				if (this.bytesToSkip == 0)
				{
					CharClass charClass;
					do
					{
						b = this.parseBuffer[++num];
						charClass = ParseSupport.GetCharClass(b);
					}
					while (!ParseSupport.RtfInterestingCharacter(charClass));
					skip = false;
				}
				else
				{
					CharClass charClass;
					do
					{
						b = this.parseBuffer[++num];
						charClass = ParseSupport.GetCharClass(b);
					}
					while (num != num2 && !ParseSupport.RtfInterestingCharacter(charClass));
					skip = this.SkipIfNecessary(num - this.parseOffset);
				}
				kind = RtfRunKind.Text;
			}
			else
			{
				for (;;)
				{
					this.lastLeadByte = (!this.lastLeadByte && ParseSupport.IsLeadByte(b, this.leadMask));
					b = this.parseBuffer[++num];
					CharClass charClass = ParseSupport.GetCharClass(b);
					if (num == num2 || ParseSupport.RtfInterestingCharacter(charClass))
					{
						if (!this.lastLeadByte)
						{
							goto IL_181;
						}
						if (num == num2 || (b != 123 && b != 125))
						{
							break;
						}
					}
				}
				if (num - this.parseOffset > 1)
				{
					num--;
					this.lastLeadByte = false;
					b = this.parseBuffer[num];
					CharClass charClass = ParseSupport.GetCharClass(b);
				}
				else if (num == num2 && !this.endOfFileVisible && num2 == this.parseEnd)
				{
					this.lastLeadByte = false;
					return false;
				}
				IL_181:
				skip = this.SkipIfNecessary(num - this.parseOffset);
				kind = RtfRunKind.Text;
			}
			this.run.Initialize(kind, num - this.parseOffset, 0, skip, this.lastLeadByte);
			this.parseOffset = num;
			return true;
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x000C52C9 File Offset: 0x000C34C9
		private bool SkipIfNecessary(int length)
		{
			if (this.bytesToSkip != 0)
			{
				this.bytesToSkip -= length;
				return true;
			}
			return false;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000C52E4 File Offset: 0x000C34E4
		private short LookupKeyword(short hash, int nameOffset, int nameLength)
		{
			short num = RTFData.keywordHashTable[(int)hash];
			if (num != 0)
			{
				bool flag = false;
				for (;;)
				{
					if (RTFData.keywords[(int)num].name.Length == nameLength && RTFData.keywords[(int)num].name[0] == (char)this.parseBuffer[nameOffset])
					{
						int num2 = 1;
						while (num2 < nameLength && RTFData.keywords[(int)num].name[num2] == (char)this.parseBuffer[nameOffset + num2])
						{
							num2++;
						}
						if (num2 == nameLength)
						{
							break;
						}
					}
					if ((int)(num += 1) >= RTFData.keywords.Length || RTFData.keywords[(int)num].hash != hash)
					{
						goto IL_A3;
					}
				}
				flag = true;
				IL_A3:
				if (!flag)
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x04001CF6 RID: 7414
		public const int ReadThreshold = 128;

		// Token: 0x04001CF7 RID: 7415
		protected static readonly short TwelvePointsInTwips = 240;

		// Token: 0x04001CF8 RID: 7416
		protected RtfRunEntry run;

		// Token: 0x04001CF9 RID: 7417
		protected ushort defaultCodePage;

		// Token: 0x04001CFA RID: 7418
		protected ushort currentCodePage;

		// Token: 0x04001CFB RID: 7419
		protected TextMapping currentTextMapping;

		// Token: 0x04001CFC RID: 7420
		protected RtfSupport.CharRep currentCharRep;

		// Token: 0x04001CFD RID: 7421
		protected bool lastLeadByte;

		// Token: 0x04001CFE RID: 7422
		protected byte bytesSkipForUnicodeEscape;

		// Token: 0x04001CFF RID: 7423
		private byte[] parseBuffer;

		// Token: 0x04001D00 RID: 7424
		private int parseStart;

		// Token: 0x04001D01 RID: 7425
		private int parseOffset;

		// Token: 0x04001D02 RID: 7426
		private int parseEnd;

		// Token: 0x04001D03 RID: 7427
		private bool endOfFileVisible;

		// Token: 0x04001D04 RID: 7428
		private bool firstKeyword;

		// Token: 0x04001D05 RID: 7429
		private int bytesToSkip;

		// Token: 0x04001D06 RID: 7430
		private int binLength;

		// Token: 0x04001D07 RID: 7431
		private DbcsLeadBits leadMask;

		// Token: 0x04001D08 RID: 7432
		private IReportBytes reportBytes;
	}
}
