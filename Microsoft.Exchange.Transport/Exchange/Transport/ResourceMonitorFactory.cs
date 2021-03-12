using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200004C RID: 76
	internal class ResourceMonitorFactory
	{
		// Token: 0x060001DD RID: 477 RVA: 0x0000915B File Offset: 0x0000735B
		internal ResourceMonitorFactory(ResourceManagerConfiguration resourceManagerConfig)
		{
			if (resourceManagerConfig == null)
			{
				throw new ArgumentNullException("resourceManagerConfig");
			}
			this.resourceManagerConfig = resourceManagerConfig;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00009178 File Offset: 0x00007378
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000091AD File Offset: 0x000073AD
		public ResourceMonitor MailDatabaseMonitor
		{
			get
			{
				if (this.mailDatabaseMonitor == null)
				{
					this.mailDatabaseMonitor = new DatabaseMonitor(Components.MessagingDatabase.Database.DataSource, this.resourceManagerConfig.DatabaseDiskSpaceResourceMonitor);
				}
				return this.mailDatabaseMonitor;
			}
			protected set
			{
				this.mailDatabaseMonitor = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000091B6 File Offset: 0x000073B6
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000091EB File Offset: 0x000073EB
		public ResourceMonitor MailDatabaseLoggingFolderMonitor
		{
			get
			{
				if (this.mailDatabaseLoggingFolderMonitor == null)
				{
					this.mailDatabaseLoggingFolderMonitor = new DatabaseLoggingFolderMonitor(Components.MessagingDatabase.Database.DataSource, this.resourceManagerConfig.DatabaseLoggingDiskSpaceResourceMonitor);
				}
				return this.mailDatabaseLoggingFolderMonitor;
			}
			protected set
			{
				this.mailDatabaseLoggingFolderMonitor = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000091F4 File Offset: 0x000073F4
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00009264 File Offset: 0x00007464
		public ResourceMonitor TempDriveMonitor
		{
			get
			{
				if (this.tempDriveMonitor == null)
				{
					this.tempDriveMonitor = new DiskSpaceMonitor(Strings.TemporaryStorageResource(Components.TransportAppConfig.WorkerProcess.TemporaryStoragePath), Components.TransportAppConfig.WorkerProcess.TemporaryStoragePath, new ResourceManagerConfiguration.ResourceMonitorConfiguration(100, 100, 100), Components.TransportAppConfig.ResourceManager.TempDiskSpaceRequired.ToBytes());
				}
				return this.tempDriveMonitor;
			}
			protected set
			{
				this.tempDriveMonitor = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00009270 File Offset: 0x00007470
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000092C0 File Offset: 0x000074C0
		public ResourceMonitor VersionBucketResourceMonitor
		{
			get
			{
				if (this.versionBucketResourceMonitor == null)
				{
					this.versionBucketResourceMonitor = new ResourceMonitorStabilizer(new VersionBucketsMonitor(Components.MessagingDatabase.Database.DataSource, this.resourceManagerConfig.VersionBucketsResourceMonitor), this.resourceManagerConfig.VersionBucketsResourceMonitor);
				}
				return this.versionBucketResourceMonitor;
			}
			protected set
			{
				this.versionBucketResourceMonitor = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000092C9 File Offset: 0x000074C9
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000092FF File Offset: 0x000074FF
		public ResourceMonitor MemoryPrivateBytesMonitor
		{
			get
			{
				if (this.memoryPrivateBytesMonitor == null)
				{
					this.memoryPrivateBytesMonitor = new ResourceMonitorStabilizer(new MemoryPrivateBytesMonitor(this.resourceManagerConfig.PrivateBytesResourceMonitor), this.resourceManagerConfig.PrivateBytesResourceMonitor);
				}
				return this.memoryPrivateBytesMonitor;
			}
			protected set
			{
				this.memoryPrivateBytesMonitor = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00009308 File Offset: 0x00007508
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000932E File Offset: 0x0000752E
		public ResourceMonitor MemoryTotalBytesMonitor
		{
			get
			{
				if (this.memoryTotalBytesMonitor == null)
				{
					this.memoryTotalBytesMonitor = new MemoryTotalBytesMonitor(this.resourceManagerConfig.MemoryTotalBytesResourceMonitor);
				}
				return this.memoryTotalBytesMonitor;
			}
			protected set
			{
				this.memoryTotalBytesMonitor = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00009337 File Offset: 0x00007537
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000936D File Offset: 0x0000756D
		public ResourceMonitor SubmissionQueueMonitor
		{
			get
			{
				if (this.submissionQueueMonitor == null)
				{
					this.submissionQueueMonitor = new ResourceMonitorStabilizer(new SubmissionQueueMonitor(this.resourceManagerConfig.SubmissionQueueResourceMonitor), this.resourceManagerConfig.SubmissionQueueResourceMonitor);
				}
				return this.submissionQueueMonitor;
			}
			protected set
			{
				this.submissionQueueMonitor = value;
			}
		}

		// Token: 0x04000120 RID: 288
		private ResourceManagerConfiguration resourceManagerConfig;

		// Token: 0x04000121 RID: 289
		private ResourceMonitor mailDatabaseMonitor;

		// Token: 0x04000122 RID: 290
		private ResourceMonitor mailDatabaseLoggingFolderMonitor;

		// Token: 0x04000123 RID: 291
		private ResourceMonitor tempDriveMonitor;

		// Token: 0x04000124 RID: 292
		private ResourceMonitor versionBucketResourceMonitor;

		// Token: 0x04000125 RID: 293
		private ResourceMonitor memoryPrivateBytesMonitor;

		// Token: 0x04000126 RID: 294
		private ResourceMonitor memoryTotalBytesMonitor;

		// Token: 0x04000127 RID: 295
		private ResourceMonitor submissionQueueMonitor;
	}
}
