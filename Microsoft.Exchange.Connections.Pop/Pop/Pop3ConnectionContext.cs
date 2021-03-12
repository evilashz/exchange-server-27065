using System;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class Pop3ConnectionContext : DisposeTrackableBase
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000050EF File Offset: 0x000032EF
		internal Pop3ConnectionContext(ConnectionParameters connectionParameters, IMonitorEvents eventsMonitor = null)
		{
			this.connectionParameters = connectionParameters;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000050FE File Offset: 0x000032FE
		internal ILog Log
		{
			get
			{
				return this.ConnectionParameters.Log;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000510B File Offset: 0x0000330B
		internal ConnectionParameters ConnectionParameters
		{
			get
			{
				base.CheckDisposed();
				return this.connectionParameters;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00005119 File Offset: 0x00003319
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00005127 File Offset: 0x00003327
		internal Pop3AuthenticationParameters AuthenticationParameters
		{
			get
			{
				base.CheckDisposed();
				return this.authenticationParameters;
			}
			set
			{
				base.CheckDisposed();
				this.authenticationParameters = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00005136 File Offset: 0x00003336
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00005144 File Offset: 0x00003344
		internal ServerParameters ServerParameters
		{
			get
			{
				base.CheckDisposed();
				return this.serverParameters;
			}
			set
			{
				base.CheckDisposed();
				this.serverParameters = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00005153 File Offset: 0x00003353
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00005161 File Offset: 0x00003361
		internal Pop3Client Client
		{
			get
			{
				base.CheckDisposed();
				return this.client;
			}
			set
			{
				base.CheckDisposed();
				this.client = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00005170 File Offset: 0x00003370
		internal string UserName
		{
			get
			{
				base.CheckDisposed();
				if (this.AuthenticationParameters == null || this.AuthenticationParameters.NetworkCredential == null)
				{
					return string.Empty;
				}
				return this.AuthenticationParameters.NetworkCredential.UserName;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000051A3 File Offset: 0x000033A3
		internal string Server
		{
			get
			{
				base.CheckDisposed();
				if (this.ServerParameters == null)
				{
					return string.Empty;
				}
				return this.ServerParameters.Server;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000051C4 File Offset: 0x000033C4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.client != null)
			{
				this.client.Dispose();
				this.client = null;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000051E3 File Offset: 0x000033E3
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Pop3ConnectionContext>(this);
		}

		// Token: 0x04000050 RID: 80
		private readonly ConnectionParameters connectionParameters;

		// Token: 0x04000051 RID: 81
		private ServerParameters serverParameters;

		// Token: 0x04000052 RID: 82
		private Pop3AuthenticationParameters authenticationParameters;

		// Token: 0x04000053 RID: 83
		private Pop3Client client;
	}
}
