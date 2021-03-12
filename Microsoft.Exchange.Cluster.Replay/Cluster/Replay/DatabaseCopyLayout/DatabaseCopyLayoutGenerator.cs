using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay.DatabaseCopyLayout
{
	// Token: 0x02000174 RID: 372
	public class DatabaseCopyLayoutGenerator
	{
		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003FF88 File Offset: 0x0003E188
		public DatabaseCopyLayoutGenerator(int serversPerDag, int databasesPerDag, int databaseDrivesPerServer, int databasesPerDrive, int copiesPerDatabase, int numberOfExtraCopiesOnSpares = 0, string dbNamePrefix = "", string dbNumberFormatSpecifier = "D")
		{
			this.m_serversPerDag = serversPerDag;
			this.m_databasesPerDag = databasesPerDag;
			this.m_databaseDrivesPerServer = databaseDrivesPerServer;
			this.m_databasesPerDrive = databasesPerDrive;
			this.m_copiesPerDatabase = copiesPerDatabase;
			this.m_numberOfExtraCopiesOnSpares = numberOfExtraCopiesOnSpares;
			this.m_copyLayoutEntry = new DatabaseCopyLayoutEntry(dbNamePrefix, dbNumberFormatSpecifier);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003FFD8 File Offset: 0x0003E1D8
		public Dictionary<int, List<DatabaseGroupLayoutEntry>> GenerateDatabaseCopyLayoutForDag()
		{
			return CopyLayoutGenerator.GenerateCopyLayoutTable(this.m_copyLayoutEntry, this.m_serversPerDag, this.m_databasesPerDag, this.m_databaseDrivesPerServer, this.m_databasesPerDrive, this.m_copiesPerDatabase, this.m_numberOfExtraCopiesOnSpares);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00040018 File Offset: 0x0003E218
		public List<DatabaseGroupLayoutEntry> GenerateDatabaseCopyLayoutForServer(int serverIndex)
		{
			Dictionary<int, List<DatabaseGroupLayoutEntry>> dictionary = this.GenerateDatabaseCopyLayoutForDag();
			if (dictionary != null)
			{
				return dictionary[serverIndex];
			}
			return null;
		}

		// Token: 0x04000626 RID: 1574
		private readonly int m_serversPerDag;

		// Token: 0x04000627 RID: 1575
		private readonly int m_databasesPerDag;

		// Token: 0x04000628 RID: 1576
		private readonly int m_databaseDrivesPerServer;

		// Token: 0x04000629 RID: 1577
		private readonly int m_databasesPerDrive;

		// Token: 0x0400062A RID: 1578
		private readonly int m_copiesPerDatabase;

		// Token: 0x0400062B RID: 1579
		private readonly int m_numberOfExtraCopiesOnSpares;

		// Token: 0x0400062C RID: 1580
		private DatabaseCopyLayoutEntry m_copyLayoutEntry;
	}
}
