using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006D0 RID: 1744
	internal class TypeMappingPropertyConstraintValidatorsGenerator
	{
		// Token: 0x06004A08 RID: 18952 RVA: 0x000E2AA1 File Offset: 0x000E0CA1
		public TypeMappingPropertyConstraintValidatorsGenerator()
		{
			this.TypeMappingManager = new TypeMappingManager<PropertyConstraintValidatorMapping>();
		}

		// Token: 0x17002810 RID: 10256
		// (get) Token: 0x06004A09 RID: 18953 RVA: 0x000E2AB4 File Offset: 0x000E0CB4
		// (set) Token: 0x06004A0A RID: 18954 RVA: 0x000E2ABC File Offset: 0x000E0CBC
		public TypeMappingManager<PropertyConstraintValidatorMapping> TypeMappingManager { get; private set; }

		// Token: 0x06004A0B RID: 18955 RVA: 0x000E2AC5 File Offset: 0x000E0CC5
		public void RegisterMapping(Type sourceType, Type validatorType)
		{
			this.RegisterMapping(new PropertyConstraintValidatorMapping(sourceType, validatorType));
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x000E2AD4 File Offset: 0x000E0CD4
		public void RegisterMapping(Type sourceType, IPropertyConstraintValidatorCreator validatorCreator)
		{
			this.RegisterMapping(new PropertyConstraintValidatorMapping(sourceType, validatorCreator));
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x000E2AE3 File Offset: 0x000E0CE3
		public void RegisterMapping(PropertyConstraintValidatorMapping mapping)
		{
			this.TypeMappingManager.RegisterMapping(mapping);
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x000E2AF4 File Offset: 0x000E0CF4
		public IPropertyConstraintValidatorCreator[] GetValidatorCreators(Type sourceType)
		{
			List<IPropertyConstraintValidatorCreator> list = new List<IPropertyConstraintValidatorCreator>();
			PropertyConstraintValidatorMapping[] nearestMappings = this.TypeMappingManager.GetNearestMappings(sourceType);
			foreach (PropertyConstraintValidatorMapping propertyConstraintValidatorMapping in nearestMappings)
			{
				list.Add(propertyConstraintValidatorMapping.ValidatorCreator);
			}
			return list.ToArray();
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x000E2B60 File Offset: 0x000E0D60
		public ValidatorInfo[] ValidatorsFromPropertyDefinition(ProviderPropertyDefinition propertyDefinition)
		{
			if (propertyDefinition != null)
			{
				List<ValidatorInfo> list = new List<ValidatorInfo>();
				foreach (PropertyDefinitionConstraint propertyDefinitionConstraint in propertyDefinition.AllConstraints)
				{
					IPropertyConstraintValidatorCreator[] validatorCreators = this.GetValidatorCreators(propertyDefinitionConstraint.GetType());
					IPropertyConstraintValidatorCreator[] array = validatorCreators;
					for (int i = 0; i < array.Length; i++)
					{
						IPropertyConstraintValidatorCreator propertyConstraintValidatorCreator = array[i];
						ValidatorInfo validator = propertyConstraintValidatorCreator.Create(propertyDefinition, propertyDefinitionConstraint);
						if (validator != null)
						{
							if (!list.Any((ValidatorInfo c) => c.Type == validator.Type))
							{
								if (propertyDefinition.IsMultivalued && !(propertyDefinitionConstraint is CollectionPropertyDefinitionConstraint))
								{
									list.Add(new CollectionItemValidatorInfo
									{
										ItemValidator = validator
									});
								}
								else
								{
									list.Add(validator);
								}
							}
						}
					}
				}
				return list.ToArray();
			}
			return new ValidatorInfo[0];
		}
	}
}
