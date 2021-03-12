using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200002C RID: 44
	internal class ServerSchemaVersionSource
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public ServerSchemaVersionSource(Guid localServer, IDiagnosticsSession diagnosticsSession)
		{
			Dictionary<Guid, int> dictionary = this.serverSchemaVersion;
			VersionInfo latest = VersionInfo.Latest;
			dictionary.Add(localServer, latest.FeedingVersion);
			this.diagnosticsSession = diagnosticsSession;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000B0F4 File Offset: 0x000092F4
		public int GetServerVersion(Guid serverGuid)
		{
			int feedingVersion;
			if (!this.serverSchemaVersion.TryGetValue(serverGuid, out feedingVersion))
			{
				VersionInfo legacy = VersionInfo.Legacy;
				feedingVersion = legacy.FeedingVersion;
			}
			return feedingVersion;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B120 File Offset: 0x00009320
		public void LoadVersions(ICollection<Guid> serverGuids)
		{
			AdDataProvider adDataProvider = AdDataProvider.Create(this.diagnosticsSession);
			MiniServer[] servers = adDataProvider.GetServers(serverGuids, 1000);
			foreach (MiniServer miniServer in servers)
			{
				this.SetSchemaVersion(miniServer.Guid, VersionInfo.GetSchemaVersionForServerVersion(miniServer.AdminDisplayVersion));
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B17B File Offset: 0x0000937B
		internal void SetSchemaVersion(Guid serverGuid, int version)
		{
			this.serverSchemaVersion[serverGuid] = version;
		}

		// Token: 0x040000F4 RID: 244
		private readonly Dictionary<Guid, int> serverSchemaVersion = new Dictionary<Guid, int>();

		// Token: 0x040000F5 RID: 245
		private readonly IDiagnosticsSession diagnosticsSession;
	}
}
