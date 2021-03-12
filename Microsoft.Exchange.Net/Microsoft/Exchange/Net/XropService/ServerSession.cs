using System;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000BB3 RID: 2995
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
	internal sealed class ServerSession : IService, IDisposable
	{
		// Token: 0x06004027 RID: 16423 RVA: 0x000A949B File Offset: 0x000A769B
		public ServerSession(IServerSession sessionInstance)
		{
			ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "MapiService instance created");
			this.sessionInstance = sessionInstance;
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x000A94C0 File Offset: 0x000A76C0
		public IAsyncResult BeginConnect(ConnectRequestMessage request, AsyncCallback asyncCallback, object asyncState)
		{
			ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "BeginConnect requested");
			IAsyncResult result;
			try
			{
				if (request == null)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError((long)this.GetHashCode(), "ConnectRequestMessage is empty.");
					throw new FaultException("XropService.BeginConnect: ConnectRequestMessage is empty.");
				}
				if (request.Request == null)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError((long)this.GetHashCode(), "ConnectRequest is empty.");
					throw new FaultException("XropService.BeginConnect: ConnectRequest is empty.");
				}
				result = this.sessionInstance.BeginConnect(request.Request, asyncCallback, asyncState);
			}
			finally
			{
				ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "BeginConnect completed");
			}
			return result;
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x000A9570 File Offset: 0x000A7770
		public ConnectResponseMessage EndConnect(IAsyncResult asyncResult)
		{
			ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "EndConnect requested");
			ConnectResponseMessage result;
			try
			{
				result = new ConnectResponseMessage
				{
					Response = this.sessionInstance.EndConnect(asyncResult)
				};
			}
			finally
			{
				ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "EndConnect completed");
			}
			return result;
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x000A95D8 File Offset: 0x000A77D8
		public IAsyncResult BeginExecute(ExecuteRequestMessage request, AsyncCallback asyncCallback, object asyncState)
		{
			ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "BeginExecute requested");
			IAsyncResult result;
			try
			{
				if (request == null)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError((long)this.GetHashCode(), "ExecuteRequestMessage is empty.");
					throw new FaultException("XropService.BeginExecute: ExecuteRequestMessage is empty.");
				}
				if (request.Request == null)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError((long)this.GetHashCode(), "ExecuteRequest is empty.");
					throw new FaultException("XropService.BeginExecute: ExecuteRequest is empty.");
				}
				result = this.sessionInstance.BeginExecute(request.Request, asyncCallback, asyncState);
			}
			finally
			{
				ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "BeginExecute completed");
			}
			return result;
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x000A9688 File Offset: 0x000A7888
		public ExecuteResponseMessage EndExecute(IAsyncResult asyncResult)
		{
			ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "EndExecute requested");
			ExecuteResponseMessage result;
			try
			{
				result = new ExecuteResponseMessage
				{
					Response = this.sessionInstance.EndExecute(asyncResult)
				};
			}
			finally
			{
				ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "EndExecute completed");
			}
			return result;
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x000A96F0 File Offset: 0x000A78F0
		public IAsyncResult BeginDisconnect(DisconnectRequestMessage request, AsyncCallback asyncCallback, object asyncState)
		{
			ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "BeginDisconnect requested");
			IAsyncResult result;
			try
			{
				if (request == null)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError((long)this.GetHashCode(), "DisconnectRequestMessage is empty.");
					throw new FaultException("XropService.BeginDisconnect: DisconnectRequestMessage is empty.");
				}
				if (request.Request == null)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError((long)this.GetHashCode(), "DisconnectRequest is empty.");
					throw new FaultException("XropService.BeginDisconnect: DisconnectRequest is empty.");
				}
				result = this.sessionInstance.BeginDisconnect(request.Request, asyncCallback, asyncState);
			}
			finally
			{
				ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "BeginDisconnect completed");
			}
			return result;
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x000A97A0 File Offset: 0x000A79A0
		public DisconnectResponseMessage EndDisconnect(IAsyncResult asyncResult)
		{
			ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "EndDisconnect requested");
			DisconnectResponseMessage result;
			try
			{
				result = new DisconnectResponseMessage
				{
					Response = this.sessionInstance.EndDisconnect(asyncResult)
				};
			}
			finally
			{
				ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "EndDisconnect completed");
			}
			return result;
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x000A9808 File Offset: 0x000A7A08
		public void Dispose()
		{
			IDisposable disposable = this.sessionInstance as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "MapiService instance disposed");
		}

		// Token: 0x0400378D RID: 14221
		private IServerSession sessionInstance;
	}
}
