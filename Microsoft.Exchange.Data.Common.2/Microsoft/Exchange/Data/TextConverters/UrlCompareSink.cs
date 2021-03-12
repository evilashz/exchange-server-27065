using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200019A RID: 410
	internal class UrlCompareSink : ITextSink
	{
		// Token: 0x06001198 RID: 4504 RVA: 0x0007DDED File Offset: 0x0007BFED
		public void Initialize(string url)
		{
			this.url = url;
			this.urlPosition = 0;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0007DDFD File Offset: 0x0007BFFD
		public void Reset()
		{
			this.urlPosition = -1;
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0007DE06 File Offset: 0x0007C006
		public bool IsActive
		{
			get
			{
				return this.urlPosition >= 0;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0007DE14 File Offset: 0x0007C014
		public bool IsMatch
		{
			get
			{
				return this.urlPosition == this.url.Length;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0007DE29 File Offset: 0x0007C029
		public bool IsEnough
		{
			get
			{
				return this.urlPosition < 0;
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0007DE34 File Offset: 0x0007C034
		public void Write(char[] buffer, int offset, int count)
		{
			if (this.IsActive)
			{
				int num = offset + count;
				while (offset < num)
				{
					if (this.urlPosition == 0)
					{
						if (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(buffer[offset])))
						{
							offset++;
							continue;
						}
					}
					else if (this.urlPosition == this.url.Length)
					{
						if (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass(buffer[offset])))
						{
							offset++;
							continue;
						}
						this.urlPosition = -1;
						return;
					}
					if (buffer[offset] != this.url[this.urlPosition])
					{
						this.urlPosition = -1;
						return;
					}
					offset++;
					this.urlPosition++;
				}
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0007DEDC File Offset: 0x0007C0DC
		public void Write(int ucs32Char)
		{
			if (Token.LiteralLength(ucs32Char) != 1)
			{
				this.urlPosition = -1;
				return;
			}
			if (this.urlPosition == 0)
			{
				if (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass((char)ucs32Char)))
				{
					return;
				}
			}
			else if (this.urlPosition == this.url.Length)
			{
				if (ParseSupport.WhitespaceCharacter(ParseSupport.GetCharClass((char)ucs32Char)))
				{
					return;
				}
				this.urlPosition = -1;
				return;
			}
			if ((char)ucs32Char != this.url[this.urlPosition])
			{
				this.urlPosition = -1;
				return;
			}
			this.urlPosition++;
		}

		// Token: 0x040011D5 RID: 4565
		private string url;

		// Token: 0x040011D6 RID: 4566
		private int urlPosition;
	}
}
