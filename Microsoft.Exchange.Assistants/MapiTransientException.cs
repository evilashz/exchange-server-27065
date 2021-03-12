using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000052 RID: 82
	internal sealed class MapiTransientException : TransientMailboxException
	{
		// Token: 0x060002AF RID: 687 RVA: 0x0000EC8E File Offset: 0x0000CE8E
		public MapiTransientException(Exception innerException) : base(LocalizedString.Empty, innerException, MapiTransientException.schedule)
		{
		}

		// Token: 0x040001B6 RID: 438
		private static RetrySchedule schedule = new RetrySchedule(FinalAction.Skip, TimeSpan.FromHours(2.0), new TimeSpan[]
		{
			TimeSpan.FromMinutes(1.0),
			TimeSpan.FromMinutes(5.0),
			TimeSpan.FromMinutes(15.0),
			TimeSpan.FromHours(1.0)
		});
	}
}
