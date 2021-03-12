using System;
using System.Collections;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000133 RID: 307
	[Serializable]
	internal class CollectionDelegateConstraint : CollectionPropertyDefinitionConstraint
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x00021047 File Offset: 0x0001F247
		public CollectionDelegateConstraint(CollectionValidationDelegate validator)
		{
			this.validator = validator;
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00021056 File Offset: 0x0001F256
		public CollectionValidationDelegate Delegate
		{
			get
			{
				return this.validator;
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002105E File Offset: 0x0001F25E
		public override PropertyConstraintViolationError Validate(IEnumerable collection, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			return this.Delegate(collection, propertyDefinition, propertyBag, this);
		}

		// Token: 0x0400066D RID: 1645
		[NonSerialized]
		private CollectionValidationDelegate validator;
	}
}
