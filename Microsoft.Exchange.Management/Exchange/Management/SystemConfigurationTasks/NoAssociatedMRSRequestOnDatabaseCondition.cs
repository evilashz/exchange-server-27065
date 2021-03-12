using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200097F RID: 2431
	[Serializable]
	internal sealed class NoAssociatedMRSRequestOnDatabaseCondition : DatabaseCondition
	{
		// Token: 0x060056C0 RID: 22208 RVA: 0x00165AA8 File Offset: 0x00163CA8
		public NoAssociatedMRSRequestOnDatabaseCondition(Database database) : base(database, null)
		{
		}

		// Token: 0x060056C1 RID: 22209 RVA: 0x00165AB4 File Offset: 0x00163CB4
		public override bool Verify()
		{
			MRSRequest mrsrequest;
			return this.Verify(out mrsrequest);
		}

		// Token: 0x060056C2 RID: 22210 RVA: 0x00165ACC File Offset: 0x00163CCC
		public bool Verify(out MRSRequest matchingMRSObject)
		{
			matchingMRSObject = PartitionDataAggregator.FindFirstMRSRequestLinkedToDatabase((ADObjectId)base.Database.Identity);
			bool flag;
			if (matchingMRSObject != null)
			{
				this.type = matchingMRSObject.RequestType;
				flag = false;
			}
			else
			{
				flag = true;
			}
			TaskLogger.Trace("NoAssociatedMRSRequestOnDatabaseCondition.Verify(Database '{0}') returns {1}.", new object[]
			{
				base.Database.Identity,
				flag
			});
			return flag;
		}

		// Token: 0x170019DB RID: 6619
		// (get) Token: 0x060056C3 RID: 22211 RVA: 0x00165B33 File Offset: 0x00163D33
		internal MRSRequestType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04003227 RID: 12839
		private MRSRequestType type;
	}
}
