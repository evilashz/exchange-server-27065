using System;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.Exchange.Cluster.ReplicaSeeder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000E7 RID: 231
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class EseDatabaseReader : SafeBackupContextHandle, IEseDatabaseReader, IDisposable
	{
		// Token: 0x06000950 RID: 2384 RVA: 0x0002BB13 File Offset: 0x00029D13
		internal static IEseDatabaseReader GetEseDatabaseReader(string serverName, Guid dbGuid, string dbName, string dbPath)
		{
			return EseDatabaseReader.hookableFactory.Value(serverName, dbGuid, dbName, dbPath);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002BB28 File Offset: 0x00029D28
		internal static IDisposable SetTestHook(Func<string, Guid, string, string, IEseDatabaseReader> newFactory)
		{
			return EseDatabaseReader.hookableFactory.SetTestHook(newFactory);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0002BB35 File Offset: 0x00029D35
		private EseDatabaseReader(string dbName, Guid dbGuid) : base(dbName, dbGuid)
		{
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002BB40 File Offset: 0x00029D40
		private static EseDatabaseReader BuildEseDatabaseReader(string serverName, Guid dbGuid, string dbName, string dbPath)
		{
			EseDatabaseReader eseDatabaseReader = new EseDatabaseReader(dbName, dbGuid);
			SafeBackupContextHandle safeBackupContextHandle = eseDatabaseReader;
			SafeBackupContextHandle.GetAndSetIntPtrInCER(serverName, dbName, null, ref safeBackupContextHandle);
			bool flag = false;
			try
			{
				JET_DBINFOMISC jet_DBINFOMISC;
				int databaseInfo = CReplicaSeederInterop.GetDatabaseInfo(eseDatabaseReader.handle, dbPath, out jet_DBINFOMISC);
				if (databaseInfo != 0)
				{
					throw new FailedToGetDatabaseInfo(databaseInfo);
				}
				eseDatabaseReader.m_pageSize = (long)jet_DBINFOMISC.cbPageSize;
				eseDatabaseReader.m_dbPath = dbPath;
				if (jet_DBINFOMISC.cbPageSize != 4096 && jet_DBINFOMISC.cbPageSize != 8192 && jet_DBINFOMISC.cbPageSize != 32768)
				{
					throw new UnExpectedPageSizeException(dbPath, (long)jet_DBINFOMISC.cbPageSize);
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					eseDatabaseReader.ReleaseHandle();
				}
			}
			return eseDatabaseReader;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002BBE8 File Offset: 0x00029DE8
		public static IEseDatabaseReader GetRemoteEseDatabaseReader(string serverName, Guid databaseGuid, bool useClassicEseback)
		{
			string text;
			string text2;
			SeedHelper.GetDatabaseNameAndPath(databaseGuid, out text, out text2);
			if (useClassicEseback)
			{
				return EseDatabaseReader.GetEseDatabaseReader(serverName, databaseGuid, text, text2);
			}
			return new RemoteEseDatabaseReader(serverName, databaseGuid, text, text2);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002BC15 File Offset: 0x00029E15
		public void ForceNewLog()
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "rolling a log file");
			CReplicaSeederInterop.ForceNewLog(this.handle);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0002BC3C File Offset: 0x00029E3C
		public byte[] ReadOnePage(long pageNumber, out long lowGen, out long highGen)
		{
			if (pageNumber < 1L)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, "pageNumber is {0}, must be >= 1 ", new object[]
				{
					pageNumber
				}));
			}
			byte[] result = new byte[this.m_pageSize];
			long num2;
			int num = CReplicaSeederInterop.OnlineGetDatabasePages(this.handle, this.m_dbPath, (ulong)pageNumber, 1UL, (ulong)this.m_pageSize, out result, out num2, out lowGen, out highGen);
			if (num == -4001)
			{
				throw new JetErrorFileIOBeyondEOFException(pageNumber.ToString());
			}
			if (num != 0)
			{
				throw new FailedToReadDatabasePage(num);
			}
			DiagCore.RetailAssert(num2 == this.m_pageSize, "cRead {0} != m_pageSize {1}", new object[]
			{
				num2,
				this.m_pageSize
			});
			return result;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0002BCF8 File Offset: 0x00029EF8
		public long PageSize
		{
			get
			{
				return this.m_pageSize;
			}
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002BD00 File Offset: 0x00029F00
		public long ReadPageSize()
		{
			return this.m_pageSize;
		}

		// Token: 0x040003DE RID: 990
		private static Hookable<Func<string, Guid, string, string, IEseDatabaseReader>> hookableFactory = Hookable<Func<string, Guid, string, string, IEseDatabaseReader>>.Create(false, new Func<string, Guid, string, string, IEseDatabaseReader>(EseDatabaseReader.BuildEseDatabaseReader));

		// Token: 0x040003DF RID: 991
		private long m_pageSize;

		// Token: 0x040003E0 RID: 992
		private string m_dbPath;
	}
}
