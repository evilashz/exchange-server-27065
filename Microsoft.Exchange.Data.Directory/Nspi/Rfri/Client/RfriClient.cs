using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Rfri;

namespace Microsoft.Exchange.Nspi.Rfri.Client
{
	// Token: 0x020001D4 RID: 468
	internal class RfriClient : IDisposeTrackable, IDisposable
	{
		// Token: 0x060012EF RID: 4847 RVA: 0x0005B3F4 File Offset: 0x000595F4
		public RfriClient(string host) : this(host, null)
		{
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0005B3FE File Offset: 0x000595FE
		public RfriClient(string host, NetworkCredential nc)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.client = new RfriRpcClient(host, "ncacn_ip_tcp", nc);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0005B424 File Offset: 0x00059624
		public RfriClient(string machinename, string proxyserver, NetworkCredential nc)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.client = new RfriRpcClient(machinename, proxyserver, "ncacn_http:6002", nc);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0005B44B File Offset: 0x0005964B
		public RfriClient(string machinename, string proxyserver, string protocolSequence, NetworkCredential nc)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.client = new RfriRpcClient(machinename, proxyserver, protocolSequence, nc);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0005B46F File Offset: 0x0005966F
		public RfriClient(string machinename, string proxyserver, string protocolSequence, NetworkCredential nc, HTTPAuthentication httpAuth, AuthenticationService authService)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.client = new RfriRpcClient(machinename, proxyserver, protocolSequence, true, nc, (HttpAuthenticationScheme)httpAuth, authService);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0005B498 File Offset: 0x00059698
		public RfriClient(string machinename, string proxyserver, string protocolSequence, NetworkCredential nc, HTTPAuthentication httpAuth, AuthenticationService authService, string instanceName)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.client = new RfriRpcClient(machinename, proxyserver, protocolSequence, nc, (HttpAuthenticationScheme)httpAuth, authService, instanceName);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0005B4C4 File Offset: 0x000596C4
		public RfriClient(string machinename, string proxyserver, string protocolSequence, NetworkCredential nc, HTTPAuthentication httpAuth, AuthenticationService authService, string instanceName, string certificateSubjectName)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.client = new RfriRpcClient(machinename, proxyserver, protocolSequence, nc, (HttpAuthenticationScheme)httpAuth, authService, instanceName, true, certificateSubjectName);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0005B4FC File Offset: 0x000596FC
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RfriClient>(this);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0005B504 File Offset: 0x00059704
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0005B519 File Offset: 0x00059719
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0005B528 File Offset: 0x00059728
		public RfriStatus GetNewDSA(string userDN, out string server)
		{
			return this.client.GetNewDSA(userDN, out server);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0005B537 File Offset: 0x00059737
		public RfriStatus GetFQDNFromLegacyDN(string serverDN, out string serverFQDN)
		{
			return this.client.GetFQDNFromLegacyDN(serverDN, out serverFQDN);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0005B546 File Offset: 0x00059746
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.client != null)
				{
					this.client.Dispose();
					this.client = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
			}
		}

		// Token: 0x04000ADD RID: 2781
		private RfriRpcClient client;

		// Token: 0x04000ADE RID: 2782
		private DisposeTracker disposeTracker;
	}
}
