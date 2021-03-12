using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000981 RID: 2433
	[Serializable]
	internal sealed class DbLogLocationUniqueUnderDAGCondition : ServerCondition
	{
		// Token: 0x060056C6 RID: 22214 RVA: 0x00165C00 File Offset: 0x00163E00
		public DbLogLocationUniqueUnderDAGCondition(string logLocation, ADObjectId ownerServerId, ADObjectId[] serversId, IList<Database> databases) : base(ownerServerId, null, databases, null)
		{
			this.logLocation = logLocation;
			this.serversOfMyDBCopies = serversId;
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x00165C1C File Offset: 0x00163E1C
		public override bool Verify()
		{
			bool flag = true;
			foreach (Database database in base.Databases)
			{
				ADObjectId[] serversToCompare;
				if (database.Servers != null && database.Servers.Length != 0)
				{
					serversToCompare = database.Servers;
				}
				else
				{
					serversToCompare = new ADObjectId[]
					{
						database.Server
					};
				}
				if (string.Equals((null == database.LogFolderPath) ? string.Empty : database.LogFolderPath.PathName, this.logLocation, StringComparison.OrdinalIgnoreCase) && EdbFileLocationUniqueUnderDAGCondition.HasCommonServer(this.serversOfMyDBCopies, serversToCompare))
				{
					flag = false;
					break;
				}
			}
			TaskLogger.Trace("DbLogLocationUniqueUnderServerCondition.Verify() returns {0}: <Server '{1}', LogLocation '{2}'>", new object[]
			{
				flag,
				base.ServerId.ToString(),
				this.logLocation
			});
			return flag;
		}

		// Token: 0x04003229 RID: 12841
		private readonly string logLocation;

		// Token: 0x0400322A RID: 12842
		private ADObjectId[] serversOfMyDBCopies;
	}
}
