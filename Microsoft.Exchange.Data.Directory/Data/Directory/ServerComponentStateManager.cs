using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000193 RID: 403
	internal class ServerComponentStateManager
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00052937 File Offset: 0x00050B37
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ServerComponentStateManagerTracer;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x0005293E File Offset: 0x00050B3E
		private static ServerComponentStateManager Instance
		{
			get
			{
				if (ServerComponentStateManager.sm_instance == null)
				{
					ServerComponentStateManager.sm_instance = new ServerComponentStateManager();
				}
				return ServerComponentStateManager.sm_instance;
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00052956 File Offset: 0x00050B56
		public static string GetComponentId(ServerComponentEnum serverComponent)
		{
			return serverComponent.ToString();
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00052964 File Offset: 0x00050B64
		public static bool IsValidComponent(string componentId)
		{
			ServerComponentEnum serverComponent;
			return Enum.TryParse<ServerComponentEnum>(componentId, true, out serverComponent) && ServerComponentStateManager.IsValidComponent(serverComponent);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00052984 File Offset: 0x00050B84
		public static bool IsValidComponent(ServerComponentEnum serverComponent)
		{
			return serverComponent != ServerComponentEnum.None && serverComponent != ServerComponentEnum.TestComponent && serverComponent != ServerComponentEnum.ServerWideSettings;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00052998 File Offset: 0x00050B98
		public static ServiceState GetDefaultState(string componentId)
		{
			ServerComponentEnum serverComponent;
			if (!Enum.TryParse<ServerComponentEnum>(componentId, true, out serverComponent))
			{
				throw new ArgumentException(DirectoryStrings.ComponentNameInvalid);
			}
			return ServerComponentStateManager.GetDefaultState(serverComponent);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000529C8 File Offset: 0x00050BC8
		public static ServiceState GetDefaultState(ServerComponentEnum serverComponent)
		{
			ServerComponentStateManager.ComponentStateData componentStateData;
			if (!ServerComponentStateManager.defaultComponentStates.TryGetValue(serverComponent, out componentStateData))
			{
				return ServiceState.Active;
			}
			if (!Datacenter.IsMicrosoftHostedOnly(true))
			{
				return componentStateData.OnPremState;
			}
			return componentStateData.DatacenterState;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000529FD File Offset: 0x00050BFD
		public static bool IsOnline(ServerComponentEnum serverComponent)
		{
			return ServerComponentStateManager.IsOnline(serverComponent, true);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00052A08 File Offset: 0x00050C08
		public static bool IsOnline(ServerComponentEnum serverComponent, bool onlineByDefault)
		{
			ServiceState effectiveState = ServerComponentStateManager.GetEffectiveState(serverComponent, onlineByDefault);
			return effectiveState == ServiceState.Active || effectiveState == ServiceState.Draining;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00052A28 File Offset: 0x00050C28
		public static ServiceState GetEffectiveState(ServerComponentEnum serverComponent)
		{
			ServiceState defaultState = ServerComponentStateManager.GetDefaultState(serverComponent);
			return ServerComponentStateManager.GetEffectiveState(serverComponent, defaultState != ServiceState.Inactive);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00052A7C File Offset: 0x00050C7C
		public static ServiceState GetEffectiveState(ServerComponentEnum serverComponent, bool onlineByDefault)
		{
			MultiValuedProperty<string> adStates = null;
			try
			{
				adStates = ServerComponentStateManager.Instance.GetAdStates();
			}
			catch (ServerComponentApiException arg)
			{
				ServerComponentStateManager.Tracer.TraceError<ServerComponentApiException>(0L, "GetEffectiveState ignoring failure to get adStates. Ex was {0}", arg);
			}
			ServiceState compState = ServiceState.Inactive;
			ServerComponentStateManager.RunLocalRegistryOperation(delegate
			{
				compState = ServerComponentStates.ReadEffectiveComponentState(null, adStates, ServerComponentStates.GetComponentId(serverComponent), onlineByDefault ? ServiceState.Active : ServiceState.Inactive);
			});
			return compState;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00052B28 File Offset: 0x00050D28
		public static void SetOffline(ServerComponentEnum serverComponent)
		{
			Exception ex = ServerComponentStateManager.RunLocalRegistryOperationNoThrow(delegate
			{
				ServerComponentStates.UpdateLocalState(null, ServerComponentRequest.HealthApi.ToString(), serverComponent.ToString(), ServiceState.Inactive);
			});
			if (ex != null)
			{
				ServerComponentStateManager.Tracer.TraceError<ServerComponentEnum, Exception>(0L, "SetOffline({0}) failed: {1}", serverComponent, ex);
				throw new ServerComponentApiException(DirectoryStrings.ServerComponentLocalRegistryError(ex.ToString()), ex);
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00052BB0 File Offset: 0x00050DB0
		public static void SetOnline(ServerComponentEnum serverComponent)
		{
			Exception ex = ServerComponentStateManager.RunLocalRegistryOperationNoThrow(delegate
			{
				ServerComponentStates.UpdateLocalState(null, ServerComponentRequest.HealthApi.ToString(), serverComponent.ToString(), ServiceState.Active);
			});
			if (ex != null)
			{
				ServerComponentStateManager.Tracer.TraceError<ServerComponentEnum, Exception>(0L, "SetOnline({0}) failed: {1}", serverComponent, ex);
				throw new ServerComponentApiException(DirectoryStrings.ServerComponentLocalRegistryError(ex.ToString()), ex);
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00052C38 File Offset: 0x00050E38
		public static void SyncAdState()
		{
			MultiValuedProperty<string> adStates = null;
			Exception ex = ServerComponentStateManager.RunADOperationNoThrow(delegate
			{
				adStates = ServerComponentStateManager.ReadLocalServerComponentStatesFromAD();
				ServerComponentStateManager.UseAdStates(adStates);
			});
			if (ex != null)
			{
				ServerComponentStateManager.Tracer.TraceError<Exception>(0L, "SyncAdState failed to read from AD: {0}", ex);
				throw new ServerComponentApiException(DirectoryStrings.ServerComponentReadADError(ex.ToString()), ex);
			}
			Exception ex2 = ServerComponentStateManager.RunLocalRegistryOperationNoThrow(delegate
			{
				ServerComponentStates.SyncComponentStates(adStates);
			});
			if (ex2 != null)
			{
				ServerComponentStateManager.Tracer.TraceError<Exception>(0L, "SyncAdState failed in ServerComponentStates.SyncComponentStates: {0}", ex2);
				throw new ServerComponentApiException(DirectoryStrings.ServerComponentLocalRegistryError(ex2.ToString()), ex2);
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00052CC4 File Offset: 0x00050EC4
		internal static void UseAdStates(MultiValuedProperty<string> adStates)
		{
			ServerComponentStateManager.Instance.SetAdStates(adStates);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00052CD4 File Offset: 0x00050ED4
		public static Exception RunLocalRegistryOperationNoThrow(Action codeToRun)
		{
			Exception result = null;
			try
			{
				codeToRun();
			}
			catch (UnauthorizedAccessException ex)
			{
				result = ex;
			}
			catch (SecurityException ex2)
			{
				result = ex2;
			}
			catch (IOException ex3)
			{
				result = ex3;
			}
			return result;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00052D24 File Offset: 0x00050F24
		private static void RunLocalRegistryOperation(Action codeToRun)
		{
			Exception ex = ServerComponentStateManager.RunLocalRegistryOperationNoThrow(codeToRun);
			if (ex != null)
			{
				throw new ServerComponentApiException(DirectoryStrings.ServerComponentLocalRegistryError(ex.ToString()), ex);
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00052D50 File Offset: 0x00050F50
		private static Exception RunADOperationNoThrow(Action codeToRun)
		{
			Exception result = null;
			try
			{
				codeToRun();
			}
			catch (ADExternalException ex)
			{
				result = ex;
			}
			catch (ADOperationException ex2)
			{
				result = ex2;
			}
			catch (ADTransientException ex3)
			{
				result = ex3;
			}
			return result;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00052DA0 File Offset: 0x00050FA0
		private static MultiValuedProperty<string> ReadLocalServerComponentStatesFromAD()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 480, "ReadLocalServerComponentStatesFromAD", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\ServerComponentStateManager.cs");
			string machineName = Environment.MachineName;
			Server server = topologyConfigurationSession.FindServerByName(machineName);
			if (server != null)
			{
				return server.ComponentStates;
			}
			topologyConfigurationSession.UseConfigNC = false;
			topologyConfigurationSession.UseGlobalCatalog = true;
			ADComputer adcomputer = topologyConfigurationSession.FindComputerByHostName(machineName);
			if (adcomputer == null)
			{
				throw new LocalServerNotFoundException(machineName);
			}
			return adcomputer.ComponentStates;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00052E0B File Offset: 0x0005100B
		private MultiValuedProperty<string> GetAdStates()
		{
			if (!this.m_lastKnownADComponentStatesAreSet)
			{
				this.LazyInit();
			}
			return this.m_lastKnownADComponentStates;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00052E21 File Offset: 0x00051021
		private void SetAdStates(MultiValuedProperty<string> adStates)
		{
			this.m_lastKnownADComponentStates = adStates;
			this.m_lastKnownADComponentStatesAreSet = true;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00052E48 File Offset: 0x00051048
		private void LazyInit()
		{
			int num = 30;
			try
			{
				if (!Monitor.TryEnter(this.syncLock, TimeSpan.FromSeconds((double)num)))
				{
					throw new ServerComponentApiException(DirectoryStrings.ServerComponentReadTimeout(num));
				}
				if (!this.m_lastKnownADComponentStatesAreSet)
				{
					MultiValuedProperty<string> adStates = null;
					Exception ex = ServerComponentStateManager.RunADOperationNoThrow(delegate
					{
						adStates = ServerComponentStateManager.ReadLocalServerComponentStatesFromAD();
					});
					if (ex != null)
					{
						ServerComponentStateManager.Tracer.TraceError<Exception>(0L, "LazyInit failed: {0}", ex);
					}
					this.SetAdStates(adStates);
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.syncLock))
				{
					Monitor.Exit(this.syncLock);
				}
			}
		}

		// Token: 0x040009C7 RID: 2503
		private const int MaxWaitForLazyInitInSecs = 30;

		// Token: 0x040009C8 RID: 2504
		private static readonly string serverFqdn = ComputerInformation.DnsFullyQualifiedDomainName;

		// Token: 0x040009C9 RID: 2505
		private static readonly string ServerWideOfflineComponentId = ServerComponentEnum.ServerWideOffline.ToString();

		// Token: 0x040009CA RID: 2506
		private static readonly Dictionary<ServerComponentEnum, ServerComponentStateManager.ComponentStateData> defaultComponentStates = new Dictionary<ServerComponentEnum, ServerComponentStateManager.ComponentStateData>
		{
			{
				ServerComponentEnum.HubTransport,
				new ServerComponentStateManager.ComponentStateData
				{
					OnPremState = ServiceState.Active,
					DatacenterState = ServiceState.Inactive
				}
			},
			{
				ServerComponentEnum.FrontendTransport,
				new ServerComponentStateManager.ComponentStateData
				{
					OnPremState = ServiceState.Active,
					DatacenterState = ServiceState.Inactive
				}
			},
			{
				ServerComponentEnum.ForwardSyncDaemon,
				new ServerComponentStateManager.ComponentStateData
				{
					OnPremState = ServiceState.Inactive,
					DatacenterState = ServiceState.Inactive
				}
			},
			{
				ServerComponentEnum.ProvisioningRps,
				new ServerComponentStateManager.ComponentStateData
				{
					OnPremState = ServiceState.Inactive,
					DatacenterState = ServiceState.Inactive
				}
			}
		};

		// Token: 0x040009CB RID: 2507
		private static ServerComponentStateManager sm_instance;

		// Token: 0x040009CC RID: 2508
		private bool m_lastKnownADComponentStatesAreSet;

		// Token: 0x040009CD RID: 2509
		private MultiValuedProperty<string> m_lastKnownADComponentStates;

		// Token: 0x040009CE RID: 2510
		private object syncLock = new object();

		// Token: 0x02000194 RID: 404
		private struct ComponentStateData
		{
			// Token: 0x170002CD RID: 717
			// (get) Token: 0x06001126 RID: 4390 RVA: 0x00052FB9 File Offset: 0x000511B9
			// (set) Token: 0x06001127 RID: 4391 RVA: 0x00052FC1 File Offset: 0x000511C1
			public ServiceState OnPremState { get; set; }

			// Token: 0x170002CE RID: 718
			// (get) Token: 0x06001128 RID: 4392 RVA: 0x00052FCA File Offset: 0x000511CA
			// (set) Token: 0x06001129 RID: 4393 RVA: 0x00052FD2 File Offset: 0x000511D2
			public ServiceState DatacenterState { get; set; }
		}
	}
}
