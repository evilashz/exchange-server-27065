using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200097D RID: 2429
	[Serializable]
	internal sealed class NoAssociatedMoveRequestOnDatabaseCondition : DatabaseCondition
	{
		// Token: 0x060056BB RID: 22203 RVA: 0x001659D4 File Offset: 0x00163BD4
		public NoAssociatedMoveRequestOnDatabaseCondition(Database database) : base(database, null)
		{
		}

		// Token: 0x060056BC RID: 22204 RVA: 0x001659E0 File Offset: 0x00163BE0
		public override bool Verify()
		{
			bool flag = null == PartitionDataAggregator.FindFirstMoveRequestLinkedToDatabase((ADObjectId)base.Database.Identity);
			TaskLogger.Trace("NoAssociatedMoveRequestOnDatabaseCondition.Verify(Database '{0}') returns {1}.", new object[]
			{
				base.Database.Identity,
				flag
			});
			return flag;
		}
	}
}
