using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Network
{
	// Token: 0x02000225 RID: 549
	internal class Win32DnsQueryResult<T>
	{
		// Token: 0x06000F4E RID: 3918 RVA: 0x00065C04 File Offset: 0x00063E04
		internal Win32DnsQueryResult(TimeSpan duration, long resultCode, T[] records)
		{
			if (resultCode == 0L && records == null)
			{
				throw new ArgumentException("The 'records' parameter must be non-null when 'resultCode' is zero.");
			}
			this.Duration = duration;
			this.ResultCode = resultCode;
			this.Records = records;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00065C34 File Offset: 0x00063E34
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x00065C3C File Offset: 0x00063E3C
		public TimeSpan Duration { get; private set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00065C45 File Offset: 0x00063E45
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x00065C4D File Offset: 0x00063E4D
		public long ResultCode { get; private set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00065C56 File Offset: 0x00063E56
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x00065C5E File Offset: 0x00063E5E
		public T[] Records { get; private set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00065C67 File Offset: 0x00063E67
		public bool Success
		{
			get
			{
				return this.ResultCode == 0L;
			}
		}
	}
}
