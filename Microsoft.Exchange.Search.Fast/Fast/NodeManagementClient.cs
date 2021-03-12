using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Ceres.CoreServices.Admin;
using Microsoft.Ceres.CoreServices.Services.HealthCheck;
using Microsoft.Ceres.CoreServices.Services.SystemManager;
using Microsoft.Ceres.CoreServices.Tools.Management.Client;
using Microsoft.Ceres.HostController.WcfClient;
using Microsoft.Ceres.HostController.WcfTypes;
using Microsoft.Ceres.SearchCore.Admin;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000020 RID: 32
	internal class NodeManagementClient : FastManagementClient, INodeManager
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		internal NodeManagementClient()
		{
			base.DiagnosticsSession.ComponentName = "NodeManagementClient";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.IndexManagementTracer;
			base.ConnectManagementAgents();
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		public static NodeManagementClient Instance
		{
			get
			{
				if (Interlocked.CompareExchange<Hookable<NodeManagementClient>>(ref NodeManagementClient.hookableInstance, null, null) == null)
				{
					lock (NodeManagementClient.lockObject)
					{
						if (NodeManagementClient.hookableInstance == null)
						{
							Hookable<NodeManagementClient> hookable = Hookable<NodeManagementClient>.Create(true, new NodeManagementClient());
							Thread.MemoryBarrier();
							NodeManagementClient.hookableInstance = hookable;
						}
					}
				}
				return NodeManagementClient.hookableInstance.Value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000BB50 File Offset: 0x00009D50
		protected virtual IAdminServiceManagementAgent AdminService
		{
			get
			{
				return this.adminService;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000BB5A File Offset: 0x00009D5A
		protected override int ManagementPortOffset
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000BB5D File Offset: 0x00009D5D
		protected IHostController HostController
		{
			get
			{
				if (this.hostControllerClient == null)
				{
					base.ConnectManagementAgents("localhost");
				}
				return this.hostControllerClient.HostController;
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000BB81 File Offset: 0x00009D81
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NodeManagementClient>(this);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000BB96 File Offset: 0x00009D96
		public IEnumerable<HealthCheckInfo> GetSystemInfo()
		{
			return this.PerformFastOperation<IEnumerable<HealthCheckInfo>>(() => this.AdminService.SystemInfo(), "GetSystemInfo");
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		public NodeInfo GetNodeInfo(string nodeName)
		{
			return this.PerformFastOperation<NodeInfo>(() => this.AdminService.NodeInfo(nodeName), "GetNodeInfo");
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000BD30 File Offset: 0x00009F30
		public XElement GetAllNodesInfoDiagnostics()
		{
			return this.PerformFastOperation<XElement>(delegate()
			{
				XElement xelement = new XElement("Nodes");
				foreach (NodeInfo nodeInfo in this.AdminService.AllNodesInfo())
				{
					XElement xelement2 = new XElement("Node");
					xelement2.Add(new XElement("Name", nodeInfo.Name));
					xelement2.Add(new XElement("Host", nodeInfo.Host));
					xelement2.Add(new XElement("Roles", nodeInfo.Roles));
					xelement2.Add(new XElement("BasePort", nodeInfo.BasePort));
					xelement2.Add(new XElement("State", nodeInfo.State));
					xelement2.Add(new XElement("ExtendedState", nodeInfo.ExtendedState));
					xelement.Add(xelement2);
				}
				return xelement;
			}, "GetAllNodesInfoDiagnostics");
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000BD49 File Offset: 0x00009F49
		public bool AreAllNodesHealthy()
		{
			return this.AreAllNodesHealthy(false);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000BD70 File Offset: 0x00009F70
		public bool AreAllNodesHealthy(bool retry)
		{
			return this.PerformFastOperation<bool>(() => this.CheckForAllNodesHealthy(retry), "AreAllNodesHealthy");
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public bool IsNodeHealthy(string nodeName)
		{
			Util.ThrowOnNullArgument(nodeName, "nodeName");
			return this.PerformFastOperation<bool>(delegate()
			{
				NodeInfo nodeInfo = this.AdminService.NodeInfo(nodeName);
				return nodeInfo.State.Equals(1);
			}, "IsNodeHealthy");
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C050 File Offset: 0x0000A250
		public XElement GetAllNodesHealthReport()
		{
			return this.PerformFastOperation<XElement>(delegate()
			{
				XElement xelement = new XElement("NodesHealthInfo");
				using (SystemClient systemClient = base.ConnectSystem())
				{
					foreach (NodeInfo nodeInfo in this.AdminService.AllNodesInfo())
					{
						using (WcfManagementClient wcfManagementClient = this.ConnectNode(systemClient, nodeInfo.Name))
						{
							IHealthReporterManagementAgent healthReporterAgent = this.GetHealthReporterAgent(wcfManagementClient, nodeInfo.Name);
							XElement xelement2 = new XElement("NodeHealthInfo");
							xelement2.Add(new XElement("NodeName", nodeInfo.Name));
							xelement2.Add(new XElement("HealthStatusOk", healthReporterAgent.HealthStatusOk));
							XElement xelement3 = new XElement("Records");
							IEnumerable<HealthReportItem> healthStatus = healthReporterAgent.HealthStatus;
							if (healthStatus != null)
							{
								foreach (HealthReportItem healthReportItem in healthStatus)
								{
									XElement xelement4 = new XElement("Record");
									xelement4.Add(new XElement("Id", healthReportItem.Id));
									xelement4.Add(new XElement("Level", healthReportItem.Level));
									xelement4.Add(new XElement("Message", healthReportItem.Message));
									xelement3.Add(xelement4);
								}
							}
							xelement2.Add(xelement3);
							xelement.Add(xelement2);
						}
					}
				}
				return xelement;
			}, "GetAllNodesHealthReport");
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		public bool IsNodeStopped(string nodeName)
		{
			Util.ThrowOnNullArgument(nodeName, "nodeName");
			return this.PerformFastOperation<bool>(delegate()
			{
				NodeInfo nodeInfo = this.AdminService.NodeInfo(nodeName);
				return nodeInfo.State.Equals(0);
			}, "IsNodeStopped");
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C120 File Offset: 0x0000A320
		public void StartNode(string nodeName)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Starting Node", new object[0]);
			base.PerformFastOperation(delegate()
			{
				this.HostController.StartNode("Fsis", nodeName);
			}, "StartNode");
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C194 File Offset: 0x0000A394
		public void KillNode(string nodeName)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Killing Node", new object[0]);
			base.PerformFastOperation(delegate()
			{
				this.HostController.KillNode("Fsis", nodeName);
			}, "KillNode");
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C208 File Offset: 0x0000A408
		public void KillAndRestartNode(string nodeName)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Killing and Restarting Node", new object[0]);
			base.PerformFastOperation(delegate()
			{
				this.HostController.KillAndRestartNode("Fsis", nodeName);
			}, "KillAndRestartNode");
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C278 File Offset: 0x0000A478
		public void StopNode(string nodeName)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Stop Node", new object[0]);
			base.PerformFastOperation(delegate()
			{
				this.AdminService.StopNode(nodeName);
			}, "StopNode");
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C2C7 File Offset: 0x0000A4C7
		internal static IDisposable SetInstanceTestHook(NodeManagementClient mockNodeManager)
		{
			if (NodeManagementClient.hookableInstance == null)
			{
				NodeManagementClient instance = NodeManagementClient.Instance;
			}
			return NodeManagementClient.hookableInstance.SetTestHook(mockNodeManager);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		internal bool CheckForAllNodesHealthy(bool retry)
		{
			int num = retry ? 120 : 1;
			for (int i = 0; i < num; i++)
			{
				bool flag = true;
				foreach (HealthCheckInfo healthCheckInfo in this.AdminService.SystemInfo())
				{
					if ((!healthCheckInfo.Name.Equals("IndexNode1") || healthCheckInfo.State != 2) && healthCheckInfo.State != 1)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return true;
				}
				if (retry)
				{
					Thread.Sleep(1000);
				}
			}
			return false;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C388 File Offset: 0x0000A588
		internal List<string> GetNodeNamesBasedOnRole(string role)
		{
			List<string> list = new List<string>();
			foreach (NodeInfo nodeInfo in this.AdminService.AllNodesInfo())
			{
				if (nodeInfo.Roles.Contains(role))
				{
					list.Add(nodeInfo.Name);
				}
			}
			return list;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		protected override void InternalConnectManagementAgents(WcfManagementClient client)
		{
			this.CreateWcfHostControllerClient();
			this.adminService = client.GetManagementAgent<IAdminServiceManagementAgent>("AdminService");
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C410 File Offset: 0x0000A610
		private void CreateWcfHostControllerClient()
		{
			if (this.hostControllerClient != null)
			{
				this.hostControllerClient.Dispose();
				this.hostControllerClient = null;
			}
			Uri uri = new Uri(string.Format("net.tcp://{0}:{1}/ceres/hostcontroller/nettcp", "localhost", FastManagementClient.FsisInstallBasePort + 1));
			DuplexChannelFactory<IHostController> duplexChannelFactory = WcfClientConfiguration.CreateDefaultChannelFactory(uri, true);
			this.hostControllerClient = new WcfHostControllerClient(duplexChannelFactory, null, new EndpointAddress(uri, EndpointIdentity.CreateUpnIdentity("*"), new AddressHeader[0]), TimeSpan.FromMinutes(1.0));
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000C49C File Offset: 0x0000A69C
		private WcfManagementClient ConnectNode(SystemClient sc, string nodeName)
		{
			ISystemManagerManagementAgent managementAgent = base.GetManagementAgent<ISystemManagerManagementAgent>("SystemManager");
			Uri managementUri = managementAgent.GetManagementUri(nodeName, false);
			return sc.GetManagementClient(managementUri);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		private IHealthReporterManagementAgent GetHealthReporterAgent(WcfManagementClient connection, string nodeName)
		{
			return connection.GetManagementAgent<IHealthReporterManagementAgent>(nodeName + ".HealthReport");
		}

		// Token: 0x040000D7 RID: 215
		private const int RetryCount = 120;

		// Token: 0x040000D8 RID: 216
		private const int RetryInterval = 1000;

		// Token: 0x040000D9 RID: 217
		private const int FsisHostControllerRelativePortNumber = 1;

		// Token: 0x040000DA RID: 218
		private const int ProxyCacheIntervalInMinutes = 1;

		// Token: 0x040000DB RID: 219
		private const string ServerName = "localhost";

		// Token: 0x040000DC RID: 220
		private static object lockObject = new object();

		// Token: 0x040000DD RID: 221
		private static Hookable<NodeManagementClient> hookableInstance;

		// Token: 0x040000DE RID: 222
		private volatile IAdminServiceManagementAgent adminService;

		// Token: 0x040000DF RID: 223
		private volatile WcfHostControllerClient hostControllerClient;
	}
}
