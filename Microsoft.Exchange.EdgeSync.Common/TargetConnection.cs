using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.MessageSecurity.EdgeSync;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000008 RID: 8
	internal abstract class TargetConnection : IDisposable
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000028BC File Offset: 0x00000ABC
		public TargetConnection(int localServerVersion, TargetServerConfig config)
		{
			this.localServerVersion = localServerVersion;
			this.host = config.Host;
			this.port = config.Port;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000028E3 File Offset: 0x00000AE3
		public int LocalServerVersion
		{
			get
			{
				return this.localServerVersion;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000028EB File Offset: 0x00000AEB
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000028F3 File Offset: 0x00000AF3
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000028FB File Offset: 0x00000AFB
		public virtual bool SkipSyncCycle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000036 RID: 54
		public abstract bool OnSynchronizing();

		// Token: 0x06000037 RID: 55
		public abstract void OnConnectedToSource(Connection sourceConnection);

		// Token: 0x06000038 RID: 56
		public abstract bool OnSynchronized();

		// Token: 0x06000039 RID: 57
		public abstract SyncResult OnAddEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes);

		// Token: 0x0600003A RID: 58
		public abstract SyncResult OnModifyEntry(ExSearchResultEntry entry, SortedList<string, DirectoryAttribute> sourceAttributes);

		// Token: 0x0600003B RID: 59
		public abstract SyncResult OnDeleteEntry(ExSearchResultEntry entry);

		// Token: 0x0600003C RID: 60
		public abstract SyncResult OnRenameEntry(ExSearchResultEntry entry);

		// Token: 0x0600003D RID: 61
		public abstract bool TryReadCookie(out Dictionary<string, Cookie> cookies);

		// Token: 0x0600003E RID: 62
		public abstract bool TrySaveCookie(Dictionary<string, Cookie> cookies);

		// Token: 0x0600003F RID: 63
		public abstract LeaseToken GetLease();

		// Token: 0x06000040 RID: 64
		public abstract bool CanTakeOverLease(bool force, LeaseToken lease, DateTime now);

		// Token: 0x06000041 RID: 65
		public abstract void SetLease(LeaseToken newLeaseToken);

		// Token: 0x06000042 RID: 66 RVA: 0x000028FE File Offset: 0x00000AFE
		public virtual void Dispose()
		{
		}

		// Token: 0x04000011 RID: 17
		private string host;

		// Token: 0x04000012 RID: 18
		private int port;

		// Token: 0x04000013 RID: 19
		private int localServerVersion;
	}
}
