using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200076B RID: 1899
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReadEventsFailedException : StoragePermanentException
	{
		// Token: 0x0600489C RID: 18588 RVA: 0x00131420 File Offset: 0x0012F620
		public ReadEventsFailedException(LocalizedString message, Exception innerException, EventWatermark eventWatermark) : base(message, innerException)
		{
			this.eventWatermark = eventWatermark;
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x00131431 File Offset: 0x0012F631
		public ReadEventsFailedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x0600489E RID: 18590 RVA: 0x0013143B File Offset: 0x0012F63B
		public EventWatermark EventWatermark
		{
			get
			{
				return this.eventWatermark;
			}
		}

		// Token: 0x04002760 RID: 10080
		private readonly EventWatermark eventWatermark;
	}
}
