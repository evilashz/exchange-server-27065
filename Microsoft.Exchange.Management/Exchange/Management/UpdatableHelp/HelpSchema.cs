using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x02000BFB RID: 3067
	internal class HelpSchema
	{
		// Token: 0x060073BD RID: 29629 RVA: 0x001D6B34 File Offset: 0x001D4D34
		internal HelpSchema()
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.Schemas.Add("http://schemas.microsoft.com/exchange/help/2013/02", new XmlTextReader(new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n            <xs:schema attributeFormDefault=\"unqualified\" elementFormDefault=\"qualified\"\r\n                targetNamespace=\"http://schemas.microsoft.com/exchange/help/2013/02\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\">\r\n                <xs:element name=\"ExchangeHelpInfo\">\r\n                    <xs:complexType>\r\n                        <xs:sequence>\r\n                            <xs:element name=\"HelpVersions\" minOccurs=\"1\" maxOccurs=\"1\">\r\n                                <xs:complexType>\r\n                                    <xs:sequence>\r\n                                        <xs:element name=\"HelpVersion\" minOccurs=\"1\" maxOccurs=\"unbounded\">\r\n                                            <xs:complexType>\r\n                                                <xs:sequence>\r\n                                                    <xs:element name=\"Version\" type=\"xs:string\" minOccurs=\"1\" />\r\n                                                    <xs:element name=\"HelpRevision\" type=\"xs:string\" minOccurs=\"1\" maxOccurs=\"1\" />\r\n                                                    <xs:element name=\"CulturesUpdated\" type=\"xs:string\" minOccurs=\"1\" maxOccurs=\"1\" />\r\n                                                    <xs:element name=\"CabinetUrl\" type=\"xs:string\" minOccurs=\"1\" maxOccurs=\"1\" />\r\n                                                </xs:sequence>\r\n                                            </xs:complexType>\r\n                                        </xs:element>\r\n                                    </xs:sequence>\r\n                                </xs:complexType>\r\n                            </xs:element>\r\n                        </xs:sequence>\r\n                    </xs:complexType>\r\n                </xs:element>\r\n            </xs:schema>")));
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			this.readerSettings = xmlReaderSettings;
		}

		// Token: 0x060073BE RID: 29630 RVA: 0x001D6B7C File Offset: 0x001D4D7C
		internal HelpSchema(string ns, string schema)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.Schemas.Add(ns, new XmlTextReader(new StringReader(schema)));
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			this.readerSettings = xmlReaderSettings;
		}

		// Token: 0x060073BF RID: 29631 RVA: 0x001D6BBC File Offset: 0x001D4DBC
		internal UpdatableHelpVersionRange ParseManifestForApplicableUpdates(string xml, UpdatableHelpVersion currentVersion, int currentRevision)
		{
			UpdatableHelpVersionRange result = null;
			XmlDocument xmlDocument = null;
			try
			{
				xmlDocument = this.CreateValidXmlDocument(xml, new ValidationEventHandler(this.ManifestValidationHandler), true);
			}
			catch (XmlException innerException)
			{
				throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateManifestXmlValidationFailureErrorID, UpdatableHelpStrings.UpdateManifestXmlValidationFailure, ErrorCategory.InvalidData, null, innerException);
			}
			new SortedList<int, UpdatableHelpVersion>();
			XmlNodeList childNodes = xmlDocument["ExchangeHelpInfo"]["HelpVersions"].ChildNodes;
			foreach (object obj in childNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.HasChildNodes)
				{
					List<string> list = new List<string>();
					string text = string.Empty;
					string cabinetUrl = string.Empty;
					int num = 0;
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						string name;
						if ((name = xmlNode2.Name) != null)
						{
							if (!(name == "Version"))
							{
								if (!(name == "Revision"))
								{
									if (!(name == "CulturesUpdated"))
									{
										if (name == "CabinetUrl")
										{
											cabinetUrl = xmlNode["CabinetUrl"].InnerText;
										}
									}
									else
									{
										text = xmlNode2.InnerText;
									}
								}
								else if (!int.TryParse(xmlNode2.InnerText, out num))
								{
									throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateInvalidVersionNumberErrorID, UpdatableHelpStrings.UpdateInvalidVersionNumber(xmlNode2.InnerText), ErrorCategory.InvalidData, null, null);
								}
							}
							else
							{
								list.Add(xmlNode2.InnerText.Trim());
							}
						}
					}
					int newestRevisionFound = currentRevision;
					if (list.Count > 0 && num > 0 && !string.IsNullOrEmpty(text) && !string.IsNullOrEmpty("CabinetUrl"))
					{
						foreach (string versionRange in list)
						{
							UpdatableHelpVersionRange updatableHelpVersionRange = new UpdatableHelpVersionRange(versionRange, num, text, cabinetUrl);
							if (updatableHelpVersionRange.IsInRangeAndNewerThan(currentVersion, newestRevisionFound))
							{
								result = updatableHelpVersionRange;
								newestRevisionFound = updatableHelpVersionRange.HelpRevision;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060073C0 RID: 29632 RVA: 0x001D6E48 File Offset: 0x001D5048
		internal XmlDocument CreateValidXmlDocument(string xml, ValidationEventHandler handler, bool isManifest)
		{
			XmlReader reader = XmlReader.Create(new StringReader(xml), this.readerSettings);
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(reader);
				xmlDocument.Validate(handler);
			}
			catch (XmlSchemaValidationException innerException)
			{
				if (isManifest)
				{
					throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateManifestXmlValidationFailureErrorID, UpdatableHelpStrings.UpdateManifestXmlValidationFailure, ErrorCategory.InvalidData, null, innerException);
				}
				throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateContentXmlValidationFailureErrorID, UpdatableHelpStrings.UpdateContentXmlValidationFailure, ErrorCategory.InvalidData, null, innerException);
			}
			return xmlDocument;
		}

		// Token: 0x060073C1 RID: 29633 RVA: 0x001D6EB8 File Offset: 0x001D50B8
		private void ManifestValidationHandler(object sender, ValidationEventArgs arg)
		{
			switch (arg.Severity)
			{
			case XmlSeverityType.Error:
				throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateManifestXmlValidationFailureErrorID, UpdatableHelpStrings.UpdateManifestXmlValidationFailure, ErrorCategory.InvalidData, null, arg.Exception);
			case XmlSeverityType.Warning:
				return;
			default:
				return;
			}
		}

		// Token: 0x04003AD3 RID: 15059
		private const string HelpInfoXmlSchema = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n            <xs:schema attributeFormDefault=\"unqualified\" elementFormDefault=\"qualified\"\r\n                targetNamespace=\"http://schemas.microsoft.com/exchange/help/2013/02\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\">\r\n                <xs:element name=\"ExchangeHelpInfo\">\r\n                    <xs:complexType>\r\n                        <xs:sequence>\r\n                            <xs:element name=\"HelpVersions\" minOccurs=\"1\" maxOccurs=\"1\">\r\n                                <xs:complexType>\r\n                                    <xs:sequence>\r\n                                        <xs:element name=\"HelpVersion\" minOccurs=\"1\" maxOccurs=\"unbounded\">\r\n                                            <xs:complexType>\r\n                                                <xs:sequence>\r\n                                                    <xs:element name=\"Version\" type=\"xs:string\" minOccurs=\"1\" />\r\n                                                    <xs:element name=\"HelpRevision\" type=\"xs:string\" minOccurs=\"1\" maxOccurs=\"1\" />\r\n                                                    <xs:element name=\"CulturesUpdated\" type=\"xs:string\" minOccurs=\"1\" maxOccurs=\"1\" />\r\n                                                    <xs:element name=\"CabinetUrl\" type=\"xs:string\" minOccurs=\"1\" maxOccurs=\"1\" />\r\n                                                </xs:sequence>\r\n                                            </xs:complexType>\r\n                                        </xs:element>\r\n                                    </xs:sequence>\r\n                                </xs:complexType>\r\n                            </xs:element>\r\n                        </xs:sequence>\r\n                    </xs:complexType>\r\n                </xs:element>\r\n            </xs:schema>";

		// Token: 0x04003AD4 RID: 15060
		private const string HelpInfoXmlNamespace = "http://schemas.microsoft.com/exchange/help/2013/02";

		// Token: 0x04003AD5 RID: 15061
		private const string rootElementName = "ExchangeHelpInfo";

		// Token: 0x04003AD6 RID: 15062
		private const string helpVersionsElementName = "HelpVersions";

		// Token: 0x04003AD7 RID: 15063
		private const string helpVersionElementName = "HelpVersion";

		// Token: 0x04003AD8 RID: 15064
		private const string versionElementName = "Version";

		// Token: 0x04003AD9 RID: 15065
		private const string revisionElementName = "Revision";

		// Token: 0x04003ADA RID: 15066
		private const string culturesUpdatedElementName = "CulturesUpdated";

		// Token: 0x04003ADB RID: 15067
		private const string cabinetUrlElementName = "CabinetUrl";

		// Token: 0x04003ADC RID: 15068
		private XmlReaderSettings readerSettings;
	}
}
