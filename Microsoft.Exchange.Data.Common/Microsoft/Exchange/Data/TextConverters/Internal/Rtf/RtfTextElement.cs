using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000261 RID: 609
	internal struct RtfTextElement
	{
		// Token: 0x06001932 RID: 6450 RVA: 0x000C896B File Offset: 0x000C6B6B
		internal RtfTextElement(RtfToken token)
		{
			this.token = token;
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x000C8974 File Offset: 0x000C6B74
		public RunTextType TextType
		{
			get
			{
				return this.token.ElementTextType;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x000C8981 File Offset: 0x000C6B81
		public TextMapping TextMapping
		{
			get
			{
				return this.token.TextMapping;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x000C898E File Offset: 0x000C6B8E
		public int Length
		{
			get
			{
				return this.RawLength;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x000C8996 File Offset: 0x000C6B96
		public bool IsAnyWhitespace
		{
			get
			{
				return this.TextType <= RunTextType.UnusualWhitespace;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x000C89A8 File Offset: 0x000C6BA8
		public bool IsLiteral
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x000C89AB File Offset: 0x000C6BAB
		public char[] RawBuffer
		{
			get
			{
				return this.token.CharBuffer;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x000C89B8 File Offset: 0x000C6BB8
		public int RawOffset
		{
			get
			{
				return this.token.ElementOffset;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x000C89C5 File Offset: 0x000C6BC5
		public int RawLength
		{
			get
			{
				return this.token.ElementLength;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x000C89D2 File Offset: 0x000C6BD2
		public int Literal
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x000C89D5 File Offset: 0x000C6BD5
		public int Value
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x000C89D8 File Offset: 0x000C6BD8
		public bool Eof
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x000C89DB File Offset: 0x000C6BDB
		public int Read(char[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x000C89DE File Offset: 0x000C6BDE
		public void WriteTo(ITextSink sink)
		{
			sink.Write(this.token.CharBuffer, this.token.ElementOffset, this.token.ElementLength);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000C8A07 File Offset: 0x000C6C07
		public string GetString(int maxLength)
		{
			return new string(this.token.CharBuffer, this.token.ElementOffset, this.token.ElementLength);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x000C8A2F File Offset: 0x000C6C2F
		[Conditional("DEBUG")]
		private void AssertCurrent()
		{
		}

		// Token: 0x04001E05 RID: 7685
		private RtfToken token;
	}
}
