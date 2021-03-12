using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001BE RID: 446
	internal sealed class ToXmlForPropertyBagUsingStoreObject : ToXmlPropertyList
	{
		// Token: 0x06000C45 RID: 3141 RVA: 0x0003DA0F File Offset: 0x0003BC0F
		public ToXmlForPropertyBagUsingStoreObject(Shape shape, ResponseShape responseShape) : base(shape, responseShape)
		{
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0003DA19 File Offset: 0x0003BC19
		protected override bool IsErrorReturnedForInvalidBaseShapeProperty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0003DA1C File Offset: 0x0003BC1C
		protected override bool ValidateProperty(PropertyInformation propertyInformation, bool returnErrorForInvalidProperty)
		{
			bool implementsToXmlForPropertyBagCommand = propertyInformation.ImplementsToXmlForPropertyBagCommand;
			if (!implementsToXmlForPropertyBagCommand && returnErrorForInvalidProperty)
			{
				throw new InvalidPropertyForOperationException(propertyInformation.PropertyPath);
			}
			return implementsToXmlForPropertyBagCommand;
		}
	}
}
