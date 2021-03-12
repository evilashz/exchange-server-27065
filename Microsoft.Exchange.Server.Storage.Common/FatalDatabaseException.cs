using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000028 RID: 40
	public class FatalDatabaseException : Exception
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x00007BDD File Offset: 0x00005DDD
		public FatalDatabaseException(string message) : base(message)
		{
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00007BE6 File Offset: 0x00005DE6
		public FatalDatabaseException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
