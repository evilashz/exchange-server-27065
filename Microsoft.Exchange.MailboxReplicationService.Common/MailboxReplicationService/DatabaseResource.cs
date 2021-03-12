using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000268 RID: 616
	internal abstract class DatabaseResource : WlmResource
	{
		// Token: 0x06001F2E RID: 7982 RVA: 0x0004129F File Offset: 0x0003F49F
		public DatabaseResource(Guid mdbGuid, WorkloadType workloadType) : base(workloadType)
		{
			base.ResourceGuid = mdbGuid;
			base.ConfigContext = new DatabaseSettingsContext(base.ResourceGuid, base.ConfigContext);
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x000412C8 File Offset: 0x0003F4C8
		public override string ResourceName
		{
			get
			{
				DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(base.ResourceGuid, null, null, FindServerFlags.AllowMissing);
				if (!string.IsNullOrEmpty(databaseInformation.DatabaseName))
				{
					return databaseInformation.DatabaseName;
				}
				return MrsStrings.MissingDatabaseName2(base.ResourceGuid, databaseInformation.ForestFqdn).ToString();
			}
		}
	}
}
