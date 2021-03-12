using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors
{
	// Token: 0x0200006A RID: 106
	internal class DefaultStoragePropertyAccessor<TStoreObject, TValue> : StoragePropertyAccessor<TStoreObject, TValue>, IPropertyValueCollectionAccessor<TStoreObject, Microsoft.Exchange.Data.PropertyDefinition, TValue>, IPropertyAccessor<TStoreObject, TValue> where TStoreObject : IStorePropertyBag
	{
		// Token: 0x06000247 RID: 583 RVA: 0x00007D98 File Offset: 0x00005F98
		public DefaultStoragePropertyAccessor(StorePropertyDefinition property, bool forceReadonly = false) : base(forceReadonly || (property.PropertyFlags & PropertyFlags.ReadOnly) == PropertyFlags.ReadOnly, null, new StorePropertyDefinition[]
		{
			property
		})
		{
			base.PropertyChangeMetadataGroup = PropertyChangeMetadata.GetGroupForProperty(property);
			this.Property = property;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00007DDC File Offset: 0x00005FDC
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00007DE4 File Offset: 0x00005FE4
		private protected Microsoft.Exchange.Data.PropertyDefinition Property { protected get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00007DED File Offset: 0x00005FED
		protected ITracer Trace
		{
			get
			{
				return ExTraceGlobals.CommonTracer;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007DF4 File Offset: 0x00005FF4
		public bool TryGetValue(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, out TValue value)
		{
			if (propertyIndices == null)
			{
				throw new ArgumentNullException("propertyIndices");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num;
			if (!propertyIndices.TryGetValue(this.Property, out num))
			{
				value = default(TValue);
				return false;
			}
			if (num >= values.Count || num < 0)
			{
				string message = string.Format("Property index ({0}) is out of range (# Values: {1}).", num, values.Count);
				throw new ArgumentException(message, "propertyIndices");
			}
			object givenValue = values[num];
			return this.TryCastValue(givenValue, out value);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007E7C File Offset: 0x0000607C
		protected override bool PerformTryGetValue(TStoreObject container, out TValue value)
		{
			object givenValue = container.TryGetProperty(this.Property);
			return this.TryCastValue(givenValue, out value);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007EA5 File Offset: 0x000060A5
		protected override void PerformSet(TStoreObject container, TValue value)
		{
			this.Trace.TraceDebug<string, TValue>((long)this.GetHashCode(), "DefaultStoragePropertyAccessor::PerformSet on {0} with {1}", this.Property.Name, value);
			container.SetOrDeleteProperty(this.Property, value);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00007EE4 File Offset: 0x000060E4
		protected virtual bool TryCastValue(object givenValue, out TValue value)
		{
			if (givenValue == null)
			{
				value = default(TValue);
				return false;
			}
			PropertyError propertyError = givenValue as PropertyError;
			if (propertyError != null)
			{
				value = default(TValue);
				return false;
			}
			bool result;
			try
			{
				value = (TValue)((object)givenValue);
				result = true;
			}
			catch (InvalidCastException)
			{
				this.Trace.TraceError((long)this.GetHashCode(), "[{0}::TryCastValue] InvalidCastException - Property: {1}; Type: {2}; Value Type: {3}; Value: {4}", new object[]
				{
					base.GetType().Name,
					this.Property.Name,
					this.Property.Type.FullName,
					givenValue.GetType().FullName,
					givenValue
				});
				value = default(TValue);
				result = false;
			}
			return result;
		}
	}
}
