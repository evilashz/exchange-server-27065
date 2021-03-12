using System;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006BF RID: 1727
	internal class PropertyConstraintValidatorCreator : IPropertyConstraintValidatorCreator
	{
		// Token: 0x060049B0 RID: 18864 RVA: 0x000E0A6C File Offset: 0x000DEC6C
		public PropertyConstraintValidatorCreator(Type constraintType, Type validatorType)
		{
			if (constraintType == null)
			{
				throw new ArgumentNullException();
			}
			if (!typeof(PropertyDefinitionConstraint).IsAssignableFrom(constraintType))
			{
				throw new ArgumentException();
			}
			if (validatorType == null)
			{
				throw new ArgumentNullException();
			}
			if (!typeof(ValidatorInfo).IsAssignableFrom(validatorType))
			{
				throw new ArgumentException();
			}
			this.ConstraintType = constraintType;
			this.ValidatorType = validatorType;
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x000E0ADC File Offset: 0x000DECDC
		public virtual ValidatorInfo Create(ProviderPropertyDefinition propertyDefinition, PropertyDefinitionConstraint constraint)
		{
			if (this.ValidatorType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
			{
				this.ConstraintType
			}, null) != null)
			{
				return (ValidatorInfo)Activator.CreateInstance(this.ValidatorType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[]
				{
					constraint
				}, null);
			}
			if (this.ValidatorType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null) != null)
			{
				return (ValidatorInfo)Activator.CreateInstance(this.ValidatorType, true);
			}
			return null;
		}

		// Token: 0x17002805 RID: 10245
		// (get) Token: 0x060049B2 RID: 18866 RVA: 0x000E0B62 File Offset: 0x000DED62
		// (set) Token: 0x060049B3 RID: 18867 RVA: 0x000E0B6A File Offset: 0x000DED6A
		public Type ConstraintType { get; private set; }

		// Token: 0x17002806 RID: 10246
		// (get) Token: 0x060049B4 RID: 18868 RVA: 0x000E0B73 File Offset: 0x000DED73
		// (set) Token: 0x060049B5 RID: 18869 RVA: 0x000E0B7B File Offset: 0x000DED7B
		public Type ValidatorType { get; private set; }
	}
}
