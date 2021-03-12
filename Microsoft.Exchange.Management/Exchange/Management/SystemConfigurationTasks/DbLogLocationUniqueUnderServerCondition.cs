using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000980 RID: 2432
	[Serializable]
	internal sealed class DbLogLocationUniqueUnderServerCondition : ServerCondition
	{
		// Token: 0x060056C4 RID: 22212 RVA: 0x00165B3B File Offset: 0x00163D3B
		public DbLogLocationUniqueUnderServerCondition(string logLocation, ADObjectId serverId, IList<Database> databases) : base(serverId, null, databases, null)
		{
			this.logLocation = logLocation;
		}

		// Token: 0x060056C5 RID: 22213 RVA: 0x00165B50 File Offset: 0x00163D50
		public override bool Verify()
		{
			bool flag = true;
			foreach (Database database in base.Databases)
			{
				if (string.Equals((null == database.LogFolderPath) ? string.Empty : database.LogFolderPath.PathName, this.logLocation, StringComparison.OrdinalIgnoreCase))
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

		// Token: 0x04003228 RID: 12840
		private readonly string logLocation;
	}
}
