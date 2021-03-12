using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000029 RID: 41
	public class DatabaseSchemaBroken : Exception
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000EB98 File Offset: 0x0000CD98
		public DatabaseSchemaBroken(string mdbName, string errorDetails) : base(string.Format("Database {0} has a broken schema. Error details are {1}", mdbName, errorDetails))
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		public DatabaseSchemaBroken(string mdbName, string errorDetails, Exception innerException) : base(string.Format("Database {0} has a broken schema. Error details are {1}", mdbName, errorDetails), innerException)
		{
		}

		// Token: 0x040000B6 RID: 182
		private const string DatabaseSchemaBrokenMessage = "Database {0} has a broken schema. Error details are {1}";
	}
}
