using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000066 RID: 102
	public interface IDxStoreConfigProvider
	{
		// Token: 0x06000431 RID: 1073
		void RefreshTopology(bool isForceRefresh = false);

		// Token: 0x06000432 RID: 1074
		InstanceManagerConfig GetManagerConfig();

		// Token: 0x06000433 RID: 1075
		string[] GetAllGroupNames();

		// Token: 0x06000434 RID: 1076
		InstanceGroupConfig[] GetAllGroupConfigs();

		// Token: 0x06000435 RID: 1077
		InstanceGroupConfig GetGroupConfig(string groupName, bool isFillDefaultValueIfNotExist = false);

		// Token: 0x06000436 RID: 1078
		string[] GetGroupMemberNames(string groupName);

		// Token: 0x06000437 RID: 1079
		InstanceGroupMemberConfig[] GetGroupMemberConfigs(string groupName);

		// Token: 0x06000438 RID: 1080
		void DisableGroup(string groupName);

		// Token: 0x06000439 RID: 1081
		void EnableGroup(string groupName);

		// Token: 0x0600043A RID: 1082
		void RemoveGroupConfig(string groupName);

		// Token: 0x0600043B RID: 1083
		string GetDefaultGroupName();

		// Token: 0x0600043C RID: 1084
		void SetDefaultGroupName(string groupName);

		// Token: 0x0600043D RID: 1085
		void RemoveDefaultGroupName();

		// Token: 0x0600043E RID: 1086
		void SetRestartRequired(string groupName, bool isRestartRequired);
	}
}
