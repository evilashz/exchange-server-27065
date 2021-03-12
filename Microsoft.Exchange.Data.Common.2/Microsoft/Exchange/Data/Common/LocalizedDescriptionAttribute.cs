using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace Microsoft.Exchange.Data.Common
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public class LocalizedDescriptionAttribute : DescriptionAttribute, ILocalizedString
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002637 File Offset: 0x00000837
		public LocalizedDescriptionAttribute()
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000263F File Offset: 0x0000083F
		public LocalizedDescriptionAttribute(LocalizedString description)
		{
			this.description = description;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000264E File Offset: 0x0000084E
		public LocalizedString LocalizedString
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002656 File Offset: 0x00000856
		public static string FromEnum(Type enumType, object value)
		{
			return LocalizedDescriptionAttribute.FromEnum(enumType, value, null);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002688 File Offset: 0x00000888
		public static string FromEnum(Type enumType, object value, CultureInfo culture)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.GetTypeInfo().IsEnum)
			{
				throw new ArgumentException("enumType must be an enum.", "enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			object obj = Enum.ToObject(enumType, value);
			if (LocalizedDescriptionAttribute.locEnumStringTable == null)
			{
				Dictionary<string, Dictionary<object, string>> value2 = new Dictionary<string, Dictionary<object, string>>();
				Interlocked.CompareExchange<Dictionary<string, Dictionary<object, string>>>(ref LocalizedDescriptionAttribute.locEnumStringTable, value2, null);
			}
			string text;
			lock (LocalizedDescriptionAttribute.locEnumStringTable)
			{
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				Dictionary<object, string> dictionary;
				if (!LocalizedDescriptionAttribute.locEnumStringTable.TryGetValue(culture.Name, out dictionary))
				{
					dictionary = new Dictionary<object, string>();
					LocalizedDescriptionAttribute.locEnumStringTable.Add(culture.Name, dictionary);
				}
				if (dictionary.TryGetValue(obj, out text))
				{
					return text;
				}
				string[] array = obj.ToString().Split(new char[]
				{
					','
				});
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					string fieldName = array[i].Trim();
					string value3 = fieldName;
					foreach (FieldInfo fieldInfo in from x in enumType.GetTypeInfo().DeclaredFields
					where x.Name == fieldName
					select x)
					{
						using (IEnumerator<object> enumerator2 = fieldInfo.GetCustomAttributes(false).Where((object x) => x is DescriptionAttribute).GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj3 = enumerator2.Current;
								if (obj3 is LocalizedDescriptionAttribute)
								{
									value3 = ((LocalizedDescriptionAttribute)obj3).LocalizedString.ToString(culture);
								}
								else
								{
									value3 = ((DescriptionAttribute)obj3).Description;
								}
							}
						}
					}
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(value3);
				}
				text = stringBuilder.ToString();
				dictionary.Add(obj, text);
			}
			return text;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000290C File Offset: 0x00000B0C
		public sealed override string Description
		{
			[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
			get
			{
				return this.description;
			}
		}

		// Token: 0x0400000C RID: 12
		private static Dictionary<string, Dictionary<object, string>> locEnumStringTable;

		// Token: 0x0400000D RID: 13
		private LocalizedString description;
	}
}
