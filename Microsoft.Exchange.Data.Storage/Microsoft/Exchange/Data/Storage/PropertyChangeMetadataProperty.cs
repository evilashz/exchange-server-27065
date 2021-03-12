using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C3B RID: 3131
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class PropertyChangeMetadataProperty : SmartPropertyDefinition
	{
		// Token: 0x06006EE9 RID: 28393 RVA: 0x001DD554 File Offset: 0x001DB754
		internal PropertyChangeMetadataProperty() : base("PropertyChangeMetadataProperty", typeof(PropertyChangeMetadata), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.PropertyChangeMetadataRaw, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x06006EEA RID: 28394 RVA: 0x001DD594 File Offset: 0x001DB794
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			byte[] valueOrDefault = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.PropertyChangeMetadataRaw);
			if (valueOrDefault == null)
			{
				CalendarItemOccurrence calendarItemOccurrence = propertyBag.Context.StoreObject as CalendarItemOccurrence;
				if (calendarItemOccurrence == null)
				{
					return null;
				}
				return calendarItemOccurrence.OccurrencePropertyBag.ComputeChangeMetadataBasedOnLoadedProperties();
			}
			else
			{
				if (valueOrDefault.Length == 0)
				{
					return null;
				}
				object result;
				try
				{
					result = PropertyChangeMetadata.Parse(valueOrDefault);
				}
				catch (PropertyChangeMetadataFormatException ex)
				{
					result = new PropertyError(this, PropertyErrorCode.CorruptedData, ex.Message);
				}
				return result;
			}
		}

		// Token: 0x06006EEB RID: 28395 RVA: 0x001DD608 File Offset: 0x001DB808
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			PropertyChangeMetadata propertyChangeMetadata = value as PropertyChangeMetadata;
			if (propertyChangeMetadata == null)
			{
				throw new ArgumentException("value");
			}
			propertyBag.SetValue(InternalSchema.PropertyChangeMetadataRaw, propertyChangeMetadata.ToByteArray());
		}
	}
}
