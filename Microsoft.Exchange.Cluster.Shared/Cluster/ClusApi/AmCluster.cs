using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200001B RID: 27
	internal class AmCluster : DisposeTrackableBase, IAmCluster, IDisposable
	{
		// Token: 0x060000BC RID: 188 RVA: 0x000039B2 File Offset: 0x00001BB2
		private AmCluster(AmClusterHandle clusterHandle, bool ownsHandleLifetime, string name, string nodeNameUsedForCluster)
		{
			this.m_handle = clusterHandle;
			this.m_ownsHandleLifetime = ownsHandleLifetime;
			this.m_name = name;
			this.NodeNameUsedForCluster = nodeNameUsedForCluster;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000039D7 File Offset: 0x00001BD7
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ClusterEventsTracer;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000039DE File Offset: 0x00001BDE
		public AmClusterHandle Handle
		{
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000039E8 File Offset: 0x00001BE8
		public bool IsRefreshRequired
		{
			get
			{
				if (this.m_localNode == null)
				{
					return true;
				}
				bool result = false;
				try
				{
					this.m_localNode.GetState(true);
				}
				catch (ClusterApiException ex)
				{
					result = true;
					AmTrace.Warning("IsRefreshRequired: GetState() for node {0} returned error: {1}", new object[]
					{
						this.m_localNode.Name,
						ex.Message
					});
				}
				return result;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003A50 File Offset: 0x00001C50
		public bool IsLocalNodeUp
		{
			get
			{
				bool result = false;
				if (this.m_localNode != null)
				{
					try
					{
						AmNodeState state = this.m_localNode.GetState(true);
						if (AmClusterNode.IsNodeUp(state))
						{
							result = true;
						}
						else
						{
							AmTrace.Warning("IsLocalNodeUp: Local node {0} has not reached the UP state (current state: {1})", new object[]
							{
								this.m_localNode.Name,
								state
							});
						}
					}
					catch (ClusterApiException ex)
					{
						AmTrace.Error("IsLocalNodeUp: GetState() for node {0} returned error: {1}", new object[]
						{
							this.m_localNode.Name,
							ex.Message
						});
					}
				}
				return result;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003C8C File Offset: 0x00001E8C
		internal IEnumerable<IAmClusterNode> Nodes
		{
			get
			{
				foreach (AmServerName nodeName in this.EnumerateNodeNames())
				{
					IAmClusterNode node = this.OpenNode(nodeName);
					yield return node;
				}
				yield break;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003CAC File Offset: 0x00001EAC
		public string Name
		{
			get
			{
				if (this.m_name == null && this.m_handle != null && !this.m_handle.IsInvalid)
				{
					int capacity = 16;
					StringBuilder stringBuilder = new StringBuilder(capacity);
					if (ClusapiMethods.GetClusterInformation(this.m_handle, stringBuilder, ref capacity, IntPtr.Zero) == 0U)
					{
						this.m_name = stringBuilder.ToString();
					}
				}
				return this.m_name;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003D0C File Offset: 0x00001F0C
		public string CnoName
		{
			get
			{
				if (this.m_cnoName == null)
				{
					using (IAmClusterGroup amClusterGroup = this.FindCoreClusterGroup())
					{
						using (IAmClusterResource amClusterResource = amClusterGroup.FindResourceByTypeName("Network Name"))
						{
							if (amClusterResource == null)
							{
								this.m_cnoName = string.Empty;
							}
							else
							{
								this.m_cnoName = this.Name + "$";
							}
						}
					}
				}
				return this.m_cnoName;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003D94 File Offset: 0x00001F94
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003D9C File Offset: 0x00001F9C
		private string NodeNameUsedForCluster { get; set; }

		// Token: 0x060000C6 RID: 198 RVA: 0x00003DA5 File Offset: 0x00001FA5
		public static bool IsInstalled()
		{
			return AmCluster.IsInstalled(AmServerName.LocalComputerName);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public static bool IsInstalled(AmServerName machineAmName)
		{
			string text = machineAmName.Fqdn;
			if (machineAmName.IsLocalComputerName)
			{
				text = null;
			}
			AmNodeClusterState amNodeClusterState = AmNodeClusterState.NotInstalled;
			int nodeClusterState = ClusapiMethods.GetNodeClusterState(text, ref amNodeClusterState);
			if (nodeClusterState == 0)
			{
				return amNodeClusterState != AmNodeClusterState.NotInstalled;
			}
			throw AmExceptionHelper.ConstructClusterApiException(nodeClusterState, "IsInstalled({0})", new object[]
			{
				text ?? Environment.MachineName
			});
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003E08 File Offset: 0x00002008
		public static bool IsRunning()
		{
			return AmCluster.IsRunning(AmServerName.LocalComputerName);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003E14 File Offset: 0x00002014
		public static bool IsRunning(AmServerName machineAmName)
		{
			string text = machineAmName.Fqdn;
			if (machineAmName.IsLocalComputerName)
			{
				text = null;
			}
			AmNodeClusterState amNodeClusterState = AmNodeClusterState.NotInstalled;
			int nodeClusterState = ClusapiMethods.GetNodeClusterState(text, ref amNodeClusterState);
			if (nodeClusterState == 0)
			{
				return amNodeClusterState == AmNodeClusterState.Running;
			}
			throw AmExceptionHelper.ConstructClusterApiException(nodeClusterState, "IsRunning({0})", new object[]
			{
				text ?? Environment.MachineName
			});
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003E6C File Offset: 0x0000206C
		public static bool IsEvicted(AmServerName machineName)
		{
			int num;
			return AmCluster.IsEvictedEx(machineName, out num);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003E84 File Offset: 0x00002084
		public static bool IsEvictedEx(AmServerName machineName, out int lastError)
		{
			AmNodeClusterState amNodeClusterState = AmNodeClusterState.NotInstalled;
			int nodeClusterState = ClusapiMethods.GetNodeClusterState(machineName.Fqdn, ref amNodeClusterState);
			lastError = nodeClusterState;
			if (nodeClusterState == 53)
			{
				AmTrace.Debug("GetNodeClusterState() returned error (rc={0})", new object[]
				{
					nodeClusterState
				});
				return false;
			}
			if (nodeClusterState != 0)
			{
				throw AmExceptionHelper.ConstructClusterApiException(nodeClusterState, "IsEvicted('{0}')", new object[]
				{
					machineName
				});
			}
			if (amNodeClusterState == AmNodeClusterState.NotConfigured)
			{
				return true;
			}
			if (amNodeClusterState == AmNodeClusterState.NotInstalled)
			{
				throw new ClusterNotInstalledException(machineName.NetbiosName);
			}
			return false;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003EF8 File Offset: 0x000020F8
		public static AmCluster CreateExchangeCluster(string clusterName, AmServerName firstNodeName, string[] ipAddress, uint[] ipPrefixLength, IClusterSetupProgress setupProgress, IntPtr context, out Exception failureException, bool throwExceptionOnFailure)
		{
			if (setupProgress == null)
			{
				throw new ArgumentException("setupProgress must be provided. Consider HaTaskStringBuilderOutputHelper");
			}
			ClusapiMethods.PCLUSTER_SETUP_PROGRESS_CALLBACK pfnProgressCallback = new ClusapiMethods.PCLUSTER_SETUP_PROGRESS_CALLBACK(setupProgress.ClusterSetupProgressCallback);
			failureException = null;
			AmClusterHandle amClusterHandle = null;
			if (ipAddress != null && ipAddress.Length != ipPrefixLength.Length)
			{
				throw new ArgumentException(string.Format("ipAddress.Length ({0}) should equal ipPrefix.Length ({1}).", ipAddress.Length, ipPrefixLength.Length));
			}
			ExTraceGlobals.ClusterTracer.TraceDebug(0L, "Going to CreateCluster(), name={0}, firstNode={1}, ipAddress={2}, ipPrefixLength={3}.", new object[]
			{
				clusterName,
				firstNodeName,
				ipAddress,
				ipPrefixLength
			});
			string[] array = new string[]
			{
				firstNodeName.Fqdn
			};
			ClusapiMethods.CREATE_CLUSTER_CONFIG create_CLUSTER_CONFIG = new ClusapiMethods.CREATE_CLUSTER_CONFIG();
			create_CLUSTER_CONFIG.dwVersion = 1536U;
			create_CLUSTER_CONFIG.lpszClusterName = clusterName;
			create_CLUSTER_CONFIG.cNodes = (uint)array.Length;
			create_CLUSTER_CONFIG.ppszNodeNames = ClusapiMethods.StringArrayToIntPtr(array);
			create_CLUSTER_CONFIG.fEmptyCluster = 0U;
			if (ipAddress.Length != 1 || !IPAddress.None.ToString().Equals(ipAddress[0], StringComparison.OrdinalIgnoreCase))
			{
				ClusapiMethods.CLUSTER_IP_ENTRY[] array2 = new ClusapiMethods.CLUSTER_IP_ENTRY[ipAddress.Length];
				for (int i = 0; i < ipAddress.Length; i++)
				{
					array2[i] = new ClusapiMethods.CLUSTER_IP_ENTRY(ipAddress[i], ipPrefixLength[i]);
					if (ipPrefixLength[i] == 0U)
					{
						throw new ArgumentException(string.Format("ipPrefixLength[{0}] was zero! (value was {1}).", i.ToString(), ipPrefixLength[i].ToString()));
					}
				}
				create_CLUSTER_CONFIG.cIpEntries = (uint)array2.Length;
				create_CLUSTER_CONFIG.pIpEntries = ClusapiMethods.ClusterIpEntryArrayToIntPtr(array2);
			}
			else
			{
				create_CLUSTER_CONFIG.cIpEntries = 0U;
				create_CLUSTER_CONFIG.pIpEntries = IntPtr.Zero;
				create_CLUSTER_CONFIG.dwVersion = 1794U;
			}
			SafeHGlobalHandle safeHGlobalHandle = new SafeHGlobalHandle(Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ClusapiMethods.CREATE_CLUSTER_CONFIG))));
			using (safeHGlobalHandle)
			{
				Marshal.StructureToPtr(create_CLUSTER_CONFIG, safeHGlobalHandle.DangerousGetHandle(), false);
				try
				{
					setupProgress.MaxPercentageDuringCallback = 0;
					amClusterHandle = ClusapiMethods.CreateCluster(safeHGlobalHandle, pfnProgressCallback, context);
					if (amClusterHandle == null || amClusterHandle.IsInvalid)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						ExTraceGlobals.ClusterTracer.TraceDebug<int>(0L, "CreateCluster() failed. GLE is {0}.", lastWin32Error);
						if (setupProgress.LastException == null)
						{
							throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "CreateCluster()", new object[0]);
						}
						failureException = setupProgress.LastException;
						if (throwExceptionOnFailure)
						{
							throw failureException;
						}
						return null;
					}
					else
					{
						ExTraceGlobals.ClusterTracer.TraceDebug<int>(0L, "Successfully called CreateCluster(), maximum percentage progress reached = {0}.", setupProgress.MaxPercentageDuringCallback);
					}
				}
				finally
				{
					ClusapiMethods.FreeIntPtrOfMarshalledObjectsArray(create_CLUSTER_CONFIG.ppszNodeNames, (int)create_CLUSTER_CONFIG.cNodes);
					ClusapiMethods.FreeIntPtrOfMarshalledObjectsArray(create_CLUSTER_CONFIG.pIpEntries, (int)create_CLUSTER_CONFIG.cIpEntries);
				}
			}
			return AmCluster.TransferClusterFromAmClusterHandle(amClusterHandle);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000418C File Offset: 0x0000238C
		public static IEnumerable<T> EvaluateAllElements<T>(IEnumerable<T> notEvaluatedList)
		{
			List<T> list = new List<T>(16);
			bool flag = false;
			try
			{
				foreach (T item in notEvaluatedList)
				{
					list.Add(item);
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (T t in list)
					{
						using (t as IDisposable)
						{
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004258 File Offset: 0x00002458
		public bool IsEvictedBasedOnMemberShip(AmServerName machineName)
		{
			foreach (AmServerName amServerName in this.EnumerateNodeNames())
			{
				if (amServerName.Equals(machineName))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000042B0 File Offset: 0x000024B0
		public void DestroyExchangeCluster(IClusterSetupProgress setupProgress, IntPtr context, out Exception errorException, bool throwExceptionOnFailure)
		{
			if (setupProgress == null)
			{
				throw new ArgumentException("setupProgress must be provided. Consider HaTaskStringBuilderOutputHelper");
			}
			ClusapiMethods.PCLUSTER_SETUP_PROGRESS_CALLBACK pfnProgressCallback = new ClusapiMethods.PCLUSTER_SETUP_PROGRESS_CALLBACK(setupProgress.ClusterSetupProgressCallback);
			errorException = null;
			ExTraceGlobals.ClusterTracer.TraceDebug<string>((long)this.GetHashCode(), "Going to DestroyCluster( {0} ) on this machine.", this.Name);
			setupProgress.MaxPercentageDuringCallback = 0;
			uint num = ClusapiMethods.DestroyCluster(this.m_handle, pfnProgressCallback, context, 0U);
			if (num != 0U && setupProgress.MaxPercentageDuringCallback != 100)
			{
				ExTraceGlobals.ClusterTracer.TraceDebug<string, uint, int>((long)this.GetHashCode(), "DestroyCluster( {0} ) failed. It returned {1}, and MaxPercentage seen was {2}.", this.Name, num, setupProgress.MaxPercentageDuringCallback);
				if (setupProgress.LastException == null)
				{
					throw AmExceptionHelper.ConstructClusterApiException((int)num, "DestroyCluster( {0} ) (MaxPercentage={1})", new object[]
					{
						this.Name,
						setupProgress.MaxPercentageDuringCallback
					});
				}
				errorException = setupProgress.LastException;
				if (throwExceptionOnFailure)
				{
					throw errorException;
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004384 File Offset: 0x00002584
		public void AddNodeToCluster(AmServerName nodeName, IClusterSetupProgress setupProgress, IntPtr context, out Exception errorException, bool throwExceptionOnFailure)
		{
			if (nodeName == null)
			{
				throw new ArgumentNullException("nodeName");
			}
			errorException = null;
			if (setupProgress == null)
			{
				throw new ArgumentException("setupProgress must be provided. Consider HaTaskStringBuilderOutputHelper");
			}
			ClusapiMethods.PCLUSTER_SETUP_PROGRESS_CALLBACK pcluster_SETUP_PROGRESS_CALLBACK = new ClusapiMethods.PCLUSTER_SETUP_PROGRESS_CALLBACK(setupProgress.ClusterSetupProgressCallback);
			setupProgress.MaxPercentageDuringCallback = 0;
			using (AmClusterNodeHandle amClusterNodeHandle = ClusapiMethods.AddClusterNode(this.Handle, nodeName.Fqdn, pcluster_SETUP_PROGRESS_CALLBACK, context))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				ExTraceGlobals.ClusterTracer.TraceDebug((long)this.GetHashCode(), "AddClusterNode( {0}, {1}, {2}, {3} ) returned {4}, gle={5}.", new object[]
				{
					this.m_handle,
					nodeName,
					pcluster_SETUP_PROGRESS_CALLBACK,
					context,
					amClusterNodeHandle,
					lastWin32Error
				});
				if (amClusterNodeHandle == null || amClusterNodeHandle.IsInvalid)
				{
					if (setupProgress.LastException != null)
					{
						errorException = setupProgress.LastException;
					}
					else if (lastWin32Error == 5065)
					{
						errorException = new ClusterNodeJoinedException(nodeName.NetbiosName);
					}
					else
					{
						if (lastWin32Error != 2)
						{
							throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "AddClusterNode() (MaxPercentage={0})", new object[]
							{
								setupProgress.MaxPercentageDuringCallback
							});
						}
						errorException = new ClusterFileNotFoundException(nodeName.NetbiosName);
					}
				}
				if (throwExceptionOnFailure && errorException != null)
				{
					throw errorException;
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000456C File Offset: 0x0000276C
		public void EvictNodeFromCluster(AmServerName nodeName)
		{
			AmCluster.<>c__DisplayClass8 CS$<>8__locals1 = new AmCluster.<>c__DisplayClass8();
			CS$<>8__locals1.nodeName = nodeName;
			using (AmClusterNodeHandle hNode = ClusapiMethods.OpenClusterNode(this.Handle, CS$<>8__locals1.nodeName.NetbiosName))
			{
				if (hNode == null || hNode.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 5042)
					{
						throw new ClusterNodeNotFoundException(CS$<>8__locals1.nodeName.NetbiosName, new Win32Exception(lastWin32Error));
					}
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "OpenClusterNode(clus,{0})", new object[]
					{
						CS$<>8__locals1.nodeName
					});
				}
				else
				{
					this.RetryOperationOnClusterJoining(delegate
					{
						int hresult;
						uint num = ClusapiMethods.EvictClusterNodeEx(hNode, 90000U, out hresult);
						if (num == 0U)
						{
							return (int)num;
						}
						if (num == 5896U)
						{
							throw new ClusterEvictWithoutCleanupException(CS$<>8__locals1.nodeName.NetbiosName, new Win32Exception(AmCluster.Win32ErrorCodeFromHresult(hresult)));
						}
						if (num == 5042U)
						{
							throw new ClusterNodeNotFoundException(CS$<>8__locals1.nodeName.NetbiosName, new Win32Exception((int)num));
						}
						throw AmExceptionHelper.ConstructClusterApiException((int)num, "EvictClusterNodeEx('{0}')", new object[]
						{
							CS$<>8__locals1.nodeName
						});
					}, "EvictClusterNodeEx( node={0}, timeout={1} )", new object[]
					{
						CS$<>8__locals1.nodeName.NetbiosName,
						90000U
					});
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004674 File Offset: 0x00002874
		public AmClusterNetwork FindNetworkByIPv4Address(IPAddress ipAddr)
		{
			AmClusterNetwork amClusterNetwork = null;
			IEnumerable<AmClusterNetwork> enumerable = this.EnumerateNetworks();
			try
			{
				foreach (AmClusterNetwork amClusterNetwork2 in enumerable)
				{
					if (amClusterNetwork2.IsIPInNetwork(ipAddr))
					{
						amClusterNetwork = amClusterNetwork2;
						break;
					}
				}
			}
			finally
			{
				foreach (AmClusterNetwork amClusterNetwork3 in enumerable)
				{
					if (amClusterNetwork3 != null && amClusterNetwork3 != amClusterNetwork)
					{
						amClusterNetwork3.Dispose();
					}
				}
			}
			return amClusterNetwork;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004724 File Offset: 0x00002924
		public override string ToString()
		{
			return string.Format("cluster:{0}", this.Name);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004738 File Offset: 0x00002938
		public static AmCluster Open()
		{
			AmServerName localComputerName = AmServerName.LocalComputerName;
			return AmCluster.OpenByName(localComputerName);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004751 File Offset: 0x00002951
		internal static AmCluster OpenByName(AmServerName serverName)
		{
			return AmCluster.OpenByName(serverName, TimeSpan.Zero, string.Empty);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004780 File Offset: 0x00002980
		internal static AmCluster OpenByName(AmServerName serverName, TimeSpan timeout, string context)
		{
			AmCluster cluster = null;
			if (timeout == TimeSpan.Zero)
			{
				cluster = AmCluster.OpenByNameInternal(serverName);
			}
			else
			{
				try
				{
					InvokeWithTimeout.Invoke(delegate()
					{
						cluster = AmCluster.OpenByNameInternal(serverName);
					}, timeout);
				}
				catch (TimeoutException innerException)
				{
					throw new OpenClusterTimedoutException(serverName.Fqdn, (int)timeout.TotalSeconds, context, innerException);
				}
			}
			return cluster;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004810 File Offset: 0x00002A10
		internal static AmCluster OpenByNameInternal(AmServerName serverName)
		{
			bool flag = false;
			AmCluster amCluster = null;
			try
			{
				string clusterName = null;
				if (!serverName.IsLocalComputerName)
				{
					clusterName = serverName.Fqdn;
				}
				AmClusterHandle amClusterHandle = ClusapiMethods.OpenCluster(clusterName);
				if (amClusterHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "OpenCluster({0})", new object[]
					{
						serverName.Fqdn
					});
				}
				amCluster = new AmCluster(amClusterHandle, true, null, serverName.Fqdn);
				if (serverName == AmServerName.LocalComputerName)
				{
					amCluster.InitializeLocalNode();
				}
				flag = true;
			}
			finally
			{
				if (!flag && amCluster != null)
				{
					amCluster.Dispose();
					amCluster = null;
				}
			}
			return amCluster;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004A0C File Offset: 0x00002C0C
		internal static AmCluster OpenByNames(IEnumerable<AmServerName> serverNamesToTry)
		{
			AmCluster amCluster = null;
			List<ClusterApiException> list = new List<ClusterApiException>(1);
			Random random = new Random((int)DateTime.UtcNow.Ticks);
			IEnumerable<AmServerName> enumerable = from server in serverNamesToTry
			select new
			{
				Name = server,
				Index = random.Next()
			} into server
			orderby server.Index
			select server.Name;
			if (enumerable.Contains(AmServerName.LocalComputerName))
			{
				List<AmServerName> list2 = new List<AmServerName>(serverNamesToTry.Count<AmServerName>());
				list2.Add(AmServerName.LocalComputerName);
				list2.AddRange(from serverName in enumerable
				where !serverName.IsLocalComputerName
				select serverName);
				enumerable = list2;
			}
			foreach (AmServerName serverName2 in enumerable)
			{
				try
				{
					amCluster = AmCluster.OpenByName(serverName2);
					if (amCluster != null)
					{
						break;
					}
				}
				catch (ClusterApiException item)
				{
					list.Add(item);
				}
			}
			if (amCluster == null && list.Count > 0)
			{
				string[] source = (from clusterException in list
				select clusterException.Message).ToArray<string>();
				string[] source2 = (from serverName in enumerable
				select serverName.Fqdn).ToArray<string>();
				throw new ClusterApiException(string.Format("OpenByNames('{0}') failed for each server. Specific exceptions: '{1}'.", string.Join("', '", source2.ToArray<string>()), string.Join("', '", source.ToArray<string>())), list[0]);
			}
			return amCluster;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004BEC File Offset: 0x00002DEC
		internal static AmCluster TransferClusterFromAmClusterHandle(AmClusterHandle amClusterHandle)
		{
			return new AmCluster(amClusterHandle, false, null, string.Empty);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004C08 File Offset: 0x00002E08
		internal static AmCluster OpenDagClus(DatabaseAvailabilityGroup dag)
		{
			if (dag == null)
			{
				throw new ArgumentNullException("dag");
			}
			List<AmServerName> list = new List<AmServerName>(8);
			if (dag.DatacenterActivationMode == DatacenterActivationModeOption.DagOnly)
			{
				using (MultiValuedProperty<ADObjectId>.Enumerator enumerator = dag.Servers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ADObjectId serverId = enumerator.Current;
						if (!dag.StoppedMailboxServers.Contains(new AmServerName(serverId).Fqdn))
						{
							list.Add(new AmServerName(serverId));
						}
					}
					goto IL_AD;
				}
			}
			foreach (ADObjectId serverId2 in dag.Servers)
			{
				list.Add(new AmServerName(serverId2));
			}
			IL_AD:
			if (list.Count == 0)
			{
				throw new ClusterNoServerToConnectException(dag.Name);
			}
			return AmCluster.OpenByNames(list);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004CF8 File Offset: 0x00002EF8
		internal static int Win32ErrorCodeFromHresult(int hresult)
		{
			if ((hresult & -65536) == -2147024896 || (hresult & -65536) == -2147418112)
			{
				return hresult & 65535;
			}
			return hresult;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004D88 File Offset: 0x00002F88
		public AmClusterGroup OpenGroup(string groupName)
		{
			if (string.IsNullOrEmpty(groupName))
			{
				throw new ArgumentNullException("groupName");
			}
			AmClusterGroup result = null;
			AmClusterGroupHandle groupHandle = null;
			bool flag = false;
			try
			{
				this.RetryOperationOnClusterJoining(delegate
				{
					groupHandle = ClusapiMethods.OpenClusterGroup(this.m_handle, groupName);
					if (groupHandle.IsInvalid)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						groupHandle.Dispose();
						groupHandle = null;
						return lastWin32Error;
					}
					return 0;
				}, "OpenGroup({0})", new object[]
				{
					groupName
				});
				result = new AmClusterGroup(this, groupName, groupHandle);
				flag = true;
			}
			finally
			{
				if (!flag && groupHandle != null)
				{
					groupHandle.Dispose();
					groupHandle = null;
				}
			}
			return result;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004E44 File Offset: 0x00003044
		public AmClusterNetwork OpenNetwork(string networkName)
		{
			if (string.IsNullOrEmpty(networkName))
			{
				throw new ArgumentNullException("networkName");
			}
			AmClusterNetwork result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				AmClusterNetworkHandle amClusterNetworkHandle = ClusapiMethods.OpenClusterNetwork(this.m_handle, networkName);
				disposeGuard.Add<AmClusterNetworkHandle>(amClusterNetworkHandle);
				if (amClusterNetworkHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if ((long)lastWin32Error != 5045L)
					{
						throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "OpenClusterNetwork('{0}')", new object[]
						{
							networkName
						});
					}
					AmCluster.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "OpenClusterNetwork( {0} ) failed with {1}. Silently continuing.", networkName, lastWin32Error);
					result = null;
				}
				else
				{
					AmClusterNetwork amClusterNetwork = new AmClusterNetwork(networkName, this, amClusterNetworkHandle);
					disposeGuard.Success();
					result = amClusterNetwork;
				}
			}
			return result;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004F08 File Offset: 0x00003108
		public AmClusterNetInterface OpenNetInterface(string nicName)
		{
			if (string.IsNullOrEmpty(nicName))
			{
				throw new ArgumentNullException("nicName");
			}
			AmClusterNetInterfaceHandle amClusterNetInterfaceHandle = ClusapiMethods.OpenClusterNetInterface(this.m_handle, nicName);
			if (amClusterNetInterfaceHandle == null || amClusterNetInterfaceHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "OpenNetInterface({0})", new object[]
				{
					nicName
				});
			}
			return new AmClusterNetInterface(nicName, amClusterNetInterfaceHandle);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004F68 File Offset: 0x00003168
		public IAmClusterNode OpenNode(AmServerName nodeName)
		{
			if (nodeName == null)
			{
				throw new ArgumentNullException("nodeName");
			}
			AmClusterNodeHandle amClusterNodeHandle = ClusapiMethods.OpenClusterNode(this.m_handle, nodeName.NetbiosName);
			if (amClusterNodeHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "OpenClusterNode({0})", new object[]
				{
					nodeName.Fqdn
				});
			}
			return new AmClusterNode(nodeName, this, amClusterNodeHandle);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004FCC File Offset: 0x000031CC
		public AmClusterResource OpenResource(string resourceName)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			AmClusterResourceHandle amClusterResourceHandle = null;
			bool flag = false;
			AmClusterResource result;
			try
			{
				amClusterResourceHandle = ClusapiMethods.OpenClusterResource(this.m_handle, resourceName);
				if (amClusterResourceHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "OpenClusterResource({0})", new object[]
					{
						resourceName
					});
				}
				AmClusterResource amClusterResource = new AmClusterResource(resourceName, this, amClusterResourceHandle);
				flag = true;
				result = amClusterResource;
			}
			finally
			{
				if (!flag && amClusterResourceHandle != null)
				{
					amClusterResourceHandle.Dispose();
					amClusterResourceHandle = null;
				}
			}
			return result;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005054 File Offset: 0x00003254
		public AmNodeState GetNodeState(AmServerName nodeName, out Exception ex)
		{
			ex = null;
			AmNodeState result = AmNodeState.Unknown;
			try
			{
				using (IAmClusterNode amClusterNode = this.OpenNode(nodeName))
				{
					result = amClusterNode.GetState(true);
				}
			}
			catch (ClusterApiException ex2)
			{
				ex = ex2;
				AmCluster.Tracer.TraceError<AmServerName, string>((long)this.GetHashCode(), "Failed to get node state on node {0} (error={1})", nodeName, ex.Message);
			}
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000050C8 File Offset: 0x000032C8
		public AmNetInterfaceState GetNetInterfaceState(string nicName, out Exception ex)
		{
			ex = null;
			AmNetInterfaceState result = AmNetInterfaceState.Unknown;
			try
			{
				using (AmClusterNetInterface amClusterNetInterface = this.OpenNetInterface(nicName))
				{
					result = amClusterNetInterface.GetState(true);
				}
			}
			catch (ClusterApiException ex2)
			{
				ex = ex2;
				AmCluster.Tracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to get nic state for nic {0} (error={1})", nicName, ex.Message);
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000513C File Offset: 0x0000333C
		public string GetQuorumResourceInformation(out string outDeviceName, out uint outMaxQuorumLogSize)
		{
			uint capacity = 256U;
			StringBuilder stringBuilder = new StringBuilder((int)capacity);
			uint capacity2 = 256U;
			StringBuilder stringBuilder2 = new StringBuilder((int)capacity2);
			uint num;
			uint clusterQuorumResource;
			for (;;)
			{
				clusterQuorumResource = ClusapiMethods.GetClusterQuorumResource(this.Handle, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out num);
				if (clusterQuorumResource != 234U)
				{
					break;
				}
				stringBuilder.EnsureCapacity((int)capacity);
				stringBuilder2.EnsureCapacity((int)capacity2);
			}
			if (clusterQuorumResource != 0U)
			{
				throw AmExceptionHelper.ConstructClusterApiException((int)clusterQuorumResource, "GetClusterQuorumResource()", new object[0]);
			}
			outDeviceName = stringBuilder2.ToString();
			outMaxQuorumLogSize = num;
			return stringBuilder.ToString();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000051BC File Offset: 0x000033BC
		public AmClusterResource OpenQuorumResource()
		{
			AmClusterResource result = null;
			string text;
			uint num;
			string quorumResourceInformation = this.GetQuorumResourceInformation(out text, out num);
			if (!string.IsNullOrEmpty(quorumResourceInformation))
			{
				result = this.OpenResource(quorumResourceInformation);
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000051E8 File Offset: 0x000033E8
		public void SetQuorumResource(IAmClusterResource newQuorum, string deviceName, uint maxLogSize)
		{
			string typeName = newQuorum.GetTypeName();
			if (typeName == "File Share Witness")
			{
				if (maxLogSize == 0U)
				{
					maxLogSize = 1024U;
				}
			}
			else if (typeName == "Network Name")
			{
				maxLogSize = 0U;
			}
			uint num = ClusapiMethods.SetClusterQuorumResource(newQuorum.Handle, deviceName, maxLogSize);
			if (num != 0U)
			{
				throw AmExceptionHelper.ConstructClusterApiException((int)num, "SetClusterQuorumResource({0})", new object[]
				{
					deviceName
				});
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005250 File Offset: 0x00003450
		public void ClearQuorumResource()
		{
			AmCluster.Tracer.TraceDebug((long)this.GetHashCode(), "Creating a PowerShell session");
			using (PowerShell powerShell = PowerShell.Create())
			{
				PSCommand pscommand = new PSCommand();
				PSInvocationSettings psinvocationSettings = new PSInvocationSettings();
				psinvocationSettings.ErrorActionPreference = new ActionPreference?(ActionPreference.Stop);
				AmCluster.Tracer.TraceDebug((long)this.GetHashCode(), "Importing the FailoverClusters module");
				pscommand.Clear();
				pscommand.AddCommand("Import-Module").AddParameter("Name", "FailoverClusters");
				powerShell.Commands = pscommand;
				Collection<PSObject> collection = powerShell.Invoke(null, psinvocationSettings);
				foreach (PSObject arg in collection)
				{
					AmCluster.Tracer.TraceDebug<PSObject>((long)this.GetHashCode(), "Import-Module returned: {0}", arg);
				}
				AmCluster.Tracer.TraceDebug((long)this.GetHashCode(), "Calling Set-ClusterQuorum -NoWitness");
				pscommand.Clear();
				pscommand.AddCommand("Set-ClusterQuorum").AddParameter("NoWitness", true);
				powerShell.Commands = pscommand;
				collection = powerShell.Invoke(null, psinvocationSettings);
				foreach (PSObject arg2 in collection)
				{
					AmCluster.Tracer.TraceDebug<PSObject>((long)this.GetHashCode(), "Set-ClusterQuorum returned: {0}", arg2);
				}
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000053FC File Offset: 0x000035FC
		internal IEnumerable<string> EnumerateGroupNames()
		{
			return this.EnumerateObjects(AmClusterEnum.CLUSTER_ENUM_GROUP);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005405 File Offset: 0x00003605
		internal IEnumerable<IAmClusterGroup> EnumerateGroups()
		{
			return AmCluster.EvaluateAllElements<IAmClusterGroup>(this.LazyEnumerateGroups());
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005412 File Offset: 0x00003612
		internal IEnumerable<string> EnumerateNetworkNames()
		{
			return this.EnumerateObjects(AmClusterEnum.CLUSTER_ENUM_NETWORK);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000541C File Offset: 0x0000361C
		public IEnumerable<AmClusterNetwork> EnumerateNetworks()
		{
			return AmCluster.EvaluateAllElements<AmClusterNetwork>(this.LazyEnumerateNetworks());
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000542C File Offset: 0x0000362C
		public AmClusterNetwork FindNetworkByName(string networkName, IPVersion ipVer)
		{
			AmClusterNetwork amClusterNetwork = this.OpenNetwork(networkName);
			if (amClusterNetwork != null)
			{
				return amClusterNetwork;
			}
			IEnumerable<AmClusterNetwork> enumerable = this.EnumerateNetworks();
			try
			{
				foreach (AmClusterNetwork amClusterNetwork2 in enumerable)
				{
					IEnumerable<string> enumerable2;
					if (ipVer == IPVersion.IPv4)
					{
						enumerable2 = amClusterNetwork2.EnumerateAlternateIPv4Names();
					}
					else
					{
						enumerable2 = amClusterNetwork2.EnumerateAlternateIPv6Names();
					}
					foreach (string str in enumerable2)
					{
						if (SharedHelper.StringIEquals(str, networkName))
						{
							amClusterNetwork = amClusterNetwork2;
							break;
						}
					}
					if (amClusterNetwork != null)
					{
						break;
					}
				}
			}
			finally
			{
				foreach (AmClusterNetwork amClusterNetwork3 in enumerable)
				{
					if (amClusterNetwork3 != null && amClusterNetwork3 != amClusterNetwork)
					{
						amClusterNetwork3.Dispose();
					}
				}
			}
			if (amClusterNetwork == null)
			{
				ExTraceGlobals.ClusterTracer.TraceDebug<string>((long)this.GetHashCode(), "Unable to find a network with name '{0}'", networkName);
			}
			return amClusterNetwork;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005550 File Offset: 0x00003750
		internal IEnumerable<string> EnumerateResourceNames()
		{
			return this.EnumerateObjects(AmClusterEnum.CLUSTER_ENUM_RESOURCE);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005559 File Offset: 0x00003759
		internal IEnumerable<AmClusterResource> EnumerateResources()
		{
			return AmCluster.EvaluateAllElements<AmClusterResource>(this.LazyEnumerateResources());
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005566 File Offset: 0x00003766
		internal IEnumerable<AmServerName> EnumerateNodeNames()
		{
			return AmCluster.EvaluateAllElements<AmServerName>(this.LazyEnumerateNodeNames());
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000557C File Offset: 0x0000377C
		public IEnumerable<IAmClusterNode> EnumerateNodes()
		{
			return AmCluster.EvaluateAllElements<IAmClusterNode>(from nodeName in this.EnumerateNodeNames()
			select this.OpenNode(nodeName));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000559C File Offset: 0x0000379C
		public IAmClusterGroup FindCoreClusterGroup()
		{
			AmClusterRegkeyHandle amClusterRegkeyHandle = null;
			AmClusterRegkeyHandle amClusterRegkeyHandle2 = null;
			IAmClusterGroup amClusterGroup = null;
			bool flag = false;
			try
			{
				amClusterRegkeyHandle = ClusapiMethods.GetClusterKey(this.Handle, RegSAM.AllAccess);
				if (amClusterRegkeyHandle == null || amClusterRegkeyHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "GetClusterKey()", new object[0]);
				}
				bool flag2 = false;
				string str = AmClusterRegProperty.Get<string>("ClusterGroup", amClusterRegkeyHandle, out flag2);
				if (!flag2)
				{
					throw new AmCoreGroupRegNotFound("ClusterGroup");
				}
				string text = "Groups\\" + str;
				int num = ClusapiMethods.ClusterRegOpenKey(amClusterRegkeyHandle, text, RegSAM.Read, out amClusterRegkeyHandle2);
				if (num != 0)
				{
					throw AmExceptionHelper.ConstructClusterApiException(num, "ClusterRegOpenKey({0})", new object[]
					{
						text
					});
				}
				string groupName = AmClusterRegProperty.Get<string>("Name", amClusterRegkeyHandle2, out flag2);
				if (!flag2)
				{
					throw new AmCoreGroupRegNotFound("ClusterGroup");
				}
				amClusterGroup = this.OpenGroup(groupName);
				if (amClusterGroup != null && amClusterGroup.IsCoreGroup())
				{
					flag = true;
				}
			}
			finally
			{
				if (!flag && amClusterGroup != null)
				{
					amClusterGroup.Dispose();
					amClusterGroup = null;
				}
				if (amClusterRegkeyHandle2 != null)
				{
					amClusterRegkeyHandle2.Close();
					amClusterRegkeyHandle2 = null;
				}
				if (amClusterRegkeyHandle != null)
				{
					amClusterRegkeyHandle.Close();
					amClusterRegkeyHandle = null;
				}
			}
			return amClusterGroup;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000056B8 File Offset: 0x000038B8
		internal bool CheckVoterEvictLoseQuorum(string resourceNodeIdentifier)
		{
			uint num = 4U;
			AmClusterRawData amClusterRawData = AmClusterRawData.Allocate(num);
			IntPtr intPtr = IntPtr.Zero;
			bool flag = true;
			AmClusterClusterControlCode amClusterClusterControlCode = AmClusterClusterControlCode.CLUSCTL_CLUSTER_CHECK_VOTER_EVICT;
			try
			{
				using (amClusterRawData)
				{
					intPtr = Marshal.StringToHGlobalUni(resourceNodeIdentifier);
					uint inBufferSize = (uint)((resourceNodeIdentifier.Length + 1) * 2);
					int num2 = ClusapiMethods.ClusterControl(this.m_handle, IntPtr.Zero, amClusterClusterControlCode, intPtr, inBufferSize, amClusterRawData.Buffer, num, out num);
					if (num2 != 0)
					{
						throw AmExceptionHelper.ConstructClusterApiException(num2, "ClusterNodeControl(controlcode={0})", new object[]
						{
							amClusterClusterControlCode
						});
					}
					if (amClusterRawData.ReadInt32() == 0)
					{
						flag = false;
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
					intPtr = IntPtr.Zero;
				}
			}
			AmTrace.Debug("CheckVoterEvictLoseQuorum: Evicting {0} will result in lost quorum: {1}", new object[]
			{
				resourceNodeIdentifier,
				flag
			});
			return flag;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000057B4 File Offset: 0x000039B4
		internal void RetryOperationOnClusterJoining(AmCluster.ClusterOperation operation, string functionName, params object[] functionArgs)
		{
			int i = 0;
			while (i < 90000)
			{
				int num = operation();
				if (num == 70)
				{
					Thread.Sleep(3000);
					i += 3000;
					AmTrace.Debug("Retry {0} on ERROR_SHARING_PAUSED", new object[]
					{
						string.Format(functionName, functionArgs)
					});
				}
				else
				{
					if (num == 0)
					{
						return;
					}
					throw AmExceptionHelper.ConstructClusterApiException(num, string.Format(functionName, functionArgs), new object[0]);
				}
			}
			throw AmExceptionHelper.ConstructClusterApiException(70, string.Format(functionName, functionArgs), new object[0]);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005838 File Offset: 0x00003A38
		internal AmClusterRawData GetClusterControlData(AmClusterClusterControlCode code, uint initialDataSize = 1024U)
		{
			uint num = initialDataSize;
			AmClusterRawData amClusterRawData = AmClusterRawData.Allocate(num);
			int num2 = ClusapiMethods.ClusterControl(this.Handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			if (num2 == 234)
			{
				amClusterRawData.Dispose();
				amClusterRawData = AmClusterRawData.Allocate(num);
				num2 = ClusapiMethods.ClusterControl(this.Handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			}
			if (num2 != 0)
			{
				amClusterRawData.Dispose();
				throw AmExceptionHelper.ConstructClusterApiException(num2, "ClusterControl(controlcode={0})", new object[]
				{
					code
				});
			}
			return amClusterRawData;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000058D0 File Offset: 0x00003AD0
		internal AmServerName GetCurrentGumLockOwnerInfo(out int currentGumId)
		{
			currentGumId = 0;
			AmServerName result = null;
			using (AmClusterRawData clusterControlData = this.GetClusterControlData(AmClusterClusterControlCode.CLUSCTL_CLUSTER_GET_GUM_LOCK_OWNER, 1024U))
			{
				IntPtr intPtr = clusterControlData.Buffer;
				currentGumId = Marshal.ReadInt32(intPtr);
				intPtr += Marshal.SizeOf(typeof(int));
				result = new AmServerName(Marshal.PtrToStringUni(intPtr));
			}
			return result;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005944 File Offset: 0x00003B44
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AmCluster>(this);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000594C File Offset: 0x00003B4C
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				if (disposing)
				{
					if (this.m_localNode != null)
					{
						this.m_localNode.Dispose();
						this.m_localNode = null;
					}
					if (this.m_ownsHandleLifetime)
					{
						this.m_handle.Dispose();
						this.m_handle = null;
					}
				}
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000059B8 File Offset: 0x00003BB8
		private void InitializeLocalNode()
		{
			this.m_localNode = this.OpenNode(AmServerName.LocalComputerName);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000059CC File Offset: 0x00003BCC
		private List<string> EnumerateObjects(AmClusterEnum objectType)
		{
			List<string> list = new List<string>(16);
			using (AmClusEnumHandle amClusEnumHandle = ClusapiMethods.ClusterOpenEnum(this.m_handle, objectType))
			{
				if (amClusEnumHandle == null || amClusEnumHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "ClusterOpenEnum(objecttype={0})", new object[]
					{
						objectType
					});
				}
				int num = 0;
				int capacity = 256;
				StringBuilder stringBuilder = new StringBuilder(capacity);
				int num2;
				for (;;)
				{
					capacity = stringBuilder.Capacity;
					AmClusterEnum amClusterEnum;
					num2 = ClusapiMethods.ClusterEnum(amClusEnumHandle, num, out amClusterEnum, stringBuilder, ref capacity);
					if (num2 == 259)
					{
						goto IL_BD;
					}
					if (num2 == 234)
					{
						stringBuilder.EnsureCapacity(capacity);
					}
					else
					{
						if (num2 != 0)
						{
							break;
						}
						if (amClusterEnum == objectType)
						{
							list.Add(stringBuilder.ToString());
						}
						num++;
					}
				}
				throw AmExceptionHelper.ConstructClusterApiException(num2, "ClusterEnum()", new object[0]);
				IL_BD:;
			}
			return list;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005ABD File Offset: 0x00003CBD
		private IEnumerable<IAmClusterGroup> LazyEnumerateGroups()
		{
			return from groupName in this.EnumerateGroupNames()
			select this.OpenGroup(groupName);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005C14 File Offset: 0x00003E14
		private IEnumerable<AmClusterNetwork> LazyEnumerateNetworks()
		{
			return from networkName in this.EnumerateNetworkNames()
			let network = this.OpenNetwork(networkName)
			where network != null
			select network;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005C87 File Offset: 0x00003E87
		private IEnumerable<AmClusterResource> LazyEnumerateResources()
		{
			return from resourceName in this.EnumerateResourceNames()
			select this.OpenResource(resourceName);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005E2C File Offset: 0x0000402C
		private IEnumerable<AmServerName> LazyEnumerateNodeNames()
		{
			foreach (string nodeShortName in this.EnumerateObjects(AmClusterEnum.CLUSTER_ENUM_NODE))
			{
				yield return new AmServerName(nodeShortName);
			}
			yield break;
		}

		// Token: 0x04000033 RID: 51
		private const int MAX_COMPUTERNAME_LENGTH = 15;

		// Token: 0x04000034 RID: 52
		private const int PollingInterval = 3000;

		// Token: 0x04000035 RID: 53
		private const int ServiceTimeout = 90000;

		// Token: 0x04000036 RID: 54
		private const string ClusterGroupRegistryName = "ClusterGroup";

		// Token: 0x04000037 RID: 55
		private const string ClusterGroupsRegistryKeyName = "Groups";

		// Token: 0x04000038 RID: 56
		private const string ClusterGroupRegistryValueName = "Name";

		// Token: 0x04000039 RID: 57
		private const uint EvictionTimeout = 90000U;

		// Token: 0x0400003A RID: 58
		private AmClusterHandle m_handle;

		// Token: 0x0400003B RID: 59
		private IAmClusterNode m_localNode;

		// Token: 0x0400003C RID: 60
		private bool m_ownsHandleLifetime;

		// Token: 0x0400003D RID: 61
		private string m_name;

		// Token: 0x0400003E RID: 62
		private string m_cnoName;

		// Token: 0x0200001C RID: 28
		// (Invoke) Token: 0x06000109 RID: 265
		public delegate int ClusterOperation();
	}
}
