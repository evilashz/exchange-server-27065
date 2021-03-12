using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.ReplicaSeeder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200029E RID: 670
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EseDatabaseBackupReader : SafeBackupContextHandle
	{
		// Token: 0x06001A28 RID: 6696 RVA: 0x0006DC0A File Offset: 0x0006BE0A
		public EseDatabaseBackupReader(string dbName, Guid dbGuid) : base(dbName, dbGuid)
		{
			this.m_backupFileHandle = IntPtr.Zero;
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x0006DC1F File Offset: 0x0006BE1F
		public long SourceFileSizeBytes
		{
			get
			{
				return this.m_sourceFileSizeBytes;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x0006DC27 File Offset: 0x0006BE27
		public IntPtr BackupContextHandle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0006DC2F File Offset: 0x0006BE2F
		public IntPtr BackupFileHandle
		{
			get
			{
				return this.m_backupFileHandle;
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0006DC38 File Offset: 0x0006BE38
		protected override bool ReleaseHandle()
		{
			bool result = true;
			if (!this.IsInvalid)
			{
				if (this.m_backupFileHandle != IntPtr.Zero)
				{
					int num = CReplicaSeederInterop.CloseBackupFileHandle(this.handle, this.m_backupFileHandle);
					this.m_backupFileHandle = IntPtr.Zero;
					if (num != 0)
					{
						ExTraceGlobals.SeederServerTracer.TraceError<string, int>((long)this.GetHashCode(), "CloseBackupFileHandle() for database '{0}' failed with error code {1}", base.DatabaseName, num);
						ReplayCrimsonEvents.BackupHandleCloseFailed.Log<Guid, string, string>(base.DatabaseGuid, base.DatabaseName, string.Format("CloseBackupFileHandle() failed with error code {0}", num));
					}
				}
				result = base.ReleaseHandle();
				base.SetHandleAsInvalid();
				EseDatabaseBackupReader.CleanupNativeLogger();
			}
			return result;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0006DCDC File Offset: 0x0006BEDC
		public void SetLastError(int ec)
		{
			this.m_ecLast = ec;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0006DCE8 File Offset: 0x0006BEE8
		public int PerformDatabaseRead(long readOffset, byte[] buffer, int bytesToRead)
		{
			int num = bytesToRead;
			int num2 = CReplicaSeederInterop.PerformDatabaseRead(this.handle, this.m_backupFileHandle, readOffset, buffer, ref num);
			if (num2 != 0)
			{
				if (num2 == 38)
				{
					ExTraceGlobals.SeederServerTracer.TraceError<string, int>((long)this.GetHashCode(), "Reading the database '{0}' encountered the end of the file: {1}.", base.DatabaseName, num2);
				}
				else
				{
					ExTraceGlobals.SeederServerTracer.TraceError<string, int>((long)this.GetHashCode(), "Reading the database '{0}' encountered an error: {1}.", base.DatabaseName, num2);
				}
				throw new IOException(string.Format("EseDatabaseBackupReader: PerformDatabaseRead failed with error code 0x{0:X}. Expected {1} bytes read, but actually only {2} were read.", num2, bytesToRead, num));
			}
			return num;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0006DD74 File Offset: 0x0006BF74
		public static EseDatabaseBackupReader GetESEDatabaseBackupReader(string serverName, string dbName, Guid dbGuid, string transferAddress, string sourceFileToBackupFullPath, uint readHintSizeBytes)
		{
			bool flag = false;
			int num = 0;
			IntPtr value = new IntPtr(-1);
			EseDatabaseBackupReader eseDatabaseBackupReader = new EseDatabaseBackupReader(dbName, dbGuid);
			SafeBackupContextHandle safeBackupContextHandle = eseDatabaseBackupReader;
			EseDatabaseBackupReader result;
			try
			{
				EseDatabaseBackupReader.SetupNativeLogger();
				SafeBackupContextHandle.GetAndSetIntPtrInCER(serverName, dbName, transferAddress, ref safeBackupContextHandle);
				flag = true;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					num = CReplicaSeederInterop.OpenBackupFileHandle(eseDatabaseBackupReader.handle, sourceFileToBackupFullPath, readHintSizeBytes, out eseDatabaseBackupReader.m_backupFileHandle, out eseDatabaseBackupReader.m_sourceFileSizeBytes);
				}
				if (num != 0 || eseDatabaseBackupReader.m_backupFileHandle == IntPtr.Zero || eseDatabaseBackupReader.m_backupFileHandle == value)
				{
					eseDatabaseBackupReader.Close();
					throw new FailedToOpenBackupFileHandleException(dbName, serverName, num, SeedHelper.TranslateSeederErrorCode((long)num, serverName));
				}
				result = eseDatabaseBackupReader;
			}
			finally
			{
				if (!flag)
				{
					EseDatabaseBackupReader.CleanupNativeLogger();
				}
			}
			return result;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0006DE34 File Offset: 0x0006C034
		public static void SetupNativeLogger()
		{
			CReplicaSeederInterop.SetupNativeLogger();
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0006DE3B File Offset: 0x0006C03B
		public static void CleanupNativeLogger()
		{
			CReplicaSeederInterop.CleanupNativeLogger();
		}

		// Token: 0x04000A7E RID: 2686
		private IntPtr m_backupFileHandle;

		// Token: 0x04000A7F RID: 2687
		private long m_sourceFileSizeBytes;
	}
}
