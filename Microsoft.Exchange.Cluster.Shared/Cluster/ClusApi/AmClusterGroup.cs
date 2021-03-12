using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000025 RID: 37
	internal class AmClusterGroup : DisposeTrackableBase, IAmClusterGroup, IDisposable
	{
		// Token: 0x0600015A RID: 346 RVA: 0x000066C0 File Offset: 0x000048C0
		internal AmClusterGroup(IAmCluster owningCluster, string groupName, AmClusterGroupHandle groupHandle)
		{
			this.m_name = groupName;
			this.m_handle = groupHandle;
			this.OwningCluster = owningCluster;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000066DD File Offset: 0x000048DD
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000066E5 File Offset: 0x000048E5
		internal IAmCluster OwningCluster { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000066EE File Offset: 0x000048EE
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000066F6 File Offset: 0x000048F6
		internal AmClusterGroupHandle Handle
		{
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006700 File Offset: 0x00004900
		public AmGroupState State
		{
			get
			{
				AmGroupState result;
				AmServerName amServerName;
				this.GetStateAndOwningNode(out result, out amServerName);
				return result;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006718 File Offset: 0x00004918
		public AmServerName OwnerNode
		{
			get
			{
				AmGroupState amGroupState;
				AmServerName result;
				this.GetStateAndOwningNode(out amGroupState, out result);
				return result;
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000067B8 File Offset: 0x000049B8
		public static void MoveClusterGroupWithTimeout(AmServerName clusterNode, AmServerName destinationNode, TimeSpan timeout)
		{
			uint errorCode = 0U;
			bool flag = false;
			try
			{
				InvokeWithTimeout.Invoke(delegate()
				{
					using (IAmCluster amCluster = MockableCluster.Instance.OpenByName(clusterNode))
					{
						using (IAmClusterGroup amClusterGroup = amCluster.FindCoreClusterGroup())
						{
							if (!amClusterGroup.OwnerNode.Equals(destinationNode))
							{
								amClusterGroup.MoveGroup(timeout, destinationNode);
							}
						}
					}
				}, timeout);
			}
			catch (TimeoutException)
			{
				errorCode = 258U;
				flag = true;
			}
			if (flag)
			{
				throw AmExceptionHelper.ConstructClusterApiException((int)errorCode, "MoveClusterGroup( dest={0}, timeout={1})", new object[]
				{
					destinationNode.NetbiosName,
					timeout
				});
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006858 File Offset: 0x00004A58
		public bool IsQuorumGroup()
		{
			return false;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000685C File Offset: 0x00004A5C
		public bool IsClusterGroup()
		{
			using (IAmClusterGroup amClusterGroup = this.OwningCluster.FindCoreClusterGroup())
			{
				if (amClusterGroup != null && SharedHelper.StringIEquals(amClusterGroup.Name, this.Name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000068B0 File Offset: 0x00004AB0
		public IAmClusterResource CreateUniqueResource(ILogTraceHelper logger, string resPrefix, string resType, ref int nextIndex)
		{
			int num = nextIndex;
			string text = null;
			IAmClusterResource result = null;
			logger = (logger ?? NullLogTraceHelper.GetNullLogger());
			if (resPrefix == null)
			{
				resPrefix = resType;
			}
			try
			{
				IL_1A:
				text = string.Format("{0} {1} ({2})", resPrefix, num, this.Name);
				logger.AppendLogMessage("CreateUniqueResource() Trying to create a resource named '{0}'.", new object[]
				{
					text
				});
				result = this.CreateResource(text, resType);
				num++;
				nextIndex = num;
				logger.AppendLogMessage("CreateUniqueResource() Created a resource named '{0}'.", new object[]
				{
					text
				});
			}
			catch (ClusResourceAlreadyExistsException)
			{
				logger.AppendLogMessage("CreateUniqueResource() Resource '{0}' already exists. Trying again.", new object[]
				{
					text
				});
				num++;
				goto IL_1A;
			}
			return result;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006968 File Offset: 0x00004B68
		public IAmClusterResource CreateResource(string resName, string resType)
		{
			try
			{
				using (this.OwningCluster.OpenResource(resName))
				{
					throw new ClusResourceAlreadyExistsException(resName);
				}
			}
			catch (ClusterApiException ex)
			{
				Win32Exception ex2 = ex.InnerException as Win32Exception;
				if (ex2 != null && (long)ex2.NativeErrorCode != 5007L)
				{
					throw;
				}
			}
			AmClusterResourceHandle amClusterResourceHandle = ClusapiMethods.CreateClusterResource(this.Handle, resName, resType, ClusterResourceCreateFlags.CLUSTER_RESOURCE_DEFAULT_MONITOR);
			if (amClusterResourceHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "CreateClusterResource(name={0},type={1})", new object[]
				{
					resName,
					resType
				});
			}
			return new AmClusterResource(resName, this.OwningCluster, amClusterResourceHandle);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006A24 File Offset: 0x00004C24
		public void MoveGroup(TimeSpan timeout, AmServerName destinationNode)
		{
			DateTime t = DateTime.UtcNow.Add(timeout);
			uint num = 0U;
			AmGroupState amGroupState;
			AmServerName amServerName;
			this.GetStateAndOwningNode(out amGroupState, out amServerName);
			using (IAmClusterNode amClusterNode = this.OwningCluster.OpenNode(destinationNode))
			{
				num = ClusapiMethods.MoveClusterGroup(this.Handle, amClusterNode.Handle);
				goto IL_87;
			}
			IL_4B:
			AmGroupState amGroupState2;
			AmServerName amServerName2;
			this.GetStateAndOwningNode(out amGroupState2, out amServerName2);
			if (amGroupState == amGroupState2)
			{
				num = 0U;
				goto IL_9C;
			}
			ExTraceGlobals.ClusterTracer.TraceDebug<string, AmGroupState, AmGroupState>((long)this.GetHashCode(), "MoveGroup: {0} is in progress (initial={1}, current={2}), waiting and polling again.", this.Name, amGroupState, amGroupState2);
			Thread.Sleep(1000);
			IL_87:
			if (num == 997U && DateTime.UtcNow < t)
			{
				goto IL_4B;
			}
			IL_9C:
			if (num != 0U)
			{
				throw AmExceptionHelper.ConstructClusterApiException((int)num, "MoveClusterGroup( name={0}, dest={1})", new object[]
				{
					this.Name,
					destinationNode.NetbiosName
				});
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006B0C File Offset: 0x00004D0C
		public bool MoveGroupToReplayEnabledNode(IsReplayRunning isReplayRunning, string resourceType, TimeSpan timeout, out string finalDestinationNode)
		{
			finalDestinationNode = null;
			if (resourceType == string.Empty)
			{
				return this.MoveGroupToReplayEnabledNode(isReplayRunning, timeout, out finalDestinationNode);
			}
			IEnumerable<AmClusterResource> enumerable = this.EnumerateResourcesOfType(resourceType);
			AmClusterResource amClusterResource = enumerable.ElementAtOrDefault(0);
			if (amClusterResource != null)
			{
				try
				{
					AmServerName ownerNode = this.OwnerNode;
					foreach (string text in amClusterResource.EnumeratePossibleOwnerNames())
					{
						AmServerName amServerName = new AmServerName(text);
						if (!ownerNode.Equals(amServerName) && isReplayRunning(amServerName.Fqdn))
						{
							try
							{
								AmClusterGroup.MoveClusterGroupWithTimeout(ownerNode, amServerName, timeout);
								if (this.OwnerNode.Equals(amServerName))
								{
									finalDestinationNode = amServerName.Fqdn;
									return true;
								}
							}
							catch (ClusterException arg)
							{
								ExTraceGlobals.ClusterTracer.TraceError<string, string, ClusterException>((long)this.GetHashCode(), "MoveGroupToReplayEnabledNode: MoveGroup ({0}) to node {1} failed with exception: {2}.", this.Name, text, arg);
							}
						}
					}
					return false;
				}
				finally
				{
					foreach (AmClusterResource amClusterResource2 in enumerable)
					{
						amClusterResource2.Dispose();
					}
				}
			}
			return this.MoveGroupToReplayEnabledNode(isReplayRunning, timeout, out finalDestinationNode);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006C6C File Offset: 0x00004E6C
		private bool MoveGroupToReplayEnabledNode(IsReplayRunning isReplayRunning, TimeSpan timeout, out string finalDestinationNode)
		{
			DateTime dateTime = DateTime.UtcNow.Add(timeout);
			AmServerName ownerNode = this.OwnerNode;
			finalDestinationNode = ownerNode.NetbiosName;
			foreach (IAmClusterNode amClusterNode in this.OwningCluster.EnumerateNodes())
			{
				try
				{
					if (!amClusterNode.Name.Equals(this.OwnerNode))
					{
						TimeSpan t = dateTime.Subtract(DateTime.UtcNow);
						if (t > TimeSpan.Zero && amClusterNode.State == AmNodeState.Up && isReplayRunning(amClusterNode.Name.Fqdn))
						{
							try
							{
								AmServerName name = amClusterNode.Name;
								AmClusterGroup.MoveClusterGroupWithTimeout(ownerNode, name, timeout);
								if (this.OwnerNode.Equals(name))
								{
									finalDestinationNode = name.Fqdn;
									return true;
								}
							}
							catch (ClusterException arg)
							{
								ExTraceGlobals.ClusterTracer.TraceError<string, AmServerName, ClusterException>((long)this.GetHashCode(), "MoveGroupToReplayEnabledNode: MoveGroup ({0}) to node {1} failed with exception: {2}.", this.Name, amClusterNode.Name, arg);
							}
						}
					}
				}
				finally
				{
					amClusterNode.Dispose();
				}
			}
			return false;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006DAC File Offset: 0x00004FAC
		internal IEnumerable<string> EnumerateResourceNames()
		{
			return AmClusterGroup.EnumerateObjects(this.m_handle, AmClusterGroupEnum.CLUSTER_GROUP_ENUM_CONTAINS);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006DBA File Offset: 0x00004FBA
		public IEnumerable<AmClusterResource> EnumerateResources()
		{
			return AmCluster.EvaluateAllElements<AmClusterResource>(this.LazyEnumerateResources());
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public IEnumerable<AmClusterResource> EnumerateResourcesOfType(string resourceType)
		{
			IEnumerable<AmClusterResource> enumerable = this.EnumerateResources();
			List<AmClusterResource> list = (from resource in enumerable
			where SharedHelper.StringIEquals(resource.GetTypeName(), resourceType)
			select resource).ToList<AmClusterResource>();
			IEnumerable<AmClusterResource> enumerable2 = enumerable.Except(list);
			foreach (AmClusterResource amClusterResource in enumerable2)
			{
				amClusterResource.Dispose();
			}
			return list;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006E68 File Offset: 0x00005068
		public IAmClusterResource FindResourceByTypeName(string desiredTypeName)
		{
			AmClusterResource amClusterResource = null;
			foreach (AmClusterResource amClusterResource2 in this.EnumerateResources())
			{
				if (amClusterResource != null || !SharedHelper.StringIEquals(amClusterResource2.GetTypeName(), desiredTypeName))
				{
					amClusterResource2.Dispose();
				}
				else
				{
					amClusterResource = amClusterResource2;
				}
			}
			return amClusterResource;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006ECC File Offset: 0x000050CC
		public bool IsCoreGroup()
		{
			bool result = false;
			using (AmClusterRawData groupControlData = this.GetGroupControlData(AmClusterGroupControlCode.CLUSCTL_GROUP_GET_FLAGS))
			{
				int num = groupControlData.ReadInt32();
				if ((num & 1) == 1)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006F14 File Offset: 0x00005114
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AmClusterGroup>(this);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006F1C File Offset: 0x0000511C
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				if (disposing && !this.m_handle.IsInvalid)
				{
					this.m_handle.Dispose();
					this.m_handle = null;
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007208 File Offset: 0x00005408
		private static IEnumerable<string> EnumerateObjects(AmClusterGroupHandle handle, AmClusterGroupEnum objectType)
		{
			new List<string>(16);
			using (AmClusGroupEnumHandle enumHandle = ClusapiMethods.ClusterGroupOpenEnum(handle, objectType))
			{
				if (enumHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "ClusterOpenGroupEnum(objecttype={0})", new object[]
					{
						objectType
					});
				}
				int entryIndex = 0;
				int objectNameLen = 256;
				StringBuilder objectNameBuffer = new StringBuilder(objectNameLen);
				int errorCode;
				for (;;)
				{
					objectNameLen = objectNameBuffer.Capacity;
					AmClusterGroupEnum objectTypeRetrived;
					errorCode = ClusapiMethods.ClusterGroupEnum(enumHandle, entryIndex, out objectTypeRetrived, objectNameBuffer, ref objectNameLen);
					if (errorCode == 259)
					{
						goto IL_171;
					}
					if (errorCode == 234)
					{
						objectNameBuffer.EnsureCapacity(objectNameLen);
					}
					else
					{
						if (errorCode != 0)
						{
							break;
						}
						if (objectTypeRetrived == objectType)
						{
							yield return objectNameBuffer.ToString();
						}
						entryIndex++;
					}
				}
				throw AmExceptionHelper.ConstructClusterApiException(errorCode, "ClusterGroupEnum()", new object[0]);
				IL_171:;
			}
			yield break;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000723A File Offset: 0x0000543A
		private IEnumerable<AmClusterResource> LazyEnumerateResources()
		{
			return from resourceName in this.EnumerateResourceNames()
			select this.OwningCluster.OpenResource(resourceName);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007254 File Offset: 0x00005454
		private void GetStateAndOwningNode(out AmGroupState state, out AmServerName nodeName)
		{
			int capacity = 260;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			state = ClusapiMethods.GetClusterGroupState(this.m_handle, stringBuilder, ref capacity);
			if (state == AmGroupState.Unknown)
			{
				throw new ClusterApiException("GetClusterGroupState() failed. State returned is Unknown.", new Win32Exception());
			}
			nodeName = new AmServerName(stringBuilder.ToString());
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000072A0 File Offset: 0x000054A0
		private AmClusterRawData GetGroupControlData(AmClusterGroupControlCode code)
		{
			uint num = 1024U;
			AmClusterRawData amClusterRawData = AmClusterRawData.Allocate(num);
			int num2 = ClusapiMethods.ClusterGroupControl(this.m_handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			if (num2 == 234)
			{
				amClusterRawData.Dispose();
				amClusterRawData = AmClusterRawData.Allocate(num);
				num2 = ClusapiMethods.ClusterGroupControl(this.m_handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			}
			if (num2 != 0)
			{
				amClusterRawData.Dispose();
				throw AmExceptionHelper.ConstructClusterApiException(num2, "ClusterGroupControl(controlcode={0})", new object[]
				{
					code
				});
			}
			return amClusterRawData;
		}

		// Token: 0x0400005B RID: 91
		private string m_name;

		// Token: 0x0400005C RID: 92
		private AmClusterGroupHandle m_handle;
	}
}
