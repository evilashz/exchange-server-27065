using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200028F RID: 655
	internal class DummyTimeEntry : ITimeEntry, IDisposable
	{
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0008D918 File Offset: 0x0008BB18
		public TimeId TimeId
		{
			get
			{
				return TimeId.HandlerBeginProcessRequest;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x0008D91B File Offset: 0x0008BB1B
		public DateTime StartTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0008D922 File Offset: 0x0008BB22
		public DateTime EndTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x0008D929 File Offset: 0x0008BB29
		public TimeSpan ElapsedInclusive
		{
			get
			{
				return TimeSpan.Zero;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x0008D930 File Offset: 0x0008BB30
		public TimeSpan ElapsedExclusive
		{
			get
			{
				return TimeSpan.Zero;
			}
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0008D937 File Offset: 0x0008BB37
		public void AddChild(ITimeEntry child)
		{
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0008D939 File Offset: 0x0008BB39
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000EB8 RID: 3768
		public static readonly DummyTimeEntry Singleton = new DummyTimeEntry();
	}
}
