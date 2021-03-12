using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000044 RID: 68
	internal sealed class AIGrayException : TransientMailboxException
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0000E850 File Offset: 0x0000CA50
		public AIGrayException(Exception innerException) : base(LocalizedString.Empty, innerException, AIGrayException.schedule)
		{
		}

		// Token: 0x040001B0 RID: 432
		private static RetrySchedule schedule = new RetrySchedule(FinalAction.Skip, TimeSpan.FromMinutes(30.0), new TimeSpan[]
		{
			TimeSpan.Zero,
			TimeSpan.FromSeconds(5.0),
			TimeSpan.FromMinutes(1.0),
			TimeSpan.FromMinutes(5.0),
			TimeSpan.FromMinutes(15.0)
		});
	}
}
