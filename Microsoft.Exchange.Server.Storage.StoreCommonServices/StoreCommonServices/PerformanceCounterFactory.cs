using System;
using System.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000EB RID: 235
	internal static class PerformanceCounterFactory
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0002B94A File Offset: 0x00029B4A
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x0002B95A File Offset: 0x00029B5A
		public static string DefaultDatabaseInstanceName
		{
			get
			{
				if (Globals.IsMultiProcess)
				{
					return PerformanceCounterFactory.defaultDatabaseInstanceName;
				}
				return null;
			}
			set
			{
				PerformanceCounterFactory.defaultDatabaseInstanceName = value;
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002B964 File Offset: 0x00029B64
		public static StorePerDatabasePerformanceCountersInstance GetDatabaseInstance(StoreDatabase database)
		{
			if (database == null)
			{
				if (Globals.IsMultiProcess)
				{
					if (PerformanceCounterFactory.cachedDefaultDatabaseInstance == null)
					{
						string text = PerformanceCounterFactory.DefaultDatabaseInstanceName;
						if (text != null)
						{
							PerformanceCounterFactory.cachedDefaultDatabaseInstance = StorePerDatabasePerformanceCounters.GetInstance(text);
						}
					}
					if (PerformanceCounterFactory.cachedDefaultDatabaseInstance != null)
					{
						return PerformanceCounterFactory.cachedDefaultDatabaseInstance;
					}
				}
				return StorePerDatabasePerformanceCounters.TotalInstance;
			}
			if (database.CachedStorePerDatabasePerformanceCountersInstance == null)
			{
				database.CachedStorePerDatabasePerformanceCountersInstance = StorePerDatabasePerformanceCounters.GetInstance(database.MdbName);
			}
			return database.CachedStorePerDatabasePerformanceCountersInstance;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0002B9C8 File Offset: 0x00029BC8
		public static StorePerDatabasePerformanceCountersInstance CreateDatabaseInstance(StoreDatabase database)
		{
			StorePerDatabasePerformanceCountersInstance instance = StorePerDatabasePerformanceCounters.GetInstance(database.MdbName);
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				if (instance.ProcessId.RawValue != (long)currentProcess.Id)
				{
					PerformanceCounterFactory.CloseDatabaseInstance(database);
					instance = StorePerDatabasePerformanceCounters.GetInstance(database.MdbName);
					StorePerDatabasePerformanceCounters.ResetInstance(instance.Name);
					instance.ProcessId.RawValue = (long)currentProcess.Id;
					instance.PercentRPCsInProgressBase.RawValue = (long)((ulong)Globals.MaxRPCThreadCount);
				}
			}
			database.CachedStorePerDatabasePerformanceCountersInstance = instance;
			return database.CachedStorePerDatabasePerformanceCountersInstance;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002BA64 File Offset: 0x00029C64
		public static void CloseDatabaseInstance(StoreDatabase database)
		{
			StorePerDatabasePerformanceCounters.RemoveInstance(database.MdbName);
			database.CachedStorePerDatabasePerformanceCountersInstance = null;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002BA78 File Offset: 0x00029C78
		public static StorePerClientTypePerformanceCountersInstance GetClientTypeInstance(ClientType clientType)
		{
			if (clientType < (ClientType)0 || (int)clientType >= PerformanceCounterFactory.perClientTypeCacheInstances.Length)
			{
				return null;
			}
			if (PerformanceCounterFactory.perClientTypeCacheInstances[(int)clientType] == null)
			{
				string instanceName = PerformanceCounterFactory.CreateClientTypeInstanceName(clientType);
				PerformanceCounterFactory.perClientTypeCacheInstances[(int)clientType] = StorePerClientTypePerformanceCounters.GetInstance(instanceName);
			}
			return PerformanceCounterFactory.perClientTypeCacheInstances[(int)clientType];
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002BABC File Offset: 0x00029CBC
		public static void CreateClientTypeInstances(bool reset)
		{
			for (int i = 1; i < 42; i++)
			{
				string instanceName = PerformanceCounterFactory.CreateClientTypeInstanceName((ClientType)i);
				StorePerClientTypePerformanceCountersInstance instance = StorePerClientTypePerformanceCounters.GetInstance(instanceName);
				if (reset)
				{
					StorePerClientTypePerformanceCounters.ResetInstance(instanceName);
				}
				PerformanceCounterFactory.perClientTypeCacheInstances[i] = instance;
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0002BAF8 File Offset: 0x00029CF8
		public static void CloseClientTypeInstances()
		{
			for (int i = 1; i < 42; i++)
			{
				if (PerformanceCounterFactory.perClientTypeCacheInstances[i] != null)
				{
					StorePerClientTypePerformanceCounters.RemoveInstance(PerformanceCounterFactory.CreateClientTypeInstanceName((ClientType)i));
					PerformanceCounterFactory.perClientTypeCacheInstances[i] = null;
				}
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0002BB2F File Offset: 0x00029D2F
		private static string CreateClientTypeInstanceName(ClientType clientType)
		{
			return clientType.ToString();
		}

		// Token: 0x04000545 RID: 1349
		private static StorePerClientTypePerformanceCountersInstance[] perClientTypeCacheInstances = new StorePerClientTypePerformanceCountersInstance[42];

		// Token: 0x04000546 RID: 1350
		private static string defaultDatabaseInstanceName = null;

		// Token: 0x04000547 RID: 1351
		private static StorePerDatabasePerformanceCountersInstance cachedDefaultDatabaseInstance = null;

		// Token: 0x04000548 RID: 1352
		public static string ProcessInstanceName = Process.GetCurrentProcess().ProcessName;
	}
}
