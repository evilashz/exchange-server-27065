using System;
using System.IO;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Text
{
	// Token: 0x0200027F RID: 639
	internal class TextToTextConverter : IProducerConsumer, IDisposable
	{
		// Token: 0x060019C9 RID: 6601 RVA: 0x000CC89C File Offset: 0x000CAA9C
		public TextToTextConverter(TextParser parser, TextOutput output, Injection injection, bool convertFragment, bool treatNbspAsBreakable, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum)
		{
			this.treatNbspAsBreakable = treatNbspAsBreakable;
			this.convertFragment = convertFragment;
			this.output = output;
			this.parser = parser;
			this.injection = injection;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x000CC8C9 File Offset: 0x000CAAC9
		public void Initialize(string fragment)
		{
			this.parser.Initialize(fragment);
			this.endOfFile = false;
			this.lineLength = 0;
			this.newLines = 0;
			this.spaces = 0;
			this.nbsps = 0;
			this.paragraphStarted = false;
			this.started = false;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000CC908 File Offset: 0x000CAB08
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

		// Token: 0x060019CC RID: 6604 RVA: 0x000CC935 File Offset: 0x000CAB35
		public bool Flush()
		{
			if (!this.endOfFile)
			{
				this.Run();
			}
			return this.endOfFile;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000CC94C File Offset: 0x000CAB4C
		protected void Process(TextTokenId tokenId)
		{
			if (!this.started)
			{
				if (!this.convertFragment)
				{
					this.output.OpenDocument();
					if (this.injection != null && this.injection.HaveHead)
					{
						this.injection.Inject(true, this.output);
					}
				}
				this.output.SetQuotingLevel(0);
				this.started = true;
			}
			switch (tokenId)
			{
			case TextTokenId.EndOfFile:
				if (!this.convertFragment)
				{
					if (this.injection != null && this.injection.HaveTail)
					{
						if (!this.output.LineEmpty)
						{
							this.output.OutputNewLine();
						}
						this.injection.Inject(false, this.output);
					}
					this.output.Flush();
				}
				else
				{
					this.output.CloseParagraph();
				}
				this.endOfFile = true;
				break;
			case TextTokenId.Text:
				this.OutputFragmentSimple(this.parser.Token);
				return;
			case TextTokenId.EncodingChange:
				if (!this.convertFragment && this.output.OutputCodePageSameAsInput)
				{
					this.output.OutputEncoding = this.parser.Token.TokenEncoding;
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000CCA74 File Offset: 0x000CAC74
		private void OutputFragmentSimple(TextToken token)
		{
			foreach (TokenRun tokenRun in token.Runs)
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
								this.output.OutputNewLine();
								continue;
							}
							if (textType != RunTextType.Tabulation)
							{
								continue;
							}
							this.output.OutputTabulation(tokenRun.Length);
							continue;
						}
					}
					else if (textType != RunTextType.UnusualWhitespace)
					{
						if (textType != RunTextType.Nbsp)
						{
							if (textType != RunTextType.NonSpace)
							{
								continue;
							}
							this.output.OutputNonspace(tokenRun.RawBuffer, tokenRun.RawOffset, tokenRun.RawLength, TextMapping.Unicode);
							continue;
						}
						else
						{
							if (this.treatNbspAsBreakable)
							{
								this.output.OutputSpace(tokenRun.Length);
								continue;
							}
							this.output.OutputNbsp(tokenRun.Length);
							continue;
						}
					}
					this.output.OutputSpace(tokenRun.Length);
				}
				else if (tokenRun.IsSpecial && tokenRun.Kind == 167772160U)
				{
					this.output.SetQuotingLevel((int)((ushort)tokenRun.Value));
				}
			}
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x000CCBB8 File Offset: 0x000CADB8
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
			if (this.injection != null)
			{
				((IDisposable)this.injection).Dispose();
			}
			this.parser = null;
			this.output = null;
			this.injection = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x04001F13 RID: 7955
		protected TextParser parser;

		// Token: 0x04001F14 RID: 7956
		protected bool endOfFile;

		// Token: 0x04001F15 RID: 7957
		protected TextOutput output;

		// Token: 0x04001F16 RID: 7958
		protected bool convertFragment;

		// Token: 0x04001F17 RID: 7959
		protected int lineLength;

		// Token: 0x04001F18 RID: 7960
		protected int newLines;

		// Token: 0x04001F19 RID: 7961
		protected int spaces;

		// Token: 0x04001F1A RID: 7962
		protected int nbsps;

		// Token: 0x04001F1B RID: 7963
		protected bool paragraphStarted;

		// Token: 0x04001F1C RID: 7964
		protected bool treatNbspAsBreakable;

		// Token: 0x04001F1D RID: 7965
		private bool started;

		// Token: 0x04001F1E RID: 7966
		protected Injection injection;
	}
}
