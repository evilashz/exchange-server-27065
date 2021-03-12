using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000056 RID: 86
	internal sealed class ServerIneptException : TransientServerException
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		public ServerIneptException(Exception innerException) : base(LocalizedString.Empty, innerException, ServerIneptException.schedule)
		{
		}

		// Token: 0x040001B7 RID: 439
		private static RetrySchedule schedule = new RetrySchedule(FinalAction.RetryForever, TimeSpan.MaxValue, new TimeSpan[]
		{
			TimeSpan.Zero,
			TimeSpan.FromSeconds(5.0),
			TimeSpan.FromMinutes(1.0),
			TimeSpan.FromMinutes(2.0),
			TimeSpan.FromMinutes(5.0)
		});
	}
}
