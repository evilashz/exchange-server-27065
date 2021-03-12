using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E5D RID: 3677
	internal class FileAttachmentSchema : AttachmentSchema
	{
		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06005F1B RID: 24347 RVA: 0x00128BBF File Offset: 0x00126DBF
		public new static FileAttachmentSchema SchemaInstance
		{
			get
			{
				return FileAttachmentSchema.FileAttachmentSchemaInstance.Member;
			}
		}

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06005F1C RID: 24348 RVA: 0x00128BCB File Offset: 0x00126DCB
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return FileAttachment.EdmEntityType;
			}
		}

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06005F1D RID: 24349 RVA: 0x00128BD2 File Offset: 0x00126DD2
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return FileAttachmentSchema.DeclaredFileAttachmentProperties;
			}
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x06005F1E RID: 24350 RVA: 0x00128BD9 File Offset: 0x00126DD9
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return FileAttachmentSchema.AllFileAttachmentProperties;
			}
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06005F1F RID: 24351 RVA: 0x00128BE0 File Offset: 0x00126DE0
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return FileAttachmentSchema.DefaultFileAttachmentProperties;
			}
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x00128C80 File Offset: 0x00126E80
		// Note: this type is marked as 'beforefieldinit'.
		static FileAttachmentSchema()
		{
			PropertyDefinition propertyDefinition = new PropertyDefinition("ContentId", typeof(string));
			propertyDefinition.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate);
			propertyDefinition.EdmType = EdmCoreModel.Instance.GetString(true);
			PropertyDefinition propertyDefinition2 = propertyDefinition;
			GenericPropertyProvider<FileAttachmentType> genericPropertyProvider = new GenericPropertyProvider<FileAttachmentType>();
			genericPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, FileAttachmentType a)
			{
				e[ep] = a.ContentId;
			};
			genericPropertyProvider.Setter = delegate(Entity e, PropertyDefinition ep, FileAttachmentType a)
			{
				a.ContentId = (string)e[ep];
			};
			propertyDefinition2.EwsPropertyProvider = genericPropertyProvider;
			FileAttachmentSchema.ContentId = propertyDefinition;
			PropertyDefinition propertyDefinition3 = new PropertyDefinition("ContentLocation", typeof(string));
			propertyDefinition3.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate);
			propertyDefinition3.EdmType = EdmCoreModel.Instance.GetString(true);
			PropertyDefinition propertyDefinition4 = propertyDefinition3;
			GenericPropertyProvider<FileAttachmentType> genericPropertyProvider2 = new GenericPropertyProvider<FileAttachmentType>();
			genericPropertyProvider2.Getter = delegate(Entity e, PropertyDefinition ep, FileAttachmentType a)
			{
				e[ep] = a.ContentLocation;
			};
			genericPropertyProvider2.Setter = delegate(Entity e, PropertyDefinition ep, FileAttachmentType a)
			{
				a.ContentLocation = (string)e[ep];
			};
			propertyDefinition4.EwsPropertyProvider = genericPropertyProvider2;
			FileAttachmentSchema.ContentLocation = propertyDefinition3;
			PropertyDefinition propertyDefinition5 = new PropertyDefinition("ContentBytes", typeof(byte[]));
			propertyDefinition5.Flags = PropertyDefinitionFlags.CanCreate;
			propertyDefinition5.EdmType = EdmCoreModel.Instance.GetBinary(true);
			PropertyDefinition propertyDefinition6 = propertyDefinition5;
			GenericPropertyProvider<FileAttachmentType> genericPropertyProvider3 = new GenericPropertyProvider<FileAttachmentType>();
			genericPropertyProvider3.Getter = delegate(Entity e, PropertyDefinition ep, FileAttachmentType a)
			{
				e[ep] = a.Content;
			};
			genericPropertyProvider3.Setter = delegate(Entity e, PropertyDefinition ep, FileAttachmentType a)
			{
				a.Content = (byte[])e[ep];
			};
			propertyDefinition6.EwsPropertyProvider = genericPropertyProvider3;
			FileAttachmentSchema.ContentBytes = propertyDefinition5;
			PropertyDefinition propertyDefinition7 = new PropertyDefinition("IsContactPhoto", typeof(bool));
			propertyDefinition7.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate);
			propertyDefinition7.EdmType = EdmCoreModel.Instance.GetBoolean(false);
			PropertyDefinition propertyDefinition8 = propertyDefinition7;
			GenericPropertyProvider<FileAttachmentType> genericPropertyProvider4 = new GenericPropertyProvider<FileAttachmentType>();
			genericPropertyProvider4.Getter = delegate(Entity e, PropertyDefinition ep, FileAttachmentType a)
			{
				e[ep] = a.IsContactPhoto;
			};
			genericPropertyProvider4.Setter = delegate(Entity e, PropertyDefinition ep, FileAttachmentType a)
			{
				a.IsContactPhoto = (bool)e[ep];
			};
			propertyDefinition8.EwsPropertyProvider = genericPropertyProvider4;
			FileAttachmentSchema.IsContactPhoto = propertyDefinition7;
			FileAttachmentSchema.DeclaredFileAttachmentProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				FileAttachmentSchema.ContentId,
				FileAttachmentSchema.ContentLocation,
				FileAttachmentSchema.IsContactPhoto,
				FileAttachmentSchema.ContentBytes
			});
			FileAttachmentSchema.AllFileAttachmentProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(AttachmentSchema.AllAttachmentProperties.Union(FileAttachmentSchema.DeclaredFileAttachmentProperties)));
			FileAttachmentSchema.DefaultFileAttachmentProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(AttachmentSchema.DefaultAttachmentProperties)
			{
				FileAttachmentSchema.ContentId,
				FileAttachmentSchema.ContentLocation,
				FileAttachmentSchema.IsContactPhoto,
				FileAttachmentSchema.ContentBytes
			});
			FileAttachmentSchema.FileAttachmentSchemaInstance = new LazyMember<FileAttachmentSchema>(() => new FileAttachmentSchema());
		}

		// Token: 0x04003388 RID: 13192
		public static readonly PropertyDefinition ContentId;

		// Token: 0x04003389 RID: 13193
		public static readonly PropertyDefinition ContentLocation;

		// Token: 0x0400338A RID: 13194
		public static readonly PropertyDefinition ContentBytes;

		// Token: 0x0400338B RID: 13195
		public static readonly PropertyDefinition IsContactPhoto;

		// Token: 0x0400338C RID: 13196
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredFileAttachmentProperties;

		// Token: 0x0400338D RID: 13197
		public static readonly ReadOnlyCollection<PropertyDefinition> AllFileAttachmentProperties;

		// Token: 0x0400338E RID: 13198
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultFileAttachmentProperties;

		// Token: 0x0400338F RID: 13199
		private static readonly LazyMember<FileAttachmentSchema> FileAttachmentSchemaInstance;
	}
}
