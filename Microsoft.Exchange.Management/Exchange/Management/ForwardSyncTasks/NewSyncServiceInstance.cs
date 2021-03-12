using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000370 RID: 880
	[Cmdlet("New", "SyncServiceInstance", SupportsShouldProcess = true)]
	public sealed class NewSyncServiceInstance : NewADTaskBase<SyncServiceInstance>
	{
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x00085977 File Offset: 0x00083B77
		// (set) Token: 0x06001ED8 RID: 7896 RVA: 0x0008598E File Offset: 0x00083B8E
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public ServiceInstanceId Name
		{
			get
			{
				return (ServiceInstanceId)base.Fields[ADObjectSchema.Name];
			}
			set
			{
				base.Fields[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x000859A1 File Offset: 0x00083BA1
		// (set) Token: 0x06001EDA RID: 7898 RVA: 0x000859B8 File Offset: 0x00083BB8
		[Parameter(Mandatory = false)]
		public AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return (AccountPartitionIdParameter)base.Fields[SyncServiceInstanceSchema.AccountPartition];
			}
			set
			{
				base.Fields[SyncServiceInstanceSchema.AccountPartition] = value;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x000859CB File Offset: 0x00083BCB
		// (set) Token: 0x06001EDC RID: 7900 RVA: 0x000859E2 File Offset: 0x00083BE2
		[Parameter]
		public Version MinVersion
		{
			get
			{
				return (Version)base.Fields[SyncServiceInstanceSchema.MinVersion];
			}
			set
			{
				base.Fields[SyncServiceInstanceSchema.MinVersion] = value;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x000859F5 File Offset: 0x00083BF5
		// (set) Token: 0x06001EDE RID: 7902 RVA: 0x00085A0C File Offset: 0x00083C0C
		[Parameter]
		public Version MaxVersion
		{
			get
			{
				return (Version)base.Fields[SyncServiceInstanceSchema.MaxVersion];
			}
			set
			{
				base.Fields[SyncServiceInstanceSchema.MaxVersion] = value;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x00085A1F File Offset: 0x00083C1F
		// (set) Token: 0x06001EE0 RID: 7904 RVA: 0x00085A36 File Offset: 0x00083C36
		[Parameter]
		public int ActiveInstanceSleepInterval
		{
			get
			{
				return (int)base.Fields[SyncServiceInstanceSchema.ActiveInstanceSleepInterval];
			}
			set
			{
				base.Fields[SyncServiceInstanceSchema.ActiveInstanceSleepInterval] = value;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x00085A4E File Offset: 0x00083C4E
		// (set) Token: 0x06001EE2 RID: 7906 RVA: 0x00085A65 File Offset: 0x00083C65
		[Parameter]
		public int PassiveInstanceSleepInterval
		{
			get
			{
				return (int)base.Fields[SyncServiceInstanceSchema.PassiveInstanceSleepInterval];
			}
			set
			{
				base.Fields[SyncServiceInstanceSchema.PassiveInstanceSleepInterval] = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x00085A7D File Offset: 0x00083C7D
		// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x00085A94 File Offset: 0x00083C94
		[Parameter]
		public bool IsEnabled
		{
			get
			{
				return (bool)base.Fields[SyncServiceInstanceSchema.IsEnabled];
			}
			set
			{
				base.Fields[SyncServiceInstanceSchema.IsEnabled] = value;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x00085AAC File Offset: 0x00083CAC
		// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x00085AC3 File Offset: 0x00083CC3
		[Parameter(Mandatory = false)]
		public Version NewTenantMinVersion
		{
			get
			{
				return (Version)base.Fields[SyncServiceInstanceSchema.NewTenantMinVersion];
			}
			set
			{
				base.Fields[SyncServiceInstanceSchema.NewTenantMinVersion] = value;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x00085AD6 File Offset: 0x00083CD6
		// (set) Token: 0x06001EE8 RID: 7912 RVA: 0x00085AED File Offset: 0x00083CED
		[Parameter(Mandatory = false)]
		public Version NewTenantMaxVersion
		{
			get
			{
				return (Version)base.Fields[SyncServiceInstanceSchema.NewTenantMaxVersion];
			}
			set
			{
				base.Fields[SyncServiceInstanceSchema.NewTenantMaxVersion] = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x00085B00 File Offset: 0x00083D00
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewSyncServiceInstance(this.DataObject.Name);
			}
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x00085B12 File Offset: 0x00083D12
		protected override IConfigDataProvider CreateSession()
		{
			return ForwardSyncDataAccessHelper.CreateSession(false);
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x00085B1C File Offset: 0x00083D1C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			SyncServiceInstance syncServiceInstance = (SyncServiceInstance)base.PrepareDataObject();
			syncServiceInstance.SetId(SyncServiceInstance.GetServiceInstanceObjectId(this.Name.InstanceId));
			if (this.AccountPartition != null)
			{
				AccountPartition accountPartition = (AccountPartition)base.GetDataObject<AccountPartition>(this.AccountPartition, this.ConfigurationSession, null, null, null);
				syncServiceInstance.AccountPartition = accountPartition.Id;
			}
			if (base.Fields.IsModified(SyncServiceInstanceSchema.MinVersion))
			{
				syncServiceInstance.MinVersion = this.MinVersion;
			}
			if (base.Fields.IsModified(SyncServiceInstanceSchema.MaxVersion))
			{
				syncServiceInstance.MaxVersion = this.MaxVersion;
			}
			if (base.Fields.IsModified(SyncServiceInstanceSchema.ActiveInstanceSleepInterval))
			{
				syncServiceInstance.ActiveInstanceSleepInterval = this.ActiveInstanceSleepInterval;
			}
			if (base.Fields.IsModified(SyncServiceInstanceSchema.PassiveInstanceSleepInterval))
			{
				syncServiceInstance.PassiveInstanceSleepInterval = this.PassiveInstanceSleepInterval;
			}
			if (base.Fields.IsModified(SyncServiceInstanceSchema.IsEnabled))
			{
				syncServiceInstance.IsEnabled = this.IsEnabled;
			}
			if (base.Fields.IsModified(SyncServiceInstanceSchema.NewTenantMinVersion))
			{
				syncServiceInstance.NewTenantMinVersion = this.NewTenantMinVersion;
			}
			if (base.Fields.IsModified(SyncServiceInstanceSchema.NewTenantMaxVersion))
			{
				syncServiceInstance.NewTenantMaxVersion = this.NewTenantMaxVersion;
			}
			syncServiceInstance.IsMultiObjectCookieEnabled = true;
			TaskLogger.LogExit();
			return syncServiceInstance;
		}
	}
}
