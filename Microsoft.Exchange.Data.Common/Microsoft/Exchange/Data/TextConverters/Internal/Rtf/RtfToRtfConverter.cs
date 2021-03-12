using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000235 RID: 565
	internal class RtfToRtfConverter : IProducerConsumer, IDisposable
	{
		// Token: 0x06001749 RID: 5961 RVA: 0x000B5510 File Offset: 0x000B3710
		public RtfToRtfConverter(RtfParser parser, Stream destination, bool push, Injection injection, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum)
		{
			this.output = new RtfOutput(destination, push, false);
			this.parser = parser;
			this.injection = injection;
			this.state = new RtfToRtfConverter.RtfState(128);
			if (this.injection != null)
			{
				this.injection.CompileForRtf(this.output);
			}
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000B556C File Offset: 0x000B376C
		void IDisposable.Dispose()
		{
			if (this.parser != null && this.parser is IDisposable)
			{
				((IDisposable)this.parser).Dispose();
			}
			if (this.output != null && this.output != null)
			{
				((IDisposable)this.output).Dispose();
			}
			this.parser = null;
			this.output = null;
			this.scratch.DisposeBuffer();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000B55D8 File Offset: 0x000B37D8
		public void Run()
		{
			if (!this.endOfFile)
			{
				RtfTokenId rtfTokenId = this.parser.Parse();
				if (rtfTokenId != RtfTokenId.None)
				{
					this.Process(rtfTokenId);
				}
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x000B5603 File Offset: 0x000B3803
		public bool Flush()
		{
			this.Run();
			return this.endOfFile;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x000B5614 File Offset: 0x000B3814
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

		// Token: 0x0600174E RID: 5966 RVA: 0x000B5694 File Offset: 0x000B3894
		private void ProcessBeginGroup()
		{
			if (this.state.Level >= 0)
			{
				if (this.state.SkipLevel != 0 || this.state.Level < 0 || this.firstKeyword)
				{
					this.output.WriteControlText("{", false);
				}
				this.state.Push();
				this.firstKeyword = true;
			}
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000B56F8 File Offset: 0x000B38F8
		private void ProcessEndGroup()
		{
			if (this.state.Level > 0)
			{
				if (this.state.SkipLevel != 0 && this.state.Level == this.state.SkipLevel)
				{
					this.state.SkipLevel = 0;
				}
				this.firstKeyword = false;
				this.EndGroup();
				this.state.Pop();
				if (this.state.Level == 0)
				{
					this.state.Level = this.state.Level - 1;
					if (this.injection != null && this.injection.HaveTail && !this.injection.TailDone)
					{
						if (this.lineLength != 0)
						{
							this.output.WriteControlText("\\par\r\n", false);
							this.output.RtfLineLength = 0;
							this.lineBreaks = 1;
						}
						if (this.output.RtfLineLength != 0)
						{
							this.output.WriteControlText("\r\n", false);
							this.output.RtfLineLength = 0;
						}
						this.injection.InjectRtf(false, this.lineBreaks <= 1);
					}
				}
				this.output.WriteControlText("}", false);
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000B5828 File Offset: 0x000B3A28
		private void ProcessKeywords(RtfToken token)
		{
			if (this.state.Level >= 0)
			{
				foreach (RtfKeyword kw in token.Keywords)
				{
					if (this.state.SkipLevel != 0)
					{
						this.WriteKeyword(kw);
					}
					else
					{
						if (this.firstKeyword)
						{
							this.firstKeyword = false;
							if (kw.Id == 1)
							{
								this.state.SkipGroup();
							}
							else
							{
								short id = kw.Id;
								if (id <= 241)
								{
									if (id <= 175)
									{
										if (id != 15 && id != 24)
										{
											if (id != 175)
											{
												goto IL_25B;
											}
											if (this.state.Destination == RtfDestination.RTF)
											{
												this.state.FontIndex = -1;
												this.state.Destination = RtfDestination.FontTable;
												goto IL_2B4;
											}
											goto IL_2B4;
										}
									}
									else if (id <= 210)
									{
										if (id != 201)
										{
											if (id != 210)
											{
												goto IL_25B;
											}
											if (this.state.Destination == RtfDestination.FontTable && this.state.FontIndex >= 0)
											{
												this.state.Destination = RtfDestination.RealFontName;
												goto IL_2B4;
											}
											goto IL_2B4;
										}
									}
									else if (id != 230 && id != 241)
									{
										goto IL_25B;
									}
								}
								else if (id <= 258)
								{
									if (id != 246)
									{
										switch (id)
										{
										case 252:
											if (this.state.Destination == RtfDestination.RTF)
											{
												this.state.Destination = RtfDestination.ColorTable;
												goto IL_2B4;
											}
											goto IL_2B4;
										case 253:
											break;
										default:
											if (id != 258)
											{
												goto IL_25B;
											}
											break;
										}
									}
								}
								else if (id <= 273)
								{
									switch (id)
									{
									case 267:
										this.output.WriteControlText("{", false);
										this.WriteKeyword(kw);
										continue;
									case 268:
										if (this.state.Destination == RtfDestination.FontTable && this.state.FontIndex >= 0)
										{
											this.state.Destination = RtfDestination.AltFontName;
											goto IL_2B4;
										}
										goto IL_2B4;
									default:
										if (id != 273)
										{
											goto IL_25B;
										}
										break;
									}
								}
								else if (id != 277)
								{
									switch (id)
									{
									case 315:
									case 316:
										break;
									default:
										goto IL_25B;
									}
								}
								this.state.SkipGroup();
								goto IL_2B4;
								IL_25B:
								if (this.state.Destination == RtfDestination.RTF && this.injection != null && this.injection.HaveHead && !this.injection.HeadDone)
								{
									this.output.WriteControlText("\r\n", false);
									this.output.RtfLineLength = 0;
									this.injection.InjectRtf(true, false);
								}
							}
							IL_2B4:
							this.output.WriteControlText("{", false);
						}
						short id2 = kw.Id;
						if (id2 <= 148)
						{
							if (id2 <= 76)
							{
								if (id2 != 48 && id2 != 68)
								{
									if (id2 == 76)
									{
										goto IL_584;
									}
								}
								else
								{
									if (this.state.Destination == RtfDestination.RTF)
									{
										this.lineLength = 0;
										this.lineBreaks++;
										goto IL_51D;
									}
									goto IL_51D;
								}
							}
							else if (id2 <= 116)
							{
								if (id2 == 88)
								{
									goto IL_480;
								}
								switch (id2)
								{
								case 109:
								case 112:
								case 115:
								case 116:
									goto IL_584;
								case 113:
									if (this.state.Destination == RtfDestination.FontTable || this.state.Destination == RtfDestination.AltFontName || this.state.Destination == RtfDestination.RealFontName)
									{
										if (this.state.FontIndex >= 0)
										{
											this.state.FontIndex = -1;
										}
										short num = this.parser.FontIndex((short)kw.Value);
										if (num >= 0)
										{
											this.state.FontIndex = num;
											if (this.parser.FontHandle(num) > this.maxFontHandle)
											{
												this.maxFontHandle = this.parser.FontHandle(num);
											}
										}
									}
									break;
								}
							}
							else
							{
								if (id2 == 126)
								{
									goto IL_51D;
								}
								if (id2 == 148)
								{
									goto IL_480;
								}
							}
						}
						else if (id2 <= 206)
						{
							if (id2 <= 184)
							{
								if (id2 == 170)
								{
									goto IL_584;
								}
								if (id2 == 184)
								{
									goto IL_51D;
								}
							}
							else
							{
								if (id2 == 200)
								{
									goto IL_51D;
								}
								if (id2 == 206)
								{
									goto IL_584;
								}
							}
						}
						else if (id2 <= 284)
						{
							if (id2 == 265 || id2 == 284)
							{
								goto IL_584;
							}
						}
						else
						{
							if (id2 == 309)
							{
								goto IL_480;
							}
							if (id2 == 331)
							{
								goto IL_584;
							}
						}
						IL_5DD:
						this.WriteKeyword(kw);
						continue;
						IL_480:
						if (this.state.Destination == RtfDestination.ColorTable)
						{
							this.color &= ~(255 << (int)(RTFData.keywords[(int)kw.Id].idx * 8));
							this.color |= (kw.Value & 255) << (int)(RTFData.keywords[(int)kw.Id].idx * 8);
							goto IL_5DD;
						}
						goto IL_5DD;
						IL_51D:
						if (this.state.Destination == RtfDestination.RTF && this.injection != null && this.injection.HaveHead && !this.injection.HeadDone)
						{
							this.output.WriteControlText("\r\n", false);
							this.output.RtfLineLength = 0;
							this.injection.InjectRtf(true, false);
							goto IL_5DD;
						}
						goto IL_5DD;
						IL_584:
						if (this.state.Destination == RtfDestination.RTF && this.injection != null && this.injection.HaveHead && !this.injection.HeadDone)
						{
							this.output.WriteControlText("\r\n", false);
							this.output.RtfLineLength = 0;
							this.injection.InjectRtf(true, false);
							goto IL_5DD;
						}
						goto IL_5DD;
					}
				}
			}
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000B5E28 File Offset: 0x000B4028
		private void ProcessText(RtfToken token)
		{
			if (this.state.Level >= 0)
			{
				if (this.state.Destination == RtfDestination.RTF && this.state.SkipLevel == 0)
				{
					bool flag = true;
					foreach (RtfRun rtfRun in token.Runs)
					{
						if (rtfRun.Kind != RtfRunKind.Ignore)
						{
							flag = false;
							break;
						}
					}
					token.Runs.Rewind();
					if (!flag)
					{
						if (this.injection != null && this.injection.HaveHead && !this.injection.HeadDone)
						{
							this.output.WriteControlText("\r\n", false);
							this.output.RtfLineLength = 0;
							this.injection.InjectRtf(true, false);
						}
						this.lineLength++;
						this.lineBreaks = 0;
					}
				}
				if (this.firstKeyword && this.state.SkipLevel == 0)
				{
					this.output.WriteControlText("{", false);
					this.firstKeyword = false;
				}
				this.WriteToken(token);
				token.Runs.Rewind();
				switch (this.state.Destination)
				{
				case RtfDestination.FontTable:
				case RtfDestination.RealFontName:
				case RtfDestination.AltFontName:
					break;
				case RtfDestination.ColorTable:
					this.scratch.Reset();
					while (this.scratch.AppendRtfTokenText(token, 256))
					{
						for (int i = 0; i < this.scratch.Length; i++)
						{
							if (this.scratch[i] == ';')
							{
								this.colorsCount++;
								this.color = 0;
							}
						}
						this.scratch.Reset();
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000B5FDB File Offset: 0x000B41DB
		private void ProcessBinary(RtfToken token)
		{
			if (this.state.Level >= 0)
			{
				this.WriteToken(token);
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000B5FF2 File Offset: 0x000B41F2
		private void ProcessEOF()
		{
			while (this.state.Level > 0)
			{
				this.ProcessEndGroup();
			}
			this.output.Flush();
			this.endOfFile = true;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000B601C File Offset: 0x000B421C
		private void EndGroup()
		{
			RtfDestination destination = this.state.Destination;
			if (destination != RtfDestination.FontTable)
			{
				if (destination != RtfDestination.ColorTable)
				{
					return;
				}
				if (this.state.ParentDestination != RtfDestination.ColorTable && this.injection != null)
				{
					if (this.color != 0)
					{
						this.colorsCount++;
						this.output.WriteControlText(";", false);
					}
					this.output.WriteControlText("\r\n", false);
					this.output.RtfLineLength = 0;
					this.injection.InjectRtfColors(this.colorsCount);
				}
			}
			else if (this.state.ParentDestination != RtfDestination.FontTable)
			{
				if (this.injection != null)
				{
					this.output.WriteControlText("\r\n", false);
					this.output.RtfLineLength = 0;
					this.injection.InjectRtfFonts((int)(this.maxFontHandle + 1));
					return;
				}
			}
			else if (this.state.FontIndex >= 0)
			{
				this.state.ParentFontIndex = this.state.FontIndex;
				return;
			}
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x000B611C File Offset: 0x000B431C
		private void WriteToken(RtfToken token)
		{
			foreach (RtfRun rtfRun in token.Runs)
			{
				this.output.WriteBinary(rtfRun.Buffer, rtfRun.Offset, rtfRun.Length);
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000B616B File Offset: 0x000B436B
		private void WriteKeyword(RtfKeyword kw)
		{
			this.output.WriteBinary(kw.Buffer, kw.Offset, kw.Length);
		}

		// Token: 0x04001A96 RID: 6806
		private RtfOutput output;

		// Token: 0x04001A97 RID: 6807
		private bool endOfFile;

		// Token: 0x04001A98 RID: 6808
		private RtfParser parser;

		// Token: 0x04001A99 RID: 6809
		private bool firstKeyword;

		// Token: 0x04001A9A RID: 6810
		private RtfToRtfConverter.RtfState state;

		// Token: 0x04001A9B RID: 6811
		private short maxFontHandle;

		// Token: 0x04001A9C RID: 6812
		private int colorsCount;

		// Token: 0x04001A9D RID: 6813
		private int lineLength;

		// Token: 0x04001A9E RID: 6814
		private int lineBreaks;

		// Token: 0x04001A9F RID: 6815
		private Injection injection;

		// Token: 0x04001AA0 RID: 6816
		private int color;

		// Token: 0x04001AA1 RID: 6817
		private ScratchBuffer scratch;

		// Token: 0x02000236 RID: 566
		internal struct RtfState
		{
			// Token: 0x06001757 RID: 5975 RVA: 0x000B6190 File Offset: 0x000B4390
			public RtfState(int initialStackSize)
			{
				this.Level = 0;
				this.SkipLevel = 0;
				this.Current = default(RtfToRtfConverter.RtfState.State);
				this.stack = new RtfToRtfConverter.RtfState.State[initialStackSize];
				this.stackTop = 0;
				this.Current.Dest = RtfDestination.RTF;
				this.Current.FontIndex = 0;
			}

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x06001758 RID: 5976 RVA: 0x000B61E2 File Offset: 0x000B43E2
			// (set) Token: 0x06001759 RID: 5977 RVA: 0x000B61EF File Offset: 0x000B43EF
			public RtfDestination Destination
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

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x0600175A RID: 5978 RVA: 0x000B6211 File Offset: 0x000B4411
			// (set) Token: 0x0600175B RID: 5979 RVA: 0x000B621E File Offset: 0x000B441E
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

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x0600175C RID: 5980 RVA: 0x000B6240 File Offset: 0x000B4440
			public RtfDestination ParentDestination
			{
				get
				{
					if (this.Current.Depth <= 1 && this.stackTop != 0)
					{
						return this.stack[this.stackTop - 1].Dest;
					}
					return this.Current.Dest;
				}
			}

			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x0600175D RID: 5981 RVA: 0x000B627C File Offset: 0x000B447C
			// (set) Token: 0x0600175E RID: 5982 RVA: 0x000B62B8 File Offset: 0x000B44B8
			public short ParentFontIndex
			{
				get
				{
					if (this.Current.Depth <= 1 && this.stackTop != 0)
					{
						return this.stack[this.stackTop - 1].FontIndex;
					}
					return this.Current.FontIndex;
				}
				set
				{
					this.Current.FontIndex = value;
					if (this.stackTop > 0)
					{
						this.stack[this.stackTop - 1].FontIndex = value;
					}
				}
			}

			// Token: 0x0600175F RID: 5983 RVA: 0x000B62E8 File Offset: 0x000B44E8
			public void Push()
			{
				this.Level++;
				this.Current.Depth = this.Current.Depth + 1;
				if (this.Current.Depth == 32767)
				{
					this.PushReally();
				}
			}

			// Token: 0x06001760 RID: 5984 RVA: 0x000B6324 File Offset: 0x000B4524
			public void SetDirty()
			{
				if (this.Current.Depth > 1)
				{
					this.PushReally();
				}
			}

			// Token: 0x06001761 RID: 5985 RVA: 0x000B633C File Offset: 0x000B453C
			public void Pop()
			{
				if (this.Level > 0)
				{
					this.Current.Depth = this.Current.Depth - 1;
					if (this.Current.Depth == 0 && this.stackTop != 0)
					{
						this.Current = this.stack[--this.stackTop];
					}
					this.Level--;
				}
			}

			// Token: 0x06001762 RID: 5986 RVA: 0x000B63B0 File Offset: 0x000B45B0
			public void SkipGroup()
			{
				this.SkipLevel = this.Level;
			}

			// Token: 0x06001763 RID: 5987 RVA: 0x000B63C0 File Offset: 0x000B45C0
			private void PushReally()
			{
				if (this.stackTop >= 4096)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				if (this.stack == null || this.stackTop == this.stack.Length)
				{
					RtfToRtfConverter.RtfState.State[] destinationArray;
					if (this.stack != null)
					{
						destinationArray = new RtfToRtfConverter.RtfState.State[this.stack.Length * 2];
						Array.Copy(this.stack, 0, destinationArray, 0, this.stackTop);
					}
					else
					{
						destinationArray = new RtfToRtfConverter.RtfState.State[4];
					}
					this.stack = destinationArray;
				}
				this.stack[this.stackTop] = this.Current;
				RtfToRtfConverter.RtfState.State[] array = this.stack;
				int num = this.stackTop;
				array[num].Depth = array[num].Depth - 1;
				this.stackTop++;
				this.Current.Depth = 1;
			}

			// Token: 0x04001AA2 RID: 6818
			public int Level;

			// Token: 0x04001AA3 RID: 6819
			public int SkipLevel;

			// Token: 0x04001AA4 RID: 6820
			public RtfToRtfConverter.RtfState.State Current;

			// Token: 0x04001AA5 RID: 6821
			private RtfToRtfConverter.RtfState.State[] stack;

			// Token: 0x04001AA6 RID: 6822
			private int stackTop;

			// Token: 0x02000237 RID: 567
			public struct State
			{
				// Token: 0x04001AA7 RID: 6823
				public short Depth;

				// Token: 0x04001AA8 RID: 6824
				public RtfDestination Dest;

				// Token: 0x04001AA9 RID: 6825
				public short FontIndex;
			}
		}
	}
}
