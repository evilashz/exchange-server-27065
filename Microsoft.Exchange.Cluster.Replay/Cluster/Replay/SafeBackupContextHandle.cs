using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.ReplicaSeeder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000E6 RID: 230
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal abstract class SafeBackupContextHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x0002B9CA File Offset: 0x00029BCA
		protected SafeBackupContextHandle(string dbName, Guid dbGuid) : base(true)
		{
			this.m_dbName = dbName;
			this.m_dbGuid = dbGuid;
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0002B9EC File Offset: 0x00029BEC
		public string DatabaseName
		{
			get
			{
				return this.m_dbName;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0002B9F4 File Offset: 0x00029BF4
		public Guid DatabaseGuid
		{
			get
			{
				return this.m_dbGuid;
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0002B9FC File Offset: 0x00029BFC
		protected static void GetAndSetIntPtrInCER(string serverName, string dbName, string transferAddress, ref SafeBackupContextHandle backupHandle)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr value = new IntPtr(-1);
			bool flag = false;
			int num = 0;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				num = backupHandle.GetBackupContextIntPtr(serverName, dbName, transferAddress, out zero);
				flag = (num == 0 && zero != IntPtr.Zero && zero != value);
				if (flag)
				{
					backupHandle.SetHandle(zero);
				}
			}
			if (!flag)
			{
				throw new FailedToOpenBackupFileHandleException(dbName, serverName, num, SeedHelper.TranslateSeederErrorCode((long)num, serverName));
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0002BA7C File Offset: 0x00029C7C
		protected override bool ReleaseHandle()
		{
			int num = CReplicaSeederInterop.CloseBackupContext(this.handle, this.m_ecLast);
			if (num != 0)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<string, int>((long)this.GetHashCode(), "CloseBackupContext() for database '{0}' failed with error code {1}", this.DatabaseName, num);
				ReplayCrimsonEvents.BackupHandleCloseFailed.Log<Guid, string, string>(this.DatabaseGuid, this.DatabaseName, string.Format("CloseBackupContext() failed with error code {0}", num));
			}
			return num == 0;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0002BAE8 File Offset: 0x00029CE8
		protected int GetBackupContextIntPtr(string serverName, string dbName, string transferAddress, out IntPtr hContext)
		{
			hContext = IntPtr.Zero;
			return CReplicaSeederInterop.OpenBackupContext(serverName, dbName, transferAddress, this.m_dbGuid, out hContext);
		}

		// Token: 0x040003DB RID: 987
		protected int m_ecLast;

		// Token: 0x040003DC RID: 988
		private readonly string m_dbName;

		// Token: 0x040003DD RID: 989
		private readonly Guid m_dbGuid;
	}
}
