using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000937 RID: 2359
	[Serializable]
	internal abstract class ServerCondition : ConfigurationSessionCondition
	{
		// Token: 0x060053F3 RID: 21491 RVA: 0x0015AF93 File Offset: 0x00159193
		protected ServerCondition(ADObjectId serverId, IList<StorageGroup> storageGroups, IList<Database> databases, IConfigurationSession session) : base(session)
		{
			this.ServerId = serverId;
			this.StorageGroups = storageGroups;
			this.Databases = databases;
		}

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x060053F4 RID: 21492 RVA: 0x0015AFB2 File Offset: 0x001591B2
		// (set) Token: 0x060053F5 RID: 21493 RVA: 0x0015AFBA File Offset: 0x001591BA
		protected ADObjectId ServerId
		{
			get
			{
				return this.serverId;
			}
			set
			{
				this.serverId = value;
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x060053F6 RID: 21494 RVA: 0x0015AFC3 File Offset: 0x001591C3
		// (set) Token: 0x060053F7 RID: 21495 RVA: 0x0015AFCB File Offset: 0x001591CB
		protected IList<StorageGroup> StorageGroups
		{
			get
			{
				return this.storageGroups;
			}
			set
			{
				this.storageGroups = value;
			}
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x060053F8 RID: 21496 RVA: 0x0015AFD4 File Offset: 0x001591D4
		// (set) Token: 0x060053F9 RID: 21497 RVA: 0x0015AFDC File Offset: 0x001591DC
		protected IList<Database> Databases
		{
			get
			{
				return this.databases;
			}
			set
			{
				this.databases = value;
			}
		}

		// Token: 0x04003109 RID: 12553
		private ADObjectId serverId;

		// Token: 0x0400310A RID: 12554
		private IList<StorageGroup> storageGroups;

		// Token: 0x0400310B RID: 12555
		private IList<Database> databases;
	}
}
