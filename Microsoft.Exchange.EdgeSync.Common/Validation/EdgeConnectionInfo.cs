using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Common.Internal;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.MessageSecurity;
using Microsoft.Exchange.MessageSecurity.EdgeSync;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x02000043 RID: 67
	internal class EdgeConnectionInfo
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00008760 File Offset: 0x00006960
		public EdgeConnectionInfo(ReplicationTopology topology, Server edgeServer)
		{
			this.edgeServer = edgeServer;
			this.leaseType = LeaseTokenType.None;
			this.lastSynchronizedDate = DateTime.MinValue;
			this.Connect(topology);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00008788 File Offset: 0x00006988
		public Server EdgeServer
		{
			get
			{
				return this.edgeServer;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00008790 File Offset: 0x00006990
		public string LeaseHolder
		{
			get
			{
				return this.leaseHolder;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00008798 File Offset: 0x00006998
		public DateTime LeaseExpiry
		{
			get
			{
				return this.leaseExpiry;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000087A0 File Offset: 0x000069A0
		public LdapTargetConnection EdgeConnection
		{
			get
			{
				return this.edgeConnection;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000087A8 File Offset: 0x000069A8
		public string FailureDetail
		{
			get
			{
				return this.failureDetail;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000087B0 File Offset: 0x000069B0
		public DateTime LastSynchronizedDate
		{
			get
			{
				return this.lastSynchronizedDate;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000087B8 File Offset: 0x000069B8
		public LeaseTokenType LeaseType
		{
			get
			{
				return this.leaseType;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000087C0 File Offset: 0x000069C0
		public Dictionary<string, Cookie> Cookies
		{
			get
			{
				return this.cookies;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000087C8 File Offset: 0x000069C8
		private void Connect(ReplicationTopology topology)
		{
			EdgeSyncLog edgeSyncLog = new EdgeSyncLog(string.Empty, new Version(), string.Empty, string.Empty, string.Empty);
			EdgeSyncLogSession logSession = edgeSyncLog.OpenSession(string.Empty, string.Empty, 0, string.Empty, EdgeSyncLoggingLevel.None);
			try
			{
				DirectTrust.Load();
				NetworkCredential networkCredential = Util.ExtractNetworkCredential(topology.LocalHub, this.edgeServer.Fqdn, logSession);
				if (networkCredential == null)
				{
					this.failureDetail = Strings.NoCredentialsFound(this.EdgeServer.Fqdn).ToString();
				}
				else
				{
					this.edgeConnection = (LdapTargetConnection)TestEdgeConnectionFactory.Create(topology.LocalHub, new TargetServerConfig(this.EdgeServer.Name, this.EdgeServer.Fqdn, this.EdgeServer.EdgeSyncAdamSslPort), networkCredential, SyncTreeType.General, logSession);
					this.failureDetail = string.Empty;
					if (this.edgeConnection != null)
					{
						this.ExtractLeaseInfo();
						this.ExtractCookieRecords();
					}
				}
			}
			catch (ExDirectoryException ex)
			{
				this.failureDetail = ex.Message;
				this.edgeConnection = null;
			}
			finally
			{
				DirectTrust.Unload();
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000088F0 File Offset: 0x00006AF0
		private void ExtractCookieRecords()
		{
			this.edgeConnection.TryReadCookie(out this.cookies);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008904 File Offset: 0x00006B04
		private void ExtractLeaseInfo()
		{
			string stringForm = this.edgeConnection.GetLease().StringForm;
			if (!string.IsNullOrEmpty(stringForm))
			{
				LeaseToken leaseToken = LeaseToken.Parse(stringForm);
				this.leaseType = leaseToken.Type;
				this.leaseExpiry = leaseToken.Expiry;
				this.leaseHolder = leaseToken.Path;
				this.lastSynchronizedDate = leaseToken.LastSync;
			}
		}

		// Token: 0x04000118 RID: 280
		private Server edgeServer;

		// Token: 0x04000119 RID: 281
		private LdapTargetConnection edgeConnection;

		// Token: 0x0400011A RID: 282
		private string failureDetail;

		// Token: 0x0400011B RID: 283
		private DateTime lastSynchronizedDate;

		// Token: 0x0400011C RID: 284
		private DateTime leaseExpiry;

		// Token: 0x0400011D RID: 285
		private string leaseHolder;

		// Token: 0x0400011E RID: 286
		private LeaseTokenType leaseType;

		// Token: 0x0400011F RID: 287
		private Dictionary<string, Cookie> cookies;
	}
}
