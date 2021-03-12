using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E9E RID: 3742
	internal static class DataEntityObjectFactory
	{
		// Token: 0x06006171 RID: 24945 RVA: 0x0012FD04 File Offset: 0x0012DF04
		public static TEntity CreateEntity<TEntity>(object dataEntity) where TEntity : Entity
		{
			ArgumentValidator.ThrowIfNull("dataEntity", dataEntity);
			DataEntityObjectFactory.DataEntityTypeMapEntry dataEntityTypeMapEntry = DataEntityObjectFactory.map.FirstOrDefault((DataEntityObjectFactory.DataEntityTypeMapEntry x) => x.DataEntityType.Equals(dataEntity.GetType()));
			if (dataEntityTypeMapEntry == null)
			{
				throw new NotSupportedException(string.Format("Service object type {0} not suppported", dataEntity.GetType()));
			}
			return (TEntity)((object)dataEntityTypeMapEntry.EntityCreator());
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x0012FD94 File Offset: 0x0012DF94
		public static TDataEntity CreateDataEntity<TDataEntity>(Entity entityObject) where TDataEntity : class
		{
			ArgumentValidator.ThrowIfNull("entityObject", entityObject);
			DataEntityObjectFactory.DataEntityTypeMapEntry dataEntityTypeMapEntry = DataEntityObjectFactory.map.FirstOrDefault((DataEntityObjectFactory.DataEntityTypeMapEntry x) => x.EntityType.Equals(entityObject.GetType()));
			if (dataEntityTypeMapEntry == null)
			{
				throw new NotSupportedException(string.Format("Entity type {0} not suppported", entityObject.GetType()));
			}
			return (TDataEntity)((object)dataEntityTypeMapEntry.DataEntityCreator());
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x0012FE04 File Offset: 0x0012E004
		public static TDataEntity CreateAndSetPropertiesOnDataEntityForCreate<TDataEntity>(Entity entityObject) where TDataEntity : class
		{
			TDataEntity tdataEntity = DataEntityObjectFactory.CreateDataEntity<TDataEntity>(entityObject);
			foreach (PropertyDefinition propertyDefinition in entityObject.PropertyBag.GetProperties())
			{
				if (propertyDefinition.Flags.HasFlag(PropertyDefinitionFlags.CanCreate))
				{
					propertyDefinition.DataEntityPropertyProvider.SetPropertyToDataSource(entityObject, propertyDefinition, tdataEntity);
				}
			}
			return tdataEntity;
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x0012FE64 File Offset: 0x0012E064
		public static TDataEntity CreateAndSetPropertiesOnDataEntityForUpdate<TDataEntity>(Entity entityObject) where TDataEntity : class
		{
			TDataEntity tdataEntity = DataEntityObjectFactory.CreateDataEntity<TDataEntity>(entityObject);
			foreach (PropertyDefinition propertyDefinition in entityObject.PropertyBag.GetProperties())
			{
				if (propertyDefinition.Flags.HasFlag(PropertyDefinitionFlags.CanUpdate))
				{
					propertyDefinition.DataEntityPropertyProvider.SetPropertyToDataSource(entityObject, propertyDefinition, tdataEntity);
				}
			}
			return tdataEntity;
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x0012FEF0 File Offset: 0x0012E0F0
		// Note: this type is marked as 'beforefieldinit'.
		static DataEntityObjectFactory()
		{
			DataEntityObjectFactory.DataEntityTypeMapEntry[] array = new DataEntityObjectFactory.DataEntityTypeMapEntry[3];
			array[0] = new DataEntityObjectFactory.DataEntityTypeMapEntry(typeof(Calendar), typeof(Calendar), () => new Calendar(), () => new Calendar());
			array[1] = new DataEntityObjectFactory.DataEntityTypeMapEntry(typeof(CalendarGroup), typeof(CalendarGroup), () => new CalendarGroup(), () => new CalendarGroup());
			array[2] = new DataEntityObjectFactory.DataEntityTypeMapEntry(typeof(Event), typeof(Event), () => new Event(), () => new Event());
			DataEntityObjectFactory.map = array;
		}

		// Token: 0x040034BD RID: 13501
		private static readonly DataEntityObjectFactory.DataEntityTypeMapEntry[] map;

		// Token: 0x02000E9F RID: 3743
		private class DataEntityTypeMapEntry
		{
			// Token: 0x0600617C RID: 24956 RVA: 0x0013000C File Offset: 0x0012E20C
			public DataEntityTypeMapEntry(Type dataEntityType, Type entityType, Func<object> dataEntityCreator, Func<Entity> entityCreator)
			{
				ArgumentValidator.ThrowIfNull("dataEntityType", dataEntityType);
				ArgumentValidator.ThrowIfNull("entityType", entityType);
				ArgumentValidator.ThrowIfNull("dataEntityCreator", dataEntityCreator);
				ArgumentValidator.ThrowIfNull("entityCreator", entityCreator);
				this.DataEntityType = dataEntityType;
				this.EntityType = entityType;
				this.DataEntityCreator = dataEntityCreator;
				this.EntityCreator = entityCreator;
			}

			// Token: 0x17001664 RID: 5732
			// (get) Token: 0x0600617D RID: 24957 RVA: 0x00130069 File Offset: 0x0012E269
			// (set) Token: 0x0600617E RID: 24958 RVA: 0x00130071 File Offset: 0x0012E271
			public Type DataEntityType { get; private set; }

			// Token: 0x17001665 RID: 5733
			// (get) Token: 0x0600617F RID: 24959 RVA: 0x0013007A File Offset: 0x0012E27A
			// (set) Token: 0x06006180 RID: 24960 RVA: 0x00130082 File Offset: 0x0012E282
			public Type EntityType { get; private set; }

			// Token: 0x17001666 RID: 5734
			// (get) Token: 0x06006181 RID: 24961 RVA: 0x0013008B File Offset: 0x0012E28B
			// (set) Token: 0x06006182 RID: 24962 RVA: 0x00130093 File Offset: 0x0012E293
			public Func<object> DataEntityCreator { get; private set; }

			// Token: 0x17001667 RID: 5735
			// (get) Token: 0x06006183 RID: 24963 RVA: 0x0013009C File Offset: 0x0012E29C
			// (set) Token: 0x06006184 RID: 24964 RVA: 0x001300A4 File Offset: 0x0012E2A4
			public Func<Entity> EntityCreator { get; private set; }
		}
	}
}
