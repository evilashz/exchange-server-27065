using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000131 RID: 305
	[Serializable]
	internal class DelegateConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x0002101F File Offset: 0x0001F21F
		public DelegateConstraint(ValidationDelegate validator)
		{
			this.validator = validator;
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002102E File Offset: 0x0001F22E
		public ValidationDelegate Delegate
		{
			get
			{
				return this.validator;
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00021036 File Offset: 0x0001F236
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			return this.Delegate(value, propertyDefinition, propertyBag, this);
		}

		// Token: 0x0400066C RID: 1644
		[NonSerialized]
		private ValidationDelegate validator;
	}
}
