using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200097A RID: 2426
	[Serializable]
	internal sealed class EdbFileLocationUniqueUnderServerCondition : ServerCondition
	{
		// Token: 0x060056B4 RID: 22196 RVA: 0x001656F7 File Offset: 0x001638F7
		public EdbFileLocationUniqueUnderServerCondition(string edbFileLocation, ADObjectId serverId, IList<Database> databases) : base(serverId, null, databases, null)
		{
			this.edbFileLocation = edbFileLocation;
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x0016570C File Offset: 0x0016390C
		public override bool Verify()
		{
			bool flag = true;
			foreach (Database database in base.Databases)
			{
				if (string.Equals((null == database.EdbFilePath) ? string.Empty : database.EdbFilePath.PathName, this.edbFileLocation, StringComparison.OrdinalIgnoreCase))
				{
					TaskLogger.Trace("The specifed path '{0}' equals to the product Edb file location or copy Edb file location of database '{1}'", new object[]
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

		// Token: 0x04003224 RID: 12836
		private readonly string edbFileLocation;
	}
}
