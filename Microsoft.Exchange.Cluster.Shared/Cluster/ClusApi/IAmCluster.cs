using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x0200001A RID: 26
	internal interface IAmCluster : IDisposable
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A5 RID: 165
		string Name { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A6 RID: 166
		string CnoName { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A7 RID: 167
		AmClusterHandle Handle { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A8 RID: 168
		bool IsRefreshRequired { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A9 RID: 169
		bool IsLocalNodeUp { get; }

		// Token: 0x060000AA RID: 170
		void DestroyExchangeCluster(IClusterSetupProgress setupProgress, IntPtr context, out Exception errorException, bool throwExceptionOnFailure);

		// Token: 0x060000AB RID: 171
		IAmClusterNode OpenNode(AmServerName nodeName);

		// Token: 0x060000AC RID: 172
		IEnumerable<IAmClusterNode> EnumerateNodes();

		// Token: 0x060000AD RID: 173
		AmNodeState GetNodeState(AmServerName nodeName, out Exception ex);

		// Token: 0x060000AE RID: 174
		void AddNodeToCluster(AmServerName nodeName, IClusterSetupProgress setupProgress, IntPtr context, out Exception errorException, bool throwExceptionOnFailure);

		// Token: 0x060000AF RID: 175
		void EvictNodeFromCluster(AmServerName nodeName);

		// Token: 0x060000B0 RID: 176
		IAmClusterGroup FindCoreClusterGroup();

		// Token: 0x060000B1 RID: 177
		AmClusterResource OpenQuorumResource();

		// Token: 0x060000B2 RID: 178
		string GetQuorumResourceInformation(out string outDeviceName, out uint outMaxQuorumLogSize);

		// Token: 0x060000B3 RID: 179
		void SetQuorumResource(IAmClusterResource newQuorum, string deviceName, uint maxLogSize);

		// Token: 0x060000B4 RID: 180
		void ClearQuorumResource();

		// Token: 0x060000B5 RID: 181
		AmClusterResource OpenResource(string resourceName);

		// Token: 0x060000B6 RID: 182
		IEnumerable<AmClusterNetwork> EnumerateNetworks();

		// Token: 0x060000B7 RID: 183
		AmClusterNetwork OpenNetwork(string networkName);

		// Token: 0x060000B8 RID: 184
		AmClusterNetwork FindNetworkByName(string networkName, IPVersion ipVer);

		// Token: 0x060000B9 RID: 185
		AmClusterNetwork FindNetworkByIPv4Address(IPAddress ipAddr);

		// Token: 0x060000BA RID: 186
		AmClusterNetInterface OpenNetInterface(string nicName);

		// Token: 0x060000BB RID: 187
		AmNetInterfaceState GetNetInterfaceState(string nicName, out Exception ex);
	}
}
