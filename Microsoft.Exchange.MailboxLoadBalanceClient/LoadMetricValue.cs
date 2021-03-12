using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalanceClient
{
	// Token: 0x02000006 RID: 6
	public class LoadMetricValue
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000025CC File Offset: 0x000007CC
		internal LoadMetricValue(LoadMetric loadMetric, long value)
		{
			this.LoadMetric = loadMetric.FriendlyName;
			this.Value = value;
			if (loadMetric.IsSize)
			{
				this.Size = new ByteQuantifiedSize?(loadMetric.ToByteQuantifiedSize(value));
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002601 File Offset: 0x00000801
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002609 File Offset: 0x00000809
		public LocalizedString LoadMetric { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002612 File Offset: 0x00000812
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000261A File Offset: 0x0000081A
		public ByteQuantifiedSize? Size { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002623 File Offset: 0x00000823
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000262B File Offset: 0x0000082B
		public long Value { get; private set; }

		// Token: 0x06000028 RID: 40 RVA: 0x00002634 File Offset: 0x00000834
		public override string ToString()
		{
			if (this.Size != null)
			{
				return string.Format("{0}: {1} ({2})", this.LoadMetric, this.Size, this.Value);
			}
			return string.Format("{0}: {1}", this.LoadMetric, this.Value);
		}
	}
}
