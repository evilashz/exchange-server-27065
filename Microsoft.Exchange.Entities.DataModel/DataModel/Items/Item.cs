using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000045 RID: 69
	public class Item<TSchema> : StorageEntity<TSchema>, IItem, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned where TSchema : ItemSchema, new()
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00003F48 File Offset: 0x00002148
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00003F70 File Offset: 0x00002170
		[NotMapped]
		public List<IAttachment> Attachments
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<List<IAttachment>>(schema.AttachmentsProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<List<IAttachment>>(schema.AttachmentsProperty, value);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00003F98 File Offset: 0x00002198
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00003FC0 File Offset: 0x000021C0
		public ItemBody Body
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<ItemBody>(schema.BodyProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<ItemBody>(schema.BodyProperty, value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00003FE8 File Offset: 0x000021E8
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00004010 File Offset: 0x00002210
		public List<string> Categories
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<List<string>>(schema.CategoriesProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<List<string>>(schema.CategoriesProperty, value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00004038 File Offset: 0x00002238
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00004060 File Offset: 0x00002260
		public ExDateTime DateTimeCreated
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<ExDateTime>(schema.DateTimeCreatedProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<ExDateTime>(schema.DateTimeCreatedProperty, value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00004088 File Offset: 0x00002288
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000040B0 File Offset: 0x000022B0
		public bool HasAttachments
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<bool>(schema.HasAttachmentsProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<bool>(schema.HasAttachmentsProperty, value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000040D8 File Offset: 0x000022D8
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00004100 File Offset: 0x00002300
		public Importance Importance
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<Importance>(schema.ImportanceProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<Importance>(schema.ImportanceProperty, value);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00004128 File Offset: 0x00002328
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00004150 File Offset: 0x00002350
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00004178 File Offset: 0x00002378
		// (set) Token: 0x06000188 RID: 392 RVA: 0x000041A0 File Offset: 0x000023A0
		public string Preview
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<string>(schema.PreviewProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<string>(schema.PreviewProperty, value);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000041C8 File Offset: 0x000023C8
		// (set) Token: 0x0600018A RID: 394 RVA: 0x000041F0 File Offset: 0x000023F0
		public ExDateTime ReceivedTime
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<ExDateTime>(schema.ReceivedTimeProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<ExDateTime>(schema.ReceivedTimeProperty, value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00004218 File Offset: 0x00002418
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00004240 File Offset: 0x00002440
		public Sensitivity Sensitivity
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<Sensitivity>(schema.SensitivityProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<Sensitivity>(schema.SensitivityProperty, value);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00004268 File Offset: 0x00002468
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00004290 File Offset: 0x00002490
		public string Subject
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<string>(schema.SubjectProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<string>(schema.SubjectProperty, value);
			}
		}

		// Token: 0x02000046 RID: 70
		public new static class Accessors
		{
			// Token: 0x06000190 RID: 400 RVA: 0x0000437C File Offset: 0x0000257C
			// Note: this type is marked as 'beforefieldinit'.
			static Accessors()
			{
				TSchema schemaInstance = SchematizedObject<TSchema>.SchemaInstance;
				Item<TSchema>.Accessors.Attachments = new EntityPropertyAccessor<IItem, List<IAttachment>>(schemaInstance.AttachmentsProperty, (IItem item) => item.Attachments, delegate(IItem item, List<IAttachment> list)
				{
					item.Attachments = list;
				});
				TSchema schemaInstance2 = SchematizedObject<TSchema>.SchemaInstance;
				Item<TSchema>.Accessors.Body = new EntityPropertyAccessor<IItem, ItemBody>(schemaInstance2.BodyProperty, (IItem item) => item.Body, delegate(IItem item, ItemBody body)
				{
					item.Body = body;
				});
				TSchema schemaInstance3 = SchematizedObject<TSchema>.SchemaInstance;
				Item<TSchema>.Accessors.Categories = new EntityPropertyAccessor<IItem, List<string>>(schemaInstance3.CategoriesProperty, (IItem item) => item.Categories, delegate(IItem item, List<string> list)
				{
					item.Categories = list;
				});
				TSchema schemaInstance4 = SchematizedObject<TSchema>.SchemaInstance;
				Item<TSchema>.Accessors.DateTimeCreated = new EntityPropertyAccessor<IItem, ExDateTime>(schemaInstance4.DateTimeCreatedProperty, (IItem item) => item.DateTimeCreated, delegate(IItem item, ExDateTime time)
				{
					item.DateTimeCreated = time;
				});
				TSchema schemaInstance5 = SchematizedObject<TSchema>.SchemaInstance;
				Item<TSchema>.Accessors.HasAttachments = new EntityPropertyAccessor<IItem, bool>(schemaInstance5.HasAttachmentsProperty, (IItem item) => item.HasAttachments, delegate(IItem item, bool b)
				{
					item.HasAttachments = b;
				});
				TSchema schemaInstance6 = SchematizedObject<TSchema>.SchemaInstance;
				Item<TSchema>.Accessors.Importance = new EntityPropertyAccessor<IItem, Importance>(schemaInstance6.ImportanceProperty, (IItem item) => item.Importance, delegate(IItem item, Importance importance)
				{
					item.Importance = importance;
				});
				TSchema schemaInstance7 = SchematizedObject<TSchema>.SchemaInstance;
				Item<TSchema>.Accessors.LastModifiedTime = new EntityPropertyAccessor<IItem, ExDateTime>(schemaInstance7.LastModifiedTimeProperty, (IItem item) => item.LastModifiedTime, delegate(IItem item, ExDateTime time)
				{
					item.LastModifiedTime = time;
				});
				TSchema schemaInstance8 = SchematizedObject<TSchema>.SchemaInstance;
				Item<TSchema>.Accessors.Preview = new EntityPropertyAccessor<IItem, string>(schemaInstance8.PreviewProperty, (IItem item) => item.Preview, delegate(IItem item, string s)
				{
					item.Preview = s;
				});
				TSchema schemaInstance9 = SchematizedObject<TSchema>.SchemaInstance;
				Item<TSchema>.Accessors.ReceivedTime = new EntityPropertyAccessor<IItem, ExDateTime>(schemaInstance9.ReceivedTimeProperty, (IItem item) => item.ReceivedTime, delegate(IItem item, ExDateTime time)
				{
					item.ReceivedTime = time;
				});
				TSchema schemaInstance10 = SchematizedObject<TSchema>.SchemaInstance;
				Item<TSchema>.Accessors.Sensitivity = new EntityPropertyAccessor<IItem, Sensitivity>(schemaInstance10.SensitivityProperty, (IItem item) => item.Sensitivity, delegate(IItem item, Sensitivity sensitivity)
				{
					item.Sensitivity = sensitivity;
				});
				TSchema schemaInstance11 = SchematizedObject<TSchema>.SchemaInstance;
				Item<TSchema>.Accessors.Subject = new EntityPropertyAccessor<IItem, string>(schemaInstance11.SubjectProperty, (IItem item) => item.Subject, delegate(IItem item, string s)
				{
					item.Subject = s;
				});
			}

			// Token: 0x0400008D RID: 141
			public static readonly EntityPropertyAccessor<IItem, List<IAttachment>> Attachments;

			// Token: 0x0400008E RID: 142
			public static readonly EntityPropertyAccessor<IItem, ItemBody> Body;

			// Token: 0x0400008F RID: 143
			public static readonly EntityPropertyAccessor<IItem, List<string>> Categories;

			// Token: 0x04000090 RID: 144
			public static readonly EntityPropertyAccessor<IItem, ExDateTime> DateTimeCreated;

			// Token: 0x04000091 RID: 145
			public static readonly EntityPropertyAccessor<IItem, bool> HasAttachments;

			// Token: 0x04000092 RID: 146
			public static readonly EntityPropertyAccessor<IItem, Importance> Importance;

			// Token: 0x04000093 RID: 147
			public static readonly EntityPropertyAccessor<IItem, ExDateTime> LastModifiedTime;

			// Token: 0x04000094 RID: 148
			public static readonly EntityPropertyAccessor<IItem, string> Preview;

			// Token: 0x04000095 RID: 149
			public static readonly EntityPropertyAccessor<IItem, ExDateTime> ReceivedTime;

			// Token: 0x04000096 RID: 150
			public static readonly EntityPropertyAccessor<IItem, Sensitivity> Sensitivity;

			// Token: 0x04000097 RID: 151
			public static readonly EntityPropertyAccessor<IItem, string> Subject;
		}
	}
}
