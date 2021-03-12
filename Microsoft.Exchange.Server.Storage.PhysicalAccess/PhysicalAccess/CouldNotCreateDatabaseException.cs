using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000028 RID: 40
	public class CouldNotCreateDatabaseException : Exception
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000EB85 File Offset: 0x0000CD85
		public CouldNotCreateDatabaseException(string message) : base(message)
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000EB8E File Offset: 0x0000CD8E
		public CouldNotCreateDatabaseException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
