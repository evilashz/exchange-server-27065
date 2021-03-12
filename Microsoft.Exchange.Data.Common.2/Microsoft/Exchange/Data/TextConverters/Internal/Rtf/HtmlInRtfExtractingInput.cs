using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000277 RID: 631
	internal class HtmlInRtfExtractingInput : ConverterInput
	{
		// Token: 0x0600198A RID: 6538 RVA: 0x000C9AB4 File Offset: 0x000C7CB4
		public HtmlInRtfExtractingInput(RtfParser parser, int maxParseToken, bool testBoundaryConditions, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum) : base(null)
		{
			this.parser = parser;
			this.state = new HtmlInRtfExtractingInput.RtfState(16);
			this.maxTokenSize = (testBoundaryConditions ? 123 : maxParseToken);
			this.parseBuffer = new char[Math.Min((long)maxParseToken + 1L, 4096L)];
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000C9B08 File Offset: 0x000C7D08
		public override bool ReadMore(ref char[] buffer, ref int start, ref int current, ref int end)
		{
			if (this.parseBuffer.Length - this.parseEnd < 6 && !this.EnsureFreeSpace())
			{
				return true;
			}
			int num = this.parseEnd;
			if (this.incompleteToken != null)
			{
				if (this.incompleteToken.Id == RtfTokenId.Keywords)
				{
					this.ProcessKeywords(this.incompleteToken);
				}
				else
				{
					this.ProcessText(this.incompleteToken);
				}
			}
			while (!this.endOfFile && this.parseBuffer.Length - this.parseEnd >= 6)
			{
				RtfTokenId rtfTokenId = this.parser.Parse();
				if (rtfTokenId == RtfTokenId.None)
				{
					break;
				}
				this.Process(rtfTokenId);
			}
			buffer = this.parseBuffer;
			if (start != this.parseStart)
			{
				current = this.parseStart + (current - start);
				start = this.parseStart;
			}
			end = this.parseEnd;
			return end != num || this.endOfFile;
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000C9BD9 File Offset: 0x000C7DD9
		public override void ReportProcessed(int processedSize)
		{
			this.parseStart += processedSize;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000C9BEC File Offset: 0x000C7DEC
		public override int RemoveGap(int gapBegin, int gapEnd)
		{
			if (gapEnd == this.parseEnd)
			{
				this.parseEnd = gapBegin;
				this.parseBuffer[gapBegin] = '\0';
				return gapBegin;
			}
			Buffer.BlockCopy(this.parseBuffer, gapEnd, this.parseBuffer, gapBegin, this.parseEnd - gapEnd);
			this.parseEnd = gapBegin + (this.parseEnd - gapEnd);
			this.parseBuffer[this.parseEnd] = '\0';
			return this.parseEnd;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000C9C53 File Offset: 0x000C7E53
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.parser != null && this.parser is IDisposable)
			{
				((IDisposable)this.parser).Dispose();
			}
			this.parser = null;
			base.Dispose(disposing);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000C9C8C File Offset: 0x000C7E8C
		private void Process(RtfTokenId tokenId)
		{
			switch (tokenId)
			{
			case RtfTokenId.None:
			case (RtfTokenId)6:
				break;
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
			case RtfTokenId.Text:
				this.ProcessText(this.parser.Token);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000C9D0C File Offset: 0x000C7F0C
		private void ProcessBeginGroup()
		{
			this.state.Push();
			if (this.state.Skip)
			{
				return;
			}
			this.firstKeyword = true;
			this.ignorableDestination = false;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000C9D38 File Offset: 0x000C7F38
		private void ProcessEndGroup()
		{
			if (this.state.Skip)
			{
				this.state.Pop();
				if (this.state.Skip)
				{
					return;
				}
			}
			this.firstKeyword = false;
			if (this.state.CanPop)
			{
				this.state.Pop();
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000C9D8C File Offset: 0x000C7F8C
		private void ProcessKeywords(RtfToken token)
		{
			if (this.state.Skip)
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
					if (id <= 210)
					{
						if (id <= 24)
						{
							if (id != 15 && id != 24)
							{
								goto IL_19D;
							}
						}
						else if (id != 50)
						{
							switch (id)
							{
							case 175:
								if (this.state.Destination == RtfDestination.RTF)
								{
									this.state.Destination = RtfDestination.FontTable;
									continue;
								}
								continue;
							case 176:
								goto IL_19D;
							case 177:
								this.state.Destination = RtfDestination.HtmlTagIndex;
								continue;
							default:
								if (id != 210)
								{
									goto IL_19D;
								}
								if (this.state.Destination == RtfDestination.FontTable)
								{
									this.state.Destination = RtfDestination.RealFontName;
									continue;
								}
								continue;
							}
						}
					}
					else if (id <= 252)
					{
						if (id != 246)
						{
							if (id != 252)
							{
								goto IL_19D;
							}
							if (this.state.Destination == RtfDestination.RTF)
							{
								this.state.Destination = RtfDestination.ColorTable;
								continue;
							}
							continue;
						}
					}
					else if (id != 268)
					{
						if (id != 315)
						{
							if (id != 319)
							{
								goto IL_19D;
							}
							this.state.Destination = RtfDestination.ListText;
							continue;
						}
					}
					else
					{
						if (this.state.Destination == RtfDestination.FontTable)
						{
							this.state.Destination = RtfDestination.AltFontName;
							continue;
						}
						continue;
					}
					this.state.PushSkipGroup();
					return;
					IL_19D:
					if (this.ignorableDestination)
					{
						this.state.PushSkipGroup();
						return;
					}
				}
				short id2 = rtfKeyword.Id;
				if (id2 <= 68)
				{
					if (id2 == 40 || id2 == 48 || id2 == 68)
					{
						if (!this.ignoreRtf && (this.state.Destination == RtfDestination.RTF || this.state.Destination == RtfDestination.HtmlTagIndex))
						{
							this.Output("\r\n");
						}
					}
				}
				else
				{
					if (id2 <= 119)
					{
						if (id2 == 83)
						{
							goto IL_27B;
						}
						if (id2 != 119)
						{
							goto IL_27B;
						}
					}
					else if (id2 != 126)
					{
						if (id2 != 153)
						{
							goto IL_27B;
						}
						this.ignoreRtf = (rtfKeyword.Value != 0);
						goto IL_27B;
					}
					if (!this.ignoreRtf && (this.state.Destination == RtfDestination.RTF || this.state.Destination == RtfDestination.HtmlTagIndex))
					{
						this.Output("\t");
					}
				}
				IL_27B:
				if (this.parseBuffer.Length - this.parseEnd >= 6)
				{
					continue;
				}
				this.incompleteToken = token;
				return;
			}
			this.incompleteToken = null;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000CA044 File Offset: 0x000C8244
		private void ProcessText(RtfToken token)
		{
			if (this.state.Skip)
			{
				return;
			}
			RtfDestination destination = this.state.Destination;
			if (destination != RtfDestination.RTF)
			{
				if (destination != RtfDestination.HtmlTagIndex)
				{
					this.firstKeyword = false;
					return;
				}
			}
			else if (this.ignoreRtf)
			{
				return;
			}
			token.StripZeroBytes = true;
			int num = token.Text.Read(this.parseBuffer, this.parseEnd, this.parseBuffer.Length - this.parseEnd - 1);
			this.parseEnd += num;
			this.parseBuffer[this.parseEnd] = '\0';
			if (this.parseEnd == this.parseBuffer.Length - 1)
			{
				this.incompleteToken = token;
				return;
			}
			this.incompleteToken = null;
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000CA0F5 File Offset: 0x000C82F5
		private void ProcessBinary(RtfToken token)
		{
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000CA0F7 File Offset: 0x000C82F7
		private void ProcessEOF()
		{
			while (this.state.CanPop)
			{
				this.ProcessEndGroup();
			}
			this.endOfFile = true;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000CA115 File Offset: 0x000C8315
		private void Output(string str)
		{
			str.CopyTo(0, this.parseBuffer, this.parseEnd, str.Length);
			this.parseEnd += str.Length;
			this.parseBuffer[this.parseEnd] = '\0';
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x000CA154 File Offset: 0x000C8354
		private bool EnsureFreeSpace()
		{
			if (this.parseBuffer.Length - (this.parseEnd - this.parseStart) < 6 || (this.parseStart < 1 && (long)this.parseBuffer.Length < (long)this.maxTokenSize + 1L))
			{
				if ((long)this.parseBuffer.Length >= (long)this.maxTokenSize + 5L + 1L)
				{
					return false;
				}
				long num = (long)(this.parseBuffer.Length * 2);
				if (num > (long)this.maxTokenSize + 5L + 1L)
				{
					num = (long)this.maxTokenSize + 5L + 1L;
				}
				if (num > 2147483647L)
				{
					num = 2147483647L;
				}
				char[] dst = new char[(int)num];
				Buffer.BlockCopy(this.parseBuffer, this.parseStart * 2, dst, 0, (this.parseEnd - this.parseStart + 1) * 2);
				this.parseBuffer = dst;
				this.parseEnd -= this.parseStart;
				this.parseStart = 0;
			}
			else
			{
				Buffer.BlockCopy(this.parseBuffer, this.parseStart * 2, this.parseBuffer, 0, (this.parseEnd - this.parseStart + 1) * 2);
				this.parseEnd -= this.parseStart;
				this.parseStart = 0;
			}
			return true;
		}

		// Token: 0x04001EC1 RID: 7873
		private RtfParser parser;

		// Token: 0x04001EC2 RID: 7874
		private bool firstKeyword;

		// Token: 0x04001EC3 RID: 7875
		private bool ignorableDestination;

		// Token: 0x04001EC4 RID: 7876
		private HtmlInRtfExtractingInput.RtfState state;

		// Token: 0x04001EC5 RID: 7877
		private bool ignoreRtf;

		// Token: 0x04001EC6 RID: 7878
		private RtfToken incompleteToken;

		// Token: 0x04001EC7 RID: 7879
		private char[] parseBuffer;

		// Token: 0x04001EC8 RID: 7880
		private int parseStart;

		// Token: 0x04001EC9 RID: 7881
		private int parseEnd;

		// Token: 0x02000278 RID: 632
		private struct RtfState
		{
			// Token: 0x06001998 RID: 6552 RVA: 0x000CA28C File Offset: 0x000C848C
			public RtfState(int initialStackSize)
			{
				this.level = 0;
				this.skipLevel = 0;
				this.current = default(HtmlInRtfExtractingInput.RtfState.State);
				this.stack = new HtmlInRtfExtractingInput.RtfState.State[initialStackSize];
				this.stackTop = 0;
				this.current.LevelsDeep = 1;
				this.current.Dest = RtfDestination.RTF;
			}

			// Token: 0x17000685 RID: 1669
			// (get) Token: 0x06001999 RID: 6553 RVA: 0x000CA2DE File Offset: 0x000C84DE
			// (set) Token: 0x0600199A RID: 6554 RVA: 0x000CA2EB File Offset: 0x000C84EB
			public RtfDestination Destination
			{
				get
				{
					return this.current.Dest;
				}
				set
				{
					this.SetDirty();
					this.current.Dest = value;
				}
			}

			// Token: 0x17000686 RID: 1670
			// (get) Token: 0x0600199B RID: 6555 RVA: 0x000CA2FF File Offset: 0x000C84FF
			public bool Skip
			{
				get
				{
					return this.skipLevel != 0;
				}
			}

			// Token: 0x17000687 RID: 1671
			// (get) Token: 0x0600199C RID: 6556 RVA: 0x000CA30D File Offset: 0x000C850D
			public bool CanPop
			{
				get
				{
					return this.level > 1;
				}
			}

			// Token: 0x0600199D RID: 6557 RVA: 0x000CA318 File Offset: 0x000C8518
			public void Push()
			{
				this.level++;
				this.current.LevelsDeep = this.current.LevelsDeep + 1;
				if (this.current.LevelsDeep == 32767)
				{
					this.PushReally();
				}
			}

			// Token: 0x0600199E RID: 6558 RVA: 0x000CA354 File Offset: 0x000C8554
			public void PushSkipGroup()
			{
				this.skipLevel = this.level;
				this.Push();
			}

			// Token: 0x0600199F RID: 6559 RVA: 0x000CA368 File Offset: 0x000C8568
			public void Pop()
			{
				if (this.level > 1)
				{
					if ((this.current.LevelsDeep = this.current.LevelsDeep - 1) == 0 && this.stackTop != 0)
					{
						this.current = this.stack[--this.stackTop];
					}
					if (--this.level == this.skipLevel)
					{
						this.skipLevel = 0;
					}
				}
			}

			// Token: 0x060019A0 RID: 6560 RVA: 0x000CA3E6 File Offset: 0x000C85E6
			public void SetDirty()
			{
				if (this.current.LevelsDeep > 1)
				{
					this.PushReally();
				}
			}

			// Token: 0x060019A1 RID: 6561 RVA: 0x000CA3FC File Offset: 0x000C85FC
			private void PushReally()
			{
				if (this.stackTop >= 4096)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				if (this.stackTop == this.stack.Length)
				{
					HtmlInRtfExtractingInput.RtfState.State[] destinationArray = new HtmlInRtfExtractingInput.RtfState.State[this.stackTop * 2];
					Array.Copy(this.stack, 0, destinationArray, 0, this.stackTop);
					this.stack = destinationArray;
				}
				this.stack[this.stackTop] = this.current;
				HtmlInRtfExtractingInput.RtfState.State[] array = this.stack;
				int num = this.stackTop;
				array[num].LevelsDeep = array[num].LevelsDeep - 1;
				this.stackTop++;
				this.current.LevelsDeep = 1;
			}

			// Token: 0x04001ECA RID: 7882
			private int level;

			// Token: 0x04001ECB RID: 7883
			private int skipLevel;

			// Token: 0x04001ECC RID: 7884
			private HtmlInRtfExtractingInput.RtfState.State current;

			// Token: 0x04001ECD RID: 7885
			private HtmlInRtfExtractingInput.RtfState.State[] stack;

			// Token: 0x04001ECE RID: 7886
			private int stackTop;

			// Token: 0x02000279 RID: 633
			public struct State
			{
				// Token: 0x04001ECF RID: 7887
				public RtfDestination Dest;

				// Token: 0x04001ED0 RID: 7888
				public short LevelsDeep;
			}
		}
	}
}
