using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B6E RID: 2926
	internal class SoapClient
	{
		// Token: 0x06003E98 RID: 16024 RVA: 0x000A36A9 File Offset: 0x000A18A9
		public SoapClient(Uri endpoint, WebProxy webProxy)
		{
			this.httpClient = new XmlHttpClient(endpoint, webProxy);
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x000A36C0 File Offset: 0x000A18C0
		public XmlElement Invoke(IEnumerable<XmlElement> headers, XmlElement bodyContent)
		{
			XmlDocument requestXmlDocument = this.CreateRequestXmlDocument(headers, bodyContent);
			SoapFaultException ex = null;
			for (int i = 0; i < 3; i++)
			{
				XmlDocument xmlDocument = this.httpClient.Invoke(requestXmlDocument);
				XmlElement xmlElement = this.ExtractBodyContentFromResponse(xmlDocument);
				if (!Soap.Fault.IsMatch(xmlElement))
				{
					return xmlElement;
				}
				ex = this.GetSoapFaultException(xmlElement);
				if (!SoapClient.IsReceiverFault(ex.Code))
				{
					break;
				}
			}
			throw ex;
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x000A3724 File Offset: 0x000A1924
		public IAsyncResult BeginInvoke(IEnumerable<XmlElement> headers, XmlElement bodyContent, AsyncCallback callback, object state)
		{
			XmlDocument xmlDocument = this.CreateRequestXmlDocument(headers, bodyContent);
			return this.httpClient.BeginInvoke(xmlDocument, callback, state);
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x000A374C File Offset: 0x000A194C
		public XmlElement EndInvoke(IAsyncResult asyncResult)
		{
			XmlDocument xmlDocument = this.httpClient.EndInvoke(asyncResult);
			XmlElement xmlElement = this.ExtractBodyContentFromResponse(xmlDocument);
			if (Soap.Fault.IsMatch(xmlElement))
			{
				throw this.GetSoapFaultException(xmlElement);
			}
			return xmlElement;
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x000A3784 File Offset: 0x000A1984
		public void AbortInvoke(IAsyncResult asyncResult)
		{
			this.httpClient.AbortInvoke(asyncResult);
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x000A3794 File Offset: 0x000A1994
		private XmlDocument CreateRequestXmlDocument(IEnumerable<XmlElement> headers, XmlElement bodyContent)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.PreserveWhitespace = true;
			List<XmlElement> list = new List<XmlElement>();
			foreach (XmlElement node in headers)
			{
				list.Add((XmlElement)xmlDocument.ImportNode(node, true));
			}
			XmlElement bodyContent2 = (XmlElement)xmlDocument.ImportNode(bodyContent, true);
			XmlElement xmlElement = this.CreateSoapEnvelope(xmlDocument, list, bodyContent2);
			xmlDocument.AppendChild(xmlElement);
			XmlNamespaceDefinition.AddPrefixes(xmlDocument, xmlElement, new XmlNamespaceDefinition[]
			{
				Soap.Namespace,
				WSAddressing.Namespace,
				WSSecurityUtility.Namespace,
				WSSecurityExtensions.Namespace,
				WSTrust.Namespace,
				WSAuthorization.Namespace,
				WSPolicy.Namespace
			});
			return xmlDocument;
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x000A3874 File Offset: 0x000A1A74
		private XmlElement CreateSoapEnvelope(XmlDocument xmlDocument, IEnumerable<XmlElement> headers, XmlElement bodyContent)
		{
			XmlElement xmlElement = Soap.Header.CreateElement(xmlDocument, headers);
			XmlElement xmlElement2 = Soap.Body.CreateElement(xmlDocument, new XmlElement[]
			{
				bodyContent
			});
			return Soap.Envelope.CreateElement(xmlDocument, new XmlElement[]
			{
				xmlElement,
				xmlElement2
			});
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x000A38C4 File Offset: 0x000A1AC4
		private XmlElement ExtractBodyContentFromResponse(XmlDocument xmlDocument)
		{
			XmlElement requiredChildElement = this.GetRequiredChildElement(xmlDocument.DocumentElement, Soap.Body);
			return this.GetSingleChildElement(requiredChildElement);
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x000A38EC File Offset: 0x000A1AEC
		private SoapFaultException GetSoapFaultException(XmlElement fault)
		{
			SoapClient.Tracer.TraceError<SoapClient, string>((long)this.GetHashCode(), "{0}: soap fault received: {1}", this, fault.OuterXml);
			XmlElement requiredChildElement = this.GetRequiredChildElement(fault, Soap.Code);
			XmlElement requiredChildElement2 = this.GetRequiredChildElement(requiredChildElement, Soap.Value);
			XmlElement singleElementByName = Soap.Subcode.GetSingleElementByName(requiredChildElement);
			XmlElement xmlElement = null;
			if (singleElementByName != null)
			{
				xmlElement = this.GetRequiredChildElement(singleElementByName, Soap.Value);
			}
			return new SoapFaultException(fault, requiredChildElement2.InnerText, (xmlElement != null) ? xmlElement.InnerText : null);
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x000A3968 File Offset: 0x000A1B68
		protected XmlElement GetRequiredChildElement(XmlElement xmlElement, XmlElementDefinition xmlElementDefinition)
		{
			XmlElement singleElementByName = xmlElementDefinition.GetSingleElementByName(xmlElement);
			if (singleElementByName == null)
			{
				SoapClient.Tracer.TraceError<SoapClient, XmlElementDefinition, string>((long)this.GetHashCode(), "{0}: failed to find XML element: {1} in this context {1}", this, xmlElementDefinition, xmlElement.OuterXml);
				throw new SoapXmlMalformedException(xmlElement, xmlElementDefinition);
			}
			return singleElementByName;
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x000A39A7 File Offset: 0x000A1BA7
		protected XmlElement GetOptionalChildElement(XmlElement xmlElement, XmlElementDefinition xmlElementDefinition)
		{
			return xmlElementDefinition.GetSingleElementByName(xmlElement);
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x000A39B0 File Offset: 0x000A1BB0
		protected XmlElement GetSingleChildElement(XmlElement xmlElement)
		{
			if (xmlElement.ChildNodes.Count != 1)
			{
				SoapClient.Tracer.TraceError<SoapClient, string>((long)this.GetHashCode(), "{0}: found none or more than one XML element when only one was expected in this XML segment: {1}", this, xmlElement.OuterXml);
				throw new SoapXmlMalformedException(xmlElement);
			}
			return (XmlElement)xmlElement.ChildNodes[0];
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x000A3A00 File Offset: 0x000A1C00
		private static bool IsReceiverFault(string soapFaultCode)
		{
			string x = soapFaultCode;
			string[] array = soapFaultCode.Split(new char[]
			{
				':'
			});
			if (array.Length == 2)
			{
				x = array[1];
			}
			return StringComparer.OrdinalIgnoreCase.Equals(x, "receiver");
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x000A3A3D File Offset: 0x000A1C3D
		public override string ToString()
		{
			return "Soap client over " + this.httpClient.ToString();
		}

		// Token: 0x04003685 RID: 13957
		private const string SoapFaultReceiver = "receiver";

		// Token: 0x04003686 RID: 13958
		private const int RetriesForReceiverFault = 3;

		// Token: 0x04003687 RID: 13959
		private XmlHttpClient httpClient;

		// Token: 0x04003688 RID: 13960
		private static readonly Trace Tracer = ExTraceGlobals.WSTrustTracer;
	}
}
