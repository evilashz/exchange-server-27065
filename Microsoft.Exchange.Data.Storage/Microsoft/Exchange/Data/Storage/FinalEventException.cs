using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200072E RID: 1838
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FinalEventException : StoragePermanentException
	{
		// Token: 0x060047D6 RID: 18390 RVA: 0x0013062D File Offset: 0x0012E82D
		public FinalEventException(FinalEventException innerException) : base(innerException.LocalizedString, innerException)
		{
			this.finalEvent = innerException.FinalEvent;
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x00130648 File Offset: 0x0012E848
		public FinalEventException(Event finalEvent) : base(ServerStrings.ExFinalEventFound(finalEvent.ToString()))
		{
			this.finalEvent = finalEvent;
		}

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x060047D8 RID: 18392 RVA: 0x00130662 File Offset: 0x0012E862
		public Event FinalEvent
		{
			get
			{
				return this.finalEvent;
			}
		}

		// Token: 0x04002734 RID: 10036
		private readonly Event finalEvent;
	}
}
