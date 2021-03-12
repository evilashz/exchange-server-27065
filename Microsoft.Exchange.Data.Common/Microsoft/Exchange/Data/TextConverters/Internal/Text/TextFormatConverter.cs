using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Text
{
	// Token: 0x02000280 RID: 640
	internal class TextFormatConverter : FormatConverter, IProducerConsumer, IDisposable
	{
		// Token: 0x060019D0 RID: 6608 RVA: 0x000CCC2C File Offset: 0x000CAE2C
		public TextFormatConverter(TextParser parser, FormatOutput output, Injection injection, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum, Stream formatConverterTraceStream) : base(formatConverterTraceStream)
		{
			this.parser = parser;
			this.output = output;
			if (this.output != null)
			{
				this.output.Initialize(this.Store, SourceFormat.Text, "converted from text");
			}
			this.injection = injection;
			base.InitializeDocument();
			if (this.injection != null)
			{
				bool haveHead = this.injection.HaveHead;
			}
			base.OpenContainer(FormatContainerType.Block, false);
			base.Last.SetProperty(PropertyPrecedence.NonStyle, PropertyId.FontSize, new PropertyValue(LengthUnits.Points, 10));
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000CCCB8 File Offset: 0x000CAEB8
		public TextFormatConverter(TextParser parser, FormatStore store, Injection injection, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum, Stream formatConverterTraceStream) : base(store, formatConverterTraceStream)
		{
			this.parser = parser;
			this.injection = injection;
			base.InitializeDocument();
			base.OpenContainer(FormatContainerType.Block, false);
			base.Last.SetProperty(PropertyPrecedence.NonStyle, PropertyId.FontSize, new PropertyValue(LengthUnits.Points, 10));
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000CCD0A File Offset: 0x000CAF0A
		public void Initialize(string fragment)
		{
			this.parser.Initialize(fragment);
			this.lineLength = 0;
			this.newLines = 0;
			this.spaces = 0;
			this.nbsps = 0;
			this.paragraphStarted = false;
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000CCD3C File Offset: 0x000CAF3C
		public override void Run()
		{
			if (this.output != null && base.MustFlush)
			{
				if (this.CanFlush)
				{
					this.FlushOutput();
					return;
				}
			}
			else if (!base.EndOfFile)
			{
				TextTokenId textTokenId = this.parser.Parse();
				if (textTokenId != TextTokenId.None)
				{
					this.Process(textTokenId);
				}
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000CCD87 File Offset: 0x000CAF87
		public bool Flush()
		{
			this.Run();
			return base.EndOfFile && !base.MustFlush;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000CCDA2 File Offset: 0x000CAFA2
		void IDisposable.Dispose()
		{
			if (this.parser != null)
			{
				((IDisposable)this.parser).Dispose();
			}
			this.parser = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x000CCDC4 File Offset: 0x000CAFC4
		private bool CanFlush
		{
			get
			{
				return this.output.CanAcceptMoreOutput;
			}
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000CCDD1 File Offset: 0x000CAFD1
		private bool FlushOutput()
		{
			if (this.output.Flush())
			{
				base.MustFlush = false;
				return true;
			}
			return false;
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000CCDEC File Offset: 0x000CAFEC
		protected void Process(TextTokenId tokenId)
		{
			switch (tokenId)
			{
			case TextTokenId.EndOfFile:
				if (this.injection != null && this.injection.HaveTail)
				{
					base.AddLineBreak(1);
				}
				base.CloseContainer();
				base.CloseAllContainersAndSetEOF();
				break;
			case TextTokenId.Text:
				this.OutputFragmentSimple(this.parser.Token);
				return;
			case TextTokenId.EncodingChange:
				if (this.output != null && this.output.OutputCodePageSameAsInput)
				{
					this.output.OutputEncoding = this.parser.Token.TokenEncoding;
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000CCE7C File Offset: 0x000CB07C
		private void OutputFragmentSimple(TextToken token)
		{
			foreach (TokenRun tokenRun in token.Runs)
			{
				if (tokenRun.IsTextRun)
				{
					RunTextType textType = tokenRun.TextType;
					if (textType <= RunTextType.NewLine)
					{
						if (textType == RunTextType.Unknown)
						{
							goto IL_B3;
						}
						if (textType != RunTextType.Space)
						{
							if (textType != RunTextType.NewLine)
							{
								continue;
							}
							base.AddLineBreak(1);
							continue;
						}
					}
					else if (textType <= RunTextType.UnusualWhitespace)
					{
						if (textType == RunTextType.Tabulation)
						{
							base.AddTabulation(tokenRun.Length);
							continue;
						}
						if (textType != RunTextType.UnusualWhitespace)
						{
							continue;
						}
					}
					else
					{
						if (textType == RunTextType.Nbsp)
						{
							base.AddNbsp(tokenRun.Length);
							continue;
						}
						if (textType != RunTextType.NonSpace)
						{
							continue;
						}
						goto IL_B3;
					}
					base.AddSpace(tokenRun.Length);
					continue;
					IL_B3:
					base.AddNonSpaceText(tokenRun.RawBuffer, tokenRun.RawOffset, tokenRun.RawLength);
				}
				else if (tokenRun.IsSpecial)
				{
					uint kind = tokenRun.Kind;
				}
			}
		}

		// Token: 0x04001F1F RID: 7967
		protected TextParser parser;

		// Token: 0x04001F20 RID: 7968
		private FormatOutput output;

		// Token: 0x04001F21 RID: 7969
		protected int lineLength;

		// Token: 0x04001F22 RID: 7970
		protected int newLines;

		// Token: 0x04001F23 RID: 7971
		protected int spaces;

		// Token: 0x04001F24 RID: 7972
		protected int nbsps;

		// Token: 0x04001F25 RID: 7973
		protected bool paragraphStarted;

		// Token: 0x04001F26 RID: 7974
		protected Injection injection;
	}
}
