using System;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001AB RID: 427
	internal class CsvParser
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002B628 File Offset: 0x00029828
		public CsvParser(byte delimiter, byte quote)
		{
			this.delimiter = delimiter;
			this.quote = quote;
			this.state = CsvParserState.Whitespace;
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0002B645 File Offset: 0x00029845
		public CsvParserState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002B64D File Offset: 0x0002984D
		public void Reset()
		{
			this.state = CsvParserState.Whitespace;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002B658 File Offset: 0x00029858
		public int Parse(byte[] data, int offset, int count)
		{
			ExTraceGlobals.CommonTracer.TraceDebug<int, int>((long)this.GetHashCode(), "CsvParser Parse with offset {0}, count {1}", offset, count);
			int num = offset;
			int num2 = num + count;
			CsvParserState csvParserState = this.state;
			while (num < num2 && this.state == csvParserState)
			{
				byte b = data[num++];
				switch (this.state)
				{
				case CsvParserState.Whitespace:
				case CsvParserState.LineEnd:
					if (b == this.quote)
					{
						this.state = CsvParserState.QuotedField;
					}
					else
					{
						byte b2 = b;
						if (b2 != 9 && b2 != 13 && b2 != 32)
						{
							this.state = CsvParserState.Field;
							num--;
						}
					}
					break;
				case CsvParserState.Field:
					if (b == this.delimiter)
					{
						this.state = CsvParserState.Whitespace;
					}
					else if (b == 10)
					{
						this.state = CsvParserState.LineEnd;
					}
					else if (b == 13)
					{
						this.state = CsvParserState.FieldCR;
					}
					break;
				case CsvParserState.FieldCR:
					if (b == 10)
					{
						this.state = CsvParserState.LineEnd;
					}
					else if (b == this.delimiter)
					{
						this.state = CsvParserState.Whitespace;
					}
					else if (b != 13)
					{
						this.state = CsvParserState.Field;
					}
					break;
				case CsvParserState.QuotedField:
					if (b == this.quote)
					{
						this.state = CsvParserState.QuotedFieldQuote;
					}
					else if (b == 13)
					{
						this.state = CsvParserState.QuotedFieldCR;
					}
					break;
				case CsvParserState.QuotedFieldCR:
					if (b == this.quote)
					{
						this.state = CsvParserState.QuotedFieldQuote;
					}
					else if (b != 13)
					{
						this.state = CsvParserState.QuotedField;
					}
					break;
				case CsvParserState.QuotedFieldQuote:
					if (b == this.quote)
					{
						this.state = CsvParserState.QuotedField;
					}
					else
					{
						this.state = CsvParserState.EndQuote;
					}
					break;
				case CsvParserState.EndQuote:
					if (b == this.quote)
					{
						this.state = CsvParserState.QuotedField;
					}
					else if (b == this.delimiter)
					{
						this.state = CsvParserState.Whitespace;
					}
					else if (b == 10)
					{
						this.state = CsvParserState.LineEnd;
					}
					else
					{
						this.state = CsvParserState.EndQuoteIgnore;
					}
					break;
				case CsvParserState.EndQuoteIgnore:
					if (b == this.delimiter)
					{
						this.state = CsvParserState.Whitespace;
					}
					else if (b == 10)
					{
						this.state = CsvParserState.LineEnd;
					}
					break;
				}
			}
			if (this.state != csvParserState)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x040008A7 RID: 2215
		private CsvParserState state;

		// Token: 0x040008A8 RID: 2216
		private byte delimiter;

		// Token: 0x040008A9 RID: 2217
		private byte quote;
	}
}
