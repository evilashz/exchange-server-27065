using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001C0 RID: 448
	internal class MRSScheduledLog<T> : ObjectLog<T>
	{
		// Token: 0x060010E4 RID: 4324 RVA: 0x00027623 File Offset: 0x00025823
		protected MRSScheduledLog(ObjectLogSchema schema, ObjectLogConfiguration configuration) : base(schema, configuration)
		{
			this.lastLoggingTimeUtc = DateTime.UtcNow;
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00027638 File Offset: 0x00025838
		protected virtual bool IsLogEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0002763B File Offset: 0x0002583B
		protected virtual TimeSpan ScheduledLoggingPeriod
		{
			get
			{
				return MRSScheduledLog<T>.LoggingPeriod;
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00027644 File Offset: 0x00025844
		public virtual bool LogIsNeeded()
		{
			bool result = false;
			TimeSpan t = DateTime.UtcNow - this.lastLoggingTimeUtc;
			if (this.IsLogEnabled && t >= this.ScheduledLoggingPeriod)
			{
				result = true;
				this.lastLoggingTimeUtc = DateTime.UtcNow;
			}
			return result;
		}

		// Token: 0x04000983 RID: 2435
		private static readonly TimeSpan LoggingPeriod = new TimeSpan(1, 0, 0);

		// Token: 0x04000984 RID: 2436
		protected DateTime lastLoggingTimeUtc;
	}
}
