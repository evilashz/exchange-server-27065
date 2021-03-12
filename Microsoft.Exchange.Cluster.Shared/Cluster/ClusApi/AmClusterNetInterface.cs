using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200002A RID: 42
	internal class AmClusterNetInterface : DisposeTrackableBase
	{
		// Token: 0x0600018B RID: 395 RVA: 0x0000733A File Offset: 0x0000553A
		internal AmClusterNetInterface(string netInterfaceName, AmClusterNetInterfaceHandle netInterfaceHandle)
		{
			this.Name = netInterfaceName;
			this.Handle = netInterfaceHandle;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007350 File Offset: 0x00005550
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ClusterEventsTracer;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00007357 File Offset: 0x00005557
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000735F File Offset: 0x0000555F
		internal string Name { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00007368 File Offset: 0x00005568
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00007370 File Offset: 0x00005570
		internal AmClusterNetInterfaceHandle Handle { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00007379 File Offset: 0x00005579
		internal AmNetInterfaceState State
		{
			get
			{
				return this.GetState(false);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007384 File Offset: 0x00005584
		public string GetNodeName()
		{
			string text = string.Empty;
			using (AmClusterRawData netInterfaceControlData = this.GetNetInterfaceControlData(AmClusterNetInterfaceControlCode.CLUSCTL_NETINTERFACE_GET_NODE))
			{
				text = netInterfaceControlData.ReadString();
				AmTrace.Debug("GetNetInterfaceIdentifier: NetInterface '{0}' is owned by '{1}'.", new object[]
				{
					this.Name,
					text
				});
			}
			return text;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000073E8 File Offset: 0x000055E8
		public string GetAddress()
		{
			return this.GetCommonProperty<string>("Address");
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000073F5 File Offset: 0x000055F5
		public string[] GetIPv6Addresses()
		{
			return this.GetCommonProperty<string[]>("IPv6Addresses");
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007402 File Offset: 0x00005602
		public string GetNetworkName()
		{
			return this.GetCommonProperty<string>("Network");
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007410 File Offset: 0x00005610
		public MyType GetCommonProperty<MyType>(string key)
		{
			MyType result = default(MyType);
			try
			{
				using (AmClusterRawData netInterfaceControlData = this.GetNetInterfaceControlData(AmClusterNetInterfaceControlCode.CLUSCTL_NETINTERFACE_GET_RO_COMMON_PROPERTIES))
				{
					AmClusterPropList amClusterPropList = new AmClusterPropList(netInterfaceControlData.Buffer, netInterfaceControlData.Size);
					result = amClusterPropList.Read<MyType>(key);
				}
			}
			catch (ClusterApiException arg)
			{
				AmClusterNetInterface.Tracer.TraceDebug<string, string, ClusterApiException>((long)this.GetHashCode(), "GetCommonProperty( {0} ) on '{1}' encountered an exception: {2}", key, this.Name, arg);
			}
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007498 File Offset: 0x00005698
		internal static bool IsNicUsable(AmNetInterfaceState state)
		{
			return state == AmNetInterfaceState.Up || state == AmNetInterfaceState.Unreachable;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000074A4 File Offset: 0x000056A4
		internal AmNetInterfaceState GetState(bool isThrowIfUnknown)
		{
			AmNetInterfaceState clusterNetInterfaceState = ClusapiMethods.GetClusterNetInterfaceState(this.Handle);
			if (clusterNetInterfaceState == AmNetInterfaceState.Unknown)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				Exception ex = new Win32Exception(lastWin32Error);
				AmTrace.Debug("GetClusterNetInterfaceState() returned error (rc={0}, message={1})", new object[]
				{
					lastWin32Error,
					ex
				});
				if (isThrowIfUnknown)
				{
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "GetClusterNetInterfaceState()", new object[0]);
				}
			}
			return clusterNetInterfaceState;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007503 File Offset: 0x00005703
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AmClusterNetInterface>(this);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000750C File Offset: 0x0000570C
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				if (disposing && !this.Handle.IsInvalid)
				{
					this.Handle.Dispose();
					this.Handle = null;
				}
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007564 File Offset: 0x00005764
		private AmClusterRawData GetNetInterfaceControlData(AmClusterNetInterfaceControlCode code)
		{
			uint num = 1024U;
			AmClusterRawData amClusterRawData = AmClusterRawData.Allocate(num);
			int num2 = ClusapiMethods.ClusterNetInterfaceControl(this.Handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			if (num2 == 234)
			{
				amClusterRawData.Dispose();
				amClusterRawData = AmClusterRawData.Allocate(num);
				num2 = ClusapiMethods.ClusterNetInterfaceControl(this.Handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			}
			if (num2 != 0)
			{
				amClusterRawData.Dispose();
				throw AmExceptionHelper.ConstructClusterApiException(num2, "ClusterNetInterfaceControl(controlcode={0})", new object[]
				{
					code
				});
			}
			return amClusterRawData;
		}

		// Token: 0x0400005E RID: 94
		public const string CLUSREG_NAME_NETIFACE_NAME = "Name";

		// Token: 0x0400005F RID: 95
		public const string CLUSREG_NAME_NETIFACE_NODE = "Node";

		// Token: 0x04000060 RID: 96
		public const string CLUSREG_NAME_NETIFACE_NETWORK = "Network";

		// Token: 0x04000061 RID: 97
		public const string CLUSREG_NAME_NETIFACE_ADAPTER_NAME = "Adapter";

		// Token: 0x04000062 RID: 98
		public const string CLUSREG_NAME_NETIFACE_ADAPTER_ID = "AdapterId";

		// Token: 0x04000063 RID: 99
		public const string CLUSREG_NAME_NETIFACE_DHCP_ENABLED = "DhcpEnabled";

		// Token: 0x04000064 RID: 100
		public const string CLUSREG_NAME_NETIFACE_IPV6_ADDRESSES = "IPv6Addresses";

		// Token: 0x04000065 RID: 101
		public const string CLUSREG_NAME_NETIFACE_IPV4_ADDRESSES = "IPv4Addresses";

		// Token: 0x04000066 RID: 102
		public const string CLUSREG_NAME_NETIFACE_ADDRESS = "Address";

		// Token: 0x04000067 RID: 103
		public const string CLUSREG_NAME_NETIFACE_DESC = "Description";
	}
}
