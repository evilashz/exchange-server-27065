using System;

namespace Microsoft.Exchange.Configuration.SQM
{
	// Token: 0x02000269 RID: 617
	internal class SqmErrorRecord
	{
		// Token: 0x0600155A RID: 5466 RVA: 0x0004EE95 File Offset: 0x0004D095
		internal SqmErrorRecord(string exceptionType, string errorId)
		{
			this.ExceptionType = exceptionType;
			this.ErrorId = errorId;
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0004EEAB File Offset: 0x0004D0AB
		// (set) Token: 0x0600155C RID: 5468 RVA: 0x0004EEB3 File Offset: 0x0004D0B3
		public string ExceptionType { get; private set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0004EEBC File Offset: 0x0004D0BC
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x0004EEC4 File Offset: 0x0004D0C4
		public string ErrorId { get; private set; }
	}
}
