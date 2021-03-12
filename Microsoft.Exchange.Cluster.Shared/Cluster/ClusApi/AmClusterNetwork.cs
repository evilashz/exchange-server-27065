using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200002C RID: 44
	internal class AmClusterNetwork : DisposeTrackableBase, IAmClusterNetwork
	{
		// Token: 0x0600019C RID: 412 RVA: 0x000075FE File Offset: 0x000057FE
		internal AmClusterNetwork(string networkName, IAmCluster owningCluster, AmClusterNetworkHandle networkHandle)
		{
			this.Name = networkName;
			this.OwningCluster = owningCluster;
			this.Handle = networkHandle;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000761B File Offset: 0x0000581B
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ClusterEventsTracer;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00007622 File Offset: 0x00005822
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000762A File Offset: 0x0000582A
		internal string Name { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00007633 File Offset: 0x00005833
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000763B File Offset: 0x0000583B
		internal AmClusterNetworkHandle Handle { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007644 File Offset: 0x00005844
		internal AmNetworkState State
		{
			get
			{
				return this.GetState(false);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000764D File Offset: 0x0000584D
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00007655 File Offset: 0x00005855
		private IAmCluster OwningCluster { get; set; }

		// Token: 0x060001A5 RID: 421 RVA: 0x00007660 File Offset: 0x00005860
		public static bool IsIPInNetwork(IPAddress addrCandidate, string addrNetwork, string netMask)
		{
			char[] separator = new char[]
			{
				'.'
			};
			string[] array = addrNetwork.Split(separator);
			string[] array2 = netMask.Split(separator);
			byte[] addressBytes = addrCandidate.GetAddressBytes();
			if (array.Length != 4 || array2.Length != 4 || addressBytes.Length != 4)
			{
				return false;
			}
			for (int i = 0; i < 4; i++)
			{
				byte b = byte.Parse(array2[i]);
				if ((addressBytes[i] & b) != (byte.Parse(array[i]) & b))
				{
					ExTraceGlobals.ClusterTracer.TraceDebug(0L, "IsIPInNetwork(): IP {0} is NOT in network '{1}', netmask={2}. ( bytesCandidate[{3}]={4} & mask={5} ) != ( rgNet[{3}]={6} & mask={5}).", new object[]
					{
						addrCandidate.ToString(),
						addrNetwork,
						netMask,
						i,
						addressBytes[i],
						b,
						array[i]
					});
					return false;
				}
			}
			ExTraceGlobals.ClusterTracer.TraceDebug<string, string, string>(0L, "IsIPInNetwork(): IP {0} is in network '{1}', netmask={2}.", addrCandidate.ToString(), addrNetwork, netMask);
			return true;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007764 File Offset: 0x00005964
		public IEnumerable<string> EnumeratePureAlternateIPv6Names()
		{
			IEnumerable<string> source = this.EnumerateAlternateIPv6Names();
			ExTraceGlobals.ClusterTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Network.GetPureAlternateIPv6Names(): all ipv6 networks on '{0}' are '{1}'.", this.Name, string.Join(",", source.ToArray<string>()));
			return from ipString in source
			where ipString.EndsWith("/64")
			select ipString;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000077C9 File Offset: 0x000059C9
		public string GetAddress()
		{
			return this.GetCommonROProperty<string>("Address");
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000077D6 File Offset: 0x000059D6
		public string GetAddressMask()
		{
			return this.GetCommonROProperty<string>("AddressMask");
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000077E4 File Offset: 0x000059E4
		public AmNetworkRole GetNativeRole()
		{
			AmNetworkRole result;
			try
			{
				result = (AmNetworkRole)this.GetCommonProperty<int>("Role");
			}
			catch (ClusterApiException arg)
			{
				ExTraceGlobals.ClusterTracer.TraceDebug<string, ClusterApiException>((long)this.GetHashCode(), "GetNativeRole({0}) encountered an exception, defaulting to ClusterNetworkRoleNone: {1}", this.Name, arg);
				result = AmNetworkRole.ClusterNetworkRoleNone;
			}
			return result;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007834 File Offset: 0x00005A34
		public void SetPrivateProperty<MyType>(string key, MyType value)
		{
			int num = 0;
			if (typeof(string) == typeof(MyType))
			{
				string value2 = (string)((object)value);
				using (AmClusterPropListDisposable amClusterPropListDisposable = AmClusPropListMaker.CreatePropListString(key, value2, out num))
				{
					this.SetNetworkControlData(AmClusterNetworkControlCode.CLUSCTL_NETWORK_SET_PRIVATE_PROPERTIES, amClusterPropListDisposable.RawBuffer, amClusterPropListDisposable.BufferSize);
					return;
				}
			}
			if (typeof(int) == typeof(MyType))
			{
				int value3 = (int)((object)value);
				using (AmClusterPropListDisposable amClusterPropListDisposable2 = AmClusPropListMaker.CreatePropListInt(key, value3, out num))
				{
					this.SetNetworkControlData(AmClusterNetworkControlCode.CLUSCTL_NETWORK_SET_PRIVATE_PROPERTIES, amClusterPropListDisposable2.RawBuffer, amClusterPropListDisposable2.BufferSize);
					return;
				}
			}
			if (typeof(string[]) == typeof(MyType))
			{
				string[] value4 = (string[])((object)value);
				using (AmClusterPropListDisposable amClusterPropListDisposable3 = AmClusPropListMaker.CreatePropListMultiString(key, value4, out num))
				{
					this.SetNetworkControlData(AmClusterNetworkControlCode.CLUSCTL_NETWORK_SET_PRIVATE_PROPERTIES, amClusterPropListDisposable3.RawBuffer, amClusterPropListDisposable3.BufferSize);
				}
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007978 File Offset: 0x00005B78
		public void SetNativeRole(AmNetworkRole newValue)
		{
			this.SetCommonProperty<int>("Role", (int)newValue);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007988 File Offset: 0x00005B88
		public MyType GetCommonProperty<MyType>(string key)
		{
			MyType result = default(MyType);
			try
			{
				using (AmClusterRawData networkControlData = this.GetNetworkControlData(AmClusterNetworkControlCode.CLUSCTL_NETWORK_GET_COMMON_PROPERTIES))
				{
					AmClusterPropList amClusterPropList = new AmClusterPropList(networkControlData.Buffer, networkControlData.Size);
					result = amClusterPropList.Read<MyType>(key);
				}
			}
			catch (ClusterApiException arg)
			{
				AmClusterNetwork.Tracer.TraceDebug<string, string, ClusterApiException>((long)this.GetHashCode(), "GetCommonProperty( {0} ) on '{1}' encountered an exception: {2}", key, this.Name, arg);
			}
			return result;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007A10 File Offset: 0x00005C10
		public MyType GetCommonROProperty<MyType>(string key)
		{
			MyType result = default(MyType);
			try
			{
				using (AmClusterRawData networkControlData = this.GetNetworkControlData(AmClusterNetworkControlCode.CLUSCTL_NETWORK_GET_RO_COMMON_PROPERTIES))
				{
					AmClusterPropList amClusterPropList = new AmClusterPropList(networkControlData.Buffer, networkControlData.Size);
					result = amClusterPropList.Read<MyType>(key);
				}
			}
			catch (ClusterApiException arg)
			{
				AmClusterNetwork.Tracer.TraceDebug<string, string, ClusterApiException>((long)this.GetHashCode(), "GetCommonROProperty( {0} ) on '{1}' encountered an exception: {2}", key, this.Name, arg);
			}
			return result;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007A98 File Offset: 0x00005C98
		public MyType GetPrivateProperty<MyType>(string key)
		{
			MyType result = default(MyType);
			try
			{
				using (AmClusterRawData networkControlData = this.GetNetworkControlData(AmClusterNetworkControlCode.CLUSCTL_NETWORK_GET_PRIVATE_PROPERTIES))
				{
					AmClusterPropList amClusterPropList = new AmClusterPropList(networkControlData.Buffer, networkControlData.Size);
					result = amClusterPropList.Read<MyType>(key);
				}
			}
			catch (ClusterApiException arg)
			{
				AmClusterNetwork.Tracer.TraceDebug<string, string, ClusterApiException>((long)this.GetHashCode(), "GetPrivateProperty( {0} ) on '{1}' encountered an exception: {2}", key, this.Name, arg);
			}
			return result;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007B20 File Offset: 0x00005D20
		public void SetCommonProperty<MyType>(string key, MyType value)
		{
			int num = 0;
			if (typeof(string) == typeof(MyType))
			{
				string value2 = (string)((object)value);
				using (AmClusterPropListDisposable amClusterPropListDisposable = AmClusPropListMaker.CreatePropListString(key, value2, out num))
				{
					this.SetNetworkControlData(AmClusterNetworkControlCode.CLUSCTL_NETWORK_SET_COMMON_PROPERTIES, amClusterPropListDisposable.RawBuffer, amClusterPropListDisposable.BufferSize);
					return;
				}
			}
			if (typeof(int) == typeof(MyType))
			{
				int value3 = (int)((object)value);
				using (AmClusterPropListDisposable amClusterPropListDisposable2 = AmClusPropListMaker.CreatePropListInt(key, value3, out num))
				{
					this.SetNetworkControlData(AmClusterNetworkControlCode.CLUSCTL_NETWORK_SET_COMMON_PROPERTIES, amClusterPropListDisposable2.RawBuffer, amClusterPropListDisposable2.BufferSize);
					return;
				}
			}
			if (typeof(string[]) == typeof(MyType))
			{
				string[] value4 = (string[])((object)value);
				using (AmClusterPropListDisposable amClusterPropListDisposable3 = AmClusPropListMaker.CreatePropListMultiString(key, value4, out num))
				{
					this.SetNetworkControlData(AmClusterNetworkControlCode.CLUSCTL_NETWORK_SET_COMMON_PROPERTIES, amClusterPropListDisposable3.RawBuffer, amClusterPropListDisposable3.BufferSize);
				}
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007E44 File Offset: 0x00006044
		public IEnumerable<IAmClusterNode> GetVisibleNodes(IEnumerable<IAmClusterNode> nodesToCheck)
		{
			IEnumerable<IAmClusterNode> nodes = null;
			if (nodesToCheck != null)
			{
				nodes = nodesToCheck;
			}
			else
			{
				nodes = this.OwningCluster.EnumerateNodes();
			}
			foreach (IAmClusterNode node in nodes)
			{
				if (node.IsNetworkVisible(this.Name))
				{
					yield return node;
				}
			}
			yield break;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007E68 File Offset: 0x00006068
		public bool SupportsIPv4Dhcp()
		{
			IEnumerable<string> source = this.EnumerateAlternateIPv4Names();
			return source.Count<string>() > 0;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007E88 File Offset: 0x00006088
		public bool SupportsIPv6AutoConfiguration()
		{
			IEnumerable<string> source = this.EnumerateAlternateIPv6Names();
			return source.Count<string>() > 0;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007EA5 File Offset: 0x000060A5
		public bool IsIPInNetwork(IPAddress ip)
		{
			return AmClusterNetwork.IsIPInNetwork(ip, this.GetAddress(), this.GetAddressMask());
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007EBC File Offset: 0x000060BC
		internal AmNetworkState GetState(bool isThrowIfUnknown)
		{
			AmNetworkState clusterNetworkState = ClusapiMethods.GetClusterNetworkState(this.Handle);
			if (clusterNetworkState == AmNetworkState.Unavailable)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				Exception ex = new Win32Exception(lastWin32Error);
				AmTrace.Debug("GetClusterNetworkState() returned error (rc={0}, message={1})", new object[]
				{
					lastWin32Error,
					ex
				});
				if (isThrowIfUnknown)
				{
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "GetClusterNetworkState()", new object[0]);
				}
			}
			return clusterNetworkState;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007F1C File Offset: 0x0000611C
		internal IEnumerable<string> EnumerateAlternateIPv4Names()
		{
			string[] commonROProperty = this.GetCommonROProperty<string[]>("IPv4Addresses");
			string[] commonROProperty2 = this.GetCommonROProperty<string[]>("IPv4PrefixLengths");
			return this.FormSubnetIds(commonROProperty, commonROProperty2, "EnumerateAlternateIPv4Names");
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007F50 File Offset: 0x00006150
		internal IEnumerable<string> EnumerateAlternateIPv6Names()
		{
			string[] commonROProperty = this.GetCommonROProperty<string[]>("IPv6Addresses");
			string[] commonROProperty2 = this.GetCommonROProperty<string[]>("IPv6PrefixLengths");
			return this.FormSubnetIds(commonROProperty, commonROProperty2, "EnumerateAlternateIPv6Names");
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007F84 File Offset: 0x00006184
		private IEnumerable<string> FormSubnetIds(string[] addressParts, string[] prefixParts, string callerName)
		{
			List<string> list = new List<string>(16);
			if (addressParts == null || prefixParts == null)
			{
				string text = string.Format("{0} retrieved a null RO property.", callerName);
				AmClusterNetwork.Tracer.TraceError((long)this.GetHashCode(), text);
				ReplayCrimsonEvents.NetworkDiscoveryInconsistent.LogPeriodic<string>(callerName, SharedDiag.DefaultEventSuppressionInterval, text);
				return list;
			}
			if (addressParts.Length != prefixParts.Length)
			{
				string text2 = string.Format("{0} found addressParts.Length = {1} and prefixParts.Length = {2}.", callerName, addressParts.Length, prefixParts.Length);
				AmClusterNetwork.Tracer.TraceError((long)this.GetHashCode(), text2);
				ReplayCrimsonEvents.NetworkDiscoveryInconsistent.LogPeriodic<string>(callerName, SharedDiag.DefaultEventSuppressionInterval, text2);
				return new List<string>(0);
			}
			int num = 0;
			while (num < addressParts.Length && num < prefixParts.Length)
			{
				if (!string.IsNullOrEmpty(addressParts[num]) && !string.IsNullOrEmpty(prefixParts[num]))
				{
					list.Add(addressParts[num] + "/" + prefixParts[num]);
				}
				num++;
			}
			return list;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000805F File Offset: 0x0000625F
		internal IEnumerable<string> EnumerateNetworkInterfaceNames()
		{
			return AmClusterNetwork.EnumerateObjects(this.Handle, AmClusterNetworkEnum.CLUSTER_NETWORK_ENUM_NETINTERFACES);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000806D File Offset: 0x0000626D
		internal IEnumerable<AmClusterNetInterface> EnumerateNetworkInterfaces()
		{
			return AmCluster.EvaluateAllElements<AmClusterNetInterface>(this.LazyEnumerateNetworkInterfaces());
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000807A File Offset: 0x0000627A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AmClusterNetwork>(this);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008084 File Offset: 0x00006284
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

		// Token: 0x060001BC RID: 444 RVA: 0x00008370 File Offset: 0x00006570
		private static IEnumerable<string> EnumerateObjects(AmClusterNetworkHandle handle, AmClusterNetworkEnum objectType)
		{
			new List<string>(16);
			using (AmClusNetworkEnumHandle enumHandle = ClusapiMethods.ClusterNetworkOpenEnum(handle, objectType))
			{
				if (enumHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "ClusterOpenNetworkEnum(objecttype={0})", new object[]
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
					AmClusterNetworkEnum objectTypeRetrived;
					errorCode = ClusapiMethods.ClusterNetworkEnum(enumHandle, entryIndex, out objectTypeRetrived, objectNameBuffer, ref objectNameLen);
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
				throw AmExceptionHelper.ConstructClusterApiException(errorCode, "ClusterNetworkEnum()", new object[0]);
				IL_171:;
			}
			yield break;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008394 File Offset: 0x00006594
		private AmClusterRawData GetNetworkControlData(AmClusterNetworkControlCode code)
		{
			uint num = 1024U;
			AmClusterRawData amClusterRawData = AmClusterRawData.Allocate(num);
			int num2 = ClusapiMethods.ClusterNetworkControl(this.Handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			if (num2 == 234)
			{
				amClusterRawData.Dispose();
				amClusterRawData = AmClusterRawData.Allocate(num);
				num2 = ClusapiMethods.ClusterNetworkControl(this.Handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			}
			if (num2 != 0)
			{
				amClusterRawData.Dispose();
				throw AmExceptionHelper.ConstructClusterApiException(num2, "ClusterNetworkControl({0},controlcode={1})", new object[]
				{
					this.Name,
					code
				});
			}
			return amClusterRawData;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008438 File Offset: 0x00006638
		private void SetNetworkControlData(AmClusterNetworkControlCode code, IntPtr buffer, uint bufferSize)
		{
			uint num = 0U;
			int num2 = ClusapiMethods.ClusterNetworkControl(this.Handle, IntPtr.Zero, code, buffer, bufferSize, IntPtr.Zero, 0U, out num);
			if (num2 != 0)
			{
				ClusterApiException ex = AmExceptionHelper.ConstructClusterApiException(num2, "ClusterNetworkControl(controlcode={0})", new object[]
				{
					code
				});
				AmClusterNetwork.Tracer.TraceDebug((long)this.GetHashCode(), ex.Message);
				throw ex;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000084AA File Offset: 0x000066AA
		private IEnumerable<AmClusterNetInterface> LazyEnumerateNetworkInterfaces()
		{
			return from nicName in this.EnumerateNetworkInterfaceNames()
			select this.OwningCluster.OpenNetInterface(nicName);
		}

		// Token: 0x0400006D RID: 109
		public const string CLUSREG_NAME_NET_NAME = "Name";

		// Token: 0x0400006E RID: 110
		public const string CLUSREG_NAME_NET_IPV6_ADDRESSES = "IPv6Addresses";

		// Token: 0x0400006F RID: 111
		public const string CLUSREG_NAME_NET_IPV6_PREFIXLENGTHS = "IPv6PrefixLengths";

		// Token: 0x04000070 RID: 112
		public const string CLUSREG_NAME_NET_IPV4_ADDRESSES = "IPv4Addresses";

		// Token: 0x04000071 RID: 113
		public const string CLUSREG_NAME_NET_IPV4_PREFIXLENGTHS = "IPv4PrefixLengths";

		// Token: 0x04000072 RID: 114
		public const string CLUSREG_NAME_NET_ADDRESS = "Address";

		// Token: 0x04000073 RID: 115
		public const string CLUSREG_NAME_NET_ADDRESS_MASK = "AddressMask";

		// Token: 0x04000074 RID: 116
		public const string CLUSREG_NAME_NET_DESC = "Description";

		// Token: 0x04000075 RID: 117
		public const string CLUSREG_NAME_NET_ROLE = "Role";
	}
}
