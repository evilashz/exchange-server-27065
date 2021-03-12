using System;

namespace Microsoft.Exchange.Rpc.IPFilter
{
	// Token: 0x02000272 RID: 626
	internal class IPFilterRange
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x00029080 File Offset: 0x00028480
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x00029094 File Offset: 0x00028494
		public int Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x000290A8 File Offset: 0x000284A8
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x000290BC File Offset: 0x000284BC
		public int Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x000290D0 File Offset: 0x000284D0
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x000290E8 File Offset: 0x000284E8
		public DateTime ExpiresOn
		{
			get
			{
				return this.expiresOn;
			}
			set
			{
				this.expiresOn = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x000290FC File Offset: 0x000284FC
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x00029110 File Offset: 0x00028510
		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00029144 File Offset: 0x00028544
		public unsafe void GetLowerBound(ulong* high, ulong* low)
		{
			*high = this.highLowerBound;
			*low = this.lowLowerBound;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00029124 File Offset: 0x00028524
		public void GetLowerBound(out ulong high, out ulong low)
		{
			high = this.highLowerBound;
			low = this.lowLowerBound;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00029164 File Offset: 0x00028564
		public void SetLowerBound(ulong high, ulong low)
		{
			this.highLowerBound = high;
			this.lowLowerBound = low;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000291A0 File Offset: 0x000285A0
		public unsafe void GetUpperBound(ulong* high, ulong* low)
		{
			*high = this.highUpperBound;
			*low = this.lowUpperBound;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00029180 File Offset: 0x00028580
		public void GetUpperBound(out ulong high, out ulong low)
		{
			high = this.highUpperBound;
			low = this.lowUpperBound;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000291C0 File Offset: 0x000285C0
		public void SetUpperBound(ulong high, ulong low)
		{
			this.highUpperBound = high;
			this.lowUpperBound = low;
		}

		// Token: 0x04000CEC RID: 3308
		private int identity;

		// Token: 0x04000CED RID: 3309
		private DateTime expiresOn;

		// Token: 0x04000CEE RID: 3310
		private ulong highLowerBound;

		// Token: 0x04000CEF RID: 3311
		private ulong lowLowerBound;

		// Token: 0x04000CF0 RID: 3312
		private ulong highUpperBound;

		// Token: 0x04000CF1 RID: 3313
		private ulong lowUpperBound;

		// Token: 0x04000CF2 RID: 3314
		private int flags;

		// Token: 0x04000CF3 RID: 3315
		private string comment;
	}
}
