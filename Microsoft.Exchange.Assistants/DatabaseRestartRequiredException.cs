using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200004C RID: 76
	internal sealed class DatabaseRestartRequiredException : TransientDatabaseException
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000EB00 File Offset: 0x0000CD00
		public DatabaseRestartRequiredException(Exception e) : base(LocalizedString.Empty, e, DatabaseRestartRequiredException.schedule)
		{
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000EB13 File Offset: 0x0000CD13
		public DatabaseRestartRequiredException() : this(null)
		{
		}

		// Token: 0x040001B4 RID: 436
		private static RetrySchedule schedule = new RetrySchedule(FinalAction.RetryForever, TimeSpan.MaxValue, new TimeSpan[]
		{
			TimeSpan.FromHours(1.0)
		});
	}
}
