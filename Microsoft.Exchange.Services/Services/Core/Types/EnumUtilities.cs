using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005D0 RID: 1488
	public static class EnumUtilities
	{
		// Token: 0x06002CCC RID: 11468 RVA: 0x000B1434 File Offset: 0x000AF634
		public static T Parse<T>(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return default(T);
			}
			return EnumUtilities.enumTypeMap.Member.Parse<T>(value);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000B1463 File Offset: 0x000AF663
		public static string ToString<T>(T value)
		{
			return EnumUtilities.enumTypeMap.Member.ToString<T>(value);
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000B1475 File Offset: 0x000AF675
		public static bool IsDefined<T>(T value)
		{
			return EnumUtilities.enumTypeMap.Member.IsDefined<T>(value);
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000B1488 File Offset: 0x000AF688
		public static string[] ToStringArray<T>(T[] enums)
		{
			string[] array = null;
			if (enums != null)
			{
				array = new string[enums.Length];
				for (int i = 0; i < enums.Length; i++)
				{
					array[i] = EnumUtilities.ToString<T>(enums[i]);
				}
			}
			return array;
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000B14C4 File Offset: 0x000AF6C4
		public static T[] ParseStringArray<T>(string[] strings)
		{
			T[] array = null;
			if (strings != null)
			{
				array = new T[strings.Length];
				for (int i = 0; i < strings.Length; i++)
				{
					array[i] = EnumUtilities.Parse<T>(strings[i]);
				}
			}
			return array;
		}

		// Token: 0x04001AF3 RID: 6899
		private static LazyMember<EnumTypeMap> enumTypeMap = new LazyMember<EnumTypeMap>(() => new EnumTypeMap());
	}
}
