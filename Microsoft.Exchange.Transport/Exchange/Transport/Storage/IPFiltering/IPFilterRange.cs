using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Storage.IPFiltering
{
	// Token: 0x02000129 RID: 297
	internal class IPFilterRange : IPRange, IComparable<IPFilterRange>
	{
		// Token: 0x06000D43 RID: 3395 RVA: 0x00030B75 File Offset: 0x0002ED75
		public IPFilterRange(int id, IPvxAddress start, IPvxAddress end, DateTime expiry, int type) : base(start, end, (IPRange.Format)(type & 15))
		{
			this.identity = id;
			this.timeToLive = expiry;
			this.flags = (type & 4080);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00030BA1 File Offset: 0x0002EDA1
		internal IPFilterRange(IPvxAddress start) : base(start, start, IPRange.Format.SingleAddress)
		{
			this.identity = -1;
			this.timeToLive = DateTime.MaxValue;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00030BBE File Offset: 0x0002EDBE
		internal IPFilterRange(int identity, IPvxAddress start, IPvxAddress end, DateTime expiry, int type, string comment) : this(identity, start, end, expiry, type)
		{
			this.comment = comment;
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00030BD5 File Offset: 0x0002EDD5
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00030BDD File Offset: 0x0002EDDD
		public int Identity
		{
			get
			{
				return this.identity;
			}
			internal set
			{
				this.identity = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00030BE6 File Offset: 0x0002EDE6
		public DateTime ExpiresOn
		{
			get
			{
				return this.timeToLive;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00030BEE File Offset: 0x0002EDEE
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00030BF6 File Offset: 0x0002EDF6
		public string Comment
		{
			get
			{
				return this.comment;
			}
			internal set
			{
				this.comment = value;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00030BFF File Offset: 0x0002EDFF
		public PolicyType PolicyType
		{
			get
			{
				if ((this.flags & 240) != 16)
				{
					return PolicyType.Deny;
				}
				return PolicyType.Allow;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00030C14 File Offset: 0x0002EE14
		public bool AdminCreated
		{
			get
			{
				return (this.flags & 3840) == 256;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00030C29 File Offset: 0x0002EE29
		internal int TypeFlags
		{
			get
			{
				return (int)(base.RangeFormat | (IPRange.Format)this.flags);
			}
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00030C38 File Offset: 0x0002EE38
		public static IPFilterRange FromRowWithComment(IPFilterRow row)
		{
			return new IPFilterRange(row.Identity, row.LowerBound, row.UpperBound, row.ExpiresOn, row.TypeFlags, row.Comment);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00030C63 File Offset: 0x0002EE63
		public static IPFilterRange FromRowWithoutComment(IPFilterRow row)
		{
			return new IPFilterRange(row.Identity, row.LowerBound, row.UpperBound, row.ExpiresOn, row.TypeFlags);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00030C88 File Offset: 0x0002EE88
		public bool IsExpired(DateTime now)
		{
			return this.timeToLive <= now;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00030C98 File Offset: 0x0002EE98
		int IComparable<IPFilterRange>.CompareTo(IPFilterRange x)
		{
			if (x == null)
			{
				return 1;
			}
			return base.LowerBound.CompareTo(x.LowerBound);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00030CC4 File Offset: 0x0002EEC4
		internal void PurgeComment()
		{
			this.comment = null;
		}

		// Token: 0x040005B7 RID: 1463
		internal const int RangeFormatMask = 15;

		// Token: 0x040005B8 RID: 1464
		internal const int AllowFlag = 16;

		// Token: 0x040005B9 RID: 1465
		internal const int DenyFlag = 32;

		// Token: 0x040005BA RID: 1466
		internal const int AllowDenyMask = 240;

		// Token: 0x040005BB RID: 1467
		internal const int AdminCreatedFlag = 256;

		// Token: 0x040005BC RID: 1468
		internal const int ProgrammaticallyCreatedFlag = 512;

		// Token: 0x040005BD RID: 1469
		internal const int CreationTypeMask = 3840;

		// Token: 0x040005BE RID: 1470
		private int identity;

		// Token: 0x040005BF RID: 1471
		private string comment;

		// Token: 0x040005C0 RID: 1472
		private DateTime timeToLive;

		// Token: 0x040005C1 RID: 1473
		private int flags;
	}
}
