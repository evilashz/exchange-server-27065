using System;
using System.Text;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Text
{
	// Token: 0x02000282 RID: 642
	internal class TextCodePageConverter : IProducerConsumer, IDisposable
	{
		// Token: 0x06001A05 RID: 6661 RVA: 0x000CE352 File Offset: 0x000CC552
		public TextCodePageConverter(ConverterInput input, ConverterOutput output)
		{
			this.input = input;
			this.output = output;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x000CE368 File Offset: 0x000CC568
		public void Run()
		{
			if (this.endOfFile)
			{
				return;
			}
			char[] buffer = null;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (!this.input.ReadMore(ref buffer, ref num, ref num2, ref num3))
			{
				return;
			}
			if (this.input.EndOfFile)
			{
				this.endOfFile = true;
			}
			if (num3 - num != 0)
			{
				if (!this.gotAnyText)
				{
					if (this.output is ConverterEncodingOutput)
					{
						ConverterEncodingOutput converterEncodingOutput = this.output as ConverterEncodingOutput;
						if (converterEncodingOutput.CodePageSameAsInput)
						{
							if (this.input is ConverterDecodingInput)
							{
								converterEncodingOutput.Encoding = (this.input as ConverterDecodingInput).Encoding;
							}
							else
							{
								converterEncodingOutput.Encoding = Encoding.UTF8;
							}
						}
					}
					this.gotAnyText = true;
				}
				this.output.Write(buffer, num, num3 - num);
				this.input.ReportProcessed(num3 - num);
			}
			if (this.endOfFile)
			{
				this.output.Flush();
			}
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000CE44D File Offset: 0x000CC64D
		public bool Flush()
		{
			if (!this.endOfFile)
			{
				this.Run();
			}
			return this.endOfFile;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000CE463 File Offset: 0x000CC663
		void IDisposable.Dispose()
		{
			if (this.input != null)
			{
				((IDisposable)this.input).Dispose();
			}
			if (this.output != null)
			{
				((IDisposable)this.output).Dispose();
			}
			this.input = null;
			this.output = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x04001F42 RID: 8002
		protected ConverterInput input;

		// Token: 0x04001F43 RID: 8003
		protected bool endOfFile;

		// Token: 0x04001F44 RID: 8004
		protected bool gotAnyText;

		// Token: 0x04001F45 RID: 8005
		protected ConverterOutput output;
	}
}
