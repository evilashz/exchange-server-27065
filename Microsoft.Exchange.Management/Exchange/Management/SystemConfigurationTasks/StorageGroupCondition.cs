using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000938 RID: 2360
	[Serializable]
	internal abstract class StorageGroupCondition : ConfigurationSessionCondition
	{
		// Token: 0x060053FA RID: 21498 RVA: 0x0015AFE5 File Offset: 0x001591E5
		protected StorageGroupCondition(ADObjectId storageGroupId, Database[] databases, IConfigurationSession session) : base(session)
		{
			this.StorageGroupId = storageGroupId;
			this.Databases = databases;
		}

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x060053FB RID: 21499 RVA: 0x0015AFFC File Offset: 0x001591FC
		// (set) Token: 0x060053FC RID: 21500 RVA: 0x0015B004 File Offset: 0x00159204
		protected ADObjectId StorageGroupId
		{
			get
			{
				return this.storageGroupId;
			}
			set
			{
				this.storageGroupId = value;
			}
		}

		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x060053FD RID: 21501 RVA: 0x0015B00D File Offset: 0x0015920D
		// (set) Token: 0x060053FE RID: 21502 RVA: 0x0015B015 File Offset: 0x00159215
		protected Database[] Databases
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

		// Token: 0x0400310C RID: 12556
		private ADObjectId storageGroupId;

		// Token: 0x0400310D RID: 12557
		private Database[] databases;
	}
}
