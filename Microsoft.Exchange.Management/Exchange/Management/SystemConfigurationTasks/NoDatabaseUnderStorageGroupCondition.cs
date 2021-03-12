using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B13 RID: 2835
	[Serializable]
	internal sealed class NoDatabaseUnderStorageGroupCondition : StorageGroupCondition
	{
		// Token: 0x060064BA RID: 25786 RVA: 0x001A4A00 File Offset: 0x001A2C00
		public NoDatabaseUnderStorageGroupCondition(ADObjectId storageGroupId, IConfigurationSession session) : base(storageGroupId, null, session)
		{
		}

		// Token: 0x060064BB RID: 25787 RVA: 0x001A4A0C File Offset: 0x001A2C0C
		public override bool Verify()
		{
			bool flag = base.Session.Find<Database>(base.StorageGroupId, QueryScope.OneLevel, null, null, 1).Length == 0;
			TaskLogger.Trace("NoDatabaseUnderStorageGroupCondition.Verify() returns {0}: <StorageGroup '{1}'>", new object[]
			{
				flag,
				base.StorageGroupId.ToString()
			});
			return flag;
		}
	}
}
