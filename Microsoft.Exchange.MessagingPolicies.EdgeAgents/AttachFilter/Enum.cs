using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Exchange.MessagingPolicies.AttachFilter
{
	// Token: 0x02000011 RID: 17
	internal static class Enum<EnumType>
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00003F68 File Offset: 0x00002168
		private static string[] InitNames()
		{
			Type typeFromHandle = typeof(EnumType);
			FieldInfo[] fields = typeFromHandle.GetFields(BindingFlags.Static | BindingFlags.Public);
			List<string> list = new List<string>();
			foreach (FieldInfo fieldInfo in fields)
			{
				list.Add(fieldInfo.Name);
				Enum<EnumType>.innerDictionary.Add(fieldInfo.Name, (EnumType)((object)fieldInfo.GetRawConstantValue()));
			}
			return list.ToArray();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003FD8 File Offset: 0x000021D8
		public static string ToString(int enumValue)
		{
			return Enum<EnumType>.names[enumValue];
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003FE1 File Offset: 0x000021E1
		public static EnumType Parse(string enumString)
		{
			return Enum<EnumType>.innerDictionary[enumString];
		}

		// Token: 0x04000046 RID: 70
		private static Dictionary<string, EnumType> innerDictionary = new Dictionary<string, EnumType>();

		// Token: 0x04000047 RID: 71
		private static string[] names = Enum<EnumType>.InitNames();
	}
}
