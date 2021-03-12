using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200097B RID: 2427
	[Serializable]
	internal sealed class EdbFileLocationUniqueUnderDAGCondition : ServerCondition
	{
		// Token: 0x060056B6 RID: 22198 RVA: 0x001657E8 File Offset: 0x001639E8
		public EdbFileLocationUniqueUnderDAGCondition(string edbFileLocation, ADObjectId ownerServerId, ADObjectId[] serversId, IList<Database> databases) : base(ownerServerId, null, databases, null)
		{
			this.edbFileLocation = edbFileLocation;
			this.serversOfMyDBCopies = serversId;
		}

		// Token: 0x060056B7 RID: 22199 RVA: 0x00165804 File Offset: 0x00163A04
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
				if (string.Equals((null == database.EdbFilePath) ? string.Empty : database.EdbFilePath.PathName, this.edbFileLocation, StringComparison.OrdinalIgnoreCase) && EdbFileLocationUniqueUnderDAGCondition.HasCommonServer(this.serversOfMyDBCopies, serversToCompare))
				{
					TaskLogger.Trace("The specifed path '{0}' equals to the product Edb file location or copy Edb file location of database '{1}' and the database is in the same DAG", new object[]
					{
						this.edbFileLocation,
						(ADObjectId)database.Identity
					});
					flag = false;
					break;
				}
			}
			TaskLogger.Trace("EdbFileLocationUniqueUnderServerCondition.Verify() returns {0}: <Server '{1}', PathName '{2}'>", new object[]
			{
				flag,
				base.ServerId.ToString(),
				this.edbFileLocation
			});
			return flag;
		}

		// Token: 0x060056B8 RID: 22200 RVA: 0x00165928 File Offset: 0x00163B28
		internal static bool HasCommonServer(ADObjectId[] servers, ADObjectId[] serversToCompare)
		{
			foreach (ADObjectId adobjectId in servers)
			{
				foreach (ADObjectId id in serversToCompare)
				{
					if (adobjectId.Equals(id))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04003225 RID: 12837
		private readonly string edbFileLocation;

		// Token: 0x04003226 RID: 12838
		private ADObjectId[] serversOfMyDBCopies;
	}
}
