using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B11 RID: 2833
	[Serializable]
	internal sealed class SystemPathUniqueUnderServerCondition : ServerCondition
	{
		// Token: 0x060064B6 RID: 25782 RVA: 0x001A4864 File Offset: 0x001A2A64
		public SystemPathUniqueUnderServerCondition(string systemPath, ADObjectId serverId, IList<StorageGroup> storageGroups) : base(serverId, storageGroups, null, null)
		{
			this.systemPath = systemPath;
		}

		// Token: 0x060064B7 RID: 25783 RVA: 0x001A4878 File Offset: 0x001A2A78
		public override bool Verify()
		{
			bool flag = true;
			foreach (StorageGroup storageGroup in base.StorageGroups)
			{
				if (string.Compare((storageGroup.SystemFolderPath != null) ? storageGroup.SystemFolderPath.PathName : string.Empty, this.systemPath, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					flag = false;
					break;
				}
			}
			TaskLogger.Trace("SystemPathUniqueUnderServerCondition.Verify() returns {0}: <Server '{1}', SystemPath '{2}'>", new object[]
			{
				flag,
				base.ServerId.ToString(),
				this.systemPath
			});
			return flag;
		}

		// Token: 0x04003633 RID: 13875
		private readonly string systemPath;
	}
}
