using System;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001A9 RID: 425
	internal class CsvOffsetMap
	{
		// Token: 0x06000BCF RID: 3023 RVA: 0x0002B3C2 File Offset: 0x000295C2
		public CsvOffsetMap(byte delimiter, byte quote)
		{
			this.parser = new CsvParser(delimiter, quote);
			this.offset = new int[256];
			this.length = new int[256];
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0002B3F7 File Offset: 0x000295F7
		public int Count
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002B3FF File Offset: 0x000295FF
		public void Reset()
		{
			this.parser.Reset();
			this.fields = 0;
			this.passedCR = false;
			this.afterQuote = false;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002B424 File Offset: 0x00029624
		public int Parse(byte[] buffer, int offset, int count, int rowBase)
		{
			ExTraceGlobals.CommonTracer.TraceDebug<int, int, int>((long)this.GetHashCode(), "CsvOffsetMap Parse with offset {0}, count {1}, base {2}", offset, count, rowBase);
			int i = offset;
			int num = offset + count;
			if (this.parser.State == CsvParserState.LineEnd)
			{
				this.fields = 0;
			}
			while (i < num)
			{
				i = this.parser.Parse(buffer, i, num - i);
				if (i == -1)
				{
					return -1;
				}
				if (this.fields == 256)
				{
					if (this.parser.State == CsvParserState.LineEnd)
					{
						return i;
					}
				}
				else
				{
					switch (this.parser.State)
					{
					case CsvParserState.Whitespace:
					case CsvParserState.LineEnd:
						if (this.afterQuote)
						{
							this.afterQuote = false;
						}
						else
						{
							this.length[this.fields] = i - 1 - rowBase - this.offset[this.fields] - (this.passedCR ? 1 : 0);
							this.fields++;
							this.passedCR = false;
						}
						break;
					case CsvParserState.Field:
						if (!this.passedCR)
						{
							this.offset[this.fields] = i - rowBase;
						}
						else
						{
							this.passedCR = false;
						}
						break;
					case CsvParserState.FieldCR:
					case CsvParserState.QuotedFieldCR:
						this.passedCR = true;
						break;
					case CsvParserState.QuotedField:
						if (!this.passedCR)
						{
							if (!this.inQuote)
							{
								this.offset[this.fields] = i - 1 - rowBase;
							}
						}
						else
						{
							this.passedCR = false;
						}
						break;
					case CsvParserState.QuotedFieldQuote:
						this.inQuote = true;
						break;
					case CsvParserState.EndQuote:
						this.passedCR = false;
						this.inQuote = false;
						i--;
						break;
					case CsvParserState.EndQuoteIgnore:
						this.length[this.fields] = i - 1 - rowBase - this.offset[this.fields];
						this.fields++;
						this.afterQuote = true;
						break;
					}
					if (this.parser.State == CsvParserState.LineEnd)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002B614 File Offset: 0x00029814
		public int GetOffset(int index)
		{
			return this.offset[index];
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002B61E File Offset: 0x0002981E
		public int GetLength(int index)
		{
			return this.length[index];
		}

		// Token: 0x04000895 RID: 2197
		private const int FieldLimit = 256;

		// Token: 0x04000896 RID: 2198
		private CsvParser parser;

		// Token: 0x04000897 RID: 2199
		private int[] offset;

		// Token: 0x04000898 RID: 2200
		private int[] length;

		// Token: 0x04000899 RID: 2201
		private int fields;

		// Token: 0x0400089A RID: 2202
		private bool passedCR;

		// Token: 0x0400089B RID: 2203
		private bool afterQuote;

		// Token: 0x0400089C RID: 2204
		private bool inQuote;
	}
}
