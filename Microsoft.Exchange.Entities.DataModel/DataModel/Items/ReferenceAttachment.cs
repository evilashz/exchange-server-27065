using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000094 RID: 148
	public class ReferenceAttachment : Attachment<ReferenceAttachmentSchema>, IReferenceAttachment, IAttachment, IEntity, IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00007244 File Offset: 0x00005444
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00007257 File Offset: 0x00005457
		public string PathName
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.PathNameProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.PathNameProperty, value);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000726B File Offset: 0x0000546B
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000727E File Offset: 0x0000547E
		public string ProviderEndpointUrl
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.ProviderEndpointUrlProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.ProviderEndpointUrlProperty, value);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00007292 File Offset: 0x00005492
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x000072A5 File Offset: 0x000054A5
		public string ProviderType
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.ProviderTypeProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.ProviderTypeProperty, value);
			}
		}

		// Token: 0x02000095 RID: 149
		public new static class Accessors
		{
			// Token: 0x040001DC RID: 476
			public static readonly EntityPropertyAccessor<ReferenceAttachment, string> PathName = new EntityPropertyAccessor<ReferenceAttachment, string>(SchematizedObject<ReferenceAttachmentSchema>.SchemaInstance.PathNameProperty, (ReferenceAttachment referenceAttachment) => referenceAttachment.PathName, delegate(ReferenceAttachment referenceAttachment, string pathName)
			{
				referenceAttachment.PathName = pathName;
			});

			// Token: 0x040001DD RID: 477
			public static readonly EntityPropertyAccessor<ReferenceAttachment, string> ProviderEndpointUrl = new EntityPropertyAccessor<ReferenceAttachment, string>(SchematizedObject<ReferenceAttachmentSchema>.SchemaInstance.ProviderEndpointUrlProperty, (ReferenceAttachment referenceAttachment) => referenceAttachment.ProviderEndpointUrl, delegate(ReferenceAttachment referenceAttachment, string providerEndpointUrl)
			{
				referenceAttachment.ProviderEndpointUrl = providerEndpointUrl;
			});

			// Token: 0x040001DE RID: 478
			public static readonly EntityPropertyAccessor<ReferenceAttachment, string> ProviderType = new EntityPropertyAccessor<ReferenceAttachment, string>(SchematizedObject<ReferenceAttachmentSchema>.SchemaInstance.ProviderTypeProperty, (ReferenceAttachment referenceAttachment) => referenceAttachment.ProviderType, delegate(ReferenceAttachment referenceAttachment, string providerType)
			{
				referenceAttachment.ProviderType = providerType;
			});
		}
	}
}
