using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x020000B4 RID: 180
	internal class HungNodesInfo
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0002411B File Offset: 0x0002231B
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x00024123 File Offset: 0x00022323
		public int CurrentGumId { get; private set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0002412C File Offset: 0x0002232C
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x00024134 File Offset: 0x00022334
		public AmServerName CurrentLockOwnerName { get; private set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0002413D File Offset: 0x0002233D
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x00024145 File Offset: 0x00022345
		public long HungNodesAsBitmask { get; private set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0002414E File Offset: 0x0002234E
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x00024156 File Offset: 0x00022356
		public Dictionary<int, AmServerName> NodeMap { get; private set; }

		// Token: 0x06000770 RID: 1904 RVA: 0x0002415F File Offset: 0x0002235F
		private HungNodesInfo(int currentGumId, AmServerName lockOwnerName, long hungNodesMask)
		{
			this.CurrentGumId = currentGumId;
			this.CurrentLockOwnerName = lockOwnerName;
			this.HungNodesAsBitmask = hungNodesMask;
			this.NodeMap = HungNodesInfo.GenerateHungNodeMap(hungNodesMask);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00024188 File Offset: 0x00022388
		public static Dictionary<int, AmServerName> GenerateHungNodeMap(long hungNodesMask)
		{
			Dictionary<int, AmServerName> dictionary = new Dictionary<int, AmServerName>(64);
			foreach (int num in AmClusterNode.GetNodeIdsFromNodeMask(hungNodesMask))
			{
				AmServerName nameById = AmClusterNode.GetNameById(num);
				if (!AmServerName.IsNullOrEmpty(nameById))
				{
					dictionary[num] = nameById;
				}
				else
				{
					AmTrace.Error("Failed to map nodeId {0} to node name", new object[]
					{
						num
					});
				}
			}
			return dictionary;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00024210 File Offset: 0x00022410
		public static HungNodesInfo GetNodesHungInClusDbUpdate()
		{
			HungNodesInfo result = null;
			TimeSpan timeout = TimeSpan.FromSeconds((double)RegistryParameters.OpenClusterTimeoutInSec);
			using (AmCluster amCluster = AmCluster.OpenByName(AmServerName.LocalComputerName, timeout, string.Empty))
			{
				int num = 0;
				AmServerName currentGumLockOwnerInfo = amCluster.GetCurrentGumLockOwnerInfo(out num);
				if (!AmServerName.IsNullOrEmpty(currentGumLockOwnerInfo))
				{
					Thread.Sleep(RegistryParameters.ClusdbHungNodesConfirmDurationInMSec);
					string context = string.Format("GUM={0}", num);
					using (AmCluster amCluster2 = AmCluster.OpenByName(currentGumLockOwnerInfo, timeout, context))
					{
						using (IAmClusterNode amClusterNode = amCluster2.OpenNode(currentGumLockOwnerInfo))
						{
							int num2 = 0;
							long hungNodesMask = amClusterNode.GetHungNodesMask(out num2);
							if (num != num2)
							{
								throw new HungDetectionGumIdChangedException(num, num2, currentGumLockOwnerInfo.ToString(), hungNodesMask);
							}
							result = new HungNodesInfo(num, currentGumLockOwnerInfo, hungNodesMask);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0002430C File Offset: 0x0002250C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.AppendFormat("GUM ID: {0} Owner: {1}\nHung nodes:\n", this.CurrentGumId, this.CurrentLockOwnerName);
			foreach (KeyValuePair<int, AmServerName> keyValuePair in this.NodeMap)
			{
				stringBuilder.AppendFormat("{0} => {1}\n", keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}
	}
}
