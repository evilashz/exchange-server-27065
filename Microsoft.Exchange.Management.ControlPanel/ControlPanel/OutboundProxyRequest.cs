using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000398 RID: 920
	internal sealed class OutboundProxyRequest : AsyncResult
	{
		// Token: 0x060030E0 RID: 12512 RVA: 0x00095008 File Offset: 0x00093208
		public OutboundProxyRequest(IEnumerable<ProxyConnection> proxyConnections, HttpContext context, AsyncCallback requestCompletedCallback, object requestCompletedData) : base(requestCompletedCallback, requestCompletedData)
		{
			this.Context = context;
			this.proxyConnections = proxyConnections.GetEnumerator();
			this.TryNextServer();
		}

		// Token: 0x17001F4F RID: 8015
		// (get) Token: 0x060030E1 RID: 12513 RVA: 0x0009502C File Offset: 0x0009322C
		// (set) Token: 0x060030E2 RID: 12514 RVA: 0x00095034 File Offset: 0x00093234
		public HttpContext Context { get; private set; }

		// Token: 0x17001F50 RID: 8016
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x0009503D File Offset: 0x0009323D
		// (set) Token: 0x060030E4 RID: 12516 RVA: 0x00095045 File Offset: 0x00093245
		public bool AllServersFailed { get; private set; }

		// Token: 0x060030E5 RID: 12517 RVA: 0x00095050 File Offset: 0x00093250
		private void TryNextServer()
		{
			this.canSendProxyLogon = true;
			if (!this.proxyConnections.MoveNext())
			{
				this.AllServersFailed = true;
				base.Complete(null, false);
				return;
			}
			ProxyConnection proxyConnection = this.proxyConnections.Current;
			if (proxyConnection.IsAlive)
			{
				this.SendProxyRequest(proxyConnection);
				return;
			}
			proxyConnection.Ping(new Action<ProxyConnection>(this.SendProxyRequest));
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000950B0 File Offset: 0x000932B0
		private void SendProxyRequest(ProxyConnection proxyConnection)
		{
			try
			{
				if (proxyConnection.IsCompatible && proxyConnection.IsAlive)
				{
					proxyConnection.ProxyWebSession.SendProxyRequest(this.Context, new Action(this.ProxyCallSucceeded), new Action<HttpContext, HttpWebResponse, Exception>(this.ProxyCallFailed));
				}
				else
				{
					this.TryNextServer();
				}
			}
			catch (Exception exception)
			{
				base.Complete(exception, false);
			}
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x0009511C File Offset: 0x0009331C
		private void ProxyCallSucceeded()
		{
			base.Complete(null, false);
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x00095128 File Offset: 0x00093328
		private void ProxyCallFailed(HttpContext context, HttpWebResponse response, Exception exception)
		{
			try
			{
				if (response.StatusCode == (HttpStatusCode)441 && this.canSendProxyLogon)
				{
					this.SendProxyLogon();
				}
				else
				{
					this.TryNextServer();
				}
			}
			catch (Exception exception2)
			{
				base.Complete(exception2, false);
			}
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x00095178 File Offset: 0x00093378
		private void SendProxyLogon()
		{
			ProxyConnection proxyConnection = this.proxyConnections.Current;
			OutboundProxySession session = (OutboundProxySession)this.Context.User;
			proxyConnection.ProxyWebSession.SendProxyLogon(proxyConnection.BaseUri, session, new Action<HttpStatusCode>(this.ProxyLogonResponseReceived), new Action<Exception>(this.ProxyLogonFailed));
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000951CC File Offset: 0x000933CC
		private void ProxyLogonResponseReceived(HttpStatusCode responseCode)
		{
			try
			{
				this.canSendProxyLogon = false;
				if (responseCode == (HttpStatusCode)241)
				{
					this.SendProxyRequest(this.proxyConnections.Current);
				}
				else
				{
					this.TryNextServer();
				}
			}
			catch (Exception exception)
			{
				base.Complete(exception, false);
			}
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x00095220 File Offset: 0x00093420
		private void ProxyLogonFailed(Exception proxyLogonException)
		{
			try
			{
				this.TryNextServer();
			}
			catch (Exception exception)
			{
				base.Complete(exception, false);
			}
		}

		// Token: 0x040023A1 RID: 9121
		private IEnumerator<ProxyConnection> proxyConnections;

		// Token: 0x040023A2 RID: 9122
		private bool canSendProxyLogon;
	}
}
