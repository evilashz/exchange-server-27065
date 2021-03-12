using System;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.MessageSecurity.EdgeSync;

namespace Microsoft.Exchange.EdgeSync.Datacenter
{
	// Token: 0x02000005 RID: 5
	internal class FileLeaseManager
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000027F4 File Offset: 0x000009F4
		public FileLeaseManager(string leaseFileName, string primaryLeaseLocation, string backupLeaseLocation, EnhancedTimeSpan syncInterval, EdgeSyncLogSession logSession, Trace tracer)
		{
			this.logSession = logSession;
			this.tracer = tracer;
			this.interSiteLeaseExpiryInterval = 2L * syncInterval;
			this.primaryLeaseFilePath = Path.Combine(primaryLeaseLocation, leaseFileName);
			this.backupLeaseFilePath = Path.Combine(backupLeaseLocation, leaseFileName);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002840 File Offset: 0x00000A40
		public static FileLeaseManager.LeaseOperationResult TryRunLeaseOperation(FileLeaseManager.LeaseOperation leaseOperation, FileLeaseManager.LeaseOperationRequest request)
		{
			Exception e = null;
			for (int i = 0; i < 10; i++)
			{
				e = null;
				if (i > 0)
				{
					Thread.Sleep(50);
				}
				try
				{
					return leaseOperation(request);
				}
				catch (FileNotFoundException ex)
				{
					e = ex;
				}
				catch (DirectoryNotFoundException ex2)
				{
					e = ex2;
				}
				catch (IOException ex3)
				{
					e = ex3;
				}
				catch (UnauthorizedAccessException ex4)
				{
					e = ex4;
				}
			}
			return new FileLeaseManager.LeaseOperationResult(e);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028CC File Offset: 0x00000ACC
		public static FileLeaseManager.LeaseOperationResult GetLeaseOperation(FileLeaseManager.LeaseOperationRequest request)
		{
			FileLeaseManager.LeaseOperationResult result;
			using (FileStream fileStream = new FileStream(request.LeasePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
			{
				byte[] array = new byte[fileStream.Length];
				int count = fileStream.Read(array, 0, array.Length);
				string @string = Encoding.ASCII.GetString(array, 0, count);
				result = new FileLeaseManager.LeaseOperationResult(LeaseToken.Parse(@string));
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000293C File Offset: 0x00000B3C
		public LeaseToken GetLease()
		{
			FileLeaseManager.LeaseOperationResult leaseOperationResult = FileLeaseManager.TryRunLeaseOperation(new FileLeaseManager.LeaseOperation(FileLeaseManager.GetLeaseOperation), new FileLeaseManager.LeaseOperationRequest(this.primaryLeaseFilePath));
			if (leaseOperationResult.Succeeded)
			{
				if (this.useBackupLeaseLocation)
				{
					this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, null, "Switch back to primary lease from backup lease");
				}
				this.useBackupLeaseLocation = false;
				return leaseOperationResult.ResultToken;
			}
			this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, leaseOperationResult.Exception, "Failed to open primary lease file. Switch to backup lease file");
			leaseOperationResult = FileLeaseManager.TryRunLeaseOperation(new FileLeaseManager.LeaseOperation(FileLeaseManager.GetLeaseOperation), new FileLeaseManager.LeaseOperationRequest(this.backupLeaseFilePath));
			if (leaseOperationResult.Succeeded)
			{
				this.logSession.LogEvent(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, null, "Successfully failed over to backup lease file");
				this.useBackupLeaseLocation = true;
				return leaseOperationResult.ResultToken;
			}
			this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, leaseOperationResult.Exception, "Failed to open backup lease file");
			throw new ExDirectoryException(leaseOperationResult.Exception);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A18 File Offset: 0x00000C18
		public bool CanTakeOverLease(bool force, LeaseToken lease, DateTime now, HashSet<string> localSiteHubs, out bool siteChanged)
		{
			siteChanged = !localSiteHubs.Contains(lease.Path);
			if (force)
			{
				return true;
			}
			if (!siteChanged)
			{
				return lease.Expiry < now;
			}
			bool flag = lease.Expiry + this.interSiteLeaseExpiryInterval < now;
			this.tracer.TraceDebug((long)this.GetHashCode(), "{0} over out of site lease {1}, Expiry {2}, TimeNow {3}", new object[]
			{
				flag ? "Took" : "Did not take",
				lease.Path,
				lease.Expiry,
				now
			});
			return flag;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public void SetLease(LeaseToken leaseToken)
		{
			LeaseToken token = new LeaseToken(leaseToken.Path, leaseToken.Expiry, leaseToken.Type, leaseToken.LastSync, leaseToken.Expiry + this.interSiteLeaseExpiryInterval + FileLeaseManager.LeaseExpiryCriticalAlertPadding, leaseToken.Version);
			FileLeaseManager.LeaseOperationResult leaseOperationResult = FileLeaseManager.TryRunLeaseOperation(new FileLeaseManager.LeaseOperation(this.SetLeaseOperation), new FileLeaseManager.LeaseOperationRequest(token));
			if (!leaseOperationResult.Succeeded)
			{
				this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.TargetConnection, leaseOperationResult.Exception, "Sync failed because Edgesync failed to update lease file");
				throw new ExDirectoryException(leaseOperationResult.Exception);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002B60 File Offset: 0x00000D60
		private FileLeaseManager.LeaseOperationResult SetLeaseOperation(FileLeaseManager.LeaseOperationRequest request)
		{
			string path = this.useBackupLeaseLocation ? this.backupLeaseFilePath : this.primaryLeaseFilePath;
			FileLeaseManager.LeaseOperationResult result;
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				byte[] bytes = Encoding.ASCII.GetBytes(request.Token.StringForm);
				fileStream.Write(bytes, 0, bytes.Length);
				fileStream.Flush();
				result = new FileLeaseManager.LeaseOperationResult();
			}
			return result;
		}

		// Token: 0x0400000A RID: 10
		private const int LeaseRetryLimit = 10;

		// Token: 0x0400000B RID: 11
		private static readonly TimeSpan LeaseExpiryCriticalAlertPadding = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400000C RID: 12
		private readonly string primaryLeaseFilePath;

		// Token: 0x0400000D RID: 13
		private readonly string backupLeaseFilePath;

		// Token: 0x0400000E RID: 14
		private readonly EnhancedTimeSpan interSiteLeaseExpiryInterval;

		// Token: 0x0400000F RID: 15
		private bool useBackupLeaseLocation;

		// Token: 0x04000010 RID: 16
		private EdgeSyncLogSession logSession;

		// Token: 0x04000011 RID: 17
		private Trace tracer;

		// Token: 0x02000006 RID: 6
		// (Invoke) Token: 0x06000027 RID: 39
		public delegate FileLeaseManager.LeaseOperationResult LeaseOperation(FileLeaseManager.LeaseOperationRequest request);

		// Token: 0x02000007 RID: 7
		public class LeaseOperationRequest
		{
			// Token: 0x0600002A RID: 42 RVA: 0x00002BF1 File Offset: 0x00000DF1
			public LeaseOperationRequest(string leasePath)
			{
				this.leasePath = leasePath;
			}

			// Token: 0x0600002B RID: 43 RVA: 0x00002C00 File Offset: 0x00000E00
			public LeaseOperationRequest(LeaseToken token)
			{
				this.token = token;
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600002C RID: 44 RVA: 0x00002C0F File Offset: 0x00000E0F
			public string LeasePath
			{
				get
				{
					return this.leasePath;
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600002D RID: 45 RVA: 0x00002C17 File Offset: 0x00000E17
			public LeaseToken Token
			{
				get
				{
					return this.token;
				}
			}

			// Token: 0x04000012 RID: 18
			private string leasePath;

			// Token: 0x04000013 RID: 19
			private LeaseToken token;
		}

		// Token: 0x02000008 RID: 8
		public class LeaseOperationResult
		{
			// Token: 0x0600002E RID: 46 RVA: 0x00002C1F File Offset: 0x00000E1F
			public LeaseOperationResult(LeaseToken resultToken)
			{
				this.resultToken = resultToken;
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00002C2E File Offset: 0x00000E2E
			public LeaseOperationResult()
			{
			}

			// Token: 0x06000030 RID: 48 RVA: 0x00002C36 File Offset: 0x00000E36
			public LeaseOperationResult(Exception e)
			{
				this.exception = e;
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000031 RID: 49 RVA: 0x00002C45 File Offset: 0x00000E45
			public bool Succeeded
			{
				get
				{
					return this.exception == null;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000032 RID: 50 RVA: 0x00002C50 File Offset: 0x00000E50
			public Exception Exception
			{
				get
				{
					return this.exception;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000033 RID: 51 RVA: 0x00002C58 File Offset: 0x00000E58
			public LeaseToken ResultToken
			{
				get
				{
					return this.resultToken;
				}
			}

			// Token: 0x04000014 RID: 20
			private LeaseToken resultToken;

			// Token: 0x04000015 RID: 21
			private Exception exception;
		}
	}
}
