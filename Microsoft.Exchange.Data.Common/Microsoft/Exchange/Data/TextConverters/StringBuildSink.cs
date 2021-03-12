using System;
using System.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200019B RID: 411
	internal class StringBuildSink : ITextSinkEx, ITextSink
	{
		// Token: 0x0600119F RID: 4511 RVA: 0x0007DF66 File Offset: 0x0007C166
		public StringBuildSink()
		{
			this.sb = new StringBuilder();
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0007DF79 File Offset: 0x0007C179
		public bool IsEnough
		{
			get
			{
				return this.sb.Length >= this.maxLength;
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0007DF91 File Offset: 0x0007C191
		public void Reset(int maxLength)
		{
			this.maxLength = maxLength;
			this.sb.Length = 0;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0007DFA6 File Offset: 0x0007C1A6
		public void Write(char[] buffer, int offset, int count)
		{
			count = Math.Min(count, this.maxLength - this.sb.Length);
			this.sb.Append(buffer, offset, count);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0007DFD4 File Offset: 0x0007C1D4
		public void Write(int ucs32Char)
		{
			if (Token.LiteralLength(ucs32Char) == 1)
			{
				this.sb.Append((char)ucs32Char);
				return;
			}
			this.sb.Append(Token.LiteralFirstChar(ucs32Char));
			if (!this.IsEnough)
			{
				this.sb.Append(Token.LiteralLastChar(ucs32Char));
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0007E025 File Offset: 0x0007C225
		public void Write(string value)
		{
			this.sb.Append(value);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0007E034 File Offset: 0x0007C234
		public void WriteNewLine()
		{
			this.sb.Append('\r');
			if (!this.IsEnough)
			{
				this.sb.Append('\n');
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0007E05A File Offset: 0x0007C25A
		public override string ToString()
		{
			return this.sb.ToString();
		}

		// Token: 0x040011D7 RID: 4567
		private StringBuilder sb;

		// Token: 0x040011D8 RID: 4568
		private int maxLength;
	}
}
