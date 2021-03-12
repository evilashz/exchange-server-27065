using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000DE RID: 222
	public class RuleParser
	{
		// Token: 0x0600059B RID: 1435 RVA: 0x00011044 File Offset: 0x0000F244
		public RuleParser(IPolicyParserFactory policyParserFactory)
		{
			ArgumentValidator.ThrowIfNull("policyParserFactory", policyParserFactory);
			IEnumerable<string> actionsSupportedByFactory = policyParserFactory.GetSupportedActions();
			using (IEnumerator<string> enumerator = (from tagName in RuleParser.MandatoryActions
			where !actionsSupportedByFactory.Contains(tagName)
			select tagName).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					throw new CompliancePolicyValidationException("Supplied parser factory does not support '{0}' action. All workloads must support common actions.", new object[]
					{
						text
					});
				}
			}
			this.policyParserFactory = policyParserFactory;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000110DC File Offset: 0x0000F2DC
		protected RuleParser()
		{
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000110E4 File Offset: 0x0000F2E4
		public static string GetDisabledRuleXml(string inputRule)
		{
			string outerXml;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				StringReader input = new StringReader(inputRule);
				XmlTextReader reader = new XmlTextReader(input)
				{
					DtdProcessing = DtdProcessing.Prohibit,
					XmlResolver = null
				};
				XmlReaderSettings settings = new XmlReaderSettings
				{
					ConformanceLevel = ConformanceLevel.Auto,
					IgnoreComments = true,
					DtdProcessing = DtdProcessing.Prohibit,
					XmlResolver = null
				};
				XmlReader reader2 = XmlReader.Create(reader, settings);
				xmlDocument.Load(reader2);
				XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("enabled");
				xmlAttribute.Value = RuleConstants.StringFromRuleState(RuleState.Disabled);
				XmlAttributeCollection attributes = xmlDocument.DocumentElement.Attributes;
				XmlAttribute refNode = (XmlAttribute)attributes.GetNamedItem("name");
				attributes.Remove(attributes["enabled"]);
				attributes.InsertAfter(xmlAttribute, refNode);
				outerXml = xmlDocument.OuterXml;
			}
			catch (XmlException innerException)
			{
				throw new CompliancePolicyParserException("Compliance policy parsing error", innerException);
			}
			return outerXml;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000111DC File Offset: 0x0000F3DC
		public PolicyRule GetRule(string ruleString)
		{
			XmlTextReader xmlTextReader = null;
			XmlReader xmlReader = null;
			PolicyRule result = null;
			StringReader stringReader = new StringReader(ruleString);
			try
			{
				xmlTextReader = new XmlTextReader(stringReader);
				xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
				xmlTextReader.XmlResolver = null;
				XmlReaderSettings settings = new XmlReaderSettings
				{
					ConformanceLevel = ConformanceLevel.Auto,
					IgnoreComments = true,
					DtdProcessing = DtdProcessing.Prohibit,
					XmlResolver = null
				};
				xmlReader = XmlReader.Create(xmlTextReader, settings);
				this.ReadNext(xmlReader);
				this.VerifyTag(xmlReader, "rule");
				result = this.ParseRule(xmlReader);
			}
			catch (XmlException innerException)
			{
				throw new CompliancePolicyParserException("Error parsing rule", innerException);
			}
			catch (CompliancePolicyException innerException2)
			{
				throw new CompliancePolicyParserException("Error creating rule from XML", innerException2);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
				if (stringReader != null)
				{
					stringReader.Close();
				}
			}
			return result;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000112C4 File Offset: 0x0000F4C4
		public IList<PolicyRule> LoadStream(Stream stream)
		{
			XmlTextReader xmlTextReader = null;
			XmlReader xmlReader = null;
			IList<PolicyRule> result = null;
			try
			{
				xmlTextReader = new XmlTextReader(stream)
				{
					DtdProcessing = DtdProcessing.Prohibit,
					XmlResolver = null
				};
				XmlReaderSettings settings = new XmlReaderSettings
				{
					ConformanceLevel = ConformanceLevel.Auto,
					IgnoreComments = true,
					DtdProcessing = DtdProcessing.Prohibit,
					XmlResolver = null
				};
				xmlReader = XmlReader.Create(xmlTextReader, settings);
				this.ReadNext(xmlReader);
				if (xmlReader.NodeType == XmlNodeType.XmlDeclaration)
				{
					this.ReadNext(xmlReader);
				}
				this.VerifyTag(xmlReader, "rules");
				result = this.ParseRules(xmlReader);
			}
			catch (XmlException innerException)
			{
				throw new CompliancePolicyParserException("Error parsing rule", innerException);
			}
			catch (CompliancePolicyValidationException innerException2)
			{
				throw new CompliancePolicyParserException("Error creating rule from XML", innerException2);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
			return result;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000113B0 File Offset: 0x0000F5B0
		public Action CreateAction(string actionName, string externalName = null)
		{
			List<Argument> arguments = new List<Argument>();
			return this.CreateAction(actionName, arguments, externalName);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000113CC File Offset: 0x0000F5CC
		public virtual Action CreateAction(string actionName, List<Argument> arguments, string externalName = null)
		{
			Action action = this.policyParserFactory.CreateAction(actionName, arguments, externalName);
			if (action != null)
			{
				return action;
			}
			throw new CompliancePolicyParserException(string.Format("Workload factory failed to create action '{0}'.", actionName));
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00011400 File Offset: 0x0000F600
		public virtual Property CreateProperty(string propertyName, string typeName = null)
		{
			Property property = this.policyParserFactory.CreateProperty(propertyName, typeName) ?? Property.CreateProperty(propertyName, typeName);
			if (property == null)
			{
				throw new CompliancePolicyParserException(string.Format("No suitable factory for the property '{0}' found. ", propertyName));
			}
			return property;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001143C File Offset: 0x0000F63C
		public virtual PolicyRule CreateRule(string ruleName)
		{
			return new PolicyRule
			{
				Name = ruleName
			};
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00011457 File Offset: 0x0000F657
		public virtual IList<PolicyRule> CreateRuleCollection()
		{
			return new List<PolicyRule>();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001145E File Offset: 0x0000F65E
		public virtual PredicateCondition CreatePredicate(string name, Property property, List<List<KeyValuePair<string, string>>> valueEntries)
		{
			if (name.Equals("containsDataClassification", StringComparison.InvariantCultureIgnoreCase))
			{
				return new ContentContainsSensitiveInformationPredicate(valueEntries);
			}
			throw new CompliancePolicyValidationException("Invalid condition " + name);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00011488 File Offset: 0x0000F688
		public virtual PredicateCondition CreatePredicate(string name, Property property, List<string> valueEntries)
		{
			PredicateCondition predicateCondition = this.policyParserFactory.CreatePredicate(name, property, valueEntries);
			if (predicateCondition != null)
			{
				return predicateCondition;
			}
			switch (name)
			{
			case "equal":
				return new EqualPredicate(property, valueEntries);
			case "exists":
				return new ExistsPredicate(property, valueEntries);
			case "greaterThan":
				return new GreaterThanPredicate(property, valueEntries);
			case "greaterThanOrEqual":
				return new GreaterThanOrEqualPredicate(property, valueEntries);
			case "is":
				return new IsPredicate(property, valueEntries);
			case "lessThan":
				return new LessThanPredicate(property, valueEntries);
			case "lessThanOrEqual":
				return new LessThanOrEqualPredicate(property, valueEntries);
			case "notEqual":
				return new NotEqualPredicate(property, valueEntries);
			case "notExists":
				return new NotExistsPredicate(property, valueEntries);
			case "textQueryMatch":
				return new TextQueryPredicate(property, valueEntries);
			case "auditOperations":
				return new AuditOperationsPredicate(property, valueEntries);
			case "NameValuesPairConfiguration":
				return new NameValuesPairConfigurationPredicate(property, valueEntries);
			case "contentMetadataContains":
				return new ContentMetadataContainsPredicate(valueEntries);
			}
			throw new CompliancePolicyValidationException("Condition name is not recognized - " + name);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00011634 File Offset: 0x0000F834
		internal Argument ParseArgument(XmlReader reader)
		{
			string attribute = reader.GetAttribute("value");
			string attribute2 = reader.GetAttribute("property");
			if (attribute != null && attribute2 != null)
			{
				throw new CompliancePolicyParserException("Argument is incorrect");
			}
			Argument argument = null;
			if (attribute != null)
			{
				argument = new Value(attribute);
			}
			else if (attribute2 != null)
			{
				argument = this.CreateProperty(attribute2, reader.GetAttribute("type"));
			}
			if (!reader.IsEmptyElement)
			{
				this.ReadNext(reader);
				if (this.IsTag(reader, "value") && reader.NodeType == XmlNodeType.Element)
				{
					if (argument != null)
					{
						throw new CompliancePolicyParserException("Argument is incorrect. The argument tag should not have any children tags if it has the value attribute.");
					}
					List<string> list = new List<string>();
					while (this.IsTag(reader, "value") && reader.NodeType == XmlNodeType.Element)
					{
						list.Add(this.ParseSimpleValue(reader));
						this.ReadNext(reader);
					}
					if (!list.Any<string>())
					{
						throw new CompliancePolicyParserException("Argument is incorrect. There must be children tags under the argument tag if the argument tag does not have the value attribute.");
					}
					argument = new Value(list);
				}
				this.VerifyEndTag(reader, "argument");
			}
			return argument;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001171F File Offset: 0x0000F91F
		protected virtual void CreateRuleSubElements(PolicyRule rule, XmlReader reader)
		{
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00011724 File Offset: 0x0000F924
		protected virtual PolicyRule ParseRuleAttributes(XmlReader reader)
		{
			this.VerifyTag(reader, "rule");
			string requiredAttribute = this.GetRequiredAttribute(reader, "name");
			string attribute = reader.GetAttribute("comments");
			string attribute2 = reader.GetAttribute("enabled");
			string attribute3 = reader.GetAttribute("id");
			Guid empty = Guid.Empty;
			if (attribute3 != null && !Guid.TryParse(attribute3, out empty))
			{
				throw new CompliancePolicyParserException(string.Format("Invalid Attribute {0} {1}", "rule", attribute3), new object[]
				{
					reader
				});
			}
			RuleState enabled;
			if (string.IsNullOrEmpty(attribute2))
			{
				enabled = RuleState.Enabled;
			}
			else if (!RuleConstants.TryParseEnabled(attribute2, out enabled))
			{
				throw new CompliancePolicyParserException(string.Format("Invalid Attribute {0} {1} {2}", "enabled", "rule", attribute2), new object[]
				{
					reader
				});
			}
			string attribute4 = reader.GetAttribute("expiryDate");
			DateTime? expiryDate;
			if (!PolicyUtils.TryParseNullableDateTimeUtc(attribute4, out expiryDate))
			{
				throw new CompliancePolicyParserException(string.Format("Invalid Attribute {0} {1} {2}", "expiryDate", "rule", attribute4), new object[]
				{
					reader
				});
			}
			string attribute5 = reader.GetAttribute("activationDate");
			DateTime? activationDate;
			if (!PolicyUtils.TryParseNullableDateTimeUtc(attribute5, out activationDate))
			{
				throw new CompliancePolicyParserException(string.Format("Invalid Attribute {0} {1} {2}", "activationDate", "rule", attribute5), new object[]
				{
					reader
				});
			}
			string attribute6 = reader.GetAttribute("mode");
			RuleMode mode = RuleConstants.TryParseEnum<RuleMode>(attribute6, RuleMode.Enforce);
			PolicyRule policyRule = this.CreateRule(requiredAttribute);
			policyRule.IsTooAdvancedToParse = false;
			policyRule.Comments = attribute;
			policyRule.Enabled = enabled;
			policyRule.ExpiryDate = expiryDate;
			policyRule.ActivationDate = activationDate;
			policyRule.Mode = mode;
			policyRule.ImmutableId = empty;
			return policyRule;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000118CD File Offset: 0x0000FACD
		protected void ReadNext(XmlReader reader)
		{
			this.ReadNext(reader, true);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000118D8 File Offset: 0x0000FAD8
		protected void Skip(XmlReader reader)
		{
			reader.Skip();
			while (reader.NodeType == XmlNodeType.Whitespace)
			{
				if (!reader.Read())
				{
					throw new CompliancePolicyParserException("End of stream reached", new object[]
					{
						reader
					});
				}
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00011918 File Offset: 0x0000FB18
		protected void ReadNext(XmlReader reader, bool ignoreWhiteSpace)
		{
			if (!reader.Read())
			{
				throw new CompliancePolicyParserException("End of stream reached", new object[]
				{
					reader
				});
			}
			while (ignoreWhiteSpace && reader.NodeType == XmlNodeType.Whitespace)
			{
				if (!reader.Read())
				{
					throw new CompliancePolicyParserException("End of stream reached", new object[]
					{
						reader
					});
				}
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00011970 File Offset: 0x0000FB70
		protected void VerifyEndTag(XmlReader reader, string tagName)
		{
			if (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals(tagName))
			{
				throw new CompliancePolicyParserException(string.Format("End tag not found '{0}'", tagName));
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001199B File Offset: 0x0000FB9B
		protected void VerifyTag(XmlReader reader, string tagName)
		{
			if (reader.NodeType != XmlNodeType.Element || !reader.Name.Equals(tagName))
			{
				throw new CompliancePolicyParserException(string.Format("Tag not found '{0}'", tagName));
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000119C5 File Offset: 0x0000FBC5
		protected bool IsTag(XmlReader reader, string tagName)
		{
			return reader.NodeType == XmlNodeType.Element && reader.Name.Equals(tagName);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000119E1 File Offset: 0x0000FBE1
		protected void VerifyNotEmptyTag(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				throw new CompliancePolicyParserException(string.Format("Tag '{0}' is empty", reader.Name));
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00011A04 File Offset: 0x0000FC04
		protected string GetRequiredAttribute(XmlReader reader, string attributeName)
		{
			string attribute = reader.GetAttribute(attributeName);
			if (attribute == null)
			{
				throw new CompliancePolicyParserException(string.Format("Attribute not found '{0}' '{1}'", attributeName, reader.Name));
			}
			return attribute;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00011A34 File Offset: 0x0000FC34
		protected void VerifyAttributeValue(XmlReader reader, string attributeName, string attributeValue)
		{
			if (string.IsNullOrWhiteSpace(attributeValue))
			{
				throw new CompliancePolicyParserException(string.Format("Attribute not found '{0}' '{1}'", attributeName, reader.Name));
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00011A58 File Offset: 0x0000FC58
		private Property ParseProperty(XmlReader reader)
		{
			string requiredAttribute = this.GetRequiredAttribute(reader, "property");
			string attribute = reader.GetAttribute("type");
			string attribute2 = reader.GetAttribute("suppl");
			if (requiredAttribute.Equals(string.Empty))
			{
				return null;
			}
			Property property = this.CreateProperty(requiredAttribute, attribute);
			property.SupplementalInfo = attribute2;
			return property;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00011AD0 File Offset: 0x0000FCD0
		private IList<PolicyRule> ParseRules(XmlReader reader)
		{
			this.GetRequiredAttribute(reader, "name");
			IList<PolicyRule> list = this.CreateRuleCollection();
			if (reader.IsEmptyElement)
			{
				return list;
			}
			this.ReadNext(reader);
			while (this.IsTag(reader, "rule"))
			{
				PolicyRule rule = this.ParseRule(reader);
				if (list.Any((PolicyRule r) => string.Compare(r.Name, rule.Name, StringComparison.InvariantCultureIgnoreCase) == 0))
				{
					throw new CompliancePolicyParserException(string.Format("Rule '{0}' already exists. No duplicate names allowed", rule.Name));
				}
				list.Add(rule);
				this.ReadNext(reader);
			}
			this.VerifyEndTag(reader, "rules");
			return list;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00011B74 File Offset: 0x0000FD74
		private PolicyRule ParseRule(XmlReader reader)
		{
			this.VerifyNotEmptyTag(reader);
			PolicyRule policyRule = this.ParseRuleAttributes(reader);
			this.ReadNext(reader);
			bool flag = this.IsTag(reader, "version");
			if (flag)
			{
				this.VerifyTag(reader, "version");
				string requiredAttribute = this.GetRequiredAttribute(reader, "requiredMinVersion");
				Version v = null;
				try
				{
					v = new Version(requiredAttribute);
				}
				catch (ArgumentException innerException)
				{
					throw new CompliancePolicyParserException("Error parsing rule", innerException);
				}
				catch (FormatException innerException2)
				{
					throw new CompliancePolicyParserException("Error parsing rule", innerException2);
				}
				policyRule.IsTooAdvancedToParse = (v > PolicyRule.HighestHonoredVersion);
				if (policyRule.IsTooAdvancedToParse)
				{
					this.Skip(reader);
				}
				else
				{
					this.ReadNext(reader);
				}
			}
			if (!policyRule.IsTooAdvancedToParse)
			{
				this.CreateRuleSubElements(policyRule, reader);
				if (this.IsTag(reader, "tags"))
				{
					this.ParseRuleTags(reader, policyRule);
				}
				this.VerifyTag(reader, "condition");
				policyRule.Condition = this.ParseCondition(reader);
				this.ReadNext(reader);
				while (this.IsTag(reader, "action"))
				{
					Action item = this.ParseAction(reader);
					policyRule.Actions.Add(item);
					this.ReadNext(reader);
				}
				if (flag)
				{
					this.VerifyEndTag(reader, "version");
					this.ReadNext(reader);
				}
			}
			if (flag)
			{
				while (this.IsTag(reader, "version"))
				{
					this.Skip(reader);
				}
			}
			this.VerifyEndTag(reader, "rule");
			return policyRule;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		private void ParseRuleTags(XmlReader reader, PolicyRule rule)
		{
			this.ReadNext(reader);
			while (this.IsTag(reader, "tag"))
			{
				rule.AddTag(this.ParseRuleTag(reader));
				this.ReadNext(reader);
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (!(reader.Name == "tag"))
					{
						break;
					}
					this.ReadNext(reader);
				}
			}
			this.VerifyEndTag(reader, "tags");
			this.ReadNext(reader);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00011D7C File Offset: 0x0000FF7C
		private RuleTag ParseRuleTag(XmlReader reader)
		{
			string text = null;
			string text2 = null;
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			while (reader.MoveToNextAttribute())
			{
				string name;
				if ((name = reader.Name) != null)
				{
					if (name == "name")
					{
						text = reader.Value;
						continue;
					}
					if (name == "type")
					{
						text2 = reader.Value;
						continue;
					}
				}
				list.Add(new KeyValuePair<string, string>(reader.Name, reader.Value));
			}
			this.VerifyAttributeValue(reader, "name", text);
			this.VerifyAttributeValue(reader, "type", text2);
			RuleTag result = new RuleTag(text, text2);
			list.ForEach(delegate(KeyValuePair<string, string> item)
			{
				result.Data.Add(item.Key, item.Value);
			});
			return result;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00011E34 File Offset: 0x00010034
		private Action ParseAction(XmlReader reader)
		{
			string requiredAttribute = this.GetRequiredAttribute(reader, "name");
			string attribute = reader.GetAttribute("externalName");
			List<Argument> list = new List<Argument>();
			if (!reader.IsEmptyElement)
			{
				this.ReadNext(reader);
				while (this.IsTag(reader, "argument"))
				{
					list.Add(this.ParseArgument(reader));
					this.ReadNext(reader);
				}
				this.VerifyEndTag(reader, "action");
			}
			return this.CreateAction(requiredAttribute, list, attribute);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00011EA8 File Offset: 0x000100A8
		private Condition ParseCondition(XmlReader reader)
		{
			this.VerifyNotEmptyTag(reader);
			this.ReadNext(reader);
			Condition result = this.ParseSubCondition(reader);
			this.ReadNext(reader);
			this.VerifyEndTag(reader, "condition");
			return result;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00011EE0 File Offset: 0x000100E0
		private Condition ParseSubCondition(XmlReader reader)
		{
			if (reader.NodeType != XmlNodeType.Element)
			{
				throw new CompliancePolicyParserException("No COnditions tag found");
			}
			string name = reader.Name;
			string key;
			switch (key = name)
			{
			case "true":
				if (!reader.IsEmptyElement)
				{
					this.ReadNext(reader);
					this.VerifyEndTag(reader, name);
				}
				return Condition.True;
			case "false":
				if (!reader.IsEmptyElement)
				{
					this.ReadNext(reader);
					this.VerifyEndTag(reader, name);
				}
				return Condition.False;
			case "not":
			{
				this.VerifyNotEmptyTag(reader);
				this.ReadNext(reader);
				NotCondition result = new NotCondition(this.ParseSubCondition(reader));
				this.ReadNext(reader);
				this.VerifyEndTag(reader, name);
				return result;
			}
			case "and":
			{
				this.VerifyNotEmptyTag(reader);
				AndCondition andCondition = new AndCondition();
				this.ReadNext(reader);
				do
				{
					andCondition.SubConditions.Add(this.ParseSubCondition(reader));
					this.ReadNext(reader);
				}
				while (reader.NodeType == XmlNodeType.Element);
				this.VerifyEndTag(reader, name);
				return andCondition;
			}
			case "or":
			{
				this.VerifyNotEmptyTag(reader);
				OrCondition orCondition = new OrCondition();
				this.ReadNext(reader);
				do
				{
					orCondition.SubConditions.Add(this.ParseSubCondition(reader));
					this.ReadNext(reader);
				}
				while (reader.NodeType == XmlNodeType.Element);
				this.VerifyEndTag(reader, name);
				return orCondition;
			}
			case "queryMatch":
			{
				this.VerifyNotEmptyTag(reader);
				this.ReadNext(reader);
				QueryPredicate result2 = new QueryPredicate(this.ParseSubCondition(reader));
				this.ReadNext(reader);
				this.VerifyEndTag(reader, name);
				return result2;
			}
			}
			return this.CreateSubCondition(name, reader);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000120C4 File Offset: 0x000102C4
		private Condition CreateSubCondition(string conditionName, XmlReader reader)
		{
			Property property = this.ParseProperty(reader);
			List<string> list = new List<string>();
			List<List<KeyValuePair<string, string>>> list2 = new List<List<KeyValuePair<string, string>>>();
			bool flag = false;
			if (!reader.IsEmptyElement)
			{
				this.ReadNext(reader);
				while (this.IsTag(reader, "keyValues") && reader.NodeType == XmlNodeType.Element)
				{
					flag = true;
					this.ReadNext(reader);
					List<KeyValuePair<string, string>> list3 = new List<KeyValuePair<string, string>>();
					while (this.IsTag(reader, "keyValue") && reader.NodeType == XmlNodeType.Element)
					{
						list3.Add(new KeyValuePair<string, string>(reader.GetAttribute("key"), reader.GetAttribute("value")));
						this.ReadNext(reader);
					}
					if (list3.Count == 0)
					{
						throw new CompliancePolicyParserException("Inconsistent value types in condition properties");
					}
					list2.Add(list3);
					this.VerifyEndTag(reader, "keyValues");
					this.ReadNext(reader);
				}
				if (!flag)
				{
					while (this.IsTag(reader, "value") && reader.NodeType == XmlNodeType.Element)
					{
						list.Add(this.ParseSimpleValue(reader));
						flag = false;
						this.ReadNext(reader);
					}
				}
				this.VerifyEndTag(reader, conditionName);
			}
			if (flag)
			{
				return this.CreatePredicate(conditionName, property, list2);
			}
			return this.CreatePredicate(conditionName, property, list);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000121EC File Offset: 0x000103EC
		private string ParseSimpleValue(XmlReader reader)
		{
			this.ReadNext(reader, false);
			if (reader.NodeType != XmlNodeType.Text && reader.NodeType != XmlNodeType.Whitespace)
			{
				throw new CompliancePolicyParserException("Value tag not found");
			}
			string value = reader.Value;
			this.ReadNext(reader);
			this.VerifyEndTag(reader, "value");
			return value;
		}

		// Token: 0x04000395 RID: 917
		private static readonly string[] MandatoryActions = new string[]
		{
			"Hold",
			"RetentionExpire",
			"RetentionRecycle",
			"BlockAccess",
			"GenerateIncidentReport",
			"NotifyAuthors"
		};

		// Token: 0x04000396 RID: 918
		private readonly IPolicyParserFactory policyParserFactory;
	}
}
