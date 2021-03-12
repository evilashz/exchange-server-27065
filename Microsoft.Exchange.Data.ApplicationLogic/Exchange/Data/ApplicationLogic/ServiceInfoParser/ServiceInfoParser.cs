using System;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.ServiceInfoParser
{
	// Token: 0x020001AA RID: 426
	internal static class ServiceInfoParser
	{
		// Token: 0x06001065 RID: 4197 RVA: 0x0004382D File Offset: 0x00041A2D
		public static Uri GetRootSiteUrlFromServiceInfo(XmlDocument doc, ITracer tracer)
		{
			if (doc == null)
			{
				tracer.TraceDebug(0L, "GetRootSiteUrlFromServiceInfo: doc is null");
				return null;
			}
			return ServiceInfoParser.GetRootSiteUrlFromServiceInfo(doc.DocumentElement, tracer);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0004384D File Offset: 0x00041A4D
		public static Uri GetRootSiteUrlFromServiceInfo(XmlElement root, ITracer tracer)
		{
			return ServiceInfoParser.GetServiceParameterUrlFromServiceInfo(root, tracer, "SPO_ROOTSITEURL", "http://schemas.microsoft.com/online/serviceextensions/2009/08/ExtensibilitySchema.xsd");
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00043860 File Offset: 0x00041A60
		private static Uri GetServiceParameterUrlFromServiceInfo(XmlElement root, ITracer tracer, string urlAttributeName, string namespaceValue)
		{
			if (root == null)
			{
				tracer.TraceDebug<string>(0L, "GetUrlFromServiceInfo for attribute: root is null", urlAttributeName);
				return null;
			}
			Exception ex = null;
			try
			{
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(root.OwnerDocument.NameTable);
				xmlNamespaceManager.AddNamespace("es", namespaceValue);
				string xpath = string.Format("//es:ServiceParameter[translate(es:Name,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='{0}']/es:Value", urlAttributeName);
				XmlNode xmlNode = root.SelectSingleNode(xpath, xmlNamespaceManager);
				if (xmlNode != null)
				{
					tracer.TraceDebug<string, string>(0L, "Get {1} from ServiceInfo: {0}", xmlNode.InnerText, urlAttributeName);
					return new Uri(xmlNode.InnerText);
				}
			}
			catch (XmlException ex2)
			{
				ex = ex2;
			}
			catch (XPathException ex3)
			{
				ex = ex3;
			}
			catch (UriFormatException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				tracer.TraceError<Exception, string>(0L, "Failed to get {1} from ServiceInfo: {0}", ex, urlAttributeName);
			}
			return null;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00043930 File Offset: 0x00041B30
		public static Uri GetWindowsIntuneUrlFromServiceInfo(XmlElement element, ITracer tracer)
		{
			return ServiceInfoParser.GetServiceParameterUrlFromServiceInfo(element, tracer, "ODMSENDPOINTURL", string.Empty);
		}
	}
}
