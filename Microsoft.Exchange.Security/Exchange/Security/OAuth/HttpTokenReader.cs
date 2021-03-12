using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000BB RID: 187
	internal sealed class HttpTokenReader
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x0002ECF4 File Offset: 0x0002CEF4
		public HttpTokenReader(string content)
		{
			this._value = content;
			if (!string.IsNullOrEmpty(this._value))
			{
				this._index = 0;
				this._c = this._value[0];
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0002ED2C File Offset: 0x0002CF2C
		public void SkipLinearWhiteSpace(bool optional)
		{
			if (!this.EndOfContent)
			{
				if (this._c == '\r')
				{
					this.InternalMoveNext();
				}
				if (this._c == '\n')
				{
					this.InternalMoveNext();
				}
			}
			if (!optional)
			{
				this.ValidatePosition();
				if (this._c != ' ' && this._c != '\t')
				{
					throw new FormatException("Missing required whitespace.");
				}
				this.InternalMoveNext();
			}
			while ((!this.EndOfContent && this._c == ' ') || this._c == '\t')
			{
				this.InternalMoveNext();
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0002EDB4 File Offset: 0x0002CFB4
		public void SkipChar(char c)
		{
			this.ValidatePosition();
			if (c != this._c)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The current character does not match the required value '{0}'.", new object[]
				{
					c
				}));
			}
			this.InternalMoveNext();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0002EDFC File Offset: 0x0002CFFC
		public char PeekChar()
		{
			this.ValidatePosition();
			return this._c;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0002EE0C File Offset: 0x0002D00C
		public string ReadTokenOrQuotedString(bool optional)
		{
			if (optional)
			{
				if (this.EndOfContent || (this._c != '"' && this.IsSeparatorChar))
				{
					return string.Empty;
				}
			}
			else
			{
				this.ValidatePosition();
			}
			if (this._c == '"')
			{
				return this.ReadQuotedString();
			}
			return this.ReadToken();
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002EE5C File Offset: 0x0002D05C
		public string ReadToken()
		{
			this.ValidatePosition();
			if (!this.IsTokenChar)
			{
				throw new FormatException("Missing required token.");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this._c);
			this.InternalMoveNext();
			while (!this.EndOfContent && this.IsTokenChar)
			{
				stringBuilder.Append(this._c);
				this.InternalMoveNext();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0002EEC8 File Offset: 0x0002D0C8
		public string ReadQuotedString()
		{
			this.SkipChar('"');
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			while (!this.EndOfContent)
			{
				if (flag && this.IsChar)
				{
					stringBuilder.Append(this._c);
					flag = false;
				}
				else if (this._c == '\\')
				{
					flag = true;
				}
				else
				{
					if (!this.IsTextChar || this._c == '"')
					{
						break;
					}
					stringBuilder.Append(this._c);
				}
				this.InternalMoveNext();
			}
			this.SkipChar('"');
			return stringBuilder.ToString();
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0002EF4C File Offset: 0x0002D14C
		public bool EndOfContent
		{
			get
			{
				return this._value == null || this._index >= this._value.Length;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0002EF70 File Offset: 0x0002D170
		private bool IsSeparatorChar
		{
			get
			{
				return !this.EndOfContent && (this._c == '(' || this._c == ')' || this._c == '<' || this._c == '>' || this._c == '@' || this._c == ',' || this._c == ';' || this._c == ':' || this._c == '\\' || this._c == '"' || this._c == '/' || this._c == '[' || this._c == ']' || this._c == '?' || this._c == '=' || this._c == '{' || this._c == '}' || this._c == ' ' || this._c == '\t');
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0002F05C File Offset: 0x0002D25C
		private bool IsTokenChar
		{
			get
			{
				return !this.EndOfContent && this._c > '\u001f' && this._c < '\u007f' && !this.IsSeparatorChar;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0002F085 File Offset: 0x0002D285
		private bool IsTextChar
		{
			get
			{
				return !this.EndOfContent && this._c > '\u001f' && this._c < 'Ā' && this._c != '\u007f';
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0002F0B5 File Offset: 0x0002D2B5
		private bool IsChar
		{
			get
			{
				return !this.EndOfContent && this._c >= '\0' && this._c <= '\u007f';
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0002F0D7 File Offset: 0x0002D2D7
		private void ValidatePosition()
		{
			if (this.EndOfContent)
			{
				throw new FormatException("Unexpected end of content.");
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0002F0EC File Offset: 0x0002D2EC
		private void InternalMoveNext()
		{
			if (this.EndOfContent)
			{
				throw new InvalidOperationException("Internal parser error.");
			}
			this._index++;
			if (!this.EndOfContent)
			{
				this._c = this._value[this._index];
				return;
			}
			this._c = '\0';
		}

		// Token: 0x0400061D RID: 1565
		private readonly string _value;

		// Token: 0x0400061E RID: 1566
		private char _c;

		// Token: 0x0400061F RID: 1567
		private int _index;
	}
}
