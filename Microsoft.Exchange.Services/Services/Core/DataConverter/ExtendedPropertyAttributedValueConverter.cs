using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001E9 RID: 489
	internal sealed class ExtendedPropertyAttributedValueConverter : BaseConverter
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x000426E5 File Offset: 0x000408E5
		public override object ConvertToObject(string propertyString)
		{
			return null;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000426E8 File Offset: 0x000408E8
		public override string ConvertToString(object propertyValue)
		{
			return string.Empty;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000426F0 File Offset: 0x000408F0
		protected override object ConvertToServiceObjectValue(object propertyValue)
		{
			if (propertyValue == null)
			{
				return null;
			}
			AttributedValue<ContactExtendedPropertyData> attributedValue = (AttributedValue<ContactExtendedPropertyData>)propertyValue;
			ExtendedPropertyUri propertyUri = new ExtendedPropertyUri((NativeStorePropertyDefinition)attributedValue.Value.PropertyDefinition);
			ExtendedPropertyType extendedPropertyForValues = ExtendedPropertyProperty.GetExtendedPropertyForValues(propertyUri, attributedValue.Value.PropertyDefinition, attributedValue.Value.RawValue);
			return new ExtendedPropertyAttributedValue(extendedPropertyForValues, attributedValue.Attributions);
		}
	}
}
