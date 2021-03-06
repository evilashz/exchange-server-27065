using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000031 RID: 49
	internal class AmClusterNotify : DisposeTrackableBase
	{
		// Token: 0x060001FA RID: 506 RVA: 0x0000909A File Offset: 0x0000729A
		internal AmClusterNotify(AmClusterHandle hCluster)
		{
			this.m_hCluster = hCluster;
			this.m_hChange = null;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000090B0 File Offset: 0x000072B0
		internal void Initialize(ClusterNotifyFlags eventMask, IntPtr context)
		{
			if (this.m_hChange == null || this.m_hChange.IsInvalid)
			{
				AmTrace.Debug("Creating cluster notification port", new object[0]);
				this.m_hChange = ClusapiMethods.CreateClusterNotifyPort(AmClusterNotifyHandle.InvalidHandle, this.m_hCluster, eventMask, context);
				if (this.m_hChange == null || this.m_hChange.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					AmTrace.Error("CreateClusterNotifyPort failed. Error code 0x{0:X8}", new object[]
					{
						lastWin32Error
					});
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "CreateClusterNotifyPort()", new object[0]);
				}
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009144 File Offset: 0x00007344
		internal void RegisterObject(SafeHandle clusObject, ClusterNotifyFlags eventMask, IntPtr context)
		{
			AmTrace.Debug("Registering additional cluster objects for notification", new object[0]);
			int num = ClusapiMethods.RegisterClusterNotify(this.m_hChange, eventMask, clusObject, context);
			if (num != 0)
			{
				AmTrace.Error("RegisterClusterNotify for group state returned error 0x{0:X8}", new object[]
				{
					num
				});
				throw AmExceptionHelper.ConstructClusterApiException(num, "RegisterClusterNotify(CLUSTER_CHANGE_GROUP_STATE)", new object[0]);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000091A0 File Offset: 0x000073A0
		internal bool WaitForEvent(out AmClusterEventInfo eventInfo, TimeSpan timeout)
		{
			ClusterNotifyFlags eventCode = ~(ClusterNotifyFlags.CLUSTER_CHANGE_NODE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_NAME | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_ATTRIBUTES | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_VALUE | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_SUBTREE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_RECONNECT | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_QUORUM_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_HANDLE_CLOSE);
			StringBuilder stringBuilder = new StringBuilder(1024);
			uint num = Convert.ToUInt32(stringBuilder.Capacity);
			IntPtr zero = IntPtr.Zero;
			int num2 = 0;
			try
			{
				num2 = ClusapiMethods.GetClusterNotify(this.m_hChange, out zero, out eventCode, stringBuilder, ref num, Convert.ToUInt32(timeout.TotalMilliseconds));
			}
			catch (AccessViolationException innerException)
			{
				throw new ClusterApiException("GetClusterNotify", innerException);
			}
			if (num2 == 258)
			{
				eventInfo = null;
				return false;
			}
			if (num2 == 0)
			{
				eventInfo = new AmClusterEventInfo(stringBuilder.ToString(), eventCode, zero);
				ExTraceGlobals.ClusterEventsTracer.TraceDebug<AmClusterEventInfo>((long)this.GetHashCode(), "WaitForEvent returns: {0}", eventInfo);
				return true;
			}
			AmTrace.Error("GetClusterNotify returned error 0x{0:X8}", new object[]
			{
				num2
			});
			if (this.m_isClosed && num2 == 6)
			{
				AmTrace.Error("Cluster notification port handle is closed", new object[0]);
				eventInfo = null;
				return false;
			}
			throw AmExceptionHelper.ConstructClusterApiException(num2, "GetClusterNotify()", new object[0]);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000092A0 File Offset: 0x000074A0
		internal void ForceClose()
		{
			lock (this)
			{
				if (this.m_hChange != null)
				{
					this.m_hChange.DangerousCloseHandle();
					this.m_isClosed = true;
				}
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000092F0 File Offset: 0x000074F0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AmClusterNotify>(this);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000092F8 File Offset: 0x000074F8
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				lock (this)
				{
					if (disposing)
					{
						try
						{
							this.m_hChange.Dispose();
						}
						catch (ObjectDisposedException ex)
						{
							AmTrace.Error("Ignoring object disposed exception while disposing cluster notify handle (error={0})", new object[]
							{
								ex
							});
						}
					}
					this.m_hChange = null;
				}
			}
		}

		// Token: 0x04000084 RID: 132
		private const int MaxChangedObjectNameLen = 1024;

		// Token: 0x04000085 RID: 133
		private bool m_isClosed;

		// Token: 0x04000086 RID: 134
		private AmClusterNotifyHandle m_hChange;

		// Token: 0x04000087 RID: 135
		private AmClusterHandle m_hCluster;
	}
}
