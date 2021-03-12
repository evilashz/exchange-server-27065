using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000232 RID: 562
	internal class RtfToTextConverter : IProducerConsumer, IDisposable
	{
		// Token: 0x06001732 RID: 5938 RVA: 0x000B4587 File Offset: 0x000B2787
		public RtfToTextConverter(RtfParser parser, TextOutput output, Injection injection, bool treatNbspAsBreakable, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum)
		{
			this.treatNbspAsBreakable = treatNbspAsBreakable;
			this.output = output;
			this.parser = parser;
			this.injection = injection;
			this.state = new RtfToTextConverter.RtfState(128);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000B45BC File Offset: 0x000B27BC
		public void Run()
		{
			if (this.endOfFile)
			{
				return;
			}
			RtfTokenId tokenId = this.parser.Parse();
			this.Process(tokenId);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000B45E5 File Offset: 0x000B27E5
		public bool Flush()
		{
			if (!this.endOfFile)
			{
				this.Run();
			}
			return this.endOfFile;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000B45FC File Offset: 0x000B27FC
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
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x000B4660 File Offset: 0x000B2860
		private void Process(RtfTokenId tokenId)
		{
			if (!this.started)
			{
				this.output.OpenDocument();
				if (this.injection != null && this.injection.HaveHead)
				{
					this.injection.Inject(true, this.output);
				}
				this.started = true;
			}
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

		// Token: 0x06001737 RID: 5943 RVA: 0x000B4721 File Offset: 0x000B2921
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

		// Token: 0x06001738 RID: 5944 RVA: 0x000B4760 File Offset: 0x000B2960
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
				this.state.Pop();
			}
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000B47D8 File Offset: 0x000B29D8
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
					if (id <= 135)
					{
						if (id <= 29)
						{
							if (id != 15 && id != 24)
							{
								if (id != 29)
								{
									goto IL_2EB;
								}
								if (this.state.Destination == RtfDestination.RTF || this.state.Destination == RtfDestination.FieldResult)
								{
									this.state.Destination = RtfDestination.Field;
								}
								break;
							}
						}
						else if (id <= 50)
						{
							if (id != 43)
							{
								if (id != 50)
								{
									goto IL_2EB;
								}
								if (this.state.Destination == RtfDestination.RTF || this.state.Destination == RtfDestination.FieldResult)
								{
									this.state.Destination = RtfDestination.Picture;
									this.pictureWidth = 0;
									this.pictureHeight = 0;
									goto IL_303;
								}
								this.state.SkipGroup();
								break;
							}
							else
							{
								if (this.state.Destination == RtfDestination.Picture && this.includePictureField)
								{
									this.state.Destination = RtfDestination.PictureProperties;
									continue;
								}
								this.state.SkipGroup();
								break;
							}
						}
						else if (id != 124)
						{
							if (id != 135)
							{
								goto IL_2EB;
							}
							if (this.state.Destination == RtfDestination.PictureProperties)
							{
								this.state.Destination = RtfDestination.ShapeName;
								continue;
							}
							continue;
						}
						else
						{
							if (this.state.Destination == RtfDestination.PictureProperties)
							{
								this.state.Destination = RtfDestination.ShapeValue;
								continue;
							}
							continue;
						}
					}
					else if (id <= 246)
					{
						if (id != 175)
						{
							if (id != 201 && id != 246)
							{
								goto IL_2EB;
							}
						}
						else
						{
							if (this.state.Destination == RtfDestination.RTF)
							{
								this.state.SkipGroup();
								break;
							}
							continue;
						}
					}
					else if (id <= 269)
					{
						if (id != 252)
						{
							if (id != 269)
							{
								goto IL_2EB;
							}
							if (this.state.Destination == RtfDestination.Field && !this.skipFieldResult)
							{
								this.state.Destination = RtfDestination.FieldResult;
								continue;
							}
							this.skipFieldResult = false;
							this.state.SkipGroup();
							break;
						}
						else
						{
							if (this.state.Destination == RtfDestination.RTF)
							{
								this.state.SkipGroup();
								break;
							}
							continue;
						}
					}
					else if (id != 306)
					{
						if (id != 315)
						{
							goto IL_2EB;
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
						break;
					}
					this.state.SkipGroup();
					break;
					IL_2EB:
					if (this.ignorableDestination)
					{
						this.state.SkipGroup();
						break;
					}
				}
				IL_303:
				short id2 = rtfKeyword.Id;
				if (id2 <= 126)
				{
					if (id2 <= 68)
					{
						if (id2 <= 40)
						{
							if (id2 != 6)
							{
								if (id2 != 40)
								{
									continue;
								}
								goto IL_466;
							}
							else
							{
								if (this.state.Destination == RtfDestination.Picture)
								{
									this.pictureHeight = rtfKeyword.Value;
									continue;
								}
								continue;
							}
						}
						else if (id2 != 48)
						{
							if (id2 != 68)
							{
								continue;
							}
							goto IL_466;
						}
						IL_4A5:
						this.output.OutputNewLine();
						continue;
						IL_466:
						if (this.hyperLinkActive)
						{
							if (!this.urlCompareSink.IsMatch)
							{
								this.output.CloseAnchor();
							}
							else
							{
								this.output.CancelAnchor();
							}
							this.hyperLinkActive = false;
							this.urlCompareSink.Reset();
							goto IL_4A5;
						}
						goto IL_4A5;
					}
					else if (id2 <= 95)
					{
						if (id2 != 71)
						{
							if (id2 != 95)
							{
							}
						}
						else if (rtfKeyword.Value > 0 && rtfKeyword.Value <= 32767)
						{
						}
					}
					else if (id2 == 119 || id2 == 126)
					{
						this.output.OutputTabulation(1);
					}
				}
				else if (id2 <= 170)
				{
					if (id2 <= 142)
					{
						switch (id2)
						{
						case 134:
						case 135:
						case 137:
							break;
						case 136:
							this.state.IsInvisible = (rtfKeyword.Value != 0);
							break;
						default:
							if (id2 == 142)
							{
								if (rtfKeyword.Value >= 75 && this.output.LineEmpty && this.output.RenderingPosition() != 0)
								{
									this.output.CloseParagraph();
								}
							}
							break;
						}
					}
					else if (id2 != 154)
					{
						if (id2 != 170)
						{
						}
					}
					else if (this.state.Destination == RtfDestination.Picture)
					{
						this.pictureWidth = rtfKeyword.Value;
					}
				}
				else if (id2 <= 206)
				{
					if (id2 != 184)
					{
						if (id2 == 206)
						{
							this.lineIndent = rtfKeyword.Value;
						}
					}
					else
					{
						this.lineIndent = 0;
					}
				}
				else if (id2 != 222)
				{
					if (id2 != 284)
					{
						if (id2 != 287)
						{
						}
					}
					else
					{
						this.lineIndent += rtfKeyword.Value;
					}
				}
			}
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000B4D5C File Offset: 0x000B2F5C
		private void ProcessText(RtfToken token)
		{
			if (this.state.SkipLevel != 0 && this.state.Level >= this.state.SkipLevel)
			{
				return;
			}
			RtfDestination destination = this.state.Destination;
			if (destination != RtfDestination.RTF)
			{
				switch (destination)
				{
				case RtfDestination.Field:
					break;
				case RtfDestination.FieldResult:
					if (this.hyperLinkActive && this.urlCompareSink.IsActive)
					{
						token.Text.WriteTo(this.urlCompareSink);
						token.Text.Rewind();
						goto IL_107;
					}
					goto IL_107;
				case RtfDestination.FieldInstruction:
					this.firstKeyword = false;
					this.scratch.AppendRtfTokenText(token, 4096);
					return;
				default:
					switch (destination)
					{
					case RtfDestination.Picture:
					case RtfDestination.PictureProperties:
						break;
					case RtfDestination.ShapeName:
						this.firstKeyword = false;
						this.scratch.AppendRtfTokenText(token, 128);
						return;
					case RtfDestination.ShapeValue:
						this.firstKeyword = false;
						if (this.descriptionProperty)
						{
							this.scratch.AppendRtfTokenText(token, 4096);
							return;
						}
						return;
					default:
						this.firstKeyword = false;
						return;
					}
					break;
				}
				this.firstKeyword = false;
				return;
			}
			IL_107:
			if (this.state.IsInvisible)
			{
				return;
			}
			if (this.output.LineEmpty && this.lineIndent >= 120 && this.lineIndent < 7200)
			{
				this.output.OutputSpace(this.lineIndent / 120);
			}
			token.TextElements.OutputTextElements(this.output, this.treatNbspAsBreakable);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x000B4ED0 File Offset: 0x000B30D0
		private void ProcessBinary(RtfToken token)
		{
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x000B4ED4 File Offset: 0x000B30D4
		private void ProcessEOF()
		{
			this.output.CloseParagraph();
			if (this.injection != null && this.injection.HaveTail)
			{
				this.injection.Inject(false, this.output);
			}
			this.output.Flush();
			this.endOfFile = true;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x000B4F28 File Offset: 0x000B3128
		private void EndGroup()
		{
			RtfDestination destination = this.state.Destination;
			switch (destination)
			{
			case RtfDestination.Field:
				if (this.state.ParentDestination != RtfDestination.Field)
				{
					this.skipFieldResult = false;
					if (this.includePictureField)
					{
						PropertyValue propertyValue = new PropertyValue(LengthUnits.Twips, this.pictureHeight);
						PropertyValue propertyValue2 = new PropertyValue(LengthUnits.Twips, this.pictureWidth);
						this.output.OutputImage(this.imageUrl, this.imageAltText, propertyValue2.PixelsInteger, propertyValue.PixelsInteger);
						this.includePictureField = false;
						this.imageUrl = null;
						this.imageAltText = null;
						return;
					}
					if (this.hyperLinkActive)
					{
						if (!this.urlCompareSink.IsMatch)
						{
							this.output.CloseAnchor();
						}
						else
						{
							this.output.CancelAnchor();
						}
						this.hyperLinkActive = false;
					}
				}
				break;
			case RtfDestination.FieldResult:
				break;
			case RtfDestination.FieldInstruction:
				if (this.state.ParentDestination == RtfDestination.Field)
				{
					bool flag;
					BufferString bufferString;
					TextMapping textMapping;
					char ucs32Literal;
					short num;
					if (RtfSupport.IsHyperlinkField(ref this.scratch, out flag, out bufferString))
					{
						if (!flag)
						{
							if (this.hyperLinkActive)
							{
								if (!this.urlCompareSink.IsMatch)
								{
									this.output.CloseAnchor();
								}
								else
								{
									this.output.CancelAnchor();
								}
								this.hyperLinkActive = false;
								this.urlCompareSink.Reset();
							}
							bufferString.TrimWhitespace();
							if (bufferString.Length != 0)
							{
								string text = bufferString.ToString();
								this.output.OpenAnchor(text);
								this.hyperLinkActive = true;
								if (this.urlCompareSink == null)
								{
									this.urlCompareSink = new UrlCompareSink();
								}
								this.urlCompareSink.Initialize(text);
							}
						}
					}
					else if (RtfSupport.IsIncludePictureField(ref this.scratch, out bufferString))
					{
						bufferString.TrimWhitespace();
						if (bufferString.Length != 0)
						{
							this.includePictureField = true;
							bufferString.TrimWhitespace();
							if (bufferString.Length != 0)
							{
								this.imageUrl = bufferString.ToString();
							}
							this.pictureWidth = 0;
							this.pictureHeight = 0;
						}
					}
					else if (RtfSupport.IsSymbolField(ref this.scratch, out textMapping, out ucs32Literal, out num))
					{
						if (this.output.LineEmpty && this.lineIndent >= 120 && this.lineIndent < 7200)
						{
							this.output.OutputSpace(this.lineIndent / 120);
						}
						this.output.OutputNonspace((int)ucs32Literal, textMapping);
						this.skipFieldResult = true;
					}
					this.scratch.Reset();
					return;
				}
				break;
			default:
				switch (destination)
				{
				case RtfDestination.Picture:
					if (this.state.ParentDestination != RtfDestination.Picture && !this.includePictureField)
					{
						PropertyValue propertyValue3 = new PropertyValue(LengthUnits.Twips, this.pictureHeight);
						PropertyValue propertyValue4 = new PropertyValue(LengthUnits.Twips, this.pictureWidth);
						this.output.OutputImage(null, null, propertyValue4.PixelsInteger, propertyValue3.PixelsInteger);
						return;
					}
					break;
				case RtfDestination.PictureProperties:
					break;
				case RtfDestination.ShapeName:
					if (this.state.ParentDestination != RtfDestination.ShapeName)
					{
						BufferString bufferString2 = this.scratch.BufferString;
						bufferString2.TrimWhitespace();
						this.descriptionProperty = bufferString2.EqualsToLowerCaseStringIgnoreCase("wzdescription");
						this.scratch.Reset();
						return;
					}
					break;
				case RtfDestination.ShapeValue:
					if (this.state.ParentDestination != RtfDestination.ShapeValue && this.descriptionProperty)
					{
						BufferString bufferString3 = this.scratch.BufferString;
						bufferString3.TrimWhitespace();
						if (bufferString3.Length != 0)
						{
							this.imageAltText = bufferString3.ToString();
						}
						this.scratch.Reset();
						return;
					}
					break;
				default:
					return;
				}
				break;
			}
		}

		// Token: 0x04001A7A RID: 6778
		private bool treatNbspAsBreakable;

		// Token: 0x04001A7B RID: 6779
		private RtfParser parser;

		// Token: 0x04001A7C RID: 6780
		private bool endOfFile;

		// Token: 0x04001A7D RID: 6781
		private TextOutput output;

		// Token: 0x04001A7E RID: 6782
		private bool firstKeyword;

		// Token: 0x04001A7F RID: 6783
		private bool ignorableDestination;

		// Token: 0x04001A80 RID: 6784
		private RtfToTextConverter.RtfState state;

		// Token: 0x04001A81 RID: 6785
		private bool hyperLinkActive;

		// Token: 0x04001A82 RID: 6786
		private bool skipFieldResult;

		// Token: 0x04001A83 RID: 6787
		private bool includePictureField;

		// Token: 0x04001A84 RID: 6788
		private bool descriptionProperty;

		// Token: 0x04001A85 RID: 6789
		private string imageUrl;

		// Token: 0x04001A86 RID: 6790
		private string imageAltText;

		// Token: 0x04001A87 RID: 6791
		private int pictureWidth;

		// Token: 0x04001A88 RID: 6792
		private int pictureHeight;

		// Token: 0x04001A89 RID: 6793
		private int lineIndent;

		// Token: 0x04001A8A RID: 6794
		private Injection injection;

		// Token: 0x04001A8B RID: 6795
		private ScratchBuffer scratch;

		// Token: 0x04001A8C RID: 6796
		private UrlCompareSink urlCompareSink;

		// Token: 0x04001A8D RID: 6797
		private bool started;

		// Token: 0x02000233 RID: 563
		internal struct RtfState
		{
			// Token: 0x0600173E RID: 5950 RVA: 0x000B52A0 File Offset: 0x000B34A0
			public RtfState(int initialStackSize)
			{
				this.Level = 0;
				this.SkipLevel = 0;
				this.Current = default(RtfToTextConverter.RtfState.State);
				this.stack = new RtfToTextConverter.RtfState.State[initialStackSize];
				this.stackTop = 0;
				this.Current.LevelsDeep = 1;
				this.Current.Dest = RtfDestination.RTF;
			}

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x0600173F RID: 5951 RVA: 0x000B52F2 File Offset: 0x000B34F2
			// (set) Token: 0x06001740 RID: 5952 RVA: 0x000B52FF File Offset: 0x000B34FF
			public RtfDestination Destination
			{
				get
				{
					return this.Current.Dest;
				}
				set
				{
					if (value != this.Current.Dest)
					{
						this.SetDirty();
						this.Current.Dest = value;
					}
				}
			}

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x06001741 RID: 5953 RVA: 0x000B5321 File Offset: 0x000B3521
			public RtfDestination ParentDestination
			{
				get
				{
					if (this.Current.LevelsDeep <= 1 && this.Level != 0)
					{
						return this.stack[this.stackTop - 1].Dest;
					}
					return this.Current.Dest;
				}
			}

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x06001742 RID: 5954 RVA: 0x000B535D File Offset: 0x000B355D
			// (set) Token: 0x06001743 RID: 5955 RVA: 0x000B536A File Offset: 0x000B356A
			public bool IsInvisible
			{
				get
				{
					return this.Current.Invisible;
				}
				set
				{
					if (value != this.Current.Invisible)
					{
						this.SetDirty();
						this.Current.Invisible = value;
					}
				}
			}

			// Token: 0x06001744 RID: 5956 RVA: 0x000B538C File Offset: 0x000B358C
			public void Push()
			{
				this.Level++;
				this.Current.LevelsDeep = this.Current.LevelsDeep + 1;
				if (this.Current.LevelsDeep == 32767)
				{
					this.PushReally();
				}
			}

			// Token: 0x06001745 RID: 5957 RVA: 0x000B53C8 File Offset: 0x000B35C8
			public void SetDirty()
			{
				if (this.Current.LevelsDeep > 1)
				{
					this.PushReally();
				}
			}

			// Token: 0x06001746 RID: 5958 RVA: 0x000B53E0 File Offset: 0x000B35E0
			public void Pop()
			{
				if (this.Level > 1)
				{
					this.Current.LevelsDeep = this.Current.LevelsDeep - 1;
					if (this.Current.LevelsDeep == 0)
					{
						this.Current = this.stack[--this.stackTop];
					}
					this.Level--;
				}
			}

			// Token: 0x06001747 RID: 5959 RVA: 0x000B544C File Offset: 0x000B364C
			public void SkipGroup()
			{
				this.SkipLevel = this.Level;
			}

			// Token: 0x06001748 RID: 5960 RVA: 0x000B545C File Offset: 0x000B365C
			private void PushReally()
			{
				if (this.Level >= 4096)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				if (this.stackTop == this.stack.Length)
				{
					RtfToTextConverter.RtfState.State[] destinationArray = new RtfToTextConverter.RtfState.State[this.stackTop * 2];
					Array.Copy(this.stack, 0, destinationArray, 0, this.stackTop);
					this.stack = destinationArray;
				}
				this.stack[this.stackTop] = this.Current;
				RtfToTextConverter.RtfState.State[] array = this.stack;
				int num = this.stackTop;
				array[num].LevelsDeep = array[num].LevelsDeep - 1;
				this.stackTop++;
				this.Current.LevelsDeep = 1;
			}

			// Token: 0x04001A8E RID: 6798
			public int Level;

			// Token: 0x04001A8F RID: 6799
			public int SkipLevel;

			// Token: 0x04001A90 RID: 6800
			public RtfToTextConverter.RtfState.State Current;

			// Token: 0x04001A91 RID: 6801
			private RtfToTextConverter.RtfState.State[] stack;

			// Token: 0x04001A92 RID: 6802
			private int stackTop;

			// Token: 0x02000234 RID: 564
			public struct State
			{
				// Token: 0x04001A93 RID: 6803
				public short LevelsDeep;

				// Token: 0x04001A94 RID: 6804
				public RtfDestination Dest;

				// Token: 0x04001A95 RID: 6805
				public bool Invisible;
			}
		}
	}
}
