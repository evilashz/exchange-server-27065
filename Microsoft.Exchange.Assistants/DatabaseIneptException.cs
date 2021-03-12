using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200004B RID: 75
	internal sealed class DatabaseIneptException : TransientDatabaseException
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000EA32 File Offset: 0x0000CC32
		public DatabaseIneptException(Exception innerException) : base(LocalizedString.Empty, innerException, DatabaseIneptException.schedule)
		{
		}

		// Token: 0x040001B3 RID: 435
		private static RetrySchedule schedule = new RetrySchedule(FinalAction.RetryForever, TimeSpan.MaxValue, new TimeSpan[]
		{
			TimeSpan.Zero,
			TimeSpan.FromSeconds(5.0),
			TimeSpan.FromMinutes(1.0),
			TimeSpan.FromMinutes(2.0),
			TimeSpan.FromMinutes(5.0),
			TimeSpan.FromMinutes(15.0)
		});
	}
}
