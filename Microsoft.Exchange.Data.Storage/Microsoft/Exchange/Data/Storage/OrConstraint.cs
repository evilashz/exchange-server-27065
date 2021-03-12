using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EB2 RID: 3762
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OrConstraint : CompositeConstraint
	{
		// Token: 0x0600823F RID: 33343 RVA: 0x00238FE0 File Offset: 0x002371E0
		internal OrConstraint(params StoreObjectConstraint[] constraints) : base(constraints)
		{
		}

		// Token: 0x06008240 RID: 33344 RVA: 0x00238FEC File Offset: 0x002371EC
		internal override StoreObjectValidationError Validate(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			StoreObjectValidationError storeObjectValidationError = null;
			foreach (StoreObjectConstraint storeObjectConstraint in base.Constraints)
			{
				StoreObjectValidationError storeObjectValidationError2 = storeObjectConstraint.Validate(context, validatablePropertyBag);
				if (storeObjectValidationError2 == null)
				{
					return null;
				}
				if (storeObjectValidationError == null)
				{
					storeObjectValidationError = storeObjectValidationError2;
				}
			}
			return new StoreObjectValidationError(context, storeObjectValidationError.PropertyDefinition, storeObjectValidationError.InvalidData, this);
		}

		// Token: 0x17002281 RID: 8833
		// (get) Token: 0x06008241 RID: 33345 RVA: 0x00239046 File Offset: 0x00237246
		protected override string CompositionTypeDescription
		{
			get
			{
				return "OR";
			}
		}
	}
}
