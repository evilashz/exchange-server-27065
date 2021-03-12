using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	internal sealed class AsciiCharactersOnlyConstraint : CharacterRegexConstraint
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x0001EA04 File Offset: 0x0001CC04
		public AsciiCharactersOnlyConstraint() : base(AsciiCharactersOnlyConstraint.AsciiCharactersOnlyRegEx)
		{
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0001EA11 File Offset: 0x0001CC11
		protected override LocalizedString CustomErrorMessage(string value, PropertyDefinition propertyDefinition)
		{
			return DataStrings.ConstraintViolationStringDoesContainsNonASCIICharacter(value);
		}

		// Token: 0x0400061E RID: 1566
		public static string AsciiCharactersOnlyRegEx = "[^\u0080-￿]";
	}
}
