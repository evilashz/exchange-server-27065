using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization
{
	// Token: 0x0200005C RID: 92
	internal static class EnumConverter
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x0000D17C File Offset: 0x0000B37C
		public static T ConvertIntegerToEnum<T>(int id) where T : struct, IConvertible
		{
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsEnum)
			{
				throw new ArgumentException("T must be an enum.");
			}
			T t = (T)((object)Enum.ToObject(typeFromHandle, id));
			if (!Enum.IsDefined(typeFromHandle, t))
			{
				t = (T)((object)Enum.ToObject(typeFromHandle, 0));
			}
			return t;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		public static int ConvertEnumToInteger<T>(T obj) where T : struct, IConvertible
		{
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsEnum)
			{
				throw new ArgumentException("T must be an enum.");
			}
			return obj.ToInt32(null);
		}
	}
}
