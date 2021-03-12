using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000131 RID: 305
	internal abstract class SchemaParser
	{
		// Token: 0x06000C5C RID: 3164 RVA: 0x00032AC4 File Offset: 0x00030CC4
		public SchemaParser(SafeXmlDocument xmlDoc, ExtensionInstallScope extensionInstallScope)
		{
			this.xmlDoc = xmlDoc;
			this.extensionInstallScope = extensionInstallScope;
			this.namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
			this.namespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
			this.namespaceManager.AddNamespace(this.GetOweNamespacePrefix(), this.GetOweNamespaceUri());
			this.xpathPrefix = "//" + this.GetOweNamespacePrefix() + ":";
			this.oweNameSpacePrefixWithSemiColon = this.GetOweNamespacePrefix() + ":";
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C5D RID: 3165
		public abstract Version SchemaVersion { get; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00032B53 File Offset: 0x00030D53
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x00032B5B File Offset: 0x00030D5B
		public ExtensionInstallScope ExtensionInstallScope
		{
			get
			{
				return this.extensionInstallScope;
			}
			set
			{
				this.extensionInstallScope = value;
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00032B64 File Offset: 0x00030D64
		private static bool ParseBoolFromXmlAttribute(XmlAttribute attribute)
		{
			return attribute != null && ExtensionDataHelper.ConvertXmlStringToBoolean(attribute.Value);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00032B78 File Offset: 0x00030D78
		private static Uri ValidateUrl(ExtensionInstallScope extensionScope, string name, string url)
		{
			Uri uri;
			if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
			{
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonInvalidUrlValue(name, url)));
			}
			if (uri.IsAbsoluteUri)
			{
				if (!string.Equals(uri.Scheme, "https", StringComparison.OrdinalIgnoreCase))
				{
					throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonUnsupportedUrlScheme(name, url)));
				}
			}
			else if (ExtensionInstallScope.Default != extensionScope)
			{
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonUrlMustBeAbsolute(name, url)));
			}
			return uri;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00032BF8 File Offset: 0x00030DF8
		private static void ValidateRegEx(string regexName, string regexPattern, string ruleName, string attributeName)
		{
			try
			{
				ExtensionDataHelper.ValidateRegex(regexPattern);
			}
			catch (Exception ex)
			{
				SchemaParser.Tracer.TraceError(0L, "Failed to validate {0} rule's {1} of name '{2}' and value '{3}' with exception: '{4}'", new object[]
				{
					ruleName,
					attributeName,
					regexName,
					regexPattern,
					ex
				});
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonInvalidRegEx(ruleName, attributeName)), ex);
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00032C64 File Offset: 0x00030E64
		private static void ValidateItemIsRule(XmlNode xmlNode)
		{
			XmlAttribute xmlAttribute = xmlNode.Attributes["ItemClass"];
			if (xmlAttribute != null)
			{
				string value = xmlAttribute.Value;
				if (ObjectClass.IsSmime(value) || ObjectClass.IsRightsManagedContentClass(value))
				{
					throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonItemTypeInvalid));
				}
				XmlAttribute xmlAttribute2 = xmlNode.Attributes["IncludeSubClasses"];
				if (string.Equals(value, "IPM", StringComparison.OrdinalIgnoreCase) && xmlAttribute2 != null && ExtensionDataHelper.ConvertXmlStringToBoolean(xmlAttribute2.Value))
				{
					throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonItemTypeAllTypes));
				}
			}
			XmlAttribute xmlAttribute3 = xmlNode.Attributes["FormType"];
			ItemIsRuleFormType itemIsRuleFormType;
			if (xmlAttribute3 != null && EnumValidator.TryParse<ItemIsRuleFormType>(xmlAttribute3.Value, EnumParseOptions.Default, out itemIsRuleFormType) && (itemIsRuleFormType == ItemIsRuleFormType.Edit || itemIsRuleFormType == ItemIsRuleFormType.ReadOrEdit))
			{
				XmlAttribute xmlAttribute4 = xmlNode.Attributes["IncludeSubClasses"];
				if (xmlAttribute != null || xmlAttribute4 != null)
				{
					throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonItemIsRuleAttributesNotValidForEdit));
				}
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00032D54 File Offset: 0x00030F54
		private static void ValidateItemHasKnownEntityRule(XmlNode xmlNode, RequestedCapabilities requestedCapabilities, ref HashSet<string> entitiesRegExNames, ref int regexCount)
		{
			if (requestedCapabilities == RequestedCapabilities.Restricted)
			{
				XmlAttribute attribute = xmlNode.Attributes["EntityType"];
				string item;
				if (!ExtensionDataHelper.TryGetAttributeValue(attribute, out item) || !SchemaParser.AllowedEntityTypesInRestricted.Contains(item))
				{
					throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonOnlySelectedEntitiesInRestricted));
				}
			}
			XmlAttribute attribute2 = xmlNode.Attributes["RegExFilter"];
			XmlAttribute attribute3 = xmlNode.Attributes["FilterName"];
			string text;
			bool flag = ExtensionDataHelper.TryGetAttributeValue(attribute3, out text);
			string regexPattern;
			bool flag2 = ExtensionDataHelper.TryGetAttributeValue(attribute2, out regexPattern);
			if (flag != flag2)
			{
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonRegExNameAndValueRequiredInEntitiesRules));
			}
			if (!flag)
			{
				XmlAttribute attribute4 = xmlNode.Attributes["IgnoreCase"];
				string text2;
				if (ExtensionDataHelper.TryGetAttributeValue(attribute4, out text2))
				{
					throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonIgnoreCaseWithoutRegExInEntitiesRules));
				}
				return;
			}
			else
			{
				regexCount++;
				if (entitiesRegExNames == null)
				{
					entitiesRegExNames = new HashSet<string>();
				}
				else if (regexCount > 5)
				{
					throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonTooManyRegexRule(5)));
				}
				if (!entitiesRegExNames.Add(text))
				{
					throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonMultipleRulesWithSameFilterName(text)));
				}
				SchemaParser.ValidateRegEx(text, regexPattern, "ItemHasKnownEntity", "RegExFilter");
				return;
			}
		}

		// Token: 0x06000C65 RID: 3173
		public abstract Version GetMinApiVersion();

		// Token: 0x06000C66 RID: 3174 RVA: 0x00032E8C File Offset: 0x0003108C
		public string GetAndValidateExtensionId()
		{
			string tagStringValue = ExtensionData.GetTagStringValue(this.xmlDoc, this.GetOweXpath("Id"), this.namespaceManager);
			string text = ExtensionDataHelper.FormatExtensionId(tagStringValue);
			Guid guid;
			if (!GuidHelper.TryParseGuid(text, out guid))
			{
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonInvalidID));
			}
			return text;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00032EDD File Offset: 0x000310DD
		public RequestedCapabilities GetRequestedCapabilities()
		{
			return ExtensionData.GetEnumTagValue<RequestedCapabilities>(this.xmlDoc, this.GetOweXpath("Permissions"), this.namespaceManager);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00032EFB File Offset: 0x000310FB
		public Uri GetAndValidateIconUrl(CultureInfo culture)
		{
			return this.GetAndValidateUrls("IconUrl", culture);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00032F09 File Offset: 0x00031109
		public Uri GetAndValidateHighResolutionIconUrl(CultureInfo culture)
		{
			return this.GetAndValidateUrls("HighResolutionIconUrl", culture);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00032F18 File Offset: 0x00031118
		private Uri GetAndValidateUrls(string elementName, CultureInfo culture)
		{
			Uri result = null;
			XmlNode xmlNode = this.xmlDoc.SelectSingleNode(this.GetOweXpath(elementName), this.namespaceManager);
			if (xmlNode != null)
			{
				string attributeStringValue = ExtensionData.GetAttributeStringValue(xmlNode, "DefaultValue");
				result = SchemaParser.ValidateUrl(this.extensionInstallScope, elementName, attributeStringValue);
				using (XmlNodeList xmlNodeList = xmlNode.SelectNodes(this.GetOweChildPath("Override"), this.namespaceManager))
				{
					string name = elementName + " " + "Override";
					foreach (object obj in xmlNodeList)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						string attributeStringValue2 = ExtensionData.GetAttributeStringValue(xmlNode2, "Value");
						Uri uri = SchemaParser.ValidateUrl(this.extensionInstallScope, name, attributeStringValue2);
						string attributeStringValue3 = ExtensionData.GetAttributeStringValue(xmlNode2, "Locale");
						if (string.Compare(culture.ToString(), attributeStringValue3, StringComparison.OrdinalIgnoreCase) == 0)
						{
							result = uri;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0003302C File Offset: 0x0003122C
		public void ValidateSourceLocations()
		{
			string oweXpath = this.GetOweXpath("SourceLocation");
			string xpath = oweXpath + "/" + this.GetOweChildPath("Override");
			this.ValidateSourceLocationUrls(oweXpath, "DefaultValue", "SourceLocation");
			this.ValidateSourceLocationUrls(xpath, "Value", "SourceLocation Override");
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00033080 File Offset: 0x00031280
		public void ValidateRules()
		{
			RequestedCapabilities requestedCapabilities = this.GetRequestedCapabilities();
			using (XmlNodeList xmlNodeList = this.xmlDoc.SelectNodes(this.GetOweXpath("Rule"), this.namespaceManager))
			{
				int num = 0;
				int num2 = 0;
				HashSet<string> hashSet = null;
				HashSet<string> hashSet2 = null;
				int num3 = 0;
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlAttribute attribute = xmlNode.Attributes["type", "http://www.w3.org/2001/XMLSchema-instance"];
					string a;
					if (ExtensionDataHelper.TryGetNameSpaceStrippedAttributeValue(attribute, out a))
					{
						if (string.Equals(a, "RuleCollection", StringComparison.Ordinal))
						{
							num2++;
						}
						else
						{
							num++;
						}
						if (num > 15 || num2 > 15)
						{
							throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonTooManyRule(15)));
						}
						if (string.Equals(a, "ItemIs", StringComparison.Ordinal))
						{
							SchemaParser.ValidateItemIsRule(xmlNode);
						}
						else if (string.Equals(a, "ItemHasKnownEntity", StringComparison.Ordinal))
						{
							SchemaParser.ValidateItemHasKnownEntityRule(xmlNode, requestedCapabilities, ref hashSet2, ref num3);
						}
						else if (string.Equals(a, "ItemHasRegularExpressionMatch", StringComparison.Ordinal))
						{
							num3++;
							if (hashSet == null)
							{
								hashSet = new HashSet<string>();
							}
							string attributeStringValue = ExtensionData.GetAttributeStringValue(xmlNode, "RegExName");
							if (!hashSet.Add(attributeStringValue))
							{
								throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonMultipleRulesWithSameRegExName(attributeStringValue)));
							}
							if (requestedCapabilities == RequestedCapabilities.Restricted)
							{
								throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonNoRegexRuleInRestricted));
							}
							if (num3 > 5)
							{
								throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonTooManyRegexRule(5)));
							}
							SchemaParser.ValidateRegEx(attributeStringValue, ExtensionData.GetAttributeStringValue(xmlNode, "RegExValue"), "ItemHasRegularExpressionMatch", "RegExValue");
						}
					}
				}
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0003327C File Offset: 0x0003147C
		public string GetOweStringElement(string elementName)
		{
			return ExtensionData.GetTagStringValue(this.xmlDoc, this.GetOweXpath(elementName), this.namespaceManager);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00033298 File Offset: 0x00031498
		public void ValidateHosts()
		{
			XmlNode oweXmlNode = this.GetOweXmlNode("Hosts");
			if (oweXmlNode == null)
			{
				return;
			}
			bool flag = false;
			foreach (object obj in oweXmlNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					string attributeStringValue = ExtensionData.GetAttributeStringValue(xmlNode, "Name");
					if ("Mailbox".Equals(attributeStringValue, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonNoMailboxInHosts));
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00033344 File Offset: 0x00031544
		public bool GetDisableEntityHighlighting()
		{
			bool flag = false;
			XmlNode oweXmlNode = this.GetOweXmlNode("DisableEntityHighlighting");
			return oweXmlNode != null && bool.TryParse(oweXmlNode.Value, out flag) && flag;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00033374 File Offset: 0x00031574
		public XmlNode GetOweXmlNode(string elementName)
		{
			return this.xmlDoc.SelectSingleNode(this.GetOweXpath(elementName), this.namespaceManager);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00033390 File Offset: 0x00031590
		public XmlNode GetMandatoryOweXmlNode(string elementName)
		{
			XmlNode xmlNode = this.xmlDoc.SelectSingleNode(this.GetOweXpath(elementName), this.namespaceManager);
			if (xmlNode == null)
			{
				throw new OwaExtensionOperationException(Strings.FailureReasonTagMissing(elementName));
			}
			return xmlNode;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x000333C8 File Offset: 0x000315C8
		public string GetIdForTokenRequests()
		{
			XmlNode xmlNode = this.xmlDoc.SelectSingleNode(this.GetOweXpath("SourceLocation"), this.namespaceManager);
			if (xmlNode == null)
			{
				SchemaParser.Tracer.TraceError(0L, "SourceLocation tag is missing from the given node.");
				throw new OwaExtensionOperationException(Strings.ErrorCanNotReadInstalledList(Strings.FailureReasonSourceLocationTagMissing));
			}
			return ExtensionData.GetAttributeStringValue(xmlNode, "DefaultValue");
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00033428 File Offset: 0x00031628
		public string GetOweLocaleAwareSetting(string elementName, CultureInfo culture)
		{
			XmlNode xmlNode = this.xmlDoc.SelectSingleNode(this.GetOweXpath(elementName), this.namespaceManager);
			if (xmlNode != null)
			{
				return this.GetLocaleAwareNodeValue(xmlNode, culture.ToString());
			}
			return null;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00033460 File Offset: 0x00031660
		public List<FormSettings> GetFormSettings(FormFactor formFactor, CultureInfo culture, string etoken)
		{
			List<FormSettings> list = new List<FormSettings>();
			string settingsNodeName;
			if (formFactor == FormFactor.Mobile)
			{
				settingsNodeName = "PhoneSettings";
			}
			else if (formFactor == FormFactor.Tablet)
			{
				settingsNodeName = "TabletSettings";
			}
			else
			{
				if (formFactor != FormFactor.Desktop)
				{
					SchemaParser.Tracer.TraceError<FormFactor>(0L, "FormFactor {0} is not supported", formFactor);
					return list;
				}
				settingsNodeName = "DesktopSettings";
			}
			foreach (FormSettings.FormSettingsType formSettingsType in SchemaParser.allFormSettingsTypes)
			{
				XmlNode formSettingsParentNode = this.GetFormSettingsParentNode(formSettingsType);
				if (formSettingsParentNode != null)
				{
					FormSettings formSettingsForFormType = this.GetFormSettingsForFormType(formSettingsParentNode, settingsNodeName, culture.ToString(), etoken);
					if (formSettingsForFormType != null)
					{
						formSettingsForFormType.SettingsType = formSettingsType;
						list.Add(formSettingsForFormType);
					}
				}
			}
			return list;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x000334FC File Offset: 0x000316FC
		public bool TryCreateActivationRule(out ActivationRule activationRule)
		{
			return this.TryCreateActivationRuleInternal(this.GetOweXmlNode("Rule"), out activationRule);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00033510 File Offset: 0x00031710
		public virtual void ValidateFormSettings()
		{
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00033514 File Offset: 0x00031714
		internal bool TryUpdateSourceLocation(IExchangePrincipal exchangePrincipal, string elementName, ExtensionData extensionData, out Exception exception, ExtensionDataHelper.TryModifySourceLocationDelegate tryModifySourceLocationDelegate)
		{
			exception = null;
			foreach (object obj in this.xmlDoc.SelectNodes(this.GetOweXpath(elementName), this.namespaceManager))
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlAttribute xmlAttribute = xmlNode.Attributes["DefaultValue"];
				if (!tryModifySourceLocationDelegate(exchangePrincipal, xmlAttribute, extensionData, out exception))
				{
					return false;
				}
				if (xmlNode.ChildNodes != null)
				{
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (this.IsExpectedOweNamespace(xmlNode2.NamespaceURI) && string.Equals(xmlNode2.LocalName, "Override", StringComparison.Ordinal))
						{
							xmlAttribute = xmlNode2.Attributes["Value"];
							if (!tryModifySourceLocationDelegate(exchangePrincipal, xmlAttribute, extensionData, out exception))
							{
								return false;
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000C78 RID: 3192
		protected abstract XmlNode GetFormSettingsParentNode(FormSettings.FormSettingsType formSettingsType);

		// Token: 0x06000C79 RID: 3193
		protected abstract string GetOweNamespacePrefix();

		// Token: 0x06000C7A RID: 3194
		protected abstract string GetOweNamespaceUri();

		// Token: 0x06000C7B RID: 3195 RVA: 0x0003364C File Offset: 0x0003184C
		private FormSettings GetFormSettingsForFormType(XmlNode parentContainer, string settingsNodeName, string clientLanguage, string etoken)
		{
			int requestedHeight = 0;
			string text = null;
			foreach (object obj in parentContainer.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (this.IsExpectedOweNamespace(xmlNode.NamespaceURI) && string.Equals(xmlNode.LocalName, settingsNodeName, StringComparison.Ordinal) && xmlNode.ChildNodes != null)
				{
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (this.IsExpectedOweNamespace(xmlNode2.NamespaceURI))
						{
							if (string.Equals(xmlNode2.LocalName, "SourceLocation", StringComparison.Ordinal))
							{
								text = this.GetLocaleAwareNodeValue(xmlNode2, clientLanguage);
							}
							else if (string.Equals(xmlNode2.LocalName, "RequestedHeight", StringComparison.Ordinal))
							{
								int.TryParse(xmlNode2.InnerText, out requestedHeight);
							}
						}
					}
				}
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			if (!string.IsNullOrWhiteSpace(etoken))
			{
				string str = (text.IndexOf('?') > 0) ? "&" : "?";
				text = text + str + "et=" + etoken;
			}
			return new FormSettings
			{
				SourceLocation = text,
				RequestedHeight = requestedHeight
			};
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x000337C8 File Offset: 0x000319C8
		private string GetOweXpath(string elementName)
		{
			return this.xpathPrefix + elementName;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x000337D6 File Offset: 0x000319D6
		private string GetOweChildPath(string childElementName)
		{
			return this.oweNameSpacePrefixWithSemiColon + childElementName;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x000337E4 File Offset: 0x000319E4
		private bool TryCreateActivationRuleInternal(XmlNode node, out ActivationRule activationRule)
		{
			activationRule = null;
			if (node == null || node.Attributes == null)
			{
				return false;
			}
			XmlAttribute xmlAttribute = node.Attributes["type", "http://www.w3.org/2001/XMLSchema-instance"];
			string a;
			if (!ExtensionDataHelper.TryGetNameSpaceStrippedAttributeValue(xmlAttribute, out a))
			{
				return false;
			}
			if (!string.Equals(a, "ItemIs", StringComparison.Ordinal))
			{
				if (string.Equals(a, "ItemHasKnownEntity", StringComparison.Ordinal))
				{
					KnownEntityType entityType;
					if (EnumValidator.TryParse<KnownEntityType>(node.Attributes["EntityType"].Value, EnumParseOptions.Default, out entityType))
					{
						XmlAttribute xmlAttribute2 = node.Attributes["FilterName"];
						XmlAttribute xmlAttribute3 = node.Attributes["RegExFilter"];
						bool ignoreCase = SchemaParser.ParseBoolFromXmlAttribute(node.Attributes["IgnoreCase"]);
						activationRule = new ItemHasKnownEntityRule(entityType, (xmlAttribute2 != null) ? xmlAttribute2.Value : null, (xmlAttribute3 != null) ? xmlAttribute3.Value : null, ignoreCase);
						return true;
					}
				}
				else if (string.Equals(a, "ItemHasRegularExpressionMatch", StringComparison.Ordinal))
				{
					RegExPropertyName propertyName;
					if (EnumValidator.TryParse<RegExPropertyName>(node.Attributes["PropertyName"].Value, EnumParseOptions.Default, out propertyName))
					{
						bool ignoreCase2 = SchemaParser.ParseBoolFromXmlAttribute(node.Attributes["IgnoreCase"]);
						activationRule = new ItemHasRegularExpressionMatchRule(node.Attributes["RegExName"].Value, node.Attributes["RegExValue"].Value, propertyName, ignoreCase2);
						return true;
					}
				}
				else
				{
					if (string.Equals(a, "ItemHasAttachment", StringComparison.Ordinal))
					{
						activationRule = new ItemHasAttachmentRule();
						return true;
					}
					if (node.ChildNodes != null && 0 < node.ChildNodes.Count && string.Equals(a, "RuleCollection", StringComparison.Ordinal))
					{
						ActivationRule[] array = new ActivationRule[node.ChildNodes.Count];
						int num = 0;
						foreach (object obj in node.ChildNodes)
						{
							XmlNode xmlNode = (XmlNode)obj;
							ActivationRule activationRule2;
							if (this.IsExpectedOweNamespace(xmlNode.NamespaceURI) && string.Equals(xmlNode.LocalName, "Rule", StringComparison.Ordinal) && this.TryCreateActivationRuleInternal(xmlNode, out activationRule2))
							{
								array[num++] = activationRule2;
							}
						}
						xmlAttribute = node.Attributes["Mode"];
						activationRule = new CollectionRule((xmlAttribute == null) ? "Or" : xmlAttribute.Value, array);
						return true;
					}
				}
				return false;
			}
			ItemIsRuleItemType itemType;
			if (EnumValidator.TryParse<ItemIsRuleItemType>(node.Attributes["ItemType"].Value, EnumParseOptions.Default, out itemType))
			{
				XmlAttribute xmlAttribute4 = node.Attributes["FormType"];
				ItemIsRuleFormType formType;
				if (xmlAttribute4 == null || !EnumValidator.TryParse<ItemIsRuleFormType>(xmlAttribute4.Value, EnumParseOptions.Default, out formType))
				{
					formType = ItemIsRuleFormType.Read;
				}
				bool includeSubClasses = SchemaParser.ParseBoolFromXmlAttribute(node.Attributes["IncludeSubClasses"]);
				XmlAttribute xmlAttribute5 = node.Attributes["ItemClass"];
				activationRule = new ItemIsRule(itemType, (xmlAttribute5 != null) ? xmlAttribute5.Value : null, includeSubClasses, formType);
				return true;
			}
			return false;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00033AE8 File Offset: 0x00031CE8
		private string GetLocaleAwareNodeValue(XmlNode node, string cultureName)
		{
			if (node.ChildNodes != null)
			{
				foreach (object obj in node.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (this.IsExpectedOweNamespace(xmlNode.NamespaceURI) && string.Equals(xmlNode.LocalName, "Override", StringComparison.Ordinal) && string.Equals(xmlNode.Attributes["Locale"].Value, cultureName, StringComparison.OrdinalIgnoreCase))
					{
						return xmlNode.Attributes["Value"].Value;
					}
				}
			}
			return node.Attributes["DefaultValue"].Value;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00033BB4 File Offset: 0x00031DB4
		private bool IsExpectedOweNamespace(string namespaceUri)
		{
			return string.Equals(namespaceUri, this.GetOweNamespaceUri(), StringComparison.Ordinal);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00033BC4 File Offset: 0x00031DC4
		private void ValidateSourceLocationUrls(string xpath, string urlAttributeName, string errorMessageName)
		{
			using (XmlNodeList xmlNodeList = this.xmlDoc.SelectNodes(xpath, this.namespaceManager))
			{
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string attributeStringValue = ExtensionData.GetAttributeStringValue(xmlNode, urlAttributeName);
					SchemaParser.ValidateUrl(this.extensionInstallScope, errorMessageName, attributeStringValue);
				}
			}
		}

		// Token: 0x0400069C RID: 1692
		internal const int MaxRegexRuleNumber = 5;

		// Token: 0x0400069D RID: 1693
		internal const int MaxRuleNumber = 15;

		// Token: 0x0400069E RID: 1694
		protected static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x0400069F RID: 1695
		private static FormSettings.FormSettingsType[] allFormSettingsTypes = (FormSettings.FormSettingsType[])Enum.GetValues(typeof(FormSettings.FormSettingsType));

		// Token: 0x040006A0 RID: 1696
		private static readonly HashSet<string> AllowedEntityTypesInRestricted = new HashSet<string>(StringComparer.Ordinal)
		{
			"Address",
			"Url",
			"PhoneNumber"
		};

		// Token: 0x040006A1 RID: 1697
		protected SafeXmlDocument xmlDoc;

		// Token: 0x040006A2 RID: 1698
		protected ExtensionInstallScope extensionInstallScope;

		// Token: 0x040006A3 RID: 1699
		private readonly string xpathPrefix;

		// Token: 0x040006A4 RID: 1700
		private readonly string oweNameSpacePrefixWithSemiColon;

		// Token: 0x040006A5 RID: 1701
		private readonly XmlNamespaceManager namespaceManager;
	}
}
