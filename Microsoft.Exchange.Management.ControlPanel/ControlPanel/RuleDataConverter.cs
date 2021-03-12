using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200042A RID: 1066
	public static class RuleDataConverter
	{
		// Token: 0x06003565 RID: 13669 RVA: 0x000A5F28 File Offset: 0x000A4128
		public static string[] ToStringArray(this Array array)
		{
			if (array.IsNullOrEmpty())
			{
				return null;
			}
			string[] array2 = new string[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array.GetValue(i).ToString();
			}
			return array2;
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000A5F6C File Offset: 0x000A416C
		public static string[] ToStringArray(this MultiValuedProperty<string> stringProperty)
		{
			if (MultiValuedPropertyBase.IsNullOrEmpty(stringProperty))
			{
				return null;
			}
			return stringProperty.ToArray();
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000A5F80 File Offset: 0x000A4180
		public static string[] ToStringArray<T>(this MultiValuedProperty<T> multiValuedProperty)
		{
			if (multiValuedProperty == null)
			{
				return new string[0];
			}
			string[] array = new string[multiValuedProperty.Count];
			for (int i = 0; i < multiValuedProperty.Count; i++)
			{
				string[] array2 = array;
				int num = i;
				T t = multiValuedProperty[i];
				array2[num] = t.ToString();
			}
			return array;
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000A5FD0 File Offset: 0x000A41D0
		public static int ToKB(this ByteQuantifiedSize? size)
		{
			if (size != null)
			{
				return (int)size.Value.ToKB();
			}
			return 0;
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000A5FF8 File Offset: 0x000A41F8
		public static ByteQuantifiedSize? ToByteSize(this int size)
		{
			ByteQuantifiedSize? result = new ByteQuantifiedSize?(default(ByteQuantifiedSize));
			if (size == 0)
			{
				result = null;
			}
			else
			{
				result = new ByteQuantifiedSize?(ByteQuantifiedSize.Parse(size.ToString() + ByteQuantifiedSize.Quantifier.KB.ToString()));
			}
			return result;
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000A604B File Offset: 0x000A424B
		public static string ToCommaSeperatedString(this string[] stringArray)
		{
			if (stringArray.IsNullOrEmpty())
			{
				return null;
			}
			return string.Join(",", stringArray);
		}
	}
}
