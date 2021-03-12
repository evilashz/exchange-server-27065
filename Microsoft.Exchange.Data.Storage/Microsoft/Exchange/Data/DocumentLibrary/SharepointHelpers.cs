using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006EB RID: 1771
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SharepointHelpers
	{
		// Token: 0x0600464A RID: 17994 RVA: 0x0012B538 File Offset: 0x00129738
		private SharepointHelpers()
		{
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x0012B540 File Offset: 0x00129740
		static SharepointHelpers()
		{
			SharepointHelpers.SharepointNamespaceManager = new XmlNamespaceManager(new NameTable());
			SharepointHelpers.SharepointNamespaceManager.AddNamespace("sp", "http://schemas.microsoft.com/sharepoint/soap/");
			SharepointHelpers.SharepointNamespaceManager.AddNamespace("rs", "urn:schemas-microsoft-com:rowset");
			SharepointHelpers.SharepointNamespaceManager.AddNamespace("z", "#RowsetSchema");
			XmlDocument xmlDocument = new SafeXmlDocument();
			SharepointHelpers.DefaultQueryOptions = xmlDocument.CreateElement("QueryOptions");
			XmlNode xmlNode = xmlDocument.CreateElement("DateInUtc");
			xmlNode.InnerText = "TRUE";
			SharepointHelpers.DefaultQueryOptions.AppendChild(xmlNode);
			xmlNode = xmlDocument.CreateElement("IncludeMandatoryColumns");
			xmlNode.InnerText = "TRUE";
			SharepointHelpers.DefaultQueryOptions.AppendChild(xmlNode);
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x0012B5FE File Offset: 0x001297FE
		internal static object[] GetValuesFromCAMLView(Schema schema, XmlNode xmlNode, CultureInfo cultureInfo, params PropertyDefinition[] propertyDefinitions)
		{
			return SharepointHelpers.GetValuesFromCAMLView(schema, xmlNode, cultureInfo, (IList<PropertyDefinition>)propertyDefinitions);
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x0012B610 File Offset: 0x00129810
		internal static object[] GetValuesFromCAMLView(Schema schema, XmlNode xmlNode, CultureInfo cultureInfo, IList<PropertyDefinition> propertyDefinitions)
		{
			object[] array = new object[propertyDefinitions.Count];
			string str = string.Empty;
			if (xmlNode.LocalName == "row")
			{
				str = "ows_";
			}
			int i = 0;
			while (i < propertyDefinitions.Count)
			{
				SharepointPropertyDefinition sharepointPropertyDefinition = SharepointPropertyDefinition.PropertyDefinitionToSharepointPropertyDefinition(schema, propertyDefinitions[i]);
				if (sharepointPropertyDefinition != null)
				{
					string name = str + sharepointPropertyDefinition.SharepointName;
					try
					{
						if (xmlNode.Attributes[name] == null || (array[i] = sharepointPropertyDefinition.FromSharepoint(xmlNode.Attributes[name].Value, cultureInfo)) == null)
						{
							array[i] = new PropertyError(propertyDefinitions[i], PropertyErrorCode.NotFound);
						}
						goto IL_DC;
					}
					catch (FormatException)
					{
						array[i] = new PropertyError(propertyDefinitions[i], PropertyErrorCode.CorruptData);
						goto IL_DC;
					}
					goto IL_AC;
				}
				goto IL_AC;
				IL_DC:
				i++;
				continue;
				IL_AC:
				if (propertyDefinitions[i] is DocumentLibraryPropertyDefinition)
				{
					array[i] = new PropertyError(propertyDefinitions[i], PropertyErrorCode.NotFound);
					goto IL_DC;
				}
				array[i] = new PropertyError(propertyDefinitions[i], PropertyErrorCode.NotSupported);
				goto IL_DC;
			}
			return array;
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x0012B71C File Offset: 0x0012991C
		internal static XmlNode GenerateViewFieldCAML(Schema schema, ICollection<PropertyDefinition> propertyDefinitions)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = xmlDocument.CreateElement("ViewFields");
			foreach (SharepointPropertyDefinition sharepointPropertyDefinition in SharepointPropertyDefinition.PropertyDefinitionsToSharepointPropertyDefinitions(schema, propertyDefinitions))
			{
				if (sharepointPropertyDefinition != null)
				{
					XmlNode xmlNode2 = xmlDocument.CreateElement("FieldRef");
					xmlNode2.Attributes.Append(xmlDocument.CreateAttribute("Name"));
					xmlNode2.Attributes["Name"].Value = sharepointPropertyDefinition.SharepointName;
					xmlNode.AppendChild(xmlNode2);
				}
			}
			return xmlNode;
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x0012B7A8 File Offset: 0x001299A8
		private static XmlNode GenerateQueryCAMLHelper(QueryFilter query, XmlDocument xmlDoc, int depth)
		{
			if (depth >= SharepointHelpers.MaxFilterHierarchyDepth)
			{
				throw new ArgumentException("filter");
			}
			XmlNode xmlNode = null;
			if (query is AndFilter)
			{
				AndFilter andFilter = (AndFilter)query;
				using (ReadOnlyCollection<QueryFilter>.Enumerator enumerator = andFilter.Filters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QueryFilter query2 = enumerator.Current;
						if (xmlNode == null)
						{
							xmlNode = SharepointHelpers.GenerateQueryCAMLHelper(query2, xmlDoc, depth + 1);
						}
						else
						{
							XmlNode newChild = xmlNode;
							xmlNode = xmlDoc.CreateElement("And");
							xmlNode.AppendChild(newChild);
							xmlNode.AppendChild(SharepointHelpers.GenerateQueryCAMLHelper(query2, xmlDoc, depth + 1));
						}
					}
					return xmlNode;
				}
			}
			if (query is OrFilter)
			{
				OrFilter orFilter = (OrFilter)query;
				using (ReadOnlyCollection<QueryFilter>.Enumerator enumerator2 = orFilter.Filters.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						QueryFilter query3 = enumerator2.Current;
						if (xmlNode == null)
						{
							xmlNode = SharepointHelpers.GenerateQueryCAMLHelper(query3, xmlDoc, depth + 1);
						}
						else
						{
							XmlNode newChild2 = xmlNode;
							xmlNode = xmlDoc.CreateElement("Or");
							xmlNode.AppendChild(newChild2);
							xmlNode.AppendChild(SharepointHelpers.GenerateQueryCAMLHelper(query3, xmlDoc, depth + 1));
						}
					}
					return xmlNode;
				}
			}
			if (!(query is ComparisonFilter))
			{
				throw new NotSupportedException(Strings.ExFilterNotSupported(query.GetType()));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)query;
			switch (comparisonFilter.ComparisonOperator)
			{
			case ComparisonOperator.Equal:
				xmlNode = xmlDoc.CreateElement("Eq");
				break;
			case ComparisonOperator.NotEqual:
				xmlNode = xmlDoc.CreateElement("Neq");
				break;
			case ComparisonOperator.LessThan:
				xmlNode = xmlDoc.CreateElement("Lt");
				break;
			case ComparisonOperator.LessThanOrEqual:
				xmlNode = xmlDoc.CreateElement("Leq");
				break;
			case ComparisonOperator.GreaterThan:
				xmlNode = xmlDoc.CreateElement("Gt");
				break;
			case ComparisonOperator.GreaterThanOrEqual:
				xmlNode = xmlDoc.CreateElement("Geq");
				break;
			default:
				throw new InvalidOperationException("Invalid comparison operator");
			}
			SharepointPropertyDefinition sharepointPropertyDefinition = (SharepointPropertyDefinition)comparisonFilter.Property;
			xmlNode.InnerXml = string.Format("<FieldRef Name=\"{0}\" /><Value Type=\"{1}\">{2}</Value>", sharepointPropertyDefinition.SharepointName, sharepointPropertyDefinition.FieldTypeAsString, sharepointPropertyDefinition.ToSharepoint(comparisonFilter.PropertyValue, null));
			return xmlNode;
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x0012B9D0 File Offset: 0x00129BD0
		internal static XmlNode GenerateQueryCAML(QueryFilter query)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = xmlDocument.CreateElement("Query");
			if (query != null)
			{
				xmlNode.AppendChild(xmlDocument.CreateElement("Where"));
				xmlNode.ChildNodes[0].AppendChild(SharepointHelpers.GenerateQueryCAMLHelper(query, xmlDocument, 0));
			}
			return xmlNode;
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x0012BA20 File Offset: 0x00129C20
		internal static XmlNode GenerateQueryOptionsXml(IList<string> itemHierarchy)
		{
			string folderPath = null;
			if (itemHierarchy != null && itemHierarchy.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < itemHierarchy.Count; i++)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append("/");
					}
					stringBuilder.Append(itemHierarchy[i]);
				}
				folderPath = stringBuilder.ToString();
			}
			return SharepointHelpers.GenerateQueryOptionsXml(folderPath);
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x0012BA84 File Offset: 0x00129C84
		internal static XmlNode GenerateQueryOptionsXml(string folderPath)
		{
			XmlNode xmlNode = SharepointHelpers.DefaultQueryOptions.CloneNode(true);
			if (folderPath != null)
			{
				XmlNode xmlNode2 = xmlNode.OwnerDocument.CreateElement("Folder");
				xmlNode2.InnerText = folderPath;
				xmlNode.AppendChild(xmlNode2);
			}
			return xmlNode;
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x0012BAC1 File Offset: 0x00129CC1
		internal static object NoOp(string obj, CultureInfo cultureInfo)
		{
			return obj;
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x0012BAC4 File Offset: 0x00129CC4
		internal static string ObjectToString(object obj, CultureInfo cultureInfo)
		{
			return obj.ToString();
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x0012BACC File Offset: 0x00129CCC
		internal static object ExtensionToContentType(string str, CultureInfo cultureInfo)
		{
			return ExtensionToContentTypeMapper.Instance.GetContentTypeByExtension(str);
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x0012BADC File Offset: 0x00129CDC
		internal static object SecondJoinedValue(string str, CultureInfo cultureInfo)
		{
			int num = str.IndexOf(";#");
			if (num == -1)
			{
				return null;
			}
			return str.Substring(num + ";#".Length);
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x0012BB10 File Offset: 0x00129D10
		internal static string DateTimeToSharepoint(object obj, CultureInfo cultureInfo)
		{
			return string.Format("{0:s}Z", (DateTime)((ExDateTime)obj).ToUtc());
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x0012BB3F File Offset: 0x00129D3F
		internal static object SharepointToDateTime(string obj, CultureInfo cultureInfo)
		{
			return ExDateTime.Parse(obj, cultureInfo.DateTimeFormat);
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x0012BB54 File Offset: 0x00129D54
		internal static object SharepoinToListDateTime(string obj, CultureInfo cultureInfo)
		{
			DateTimeFormatInfo currentInfo = DateTimeFormatInfo.CurrentInfo;
			return ExDateTime.ParseExact(obj, new string[]
			{
				"M/d/yyyy h:mm:ss tt",
				"yyyyMMdd HH:mm:ss"
			}, currentInfo, DateTimeStyles.None);
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x0012BB8C File Offset: 0x00129D8C
		internal static object SharepointJoinedToDateTime(string obj, CultureInfo cultureInfo)
		{
			return SharepointHelpers.SharepointToDateTime((string)SharepointHelpers.SecondJoinedValue(obj, cultureInfo), cultureInfo);
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x0012BBA0 File Offset: 0x00129DA0
		internal static object SharepointJoinedToInt(string obj, CultureInfo cultureInfo)
		{
			string value = (string)SharepointHelpers.SecondJoinedValue(obj, cultureInfo);
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			return int.Parse((string)SharepointHelpers.SecondJoinedValue(obj, cultureInfo));
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x0012BBDC File Offset: 0x00129DDC
		internal static object SharepointJoinedToLong(string obj, CultureInfo cultureInfo)
		{
			string value = (string)SharepointHelpers.SecondJoinedValue(obj, cultureInfo);
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			return long.Parse((string)SharepointHelpers.SecondJoinedValue(obj, cultureInfo));
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x0012BC16 File Offset: 0x00129E16
		internal static object SharepointIsFolderToBool(string obj, CultureInfo cultureInfo)
		{
			return (int)SharepointHelpers.SharepointJoinedToInt(obj, cultureInfo) != 0;
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x0012BC2F File Offset: 0x00129E2F
		public static object SharepointToInt(string obj, CultureInfo cultureInfo)
		{
			return int.Parse(obj);
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x0012BC3C File Offset: 0x00129E3C
		public static object SharepointToAbsoluteUri(string obj, CultureInfo cultureInfo)
		{
			return new Uri(obj, UriKind.Absolute);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x0012BC45 File Offset: 0x00129E45
		public static object SharepointToRelateiveUri(string obj, CultureInfo cultureInfo)
		{
			return new Uri(obj, UriKind.Relative);
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x0012BC4E File Offset: 0x00129E4E
		public static object SharepointToBool(string obj, CultureInfo cultureInfo)
		{
			return bool.Parse(obj);
		}

		// Token: 0x04002680 RID: 9856
		internal static XmlNamespaceManager SharepointNamespaceManager;

		// Token: 0x04002681 RID: 9857
		internal static XmlNode DefaultQueryOptions;

		// Token: 0x04002682 RID: 9858
		internal static int MaxFilterHierarchyDepth = Utils.MaxFilterDepth;
	}
}
