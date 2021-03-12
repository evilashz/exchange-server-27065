using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000939 RID: 2361
	[Serializable]
	internal abstract class DatabaseCondition : ConfigurationSessionCondition
	{
		// Token: 0x060053FF RID: 21503 RVA: 0x0015B01E File Offset: 0x0015921E
		protected DatabaseCondition(Database database, IConfigurationSession session) : base(session)
		{
			this.Database = database;
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x06005400 RID: 21504 RVA: 0x0015B02E File Offset: 0x0015922E
		// (set) Token: 0x06005401 RID: 21505 RVA: 0x0015B036 File Offset: 0x00159236
		protected Database Database
		{
			get
			{
				return this.database;
			}
			set
			{
				this.database = value;
			}
		}

		// Token: 0x0400310E RID: 12558
		private Database database;
	}
}
