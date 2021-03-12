using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E51 RID: 3665
	internal class EntitySchema : Schema
	{
		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06005E25 RID: 24101 RVA: 0x00125B2C File Offset: 0x00123D2C
		public static EntitySchema SchemaInstance
		{
			get
			{
				return EntitySchema.EntitySchemaInstance.Member;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06005E26 RID: 24102 RVA: 0x00125B38 File Offset: 0x00123D38
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return Entity.EdmEntityType;
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06005E27 RID: 24103 RVA: 0x00125B3F File Offset: 0x00123D3F
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return EntitySchema.DeclaredEntityProperties;
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06005E28 RID: 24104 RVA: 0x00125B46 File Offset: 0x00123D46
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return EntitySchema.AllEntityProperties;
			}
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06005E29 RID: 24105 RVA: 0x00125B4D File Offset: 0x00123D4D
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return EntitySchema.DefaultEntityProperties;
			}
		}

		// Token: 0x06005E2B RID: 24107 RVA: 0x00125BA8 File Offset: 0x00123DA8
		// Note: this type is marked as 'beforefieldinit'.
		static EntitySchema()
		{
			PropertyDefinition propertyDefinition = new PropertyDefinition("Id", typeof(string));
			propertyDefinition.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition.EwsPropertyProvider = new IdPropertyProvider();
			PropertyDefinition propertyDefinition2 = propertyDefinition;
			DataEntityPropertyProvider<IEntity> dataEntityPropertyProvider = new DataEntityPropertyProvider<IEntity>("Id");
			dataEntityPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, IEntity d)
			{
				e.Id = EwsIdConverter.EwsIdToODataId(d.Id);
			};
			dataEntityPropertyProvider.Setter = delegate(Entity e, PropertyDefinition ep, IEntity d)
			{
				d.Id = EwsIdConverter.ODataIdToEwsId(e.Id);
			};
			dataEntityPropertyProvider.QueryConstantBuilder = ((object o) => Expression.Constant(EwsIdConverter.ODataIdToEwsId((string)o)));
			propertyDefinition2.DataEntityPropertyProvider = dataEntityPropertyProvider;
			PropertyDefinition propertyDefinition3 = propertyDefinition;
			SimpleDirectoryPropertyProvider simpleDirectoryPropertyProvider = new SimpleDirectoryPropertyProvider(ADRecipientSchema.PrimarySmtpAddress);
			simpleDirectoryPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, ADRawEntry d, ADPropertyDefinition dp)
			{
				e.Id = d[dp].ToString();
			};
			propertyDefinition3.ADDriverPropertyProvider = simpleDirectoryPropertyProvider;
			EntitySchema.Id = propertyDefinition;
			EntitySchema.EntitySchemaInstance = new LazyMember<EntitySchema>(() => new EntitySchema());
			EntitySchema.DeclaredEntityProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				EntitySchema.Id
			});
			EntitySchema.AllEntityProperties = EntitySchema.DeclaredEntityProperties;
			EntitySchema.DefaultEntityProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				EntitySchema.Id
			});
		}

		// Token: 0x040032F7 RID: 13047
		public static readonly PropertyDefinition Id;

		// Token: 0x040032F8 RID: 13048
		private static readonly LazyMember<EntitySchema> EntitySchemaInstance;

		// Token: 0x040032F9 RID: 13049
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredEntityProperties;

		// Token: 0x040032FA RID: 13050
		public static readonly ReadOnlyCollection<PropertyDefinition> AllEntityProperties;

		// Token: 0x040032FB RID: 13051
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultEntityProperties;
	}
}
