using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000026 RID: 38
	public class DuplicateColumnException : NonFatalDatabaseException
	{
		// Token: 0x060002CD RID: 717 RVA: 0x00007BB7 File Offset: 0x00005DB7
		public DuplicateColumnException(string message) : base(message)
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00007BC0 File Offset: 0x00005DC0
		public DuplicateColumnException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
