using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000685 RID: 1669
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CompositeValidator : IValidator
	{
		// Token: 0x06004477 RID: 17527 RVA: 0x00123ECF File Offset: 0x001220CF
		internal CompositeValidator(params IValidator[] validators)
		{
			this.validators = validators;
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x00123EE0 File Offset: 0x001220E0
		public bool Validate(DefaultFolderContext context, PropertyBag propertyBag)
		{
			foreach (IValidator validator in this.validators)
			{
				if (!validator.Validate(context, propertyBag))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x00123F18 File Offset: 0x00122118
		public void SetProperties(DefaultFolderContext context, Folder folder)
		{
			foreach (IValidator validator in this.validators)
			{
				validator.SetProperties(context, folder);
			}
		}

		// Token: 0x04002553 RID: 9555
		private readonly IValidator[] validators;
	}
}
