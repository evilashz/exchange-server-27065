using System;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B8A RID: 2954
	internal sealed class Client : IDisposable
	{
		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x000A7630 File Offset: 0x000A5830
		public bool IsClientInteractive
		{
			get
			{
				return this.isClientInteractive;
			}
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x000A7638 File Offset: 0x000A5838
		public Client(FederatedClientCredentials clientCredentials, Uri endpoint, Uri internetWebProxy, string targetSmtpAddress) : this(clientCredentials, endpoint, internetWebProxy, targetSmtpAddress, true, null)
		{
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x000A7647 File Offset: 0x000A5847
		public Client(FederatedClientCredentials clientCredentials, Uri endpoint, Uri internetWebProxy, string targetSmtpAddress, bool isClientInteractive) : this(clientCredentials, endpoint, internetWebProxy, targetSmtpAddress, isClientInteractive, null)
		{
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x000A7658 File Offset: 0x000A5858
		public Client(FederatedClientCredentials clientCredentials, Uri endpoint, Uri internetWebProxy, string targetSmtpAddress, bool isClientInteractive, IClientDiagnosticsHandler clientDiagnosticsHandler)
		{
			ExTraceGlobals.XropServiceClientTracer.TraceDebug<Uri, bool>((long)this.GetHashCode(), "Starting service client for endpoint: {0};Interactive={1}", endpoint, this.isClientInteractive);
			this.isClientInteractive = isClientInteractive;
			this.client = ClientFactory.GetClient(endpoint, internetWebProxy, clientCredentials, targetSmtpAddress, Client.MaxWaitTimeInSeconds, clientDiagnosticsHandler);
			ExTraceGlobals.XropServiceClientTracer.TraceDebug<Uri, bool>((long)this.GetHashCode(), "Started service client for endpoint: {0};Interactive={1}", endpoint, this.isClientInteractive);
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x000A76CC File Offset: 0x000A58CC
		public void Dispose()
		{
			IDisposable disposable = this.client as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)this.GetHashCode(), "Service client disposed");
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x000A7704 File Offset: 0x000A5904
		public ConnectResponse Connect(ConnectRequest request)
		{
			ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)this.GetHashCode(), "Connect requested");
			ConnectResponse result;
			try
			{
				ConnectRequestMessage request2 = new ConnectRequestMessage
				{
					Request = request
				};
				ConnectResponseMessage connectResponseMessage = this.client.EndConnect(this.client.BeginConnect(request2, null, null));
				if (connectResponseMessage != null)
				{
					result = connectResponseMessage.Response;
				}
				else
				{
					result = null;
				}
			}
			finally
			{
				ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)this.GetHashCode(), "Connect completed");
			}
			return result;
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x000A7788 File Offset: 0x000A5988
		public ExecuteResponse Execute(ExecuteRequest request)
		{
			ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)this.GetHashCode(), "Execute requested");
			ExecuteResponse result;
			try
			{
				ExecuteRequestMessage request2 = new ExecuteRequestMessage
				{
					Request = request
				};
				ExecuteResponseMessage executeResponseMessage = this.client.EndExecute(this.client.BeginExecute(request2, null, null));
				if (executeResponseMessage != null)
				{
					result = executeResponseMessage.Response;
				}
				else
				{
					result = null;
				}
			}
			finally
			{
				ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)this.GetHashCode(), "Execute completed");
			}
			return result;
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x000A780C File Offset: 0x000A5A0C
		public DisconnectResponse Disconnect(DisconnectRequest request)
		{
			ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)this.GetHashCode(), "Disconnect requested");
			DisconnectResponse result;
			try
			{
				DisconnectRequestMessage request2 = new DisconnectRequestMessage
				{
					Request = request
				};
				DisconnectResponseMessage disconnectResponseMessage = this.client.EndDisconnect(this.client.BeginDisconnect(request2, null, null));
				if (disconnectResponseMessage != null)
				{
					result = disconnectResponseMessage.Response;
				}
				else
				{
					result = null;
				}
			}
			finally
			{
				ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)this.GetHashCode(), "Disconnect completed");
			}
			return result;
		}

		// Token: 0x0400372F RID: 14127
		private static readonly TimeSpan MaxWaitTimeInSeconds = TimeSpan.FromSeconds(120.0);

		// Token: 0x04003730 RID: 14128
		private IService client;

		// Token: 0x04003731 RID: 14129
		private bool isClientInteractive = true;
	}
}
