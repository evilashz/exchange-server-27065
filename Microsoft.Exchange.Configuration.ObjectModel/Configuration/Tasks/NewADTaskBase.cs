using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000072 RID: 114
	public abstract class NewADTaskBase<TDataObject> : NewTenantADTaskBase<TDataObject> where TDataObject : ADObject, new()
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000100FB File Offset: 0x0000E2FB
		protected virtual string ClonableTypeName
		{
			get
			{
				return NewADTaskBase<TDataObject>.clonableTypeName;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00010102 File Offset: 0x0000E302
		protected virtual bool EnforceExchangeObjectVersion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00010105 File Offset: 0x0000E305
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0001011C File Offset: 0x0000E31C
		protected PSObject TemplateInstance
		{
			get
			{
				return (PSObject)base.Fields["TemplateInstance"];
			}
			set
			{
				if (!value.TypeNames.Contains(this.ClonableTypeName) && !value.TypeNames.Contains("Deserialized." + this.ClonableTypeName))
				{
					throw new ArgumentException(Strings.ErrorInvalidParameterType("TemplateInstance", this.ClonableTypeName));
				}
				base.Fields["TemplateInstance"] = value;
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00010185 File Offset: 0x0000E385
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new ADObjectTaskModuleFactory();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001018C File Offset: 0x0000E38C
		protected virtual void StampDefaultValues(TDataObject dataObject)
		{
			dataObject.StampPersistableDefaultValues();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001019B File Offset: 0x0000E39B
		protected sealed override void InitializeDataObject(TDataObject dataObject)
		{
			this.StampDefaultValues(dataObject);
			dataObject.ResetChangeTracking();
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000101B4 File Offset: 0x0000E3B4
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TDataObject tdataObject = (TDataObject)((object)base.PrepareDataObject());
			if (this.TemplateInstance != null)
			{
				TDataObject tdataObject2 = Activator.CreateInstance<TDataObject>();
				this.InitializeDataObject(tdataObject2);
				tdataObject2.ProvisionalClone(new PSObjectWrapper(this.TemplateInstance));
				tdataObject2.CopyChangesFrom(tdataObject);
				tdataObject = tdataObject2;
			}
			if (base.CurrentOrganizationId != null)
			{
				tdataObject.OrganizationId = base.CurrentOrganizationId;
			}
			else
			{
				tdataObject.OrganizationId = base.ExecutingUserOrganizationId;
			}
			TaskLogger.LogExit();
			return tdataObject;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00010254 File Offset: 0x0000E454
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			TDataObject dataObject = this.DataObject;
			ADObjectId parent = dataObject.Id.Parent;
			ADSessionSettings adsessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(parent);
			if (!parent.Equals(ADSession.GetRootDomainNamingContext(adsessionSettings.GetAccountOrResourceForestFqdn())))
			{
				IDirectorySession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, adsessionSettings, 264, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\NewAdObjectTask.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = ((IDirectorySession)base.DataSession).UseConfigNC;
				ADRawEntry adrawEntry = tenantOrTopologyConfigurationSession.ReadADRawEntry(parent, new PropertyDefinition[]
				{
					ADObjectSchema.ExchangeVersion,
					ADObjectSchema.ObjectClass
				});
				if (adrawEntry == null)
				{
					if (string.IsNullOrEmpty(base.DomainController))
					{
						TDataObject dataObject2 = this.DataObject;
						base.WriteError(new TaskException(Strings.ErrorParentNotFound(dataObject2.Name, parent.ToString())), (ErrorCategory)1003, null);
					}
					else
					{
						TDataObject dataObject3 = this.DataObject;
						base.WriteError(new TaskException(Strings.ErrorParentNotFoundOnDomainController(dataObject3.Name, base.DomainController, parent.ToString(), parent.DomainId.ToString())), (ErrorCategory)1003, null);
					}
				}
				ExchangeObjectVersion exchangeObjectVersion = (ExchangeObjectVersion)adrawEntry[ADObjectSchema.ExchangeVersion];
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)adrawEntry[ADObjectSchema.ObjectClass];
				TDataObject dataObject4 = this.DataObject;
				if (dataObject4.ExchangeVersion.IsOlderThan(exchangeObjectVersion) && !multiValuedProperty.Contains(Organization.MostDerivedClass) && this.EnforceExchangeObjectVersion)
				{
					TDataObject dataObject5 = this.DataObject;
					string name = dataObject5.Name;
					TDataObject dataObject6 = this.DataObject;
					base.WriteError(new TaskException(Strings.ErrorParentHasNewerVersion(name, dataObject6.ExchangeVersion.ToString(), exchangeObjectVersion.ToString())), (ErrorCategory)1004, null);
				}
			}
			if (base.IsVerboseOn)
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetConfigurableObjectChangedProperties(this.DataObject));
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x04000116 RID: 278
		private static string clonableTypeName = typeof(TDataObject).FullName;
	}
}
