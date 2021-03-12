using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000057 RID: 87
	internal sealed class ShutdownException : TransientDatabaseException
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0000EE96 File Offset: 0x0000D096
		public ShutdownException() : base(LocalizedString.Empty, null, ShutdownException.schedule)
		{
		}

		// Token: 0x040001B8 RID: 440
		private static RetrySchedule schedule = new RetrySchedule(FinalAction.RetryForever, TimeSpan.MaxValue, new TimeSpan[]
		{
			TimeSpan.FromHours(1.0)
		});
	}
}
