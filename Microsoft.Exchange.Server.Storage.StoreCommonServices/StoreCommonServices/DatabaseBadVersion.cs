using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000075 RID: 117
	public class DatabaseBadVersion : StoreException
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x0001CE8D File Offset: 0x0001B08D
		public DatabaseBadVersion(LID lid, Guid mdbGuid, ComponentVersion expectedVersion, ComponentVersion foundVersion) : base(lid, ErrorCodeValue.DatabaseBadVersion, string.Format("Database with Guid {0} expected version {1} and found version {2}", mdbGuid, expectedVersion, foundVersion))
		{
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001CEB8 File Offset: 0x0001B0B8
		public DatabaseBadVersion(LID lid, Guid mdbGuid, ComponentVersion expectedVersion, ComponentVersion foundVersion, Exception innerException) : base(lid, ErrorCodeValue.DatabaseBadVersion, string.Format("Database with Guid {0} expected version {1} and found version {2}", mdbGuid, expectedVersion, foundVersion), innerException)
		{
		}

		// Token: 0x040003B5 RID: 949
		private const string DatabaseBadVersionMessage = "Database with Guid {0} expected version {1} and found version {2}";
	}
}
