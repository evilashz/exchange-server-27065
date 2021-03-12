using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200007A RID: 122
	public class DatabaseLogicalCorruption : StoreException
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x0001CF61 File Offset: 0x0001B161
		public DatabaseLogicalCorruption(LID lid, Guid mdbGuid) : base(lid, ErrorCodeValue.CorruptStore, string.Format("Database with Guid {0} has logical corruption", mdbGuid))
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001CF7F File Offset: 0x0001B17F
		public DatabaseLogicalCorruption(LID lid, Guid mdbGuid, Exception innerException) : base(lid, ErrorCodeValue.CorruptStore, string.Format("Database with Guid {0} has logical corruption", mdbGuid), innerException)
		{
		}

		// Token: 0x040003B6 RID: 950
		private const string DatabaseLogicalCorruptionMessage = "Database with Guid {0} has logical corruption";
	}
}
