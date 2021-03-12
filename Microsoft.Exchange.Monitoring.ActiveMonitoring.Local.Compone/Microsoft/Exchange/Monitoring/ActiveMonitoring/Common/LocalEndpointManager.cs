using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200053C RID: 1340
	internal class LocalEndpointManager
	{
		// Token: 0x060020D1 RID: 8401 RVA: 0x000C80C0 File Offset: 0x000C62C0
		private LocalEndpointManager()
		{
			CertificateValidationManager.RegisterCallback("DefaultAMComponent", new RemoteCertificateValidationCallback(LocalEndpointManager.ValidateRemoteCertificate));
			if (!LocalEndpointManager.UseMaintenanceWorkItem)
			{
				foreach (Type type in this.endpointDiscriptors.Keys)
				{
					this.TryInitializeEndpoint(type);
				}
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x000C86B4 File Offset: 0x000C68B4
		public static LocalEndpointManager Instance
		{
			get
			{
				if (LocalEndpointManager.instance == null)
				{
					lock (LocalEndpointManager.locker)
					{
						if (LocalEndpointManager.instance == null)
						{
							LocalEndpointManager.instance = new LocalEndpointManager();
						}
					}
				}
				return LocalEndpointManager.instance;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x000C870C File Offset: 0x000C690C
		// (set) Token: 0x060020D4 RID: 8404 RVA: 0x000C8713 File Offset: 0x000C6913
		public static bool UseMaintenanceWorkItem
		{
			get
			{
				return LocalEndpointManager.useMaintenanceWorkItem;
			}
			set
			{
				LocalEndpointManager.useMaintenanceWorkItem = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x000C871B File Offset: 0x000C691B
		public static bool IsDataCenter
		{
			get
			{
				return LocalEndpointManager.isDataCenter;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x000C8722 File Offset: 0x000C6922
		public static bool IsDataCenterDedicated
		{
			get
			{
				return LocalEndpointManager.isDataCenterDedicated;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x000C8729 File Offset: 0x000C6929
		public IEnumerable<MaintenanceDefinition> EndpointWorkitems
		{
			get
			{
				return this.endpointDiscriptors.Values;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x000C8736 File Offset: 0x000C6936
		public MailboxDatabaseEndpoint MailboxDatabaseEndpoint
		{
			get
			{
				return this.GetEndpoint(typeof(MailboxDatabaseEndpoint), false) as MailboxDatabaseEndpoint;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x000C874E File Offset: 0x000C694E
		public ExchangeServerRoleEndpoint ExchangeServerRoleEndpoint
		{
			get
			{
				return this.GetEndpoint(typeof(ExchangeServerRoleEndpoint), false) as ExchangeServerRoleEndpoint;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x000C8766 File Offset: 0x000C6966
		public WindowsServerRoleEndpoint WindowsServerRoleEndpoint
		{
			get
			{
				return this.GetEndpoint(typeof(WindowsServerRoleEndpoint), false) as WindowsServerRoleEndpoint;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x000C877E File Offset: 0x000C697E
		public UnifiedMessagingCallRouterEndpoint UnifiedMessagingCallRouterEndpoint
		{
			get
			{
				return this.GetEndpoint(typeof(UnifiedMessagingCallRouterEndpoint), false) as UnifiedMessagingCallRouterEndpoint;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x000C8796 File Offset: 0x000C6996
		public UnifiedMessagingServiceEndpoint UnifiedMessagingServiceEndpoint
		{
			get
			{
				return this.GetEndpoint(typeof(UnifiedMessagingServiceEndpoint), false) as UnifiedMessagingServiceEndpoint;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x000C87AE File Offset: 0x000C69AE
		public OfflineAddressBookEndpoint OfflineAddressBookEndpoint
		{
			get
			{
				return this.GetEndpoint(typeof(OfflineAddressBookEndpoint), false) as OfflineAddressBookEndpoint;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x000C87C6 File Offset: 0x000C69C6
		public SubjectListEndpoint SubjectListEndpoint
		{
			get
			{
				return this.GetEndpoint(typeof(SubjectListEndpoint), false) as SubjectListEndpoint;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x000C87DE File Offset: 0x000C69DE
		public MonitoringEndpoint MonitoringEndpoint
		{
			get
			{
				return this.GetEndpoint(typeof(MonitoringEndpoint), false) as MonitoringEndpoint;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x000C87F6 File Offset: 0x000C69F6
		public RecoveryActionsEnabledEndpoint RecoveryActionsEnabledEndpoint
		{
			get
			{
				return this.GetEndpoint(typeof(RecoveryActionsEnabledEndpoint), false) as RecoveryActionsEnabledEndpoint;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x000C880E File Offset: 0x000C6A0E
		public ScopeMappingLocalEndpoint ScopeMappingLocalEndpoint
		{
			get
			{
				return this.GetEndpoint(typeof(ScopeMappingLocalEndpoint), false) as ScopeMappingLocalEndpoint;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x000C8826 File Offset: 0x000C6A26
		// (set) Token: 0x060020E3 RID: 8419 RVA: 0x000C882E File Offset: 0x000C6A2E
		public bool RestartThrottlingAllowed
		{
			get
			{
				return this.restartThrottleAllowed;
			}
			set
			{
				this.restartThrottleAllowed = value;
			}
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000C8838 File Offset: 0x000C6A38
		public void SetEndpoint(Type type, IEndpoint endpoint, bool validate = true)
		{
			if (validate && !this.endpointDiscriptors.ContainsKey(type))
			{
				throw new ArgumentException("Invalid endpoint type: " + type.Name);
			}
			this.endpoints[type] = endpoint;
			if (this.endpoints.Count == this.endpointDiscriptors.Count)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.LocalEndpointManagerTracer, this.traceContext, "LocalEndpointManager.SetEndpoint: Signal startup notification {0}", LocalDataAccess.EndpointManagerNotificationId, null, "SetEndpoint", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalEndpointManager.cs", 482);
				StartupNotification.InsertStartupNotification(LocalDataAccess.EndpointManagerNotificationId);
			}
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000C88C8 File Offset: 0x000C6AC8
		public IEndpoint GetEndpoint(Type type, bool throwIfEndpointContainsException = false)
		{
			IEndpoint endpoint;
			if (!this.endpoints.TryGetValue(type, out endpoint))
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.LocalEndpointManagerTracer, this.traceContext, "Cannot find endpoint type '{0}' in this.endpoints.", type.FullName, null, "GetEndpoint", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalEndpointManager.cs", 500);
				throw new EndpointManagerEndpointUninitializedException(Strings.EndpointManagerEndpointUninitialized(type.FullName));
			}
			if (endpoint != null && endpoint.Exception != null)
			{
				WTFDiagnostics.TraceInformation<string, bool, string>(ExTraceGlobals.LocalEndpointManagerTracer, this.traceContext, "Endpoint type '{0}' contains an exception (throwIfEndpointContainsException={1}): {2}", type.FullName, throwIfEndpointContainsException, endpoint.Exception.ToString(), null, "GetEndpoint", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalEndpointManager.cs", 508);
				if (throwIfEndpointContainsException)
				{
					throw endpoint.Exception;
				}
			}
			return endpoint;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000C896E File Offset: 0x000C6B6E
		public bool IsEndpointInitialized(Type type)
		{
			return this.endpoints.ContainsKey(type);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000C897C File Offset: 0x000C6B7C
		private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return true;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000C8980 File Offset: 0x000C6B80
		private void TryInitializeEndpoint(Type type)
		{
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.LocalEndpointManagerTracer, this.traceContext, "Initializing monitoring endpoint: {0}", type.FullName, null, "TryInitializeEndpoint", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalEndpointManager.cs", 562);
			this.endpoints[type] = null;
			try
			{
				IEndpoint endpoint = (IEndpoint)Activator.CreateInstance(type);
				endpoint.Initialize();
				this.endpoints[type] = endpoint;
			}
			catch (Exception arg)
			{
				WTFDiagnostics.TraceError<string, Exception>(ExTraceGlobals.LocalEndpointManagerTracer, this.traceContext, "Monitoring endpoint {0} initialization failed with exception: {1}", type.FullName, arg, null, "TryInitializeEndpoint", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\LocalEndpointManager.cs", 579);
			}
		}

		// Token: 0x0400181A RID: 6170
		private static readonly bool isDataCenter = Datacenter.IsMicrosoftHostedOnly(true);

		// Token: 0x0400181B RID: 6171
		private static readonly bool isDataCenterDedicated = Datacenter.IsDatacenterDedicated(true);

		// Token: 0x0400181C RID: 6172
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400181D RID: 6173
		private readonly Dictionary<Type, MaintenanceDefinition> endpointDiscriptors = new Dictionary<Type, MaintenanceDefinition>
		{
			{
				typeof(MailboxDatabaseEndpoint),
				new MaintenanceDefinition
				{
					AssemblyPath = LocalEndpointManager.AssemblyPath,
					TypeName = typeof(EndpointMaintenance<MailboxDatabaseEndpoint>).FullName,
					Name = EndpointMaintenance<MailboxDatabaseEndpoint>.GetDefaultName(),
					ServiceName = ExchangeComponent.Monitoring.Name,
					RecurrenceIntervalSeconds = 900,
					TimeoutSeconds = 900,
					MaxRestartRequestAllowedPerHour = 2,
					Enabled = true
				}
			},
			{
				typeof(ExchangeServerRoleEndpoint),
				new MaintenanceDefinition
				{
					AssemblyPath = LocalEndpointManager.AssemblyPath,
					TypeName = typeof(EndpointMaintenance<ExchangeServerRoleEndpoint>).FullName,
					Name = EndpointMaintenance<ExchangeServerRoleEndpoint>.GetDefaultName(),
					ServiceName = ExchangeComponent.Monitoring.Name,
					RecurrenceIntervalSeconds = 0,
					TimeoutSeconds = 30,
					Enabled = true
				}
			},
			{
				typeof(WindowsServerRoleEndpoint),
				new MaintenanceDefinition
				{
					AssemblyPath = LocalEndpointManager.AssemblyPath,
					TypeName = typeof(EndpointMaintenance<WindowsServerRoleEndpoint>).FullName,
					Name = EndpointMaintenance<WindowsServerRoleEndpoint>.GetDefaultName(),
					ServiceName = ExchangeComponent.Monitoring.Name,
					RecurrenceIntervalSeconds = 0,
					TimeoutSeconds = 120,
					Enabled = true
				}
			},
			{
				typeof(UnifiedMessagingCallRouterEndpoint),
				new MaintenanceDefinition
				{
					AssemblyPath = LocalEndpointManager.AssemblyPath,
					TypeName = typeof(EndpointMaintenance<UnifiedMessagingCallRouterEndpoint>).FullName,
					Name = EndpointMaintenance<UnifiedMessagingCallRouterEndpoint>.GetDefaultName(),
					ServiceName = ExchangeComponent.UMCallRouter.Name,
					RecurrenceIntervalSeconds = 600,
					TimeoutSeconds = 300,
					MaxRestartRequestAllowedPerHour = 2,
					Enabled = true
				}
			},
			{
				typeof(UnifiedMessagingServiceEndpoint),
				new MaintenanceDefinition
				{
					AssemblyPath = LocalEndpointManager.AssemblyPath,
					TypeName = typeof(EndpointMaintenance<UnifiedMessagingServiceEndpoint>).FullName,
					Name = EndpointMaintenance<UnifiedMessagingServiceEndpoint>.GetDefaultName(),
					ServiceName = ExchangeComponent.UMProtocol.Name,
					RecurrenceIntervalSeconds = 600,
					TimeoutSeconds = 300,
					MaxRestartRequestAllowedPerHour = 2,
					Enabled = true
				}
			},
			{
				typeof(OfflineAddressBookEndpoint),
				new MaintenanceDefinition
				{
					AssemblyPath = LocalEndpointManager.AssemblyPath,
					TypeName = typeof(EndpointMaintenance<OfflineAddressBookEndpoint>).FullName,
					Name = EndpointMaintenance<OfflineAddressBookEndpoint>.GetDefaultName(),
					ServiceName = ExchangeComponent.Oab.Name,
					RecurrenceIntervalSeconds = 600,
					TimeoutSeconds = 300,
					MaxRestartRequestAllowedPerHour = 2,
					Enabled = true
				}
			},
			{
				typeof(SubjectListEndpoint),
				new MaintenanceDefinition
				{
					AssemblyPath = LocalEndpointManager.AssemblyPath,
					TypeName = typeof(EndpointMaintenance<SubjectListEndpoint>).FullName,
					Name = EndpointMaintenance<SubjectListEndpoint>.GetDefaultName(),
					ServiceName = ExchangeComponent.Monitoring.Name,
					RecurrenceIntervalSeconds = 900,
					TimeoutSeconds = 150,
					MaxRestartRequestAllowedPerHour = 2,
					Enabled = true
				}
			},
			{
				typeof(MonitoringEndpoint),
				new MaintenanceDefinition
				{
					AssemblyPath = LocalEndpointManager.AssemblyPath,
					TypeName = typeof(EndpointMaintenance<MonitoringEndpoint>).FullName,
					Name = EndpointMaintenance<MonitoringEndpoint>.GetDefaultName(),
					ServiceName = ExchangeComponent.Monitoring.Name,
					RecurrenceIntervalSeconds = 300,
					TimeoutSeconds = 150,
					MaxRestartRequestAllowedPerHour = 2,
					Enabled = true
				}
			},
			{
				typeof(RecoveryActionsEnabledEndpoint),
				new MaintenanceDefinition
				{
					AssemblyPath = LocalEndpointManager.AssemblyPath,
					TypeName = typeof(EndpointMaintenance<RecoveryActionsEnabledEndpoint>).FullName,
					Name = EndpointMaintenance<RecoveryActionsEnabledEndpoint>.GetDefaultName(),
					ServiceName = ExchangeComponent.Monitoring.Name,
					RecurrenceIntervalSeconds = 300,
					TimeoutSeconds = 150,
					MaxRestartRequestAllowedPerHour = 2,
					Enabled = true
				}
			},
			{
				typeof(OverrideEndpoint),
				new MaintenanceDefinition
				{
					AssemblyPath = LocalEndpointManager.AssemblyPath,
					TypeName = typeof(EndpointMaintenance<OverrideEndpoint>).FullName,
					Name = EndpointMaintenance<OverrideEndpoint>.GetDefaultName(),
					ServiceName = ExchangeComponent.Monitoring.Name,
					RecurrenceIntervalSeconds = 300,
					TimeoutSeconds = 150,
					MaxRestartRequestAllowedPerHour = 3,
					Enabled = true
				}
			},
			{
				typeof(ScopeMappingLocalEndpoint),
				new MaintenanceDefinition
				{
					AssemblyPath = LocalEndpointManager.AssemblyPath,
					TypeName = typeof(EndpointMaintenance<ScopeMappingLocalEndpoint>).FullName,
					Name = EndpointMaintenance<ScopeMappingLocalEndpoint>.GetDefaultName(),
					ServiceName = ExchangeComponent.Monitoring.Name,
					RecurrenceIntervalSeconds = 0,
					TimeoutSeconds = 300,
					MaxRestartRequestAllowedPerHour = 3,
					Enabled = true
				}
			}
		};

		// Token: 0x0400181E RID: 6174
		private static bool useMaintenanceWorkItem = false;

		// Token: 0x0400181F RID: 6175
		private static LocalEndpointManager instance = null;

		// Token: 0x04001820 RID: 6176
		private static object locker = new object();

		// Token: 0x04001821 RID: 6177
		private ConcurrentDictionary<Type, IEndpoint> endpoints = new ConcurrentDictionary<Type, IEndpoint>();

		// Token: 0x04001822 RID: 6178
		private bool restartThrottleAllowed = true;

		// Token: 0x04001823 RID: 6179
		private TracingContext traceContext = TracingContext.Default;
	}
}
