using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000025 RID: 37
	public class DuplicateKeyException : NonFatalDatabaseException
	{
		// Token: 0x060002CB RID: 715 RVA: 0x00007BA4 File Offset: 0x00005DA4
		public DuplicateKeyException(string message) : base(message)
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00007BAD File Offset: 0x00005DAD
		public DuplicateKeyException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
