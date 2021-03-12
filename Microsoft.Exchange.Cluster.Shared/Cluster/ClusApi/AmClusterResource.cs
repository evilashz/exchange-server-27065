using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000037 RID: 55
	internal class AmClusterResource : DisposeTrackableBase, IAmClusterResource, IDisposable
	{
		// Token: 0x06000227 RID: 551 RVA: 0x0000A120 File Offset: 0x00008320
		internal AmClusterResource(string resourceName, IAmCluster owningCluster, AmClusterResourceHandle resourceHandle)
		{
			this.m_name = resourceName;
			this.OwningCluster = owningCluster;
			this.Handle = resourceHandle;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000A13D File Offset: 0x0000833D
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ClusterEventsTracer;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A144 File Offset: 0x00008344
		public string Name
		{
			get
			{
				if (this.m_name == null)
				{
					this.m_name = this.GetCommonProperty<string>("Name");
				}
				return this.m_name;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000A165 File Offset: 0x00008365
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000A16D File Offset: 0x0000836D
		internal IAmCluster OwningCluster { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000A176 File Offset: 0x00008376
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000A17E File Offset: 0x0000837E
		public AmClusterResourceHandle Handle
		{
			get
			{
				return this.m_handle;
			}
			private set
			{
				this.m_handle = value;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A188 File Offset: 0x00008388
		public MyType GetCommonProperty<MyType>(string key)
		{
			MyType result = default(MyType);
			try
			{
				using (AmClusterRawData resourceControlData = this.GetResourceControlData(AmClusterResourceControlCode.CLUSCTL_RESOURCE_GET_COMMON_PROPERTIES))
				{
					AmClusterPropList amClusterPropList = new AmClusterPropList(resourceControlData.Buffer, resourceControlData.Size);
					result = amClusterPropList.Read<MyType>(key);
				}
			}
			catch (ClusterApiException arg)
			{
				AmClusterResource.Tracer.TraceDebug<string, string, ClusterApiException>((long)this.GetHashCode(), "GetCommonProperty( {0} ) on '{1}' encountered an exception: {2}", key, this.Name, arg);
			}
			return result;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A210 File Offset: 0x00008410
		public MyType GetCommonROProperty<MyType>(string key)
		{
			MyType result = default(MyType);
			try
			{
				using (AmClusterRawData resourceControlData = this.GetResourceControlData(AmClusterResourceControlCode.CLUSCTL_RESOURCE_GET_RO_COMMON_PROPERTIES))
				{
					AmClusterPropList amClusterPropList = new AmClusterPropList(resourceControlData.Buffer, resourceControlData.Size);
					result = amClusterPropList.Read<MyType>(key);
				}
			}
			catch (ClusterApiException arg)
			{
				AmClusterResource.Tracer.TraceDebug<string, string, ClusterApiException>((long)this.GetHashCode(), "GetCommonROProperty( {0} ) on '{1}' encountered an exception: {2}", key, this.Name, arg);
			}
			return result;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A298 File Offset: 0x00008498
		public MyType GetPrivateProperty<MyType>(string key)
		{
			MyType result = default(MyType);
			try
			{
				using (AmClusterRawData resourceControlData = this.GetResourceControlData(AmClusterResourceControlCode.CLUSCTL_RESOURCE_GET_PRIVATE_PROPERTIES))
				{
					AmClusterPropList amClusterPropList = new AmClusterPropList(resourceControlData.Buffer, resourceControlData.Size);
					result = amClusterPropList.Read<MyType>(key);
				}
			}
			catch (ClusterApiException arg)
			{
				AmClusterResource.Tracer.TraceDebug<string, string, ClusterApiException>((long)this.GetHashCode(), "GetPrivateProperty( {0} ) on '{1}' encountered an exception: {2}", key, this.Name, arg);
			}
			return result;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A320 File Offset: 0x00008520
		public void SetPrivateProperty<MyType>(string key, MyType value)
		{
			int num = 0;
			if (typeof(string) == typeof(MyType))
			{
				string value2 = (string)((object)value);
				using (AmClusterPropListDisposable amClusterPropListDisposable = AmClusPropListMaker.CreatePropListString(key, value2, out num))
				{
					this.SetResourceControlData(AmClusterResourceControlCode.CLUSCTL_RESOURCE_SET_PRIVATE_PROPERTIES, amClusterPropListDisposable.RawBuffer, amClusterPropListDisposable.BufferSize);
					return;
				}
			}
			if (typeof(int) == typeof(MyType))
			{
				int value3 = (int)((object)value);
				using (AmClusterPropListDisposable amClusterPropListDisposable2 = AmClusPropListMaker.CreatePropListInt(key, value3, out num))
				{
					this.SetResourceControlData(AmClusterResourceControlCode.CLUSCTL_RESOURCE_SET_PRIVATE_PROPERTIES, amClusterPropListDisposable2.RawBuffer, amClusterPropListDisposable2.BufferSize);
					return;
				}
			}
			if (typeof(string[]) == typeof(MyType))
			{
				string[] value4 = (string[])((object)value);
				using (AmClusterPropListDisposable amClusterPropListDisposable3 = AmClusPropListMaker.CreatePropListMultiString(key, value4, out num))
				{
					this.SetResourceControlData(AmClusterResourceControlCode.CLUSCTL_RESOURCE_SET_PRIVATE_PROPERTIES, amClusterPropListDisposable3.RawBuffer, amClusterPropListDisposable3.BufferSize);
				}
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A464 File Offset: 0x00008664
		public void SetPrivatePropertyList(AmClusterPropList propList)
		{
			this.SetResourceControlData(AmClusterResourceControlCode.CLUSCTL_RESOURCE_SET_PRIVATE_PROPERTIES, propList.RawBuffer, propList.BufferSize);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A480 File Offset: 0x00008680
		public AmResourceState GetState()
		{
			uint num = 0U;
			uint num2 = 0U;
			AmResourceState clusterResourceState = ClusapiMethods.GetClusterResourceState(this.Handle, null, ref num, null, ref num2);
			if (clusterResourceState == AmResourceState.Unknown)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "GetClusterResourceState({0})", new object[]
				{
					this.Name
				});
			}
			return clusterResourceState;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A4CE File Offset: 0x000086CE
		public string GetTypeName()
		{
			return this.GetCommonProperty<string>("Type");
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A4DB File Offset: 0x000086DB
		public bool IsIpv4()
		{
			return SharedHelper.StringIEquals(this.GetTypeName(), "IP Address");
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A4ED File Offset: 0x000086ED
		public bool IsIpv6()
		{
			return SharedHelper.StringIEquals(this.GetTypeName(), "IPv6 Address");
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A4FF File Offset: 0x000086FF
		public override string ToString()
		{
			return string.Format("resource:{0}", this.Name);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A514 File Offset: 0x00008714
		public uint OnlineResource()
		{
			uint num = ClusapiMethods.OnlineClusterResource(this.Handle);
			if (num != 0U)
			{
				ExTraceGlobals.ClusterTracer.TraceDebug<string, uint>((long)this.GetHashCode(), "OnlineClusterResource( '{0}' ) failed with 0x{1:x}", this.Name, num);
			}
			return num;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A550 File Offset: 0x00008750
		public uint OfflineResource()
		{
			uint num = ClusapiMethods.OfflineClusterResource(this.Handle);
			if (num != 0U)
			{
				ExTraceGlobals.ClusterTracer.TraceDebug<string, uint>((long)this.GetHashCode(), "OfflineClusterResource( '{0}' ) failed with 0x{1:x}", this.Name, num);
			}
			return num;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A58C File Offset: 0x0000878C
		public void DeleteResource()
		{
			string name = this.Name;
			uint num = ClusapiMethods.DeleteClusterResource(this.Handle);
			if (num != 0U)
			{
				throw AmExceptionHelper.ConstructClusterApiException((int)num, "DeleteClusterResource()", new object[0]);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000A5C4 File Offset: 0x000087C4
		public void RemoveDependency(AmClusterResource childDependency)
		{
			string name = this.Name;
			uint num = ClusapiMethods.RemoveClusterResourceDependency(this.Handle, childDependency.Handle);
			if (num == 0U)
			{
				return;
			}
			int num2 = (int)num;
			if (num == 5002U)
			{
				AmClusterResource.Tracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "RemoveClusterResourceDependency( parent={0}, child={1}) returned a non-fatal error 0x{2:x}", this.Name, childDependency.Name, num2);
				return;
			}
			throw AmExceptionHelper.ConstructClusterApiException(num2, "RemoveClusterResourceDependency( parent={0}, child={1})", new object[]
			{
				this.Name,
				childDependency.Name
			});
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A644 File Offset: 0x00008844
		public uint SetDependencyExpression(string expression)
		{
			uint num = ClusapiMethods.SetClusterResourceDependencyExpression(this.Handle, expression);
			if (num != 0U)
			{
				throw AmExceptionHelper.ConstructClusterApiException((int)num, "SetClusterResourceDependencyExpression({0})", new object[]
				{
					expression
				});
			}
			return num;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A67C File Offset: 0x0000887C
		internal IEnumerable<string> EnumeratePossibleOwnerNames()
		{
			return AmClusterResource.EnumerateObjects(this.Handle, AmClusterResourceEnum.CLUSTER_RESOURCE_ENUM_NODES);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A68A File Offset: 0x0000888A
		internal IEnumerable<IAmClusterNode> EnumeratePossibleOwnerNodes()
		{
			return AmCluster.EvaluateAllElements<IAmClusterNode>(this.LazyEnumeratePossibleOwnerNodes());
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000A697 File Offset: 0x00008897
		internal IEnumerable<string> EnumerateDependentNames()
		{
			return AmClusterResource.EnumerateObjects(this.Handle, AmClusterResourceEnum.CLUSTER_RESOURCE_ENUM_DEPENDS);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A6A5 File Offset: 0x000088A5
		internal IEnumerable<AmClusterResource> EnumerateDependents()
		{
			return AmCluster.EvaluateAllElements<AmClusterResource>(this.LazyEnumerateDependents());
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A6B4 File Offset: 0x000088B4
		public void SetAllPossibleOwnerNodes()
		{
			IEnumerable<IAmClusterNode> enumerable = null;
			ClusterApiException ex = null;
			try
			{
				enumerable = this.OwningCluster.EnumerateNodes();
				foreach (IAmClusterNode amClusterNode in enumerable)
				{
					uint num = ClusapiMethods.AddClusterResourceNode(this.Handle, amClusterNode.Handle);
					if (num != 0U && num != 5010U)
					{
						ex = AmExceptionHelper.ConstructClusterApiException((int)num, "AddClusterResourceNode( resource={0}, node={1} )", new object[]
						{
							this.Name,
							amClusterNode.Name
						});
					}
				}
			}
			finally
			{
				if (enumerable != null)
				{
					foreach (IAmClusterNode amClusterNode2 in enumerable)
					{
						amClusterNode2.Dispose();
					}
				}
				if (ex != null)
				{
					throw ex;
				}
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000A7A8 File Offset: 0x000089A8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AmClusterResource>(this);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000A7B0 File Offset: 0x000089B0
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				if (disposing && this.Handle != null && !this.Handle.IsInvalid)
				{
					this.Handle.Dispose();
					this.Handle = null;
				}
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		private static IEnumerable<string> EnumerateObjects(AmClusterResourceHandle handle, AmClusterResourceEnum objectType)
		{
			new List<string>(16);
			using (AmClusResourceEnumHandle enumHandle = ClusapiMethods.ClusterResourceOpenEnum(handle, objectType))
			{
				if (enumHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw AmExceptionHelper.ConstructClusterApiException(lastWin32Error, "ClusterOpenResourceEnum(objecttype={0})", new object[]
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
					AmClusterResourceEnum objectTypeRetrived;
					errorCode = ClusapiMethods.ClusterResourceEnum(enumHandle, entryIndex, out objectTypeRetrived, objectNameBuffer, ref objectNameLen);
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
				throw AmExceptionHelper.ConstructClusterApiException(errorCode, "ClusterResourceEnum()", new object[0]);
				IL_171:;
			}
			yield break;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		private AmClusterRawData GetResourceControlData(AmClusterResourceControlCode code)
		{
			uint num = 1024U;
			AmClusterRawData amClusterRawData = AmClusterRawData.Allocate(num);
			int num2 = ClusapiMethods.ClusterResourceControl(this.Handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			if (num2 == 234)
			{
				amClusterRawData.Dispose();
				amClusterRawData = AmClusterRawData.Allocate(num);
				num2 = ClusapiMethods.ClusterResourceControl(this.Handle, IntPtr.Zero, code, IntPtr.Zero, 0U, amClusterRawData.Buffer, num, out num);
			}
			if (num2 != 0)
			{
				amClusterRawData.Dispose();
				throw AmExceptionHelper.ConstructClusterApiException(num2, "ClusterResourceControl(controlcode={0})", new object[]
				{
					code
				});
			}
			return amClusterRawData;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000AB64 File Offset: 0x00008D64
		private void SetResourceControlData(AmClusterResourceControlCode code, IntPtr buffer, uint bufferSize)
		{
			uint num = 0U;
			int num2 = ClusapiMethods.ClusterResourceControl(this.Handle, IntPtr.Zero, code, buffer, bufferSize, IntPtr.Zero, 0U, out num);
			if (num2 != 0)
			{
				ClusterApiException ex = AmExceptionHelper.ConstructClusterApiException(num2, "ClusterResourceControl(controlcode={0})", new object[]
				{
					code
				});
				AmClusterResource.Tracer.TraceDebug((long)this.GetHashCode(), ex.Message);
				if ((long)num2 != 5024L)
				{
					throw ex;
				}
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000ABE5 File Offset: 0x00008DE5
		private IEnumerable<IAmClusterNode> LazyEnumeratePossibleOwnerNodes()
		{
			return from nodeName in this.EnumeratePossibleOwnerNames()
			select this.OwningCluster.OpenNode(new AmServerName(nodeName));
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AC0C File Offset: 0x00008E0C
		private IEnumerable<AmClusterResource> LazyEnumerateDependents()
		{
			return from resourceName in this.EnumerateDependentNames()
			select this.OwningCluster.OpenResource(resourceName);
		}

		// Token: 0x040000A2 RID: 162
		public const string CLUS_RESTYPE_NAME_IPADDR = "IP Address";

		// Token: 0x040000A3 RID: 163
		public const string CLUS_RESTYPE_NAME_NETNAME = "Network Name";

		// Token: 0x040000A4 RID: 164
		public const string CLUS_RESTYPE_NAME_FSWITNESS = "File Share Witness";

		// Token: 0x040000A5 RID: 165
		public const string CLUS_RESTYPE_NAME_IPV6_NATIVE = "IPv6 Address";

		// Token: 0x040000A6 RID: 166
		public const string CLUSREG_NAME_RES_NAME = "Name";

		// Token: 0x040000A7 RID: 167
		public const string CLUSREG_NAME_RES_TYPE = "Type";

		// Token: 0x040000A8 RID: 168
		public const string CLUSREG_NAME_IPADDR_ENABLE_DHCP = "EnableDhcp";

		// Token: 0x040000A9 RID: 169
		public const string CLUSREG_NAME_IPADDR_SUBNET_MASK = "SubnetMask";

		// Token: 0x040000AA RID: 170
		public const string CLUSREG_NAME_IPADDR_ADDRESS = "Address";

		// Token: 0x040000AB RID: 171
		public const string CLUSREG_NAME_IPADDR_NETWORK = "Network";

		// Token: 0x040000AC RID: 172
		public const string CLUSREG_NAME_NETNAME_CREATING_DC = "CreatingDC";

		// Token: 0x040000AD RID: 173
		public const string CLUSREG_NAME_NETNAME_REQUIRE_DNS = "RequireDNS";

		// Token: 0x040000AE RID: 174
		public const string CLUSREG_NAME_NETNAME_STATUS_DNS = "StatusDNS";

		// Token: 0x040000AF RID: 175
		public const string CLUSREG_NAME_NETNAME_HOST_TTL = "HostRecordTTL";

		// Token: 0x040000B0 RID: 176
		public const string CLUSREG_NAME_NETNAME_REQUIRE_KERBEROS = "RequireKerberos";

		// Token: 0x040000B1 RID: 177
		public const string CLUSREG_NAME_NETNAME_NAME = "Name";

		// Token: 0x040000B2 RID: 178
		public const string CLUSREG_NAME_FSW_SHAREPATH = "SharePath";

		// Token: 0x040000B3 RID: 179
		public const string CLUSREG_NAME_MNS_FILESHARE = "MNSFileShare";

		// Token: 0x040000B4 RID: 180
		public const string CLUSREG_NAME_NET_NAME = "Name";

		// Token: 0x040000B5 RID: 181
		public const string CLUSREG_NAME_NET_IPV6_ADDRESSES = "IPv6Addresses";

		// Token: 0x040000B6 RID: 182
		public const string CLUSREG_NAME_NET_IPV6_PREFIXLENGTHS = "IPv6PrefixLengths";

		// Token: 0x040000B7 RID: 183
		public const string CLUSREG_NAME_NET_IPV4_ADDRESSES = "IPv4Addresses";

		// Token: 0x040000B8 RID: 184
		public const string CLUSREG_NAME_NET_IPV4_PREFIXLENGTHS = "IPv4PrefixLengths";

		// Token: 0x040000B9 RID: 185
		public const string CLUSREG_NAME_NET_ADDRESS = "Address";

		// Token: 0x040000BA RID: 186
		public const string CLUSREG_NAME_NET_ADDRESS_MASK = "AddressMask";

		// Token: 0x040000BB RID: 187
		public const string CLUSREG_NAME_NET_DESC = "Description";

		// Token: 0x040000BC RID: 188
		public const string CLUSREG_NAME_NET_ROLE = "Role";

		// Token: 0x040000BD RID: 189
		private string m_name;

		// Token: 0x040000BE RID: 190
		private AmClusterResourceHandle m_handle;
	}
}
