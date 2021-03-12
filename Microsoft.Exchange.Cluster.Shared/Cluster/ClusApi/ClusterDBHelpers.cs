using System;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000021 RID: 33
	public static class ClusterDBHelpers
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000635C File Offset: 0x0000455C
		public static void ReadServerDatabaseSchemaVersionRange(IClusterDB iClusterDB, Guid serverGuid, int defaultMinimum, int defaultMaximum, out int minVersion, out int maxVersion)
		{
			string keyName = string.Format("Exchange\\Servers\\{0}\\Schema", serverGuid);
			minVersion = iClusterDB.GetValue<int>(keyName, "Minimum Version", defaultMinimum);
			maxVersion = iClusterDB.GetValue<int>(keyName, "Maximum Version", defaultMaximum);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000639C File Offset: 0x0000459C
		public static void ReadServerDatabaseSchemaVersionRange(Guid serverGuid, int defaultMinimum, int defaultMaximum, out int minVersion, out int maxVersion)
		{
			string subkeyName = string.Format("Cluster\\Exchange\\Servers\\{0}\\Schema", serverGuid);
			minVersion = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, subkeyName, "Minimum Version", defaultMinimum);
			maxVersion = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, subkeyName, "Maximum Version", defaultMaximum);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000063EC File Offset: 0x000045EC
		public static void WriteServerDatabaseSchemaVersionRange(IClusterDB iClusterDB, Guid serverGuid, int minVersion, int maxVersion)
		{
			string registryKey = string.Format("Exchange\\Servers\\{0}\\Schema", serverGuid);
			using (IClusterDBWriteBatch clusterDBWriteBatch = iClusterDB.CreateWriteBatch(registryKey))
			{
				clusterDBWriteBatch.SetValue("Minimum Version", minVersion);
				clusterDBWriteBatch.SetValue("Maximum Version", maxVersion);
				clusterDBWriteBatch.Execute();
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000644C File Offset: 0x0000464C
		public static void RemoveServerSchemaVersionRange(IClusterDB iClusterDB, Guid serverGuid)
		{
			using (IClusterDBWriteBatch clusterDBWriteBatch = iClusterDB.CreateWriteBatch("Exchange\\Servers"))
			{
				clusterDBWriteBatch.DeleteKey(serverGuid.ToString());
				clusterDBWriteBatch.Execute();
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000649C File Offset: 0x0000469C
		public static void ReadRequestedDatabaseSchemaVersion(IClusterDB iClusterDB, Guid databaseGuid, int defaultVersion, out int requestedVersion)
		{
			string keyName = string.Format("Exchange\\Databases\\{0}\\Schema", databaseGuid);
			requestedVersion = iClusterDB.GetValue<int>(keyName, "Requested Version", defaultVersion);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000064CC File Offset: 0x000046CC
		public static void ReadRequestedDatabaseSchemaVersion(Guid databaseGuid, int defaultVersion, out int requestedVersion)
		{
			string subkeyName = string.Format("Exchange\\Databases\\{0}\\Schema", databaseGuid);
			requestedVersion = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, subkeyName, "Requested Version", defaultVersion);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006504 File Offset: 0x00004704
		public static void WriteRequestedDatabaseSchemaVersion(IClusterDB iClusterDB, Guid databaseGuid, int requestedVersion)
		{
			string registryKey = string.Format("Exchange\\Databases\\{0}\\Schema", databaseGuid);
			using (IClusterDBWriteBatch clusterDBWriteBatch = iClusterDB.CreateWriteBatch(registryKey))
			{
				clusterDBWriteBatch.SetValue("Requested Version", requestedVersion);
				clusterDBWriteBatch.Execute();
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006558 File Offset: 0x00004758
		public static void RemoveDatabaseRequestedSchemaVersion(IClusterDB iClusterDB, Guid databaseGuid)
		{
			using (IClusterDBWriteBatch clusterDBWriteBatch = iClusterDB.CreateWriteBatch("Exchange\\Databases"))
			{
				clusterDBWriteBatch.DeleteKey(databaseGuid.ToString());
				clusterDBWriteBatch.Execute();
			}
		}

		// Token: 0x0400004F RID: 79
		private const string ServerClusterDBRootPath = "Exchange\\Servers";

		// Token: 0x04000050 RID: 80
		private const string ServerClusterDBPath = "Exchange\\Servers\\{0}\\Schema";

		// Token: 0x04000051 RID: 81
		private const string ServerClusterDBRegistryPath = "Cluster\\Exchange\\Servers\\{0}\\Schema";

		// Token: 0x04000052 RID: 82
		private const string DatabaseClusterDBRootPath = "Exchange\\Databases";

		// Token: 0x04000053 RID: 83
		private const string DatabaseClusterDBPath = "Exchange\\Databases\\{0}\\Schema";

		// Token: 0x04000054 RID: 84
		private const string DatabaseClusterDBRegistryPath = "Cluster\\Exchange\\Databases\\{0}\\Schema";

		// Token: 0x04000055 RID: 85
		private const string MinimumVersion = "Minimum Version";

		// Token: 0x04000056 RID: 86
		private const string MaximumVersion = "Maximum Version";

		// Token: 0x04000057 RID: 87
		private const string RequestedVersion = "Requested Version";
	}
}
