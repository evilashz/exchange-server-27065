using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.People;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.People.DataProviders
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactDataProvider : StorageItemDataProvider<IMailboxSession, Contact, IContact>
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000225E File Offset: 0x0000045E
		internal ContactDataProvider(IStorageEntitySetScope<IMailboxSession> scope, ITracer tracer) : base(scope, null, tracer)
		{
			ArgumentValidator.ThrowIfNull("scope", scope);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.FolderInScope = DefaultFolderType.None;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002286 File Offset: 0x00000486
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000228E File Offset: 0x0000048E
		public DefaultFolderType FolderInScope { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002297 File Offset: 0x00000497
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000229F File Offset: 0x0000049F
		public PropertyDefinition[] ContactProperties { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022A8 File Offset: 0x000004A8
		protected override IStorageTranslator<IContact, Contact> Translator
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022B4 File Offset: 0x000004B4
		internal IEnumerable<IStorePropertyBag> GetAllContacts(SortBy[] sortColumns)
		{
			this.ValidateContactProperties();
			this.ValidateFolderInScope();
			return ContactsEnumerator<IStorePropertyBag>.CreateContactsOnlyEnumerator(base.Session, this.FolderInScope, sortColumns, this.ContactProperties, (IStorePropertyBag propertyBag) => propertyBag, base.XsoFactory);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002308 File Offset: 0x00000508
		internal ConflictResolutionResult AddContact(IStorePropertyBag propertyBag)
		{
			ArgumentValidator.ThrowIfNull("propertyBag", propertyBag);
			this.ValidateContactProperties();
			ConflictResolutionResult result;
			using (IContact contact = this.CreateNewStoreObject())
			{
				foreach (PropertyDefinition propertyDefinition in this.ContactProperties)
				{
					object valueOrDefault = propertyBag.GetValueOrDefault<object>(propertyDefinition, null);
					if (valueOrDefault != null)
					{
						contact[propertyDefinition] = valueOrDefault;
					}
				}
				result = contact.Save(SaveMode.ResolveConflicts);
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002388 File Offset: 0x00000588
		internal ConflictResolutionResult UpdateContact(VersionedId versionedId, ICollection<Tuple<PropertyDefinition, object>> changedPropertiesValue)
		{
			ArgumentValidator.ThrowIfNull("versionedId", versionedId);
			ArgumentValidator.ThrowIfNull("changedPropertiesValue", changedPropertiesValue);
			this.GetChangedProperties(changedPropertiesValue);
			ConflictResolutionResult result;
			using (IContact contact = this.Bind(versionedId))
			{
				foreach (Tuple<PropertyDefinition, object> tuple in changedPropertiesValue)
				{
					contact.SetOrDeleteProperty(tuple.Item1, tuple.Item2);
				}
				result = contact.Save(SaveMode.ResolveConflicts);
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002424 File Offset: 0x00000624
		protected internal override IContact BindToStoreObject(StoreId id)
		{
			this.ValidateContactProperties();
			return base.XsoFactory.BindToContact(base.Session, id, this.ContactProperties);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002444 File Offset: 0x00000644
		protected override IContact CreateNewStoreObject()
		{
			this.ValidateFolderInScope();
			return base.XsoFactory.CreateContact(base.Session, base.Session.GetDefaultFolderId(this.FolderInScope));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002470 File Offset: 0x00000670
		private PropertyDefinition[] GetChangedProperties(IEnumerable<Tuple<PropertyDefinition, object>> list)
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			foreach (Tuple<PropertyDefinition, object> tuple in list)
			{
				hashSet.Add(tuple.Item1);
			}
			return hashSet.ToArray<PropertyDefinition>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024CC File Offset: 0x000006CC
		private void ValidateContactProperties()
		{
			if (this.ContactProperties == null)
			{
				throw new InvalidOperationException("ContactPropertiesList property is not set.");
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024E1 File Offset: 0x000006E1
		private void ValidateFolderInScope()
		{
			if (this.FolderInScope == DefaultFolderType.None)
			{
				throw new InvalidOperationException("FolderInScope property is not set.");
			}
		}
	}
}
