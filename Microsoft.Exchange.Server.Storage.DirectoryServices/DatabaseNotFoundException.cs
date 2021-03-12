using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x02000002 RID: 2
	public class DatabaseNotFoundException : StoreException
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public DatabaseNotFoundException(LID lid, Guid databaseId) : base(lid, ErrorCodeValue.NotFound)
		{
			this.databaseId = databaseId;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E5 File Offset: 0x000002E5
		public DatabaseNotFoundException(LID lid, Guid databaseId, Exception innerException) : base(lid, ErrorCodeValue.NotFound, string.Empty, innerException)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020F9 File Offset: 0x000002F9
		public Guid DatabaseId
		{
			get
			{
				return this.databaseId;
			}
		}

		// Token: 0x04000001 RID: 1
		private Guid databaseId;
	}
}
