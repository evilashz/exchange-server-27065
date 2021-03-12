using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000051 RID: 81
	internal sealed class MailboxIneptException : TransientMailboxException
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000EBA7 File Offset: 0x0000CDA7
		public MailboxIneptException(Exception innerException) : base(LocalizedString.Empty, innerException, MailboxIneptException.schedule)
		{
		}

		// Token: 0x040001B5 RID: 437
		private static RetrySchedule schedule = new RetrySchedule(FinalAction.RetryForever, TimeSpan.MaxValue, new TimeSpan[]
		{
			TimeSpan.Zero,
			TimeSpan.FromSeconds(5.0),
			TimeSpan.FromMinutes(1.0),
			TimeSpan.FromMinutes(2.0),
			TimeSpan.FromMinutes(5.0),
			TimeSpan.FromMinutes(15.0),
			TimeSpan.FromHours(1.0)
		});
	}
}
