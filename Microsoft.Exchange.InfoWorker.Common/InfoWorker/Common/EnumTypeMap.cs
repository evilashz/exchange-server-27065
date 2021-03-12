using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200000E RID: 14
	internal class EnumTypeMap
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002888 File Offset: 0x00000A88
		private string GetEnumNameInSchema(Type enumType, string enumName)
		{
			MemberInfo[] member = enumType.GetMember(enumName);
			if (member == null || member.Length <= 0)
			{
				return enumName;
			}
			object[] customAttributes = member[0].GetCustomAttributes(typeof(XmlEnumAttribute), false);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				return ((XmlEnumAttribute)customAttributes[0]).Name;
			}
			return enumName;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028D4 File Offset: 0x00000AD4
		internal string GetEnumName(Type enumType, object enumValue)
		{
			return this.GetEnumNameInSchema(enumType, Enum.GetName(enumType, enumValue));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028E4 File Offset: 0x00000AE4
		internal EnumTypeMap()
		{
			this.typeToValueToNameDict = new Dictionary<Type, Dictionary<int, string>>();
			this.typeToNameToValueDict = new Dictionary<Type, Dictionary<string, int>>();
			this.enumTypesWithFlags = new List<Type>();
			Assembly assembly = typeof(EnumUtil).Assembly;
			foreach (Type type in assembly.GetTypes())
			{
				if (type.IsPublic && type.IsEnum)
				{
					Dictionary<int, string> dictionary = new Dictionary<int, string>();
					foreach (object obj in Enum.GetValues(type))
					{
						string enumName = this.GetEnumName(type, obj);
						dictionary.Add((int)obj, enumName);
					}
					this.typeToValueToNameDict.Add(type, dictionary);
					Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
					foreach (string text in Enum.GetNames(type))
					{
						string enumNameInSchema = this.GetEnumNameInSchema(type, text);
						dictionary2.Add(enumNameInSchema, (int)Enum.Parse(type, text));
					}
					this.typeToNameToValueDict.Add(type, dictionary2);
					if (type.IsDefined(typeof(FlagsAttribute), true))
					{
						this.enumTypesWithFlags.Add(type);
					}
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A50 File Offset: 0x00000C50
		internal T Parse<T>(string value)
		{
			Dictionary<string, int> dictionary;
			if (this.typeToNameToValueDict.TryGetValue(typeof(T), out dictionary))
			{
				int num;
				if (dictionary.TryGetValue(value, out num))
				{
					return (T)((object)num);
				}
				if (this.enumTypesWithFlags.Contains(typeof(T)))
				{
					num = 0;
					foreach (string key in value.Split(new char[]
					{
						','
					}))
					{
						int num2;
						if (dictionary.TryGetValue(key, out num2))
						{
							num |= num2;
						}
					}
					return (T)((object)num);
				}
			}
			return (T)((object)Enum.Parse(typeof(T), value));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B08 File Offset: 0x00000D08
		internal string ToString<T>(T value)
		{
			int num = Convert.ToInt32(value);
			Dictionary<int, string> dictionary;
			if (this.typeToValueToNameDict.TryGetValue(typeof(T), out dictionary))
			{
				string result;
				if (dictionary.TryGetValue(num, out result))
				{
					return result;
				}
				if (this.enumTypesWithFlags.Contains(typeof(T)))
				{
					StringBuilder stringBuilder = new StringBuilder();
					bool flag = true;
					foreach (int num2 in dictionary.Keys)
					{
						if ((num & num2) == num2)
						{
							if (!flag)
							{
								stringBuilder.Append(", ");
							}
							stringBuilder.Append(dictionary[num2]);
							flag = false;
							num -= num2;
						}
					}
					return stringBuilder.ToString();
				}
			}
			return value.ToString();
		}

		// Token: 0x0400002D RID: 45
		private Dictionary<Type, Dictionary<int, string>> typeToValueToNameDict;

		// Token: 0x0400002E RID: 46
		private Dictionary<Type, Dictionary<string, int>> typeToNameToValueDict;

		// Token: 0x0400002F RID: 47
		private List<Type> enumTypesWithFlags;
	}
}
