using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005D1 RID: 1489
	internal class EnumTypeMap
	{
		// Token: 0x06002CD3 RID: 11475 RVA: 0x000B1548 File Offset: 0x000AF748
		private string GetEnumNameInSchema(Type enumType, string enumName)
		{
			MemberInfo memberInfo = (from x in enumType.GetTypeInfo().DeclaredMembers
			where x.Name == enumName
			select x).ElementAtOrDefault(0);
			if (memberInfo != null)
			{
				Attribute attribute = (Attribute)memberInfo.GetCustomAttributes(typeof(XmlEnumAttribute), false).ElementAtOrDefault(0);
				if (attribute != null)
				{
					XmlEnumAttribute xmlEnumAttribute = (XmlEnumAttribute)attribute;
					if (!string.IsNullOrEmpty(xmlEnumAttribute.Name))
					{
						return xmlEnumAttribute.Name;
					}
				}
			}
			return enumName;
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000B15CF File Offset: 0x000AF7CF
		internal string GetEnumName(Type enumType, object enumValue)
		{
			return this.GetEnumNameInSchema(enumType, Enum.GetName(enumType, enumValue));
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000B15E0 File Offset: 0x000AF7E0
		internal EnumTypeMap()
		{
			this.typeToValueToNameDict = new Dictionary<Type, Dictionary<int, string>>();
			this.typeToNameToValueDict = new Dictionary<Type, Dictionary<string, int>>();
			this.enumTypesWithFlags = new List<Type>();
			Assembly assembly = typeof(EnumUtilities).GetTypeInfo().Assembly;
			foreach (TypeInfo typeInfo in assembly.DefinedTypes)
			{
				Type type = typeInfo.AsType();
				if (typeInfo.IsPublic && typeInfo.IsEnum)
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
					if (typeInfo.IsDefined(typeof(FlagsAttribute), true))
					{
						this.enumTypesWithFlags.Add(type);
					}
				}
			}
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000B178C File Offset: 0x000AF98C
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
					bool flag = false;
					num = 0;
					foreach (string key in value.Split(new char[]
					{
						','
					}))
					{
						int num2;
						if (dictionary.TryGetValue(key, out num2))
						{
							flag = true;
							num |= num2;
						}
					}
					if (flag)
					{
						return (T)((object)num);
					}
				}
			}
			T result;
			try
			{
				result = (T)((object)Enum.Parse(typeof(T), value));
			}
			catch (ArgumentException)
			{
				throw new ArgumentException(string.Format("Invalid value '{0}' for enum '{1}'", value, typeof(T).FullName));
			}
			return result;
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000B1888 File Offset: 0x000AFA88
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

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000B1970 File Offset: 0x000AFB70
		internal bool IsDefined<T>(T value)
		{
			int key = Convert.ToInt32(value);
			Dictionary<int, string> dictionary;
			if (this.typeToValueToNameDict.TryGetValue(typeof(T), out dictionary))
			{
				return dictionary.ContainsKey(key);
			}
			return Enum.IsDefined(typeof(T), value);
		}

		// Token: 0x04001AF5 RID: 6901
		private Dictionary<Type, Dictionary<int, string>> typeToValueToNameDict;

		// Token: 0x04001AF6 RID: 6902
		private Dictionary<Type, Dictionary<string, int>> typeToNameToValueDict;

		// Token: 0x04001AF7 RID: 6903
		private List<Type> enumTypesWithFlags;
	}
}
