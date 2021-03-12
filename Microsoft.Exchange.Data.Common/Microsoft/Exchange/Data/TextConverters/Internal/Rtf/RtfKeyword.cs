using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000260 RID: 608
	internal struct RtfKeyword
	{
		// Token: 0x06001929 RID: 6441 RVA: 0x000C889C File Offset: 0x000C6A9C
		internal RtfKeyword(RtfToken token)
		{
			this.token = token;
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x000C88A5 File Offset: 0x000C6AA5
		public byte[] Buffer
		{
			get
			{
				return this.token.Buffer;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x000C88B2 File Offset: 0x000C6AB2
		public int Offset
		{
			get
			{
				return this.token.CurrentRunOffset;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x000C88BF File Offset: 0x000C6ABF
		public int Length
		{
			get
			{
				return (int)this.token.RunQueue[this.token.CurrentRun].Length;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x000C88E1 File Offset: 0x000C6AE1
		public short Id
		{
			get
			{
				return this.token.RunQueue[this.token.CurrentRun].KeywordId;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x000C8903 File Offset: 0x000C6B03
		public int Value
		{
			get
			{
				return this.token.RunQueue[this.token.CurrentRun].Value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x000C8925 File Offset: 0x000C6B25
		public bool Skip
		{
			get
			{
				return this.token.RunQueue[this.token.CurrentRun].Skip;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x000C8947 File Offset: 0x000C6B47
		public bool First
		{
			get
			{
				return this.token.RunQueue[this.token.CurrentRun].Lead;
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x000C8969 File Offset: 0x000C6B69
		[Conditional("DEBUG")]
		private void AssertCurrent()
		{
		}

		// Token: 0x04001E04 RID: 7684
		private RtfToken token;
	}
}
