using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using System.ServiceModel;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200000F RID: 15
	[Cmdlet("Test", "TopologyService", SupportsShouldProcess = true)]
	public sealed class TestTopologyServiceTask : Task
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003C38 File Offset: 0x00001E38
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003C5E File Offset: 0x00001E5E
		[Parameter(Mandatory = false)]
		public SwitchParameter MonitoringContext
		{
			get
			{
				return (SwitchParameter)(base.Fields["MonitoringContext"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["MonitoringContext"] = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003C76 File Offset: 0x00001E76
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003C96 File Offset: 0x00001E96
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public ServerIdParameter Server
		{
			get
			{
				return ((ServerIdParameter)base.Fields["Server"]) ?? new ServerIdParameter();
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003CA9 File Offset: 0x00001EA9
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003CC0 File Offset: 0x00001EC0
		[Parameter(Mandatory = false)]
		public string TargetDomainController
		{
			get
			{
				return (string)base.Fields["TargetDomainController"];
			}
			set
			{
				base.Fields["TargetDomainController"] = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003CD3 File Offset: 0x00001ED3
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003CF4 File Offset: 0x00001EF4
		[Parameter(Mandatory = false)]
		public TopologyServiceServerRole ADServerRole
		{
			get
			{
				return (TopologyServiceServerRole)(base.Fields["ADServerRole"] ?? TopologyServiceServerRole.GlobalCatalog);
			}
			set
			{
				base.Fields["ADServerRole"] = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003D0C File Offset: 0x00001F0C
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003D2D File Offset: 0x00001F2D
		[Parameter(Mandatory = false)]
		public TopologyServiceOperationTypeEnum OperationType
		{
			get
			{
				return (TopologyServiceOperationTypeEnum)(base.Fields["OperationType"] ?? TopologyServiceOperationTypeEnum.Test);
			}
			set
			{
				base.Fields["OperationType"] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003D45 File Offset: 0x00001F45
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003D5C File Offset: 0x00001F5C
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public string PartitionFqdn
		{
			get
			{
				return (string)base.Fields["PartitionFqdn"];
			}
			set
			{
				base.Fields["PartitionFqdn"] = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003D6F File Offset: 0x00001F6F
		internal IConfigurationSession SystemConfigurationSession
		{
			get
			{
				if (this.systemConfigurationSession == null)
				{
					this.systemConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 266, "SystemConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\ActiveDirectory\\TestTopologyServiceTask.cs");
				}
				return this.systemConfigurationSession;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003DA4 File Offset: 0x00001FA4
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || MonitoringHelper.IsKnownExceptionForMonitoring(e);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003DCC File Offset: 0x00001FCC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				if (!base.HasErrors)
				{
					this.ServerObject = TestTopologyServiceTask.EnsureSingleObject<Server>(() => this.Server.GetObjects<Server>(null, this.SystemConfigurationSession));
					if (this.ServerObject == null)
					{
						throw new CasHealthMailboxServerNotFoundException(this.Server.ToString());
					}
					if (this.PartitionFqdn == null)
					{
						PartitionId[] array = Globals.IsDatacenter ? ADAccountPartitionLocator.GetAllAccountPartitionIds() : new PartitionId[]
						{
							PartitionId.LocalForest
						};
						this.PartitionFqdn = array[new Random().Next(0, array.Length)].ForestFQDN;
					}
					if (this.TargetDomainController == null && (this.OperationType == TopologyServiceOperationTypeEnum.ReportServerDown || this.OperationType == TopologyServiceOperationTypeEnum.SetConfigDC))
					{
						throw new TopologyServiceMissingDC(this.OperationType.ToString());
					}
				}
			}
			catch (LocalizedException exception)
			{
				this.WriteError(exception, ErrorCategory.OperationStopped, this, true);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003ED0 File Offset: 0x000020D0
		protected override void InternalBeginProcessing()
		{
			if (this.MonitoringContext)
			{
				this.monitoringData = new MonitoringData();
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003EEC File Offset: 0x000020EC
		protected override void InternalProcessRecord()
		{
			base.InternalBeginProcessing();
			TaskLogger.LogEnter();
			try
			{
				TopologyServiceOutcome sendToPipeline = new TopologyServiceOutcome(this.Server.ToString(), this.OperationType.ToString());
				this.PerformTopologyServiceTest(ref sendToPipeline, this.OperationType);
				base.WriteObject(sendToPipeline);
			}
			catch (LocalizedException e)
			{
				this.HandleException(e);
			}
			finally
			{
				if (this.MonitoringContext)
				{
					base.WriteObject(this.monitoringData);
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003F84 File Offset: 0x00002184
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestTopologyServiceIdentity(this.OperationType.ToString());
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003F9C File Offset: 0x0000219C
		private void PerformTopologyServiceTest(ref TopologyServiceOutcome result, TopologyServiceOperationTypeEnum operationType)
		{
			bool flag = true;
			using (TopologyServiceClient topologyServiceClient = TopologyServiceClient.CreateClient(this.Server.ToString()))
			{
				string error = string.Empty;
				StringBuilder stringBuilder = new StringBuilder();
				TopologyServiceError topologyServiceError = TopologyServiceError.None;
				Stopwatch stopwatch = Stopwatch.StartNew();
				try
				{
					base.WriteVerbose(Strings.TopologyServiceOperation(operationType.ToString()));
					IList<TopologyVersion> list;
					IList<ServerInfo> serversForRole;
					switch (operationType)
					{
					case TopologyServiceOperationTypeEnum.GetAllTopologyVersions:
						list = topologyServiceClient.GetAllTopologyVersions();
						using (IEnumerator<TopologyVersion> enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								TopologyVersion topologyVersion = enumerator.Current;
								stringBuilder.Append(Strings.ToplogyserviceTopologyVersion(topologyVersion.PartitionFqdn, topologyVersion.Version) + Environment.NewLine);
							}
							goto IL_264;
						}
						break;
					case TopologyServiceOperationTypeEnum.GetTopologyVersion:
						break;
					case TopologyServiceOperationTypeEnum.SetConfigDC:
						topologyServiceClient.SetConfigDC(this.PartitionFqdn, this.TargetDomainController);
						goto IL_264;
					case TopologyServiceOperationTypeEnum.ReportServerDown:
						goto IL_13A;
					case TopologyServiceOperationTypeEnum.GetServersForRole:
						serversForRole = topologyServiceClient.GetServersForRole(this.PartitionFqdn, new List<string>(), (ADServerRole)this.ADServerRole, 20, false);
						using (IEnumerator<ServerInfo> enumerator2 = serversForRole.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ServerInfo serverInfo = enumerator2.Current;
								stringBuilder.Append(Strings.TopologyServiceADServerInfo(serverInfo.Fqdn) + Environment.NewLine);
							}
							goto IL_264;
						}
						goto IL_1DA;
					case TopologyServiceOperationTypeEnum.Test:
						goto IL_1DA;
					default:
						goto IL_264;
					}
					list = topologyServiceClient.GetTopologyVersions(new List<string>
					{
						this.PartitionFqdn
					});
					using (IEnumerator<TopologyVersion> enumerator3 = list.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							TopologyVersion topologyVersion2 = enumerator3.Current;
							stringBuilder.Append(Strings.ToplogyserviceTopologyVersion(topologyVersion2.PartitionFqdn, topologyVersion2.Version) + Environment.NewLine);
						}
						goto IL_264;
					}
					IL_13A:
					topologyServiceClient.ReportServerDown(this.PartitionFqdn, this.TargetDomainController, (ADServerRole)this.ADServerRole);
					goto IL_264;
					IL_1DA:
					serversForRole = topologyServiceClient.GetServersForRole(this.PartitionFqdn, new List<string>(), (ADServerRole)this.ADServerRole, 20, false);
					foreach (ServerInfo serverInfo2 in serversForRole)
					{
						stringBuilder.Append(Strings.TopologyServiceADServerInfo(serverInfo2.Fqdn) + Environment.NewLine);
					}
					if (serversForRole.Count > 0)
					{
						flag = true;
					}
					else
					{
						flag = false;
						error = Strings.TopologyServiceNoServersReturned(this.PartitionFqdn);
					}
					IL_264:;
				}
				catch (CommunicationException ex)
				{
					flag = false;
					error = ex.Message;
					topologyServiceError = TopologyServiceError.CommunicationException;
				}
				catch (Exception ex2)
				{
					flag = false;
					error = ex2.Message;
					topologyServiceError = TopologyServiceError.OtherException;
				}
				stopwatch.Stop();
				result.Update(flag ? TopologyServiceResultEnum.Success : TopologyServiceResultEnum.Failure, stopwatch.Elapsed, error, stringBuilder.ToString());
				if (this.MonitoringContext)
				{
					this.monitoringData.Events.Add(new MonitoringEvent(TestTopologyServiceTask.CmdletMonitoringEventSource, (int)((flag ? 1000 : 2000) + this.OperationType), flag ? EventTypeEnumeration.Success : EventTypeEnumeration.Error, flag ? Strings.TopologyServiceSuccess(this.OperationType.ToString()) : (Strings.TopologyServiceFailed(this.OperationType.ToString(), error) + " " + topologyServiceError)));
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000043A0 File Offset: 0x000025A0
		private bool IsExplicitlySet(string param)
		{
			return base.Fields.Contains(param);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000043AE File Offset: 0x000025AE
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000043B6 File Offset: 0x000025B6
		private TimeSpan TotalLatency { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000043BF File Offset: 0x000025BF
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000043C7 File Offset: 0x000025C7
		private Server ServerObject { get; set; }

		// Token: 0x0600009F RID: 159 RVA: 0x000043D0 File Offset: 0x000025D0
		internal static T EnsureSingleObject<T>(Func<IEnumerable<T>> getObjects) where T : class
		{
			T t = default(T);
			foreach (T t2 in getObjects())
			{
				if (t != null)
				{
					throw new DataValidationException(new ObjectValidationError(Strings.MoreThanOneObjects(typeof(T).ToString()), null, null));
				}
				t = t2;
			}
			return t;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000444C File Offset: 0x0000264C
		private void HandleException(LocalizedException e)
		{
			if (!this.MonitoringContext)
			{
				this.WriteError(e, ErrorCategory.OperationStopped, this, true);
				return;
			}
			this.monitoringData.Events.Add(new MonitoringEvent(TestTopologyServiceTask.CmdletMonitoringEventSource, 3006, EventTypeEnumeration.Error, Strings.LiveIdConnectivityExceptionThrown(e.ToString())));
		}

		// Token: 0x0400003D RID: 61
		private const string ServerParam = "Server";

		// Token: 0x0400003E RID: 62
		private const string PartitionFqdnParam = "PartitionFqdn";

		// Token: 0x0400003F RID: 63
		private const string MonitoringContextParam = "MonitoringContext";

		// Token: 0x04000040 RID: 64
		private const string OperationParam = "OperationType";

		// Token: 0x04000041 RID: 65
		private const string TargetDomainControllerParam = "TargetDomainController";

		// Token: 0x04000042 RID: 66
		private const string ADServerRoleParam = "ADServerRole";

		// Token: 0x04000043 RID: 67
		private const int FailedEventIdBase = 2000;

		// Token: 0x04000044 RID: 68
		private const int SuccessEventIdBase = 1000;

		// Token: 0x04000045 RID: 69
		internal const string TopologyService = "TopologyService";

		// Token: 0x04000046 RID: 70
		private MonitoringData monitoringData;

		// Token: 0x04000047 RID: 71
		private IConfigurationSession systemConfigurationSession;

		// Token: 0x04000048 RID: 72
		public static readonly string CmdletMonitoringEventSource = "MSExchange Monitoring TopologyService";

		// Token: 0x04000049 RID: 73
		public static readonly string PerformanceCounter = "TopologyService Latency";

		// Token: 0x02000010 RID: 16
		public enum ScenarioId
		{
			// Token: 0x0400004D RID: 77
			PlaceHolderNoException = 1006,
			// Token: 0x0400004E RID: 78
			ExceptionThrown = 3006,
			// Token: 0x0400004F RID: 79
			AllTransactionsSucceeded = 3001
		}
	}
}
