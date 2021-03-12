using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000C1 RID: 193
	public class UnlimitedBytes : Unlimited<long>
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x0001A6A7 File Offset: 0x000188A7
		public UnlimitedBytes()
		{
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001A6AF File Offset: 0x000188AF
		public UnlimitedBytes(long value) : base(value)
		{
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0001A6B8 File Offset: 0x000188B8
		public long KB
		{
			get
			{
				return base.Value >> 10;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0001A6C3 File Offset: 0x000188C3
		public long MB
		{
			get
			{
				return base.Value >> 20;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0001A6CE File Offset: 0x000188CE
		public long GB
		{
			get
			{
				return base.Value >> 30;
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001A6D9 File Offset: 0x000188D9
		public static UnlimitedBytes FromKB(long kiloBytes)
		{
			return new UnlimitedBytes(kiloBytes << 10);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001A6E4 File Offset: 0x000188E4
		public static UnlimitedBytes FromMB(long megaBytes)
		{
			return new UnlimitedBytes(megaBytes << 20);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001A6EF File Offset: 0x000188EF
		public static UnlimitedBytes FromGB(long gigaBytes)
		{
			return new UnlimitedBytes(gigaBytes << 30);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001A6FA File Offset: 0x000188FA
		public static UnlimitedBytes operator +(UnlimitedBytes left, long right)
		{
			if (!left.IsUnlimited)
			{
				return new UnlimitedBytes(left.Value + right);
			}
			return left;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001A713 File Offset: 0x00018913
		public static UnlimitedBytes operator *(UnlimitedBytes left, long right)
		{
			if (!left.IsUnlimited)
			{
				return new UnlimitedBytes(left.Value * right);
			}
			return left;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001A72C File Offset: 0x0001892C
		public static UnlimitedBytes operator /(UnlimitedBytes left, long right)
		{
			if (!left.IsUnlimited)
			{
				return new UnlimitedBytes(left.Value / right);
			}
			return left;
		}

		// Token: 0x0400073D RID: 1853
		public new static UnlimitedBytes UnlimitedValue = new UnlimitedBytes();
	}
}
