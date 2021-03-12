using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200097E RID: 2430
	[Serializable]
	internal sealed class NoAssociatedUserMailboxOrMoveRequestOnDatabaseCondition : DatabaseCondition
	{
		// Token: 0x060056BD RID: 22205 RVA: 0x00165A30 File Offset: 0x00163C30
		public NoAssociatedUserMailboxOrMoveRequestOnDatabaseCondition(Database database) : base(database, null)
		{
		}

		// Token: 0x060056BE RID: 22206 RVA: 0x00165A3C File Offset: 0x00163C3C
		public override bool Verify()
		{
			ADUser aduser;
			return this.Verify(out aduser);
		}

		// Token: 0x060056BF RID: 22207 RVA: 0x00165A54 File Offset: 0x00163C54
		public bool Verify(out ADUser matchingObject)
		{
			matchingObject = PartitionDataAggregator.FindFirstUserOrMoveRequestLinkedToDatabase((ADObjectId)base.Database.Identity);
			bool flag = null == matchingObject;
			TaskLogger.Trace("NoAssociatedUserMailboxOnDatabaseCondition.Verify(Database '{0}') returns {1}.", new object[]
			{
				base.Database.Identity,
				flag
			});
			return flag;
		}
	}
}
