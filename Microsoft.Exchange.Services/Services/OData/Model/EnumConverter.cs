using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E9A RID: 3738
	internal static class EnumConverter
	{
		// Token: 0x0600614F RID: 24911 RVA: 0x0012F718 File Offset: 0x0012D918
		public static TResult CastEnumType<TResult>(object input)
		{
			if (input == null)
			{
				return default(TResult);
			}
			return (TResult)((object)EnumConverter.CastEnumType(typeof(TResult), input));
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x0012F74C File Offset: 0x0012D94C
		public static TResult FromODataEnumValue<TResult>(ODataEnumValue odataValue)
		{
			if (odataValue == null)
			{
				return default(TResult);
			}
			return (TResult)((object)EnumConverter.FromODataEnumValue(typeof(TResult), odataValue));
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x0012F77B File Offset: 0x0012D97B
		public static object FromODataEnumValue(Type targetType, ODataEnumValue odataValue)
		{
			ArgumentValidator.ThrowIfNull("targetType", targetType);
			if (odataValue == null)
			{
				return EnumConverter.CastEnumType(targetType, null);
			}
			return EnumConverter.CastEnumType(targetType, odataValue.Value);
		}

		// Token: 0x06006152 RID: 24914 RVA: 0x0012F79F File Offset: 0x0012D99F
		public static ODataEnumValue ToODataEnumValue(Enum enumValue)
		{
			return new ODataEnumValue(enumValue.ToString());
		}

		// Token: 0x06006153 RID: 24915 RVA: 0x0012F7AC File Offset: 0x0012D9AC
		private static object CastEnumType(Type targetType, object input)
		{
			if (targetType.Equals(input.GetType()))
			{
				return input;
			}
			string value = input.ToString();
			return Enum.Parse(targetType, value, true);
		}
	}
}
