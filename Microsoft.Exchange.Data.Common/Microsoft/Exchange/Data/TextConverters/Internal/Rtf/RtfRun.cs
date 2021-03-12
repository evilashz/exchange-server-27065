using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x0200025F RID: 607
	internal struct RtfRun
	{
		// Token: 0x0600191F RID: 6431 RVA: 0x000C87AB File Offset: 0x000C69AB
		internal RtfRun(RtfToken token)
		{
			this.token = token;
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x000C87B4 File Offset: 0x000C69B4
		public byte[] Buffer
		{
			get
			{
				return this.token.Buffer;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x000C87C1 File Offset: 0x000C69C1
		public int Offset
		{
			get
			{
				return this.token.CurrentRunOffset;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x000C87CE File Offset: 0x000C69CE
		public int Length
		{
			get
			{
				return (int)this.token.RunQueue[this.token.CurrentRun].Length;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x000C87F0 File Offset: 0x000C69F0
		public RtfRunKind Kind
		{
			get
			{
				return this.token.RunQueue[this.token.CurrentRun].Kind;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x000C8812 File Offset: 0x000C6A12
		public short KeywordId
		{
			get
			{
				return this.token.RunQueue[this.token.CurrentRun].KeywordId;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x000C8834 File Offset: 0x000C6A34
		public int Value
		{
			get
			{
				return this.token.RunQueue[this.token.CurrentRun].Value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x000C8856 File Offset: 0x000C6A56
		public bool Skip
		{
			get
			{
				return this.token.RunQueue[this.token.CurrentRun].Skip;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x000C8878 File Offset: 0x000C6A78
		public bool Lead
		{
			get
			{
				return this.token.RunQueue[this.token.CurrentRun].Lead;
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000C889A File Offset: 0x000C6A9A
		[Conditional("DEBUG")]
		private void AssertCurrent()
		{
		}

		// Token: 0x04001E03 RID: 7683
		private RtfToken token;
	}
}
