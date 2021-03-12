using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200000C RID: 12
	internal class DatabaseCache
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00005B18 File Offset: 0x00003D18
		private DatabaseCache(IDiagnosticsSession diagnosticsSession)
		{
			this.existanceDictionary = new Dictionary<Guid, bool>(40);
			this.diagnosticsSession = diagnosticsSession;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00005B34 File Offset: 0x00003D34
		public static DatabaseCache Create(IDiagnosticsSession diagnosticsSession)
		{
			return new DatabaseCache(diagnosticsSession);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00005B3C File Offset: 0x00003D3C
		public bool DatabaseExists(Guid mdbGuid)
		{
			if (ExEnvironment.IsTest)
			{
				return this.GetDatabaseExistance(mdbGuid);
			}
			bool databaseExistance;
			lock (this.existanceDictionary)
			{
				if (!this.existanceDictionary.TryGetValue(mdbGuid, out databaseExistance))
				{
					databaseExistance = this.GetDatabaseExistance(mdbGuid);
					this.existanceDictionary[mdbGuid] = databaseExistance;
				}
			}
			return databaseExistance;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00005BAC File Offset: 0x00003DAC
		private bool GetDatabaseExistance(Guid guid)
		{
			AdDataProvider adDataProvider = AdDataProvider.Create(this.diagnosticsSession);
			Database database = adDataProvider.FindDatabase(guid);
			return database != null;
		}

		// Token: 0x04000038 RID: 56
		private readonly Dictionary<Guid, bool> existanceDictionary;

		// Token: 0x04000039 RID: 57
		private readonly IDiagnosticsSession diagnosticsSession;
	}
}
