using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005E6 RID: 1510
	[Cmdlet("Test", "ServiceHealth", SupportsShouldProcess = true)]
	public sealed class TestServiceHealth : Task
	{
		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x060035B7 RID: 13751 RVA: 0x000DCDD6 File Offset: 0x000DAFD6
		// (set) Token: 0x060035B8 RID: 13752 RVA: 0x000DCDED File Offset: 0x000DAFED
		[Parameter]
		public Fqdn DomainController
		{
			get
			{
				return (Fqdn)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x060035B9 RID: 13753 RVA: 0x000DCE00 File Offset: 0x000DB000
		// (set) Token: 0x060035BA RID: 13754 RVA: 0x000DCE17 File Offset: 0x000DB017
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		[Alias(new string[]
		{
			"Identity"
		})]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x000DCE2A File Offset: 0x000DB02A
		// (set) Token: 0x060035BC RID: 13756 RVA: 0x000DCE4B File Offset: 0x000DB04B
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

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x060035BD RID: 13757 RVA: 0x000DCE63 File Offset: 0x000DB063
		// (set) Token: 0x060035BE RID: 13758 RVA: 0x000DCE85 File Offset: 0x000DB085
		[ValidateRange(1, 2147483647)]
		[Parameter(Mandatory = false)]
		public int ActiveDirectoryTimeout
		{
			get
			{
				return (int)(base.Fields["ActiveDirectoryTimeout"] ?? 15);
			}
			set
			{
				base.Fields["ActiveDirectoryTimeout"] = value;
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000DCEA0 File Offset: 0x000DB0A0
		static TestServiceHealth()
		{
			Array.Sort<TestServiceHealth.MonitoredService>(TestServiceHealth.PassiveNodeMonitoredServicesArray);
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x060035C0 RID: 13760 RVA: 0x000DD0EB File Offset: 0x000DB2EB
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestServiceHealth(this.serverName);
			}
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000DD0F8 File Offset: 0x000DB2F8
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception) || MonitoringHelper.IsKnownExceptionForMonitoring(exception);
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000DD113 File Offset: 0x000DB313
		private void WriteErrorAndMonitoringEvent(Exception exception, ErrorCategory errorCategory, object target, int eventId, string eventSource)
		{
			this.monitoringData.Events.Add(new MonitoringEvent(eventSource, eventId, EventTypeEnumeration.Error, exception.Message));
			base.WriteError(exception, errorCategory, target);
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000DD13E File Offset: 0x000DB33E
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000DD14C File Offset: 0x000DB34C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				IEnumerable<TestServiceHealth.MonitoredService> source;
				if (Datacenter.IsMultiTenancyEnabled())
				{
					source = TestServiceHealth.DefaultMonitoredServicesList;
				}
				else
				{
					source = TestServiceHealth.DefaultMonitoredServicesList.Concat(new TestServiceHealth.MonitoredService[]
					{
						new TestServiceHealth.MonitoredService("MSExchangeEdgeSync", false, true, ServerRole.HubTransport | ServerRole.Edge)
					});
				}
				source = from monitoredService in source
				orderby monitoredService
				select monitoredService;
				this.monitoredServicesArray = source.ToArray<TestServiceHealth.MonitoredService>();
				if (this.Server == null)
				{
					this.serverName = NativeHelpers.GetLocalComputerFqdn(false);
					try
					{
						this.serverRolesBitfield = MpServerRoles.GetLocalE12ServerRolesFromRegistry();
						goto IL_1AF;
					}
					catch (SecurityException ex)
					{
						CannotReadRolesFromRegistryException exception = new CannotReadRolesFromRegistryException(ex.Message);
						this.WriteErrorAndMonitoringEvent(exception, ErrorCategory.PermissionDenied, null, 10003, "MSExchange Monitoring ServiceHealth");
						return;
					}
				}
				IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 708, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Tasks\\TestServiceHealth.cs");
				configurationSession.ServerTimeout = new TimeSpan?(TimeSpan.FromSeconds((double)this.ActiveDirectoryTimeout));
				IEnumerable<Server> objects = this.Server.GetObjects<Server>(null, configurationSession);
				IEnumerator<Server> enumerator = objects.GetEnumerator();
				Server server = null;
				if (enumerator.MoveNext())
				{
					server = enumerator.Current;
				}
				if (server == null || enumerator.MoveNext())
				{
					CannotLocateServerException exception2;
					if (server == null)
					{
						exception2 = new CannotLocateServerException(Strings.ErrorServerNotFound(this.Server.ToString()));
					}
					else
					{
						exception2 = new CannotLocateServerException(Strings.ErrorServerNotUnique(this.Server.ToString()));
					}
					this.WriteErrorAndMonitoringEvent(exception2, ErrorCategory.InvalidData, null, 10007, "MSExchange Monitoring ServiceHealth");
					return;
				}
				this.serverName = server.Fqdn;
				this.serverRolesBitfield = server.CurrentServerRole;
				IL_1AF:
				this.serverRolesList = new List<ServerRole>(MpServerRoles.ValidE12MpRoles.Length);
				foreach (ServerRole serverRole in MpServerRoles.ValidE12MpRoles)
				{
					if ((serverRole & this.serverRolesBitfield) != ServerRole.None)
					{
						this.serverRolesList.Add(serverRole);
					}
				}
				if (!base.HasErrors && this.serverRolesList.Count < 1)
				{
					NoExchangeRoleInstalledException exception3 = new NoExchangeRoleInstalledException(this.serverName);
					this.WriteErrorAndMonitoringEvent(exception3, ErrorCategory.InvalidArgument, null, 10007, "MSExchange Monitoring ServiceHealth");
				}
			}
			finally
			{
				if (base.HasErrors && this.MonitoringContext)
				{
					base.WriteObject(this.monitoringData);
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000DD3E4 File Offset: 0x000DB5E4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				Dictionary<ServerRole, List<string>> dictionary = new Dictionary<ServerRole, List<string>>();
				Dictionary<ServerRole, List<string>> dictionary2 = new Dictionary<ServerRole, List<string>>();
				foreach (TestServiceHealth.MonitoredService monitoredService in this.monitoredServicesArray)
				{
					if ((monitoredService.Roles & this.serverRolesBitfield) != ServerRole.None)
					{
						try
						{
							using (ManagementObject serviceObject = WmiWrapper.GetServiceObject(this.serverName, monitoredService.Name))
							{
								if (serviceObject != null)
								{
									string strA = serviceObject["State"] as string;
									if (string.Compare(strA, "Running", true, CultureInfo.InvariantCulture) == 0)
									{
										using (List<ServerRole>.Enumerator enumerator = this.serverRolesList.GetEnumerator())
										{
											while (enumerator.MoveNext())
											{
												ServerRole serverRole = enumerator.Current;
												if ((monitoredService.Roles & serverRole) != ServerRole.None)
												{
													List<string> list = null;
													if (!dictionary2.TryGetValue(serverRole, out list))
													{
														list = new List<string>();
														dictionary2[serverRole] = list;
													}
													list.Add(monitoredService.Name);
												}
											}
											goto IL_18C;
										}
									}
									string strA2 = serviceObject["StartMode"] as string;
									if (string.Compare(strA2, "Auto", true, CultureInfo.InvariantCulture) == 0 || monitoredService.IsRequired)
									{
										foreach (ServerRole serverRole2 in this.serverRolesList)
										{
											if ((serverRole2 & monitoredService.Roles) != ServerRole.None)
											{
												List<string> list2 = null;
												if (!dictionary.TryGetValue(serverRole2, out list2))
												{
													list2 = new List<string>();
													dictionary[serverRole2] = list2;
												}
												list2.Add(monitoredService.Name);
											}
										}
									}
									IL_18C:;
								}
							}
						}
						catch (WmiException ex)
						{
							ServiceHealthWmiFailureException exception = new ServiceHealthWmiFailureException(ex.Message, ex);
							base.WriteError(exception, ErrorCategory.ReadError, null);
						}
					}
				}
				foreach (ServerRole serverRole3 in this.serverRolesList)
				{
					List<string> list3 = null;
					List<string> list4 = null;
					string[] servicesRunning;
					if (dictionary2.TryGetValue(serverRole3, out list3))
					{
						servicesRunning = list3.ToArray();
					}
					else
					{
						servicesRunning = new string[0];
					}
					ExchangeServicesStatus sendToPipeline;
					if (dictionary.TryGetValue(serverRole3, out list4))
					{
						string[] array2 = list4.ToArray();
						this.monitoringData.Events.Add(new MonitoringEvent("MSExchange Monitoring ServiceHealth", (int)(11000 + serverRole3), EventTypeEnumeration.Error, Strings.SomeEssentialServicesForTheRoleNotRunning(MpServerRoles.DisplayRoleName(serverRole3), string.Join("\n", array2))));
						sendToPipeline = new ExchangeServicesStatus(serverRole3, false, array2, servicesRunning);
					}
					else
					{
						this.monitoringData.Events.Add(new MonitoringEvent("MSExchange Monitoring ServiceHealth", (int)(12000 + serverRole3), EventTypeEnumeration.Information, Strings.AllEssentialServicesForTheRoleRunning(MpServerRoles.DisplayRoleName(serverRole3))));
						sendToPipeline = new ExchangeServicesStatus(serverRole3, true, new string[0], servicesRunning);
					}
					base.WriteObject(sendToPipeline);
				}
				this.monitoringData.Events.Add(new MonitoringEvent("MSExchange Monitoring ServiceHealth", 10000, EventTypeEnumeration.Information, Strings.NoMonitoringErrorsInTestServiceHealthTask));
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

		// Token: 0x060035C6 RID: 13766 RVA: 0x000DD7B0 File Offset: 0x000DB9B0
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.monitoringData = new MonitoringData();
		}

		// Token: 0x040024CC RID: 9420
		private const string CmdletNoun = "ServiceHealth";

		// Token: 0x040024CD RID: 9421
		private const string CmdletMonitoringEventSource = "MSExchange Monitoring ServiceHealth";

		// Token: 0x040024CE RID: 9422
		private const int DefaultADOperationsTimeoutInSeconds = 15;

		// Token: 0x040024CF RID: 9423
		private static readonly List<TestServiceHealth.MonitoredService> DefaultMonitoredServicesList = new List<TestServiceHealth.MonitoredService>
		{
			new TestServiceHealth.MonitoredService("MSExchangeTransportLogSearch", false, false, ServerRole.Mailbox | ServerRole.HubTransport | ServerRole.Edge),
			new TestServiceHealth.MonitoredService("MSExchangeTransport", false, true, ServerRole.HubTransport | ServerRole.Edge),
			new TestServiceHealth.MonitoredService("IISAdmin", false, false, ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport),
			new TestServiceHealth.MonitoredService("MSExchangeADTopology", false, true, ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport),
			new TestServiceHealth.MonitoredService("MSExchangeServiceHost", false, true, ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport | ServerRole.Edge),
			new TestServiceHealth.MonitoredService("MSExchangeIS", true, true, ServerRole.Mailbox),
			new TestServiceHealth.MonitoredService("MSExchangeMailboxAssistants", false, false, ServerRole.Mailbox),
			new TestServiceHealth.MonitoredService("MSExchangeRepl", false, true, ServerRole.Mailbox),
			new TestServiceHealth.MonitoredService("MSExchangeSA", true, true, ServerRole.Mailbox),
			new TestServiceHealth.MonitoredService("MSExchangeSearch", false, true, ServerRole.Mailbox),
			new TestServiceHealth.MonitoredService("MSExchangeUM", false, true, ServerRole.UnifiedMessaging),
			new TestServiceHealth.MonitoredService("MSExchangeIMAP4", false, false, ServerRole.ClientAccess),
			new TestServiceHealth.MonitoredService("MSExchangePOP3", false, false, ServerRole.ClientAccess),
			new TestServiceHealth.MonitoredService("W3Svc", true, false, ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport),
			new TestServiceHealth.MonitoredService("ADAM_MSExchange", false, true, ServerRole.Edge),
			new TestServiceHealth.MonitoredService("MSExchangeEdgeCredential", false, true, ServerRole.Edge),
			new TestServiceHealth.MonitoredService("MSExchangeThrottling", false, true, ServerRole.Mailbox),
			new TestServiceHealth.MonitoredService("MSExchangeMailboxReplication", false, false, ServerRole.ClientAccess),
			new TestServiceHealth.MonitoredService("MSExchangeRPC", false, true, ServerRole.Mailbox | ServerRole.ClientAccess),
			new TestServiceHealth.MonitoredService("WinRM", false, true, ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport),
			new TestServiceHealth.MonitoredService("MSExchangeProtectedServiceHost", false, true, ServerRole.ClientAccess),
			new TestServiceHealth.MonitoredService("MSExchangeFBA", false, false, ServerRole.ClientAccess),
			new TestServiceHealth.MonitoredService("MSExchangeSubmission", false, true, ServerRole.Mailbox),
			new TestServiceHealth.MonitoredService("MSExchangeFrontendTransport", false, true, ServerRole.FrontendTransport),
			new TestServiceHealth.MonitoredService("MSExchangeDelivery", false, true, ServerRole.Mailbox)
		};

		// Token: 0x040024D0 RID: 9424
		private static readonly TestServiceHealth.MonitoredService[] PassiveNodeMonitoredServicesArray = new TestServiceHealth.MonitoredService[]
		{
			new TestServiceHealth.MonitoredService("MSExchangeSearch", false, true, ServerRole.Mailbox),
			new TestServiceHealth.MonitoredService("MSExchangeRepl", false, true, ServerRole.Mailbox)
		};

		// Token: 0x040024D1 RID: 9425
		private MonitoringData monitoringData;

		// Token: 0x040024D2 RID: 9426
		private string serverName;

		// Token: 0x040024D3 RID: 9427
		private ServerRole serverRolesBitfield;

		// Token: 0x040024D4 RID: 9428
		private List<ServerRole> serverRolesList;

		// Token: 0x040024D5 RID: 9429
		private TestServiceHealth.MonitoredService[] monitoredServicesArray;

		// Token: 0x020005E7 RID: 1511
		private static class EventId
		{
			// Token: 0x040024D7 RID: 9431
			internal const int NoMonitoringError = 10000;

			// Token: 0x040024D8 RID: 9432
			internal const int CannotReadRoleFromRegistry = 10003;

			// Token: 0x040024D9 RID: 9433
			internal const int NoE12RoleInstalled = 10005;

			// Token: 0x040024DA RID: 9434
			internal const int CannotLocateServer = 10007;

			// Token: 0x040024DB RID: 9435
			internal const int SvcsNotRunningBaseId = 11000;

			// Token: 0x040024DC RID: 9436
			internal const int AllSvcsRunningBaseId = 12000;
		}

		// Token: 0x020005E8 RID: 1512
		private struct MonitoredService : IComparable
		{
			// Token: 0x17001002 RID: 4098
			// (get) Token: 0x060035C9 RID: 13769 RVA: 0x000DD7CB File Offset: 0x000DB9CB
			internal string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17001003 RID: 4099
			// (get) Token: 0x060035CA RID: 13770 RVA: 0x000DD7D3 File Offset: 0x000DB9D3
			internal bool IsRequired
			{
				get
				{
					return this.isRequired;
				}
			}

			// Token: 0x17001004 RID: 4100
			// (get) Token: 0x060035CB RID: 13771 RVA: 0x000DD7DB File Offset: 0x000DB9DB
			internal ServerRole Roles
			{
				get
				{
					return this.roles;
				}
			}

			// Token: 0x060035CC RID: 13772 RVA: 0x000DD7E3 File Offset: 0x000DB9E3
			internal MonitoredService(string name, bool isClusterAware, bool isRequired, ServerRole roles)
			{
				this = default(TestServiceHealth.MonitoredService);
				this.name = name;
				this.isRequired = isRequired;
				this.roles = roles;
			}

			// Token: 0x060035CD RID: 13773 RVA: 0x000DD804 File Offset: 0x000DBA04
			public int CompareTo(object obj)
			{
				TestServiceHealth.MonitoredService monitoredService = (TestServiceHealth.MonitoredService)obj;
				return string.Compare(this.Name, monitoredService.Name, StringComparison.CurrentCultureIgnoreCase);
			}

			// Token: 0x040024DD RID: 9437
			private string name;

			// Token: 0x040024DE RID: 9438
			private bool isRequired;

			// Token: 0x040024DF RID: 9439
			private ServerRole roles;
		}
	}
}
