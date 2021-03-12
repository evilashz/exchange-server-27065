using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B12 RID: 2834
	[Serializable]
	internal sealed class StorageGroupNumberLessThanMaxLimitCondition : ServerCondition
	{
		// Token: 0x060064B8 RID: 25784 RVA: 0x001A4928 File Offset: 0x001A2B28
		public StorageGroupNumberLessThanMaxLimitCondition(bool isForRecovery, ADObjectId serverId, IList<StorageGroup> storageGroups, bool isEnterprise) : base(serverId, storageGroups, null, null)
		{
			this.isForRecovery = isForRecovery;
			this.isEnterprise = isEnterprise;
		}

		// Token: 0x060064B9 RID: 25785 RVA: 0x001A4944 File Offset: 0x001A2B44
		public override bool Verify()
		{
			int num = this.isEnterprise ? 100 : 5;
			bool flag = false;
			foreach (StorageGroup storageGroup in base.StorageGroups)
			{
				if (storageGroup.Recovery)
				{
					flag = true;
				}
			}
			bool flag2;
			if (this.isForRecovery)
			{
				flag2 = !flag;
			}
			else
			{
				flag2 = (base.StorageGroups.Count < (flag ? (num + 1) : num));
			}
			TaskLogger.Trace("StorageGroupNumberLessThanMaxLimitCondition.Verify() returns {0}: <Server '{1}'>", new object[]
			{
				flag2,
				base.ServerId.ToString()
			});
			return flag2;
		}

		// Token: 0x04003634 RID: 13876
		internal const int MaxStorageGroupNumberForEnterpriseEdition = 100;

		// Token: 0x04003635 RID: 13877
		internal const int MaxStorageGroupNumberForStandardOrCoexistenceEdition = 5;

		// Token: 0x04003636 RID: 13878
		private readonly bool isForRecovery;

		// Token: 0x04003637 RID: 13879
		private readonly bool isEnterprise;
	}
}
