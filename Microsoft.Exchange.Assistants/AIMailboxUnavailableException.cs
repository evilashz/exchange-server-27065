using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000046 RID: 70
	internal class AIMailboxUnavailableException : TransientMailboxException
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000E95F File Offset: 0x0000CB5F
		public AIMailboxUnavailableException(Exception innerException) : this(LocalizedString.Empty, innerException)
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000E96D File Offset: 0x0000CB6D
		public AIMailboxUnavailableException(LocalizedString explain, Exception innerException) : base(explain, innerException, AIMailboxUnavailableException.schedule)
		{
		}

		// Token: 0x040001B2 RID: 434
		private static RetrySchedule schedule = new RetrySchedule(FinalAction.RetryForever, TimeSpan.MaxValue, new TimeSpan[]
		{
			TimeSpan.FromHours(1.0)
		});
	}
}
