using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E5B RID: 3675
	internal class AttachmentSchema : EntitySchema
	{
		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x06005EFE RID: 24318 RVA: 0x0012865E File Offset: 0x0012685E
		public new static AttachmentSchema SchemaInstance
		{
			get
			{
				return AttachmentSchema.AttachmentSchemaInstance.Member;
			}
		}

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x06005EFF RID: 24319 RVA: 0x0012866A File Offset: 0x0012686A
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return Attachment.EdmEntityType;
			}
		}

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x06005F00 RID: 24320 RVA: 0x00128671 File Offset: 0x00126871
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return AttachmentSchema.DeclaredAttachmentProperties;
			}
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x06005F01 RID: 24321 RVA: 0x00128678 File Offset: 0x00126878
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return AttachmentSchema.AllAttachmentProperties;
			}
		}

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06005F02 RID: 24322 RVA: 0x0012867F File Offset: 0x0012687F
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return AttachmentSchema.DefaultAttachmentProperties;
			}
		}

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06005F03 RID: 24323 RVA: 0x00128686 File Offset: 0x00126886
		public override ReadOnlyCollection<PropertyDefinition> MandatoryCreationProperties
		{
			get
			{
				return AttachmentSchema.MandatoryAttachmentCreationProperties;
			}
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x00128748 File Offset: 0x00126948
		// Note: this type is marked as 'beforefieldinit'.
		static AttachmentSchema()
		{
			PropertyDefinition propertyDefinition = new PropertyDefinition("Name", typeof(string));
			propertyDefinition.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate);
			PropertyDefinition propertyDefinition2 = propertyDefinition;
			GenericPropertyProvider<AttachmentType> genericPropertyProvider = new GenericPropertyProvider<AttachmentType>();
			genericPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, AttachmentType a)
			{
				e[ep] = a.Name;
			};
			genericPropertyProvider.Setter = delegate(Entity e, PropertyDefinition ep, AttachmentType a)
			{
				a.Name = (string)e[ep];
			};
			propertyDefinition2.EwsPropertyProvider = genericPropertyProvider;
			AttachmentSchema.Name = propertyDefinition;
			PropertyDefinition propertyDefinition3 = new PropertyDefinition("ContentType", typeof(string));
			propertyDefinition3.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition3.Flags = PropertyDefinitionFlags.CanFilter;
			PropertyDefinition propertyDefinition4 = propertyDefinition3;
			GenericPropertyProvider<AttachmentType> genericPropertyProvider2 = new GenericPropertyProvider<AttachmentType>();
			genericPropertyProvider2.Getter = delegate(Entity e, PropertyDefinition ep, AttachmentType a)
			{
				e[ep] = a.ContentType;
			};
			genericPropertyProvider2.Setter = delegate(Entity e, PropertyDefinition ep, AttachmentType a)
			{
				a.ContentType = (string)e[ep];
			};
			propertyDefinition4.EwsPropertyProvider = genericPropertyProvider2;
			AttachmentSchema.ContentType = propertyDefinition3;
			PropertyDefinition propertyDefinition5 = new PropertyDefinition("Size", typeof(int));
			propertyDefinition5.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate);
			propertyDefinition5.EdmType = EdmCoreModel.Instance.GetInt32(false);
			PropertyDefinition propertyDefinition6 = propertyDefinition5;
			GenericPropertyProvider<AttachmentType> genericPropertyProvider3 = new GenericPropertyProvider<AttachmentType>();
			genericPropertyProvider3.Getter = delegate(Entity e, PropertyDefinition ep, AttachmentType a)
			{
				e[ep] = a.Size;
			};
			genericPropertyProvider3.Setter = delegate(Entity e, PropertyDefinition ep, AttachmentType a)
			{
				a.Size = (int)e[ep];
			};
			propertyDefinition6.EwsPropertyProvider = genericPropertyProvider3;
			AttachmentSchema.Size = propertyDefinition5;
			PropertyDefinition propertyDefinition7 = new PropertyDefinition("IsInline", typeof(bool));
			propertyDefinition7.EdmType = EdmCoreModel.Instance.GetBoolean(false);
			propertyDefinition7.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate);
			PropertyDefinition propertyDefinition8 = propertyDefinition7;
			GenericPropertyProvider<AttachmentType> genericPropertyProvider4 = new GenericPropertyProvider<AttachmentType>();
			genericPropertyProvider4.Getter = delegate(Entity e, PropertyDefinition ep, AttachmentType a)
			{
				e[ep] = a.IsInline;
			};
			genericPropertyProvider4.Setter = delegate(Entity e, PropertyDefinition ep, AttachmentType a)
			{
				a.IsInline = (bool)e[ep];
			};
			propertyDefinition8.EwsPropertyProvider = genericPropertyProvider4;
			AttachmentSchema.IsInline = propertyDefinition7;
			PropertyDefinition propertyDefinition9 = new PropertyDefinition("LastModifiedTime", typeof(DateTimeOffset));
			propertyDefinition9.EdmType = EdmCoreModel.Instance.GetDateTimeOffset(true);
			propertyDefinition9.Flags = PropertyDefinitionFlags.CanFilter;
			PropertyDefinition propertyDefinition10 = propertyDefinition9;
			GenericPropertyProvider<AttachmentType> genericPropertyProvider5 = new GenericPropertyProvider<AttachmentType>();
			genericPropertyProvider5.Getter = delegate(Entity e, PropertyDefinition ep, AttachmentType a)
			{
				e[ep] = new DateTimeOffset(DateTime.Parse(a.LastModifiedTime));
			};
			propertyDefinition10.EwsPropertyProvider = genericPropertyProvider5;
			AttachmentSchema.LastModifiedTime = propertyDefinition9;
			AttachmentSchema.DeclaredAttachmentProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				AttachmentSchema.Name,
				AttachmentSchema.ContentType,
				AttachmentSchema.Size,
				AttachmentSchema.IsInline,
				AttachmentSchema.LastModifiedTime
			});
			AttachmentSchema.AllAttachmentProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.AllEntityProperties.Union(AttachmentSchema.DeclaredAttachmentProperties)));
			AttachmentSchema.DefaultAttachmentProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.DefaultEntityProperties)
			{
				AttachmentSchema.Name,
				AttachmentSchema.ContentType,
				AttachmentSchema.Size,
				AttachmentSchema.IsInline,
				AttachmentSchema.LastModifiedTime
			});
			AttachmentSchema.MandatoryAttachmentCreationProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				AttachmentSchema.Name
			});
			AttachmentSchema.AttachmentSchemaInstance = new LazyMember<AttachmentSchema>(() => new AttachmentSchema());
		}

		// Token: 0x04003373 RID: 13171
		public static readonly PropertyDefinition Name;

		// Token: 0x04003374 RID: 13172
		public static readonly PropertyDefinition ContentType;

		// Token: 0x04003375 RID: 13173
		public static readonly PropertyDefinition Size;

		// Token: 0x04003376 RID: 13174
		public static readonly PropertyDefinition IsInline;

		// Token: 0x04003377 RID: 13175
		public static readonly PropertyDefinition LastModifiedTime;

		// Token: 0x04003378 RID: 13176
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredAttachmentProperties;

		// Token: 0x04003379 RID: 13177
		public static readonly ReadOnlyCollection<PropertyDefinition> AllAttachmentProperties;

		// Token: 0x0400337A RID: 13178
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultAttachmentProperties;

		// Token: 0x0400337B RID: 13179
		public static readonly ReadOnlyCollection<PropertyDefinition> MandatoryAttachmentCreationProperties;

		// Token: 0x0400337C RID: 13180
		private static readonly LazyMember<AttachmentSchema> AttachmentSchemaInstance;
	}
}
