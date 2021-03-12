using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000074 RID: 116
	public abstract class NewGeneralRecipientObjectTask<TDataObject> : NewRecipientObjectTaskBase<TDataObject> where TDataObject : ADRecipient, new()
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000106EC File Offset: 0x0000E8EC
		protected ADObjectId RecipientContainerId
		{
			get
			{
				return this.containerId;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000106F4 File Offset: 0x0000E8F4
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00010718 File Offset: 0x0000E918
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.Name;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.Name = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0001073C File Offset: 0x0000E93C
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00010760 File Offset: 0x0000E960
		[Parameter]
		public string DisplayName
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.DisplayName;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.DisplayName = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00010782 File Offset: 0x0000E982
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00010799 File Offset: 0x0000E999
		[Parameter(Mandatory = false)]
		public OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields["OrganizationalUnit"];
			}
			set
			{
				base.Fields["OrganizationalUnit"] = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x000107AC File Offset: 0x0000E9AC
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x000107D0 File Offset: 0x0000E9D0
		[ValidateNotNullOrEmpty]
		[Parameter]
		public string ExternalDirectoryObjectId
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.ExternalDirectoryObjectId;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.ExternalDirectoryObjectId = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x000107F2 File Offset: 0x0000E9F2
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00010809 File Offset: 0x0000EA09
		public RecipientIdParameter SoftDeletedObject
		{
			get
			{
				return (RecipientIdParameter)base.Fields["SoftDeletedObject"];
			}
			set
			{
				base.Fields["SoftDeletedObject"] = value;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000108B0 File Offset: 0x0000EAB0
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (!string.IsNullOrEmpty(this.ExternalDirectoryObjectId))
			{
				ITenantRecipientSession tenantRecipientSession = base.TenantGlobalCatalogSession as ITenantRecipientSession;
				if (tenantRecipientSession != null)
				{
					bool useGlobalCatalog = tenantRecipientSession.UseGlobalCatalog;
					tenantRecipientSession.UseGlobalCatalog = false;
					Result<ADRawEntry>[] array = null;
					try
					{
						array = tenantRecipientSession.FindByExternalDirectoryObjectIds(new string[]
						{
							this.ExternalDirectoryObjectId
						}, true, new ADPropertyDefinition[]
						{
							DeletedObjectSchema.LastKnownParent
						});
					}
					finally
					{
						tenantRecipientSession.UseGlobalCatalog = useGlobalCatalog;
					}
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].Error != ProviderError.NotFound || array[i].Data != null)
							{
								ADObjectId adobjectId = null;
								if (array[i].Error == null)
								{
									adobjectId = (ADObjectId)array[i].Data[DeletedObjectSchema.LastKnownParent];
								}
								if (array[i].Error != null || adobjectId == null || (adobjectId.DomainId != null && !adobjectId.IsDescendantOf(ADSession.GetDeletedObjectsContainer(adobjectId.DomainId))))
								{
									base.ThrowTerminatingError(new DuplicateExternalDirectoryObjectIdException(this.Name, this.ExternalDirectoryObjectId), ExchangeErrorCategory.Client, null);
								}
							}
						}
					}
				}
			}
			bool useConfigNC = this.ConfigurationSession.UseConfigNC;
			bool useGlobalCatalog2 = this.ConfigurationSession.UseGlobalCatalog;
			this.ConfigurationSession.UseConfigNC = false;
			this.ConfigurationSession.UseGlobalCatalog = true;
			IConfigurationSession cfgSession = this.ConfigurationSession;
			if (!cfgSession.IsReadConnectionAvailable())
			{
				cfgSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, this.ConfigurationSession.SessionSettings, 623, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\NewAdObjectTask.cs");
				cfgSession.UseGlobalCatalog = true;
				cfgSession.UseConfigNC = false;
			}
			try
			{
				ExchangeOrganizationalUnit exchangeOrganizationalUnit = null;
				if (this.OrganizationalUnit != null)
				{
					exchangeOrganizationalUnit = base.ProvisioningCache.TryAddAndGetGlobalDictionaryValue<ExchangeOrganizationalUnit, string>(CannedProvisioningCacheKeys.OrganizationalUnitDictionary, this.OrganizationalUnit.RawIdentity, () => (ExchangeOrganizationalUnit)this.GetDataObject<ExchangeOrganizationalUnit>(this.OrganizationalUnit, cfgSession, (this.CurrentOrganizationId != null) ? this.CurrentOrganizationId.OrganizationalUnit : null, null, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(this.OrganizationalUnit.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(this.OrganizationalUnit.ToString()))));
				}
				if (exchangeOrganizationalUnit != null)
				{
					this.containerId = exchangeOrganizationalUnit.Id;
				}
				else if (base.CurrentOrganizationId != null && base.CurrentOrganizationId.OrganizationalUnit != null)
				{
					this.containerId = base.CurrentOrganizationId.OrganizationalUnit;
				}
				else
				{
					string defaultOUForRecipient = RecipientTaskHelper.GetDefaultOUForRecipient(base.ServerSettings.RecipientViewRoot);
					if (string.IsNullOrEmpty(defaultOUForRecipient))
					{
						base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorCannotDiscoverDefaultOrganizationUnitForRecipient), ExchangeErrorCategory.Client, null);
					}
					exchangeOrganizationalUnit = (ExchangeOrganizationalUnit)base.GetDataObject<ExchangeOrganizationalUnit>(new OrganizationalUnitIdParameter(defaultOUForRecipient), cfgSession, null, null, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(defaultOUForRecipient)), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(defaultOUForRecipient)), ExchangeErrorCategory.Client);
					this.containerId = exchangeOrganizationalUnit.Id;
				}
				if (exchangeOrganizationalUnit != null)
				{
					RecipientTaskHelper.IsOrgnizationalUnitInOrganization(cfgSession, base.CurrentOrganizationId, exchangeOrganizationalUnit, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			finally
			{
				this.ConfigurationSession.UseConfigNC = useConfigNC;
				this.ConfigurationSession.UseGlobalCatalog = useGlobalCatalog2;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00010C04 File Offset: 0x0000EE04
		protected override void PrepareRecipientObject(TDataObject dataObject)
		{
			dataObject.SetId(this.RecipientContainerId.GetChildId(this.Name));
		}

		// Token: 0x04000117 RID: 279
		private ADObjectId containerId;
	}
}
