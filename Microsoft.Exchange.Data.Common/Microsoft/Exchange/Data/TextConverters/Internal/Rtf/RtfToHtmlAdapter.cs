using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000276 RID: 630
	internal class RtfToHtmlAdapter : IProducerConsumer, IDisposable
	{
		// Token: 0x06001984 RID: 6532 RVA: 0x000C98CD File Offset: 0x000C7ACD
		public RtfToHtmlAdapter(RtfParser parser, ConverterOutput output, RtfToHtml rtfToHtml, IProgressMonitor progressMonitor)
		{
			this.parser = parser;
			this.output = output;
			this.rtfToHtml = rtfToHtml;
			this.progressMonitor = progressMonitor;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x000C98F4 File Offset: 0x000C7AF4
		void IDisposable.Dispose()
		{
			if (this.parser != null && this.parser is IDisposable)
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

		// Token: 0x06001986 RID: 6534 RVA: 0x000C9955 File Offset: 0x000C7B55
		public void Run()
		{
			if (this.consumerOrProducer != null)
			{
				this.consumerOrProducer.Run();
				return;
			}
			this.ParseAndWatch();
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x000C9971 File Offset: 0x000C7B71
		public bool Flush()
		{
			if (this.consumerOrProducer != null)
			{
				return this.consumerOrProducer.Flush();
			}
			this.Run();
			return false;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000C9990 File Offset: 0x000C7B90
		private void ParseAndWatch()
		{
			while (!this.parser.ParseRun())
			{
				if (this.parser.ParseBufferFull)
				{
					this.Restart(RtfEncapsulation.None);
					return;
				}
				if (!this.parser.ReadMoreData(false))
				{
					return;
				}
			}
			RtfRunKind runKind = this.parser.RunKind;
			if (runKind != RtfRunKind.Ignore)
			{
				if (runKind != RtfRunKind.Begin)
				{
					if (runKind != RtfRunKind.Keyword)
					{
						this.Restart(RtfEncapsulation.None);
					}
					else
					{
						if (this.countTokens++ > 10)
						{
							this.Restart(RtfEncapsulation.None);
							return;
						}
						if (this.parser.KeywordId == 292)
						{
							if (this.parser.KeywordValue >= 1)
							{
								this.Restart(RtfEncapsulation.Html);
								return;
							}
							this.Restart(RtfEncapsulation.None);
							return;
						}
						else if (this.parser.KeywordId == 329)
						{
							this.Restart(RtfEncapsulation.Text);
							return;
						}
					}
				}
				else if (this.countTokens++ != 0)
				{
					this.Restart(RtfEncapsulation.None);
					return;
				}
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000C9A81 File Offset: 0x000C7C81
		private void Restart(RtfEncapsulation encapsulation)
		{
			this.parser.Restart();
			this.consumerOrProducer = this.rtfToHtml.CreateChain(encapsulation, this.parser, this.output, this.progressMonitor);
		}

		// Token: 0x04001EBB RID: 7867
		private IProducerConsumer consumerOrProducer;

		// Token: 0x04001EBC RID: 7868
		private RtfParser parser;

		// Token: 0x04001EBD RID: 7869
		private ConverterOutput output;

		// Token: 0x04001EBE RID: 7870
		private RtfToHtml rtfToHtml;

		// Token: 0x04001EBF RID: 7871
		private int countTokens;

		// Token: 0x04001EC0 RID: 7872
		private IProgressMonitor progressMonitor;
	}
}
