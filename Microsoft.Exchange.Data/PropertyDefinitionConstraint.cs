using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	internal abstract class PropertyDefinitionConstraint
	{
		// Token: 0x06000005 RID: 5
		public abstract PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag);

		// Token: 0x06000006 RID: 6 RVA: 0x00002288 File Offset: 0x00000488
		public virtual PropertyConstraintViolationError Validate(ExchangeOperationContext operationContext, object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			return this.Validate(value, propertyDefinition, propertyBag);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002294 File Offset: 0x00000494
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == base.GetType();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022AC File Offset: 0x000004AC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x04000001 RID: 1
		public static PropertyDefinitionConstraint[] None = new PropertyDefinitionConstraint[0];
	}
}
