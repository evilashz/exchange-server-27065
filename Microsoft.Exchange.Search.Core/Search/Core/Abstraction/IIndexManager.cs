using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000028 RID: 40
	internal interface IIndexManager
	{
		// Token: 0x060000D5 RID: 213
		void CreateCatalog(string indexName, string databasePath, bool databaseCopyActive, RefinerUsage refinersToEnable);

		// Token: 0x060000D6 RID: 214
		void RemoveCatalog(string indexName);

		// Token: 0x060000D7 RID: 215
		void FlushCatalog(string indexName);

		// Token: 0x060000D8 RID: 216
		bool CatalogExists(string indexName);

		// Token: 0x060000D9 RID: 217
		CatalogState GetCatalogState(string indexName, out string seedingSource, out int? failureCode, out string failureReason);

		// Token: 0x060000DA RID: 218
		HashSet<string> GetCatalogs();

		// Token: 0x060000DB RID: 219
		void UpdateConfiguration();

		// Token: 0x060000DC RID: 220
		bool EnsureCatalog(string indexName, bool databaseCopyActive, bool suspended, RefinerUsage refinersToEnable);

		// Token: 0x060000DD RID: 221
		string GetTransportIndexSystem();

		// Token: 0x060000DE RID: 222
		bool SuspendCatalog(string indexName);

		// Token: 0x060000DF RID: 223
		bool ResumeCatalog(string indexName);

		// Token: 0x060000E0 RID: 224
		string GetRootDirectory(string indexName);
	}
}
