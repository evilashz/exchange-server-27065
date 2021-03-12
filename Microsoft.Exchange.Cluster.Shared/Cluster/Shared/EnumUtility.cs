using System;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000014 RID: 20
	internal static class EnumUtility
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000038D3 File Offset: 0x00001AD3
		public static bool TryParse<T>(string value, out T result, T defaultValue) where T : struct
		{
			return EnumUtility.TryParse<T>(value, out result, defaultValue, true);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000038E0 File Offset: 0x00001AE0
		public static bool TryParse<T>(string value, out T result, T defaultValue, bool ignoreCase) where T : struct
		{
			result = defaultValue;
			if (value != null)
			{
				try
				{
					result = (T)((object)Enum.Parse(typeof(T), value, ignoreCase));
					if (Enum.IsDefined(typeof(T), result))
					{
						return true;
					}
					result = defaultValue;
					return false;
				}
				catch (ArgumentException)
				{
				}
				return false;
			}
			return false;
		}
	}
}
