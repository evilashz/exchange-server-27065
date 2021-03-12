using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000213 RID: 531
	[Serializable]
	internal class XsoDataObject : PropertyBase, IServerDataObject, IPropertyContainer, IProperty, IDataObjectGeneratorContainer
	{
		// Token: 0x06001461 RID: 5217 RVA: 0x00075EBC File Offset: 0x000740BC
		public XsoDataObject(IList<IProperty> propertyFromSchemaLinkId, IXsoDataObjectGenerator schemaState, QueryFilter supportedClassFilter)
		{
			if (propertyFromSchemaLinkId == null)
			{
				throw new ArgumentNullException("propertyFromSchemaLinkId");
			}
			int num = 0;
			foreach (IProperty property in propertyFromSchemaLinkId)
			{
				XsoProperty xsoProperty = (XsoProperty)property;
				PropertyDefinition[] array = xsoProperty.GetPrefetchProperties();
				if (array != null)
				{
					num += xsoProperty.GetPrefetchProperties().Length;
				}
			}
			this.prefetchProperties = new PropertyDefinition[num];
			int num2 = 0;
			foreach (IProperty property2 in propertyFromSchemaLinkId)
			{
				XsoProperty xsoProperty2 = (XsoProperty)property2;
				PropertyDefinition[] array2 = xsoProperty2.GetPrefetchProperties();
				if (array2 != null)
				{
					array2.CopyTo(this.prefetchProperties, num2);
					num2 += array2.Length;
				}
			}
			this.propertyFromSchemaLinkId = propertyFromSchemaLinkId;
			this.schemaState = schemaState;
			this.supportedClassFilter = supportedClassFilter;
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00075FBC File Offset: 0x000741BC
		public IList<IProperty> Children
		{
			get
			{
				return this.propertyFromSchemaLinkId;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x00075FC4 File Offset: 0x000741C4
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x00075FCC File Offset: 0x000741CC
		public IDataObjectGenerator SchemaState
		{
			get
			{
				return this.schemaState;
			}
			set
			{
				this.schemaState = (value as IXsoDataObjectGenerator);
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x00075FDA File Offset: 0x000741DA
		public QueryFilter SupportedClassFilter
		{
			get
			{
				return this.supportedClassFilter;
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00075FE4 File Offset: 0x000741E4
		public void Bind(object item)
		{
			this.areChildPropertyStatesSet = false;
			foreach (IProperty property in this.propertyFromSchemaLinkId)
			{
				XsoProperty xsoProperty = (XsoProperty)property;
				xsoProperty.Bind((StoreObject)item);
			}
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00076044 File Offset: 0x00074244
		public override void Unbind()
		{
			try
			{
				this.areChildPropertyStatesSet = false;
				foreach (IProperty property in this.propertyFromSchemaLinkId)
				{
					XsoProperty xsoProperty = (XsoProperty)property;
					xsoProperty.Unbind();
				}
			}
			finally
			{
				base.Unbind();
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x000760B4 File Offset: 0x000742B4
		public bool CanConvertItemClassUsingCurrentSchema(string itemClass)
		{
			SinglePropertyBag propertyBag = new SinglePropertyBag(StoreObjectSchema.ItemClass, itemClass);
			return EvaluatableFilter.Evaluate(this.SupportedClassFilter, propertyBag);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x000760D9 File Offset: 0x000742D9
		public PropertyDefinition[] GetPrefetchProperties()
		{
			return this.prefetchProperties;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x000760E4 File Offset: 0x000742E4
		public void SetCopyDestination(IPropertyContainer dstPropertyContainer)
		{
			if (!this.areChildPropertyStatesSet)
			{
				foreach (IProperty property in this.propertyFromSchemaLinkId)
				{
					XsoProperty xsoProperty = (XsoProperty)property;
					xsoProperty.ComputePropertyState();
				}
			}
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00076140 File Offset: 0x00074340
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
				XsoProperty xsoProperty = this.GetPropBySchemaLinkId(property.SchemaLinkId) as XsoProperty;
				if (xsoProperty.Type != PropertyType.ReadOnly && xsoProperty.State != PropertyState.NotSupported)
				{
					if (xsoProperty is IDataObjectGeneratorContainer)
					{
						((IDataObjectGeneratorContainer)xsoProperty).SchemaState = this.schemaState;
						((IDataObjectGeneratorContainer)property).SchemaState = ((IDataObjectGeneratorContainer)propertyContainer).SchemaState;
					}
					xsoProperty.CopyFrom(property);
				}
			}
			foreach (IProperty property2 in propertyContainer.Children)
			{
				XsoProperty xsoProperty2 = this.GetPropBySchemaLinkId(property2.SchemaLinkId) as XsoProperty;
				if (xsoProperty2 != null && xsoProperty2.Type != PropertyType.ReadOnly && xsoProperty2.State != PropertyState.NotSupported && xsoProperty2.PostProcessingDelegate != null)
				{
					xsoProperty2.PostProcessingDelegate(property2, this.Children);
					xsoProperty2.PostProcessingDelegate = null;
				}
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0007628C File Offset: 0x0007448C
		public void SetChangedProperties(PropertyDefinition[] changedProperties)
		{
			if (changedProperties == null)
			{
				return;
			}
			this.areChildPropertyStatesSet = true;
			for (int i = 0; i < changedProperties.Length; i++)
			{
				foreach (IProperty property in this.propertyFromSchemaLinkId)
				{
					XsoProperty xsoProperty = (XsoProperty)property;
					if (xsoProperty.StorePropertyDefinition == changedProperties[i])
					{
						xsoProperty.State = PropertyState.Modified;
					}
					else
					{
						xsoProperty.State = PropertyState.Unmodified;
					}
				}
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0007630C File Offset: 0x0007450C
		public IProperty GetPropBySchemaLinkId(int id)
		{
			return this.propertyFromSchemaLinkId[id];
		}

		// Token: 0x04000C62 RID: 3170
		private readonly QueryFilter supportedClassFilter;

		// Token: 0x04000C63 RID: 3171
		private IList<IProperty> propertyFromSchemaLinkId;

		// Token: 0x04000C64 RID: 3172
		private IXsoDataObjectGenerator schemaState;

		// Token: 0x04000C65 RID: 3173
		private PropertyDefinition[] prefetchProperties;

		// Token: 0x04000C66 RID: 3174
		private bool areChildPropertyStatesSet;
	}
}
