using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Logging
{
	// Token: 0x0200007F RID: 127
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DiagnosticsLogSyncLogImplementation : DisposeTrackableBase, ISyncLogImplementation
	{
		// Token: 0x06000332 RID: 818 RVA: 0x00014084 File Offset: 0x00012284
		public DiagnosticsLogSyncLogImplementation(LogSchema schema, SyncLogConfiguration syncLogConfiguration)
		{
			this.log = new Log(syncLogConfiguration.LogFilePrefix, new LogHeaderFormatter(schema), syncLogConfiguration.LogComponent);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000140AC File Offset: 0x000122AC
		public void Configure(bool enabled, string path, long ageQuota, long directorySizeQuota, long perFileSizeQuota)
		{
			base.CheckDisposed();
			SyncUtilities.ThrowIfArgumentNullOrEmpty("path", path);
			SyncUtilities.ThrowIfArgumentLessThanZero("ageQuota", ageQuota);
			SyncUtilities.ThrowIfArgumentLessThanZero("directorySizeQuota", directorySizeQuota);
			SyncUtilities.ThrowIfArgumentLessThanZero("perFileSizeQuota", perFileSizeQuota);
			SyncUtilities.ThrowIfArg1LessThenArg2("directorySizeQuota", directorySizeQuota, "perFileSizeQuota", perFileSizeQuota);
			this.log.Configure(path, TimeSpan.FromHours((double)ageQuota), directorySizeQuota * 1024L, perFileSizeQuota * 1024L);
			this.enabled = enabled;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001412C File Offset: 0x0001232C
		public bool IsEnabled()
		{
			base.CheckDisposed();
			return this.enabled;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001413A File Offset: 0x0001233A
		public void Append(LogRowFormatter row, int timestampField)
		{
			base.CheckDisposed();
			this.log.Append(row, timestampField);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001414F File Offset: 0x0001234F
		public void Close()
		{
			base.CheckDisposed();
			this.Dispose();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001415D File Offset: 0x0001235D
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.log != null)
			{
				this.log.Close();
				this.log = null;
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001417C File Offset: 0x0001237C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DiagnosticsLogSyncLogImplementation>(this);
		}

		// Token: 0x040001AA RID: 426
		private bool enabled;

		// Token: 0x040001AB RID: 427
		private Log log;
	}
}
