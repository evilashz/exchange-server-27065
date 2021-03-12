using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000045 RID: 69
	internal sealed class AIMailboxInTransitException : TransientMailboxException
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000E90B File Offset: 0x0000CB0B
		public AIMailboxInTransitException(Exception innerException) : base(LocalizedString.Empty, innerException, AIMailboxInTransitException.schedule)
		{
		}

		// Token: 0x040001B1 RID: 433
		private static RetrySchedule schedule = new RetrySchedule(FinalAction.RetryForever, TimeSpan.MaxValue, new TimeSpan[]
		{
			TimeSpan.FromMinutes(5.0)
		});
	}
}
