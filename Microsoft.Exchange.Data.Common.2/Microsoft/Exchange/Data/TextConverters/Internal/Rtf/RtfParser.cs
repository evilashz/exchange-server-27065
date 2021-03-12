using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000253 RID: 595
	internal class RtfParser : RtfParserBase
	{
		// Token: 0x06001899 RID: 6297 RVA: 0x000C53A8 File Offset: 0x000C35A8
		public RtfParser(Stream input, bool push, int inputBufferSize, bool testBoundaryConditions, IProgressMonitor progressMonitor, IReportBytes reportBytes) : base(inputBufferSize, testBoundaryConditions, reportBytes)
		{
			this.progressMonitor = progressMonitor;
			if (push)
			{
				this.pushSource = (ConverterStream)input;
			}
			else
			{
				this.pullSource = input;
			}
			this.runQueue = new RtfRunEntry[testBoundaryConditions ? 15 : 256];
			this.token = new RtfToken(base.ParseBuffer, this.runQueue);
			this.fonts = new RtfParser.RtfParserFont[testBoundaryConditions ? 5 : 16];
			this.fontDirectory = new short[testBoundaryConditions ? 9 : 65];
			this.Initialize();
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x000C5440 File Offset: 0x000C3640
		internal RtfParser(Stream input, int inputBufferSize, bool testBoundaryConditions, IProgressMonitor progressMonitor, RtfParserBase previewParser, IReportBytes reportBytes) : base(inputBufferSize, testBoundaryConditions, previewParser, reportBytes)
		{
			this.progressMonitor = progressMonitor;
			this.pullSource = input;
			this.runQueue = new RtfRunEntry[testBoundaryConditions ? 15 : 256];
			this.token = new RtfToken(base.ParseBuffer, this.runQueue);
			this.fonts = new RtfParser.RtfParserFont[testBoundaryConditions ? 5 : 16];
			this.fontDirectory = new short[testBoundaryConditions ? 9 : 65];
			this.Initialize();
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x000C54C3 File Offset: 0x000C36C3
		public RtfToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x000C54CB File Offset: 0x000C36CB
		public short CurrentFontIndex
		{
			get
			{
				return this.state.FontIndex;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x000C54D8 File Offset: 0x000C36D8
		public short CurrentFontSize
		{
			get
			{
				return this.state.FontSize;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x000C54E5 File Offset: 0x000C36E5
		public bool CurrentFontBold
		{
			get
			{
				return this.state.Bold;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x000C54F2 File Offset: 0x000C36F2
		public bool CurrentFontItalic
		{
			get
			{
				return this.state.Italic;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x000C54FF File Offset: 0x000C36FF
		public bool CurrentRunBiDi
		{
			get
			{
				return this.state.BiDi;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x000C550C File Offset: 0x000C370C
		public bool CurrentRunComplexScript
		{
			get
			{
				return this.state.ComplexScript;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x000C5519 File Offset: 0x000C3719
		public ushort CurrentCodePage
		{
			get
			{
				return this.currentCodePage;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x000C5521 File Offset: 0x000C3721
		public TextMapping CurrentTextMapping
		{
			get
			{
				return this.currentTextMapping;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x000C5529 File Offset: 0x000C3729
		public RtfSupport.CharRep CurrentCharRep
		{
			get
			{
				return this.currentCharRep;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x000C5531 File Offset: 0x000C3731
		public short CurrentLanguage
		{
			get
			{
				return this.state.Language;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x000C553E File Offset: 0x000C373E
		public short DefaultLanguage
		{
			get
			{
				return this.state.DefaultLanguage;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x000C554B File Offset: 0x000C374B
		public short DefaultLanguageFE
		{
			get
			{
				return this.state.DefaultLanguageFE;
			}
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x000C5558 File Offset: 0x000C3758
		public void Restart()
		{
			base.InitializeBase();
			this.Initialize();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000C5568 File Offset: 0x000C3768
		public short FontIndex(short fontHandle)
		{
			if (fontHandle < 0)
			{
				return -1;
			}
			if ((int)fontHandle < this.fontDirectory.Length)
			{
				return this.fontDirectory[(int)fontHandle];
			}
			for (int i = (int)(this.fontsCount - 1); i >= 0; i--)
			{
				if (this.fonts[i].Handle == fontHandle)
				{
					return (short)i;
				}
			}
			return -1;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x000C55BA File Offset: 0x000C37BA
		public int FontCodePage(short fontIndex)
		{
			return (int)this.fonts[(int)fontIndex].CodePage;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x000C55CD File Offset: 0x000C37CD
		public RtfSupport.CharRep FontCharRep(short fontIndex)
		{
			return this.fonts[(int)fontIndex].CharRep;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000C55E0 File Offset: 0x000C37E0
		public short FontHandle(short fontIndex)
		{
			return this.fonts[(int)fontIndex].Handle;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000C55F3 File Offset: 0x000C37F3
		public RtfFontFamily FontFamily(short fontIndex)
		{
			return this.fonts[(int)fontIndex].Family;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x000C5606 File Offset: 0x000C3806
		public RtfFontPitch FontPitch(short fontIndex)
		{
			return this.fonts[(int)fontIndex].Pitch;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x000C561C File Offset: 0x000C381C
		public RtfTokenId Parse()
		{
			RtfTokenId rtfTokenId = RtfTokenId.None;
			this.runQueueTail = 0;
			int length;
			if (this.overflowRun)
			{
				rtfTokenId = RtfToken.TokenIdFromRunKind(this.run.Kind);
				this.runQueue[this.runQueueTail++] = this.run;
				this.overflowRun = false;
				if (this.run.Kind == RtfRunKind.Begin)
				{
					this.state.Push();
				}
				else if (this.run.Kind == RtfRunKind.End)
				{
					this.PopState();
				}
				else if (this.run.Kind == RtfRunKind.Keyword && RTFData.keywords[(int)this.run.KeywordId].affectsParsing)
				{
					this.ProcessInterestingKeyword();
				}
				if (rtfTokenId <= RtfTokenId.Binary)
				{
					length = base.ParseOffset - base.ParseStart;
					this.token.Initialize(rtfTokenId, this.runQueueTail, base.ParseStart, base.ParseOffset - base.ParseStart);
					base.ReportConsumed(length);
					return rtfTokenId;
				}
			}
			if (!base.EndOfFileVisible && base.ParseBufferNeedsRefill && !this.ReadMoreData(true))
			{
				if (this.runQueueTail != 0)
				{
					this.overflowRun = true;
				}
				this.token.Reset();
				return RtfTokenId.None;
			}
			ushort num = 0;
			while (this.CanQueueRun() && base.ParseRun())
			{
				RtfTokenId rtfTokenId2 = RtfToken.TokenIdFromRunKind(this.run.Kind);
				if (rtfTokenId == RtfTokenId.None)
				{
					rtfTokenId = rtfTokenId2;
				}
				else if (rtfTokenId2 != rtfTokenId && rtfTokenId2 != RtfTokenId.None)
				{
					this.overflowRun = true;
					num = this.run.Length;
					break;
				}
				this.runQueue[this.runQueueTail++] = this.run;
				if (this.run.Kind == RtfRunKind.Begin)
				{
					this.state.Push();
				}
				else if (this.run.Kind == RtfRunKind.End)
				{
					this.PopState();
				}
				else if (this.run.Kind == RtfRunKind.Keyword && RTFData.keywords[(int)this.run.KeywordId].affectsParsing)
				{
					this.ProcessInterestingKeyword();
				}
				if (rtfTokenId <= RtfTokenId.Binary)
				{
					break;
				}
			}
			if (rtfTokenId == RtfTokenId.None)
			{
				rtfTokenId = RtfTokenId.Keywords;
			}
			else if (rtfTokenId == RtfTokenId.Text)
			{
				this.token.SetCodePage((int)this.currentCodePage, this.currentTextMapping);
				if (this.state.Destination == RtfParser.RtfParserDestination.FontTable && !this.fontNameRecognizer.IsRejected)
				{
					int num2 = base.ParseStart;
					for (int i = 0; i < this.runQueueTail; i++)
					{
						if (this.runQueue[i].Kind == RtfRunKind.Text)
						{
							for (int j = 0; j < (int)this.runQueue[i].Length; j++)
							{
								this.fontNameRecognizer.AddCharacter(base.ParseBuffer[num2 + j]);
							}
						}
						else if (this.run.Kind != RtfRunKind.Ignore && this.run.Kind != RtfRunKind.Zero)
						{
							this.fontNameRecognizer.AddCharacter(byte.MaxValue);
						}
						num2 += (int)this.runQueue[i].Length;
					}
				}
				else if (!this.overflowRun && this.lastLeadByte)
				{
					int num3 = this.runQueueTail - 1;
					if (this.runQueue[num3].Kind != RtfRunKind.Ignore)
					{
						this.overflowRun = true;
						num = this.run.Length;
						this.runQueueTail--;
					}
				}
			}
			length = base.ParseOffset - (int)num - base.ParseStart;
			this.token.Initialize(rtfTokenId, this.runQueueTail, base.ParseStart, length);
			base.ReportConsumed(length);
			return rtfTokenId;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x000C59D4 File Offset: 0x000C3BD4
		internal bool ReadMoreData(bool compact)
		{
			int num;
			int bufferSpace = base.GetBufferSpace(compact, out num);
			if (bufferSpace == 0)
			{
				return true;
			}
			bool endOfFileVisible;
			int num2;
			if (this.pushSource != null)
			{
				byte[] src;
				int srcOffset;
				int val;
				if (!this.pushSource.GetInputChunk(out src, out srcOffset, out val, out endOfFileVisible))
				{
					return false;
				}
				num2 = Math.Min(val, bufferSpace);
				if (num2 != 0)
				{
					Buffer.BlockCopy(src, srcOffset, base.ParseBuffer, num, num2);
					this.pushSource.ReportRead(num2);
					this.bytesReadTotal += num2;
				}
			}
			else
			{
				num2 = this.pullSource.Read(base.ParseBuffer, num, bufferSpace);
				if (num2 == 0)
				{
					endOfFileVisible = true;
				}
				else
				{
					endOfFileVisible = false;
					this.bytesReadTotal += num2;
					this.progressMonitor.ReportProgress();
				}
			}
			base.ReportMoreDataAvailable(num2, endOfFileVisible);
			return true;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x000C5A88 File Offset: 0x000C3C88
		private void Initialize()
		{
			this.runQueueTail = 0;
			this.overflowRun = false;
			this.token.Reset();
			this.fonts[0].Initialize(-1, 1252);
			this.fonts[0].Family = RtfFontFamily.Swiss;
			this.fontsCount = 1;
			for (int i = 0; i < this.fontDirectory.Length; i++)
			{
				this.fontDirectory[i] = -1;
			}
			this.state.Initialize();
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x000C5B05 File Offset: 0x000C3D05
		private bool CanQueueRun()
		{
			return this.runQueueTail < this.runQueue.Length - 1;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x000C5B1C File Offset: 0x000C3D1C
		private void PopState()
		{
			if (this.state.Level != 0)
			{
				short fontIndex = this.state.FontIndex;
				RtfParser.RtfParserDestination destination = this.state.Destination;
				this.state.Pop();
				if (destination == RtfParser.RtfParserDestination.FontTable)
				{
					if (this.state.Destination != RtfParser.RtfParserDestination.FontTable)
					{
						if (fontIndex >= 0)
						{
							this.fonts[(int)fontIndex].TextMapping = this.fontNameRecognizer.TextMapping;
							if (this.fonts[(int)fontIndex].TextMapping == TextMapping.Unicode && this.fonts[(int)fontIndex].CharRep == RtfSupport.CharRep.SYMBOL_INDEX)
							{
								this.fonts[(int)fontIndex].TextMapping = TextMapping.OtherSymbol;
							}
						}
						this.fontNameRecognizer = default(RecognizeInterestingFontName);
						this.state.FontIndex = 0;
						this.state.FontIndexAscii = 0;
						if (this.defaultCodePage == 0)
						{
							if (this.state.DefaultLanguageFE != -1 && this.state.DefaultLanguageFE != 1033)
							{
								this.defaultCodePage = RtfSupport.CodePageFromCharRep(RtfSupport.CharRepFromLanguage((int)this.state.DefaultLanguageFE));
							}
							else if (this.state.DefaultLanguage != -1 && this.state.DefaultLanguage != 1033)
							{
								this.defaultCodePage = RtfSupport.CodePageFromCharRep(RtfSupport.CharRepFromLanguage((int)this.state.DefaultLanguage));
							}
						}
						this.SelectCurrentFont();
					}
					else
					{
						this.state.FontIndex = fontIndex;
					}
				}
				base.SetCodePage(this.state.CodePage, this.state.TextMapping);
				this.currentCharRep = this.state.CharRep;
				this.bytesSkipForUnicodeEscape = this.state.BytesSkipForUnicodeEscape;
			}
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x000C5CC4 File Offset: 0x000C3EC4
		private void ProcessInterestingKeyword()
		{
			int num = this.run.Value;
			short keywordId = this.run.KeywordId;
			short num2;
			Culture culture;
			if (keywordId <= 137)
			{
				if (keywordId <= 49)
				{
					if (keywordId <= 30)
					{
						if (keywordId != 1)
						{
							if (keywordId != 18 && keywordId != 30)
							{
								return;
							}
							goto IL_6B1;
						}
						else
						{
							if (this.run.Lead && this.state.Destination == RtfParser.RtfParserDestination.FontTable)
							{
								this.state.Destination = RtfParser.RtfParserDestination.IgnorableDestinationInFontTable;
								return;
							}
							return;
						}
					}
					else if (keywordId <= 37)
					{
						if (keywordId != 34)
						{
							if (keywordId != 37)
							{
								return;
							}
						}
						else
						{
							if (this.state.Destination != RtfParser.RtfParserDestination.FontTable || this.state.FontIndex < 0)
							{
								return;
							}
							this.fonts[(int)this.state.FontIndex].CharRep = RtfSupport.CharRepFromCharSet(num);
							this.fonts[(int)this.state.FontIndex].CodePage = RtfSupport.CodePageFromCharRep(this.fonts[(int)this.state.FontIndex].CharRep);
							if (!this.state.CodePageFixed && this.fonts[(int)this.state.FontIndex].CodePage != 42)
							{
								this.state.CodePage = this.fonts[(int)this.state.FontIndex].CodePage;
								this.state.TextMapping = TextMapping.Unicode;
								if (this.defaultCodePage == 0)
								{
									this.defaultCodePage = this.state.CodePage;
								}
								base.SetCodePage(this.state.CodePage, this.state.TextMapping);
							}
							if (RtfSupport.IsRtlCharSet(num) && !RtfSupport.IsBiDiCharRep(this.state.CharRepBiDi))
							{
								this.state.CharRepBiDi = this.fonts[(int)this.state.FontIndex].CharRep;
								return;
							}
							return;
						}
					}
					else if (keywordId != 42)
					{
						if (keywordId != 49)
						{
							return;
						}
						goto IL_6B1;
					}
					else
					{
						if (this.state.Destination == RtfParser.RtfParserDestination.FontTable && this.state.FontIndex >= 0 && num >= 0 && num <= 2)
						{
							this.fonts[(int)this.state.FontIndex].Pitch = (RtfFontPitch)num;
							return;
						}
						return;
					}
				}
				else if (keywordId <= 81)
				{
					if (keywordId == 60)
					{
						goto IL_357;
					}
					switch (keywordId)
					{
					case 71:
						goto IL_FAD;
					case 72:
						goto IL_6B1;
					default:
						if (keywordId != 81)
						{
							return;
						}
						if (0 <= num && num <= 2 && (int)this.state.BytesSkipForUnicodeEscape != num)
						{
							this.state.BytesSkipForUnicodeEscape = (this.bytesSkipForUnicodeEscape = (byte)num);
							return;
						}
						return;
					}
				}
				else if (keywordId <= 109)
				{
					switch (keywordId)
					{
					case 93:
					case 94:
						goto IL_6B1;
					case 95:
						goto IL_C50;
					default:
						if (keywordId != 109)
						{
							return;
						}
						goto IL_CEA;
					}
				}
				else
				{
					switch (keywordId)
					{
					case 113:
						if (this.state.Destination != RtfParser.RtfParserDestination.FontTable)
						{
							num2 = this.FontIndex((short)num);
							if (num2 == -1)
							{
								num2 = this.state.DefaultFontIndex;
								goto IL_E77;
							}
							goto IL_E77;
						}
						else
						{
							if (this.state.FontIndex >= 0)
							{
								this.fonts[(int)this.state.FontIndex].TextMapping = this.fontNameRecognizer.TextMapping;
								if (this.fonts[(int)this.state.FontIndex].TextMapping == TextMapping.Unicode && this.fonts[(int)this.state.FontIndex].CharRep == RtfSupport.CharRep.SYMBOL_INDEX)
								{
									this.fonts[(int)this.state.FontIndex].TextMapping = TextMapping.OtherSymbol;
								}
							}
							this.fontNameRecognizer = default(RecognizeInterestingFontName);
							if (num < 0)
							{
								return;
							}
							if (num > 32767)
							{
								return;
							}
							this.state.FontIndex = this.FontIndex((short)num);
							if (this.state.FontIndex >= 0)
							{
								return;
							}
							if ((int)this.fontsCount == this.fonts.Length)
							{
								if (this.fontsCount >= 2048)
								{
									throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
								}
								RtfParser.RtfParserFont[] destinationArray = new RtfParser.RtfParserFont[this.fonts.Length * 2];
								Array.Copy(this.fonts, destinationArray, (int)this.fontsCount);
								this.fonts = destinationArray;
							}
							if (this.defaultFontHandle == (short)num)
							{
								this.state.DefaultFontIndex = this.fontsCount;
							}
							short fontIndex;
							this.fontsCount = (fontIndex = this.fontsCount) + 1;
							this.state.FontIndex = fontIndex;
							this.fonts[(int)this.state.FontIndex].Initialize((short)num, this.defaultCodePage);
							this.fonts[(int)this.state.FontIndex].Handle = (short)num;
							if ((int)this.fonts[(int)this.state.FontIndex].Handle < this.fontDirectory.Length)
							{
								this.fontDirectory[(int)this.fonts[(int)this.state.FontIndex].Handle] = this.state.FontIndex;
								return;
							}
							return;
						}
						break;
					case 114:
						return;
					case 115:
						goto IL_D63;
					default:
						switch (keywordId)
						{
						case 133:
						case 135:
						case 136:
							return;
						case 134:
							goto IL_B11;
						case 137:
							goto IL_C50;
						default:
							return;
						}
						break;
					}
				}
			}
			else
			{
				if (keywordId <= 223)
				{
					if (keywordId <= 175)
					{
						if (keywordId != 149)
						{
							switch (keywordId)
							{
							case 164:
								goto IL_B11;
							case 165:
								if (num > 0 && num < 65535 && !this.state.CodePageFixed)
								{
									this.defaultCodePage = (ushort)num;
									this.fonts[0].CodePage = (ushort)num;
									this.state.CodePage = this.defaultCodePage;
									base.SetCodePage(this.defaultCodePage, TextMapping.Unicode);
									return;
								}
								return;
							case 166:
							case 167:
							case 168:
								return;
							case 169:
								break;
							case 170:
								this.state.SetPlain();
								this.SelectCurrentFont();
								return;
							default:
								if (keywordId != 175)
								{
									return;
								}
								if (!this.run.Lead || this.state.Destination != RtfParser.RtfParserDestination.Default)
								{
									return;
								}
								this.state.Destination = RtfParser.RtfParserDestination.FontTable;
								this.state.FontIndex = -1;
								if (this.defaultCodePage != 0 && !this.state.CodePageFixed)
								{
									base.SetCodePage(this.defaultCodePage, TextMapping.Unicode);
									return;
								}
								return;
							}
						}
						else
						{
							if (Culture.TryGetCulture((int)((short)num), out culture) && RtfSupport.IsFECharRep(RtfSupport.CharRepFromLanguage((int)((short)num))))
							{
								this.state.DefaultLanguageFE = (short)num;
								return;
							}
							return;
						}
					}
					else if (keywordId <= 210)
					{
						if (keywordId != 185)
						{
							if (keywordId != 210)
							{
								return;
							}
							if (this.run.Lead && this.state.Destination == RtfParser.RtfParserDestination.FontTable && this.state.FontIndex >= 0)
							{
								this.state.Destination = RtfParser.RtfParserDestination.RealFontName;
								return;
							}
							return;
						}
					}
					else
					{
						if (keywordId == 214)
						{
							goto IL_357;
						}
						switch (keywordId)
						{
						case 222:
							goto IL_C50;
						case 223:
							num2 = this.FontIndex((short)num);
							if (num2 == -1)
							{
								num2 = this.state.DefaultFontIndex;
								goto IL_E77;
							}
							goto IL_E77;
						default:
							return;
						}
					}
				}
				else if (keywordId <= 287)
				{
					if (keywordId <= 271)
					{
						switch (keywordId)
						{
						case 238:
							goto IL_FAD;
						case 239:
							return;
						case 240:
						{
							if (num <= 0 || num >= 65535)
							{
								return;
							}
							Encoding encoding;
							if (num != 42 && !Charset.TryGetEncoding(num, out encoding))
							{
								num = (int)this.defaultCodePage;
							}
							if (!this.state.CodePageFixed)
							{
								this.state.CodePage = (ushort)num;
								this.state.TextMapping = TextMapping.Unicode;
								if (num != 42 || this.state.Destination != RtfParser.RtfParserDestination.FontTable)
								{
									base.SetCodePage((ushort)num, TextMapping.Unicode);
								}
							}
							if (this.state.Destination != RtfParser.RtfParserDestination.FontTable || this.state.FontIndex < 0)
							{
								return;
							}
							this.fonts[(int)this.state.FontIndex].CodePage = (ushort)num;
							this.fonts[(int)this.state.FontIndex].CharRep = RtfSupport.CharRepFromCodePage((ushort)num);
							if (this.defaultCodePage == 0 && num != 42)
							{
								this.defaultCodePage = (ushort)num;
							}
							if (RtfSupport.IsBiDiCharRep(this.fonts[(int)this.state.FontIndex].CharRep) && !RtfSupport.IsBiDiCharRep(this.state.CharRepBiDi))
							{
								this.state.CharRepBiDi = this.fonts[(int)this.state.FontIndex].CharRep;
								return;
							}
							return;
						}
						default:
							switch (keywordId)
							{
							case 265:
								break;
							case 266:
								goto IL_6B1;
							case 267:
							case 269:
							case 270:
								return;
							case 268:
								if (this.run.Lead && this.state.Destination == RtfParser.RtfParserDestination.FontTable && this.state.FontIndex >= 0)
								{
									this.state.Destination = RtfParser.RtfParserDestination.AltFontName;
									return;
								}
								return;
							case 271:
								if (this.state.Level == 1 && this.run.Lead)
								{
									this.state.CodePage = 65001;
									this.state.TextMapping = TextMapping.Unicode;
									this.state.CodePageFixed = true;
									this.defaultCodePage = 65001;
									base.SetCodePage(65001, TextMapping.Unicode);
									return;
								}
								return;
							default:
								return;
							}
							break;
						}
					}
					else
					{
						switch (keywordId)
						{
						case 280:
							if (num < 0 || num > 32767)
							{
								return;
							}
							this.defaultFontHandle = (short)num;
							num2 = this.FontIndex((short)num);
							if (num2 != -1)
							{
								this.state.DefaultFontIndex = num2;
								return;
							}
							if (this.fonts[0].Handle != -1)
							{
								return;
							}
							this.fonts[0].Handle = (short)num;
							if ((int)this.fonts[0].Handle < this.fontDirectory.Length)
							{
								this.fontDirectory[(int)this.fonts[0].Handle] = 0;
								return;
							}
							return;
						case 281:
							goto IL_FAD;
						default:
							if (keywordId != 287)
							{
								return;
							}
							goto IL_B11;
						}
					}
				}
				else if (keywordId <= 308)
				{
					switch (keywordId)
					{
					case 291:
						goto IL_D63;
					case 292:
						goto IL_34B;
					default:
						switch (keywordId)
						{
						case 307:
							goto IL_CEA;
						case 308:
							goto IL_6B1;
						default:
							return;
						}
						break;
					}
				}
				else if (keywordId != 312)
				{
					if (keywordId != 329)
					{
						return;
					}
					goto IL_34B;
				}
				else
				{
					num2 = this.FontIndex((short)num);
					if (num2 == -1)
					{
						num2 = this.state.DefaultFontIndex;
					}
					if (!this.state.InAssocFont && this.state.AssociateRunKind != RtfTextRunKind.None)
					{
						if (this.state.AssociateRunKind == RtfTextRunKind.Dbch && !RtfSupport.IsFECharRep(this.FontCharRep(num2)))
						{
							num2 = this.state.DefaultFontIndex;
						}
						this.state.AssociateFontIndex = num2;
						this.state.AssociateRunKind = RtfTextRunKind.None;
						return;
					}
					goto IL_E77;
				}
				if (num >= 0 && num <= 3276)
				{
					this.state.FontSizeOther = (short)(num * 10);
					this.state.FontSize = this.state.FontSizeOther;
					return;
				}
				return;
			}
			IL_34B:
			this.state.SetDetectSingleChpRtl();
			return;
			IL_357:
			if (Culture.TryGetCulture((int)((short)num), out culture))
			{
				this.state.DefaultLanguage = (short)num;
				return;
			}
			return;
			IL_6B1:
			if (this.state.Destination != RtfParser.RtfParserDestination.FontTable || this.state.FontIndex < 0)
			{
				return;
			}
			this.fonts[(int)this.state.FontIndex].Family = (RtfFontFamily)RTFData.keywords[(int)this.run.KeywordId].idx;
			if (this.run.KeywordId == 266 && this.fonts[(int)this.state.FontIndex].CharRep == RtfSupport.CharRep.DEFAULT_INDEX)
			{
				this.fonts[(int)this.state.FontIndex].CharRep = RtfSupport.CharRep.SYMBOL_INDEX;
				return;
			}
			return;
			IL_B11:
			if (this.run.KeywordId == 164)
			{
				this.state.SetFcs(num != 0);
			}
			else
			{
				this.state.SetRtlch(this.run.KeywordId == 287);
			}
			if (this.state.FillBiProps)
			{
				if (this.state.FontIndexCS != -1)
				{
					this.state.FontIndex = this.state.FontIndexCS;
				}
				if (this.state.FontSizeCS != 0)
				{
					this.state.FontSize = this.state.FontSizeCS;
				}
				this.state.Bold = this.state.BoldCS;
				this.state.Italic = this.state.ItalicCS;
			}
			else
			{
				if (this.state.FontIndexOther != -1)
				{
					this.state.FontIndex = this.state.FontIndexOther;
				}
				if (this.state.FontSizeOther != 0)
				{
					this.state.FontSize = this.state.FontSizeOther;
				}
				this.state.Bold = this.state.BoldOther;
				this.state.Italic = this.state.ItalicOther;
			}
			this.SelectCurrentFont();
			return;
			IL_C50:
			this.state.AssociateRunKind = (RtfTextRunKind)RTFData.keywords[(int)this.run.KeywordId].idx;
			if (this.state.AssociateFontIndex != -1)
			{
				this.state.FontIndex = this.state.AssociateFontIndex;
			}
			if (this.state.FontSizeOther != 0)
			{
				this.state.FontSize = this.state.FontSizeOther;
			}
			this.state.Bold = this.state.BoldOther;
			this.state.Italic = this.state.ItalicOther;
			return;
			IL_CEA:
			if (!this.state.DualChpRtfCS)
			{
				this.state.BoldOther = (0 != num);
				this.state.BoldCS = (0 != num);
			}
			else if (!this.state.FillBiProps)
			{
				this.state.BoldOther = (0 != num);
			}
			else
			{
				this.state.BoldCS = (0 != num);
			}
			this.state.Bold = (0 != num);
			return;
			IL_D63:
			if (!this.state.DualChpRtfCS)
			{
				this.state.ItalicOther = (0 != num);
				this.state.ItalicCS = (0 != num);
			}
			else if (!this.state.FillBiProps)
			{
				this.state.ItalicOther = (0 != num);
			}
			else
			{
				this.state.ItalicCS = (0 != num);
			}
			this.state.Italic = (0 != num);
			return;
			IL_E77:
			if (this.state.AssociateRunKind != RtfTextRunKind.None)
			{
				if (this.state.AssociateRunKind == RtfTextRunKind.Dbch && !RtfSupport.IsFECharRep(this.FontCharRep(num2)))
				{
					num2 = this.state.DefaultFontIndex;
				}
				this.state.AssociateFontIndex = num2;
				if (!this.state.DualChpRtfCS)
				{
					this.state.FontIndexCS = num2;
				}
				this.state.AssociateRunKind = RtfTextRunKind.None;
			}
			else if (this.state.DualChpRtfCS)
			{
				if (this.state.FillBiProps)
				{
					this.state.FontIndexCS = num2;
				}
				else
				{
					this.state.FontIndexAscii = num2;
					if (!RtfSupport.IsFECharRep(this.FontCharRep(num2)))
					{
						this.state.FontIndexOther = num2;
					}
					else
					{
						this.state.FontIndexDbCh = num2;
					}
				}
			}
			else
			{
				if (RtfSupport.IsBiDiCharRep(this.FontCharRep(num2)))
				{
					this.state.FontIndexCS = num2;
				}
				this.state.FontIndexAscii = num2;
				if (!RtfSupport.IsFECharRep(this.FontCharRep(num2)))
				{
					this.state.FontIndexOther = num2;
				}
				else
				{
					this.state.FontIndexDbCh = num2;
				}
			}
			this.state.FontIndex = num2;
			this.SelectCurrentFont();
			return;
			IL_FAD:
			if (num > 0 && num <= 32767 && Culture.TryGetCulture(num, out culture))
			{
				this.state.Language = (short)num;
				if (RtfSupport.IsBiDiLanguage(num))
				{
					this.state.CharRepBiDi = RtfSupport.CharRepFromLanguage(num);
					if (this.state.AssociateRunKind == RtfTextRunKind.Ltrch)
					{
						this.state.AssociateRunKind = RtfTextRunKind.Rtlch;
					}
					if (this.state.CharRepBiDi > RtfSupport.CharRep.NCHARSETS && this.state.FontIndex >= 0)
					{
						this.fonts[(int)this.state.FontIndex].CharRep = this.state.CharRepBiDi;
						return;
					}
				}
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x000C6D54 File Offset: 0x000C4F54
		private void SelectCurrentFont()
		{
			if (this.state.FontIndex == -1)
			{
				this.state.FontIndex = this.state.DefaultFontIndex;
			}
			if (this.state.Destination != RtfParser.RtfParserDestination.FontTable)
			{
				this.state.CharRep = (this.currentCharRep = this.fonts[(int)this.state.FontIndex].CharRep);
				if (this.state.CharRep == RtfSupport.CharRep.DEFAULT_INDEX && this.state.Language != -1)
				{
					RtfSupport.CharRep charRep = RtfSupport.CharRepFromLanguage((int)this.state.Language);
					if (charRep > RtfSupport.CharRep.NCHARSETS)
					{
						this.state.CharRep = (this.currentCharRep = charRep);
						if (charRep == this.state.CharRepBiDi)
						{
							this.state.FontIndexCS = this.state.FontIndex;
						}
					}
				}
				if (RtfSupport.IsBiDiCharRep(this.state.CharRep) && this.fonts[(int)this.state.FontIndex].CodePage == 1252)
				{
					this.fonts[(int)this.state.FontIndex].CodePage = RtfSupport.CodePageFromCharRep(this.state.CharRep);
				}
			}
			if (!this.state.CodePageFixed)
			{
				if (this.fonts[(int)this.state.FontIndex].CodePage == 0)
				{
					this.state.CodePage = this.defaultCodePage;
					this.state.TextMapping = TextMapping.Unicode;
				}
				else
				{
					this.state.CodePage = this.fonts[(int)this.state.FontIndex].CodePage;
					this.state.TextMapping = this.fonts[(int)this.state.FontIndex].TextMapping;
				}
				if (this.state.CodePage <= 0)
				{
					this.state.CodePage = 1252;
					this.state.TextMapping = TextMapping.Unicode;
				}
				else if (this.state.CodePage == 1255)
				{
					if (!RtfSupport.IsHebrewLanguage((int)this.state.Language))
					{
						this.state.Language = 13;
					}
				}
				else if (this.state.CodePage == 1256)
				{
					if (!RtfSupport.IsArabicLanguage((int)this.state.Language))
					{
						this.state.Language = 1;
					}
				}
				else if (this.state.CodePage == 874 && !RtfSupport.IsThaiLanguage((int)this.state.Language))
				{
					this.state.Language = 30;
				}
				if (!this.state.DualChpRtfCS || !this.state.InAssocFont)
				{
					base.SetCodePage(this.state.CodePage, this.state.TextMapping);
				}
			}
		}

		// Token: 0x04001D09 RID: 7433
		private const int UndefinedDefaultFontIndex = 0;

		// Token: 0x04001D0A RID: 7434
		private Stream pullSource;

		// Token: 0x04001D0B RID: 7435
		private ConverterStream pushSource;

		// Token: 0x04001D0C RID: 7436
		private IProgressMonitor progressMonitor;

		// Token: 0x04001D0D RID: 7437
		private int bytesReadTotal;

		// Token: 0x04001D0E RID: 7438
		private RtfRunEntry[] runQueue;

		// Token: 0x04001D0F RID: 7439
		private int runQueueTail;

		// Token: 0x04001D10 RID: 7440
		private bool overflowRun;

		// Token: 0x04001D11 RID: 7441
		private RtfToken token;

		// Token: 0x04001D12 RID: 7442
		private short fontsCount;

		// Token: 0x04001D13 RID: 7443
		private RtfParser.RtfParserFont[] fonts;

		// Token: 0x04001D14 RID: 7444
		private short[] fontDirectory;

		// Token: 0x04001D15 RID: 7445
		private RtfParser.RtfParserState state;

		// Token: 0x04001D16 RID: 7446
		private short defaultFontHandle;

		// Token: 0x04001D17 RID: 7447
		private RecognizeInterestingFontName fontNameRecognizer;

		// Token: 0x02000254 RID: 596
		internal enum RtfParserDestination : byte
		{
			// Token: 0x04001D19 RID: 7449
			Default,
			// Token: 0x04001D1A RID: 7450
			FontTable,
			// Token: 0x04001D1B RID: 7451
			RealFontName,
			// Token: 0x04001D1C RID: 7452
			AltFontName,
			// Token: 0x04001D1D RID: 7453
			IgnorableDestinationInFontTable
		}

		// Token: 0x02000255 RID: 597
		internal struct RtfParserState
		{
			// Token: 0x17000629 RID: 1577
			// (get) Token: 0x060018B6 RID: 6326 RVA: 0x000C7023 File Offset: 0x000C5223
			public bool DualChpRtfCS
			{
				get
				{
					return this.detectAssocSA || (this.detectAssocRtl && !this.detectSingleChpRtl);
				}
			}

			// Token: 0x1700062A RID: 1578
			// (get) Token: 0x060018B7 RID: 6327 RVA: 0x000C7042 File Offset: 0x000C5242
			public bool FillBiProps
			{
				get
				{
					return (this.BiDi && this.ComplexScript) || (this.BiDi && !this.inAssocSA) || (this.ComplexScript && !this.inAssocRtl);
				}
			}

			// Token: 0x1700062B RID: 1579
			// (get) Token: 0x060018B8 RID: 6328 RVA: 0x000C7079 File Offset: 0x000C5279
			public bool InAssocFont
			{
				get
				{
					return this.inAssocRtl || this.inAssocSA;
				}
			}

			// Token: 0x1700062C RID: 1580
			// (get) Token: 0x060018B9 RID: 6329 RVA: 0x000C708B File Offset: 0x000C528B
			// (set) Token: 0x060018BA RID: 6330 RVA: 0x000C7093 File Offset: 0x000C5293
			public bool CodePageFixed
			{
				get
				{
					return this.codePageFixed;
				}
				set
				{
					this.codePageFixed = value;
				}
			}

			// Token: 0x1700062D RID: 1581
			// (get) Token: 0x060018BB RID: 6331 RVA: 0x000C709C File Offset: 0x000C529C
			// (set) Token: 0x060018BC RID: 6332 RVA: 0x000C70A4 File Offset: 0x000C52A4
			public short DefaultFontIndex
			{
				get
				{
					return this.defaultFontIndex;
				}
				set
				{
					this.defaultFontIndex = value;
				}
			}

			// Token: 0x1700062E RID: 1582
			// (get) Token: 0x060018BD RID: 6333 RVA: 0x000C70AD File Offset: 0x000C52AD
			// (set) Token: 0x060018BE RID: 6334 RVA: 0x000C70B5 File Offset: 0x000C52B5
			public short DefaultLanguage
			{
				get
				{
					return this.defaultLanguage;
				}
				set
				{
					this.defaultLanguage = value;
				}
			}

			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x060018BF RID: 6335 RVA: 0x000C70BE File Offset: 0x000C52BE
			// (set) Token: 0x060018C0 RID: 6336 RVA: 0x000C70C6 File Offset: 0x000C52C6
			public short DefaultLanguageFE
			{
				get
				{
					return this.defaultLanguageFE;
				}
				set
				{
					this.defaultLanguageFE = value;
				}
			}

			// Token: 0x17000630 RID: 1584
			// (get) Token: 0x060018C1 RID: 6337 RVA: 0x000C70CF File Offset: 0x000C52CF
			// (set) Token: 0x060018C2 RID: 6338 RVA: 0x000C70D7 File Offset: 0x000C52D7
			public RtfSupport.CharRep CharRepBiDi
			{
				get
				{
					return this.charRepBiDi;
				}
				set
				{
					this.charRepBiDi = value;
				}
			}

			// Token: 0x17000631 RID: 1585
			// (get) Token: 0x060018C3 RID: 6339 RVA: 0x000C70E0 File Offset: 0x000C52E0
			// (set) Token: 0x060018C4 RID: 6340 RVA: 0x000C70ED File Offset: 0x000C52ED
			public RtfParser.RtfParserDestination Destination
			{
				get
				{
					return this.Current.Dest;
				}
				set
				{
					if (this.Current.Dest != value)
					{
						this.SetDirty();
						this.Current.Dest = value;
					}
				}
			}

			// Token: 0x17000632 RID: 1586
			// (get) Token: 0x060018C5 RID: 6341 RVA: 0x000C710F File Offset: 0x000C530F
			// (set) Token: 0x060018C6 RID: 6342 RVA: 0x000C7125 File Offset: 0x000C5325
			public bool BiDi
			{
				get
				{
					return (byte)(this.Current.Flags & RtfParser.RtfParserState.StateFlags.BiDi) != 0;
				}
				set
				{
					if (this.BiDi != value)
					{
						this.SetDirty();
						this.Current.Flags = (this.Current.Flags ^ RtfParser.RtfParserState.StateFlags.BiDi);
					}
				}
			}

			// Token: 0x17000633 RID: 1587
			// (get) Token: 0x060018C7 RID: 6343 RVA: 0x000C714A File Offset: 0x000C534A
			// (set) Token: 0x060018C8 RID: 6344 RVA: 0x000C7160 File Offset: 0x000C5360
			public bool ComplexScript
			{
				get
				{
					return (byte)(this.Current.Flags & RtfParser.RtfParserState.StateFlags.ComplexScript) != 0;
				}
				set
				{
					if (this.ComplexScript != value)
					{
						this.SetDirty();
						this.Current.Flags = (this.Current.Flags ^ RtfParser.RtfParserState.StateFlags.ComplexScript);
					}
				}
			}

			// Token: 0x17000634 RID: 1588
			// (get) Token: 0x060018C9 RID: 6345 RVA: 0x000C7185 File Offset: 0x000C5385
			// (set) Token: 0x060018CA RID: 6346 RVA: 0x000C719B File Offset: 0x000C539B
			public bool Bold
			{
				get
				{
					return (byte)(this.Current.Flags & RtfParser.RtfParserState.StateFlags.Bold) != 0;
				}
				set
				{
					if (this.Bold != value)
					{
						this.SetDirty();
						this.Current.Flags = (this.Current.Flags ^ RtfParser.RtfParserState.StateFlags.Bold);
					}
				}
			}

			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x060018CB RID: 6347 RVA: 0x000C71C0 File Offset: 0x000C53C0
			// (set) Token: 0x060018CC RID: 6348 RVA: 0x000C71D6 File Offset: 0x000C53D6
			public bool BoldCS
			{
				get
				{
					return (byte)(this.Current.Flags & RtfParser.RtfParserState.StateFlags.BoldCS) != 0;
				}
				set
				{
					if (this.BoldCS != value)
					{
						this.SetDirty();
						this.Current.Flags = (this.Current.Flags ^ RtfParser.RtfParserState.StateFlags.BoldCS);
					}
				}
			}

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x060018CD RID: 6349 RVA: 0x000C71FB File Offset: 0x000C53FB
			// (set) Token: 0x060018CE RID: 6350 RVA: 0x000C7212 File Offset: 0x000C5412
			public bool BoldOther
			{
				get
				{
					return (byte)(this.Current.Flags & RtfParser.RtfParserState.StateFlags.BoldOther) != 0;
				}
				set
				{
					if (this.BoldOther != value)
					{
						this.SetDirty();
						this.Current.Flags = (this.Current.Flags ^ RtfParser.RtfParserState.StateFlags.BoldOther);
					}
				}
			}

			// Token: 0x17000637 RID: 1591
			// (get) Token: 0x060018CF RID: 6351 RVA: 0x000C7238 File Offset: 0x000C5438
			// (set) Token: 0x060018D0 RID: 6352 RVA: 0x000C724F File Offset: 0x000C544F
			public bool Italic
			{
				get
				{
					return (byte)(this.Current.Flags & RtfParser.RtfParserState.StateFlags.Italic) != 0;
				}
				set
				{
					if (this.Italic != value)
					{
						this.SetDirty();
						this.Current.Flags = (this.Current.Flags ^ RtfParser.RtfParserState.StateFlags.Italic);
					}
				}
			}

			// Token: 0x17000638 RID: 1592
			// (get) Token: 0x060018D1 RID: 6353 RVA: 0x000C7275 File Offset: 0x000C5475
			// (set) Token: 0x060018D2 RID: 6354 RVA: 0x000C728C File Offset: 0x000C548C
			public bool ItalicCS
			{
				get
				{
					return (byte)(this.Current.Flags & RtfParser.RtfParserState.StateFlags.ItalicCS) != 0;
				}
				set
				{
					if (this.ItalicCS != value)
					{
						this.SetDirty();
						this.Current.Flags = (this.Current.Flags ^ RtfParser.RtfParserState.StateFlags.ItalicCS);
					}
				}
			}

			// Token: 0x17000639 RID: 1593
			// (get) Token: 0x060018D3 RID: 6355 RVA: 0x000C72B2 File Offset: 0x000C54B2
			// (set) Token: 0x060018D4 RID: 6356 RVA: 0x000C72CC File Offset: 0x000C54CC
			public bool ItalicOther
			{
				get
				{
					return (byte)(this.Current.Flags & RtfParser.RtfParserState.StateFlags.ItalicOther) != 0;
				}
				set
				{
					if (this.ItalicOther != value)
					{
						this.SetDirty();
						this.Current.Flags = (this.Current.Flags ^ RtfParser.RtfParserState.StateFlags.ItalicOther);
					}
				}
			}

			// Token: 0x1700063A RID: 1594
			// (get) Token: 0x060018D5 RID: 6357 RVA: 0x000C72F5 File Offset: 0x000C54F5
			// (set) Token: 0x060018D6 RID: 6358 RVA: 0x000C7302 File Offset: 0x000C5502
			public byte BytesSkipForUnicodeEscape
			{
				get
				{
					return this.Current.BytesSkipForUnicodeEscape;
				}
				set
				{
					if (this.Current.BytesSkipForUnicodeEscape != value)
					{
						this.SetDirty();
						this.Current.BytesSkipForUnicodeEscape = value;
					}
				}
			}

			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x060018D7 RID: 6359 RVA: 0x000C7324 File Offset: 0x000C5524
			// (set) Token: 0x060018D8 RID: 6360 RVA: 0x000C7331 File Offset: 0x000C5531
			public ushort CodePage
			{
				get
				{
					return this.Current.CodePage;
				}
				set
				{
					if (this.Current.CodePage != value)
					{
						this.SetDirty();
						this.Current.CodePage = value;
					}
				}
			}

			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x060018D9 RID: 6361 RVA: 0x000C7353 File Offset: 0x000C5553
			// (set) Token: 0x060018DA RID: 6362 RVA: 0x000C7360 File Offset: 0x000C5560
			public TextMapping TextMapping
			{
				get
				{
					return this.Current.TextMapping;
				}
				set
				{
					if (this.Current.TextMapping != value)
					{
						this.SetDirty();
						this.Current.TextMapping = value;
					}
				}
			}

			// Token: 0x1700063D RID: 1597
			// (get) Token: 0x060018DB RID: 6363 RVA: 0x000C7382 File Offset: 0x000C5582
			// (set) Token: 0x060018DC RID: 6364 RVA: 0x000C738F File Offset: 0x000C558F
			public RtfSupport.CharRep CharRep
			{
				get
				{
					return this.Current.CharRep;
				}
				set
				{
					if (this.Current.CharRep != value)
					{
						this.SetDirty();
						this.Current.CharRep = value;
					}
				}
			}

			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x060018DD RID: 6365 RVA: 0x000C73B1 File Offset: 0x000C55B1
			// (set) Token: 0x060018DE RID: 6366 RVA: 0x000C73BE File Offset: 0x000C55BE
			public short Language
			{
				get
				{
					return this.Current.Language;
				}
				set
				{
					if (this.Current.Language != value)
					{
						this.SetDirty();
						this.Current.Language = value;
					}
				}
			}

			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x060018DF RID: 6367 RVA: 0x000C73E0 File Offset: 0x000C55E0
			// (set) Token: 0x060018E0 RID: 6368 RVA: 0x000C73ED File Offset: 0x000C55ED
			public short FontIndex
			{
				get
				{
					return this.Current.FontIndex;
				}
				set
				{
					if (this.Current.FontIndex != value)
					{
						this.SetDirty();
						this.Current.FontIndex = value;
					}
				}
			}

			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x060018E1 RID: 6369 RVA: 0x000C740F File Offset: 0x000C560F
			// (set) Token: 0x060018E2 RID: 6370 RVA: 0x000C741C File Offset: 0x000C561C
			public short FontIndexAscii
			{
				get
				{
					return this.Current.FontIndexAscii;
				}
				set
				{
					if (this.Current.FontIndexAscii != value)
					{
						this.SetDirty();
						this.Current.FontIndexAscii = value;
					}
				}
			}

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x060018E3 RID: 6371 RVA: 0x000C743E File Offset: 0x000C563E
			// (set) Token: 0x060018E4 RID: 6372 RVA: 0x000C744B File Offset: 0x000C564B
			public short FontIndexDbCh
			{
				get
				{
					return this.Current.FontIndexDbCh;
				}
				set
				{
					if (this.Current.FontIndexDbCh != value)
					{
						this.SetDirty();
						this.Current.FontIndexDbCh = value;
					}
				}
			}

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x060018E5 RID: 6373 RVA: 0x000C746D File Offset: 0x000C566D
			// (set) Token: 0x060018E6 RID: 6374 RVA: 0x000C747A File Offset: 0x000C567A
			public short FontIndexCS
			{
				get
				{
					return this.Current.FontIndexCS;
				}
				set
				{
					if (this.Current.FontIndexCS != value)
					{
						this.SetDirty();
						this.Current.FontIndexCS = value;
					}
				}
			}

			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x060018E7 RID: 6375 RVA: 0x000C749C File Offset: 0x000C569C
			// (set) Token: 0x060018E8 RID: 6376 RVA: 0x000C74A9 File Offset: 0x000C56A9
			public short FontIndexOther
			{
				get
				{
					return this.Current.FontIndexOther;
				}
				set
				{
					if (this.Current.FontIndexOther != value)
					{
						this.SetDirty();
						this.Current.FontIndexOther = value;
					}
				}
			}

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x060018E9 RID: 6377 RVA: 0x000C74CB File Offset: 0x000C56CB
			// (set) Token: 0x060018EA RID: 6378 RVA: 0x000C74D8 File Offset: 0x000C56D8
			public short FontSize
			{
				get
				{
					return this.Current.FontSize;
				}
				set
				{
					if (this.Current.FontSize != value)
					{
						this.SetDirty();
						this.Current.FontSize = value;
					}
				}
			}

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x060018EB RID: 6379 RVA: 0x000C74FA File Offset: 0x000C56FA
			// (set) Token: 0x060018EC RID: 6380 RVA: 0x000C7507 File Offset: 0x000C5707
			public short FontSizeCS
			{
				get
				{
					return this.Current.FontSizeCS;
				}
				set
				{
					if (this.Current.FontSizeCS != value)
					{
						this.SetDirty();
						this.Current.FontSizeCS = value;
					}
				}
			}

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x060018ED RID: 6381 RVA: 0x000C7529 File Offset: 0x000C5729
			// (set) Token: 0x060018EE RID: 6382 RVA: 0x000C7536 File Offset: 0x000C5736
			public short FontSizeOther
			{
				get
				{
					return this.Current.FontSizeOther;
				}
				set
				{
					if (this.Current.FontSizeOther != value)
					{
						this.SetDirty();
						this.Current.FontSizeOther = value;
					}
				}
			}

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x060018EF RID: 6383 RVA: 0x000C7558 File Offset: 0x000C5758
			// (set) Token: 0x060018F0 RID: 6384 RVA: 0x000C7565 File Offset: 0x000C5765
			public RtfTextRunKind AssociateRunKind
			{
				get
				{
					return this.Current.RunKind;
				}
				set
				{
					if (this.Current.RunKind != value)
					{
						this.SetDirty();
						this.Current.RunKind = value;
					}
				}
			}

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x060018F1 RID: 6385 RVA: 0x000C7588 File Offset: 0x000C5788
			// (set) Token: 0x060018F2 RID: 6386 RVA: 0x000C75F4 File Offset: 0x000C57F4
			public short AssociateFontIndex
			{
				get
				{
					if (this.Current.RunKind == RtfTextRunKind.None)
					{
						return this.Current.FontIndex;
					}
					if (this.Current.RunKind == RtfTextRunKind.Dbch)
					{
						return this.Current.FontIndexDbCh;
					}
					if (this.Current.RunKind == RtfTextRunKind.Loch)
					{
						return this.Current.FontIndexAscii;
					}
					return this.Current.FontIndexOther;
				}
				set
				{
					if (this.Current.RunKind == RtfTextRunKind.None)
					{
						if (this.Current.FontIndex != value)
						{
							this.SetDirty();
							this.Current.FontIndex = value;
							return;
						}
					}
					else if (this.Current.RunKind == RtfTextRunKind.Dbch)
					{
						if (this.Current.FontIndexDbCh != value)
						{
							this.SetDirty();
							this.Current.FontIndexDbCh = value;
							return;
						}
					}
					else if (this.Current.RunKind == RtfTextRunKind.Loch)
					{
						if (this.Current.FontIndexAscii != value)
						{
							this.SetDirty();
							this.Current.FontIndexAscii = value;
							return;
						}
					}
					else if (this.Current.FontIndexOther != value)
					{
						this.SetDirty();
						this.Current.FontIndexOther = value;
					}
				}
			}

			// Token: 0x060018F3 RID: 6387 RVA: 0x000C76B8 File Offset: 0x000C58B8
			public void Initialize()
			{
				this.Level = 0;
				this.stackTop = 0;
				this.defaultFontIndex = 0;
				this.defaultLanguage = -1;
				this.defaultLanguageFE = -1;
				this.charRepBiDi = RtfSupport.CharRep.ANSI_INDEX;
				this.codePageFixed = false;
				this.Current.Depth = 1;
				this.Current.Dest = RtfParser.RtfParserDestination.Default;
				this.Current.BytesSkipForUnicodeEscape = 1;
				this.SetPlain();
			}

			// Token: 0x060018F4 RID: 6388 RVA: 0x000C7720 File Offset: 0x000C5920
			public void SetDetectSingleChpRtl()
			{
				this.detectSingleChpRtl = true;
			}

			// Token: 0x060018F5 RID: 6389 RVA: 0x000C7729 File Offset: 0x000C5929
			public void SetRtlch(bool value)
			{
				this.detectAssocRtl = true;
				this.inAssocRtl = !this.inAssocRtl;
				this.BiDi = value;
			}

			// Token: 0x060018F6 RID: 6390 RVA: 0x000C7748 File Offset: 0x000C5948
			public void SetFcs(bool value)
			{
				this.detectAssocSA = true;
				this.inAssocSA = !this.inAssocSA;
				this.ComplexScript = value;
			}

			// Token: 0x060018F7 RID: 6391 RVA: 0x000C7768 File Offset: 0x000C5968
			public void SetPlain()
			{
				this.SetDirty();
				this.Current.Flags = RtfParser.RtfParserState.StateFlags.None;
				this.Current.RunKind = RtfTextRunKind.None;
				this.Current.FontIndex = this.defaultFontIndex;
				this.Current.FontIndexCS = -1;
				this.Current.FontIndexAscii = -1;
				this.Current.FontIndexDbCh = -1;
				this.Current.FontIndexOther = this.defaultFontIndex;
				this.Current.FontSize = RtfParserBase.TwelvePointsInTwips;
				this.Current.FontSizeCS = 0;
				this.Current.FontSizeOther = 0;
				this.Current.CodePage = 0;
				this.Current.TextMapping = TextMapping.Unicode;
				this.Current.Language = ((this.defaultLanguageFE != -1) ? this.defaultLanguageFE : this.defaultLanguage);
				this.Current.CharRep = RtfSupport.CharRep.DEFAULT_INDEX;
			}

			// Token: 0x060018F8 RID: 6392 RVA: 0x000C784C File Offset: 0x000C5A4C
			public void Push()
			{
				this.Level++;
				this.Current.Depth = this.Current.Depth + 1;
				if (this.Current.Depth == 32767)
				{
					this.PushReally();
				}
			}

			// Token: 0x060018F9 RID: 6393 RVA: 0x000C7888 File Offset: 0x000C5A88
			public void SetDirty()
			{
				if (this.Current.Depth > 1)
				{
					this.PushReally();
				}
			}

			// Token: 0x060018FA RID: 6394 RVA: 0x000C78A0 File Offset: 0x000C5AA0
			public void Pop()
			{
				if (this.Level > 1)
				{
					this.Current.Depth = this.Current.Depth - 1;
					if (this.Current.Depth == 0 && this.stackTop != 0)
					{
						this.Current = this.stack[--this.stackTop];
					}
					this.Level--;
				}
			}

			// Token: 0x060018FB RID: 6395 RVA: 0x000C7914 File Offset: 0x000C5B14
			private void PushReally()
			{
				if (this.Level >= 4096)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				if (this.stack == null || this.stackTop == this.stack.Length)
				{
					RtfParser.RtfParserState.State[] destinationArray;
					if (this.stack != null)
					{
						destinationArray = new RtfParser.RtfParserState.State[this.stackTop * 2];
						Array.Copy(this.stack, 0, destinationArray, 0, this.stackTop);
					}
					else
					{
						destinationArray = new RtfParser.RtfParserState.State[8];
					}
					this.stack = destinationArray;
				}
				this.stack[this.stackTop] = this.Current;
				RtfParser.RtfParserState.State[] array = this.stack;
				int num = this.stackTop;
				array[num].Depth = array[num].Depth - 1;
				this.stackTop++;
				this.Current.Depth = 1;
			}

			// Token: 0x04001D1E RID: 7454
			public int Level;

			// Token: 0x04001D1F RID: 7455
			public RtfParser.RtfParserState.State Current;

			// Token: 0x04001D20 RID: 7456
			private RtfParser.RtfParserState.State[] stack;

			// Token: 0x04001D21 RID: 7457
			private int stackTop;

			// Token: 0x04001D22 RID: 7458
			private bool codePageFixed;

			// Token: 0x04001D23 RID: 7459
			private short defaultFontIndex;

			// Token: 0x04001D24 RID: 7460
			private short defaultLanguage;

			// Token: 0x04001D25 RID: 7461
			private short defaultLanguageFE;

			// Token: 0x04001D26 RID: 7462
			private RtfSupport.CharRep charRepBiDi;

			// Token: 0x04001D27 RID: 7463
			private bool detectSingleChpRtl;

			// Token: 0x04001D28 RID: 7464
			private bool detectAssocRtl;

			// Token: 0x04001D29 RID: 7465
			private bool detectAssocSA;

			// Token: 0x04001D2A RID: 7466
			private bool inAssocRtl;

			// Token: 0x04001D2B RID: 7467
			private bool inAssocSA;

			// Token: 0x02000256 RID: 598
			internal enum StateFlagsIndex : byte
			{
				// Token: 0x04001D2D RID: 7469
				BiDi,
				// Token: 0x04001D2E RID: 7470
				ComplexScript,
				// Token: 0x04001D2F RID: 7471
				Bold,
				// Token: 0x04001D30 RID: 7472
				BoldCS,
				// Token: 0x04001D31 RID: 7473
				BoldOther,
				// Token: 0x04001D32 RID: 7474
				Italic,
				// Token: 0x04001D33 RID: 7475
				ItalicCS,
				// Token: 0x04001D34 RID: 7476
				ItalicOther
			}

			// Token: 0x02000257 RID: 599
			[Flags]
			internal enum StateFlags : byte
			{
				// Token: 0x04001D36 RID: 7478
				None = 0,
				// Token: 0x04001D37 RID: 7479
				BiDi = 1,
				// Token: 0x04001D38 RID: 7480
				ComplexScript = 2,
				// Token: 0x04001D39 RID: 7481
				Bold = 4,
				// Token: 0x04001D3A RID: 7482
				BoldCS = 8,
				// Token: 0x04001D3B RID: 7483
				BoldOther = 16,
				// Token: 0x04001D3C RID: 7484
				Italic = 32,
				// Token: 0x04001D3D RID: 7485
				ItalicCS = 64,
				// Token: 0x04001D3E RID: 7486
				ItalicOther = 128
			}

			// Token: 0x02000258 RID: 600
			public struct State
			{
				// Token: 0x04001D3F RID: 7487
				public short Depth;

				// Token: 0x04001D40 RID: 7488
				public RtfParser.RtfParserState.StateFlags Flags;

				// Token: 0x04001D41 RID: 7489
				public RtfParser.RtfParserDestination Dest;

				// Token: 0x04001D42 RID: 7490
				public byte BytesSkipForUnicodeEscape;

				// Token: 0x04001D43 RID: 7491
				public RtfSupport.CharRep CharRep;

				// Token: 0x04001D44 RID: 7492
				public ushort CodePage;

				// Token: 0x04001D45 RID: 7493
				public TextMapping TextMapping;

				// Token: 0x04001D46 RID: 7494
				public short Language;

				// Token: 0x04001D47 RID: 7495
				public short FontIndex;

				// Token: 0x04001D48 RID: 7496
				public short FontIndexAscii;

				// Token: 0x04001D49 RID: 7497
				public short FontIndexDbCh;

				// Token: 0x04001D4A RID: 7498
				public short FontIndexCS;

				// Token: 0x04001D4B RID: 7499
				public short FontIndexOther;

				// Token: 0x04001D4C RID: 7500
				public short FontSize;

				// Token: 0x04001D4D RID: 7501
				public short FontSizeCS;

				// Token: 0x04001D4E RID: 7502
				public short FontSizeOther;

				// Token: 0x04001D4F RID: 7503
				public RtfTextRunKind RunKind;
			}
		}

		// Token: 0x02000259 RID: 601
		internal struct RtfParserFont
		{
			// Token: 0x060018FC RID: 6396 RVA: 0x000C79E0 File Offset: 0x000C5BE0
			public void Initialize(short handle, ushort cpid)
			{
				this.Handle = handle;
				this.CodePage = cpid;
				this.CharRep = RtfSupport.CharRep.DEFAULT_INDEX;
				this.Family = RtfFontFamily.Default;
				this.Pitch = RtfFontPitch.Default;
				this.TextMapping = TextMapping.Unicode;
			}

			// Token: 0x04001D50 RID: 7504
			public short Handle;

			// Token: 0x04001D51 RID: 7505
			public ushort CodePage;

			// Token: 0x04001D52 RID: 7506
			public RtfSupport.CharRep CharRep;

			// Token: 0x04001D53 RID: 7507
			public RtfFontFamily Family;

			// Token: 0x04001D54 RID: 7508
			public RtfFontPitch Pitch;

			// Token: 0x04001D55 RID: 7509
			public TextMapping TextMapping;
		}
	}
}
