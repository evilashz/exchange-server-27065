using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000188 RID: 392
	internal class HtmlInjection : Injection
	{
		// Token: 0x060010E7 RID: 4327 RVA: 0x0007ACC7 File Offset: 0x00078EC7
		public HtmlInjection(string injectHead, string injectTail, HeaderFooterFormat injectionFormat, bool filterHtml, HtmlTagCallback callback, bool testBoundaryConditions, Stream traceStream, IProgressMonitor progressMonitor)
		{
			this.injectHead = injectHead;
			this.injectTail = injectTail;
			this.injectionFormat = injectionFormat;
			this.filterHtml = filterHtml;
			this.callback = callback;
			this.testBoundaryConditions = testBoundaryConditions;
			this.progressMonitor = progressMonitor;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0007AD04 File Offset: 0x00078F04
		public bool Active
		{
			get
			{
				return this.documentParser != null;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0007AD12 File Offset: 0x00078F12
		public bool InjectingHead
		{
			get
			{
				return this.injectingHead;
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0007AD1C File Offset: 0x00078F1C
		public IHtmlParser Push(bool head, IHtmlParser documentParser)
		{
			if (head)
			{
				if (this.injectHead != null && !this.headInjected)
				{
					this.documentParser = documentParser;
					if (this.fragmentParser == null)
					{
						this.fragmentParser = new HtmlParser(new ConverterBufferInput(this.injectHead, this.progressMonitor), false, this.injectionFormat == HeaderFooterFormat.Text, 64, 8, this.testBoundaryConditions);
					}
					else
					{
						this.fragmentParser.Initialize(this.injectHead, this.injectionFormat == HeaderFooterFormat.Text);
					}
					this.injectingHead = true;
					return this.fragmentParser;
				}
			}
			else
			{
				if (this.injectHead != null && !this.headInjected)
				{
					this.headInjected = true;
				}
				if (this.injectTail != null && !this.tailInjected)
				{
					this.documentParser = documentParser;
					if (this.fragmentParser == null)
					{
						this.fragmentParser = new HtmlParser(new ConverterBufferInput(this.injectTail, this.progressMonitor), false, this.injectionFormat == HeaderFooterFormat.Text, 64, 8, this.testBoundaryConditions);
					}
					else
					{
						this.fragmentParser.Initialize(this.injectTail, this.injectionFormat == HeaderFooterFormat.Text);
					}
					this.injectingHead = false;
					return this.fragmentParser;
				}
			}
			return documentParser;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0007AE3C File Offset: 0x0007903C
		public IHtmlParser Pop()
		{
			if (this.injectingHead)
			{
				this.headInjected = true;
				if (this.injectTail == null)
				{
					((IDisposable)this.fragmentParser).Dispose();
					this.fragmentParser = null;
				}
			}
			else
			{
				this.tailInjected = true;
				((IDisposable)this.fragmentParser).Dispose();
				this.fragmentParser = null;
			}
			IHtmlParser result = this.documentParser;
			this.documentParser = null;
			return result;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0007AE9C File Offset: 0x0007909C
		public void Inject(bool head, HtmlWriter writer)
		{
			if (head)
			{
				if (this.injectHead != null && !this.headInjected)
				{
					if (this.injectionFormat == HeaderFooterFormat.Text)
					{
						writer.WriteStartTag(HtmlNameIndex.TT);
						writer.WriteStartTag(HtmlNameIndex.Pre);
						writer.WriteNewLine();
					}
					this.CreateHtmlToHtmlConverter(this.injectHead, writer);
					while (!this.fragmentToHtmlConverter.Flush())
					{
					}
					this.headInjected = true;
					if (this.injectTail == null)
					{
						((IDisposable)this.fragmentToHtmlConverter).Dispose();
						this.fragmentToHtmlConverter = null;
					}
					if (this.injectionFormat == HeaderFooterFormat.Text)
					{
						writer.WriteEndTag(HtmlNameIndex.Pre);
						writer.WriteEndTag(HtmlNameIndex.TT);
						return;
					}
				}
			}
			else
			{
				if (this.injectHead != null && !this.headInjected)
				{
					this.headInjected = true;
				}
				if (this.injectTail != null && !this.tailInjected)
				{
					if (this.injectionFormat == HeaderFooterFormat.Text)
					{
						writer.WriteStartTag(HtmlNameIndex.TT);
						writer.WriteStartTag(HtmlNameIndex.Pre);
						writer.WriteNewLine();
					}
					if (this.fragmentToHtmlConverter == null)
					{
						this.CreateHtmlToHtmlConverter(this.injectTail, writer);
					}
					else
					{
						this.fragmentToHtmlConverter.Initialize(this.injectTail, this.injectionFormat == HeaderFooterFormat.Text);
					}
					while (!this.fragmentToHtmlConverter.Flush())
					{
					}
					((IDisposable)this.fragmentToHtmlConverter).Dispose();
					this.fragmentToHtmlConverter = null;
					this.tailInjected = true;
					if (this.injectionFormat == HeaderFooterFormat.Text)
					{
						writer.WriteEndTag(HtmlNameIndex.Pre);
						writer.WriteEndTag(HtmlNameIndex.TT);
					}
				}
			}
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0007B000 File Offset: 0x00079200
		private void CreateHtmlToHtmlConverter(string fragment, HtmlWriter writer)
		{
			HtmlParser htmlParser = new HtmlParser(new ConverterBufferInput(fragment, this.progressMonitor), false, this.injectionFormat == HeaderFooterFormat.Text, 64, 8, this.testBoundaryConditions);
			IHtmlParser parser = htmlParser;
			if (this.injectionFormat == HeaderFooterFormat.Html)
			{
				parser = new HtmlNormalizingParser(htmlParser, null, false, 4096, this.testBoundaryConditions, null, true, 0);
			}
			this.fragmentToHtmlConverter = new HtmlToHtmlConverter(parser, writer, true, this.injectionFormat == HeaderFooterFormat.Html, this.filterHtml, this.callback, true, false, this.traceStream, true, 0, -1, false, this.progressMonitor);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0007B08C File Offset: 0x0007928C
		public override void Inject(bool head, TextOutput output)
		{
			if (head)
			{
				if (this.injectHead != null && !this.headInjected)
				{
					HtmlParser parser = new HtmlParser(new ConverterBufferInput(this.injectHead, this.progressMonitor), false, this.injectionFormat == HeaderFooterFormat.Text, 64, 8, this.testBoundaryConditions);
					this.fragmentToTextConverter = new HtmlToTextConverter(parser, output, null, true, this.injectionFormat == HeaderFooterFormat.Text, false, this.traceStream, true, 0, false, true, true);
					while (!this.fragmentToTextConverter.Flush())
					{
					}
					this.headInjected = true;
					if (this.injectTail == null)
					{
						((IDisposable)this.fragmentToTextConverter).Dispose();
						this.fragmentToTextConverter = null;
						return;
					}
				}
			}
			else
			{
				if (this.injectHead != null && !this.headInjected)
				{
					this.headInjected = true;
				}
				if (this.injectTail != null && !this.tailInjected)
				{
					if (this.fragmentToTextConverter == null)
					{
						HtmlParser parser = new HtmlParser(new ConverterBufferInput(this.injectTail, this.progressMonitor), false, this.injectionFormat == HeaderFooterFormat.Text, 64, 8, this.testBoundaryConditions);
						this.fragmentToTextConverter = new HtmlToTextConverter(parser, output, null, true, this.injectionFormat == HeaderFooterFormat.Text, false, this.traceStream, true, 0, false, true, true);
					}
					else
					{
						this.fragmentToTextConverter.Initialize(this.injectTail, this.injectionFormat == HeaderFooterFormat.Text);
					}
					while (!this.fragmentToTextConverter.Flush())
					{
					}
					((IDisposable)this.fragmentToTextConverter).Dispose();
					this.fragmentToTextConverter = null;
					this.tailInjected = true;
				}
			}
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0007B1FC File Offset: 0x000793FC
		protected override void Dispose(bool disposing)
		{
			if (this.fragmentToHtmlConverter != null)
			{
				((IDisposable)this.fragmentToHtmlConverter).Dispose();
				this.fragmentToHtmlConverter = null;
			}
			if (this.fragmentToTextConverter != null)
			{
				((IDisposable)this.fragmentToTextConverter).Dispose();
				this.fragmentToTextConverter = null;
			}
			if (this.fragmentParser != null)
			{
				((IDisposable)this.fragmentParser).Dispose();
				this.fragmentParser = null;
			}
			this.Reset();
			base.Dispose(disposing);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0007B264 File Offset: 0x00079464
		public override void CompileForRtf(RtfOutput output)
		{
			if (this.fragmentToRtfConverter == null)
			{
				if (this.injectHead != null)
				{
					this.fragmentToRtfConverter = new HtmlFragmentToRtfConverter(output, this.traceStream, this.progressMonitor);
					this.fragmentToRtfConverter.PrepareHead(this.injectHead);
				}
				if (this.injectTail != null)
				{
					if (this.fragmentToRtfConverter == null)
					{
						this.fragmentToRtfConverter = new HtmlFragmentToRtfConverter(output, this.traceStream, this.progressMonitor);
					}
					this.fragmentToRtfConverter.PrepareTail(this.injectTail);
				}
			}
			this.fragmentToRtfConverter.EndPrepare();
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0007B2EE File Offset: 0x000794EE
		public override void InjectRtfFonts(int firstAvailableFontHandle)
		{
			if (this.fragmentToRtfConverter != null)
			{
				this.fragmentToRtfConverter.InjectFonts(firstAvailableFontHandle);
			}
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0007B304 File Offset: 0x00079504
		public override void InjectRtfColors(int nextColorIndex)
		{
			if (this.fragmentToRtfConverter != null)
			{
				this.fragmentToRtfConverter.InjectColors(nextColorIndex);
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0007B31C File Offset: 0x0007951C
		public override void InjectRtf(bool head, bool immediatelyAfterText)
		{
			if (head)
			{
				if (this.injectHead != null && !this.headInjected)
				{
					this.fragmentToRtfConverter.InjectHead();
					this.headInjected = true;
					return;
				}
			}
			else
			{
				if (this.injectHead != null && !this.headInjected)
				{
					this.fragmentToRtfConverter.InjectHead();
					this.headInjected = true;
				}
				if (this.injectTail != null && !this.tailInjected)
				{
					this.fragmentToRtfConverter.InjectTail(immediatelyAfterText);
					this.tailInjected = true;
				}
			}
		}

		// Token: 0x04001178 RID: 4472
		protected bool filterHtml;

		// Token: 0x04001179 RID: 4473
		protected HtmlTagCallback callback;

		// Token: 0x0400117A RID: 4474
		protected bool injectingHead;

		// Token: 0x0400117B RID: 4475
		protected IProgressMonitor progressMonitor;

		// Token: 0x0400117C RID: 4476
		protected IHtmlParser documentParser;

		// Token: 0x0400117D RID: 4477
		protected HtmlParser fragmentParser;

		// Token: 0x0400117E RID: 4478
		protected HtmlToHtmlConverter fragmentToHtmlConverter;

		// Token: 0x0400117F RID: 4479
		protected HtmlToTextConverter fragmentToTextConverter;

		// Token: 0x04001180 RID: 4480
		private HtmlFragmentToRtfConverter fragmentToRtfConverter;
	}
}
