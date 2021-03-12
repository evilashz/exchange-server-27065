using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000852 RID: 2130
	internal static class PropertyUriMapper
	{
		// Token: 0x06003D63 RID: 15715 RVA: 0x000D75D4 File Offset: 0x000D57D4
		static PropertyUriMapper()
		{
			Type typeFromHandle = typeof(PropertyUriEnum);
			IEnumerable<FieldInfo> declaredFields = typeFromHandle.GetTypeInfo().DeclaredFields;
			foreach (FieldInfo fieldInfo in declaredFields)
			{
				if (fieldInfo.FieldType == typeof(PropertyUriEnum))
				{
					XmlEnumAttribute xmlEnumAttribute = (XmlEnumAttribute)fieldInfo.GetCustomAttributes(typeof(XmlEnumAttribute), false).ElementAt(0);
					PropertyUriEnum propertyUriEnum = (PropertyUriEnum)fieldInfo.GetValue(null);
					PropertyUriMapper.AddEntry(propertyUriEnum, xmlEnumAttribute.Name);
				}
			}
			typeFromHandle = typeof(DictionaryUriEnum);
			declaredFields = typeFromHandle.GetTypeInfo().DeclaredFields;
			foreach (FieldInfo fieldInfo2 in declaredFields)
			{
				if (fieldInfo2.FieldType == typeof(DictionaryUriEnum))
				{
					XmlEnumAttribute xmlEnumAttribute2 = (XmlEnumAttribute)fieldInfo2.GetCustomAttributes(typeof(XmlEnumAttribute), false).ElementAt(0);
					DictionaryUriEnum dictionaryUriEnum = (DictionaryUriEnum)fieldInfo2.GetValue(null);
					PropertyUriMapper.AddEntry(dictionaryUriEnum, xmlEnumAttribute2.Name);
				}
			}
			typeFromHandle = typeof(ExceptionPropertyUriEnum);
			declaredFields = typeFromHandle.GetTypeInfo().DeclaredFields;
			foreach (FieldInfo fieldInfo3 in declaredFields)
			{
				if (fieldInfo3.FieldType == typeof(ExceptionPropertyUriEnum))
				{
					XmlEnumAttribute xmlEnumAttribute3 = (XmlEnumAttribute)fieldInfo3.GetCustomAttributes(typeof(XmlEnumAttribute), false).ElementAt(0);
					ExceptionPropertyUriEnum exceptionPropertyUriEnum = (ExceptionPropertyUriEnum)fieldInfo3.GetValue(null);
					PropertyUriMapper.AddEntry(exceptionPropertyUriEnum, xmlEnumAttribute3.Name);
				}
			}
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x000D77F4 File Offset: 0x000D59F4
		private static void AddEntry(PropertyUriEnum propertyUriEnum, string xmlEnumValue)
		{
			PropertyUriMapper.propUriToUriMap.Add(propertyUriEnum, xmlEnumValue);
			PropertyUriMapper.uriToPropUriMap.Add(xmlEnumValue, propertyUriEnum);
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x000D780E File Offset: 0x000D5A0E
		private static void AddEntry(DictionaryUriEnum dictionaryUriEnum, string xmlEnumValue)
		{
			PropertyUriMapper.dictionaryUriToUriMap.Add(dictionaryUriEnum, xmlEnumValue);
			PropertyUriMapper.uriToDictionaryUriMap.Add(xmlEnumValue, dictionaryUriEnum);
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x000D7828 File Offset: 0x000D5A28
		private static void AddEntry(ExceptionPropertyUriEnum exceptionPropertyUriEnum, string xmlEnumValue)
		{
			PropertyUriMapper.exceptionPropertyUriToUriMap.Add(exceptionPropertyUriEnum, xmlEnumValue);
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x000D7836 File Offset: 0x000D5A36
		public static string GetXmlEnumValue(PropertyUriEnum propertyUriEnum)
		{
			return PropertyUriMapper.propUriToUriMap[propertyUriEnum];
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x000D7843 File Offset: 0x000D5A43
		public static bool TryGetPropertyUriEnum(string xmlEnumValue, out PropertyUriEnum propertyUriEnum)
		{
			return PropertyUriMapper.uriToPropUriMap.TryGetValue(xmlEnumValue, out propertyUriEnum);
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x000D7851 File Offset: 0x000D5A51
		public static string GetXmlEnumValue(DictionaryUriEnum dictionaryUriEnum)
		{
			return PropertyUriMapper.dictionaryUriToUriMap[dictionaryUriEnum];
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x000D785E File Offset: 0x000D5A5E
		public static bool TryGetDictionaryUriEnum(string xmlEnumValue, out DictionaryUriEnum dictionaryUriEnum)
		{
			return PropertyUriMapper.uriToDictionaryUriMap.TryGetValue(xmlEnumValue, out dictionaryUriEnum);
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x000D786C File Offset: 0x000D5A6C
		public static string GetXmlEnumValue(ExceptionPropertyUriEnum dictionaryUriEnum)
		{
			return PropertyUriMapper.exceptionPropertyUriToUriMap[dictionaryUriEnum];
		}

		// Token: 0x04002360 RID: 9056
		private static Dictionary<PropertyUriEnum, string> propUriToUriMap = new Dictionary<PropertyUriEnum, string>();

		// Token: 0x04002361 RID: 9057
		private static Dictionary<string, PropertyUriEnum> uriToPropUriMap = new Dictionary<string, PropertyUriEnum>();

		// Token: 0x04002362 RID: 9058
		private static Dictionary<DictionaryUriEnum, string> dictionaryUriToUriMap = new Dictionary<DictionaryUriEnum, string>();

		// Token: 0x04002363 RID: 9059
		private static Dictionary<string, DictionaryUriEnum> uriToDictionaryUriMap = new Dictionary<string, DictionaryUriEnum>();

		// Token: 0x04002364 RID: 9060
		private static Dictionary<ExceptionPropertyUriEnum, string> exceptionPropertyUriToUriMap = new Dictionary<ExceptionPropertyUriEnum, string>();
	}
}
