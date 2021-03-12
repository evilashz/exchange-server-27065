using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000D0 RID: 208
	internal static class DatabaseLocation
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x0002E958 File Offset: 0x0002CB58
		public static string GetConnectionString(string databaseName)
		{
			databaseName = databaseName.Trim();
			databaseName = databaseName.Replace(';', '_');
			databaseName = databaseName.Replace('[', '_');
			databaseName = databaseName.Replace(']', '_');
			databaseName = databaseName.Replace("--", "_");
			databaseName = databaseName.Substring(0, (databaseName.Length > 128) ? 128 : databaseName.Length);
			return string.Format("Trusted_Connection=yes;Integrated Security=SSPI;packet size=4096;Data Source={0}", ".") + string.Format(";Database={0}", databaseName) + ";Application Name='Exchange';Connect Timeout=6000;Max Pool Size=1000;MultipleActiveResultSets=true";
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002E9EA File Offset: 0x0002CBEA
		public static string GetMasterConnectionString()
		{
			return DatabaseLocation.GetConnectionString("master");
		}

		// Token: 0x0400032F RID: 815
		private const string DefaultDataSource = ".";

		// Token: 0x04000330 RID: 816
		private const string ConnectionStringFormat = "Trusted_Connection=yes;Integrated Security=SSPI;packet size=4096;Data Source={0}";

		// Token: 0x04000331 RID: 817
		private const string DatabaseStringFormat = ";Database={0}";

		// Token: 0x04000332 RID: 818
		private const string DefaultMasterDatabase = "master";
	}
}
