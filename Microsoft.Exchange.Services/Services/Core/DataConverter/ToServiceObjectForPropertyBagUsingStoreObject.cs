using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001B9 RID: 441
	internal sealed class ToServiceObjectForPropertyBagUsingStoreObject : ToServiceObjectPropertyList
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x0003CE60 File Offset: 0x0003B060
		public ToServiceObjectForPropertyBagUsingStoreObject(Shape shape, ResponseShape responseShape, IParticipantResolver participantResolver) : base(shape, responseShape, participantResolver)
		{
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0003CE6B File Offset: 0x0003B06B
		protected override bool IsErrorReturnedForInvalidBaseShapeProperty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0003CE70 File Offset: 0x0003B070
		protected override bool ValidateProperty(PropertyInformation propertyInformation, bool returnErrorForInvalidProperty)
		{
			bool implementsToServiceObjectForPropertyBagCommand = propertyInformation.ImplementsToServiceObjectForPropertyBagCommand;
			if (!implementsToServiceObjectForPropertyBagCommand && returnErrorForInvalidProperty)
			{
				throw new InvalidPropertyForOperationException(propertyInformation.PropertyPath);
			}
			return implementsToServiceObjectForPropertyBagCommand;
		}
	}
}
