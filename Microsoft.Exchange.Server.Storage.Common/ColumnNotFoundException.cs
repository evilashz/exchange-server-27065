using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000027 RID: 39
	public class ColumnNotFoundException : NonFatalDatabaseException
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00007BCA File Offset: 0x00005DCA
		public ColumnNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00007BD3 File Offset: 0x00005DD3
		public ColumnNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
