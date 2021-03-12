using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EB0 RID: 3760
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NoSurroundingWhiteSpaceConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x0600823B RID: 33339 RVA: 0x00238F57 File Offset: 0x00237157
		internal NoSurroundingWhiteSpaceConstraint()
		{
		}

		// Token: 0x0600823C RID: 33340 RVA: 0x00238F60 File Offset: 0x00237160
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			string text = (string)value;
			if (text.Length > 0 && (char.IsWhiteSpace(text[0]) || char.IsWhiteSpace(text[text.Length - 1])))
			{
				return new PropertyConstraintViolationError(new LocalizedString(ServerStrings.ExStringContainsSurroundingWhiteSpace), propertyDefinition, value, this);
			}
			return null;
		}
	}
}
