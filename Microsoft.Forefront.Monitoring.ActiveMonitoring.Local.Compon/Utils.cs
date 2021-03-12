﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000051 RID: 81
	internal class Utils
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000C92C File Offset: 0x0000AB2C
		internal static T GetEnumValue<T>(string value, string elementName)
		{
			T result;
			try
			{
				result = (T)((object)Enum.Parse(typeof(T), value, true));
			}
			catch (Exception innerException)
			{
				throw new ArgumentException(string.Format("Work definition error - the attribute or element '{0}' has an invalid  value: '{1}' of type '{2}'.", elementName, value, typeof(T).Name), innerException);
			}
			return result;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000C988 File Offset: 0x0000AB88
		internal static T GetProperty<T>(string propertyName, string elementName)
		{
			PropertyInfo[] properties = typeof(T).GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
				{
					object value = propertyInfo.GetValue(null, null);
					if (value is T)
					{
						return (T)((object)value);
					}
				}
			}
			throw new ArgumentException(string.Format("Work definition error - attribute or element {0} has an invalid value '{1}' of type '{2}'.", elementName, propertyName, typeof(T).Name));
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000CA0C File Offset: 0x0000AC0C
		internal static string SerializeToXml(object o)
		{
			if (o == null)
			{
				return null;
			}
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new DataContractSerializer(o.GetType()).WriteObject(memoryStream, o);
				@string = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000CA64 File Offset: 0x0000AC64
		internal static XElement SerializeToXmlElement(object o)
		{
			XElement result = null;
			string text = Utils.SerializeToXml(o);
			if (!string.IsNullOrWhiteSpace(text))
			{
				result = XElement.Parse(text);
			}
			return result;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		internal static object DeserializeFromXml(string xml, Type type)
		{
			if (string.IsNullOrWhiteSpace(xml) || type == null)
			{
				return null;
			}
			object result;
			using (XmlReader xmlReader = new XmlTextReader(new StringReader(xml)))
			{
				result = new DataContractSerializer(type).ReadObject(xmlReader);
			}
			return result;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		internal static TimeSpan GetTimeSpan(string strValue, string elementName, TimeSpan defaultValue)
		{
			if (string.IsNullOrWhiteSpace(strValue))
			{
				return defaultValue;
			}
			TimeSpan timeSpan;
			if (TimeSpan.TryParse(strValue, out timeSpan) && timeSpan > TimeSpan.Zero)
			{
				return timeSpan;
			}
			throw new ArgumentException(string.Format("Work definition error - the attribute or element '{0}' has an invalid value '{1}' of type 'TimeSpan'.", elementName, strValue));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000CB28 File Offset: 0x0000AD28
		internal static DateTime GetDateTime(string strValue, string elementName)
		{
			DateTime result;
			if (!string.IsNullOrWhiteSpace(strValue) && DateTime.TryParse(strValue, null, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
			{
				return result;
			}
			throw new ArgumentException(string.Format("Work definition error - the attribute or element '{0}' has an invalid value '{1}' of type 'DateTime'.", elementName, strValue));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000CB60 File Offset: 0x0000AD60
		internal static int GetInteger(string strValue, string elementName, int defaultValue, int minValue)
		{
			if (string.IsNullOrWhiteSpace(strValue))
			{
				return defaultValue;
			}
			int num;
			if (int.TryParse(strValue, out num) && num >= minValue)
			{
				return num;
			}
			throw new ArgumentException(string.Format("Work definition error - the attribute or element '{0}' has an invalid value '{1}' of type '{2}'. It should be greater than or equal to {3}.", new object[]
			{
				elementName,
				strValue,
				typeof(int).Name,
				minValue
			}));
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000CBC1 File Offset: 0x0000ADC1
		internal static int GetPositiveInteger(string strValue, string elementName)
		{
			return Utils.GetInteger(strValue, elementName, 0, 1);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		internal static bool GetBoolean(string strValue, string elementName, bool defaultValue)
		{
			if (string.IsNullOrWhiteSpace(strValue))
			{
				return defaultValue;
			}
			return Utils.GetBoolean(strValue, elementName);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		internal static bool GetBoolean(string strValue, string elementName)
		{
			bool result;
			if (bool.TryParse(strValue, out result))
			{
				return result;
			}
			throw new ArgumentException(string.Format("Work definition error - the attribute or element '{0}' has an invalid value '{1}' of type '{2}'.", elementName, strValue, typeof(bool).Name));
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000CC19 File Offset: 0x0000AE19
		internal static string CheckNullOrWhiteSpace(string s, string elementName)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				throw new ArgumentException(string.Format("Work definition error -  attribute or element '{0}' missing or empty.", elementName));
			}
			return s;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000CC35 File Offset: 0x0000AE35
		internal static XmlNode CheckNode(XmlNode node, string elementName)
		{
			if (node == null)
			{
				throw new ArgumentException(string.Format("Work definition error - node '{0}' missing.", elementName));
			}
			return node;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		internal static XmlElement CheckXmlElement(XmlNode node, string elementName)
		{
			Utils.CheckNode(node, elementName);
			XmlElement xmlElement = node as XmlElement;
			if (xmlElement == null)
			{
				throw new ArgumentException(string.Format("Work definition error - node '{0}' is not an XML element.", elementName));
			}
			return xmlElement;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000CC80 File Offset: 0x0000AE80
		internal static T GetMandatoryXmlAttribute<T>(XmlNode definition, string attributeName)
		{
			T t = default(T);
			XmlAttribute xmlAttribute = Utils.GetXmlAttribute(definition, attributeName, true);
			return (T)((object)Convert.ChangeType(xmlAttribute.Value, typeof(T)));
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		internal static T GetOptionalXmlAttribute<T>(XmlNode definition, string attributeName, T defaultValue)
		{
			T result = default(T);
			XmlAttribute xmlAttribute = Utils.GetXmlAttribute(definition, attributeName, false);
			if (xmlAttribute == null)
			{
				result = defaultValue;
			}
			else
			{
				result = (T)((object)Convert.ChangeType(xmlAttribute.Value, typeof(T)));
			}
			return result;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000CD00 File Offset: 0x0000AF00
		internal static T GetMandatoryXmlEnumAttribute<T>(XmlNode definition, string attributeName)
		{
			XmlAttribute xmlAttribute = Utils.GetXmlAttribute(definition, attributeName, true);
			T result;
			try
			{
				result = (T)((object)Enum.Parse(typeof(T), xmlAttribute.Value, true));
			}
			catch (Exception innerException)
			{
				throw new ArgumentException(string.Format("Work definition error - the attribute or element '{0}' has an invalid  value: '{1}' of type '{2}'.", attributeName, xmlAttribute.Value, typeof(T).Name), innerException);
			}
			return result;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000CD70 File Offset: 0x0000AF70
		internal static T GetOptionalXmlEnumAttribute<T>(XmlNode definition, string attributeName, T defaultValue)
		{
			XmlAttribute xmlAttribute = Utils.GetXmlAttribute(definition, attributeName, false);
			T result = defaultValue;
			if (xmlAttribute != null && !string.IsNullOrEmpty(xmlAttribute.Value) && Enum.IsDefined(typeof(T), xmlAttribute.Value))
			{
				result = (T)((object)Enum.Parse(typeof(T), xmlAttribute.Value));
			}
			return result;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		internal static XmlAttribute GetXmlAttribute(XmlNode definition, string attributeName, bool throwOnFailure)
		{
			if (definition == null)
			{
				throw new ArgumentNullException("definition");
			}
			XmlAttribute xmlAttribute = definition.Attributes[attributeName];
			if (xmlAttribute == null)
			{
				string text = string.Format("Attribute {0} was not found in the WorkDefinition xml.", attributeName);
				XmlAttribute xmlAttribute2 = definition.Attributes["Name"];
				if (xmlAttribute2 != null)
				{
					text = string.Format("{0} WorkDefinition name was {1}", text, xmlAttribute2.Value);
				}
				if (throwOnFailure)
				{
					throw new XmlException(text);
				}
			}
			return xmlAttribute;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000D05C File Offset: 0x0000B25C
		internal static IEnumerable<XmlNode> GetDescendants(XmlNode node, string descendantName)
		{
			string filterString = string.Format("descendant::{0}", descendantName);
			using (XmlNodeList deploymentNodes = node.SelectNodes(filterString))
			{
				foreach (object obj in deploymentNodes)
				{
					XmlNode elementXml = (XmlNode)obj;
					yield return elementXml;
				}
			}
			yield break;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		internal static IEnumerable<XmlNode> GetDescendantsContainingFilter(XmlNode node, string filter)
		{
			string filterString = string.Format("descendant::*[contains(name(),'{0}')]", filter);
			using (XmlNodeList deploymentNodes = node.SelectNodes(filterString))
			{
				foreach (object obj in deploymentNodes)
				{
					XmlNode elementXml = (XmlNode)obj;
					yield return elementXml;
				}
			}
			yield break;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		internal static string TryGetStringValueFromRegistry(string regPath, string regName, string defaultValue)
		{
			string result = defaultValue;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regPath))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue(regName);
					if (value != null)
					{
						result = value.ToString();
					}
				}
			}
			return result;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000D31C File Offset: 0x0000B51C
		internal static string GetStringValueFromRegistry(string regPath, string regName)
		{
			string text = Utils.TryGetStringValueFromRegistry(regPath, regName, string.Empty);
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentException(string.Format("Missing registry setting (keyPath=HKLM\\{0}, name={1}).", regPath, regName));
			}
			return text;
		}
	}
}
