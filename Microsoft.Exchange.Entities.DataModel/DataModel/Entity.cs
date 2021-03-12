using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x02000010 RID: 16
	public abstract class Entity<TSchema> : SchematizedObject<TSchema>, IEntity, IPropertyChangeTracker<PropertyDefinition> where TSchema : EntitySchema, new()
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000025E8 File Offset: 0x000007E8
		protected Entity()
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000025F0 File Offset: 0x000007F0
		protected Entity(string id)
		{
			this.Id = id;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002600 File Offset: 0x00000800
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002628 File Offset: 0x00000828
		public string Id
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<string>(schema.IdProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<string>(schema.IdProperty, value);
			}
		}

		// Token: 0x02000011 RID: 17
		public static class Accessors
		{
			// Token: 0x0600003D RID: 61 RVA: 0x00002664 File Offset: 0x00000864
			// Note: this type is marked as 'beforefieldinit'.
			static Accessors()
			{
				TSchema schemaInstance = SchematizedObject<TSchema>.SchemaInstance;
				Entity<TSchema>.Accessors.Id = new EntityPropertyAccessor<IEntity, string>(schemaInstance.IdProperty, (IEntity entity) => entity.Id, delegate(IEntity entity, string s)
				{
					entity.Id = s;
				});
			}

			// Token: 0x04000014 RID: 20
			public static readonly EntityPropertyAccessor<IEntity, string> Id;
		}
	}
}
