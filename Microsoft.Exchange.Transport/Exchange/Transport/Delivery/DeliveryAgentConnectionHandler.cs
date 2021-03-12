using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x020003C2 RID: 962
	internal class DeliveryAgentConnectionHandler : IStartableTransportComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x06002C1B RID: 11291 RVA: 0x000B05E5 File Offset: 0x000AE7E5
		public DeliveryAgentConnectionHandler()
		{
			this.connections = new List<DeliveryAgentConnection>();
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000B0603 File Offset: 0x000AE803
		public string CurrentState
		{
			get
			{
				return "Connection count=" + this.connections.Count;
			}
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000B061F File Offset: 0x000AE81F
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000B0624 File Offset: 0x000AE824
		public void Stop()
		{
			lock (this.syncObject)
			{
				this.retire = true;
				this.allConnectionsRetired = new AutoResetEvent(this.connections.Count == 0);
				foreach (DeliveryAgentConnection deliveryAgentConnection in this.connections)
				{
					deliveryAgentConnection.Retire();
				}
			}
			this.allConnectionsRetired.WaitOne();
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000B06CC File Offset: 0x000AE8CC
		public void Pause()
		{
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000B06CE File Offset: 0x000AE8CE
		public void Continue()
		{
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000B06D0 File Offset: 0x000AE8D0
		public void Load()
		{
			this.mexEvents = new DeliveryAgentMExEvents();
			this.mexEvents.Initialize(Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\Shared\\agents.config"));
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000B06F7 File Offset: 0x000AE8F7
		public void Unload()
		{
			this.mexEvents.Shutdown();
			this.mexEvents = null;
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000B070B File Offset: 0x000AE90B
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000B070E File Offset: 0x000AE90E
		public void HandleConnection(NextHopConnection nextHopConnection)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.HandleConnectionCallback), nextHopConnection);
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000B0724 File Offset: 0x000AE924
		private void HandleConnectionCallback(object connection)
		{
			DeliveryAgentConnection deliveryAgentConnection = null;
			lock (this.syncObject)
			{
				if (this.retire)
				{
					return;
				}
				deliveryAgentConnection = new DeliveryAgentConnection((NextHopConnection)connection, this.mexEvents);
				this.connections.Add(deliveryAgentConnection);
			}
			deliveryAgentConnection.BeginConnection(deliveryAgentConnection, new AsyncCallback(this.ConnectionCompleted));
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000B079C File Offset: 0x000AE99C
		private void ConnectionCompleted(IAsyncResult ar)
		{
			DeliveryAgentConnection deliveryAgentConnection = (DeliveryAgentConnection)ar.AsyncState;
			deliveryAgentConnection.EndConnection(ar);
			lock (this.syncObject)
			{
				this.connections.Remove(deliveryAgentConnection);
				if (this.retire && this.connections.Count == 0)
				{
					this.allConnectionsRetired.Set();
				}
			}
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000B0818 File Offset: 0x000AEA18
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "DeliveryAgents";
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000B0820 File Offset: 0x000AEA20
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
			if (this.mexEvents != null)
			{
				xelement.Add(this.mexEvents.GetDiagnosticInfo(parameters));
			}
			return xelement;
		}

		// Token: 0x04001629 RID: 5673
		private DeliveryAgentMExEvents mexEvents;

		// Token: 0x0400162A RID: 5674
		private List<DeliveryAgentConnection> connections;

		// Token: 0x0400162B RID: 5675
		private bool retire;

		// Token: 0x0400162C RID: 5676
		private AutoResetEvent allConnectionsRetired;

		// Token: 0x0400162D RID: 5677
		private object syncObject = new object();
	}
}
