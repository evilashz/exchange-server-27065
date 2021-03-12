using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200002F RID: 47
	internal class AmClusterNode : EqualityComparer<AmClusterNode>, IDisposeTrackable, IEquatable<AmClusterNode>, IEqualityComparer<AmClusterNode>, IAmClusterNode, IDisposable
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00008548 File Offset: 0x00006748
		internal static bool IsNodeUp(AmNodeState nodeState)
		{
			return nodeState == AmNodeState.Up || nodeState == AmNodeState.Paused;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008554 File Offset: 0x00006754
		internal static AmServerName GetNameById(int nodeId)
		{
			AmServerName result = null;
			string name = string.Format("Cluster\\Nodes\\{0}", nodeId);
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
			{
				if (registryKey != null)
				{
					result = new AmServerName((string)registryKey.GetValue("NodeName"));
				}
			}
			return result;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000086C0 File Offset: 0x000068C0
		internal static IEnumerable<int> GetNodeIdsFromNodeMask(long nodeMask)
		{
			int nodeId = 1;
			while (nodeMask != 0L)
			{
				nodeMask >>= 1;
				if ((nodeMask & 1L) == 1L)
				{
					yield return nodeId;
				}
				nodeId++;
			}
			yield break;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000086DD File Offset: 0x000068DD
		internal AmClusterNode(AmServerName nodeName, IAmCluster owningCluster, AmClusterNodeHandle nodeHandle)
		{
			this.m_disposeTracker = this.GetDisposeTracker();
			this.Name = nodeName;
			this.OwningCluster = owningCluster;
			this.Handle = nodeHandle;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00008706 File Offset: 0x00006906
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000870E File Offset: 0x0000690E
		public AmServerName Name { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00008717 File Offset: 0x00006917
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000871F File Offset: 0x0000691F
		internal IAmCluster OwningCluster { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008728 File Offset: 0x00006928
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00008730 File Offset: 0x00006930
		public AmClusterNodeHandle Handle { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008739 File Offset: 0x00006939
		public AmNodeState State
		{
			get
			{
				return this.GetState(false);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008744 File Offset: 0x00006944
		public string GetNodeIdentifier()
		{
			string text = string.Empty;
			using (AmClusterRawData nodeControlData = this.GetNodeControlData(AmClusterNodeControlCode.CLUSCTL_NODE_GET_ID, 1024U))
			{
				text = nodeControlData.ReadString();
				AmTrace.Debug("GetNodeIdentifier: Node '{0}' is identified by '{1}'.", new object[]
				{
					this.Name,
					text
				});
			}
			return text;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000087AC File Offset: 0x000069AC
		public bool IsNetworkVisible(string networkName)
		{
			AmClusterNetInterface amClusterNetInterface;
			bool result = this.IsNetworkVisible(networkName, out amClusterNetInterface);
			using (amClusterNetInterface)
			{
			}
			return result;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000087E4 File Offset: 0x000069E4
		public bool IsNetworkVisible(string networkStr1, out AmClusterNetInterface networkInterface)
		{
			networkInterface = null;
			foreach (AmClusterNetInterface amClusterNetInterface in this.EnumerateNetInterfaces())
			{
				string networkName = amClusterNetInterface.GetNetworkName();
				if (SharedHelper.StringIEquals(networkStr1, networkName))
				{
					networkInterface = amClusterNetInterface;
					return true;
				}
				amClusterNetInterface.Dispose();
			}
			return false;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008850 File Offset: 0x00006A50
		public override string ToString()
		{
			return string.Format("node:{0}", this.Name.NetbiosName);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008867 File Offset: 0x00006A67
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AmClusterNode>(this);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000886F File Offset: 0x00006A6F
		public void SuppressDisposeTracker()
		{
			if (this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Suppress();
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008884 File Offset: 0x00006A84
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008893 File Offset: 0x00006A93
		public override bool Equals(AmClusterNode x, AmClusterNode y)
		{
			return x.Equals(y);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000889C File Offset: 0x00006A9C
		public override int GetHashCode(AmClusterNode obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000088A4 File Offset: 0x00006AA4
		public override int GetHashCode()
		{
			if (this.Name == null)
			{
				return 0;
			}
			return this.Name.GetHashCode();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000088BB File Offset: 0x00006ABB
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000088C4 File Offset: 0x00006AC4
		public bool Equals(AmClusterNode other)
		{
			return this.Name.Equals(other.Name);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000088D8 File Offset: 0x00006AD8
		public AmNodeState GetState(bool isThrowIfUnknown)
		{
			AmNodeState clusterNodeState = ClusapiMethods.GetClusterNodeState(this.Handle);
			if (clusterNodeState == AmNodeState.Unknown)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				Exception ex = new Win32Exception(lastWin32Error);
				AmTrace.Debug("GetClusterNodeState() returned error (rc={0}, message={1})", new object[]
				{
					lastWin32Error,
					ex
				});
				if (isThrowIfUnknown)
				{
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "GetClusterNodeState()", new object[0]);
				}
			}
			return clusterNodeState;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008945 File Offset: 0x00006B45
		public IEnumerable<AmClusterNetInterface> EnumerateNetInterfaces()
		{
			return from nicName in this.EnumerateNetInterfaceNames()
			select this.OwningCluster.OpenNetInterface(nicName);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000895E File Offset: 0x00006B5E
		internal IEnumerable<string> EnumerateNetInterfaceNames()
		{
			return AmClusterNode.EnumerateObjects(this.Handle, AmClusterNodeEnum.CLUSTER_NODE_ENUM_NETINTERFACES);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000896C File Offset: 0x00006B6C
		public long GetHungNodesMask(out int currentGumId)
		{
			currentGumId = 0;
			long result = 0L;
			using (AmClusterRawData nodeControlData = this.GetNodeControlData(AmClusterNodeControlCode.CLUSCTL_NODE_GET_STUCK_NODES, 1024U))
			{
				IntPtr intPtr = nodeControlData.Buffer;
				currentGumId = Marshal.ReadInt32(intPtr);
				intPtr += Marshal.SizeOf(typeof(int));
				result = Marshal.ReadInt64(intPtr);
			}
			return result;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000089DC File Offset: 0x00006BDC
		protected void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_isDisposed)
				{
					if (disposing)
					{
						if (!this.Handle.IsInvalid)
						{
							this.Handle.Dispose();
							this.Handle = null;
						}
						if (this.m_disposeTracker != null)
						{
							this.m_disposeTracker.Dispose();
							this.m_disposeTracker = null;
						}
					}
					this.m_isDisposed = true;
				}
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008CF0 File Offset: 0x00006EF0
		private static IEnumerable<string> EnumerateObjects(AmClusterNodeHandle handle, AmClusterNodeEnum objectType)
		{
			new List<string>(16);
			using (AmClusNodeEnumHandle enumHandle = ClusapiMethods.ClusterNodeOpenEnum(handle, objectType))
			{
				if (enumHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "ClusterOpenNodeEnum(objecttype={0})", new object[]
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
					AmClusterNodeEnum objectTypeRetrived;
					errorCode = ClusapiMethods.ClusterNodeEnum(enumHandle, entryIndex, out objectTypeRetrived, objectNameBuffer, ref objectNameLen);
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
				throw AmExceptionHelper.ConstructClusterApiException(errorCode, "ClusterNodeEnum()", new object[0]);
				IL_171:;
			}
			yield break;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008D14 File Offset: 0x00006F14
		private AmClusterRawData GetNodeControlData(AmClusterNodeControlCode code, uint initialDataSize = 1024U)
		{
			uint num = initialDataSize;
			AmClusterRawData amClusterRawData = AmClusterRawData.Allocate(num);
			int num2 = ClusapiMethods.ClusterNodeControl(this.Handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			if (num2 == 234)
			{
				amClusterRawData.Dispose();
				amClusterRawData = AmClusterRawData.Allocate(num);
				num2 = ClusapiMethods.ClusterNodeControl(this.Handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			}
			if (num2 != 0)
			{
				amClusterRawData.Dispose();
				throw AmExceptionHelper.ConstructClusterApiException(num2, "ClusterNodeControl(controlcode={0})", new object[]
				{
					code
				});
			}
			return amClusterRawData;
		}

		// Token: 0x0400007B RID: 123
		private DisposeTracker m_disposeTracker;

		// Token: 0x0400007C RID: 124
		private bool m_isDisposed;
	}
}
