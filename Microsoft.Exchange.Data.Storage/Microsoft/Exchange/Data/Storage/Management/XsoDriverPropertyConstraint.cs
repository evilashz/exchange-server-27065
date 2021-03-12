using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A8D RID: 2701
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class XsoDriverPropertyConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x17001B6D RID: 7021
		// (get) Token: 0x060062F2 RID: 25330 RVA: 0x001A1D28 File Offset: 0x0019FF28
		// (set) Token: 0x060062F3 RID: 25331 RVA: 0x001A1D30 File Offset: 0x0019FF30
		public StorePropertyDefinition StorePropertyDefinition { get; private set; }

		// Token: 0x060062F4 RID: 25332 RVA: 0x001A1D39 File Offset: 0x0019FF39
		public XsoDriverPropertyConstraint(StorePropertyDefinition storePropertyDefinition)
		{
			if (storePropertyDefinition == null)
			{
				throw new ArgumentNullException("storePropertyDefinition");
			}
			this.StorePropertyDefinition = storePropertyDefinition;
		}

		// Token: 0x060062F5 RID: 25333 RVA: 0x001A1D58 File Offset: 0x0019FF58
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				return null;
			}
			PropertyValidationError[] array = this.StorePropertyDefinition.Validate(null, value);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return new PropertyConstraintViolationError(array[0].Description, propertyDefinition, value, this);
		}
	}
}
