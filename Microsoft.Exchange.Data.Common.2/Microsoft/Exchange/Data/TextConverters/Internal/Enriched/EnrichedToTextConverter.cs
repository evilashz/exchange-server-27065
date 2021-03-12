using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Enriched
{
	// Token: 0x020001C4 RID: 452
	internal class EnrichedToTextConverter : IProducerConsumer, IDisposable
	{
		// Token: 0x06001398 RID: 5016 RVA: 0x00089D50 File Offset: 0x00087F50
		public EnrichedToTextConverter(EnrichedParser parser, TextOutput output, Injection injection, bool testTreatNbspAsBreakable, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum)
		{
			this.treatNbspAsBreakable = testTreatNbspAsBreakable;
			this.output = output;
			this.parser = parser;
			this.injection = injection;
			this.output.OpenDocument();
			if (this.injection != null && this.injection.HaveHead)
			{
				this.injection.Inject(true, this.output);
			}
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00089DB4 File Offset: 0x00087FB4
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

		// Token: 0x0600139A RID: 5018 RVA: 0x00089DDF File Offset: 0x00087FDF
		public bool Flush()
		{
			if (!this.endOfFile)
			{
				this.Run();
			}
			return this.endOfFile;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00089DF8 File Offset: 0x00087FF8
		private void Process(HtmlTokenId tokenId)
		{
			this.token = this.parser.Token;
			switch (tokenId)
			{
			case HtmlTokenId.EndOfFile:
				if (this.injection != null && this.injection.HaveTail)
				{
					if (!this.output.LineEmpty)
					{
						this.output.OutputNewLine();
					}
					this.injection.Inject(false, this.output);
				}
				this.output.CloseDocument();
				this.output.Flush();
				this.endOfFile = true;
				break;
			case HtmlTokenId.Text:
				if (!this.insideParam)
				{
					this.OutputText(this.token);
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
				if (this.token.IsTagBegin)
				{
					HtmlNameIndex nameIndex = this.token.NameIndex;
					if (nameIndex <= HtmlNameIndex.ParaIndent)
					{
						if (nameIndex <= HtmlNameIndex.Param)
						{
							if (nameIndex != HtmlNameIndex.Nofill && nameIndex != HtmlNameIndex.FlushRight)
							{
								if (nameIndex != HtmlNameIndex.Param)
								{
									return;
								}
								this.insideParam = !this.token.IsEndTag;
								return;
							}
						}
						else if (nameIndex <= HtmlNameIndex.Italic)
						{
							if (nameIndex != HtmlNameIndex.Color && nameIndex != HtmlNameIndex.Italic)
							{
								return;
							}
							break;
						}
						else
						{
							switch (nameIndex)
							{
							case HtmlNameIndex.Center:
							case HtmlNameIndex.FlushBoth:
								break;
							case HtmlNameIndex.Height:
							case HtmlNameIndex.Underline:
								return;
							default:
								if (nameIndex != HtmlNameIndex.ParaIndent)
								{
									return;
								}
								break;
							}
						}
					}
					else if (nameIndex <= HtmlNameIndex.FlushLeft)
					{
						if (nameIndex == HtmlNameIndex.Fixed || nameIndex == HtmlNameIndex.Smaller)
						{
							break;
						}
						switch (nameIndex)
						{
						case HtmlNameIndex.Bold:
						case HtmlNameIndex.Strike:
							return;
						case HtmlNameIndex.FlushLeft:
							break;
						default:
							return;
						}
					}
					else if (nameIndex <= HtmlNameIndex.Lang)
					{
						if (nameIndex == HtmlNameIndex.Excerpt)
						{
							if (!this.output.LineEmpty)
							{
								this.output.OutputNewLine();
							}
							if (!this.token.IsEndTag)
							{
								this.quotingLevel++;
							}
							else
							{
								this.quotingLevel--;
							}
							this.output.SetQuotingLevel(this.quotingLevel);
							return;
						}
						if (nameIndex != HtmlNameIndex.Lang)
						{
							return;
						}
						break;
					}
					else
					{
						if (nameIndex != HtmlNameIndex.Bigger && nameIndex != HtmlNameIndex.FontFamily)
						{
							return;
						}
						break;
					}
					if (!this.output.LineEmpty)
					{
						this.output.OutputNewLine();
						return;
					}
				}
				break;
			case HtmlTokenId.Restart:
			case HtmlTokenId.OverlappedClose:
			case HtmlTokenId.OverlappedReopen:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0008A038 File Offset: 0x00088238
		private void OutputText(HtmlToken token)
		{
			foreach (TokenRun tokenRun in token.Runs)
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
						this.output.OutputSpace(tokenRun.Length);
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
						this.output.OutputNonspace(tokenRun.Literal, TextMapping.Unicode);
					}
					else
					{
						this.output.OutputNonspace(tokenRun.RawBuffer, tokenRun.RawOffset, tokenRun.RawLength, TextMapping.Unicode);
					}
				}
			}
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0008A15C File Offset: 0x0008835C
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
			if (this.token != null && this.token is IDisposable)
			{
				((IDisposable)this.token).Dispose();
			}
			this.parser = null;
			this.output = null;
			this.token = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x04001380 RID: 4992
		private EnrichedParser parser;

		// Token: 0x04001381 RID: 4993
		private bool endOfFile;

		// Token: 0x04001382 RID: 4994
		private TextOutput output;

		// Token: 0x04001383 RID: 4995
		private HtmlToken token;

		// Token: 0x04001384 RID: 4996
		private bool treatNbspAsBreakable;

		// Token: 0x04001385 RID: 4997
		private bool insideParam;

		// Token: 0x04001386 RID: 4998
		private int quotingLevel;

		// Token: 0x04001387 RID: 4999
		private Injection injection;
	}
}
