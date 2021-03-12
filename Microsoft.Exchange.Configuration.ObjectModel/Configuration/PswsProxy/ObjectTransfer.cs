using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.PswsProxy
{
	// Token: 0x020000CE RID: 206
	internal class ObjectTransfer
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x0001C338 File Offset: 0x0001A538
		internal static ResponseContent GetResponseInformation(string responseXml)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			try
			{
				xmlDocument.LoadXml(responseXml);
			}
			catch (Exception innerException)
			{
				throw new PswsProxyException(Strings.PswsResponseIsnotXMLError(responseXml), innerException);
			}
			ResponseContent responseContent = new ResponseContent();
			XmlNamespaceManager pswsNamespaceManager = ObjectTransfer.GetPswsNamespaceManager(xmlDocument);
			XmlElement xmlElementMustExisting = ObjectTransfer.GetXmlElementMustExisting(xmlDocument, "/rt:entry/rt:content/m:properties", pswsNamespaceManager);
			responseContent.Id = ObjectTransfer.GetChildXmlElementMustExisting(xmlElementMustExisting, "ID").InnerText;
			responseContent.Command = ObjectTransfer.GetChildXmlElementMustExisting(xmlElementMustExisting, "Command").InnerText;
			responseContent.Status = (ExecutionStatus)Enum.Parse(typeof(ExecutionStatus), ObjectTransfer.GetChildXmlElementMustExisting(xmlElementMustExisting, "Status").InnerText);
			responseContent.OutputXml = ObjectTransfer.GetChildXmlElementMustExisting(xmlElementMustExisting, "Output").InnerText;
			responseContent.Error = ObjectTransfer.GetErrorRecord(ObjectTransfer.GetChildXmlElementMustExisting(xmlElementMustExisting, "Errors"));
			responseContent.ExpirationTime = DateTime.Parse(ObjectTransfer.GetChildXmlElementMustExisting(xmlElementMustExisting, "ExpirationTime").InnerText);
			responseContent.WaitMsec = int.Parse(ObjectTransfer.GetChildXmlElementMustExisting(xmlElementMustExisting, "WaitMsec").InnerText);
			return responseContent;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001C450 File Offset: 0x0001A650
		internal static List<PSObject> GetResultObjects(string outputXml, TypeTable typeTable)
		{
			List<PSObject> list = new List<PSObject>();
			PSObjectSerializer psobjectSerializer = new PSObjectSerializer(typeTable);
			foreach (string serializedData in ObjectTransfer.GetPSObjectSerializedData(outputXml))
			{
				list.Add(psobjectSerializer.Deserialize(serializedData));
			}
			return list;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
		private static ResponseErrorRecord GetErrorRecord(XmlElement xmlElement)
		{
			XmlElement childXmlElement = ObjectTransfer.GetChildXmlElement(xmlElement, "element");
			if (childXmlElement == null)
			{
				return null;
			}
			return new ResponseErrorRecord
			{
				FullyQualifiedErrorId = ObjectTransfer.GetChildXmlElementMustExisting(childXmlElement, "FullyQualifiedErrorId").InnerText,
				CategoryInfo = ObjectTransfer.GetCategoryInfo(ObjectTransfer.GetChildXmlElementMustExisting(childXmlElement, "CategoryInfo")),
				ErrorDetail = ObjectTransfer.GetErrorDetails(ObjectTransfer.GetChildXmlElementMustExisting(childXmlElement, "ErrorDetails")),
				Exception = ObjectTransfer.GetChildXmlElementMustExisting(childXmlElement, "Exception").InnerText
			};
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001C534 File Offset: 0x0001A734
		private static ResponseCategoryInfo GetCategoryInfo(XmlElement xmlElement)
		{
			return new ResponseCategoryInfo
			{
				Activity = ObjectTransfer.GetChildXmlElementMustExisting(xmlElement, "Activity").InnerText,
				Category = ObjectTransfer.GetChildXmlElementMustExisting(xmlElement, "Category").InnerText,
				Reason = ObjectTransfer.GetChildXmlElementMustExisting(xmlElement, "Reason").InnerText,
				TargetName = ObjectTransfer.GetChildXmlElementMustExisting(xmlElement, "TargetName").InnerText,
				TargetType = ObjectTransfer.GetChildXmlElementMustExisting(xmlElement, "TargetType").InnerText
			};
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001C5B8 File Offset: 0x0001A7B8
		private static ResponseErrorDetail GetErrorDetails(XmlElement xmlElement)
		{
			return new ResponseErrorDetail
			{
				Message = ObjectTransfer.GetChildXmlElementMustExisting(xmlElement, "Message").InnerText,
				RecommendedAction = ObjectTransfer.GetChildXmlElementMustExisting(xmlElement, "RecommendedAction").InnerText
			};
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001C5F8 File Offset: 0x0001A7F8
		private static XmlNamespaceManager GetPswsNamespaceManager(XmlDocument xmlDoc)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
			xmlNamespaceManager.AddNamespace("rt", "http://www.w3.org/2005/Atom");
			xmlNamespaceManager.AddNamespace("d", "http://schemas.microsoft.com/ado/2007/08/dataservices");
			xmlNamespaceManager.AddNamespace("m", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			return xmlNamespaceManager;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001C644 File Offset: 0x0001A844
		private static XmlNamespaceManager GetOutputNamespaceManager(XmlDocument xmlDoc)
		{
			return new XmlNamespaceManager(xmlDoc.NameTable);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001C660 File Offset: 0x0001A860
		private static XmlElement GetXmlElementMustExisting(XmlDocument xmlDoc, string xPath, XmlNamespaceManager nsmgr)
		{
			XmlElement xmlElement = xmlDoc.SelectSingleNode(xPath, nsmgr) as XmlElement;
			if (xmlElement == null)
			{
				throw new PswsProxyException(Strings.PswsResponseElementNotExisingError(xmlElement.OuterXml, xPath));
			}
			return xmlElement;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001C694 File Offset: 0x0001A894
		private static XmlElement GetChildXmlElementMustExisting(XmlElement parentElement, string elementName)
		{
			XmlElement childXmlElement = ObjectTransfer.GetChildXmlElement(parentElement, elementName);
			if (childXmlElement == null)
			{
				throw new PswsProxyException(Strings.PswsResponseChildElementNotExisingError(parentElement.Name, elementName));
			}
			return childXmlElement;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
		private static XmlElement GetChildXmlElement(XmlElement parentElement, string elementName)
		{
			foreach (object obj in parentElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.LocalName == elementName)
				{
					return (XmlElement)xmlNode;
				}
			}
			return null;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001C72C File Offset: 0x0001A92C
		private static List<XmlElement> GetChildXmlElements(XmlElement parentElement, string elementName)
		{
			List<XmlElement> list = new List<XmlElement>();
			foreach (object obj in parentElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.LocalName == elementName)
				{
					list.Add((XmlElement)xmlNode);
				}
			}
			return list;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001C97C File Offset: 0x0001AB7C
		private static IEnumerable<string> GetPSObjectSerializedData(string serializationData)
		{
			XmlDocument xmlDoc = new SafeXmlDocument();
			xmlDoc.LoadXml(serializationData);
			XmlNamespaceManager nsmgr = ObjectTransfer.GetOutputNamespaceManager(xmlDoc);
			XmlElement objectsElement = ObjectTransfer.GetXmlElementMustExisting(xmlDoc, "Objs", nsmgr);
			foreach (XmlElement objectElement in ObjectTransfer.GetChildXmlElements(objectsElement, "Obj"))
			{
				yield return objectElement.OuterXml;
			}
			yield break;
		}
	}
}
