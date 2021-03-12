using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000189 RID: 393
	internal class HtmlFragmentToRtfConverter
	{
		// Token: 0x060010F4 RID: 4340 RVA: 0x0007B394 File Offset: 0x00079594
		public HtmlFragmentToRtfConverter(RtfOutput output, Stream traceStream, IProgressMonitor progressMonitor)
		{
			HtmlParser parser = new HtmlParser(new ConverterBufferInput(string.Empty, progressMonitor), false, false, 64, 8, false);
			HtmlNormalizingParser parser2 = new HtmlNormalizingParser(parser, null, false, 4096, false, null, true, 0);
			this.formatStore = new FormatStore();
			this.formatStore.InitializeCodepageDetector();
			this.converter = new HtmlFormatConverter(parser2, this.formatStore, true, false, traceStream, true, 0, null, progressMonitor);
			this.output = output;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0007B406 File Offset: 0x00079606
		public void PrepareHead(string headHtml)
		{
			this.headFragmentNode = this.converter.Initialize(headHtml);
			this.converter.ConvertToStore();
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0007B426 File Offset: 0x00079626
		public void PrepareTail(string tailHtml)
		{
			this.tailFragmentNode = this.converter.Initialize(tailHtml);
			this.converter.ConvertToStore();
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0007B446 File Offset: 0x00079646
		public void EndPrepare()
		{
			this.converter = null;
			this.formatOutput = new RtfFormatOutput(this.output, null, null);
			this.formatOutput.Initialize(this.formatStore, SourceFormat.Html, null);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0007B475 File Offset: 0x00079675
		public void InjectFonts(int firstAvailableFontHandle)
		{
			this.formatOutput.OutputFonts(firstAvailableFontHandle);
			this.fontsWritten = true;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0007B48A File Offset: 0x0007968A
		public void InjectColors(int nextColorIndex)
		{
			this.formatOutput.OutputColors(nextColorIndex);
			this.colorsWritten = true;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0007B4A0 File Offset: 0x000796A0
		public void InjectHead()
		{
			if (!this.fontsWritten)
			{
				this.output.WriteControlText("{\\fonttbl", false);
				this.InjectFonts(1);
				this.output.WriteControlText("}", false);
			}
			if (!this.colorsWritten)
			{
				this.output.WriteControlText("{\\colortbl;", false);
				this.InjectColors(1);
				this.output.WriteControlText("}", false);
			}
			this.InjectFragment(this.headFragmentNode);
			this.output.WriteControlText("\\pard\\par\r\n", false);
			this.output.RtfLineLength = 0;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0007B538 File Offset: 0x00079738
		public void InjectTail(bool immediatelyAfterText)
		{
			if (!this.fontsWritten)
			{
				this.output.WriteControlText("{\\fonttbl", false);
				this.InjectFonts(1);
				this.output.WriteControlText("}", false);
			}
			if (!this.colorsWritten)
			{
				this.output.WriteControlText("{\\colortbl;", false);
				this.InjectColors(1);
				this.output.WriteControlText("}", false);
			}
			if (immediatelyAfterText)
			{
				this.output.WriteControlText("\\pard\\par\r\n", true);
			}
			this.output.WriteControlText("\\pard\\plain", true);
			this.InjectFragment(this.tailFragmentNode);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0007B5D8 File Offset: 0x000797D8
		private void InjectFragment(FormatNode fragmentNode)
		{
			this.formatOutput.OutputFragment(fragmentNode);
		}

		// Token: 0x04001181 RID: 4481
		private FormatStore formatStore;

		// Token: 0x04001182 RID: 4482
		private FormatNode headFragmentNode;

		// Token: 0x04001183 RID: 4483
		private FormatNode tailFragmentNode;

		// Token: 0x04001184 RID: 4484
		private HtmlFormatConverter converter;

		// Token: 0x04001185 RID: 4485
		private RtfFormatOutput formatOutput;

		// Token: 0x04001186 RID: 4486
		private RtfOutput output;

		// Token: 0x04001187 RID: 4487
		private bool colorsWritten;

		// Token: 0x04001188 RID: 4488
		private bool fontsWritten;
	}
}
