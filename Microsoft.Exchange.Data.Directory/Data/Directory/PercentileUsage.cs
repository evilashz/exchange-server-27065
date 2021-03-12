using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009C1 RID: 2497
	internal class PercentileUsage
	{
		// Token: 0x060073CB RID: 29643 RVA: 0x0017D92C File Offset: 0x0017BB2C
		public static int FiveMinuteComparer(PercentileUsage x, PercentileUsage y)
		{
			return Comparer<int>.Default.Compare(x.FiveMinuteUsage, y.FiveMinuteUsage);
		}

		// Token: 0x060073CC RID: 29644 RVA: 0x0017D944 File Offset: 0x0017BB44
		public static int OneHourComparer(PercentileUsage x, PercentileUsage y)
		{
			return Comparer<int>.Default.Compare(x.OneHourUsage, y.OneHourUsage);
		}

		// Token: 0x060073CD RID: 29645 RVA: 0x0017D95C File Offset: 0x0017BB5C
		public PercentileUsage()
		{
			this.CreationTime = TimeProvider.UtcNow;
		}

		// Token: 0x060073CE RID: 29646 RVA: 0x0017D96F File Offset: 0x0017BB6F
		public PercentileUsage(PercentileUsage source)
		{
			this.FiveMinuteUsage = source.FiveMinuteUsage;
			this.OneHourUsage = source.OneHourUsage;
			this.CreationTime = source.CreationTime;
		}

		// Token: 0x060073CF RID: 29647 RVA: 0x0017D99B File Offset: 0x0017BB9B
		public void AddUsage(int usage)
		{
			Interlocked.Add(ref this.fiveMinuteUsage, usage);
			Interlocked.Add(ref this.oneHourUsage, usage);
		}

		// Token: 0x17002953 RID: 10579
		// (get) Token: 0x060073D0 RID: 29648 RVA: 0x0017D9B7 File Offset: 0x0017BBB7
		// (set) Token: 0x060073D1 RID: 29649 RVA: 0x0017D9BF File Offset: 0x0017BBBF
		public int FiveMinuteUsage
		{
			get
			{
				return this.fiveMinuteUsage;
			}
			private set
			{
				Interlocked.Exchange(ref this.fiveMinuteUsage, value);
			}
		}

		// Token: 0x17002954 RID: 10580
		// (get) Token: 0x060073D2 RID: 29650 RVA: 0x0017D9CE File Offset: 0x0017BBCE
		// (set) Token: 0x060073D3 RID: 29651 RVA: 0x0017D9D6 File Offset: 0x0017BBD6
		public int OneHourUsage
		{
			get
			{
				return this.oneHourUsage;
			}
			private set
			{
				Interlocked.Exchange(ref this.oneHourUsage, value);
			}
		}

		// Token: 0x17002955 RID: 10581
		// (get) Token: 0x060073D4 RID: 29652 RVA: 0x0017D9E5 File Offset: 0x0017BBE5
		// (set) Token: 0x060073D5 RID: 29653 RVA: 0x0017D9ED File Offset: 0x0017BBED
		public DateTime CreationTime { get; private set; }

		// Token: 0x17002956 RID: 10582
		// (get) Token: 0x060073D6 RID: 29654 RVA: 0x0017D9F6 File Offset: 0x0017BBF6
		// (set) Token: 0x060073D7 RID: 29655 RVA: 0x0017D9FE File Offset: 0x0017BBFE
		internal bool Expired { get; set; }

		// Token: 0x060073D8 RID: 29656 RVA: 0x0017DA07 File Offset: 0x0017BC07
		public void Clear(bool oneHour)
		{
			if (oneHour)
			{
				this.OneHourUsage = 0;
			}
			this.FiveMinuteUsage = 0;
		}

		// Token: 0x04004AC6 RID: 19142
		private int oneHourUsage;

		// Token: 0x04004AC7 RID: 19143
		private int fiveMinuteUsage;
	}
}
