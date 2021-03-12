using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001CB RID: 459
	internal class HtmlFormatConverterWithEncapsulation : HtmlFormatConverter
	{
		// Token: 0x060013EA RID: 5098 RVA: 0x0008DC74 File Offset: 0x0008BE74
		public HtmlFormatConverterWithEncapsulation(HtmlNormalizingParser parser, FormatOutput output, bool testTreatNbspAsBreakable, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum, Stream formatConverterTraceStream, IProgressMonitor progressMonitor) : this(parser, output, false, testTreatNbspAsBreakable, traceStream, traceShowTokenNum, traceStopOnTokenNum, formatConverterTraceStream, progressMonitor)
		{
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0008DC98 File Offset: 0x0008BE98
		public HtmlFormatConverterWithEncapsulation(HtmlNormalizingParser parser, FormatOutput output, bool encapsulateMarkup, bool testTreatNbspAsBreakable, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum, Stream formatConverterTraceStream, IProgressMonitor progressMonitor) : base(parser, output, testTreatNbspAsBreakable, traceStream, traceShowTokenNum, traceStopOnTokenNum, formatConverterTraceStream, progressMonitor)
		{
			this.encapsulateMarkup = encapsulateMarkup;
			if (this.output != null && this.encapsulateMarkup)
			{
				this.output.Initialize(this.Store, SourceFormat.HtmlEncapsulateMarkup, "converted from html");
			}
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0008DCE8 File Offset: 0x0008BEE8
		protected override void Process(HtmlTokenId tokenId)
		{
			this.token = this.parser.Token;
			switch (tokenId)
			{
			case HtmlTokenId.EndOfFile:
				base.CloseAllContainersAndSetEOF();
				break;
			case HtmlTokenId.Text:
				if (this.insideStyle)
				{
					this.OutputEncapsulatedMarkup();
					this.token.Text.WriteTo(this.cssParserInput);
					return;
				}
				if (this.insideComment)
				{
					this.OutputEncapsulatedMarkup();
					return;
				}
				if (this.insidePre)
				{
					base.ProcessPreformatedText();
					return;
				}
				base.ProcessText();
				return;
			case HtmlTokenId.EncodingChange:
				if (this.output != null && this.output.OutputCodePageSameAsInput)
				{
					this.output.OutputEncoding = this.token.TokenEncoding;
					return;
				}
				break;
			case HtmlTokenId.Tag:
				this.OutputEncapsulatedMarkup();
				if (this.token.TagIndex <= HtmlTagIndex.Unknown)
				{
					if (this.insideStyle && this.token.TagIndex == HtmlTagIndex._COMMENT)
					{
						this.token.Text.WriteTo(this.cssParserInput);
						return;
					}
				}
				else
				{
					HtmlDtd.TagDefinition tagDefinition = HtmlFormatConverter.GetTagDefinition(this.token.TagIndex);
					if (!this.token.IsEndTag)
					{
						if (this.token.IsTagBegin)
						{
							base.PushElement(tagDefinition, this.token.IsEmptyScope);
						}
						base.ProcessStartTagAttributes(tagDefinition);
						return;
					}
					if (this.token.IsTagEnd)
					{
						base.PopElement(this.BuildStackTop - 1 - this.temporarilyClosedLevels, this.token.Argument != 1);
						return;
					}
				}
				break;
			case HtmlTokenId.Restart:
				break;
			case HtmlTokenId.OverlappedClose:
				this.temporarilyClosedLevels = this.token.Argument;
				return;
			case HtmlTokenId.OverlappedReopen:
				this.temporarilyClosedLevels = 0;
				return;
			default:
				return;
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0008DE90 File Offset: 0x0008C090
		private void OutputEncapsulatedMarkup()
		{
			if (this.encapsulateMarkup)
			{
				if (this.markupSink == null)
				{
					this.markupSink = new HtmlFormatConverterWithEncapsulation.MarkupSink(this);
				}
				if (this.token.IsEndTag && this.token.TagIndex > HtmlTagIndex.Unknown)
				{
					char[] endTagText = this.GetEndTagText(this.token);
					this.markupSink.Write(endTagText, 0, endTagText.Length);
					return;
				}
				this.token.Text.WriteTo(this.markupSink);
			}
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0008DF0C File Offset: 0x0008C10C
		private char[] GetEndTagText(HtmlToken htmlToken)
		{
			char[] array = this.token.NameIndex.ToString().ToCharArray();
			int num = array.Length;
			char[] array2 = new char[num + 3];
			array2[0] = '<';
			array2[1] = '/';
			array.CopyTo(array2, 2);
			array2[array2.Length - 1] = '>';
			return array2;
		}

		// Token: 0x040013C8 RID: 5064
		private readonly bool encapsulateMarkup;

		// Token: 0x040013C9 RID: 5065
		private HtmlFormatConverterWithEncapsulation.MarkupSink markupSink;

		// Token: 0x020001CC RID: 460
		private class MarkupSink : ITextSink
		{
			// Token: 0x060013EF RID: 5103 RVA: 0x0008DF5D File Offset: 0x0008C15D
			public MarkupSink(HtmlFormatConverter builder)
			{
				this.builder = builder;
				this.literalBuffer = new char[2];
			}

			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0008DF78 File Offset: 0x0008C178
			public bool IsEnough
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060013F1 RID: 5105 RVA: 0x0008DF7B File Offset: 0x0008C17B
			public void Write(char[] buffer, int offset, int count)
			{
				this.builder.AddMarkupText(buffer, offset, count);
			}

			// Token: 0x060013F2 RID: 5106 RVA: 0x0008DF8C File Offset: 0x0008C18C
			public void Write(int ucs32Char)
			{
				int num = Token.LiteralLength(ucs32Char);
				this.literalBuffer[0] = Token.LiteralFirstChar(ucs32Char);
				if (num > 1)
				{
					this.literalBuffer[1] = Token.LiteralLastChar(ucs32Char);
				}
				this.builder.AddMarkupText(this.literalBuffer, 0, num);
			}

			// Token: 0x040013CA RID: 5066
			private HtmlFormatConverter builder;

			// Token: 0x040013CB RID: 5067
			private char[] literalBuffer;
		}
	}
}
