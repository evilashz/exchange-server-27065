using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E63 RID: 3683
	internal class ContactFolderSchema : EntitySchema
	{
		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06005F9D RID: 24477 RVA: 0x0012A7B5 File Offset: 0x001289B5
		public new static ContactFolderSchema SchemaInstance
		{
			get
			{
				return ContactFolderSchema.ContactFolderSchemaInstance.Member;
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06005F9E RID: 24478 RVA: 0x0012A7C1 File Offset: 0x001289C1
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return ContactFolder.EdmEntityType;
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06005F9F RID: 24479 RVA: 0x0012A7C8 File Offset: 0x001289C8
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return ContactFolderSchema.DeclaredContactFolderProperties;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x06005FA0 RID: 24480 RVA: 0x0012A7CF File Offset: 0x001289CF
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return ContactFolderSchema.AllContactFolderProperties;
			}
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06005FA1 RID: 24481 RVA: 0x0012A7D6 File Offset: 0x001289D6
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return ContactFolderSchema.DefaultContactFolderProperties;
			}
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06005FA2 RID: 24482 RVA: 0x0012A7DD File Offset: 0x001289DD
		public override ReadOnlyCollection<PropertyDefinition> MandatoryCreationProperties
		{
			get
			{
				return ContactFolderSchema.MandatoryContactFolderCreationProperties;
			}
		}

		// Token: 0x06005FA4 RID: 24484 RVA: 0x0012A80C File Offset: 0x00128A0C
		// Note: this type is marked as 'beforefieldinit'.
		static ContactFolderSchema()
		{
			PropertyDefinition propertyDefinition = new PropertyDefinition("ParentFolderId", typeof(string));
			propertyDefinition.EdmType = EdmCoreModel.Instance.GetString(true);
			PropertyDefinition propertyDefinition2 = propertyDefinition;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider = new SimpleEwsPropertyProvider(BaseFolderSchema.ParentFolderId);
			simpleEwsPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				e[ep] = EwsIdConverter.EwsIdToODataId((s[sp] as FolderId).Id);
			};
			propertyDefinition2.EwsPropertyProvider = simpleEwsPropertyProvider;
			ContactFolderSchema.ParentFolderId = propertyDefinition;
			ContactFolderSchema.DisplayName = new PropertyDefinition("DisplayName", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(BaseFolderSchema.DisplayName)
				{
					SetPropertyUpdateCreator = EwsPropertyProvider.SetFolderPropertyUpdateDelegate,
					DeletePropertyUpdateCreator = EwsPropertyProvider.DeleteFolderPropertyUpdateDelegate
				}
			};
			ContactFolderSchema.Contacts = new PropertyDefinition("Contacts", typeof(IEnumerable<Contact>))
			{
				Flags = PropertyDefinitionFlags.Navigation,
				NavigationTargetEntity = Contact.EdmEntityType
			};
			ContactFolderSchema.ChildFolders = new PropertyDefinition("ChildFolders", typeof(IEnumerable<ContactFolder>))
			{
				Flags = PropertyDefinitionFlags.Navigation,
				NavigationTargetEntity = ContactFolder.EdmEntityType
			};
			ContactFolderSchema.DeclaredContactFolderProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				ContactFolderSchema.ParentFolderId,
				ContactFolderSchema.DisplayName,
				ContactFolderSchema.Contacts,
				ContactFolderSchema.ChildFolders
			});
			ContactFolderSchema.AllContactFolderProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.AllEntityProperties.Union(ContactFolderSchema.DeclaredContactFolderProperties)));
			ContactFolderSchema.DefaultContactFolderProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.DefaultEntityProperties)
			{
				ContactFolderSchema.ParentFolderId,
				ContactFolderSchema.DisplayName
			});
			ContactFolderSchema.MandatoryContactFolderCreationProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				FolderSchema.DisplayName
			});
			ContactFolderSchema.ContactFolderSchemaInstance = new LazyMember<ContactFolderSchema>(() => new ContactFolderSchema());
		}

		// Token: 0x040033D6 RID: 13270
		public static readonly PropertyDefinition ParentFolderId;

		// Token: 0x040033D7 RID: 13271
		public static readonly PropertyDefinition DisplayName;

		// Token: 0x040033D8 RID: 13272
		public static readonly PropertyDefinition Contacts;

		// Token: 0x040033D9 RID: 13273
		public static readonly PropertyDefinition ChildFolders;

		// Token: 0x040033DA RID: 13274
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredContactFolderProperties;

		// Token: 0x040033DB RID: 13275
		public static readonly ReadOnlyCollection<PropertyDefinition> AllContactFolderProperties;

		// Token: 0x040033DC RID: 13276
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultContactFolderProperties;

		// Token: 0x040033DD RID: 13277
		public static readonly ReadOnlyCollection<PropertyDefinition> MandatoryContactFolderCreationProperties;

		// Token: 0x040033DE RID: 13278
		private static readonly LazyMember<ContactFolderSchema> ContactFolderSchemaInstance;
	}
}
