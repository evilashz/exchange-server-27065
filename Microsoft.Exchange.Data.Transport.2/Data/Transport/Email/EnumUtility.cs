using System;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000D8 RID: 216
	internal class EnumUtility
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public static string GetString(EnumUtility.StringIntPair[] map, int value)
		{
			string result = string.Empty;
			for (int i = 0; i < map.Length; i++)
			{
				if (map[i].Int == value)
				{
					return map[i].String;
				}
				if (map[i].Int == 0)
				{
					result = map[i].String;
				}
			}
			return result;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000CACC File Offset: 0x0000ACCC
		public static bool TryGetInt(EnumUtility.StringIntPair[] map, string value, ref int result)
		{
			for (int i = 0; i < map.Length; i++)
			{
				if (map[i].String == null)
				{
					if (string.IsNullOrEmpty(value))
					{
						result = map[i].Int;
						return true;
					}
				}
				else if (map[i].String.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					result = map[i].Int;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400030F RID: 783
		public static EnumUtility.StringIntPair[] PriorityMap = new EnumUtility.StringIntPair[]
		{
			new EnumUtility.StringIntPair("normal", 0),
			new EnumUtility.StringIntPair("urgent", 1),
			new EnumUtility.StringIntPair("non-urgent", 2)
		};

		// Token: 0x04000310 RID: 784
		public static EnumUtility.StringIntPair[] XPriorityMap = new EnumUtility.StringIntPair[]
		{
			new EnumUtility.StringIntPair("1", 1),
			new EnumUtility.StringIntPair("2", 1),
			new EnumUtility.StringIntPair("3", 0),
			new EnumUtility.StringIntPair("5", 2),
			new EnumUtility.StringIntPair("4", 2)
		};

		// Token: 0x04000311 RID: 785
		public static EnumUtility.StringIntPair[] ImportanceMap = new EnumUtility.StringIntPair[]
		{
			new EnumUtility.StringIntPair("normal", 0),
			new EnumUtility.StringIntPair("high", 1),
			new EnumUtility.StringIntPair("low", 2)
		};

		// Token: 0x04000312 RID: 786
		public static EnumUtility.StringIntPair[] SensitivityMap = new EnumUtility.StringIntPair[]
		{
			new EnumUtility.StringIntPair(null, 0),
			new EnumUtility.StringIntPair("Personal", 1),
			new EnumUtility.StringIntPair("Private", 2),
			new EnumUtility.StringIntPair("Company-Confidential", 3)
		};

		// Token: 0x020000D9 RID: 217
		public struct StringIntPair
		{
			// Token: 0x0600051A RID: 1306 RVA: 0x0000CCD2 File Offset: 0x0000AED2
			public StringIntPair(string s, int i)
			{
				this.String = s;
				this.Int = i;
			}

			// Token: 0x04000313 RID: 787
			public string String;

			// Token: 0x04000314 RID: 788
			public int Int;
		}
	}
}
