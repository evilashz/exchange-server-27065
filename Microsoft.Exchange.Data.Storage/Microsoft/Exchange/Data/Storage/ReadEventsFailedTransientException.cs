using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200076C RID: 1900
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReadEventsFailedTransientException : StorageTransientException
	{
		// Token: 0x0600489F RID: 18591 RVA: 0x00131443 File Offset: 0x0012F643
		public ReadEventsFailedTransientException(LocalizedString message, Exception innerException, EventWatermark eventWatermark) : base(message, innerException)
		{
			this.eventWatermark = eventWatermark;
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x00131454 File Offset: 0x0012F654
		public ReadEventsFailedTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x060048A1 RID: 18593 RVA: 0x0013145E File Offset: 0x0012F65E
		public EventWatermark EventWatermark
		{
			get
			{
				return this.eventWatermark;
			}
		}

		// Token: 0x04002761 RID: 10081
		private readonly EventWatermark eventWatermark;
	}
}
