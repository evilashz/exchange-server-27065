using System;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F4 RID: 500
	internal class TimeRange
	{
		// Token: 0x06000EA8 RID: 3752 RVA: 0x00041AED File Offset: 0x0003FCED
		internal TimeRange(ExDateTime st, ExDateTime et)
		{
			this.startTime = st;
			this.endTime = et;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00041B03 File Offset: 0x0003FD03
		internal ExDateTime StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00041B0B File Offset: 0x0003FD0B
		internal ExDateTime EndTime
		{
			get
			{
				return this.endTime;
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00041B13 File Offset: 0x0003FD13
		public override string ToString()
		{
			return this.ToString(Thread.CurrentThread.CurrentCulture);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00041B25 File Offset: 0x0003FD25
		internal string ToString(CultureInfo c)
		{
			return this.startTime.ToString("t", c) + "-" + this.endTime.ToString("t", c);
		}

		// Token: 0x04000B08 RID: 2824
		private ExDateTime startTime;

		// Token: 0x04000B09 RID: 2825
		private ExDateTime endTime;
	}
}
