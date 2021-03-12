using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000080 RID: 128
	public abstract class Attachment<TSchema> : Entity<TSchema>, IAttachment, IEntity, IPropertyChangeTracker<PropertyDefinition> where TSchema : AttachmentSchema, new()
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000069EC File Offset: 0x00004BEC
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00006A14 File Offset: 0x00004C14
		public string ContentType
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<string>(schema.ContentTypeProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<string>(schema.ContentTypeProperty, value);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00006A3C File Offset: 0x00004C3C
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00006A64 File Offset: 0x00004C64
		public bool IsInline
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<bool>(schema.IsInlineProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<bool>(schema.IsInlineProperty, value);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00006A8C File Offset: 0x00004C8C
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public ExDateTime LastModifiedTime
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<ExDateTime>(schema.LastModifiedTimeProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<ExDateTime>(schema.LastModifiedTimeProperty, value);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00006ADC File Offset: 0x00004CDC
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00006B04 File Offset: 0x00004D04
		public string Name
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<string>(schema.NameProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<string>(schema.NameProperty, value);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00006B2C File Offset: 0x00004D2C
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00006B54 File Offset: 0x00004D54
		public long Size
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<long>(schema.SizeProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<long>(schema.SizeProperty, value);
			}
		}

		// Token: 0x02000081 RID: 129
		public new static class Accessors
		{
			// Token: 0x06000380 RID: 896 RVA: 0x00006BDC File Offset: 0x00004DDC
			// Note: this type is marked as 'beforefieldinit'.
			static Accessors()
			{
				TSchema schemaInstance = SchematizedObject<TSchema>.SchemaInstance;
				Attachment<TSchema>.Accessors.ContentType = new EntityPropertyAccessor<Attachment<TSchema>, string>(schemaInstance.ContentTypeProperty, (Attachment<TSchema> attachment) => attachment.ContentType, delegate(Attachment<TSchema> attachment, string contentType)
				{
					attachment.ContentType = contentType;
				});
				TSchema schemaInstance2 = SchematizedObject<TSchema>.SchemaInstance;
				Attachment<TSchema>.Accessors.IsInline = new EntityPropertyAccessor<Attachment<TSchema>, bool>(schemaInstance2.IsInlineProperty, (Attachment<TSchema> attachment) => attachment.IsInline, delegate(Attachment<TSchema> attachment, bool isInline)
				{
					attachment.IsInline = isInline;
				});
				TSchema schemaInstance3 = SchematizedObject<TSchema>.SchemaInstance;
				Attachment<TSchema>.Accessors.LastModifiedTime = new EntityPropertyAccessor<Attachment<TSchema>, ExDateTime>(schemaInstance3.LastModifiedTimeProperty, (Attachment<TSchema> attachment) => attachment.LastModifiedTime, delegate(Attachment<TSchema> attachment, ExDateTime time)
				{
					attachment.LastModifiedTime = time;
				});
				TSchema schemaInstance4 = SchematizedObject<TSchema>.SchemaInstance;
				Attachment<TSchema>.Accessors.Name = new EntityPropertyAccessor<Attachment<TSchema>, string>(schemaInstance4.NameProperty, (Attachment<TSchema> attachment) => attachment.Name, delegate(Attachment<TSchema> attachment, string name)
				{
					attachment.Name = name;
				});
				TSchema schemaInstance5 = SchematizedObject<TSchema>.SchemaInstance;
				Attachment<TSchema>.Accessors.Size = new EntityPropertyAccessor<Attachment<TSchema>, long>(schemaInstance5.SizeProperty, (Attachment<TSchema> attachment) => attachment.Size, delegate(Attachment<TSchema> attachment, long size)
				{
					attachment.Size = size;
				});
			}

			// Token: 0x040001AB RID: 427
			public static readonly EntityPropertyAccessor<Attachment<TSchema>, string> ContentType;

			// Token: 0x040001AC RID: 428
			public static readonly EntityPropertyAccessor<Attachment<TSchema>, bool> IsInline;

			// Token: 0x040001AD RID: 429
			public static readonly EntityPropertyAccessor<Attachment<TSchema>, ExDateTime> LastModifiedTime;

			// Token: 0x040001AE RID: 430
			public static readonly EntityPropertyAccessor<Attachment<TSchema>, string> Name;

			// Token: 0x040001AF RID: 431
			public static readonly EntityPropertyAccessor<Attachment<TSchema>, long> Size;
		}
	}
}
