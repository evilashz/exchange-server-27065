using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EB1 RID: 3761
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class NullValueError : PropertyValidationError
	{
		// Token: 0x0600823D RID: 33341 RVA: 0x00238FB9 File Offset: 0x002371B9
		internal NullValueError(PropertyDefinition propertyDefinition, object invalidData) : base(ServerStrings.ExValueCannotBeNull, propertyDefinition, invalidData)
		{
		}

		// Token: 0x0600823E RID: 33342 RVA: 0x00238FC8 File Offset: 0x002371C8
		public override string ToString()
		{
			return string.Format("Value cannot be null for property {0}. Invalid data = {1}.", base.PropertyDefinition, base.InvalidData);
		}
	}
}
