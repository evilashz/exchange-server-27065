using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000038 RID: 56
	internal class EnumAttributeInfo<T, A> where T : struct where A : Attribute
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00007DCB File Offset: 0x00005FCB
		public static bool TryGetValue(int enumValue, out A attributeValue)
		{
			EnumAttributeInfo<T, A>.InitializeIfNeccessary();
			return EnumAttributeInfo<T, A>.attributeInfo.TryGetValue(Names<T>.Map[enumValue], out attributeValue);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007DE4 File Offset: 0x00005FE4
		private static void InitializeIfNeccessary()
		{
			if (EnumAttributeInfo<T, A>.attributeInfo == null)
			{
				lock (EnumAttributeInfo<T, A>.staticInitLock)
				{
					if (EnumAttributeInfo<T, A>.attributeInfo == null)
					{
						Dictionary<string, A> dictionary = new Dictionary<string, A>(EnumAttributeInfo<T, A>.Count, StringComparer.Ordinal);
						Type typeFromHandle = typeof(T);
						string[] map = Names<T>.Map;
						foreach (string name in map)
						{
							FieldInfo field = typeFromHandle.GetField(name);
							object[] customAttributes = field.GetCustomAttributes(typeof(A), false);
							foreach (object obj2 in customAttributes)
							{
								dictionary.Add(field.Name, (A)((object)obj2));
							}
							EnumAttributeInfo<T, A>.attributeInfo = dictionary;
						}
					}
				}
			}
		}

		// Token: 0x040000E1 RID: 225
		private static readonly int Count = Enum.GetNames(typeof(T)).Length;

		// Token: 0x040000E2 RID: 226
		private static object staticInitLock = new object();

		// Token: 0x040000E3 RID: 227
		private static Dictionary<string, A> attributeInfo;
	}
}
