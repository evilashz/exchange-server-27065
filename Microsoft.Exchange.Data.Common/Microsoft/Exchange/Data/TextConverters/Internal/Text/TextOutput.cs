using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Text
{
	// Token: 0x02000281 RID: 641
	internal class TextOutput : IRestartable, IReusable, IFallback, IDisposable
	{
		// Token: 0x060019DA RID: 6618 RVA: 0x000CCF7C File Offset: 0x000CB17C
		public TextOutput(ConverterOutput output, bool lineWrapping, bool flowed, int wrapBeforePosition, int longestNonWrappedParagraph, ImageRenderingCallbackInternal imageRenderingCallback, bool fallbacks, bool htmlEscape, bool preserveSpace, Stream testTraceStream)
		{
			this.rfc2646 = flowed;
			this.lineWrapping = lineWrapping;
			this.wrapBeforePosition = wrapBeforePosition;
			this.longestNonWrappedParagraph = longestNonWrappedParagraph;
			if (!this.lineWrapping)
			{
				this.preserveTrailingSpace = preserveSpace;
				this.preserveTabulation = preserveSpace;
				this.preserveNbsp = preserveSpace;
			}
			this.output = output;
			this.fallbacks = fallbacks;
			this.htmlEscape = htmlEscape;
			this.imageRenderingCallback = imageRenderingCallback;
			this.wrapBuffer = new char[(this.longestNonWrappedParagraph + 1) * 5];
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x000CD008 File Offset: 0x000CB208
		public bool OutputCodePageSameAsInput
		{
			get
			{
				return this.output is ConverterEncodingOutput && (this.output as ConverterEncodingOutput).CodePageSameAsInput;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x000CD029 File Offset: 0x000CB229
		public Encoding OutputEncoding
		{
			set
			{
				if (this.output is ConverterEncodingOutput)
				{
					(this.output as ConverterEncodingOutput).Encoding = value;
					return;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x000CD04F File Offset: 0x000CB24F
		public bool LineEmpty
		{
			get
			{
				return this.lineLength == 0 && this.tailSpace == 0;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x000CD064 File Offset: 0x000CB264
		public bool ImageRenderingCallbackDefined
		{
			get
			{
				return this.imageRenderingCallback != null;
			}
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000CD072 File Offset: 0x000CB272
		public void OpenDocument()
		{
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x000CD074 File Offset: 0x000CB274
		public void CloseDocument()
		{
			if (!this.anyNewlines)
			{
				this.output.Write("\r\n");
			}
			this.endParagraph = false;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000CD095 File Offset: 0x000CB295
		public void SetQuotingLevel(int quotingLevel)
		{
			this.quotingLevel = Math.Min(quotingLevel, this.wrapBeforePosition / 2);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000CD0AB File Offset: 0x000CB2AB
		public void CloseParagraph()
		{
			if (this.lineLength != 0 || this.tailSpace != 0)
			{
				this.OutputNewLine();
			}
			this.endParagraph = true;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000CD0CC File Offset: 0x000CB2CC
		public void OutputNewLine()
		{
			if (this.lineWrapping)
			{
				this.FlushLine('\n');
				if (this.signaturePossible && this.lineLength == 2 && this.tailSpace == 1)
				{
					this.output.Write(' ');
					this.lineLength++;
				}
			}
			else if (this.preserveTrailingSpace && this.tailSpace != 0)
			{
				this.FlushTailSpace();
			}
			if (!this.endParagraph)
			{
				this.output.Write("\r\n");
				this.anyNewlines = true;
				this.linePosition += 2;
			}
			this.linePosition += this.lineLength;
			this.lineLength = 0;
			this.lineLengthBeforeSoftWrap = 0;
			this.flushedLength = 0;
			this.tailSpace = 0;
			this.breakOpportunity = 0;
			this.nextBreakOpportunity = 0;
			this.wrapped = false;
			this.seenSpace = false;
			this.signaturePossible = true;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x000CD1B8 File Offset: 0x000CB3B8
		public void OutputTabulation(int count)
		{
			if (this.preserveTabulation)
			{
				while (count != 0)
				{
					this.OutputNonspace("\t", TextMapping.Unicode);
					count--;
				}
				return;
			}
			int num = (this.lineLengthBeforeSoftWrap + this.lineLength + this.tailSpace) / 8 * 8 + 8 * count;
			count = num - (this.lineLengthBeforeSoftWrap + this.lineLength + this.tailSpace);
			this.OutputSpace(count);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x000CD220 File Offset: 0x000CB420
		public void OutputSpace(int count)
		{
			if (this.lineWrapping)
			{
				if (this.breakOpportunity == 0 || this.lineLength + this.tailSpace <= this.WrapBeforePosition())
				{
					this.breakOpportunity = this.lineLength + this.tailSpace;
					if (this.lineLength + this.tailSpace < this.WrapBeforePosition() && count > 1)
					{
						this.breakOpportunity += Math.Min(this.WrapBeforePosition() - (this.lineLength + this.tailSpace), count - 1);
					}
					if (this.breakOpportunity < this.lineLength + this.tailSpace + count - 1)
					{
						this.nextBreakOpportunity = this.lineLength + this.tailSpace + count - 1;
					}
					if (this.lineLength > this.flushedLength)
					{
						this.FlushLine(' ');
					}
				}
				else
				{
					this.nextBreakOpportunity = this.lineLength + this.tailSpace + count - 1;
				}
			}
			this.tailSpace += count;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000CD31A File Offset: 0x000CB51A
		public void OutputNbsp(int count)
		{
			if (this.preserveNbsp)
			{
				while (count != 0)
				{
					this.OutputNonspace("\u00a0", TextMapping.Unicode);
					count--;
				}
				return;
			}
			this.tailSpace += count;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x000CD34C File Offset: 0x000CB54C
		public void OutputNonspace(char[] buffer, int offset, int count, TextMapping textMapping)
		{
			if (!this.lineWrapping && !this.endParagraph && textMapping == TextMapping.Unicode)
			{
				if (this.tailSpace != 0)
				{
					this.FlushTailSpace();
				}
				this.output.Write(buffer, offset, count, this.fallbacks ? this : null);
				this.lineLength += count;
				return;
			}
			this.OutputNonspaceImpl(buffer, offset, count, textMapping);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000CD3AF File Offset: 0x000CB5AF
		public void OutputNonspace(string text, TextMapping textMapping)
		{
			this.OutputNonspace(text, 0, text.Length, textMapping);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000CD3C0 File Offset: 0x000CB5C0
		public void OutputNonspace(string text, int offset, int length, TextMapping textMapping)
		{
			if (textMapping != TextMapping.Unicode)
			{
				for (int i = offset; i < length; i++)
				{
					this.MapAndOutputSymbolCharacter(text[i], textMapping);
				}
				return;
			}
			if (this.endParagraph)
			{
				this.output.Write("\r\n");
				this.linePosition += 2;
				this.anyNewlines = true;
				this.endParagraph = false;
			}
			if (this.lineWrapping)
			{
				if (length != 0)
				{
					this.WrapPrepareToAppendNonspace(length);
					if (this.breakOpportunity == 0)
					{
						this.FlushLine(text[offset]);
						this.output.Write(text, offset, length, this.fallbacks ? this : null);
						this.flushedLength += length;
					}
					else
					{
						text.CopyTo(offset, this.wrapBuffer, this.lineLength - this.flushedLength, length);
					}
					this.lineLength += length;
					if (this.lineLength > 2 || text[offset] != '-' || (length == 2 && text[offset + 1] != '-'))
					{
						this.signaturePossible = false;
						return;
					}
				}
			}
			else
			{
				if (this.tailSpace != 0)
				{
					this.FlushTailSpace();
				}
				this.output.Write(text, offset, length, this.fallbacks ? this : null);
				this.lineLength += length;
			}
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x000CD504 File Offset: 0x000CB704
		public void OutputNonspace(int ucs32Literal, TextMapping textMapping)
		{
			if (textMapping != TextMapping.Unicode)
			{
				this.MapAndOutputSymbolCharacter((char)ucs32Literal, textMapping);
				return;
			}
			if (this.endParagraph)
			{
				this.output.Write("\r\n");
				this.linePosition += 2;
				this.anyNewlines = true;
				this.endParagraph = false;
			}
			if (this.lineWrapping)
			{
				int num = Token.LiteralLength(ucs32Literal);
				this.WrapPrepareToAppendNonspace(num);
				if (this.breakOpportunity == 0)
				{
					this.FlushLine(Token.LiteralFirstChar(ucs32Literal));
					this.output.Write(ucs32Literal, this.fallbacks ? this : null);
					this.flushedLength += num;
				}
				else
				{
					this.wrapBuffer[this.lineLength - this.flushedLength] = Token.LiteralFirstChar(ucs32Literal);
					if (num != 1)
					{
						this.wrapBuffer[this.lineLength - this.flushedLength + 1] = Token.LiteralLastChar(ucs32Literal);
					}
				}
				this.lineLength += num;
				if (this.lineLength > 2 || num != 1 || (ushort)ucs32Literal != 45)
				{
					this.signaturePossible = false;
					return;
				}
			}
			else
			{
				if (this.tailSpace != 0)
				{
					this.FlushTailSpace();
				}
				this.output.Write(ucs32Literal, this.fallbacks ? this : null);
				this.lineLength += Token.LiteralLength(ucs32Literal);
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000CD643 File Offset: 0x000CB843
		public void OpenAnchor(string anchorUrl)
		{
			this.anchorUrl = anchorUrl;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000CD64C File Offset: 0x000CB84C
		public void CloseAnchor()
		{
			if (this.anchorUrl != null)
			{
				bool flag = this.tailSpace != 0;
				string text = this.anchorUrl;
				if (text.IndexOf(' ') != -1)
				{
					text = text.Replace(" ", "%20");
				}
				this.OutputNonspace("<", TextMapping.Unicode);
				this.OutputNonspace(text, TextMapping.Unicode);
				this.OutputNonspace(">", TextMapping.Unicode);
				if (flag)
				{
					this.OutputSpace(1);
				}
				this.anchorUrl = null;
			}
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x000CD6C2 File Offset: 0x000CB8C2
		public void CancelAnchor()
		{
			this.anchorUrl = null;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x000CD6CC File Offset: 0x000CB8CC
		public void OutputImage(string imageUrl, string imageAltText, int wdthPixels, int heightPixels)
		{
			if (this.imageRenderingCallback != null && this.imageRenderingCallback(imageUrl, this.RenderingPosition()))
			{
				this.OutputSpace(1);
				return;
			}
			if ((wdthPixels == 0 || wdthPixels >= 8) && (heightPixels == 0 || heightPixels >= 8))
			{
				bool flag = this.tailSpace != 0;
				this.OutputNonspace("[", TextMapping.Unicode);
				if (!string.IsNullOrEmpty(imageAltText))
				{
					int num2;
					for (int num = 0; num != imageAltText.Length; num = num2 + 1)
					{
						num2 = imageAltText.IndexOfAny(TextOutput.Whitespaces, num);
						if (num2 == -1)
						{
							this.OutputNonspace(imageAltText, num, imageAltText.Length - num, TextMapping.Unicode);
							break;
						}
						if (num2 != num)
						{
							this.OutputNonspace(imageAltText, num, num2 - num, TextMapping.Unicode);
						}
						if (imageAltText[num] == '\t')
						{
							this.OutputTabulation(1);
						}
						else
						{
							this.OutputSpace(1);
						}
					}
				}
				else if (!string.IsNullOrEmpty(imageUrl))
				{
					if (imageUrl.Contains("/") && !imageUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !imageUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
					{
						imageUrl = "X";
					}
					else if (imageUrl.IndexOf(' ') != -1)
					{
						imageUrl = imageUrl.Replace(" ", "%20");
					}
					this.OutputNonspace(imageUrl, TextMapping.Unicode);
				}
				else
				{
					this.OutputNonspace("X", TextMapping.Unicode);
				}
				this.OutputNonspace("]", TextMapping.Unicode);
				if (flag)
				{
					this.OutputSpace(1);
				}
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000CD81C File Offset: 0x000CBA1C
		public int RenderingPosition()
		{
			return this.linePosition + this.lineLength + this.tailSpace;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000CD832 File Offset: 0x000CBA32
		public void Flush()
		{
			if (this.lineWrapping)
			{
				if (this.lineLength != 0)
				{
					this.FlushLine('\r');
					this.OutputNewLine();
				}
			}
			else if (this.lineLength != 0)
			{
				this.OutputNewLine();
			}
			this.output.Flush();
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000CD86D File Offset: 0x000CBA6D
		byte[] IFallback.GetUnsafeAsciiMap(out byte unsafeAsciiMask)
		{
			if (this.htmlEscape)
			{
				unsafeAsciiMask = 1;
				return HtmlSupport.UnsafeAsciiMap;
			}
			unsafeAsciiMask = 0;
			return null;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000CD884 File Offset: 0x000CBA84
		bool IFallback.HasUnsafeUnicode()
		{
			return this.htmlEscape;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000CD88C File Offset: 0x000CBA8C
		bool IFallback.TreatNonAsciiAsUnsafe(string charset)
		{
			return false;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000CD88F File Offset: 0x000CBA8F
		bool IFallback.IsUnsafeUnicode(char ch, bool isFirstChar)
		{
			return this.htmlEscape && (ch < '\ud800' || ch >= '') && ((byte)(ch & 'ÿ') == 60 || (byte)(ch >> 8 & 'ÿ') == 60);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000CD8CC File Offset: 0x000CBACC
		bool IFallback.FallBackChar(char ch, char[] outputBuffer, ref int outputBufferCount, int outputEnd)
		{
			if (this.htmlEscape)
			{
				HtmlEntityIndex htmlEntityIndex = (HtmlEntityIndex)0;
				if (ch <= '>')
				{
					if (ch == '>')
					{
						htmlEntityIndex = HtmlEntityIndex.gt;
					}
					else if (ch == '<')
					{
						htmlEntityIndex = HtmlEntityIndex.lt;
					}
					else if (ch == '&')
					{
						htmlEntityIndex = HtmlEntityIndex.amp;
					}
					else if (ch == '"')
					{
						htmlEntityIndex = HtmlEntityIndex.quot;
					}
				}
				else if ('\u00a0' <= ch && ch <= 'ÿ')
				{
					htmlEntityIndex = HtmlSupport.EntityMap[(int)(ch - '\u00a0')];
				}
				if (htmlEntityIndex != (HtmlEntityIndex)0)
				{
					string name = HtmlNameData.entities[(int)htmlEntityIndex].Name;
					if (outputEnd - outputBufferCount < name.Length + 2)
					{
						return false;
					}
					outputBuffer[outputBufferCount++] = '&';
					name.CopyTo(0, outputBuffer, outputBufferCount, name.Length);
					outputBufferCount += name.Length;
					outputBuffer[outputBufferCount++] = ';';
				}
				else
				{
					uint num = (uint)ch;
					int num2 = (num < 16U) ? 1 : ((num < 256U) ? 2 : ((num < 4096U) ? 3 : 4));
					if (outputEnd - outputBufferCount < num2 + 4)
					{
						return false;
					}
					outputBuffer[outputBufferCount++] = '&';
					outputBuffer[outputBufferCount++] = '#';
					outputBuffer[outputBufferCount++] = 'x';
					int num3 = outputBufferCount + num2;
					while (num != 0U)
					{
						uint num4 = num & 15U;
						outputBuffer[--num3] = (char)((ulong)num4 + (ulong)((num4 < 10U) ? 48L : 55L));
						num >>= 4;
					}
					outputBufferCount += num2;
					outputBuffer[outputBufferCount++] = ';';
				}
			}
			else
			{
				string substitute = TextOutput.GetSubstitute(ch);
				if (substitute != null)
				{
					if (outputEnd - outputBufferCount < substitute.Length)
					{
						return false;
					}
					substitute.CopyTo(0, outputBuffer, outputBufferCount, substitute.Length);
					outputBufferCount += substitute.Length;
				}
				else
				{
					outputBuffer[outputBufferCount++] = ch;
				}
			}
			return true;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000CDA87 File Offset: 0x000CBC87
		void IDisposable.Dispose()
		{
			if (this.output != null)
			{
				((IDisposable)this.output).Dispose();
			}
			this.output = null;
			this.wrapBuffer = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x000CDAB0 File Offset: 0x000CBCB0
		bool IRestartable.CanRestart()
		{
			return this.output is IRestartable && ((IRestartable)this.output).CanRestart();
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x000CDAD1 File Offset: 0x000CBCD1
		void IRestartable.Restart()
		{
			((IRestartable)this.output).Restart();
			this.Reinitialize();
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000CDAE9 File Offset: 0x000CBCE9
		void IRestartable.DisableRestart()
		{
			if (this.output is IRestartable)
			{
				((IRestartable)this.output).DisableRestart();
			}
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000CDB08 File Offset: 0x000CBD08
		void IReusable.Initialize(object newSourceOrDestination)
		{
			((IReusable)this.output).Initialize(newSourceOrDestination);
			this.Reinitialize();
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x000CDB24 File Offset: 0x000CBD24
		private void Reinitialize()
		{
			this.anchorUrl = null;
			this.linePosition = 0;
			this.lineLength = 0;
			this.lineLengthBeforeSoftWrap = 0;
			this.flushedLength = 0;
			this.tailSpace = 0;
			this.breakOpportunity = 0;
			this.nextBreakOpportunity = 0;
			this.quotingLevel = 0;
			this.seenSpace = false;
			this.wrapped = false;
			this.signaturePossible = true;
			this.anyNewlines = false;
			this.endParagraph = false;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000CDB94 File Offset: 0x000CBD94
		private void OutputNonspaceImpl(char[] buffer, int offset, int count, TextMapping textMapping)
		{
			if (count != 0)
			{
				if (textMapping != TextMapping.Unicode)
				{
					for (int i = 0; i < count; i++)
					{
						this.MapAndOutputSymbolCharacter(buffer[offset++], textMapping);
					}
					return;
				}
				if (this.endParagraph)
				{
					this.output.Write("\r\n");
					this.linePosition += 2;
					this.anyNewlines = true;
					this.endParagraph = false;
				}
				if (this.lineWrapping)
				{
					this.WrapPrepareToAppendNonspace(count);
					if (this.breakOpportunity == 0)
					{
						this.FlushLine(buffer[offset]);
						this.output.Write(buffer, offset, count, this.fallbacks ? this : null);
						this.flushedLength += count;
					}
					else
					{
						Buffer.BlockCopy(buffer, offset * 2, this.wrapBuffer, (this.lineLength - this.flushedLength) * 2, count * 2);
					}
					this.lineLength += count;
					if (this.lineLength > 2 || buffer[offset] != '-' || (count == 2 && buffer[offset + 1] != '-'))
					{
						this.signaturePossible = false;
						return;
					}
				}
				else
				{
					if (this.tailSpace != 0)
					{
						this.FlushTailSpace();
					}
					this.output.Write(buffer, offset, count, this.fallbacks ? this : null);
					this.lineLength += count;
				}
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000CDCD3 File Offset: 0x000CBED3
		private int WrapBeforePosition()
		{
			return this.wrapBeforePosition - (this.rfc2646 ? (this.quotingLevel + 1) : 0);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x000CDCEF File Offset: 0x000CBEEF
		private int LongestNonWrappedParagraph()
		{
			return this.longestNonWrappedParagraph - (this.rfc2646 ? (this.quotingLevel + 1) : 0);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x000CDD0C File Offset: 0x000CBF0C
		private void WrapPrepareToAppendNonspace(int count)
		{
			while (this.breakOpportunity != 0 && this.lineLength + this.tailSpace + count > (this.wrapped ? this.WrapBeforePosition() : this.LongestNonWrappedParagraph()))
			{
				if (this.flushedLength == 0 && this.rfc2646)
				{
					for (int i = 0; i < this.quotingLevel; i++)
					{
						this.output.Write('>');
					}
					if (this.quotingLevel != 0 || this.wrapBuffer[0] == '>' || this.wrapBuffer[0] == ' ')
					{
						this.output.Write(' ');
					}
				}
				if (this.breakOpportunity >= this.lineLength)
				{
					do
					{
						if (this.lineLength - this.flushedLength == this.wrapBuffer.Length)
						{
							this.output.Write(this.wrapBuffer, 0, this.wrapBuffer.Length, this.fallbacks ? this : null);
							this.flushedLength += this.wrapBuffer.Length;
						}
						this.wrapBuffer[this.lineLength - this.flushedLength] = ' ';
						this.lineLength++;
						this.tailSpace--;
					}
					while (this.lineLength != this.breakOpportunity + 1);
				}
				this.output.Write(this.wrapBuffer, 0, this.breakOpportunity + 1 - this.flushedLength, this.fallbacks ? this : null);
				this.anyNewlines = true;
				this.output.Write("\r\n");
				this.wrapped = true;
				this.lineLengthBeforeSoftWrap += this.breakOpportunity + 1;
				this.linePosition += this.breakOpportunity + 1 + 2;
				this.lineLength -= this.breakOpportunity + 1;
				int num = this.flushedLength;
				this.flushedLength = 0;
				if (this.lineLength != 0)
				{
					if (this.nextBreakOpportunity == 0 || this.nextBreakOpportunity - (this.breakOpportunity + 1) >= this.lineLength || this.nextBreakOpportunity - (this.breakOpportunity + 1) == 0)
					{
						if (this.rfc2646)
						{
							for (int j = 0; j < this.quotingLevel; j++)
							{
								this.output.Write('>');
							}
							if (this.quotingLevel != 0 || this.wrapBuffer[this.breakOpportunity + 1 - num] == '>' || this.wrapBuffer[this.breakOpportunity + 1 - num] == ' ')
							{
								this.output.Write(' ');
							}
						}
						this.output.Write(this.wrapBuffer, this.breakOpportunity + 1 - num, this.lineLength, this.fallbacks ? this : null);
						this.flushedLength = this.lineLength;
					}
					else
					{
						Buffer.BlockCopy(this.wrapBuffer, (this.breakOpportunity + 1 - num) * 2, this.wrapBuffer, 0, this.lineLength * 2);
					}
				}
				if (this.nextBreakOpportunity != 0)
				{
					this.breakOpportunity = this.nextBreakOpportunity - (this.breakOpportunity + 1);
					if (this.breakOpportunity > this.WrapBeforePosition())
					{
						if (this.lineLength < this.WrapBeforePosition())
						{
							this.nextBreakOpportunity = this.breakOpportunity;
							this.breakOpportunity = this.WrapBeforePosition();
						}
						else if (this.breakOpportunity > this.lineLength)
						{
							this.nextBreakOpportunity = this.breakOpportunity;
							this.breakOpportunity = this.lineLength;
						}
						else
						{
							this.nextBreakOpportunity = 0;
						}
					}
					else
					{
						this.nextBreakOpportunity = 0;
					}
				}
				else
				{
					this.breakOpportunity = 0;
				}
			}
			if (this.tailSpace != 0)
			{
				if (this.breakOpportunity == 0)
				{
					if (this.flushedLength == 0 && this.rfc2646)
					{
						for (int k = 0; k < this.quotingLevel; k++)
						{
							this.output.Write('>');
						}
						this.output.Write(' ');
					}
					this.flushedLength += this.tailSpace;
					this.FlushTailSpace();
					return;
				}
				do
				{
					this.wrapBuffer[this.lineLength - this.flushedLength] = ' ';
					this.lineLength++;
					this.tailSpace--;
				}
				while (this.tailSpace != 0);
			}
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x000CE12C File Offset: 0x000CC32C
		private void FlushLine(char nextChar)
		{
			if (this.flushedLength == 0 && this.rfc2646)
			{
				for (int i = 0; i < this.quotingLevel; i++)
				{
					this.output.Write('>');
				}
				char c = (this.lineLength != 0) ? this.wrapBuffer[0] : nextChar;
				if (this.quotingLevel != 0 || c == '>' || c == ' ')
				{
					this.output.Write(' ');
				}
			}
			if (this.lineLength != this.flushedLength)
			{
				this.output.Write(this.wrapBuffer, 0, this.lineLength - this.flushedLength, this.fallbacks ? this : null);
				this.flushedLength = this.lineLength;
			}
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000CE1DF File Offset: 0x000CC3DF
		private void FlushTailSpace()
		{
			this.lineLength += this.tailSpace;
			do
			{
				this.output.Write(' ');
				this.tailSpace--;
			}
			while (this.tailSpace != 0);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x000CE218 File Offset: 0x000CC418
		private void MapAndOutputSymbolCharacter(char ch, TextMapping textMapping)
		{
			if (ch == ' ' || ch == '\t' || ch == '\r' || ch == '\n')
			{
				this.OutputNonspace((int)ch, TextMapping.Unicode);
				return;
			}
			string text = null;
			if (textMapping == TextMapping.Wingdings)
			{
				if (ch <= 'Ø')
				{
					switch (ch)
					{
					case 'J':
						text = "☺";
						break;
					case 'K':
						text = ":|";
						break;
					case 'L':
						text = "☹";
						break;
					default:
						if (ch == 'Ø')
						{
							text = ">";
						}
						break;
					}
				}
				else
				{
					switch (ch)
					{
					case 'ß':
						text = "<--";
						break;
					case 'à':
						text = "-->";
						break;
					default:
						switch (ch)
						{
						case 'ç':
							text = "<==";
							break;
						case 'è':
							text = "==>";
							break;
						default:
							switch (ch)
							{
							case 'ï':
								text = "<=";
								break;
							case 'ð':
								text = "=>";
								break;
							case 'ó':
								text = "<=>";
								break;
							}
							break;
						}
						break;
					}
				}
			}
			if (text == null)
			{
				text = "•";
			}
			this.OutputNonspace(text, TextMapping.Unicode);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x000CE328 File Offset: 0x000CC528
		private static string GetSubstitute(char ch)
		{
			return AsciiEncoderFallback.GetCharacterFallback(ch);
		}

		// Token: 0x04001F27 RID: 7975
		protected ConverterOutput output;

		// Token: 0x04001F28 RID: 7976
		protected bool lineWrapping;

		// Token: 0x04001F29 RID: 7977
		protected bool rfc2646;

		// Token: 0x04001F2A RID: 7978
		protected int longestNonWrappedParagraph;

		// Token: 0x04001F2B RID: 7979
		protected int wrapBeforePosition;

		// Token: 0x04001F2C RID: 7980
		protected bool preserveTrailingSpace;

		// Token: 0x04001F2D RID: 7981
		protected bool preserveTabulation;

		// Token: 0x04001F2E RID: 7982
		protected bool preserveNbsp;

		// Token: 0x04001F2F RID: 7983
		protected int lineLength;

		// Token: 0x04001F30 RID: 7984
		protected int lineLengthBeforeSoftWrap;

		// Token: 0x04001F31 RID: 7985
		protected int flushedLength;

		// Token: 0x04001F32 RID: 7986
		protected int tailSpace;

		// Token: 0x04001F33 RID: 7987
		protected int breakOpportunity;

		// Token: 0x04001F34 RID: 7988
		protected int nextBreakOpportunity;

		// Token: 0x04001F35 RID: 7989
		protected int quotingLevel;

		// Token: 0x04001F36 RID: 7990
		protected bool seenSpace;

		// Token: 0x04001F37 RID: 7991
		protected bool wrapped;

		// Token: 0x04001F38 RID: 7992
		protected char[] wrapBuffer;

		// Token: 0x04001F39 RID: 7993
		protected bool signaturePossible = true;

		// Token: 0x04001F3A RID: 7994
		protected bool anyNewlines;

		// Token: 0x04001F3B RID: 7995
		protected bool endParagraph;

		// Token: 0x04001F3C RID: 7996
		private static readonly char[] Whitespaces = new char[]
		{
			' ',
			'\t',
			'\r',
			'\n',
			'\f'
		};

		// Token: 0x04001F3D RID: 7997
		private bool fallbacks;

		// Token: 0x04001F3E RID: 7998
		private bool htmlEscape;

		// Token: 0x04001F3F RID: 7999
		private string anchorUrl;

		// Token: 0x04001F40 RID: 8000
		private int linePosition;

		// Token: 0x04001F41 RID: 8001
		private ImageRenderingCallbackInternal imageRenderingCallback;
	}
}
