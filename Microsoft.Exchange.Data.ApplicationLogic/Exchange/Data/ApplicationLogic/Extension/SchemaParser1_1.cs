using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000133 RID: 307
	internal class SchemaParser1_1 : SchemaParser
	{
		// Token: 0x06000C89 RID: 3209 RVA: 0x00033CFB File Offset: 0x00031EFB
		public SchemaParser1_1(SafeXmlDocument xmlDoc, ExtensionInstallScope extensionInstallScope) : base(xmlDoc, extensionInstallScope)
		{
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00033D05 File Offset: 0x00031F05
		public override Version SchemaVersion
		{
			get
			{
				return SchemaConstants.SchemaVersion1_1;
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00033D0C File Offset: 0x00031F0C
		public override Version GetMinApiVersion()
		{
			XmlNode mandatoryOweXmlNode = base.GetMandatoryOweXmlNode("Requirements");
			Version version = null;
			foreach (object obj in mandatoryOweXmlNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element && "Sets".Equals(xmlNode.LocalName, StringComparison.Ordinal))
				{
					string input = ExtensionData.GetOptionalAttributeStringValue(xmlNode, "DefaultMinVersion", "1.1");
					if (!Version.TryParse(input, out version) || version < SchemaConstants.LowestApiVersionSupportedBySchemaVersion1_1)
					{
						throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonDefaultVersionIsInvalid));
					}
					int num = 0;
					foreach (object obj2 in xmlNode)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.NodeType == XmlNodeType.Element && "Set".Equals(xmlNode2.LocalName, StringComparison.Ordinal))
						{
							num++;
							if (num > 1)
							{
								throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonOnlyMailboxSetAllowedInRequirement));
							}
							string attributeStringValue = ExtensionData.GetAttributeStringValue(xmlNode2, "Name");
							if (!"Mailbox".Equals(attributeStringValue, StringComparison.OrdinalIgnoreCase))
							{
								throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonOnlyMailboxSetAllowedInRequirement));
							}
							if (xmlNode2.Attributes != null)
							{
								XmlAttribute xmlAttribute = xmlNode2.Attributes["MinVersion"];
								if (xmlAttribute != null && !string.IsNullOrEmpty(xmlAttribute.Value))
								{
									input = xmlAttribute.Value;
									if (!Version.TryParse(input, out version) || version < SchemaConstants.LowestApiVersionSupportedBySchemaVersion1_1)
									{
										throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonMinVersionIsInvalid));
									}
								}
							}
						}
					}
					if (num != 1)
					{
						throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonOnlyMailboxSetAllowedInRequirement));
					}
					break;
				}
			}
			return version;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00033F34 File Offset: 0x00032134
		public override void ValidateFormSettings()
		{
			base.ValidateFormSettings();
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			foreach (XmlNode xmlNode in this.GetFormNodesInFormSettings())
			{
				XmlAttribute xmlAttribute = xmlNode.Attributes["type", "http://www.w3.org/2001/XMLSchema-instance"];
				if (xmlAttribute != null && !string.IsNullOrEmpty(xmlAttribute.Value) && !hashSet.Add(xmlAttribute.Value))
				{
					throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonFormInFormSettingsNotUnique));
				}
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00033FD4 File Offset: 0x000321D4
		protected override string GetOweNamespacePrefix()
		{
			return "owe1_1";
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00033FDB File Offset: 0x000321DB
		protected override string GetOweNamespaceUri()
		{
			return "http://schemas.microsoft.com/office/appforoffice/1.1";
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00033FE4 File Offset: 0x000321E4
		protected override XmlNode GetFormSettingsParentNode(FormSettings.FormSettingsType formSettingsType)
		{
			string b = SchemaParser1_1.FormSettingsTypeToXmlTypeName[formSettingsType];
			foreach (XmlNode xmlNode in this.GetFormNodesInFormSettings())
			{
				XmlAttribute attribute = xmlNode.Attributes["type", "http://www.w3.org/2001/XMLSchema-instance"];
				string a;
				if (ExtensionDataHelper.TryGetNameSpaceStrippedAttributeValue(attribute, out a) && string.Equals(a, b))
				{
					return xmlNode;
				}
			}
			return null;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00034238 File Offset: 0x00032438
		private IEnumerable<XmlNode> GetFormNodesInFormSettings()
		{
			XmlNode formSettingsNode = base.GetMandatoryOweXmlNode("FormSettings");
			foreach (object obj in formSettingsNode)
			{
				XmlNode node = (XmlNode)obj;
				if (node != null && node.Attributes != null)
				{
					yield return node;
				}
			}
			yield break;
		}

		// Token: 0x040006A6 RID: 1702
		private const string FormSettingsElementName = "FormSettings";

		// Token: 0x040006A7 RID: 1703
		private static readonly Dictionary<FormSettings.FormSettingsType, string> FormSettingsTypeToXmlTypeName = new Dictionary<FormSettings.FormSettingsType, string>
		{
			{
				FormSettings.FormSettingsType.ItemEdit,
				"ItemEdit"
			},
			{
				FormSettings.FormSettingsType.ItemRead,
				"ItemRead"
			}
		};
	}
}
