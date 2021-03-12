using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x0200019D RID: 413
	internal class EntityDataObject : PropertyBase, IServerDataObject, IPropertyContainer, IProperty, IDataObjectGeneratorContainer
	{
		// Token: 0x060011DC RID: 4572 RVA: 0x0006178A File Offset: 0x0005F98A
		public EntityDataObject(IList<IProperty> propertyFromSchemaLinkId, AirSyncEntitySchemaState airSyncEntitySchemaState)
		{
			base.State = PropertyState.Modified;
			this.propertyFromSchemaLinkId = propertyFromSchemaLinkId;
			this.SchemaState = airSyncEntitySchemaState;
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x000617A7 File Offset: 0x0005F9A7
		public IList<IProperty> Children
		{
			get
			{
				return this.propertyFromSchemaLinkId;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x000617AF File Offset: 0x0005F9AF
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x000617B7 File Offset: 0x0005F9B7
		public IDataObjectGenerator SchemaState
		{
			get
			{
				return this.airSyncEntitySchemaState;
			}
			set
			{
				this.airSyncEntitySchemaState = (AirSyncEntitySchemaState)value;
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x000617C8 File Offset: 0x0005F9C8
		public void Bind(object item)
		{
			Item item2 = item as Item;
			IItem item3;
			if (item2 != null)
			{
				item3 = EntitySyncItem.GetItem(item2);
			}
			else
			{
				item3 = (item as IItem);
			}
			if (item == null)
			{
				throw new ArgumentNullException("Item is null!");
			}
			foreach (IProperty property in this.propertyFromSchemaLinkId)
			{
				EntityProperty entityProperty = (EntityProperty)property;
				entityProperty.Bind(item3);
			}
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00061844 File Offset: 0x0005FA44
		public bool CanConvertItemClassUsingCurrentSchema(string itemClass)
		{
			SinglePropertyBag propertyBag = new SinglePropertyBag(StoreObjectSchema.ItemClass, itemClass);
			return EvaluatableFilter.Evaluate(this.airSyncEntitySchemaState.SupportedClassFilter, propertyBag);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0006186E File Offset: 0x0005FA6E
		public PropertyDefinition[] GetPrefetchProperties()
		{
			return null;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00061871 File Offset: 0x0005FA71
		public void SetChangedProperties(PropertyDefinition[] changedProperties)
		{
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00061873 File Offset: 0x0005FA73
		public IProperty GetPropBySchemaLinkId(int id)
		{
			return this.propertyFromSchemaLinkId[id];
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00061884 File Offset: 0x0005FA84
		public override void Unbind()
		{
			foreach (IProperty property in this.propertyFromSchemaLinkId)
			{
				EntityProperty entityProperty = (EntityProperty)property;
				entityProperty.Unbind();
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x000618D8 File Offset: 0x0005FAD8
		public void SetCopyDestination(IPropertyContainer dstPropertyContainer)
		{
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x000618DC File Offset: 0x0005FADC
		public override void CopyFrom(IProperty srcRootProperty)
		{
			IPropertyContainer propertyContainer = srcRootProperty as IPropertyContainer;
			if (propertyContainer == null)
			{
				throw new ArgumentNullException("srcPropertyContainer");
			}
			propertyContainer.SetCopyDestination(this);
			foreach (IProperty property in propertyContainer.Children)
			{
				if (property.State != PropertyState.NotSupported && property.State != PropertyState.Unmodified && property.State != PropertyState.SetToDefault)
				{
					EntityProperty entityProperty = (EntityProperty)this.propertyFromSchemaLinkId[property.SchemaLinkId];
					if (entityProperty.Type != PropertyType.ReadOnly && entityProperty.State != PropertyState.NotSupported)
					{
						entityProperty.CopyFrom(property);
					}
				}
			}
		}

		// Token: 0x04000B45 RID: 2885
		private AirSyncEntitySchemaState airSyncEntitySchemaState;

		// Token: 0x04000B46 RID: 2886
		private IList<IProperty> propertyFromSchemaLinkId;
	}
}
