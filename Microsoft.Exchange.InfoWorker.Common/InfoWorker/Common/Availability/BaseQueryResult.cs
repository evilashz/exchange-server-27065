using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200006F RID: 111
	internal abstract class BaseQueryResult
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000D1B6 File Offset: 0x0000B3B6
		protected BaseQueryResult(LocalizedException exception)
		{
			this.exception = exception;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		protected BaseQueryResult()
		{
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000D1CD File Offset: 0x0000B3CD
		public LocalizedException ExceptionInfo
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040001BC RID: 444
		private LocalizedException exception;
	}
}
