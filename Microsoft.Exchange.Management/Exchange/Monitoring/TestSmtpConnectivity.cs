using System;
using System.Collections.Generic;
using System.Management;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005EC RID: 1516
	[Cmdlet("Test", "SmtpConnectivity", SupportsShouldProcess = true)]
	public sealed class TestSmtpConnectivity : DataAccessTask<Server>
	{
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x060035DB RID: 13787 RVA: 0x000DD9EE File Offset: 0x000DBBEE
		// (set) Token: 0x060035DC RID: 13788 RVA: 0x000DD9F6 File Offset: 0x000DBBF6
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x060035DD RID: 13789 RVA: 0x000DD9FF File Offset: 0x000DBBFF
		// (set) Token: 0x060035DE RID: 13790 RVA: 0x000DDA16 File Offset: 0x000DBC16
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public ServerIdParameter Identity
		{
			get
			{
				return (ServerIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x000DDA29 File Offset: 0x000DBC29
		// (set) Token: 0x060035E0 RID: 13792 RVA: 0x000DDA4A File Offset: 0x000DBC4A
		[Parameter(Mandatory = false)]
		public bool MonitoringContext
		{
			get
			{
				return (bool)(base.Fields["MonitoringContext"] ?? false);
			}
			set
			{
				base.Fields["MonitoringContext"] = value;
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x000DDA62 File Offset: 0x000DBC62
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestSmtpConnectivity;
			}
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000DDA6C File Offset: 0x000DBC6C
		internal static IList<ReceiveConnector> GetReceiveConnectors(IConfigurationSession session, Server server)
		{
			List<ReceiveConnector> list = new List<ReceiveConnector>();
			ADPagedReader<ReceiveConnector> adpagedReader = session.FindPaged<ReceiveConnector>(server.Id, QueryScope.SubTree, null, null, ADGenericPagedReader<ReceiveConnector>.DefaultPageSize);
			foreach (ReceiveConnector receiveConnector in adpagedReader)
			{
				if (receiveConnector.Enabled)
				{
					list.Add(receiveConnector);
				}
			}
			return list;
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000DDAD8 File Offset: 0x000DBCD8
		internal static bool ConnectorsHaveBindings(IList<ReceiveConnector> connectors)
		{
			foreach (ReceiveConnector receiveConnector in connectors)
			{
				if (receiveConnector.Bindings.Count > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000DDB30 File Offset: 0x000DBD30
		internal static SmtpConnectivityStatus GetStatus(Server server, ReceiveConnector connector, IPBinding binding, IPEndPoint endPoint)
		{
			SmtpConnectivityStatusCode statusCode;
			string details;
			TestSmtpConnectivity.TestEndPoint(endPoint, out statusCode, out details);
			return new SmtpConnectivityStatus(server, connector, binding, endPoint)
			{
				StatusCode = statusCode,
				Details = details
			};
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000DDB60 File Offset: 0x000DBD60
		internal static IList<IPEndPoint> GetEndPoints(Server server, IPBinding binding)
		{
			if (!binding.Address.Equals(IPAddress.Any) && !binding.Address.Equals(IPAddress.IPv6Any))
			{
				return new IPEndPoint[]
				{
					binding
				};
			}
			ManagementPath path = new ManagementPath(string.Format("\\\\{0}\\root\\cimv2", server.Fqdn));
			System.Management.ManagementScope scope = new System.Management.ManagementScope(path);
			IList<NetworkConnectionInfo> connectionInfo = NetworkConnectionInfo.GetConnectionInfo(scope);
			List<IPEndPoint> list = new List<IPEndPoint>();
			foreach (NetworkConnectionInfo networkConnectionInfo in connectionInfo)
			{
				foreach (IPAddress address in networkConnectionInfo.IPAddresses)
				{
					list.Add(new IPEndPoint(address, binding.Port));
				}
			}
			return list;
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000DDC3C File Offset: 0x000DBE3C
		internal static void TestEndPoint(IPEndPoint endPoint, out SmtpConnectivityStatusCode statusCode, out string details)
		{
			statusCode = SmtpConnectivityStatusCode.Success;
			details = string.Empty;
			using (Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
			{
				try
				{
					socket.Connect(endPoint);
					if (!socket.Connected)
					{
						statusCode = SmtpConnectivityStatusCode.Error;
						details = Strings.UnableToConnect;
					}
					else
					{
						byte[] array = new byte[2000];
						int count = socket.Receive(array, array.Length, SocketFlags.None);
						string @string = Encoding.ASCII.GetString(array, 0, count);
						TestSmtpConnectivity.ParseBanner(@string, out statusCode, out details);
					}
				}
				catch (SocketException ex)
				{
					statusCode = SmtpConnectivityStatusCode.Error;
					details = ex.Message;
				}
			}
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000DDCF4 File Offset: 0x000DBEF4
		internal static MonitoringEvent CreateMonitoringEvent(string serverName, IList<SmtpConnectivityStatus> results)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			foreach (SmtpConnectivityStatus smtpConnectivityStatus in results)
			{
				string value = Strings.SmtpConnectivityEndPointResult(smtpConnectivityStatus.Identity.ToString(), smtpConnectivityStatus.Details);
				switch (smtpConnectivityStatus.StatusCode)
				{
				case SmtpConnectivityStatusCode.Success:
					stringBuilder2.AppendLine(value);
					break;
				case SmtpConnectivityStatusCode.Error:
					stringBuilder.AppendLine(value);
					break;
				case SmtpConnectivityStatusCode.UnableToComplete:
					stringBuilder3.AppendLine(value);
					break;
				}
			}
			string failures = (stringBuilder.Length == 0) ? string.Empty : Strings.SmtpConnectivityFailures(stringBuilder.ToString());
			string untested = (stringBuilder3.Length == 0) ? Strings.SmtpConnectivityAllTested : Strings.SmtpConnectivityNotTested(stringBuilder3.ToString());
			string successes = (stringBuilder2.Length == 0) ? Strings.SmtpConnectivityNoneSucceeded : Strings.SmtpConnectivitySuccesses(stringBuilder2.ToString());
			SmtpConnectivityStatusCode eventIdentifier;
			EventTypeEnumeration eventType;
			string eventMessage;
			if (stringBuilder.Length > 0)
			{
				eventIdentifier = SmtpConnectivityStatusCode.Error;
				eventType = EventTypeEnumeration.Error;
				eventMessage = Strings.SmtpConnectivityFailureEvent(serverName, failures, untested, successes);
			}
			else if (stringBuilder3.Length > 0)
			{
				eventIdentifier = SmtpConnectivityStatusCode.UnableToComplete;
				eventType = EventTypeEnumeration.Warning;
				eventMessage = Strings.SmtpConnectivityIncompleteEvent(serverName, untested, successes);
			}
			else if (stringBuilder2.Length > 0)
			{
				eventIdentifier = SmtpConnectivityStatusCode.Success;
				eventType = EventTypeEnumeration.Information;
				eventMessage = Strings.SmtpConnectivitySuccessEvent(serverName, successes);
			}
			else
			{
				eventIdentifier = SmtpConnectivityStatusCode.Error;
				eventType = EventTypeEnumeration.Error;
				eventMessage = Strings.SmtpConnectivityServerNotConfigured(serverName);
			}
			return new MonitoringEvent("MSExchange Monitoring SmtpConnectivity", (int)eventIdentifier, eventType, eventMessage);
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000DDEAC File Offset: 0x000DC0AC
		internal static void ParseBanner(string banner, out SmtpConnectivityStatusCode statusCode, out string details)
		{
			banner = banner.Trim();
			if (banner.Length < 3)
			{
				statusCode = SmtpConnectivityStatusCode.Error;
				details = Strings.InvalidSmtpBanner(banner);
				return;
			}
			switch (banner[0])
			{
			case '2':
				statusCode = SmtpConnectivityStatusCode.Success;
				details = banner;
				return;
			case '4':
			case '5':
				statusCode = SmtpConnectivityStatusCode.Error;
				details = banner;
				return;
			}
			statusCode = SmtpConnectivityStatusCode.Error;
			details = Strings.InvalidSmtpBanner(banner);
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000DDF2E File Offset: 0x000DC12E
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || MonitoringHelper.IsKnownExceptionForMonitoring(exception);
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000DDF41 File Offset: 0x000DC141
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 482, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Tasks\\TestSmtpConnectivity.cs");
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000DDF70 File Offset: 0x000DC170
		protected override void InternalValidate()
		{
			base.InternalValidate();
			TaskLogger.LogEnter();
			try
			{
				if (this.Identity == null)
				{
					ITopologyConfigurationSession topologyConfigurationSession = (ITopologyConfigurationSession)base.DataSession;
					Server server = topologyConfigurationSession.FindLocalServer();
					this.Identity = new ServerIdParameter(new Fqdn(server.Fqdn));
				}
				this.server = (Server)base.GetDataObject<Server>(this.Identity, base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorServerNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Identity.ToString())));
				if (!this.server.IsHubTransportServer && !this.server.IsEdgeServer)
				{
					this.WriteErrorAndAddMonitoringEvent(new NotTransportServerException(this.Identity.ToString()), ErrorCategory.InvalidArgument, SmtpConnectivityStatusCode.UnableToComplete);
				}
				if (!this.ServerHasBindings())
				{
				}
			}
			finally
			{
				if (base.HasErrors && this.MonitoringContext)
				{
					this.WriteMonitoringObject();
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000DE074 File Offset: 0x000DC274
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			TaskLogger.LogEnter();
			try
			{
				foreach (ReceiveConnector receiveConnector in this.receiveConnectors)
				{
					foreach (IPBinding ipbinding in receiveConnector.Bindings)
					{
						Exception ex = null;
						try
						{
							IList<IPEndPoint> endPoints = TestSmtpConnectivity.GetEndPoints(this.server, ipbinding);
							foreach (IPEndPoint endPoint in endPoints)
							{
								SmtpConnectivityStatus status = TestSmtpConnectivity.GetStatus(this.server, receiveConnector, ipbinding, endPoint);
								base.WriteObject(status);
								if (this.MonitoringContext)
								{
									this.AddMonitoringData(status);
								}
							}
						}
						catch (ManagementException ex2)
						{
							ex = ex2;
						}
						catch (COMException ex3)
						{
							ex = ex3;
						}
						catch (UnauthorizedAccessException ex4)
						{
							ex = ex4;
						}
						if (ex != null)
						{
							SmtpConnectivityStatus smtpConnectivityStatus = new SmtpConnectivityStatus(this.server, receiveConnector, ipbinding, ipbinding);
							smtpConnectivityStatus.StatusCode = SmtpConnectivityStatusCode.UnableToComplete;
							smtpConnectivityStatus.Details = ex.Message;
							base.WriteObject(smtpConnectivityStatus);
							if (this.MonitoringContext)
							{
								this.AddMonitoringData(smtpConnectivityStatus);
							}
						}
					}
				}
			}
			finally
			{
				if (this.MonitoringContext)
				{
					this.WriteMonitoringObject();
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000DE278 File Offset: 0x000DC478
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.monitoringData = new MonitoringData();
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000DE28C File Offset: 0x000DC48C
		private bool ServerHasBindings()
		{
			this.receiveConnectors = TestSmtpConnectivity.GetReceiveConnectors((IConfigurationSession)base.DataSession, this.server);
			if (this.receiveConnectors.Count == 0)
			{
				this.WriteErrorAndAddMonitoringEvent(new NoReceiveConnectorsException(this.server.Fqdn), ErrorCategory.InvalidData, SmtpConnectivityStatusCode.Error);
				return false;
			}
			if (!TestSmtpConnectivity.ConnectorsHaveBindings(this.receiveConnectors))
			{
				this.WriteErrorAndAddMonitoringEvent(new NoBindingsException(this.server.Fqdn), ErrorCategory.InvalidData, SmtpConnectivityStatusCode.Error);
				return false;
			}
			return true;
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000DE30C File Offset: 0x000DC50C
		private void WriteErrorAndAddMonitoringEvent(Exception exception, ErrorCategory errorCategory, SmtpConnectivityStatusCode statusCode)
		{
			this.monitoringData.Events.Add(new MonitoringEvent("MSExchange Monitoring SmtpConnectivity", (int)statusCode, EventTypeEnumeration.Error, exception.Message));
			base.WriteError(exception, errorCategory, null);
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000DE339 File Offset: 0x000DC539
		private void AddMonitoringData(SmtpConnectivityStatus status)
		{
			if (this.results == null)
			{
				this.results = new List<SmtpConnectivityStatus>();
			}
			this.results.Add(status);
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000DE35C File Offset: 0x000DC55C
		private void WriteMonitoringObject()
		{
			if (this.results != null)
			{
				MonitoringEvent item = TestSmtpConnectivity.CreateMonitoringEvent(this.server.Name, this.results);
				this.monitoringData.Events.Add(item);
			}
			base.WriteObject(this.monitoringData);
		}

		// Token: 0x040024EA RID: 9450
		private const string CmdletNoun = "SmtpConnectivity";

		// Token: 0x040024EB RID: 9451
		private const string MonitoringEventSource = "MSExchange Monitoring SmtpConnectivity";

		// Token: 0x040024EC RID: 9452
		private MonitoringData monitoringData;

		// Token: 0x040024ED RID: 9453
		private Server server;

		// Token: 0x040024EE RID: 9454
		private IList<ReceiveConnector> receiveConnectors;

		// Token: 0x040024EF RID: 9455
		private List<SmtpConnectivityStatus> results;
	}
}
