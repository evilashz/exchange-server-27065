using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001C5 RID: 453
	internal class HtmlToTextConverter : IProducerConsumer, IRestartable, IReusable, IDisposable
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x0008A1D8 File Offset: 0x000883D8
		public HtmlToTextConverter(IHtmlParser parser, TextOutput output, Injection injection, bool convertFragment, bool preformattedText, bool testTreatNbspAsBreakable, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum, bool shouldUseNarrowGapForPTagHtmlToTextConversion, bool outputAnchorLinks, bool outputImageLinks)
		{
			this.normalizedInput = (parser is HtmlNormalizingParser);
			this.treatNbspAsBreakable = testTreatNbspAsBreakable;
			this.convertFragment = convertFragment;
			this.output = output;
			this.parser = parser;
			this.parser.SetRestartConsumer(this);
			this.injection = injection;
			if (!convertFragment)
			{
				this.output.OpenDocument();
				if (this.injection != null && this.injection.HaveHead)
				{
					this.injection.Inject(true, this.output);
				}
			}
			else
			{
				this.insidePre = preformattedText;
			}
			this.shouldUseNarrowGapForPTagHtmlToTextConversion = shouldUseNarrowGapForPTagHtmlToTextConversion;
			this.outputAnchorLinks = outputImageLinks;
			this.outputImageLinks = outputImageLinks;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0008A28C File Offset: 0x0008848C
		private void Reinitialize()
		{
			this.endOfFile = false;
			this.normalizerContext.HasSpace = false;
			this.normalizerContext.EatSpace = false;
			this.normalizerContext.OneNL = false;
			this.normalizerContext.LastCh = '\0';
			this.lineStarted = false;
			this.wideGap = false;
			this.nextParagraphCloseWideGap = true;
			this.afterFirstParagraph = false;
			this.ignoreNextP = false;
			this.insideComment = false;
			this.insidePre = false;
			this.insideAnchor = false;
			if (this.urlCompareSink != null)
			{
				this.urlCompareSink.Reset();
			}
			this.listLevel = 0;
			this.listIndex = 0;
			this.listOrdered = false;
			if (!this.convertFragment)
			{
				this.output.OpenDocument();
				if (this.injection != null)
				{
					this.injection.Reset();
					if (this.injection.HaveHead)
					{
						this.injection.Inject(true, this.output);
					}
				}
			}
			this.textMapping = TextMapping.Unicode;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0008A37C File Offset: 0x0008857C
		public void Run()
		{
			if (!this.endOfFile)
			{
				HtmlTokenId htmlTokenId = this.parser.Parse();
				if (htmlTokenId != HtmlTokenId.None)
				{
					this.Process(htmlTokenId);
				}
			}
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0008A3A7 File Offset: 0x000885A7
		public bool Flush()
		{
			if (!this.endOfFile)
			{
				this.Run();
			}
			return this.endOfFile;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0008A3C0 File Offset: 0x000885C0
		private void Process(HtmlTokenId tokenId)
		{
			this.token = this.parser.Token;
			switch (tokenId)
			{
			case HtmlTokenId.EndOfFile:
				if (this.lineStarted)
				{
					this.output.OutputNewLine();
					this.lineStarted = false;
				}
				if (!this.convertFragment)
				{
					if (this.injection != null && this.injection.HaveHead)
					{
						if (this.wideGap)
						{
							this.output.OutputNewLine();
							this.wideGap = false;
						}
						this.injection.Inject(false, this.output);
					}
					this.output.CloseDocument();
					this.output.Flush();
				}
				this.endOfFile = true;
				break;
			case HtmlTokenId.Text:
				if (!this.insideComment)
				{
					if (this.insideAnchor && this.urlCompareSink.IsActive)
					{
						this.token.Text.WriteTo(this.urlCompareSink);
					}
					if (this.insidePre)
					{
						this.ProcessPreformatedText();
						return;
					}
					if (this.normalizedInput)
					{
						this.ProcessText();
						return;
					}
					this.NormalizeProcessText();
					return;
				}
				break;
			case HtmlTokenId.EncodingChange:
				if (this.output.OutputCodePageSameAsInput)
				{
					this.output.OutputEncoding = this.token.TokenEncoding;
					return;
				}
				break;
			case HtmlTokenId.Tag:
			{
				if (this.token.TagIndex <= HtmlTagIndex.Unknown)
				{
					return;
				}
				HtmlDtd.TagDefinition tagDefinition = HtmlToTextConverter.GetTagDefinition(this.token.TagIndex);
				if (this.normalizedInput)
				{
					if (!this.token.IsEndTag)
					{
						if (this.token.IsTagBegin)
						{
							this.PushElement(tagDefinition);
						}
						this.ProcessStartTagAttributes(tagDefinition);
						return;
					}
					if (this.token.IsTagBegin)
					{
						this.PopElement(tagDefinition);
						return;
					}
				}
				else
				{
					if (!this.token.IsEndTag)
					{
						if (this.token.IsTagBegin)
						{
							this.LFillTagB(tagDefinition);
							this.PushElement(tagDefinition);
							this.RFillTagB(tagDefinition);
						}
						this.ProcessStartTagAttributes(tagDefinition);
						return;
					}
					if (this.token.IsTagBegin)
					{
						this.LFillTagE(tagDefinition);
						this.PopElement(tagDefinition);
						this.RFillTagE(tagDefinition);
						return;
					}
				}
				break;
			}
			case HtmlTokenId.Restart:
			case HtmlTokenId.OverlappedClose:
			case HtmlTokenId.OverlappedReopen:
				break;
			default:
				return;
			}
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0008A5D0 File Offset: 0x000887D0
		private void PushElement(HtmlDtd.TagDefinition tagDef)
		{
			HtmlTagIndex tagIndex = tagDef.TagIndex;
			if (tagIndex <= HtmlTagIndex.Listing)
			{
				if (tagIndex <= HtmlTagIndex.DT)
				{
					if (tagIndex != HtmlTagIndex.A)
					{
						if (tagIndex == HtmlTagIndex.BR)
						{
							goto IL_193;
						}
						switch (tagIndex)
						{
						case HtmlTagIndex.Comment:
							break;
						case HtmlTagIndex.DD:
							if (this.lineStarted)
							{
								this.EndLine();
								goto IL_2FF;
							}
							goto IL_2FF;
						case HtmlTagIndex.Del:
						case HtmlTagIndex.Dfn:
						case HtmlTagIndex.Div:
							goto IL_2F0;
						case HtmlTagIndex.Dir:
							goto IL_1BC;
						case HtmlTagIndex.DL:
							this.EndParagraph(true);
							goto IL_2FF;
						case HtmlTagIndex.DT:
							if (this.lineStarted)
							{
								this.EndLine();
								goto IL_2FF;
							}
							goto IL_2FF;
						default:
							goto IL_2F0;
						}
					}
					else
					{
						if (this.insideAnchor)
						{
							this.EndAnchor();
							goto IL_2FF;
						}
						goto IL_2FF;
					}
				}
				else if (tagIndex <= HtmlTagIndex.HR)
				{
					if (tagIndex == HtmlTagIndex.Font)
					{
						goto IL_2FF;
					}
					if (tagIndex != HtmlTagIndex.HR)
					{
						goto IL_2F0;
					}
					this.EndParagraph(false);
					this.OutputText("________________________________");
					this.EndParagraph(false);
					goto IL_2FF;
				}
				else
				{
					switch (tagIndex)
					{
					case HtmlTagIndex.Image:
					case HtmlTagIndex.Img:
						goto IL_2FF;
					default:
						switch (tagIndex)
						{
						case HtmlTagIndex.LI:
						{
							this.EndParagraph(false);
							this.OutputText("  ");
							for (int i = 0; i < this.listLevel - 1; i++)
							{
								this.OutputText("   ");
							}
							if (this.listLevel > 1 || !this.listOrdered)
							{
								this.OutputText("*");
								this.output.OutputSpace(3);
								goto IL_2FF;
							}
							string text = this.listIndex.ToString();
							this.OutputText(text);
							this.OutputText(".");
							this.output.OutputSpace((text.Length == 1) ? 2 : 1);
							this.listIndex++;
							goto IL_2FF;
						}
						case HtmlTagIndex.Link:
							goto IL_2F0;
						case HtmlTagIndex.Listing:
							goto IL_2E0;
						default:
							goto IL_2F0;
						}
						break;
					}
				}
			}
			else if (tagIndex <= HtmlTagIndex.Style)
			{
				if (tagIndex <= HtmlTagIndex.Script)
				{
					switch (tagIndex)
					{
					case HtmlTagIndex.Menu:
					case HtmlTagIndex.OL:
						goto IL_1BC;
					case HtmlTagIndex.Meta:
					case HtmlTagIndex.NextId:
					case HtmlTagIndex.NoBR:
					case HtmlTagIndex.NoScript:
					case HtmlTagIndex.Object:
					case HtmlTagIndex.OptGroup:
					case HtmlTagIndex.Param:
						goto IL_2F0;
					case HtmlTagIndex.NoEmbed:
					case HtmlTagIndex.NoFrames:
						break;
					case HtmlTagIndex.Option:
						goto IL_193;
					case HtmlTagIndex.P:
						if (!this.ignoreNextP)
						{
							this.EndParagraph(true);
						}
						this.nextParagraphCloseWideGap = true;
						goto IL_2FF;
					case HtmlTagIndex.PlainText:
					case HtmlTagIndex.Pre:
						goto IL_2E0;
					default:
						if (tagIndex != HtmlTagIndex.Script)
						{
							goto IL_2F0;
						}
						break;
					}
				}
				else
				{
					if (tagIndex == HtmlTagIndex.Span)
					{
						goto IL_2FF;
					}
					if (tagIndex != HtmlTagIndex.Style)
					{
						goto IL_2F0;
					}
				}
			}
			else if (tagIndex <= HtmlTagIndex.Title)
			{
				if (tagIndex != HtmlTagIndex.TD)
				{
					switch (tagIndex)
					{
					case HtmlTagIndex.TH:
						break;
					case HtmlTagIndex.Thead:
						goto IL_2F0;
					case HtmlTagIndex.Title:
						goto IL_13A;
					default:
						goto IL_2F0;
					}
				}
				if (this.lineStarted)
				{
					this.output.OutputTabulation(1);
					goto IL_2FF;
				}
				goto IL_2FF;
			}
			else
			{
				if (tagIndex == HtmlTagIndex.UL)
				{
					goto IL_1BC;
				}
				if (tagIndex != HtmlTagIndex.Xmp)
				{
					goto IL_2F0;
				}
				goto IL_2E0;
			}
			IL_13A:
			this.insideComment = true;
			goto IL_2FF;
			IL_193:
			this.EndLine();
			goto IL_2FF;
			IL_1BC:
			this.EndParagraph(this.listLevel == 0);
			if (this.listLevel < 10)
			{
				this.listLevel++;
				if (this.listLevel == 1)
				{
					this.listIndex = 1;
					this.listOrdered = (this.token.TagIndex == HtmlTagIndex.OL);
				}
			}
			this.nextParagraphCloseWideGap = false;
			goto IL_2FF;
			IL_2E0:
			this.EndParagraph(true);
			this.insidePre = true;
			goto IL_2FF;
			IL_2F0:
			if (tagDef.BlockElement)
			{
				this.EndParagraph(false);
			}
			IL_2FF:
			this.ignoreNextP = false;
			if (tagDef.TagIndex == HtmlTagIndex.LI)
			{
				this.ignoreNextP = true;
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0008A8F4 File Offset: 0x00088AF4
		private void ProcessStartTagAttributes(HtmlDtd.TagDefinition tagDef)
		{
			HtmlTagIndex tagIndex = tagDef.TagIndex;
			if (tagIndex <= HtmlTagIndex.Font)
			{
				if (tagIndex != HtmlTagIndex.A)
				{
					if (tagIndex != HtmlTagIndex.Font)
					{
						return;
					}
					foreach (HtmlAttribute attr in this.token.Attributes)
					{
						if (attr.NameIndex == HtmlNameIndex.Face)
						{
							this.scratch.Reset();
							this.scratch.AppendHtmlAttributeValue(attr, 4096);
							RecognizeInterestingFontName recognizeInterestingFontName = default(RecognizeInterestingFontName);
							int num = 0;
							while (num < this.scratch.Length && !recognizeInterestingFontName.IsRejected)
							{
								recognizeInterestingFontName.AddCharacter(this.scratch.Buffer[num]);
								num++;
							}
							this.textMapping = recognizeInterestingFontName.TextMapping;
							return;
						}
					}
					return;
				}
				else if (this.outputAnchorLinks)
				{
					foreach (HtmlAttribute attr2 in this.token.Attributes)
					{
						if (attr2.NameIndex == HtmlNameIndex.Href)
						{
							if (attr2.IsAttrBegin)
							{
								this.urlScratch.Reset();
							}
							this.urlScratch.AppendHtmlAttributeValue(attr2, 4096);
							break;
						}
					}
					if (this.token.IsTagEnd)
					{
						BufferString bufferString = this.urlScratch.BufferString;
						bufferString.TrimWhitespace();
						if (bufferString.Length != 0 && bufferString[0] != '#' && bufferString[0] != '?' && bufferString[0] != ';')
						{
							if (!this.lineStarted)
							{
								this.StartParagraphOrLine();
							}
							string text = bufferString.ToString();
							if (text.IndexOf(' ') != -1)
							{
								text = text.Replace(" ", "%20");
							}
							this.output.OpenAnchor(text);
							this.insideAnchor = true;
							if (this.urlCompareSink == null)
							{
								this.urlCompareSink = new UrlCompareSink();
							}
							this.urlCompareSink.Initialize(text);
						}
						this.urlScratch.Reset();
						return;
					}
				}
			}
			else
			{
				switch (tagIndex)
				{
				case HtmlTagIndex.Image:
				case HtmlTagIndex.Img:
					if (this.outputImageLinks)
					{
						foreach (HtmlAttribute attr3 in this.token.Attributes)
						{
							if (attr3.NameIndex == HtmlNameIndex.Src)
							{
								if (attr3.IsAttrBegin)
								{
									this.urlScratch.Reset();
								}
								this.urlScratch.AppendHtmlAttributeValue(attr3, 4096);
							}
							else if (attr3.NameIndex == HtmlNameIndex.Alt)
							{
								if (attr3.IsAttrBegin)
								{
									this.imageAltText.Reset();
								}
								this.imageAltText.AppendHtmlAttributeValue(attr3, 4096);
							}
							else if (attr3.NameIndex == HtmlNameIndex.Height)
							{
								if (!attr3.Value.IsEmpty)
								{
									PropertyValue propertyValue;
									if (attr3.Value.IsContiguous)
									{
										propertyValue = HtmlSupport.ParseNumber(attr3.Value.ContiguousBufferString, HtmlSupport.NumberParseFlags.Length);
									}
									else
									{
										this.scratch.Reset();
										this.scratch.AppendHtmlAttributeValue(attr3, 4096);
										propertyValue = HtmlSupport.ParseNumber(this.scratch.BufferString, HtmlSupport.NumberParseFlags.Length);
									}
									if (propertyValue.IsAbsRelLength)
									{
										this.imageHeightPixels = propertyValue.PixelsInteger;
										if (this.imageHeightPixels == 0)
										{
											this.imageHeightPixels = 1;
										}
									}
								}
							}
							else if (attr3.NameIndex == HtmlNameIndex.Width && !attr3.Value.IsEmpty)
							{
								PropertyValue propertyValue2;
								if (attr3.Value.IsContiguous)
								{
									propertyValue2 = HtmlSupport.ParseNumber(attr3.Value.ContiguousBufferString, HtmlSupport.NumberParseFlags.Length);
								}
								else
								{
									this.scratch.Reset();
									this.scratch.AppendHtmlAttributeValue(attr3, 4096);
									propertyValue2 = HtmlSupport.ParseNumber(this.scratch.BufferString, HtmlSupport.NumberParseFlags.Length);
								}
								if (propertyValue2.IsAbsRelLength)
								{
									this.imageWidthPixels = propertyValue2.PixelsInteger;
									if (this.imageWidthPixels == 0)
									{
										this.imageWidthPixels = 1;
									}
								}
							}
						}
						if (this.token.IsTagEnd)
						{
							string imageUrl = null;
							string text2 = null;
							BufferString bufferString2 = this.imageAltText.BufferString;
							bufferString2.TrimWhitespace();
							if (bufferString2.Length != 0)
							{
								text2 = bufferString2.ToString();
							}
							if (text2 == null || this.output.ImageRenderingCallbackDefined)
							{
								BufferString bufferString3 = this.urlScratch.BufferString;
								bufferString3.TrimWhitespace();
								if (bufferString3.Length != 0)
								{
									imageUrl = bufferString3.ToString();
								}
							}
							if (!this.lineStarted)
							{
								this.StartParagraphOrLine();
							}
							this.output.OutputImage(imageUrl, text2, this.imageWidthPixels, this.imageHeightPixels);
							this.urlScratch.Reset();
							this.imageAltText.Reset();
							this.imageHeightPixels = 0;
							this.imageWidthPixels = 0;
							return;
						}
					}
					break;
				default:
					if (tagIndex == HtmlTagIndex.P)
					{
						if (!this.shouldUseNarrowGapForPTagHtmlToTextConversion)
						{
							if (!this.token.Attributes.Find(HtmlNameIndex.Class))
							{
								break;
							}
							HtmlAttribute htmlAttribute = this.token.Attributes.Current;
							if (!htmlAttribute.Value.CaseInsensitiveCompareEqual("msonormal"))
							{
								break;
							}
						}
						this.wideGap = false;
						this.nextParagraphCloseWideGap = false;
						return;
					}
					if (tagIndex != HtmlTagIndex.Span)
					{
						return;
					}
					foreach (HtmlAttribute attr4 in this.token.Attributes)
					{
						if (attr4.NameIndex == HtmlNameIndex.Style)
						{
							this.scratch.Reset();
							this.scratch.AppendHtmlAttributeValue(attr4, 4096);
							RecognizeInterestingFontNameInInlineStyle recognizeInterestingFontNameInInlineStyle = default(RecognizeInterestingFontNameInInlineStyle);
							int num2 = 0;
							while (num2 < this.scratch.Length && !recognizeInterestingFontNameInInlineStyle.IsFinished)
							{
								recognizeInterestingFontNameInInlineStyle.AddCharacter(this.scratch.Buffer[num2]);
								num2++;
							}
							this.textMapping = recognizeInterestingFontNameInInlineStyle.TextMapping;
							return;
						}
					}
					break;
				}
			}
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0008AEF4 File Offset: 0x000890F4
		private void PopElement(HtmlDtd.TagDefinition tagDef)
		{
			HtmlTagIndex tagIndex = tagDef.TagIndex;
			if (tagIndex <= HtmlTagIndex.Listing)
			{
				if (tagIndex <= HtmlTagIndex.DT)
				{
					if (tagIndex <= HtmlTagIndex.BR)
					{
						if (tagIndex != HtmlTagIndex.A)
						{
							if (tagIndex != HtmlTagIndex.BR)
							{
								goto IL_1D6;
							}
							goto IL_173;
						}
						else
						{
							if (this.insideAnchor)
							{
								this.EndAnchor();
								goto IL_1E5;
							}
							goto IL_1E5;
						}
					}
					else
					{
						switch (tagIndex)
						{
						case HtmlTagIndex.Comment:
							break;
						case HtmlTagIndex.DD:
							goto IL_1E5;
						case HtmlTagIndex.Del:
						case HtmlTagIndex.Dfn:
							goto IL_1D6;
						case HtmlTagIndex.Dir:
							goto IL_196;
						default:
							if (tagIndex != HtmlTagIndex.DT)
							{
								goto IL_1D6;
							}
							goto IL_1E5;
						}
					}
				}
				else if (tagIndex <= HtmlTagIndex.HR)
				{
					if (tagIndex == HtmlTagIndex.Font)
					{
						goto IL_1CD;
					}
					if (tagIndex != HtmlTagIndex.HR)
					{
						goto IL_1D6;
					}
					this.EndParagraph(false);
					this.OutputText("________________________________");
					this.EndParagraph(false);
					goto IL_1E5;
				}
				else
				{
					switch (tagIndex)
					{
					case HtmlTagIndex.Image:
					case HtmlTagIndex.Img:
						goto IL_1E5;
					default:
						if (tagIndex != HtmlTagIndex.Listing)
						{
							goto IL_1D6;
						}
						goto IL_1BD;
					}
				}
			}
			else if (tagIndex <= HtmlTagIndex.Style)
			{
				if (tagIndex <= HtmlTagIndex.Script)
				{
					switch (tagIndex)
					{
					case HtmlTagIndex.Menu:
					case HtmlTagIndex.OL:
						goto IL_196;
					case HtmlTagIndex.Meta:
					case HtmlTagIndex.NextId:
					case HtmlTagIndex.NoBR:
					case HtmlTagIndex.NoScript:
					case HtmlTagIndex.Object:
					case HtmlTagIndex.OptGroup:
					case HtmlTagIndex.Param:
						goto IL_1D6;
					case HtmlTagIndex.NoEmbed:
					case HtmlTagIndex.NoFrames:
						break;
					case HtmlTagIndex.Option:
						goto IL_173;
					case HtmlTagIndex.P:
						this.EndParagraph(this.nextParagraphCloseWideGap);
						this.nextParagraphCloseWideGap = true;
						goto IL_1E5;
					case HtmlTagIndex.PlainText:
					case HtmlTagIndex.Pre:
						goto IL_1BD;
					default:
						if (tagIndex != HtmlTagIndex.Script)
						{
							goto IL_1D6;
						}
						break;
					}
				}
				else
				{
					if (tagIndex == HtmlTagIndex.Span)
					{
						goto IL_1CD;
					}
					if (tagIndex != HtmlTagIndex.Style)
					{
						goto IL_1D6;
					}
				}
			}
			else
			{
				if (tagIndex <= HtmlTagIndex.Title)
				{
					if (tagIndex != HtmlTagIndex.TD)
					{
						switch (tagIndex)
						{
						case HtmlTagIndex.TH:
							break;
						case HtmlTagIndex.Thead:
							goto IL_1D6;
						case HtmlTagIndex.Title:
							goto IL_130;
						default:
							goto IL_1D6;
						}
					}
					this.lineStarted = true;
					goto IL_1E5;
				}
				if (tagIndex == HtmlTagIndex.UL)
				{
					goto IL_196;
				}
				if (tagIndex != HtmlTagIndex.Xmp)
				{
					goto IL_1D6;
				}
				goto IL_1BD;
			}
			IL_130:
			this.insideComment = false;
			goto IL_1E5;
			IL_173:
			this.EndLine();
			goto IL_1E5;
			IL_196:
			if (this.listLevel != 0)
			{
				this.listLevel--;
			}
			this.EndParagraph(this.listLevel == 0);
			goto IL_1E5;
			IL_1BD:
			this.EndParagraph(true);
			this.insidePre = false;
			goto IL_1E5;
			IL_1CD:
			this.textMapping = TextMapping.Unicode;
			goto IL_1E5;
			IL_1D6:
			if (tagDef.BlockElement)
			{
				this.EndParagraph(false);
			}
			IL_1E5:
			this.ignoreNextP = false;
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0008B0F0 File Offset: 0x000892F0
		private void ProcessText()
		{
			if (!this.lineStarted)
			{
				this.StartParagraphOrLine();
			}
			foreach (TokenRun tokenRun in this.token.Runs)
			{
				if (tokenRun.IsTextRun)
				{
					if (tokenRun.IsAnyWhitespace)
					{
						this.output.OutputSpace(1);
					}
					else if (tokenRun.TextType == RunTextType.Nbsp)
					{
						if (this.treatNbspAsBreakable)
						{
							this.output.OutputSpace(tokenRun.Length);
						}
						else
						{
							this.output.OutputNbsp(tokenRun.Length);
						}
					}
					else if (tokenRun.IsLiteral)
					{
						this.output.OutputNonspace(tokenRun.Literal, this.textMapping);
					}
					else
					{
						this.output.OutputNonspace(tokenRun.RawBuffer, tokenRun.RawOffset, tokenRun.RawLength, this.textMapping);
					}
				}
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0008B1E8 File Offset: 0x000893E8
		private void ProcessPreformatedText()
		{
			if (!this.lineStarted)
			{
				this.StartParagraphOrLine();
			}
			foreach (TokenRun tokenRun in this.token.Runs)
			{
				if (tokenRun.IsTextRun)
				{
					if (tokenRun.IsAnyWhitespace)
					{
						RunTextType textType = tokenRun.TextType;
						if (textType != RunTextType.Space)
						{
							if (textType == RunTextType.NewLine)
							{
								this.output.OutputNewLine();
								continue;
							}
							if (textType == RunTextType.Tabulation)
							{
								this.output.OutputTabulation(tokenRun.Length);
								continue;
							}
						}
						if (this.treatNbspAsBreakable)
						{
							this.output.OutputSpace(tokenRun.Length);
						}
						else
						{
							this.output.OutputNbsp(tokenRun.Length);
						}
					}
					else if (tokenRun.TextType == RunTextType.Nbsp)
					{
						if (this.treatNbspAsBreakable)
						{
							this.output.OutputSpace(tokenRun.Length);
						}
						else
						{
							this.output.OutputNbsp(tokenRun.Length);
						}
					}
					else if (tokenRun.IsLiteral)
					{
						this.output.OutputNonspace(tokenRun.Literal, this.textMapping);
					}
					else
					{
						this.output.OutputNonspace(tokenRun.RawBuffer, tokenRun.RawOffset, tokenRun.RawLength, this.textMapping);
					}
				}
			}
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0008B34C File Offset: 0x0008954C
		private void NormalizeProcessText()
		{
			Token.RunEnumerator runs = this.token.Runs;
			runs.MoveNext(true);
			while (runs.IsValidPosition)
			{
				TokenRun run = runs.Current;
				if (run.IsAnyWhitespace)
				{
					int num = 0;
					TokenRun tokenRun2;
					do
					{
						int num2 = num;
						TokenRun tokenRun = runs.Current;
						num = num2 + ((tokenRun.TextType == RunTextType.NewLine) ? 1 : 2);
						if (!runs.MoveNext(true))
						{
							break;
						}
						tokenRun2 = runs.Current;
					}
					while (tokenRun2.TextType <= RunTextType.UnusualWhitespace);
					this.NormalizeAddSpace(num == 1);
				}
				else if (run.TextType == RunTextType.Nbsp)
				{
					this.NormalizeAddNbsp(run.Length);
					runs.MoveNext(true);
				}
				else
				{
					this.NormalizeAddNonspace(run);
					runs.MoveNext(true);
				}
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0008B414 File Offset: 0x00089614
		private void NormalizeAddNonspace(TokenRun run)
		{
			if (!this.lineStarted)
			{
				this.StartParagraphOrLine();
			}
			if (this.normalizerContext.HasSpace)
			{
				this.normalizerContext.HasSpace = false;
				if (this.normalizerContext.LastCh == '\0' || !this.normalizerContext.OneNL || !ParseSupport.TwoFarEastNonHanguelChars(this.normalizerContext.LastCh, run.FirstChar))
				{
					this.output.OutputSpace(1);
				}
			}
			if (run.IsLiteral)
			{
				this.output.OutputNonspace(run.Literal, this.textMapping);
			}
			else
			{
				this.output.OutputNonspace(run.RawBuffer, run.RawOffset, run.RawLength, this.textMapping);
			}
			this.normalizerContext.EatSpace = false;
			this.normalizerContext.LastCh = run.LastChar;
			this.normalizerContext.OneNL = false;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0008B4FC File Offset: 0x000896FC
		private void NormalizeAddNbsp(int count)
		{
			if (!this.lineStarted)
			{
				this.StartParagraphOrLine();
			}
			if (this.normalizerContext.HasSpace)
			{
				this.normalizerContext.HasSpace = false;
				this.output.OutputSpace(1);
			}
			if (this.treatNbspAsBreakable)
			{
				this.output.OutputSpace(count);
			}
			else
			{
				this.output.OutputNbsp(count);
			}
			this.normalizerContext.EatSpace = false;
			this.normalizerContext.LastCh = '\u00a0';
			this.normalizerContext.OneNL = false;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0008B588 File Offset: 0x00089788
		private void NormalizeAddSpace(bool oneNL)
		{
			if (!this.normalizerContext.EatSpace && this.afterFirstParagraph)
			{
				this.normalizerContext.HasSpace = true;
			}
			if (this.normalizerContext.LastCh != '\0')
			{
				if (oneNL && !this.normalizerContext.OneNL)
				{
					this.normalizerContext.OneNL = true;
					return;
				}
				this.normalizerContext.LastCh = '\0';
			}
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0008B5EC File Offset: 0x000897EC
		private void LFillTagB(HtmlDtd.TagDefinition tagDef)
		{
			if (!this.insidePre)
			{
				this.LFill(tagDef.Fill.LB);
			}
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0008B607 File Offset: 0x00089807
		private void RFillTagB(HtmlDtd.TagDefinition tagDef)
		{
			if (!this.insidePre)
			{
				this.RFill(tagDef.Fill.RB);
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0008B622 File Offset: 0x00089822
		private void LFillTagE(HtmlDtd.TagDefinition tagDef)
		{
			if (!this.insidePre)
			{
				this.LFill(tagDef.Fill.LE);
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0008B63D File Offset: 0x0008983D
		private void RFillTagE(HtmlDtd.TagDefinition tagDef)
		{
			if (!this.insidePre)
			{
				this.RFill(tagDef.Fill.RE);
			}
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0008B658 File Offset: 0x00089858
		private void LFill(HtmlDtd.FillCode codeLeft)
		{
			this.normalizerContext.LastCh = '\0';
			if (this.normalizerContext.HasSpace)
			{
				if (codeLeft == HtmlDtd.FillCode.PUT)
				{
					if (!this.lineStarted)
					{
						this.StartParagraphOrLine();
					}
					this.output.OutputSpace(1);
					this.normalizerContext.EatSpace = true;
				}
				this.normalizerContext.HasSpace = (codeLeft == HtmlDtd.FillCode.NUL);
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0008B6B7 File Offset: 0x000898B7
		private void RFill(HtmlDtd.FillCode code)
		{
			if (code == HtmlDtd.FillCode.EAT)
			{
				this.normalizerContext.HasSpace = false;
				this.normalizerContext.EatSpace = true;
				return;
			}
			if (code == HtmlDtd.FillCode.PUT)
			{
				this.normalizerContext.EatSpace = false;
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0008B6E6 File Offset: 0x000898E6
		private static HtmlDtd.TagDefinition GetTagDefinition(HtmlTagIndex tagIndex)
		{
			if (tagIndex == HtmlTagIndex._NULL)
			{
				return null;
			}
			return HtmlDtd.tags[(int)tagIndex];
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0008B6F4 File Offset: 0x000898F4
		private void EndAnchor()
		{
			if (!this.urlCompareSink.IsMatch)
			{
				if (!this.lineStarted)
				{
					this.StartParagraphOrLine();
				}
				this.output.CloseAnchor();
			}
			else
			{
				this.output.CancelAnchor();
			}
			this.insideAnchor = false;
			this.urlCompareSink.Reset();
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0008B746 File Offset: 0x00089946
		private void OutputText(string text)
		{
			if (!this.lineStarted)
			{
				this.StartParagraphOrLine();
			}
			this.output.OutputNonspace(text, this.textMapping);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0008B768 File Offset: 0x00089968
		private void StartParagraphOrLine()
		{
			if (this.wideGap)
			{
				if (this.afterFirstParagraph)
				{
					this.output.OutputNewLine();
				}
				this.wideGap = false;
			}
			this.lineStarted = true;
			this.afterFirstParagraph = true;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0008B79A File Offset: 0x0008999A
		private void EndLine()
		{
			this.output.OutputNewLine();
			this.lineStarted = false;
			this.wideGap = false;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0008B7B5 File Offset: 0x000899B5
		private void EndParagraph(bool wideGap)
		{
			if (this.insideAnchor)
			{
				this.EndAnchor();
			}
			if (this.lineStarted)
			{
				this.output.OutputNewLine();
				this.lineStarted = false;
			}
			this.wideGap = (this.wideGap || wideGap);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0008B7F4 File Offset: 0x000899F4
		void IDisposable.Dispose()
		{
			if (this.parser != null)
			{
				((IDisposable)this.parser).Dispose();
			}
			if (!this.convertFragment && this.output != null && this.output != null)
			{
				((IDisposable)this.output).Dispose();
			}
			if (this.token != null && this.token is IDisposable)
			{
				((IDisposable)this.token).Dispose();
			}
			if (this.injection != null)
			{
				((IDisposable)this.injection).Dispose();
			}
			this.parser = null;
			this.output = null;
			this.token = null;
			this.injection = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0008B896 File Offset: 0x00089A96
		bool IRestartable.CanRestart()
		{
			return this.convertFragment || ((IRestartable)this.output).CanRestart();
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0008B8AD File Offset: 0x00089AAD
		void IRestartable.Restart()
		{
			if (!this.convertFragment)
			{
				((IRestartable)this.output).Restart();
			}
			this.Reinitialize();
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0008B8C8 File Offset: 0x00089AC8
		void IRestartable.DisableRestart()
		{
			if (!this.convertFragment)
			{
				((IRestartable)this.output).DisableRestart();
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0008B8DD File Offset: 0x00089ADD
		void IReusable.Initialize(object newSourceOrDestination)
		{
			((IReusable)this.parser).Initialize(newSourceOrDestination);
			((IReusable)this.output).Initialize(newSourceOrDestination);
			this.Reinitialize();
			this.parser.SetRestartConsumer(this);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0008B910 File Offset: 0x00089B10
		public void Initialize(string fragment, bool preformatedText)
		{
			if (this.normalizedInput)
			{
				((HtmlNormalizingParser)this.parser).Initialize(fragment, preformatedText);
			}
			else
			{
				((HtmlParser)this.parser).Initialize(fragment, preformatedText);
			}
			if (!this.convertFragment)
			{
				((IReusable)this.output).Initialize(null);
			}
			this.Reinitialize();
		}

		// Token: 0x04001388 RID: 5000
		private readonly bool outputImageLinks;

		// Token: 0x04001389 RID: 5001
		private readonly bool outputAnchorLinks;

		// Token: 0x0400138A RID: 5002
		private readonly bool shouldUseNarrowGapForPTagHtmlToTextConversion;

		// Token: 0x0400138B RID: 5003
		private bool convertFragment;

		// Token: 0x0400138C RID: 5004
		private IHtmlParser parser;

		// Token: 0x0400138D RID: 5005
		private bool endOfFile;

		// Token: 0x0400138E RID: 5006
		private TextOutput output;

		// Token: 0x0400138F RID: 5007
		private HtmlToken token;

		// Token: 0x04001390 RID: 5008
		private bool treatNbspAsBreakable;

		// Token: 0x04001391 RID: 5009
		protected bool normalizedInput;

		// Token: 0x04001392 RID: 5010
		private HtmlToTextConverter.NormalizerContext normalizerContext;

		// Token: 0x04001393 RID: 5011
		private TextMapping textMapping;

		// Token: 0x04001394 RID: 5012
		private bool lineStarted;

		// Token: 0x04001395 RID: 5013
		private bool wideGap;

		// Token: 0x04001396 RID: 5014
		private bool nextParagraphCloseWideGap = true;

		// Token: 0x04001397 RID: 5015
		private bool afterFirstParagraph;

		// Token: 0x04001398 RID: 5016
		private bool ignoreNextP;

		// Token: 0x04001399 RID: 5017
		private int listLevel;

		// Token: 0x0400139A RID: 5018
		private int listIndex;

		// Token: 0x0400139B RID: 5019
		private bool listOrdered;

		// Token: 0x0400139C RID: 5020
		private bool insideComment;

		// Token: 0x0400139D RID: 5021
		private bool insidePre;

		// Token: 0x0400139E RID: 5022
		private bool insideAnchor;

		// Token: 0x0400139F RID: 5023
		private ScratchBuffer urlScratch;

		// Token: 0x040013A0 RID: 5024
		private int imageHeightPixels;

		// Token: 0x040013A1 RID: 5025
		private int imageWidthPixels;

		// Token: 0x040013A2 RID: 5026
		private ScratchBuffer imageAltText;

		// Token: 0x040013A3 RID: 5027
		private ScratchBuffer scratch;

		// Token: 0x040013A4 RID: 5028
		private Injection injection;

		// Token: 0x040013A5 RID: 5029
		private UrlCompareSink urlCompareSink;

		// Token: 0x020001C6 RID: 454
		private struct NormalizerContext
		{
			// Token: 0x040013A6 RID: 5030
			public char LastCh;

			// Token: 0x040013A7 RID: 5031
			public bool OneNL;

			// Token: 0x040013A8 RID: 5032
			public bool HasSpace;

			// Token: 0x040013A9 RID: 5033
			public bool EatSpace;
		}
	}
}
