using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200018F RID: 399
	internal struct TokenRun
	{
		// Token: 0x060010FD RID: 4349 RVA: 0x0007B5E6 File Offset: 0x000797E6
		internal TokenRun(Token token)
		{
			this.token = token;
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x0007B5EF File Offset: 0x000797EF
		public RunType Type
		{
			get
			{
				return this.token.RunList[this.token.WholePosition.Run].Type;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0007B616 File Offset: 0x00079816
		public bool IsTextRun
		{
			get
			{
				return this.token.RunList[this.token.WholePosition.Run].Type >= (RunType)2147483648U;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0007B647 File Offset: 0x00079847
		public bool IsSpecial
		{
			get
			{
				return this.token.RunList[this.token.WholePosition.Run].Type == RunType.Special;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x0007B675 File Offset: 0x00079875
		public bool IsNormal
		{
			get
			{
				return this.token.RunList[this.token.WholePosition.Run].Type == (RunType)2147483648U;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x0007B6A3 File Offset: 0x000798A3
		public bool IsLiteral
		{
			get
			{
				return this.token.RunList[this.token.WholePosition.Run].Type == (RunType)3221225472U;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0007B6D1 File Offset: 0x000798D1
		public RunTextType TextType
		{
			get
			{
				return this.token.RunList[this.token.WholePosition.Run].TextType;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x0007B6F8 File Offset: 0x000798F8
		public char[] RawBuffer
		{
			get
			{
				return this.token.Buffer;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x0007B705 File Offset: 0x00079905
		public int RawOffset
		{
			get
			{
				return this.token.WholePosition.RunOffset;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x0007B717 File Offset: 0x00079917
		public int RawLength
		{
			get
			{
				return this.token.RunList[this.token.WholePosition.Run].Length;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0007B73E File Offset: 0x0007993E
		public uint Kind
		{
			get
			{
				return this.token.RunList[this.token.WholePosition.Run].Kind;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x0007B765 File Offset: 0x00079965
		public int Literal
		{
			get
			{
				return this.token.RunList[this.token.WholePosition.Run].Value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x0007B78C File Offset: 0x0007998C
		public int Length
		{
			get
			{
				if (this.IsNormal)
				{
					return this.RawLength;
				}
				if (!this.IsLiteral)
				{
					return 0;
				}
				return Token.LiteralLength(this.Literal);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x0007B7B2 File Offset: 0x000799B2
		public int Value
		{
			get
			{
				return this.token.RunList[this.token.WholePosition.Run].Value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0007B7D9 File Offset: 0x000799D9
		public char FirstChar
		{
			get
			{
				if (!this.IsLiteral)
				{
					return this.RawBuffer[this.RawOffset];
				}
				return Token.LiteralFirstChar(this.Literal);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x0007B7FC File Offset: 0x000799FC
		public char LastChar
		{
			get
			{
				if (!this.IsLiteral)
				{
					return this.RawBuffer[this.RawOffset + this.RawLength - 1];
				}
				return Token.LiteralLastChar(this.Literal);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0007B828 File Offset: 0x00079A28
		public bool IsAnyWhitespace
		{
			get
			{
				return this.TextType <= RunTextType.UnusualWhitespace;
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0007B83C File Offset: 0x00079A3C
		public int ReadLiteral(char[] buffer)
		{
			int value = this.token.RunList[this.token.WholePosition.Run].Value;
			int num = Token.LiteralLength(value);
			if (num == 1)
			{
				buffer[0] = (char)value;
				return 1;
			}
			buffer[0] = Token.LiteralFirstChar(value);
			buffer[1] = Token.LiteralLastChar(value);
			return 2;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0007B894 File Offset: 0x00079A94
		public string GetString(int maxSize)
		{
			int run = this.token.WholePosition.Run;
			Token.RunEntry[] runList = this.token.RunList;
			RunType type = runList[run].Type;
			if (type == (RunType)2147483648U)
			{
				return new string(this.token.Buffer, this.token.WholePosition.RunOffset, Math.Min(maxSize, runList[run].Length));
			}
			if (type != (RunType)3221225472U)
			{
				return string.Empty;
			}
			if (this.Length == 1)
			{
				return this.FirstChar.ToString();
			}
			Token.Fragment fragment = default(Token.Fragment);
			fragment.Initialize(run, this.token.WholePosition.RunOffset);
			fragment.Tail = fragment.Head + 1;
			return this.token.GetString(ref fragment, maxSize);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0007B970 File Offset: 0x00079B70
		[Conditional("DEBUG")]
		private void AssertCurrent()
		{
		}

		// Token: 0x040011AC RID: 4524
		private Token token;
	}
}
