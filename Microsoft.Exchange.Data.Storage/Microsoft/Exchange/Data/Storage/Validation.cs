using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EBB RID: 3771
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class Validation
	{
		// Token: 0x06008260 RID: 33376 RVA: 0x00239660 File Offset: 0x00237860
		internal static void Validate(IValidatable validatable, ValidationContext context)
		{
			IList<StoreObjectValidationError> list = new List<StoreObjectValidationError>();
			validatable.Validate(context, list);
			if (list.Count > 0)
			{
				throw new ObjectValidationException(ServerStrings.ExCannotSaveInvalidObject(list[0]), list);
			}
		}

		// Token: 0x06008261 RID: 33377 RVA: 0x00239698 File Offset: 0x00237898
		internal static StoreObjectValidationError[] CreateStoreObjectValiationErrorArray(IValidatable validatable, ValidationContext context)
		{
			IList<StoreObjectValidationError> list = new List<StoreObjectValidationError>();
			validatable.Validate(context, list);
			StoreObjectValidationError[] array;
			if (list.Count > 0)
			{
				array = new StoreObjectValidationError[list.Count];
				list.CopyTo(array, 0);
			}
			else
			{
				array = Validation.EmptyValidationErrorArray;
			}
			return array;
		}

		// Token: 0x06008262 RID: 33378 RVA: 0x002396DC File Offset: 0x002378DC
		internal static void ValidateProperties(ValidationContext context, IValidatable validatable, IValidatablePropertyBag propertyBag, IList<StoreObjectValidationError> validationErrors)
		{
			foreach (StoreObjectConstraint storeObjectConstraint in validatable.Schema.Constraints)
			{
				bool flag = validatable.ValidateAllProperties;
				if (!flag)
				{
					foreach (PropertyDefinition propertyDefinition in storeObjectConstraint.RelevantProperties)
					{
						if (propertyBag.IsPropertyDirty(propertyDefinition))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					StoreObjectValidationError storeObjectValidationError = storeObjectConstraint.Validate(context, propertyBag);
					if (storeObjectValidationError != null)
					{
						validationErrors.Add(storeObjectValidationError);
					}
				}
			}
		}

		// Token: 0x0400577A RID: 22394
		private static readonly StoreObjectValidationError[] EmptyValidationErrorArray = Array<StoreObjectValidationError>.Empty;
	}
}
