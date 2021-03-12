using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000B5 RID: 181
	public sealed class ADCustomPropertyParser
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x00035403 File Offset: 0x00033603
		private ADCustomPropertyParser()
		{
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0003540C File Offset: 0x0003360C
		internal static void Initialize(string directory)
		{
			string text = directory + "\\customadproperties.xml";
			if (!File.Exists(text))
			{
				return;
			}
			XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(text);
			xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
			ADCustomPropertyParser.customPropertyDictionary = new Dictionary<string, PropertyDefinition>();
			try
			{
				xmlTextReader.Read();
				if (xmlTextReader.NodeType == XmlNodeType.XmlDeclaration)
				{
					xmlTextReader.Read();
				}
				if (!xmlTextReader.Name.Equals("CustomProperties") || xmlTextReader.NodeType != XmlNodeType.Element)
				{
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_CustomPropertiesRootElementNotFound, string.Empty, new object[]
					{
						"customadproperties.xml",
						xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture)
					});
				}
				else
				{
					while (xmlTextReader.Read())
					{
						if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name.Equals("CustomProperty"))
						{
							if (xmlTextReader.AttributeCount != 2)
							{
								OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_InvalidCustomPropertiesAttributeCount, string.Empty, new object[]
								{
									"customadproperties.xml",
									xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture)
								});
								break;
							}
							if (!xmlTextReader.MoveToAttribute("DisplayName"))
							{
								OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_CustomPropertiesAttributeNotFound, string.Empty, new object[]
								{
									"DisplayName",
									"customadproperties.xml",
									xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture),
									"First"
								});
								break;
							}
							string value = xmlTextReader.Value;
							if (string.IsNullOrEmpty(value))
							{
								OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_CustomPropertiesInvalidAttibuteValue, string.Empty, new object[]
								{
									"DisplayName",
									"customadproperties.xml",
									xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture)
								});
								break;
							}
							if (!xmlTextReader.MoveToAttribute("LdapDisplayName"))
							{
								OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_CustomPropertiesAttributeNotFound, string.Empty, new object[]
								{
									"LdapDisplayName",
									"customadproperties.xml",
									xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture),
									"Second"
								});
								break;
							}
							string value2 = xmlTextReader.Value;
							if (string.IsNullOrEmpty(value2))
							{
								OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_CustomPropertiesInvalidAttibuteValue, string.Empty, new object[]
								{
									"LdapDisplayName",
									"customadproperties.xml",
									xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture)
								});
								break;
							}
							ADPropertyDefinition value3 = new ADPropertyDefinition(value, ExchangeObjectVersion.Exchange2003, typeof(string), value2, ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
							ADCustomPropertyParser.customPropertyDictionary.Add(value, value3);
						}
						else if (xmlTextReader.NodeType != XmlNodeType.EndElement || !xmlTextReader.Name.Equals("CustomProperties"))
						{
							OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_InvalidElementInCustomPropertiesFile, string.Empty, new object[]
							{
								"customadproperties.xml",
								xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture),
								xmlTextReader.LinePosition.ToString(CultureInfo.InvariantCulture)
							});
							break;
						}
					}
				}
			}
			catch (XmlException ex)
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_CustomPropertiesParseError, string.Empty, new object[]
				{
					"customadproperties.xml",
					xmlTextReader.LineNumber.ToString(CultureInfo.InvariantCulture),
					xmlTextReader.LinePosition.ToString(CultureInfo.InvariantCulture),
					ex.Message
				});
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x000357F4 File Offset: 0x000339F4
		public static Dictionary<string, PropertyDefinition> CustomPropertyDictionary
		{
			get
			{
				return ADCustomPropertyParser.customPropertyDictionary;
			}
		}

		// Token: 0x040004A2 RID: 1186
		private const string CustomPropertiesFileName = "customadproperties.xml";

		// Token: 0x040004A3 RID: 1187
		private static Dictionary<string, PropertyDefinition> customPropertyDictionary;
	}
}
