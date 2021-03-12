using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B10 RID: 2832
	[Serializable]
	internal sealed class LogLocationUniqueUnderServerCondition : ServerCondition
	{
		// Token: 0x060064B4 RID: 25780 RVA: 0x001A47A0 File Offset: 0x001A29A0
		public LogLocationUniqueUnderServerCondition(string logLocation, ADObjectId serverId, IList<StorageGroup> storageGroups) : base(serverId, storageGroups, null, null)
		{
			this.logLocation = logLocation;
		}

		// Token: 0x060064B5 RID: 25781 RVA: 0x001A47B4 File Offset: 0x001A29B4
		public override bool Verify()
		{
			bool flag = true;
			foreach (StorageGroup storageGroup in base.StorageGroups)
			{
				if (string.Compare((storageGroup.LogFolderPath != null) ? storageGroup.LogFolderPath.PathName : string.Empty, this.logLocation, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					flag = false;
					break;
				}
			}
			TaskLogger.Trace("LogLocationUniqueUnderServerCondition.Verify() returns {0}: <Server '{1}', LogLocation '{2}'>", new object[]
			{
				flag,
				base.ServerId.ToString(),
				this.logLocation
			});
			return flag;
		}

		// Token: 0x04003632 RID: 13874
		private readonly string logLocation;
	}
}
