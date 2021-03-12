using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EBA RID: 3770
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class TypeMismatchError : PropertyValidationError
	{
		// Token: 0x0600825E RID: 33374 RVA: 0x0023961C File Offset: 0x0023781C
		internal TypeMismatchError(PropertyDefinition propertyDefinition, object invalidData) : base(new LocalizedString(ServerStrings.ExValueOfWrongType(invalidData, propertyDefinition.Type)), propertyDefinition, invalidData)
		{
		}

		// Token: 0x0600825F RID: 33375 RVA: 0x0023963C File Offset: 0x0023783C
		public override string ToString()
		{
			return string.Format("Invalid data type for property {0}. Expected type = {1}. Invalid data = {2}.", base.PropertyDefinition, base.PropertyDefinition.Type, base.InvalidData);
		}
	}
}
