using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000030 RID: 48
	internal class AmClusterEventInfo
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00008DAA File Offset: 0x00006FAA
		internal AmClusterEventInfo(string changedObjectName, ClusterNotifyFlags eventCode, IntPtr context)
		{
			this.ObjectName = changedObjectName;
			this.EventCode = eventCode;
			this.Context = context;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008DC7 File Offset: 0x00006FC7
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00008DCF File Offset: 0x00006FCF
		internal string ObjectName { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008DD8 File Offset: 0x00006FD8
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00008DE0 File Offset: 0x00006FE0
		internal ClusterNotifyFlags EventCode { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008DE9 File Offset: 0x00006FE9
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00008DF1 File Offset: 0x00006FF1
		internal IntPtr Context { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00008DFA File Offset: 0x00006FFA
		internal bool IsNotifyHandleClosed
		{
			get
			{
				return this.IsEventTriggered(~(ClusterNotifyFlags.CLUSTER_CHANGE_NODE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_NAME | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_ATTRIBUTES | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_VALUE | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_SUBTREE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_RECONNECT | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_QUORUM_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_PROPERTY));
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00008E07 File Offset: 0x00007007
		internal bool IsGroupStateChanged
		{
			get
			{
				return this.IsEventTriggered(ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_STATE);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00008E14 File Offset: 0x00007014
		internal bool IsClusterStateChanged
		{
			get
			{
				return this.IsEventTriggered(ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_STATE);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008E21 File Offset: 0x00007021
		internal bool IsNodeStateChanged
		{
			get
			{
				return this.IsEventTriggered(ClusterNotifyFlags.CLUSTER_CHANGE_NODE_STATE);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00008E2A File Offset: 0x0000702A
		internal bool IsNodeAdded
		{
			get
			{
				return this.IsEventTriggered(ClusterNotifyFlags.CLUSTER_CHANGE_NODE_ADDED);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00008E33 File Offset: 0x00007033
		internal bool IsNodeRemoved
		{
			get
			{
				return this.IsEventTriggered(ClusterNotifyFlags.CLUSTER_CHANGE_NODE_DELETED);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008E3C File Offset: 0x0000703C
		internal bool IsRegistryChanged
		{
			get
			{
				return this.IsEventTriggered(ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_VALUE);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00008E46 File Offset: 0x00007046
		internal bool IsNetInterfaceStateChanged
		{
			get
			{
				return this.IsEventTriggered(ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_STATE);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008E54 File Offset: 0x00007054
		public override string ToString()
		{
			string arg;
			if (!AmClusterEventInfo.EventNameMap.TryGetValue(this.EventCode, out arg))
			{
				arg = "0x" + this.EventCode.ToString("X");
			}
			return string.Format("({0}, {1}, {2})", arg, this.ObjectName, this.Context);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008EB4 File Offset: 0x000070B4
		internal static Dictionary<ClusterNotifyFlags, string> InitializeEventNameLookupTable()
		{
			return new Dictionary<ClusterNotifyFlags, string>(32)
			{
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NODE_STATE,
					"NODE_STATE"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NODE_DELETED,
					"NODE_DELETED"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NODE_ADDED,
					"NODE_ADDED"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NODE_PROPERTY,
					"NODE_PROPERTY"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_NAME,
					"REGISTRY_NAME"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_ATTRIBUTES,
					"REGISTRY_ATTRIBUTES"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_VALUE,
					"REGISTRY_VALUE"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_SUBTREE,
					"REGISTRY_SUBTREE"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_STATE,
					"RESOURCE_STATE"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_DELETED,
					"RESOURCE_DELETED"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_ADDED,
					"RESOURCE_ADDED"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_PROPERTY,
					"RESOURCE_PROPERTY"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_STATE,
					"GROUP_STATE"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_DELETED,
					"GROUP_DELETED"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_ADDED,
					"GROUP_ADDED"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_PROPERTY,
					"GROUP_PROPERTY"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_RECONNECT,
					"CLUSTER_RECONNECT"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_STATE,
					"NETWORK_STATE"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_DELETED,
					"NETWORK_DELETED"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_ADDED,
					"NETWORK_ADDED"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_PROPERTY,
					"NETWORK_PROPERTY"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_STATE,
					"NETINTERFACE_STATE"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_DELETED,
					"NETINTERFACE_DELETED"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_ADDED,
					"NETINTERFACE_ADDED"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_PROPERTY,
					"NETINTERFACE_PROPERTY"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_QUORUM_STATE,
					"QUORUM_STATE"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_STATE,
					"CLUSTER_STATE"
				},
				{
					ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_PROPERTY,
					"CLUSTER_PROPERTY"
				},
				{
					~(ClusterNotifyFlags.CLUSTER_CHANGE_NODE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NODE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_NAME | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_ATTRIBUTES | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_VALUE | ClusterNotifyFlags.CLUSTER_CHANGE_REGISTRY_SUBTREE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_GROUP_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_RESOURCE_TYPE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_RECONNECT | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NETWORK_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_DELETED | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_ADDED | ClusterNotifyFlags.CLUSTER_CHANGE_NETINTERFACE_PROPERTY | ClusterNotifyFlags.CLUSTER_CHANGE_QUORUM_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_STATE | ClusterNotifyFlags.CLUSTER_CHANGE_CLUSTER_PROPERTY),
					"HANDLE_CLOSE"
				}
			};
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009081 File Offset: 0x00007281
		internal bool IsEventTriggered(ClusterNotifyFlags eventToCheck)
		{
			return (this.EventCode & eventToCheck) == eventToCheck;
		}

		// Token: 0x04000080 RID: 128
		internal static Dictionary<ClusterNotifyFlags, string> EventNameMap = AmClusterEventInfo.InitializeEventNameLookupTable();
	}
}
