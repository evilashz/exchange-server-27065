using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000085 RID: 133
	public class FileAttachment : Attachment<FileAttachmentSchema>, IFileAttachment, IAttachment, IEntity, IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00006E6B File Offset: 0x0000506B
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00006E7E File Offset: 0x0000507E
		public byte[] Content
		{
			get
			{
				return base.GetPropertyValueOrDefault<byte[]>(base.Schema.ContentProperty);
			}
			set
			{
				base.SetPropertyValue<byte[]>(base.Schema.ContentProperty, value);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00006E92 File Offset: 0x00005092
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00006EA5 File Offset: 0x000050A5
		public string ContentId
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.ContentIdProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.ContentIdProperty, value);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00006EB9 File Offset: 0x000050B9
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00006ECC File Offset: 0x000050CC
		public string ContentLocation
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.ContentLocationProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.ContentLocationProperty, value);
			}
		}

		// Token: 0x02000086 RID: 134
		public new static class Accessors
		{
			// Token: 0x040001C2 RID: 450
			public static readonly EntityPropertyAccessor<FileAttachment, byte[]> Content = new EntityPropertyAccessor<FileAttachment, byte[]>(SchematizedObject<FileAttachmentSchema>.SchemaInstance.ContentProperty, (FileAttachment fileAttachment) => fileAttachment.Content, delegate(FileAttachment fileAttachment, byte[] content)
			{
				fileAttachment.Content = content;
			});

			// Token: 0x040001C3 RID: 451
			public static readonly EntityPropertyAccessor<FileAttachment, string> ContentId = new EntityPropertyAccessor<FileAttachment, string>(SchematizedObject<FileAttachmentSchema>.SchemaInstance.ContentIdProperty, (FileAttachment fileAttachment) => fileAttachment.ContentId, delegate(FileAttachment fileAttachment, string contentId)
			{
				fileAttachment.ContentId = contentId;
			});

			// Token: 0x040001C4 RID: 452
			public static readonly EntityPropertyAccessor<FileAttachment, string> ContentLocation = new EntityPropertyAccessor<FileAttachment, string>(SchematizedObject<FileAttachmentSchema>.SchemaInstance.ContentLocationProperty, (FileAttachment fileAttachment) => fileAttachment.ContentLocation, delegate(FileAttachment fileAttachment, string contentLocation)
			{
				fileAttachment.ContentLocation = contentLocation;
			});
		}
	}
}
