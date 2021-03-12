using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006D1 RID: 1745
	internal class PropertyConstraintValidatorMapping : ITypeMapping
	{
		// Token: 0x06004A10 RID: 18960 RVA: 0x000E2C78 File Offset: 0x000E0E78
		public PropertyConstraintValidatorMapping(Type sourceType, Type validatorType) : this(sourceType, new PropertyConstraintValidatorCreator(sourceType, validatorType))
		{
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x000E2C88 File Offset: 0x000E0E88
		public PropertyConstraintValidatorMapping(Type sourceType, IPropertyConstraintValidatorCreator validatorCreator)
		{
			if (sourceType == null)
			{
				throw new ArgumentNullException("sourceType");
			}
			if (!typeof(PropertyDefinitionConstraint).IsAssignableFrom(sourceType))
			{
				throw new ArgumentException("sourceType must be subtype of PropertyDefinitionConstraint", "sourceType");
			}
			if (validatorCreator == null)
			{
				throw new ArgumentNullException("validatorCreator");
			}
			this.SourceType = sourceType;
			this.ValidatorCreator = validatorCreator;
		}

		// Token: 0x17002811 RID: 10257
		// (get) Token: 0x06004A12 RID: 18962 RVA: 0x000E2CED File Offset: 0x000E0EED
		// (set) Token: 0x06004A13 RID: 18963 RVA: 0x000E2CF5 File Offset: 0x000E0EF5
		public Type SourceType { get; private set; }

		// Token: 0x17002812 RID: 10258
		// (get) Token: 0x06004A14 RID: 18964 RVA: 0x000E2CFE File Offset: 0x000E0EFE
		// (set) Token: 0x06004A15 RID: 18965 RVA: 0x000E2D06 File Offset: 0x000E0F06
		public IPropertyConstraintValidatorCreator ValidatorCreator { get; private set; }
	}
}
