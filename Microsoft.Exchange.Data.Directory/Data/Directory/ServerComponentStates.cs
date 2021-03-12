using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000196 RID: 406
	internal class ServerComponentStates
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x00052FDB File Offset: 0x000511DB
		public static string GetComponentId(ServerComponentEnum serverComponent)
		{
			return serverComponent.ToString();
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00052FE8 File Offset: 0x000511E8
		public static void SyncComponentStates(MultiValuedProperty<string> adComponentStates)
		{
			if (adComponentStates != null)
			{
				using (RegistryKey localMachine = Registry.LocalMachine)
				{
					foreach (string value in adComponentStates)
					{
						ServerComponentStates.ItemEntry itemEntry;
						if (ServerComponentStates.TryParseRemoteStateString(value, out itemEntry))
						{
							string subkey = string.Format("{0}\\{1}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ServerComponentStates", itemEntry.Component);
							using (RegistryKey registryKey = localMachine.CreateSubKey(subkey))
							{
								bool flag = true;
								object value2 = registryKey.GetValue(itemEntry.Requester);
								ServiceState serviceState;
								DateTime t;
								if (value2 != null && registryKey.GetValueKind(itemEntry.Requester) == RegistryValueKind.String && ServerComponentStates.TryParseLocalStateString(value2 as string, out serviceState, out t) && itemEntry.Timestamp <= t)
								{
									flag = false;
								}
								if (flag)
								{
									ServerComponentStates.LogTransition(itemEntry.Component, itemEntry.Requester, itemEntry.State);
									string value3 = ServerComponentStates.FormLocalString(itemEntry.State, itemEntry.Timestamp);
									registryKey.SetValue(itemEntry.Requester, value3, RegistryValueKind.String);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00053128 File Offset: 0x00051328
		public static ServiceState ReadEffectiveComponentState(string targetServerFqdn, MultiValuedProperty<string> adComponentStates, ServerComponentStateSources sourcesToUse, string component, ServiceState defaultState, out bool serverWideIsInactive, out List<ServerComponentStates.ItemEntry> returnLocalStates, out List<ServerComponentStates.ItemEntry> returnRemoteStates)
		{
			if (!ServerComponentStates.IsValidName(component))
			{
				throw new ArgumentException(DirectoryStrings.ComponentNameInvalid);
			}
			if (sourcesToUse == (ServerComponentStateSources)0)
			{
				throw new ArgumentException("sourcesToUse argument must have at least one source");
			}
			serverWideIsInactive = false;
			List<ServerComponentStates.ItemEntry> reconciledStates = ServerComponentStates.GetReconciledStates(targetServerFqdn, adComponentStates, sourcesToUse, component, out returnLocalStates, out returnRemoteStates);
			if (!string.Equals(component, ServerComponentStates.MonitoringComponentId, StringComparison.OrdinalIgnoreCase) && !string.Equals(component, ServerComponentStates.RecoveryActionsEnabledComponentId, StringComparison.OrdinalIgnoreCase))
			{
				ServiceState effectiveState;
				if (string.Equals(component, ServerComponentStates.ServerWideOfflineComponentId, StringComparison.OrdinalIgnoreCase))
				{
					effectiveState = ServerComponentStates.GetEffectiveState(reconciledStates, ServiceState.Active);
					if (effectiveState != ServiceState.Active)
					{
						serverWideIsInactive = true;
					}
					return effectiveState;
				}
				List<ServerComponentStates.ItemEntry> list;
				List<ServerComponentStates.ItemEntry> list2;
				List<ServerComponentStates.ItemEntry> reconciledStates2 = ServerComponentStates.GetReconciledStates(targetServerFqdn, adComponentStates, sourcesToUse, ServerComponentStates.ServerWideOfflineComponentId, out list, out list2);
				effectiveState = ServerComponentStates.GetEffectiveState(reconciledStates2, ServiceState.Active);
				if (effectiveState != ServiceState.Active)
				{
					serverWideIsInactive = true;
					return ServiceState.Inactive;
				}
			}
			return ServerComponentStates.GetEffectiveState(reconciledStates, defaultState);
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x000531D8 File Offset: 0x000513D8
		public static ServiceState ReadEffectiveComponentState(string targetServerFqdn, MultiValuedProperty<string> adComponentStates, ServerComponentStateSources sourcesToUse, string component, ServiceState defaultState)
		{
			bool flag;
			List<ServerComponentStates.ItemEntry> list;
			List<ServerComponentStates.ItemEntry> list2;
			return ServerComponentStates.ReadEffectiveComponentState(targetServerFqdn, adComponentStates, sourcesToUse, component, defaultState, out flag, out list, out list2);
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000531F6 File Offset: 0x000513F6
		public static ServiceState ReadEffectiveComponentState(string targetServerFqdn, MultiValuedProperty<string> adComponentStates, string component, ServiceState defaultState)
		{
			return ServerComponentStates.ReadEffectiveComponentState(targetServerFqdn, adComponentStates, ServerComponentStateSources.All, component, defaultState);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00053202 File Offset: 0x00051402
		public static ServiceState ReadEffectiveComponentState(ServerComponentEnum serverComponent, ServiceState defaultState)
		{
			return ServerComponentStates.ReadEffectiveComponentState(null, null, ServerComponentStateSources.Registry, ServerComponentStates.GetComponentId(serverComponent), defaultState);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00053214 File Offset: 0x00051414
		internal static ServiceState ReadDirectComponentState(string targetServerFqdn, MultiValuedProperty<string> adComponentStates, ServerComponentStateSources sourcesToUse, string component, ServiceState defaultState, out List<ServerComponentStates.ItemEntry> returnLocalStates, out List<ServerComponentStates.ItemEntry> returnRemoteStates)
		{
			if (!ServerComponentStates.IsValidName(component))
			{
				throw new ArgumentException(DirectoryStrings.ComponentNameInvalid);
			}
			List<ServerComponentStates.ItemEntry> reconciledStates = ServerComponentStates.GetReconciledStates(targetServerFqdn, adComponentStates, sourcesToUse, component, out returnLocalStates, out returnRemoteStates);
			return ServerComponentStates.GetEffectiveState(reconciledStates, defaultState);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0005324F File Offset: 0x0005144F
		internal static bool IsServerOnline(Server server)
		{
			return ServerComponentStates.IsRemoteComponentOnlineAccordingToADInternal(server.ComponentStates, ServerComponentEnum.ServerWideOffline);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0005325D File Offset: 0x0005145D
		internal static bool IsServerOnline(MultiValuedProperty<string> componentStates)
		{
			return ServerComponentStates.IsRemoteComponentOnlineAccordingToADInternal(componentStates, ServerComponentEnum.ServerWideOffline);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00053266 File Offset: 0x00051466
		internal static bool IsServerOnline(ADComputer server)
		{
			return ServerComponentStates.IsRemoteComponentOnlineAccordingToADInternal(server.ComponentStates, ServerComponentEnum.ServerWideOffline);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00053274 File Offset: 0x00051474
		internal static bool IsRemoteComponentOnlineAccordingToAD(Server server, ServerComponentEnum serverComponent)
		{
			return ServerComponentStates.IsRemoteComponentOnlineAccordingToADInternal(server.ComponentStates, serverComponent);
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00053282 File Offset: 0x00051482
		internal static bool IsRemoteComponentOnlineAccordingToAD(ADComputer server, ServerComponentEnum serverComponent)
		{
			return ServerComponentStates.IsRemoteComponentOnlineAccordingToADInternal(server.ComponentStates, serverComponent);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00053290 File Offset: 0x00051490
		internal static bool IsRemoteComponentOnline(Server server, ServerComponentEnum serverComponent)
		{
			return ServerComponentStates.IsRemoteComponentOnlineInternal(server.Fqdn, server.ComponentStates, serverComponent);
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000532A4 File Offset: 0x000514A4
		internal static bool IsRemoteComponentOnline(ADComputer server, ServerComponentEnum serverComponent)
		{
			return ServerComponentStates.IsRemoteComponentOnlineInternal(server.DnsHostName, server.ComponentStates, serverComponent);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000532B8 File Offset: 0x000514B8
		public static void UpdateLocalState(string targetServerFqdn, string requester, string component, ServiceState state)
		{
			if (!ServerComponentStates.IsValidName(component))
			{
				throw new ArgumentException(DirectoryStrings.ComponentNameInvalid);
			}
			if (!ServerComponentStates.IsValidName(requester))
			{
				throw new ArgumentException(DirectoryStrings.RequesterNameInvalid);
			}
			string subkey = string.Format("{0}\\{1}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ServerComponentStates", component);
			using (RegistryKey localMachineKey = ServerComponentStates.GetLocalMachineKey(targetServerFqdn))
			{
				using (RegistryKey registryKey = localMachineKey.CreateSubKey(subkey))
				{
					ServerComponentStates.LogTransition(component, requester, state);
					registryKey.SetValue(requester, ServerComponentStates.FormLocalString(state, DateTime.UtcNow));
				}
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00053364 File Offset: 0x00051564
		public static bool IsRemoteComponentOnlineAccordingToADInternal(MultiValuedProperty<string> componentStates, ServerComponentEnum serverComponent)
		{
			List<ServerComponentStates.ItemEntry> remoteStates = ServerComponentStates.GetRemoteStates(componentStates, ServerComponentStates.GetComponentId(serverComponent));
			ServiceState effectiveState = ServerComponentStates.GetEffectiveState(remoteStates, ServiceState.Active);
			return effectiveState == ServiceState.Active;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0005338C File Offset: 0x0005158C
		private static bool IsRemoteComponentOnlineInternal(string fqdn, MultiValuedProperty<string> componentStates, ServerComponentEnum serverComponent)
		{
			ServiceState serviceState = ServerComponentStates.ReadEffectiveComponentState(fqdn, componentStates, ServerComponentStates.GetComponentId(serverComponent), ServiceState.Active);
			return serviceState == ServiceState.Active;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000533AC File Offset: 0x000515AC
		private static void LogTransition(string component, string requester, ServiceState state)
		{
			if (state != ServiceState.Active)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ServerComponentStateSetOffline, Environment.MachineName, new object[]
				{
					component,
					requester,
					state.ToString()
				});
				return;
			}
			bool flag = false;
			List<ServerComponentStates.ItemEntry> list = null;
			List<ServerComponentStates.ItemEntry> localStates = ServerComponentStates.GetLocalStates(null, ServerComponentStates.ServerWideOfflineComponentId);
			foreach (ServerComponentStates.ItemEntry itemEntry in localStates)
			{
				if (itemEntry.State != ServiceState.Active && !string.Equals(itemEntry.Requester, requester, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					break;
				}
			}
			if (!flag && !string.Equals(component, ServerComponentStates.ServerWideOfflineComponentId, StringComparison.OrdinalIgnoreCase))
			{
				list = ServerComponentStates.GetLocalStates(null, component);
				foreach (ServerComponentStates.ItemEntry itemEntry2 in list)
				{
					if (itemEntry2.State != ServiceState.Active && !string.Equals(itemEntry2.Requester, requester, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				DateTime dateTime = DateTime.UtcNow;
				foreach (ServerComponentStates.ItemEntry itemEntry3 in localStates)
				{
					if (itemEntry3.State != ServiceState.Active && itemEntry3.Timestamp < dateTime)
					{
						dateTime = itemEntry3.Timestamp;
					}
				}
				if (list != null)
				{
					foreach (ServerComponentStates.ItemEntry itemEntry4 in list)
					{
						if (itemEntry4.State != ServiceState.Active && itemEntry4.Timestamp < dateTime)
						{
							dateTime = itemEntry4.Timestamp;
						}
					}
				}
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ServerComponentStateSetOnline, Environment.MachineName, new object[]
				{
					component,
					requester,
					dateTime
				});
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000535BC File Offset: 0x000517BC
		public static MultiValuedProperty<string> UpdateRemoteState(MultiValuedProperty<string> componentStates, string requester, string component, ServiceState state)
		{
			if (!ServerComponentStates.IsValidName(component))
			{
				throw new ArgumentException(DirectoryStrings.ComponentNameInvalid);
			}
			if (!ServerComponentStates.IsValidName(requester))
			{
				throw new ArgumentException(DirectoryStrings.RequesterNameInvalid);
			}
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			if (componentStates != null)
			{
				foreach (string text in componentStates)
				{
					ServerComponentStates.ItemEntry itemEntry;
					if (ServerComponentStates.TryParseRemoteStateString(text, out itemEntry) && (!string.Equals(itemEntry.Component, component, StringComparison.OrdinalIgnoreCase) || !string.Equals(itemEntry.Requester, requester, StringComparison.OrdinalIgnoreCase)))
					{
						multiValuedProperty.Add(text);
					}
				}
			}
			multiValuedProperty.Add(string.Format("1:{0}:{1}:{2}:{3}", new object[]
			{
				component,
				requester,
				(int)state,
				DateTime.UtcNow.Ticks
			}));
			return multiValuedProperty;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x000536B4 File Offset: 0x000518B4
		private static string FormLocalString(ServiceState state, DateTime timeStamp)
		{
			return string.Format("1:{0}:{1}", (int)state, timeStamp.Ticks);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000536D4 File Offset: 0x000518D4
		private static List<ServerComponentStates.ItemEntry> GetRemoteStates(MultiValuedProperty<string> componentStates, string component)
		{
			List<ServerComponentStates.ItemEntry> list = new List<ServerComponentStates.ItemEntry>();
			if (componentStates == null)
			{
				return list;
			}
			foreach (string value in componentStates)
			{
				ServerComponentStates.ItemEntry itemEntry;
				if (ServerComponentStates.TryParseRemoteStateString(value, out itemEntry) && string.Equals(itemEntry.Component, component, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(itemEntry);
				}
			}
			return list;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00053748 File Offset: 0x00051948
		private static List<ServerComponentStates.ItemEntry> GetLocalStates(string targetServerFqdn, string component)
		{
			string name = string.Format("{0}\\{1}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ServerComponentStates", component);
			List<ServerComponentStates.ItemEntry> result;
			using (RegistryKey localMachineKey = ServerComponentStates.GetLocalMachineKey(targetServerFqdn))
			{
				using (RegistryKey registryKey = localMachineKey.OpenSubKey(name))
				{
					List<ServerComponentStates.ItemEntry> list = new List<ServerComponentStates.ItemEntry>();
					if (registryKey == null)
					{
						result = list;
					}
					else
					{
						foreach (string text in registryKey.GetValueNames())
						{
							object value = registryKey.GetValue(text);
							ServiceState state;
							DateTime timestamp;
							if (ServerComponentStates.IsValidName(text) && registryKey.GetValueKind(text) == RegistryValueKind.String && ServerComponentStates.TryParseLocalStateString(value as string, out state, out timestamp))
							{
								list.Add(new ServerComponentStates.ItemEntry(component, text, state, timestamp));
							}
						}
						result = list;
					}
				}
			}
			return result;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00053820 File Offset: 0x00051A20
		private static List<ServerComponentStates.ItemEntry> ReconcileStates(List<ServerComponentStates.ItemEntry> localStates, List<ServerComponentStates.ItemEntry> remoteStates)
		{
			List<ServerComponentStates.ItemEntry> list = new List<ServerComponentStates.ItemEntry>(localStates.Count);
			foreach (ServerComponentStates.ItemEntry itemEntry in localStates)
			{
				list.Add(new ServerComponentStates.ItemEntry(itemEntry.Component, itemEntry.Requester, itemEntry.State, itemEntry.Timestamp));
			}
			foreach (ServerComponentStates.ItemEntry itemEntry2 in remoteStates)
			{
				bool flag = false;
				foreach (ServerComponentStates.ItemEntry itemEntry3 in list)
				{
					if (string.Equals(itemEntry3.Component, itemEntry2.Component, StringComparison.OrdinalIgnoreCase) && string.Equals(itemEntry3.Requester, itemEntry2.Requester, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						if (itemEntry3.Timestamp < itemEntry2.Timestamp)
						{
							itemEntry3.State = itemEntry2.State;
							itemEntry3.Timestamp = itemEntry2.Timestamp;
							break;
						}
						break;
					}
				}
				if (!flag)
				{
					list.Add(itemEntry2);
				}
			}
			return list;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0005399C File Offset: 0x00051B9C
		private static List<ServerComponentStates.ItemEntry> GetReconciledStates(string targetServerFqdn, MultiValuedProperty<string> adComponentStates, ServerComponentStateSources sourcesToUse, string component, out List<ServerComponentStates.ItemEntry> localStates, out List<ServerComponentStates.ItemEntry> remoteStates)
		{
			localStates = null;
			remoteStates = null;
			List<ServerComponentStates.ItemEntry> localStatesTemp = null;
			List<ServerComponentStates.ItemEntry> result = null;
			if (sourcesToUse.HasFlag(ServerComponentStateSources.Registry))
			{
				Exception ex = ServerComponentStateManager.RunLocalRegistryOperationNoThrow(delegate
				{
					localStatesTemp = ServerComponentStates.GetLocalStates(targetServerFqdn, component);
				});
				if (ex != null && !sourcesToUse.HasFlag(ServerComponentStateSources.AD))
				{
					throw new ServerComponentApiException(DirectoryStrings.ServerComponentLocalRegistryError(ex.ToString()), ex);
				}
				localStates = localStatesTemp;
				result = localStatesTemp;
			}
			if (sourcesToUse.HasFlag(ServerComponentStateSources.AD))
			{
				remoteStates = ServerComponentStates.GetRemoteStates(adComponentStates, component);
				result = remoteStates;
			}
			if (localStates != null && remoteStates != null)
			{
				result = ServerComponentStates.ReconcileStates(localStates, remoteStates);
			}
			return result;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00053A88 File Offset: 0x00051C88
		private static ServiceState GetEffectiveState(List<ServerComponentStates.ItemEntry> stateData, ServiceState defaultState)
		{
			if (stateData.Count == 0)
			{
				return defaultState;
			}
			if (stateData.Any((ServerComponentStates.ItemEntry item) => item.State == ServiceState.Inactive))
			{
				return ServiceState.Inactive;
			}
			if (stateData.Any((ServerComponentStates.ItemEntry item) => item.State == ServiceState.Draining))
			{
				return ServiceState.Draining;
			}
			return ServiceState.Active;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00053AF0 File Offset: 0x00051CF0
		private static bool TryParseLocalStateString(string value, out ServiceState state, out DateTime timeStamp)
		{
			state = ServiceState.Inactive;
			timeStamp = DateTime.MinValue;
			string[] array = value.Split(new char[]
			{
				':'
			});
			return array.Length >= 3 && ServerComponentStates.IsSupportedEncodingVersion(array[0]) && Enum.TryParse<ServiceState>(array[1], out state) && ServerComponentStates.TryParseTimeStamp(array[2], out timeStamp);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00053B48 File Offset: 0x00051D48
		private static bool TryParseRemoteStateString(string value, out ServerComponentStates.ItemEntry entry)
		{
			entry = null;
			string[] array = value.Split(new char[]
			{
				':'
			});
			ServiceState state;
			DateTime timestamp;
			if (array.Length >= 5 && ServerComponentStates.IsSupportedEncodingVersion(array[0]) && !string.IsNullOrEmpty(array[1]) && !string.IsNullOrEmpty(array[2]) && Enum.TryParse<ServiceState>(array[3], out state) && ServerComponentStates.TryParseTimeStamp(array[4], out timestamp))
			{
				entry = new ServerComponentStates.ItemEntry(array[1], array[2], state, timestamp);
				return true;
			}
			return false;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00053BBC File Offset: 0x00051DBC
		private static bool TryParseTimeStamp(string value, out DateTime timeStamp)
		{
			long ticks = 0L;
			if (!long.TryParse(value, out ticks))
			{
				timeStamp = DateTime.MinValue;
				return false;
			}
			timeStamp = new DateTime(ticks, DateTimeKind.Utc);
			return true;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00053BF1 File Offset: 0x00051DF1
		private static RegistryKey GetLocalMachineKey(string targetServerFqdn)
		{
			if (string.IsNullOrEmpty(targetServerFqdn) || string.Equals(targetServerFqdn, ServerComponentStates.serverFqdn, StringComparison.OrdinalIgnoreCase))
			{
				return Registry.LocalMachine;
			}
			return RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, targetServerFqdn);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00053C1A File Offset: 0x00051E1A
		private static bool IsValidName(string name)
		{
			return !string.IsNullOrEmpty(name) && name.Length <= 32 && !name.Contains(':');
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00053C3C File Offset: 0x00051E3C
		private static bool IsSupportedEncodingVersion(string versionString)
		{
			int num;
			return int.TryParse(versionString, out num) && num >= 1;
		}

		// Token: 0x040009D5 RID: 2517
		internal const string ComponentStatesKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ServerComponentStates";

		// Token: 0x040009D6 RID: 2518
		private const int EncodingVersion = 1;

		// Token: 0x040009D7 RID: 2519
		private const char ComponentDataSeparater = ':';

		// Token: 0x040009D8 RID: 2520
		private const int MaxNameLength = 32;

		// Token: 0x040009D9 RID: 2521
		private static readonly string serverFqdn = ComputerInformation.DnsFullyQualifiedDomainName;

		// Token: 0x040009DA RID: 2522
		private static readonly string ServerWideOfflineComponentId = ServerComponentEnum.ServerWideOffline.ToString();

		// Token: 0x040009DB RID: 2523
		private static readonly string MonitoringComponentId = ServerComponentEnum.Monitoring.ToString();

		// Token: 0x040009DC RID: 2524
		private static readonly string RecoveryActionsEnabledComponentId = ServerComponentEnum.RecoveryActionsEnabled.ToString();

		// Token: 0x02000197 RID: 407
		internal class ItemEntry
		{
			// Token: 0x0600114D RID: 4429 RVA: 0x00053CA0 File Offset: 0x00051EA0
			public ItemEntry(string component, string requester, ServiceState state, DateTime timestamp)
			{
				this.Requester = requester;
				this.Component = component;
				this.State = state;
				this.Timestamp = timestamp;
			}

			// Token: 0x170002CF RID: 719
			// (get) Token: 0x0600114E RID: 4430 RVA: 0x00053CC5 File Offset: 0x00051EC5
			// (set) Token: 0x0600114F RID: 4431 RVA: 0x00053CCD File Offset: 0x00051ECD
			public string Requester { get; private set; }

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06001150 RID: 4432 RVA: 0x00053CD6 File Offset: 0x00051ED6
			// (set) Token: 0x06001151 RID: 4433 RVA: 0x00053CDE File Offset: 0x00051EDE
			public string Component { get; private set; }

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06001152 RID: 4434 RVA: 0x00053CE7 File Offset: 0x00051EE7
			// (set) Token: 0x06001153 RID: 4435 RVA: 0x00053CEF File Offset: 0x00051EEF
			public ServiceState State { get; set; }

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06001154 RID: 4436 RVA: 0x00053CF8 File Offset: 0x00051EF8
			// (set) Token: 0x06001155 RID: 4437 RVA: 0x00053D00 File Offset: 0x00051F00
			public DateTime Timestamp { get; set; }
		}
	}
}
