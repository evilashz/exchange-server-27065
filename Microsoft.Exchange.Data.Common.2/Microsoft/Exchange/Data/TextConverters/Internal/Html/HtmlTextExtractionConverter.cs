using System;
using System.IO;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001C7 RID: 455
	internal class HtmlTextExtractionConverter : IProducerConsumer, IRestartable, IDisposable
	{
		// Token: 0x060013BE RID: 5054 RVA: 0x0008B965 File Offset: 0x00089B65
		public HtmlTextExtractionConverter(IHtmlParser parser, ConverterOutput output, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum)
		{
			this.output = output;
			this.parser = parser;
			this.parser.SetRestartConsumer(this);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0008B987 File Offset: 0x00089B87
		public bool CanRestart()
		{
			return ((IRestartable)this.output).CanRestart();
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0008B999 File Offset: 0x00089B99
		public void Restart()
		{
			((IRestartable)this.output).Restart();
			this.endOfFile = false;
			this.insideComment = false;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0008B9B9 File Offset: 0x00089BB9
		public void DisableRestart()
		{
			((IRestartable)this.output).DisableRestart();
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0008B9CC File Offset: 0x00089BCC
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

		// Token: 0x060013C3 RID: 5059 RVA: 0x0008B9F7 File Offset: 0x00089BF7
		public bool Flush()
		{
			if (!this.endOfFile)
			{
				this.Run();
			}
			return this.endOfFile;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0008BA10 File Offset: 0x00089C10
		void IDisposable.Dispose()
		{
			if (this.parser != null)
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

		// Token: 0x060013C5 RID: 5061 RVA: 0x0008BA64 File Offset: 0x00089C64
		private void Process(HtmlTokenId tokenId)
		{
			HtmlToken token = this.parser.Token;
			switch (tokenId)
			{
			case HtmlTokenId.EndOfFile:
				this.output.Write("\r\n");
				this.output.Flush();
				this.endOfFile = true;
				break;
			case HtmlTokenId.Text:
				if (!this.insideComment)
				{
					token.Text.WriteToAndCollapseWhitespace(this.output, ref this.collapseWhitespaceState);
					return;
				}
				break;
			case HtmlTokenId.EncodingChange:
			{
				ConverterEncodingOutput converterEncodingOutput = this.output as ConverterEncodingOutput;
				if (converterEncodingOutput != null && converterEncodingOutput.CodePageSameAsInput)
				{
					converterEncodingOutput.Encoding = token.TokenEncoding;
					return;
				}
				break;
			}
			case HtmlTokenId.Tag:
			{
				if (token.IsTagBegin)
				{
					switch (token.TagIndex)
					{
					case HtmlTagIndex.A:
						if (!token.IsEndTag)
						{
						}
						break;
					case HtmlTagIndex.Address:
					case HtmlTagIndex.BlockQuote:
					case HtmlTagIndex.BR:
					case HtmlTagIndex.Caption:
					case HtmlTagIndex.Center:
					case HtmlTagIndex.Col:
					case HtmlTagIndex.ColGroup:
					case HtmlTagIndex.DD:
					case HtmlTagIndex.Dir:
					case HtmlTagIndex.Div:
					case HtmlTagIndex.DL:
					case HtmlTagIndex.DT:
					case HtmlTagIndex.FieldSet:
					case HtmlTagIndex.Form:
					case HtmlTagIndex.H1:
					case HtmlTagIndex.H2:
					case HtmlTagIndex.H3:
					case HtmlTagIndex.H4:
					case HtmlTagIndex.H5:
					case HtmlTagIndex.H6:
					case HtmlTagIndex.HR:
					case HtmlTagIndex.LI:
					case HtmlTagIndex.Listing:
					case HtmlTagIndex.Map:
					case HtmlTagIndex.Marquee:
					case HtmlTagIndex.Menu:
					case HtmlTagIndex.OL:
					case HtmlTagIndex.OptGroup:
					case HtmlTagIndex.Option:
					case HtmlTagIndex.P:
					case HtmlTagIndex.PlainText:
					case HtmlTagIndex.Pre:
					case HtmlTagIndex.Select:
					case HtmlTagIndex.Table:
					case HtmlTagIndex.Tbody:
					case HtmlTagIndex.TC:
					case HtmlTagIndex.Tfoot:
					case HtmlTagIndex.Thead:
					case HtmlTagIndex.TR:
					case HtmlTagIndex.UL:
						this.collapseWhitespaceState = CollapseWhitespaceState.NewLine;
						break;
					case HtmlTagIndex.Comment:
					case HtmlTagIndex.Script:
					case HtmlTagIndex.Style:
						this.insideComment = !token.IsEndTag;
						break;
					case HtmlTagIndex.NoEmbed:
					case HtmlTagIndex.NoFrames:
						this.insideComment = !token.IsEndTag;
						break;
					case HtmlTagIndex.TD:
					case HtmlTagIndex.TH:
						if (!token.IsEndTag)
						{
							this.output.Write("\t");
						}
						break;
					}
				}
				HtmlTagIndex tagIndex = token.TagIndex;
				if (tagIndex != HtmlTagIndex.A)
				{
					if (tagIndex != HtmlTagIndex.Area)
					{
						switch (tagIndex)
						{
						case HtmlTagIndex.Image:
						case HtmlTagIndex.Img:
							if (!token.IsEndTag)
							{
								return;
							}
							break;
						default:
							return;
						}
					}
					else if (!token.IsEndTag)
					{
						return;
					}
				}
				else if (!token.IsEndTag)
				{
					return;
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

		// Token: 0x040013AA RID: 5034
		private const bool OutputAnchorLinks = true;

		// Token: 0x040013AB RID: 5035
		private IHtmlParser parser;

		// Token: 0x040013AC RID: 5036
		private bool endOfFile;

		// Token: 0x040013AD RID: 5037
		private ConverterOutput output;

		// Token: 0x040013AE RID: 5038
		private bool insideComment;

		// Token: 0x040013AF RID: 5039
		private CollapseWhitespaceState collapseWhitespaceState;
	}
}
