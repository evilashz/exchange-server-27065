using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200097C RID: 2428
	[Serializable]
	internal sealed class NoAssociatedUserMailboxOnDatabaseCondition : DatabaseCondition
	{
		// Token: 0x060056B9 RID: 22201 RVA: 0x0016597A File Offset: 0x00163B7A
		public NoAssociatedUserMailboxOnDatabaseCondition(Database database) : base(database, null)
		{
		}

		// Token: 0x060056BA RID: 22202 RVA: 0x00165984 File Offset: 0x00163B84
		public override bool Verify()
		{
			bool flag = null == PartitionDataAggregator.FindFirstUserLinkedToDatabase((ADObjectId)base.Database.Identity);
			TaskLogger.Trace("NoAssociatedUserMailboxOnDatabaseCondition.Verify(Database '{0}') returns {1}.", new object[]
			{
				base.Database.Identity,
				flag
			});
			return flag;
		}
	}
}
