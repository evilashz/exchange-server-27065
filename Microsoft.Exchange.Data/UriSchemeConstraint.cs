using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001DC RID: 476
	[Serializable]
	internal class UriSchemeConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06001097 RID: 4247 RVA: 0x0003233A File Offset: 0x0003053A
		public UriSchemeConstraint(IEnumerable<string> expectedUriSchemes)
		{
			this.expectedUriSchemes = expectedUriSchemes;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0003234C File Offset: 0x0003054C
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			Uri uri = value as Uri;
			if (uri != null && !this.IsExpectedScheme(uri.Scheme))
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationInvalidUriScheme(uri, this.MakeCommaSeparatedListOfAllowedSchemes()), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0003238D File Offset: 0x0003058D
		private string MakeCommaSeparatedListOfAllowedSchemes()
		{
			return string.Join(", ", this.expectedUriSchemes.ToArray<string>());
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000323A4 File Offset: 0x000305A4
		private bool IsExpectedScheme(string scheme)
		{
			foreach (string x in this.expectedUriSchemes)
			{
				if (StringComparer.OrdinalIgnoreCase.Compare(x, scheme) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040009D7 RID: 2519
		private readonly IEnumerable<string> expectedUriSchemes;
	}
}
