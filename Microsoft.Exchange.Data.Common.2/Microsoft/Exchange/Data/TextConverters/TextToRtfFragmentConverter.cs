using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000187 RID: 391
	internal class TextToRtfFragmentConverter : IProducerConsumer, IDisposable
	{
		// Token: 0x060010E0 RID: 4320 RVA: 0x0007AA9F File Offset: 0x00078C9F
		public TextToRtfFragmentConverter(TextParser parser, RtfOutput output, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum)
		{
			this.output = output;
			this.parser = parser;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0007AAB5 File Offset: 0x00078CB5
		public void Initialize(string fragment)
		{
			this.parser.Initialize(fragment);
			this.endOfFile = false;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0007AACC File Offset: 0x00078CCC
		public void Run()
		{
			if (this.endOfFile)
			{
				return;
			}
			TextTokenId textTokenId = this.parser.Parse();
			if (textTokenId == TextTokenId.None)
			{
				return;
			}
			this.Process(textTokenId);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0007AAF9 File Offset: 0x00078CF9
		public bool Flush()
		{
			if (!this.endOfFile)
			{
				this.Run();
			}
			return this.endOfFile;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0007AB10 File Offset: 0x00078D10
		protected void Process(TextTokenId tokenId)
		{
			switch (tokenId)
			{
			case TextTokenId.EndOfFile:
				this.endOfFile = true;
				break;
			case TextTokenId.Text:
				foreach (TokenRun tokenRun in this.parser.Token.Runs)
				{
					if (tokenRun.IsTextRun)
					{
						RunTextType textType = tokenRun.TextType;
						if (textType <= RunTextType.Tabulation)
						{
							if (textType != RunTextType.Space)
							{
								if (textType == RunTextType.NewLine)
								{
									this.output.WriteControlText("\\line\r\n", false);
									continue;
								}
								if (textType != RunTextType.Tabulation)
								{
									continue;
								}
								for (int i = 0; i < tokenRun.Length; i++)
								{
									this.output.WriteControlText("\\tab", true);
								}
								continue;
							}
						}
						else if (textType != RunTextType.UnusualWhitespace)
						{
							if (textType == RunTextType.Nbsp)
							{
								for (int j = 0; j < tokenRun.Length; j++)
								{
									this.output.WriteText("\u00a0");
								}
								continue;
							}
							if (textType != RunTextType.NonSpace)
							{
								continue;
							}
							this.output.WriteText(tokenRun.RawBuffer, tokenRun.RawOffset, tokenRun.RawLength);
							continue;
						}
						for (int k = 0; k < tokenRun.Length; k++)
						{
							this.output.WriteText(" ");
						}
					}
					else if (tokenRun.IsSpecial)
					{
						uint kind = tokenRun.Kind;
					}
				}
				return;
			case TextTokenId.EncodingChange:
				break;
			default:
				return;
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0007AC92 File Offset: 0x00078E92
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0007ACA1 File Offset: 0x00078EA1
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.parser != null)
			{
				((IDisposable)this.parser).Dispose();
			}
			this.parser = null;
			this.output = null;
		}

		// Token: 0x04001175 RID: 4469
		protected TextParser parser;

		// Token: 0x04001176 RID: 4470
		protected bool endOfFile;

		// Token: 0x04001177 RID: 4471
		protected RtfOutput output;
	}
}
