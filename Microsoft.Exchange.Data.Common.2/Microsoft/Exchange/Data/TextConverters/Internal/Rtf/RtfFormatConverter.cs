using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000239 RID: 569
	internal class RtfFormatConverter : FormatConverter, IProducerConsumer, IDisposable
	{
		// Token: 0x0600176E RID: 5998 RVA: 0x000B689C File Offset: 0x000B4A9C
		public RtfFormatConverter(RtfParser parser, FormatOutput output, Injection injection, bool treatNbspAsBreakable, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum, Stream formatConverterTraceStream) : base(formatConverterTraceStream)
		{
			this.treatNbspAsBreakable = treatNbspAsBreakable;
			this.parser = parser;
			this.injection = injection;
			this.output = output;
			if (this.output != null)
			{
				this.output.Initialize(this.Store, SourceFormat.Rtf, "converted from rtf");
			}
			this.Construct();
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000B68FA File Offset: 0x000B4AFA
		public RtfFormatConverter(RtfParser parser, FormatStore formatStore, bool treatNbspAsBreakable, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum, Stream formatConverterTraceStream) : base(formatStore, formatConverterTraceStream)
		{
			this.treatNbspAsBreakable = treatNbspAsBreakable;
			this.parser = parser;
			this.Construct();
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x000B6920 File Offset: 0x000B4B20
		private bool CanFlush
		{
			get
			{
				return this.output.CanAcceptMoreOutput;
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x000B6930 File Offset: 0x000B4B30
		void IDisposable.Dispose()
		{
			this.scratch.DisposeBuffer();
			this.scratchAlt.DisposeBuffer();
			if (this.parser != null && this.parser is IDisposable)
			{
				((IDisposable)this.parser).Dispose();
			}
			this.parser = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x000B6988 File Offset: 0x000B4B88
		public override void Run()
		{
			if (this.output != null && base.MustFlush)
			{
				if (this.CanFlush)
				{
					this.FlushOutput();
					return;
				}
			}
			else if (!base.EndOfFile)
			{
				RtfTokenId rtfTokenId = this.parser.Parse();
				if (rtfTokenId != RtfTokenId.None)
				{
					this.Process(rtfTokenId);
				}
			}
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x000B69D3 File Offset: 0x000B4BD3
		public bool Flush()
		{
			this.Run();
			return base.EndOfFile && !base.MustFlush;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000B69EE File Offset: 0x000B4BEE
		public void BreakLine()
		{
			if (this.beforeContent)
			{
				this.PrepareToAddContent();
			}
			base.AddLineBreak(1);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000B6A05 File Offset: 0x000B4C05
		public void OutputTabulation(int count)
		{
			if (this.beforeContent)
			{
				this.PrepareToAddContent();
			}
			base.AddTabulation(count);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000B6A1C File Offset: 0x000B4C1C
		public void OutputSpace(int count)
		{
			base.AddSpace(count);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000B6A25 File Offset: 0x000B4C25
		public void OutputNbsp(int count)
		{
			base.AddNbsp(count);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x000B6A2E File Offset: 0x000B4C2E
		public void OutputNonspace(char[] buffer, int offset, int count, TextMapping textMapping)
		{
			base.AddNonSpaceText(buffer, offset, count);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x000B6A3C File Offset: 0x000B4C3C
		private static RtfNumbering ListTypeToNumbering(int listType)
		{
			switch (listType)
			{
			case 0:
				return RtfNumbering.Arabic;
			case 1:
				return RtfNumbering.UcRoman;
			case 2:
				return RtfNumbering.LcRoman;
			case 3:
				return RtfNumbering.UcLetter;
			case 4:
				return RtfNumbering.LcLetter;
			default:
				if (listType != 23)
				{
					return RtfNumbering.Bullet;
				}
				return RtfNumbering.Bullet;
			}
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000B6A7C File Offset: 0x000B4C7C
		private void Construct()
		{
			this.state = default(RtfFormatConverter.RtfState);
			this.state.Initialize();
			this.colors = new int[64];
			this.fonts = new RtfFont[32];
			this.lists = new RtfFormatConverter.List[128];
			this.listDirectory = new short[128];
			this.fonts[0] = new RtfFont("Times New Roman");
			this.fontsCount += 1;
			this.lists[0].Levels = new RtfFormatConverter.ListLevel[9];
			this.lists[0].Levels[0].Type = RtfNumbering.Arabic;
			this.lists[0].Levels[0].Start = 1;
			this.lists[0].Levels[1].Type = RtfNumbering.LcLetter;
			this.lists[0].Levels[1].Start = 1;
			this.lists[0].Levels[2].Type = RtfNumbering.LcRoman;
			this.lists[0].Levels[2].Start = 1;
			this.lists[0].Levels[3].Type = RtfNumbering.Arabic;
			this.lists[0].Levels[3].Start = 1;
			this.lists[0].Levels[4].Type = RtfNumbering.LcLetter;
			this.lists[0].Levels[4].Start = 1;
			this.lists[0].Levels[5].Type = RtfNumbering.LcRoman;
			this.lists[0].Levels[5].Start = 1;
			this.lists[0].Levels[6].Type = RtfNumbering.Arabic;
			this.lists[0].Levels[6].Start = 1;
			this.lists[0].Levels[7].Type = RtfNumbering.LcLetter;
			this.lists[0].Levels[7].Start = 1;
			this.lists[0].Levels[8].Type = RtfNumbering.LcRoman;
			this.lists[0].Levels[8].Start = 1;
			this.lists[0].LevelCount = 9;
			this.listsCount += 1;
			this.lists[1].Levels = new RtfFormatConverter.ListLevel[9];
			this.lists[1].Levels[0].Type = RtfNumbering.Bullet;
			this.lists[1].Levels[0].Start = 1;
			this.lists[1].Levels[1].Type = RtfNumbering.Bullet;
			this.lists[1].Levels[1].Start = 1;
			this.lists[1].Levels[2].Type = RtfNumbering.Bullet;
			this.lists[1].Levels[2].Start = 1;
			this.lists[1].Levels[3].Type = RtfNumbering.Bullet;
			this.lists[1].Levels[3].Start = 1;
			this.lists[1].Levels[4].Type = RtfNumbering.Bullet;
			this.lists[1].Levels[4].Start = 1;
			this.lists[1].Levels[5].Type = RtfNumbering.Bullet;
			this.lists[1].Levels[5].Start = 1;
			this.lists[1].Levels[6].Type = RtfNumbering.Bullet;
			this.lists[1].Levels[6].Start = 1;
			this.lists[1].Levels[7].Type = RtfNumbering.Bullet;
			this.lists[1].Levels[7].Start = 1;
			this.lists[1].Levels[8].Type = RtfNumbering.Bullet;
			this.lists[1].Levels[8].Start = 1;
			this.lists[1].LevelCount = 9;
			this.listsCount += 1;
			this.listDirectory[0] = 0;
			this.listDirectoryCount += 1;
			this.state.SetPlain();
			base.InitializeDocument();
			this.RegisterFontValue(0);
			base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontFace, this.fonts[(int)this.state.FontIndex].Value);
			base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontSize, new PropertyValue(LengthUnits.Twips, (int)this.state.FontSize));
			this.currentListState.ListIndex = -1;
			this.documentContainerStillOpen = true;
			if (this.injection != null)
			{
				bool haveHead = this.injection.HaveHead;
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x000B7032 File Offset: 0x000B5232
		private bool FlushOutput()
		{
			if (this.output.Flush())
			{
				base.MustFlush = false;
				return true;
			}
			return false;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x000B704C File Offset: 0x000B524C
		private void Process(RtfTokenId tokenId)
		{
			switch (tokenId)
			{
			case RtfTokenId.EndOfFile:
				this.ProcessEOF();
				break;
			case RtfTokenId.Begin:
				this.ProcessBeginGroup();
				return;
			case RtfTokenId.End:
				this.ProcessEndGroup();
				return;
			case RtfTokenId.Binary:
				this.ProcessBinary(this.parser.Token);
				return;
			case RtfTokenId.Keywords:
				this.ProcessKeywords(this.parser.Token);
				return;
			case (RtfTokenId)6:
				break;
			case RtfTokenId.Text:
				this.ProcessText(this.parser.Token);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x000B70CA File Offset: 0x000B52CA
		private void ProcessBeginGroup()
		{
			if (this.state.SkipLevel != 0)
			{
				this.state.Level = this.state.Level + 1;
				return;
			}
			this.state.Push();
			this.firstKeyword = true;
			this.ignorableDestination = false;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000B7108 File Offset: 0x000B5308
		private void ProcessEndGroup()
		{
			if (this.state.SkipLevel != 0)
			{
				if (this.state.Level != this.state.SkipLevel)
				{
					this.state.Level = this.state.Level - 1;
					return;
				}
				this.state.SkipLevel = 0;
			}
			this.firstKeyword = false;
			if (this.state.Level > 0)
			{
				this.EndGroup();
			}
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x000B7178 File Offset: 0x000B5378
		private void ProcessKeywords(RtfToken token)
		{
			if (this.state.SkipLevel != 0 && this.state.Level >= this.state.SkipLevel)
			{
				return;
			}
			foreach (RtfKeyword rtfKeyword in token.Keywords)
			{
				if (this.firstKeyword)
				{
					if (rtfKeyword.Id == 1)
					{
						this.ignorableDestination = true;
						continue;
					}
					this.firstKeyword = false;
					short id = rtfKeyword.Id;
					if (id <= 203)
					{
						if (id <= 50)
						{
							if (id <= 24)
							{
								switch (id)
								{
								case 8:
									this.state.SkipGroup();
									return;
								case 9:
									if (this.state.Destination != RtfDestination.ListTableEntry)
									{
										goto IL_6D7;
									}
									if (this.lists[(int)this.listIdx].LevelCount != 9)
									{
										this.state.Destination = RtfDestination.ListLevelEntry;
										RtfFormatConverter.List[] array = this.lists;
										short num = this.listIdx;
										byte levelCount;
										array[(int)num].LevelCount = (levelCount = array[(int)num].LevelCount) + 1;
										this.listLevel = (short)levelCount;
										this.lists[(int)this.listIdx].Levels[(int)this.listLevel].Start = 1;
										goto IL_6D7;
									}
									continue;
								default:
									if (id != 15 && id != 24)
									{
										goto IL_6BF;
									}
									goto IL_28D;
								}
							}
							else
							{
								if (id == 29)
								{
									if (this.state.Destination == RtfDestination.RTF || (this.state.Destination == RtfDestination.FieldResult && !this.ignoreFieldResult))
									{
										this.state.Destination = RtfDestination.Field;
									}
									return;
								}
								if (id != 39)
								{
									if (id != 50)
									{
										goto IL_6BF;
									}
									if (this.state.Destination == RtfDestination.RTF || this.state.Destination == RtfDestination.FieldResult)
									{
										this.state.Destination = RtfDestination.Picture;
										this.pictureWidth = 0;
										this.pictureHeight = 0;
										continue;
									}
									this.state.SkipGroup();
									return;
								}
							}
						}
						else if (id <= 92)
						{
							if (id != 54)
							{
								if (id == 65)
								{
									this.state.Destination = RtfDestination.NestTableProps;
									continue;
								}
								if (id != 92)
								{
									goto IL_6BF;
								}
							}
							else
							{
								if (this.state.Destination == RtfDestination.ListTable && (int)this.listsCount < this.lists.Length)
								{
									this.lists[(int)this.listsCount].Levels = new RtfFormatConverter.ListLevel[9];
									this.listIdx = this.listsCount;
									this.listsCount += 1;
									this.state.Destination = RtfDestination.ListTableEntry;
									continue;
								}
								continue;
							}
						}
						else if (id <= 175)
						{
							if (id != 123)
							{
								if (id != 175)
								{
									goto IL_6BF;
								}
								if (this.state.Destination == RtfDestination.RTF)
								{
									this.state.FontIndex = -1;
									this.state.Destination = RtfDestination.FontTable;
									continue;
								}
								continue;
							}
							else
							{
								if (this.state.Destination != RtfDestination.Picture)
								{
									this.state.ListIndex = 0;
									this.state.ListLevel = 0;
									this.state.Destination = RtfDestination.ParaNumbering;
									goto IL_6D7;
								}
								goto IL_6D7;
							}
						}
						else if (id != 193)
						{
							switch (id)
							{
							case 201:
								goto IL_28D;
							case 202:
								goto IL_6BF;
							case 203:
								if (this.state.Destination == RtfDestination.ListOverrideTable)
								{
									if ((int)this.listDirectoryCount < this.lists.Length)
									{
										this.listDirectory[(int)this.listDirectoryCount] = 0;
										this.listDirectoryCount += 1;
									}
									this.state.Destination = RtfDestination.ListOverrideTableEntry;
									continue;
								}
								continue;
							default:
								goto IL_6BF;
							}
						}
						else
						{
							if (this.state.Destination == RtfDestination.RTF || (this.state.Destination == RtfDestination.FieldResult && !this.ignoreFieldResult))
							{
								this.state.Destination = RtfDestination.BookmarkName;
								continue;
							}
							this.state.SkipGroup();
							return;
						}
					}
					else if (id <= 258)
					{
						if (id <= 230)
						{
							switch (id)
							{
							case 210:
								if (this.state.Destination == RtfDestination.FontTable && this.state.FontIndex >= 0)
								{
									this.state.Destination = RtfDestination.RealFontName;
									continue;
								}
								continue;
							case 211:
								goto IL_6BF;
							case 212:
								this.state.SkipGroup();
								return;
							default:
								if (id != 227 && id != 230)
								{
									goto IL_6BF;
								}
								goto IL_28D;
							}
						}
						else if (id <= 241)
						{
							if (id != 233 && id != 241)
							{
								goto IL_6BF;
							}
							goto IL_28D;
						}
						else if (id != 246)
						{
							switch (id)
							{
							case 252:
								if (this.state.Destination == RtfDestination.RTF)
								{
									this.state.Destination = RtfDestination.ColorTable;
									continue;
								}
								continue;
							case 253:
							case 258:
								goto IL_28D;
							case 254:
							case 255:
							case 256:
								goto IL_6BF;
							case 257:
								this.state.SkipGroup();
								return;
							default:
								goto IL_6BF;
							}
						}
						else
						{
							if (this.state.Destination == RtfDestination.RTF && this.listsCount == 2)
							{
								this.state.Destination = RtfDestination.ListTable;
								continue;
							}
							continue;
						}
					}
					else if (id <= 277)
					{
						switch (id)
						{
						case 268:
							if (this.state.Destination == RtfDestination.FontTable && this.state.FontIndex >= 0)
							{
								this.state.Destination = RtfDestination.AltFontName;
								continue;
							}
							continue;
						case 269:
							if (this.state.Destination == RtfDestination.Field)
							{
								this.state.Destination = RtfDestination.FieldResult;
								continue;
							}
							this.state.SkipGroup();
							return;
						default:
							if (id != 273 && id != 277)
							{
								goto IL_6BF;
							}
							goto IL_28D;
						}
					}
					else if (id <= 301)
					{
						if (id == 283)
						{
							this.state.SkipGroup();
							return;
						}
						if (id != 301)
						{
							goto IL_6BF;
						}
					}
					else if (id != 306)
					{
						switch (id)
						{
						case 315:
							goto IL_28D;
						case 316:
							if (this.state.Destination == RtfDestination.RTF && this.listDirectoryCount == 1)
							{
								this.state.Destination = RtfDestination.ListOverrideTable;
								continue;
							}
							continue;
						case 317:
						case 318:
							goto IL_6BF;
						case 319:
							this.state.SkipGroup();
							return;
						default:
							goto IL_6BF;
						}
					}
					else
					{
						if (this.state.Destination == RtfDestination.Field)
						{
							this.state.Destination = RtfDestination.FieldInstruction;
							continue;
						}
						this.state.SkipGroup();
						return;
					}
					this.state.SkipGroup();
					return;
					IL_6BF:
					if (this.ignorableDestination)
					{
						this.state.SkipGroup();
						return;
					}
					goto IL_6D7;
					IL_28D:
					this.state.SkipGroup();
					return;
				}
				IL_6D7:
				short id2 = rtfKeyword.Id;
				if (id2 <= 285)
				{
					switch (id2)
					{
					case 4:
					case 5:
					case 19:
					case 61:
					case 63:
					case 76:
					case 150:
					case 155:
					case 159:
					case 178:
					case 190:
					case 225:
						goto IL_1E32;
					case 6:
						if (this.state.Destination == RtfDestination.Picture)
						{
							this.pictureHeight = rtfKeyword.Value;
							continue;
						}
						continue;
					case 7:
					case 10:
					case 12:
					case 16:
					case 22:
					case 25:
					case 181:
					case 186:
					case 188:
					case 194:
					case 195:
					case 196:
					case 215:
					case 218:
						this.borderId = (RtfBorderId)RTFData.keywords[(int)rtfKeyword.Id].idx;
						continue;
					case 8:
					case 9:
					case 15:
					case 18:
					case 20:
					case 24:
					case 26:
					case 27:
					case 28:
					case 29:
					case 30:
					case 31:
					case 32:
					case 33:
					case 34:
					case 35:
					case 37:
					case 38:
					case 39:
					case 42:
					case 43:
					case 45:
					case 47:
					case 49:
					case 50:
					case 53:
					case 54:
					case 56:
					case 57:
					case 59:
					case 60:
					case 62:
					case 65:
					case 67:
					case 69:
					case 70:
					case 71:
					case 72:
					case 74:
					case 75:
					case 79:
					case 81:
					case 83:
					case 84:
					case 85:
					case 87:
					case 92:
					case 93:
					case 94:
					case 95:
					case 96:
					case 104:
					case 106:
					case 107:
					case 108:
					case 109:
					case 110:
					case 114:
					case 115:
					case 117:
					case 123:
					case 124:
					case 125:
					case 127:
					case 128:
					case 129:
					case 130:
					case 133:
					case 134:
					case 135:
					case 137:
					case 145:
					case 146:
					case 147:
					case 149:
					case 152:
					case 153:
					case 158:
					case 162:
					case 164:
					case 165:
					case 166:
					case 168:
					case 169:
					case 175:
					case 177:
					case 182:
					case 183:
					case 185:
					case 187:
					case 189:
					case 192:
					case 193:
					case 197:
					case 201:
					case 202:
					case 203:
					case 204:
					case 207:
					case 208:
					case 210:
					case 212:
					case 214:
					case 217:
					case 222:
					case 223:
					case 224:
					case 226:
					case 227:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
					case 236:
					case 238:
					case 240:
					case 241:
					case 242:
					case 244:
					case 245:
					case 246:
					case 249:
					case 252:
					case 253:
					case 255:
					case 257:
					case 258:
					case 260:
					case 263:
						continue;
					case 11:
					case 14:
					case 23:
					case 46:
					case 52:
					case 64:
					case 80:
					case 89:
					case 90:
					case 97:
					case 122:
					case 132:
					case 156:
					case 171:
					case 209:
					case 216:
					case 239:
					case 243:
					case 247:
					case 251:
					case 254:
					case 256:
					case 261:
						goto IL_1B15;
					case 13:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null)
						{
							continue;
						}
						continue;
					case 17:
						this.state.Capitalize = (0 != rtfKeyword.Value);
						continue;
					case 21:
					case 36:
					case 77:
					case 248:
						break;
					case 40:
						this.EventEndRow(false);
						continue;
					case 41:
					case 91:
						if (this.newRow != null)
						{
							this.newRow.RightToLeft = true;
							continue;
						}
						continue;
					case 44:
						if (this.state.Destination == RtfDestination.ListLevelEntry && 0 < rtfKeyword.Value)
						{
							this.lists[(int)this.listIdx].Levels[(int)this.listLevel].Start = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 48:
						if (this.state.Destination == RtfDestination.RTF || (this.state.Destination == RtfDestination.FieldResult && !this.ignoreFieldResult))
						{
							this.BreakLine();
							continue;
						}
						continue;
					case 51:
						if (this.state.Destination == RtfDestination.ParaNumbering)
						{
							this.state.ListIndex = 1;
							this.state.ListLevel = 0;
							this.state.ListStyle = RtfNumbering.Bullet;
							continue;
						}
						continue;
					case 55:
						this.EventEndRow(true);
						continue;
					case 58:
						if (rtfKeyword.Value < 0 || rtfKeyword.Value >= this.colorsCount)
						{
							continue;
						}
						if (rtfKeyword.Value != 0)
						{
							this.state.ParagraphBackColor = this.colors[rtfKeyword.Value];
							continue;
						}
						this.state.ParagraphBackColor = 16777215;
						continue;
					case 66:
						if (rtfKeyword.Value >= -32768 && rtfKeyword.Value < 32767 && this.newRow != null && this.newRow.CellCount == 0)
						{
							this.newRow.Left = (this.newRow.Right = (short)(rtfKeyword.Value + 108));
							continue;
						}
						continue;
					case 68:
						if (this.state.Destination == RtfDestination.RTF || (this.state.Destination == RtfDestination.FieldResult && !this.ignoreFieldResult))
						{
							this.EventEndParagraph();
							continue;
						}
						continue;
					case 73:
						if (this.newRow != null)
						{
							this.newRow.LastRow = true;
							continue;
						}
						continue;
					case 78:
						if (this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							this.newRow.Cells[(int)this.newRow.CellCount].MergeUp = true;
							continue;
						}
						continue;
					case 82:
						goto IL_1DD1;
					case 86:
						if (this.state.Destination == RtfDestination.ParaNumbering && this.state.ListIndex != -1)
						{
							this.lists[(int)this.state.ListIndex].Levels[(int)this.state.ListLevel].Start = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 88:
					case 148:
						goto IL_D53;
					case 98:
					case 100:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < this.colorsCount && this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							this.newRow.Cells[(int)this.newRow.CellCount].VerticalAlignment = (RtfVertAlignment)RTFData.keywords[(int)rtfKeyword.Id].idx;
							continue;
						}
						continue;
					case 99:
					case 198:
						this.state.Strikethrough = (0 != rtfKeyword.Value);
						continue;
					case 101:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 10)
						{
							this.requiredTableLevel = rtfKeyword.Value;
							continue;
						}
						continue;
					case 102:
						if (this.state.Destination == RtfDestination.ParaNumbering && 1 <= rtfKeyword.Value && rtfKeyword.Value <= 9 && this.state.ListIndex == -1)
						{
							this.state.ListIndex = 0;
							this.state.ListLevel = (byte)(rtfKeyword.Value - 1);
							continue;
						}
						continue;
					case 103:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value <= 3 && this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							this.newRow.Cells[(int)this.newRow.CellCount].WidthType = (byte)rtfKeyword.Value;
							continue;
						}
						continue;
					case 105:
						this.EventEndCell(true);
						continue;
					case 111:
					case 112:
					case 116:
					case 120:
					case 140:
					case 141:
						this.state.ParagraphAlignment = (RtfAlignment)RTFData.keywords[(int)rtfKeyword.Id].idx;
						continue;
					case 113:
					{
						if (this.state.Destination != RtfDestination.FontTable && this.state.Destination != RtfDestination.AltFontName && this.state.Destination != RtfDestination.RealFontName)
						{
							continue;
						}
						if (this.state.FontIndex >= 0 && this.state.FontIndex < this.fontsCount)
						{
							this.fonts[(int)this.state.FontIndex].Name = RtfSupport.StringFontNameFromScratch(this.scratch);
							this.scratch.Reset();
							this.state.FontIndex = -1;
						}
						short num2 = this.parser.FontIndex((short)rtfKeyword.Value);
						if (num2 < 0)
						{
							continue;
						}
						this.state.FontIndex = num2;
						if (num2 >= this.fontsCount)
						{
							if ((int)num2 >= this.fonts.Length)
							{
								RtfFont[] destinationArray = new RtfFont[Math.Max(this.fonts.Length * 2, (int)(num2 + 1))];
								Array.Copy(this.fonts, destinationArray, (int)this.fontsCount);
								this.fonts = destinationArray;
							}
							this.fontsCount = num2 + 1;
						}
						if (this.fonts[(int)num2].Value.IsRefCountedHandle)
						{
							base.ReleasePropertyValue(this.fonts[(int)num2].Value);
							this.fonts[(int)num2].Value = PropertyValue.Null;
							continue;
						}
						continue;
					}
					case 118:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null)
						{
							this.newRow.CellPadding.Right = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 119:
						this.EventEndCell(false);
						continue;
					case 121:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null)
						{
							this.newRow.CellPadding.Top = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 126:
						if (this.state.Destination == RtfDestination.RTF || (this.state.Destination == RtfDestination.FieldResult && !this.ignoreFieldResult))
						{
							this.OutputTabulation(1);
							continue;
						}
						continue;
					case 131:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null)
						{
							this.newRow.CellPadding.Left = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 136:
						this.state.Invisible = (rtfKeyword.Value != 0);
						continue;
					case 138:
						this.state.RightIndent = (short)rtfKeyword.Value;
						continue;
					case 139:
						this.state.SpaceAfter = (short)rtfKeyword.Value;
						continue;
					case 142:
						this.state.SpaceBefore = (short)rtfKeyword.Value;
						continue;
					case 143:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < this.colorsCount && this.newRow != null)
						{
							this.newRow.BackColor = this.colors[rtfKeyword.Value];
							continue;
						}
						continue;
					case 144:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null)
						{
							this.newRow.CellPadding.Bottom = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 151:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null)
						{
							this.newRow.Height = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 154:
						if (this.state.Destination == RtfDestination.Picture)
						{
							this.pictureWidth = rtfKeyword.Value;
							continue;
						}
						continue;
					case 157:
						if (this.beforeContent)
						{
							this.PrepareToAddContent();
						}
						base.OpenContainer(FormatContainerType.Image, true);
						base.Last.SetStringProperty(PropertyPrecedence.InlineStyle, PropertyId.ImageUrl, "objattph://");
						this.documentContainerStillOpen = false;
						continue;
					case 160:
						if (this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							this.newRow.Cells[(int)this.newRow.CellCount].MergeLeft = true;
							continue;
						}
						continue;
					case 161:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							this.newRow.Cells[(int)this.newRow.CellCount].Padding.Right = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 163:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							this.newRow.Cells[(int)this.newRow.CellCount].Padding.Top = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 167:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < this.colorsCount)
						{
							this.SetBorderColor(this.colors[rtfKeyword.Value]);
							continue;
						}
						continue;
					case 170:
						this.state.SetPlain();
						continue;
					case 172:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							this.newRow.Cells[(int)this.newRow.CellCount].Padding.Left = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 173:
						if (this.state.Destination == RtfDestination.ListTableEntry)
						{
							this.lists[(int)this.listIdx].Id = rtfKeyword.Value;
							continue;
						}
						if (this.state.Destination == RtfDestination.ListOverrideTableEntry && this.listsCount != 1)
						{
							for (short num3 = 1; num3 < this.listsCount; num3 += 1)
							{
								if (this.lists[(int)num3].Id == rtfKeyword.Value)
								{
									this.listDirectory[(int)(this.listDirectoryCount - 1)] = num3;
									break;
								}
							}
							continue;
						}
						continue;
					case 174:
						goto IL_1D6F;
					case 176:
						this.state.ParagraphRtl = true;
						continue;
					case 179:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							this.newRow.Cells[(int)this.newRow.CellCount].Padding.Bottom = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 180:
						this.state.SmallCaps = (0 != rtfKeyword.Value);
						continue;
					case 184:
						this.state.SetParagraphDefault();
						this.requiredTableLevel = 0;
						continue;
					case 191:
						if (this.requiredTableLevel == 0)
						{
							this.requiredTableLevel = 1;
							continue;
						}
						continue;
					case 199:
						if (this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							continue;
						}
						continue;
					case 200:
						this.InitializeFreshRowProperties();
						continue;
					case 205:
						if (this.state.ListIndex >= 0 && 0 <= rtfKeyword.Value && rtfKeyword.Value < (int)this.lists[(int)this.state.ListIndex].LevelCount)
						{
							this.state.ListLevel = (byte)rtfKeyword.Value;
							this.state.ListStyle = this.lists[(int)this.state.ListIndex].Levels[(int)this.state.ListLevel].Type;
							continue;
						}
						continue;
					case 206:
						this.state.LeftIndent = (short)rtfKeyword.Value;
						continue;
					case 211:
						this.state.Underline = false;
						continue;
					case 213:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							this.newRow.Cells[(int)this.newRow.CellCount].Width = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 219:
						if (this.state.Destination != RtfDestination.ListOverrideTableEntry && rtfKeyword.Value >= 1)
						{
							short num4 = (short)rtfKeyword.Value;
							if (num4 < 0 || num4 >= this.listDirectoryCount)
							{
								num4 = 0;
							}
							this.state.ListIndex = this.listDirectory[(int)num4];
							this.state.ListLevel = 0;
							this.state.ListStyle = this.lists[(int)this.state.ListIndex].Levels[(int)this.state.ListLevel].Type;
							continue;
						}
						continue;
					case 220:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value <= 75)
						{
							this.SetBorderWidth((byte)rtfKeyword.Value);
							continue;
						}
						continue;
					case 221:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null)
						{
							continue;
						}
						continue;
					case 228:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32659 && this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							if (rtfKeyword.Value >= (int)this.newRow.Right)
							{
								this.newRow.Right = (this.newRow.Cells[(int)this.newRow.CellCount].Cellx = (short)(rtfKeyword.Value + 108));
							}
							else
							{
								this.newRow.Cells[(int)this.newRow.CellCount].Cellx = this.newRow.Right;
							}
							RtfFormatConverter.RtfRow rtfRow = this.newRow;
							rtfRow.CellCount += 1;
							continue;
						}
						continue;
					case 229:
						if (this.state.Destination == RtfDestination.ParaNumbering)
						{
							this.state.ListStyle = RtfNumbering.None;
							this.state.ListIndex = -1;
							continue;
						}
						continue;
					case 237:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value < 32767 && this.newRow != null)
						{
							this.newRow.Width = (short)rtfKeyword.Value;
							continue;
						}
						continue;
					case 250:
						if (this.state.Destination == RtfDestination.ListLevelEntry && 0 <= rtfKeyword.Value && rtfKeyword.Value <= 2)
						{
							continue;
						}
						continue;
					case 259:
						if (this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
						{
							continue;
						}
						continue;
					case 262:
						this.state.Subscript = (0 != rtfKeyword.Value);
						continue;
					case 264:
						this.state.Subscript = false;
						this.state.Superscript = false;
						continue;
					default:
						switch (id2)
						{
						case 274:
							break;
						case 275:
						case 277:
							continue;
						case 276:
							goto IL_1B15;
						case 278:
						case 279:
							goto IL_1E32;
						default:
							switch (id2)
							{
							case 284:
								this.state.FirstLineIndent = (short)rtfKeyword.Value;
								continue;
							case 285:
								goto IL_1DD1;
							default:
								continue;
							}
							break;
						}
						break;
					}
					if (this.state.Destination == RtfDestination.ParaNumbering)
					{
						this.state.ListStyle = (RtfNumbering)RTFData.keywords[(int)rtfKeyword.Id].idx;
						continue;
					}
					continue;
					IL_1DD1:
					if (rtfKeyword.Value < 0 || rtfKeyword.Value >= this.colorsCount)
					{
						continue;
					}
					if (rtfKeyword.Value != 0)
					{
						this.state.FontBackColor = this.colors[rtfKeyword.Value];
						continue;
					}
					this.state.FontBackColor = this.state.ParagraphBackColor;
					continue;
				}
				else if (id2 <= 299)
				{
					if (id2 == 289)
					{
						goto IL_1B15;
					}
					switch (id2)
					{
					case 293:
						if (this.newRow != null)
						{
							this.newRow.HeaderRow = true;
							continue;
						}
						continue;
					case 294:
					case 299:
						goto IL_1E32;
					case 295:
					case 297:
					case 298:
						continue;
					case 296:
						if (this.state.Destination == RtfDestination.ListLevelEntry && 0 <= rtfKeyword.Value && rtfKeyword.Value <= 255)
						{
							this.lists[(int)this.listIdx].Levels[(int)this.listLevel].Type = RtfFormatConverter.ListTypeToNumbering(rtfKeyword.Value);
							continue;
						}
						continue;
					default:
						continue;
					}
				}
				else
				{
					if (id2 == 302)
					{
						continue;
					}
					switch (id2)
					{
					case 309:
						break;
					case 310:
					case 313:
					case 326:
						goto IL_1E32;
					case 311:
					case 312:
					case 314:
					case 315:
					case 316:
					case 317:
					case 319:
					case 324:
					case 325:
					case 327:
					case 329:
						continue;
					case 318:
						if (rtfKeyword.Value < 0 || rtfKeyword.Value >= this.colorsCount || this.newRow == null || !this.newRow.EnsureEntryForCurrentCell())
						{
							continue;
						}
						if (rtfKeyword.Value != 0)
						{
							this.newRow.Cells[(int)this.newRow.CellCount].BackColor = this.colors[rtfKeyword.Value];
							continue;
						}
						this.newRow.Cells[(int)this.newRow.CellCount].BackColor = 16777215;
						continue;
					case 320:
						if (rtfKeyword.Value >= 0 && rtfKeyword.Value <= 3 && this.newRow != null)
						{
							this.newRow.WidthType = (byte)rtfKeyword.Value;
							continue;
						}
						continue;
					case 321:
					case 328:
					case 330:
						goto IL_1B15;
					case 322:
						if (this.state.Destination == RtfDestination.ParaNumbering && this.state.ListIndex == -1)
						{
							this.state.ListIndex = 0;
							this.state.ListLevel = 0;
							continue;
						}
						continue;
					case 323:
						this.state.Superscript = (0 != rtfKeyword.Value);
						continue;
					case 331:
						goto IL_1D6F;
					default:
						continue;
					}
				}
				IL_D53:
				if (this.state.Destination == RtfDestination.ColorTable)
				{
					this.color &= ~(255 << (int)(RTFData.keywords[(int)rtfKeyword.Id].idx * 8));
					this.color |= (rtfKeyword.Value & 255) << (int)(RTFData.keywords[(int)rtfKeyword.Id].idx * 8);
					continue;
				}
				continue;
				IL_1B15:
				this.SetBorderKind((RtfBorderKind)RTFData.keywords[(int)rtfKeyword.Id].idx);
				continue;
				IL_1D6F:
				if (rtfKeyword.Value < 0 || rtfKeyword.Value >= this.colorsCount)
				{
					continue;
				}
				this.state.FontColor = this.colors[rtfKeyword.Value];
				if (this.state.FontColor == 16777215)
				{
					this.state.FontColor = 13619151;
					continue;
				}
				continue;
				IL_1E32:
				this.state.Underline = (0 != rtfKeyword.Value);
			}
			if (this.parser.CurrentFontIndex < this.fontsCount)
			{
				this.state.FontIndex = this.parser.CurrentFontIndex;
			}
			this.state.FontSize = this.parser.CurrentFontSize;
			this.state.Language = this.parser.CurrentLanguage;
			this.state.Bold = this.parser.CurrentFontBold;
			this.state.Italic = this.parser.CurrentFontItalic;
			this.state.BiDi = this.parser.CurrentRunBiDi;
			this.state.ComplexScript = this.parser.CurrentRunComplexScript;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000B91D8 File Offset: 0x000B73D8
		private void ProcessText(RtfToken token)
		{
			if (this.state.SkipLevel != 0 && this.state.Level >= this.state.SkipLevel)
			{
				return;
			}
			switch (this.state.Destination)
			{
			case RtfDestination.RTF:
				goto IL_1C7;
			case RtfDestination.FontTable:
				this.firstKeyword = false;
				this.scratch.AppendRtfTokenText(token, 256);
				return;
			case RtfDestination.RealFontName:
			case RtfDestination.AltFontName:
				this.firstKeyword = false;
				return;
			case RtfDestination.ColorTable:
				this.firstKeyword = false;
				this.scratch.Reset();
				while (this.scratch.AppendRtfTokenText(token, this.scratch.Capacity))
				{
					for (int i = 0; i < this.scratch.Length; i++)
					{
						if (this.scratch[i] == ';')
						{
							if (this.colorsCount == this.colors.Length)
							{
								return;
							}
							this.colors[this.colorsCount] = this.color;
							this.colorsCount++;
							this.color = 0;
						}
					}
					this.scratch.Reset();
				}
				return;
			case RtfDestination.FieldResult:
				if (this.ignoreFieldResult)
				{
					return;
				}
				goto IL_1C7;
			case RtfDestination.FieldInstruction:
				this.firstKeyword = false;
				this.scratch.AppendRtfTokenText(token, 4096);
				return;
			case RtfDestination.BookmarkName:
				this.firstKeyword = false;
				this.scratch.AppendRtfTokenText(token, 4096);
				return;
			}
			this.firstKeyword = false;
			return;
			IL_1C7:
			if (this.state.Invisible)
			{
				return;
			}
			RtfToken.TextEnumerator textElements = token.TextElements;
			if (textElements.MoveNext())
			{
				if (this.beforeContent)
				{
					this.PrepareToAddContent();
				}
				if (this.state.TextPropertiesChanged)
				{
					base.OpenTextContainer();
					if (this.state.FontSize != this.containerFontSize)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontSize, new PropertyValue(LengthUnits.Twips, (int)this.state.FontSize));
					}
					if (this.state.Bold)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FirstFlag, new PropertyValue(this.state.Bold));
					}
					if (this.state.Italic)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.Italic, new PropertyValue(this.state.Italic));
					}
					if (this.state.Underline)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.Underline, new PropertyValue(this.state.Underline));
					}
					if (this.state.Subscript)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.Subscript, new PropertyValue(this.state.Subscript));
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontSize, new PropertyValue(LengthUnits.Twips, (int)(this.state.FontSize * 2 / 3)));
					}
					if (this.state.Superscript)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.Superscript, new PropertyValue(this.state.Superscript));
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontSize, new PropertyValue(LengthUnits.Twips, (int)(this.state.FontSize * 2 / 3)));
					}
					if (this.state.Strikethrough)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.Strikethrough, new PropertyValue(this.state.Strikethrough));
					}
					if (this.state.SmallCaps)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.SmallCaps, new PropertyValue(this.state.SmallCaps));
					}
					if (this.state.Capitalize)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.Capitalize, new PropertyValue(this.state.Capitalize));
					}
					if (this.state.FontIndex != this.containerFontIndex && this.state.FontIndex != -1)
					{
						if (this.fonts[(int)this.state.FontIndex].Value.IsNull)
						{
							this.RegisterFontValue(this.state.FontIndex);
						}
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontFace, this.fonts[(int)this.state.FontIndex].Value);
					}
					if (this.state.FontColor != this.containerFontColor)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontColor, new PropertyValue(new RGBT((uint)this.state.FontColor)));
					}
					if (this.state.FontBackColor != this.containerBackColor)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.BackColor, new PropertyValue(new RGBT((uint)this.state.FontBackColor)));
					}
					this.state.TextPropertiesChanged = false;
				}
				if (this.firstKeyword)
				{
					RtfTextElement rtfTextElement = textElements.Current;
					if (rtfTextElement.TextType != RunTextType.Space || rtfTextElement.Length != 1)
					{
						if (textElements.MoveNext())
						{
							this.firstKeyword = false;
						}
						textElements.Rewind();
						textElements.MoveNext();
					}
				}
				do
				{
					RtfTextElement rtfTextElement = textElements.Current;
					RunTextType textType = rtfTextElement.TextType;
					if (textType <= RunTextType.UnusualWhitespace)
					{
						if (textType == RunTextType.Space || textType == RunTextType.UnusualWhitespace)
						{
							this.OutputSpace(rtfTextElement.Length);
						}
					}
					else if (textType != RunTextType.Nbsp)
					{
						if (textType == RunTextType.NonSpace)
						{
							this.OutputNonspace(rtfTextElement.RawBuffer, rtfTextElement.RawOffset, rtfTextElement.RawLength, token.TextMapping);
						}
					}
					else if (this.treatNbspAsBreakable)
					{
						this.OutputSpace(rtfTextElement.Length);
					}
					else
					{
						this.OutputNbsp(rtfTextElement.Length);
					}
				}
				while (textElements.MoveNext());
			}
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000B97D1 File Offset: 0x000B79D1
		private void ProcessBinary(RtfToken token)
		{
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000B97D4 File Offset: 0x000B79D4
		private void RegisterFontValue(short fontIndex)
		{
			PropertyValue value;
			if (this.fonts[(int)fontIndex].Name != null)
			{
				value = base.RegisterFaceName(false, this.fonts[(int)fontIndex].Name);
			}
			else
			{
				string text = RtfFormatConverter.familyGenericName[(int)this.parser.FontFamily(fontIndex)];
				if (text != null)
				{
					value = base.RegisterFaceName(false, text);
				}
				else
				{
					value = base.RegisterFaceName(false, "serif");
				}
			}
			this.fonts[(int)fontIndex].Value = value;
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x000B9850 File Offset: 0x000B7A50
		private void ProcessEOF()
		{
			while (base.LastNonEmpty.Type != FormatContainerType.Document)
			{
				base.CloseContainer();
			}
			if (this.injection != null && this.injection.HaveTail)
			{
				while (base.LastNonEmpty.Type != FormatContainerType.Document)
				{
					base.CloseContainer();
				}
			}
			base.CloseAllContainersAndSetEOF();
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000B98B4 File Offset: 0x000B7AB4
		private void EndGroup()
		{
			short listIndex = -1;
			byte b = 0;
			RtfNumbering listStyle = RtfNumbering.None;
			int fontColor = 0;
			short fontSize = 0;
			short fontIndex = 0;
			RtfDestination destination = this.state.Destination;
			if (destination <= RtfDestination.ParaNumText)
			{
				switch (destination)
				{
				case RtfDestination.FontTable:
					if (this.state.ParentDestination != RtfDestination.FontTable)
					{
						if (this.state.FontIndex >= 0)
						{
							this.fonts[(int)this.state.FontIndex].Name = RtfSupport.StringFontNameFromScratch(this.scratch);
							this.scratch.Reset();
						}
					}
					else if (this.state.FontIndex >= 0)
					{
						this.state.SetParentFontIndex(this.state.FontIndex);
					}
					break;
				case RtfDestination.RealFontName:
					if (this.state.ParentDestination != RtfDestination.RealFontName && this.state.ParentFontIndex >= 0)
					{
						this.scratchAlt.Reset();
					}
					break;
				case RtfDestination.AltFontName:
					if (this.state.ParentDestination != RtfDestination.AltFontName && this.state.ParentFontIndex >= 0)
					{
						this.scratchAlt.Reset();
					}
					break;
				default:
					switch (destination)
					{
					case RtfDestination.Field:
						if (this.state.ParentDestination != RtfDestination.Field)
						{
							this.ignoreFieldResult = false;
							if (base.LastNonEmpty.Type == FormatContainerType.HyperLink && this.state.ParentDestination != RtfDestination.FieldResult)
							{
								base.CloseContainer();
							}
						}
						break;
					case RtfDestination.FieldInstruction:
						if (this.state.ParentDestination == RtfDestination.Field)
						{
							bool flag;
							BufferString value;
							TextMapping textMapping;
							char c;
							short num;
							if (RtfSupport.IsHyperlinkField(ref this.scratch, out flag, out value))
							{
								if (this.beforeContent)
								{
									this.PrepareToAddContent();
								}
								else if (base.LastNonEmpty.Type == FormatContainerType.HyperLink)
								{
									base.CloseContainer();
								}
								value.TrimWhitespace();
								base.OpenContainer(FormatContainerType.HyperLink, false);
								base.Last.SetStringProperty(PropertyPrecedence.InlineStyle, PropertyId.HyperlinkUrl, value);
								this.documentContainerStillOpen = false;
							}
							else if (RtfSupport.IsIncludePictureField(ref this.scratch, out value))
							{
								if (this.beforeContent)
								{
									this.PrepareToAddContent();
								}
								value.TrimWhitespace();
								base.OpenContainer(FormatContainerType.Image, true);
								base.Last.SetStringProperty(PropertyPrecedence.InlineStyle, PropertyId.ImageUrl, value);
								this.documentContainerStillOpen = false;
								this.ignoreFieldResult = true;
							}
							else if (RtfSupport.IsSymbolField(ref this.scratch, out textMapping, out c, out num))
							{
								if (this.beforeContent)
								{
									this.PrepareToAddContent();
								}
								base.OpenTextContainer();
								PropertyValue value2 = default(PropertyValue);
								switch (textMapping)
								{
								case TextMapping.Symbol:
									value2 = base.RegisterFaceName(false, "Symbol");
									goto IL_397;
								}
								value2 = base.RegisterFaceName(false, "Wingdings");
								IL_397:
								if (!value2.IsNull)
								{
									base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontFace, value2);
								}
								if (num != 0)
								{
									base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontSize, new PropertyValue(LengthUnits.Points, (int)num));
								}
								this.scratch.Buffer[0] = c;
								this.OutputNonspace(this.scratch.Buffer, 0, 1, textMapping);
								this.documentContainerStillOpen = false;
								this.ignoreFieldResult = true;
							}
							this.scratch.Reset();
						}
						break;
					case RtfDestination.ParaNumbering:
					case RtfDestination.ParaNumText:
						listIndex = this.state.ListIndex;
						b = this.state.ListLevel;
						listStyle = this.state.ListStyle;
						fontColor = this.state.FontColor;
						fontSize = this.state.FontSize;
						fontIndex = this.state.FontIndex;
						break;
					}
					break;
				}
			}
			else if (destination != RtfDestination.Picture)
			{
				if (destination == RtfDestination.BookmarkName)
				{
					if (this.state.ParentDestination != RtfDestination.BookmarkName)
					{
						BufferString bufferString = this.scratch.BufferString;
						bufferString.TrimWhitespace();
						if (bufferString.Length != 0)
						{
							bool flag2 = this.beforeContent;
							base.OpenContainer(FormatContainerType.Bookmark, false);
							base.Last.SetStringProperty(PropertyPrecedence.InlineStyle, PropertyId.BookmarkName, bufferString);
							base.CloseContainer();
							this.documentContainerStillOpen = false;
						}
						this.scratch.Reset();
					}
				}
			}
			else if (this.state.ParentDestination != RtfDestination.Picture)
			{
				if (!this.ignoreFieldResult)
				{
					if (this.beforeContent)
					{
						this.PrepareToAddContent();
					}
					base.OpenContainer(FormatContainerType.Image, true);
					base.Last.SetStringProperty(PropertyPrecedence.InlineStyle, PropertyId.ImageUrl, "rtfimage://");
					this.documentContainerStillOpen = false;
				}
				if (base.Last.Type == FormatContainerType.Image)
				{
					if (this.pictureWidth != 0)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.Width, new PropertyValue(LengthUnits.Twips, this.pictureWidth));
					}
					if (this.pictureHeight != 0)
					{
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.Height, new PropertyValue(LengthUnits.Twips, this.pictureHeight));
					}
				}
			}
			RtfDestination destination2 = this.state.Destination;
			this.state.Pop();
			if (destination2 == RtfDestination.ParaNumbering && this.state.Destination != RtfDestination.ParaNumbering)
			{
				this.state.ListIndex = listIndex;
				this.state.ListLevel = b;
				this.state.ListStyle = listStyle;
				this.state.FontColor = fontColor;
				this.state.FontSize = fontSize;
				this.state.FontIndex = fontIndex;
			}
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x000B9DFC File Offset: 0x000B7FFC
		private void EventEndParagraph()
		{
			if (this.beforeContent)
			{
				this.PrepareToAddContent();
				base.AddNbsp(1);
			}
			if (base.LastNonEmpty.Type == FormatContainerType.HyperLink)
			{
				base.CloseContainer();
			}
			if (base.LastNonEmpty.Type == FormatContainerType.Block)
			{
				base.CloseContainer();
			}
			if (base.LastNonEmpty.Type == FormatContainerType.ListItem)
			{
				base.CloseContainer();
			}
			this.beforeContent = true;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x000B9E74 File Offset: 0x000B8074
		private void EventEndRow(bool nested)
		{
			if (this.beforeContent && (this.requiredTableLevel != this.currentTableLevel || base.LastNonEmpty.Type != FormatContainerType.TableRow || base.LastNonEmpty.Node.LastChild.IsNull))
			{
				this.PrepareToAddContent();
			}
			if (this.currentTableLevel > (nested ? 1 : 0))
			{
				if (!nested && 1 != this.currentTableLevel)
				{
					this.AdjustTableLevel(1);
				}
				else if (-1 != this.currentListState.ListIndex)
				{
					this.CloseList();
				}
				else
				{
					if (base.LastNonEmpty.Type == FormatContainerType.HyperLink)
					{
						base.CloseContainer();
					}
					if (base.LastNonEmpty.Type == FormatContainerType.Block)
					{
						base.CloseContainer();
					}
				}
				if (base.LastNonEmpty.Type == FormatContainerType.Table)
				{
					this.OpenRow();
					this.OpenCell();
				}
				if (base.LastNonEmpty.Type == FormatContainerType.TableCell)
				{
					this.CloseCell();
				}
				this.CloseRow(false);
				this.beforeContent = true;
				return;
			}
			this.EventEndParagraph();
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x000B9F98 File Offset: 0x000B8198
		private void EventEndCell(bool nested)
		{
			if (this.beforeContent)
			{
				this.PrepareToAddContent();
			}
			if (this.currentTableLevel > (nested ? 1 : 0))
			{
				if (!nested && 1 != this.currentTableLevel)
				{
					this.AdjustTableLevel(1);
				}
				else if (-1 != this.currentListState.ListIndex)
				{
					this.CloseList();
				}
				else
				{
					if (base.LastNonEmpty.Type == FormatContainerType.HyperLink)
					{
						base.CloseContainer();
					}
					if (base.LastNonEmpty.Type == FormatContainerType.Block)
					{
						base.CloseContainer();
					}
				}
				if (base.LastNonEmpty.Type == FormatContainerType.Table)
				{
					this.OpenRow();
				}
				if (base.LastNonEmpty.Type == FormatContainerType.TableRow)
				{
					this.OpenCell();
				}
				this.CloseCell();
				this.beforeContent = true;
				return;
			}
			if (!nested)
			{
				base.AddLineBreak(1);
				this.documentContainerStillOpen = false;
			}
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x000BA078 File Offset: 0x000B8278
		private void AdjustContainer()
		{
			if (this.requiredTableLevel != this.currentTableLevel)
			{
				this.AdjustTableLevel(this.requiredTableLevel);
			}
			if (this.state.ListIndex != this.currentListState.ListIndex || this.state.ListLevel != this.currentListState.ListLevel || this.state.ListStyle != this.currentListState.ListStyle || this.state.LeftIndent != this.currentListState.LeftIndent || this.state.RightIndent != this.currentListState.RightIndent || this.state.FirstLineIndent != this.currentListState.FirstLineIndent || this.state.ParagraphRtl != this.currentListState.Rtl)
			{
				this.CloseList();
			}
			if (this.state.ListIndex != -1)
			{
				this.OpenList();
			}
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x000BA164 File Offset: 0x000B8364
		private void PrepareToAddContent()
		{
			if (this.documentContainerStillOpen)
			{
				short num = this.state.FontIndex;
				if (this.state.FontIndex != -1)
				{
					if (this.fonts[(int)this.state.FontIndex].Name != null && !this.fonts[(int)this.state.FontIndex].Name.StartsWith("Wingdings", StringComparison.OrdinalIgnoreCase) && !this.fonts[(int)this.state.FontIndex].Name.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
					{
						if (this.fonts[(int)this.state.FontIndex].Value.IsNull)
						{
							this.RegisterFontValue(this.state.FontIndex);
						}
						base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontFace, this.fonts[(int)this.state.FontIndex].Value);
					}
					else
					{
						num = -1;
					}
				}
				if (this.state.FontSize != RtfFormatConverter.TwelvePointsInTwips)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontSize, new PropertyValue(LengthUnits.Twips, (int)this.state.FontSize));
				}
				this.documentFontIndex = num;
				this.documentFontSize = this.state.FontSize;
				this.documentContainerStillOpen = false;
			}
			this.AdjustContainer();
			if (base.LastNonEmpty.Type == FormatContainerType.Document)
			{
				base.OpenContainer(FormatContainerType.Block, false);
			}
			else if (base.LastNonEmpty.Type == FormatContainerType.Table)
			{
				this.OpenRow();
				this.OpenCell();
			}
			else if (base.LastNonEmpty.Type == FormatContainerType.TableRow)
			{
				this.OpenCell();
			}
			else if (base.LastNonEmpty.Type == FormatContainerType.List)
			{
				base.OpenContainer(FormatContainerType.ListItem, false);
				RtfFormatConverter.ListLevel[] levels = this.lists[(int)this.currentListState.ListIndex].Levels;
				byte b = this.currentListState.ListLevel;
				levels[(int)b].Start = levels[(int)b].Start + 1;
			}
			if (base.LastNonEmpty.Type == FormatContainerType.TableCell && !base.LastNonEmpty.Node.FirstChild.IsNull)
			{
				base.OpenContainer(FormatContainerType.Block, false);
			}
			if (base.LastNonEmpty.Type != FormatContainerType.ListItem)
			{
				short fontIndex = this.state.FontIndex;
				if (this.state.FontIndex != -1 && this.state.FontIndex != this.documentFontIndex && this.fonts[(int)this.state.FontIndex].Name != null && !this.fonts[(int)this.state.FontIndex].Name.StartsWith("Wingdings", StringComparison.OrdinalIgnoreCase) && !this.fonts[(int)this.state.FontIndex].Name.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
				{
					if (this.fonts[(int)this.state.FontIndex].Value.IsNull)
					{
						this.RegisterFontValue(this.state.FontIndex);
					}
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontFace, this.fonts[(int)this.state.FontIndex].Value);
				}
				else
				{
					fontIndex = this.documentFontIndex;
				}
				this.containerFontIndex = fontIndex;
				if (this.state.FontSize != this.documentFontSize)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontSize, new PropertyValue(LengthUnits.Twips, (int)this.state.FontSize));
				}
				this.containerFontSize = this.state.FontSize;
				if (this.state.FontColor != 0)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontColor, new PropertyValue(new RGBT((uint)this.state.FontColor)));
				}
				this.containerFontColor = this.state.FontColor;
				if (this.state.ParagraphBackColor != 16777215)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.BackColor, new PropertyValue(new RGBT((uint)this.state.ParagraphBackColor)));
				}
				this.containerBackColor = this.state.ParagraphBackColor;
			}
			if (this.state.SpaceBefore != 0)
			{
				base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.Margins, new PropertyValue(LengthUnits.Twips, (int)this.state.SpaceBefore));
			}
			if (this.state.SpaceAfter != 0)
			{
				base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.BottomMargin, new PropertyValue(LengthUnits.Twips, (int)this.state.SpaceAfter));
			}
			if (base.LastNonEmpty.Type != FormatContainerType.ListItem)
			{
				if (this.state.LeftIndent != 0)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.LeftPadding, new PropertyValue(LengthUnits.Twips, (int)this.state.LeftIndent));
				}
				if (this.state.RightIndent != 0)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.RightPadding, new PropertyValue(LengthUnits.Twips, (int)this.state.RightIndent));
				}
				if (this.state.FirstLineIndent != 0)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FirstLineIndent, new PropertyValue(LengthUnits.Twips, (int)this.state.FirstLineIndent));
				}
				if (this.state.ParagraphAlignment != RtfAlignment.Left && this.state.ParagraphAlignment < RtfAlignment.Distributed)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.TextAlignment, new PropertyValue((TextAlign)this.state.ParagraphAlignment));
				}
				if (this.state.ParagraphRtl)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.RightToLeft, new PropertyValue(this.state.ParagraphRtl));
				}
			}
			this.state.TextPropertiesChanged = true;
			this.beforeContent = false;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x000BA768 File Offset: 0x000B8968
		private void AdjustTableLevel(int requiredTableLevel)
		{
			if (-1 != this.currentListState.ListIndex)
			{
				this.CloseList();
			}
			if (base.LastNonEmpty.Type == FormatContainerType.HyperLink)
			{
				base.CloseContainer();
			}
			if (base.LastNonEmpty.Type == FormatContainerType.Block)
			{
				base.CloseContainer();
			}
			if (this.currentTableLevel < requiredTableLevel)
			{
				if (base.LastNonEmpty.Type == FormatContainerType.Table)
				{
					this.OpenRow();
				}
				if (base.LastNonEmpty.Type == FormatContainerType.TableRow)
				{
					this.OpenCell();
				}
				do
				{
					this.OpenTable();
					this.OpenRow();
					this.OpenCell();
				}
				while (this.currentTableLevel < requiredTableLevel);
				this.documentContainerStillOpen = false;
				return;
			}
			do
			{
				if (base.LastNonEmpty.Type == FormatContainerType.TableCell)
				{
					this.CloseCell();
				}
				if (base.LastNonEmpty.Type == FormatContainerType.TableRow)
				{
					this.CloseRow(true);
				}
				this.CloseTable();
			}
			while (this.currentTableLevel < requiredTableLevel);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000BA868 File Offset: 0x000B8A68
		private void CloseList()
		{
			if (base.LastNonEmpty.Type == FormatContainerType.HyperLink)
			{
				base.CloseContainer();
			}
			if (base.LastNonEmpty.Type == FormatContainerType.Block)
			{
				base.CloseContainer();
			}
			if (base.LastNonEmpty.Type == FormatContainerType.ListItem)
			{
				base.CloseContainer();
			}
			if (base.LastNonEmpty.Type == FormatContainerType.List)
			{
				base.CloseContainer();
			}
			this.currentListState.ListIndex = -1;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000BA8EC File Offset: 0x000B8AEC
		private void OpenList()
		{
			if (base.LastNonEmpty.Type == FormatContainerType.HyperLink)
			{
				base.CloseContainer();
			}
			if (base.LastNonEmpty.Type == FormatContainerType.Block)
			{
				base.CloseContainer();
			}
			if (base.LastNonEmpty.Type == FormatContainerType.ListItem)
			{
				base.CloseContainer();
			}
			if (base.LastNonEmpty.Type != FormatContainerType.List)
			{
				if (base.LastNonEmpty.Type == FormatContainerType.Table)
				{
					this.OpenRow();
				}
				if (base.LastNonEmpty.Type == FormatContainerType.TableRow)
				{
					this.OpenCell();
				}
				base.OpenContainer(FormatContainerType.List, false);
				this.currentListState.ListIndex = this.state.ListIndex;
				this.currentListState.ListLevel = this.state.ListLevel;
				this.currentListState.ListStyle = this.state.ListStyle;
				this.currentListState.LeftIndent = this.state.LeftIndent;
				this.currentListState.RightIndent = this.state.RightIndent;
				this.currentListState.FirstLineIndent = this.state.FirstLineIndent;
				this.currentListState.Rtl = this.state.ParagraphRtl;
				base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.Margins, new PropertyValue(LengthUnits.Twips, 0));
				base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.RightMargin, new PropertyValue(LengthUnits.Twips, 0));
				base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.BottomMargin, new PropertyValue(LengthUnits.Twips, 0));
				base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.LeftMargin, new PropertyValue(LengthUnits.Twips, 0));
				if (this.lists[(int)this.currentListState.ListIndex].Levels[(int)this.currentListState.ListLevel].Start != 1)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.ListStart, new PropertyValue(PropertyType.Integer, (int)this.lists[(int)this.currentListState.ListIndex].Levels[(int)this.currentListState.ListLevel].Start));
				}
				if (this.state.ParagraphBackColor != 16777215)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.BackColor, new PropertyValue(new RGBT((uint)this.state.ParagraphBackColor)));
				}
				this.containerBackColor = this.state.ParagraphBackColor;
				if (this.state.ListStyle != RtfNumbering.Bullet)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.ListStyle, new PropertyValue((ListStyle)this.state.ListStyle));
				}
				if (this.state.LeftIndent != 0)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.LeftPadding, new PropertyValue(LengthUnits.Twips, (int)this.state.LeftIndent));
				}
				if (this.state.RightIndent != 0)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.RightMargin, new PropertyValue(LengthUnits.Twips, (int)this.state.RightIndent));
				}
				if (this.state.ParagraphAlignment != RtfAlignment.Left && this.state.ParagraphAlignment < RtfAlignment.Distributed)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.TextAlignment, new PropertyValue((TextAlign)this.state.ParagraphAlignment));
				}
				if (this.state.ParagraphRtl)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.RightToLeft, new PropertyValue(this.state.ParagraphRtl));
				}
				short fontIndex = this.state.FontIndex;
				if (this.state.FontIndex != -1 && this.state.FontIndex != this.documentFontIndex && this.fonts[(int)this.state.FontIndex].Name != null && !this.fonts[(int)this.state.FontIndex].Name.StartsWith("Wingdings", StringComparison.OrdinalIgnoreCase) && !this.fonts[(int)this.state.FontIndex].Name.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
				{
					if (this.fonts[(int)this.state.FontIndex].Value.IsNull)
					{
						this.RegisterFontValue(this.state.FontIndex);
					}
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontFace, this.fonts[(int)this.state.FontIndex].Value);
				}
				else
				{
					fontIndex = this.documentFontIndex;
				}
				this.containerFontIndex = fontIndex;
				if (this.state.FontSize != this.documentFontSize)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontSize, new PropertyValue(LengthUnits.Twips, (int)this.state.FontSize));
				}
				this.containerFontSize = this.state.FontSize;
				if (this.state.FontColor != 0)
				{
					base.Last.SetProperty(PropertyPrecedence.InlineStyle, PropertyId.FontColor, new PropertyValue(new RGBT((uint)this.state.FontColor)));
				}
				this.containerFontColor = this.state.FontColor;
				this.documentContainerStillOpen = false;
			}
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000BAE18 File Offset: 0x000B9018
		private void InitializeFreshRowProperties()
		{
			if (this.state.Destination == RtfDestination.NestTableProps)
			{
				if (this.newRowNested != null)
				{
					this.newRowNested.NextFree = this.firstFreeRow;
					this.firstFreeRow = this.newRowNested;
				}
			}
			else if (this.newRowTopLevel != null)
			{
				this.newRowTopLevel.NextFree = this.firstFreeRow;
				this.firstFreeRow = this.newRowTopLevel;
			}
			if (this.firstFreeRow != null)
			{
				this.newRow = this.firstFreeRow;
				this.firstFreeRow = this.newRow.NextFree;
				this.newRow.NextFree = null;
				this.newRow.Initialize();
			}
			else
			{
				this.newRow = new RtfFormatConverter.RtfRow();
			}
			if (this.state.Destination == RtfDestination.NestTableProps)
			{
				this.newRowNested = this.newRow;
				return;
			}
			this.newRowTopLevel = this.newRow;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000BAEF4 File Offset: 0x000B90F4
		private void OpenTable()
		{
			if (this.tableStack == null)
			{
				this.tableStack = new RtfFormatConverter.RtfTable[4];
			}
			else if (this.currentTableLevel == this.tableStack.Length)
			{
				RtfFormatConverter.RtfTable[] destinationArray = new RtfFormatConverter.RtfTable[this.currentTableLevel * 2];
				Array.Copy(this.tableStack, 0, destinationArray, 0, this.currentTableLevel);
				this.tableStack = destinationArray;
			}
			this.currentTableLevel++;
			this.tableStack[this.currentTableLevel - 1].Initialize();
			this.tableStack[this.currentTableLevel - 1].TableContainer = base.OpenContainer(FormatContainerType.Table, false);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000BAF9C File Offset: 0x000B919C
		private void CloseTable()
		{
			FormatNode node = this.tableStack[this.currentTableLevel - 1].TableContainer.Node;
			FormatNode newChildNode = base.CreateNode(FormatContainerType.TableDefinition);
			node.PrependChild(newChildNode);
			for (int i = 0; i < (int)this.tableStack[this.currentTableLevel - 1].ColumnCount; i++)
			{
				FormatNode newChildNode2 = base.CreateNode(FormatContainerType.TableColumn);
				newChildNode2.SetProperty(PropertyId.Width, new PropertyValue(LengthUnits.Twips, (int)this.tableStack[this.currentTableLevel - 1].Columns[i].Width));
				newChildNode.AppendChild(newChildNode2);
			}
			base.CloseContainer();
			this.tableStack[this.currentTableLevel - 1].TableContainer = FormatConverterContainer.Null;
			this.currentTableLevel--;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000BB070 File Offset: 0x000B9270
		private void OpenRow()
		{
			if (this.currentTableLevel == 1 && !this.tableStack[this.currentTableLevel - 1].FirstRow && this.newRowTopLevel != null && this.newRowTopLevel.CellCount != 0 && (this.newRowTopLevel.CellCount != this.tableStack[this.currentTableLevel - 1].ColumnCount || this.newRowTopLevel.Left != this.tableStack[this.currentTableLevel - 1].Left || this.newRowTopLevel.Right - this.newRowTopLevel.Left != this.tableStack[this.currentTableLevel - 1].Width))
			{
				this.CloseTable();
				this.OpenTable();
			}
			this.tableStack[this.currentTableLevel - 1].RowContainer = base.OpenContainer(FormatContainerType.TableRow, false);
			this.tableStack[this.currentTableLevel - 1].CellIndex = -1;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000BB188 File Offset: 0x000B9388
		private void CloseRow(bool closingTable)
		{
			RtfFormatConverter.RtfRow rtfRow;
			if (this.currentTableLevel == 1)
			{
				rtfRow = this.newRowTopLevel;
			}
			else
			{
				rtfRow = this.newRowNested;
				this.newRowNested = null;
			}
			int num = this.currentTableLevel - 1;
			bool flag = false;
			if (rtfRow != null)
			{
				flag = rtfRow.LastRow;
				FormatNode node = this.tableStack[num].TableContainer.Node;
				FormatNode node2 = this.tableStack[num].RowContainer.Node;
				if (rtfRow.Height != 0)
				{
					node2.SetProperty(PropertyId.Height, new PropertyValue(LengthUnits.Twips, (int)rtfRow.Height));
				}
				if (this.tableStack[num].FirstRow)
				{
					if (rtfRow.Left != 0)
					{
						node.SetProperty(PropertyId.LeftMargin, new PropertyValue(LengthUnits.Twips, (int)rtfRow.Left));
					}
					node.SetProperty(PropertyId.Width, new PropertyValue(LengthUnits.Twips, (int)(rtfRow.Right - rtfRow.Left)));
					short count = Math.Max(rtfRow.CellCount, this.tableStack[num].CellIndex + 1);
					this.tableStack[num].RightToLeft = rtfRow.RightToLeft;
					this.tableStack[num].Left = rtfRow.Left;
					this.tableStack[num].Width = rtfRow.Right - rtfRow.Left;
					if (rtfRow.WidthType == 2)
					{
						this.tableStack[num].WidthPercentage = rtfRow.Width;
					}
					this.tableStack[num].BackColor = rtfRow.BackColor;
					this.tableStack[num].BorderKind = rtfRow.BorderKind;
					this.tableStack[num].BorderWidth = rtfRow.BorderWidth;
					this.tableStack[num].BorderColor = rtfRow.BorderColor;
					this.tableStack[num].EnsureColumns((int)count);
					FormatNode verticalMergeCell = node2.FirstChild;
					int num2 = 0;
					int num3 = (int)this.tableStack[num].Left;
					int num4 = 0;
					while (num4 < (int)rtfRow.CellCount && !verticalMergeCell.IsNull)
					{
						FormatNode nextSibling = verticalMergeCell.NextSibling;
						this.tableStack[num].Columns[num2].Cellx = rtfRow.Cells[num4].Cellx;
						this.tableStack[num].Columns[num2].Width = (short)((int)rtfRow.Cells[num4].Cellx - num3);
						if (rtfRow.Cells[num4].WidthType == 2)
						{
							this.tableStack[num].Columns[num2].WidthPercentage = rtfRow.Cells[num4].Width;
						}
						if (num4 == 0 || !rtfRow.Cells[num4].MergeLeft)
						{
							this.tableStack[num].Columns[num2].BackColor = rtfRow.Cells[num4].BackColor;
							this.tableStack[num].Columns[num2].VerticalMergeCell = verticalMergeCell;
							if (rtfRow.Cells[num4].BackColor != 16777215)
							{
								verticalMergeCell.SetProperty(PropertyId.BackColor, new PropertyValue(new RGBT((uint)rtfRow.Cells[num4].BackColor)));
							}
						}
						else
						{
							this.tableStack[num].Columns[num2].BackColor = this.tableStack[num].Columns[num4 - 1].BackColor;
							FormatNode previousSibling = verticalMergeCell.PreviousSibling;
							while (!verticalMergeCell.FirstChild.IsNull)
							{
								FormatNode firstChild = verticalMergeCell.FirstChild;
								firstChild.RemoveFromParent();
								previousSibling.AppendChild(firstChild);
							}
							verticalMergeCell.RemoveFromParent();
							int num5 = 1;
							PropertyValue property = previousSibling.GetProperty(PropertyId.NumColumns);
							if (!property.IsNull)
							{
								num5 = property.Integer;
							}
							previousSibling.SetProperty(PropertyId.NumColumns, new PropertyValue(PropertyType.Integer, num5 + 1));
							this.tableStack[num].Columns[num2].VerticalMergeCell = FormatNode.Null;
						}
						num3 = (int)rtfRow.Cells[num4].Cellx;
						num2++;
						verticalMergeCell = nextSibling;
						num4++;
					}
					this.tableStack[num].ColumnCount = (short)num4;
				}
				else if (rtfRow.Left == this.tableStack[num].Left && rtfRow.Right - rtfRow.Left == this.tableStack[num].Width)
				{
					FormatNode verticalMergeCell2 = node2.FirstChild;
					int num6 = 0;
					int num7 = 0;
					while (num7 < (int)rtfRow.CellCount && !verticalMergeCell2.IsNull && num6 < (int)this.tableStack[num].ColumnCount)
					{
						int num8 = num6;
						while (num8 < (int)this.tableStack[num].ColumnCount && rtfRow.Cells[num7].Cellx > this.tableStack[num].Columns[num8].Cellx)
						{
							num8++;
							if (num8 < (int)this.tableStack[num].ColumnCount)
							{
								this.tableStack[num].Columns[num8].VerticalMergeCell = FormatNode.Null;
							}
						}
						if (num8 < (int)this.tableStack[num].ColumnCount && rtfRow.Cells[num7].Cellx < this.tableStack[num].Columns[num8].Cellx)
						{
							this.tableStack[num].InsertColumn(num8, rtfRow.Cells[num7].Cellx);
							FormatNode x = node.FirstChild;
							while (x != node.LastChild)
							{
								FormatNode formatNode = x.FirstChild;
								int num9 = 0;
								while (!formatNode.IsNull)
								{
									int num10 = 1;
									PropertyValue property2 = formatNode.GetProperty(PropertyId.NumColumns);
									if (!property2.IsNull)
									{
										num10 = property2.Integer;
									}
									if (num9 + num10 > num8)
									{
										formatNode.SetProperty(PropertyId.NumColumns, new PropertyValue(PropertyType.Integer, num10 + 1));
										break;
									}
									num9 += num10;
									formatNode = formatNode.NextSibling;
								}
								x = x.NextSibling;
							}
						}
						FormatNode nextSibling2 = verticalMergeCell2.NextSibling;
						if (num7 != 0 && rtfRow.Cells[num7].MergeLeft)
						{
							this.tableStack[num].Columns[num6].VerticalMergeCell = FormatNode.Null;
							FormatNode previousSibling2 = verticalMergeCell2.PreviousSibling;
							while (!verticalMergeCell2.FirstChild.IsNull)
							{
								FormatNode firstChild2 = verticalMergeCell2.FirstChild;
								firstChild2.RemoveFromParent();
								previousSibling2.AppendChild(firstChild2);
							}
							verticalMergeCell2.RemoveFromParent();
							int num11 = 1;
							PropertyValue property3 = previousSibling2.GetProperty(PropertyId.NumColumns);
							if (!property3.IsNull)
							{
								num11 = property3.Integer;
							}
							previousSibling2.SetProperty(PropertyId.NumColumns, new PropertyValue(PropertyType.Integer, num11 + (num8 - num6 + 1)));
						}
						else
						{
							if (num8 != num6)
							{
								verticalMergeCell2.SetProperty(PropertyId.NumColumns, new PropertyValue(PropertyType.Integer, num8 - num6 + 1));
							}
							if (rtfRow.Cells[num7].MergeUp)
							{
								FormatNode verticalMergeCell3 = this.tableStack[num].Columns[num6].VerticalMergeCell;
								if (!verticalMergeCell3.IsNull)
								{
									verticalMergeCell2.SetProperty(PropertyId.MergedCell, new PropertyValue(true));
									int num12 = 1;
									PropertyValue property4 = verticalMergeCell3.GetProperty(PropertyId.NumRows);
									if (!property4.IsNull)
									{
										num12 = property4.Integer;
									}
									verticalMergeCell3.SetProperty(PropertyId.NumRows, new PropertyValue(PropertyType.Integer, num12 + 1));
								}
							}
							else
							{
								this.tableStack[num].Columns[num6].VerticalMergeCell = verticalMergeCell2;
								if (rtfRow.Cells[num7].BackColor != 16777215)
								{
									verticalMergeCell2.SetProperty(PropertyId.BackColor, new PropertyValue(new RGBT((uint)rtfRow.Cells[num7].BackColor)));
								}
							}
						}
						num6 = num8 + 1;
						verticalMergeCell2 = nextSibling2;
						num7++;
					}
				}
			}
			base.CloseContainer();
			this.tableStack[num].RowContainer = FormatConverterContainer.Null;
			this.tableStack[num].FirstRow = false;
			if (!closingTable && flag)
			{
				this.CloseTable();
			}
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x000BBA1C File Offset: 0x000B9C1C
		private void OpenCell()
		{
			RtfFormatConverter.RtfTable[] array = this.tableStack;
			int num = this.currentTableLevel - 1;
			array[num].CellIndex = array[num].CellIndex + 1;
			this.tableStack[this.currentTableLevel - 1].CellContainer = base.OpenContainer(FormatContainerType.TableCell, false);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x000BBA6E File Offset: 0x000B9C6E
		private void CloseCell()
		{
			base.CloseContainer();
			this.tableStack[this.currentTableLevel - 1].CellContainer = FormatConverterContainer.Null;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x000BBA94 File Offset: 0x000B9C94
		private void SetBorderKind(RtfBorderKind kind)
		{
			switch (this.borderId)
			{
			case RtfBorderId.Left:
			case RtfBorderId.Top:
			case RtfBorderId.Right:
			case RtfBorderId.Bottom:
				this.state.SetParagraphBorderKind(this.borderId, kind);
				return;
			case RtfBorderId.RowLeft:
			case RtfBorderId.RowTop:
			case RtfBorderId.RowRight:
			case RtfBorderId.RowBottom:
			case RtfBorderId.RowHorizontal:
			case RtfBorderId.RowVertical:
				if (this.newRow != null)
				{
					this.newRow.SetBorderKind(this.borderId, kind);
					return;
				}
				break;
			case RtfBorderId.CellLeft:
			case RtfBorderId.CellTop:
			case RtfBorderId.CellRight:
			case RtfBorderId.CellBottom:
				if (this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
				{
					this.newRow.Cells[(int)this.newRow.CellCount].SetBorderKind(this.borderId, kind);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000BBB54 File Offset: 0x000B9D54
		private void SetBorderColor(int color)
		{
			switch (this.borderId)
			{
			case RtfBorderId.Left:
			case RtfBorderId.Top:
			case RtfBorderId.Right:
			case RtfBorderId.Bottom:
				this.state.SetParagraphBorderColor(this.borderId, color);
				return;
			case RtfBorderId.RowLeft:
			case RtfBorderId.RowTop:
			case RtfBorderId.RowRight:
			case RtfBorderId.RowBottom:
			case RtfBorderId.RowHorizontal:
			case RtfBorderId.RowVertical:
				if (this.newRow != null)
				{
					this.newRow.SetBorderColor(this.borderId, color);
					return;
				}
				break;
			case RtfBorderId.CellLeft:
			case RtfBorderId.CellTop:
			case RtfBorderId.CellRight:
			case RtfBorderId.CellBottom:
				if (this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
				{
					this.newRow.Cells[(int)this.newRow.CellCount].SetBorderColor(this.borderId, color);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x000BBC14 File Offset: 0x000B9E14
		private void SetBorderWidth(byte width)
		{
			switch (this.borderId)
			{
			case RtfBorderId.Left:
			case RtfBorderId.Top:
			case RtfBorderId.Right:
			case RtfBorderId.Bottom:
				this.state.SetParagraphBorderWidth(this.borderId, width);
				return;
			case RtfBorderId.RowLeft:
			case RtfBorderId.RowTop:
			case RtfBorderId.RowRight:
			case RtfBorderId.RowBottom:
			case RtfBorderId.RowHorizontal:
			case RtfBorderId.RowVertical:
				if (this.newRow != null)
				{
					this.newRow.SetBorderWidth(this.borderId, width);
					return;
				}
				break;
			case RtfBorderId.CellLeft:
			case RtfBorderId.CellTop:
			case RtfBorderId.CellRight:
			case RtfBorderId.CellBottom:
				if (this.newRow != null && this.newRow.EnsureEntryForCurrentCell())
				{
					this.newRow.Cells[(int)this.newRow.CellCount].SetBorderWidth(this.borderId, width);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000BBCD4 File Offset: 0x000B9ED4
		// Note: this type is marked as 'beforefieldinit'.
		static RtfFormatConverter()
		{
			string[] array = new string[8];
			array[1] = "serif";
			array[2] = "sans-serif";
			array[3] = "monospace";
			array[4] = "cursive";
			array[5] = "fantasy";
			RtfFormatConverter.familyGenericName = array;
		}

		// Token: 0x04001AB2 RID: 6834
		private const short TableDefaultLeftIndent = 108;

		// Token: 0x04001AB3 RID: 6835
		protected short listsCount;

		// Token: 0x04001AB4 RID: 6836
		protected RtfFormatConverter.List[] lists;

		// Token: 0x04001AB5 RID: 6837
		protected short[] listDirectory;

		// Token: 0x04001AB6 RID: 6838
		protected short listDirectoryCount;

		// Token: 0x04001AB7 RID: 6839
		protected short listIdx;

		// Token: 0x04001AB8 RID: 6840
		protected short listLevel;

		// Token: 0x04001AB9 RID: 6841
		private static readonly short TwelvePointsInTwips = 240;

		// Token: 0x04001ABA RID: 6842
		private static string[] familyGenericName;

		// Token: 0x04001ABB RID: 6843
		private RtfParser parser;

		// Token: 0x04001ABC RID: 6844
		private FormatOutput output;

		// Token: 0x04001ABD RID: 6845
		private bool firstKeyword;

		// Token: 0x04001ABE RID: 6846
		private bool ignorableDestination;

		// Token: 0x04001ABF RID: 6847
		private RtfFormatConverter.RtfState state;

		// Token: 0x04001AC0 RID: 6848
		private short fontsCount;

		// Token: 0x04001AC1 RID: 6849
		private RtfFont[] fonts;

		// Token: 0x04001AC2 RID: 6850
		private int colorsCount;

		// Token: 0x04001AC3 RID: 6851
		private int[] colors;

		// Token: 0x04001AC4 RID: 6852
		private short containerFontIndex;

		// Token: 0x04001AC5 RID: 6853
		private int containerFontColor;

		// Token: 0x04001AC6 RID: 6854
		private int containerBackColor;

		// Token: 0x04001AC7 RID: 6855
		private short containerFontSize;

		// Token: 0x04001AC8 RID: 6856
		private bool documentContainerStillOpen;

		// Token: 0x04001AC9 RID: 6857
		private short documentFontIndex;

		// Token: 0x04001ACA RID: 6858
		private short documentFontSize;

		// Token: 0x04001ACB RID: 6859
		private bool ignoreFieldResult;

		// Token: 0x04001ACC RID: 6860
		private bool treatNbspAsBreakable;

		// Token: 0x04001ACD RID: 6861
		private Injection injection;

		// Token: 0x04001ACE RID: 6862
		private int color;

		// Token: 0x04001ACF RID: 6863
		private ScratchBuffer scratch;

		// Token: 0x04001AD0 RID: 6864
		private ScratchBuffer scratchAlt;

		// Token: 0x04001AD1 RID: 6865
		private int pictureWidth;

		// Token: 0x04001AD2 RID: 6866
		private int pictureHeight;

		// Token: 0x04001AD3 RID: 6867
		private bool beforeContent = true;

		// Token: 0x04001AD4 RID: 6868
		private RtfBorderId borderId;

		// Token: 0x04001AD5 RID: 6869
		private RtfFormatConverter.RtfTable[] tableStack;

		// Token: 0x04001AD6 RID: 6870
		private int currentTableLevel;

		// Token: 0x04001AD7 RID: 6871
		private RtfFormatConverter.RtfRow newRow;

		// Token: 0x04001AD8 RID: 6872
		private RtfFormatConverter.RtfRow newRowTopLevel;

		// Token: 0x04001AD9 RID: 6873
		private RtfFormatConverter.RtfRow newRowNested;

		// Token: 0x04001ADA RID: 6874
		private int requiredTableLevel;

		// Token: 0x04001ADB RID: 6875
		private RtfFormatConverter.RtfRow firstFreeRow;

		// Token: 0x04001ADC RID: 6876
		private RtfFormatConverter.ListState currentListState;

		// Token: 0x0200023A RID: 570
		internal struct ListLevel
		{
			// Token: 0x04001ADD RID: 6877
			public RtfNumbering Type;

			// Token: 0x04001ADE RID: 6878
			public short Start;
		}

		// Token: 0x0200023B RID: 571
		internal struct List
		{
			// Token: 0x04001ADF RID: 6879
			public int Id;

			// Token: 0x04001AE0 RID: 6880
			public byte LevelCount;

			// Token: 0x04001AE1 RID: 6881
			public RtfFormatConverter.ListLevel[] Levels;
		}

		// Token: 0x0200023C RID: 572
		internal struct RtfFourSideValue<T>
		{
			// Token: 0x06001798 RID: 6040 RVA: 0x000BBD20 File Offset: 0x000B9F20
			public void Initialize(T value)
			{
				this.Bottom = value;
				this.Top = value;
				this.Right = value;
				this.Left = value;
			}

			// Token: 0x04001AE2 RID: 6882
			public T Left;

			// Token: 0x04001AE3 RID: 6883
			public T Top;

			// Token: 0x04001AE4 RID: 6884
			public T Right;

			// Token: 0x04001AE5 RID: 6885
			public T Bottom;
		}

		// Token: 0x0200023D RID: 573
		internal struct RtfSixSideValue<T>
		{
			// Token: 0x06001799 RID: 6041 RVA: 0x000BBD50 File Offset: 0x000B9F50
			public void Initialize(T value)
			{
				this.Vertical = value;
				this.Horizontal = value;
				this.Bottom = value;
				this.Top = value;
				this.Right = value;
				this.Left = value;
			}

			// Token: 0x04001AE6 RID: 6886
			public T Left;

			// Token: 0x04001AE7 RID: 6887
			public T Top;

			// Token: 0x04001AE8 RID: 6888
			public T Right;

			// Token: 0x04001AE9 RID: 6889
			public T Bottom;

			// Token: 0x04001AEA RID: 6890
			public T Horizontal;

			// Token: 0x04001AEB RID: 6891
			public T Vertical;
		}

		// Token: 0x0200023E RID: 574
		internal struct RtfRowCell
		{
			// Token: 0x0600179A RID: 6042 RVA: 0x000BBD94 File Offset: 0x000B9F94
			public void Initialize()
			{
				this.BackColor = 16777215;
				this.Width = 0;
				this.WidthType = 0;
				this.Cellx = 0;
				this.VerticalAlignment = RtfVertAlignment.Undefined;
				this.MergeLeft = false;
				this.MergeUp = false;
				this.Padding.Initialize(-1);
				this.BorderWidth.Initialize(byte.MaxValue);
				this.BorderColor.Initialize(-1);
				this.BorderKind.Initialize(RtfBorderKind.None);
			}

			// Token: 0x0600179B RID: 6043 RVA: 0x000BBE10 File Offset: 0x000BA010
			public void SetBorderKind(RtfBorderId borderId, RtfBorderKind value)
			{
				switch (borderId)
				{
				case RtfBorderId.CellLeft:
					this.BorderKind.Left = value;
					return;
				case RtfBorderId.CellTop:
					this.BorderKind.Top = value;
					return;
				case RtfBorderId.CellRight:
					this.BorderKind.Right = value;
					return;
				case RtfBorderId.CellBottom:
					this.BorderKind.Bottom = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x0600179C RID: 6044 RVA: 0x000BBE6C File Offset: 0x000BA06C
			public void SetBorderWidth(RtfBorderId borderId, byte value)
			{
				switch (borderId)
				{
				case RtfBorderId.CellLeft:
					this.BorderWidth.Left = value;
					return;
				case RtfBorderId.CellTop:
					this.BorderWidth.Top = value;
					return;
				case RtfBorderId.CellRight:
					this.BorderWidth.Right = value;
					return;
				case RtfBorderId.CellBottom:
					this.BorderWidth.Bottom = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x0600179D RID: 6045 RVA: 0x000BBEC8 File Offset: 0x000BA0C8
			public void SetBorderColor(RtfBorderId borderId, int value)
			{
				switch (borderId)
				{
				case RtfBorderId.CellLeft:
					this.BorderColor.Left = value;
					return;
				case RtfBorderId.CellTop:
					this.BorderColor.Top = value;
					return;
				case RtfBorderId.CellRight:
					this.BorderColor.Right = value;
					return;
				case RtfBorderId.CellBottom:
					this.BorderColor.Bottom = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x04001AEC RID: 6892
			public bool MergeLeft;

			// Token: 0x04001AED RID: 6893
			public bool MergeUp;

			// Token: 0x04001AEE RID: 6894
			public short Cellx;

			// Token: 0x04001AEF RID: 6895
			public short Width;

			// Token: 0x04001AF0 RID: 6896
			public byte WidthType;

			// Token: 0x04001AF1 RID: 6897
			public int BackColor;

			// Token: 0x04001AF2 RID: 6898
			public RtfFormatConverter.RtfFourSideValue<byte> BorderWidth;

			// Token: 0x04001AF3 RID: 6899
			public RtfFormatConverter.RtfFourSideValue<int> BorderColor;

			// Token: 0x04001AF4 RID: 6900
			public RtfFormatConverter.RtfFourSideValue<RtfBorderKind> BorderKind;

			// Token: 0x04001AF5 RID: 6901
			public RtfFormatConverter.RtfFourSideValue<short> Padding;

			// Token: 0x04001AF6 RID: 6902
			public RtfVertAlignment VerticalAlignment;
		}

		// Token: 0x0200023F RID: 575
		internal struct RtfTable
		{
			// Token: 0x0600179E RID: 6046 RVA: 0x000BBF24 File Offset: 0x000BA124
			public void SetBorderKind(RtfBorderId borderId, RtfBorderKind value)
			{
				switch (borderId)
				{
				case RtfBorderId.RowLeft:
					this.BorderKind.Left = value;
					return;
				case RtfBorderId.RowTop:
					this.BorderKind.Top = value;
					return;
				case RtfBorderId.RowRight:
					this.BorderKind.Right = value;
					return;
				case RtfBorderId.RowBottom:
					this.BorderKind.Bottom = value;
					return;
				case RtfBorderId.RowHorizontal:
					this.BorderKind.Horizontal = value;
					return;
				case RtfBorderId.RowVertical:
					this.BorderKind.Vertical = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x0600179F RID: 6047 RVA: 0x000BBFA4 File Offset: 0x000BA1A4
			public void SetBorderWidth(RtfBorderId borderId, byte value)
			{
				switch (borderId)
				{
				case RtfBorderId.RowLeft:
					this.BorderWidth.Left = value;
					return;
				case RtfBorderId.RowTop:
					this.BorderWidth.Top = value;
					return;
				case RtfBorderId.RowRight:
					this.BorderWidth.Right = value;
					return;
				case RtfBorderId.RowBottom:
					this.BorderWidth.Bottom = value;
					return;
				case RtfBorderId.RowHorizontal:
					this.BorderWidth.Horizontal = value;
					return;
				case RtfBorderId.RowVertical:
					this.BorderWidth.Vertical = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x060017A0 RID: 6048 RVA: 0x000BC024 File Offset: 0x000BA224
			public void SetBorderColor(RtfBorderId borderId, int value)
			{
				switch (borderId)
				{
				case RtfBorderId.RowLeft:
					this.BorderColor.Left = value;
					return;
				case RtfBorderId.RowTop:
					this.BorderColor.Top = value;
					return;
				case RtfBorderId.RowRight:
					this.BorderColor.Right = value;
					return;
				case RtfBorderId.RowBottom:
					this.BorderColor.Bottom = value;
					return;
				case RtfBorderId.RowHorizontal:
					this.BorderColor.Horizontal = value;
					return;
				case RtfBorderId.RowVertical:
					this.BorderColor.Vertical = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x060017A1 RID: 6049 RVA: 0x000BC0A4 File Offset: 0x000BA2A4
			public void Initialize()
			{
				this.FirstRow = true;
				this.RightToLeft = false;
				this.Left = 0;
				this.Width = 0;
				this.WidthPercentage = 0;
				this.BackColor = 16777215;
				this.BorderWidth.Initialize(0);
				this.BorderColor.Initialize(0);
				this.BorderKind.Initialize(RtfBorderKind.None);
				this.ColumnCount = 0;
				if (this.Columns != null)
				{
					this.InitializeColumns(0);
				}
			}

			// Token: 0x060017A2 RID: 6050 RVA: 0x000BC11C File Offset: 0x000BA31C
			public void EnsureColumns(int count)
			{
				if (this.Columns == null)
				{
					this.Columns = new RtfFormatConverter.RtfTableColumn[Math.Max(count, 4)];
					this.InitializeColumns(0);
					return;
				}
				if (this.Columns.Length < count)
				{
					RtfFormatConverter.RtfTableColumn[] array = new RtfFormatConverter.RtfTableColumn[Math.Max(count, this.Columns.Length * 2)];
					if (this.ColumnCount != 0)
					{
						Array.Copy(this.Columns, 0, array, 0, (int)this.ColumnCount);
					}
					this.Columns = array;
					this.InitializeColumns((int)this.ColumnCount);
				}
			}

			// Token: 0x060017A3 RID: 6051 RVA: 0x000BC19C File Offset: 0x000BA39C
			public void InsertColumn(int index, short cellx)
			{
				this.EnsureColumns((int)(this.ColumnCount + 1));
				Array.Copy(this.Columns, index, this.Columns, index + 1, (int)this.ColumnCount - index);
				this.ColumnCount += 1;
				this.Columns[index].Cellx = cellx;
				short width = this.Columns[index].Width;
				this.Columns[index].Width = this.Columns[index].Cellx - ((index == 0) ? this.Left : this.Columns[index - 1].Cellx);
				this.Columns[index + 1].Width = this.Columns[index + 1].Cellx - cellx;
				if (this.Columns[index].WidthPercentage != 0)
				{
					this.Columns[index].WidthPercentage = this.Columns[index].WidthPercentage * this.Columns[index].Width / width;
					this.Columns[index + 1].WidthPercentage = this.Columns[index + 1].WidthPercentage * this.Columns[index + 1].Width / width;
				}
			}

			// Token: 0x060017A4 RID: 6052 RVA: 0x000BC2FC File Offset: 0x000BA4FC
			private void InitializeColumns(int startIndex)
			{
				for (int i = startIndex; i < this.Columns.Length; i++)
				{
					this.Columns[i].Initialize();
				}
			}

			// Token: 0x04001AF7 RID: 6903
			public bool FirstRow;

			// Token: 0x04001AF8 RID: 6904
			public bool RightToLeft;

			// Token: 0x04001AF9 RID: 6905
			public short Left;

			// Token: 0x04001AFA RID: 6906
			public short Width;

			// Token: 0x04001AFB RID: 6907
			public short WidthPercentage;

			// Token: 0x04001AFC RID: 6908
			public int BackColor;

			// Token: 0x04001AFD RID: 6909
			public RtfFormatConverter.RtfSixSideValue<byte> BorderWidth;

			// Token: 0x04001AFE RID: 6910
			public RtfFormatConverter.RtfSixSideValue<int> BorderColor;

			// Token: 0x04001AFF RID: 6911
			public RtfFormatConverter.RtfSixSideValue<RtfBorderKind> BorderKind;

			// Token: 0x04001B00 RID: 6912
			public short ColumnCount;

			// Token: 0x04001B01 RID: 6913
			public RtfFormatConverter.RtfTableColumn[] Columns;

			// Token: 0x04001B02 RID: 6914
			public short CellIndex;

			// Token: 0x04001B03 RID: 6915
			public FormatConverterContainer TableContainer;

			// Token: 0x04001B04 RID: 6916
			public FormatConverterContainer RowContainer;

			// Token: 0x04001B05 RID: 6917
			public FormatConverterContainer CellContainer;
		}

		// Token: 0x02000240 RID: 576
		internal struct RtfTableColumn
		{
			// Token: 0x060017A5 RID: 6053 RVA: 0x000BC32D File Offset: 0x000BA52D
			public void Initialize()
			{
				this.Cellx = 0;
				this.Width = 0;
				this.WidthPercentage = 0;
				this.BackColor = 16777215;
				this.VerticalMergeCell = FormatNode.Null;
			}

			// Token: 0x04001B06 RID: 6918
			public short Cellx;

			// Token: 0x04001B07 RID: 6919
			public short Width;

			// Token: 0x04001B08 RID: 6920
			public short WidthPercentage;

			// Token: 0x04001B09 RID: 6921
			public int BackColor;

			// Token: 0x04001B0A RID: 6922
			public FormatNode VerticalMergeCell;
		}

		// Token: 0x02000241 RID: 577
		internal struct RtfState
		{
			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x060017A6 RID: 6054 RVA: 0x000BC35A File Offset: 0x000BA55A
			// (set) Token: 0x060017A7 RID: 6055 RVA: 0x000BC362 File Offset: 0x000BA562
			public bool TextPropertiesChanged
			{
				get
				{
					return this.textPropertiesChanged;
				}
				set
				{
					this.textPropertiesChanged = value;
				}
			}

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x060017A8 RID: 6056 RVA: 0x000BC36B File Offset: 0x000BA56B
			// (set) Token: 0x060017A9 RID: 6057 RVA: 0x000BC378 File Offset: 0x000BA578
			public RtfDestination Destination
			{
				get
				{
					return this.basic.Destination;
				}
				set
				{
					if (this.basic.Destination != value)
					{
						this.DirtyBasicState();
						this.basic.Destination = value;
					}
				}
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x060017AA RID: 6058 RVA: 0x000BC39A File Offset: 0x000BA59A
			// (set) Token: 0x060017AB RID: 6059 RVA: 0x000BC3B0 File Offset: 0x000BA5B0
			public bool BiDi
			{
				get
				{
					return (ushort)(this.font.Flags & RtfFormatConverter.RtfState.FontFlags.BiDi) != 0;
				}
				set
				{
					if (this.BiDi != value)
					{
						this.DirtyFontProps();
						this.font.Flags = (this.font.Flags ^ RtfFormatConverter.RtfState.FontFlags.BiDi);
					}
				}
			}

			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x060017AC RID: 6060 RVA: 0x000BC3D5 File Offset: 0x000BA5D5
			// (set) Token: 0x060017AD RID: 6061 RVA: 0x000BC3EB File Offset: 0x000BA5EB
			public bool ComplexScript
			{
				get
				{
					return (ushort)(this.font.Flags & RtfFormatConverter.RtfState.FontFlags.ComplexScript) != 0;
				}
				set
				{
					if (this.ComplexScript != value)
					{
						this.DirtyFontProps();
						this.font.Flags = (this.font.Flags ^ RtfFormatConverter.RtfState.FontFlags.ComplexScript);
					}
				}
			}

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x060017AE RID: 6062 RVA: 0x000BC410 File Offset: 0x000BA610
			// (set) Token: 0x060017AF RID: 6063 RVA: 0x000BC426 File Offset: 0x000BA626
			public bool Bold
			{
				get
				{
					return (ushort)(this.font.Flags & RtfFormatConverter.RtfState.FontFlags.Bold) != 0;
				}
				set
				{
					if (this.Bold != value)
					{
						this.DirtyFontProps();
						this.font.Flags = (this.font.Flags ^ RtfFormatConverter.RtfState.FontFlags.Bold);
					}
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000BC44B File Offset: 0x000BA64B
			// (set) Token: 0x060017B1 RID: 6065 RVA: 0x000BC461 File Offset: 0x000BA661
			public bool Italic
			{
				get
				{
					return (ushort)(this.font.Flags & RtfFormatConverter.RtfState.FontFlags.Italic) != 0;
				}
				set
				{
					if (this.Italic != value)
					{
						this.DirtyFontProps();
						this.font.Flags = (this.font.Flags ^ RtfFormatConverter.RtfState.FontFlags.Italic);
					}
				}
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000BC486 File Offset: 0x000BA686
			// (set) Token: 0x060017B3 RID: 6067 RVA: 0x000BC49D File Offset: 0x000BA69D
			public bool Underline
			{
				get
				{
					return (ushort)(this.font.Flags & RtfFormatConverter.RtfState.FontFlags.Underline) != 0;
				}
				set
				{
					if (this.Underline != value)
					{
						this.DirtyFontProps();
						this.font.Flags = (this.font.Flags ^ RtfFormatConverter.RtfState.FontFlags.Underline);
					}
				}
			}

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x060017B4 RID: 6068 RVA: 0x000BC4C3 File Offset: 0x000BA6C3
			// (set) Token: 0x060017B5 RID: 6069 RVA: 0x000BC4DA File Offset: 0x000BA6DA
			public bool Subscript
			{
				get
				{
					return (ushort)(this.font.Flags & RtfFormatConverter.RtfState.FontFlags.Subscript) != 0;
				}
				set
				{
					if (this.Subscript != value)
					{
						this.DirtyFontProps();
						this.font.Flags = (this.font.Flags ^ RtfFormatConverter.RtfState.FontFlags.Subscript);
						if (value)
						{
							this.Superscript = false;
						}
					}
				}
			}

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000BC50A File Offset: 0x000BA70A
			// (set) Token: 0x060017B7 RID: 6071 RVA: 0x000BC521 File Offset: 0x000BA721
			public bool Superscript
			{
				get
				{
					return (ushort)(this.font.Flags & RtfFormatConverter.RtfState.FontFlags.Superscript) != 0;
				}
				set
				{
					if (this.Superscript != value)
					{
						this.DirtyFontProps();
						this.font.Flags = (this.font.Flags ^ RtfFormatConverter.RtfState.FontFlags.Superscript);
						if (value)
						{
							this.Subscript = false;
						}
					}
				}
			}

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000BC551 File Offset: 0x000BA751
			// (set) Token: 0x060017B9 RID: 6073 RVA: 0x000BC56B File Offset: 0x000BA76B
			public bool Strikethrough
			{
				get
				{
					return (ushort)(this.font.Flags & RtfFormatConverter.RtfState.FontFlags.Strikethrough) != 0;
				}
				set
				{
					if (this.Strikethrough != value)
					{
						this.DirtyFontProps();
						this.font.Flags = (this.font.Flags ^ RtfFormatConverter.RtfState.FontFlags.Strikethrough);
					}
				}
			}

			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x060017BA RID: 6074 RVA: 0x000BC594 File Offset: 0x000BA794
			// (set) Token: 0x060017BB RID: 6075 RVA: 0x000BC5AE File Offset: 0x000BA7AE
			public bool SmallCaps
			{
				get
				{
					return (ushort)(this.font.Flags & RtfFormatConverter.RtfState.FontFlags.SmallCaps) != 0;
				}
				set
				{
					if (this.SmallCaps != value)
					{
						this.DirtyFontProps();
						this.font.Flags = (this.font.Flags ^ RtfFormatConverter.RtfState.FontFlags.SmallCaps);
					}
				}
			}

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x060017BC RID: 6076 RVA: 0x000BC5D7 File Offset: 0x000BA7D7
			// (set) Token: 0x060017BD RID: 6077 RVA: 0x000BC5F1 File Offset: 0x000BA7F1
			public bool Capitalize
			{
				get
				{
					return (ushort)(this.font.Flags & RtfFormatConverter.RtfState.FontFlags.Capitalize) != 0;
				}
				set
				{
					if (this.Capitalize != value)
					{
						this.DirtyFontProps();
						this.font.Flags = (this.font.Flags ^ RtfFormatConverter.RtfState.FontFlags.Capitalize);
					}
				}
			}

			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x060017BE RID: 6078 RVA: 0x000BC61A File Offset: 0x000BA81A
			// (set) Token: 0x060017BF RID: 6079 RVA: 0x000BC634 File Offset: 0x000BA834
			public bool Invisible
			{
				get
				{
					return (ushort)(this.font.Flags & RtfFormatConverter.RtfState.FontFlags.Invisible) != 0;
				}
				set
				{
					if (this.Invisible != value)
					{
						this.DirtyFontProps();
						this.font.Flags = (this.font.Flags ^ RtfFormatConverter.RtfState.FontFlags.Invisible);
					}
				}
			}

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x060017C0 RID: 6080 RVA: 0x000BC65D File Offset: 0x000BA85D
			// (set) Token: 0x060017C1 RID: 6081 RVA: 0x000BC66A File Offset: 0x000BA86A
			public short FontIndex
			{
				get
				{
					return this.font.FontIndex;
				}
				set
				{
					if (this.font.FontIndex != value)
					{
						this.DirtyFontProps();
						this.font.FontIndex = value;
					}
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x060017C2 RID: 6082 RVA: 0x000BC68C File Offset: 0x000BA88C
			// (set) Token: 0x060017C3 RID: 6083 RVA: 0x000BC699 File Offset: 0x000BA899
			public short FontSize
			{
				get
				{
					return this.font.FontSize;
				}
				set
				{
					if (this.font.FontSize != value)
					{
						this.DirtyFontProps();
						this.font.FontSize = value;
					}
				}
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x060017C4 RID: 6084 RVA: 0x000BC6BB File Offset: 0x000BA8BB
			// (set) Token: 0x060017C5 RID: 6085 RVA: 0x000BC6C8 File Offset: 0x000BA8C8
			public int FontColor
			{
				get
				{
					return this.font.FontColor;
				}
				set
				{
					if (this.font.FontColor != value)
					{
						this.DirtyFontProps();
						this.font.FontColor = value;
					}
				}
			}

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x060017C6 RID: 6086 RVA: 0x000BC6EA File Offset: 0x000BA8EA
			// (set) Token: 0x060017C7 RID: 6087 RVA: 0x000BC6F7 File Offset: 0x000BA8F7
			public int FontBackColor
			{
				get
				{
					return this.font.FontBackColor;
				}
				set
				{
					if (this.font.FontBackColor != value)
					{
						this.DirtyFontProps();
						this.font.FontBackColor = value;
					}
				}
			}

			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x060017C8 RID: 6088 RVA: 0x000BC719 File Offset: 0x000BA919
			// (set) Token: 0x060017C9 RID: 6089 RVA: 0x000BC726 File Offset: 0x000BA926
			public short Language
			{
				get
				{
					return this.font.Language;
				}
				set
				{
					if (this.font.Language != value)
					{
						this.DirtyFontProps();
						this.font.Language = value;
					}
				}
			}

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x060017CA RID: 6090 RVA: 0x000BC748 File Offset: 0x000BA948
			// (set) Token: 0x060017CB RID: 6091 RVA: 0x000BC75E File Offset: 0x000BA95E
			public bool Preformatted
			{
				get
				{
					return (byte)(this.paragraph.Flags & RtfFormatConverter.RtfState.ParagraphFlags.Preformatted) != 0;
				}
				set
				{
					if (this.Preformatted != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.Flags = (this.paragraph.Flags ^ RtfFormatConverter.RtfState.ParagraphFlags.Preformatted);
					}
				}
			}

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x060017CC RID: 6092 RVA: 0x000BC783 File Offset: 0x000BA983
			// (set) Token: 0x060017CD RID: 6093 RVA: 0x000BC799 File Offset: 0x000BA999
			public bool ParagraphRtl
			{
				get
				{
					return (byte)(this.paragraph.Flags & RtfFormatConverter.RtfState.ParagraphFlags.ParagraphRtl) != 0;
				}
				set
				{
					if (this.ParagraphRtl != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.Flags = (this.paragraph.Flags ^ RtfFormatConverter.RtfState.ParagraphFlags.ParagraphRtl);
					}
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x060017CE RID: 6094 RVA: 0x000BC7BE File Offset: 0x000BA9BE
			// (set) Token: 0x060017CF RID: 6095 RVA: 0x000BC7CB File Offset: 0x000BA9CB
			public short FirstLineIndent
			{
				get
				{
					return this.paragraph.FirstLineIndent;
				}
				set
				{
					if (this.paragraph.FirstLineIndent != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.FirstLineIndent = value;
					}
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x060017D0 RID: 6096 RVA: 0x000BC7ED File Offset: 0x000BA9ED
			// (set) Token: 0x060017D1 RID: 6097 RVA: 0x000BC7FA File Offset: 0x000BA9FA
			public short LeftIndent
			{
				get
				{
					return this.paragraph.LeftIndent;
				}
				set
				{
					if (this.paragraph.LeftIndent != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.LeftIndent = value;
					}
				}
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000BC81C File Offset: 0x000BAA1C
			// (set) Token: 0x060017D3 RID: 6099 RVA: 0x000BC829 File Offset: 0x000BAA29
			public short RightIndent
			{
				get
				{
					return this.paragraph.RightIndent;
				}
				set
				{
					if (this.paragraph.RightIndent != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.RightIndent = value;
					}
				}
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000BC84B File Offset: 0x000BAA4B
			// (set) Token: 0x060017D5 RID: 6101 RVA: 0x000BC858 File Offset: 0x000BAA58
			public short SpaceBefore
			{
				get
				{
					return this.paragraph.SpaceBefore;
				}
				set
				{
					if (this.paragraph.SpaceBefore != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.SpaceBefore = value;
					}
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x060017D6 RID: 6102 RVA: 0x000BC87A File Offset: 0x000BAA7A
			// (set) Token: 0x060017D7 RID: 6103 RVA: 0x000BC887 File Offset: 0x000BAA87
			public short SpaceAfter
			{
				get
				{
					return this.paragraph.SpaceAfter;
				}
				set
				{
					if (this.paragraph.SpaceAfter != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.SpaceAfter = value;
					}
				}
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000BC8A9 File Offset: 0x000BAAA9
			// (set) Token: 0x060017D9 RID: 6105 RVA: 0x000BC8B6 File Offset: 0x000BAAB6
			public RtfAlignment ParagraphAlignment
			{
				get
				{
					return this.paragraph.Alignment;
				}
				set
				{
					if (this.paragraph.Alignment != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.Alignment = value;
					}
				}
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x060017DA RID: 6106 RVA: 0x000BC8D8 File Offset: 0x000BAAD8
			// (set) Token: 0x060017DB RID: 6107 RVA: 0x000BC8E5 File Offset: 0x000BAAE5
			public short ListIndex
			{
				get
				{
					return this.paragraph.ListIndex;
				}
				set
				{
					if (this.paragraph.ListIndex != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.ListIndex = value;
					}
				}
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x060017DC RID: 6108 RVA: 0x000BC907 File Offset: 0x000BAB07
			// (set) Token: 0x060017DD RID: 6109 RVA: 0x000BC914 File Offset: 0x000BAB14
			public byte ListLevel
			{
				get
				{
					return this.paragraph.ListLevel;
				}
				set
				{
					if (this.paragraph.ListLevel != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.ListLevel = value;
					}
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x060017DE RID: 6110 RVA: 0x000BC936 File Offset: 0x000BAB36
			// (set) Token: 0x060017DF RID: 6111 RVA: 0x000BC943 File Offset: 0x000BAB43
			public RtfNumbering ListStyle
			{
				get
				{
					return this.paragraph.ListStyle;
				}
				set
				{
					if (this.paragraph.ListStyle != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.ListStyle = value;
					}
				}
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x060017E0 RID: 6112 RVA: 0x000BC965 File Offset: 0x000BAB65
			// (set) Token: 0x060017E1 RID: 6113 RVA: 0x000BC972 File Offset: 0x000BAB72
			public int ParagraphBackColor
			{
				get
				{
					return this.paragraph.BackColor;
				}
				set
				{
					if (this.paragraph.BackColor != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BackColor = value;
					}
				}
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x060017E2 RID: 6114 RVA: 0x000BC994 File Offset: 0x000BAB94
			// (set) Token: 0x060017E3 RID: 6115 RVA: 0x000BC9A6 File Offset: 0x000BABA6
			public byte BorderWidthLeft
			{
				get
				{
					return this.paragraph.BorderWidth.Left;
				}
				set
				{
					if (this.paragraph.BorderWidth.Left != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderWidth.Left = value;
					}
				}
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x060017E4 RID: 6116 RVA: 0x000BC9D2 File Offset: 0x000BABD2
			// (set) Token: 0x060017E5 RID: 6117 RVA: 0x000BC9E4 File Offset: 0x000BABE4
			public byte BorderWidthRight
			{
				get
				{
					return this.paragraph.BorderWidth.Right;
				}
				set
				{
					if (this.paragraph.BorderWidth.Right != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderWidth.Right = value;
					}
				}
			}

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x060017E6 RID: 6118 RVA: 0x000BCA10 File Offset: 0x000BAC10
			// (set) Token: 0x060017E7 RID: 6119 RVA: 0x000BCA22 File Offset: 0x000BAC22
			public byte BorderWidthTop
			{
				get
				{
					return this.paragraph.BorderWidth.Top;
				}
				set
				{
					if (this.paragraph.BorderWidth.Top != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderWidth.Top = value;
					}
				}
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x060017E8 RID: 6120 RVA: 0x000BCA4E File Offset: 0x000BAC4E
			// (set) Token: 0x060017E9 RID: 6121 RVA: 0x000BCA60 File Offset: 0x000BAC60
			public byte BorderWidthBottom
			{
				get
				{
					return this.paragraph.BorderWidth.Bottom;
				}
				set
				{
					if (this.paragraph.BorderWidth.Bottom != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderWidth.Bottom = value;
					}
				}
			}

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x060017EA RID: 6122 RVA: 0x000BCA8C File Offset: 0x000BAC8C
			// (set) Token: 0x060017EB RID: 6123 RVA: 0x000BCA9E File Offset: 0x000BAC9E
			public int BorderColorLeft
			{
				get
				{
					return this.paragraph.BorderColor.Left;
				}
				set
				{
					if (this.paragraph.BorderColor.Left != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderColor.Left = value;
					}
				}
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x060017EC RID: 6124 RVA: 0x000BCACA File Offset: 0x000BACCA
			// (set) Token: 0x060017ED RID: 6125 RVA: 0x000BCADC File Offset: 0x000BACDC
			public int BorderColorRight
			{
				get
				{
					return this.paragraph.BorderColor.Right;
				}
				set
				{
					if (this.paragraph.BorderColor.Right != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderColor.Right = value;
					}
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x060017EE RID: 6126 RVA: 0x000BCB08 File Offset: 0x000BAD08
			// (set) Token: 0x060017EF RID: 6127 RVA: 0x000BCB1A File Offset: 0x000BAD1A
			public int BorderColorTop
			{
				get
				{
					return this.paragraph.BorderColor.Top;
				}
				set
				{
					if (this.paragraph.BorderColor.Top != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderColor.Top = value;
					}
				}
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x060017F0 RID: 6128 RVA: 0x000BCB46 File Offset: 0x000BAD46
			// (set) Token: 0x060017F1 RID: 6129 RVA: 0x000BCB58 File Offset: 0x000BAD58
			public int BorderColorBottom
			{
				get
				{
					return this.paragraph.BorderColor.Bottom;
				}
				set
				{
					if (this.paragraph.BorderColor.Bottom != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderColor.Bottom = value;
					}
				}
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x060017F2 RID: 6130 RVA: 0x000BCB84 File Offset: 0x000BAD84
			// (set) Token: 0x060017F3 RID: 6131 RVA: 0x000BCB96 File Offset: 0x000BAD96
			public RtfBorderKind BorderKindLeft
			{
				get
				{
					return this.paragraph.BorderKind.Left;
				}
				set
				{
					if (this.paragraph.BorderKind.Left != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderKind.Left = value;
					}
				}
			}

			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000BCBC2 File Offset: 0x000BADC2
			// (set) Token: 0x060017F5 RID: 6133 RVA: 0x000BCBD4 File Offset: 0x000BADD4
			public RtfBorderKind BorderKindRight
			{
				get
				{
					return this.paragraph.BorderKind.Right;
				}
				set
				{
					if (this.paragraph.BorderKind.Right != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderKind.Right = value;
					}
				}
			}

			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x060017F6 RID: 6134 RVA: 0x000BCC00 File Offset: 0x000BAE00
			// (set) Token: 0x060017F7 RID: 6135 RVA: 0x000BCC12 File Offset: 0x000BAE12
			public RtfBorderKind BorderKindTop
			{
				get
				{
					return this.paragraph.BorderKind.Top;
				}
				set
				{
					if (this.paragraph.BorderKind.Top != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderKind.Top = value;
					}
				}
			}

			// Token: 0x1700060B RID: 1547
			// (get) Token: 0x060017F8 RID: 6136 RVA: 0x000BCC3E File Offset: 0x000BAE3E
			// (set) Token: 0x060017F9 RID: 6137 RVA: 0x000BCC50 File Offset: 0x000BAE50
			public RtfBorderKind BorderKindBottom
			{
				get
				{
					return this.paragraph.BorderKind.Bottom;
				}
				set
				{
					if (this.paragraph.BorderKind.Bottom != value)
					{
						this.DirtyParagraphProps();
						this.paragraph.BorderKind.Bottom = value;
					}
				}
			}

			// Token: 0x1700060C RID: 1548
			// (get) Token: 0x060017FA RID: 6138 RVA: 0x000BCC7C File Offset: 0x000BAE7C
			public RtfDestination ParentDestination
			{
				get
				{
					if (this.basic.Depth <= 1 && this.basicStackTop != 0)
					{
						return this.basicStack[this.basicStackTop - 1].Destination;
					}
					return this.basic.Destination;
				}
			}

			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x060017FB RID: 6139 RVA: 0x000BCCB8 File Offset: 0x000BAEB8
			public short ParentFontIndex
			{
				get
				{
					if (this.font.Depth <= 1 && this.fontStackTop != 0)
					{
						return this.fontStack[this.fontStackTop - 1].FontIndex;
					}
					return this.font.FontIndex;
				}
			}

			// Token: 0x060017FC RID: 6140 RVA: 0x000BCCF4 File Offset: 0x000BAEF4
			public void Initialize()
			{
				this.basic.Depth = 1;
				this.font.Depth = 1;
				this.paragraph.Depth = 1;
				this.SetParagraphDefault();
				this.SetPlain();
				this.Destination = RtfDestination.RTF;
			}

			// Token: 0x060017FD RID: 6141 RVA: 0x000BCD30 File Offset: 0x000BAF30
			public byte GetParagraphBorderWidth(RtfBorderId borderId)
			{
				switch (borderId)
				{
				default:
					return this.BorderWidthLeft;
				case RtfBorderId.Top:
					return this.BorderWidthTop;
				case RtfBorderId.Right:
					return this.BorderWidthRight;
				case RtfBorderId.Bottom:
					return this.BorderWidthBottom;
				}
			}

			// Token: 0x060017FE RID: 6142 RVA: 0x000BCD70 File Offset: 0x000BAF70
			public void SetParagraphBorderWidth(RtfBorderId borderId, byte value)
			{
				switch (borderId)
				{
				case RtfBorderId.Left:
					this.BorderWidthLeft = value;
					return;
				case RtfBorderId.Top:
					this.BorderWidthTop = value;
					return;
				case RtfBorderId.Right:
					this.BorderWidthRight = value;
					return;
				case RtfBorderId.Bottom:
					this.BorderWidthBottom = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x060017FF RID: 6143 RVA: 0x000BCDB8 File Offset: 0x000BAFB8
			public int GetParagraphBorderColor(RtfBorderId borderId)
			{
				switch (borderId)
				{
				default:
					return this.BorderColorLeft;
				case RtfBorderId.Top:
					return this.BorderColorTop;
				case RtfBorderId.Right:
					return this.BorderColorRight;
				case RtfBorderId.Bottom:
					return this.BorderColorBottom;
				}
			}

			// Token: 0x06001800 RID: 6144 RVA: 0x000BCDF8 File Offset: 0x000BAFF8
			public void SetParagraphBorderColor(RtfBorderId borderId, int value)
			{
				switch (borderId)
				{
				case RtfBorderId.Left:
					this.BorderColorLeft = value;
					return;
				case RtfBorderId.Top:
					this.BorderColorTop = value;
					return;
				case RtfBorderId.Right:
					this.BorderColorRight = value;
					return;
				case RtfBorderId.Bottom:
					this.BorderColorBottom = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x06001801 RID: 6145 RVA: 0x000BCE40 File Offset: 0x000BB040
			public RtfBorderKind GetParagraphBorderKind(RtfBorderId borderId)
			{
				switch (borderId)
				{
				default:
					return this.BorderKindLeft;
				case RtfBorderId.Top:
					return this.BorderKindTop;
				case RtfBorderId.Right:
					return this.BorderKindRight;
				case RtfBorderId.Bottom:
					return this.BorderKindBottom;
				}
			}

			// Token: 0x06001802 RID: 6146 RVA: 0x000BCE80 File Offset: 0x000BB080
			public void SetParagraphBorderKind(RtfBorderId borderId, RtfBorderKind value)
			{
				switch (borderId)
				{
				case RtfBorderId.Left:
					this.BorderKindLeft = value;
					return;
				case RtfBorderId.Top:
					this.BorderKindTop = value;
					return;
				case RtfBorderId.Right:
					this.BorderKindRight = value;
					return;
				case RtfBorderId.Bottom:
					this.BorderKindBottom = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x06001803 RID: 6147 RVA: 0x000BCEC5 File Offset: 0x000BB0C5
			public void SetParentFontIndex(short index)
			{
				this.font.FontIndex = index;
				if (this.fontStackTop > 0)
				{
					this.fontStack[this.fontStackTop - 1].FontIndex = index;
				}
			}

			// Token: 0x06001804 RID: 6148 RVA: 0x000BCEF8 File Offset: 0x000BB0F8
			public void SetPlain()
			{
				this.DirtyFontProps();
				this.font.Flags = RtfFormatConverter.RtfState.FontFlags.None;
				this.font.FontIndex = 0;
				this.font.FontSize = RtfFormatConverter.TwelvePointsInTwips;
				this.font.Language = 0;
				this.font.FontColor = 0;
				this.font.FontBackColor = 16777215;
			}

			// Token: 0x06001805 RID: 6149 RVA: 0x000BCF5C File Offset: 0x000BB15C
			public void SetParagraphDefault()
			{
				this.DirtyParagraphProps();
				this.paragraph.Flags = RtfFormatConverter.RtfState.ParagraphFlags.None;
				this.paragraph.Alignment = RtfAlignment.Left;
				this.paragraph.SpaceBefore = 0;
				this.paragraph.SpaceAfter = 0;
				this.paragraph.LeftIndent = 0;
				this.paragraph.RightIndent = 0;
				this.paragraph.FirstLineIndent = 0;
				this.paragraph.ListIndex = -1;
				this.paragraph.ListLevel = 0;
				this.paragraph.ListStyle = RtfNumbering.None;
				this.paragraph.BackColor = 16777215;
				this.paragraph.BorderWidth.Initialize(0);
				this.paragraph.BorderColor.Initialize(-1);
				this.paragraph.BorderKind.Initialize(RtfBorderKind.None);
			}

			// Token: 0x06001806 RID: 6150 RVA: 0x000BD02C File Offset: 0x000BB22C
			public void Push()
			{
				this.Level++;
				this.basic.Depth = this.basic.Depth + 1;
				this.font.Depth = this.font.Depth + 1;
				this.paragraph.Depth = this.paragraph.Depth + 1;
				if (this.basic.Depth == 32767)
				{
					this.PushBasicState();
				}
				if (this.font.Depth == 32767)
				{
					this.PushFontProps();
				}
				if (this.paragraph.Depth == 32767)
				{
					this.PushParagraphProps();
				}
			}

			// Token: 0x06001807 RID: 6151 RVA: 0x000BD0CC File Offset: 0x000BB2CC
			public void Pop()
			{
				if (this.Level > 1)
				{
					if ((this.basic.Depth = this.basic.Depth - 1) == 0)
					{
						this.basic = this.basicStack[--this.basicStackTop];
					}
					if ((this.font.Depth = this.font.Depth - 1) == 0)
					{
						this.font = this.fontStack[--this.fontStackTop];
						this.textPropertiesChanged = true;
					}
					if ((this.paragraph.Depth = this.paragraph.Depth - 1) == 0)
					{
						this.paragraph = this.paragraphStack[--this.paragraphStackTop];
					}
					this.Level--;
				}
			}

			// Token: 0x06001808 RID: 6152 RVA: 0x000BD1BE File Offset: 0x000BB3BE
			public void SkipGroup()
			{
				this.SkipLevel = this.Level;
			}

			// Token: 0x06001809 RID: 6153 RVA: 0x000BD1CC File Offset: 0x000BB3CC
			private void DirtyFontProps()
			{
				this.textPropertiesChanged = true;
				if (this.font.Depth > 1)
				{
					this.PushFontProps();
				}
			}

			// Token: 0x0600180A RID: 6154 RVA: 0x000BD1EC File Offset: 0x000BB3EC
			private void PushFontProps()
			{
				if (this.fontStackTop >= 4096)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				if (this.fontStack == null || this.fontStackTop == this.fontStack.Length)
				{
					RtfFormatConverter.RtfState.FontProperties[] destinationArray;
					if (this.fontStack != null)
					{
						destinationArray = new RtfFormatConverter.RtfState.FontProperties[this.fontStackTop * 2];
						Array.Copy(this.fontStack, 0, destinationArray, 0, this.fontStackTop);
					}
					else
					{
						destinationArray = new RtfFormatConverter.RtfState.FontProperties[8];
					}
					this.fontStack = destinationArray;
				}
				this.fontStack[this.fontStackTop] = this.font;
				RtfFormatConverter.RtfState.FontProperties[] array = this.fontStack;
				int num = this.fontStackTop;
				array[num].Depth = array[num].Depth - 1;
				this.fontStackTop++;
				this.font.Depth = 1;
			}

			// Token: 0x0600180B RID: 6155 RVA: 0x000BD2B8 File Offset: 0x000BB4B8
			private void DirtyParagraphProps()
			{
				if (this.paragraph.Depth > 1)
				{
					this.PushParagraphProps();
				}
			}

			// Token: 0x0600180C RID: 6156 RVA: 0x000BD2D0 File Offset: 0x000BB4D0
			private void PushParagraphProps()
			{
				if (this.paragraphStackTop >= 4096)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				if (this.paragraphStack == null || this.paragraphStackTop == this.paragraphStack.Length)
				{
					RtfFormatConverter.RtfState.ParagraphProperties[] destinationArray;
					if (this.paragraphStack != null)
					{
						destinationArray = new RtfFormatConverter.RtfState.ParagraphProperties[this.paragraphStackTop * 2];
						Array.Copy(this.paragraphStack, 0, destinationArray, 0, this.paragraphStackTop);
					}
					else
					{
						destinationArray = new RtfFormatConverter.RtfState.ParagraphProperties[4];
					}
					this.paragraphStack = destinationArray;
				}
				this.paragraphStack[this.paragraphStackTop] = this.paragraph;
				RtfFormatConverter.RtfState.ParagraphProperties[] array = this.paragraphStack;
				int num = this.paragraphStackTop;
				array[num].Depth = array[num].Depth - 1;
				this.paragraphStackTop++;
				this.paragraph.Depth = 1;
			}

			// Token: 0x0600180D RID: 6157 RVA: 0x000BD39C File Offset: 0x000BB59C
			private void DirtyBasicState()
			{
				if (this.basic.Depth > 1)
				{
					this.PushBasicState();
				}
			}

			// Token: 0x0600180E RID: 6158 RVA: 0x000BD3B4 File Offset: 0x000BB5B4
			private void PushBasicState()
			{
				if (this.basicStackTop >= 4096)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				if (this.basicStack == null || this.basicStackTop == this.basicStack.Length)
				{
					RtfFormatConverter.RtfState.BasicState[] destinationArray;
					if (this.basicStack != null)
					{
						destinationArray = new RtfFormatConverter.RtfState.BasicState[this.basicStackTop * 2];
						Array.Copy(this.basicStack, 0, destinationArray, 0, this.basicStackTop);
					}
					else
					{
						destinationArray = new RtfFormatConverter.RtfState.BasicState[16];
					}
					this.basicStack = destinationArray;
				}
				this.basicStack[this.basicStackTop] = this.basic;
				RtfFormatConverter.RtfState.BasicState[] array = this.basicStack;
				int num = this.basicStackTop;
				array[num].Depth = array[num].Depth - 1;
				this.basicStackTop++;
				this.basic.Depth = 1;
			}

			// Token: 0x04001B0B RID: 6923
			public int Level;

			// Token: 0x04001B0C RID: 6924
			public int SkipLevel;

			// Token: 0x04001B0D RID: 6925
			private RtfFormatConverter.RtfState.BasicState basic;

			// Token: 0x04001B0E RID: 6926
			private RtfFormatConverter.RtfState.BasicState[] basicStack;

			// Token: 0x04001B0F RID: 6927
			private int basicStackTop;

			// Token: 0x04001B10 RID: 6928
			private RtfFormatConverter.RtfState.FontProperties font;

			// Token: 0x04001B11 RID: 6929
			private RtfFormatConverter.RtfState.FontProperties[] fontStack;

			// Token: 0x04001B12 RID: 6930
			private int fontStackTop;

			// Token: 0x04001B13 RID: 6931
			private RtfFormatConverter.RtfState.ParagraphProperties paragraph;

			// Token: 0x04001B14 RID: 6932
			private RtfFormatConverter.RtfState.ParagraphProperties[] paragraphStack;

			// Token: 0x04001B15 RID: 6933
			private int paragraphStackTop;

			// Token: 0x04001B16 RID: 6934
			private bool textPropertiesChanged;

			// Token: 0x02000242 RID: 578
			internal enum FontFlagsIndex : byte
			{
				// Token: 0x04001B18 RID: 6936
				BiDi,
				// Token: 0x04001B19 RID: 6937
				ComplexScript,
				// Token: 0x04001B1A RID: 6938
				Bold,
				// Token: 0x04001B1B RID: 6939
				Italic,
				// Token: 0x04001B1C RID: 6940
				Underline,
				// Token: 0x04001B1D RID: 6941
				Subscript,
				// Token: 0x04001B1E RID: 6942
				Superscript,
				// Token: 0x04001B1F RID: 6943
				Strikethrough,
				// Token: 0x04001B20 RID: 6944
				SmallCaps,
				// Token: 0x04001B21 RID: 6945
				Capitalize,
				// Token: 0x04001B22 RID: 6946
				Invisible
			}

			// Token: 0x02000243 RID: 579
			[Flags]
			internal enum FontFlags : ushort
			{
				// Token: 0x04001B24 RID: 6948
				None = 0,
				// Token: 0x04001B25 RID: 6949
				BiDi = 1,
				// Token: 0x04001B26 RID: 6950
				ComplexScript = 2,
				// Token: 0x04001B27 RID: 6951
				Bold = 4,
				// Token: 0x04001B28 RID: 6952
				Italic = 8,
				// Token: 0x04001B29 RID: 6953
				Underline = 16,
				// Token: 0x04001B2A RID: 6954
				Subscript = 32,
				// Token: 0x04001B2B RID: 6955
				Superscript = 64,
				// Token: 0x04001B2C RID: 6956
				Strikethrough = 128,
				// Token: 0x04001B2D RID: 6957
				SmallCaps = 256,
				// Token: 0x04001B2E RID: 6958
				Capitalize = 512,
				// Token: 0x04001B2F RID: 6959
				Invisible = 1024
			}

			// Token: 0x02000244 RID: 580
			internal enum ParagraphFlagsIndex : byte
			{
				// Token: 0x04001B31 RID: 6961
				ParagraphRtl,
				// Token: 0x04001B32 RID: 6962
				Preformatted
			}

			// Token: 0x02000245 RID: 581
			[Flags]
			internal enum ParagraphFlags : byte
			{
				// Token: 0x04001B34 RID: 6964
				None = 0,
				// Token: 0x04001B35 RID: 6965
				ParagraphRtl = 1,
				// Token: 0x04001B36 RID: 6966
				Preformatted = 2
			}

			// Token: 0x02000246 RID: 582
			internal struct BasicState
			{
				// Token: 0x04001B37 RID: 6967
				public short Depth;

				// Token: 0x04001B38 RID: 6968
				public RtfDestination Destination;
			}

			// Token: 0x02000247 RID: 583
			internal struct FontProperties
			{
				// Token: 0x04001B39 RID: 6969
				public short Depth;

				// Token: 0x04001B3A RID: 6970
				public RtfFormatConverter.RtfState.FontFlags Flags;

				// Token: 0x04001B3B RID: 6971
				public short FontIndex;

				// Token: 0x04001B3C RID: 6972
				public short FontSize;

				// Token: 0x04001B3D RID: 6973
				public short Language;

				// Token: 0x04001B3E RID: 6974
				public int FontColor;

				// Token: 0x04001B3F RID: 6975
				public int FontBackColor;
			}

			// Token: 0x02000248 RID: 584
			internal struct ParagraphProperties
			{
				// Token: 0x04001B40 RID: 6976
				public short Depth;

				// Token: 0x04001B41 RID: 6977
				public RtfFormatConverter.RtfState.ParagraphFlags Flags;

				// Token: 0x04001B42 RID: 6978
				public RtfAlignment Alignment;

				// Token: 0x04001B43 RID: 6979
				public short FirstLineIndent;

				// Token: 0x04001B44 RID: 6980
				public short LeftIndent;

				// Token: 0x04001B45 RID: 6981
				public short RightIndent;

				// Token: 0x04001B46 RID: 6982
				public short SpaceBefore;

				// Token: 0x04001B47 RID: 6983
				public short SpaceAfter;

				// Token: 0x04001B48 RID: 6984
				public RtfFormatConverter.RtfFourSideValue<byte> BorderWidth;

				// Token: 0x04001B49 RID: 6985
				public RtfFormatConverter.RtfFourSideValue<int> BorderColor;

				// Token: 0x04001B4A RID: 6986
				public RtfFormatConverter.RtfFourSideValue<RtfBorderKind> BorderKind;

				// Token: 0x04001B4B RID: 6987
				public int BackColor;

				// Token: 0x04001B4C RID: 6988
				public short ListIndex;

				// Token: 0x04001B4D RID: 6989
				public byte ListLevel;

				// Token: 0x04001B4E RID: 6990
				public RtfNumbering ListStyle;
			}
		}

		// Token: 0x02000249 RID: 585
		private struct ListState
		{
			// Token: 0x04001B4F RID: 6991
			public short ListIndex;

			// Token: 0x04001B50 RID: 6992
			public byte ListLevel;

			// Token: 0x04001B51 RID: 6993
			public RtfNumbering ListStyle;

			// Token: 0x04001B52 RID: 6994
			public bool Rtl;

			// Token: 0x04001B53 RID: 6995
			public short LeftIndent;

			// Token: 0x04001B54 RID: 6996
			public short RightIndent;

			// Token: 0x04001B55 RID: 6997
			public short FirstLineIndent;
		}

		// Token: 0x0200024A RID: 586
		internal class RtfRow
		{
			// Token: 0x0600180F RID: 6159 RVA: 0x000BD481 File Offset: 0x000BB681
			public RtfRow()
			{
				this.BackColor = 16777215;
			}

			// Token: 0x06001810 RID: 6160 RVA: 0x000BD494 File Offset: 0x000BB694
			public void Initialize()
			{
				this.CellCount = 0;
				if (this.Cells != null)
				{
					this.InitializeCells(0);
				}
				this.RightToLeft = false;
				this.HeaderRow = false;
				this.LastRow = false;
				this.Left = 0;
				this.Right = 0;
				this.Width = 0;
				this.WidthType = 0;
				this.Height = 0;
				this.HeightExact = false;
				this.BackColor = 16777215;
				this.CellPadding.Initialize(0);
				this.BorderWidth.Initialize(0);
				this.BorderColor.Initialize(0);
				this.BorderKind.Initialize(RtfBorderKind.None);
			}

			// Token: 0x06001811 RID: 6161 RVA: 0x000BD534 File Offset: 0x000BB734
			public bool EnsureEntryForCurrentCell()
			{
				if (this.Cells == null)
				{
					this.Cells = new RtfFormatConverter.RtfRowCell[4];
					this.InitializeCells(0);
				}
				else if ((int)this.CellCount == this.Cells.Length)
				{
					if (this.CellCount >= 64)
					{
						return false;
					}
					RtfFormatConverter.RtfRowCell[] array = new RtfFormatConverter.RtfRowCell[this.Cells.Length * 2];
					Array.Copy(this.Cells, 0, array, 0, (int)this.CellCount);
					this.Cells = array;
					this.InitializeCells((int)this.CellCount);
				}
				return true;
			}

			// Token: 0x06001812 RID: 6162 RVA: 0x000BD5B4 File Offset: 0x000BB7B4
			public void SetBorderKind(RtfBorderId borderId, RtfBorderKind value)
			{
				switch (borderId)
				{
				case RtfBorderId.RowLeft:
					this.BorderKind.Left = value;
					return;
				case RtfBorderId.RowTop:
					this.BorderKind.Top = value;
					return;
				case RtfBorderId.RowRight:
					this.BorderKind.Right = value;
					return;
				case RtfBorderId.RowBottom:
					this.BorderKind.Bottom = value;
					return;
				case RtfBorderId.RowHorizontal:
					this.BorderKind.Horizontal = value;
					return;
				case RtfBorderId.RowVertical:
					this.BorderKind.Vertical = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x06001813 RID: 6163 RVA: 0x000BD634 File Offset: 0x000BB834
			public void SetBorderWidth(RtfBorderId borderId, byte value)
			{
				switch (borderId)
				{
				case RtfBorderId.RowLeft:
					this.BorderWidth.Left = value;
					return;
				case RtfBorderId.RowTop:
					this.BorderWidth.Top = value;
					return;
				case RtfBorderId.RowRight:
					this.BorderWidth.Right = value;
					return;
				case RtfBorderId.RowBottom:
					this.BorderWidth.Bottom = value;
					return;
				case RtfBorderId.RowHorizontal:
					this.BorderWidth.Horizontal = value;
					return;
				case RtfBorderId.RowVertical:
					this.BorderWidth.Vertical = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x06001814 RID: 6164 RVA: 0x000BD6B4 File Offset: 0x000BB8B4
			public void SetBorderColor(RtfBorderId borderId, int value)
			{
				switch (borderId)
				{
				case RtfBorderId.RowLeft:
					this.BorderColor.Left = value;
					return;
				case RtfBorderId.RowTop:
					this.BorderColor.Top = value;
					return;
				case RtfBorderId.RowRight:
					this.BorderColor.Right = value;
					return;
				case RtfBorderId.RowBottom:
					this.BorderColor.Bottom = value;
					return;
				case RtfBorderId.RowHorizontal:
					this.BorderColor.Horizontal = value;
					return;
				case RtfBorderId.RowVertical:
					this.BorderColor.Vertical = value;
					return;
				default:
					return;
				}
			}

			// Token: 0x06001815 RID: 6165 RVA: 0x000BD734 File Offset: 0x000BB934
			private void InitializeCells(int startIndex)
			{
				for (int i = startIndex; i < this.Cells.Length; i++)
				{
					this.Cells[i].Initialize();
				}
			}

			// Token: 0x04001B56 RID: 6998
			public RtfFormatConverter.RtfRow NextFree;

			// Token: 0x04001B57 RID: 6999
			public short CellCount;

			// Token: 0x04001B58 RID: 7000
			public RtfFormatConverter.RtfRowCell[] Cells;

			// Token: 0x04001B59 RID: 7001
			public bool RightToLeft;

			// Token: 0x04001B5A RID: 7002
			public bool HeaderRow;

			// Token: 0x04001B5B RID: 7003
			public bool LastRow;

			// Token: 0x04001B5C RID: 7004
			public short Left;

			// Token: 0x04001B5D RID: 7005
			public short Right;

			// Token: 0x04001B5E RID: 7006
			public short Width;

			// Token: 0x04001B5F RID: 7007
			public byte WidthType;

			// Token: 0x04001B60 RID: 7008
			public short Height;

			// Token: 0x04001B61 RID: 7009
			public bool HeightExact;

			// Token: 0x04001B62 RID: 7010
			public int BackColor;

			// Token: 0x04001B63 RID: 7011
			public RtfFormatConverter.RtfFourSideValue<short> CellPadding;

			// Token: 0x04001B64 RID: 7012
			public RtfFormatConverter.RtfSixSideValue<byte> BorderWidth;

			// Token: 0x04001B65 RID: 7013
			public RtfFormatConverter.RtfSixSideValue<int> BorderColor;

			// Token: 0x04001B66 RID: 7014
			public RtfFormatConverter.RtfSixSideValue<RtfBorderKind> BorderKind;
		}
	}
}
