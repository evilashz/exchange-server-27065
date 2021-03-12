using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200020F RID: 527
	internal static class ServiceXml
	{
		// Token: 0x06000DA4 RID: 3492 RVA: 0x00044024 File Offset: 0x00042224
		static ServiceXml()
		{
			ServiceXml.namespacePrefixes.Add("http://schemas.microsoft.com/exchange/services/2006/types", "t");
			ServiceXml.namespacePrefixes.Add("http://schemas.microsoft.com/exchange/services/2006/messages", "m");
			ServiceXml.namespacePrefixes.Add("http://schemas.microsoft.com/exchange/services/2006/errors", "e");
			ServiceXml.namespacePrefixes.Add("http://schemas.xmlsoap.org/soap/envelope/", "soap11");
			ServiceXml.namespacePrefixes.Add("http://www.w3.org/2003/05/soap-envelope", "soap12");
			ServiceXml.InitNamespaceManager();
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000440A6 File Offset: 0x000422A6
		public static string GetFullyQualifiedName(string name, string namespaceUri)
		{
			return namespaceUri + ":" + name;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000440B4 File Offset: 0x000422B4
		public static string GetFullyQualifiedName(string name)
		{
			return ServiceXml.GetFullyQualifiedName(name, ServiceXml.DefaultNamespaceUri);
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000440C1 File Offset: 0x000422C1
		public static string DefaultNamespaceUri
		{
			get
			{
				return "http://schemas.microsoft.com/exchange/services/2006/types";
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000440C8 File Offset: 0x000422C8
		private static bool IsTextOnlyWhitespace(string nodeValue)
		{
			foreach (char c in nodeValue)
			{
				if (!char.IsWhiteSpace(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x000440FF File Offset: 0x000422FF
		public static string ConvertToString(object value)
		{
			return BaseConverter.GetConverterForType(value.GetType()).ConvertToString(value);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00044114 File Offset: 0x00042314
		public static void InitNamespaceManager()
		{
			ServiceXml.namespaceManager = new XmlNamespaceManager(new NameTable());
			ServiceXml.namespaceManager.AddNamespace("t", "http://schemas.microsoft.com/exchange/services/2006/types");
			ServiceXml.namespaceManager.AddNamespace("m", "http://schemas.microsoft.com/exchange/services/2006/messages");
			ServiceXml.namespaceManager.AddNamespace("e", "http://schemas.microsoft.com/exchange/services/2006/errors");
			ServiceXml.namespaceManager.AddNamespace("soap11", "http://schemas.xmlsoap.org/soap/envelope/");
			ServiceXml.namespaceManager.AddNamespace("soap12", "http://www.w3.org/2003/05/soap-envelope");
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00044194 File Offset: 0x00042394
		public static XmlNamespaceManager NamespaceManager
		{
			get
			{
				return ServiceXml.namespaceManager;
			}
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0004419B File Offset: 0x0004239B
		public static XmlElement CreateElement(XmlDocument xmlDocument, string localName, string namespaceUri)
		{
			return xmlDocument.CreateElement(ServiceXml.namespacePrefixes[namespaceUri], localName, namespaceUri);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x000441B0 File Offset: 0x000423B0
		public static string GetXmlTextNodeValue(XmlElement textNodeParent)
		{
			return textNodeParent.InnerText;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x000441B8 File Offset: 0x000423B8
		public static string GetXmlTextNodeValue(XmlElement xmlParentNode, string xmlElementName, string typeNamespace)
		{
			XmlElement xmlElement = xmlParentNode[xmlElementName, typeNamespace];
			if (xmlElement != null)
			{
				return xmlElement.InnerText;
			}
			return null;
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000441D9 File Offset: 0x000423D9
		public static string GetXmlElementAttributeValueOptional(XmlElement xmlParentNode, string xmlAttributeName)
		{
			if (xmlParentNode.HasAttribute(xmlAttributeName))
			{
				return xmlParentNode.Attributes[xmlAttributeName].Value;
			}
			return null;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000441F8 File Offset: 0x000423F8
		public static ServiceXml.NormalizationAction GetNormalizationAction(XmlNode node)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
				if (ServiceXml.IsLeafNode(node))
				{
					return ServiceXml.NormalizationAction.LeaveAsIs;
				}
				return ServiceXml.NormalizationAction.Normalize;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				return ServiceXml.NormalizationAction.Remove;
			}
			return ServiceXml.NormalizationAction.Remove;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00044238 File Offset: 0x00042438
		public static void NormalizeXml(XmlNode xmlNode)
		{
			for (int i = xmlNode.ChildNodes.Count - 1; i >= 0; i--)
			{
				XmlNode xmlNode2 = xmlNode.ChildNodes[i];
				switch (ServiceXml.GetNormalizationAction(xmlNode2))
				{
				case ServiceXml.NormalizationAction.Normalize:
					ServiceXml.NormalizeXml(xmlNode2);
					break;
				case ServiceXml.NormalizationAction.Remove:
					xmlNode.RemoveChild(xmlNode2);
					break;
				}
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00044295 File Offset: 0x00042495
		public static void NormalizeXmlHandleNullNode(XmlNode xmlNode)
		{
			if (xmlNode != null)
			{
				ServiceXml.NormalizeXml(xmlNode);
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000442A0 File Offset: 0x000424A0
		public static XmlElement CreateElement(XmlElement parentElement, string localName, string namespaceUri)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(parentElement.OwnerDocument, localName, namespaceUri);
			parentElement.AppendChild(xmlElement);
			return xmlElement;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000442C4 File Offset: 0x000424C4
		public static XmlElement CreateElement(XmlElement parentElement, string localName)
		{
			return ServiceXml.CreateElement(parentElement, localName, parentElement.NamespaceURI);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000442D4 File Offset: 0x000424D4
		public static XmlElement CreateTextElement(XmlElement parentElement, string localName, string textValue, string namespaceUri)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(parentElement, localName, namespaceUri);
			ServiceXml.AppendText(xmlElement, textValue);
			return xmlElement;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000442F4 File Offset: 0x000424F4
		public static XmlElement CreateTextElement(XmlElement parentElement, string localName, XmlText textNode, string namespaceUri)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(parentElement, localName, namespaceUri);
			xmlElement.AppendChild(textNode);
			return xmlElement;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00044314 File Offset: 0x00042514
		private static void AppendText(XmlElement parentElement, string textValue)
		{
			if (!string.IsNullOrEmpty(textValue))
			{
				XmlText newChild = parentElement.OwnerDocument.CreateTextNode(textValue);
				parentElement.AppendChild(newChild);
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0004433E File Offset: 0x0004253E
		public static XmlElement CreateTextElement(XmlElement parentElement, string localName, string textValue)
		{
			return ServiceXml.CreateTextElement(parentElement, localName, textValue, parentElement.NamespaceURI);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0004434E File Offset: 0x0004254E
		public static XmlElement CreateNonEmptyTextElement(XmlElement xmlElement, string xmlElementName, string xmlElementValue)
		{
			if (string.IsNullOrEmpty(xmlElementValue))
			{
				return null;
			}
			return ServiceXml.CreateTextElement(xmlElement, xmlElementName, xmlElementValue);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00044364 File Offset: 0x00042564
		public static XmlAttribute CreateAttribute(XmlElement parentElement, string attributeName, string attributeValue)
		{
			XmlAttribute xmlAttribute = parentElement.OwnerDocument.CreateAttribute(string.Empty, attributeName, string.Empty);
			xmlAttribute.Value = attributeValue;
			parentElement.Attributes.Append(xmlAttribute);
			return xmlAttribute;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000443A0 File Offset: 0x000425A0
		public static bool IsLeafNode(XmlNode node)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000ABC RID: 2748
		private const string NamespaceUriDelimiter = ":";

		// Token: 0x04000ABD RID: 2749
		private static Dictionary<string, string> namespacePrefixes = new Dictionary<string, string>(10);

		// Token: 0x04000ABE RID: 2750
		private static XmlNamespaceManager namespaceManager;

		// Token: 0x02000210 RID: 528
		public enum NormalizationAction
		{
			// Token: 0x04000AC0 RID: 2752
			Normalize,
			// Token: 0x04000AC1 RID: 2753
			LeaveAsIs,
			// Token: 0x04000AC2 RID: 2754
			Remove
		}
	}
}
