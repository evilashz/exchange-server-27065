using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001ED RID: 493
	internal class IconIndexConverter : EnumConverter
	{
		// Token: 0x06000D04 RID: 3332 RVA: 0x00042854 File Offset: 0x00040A54
		public override object ConvertToObject(string propertyString)
		{
			return EnumUtilities.Parse<IconIndexType>(propertyString);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00042864 File Offset: 0x00040A64
		public override string ConvertToString(object propertyValue)
		{
			IconIndexType value = (IconIndexType)propertyValue;
			if (!EnumUtilities.IsDefined<IconIndexType>(value))
			{
				value = IconIndexType.Default;
			}
			return EnumUtilities.ToString<IconIndexType>(value);
		}
	}
}
