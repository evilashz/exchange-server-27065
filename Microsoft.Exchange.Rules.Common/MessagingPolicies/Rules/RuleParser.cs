using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200002F RID: 47
	public abstract class RuleParser
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00004708 File Offset: 0x00002908
		public static string GetDisabledRuleXml(string inputRule)
		{
			string outerXml;
			try
			{
				XmlDocument xmlDocument = new SafeXmlDocument();
				xmlDocument.LoadXml(inputRule);
				XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("enabled");
				xmlAttribute.Value = RuleConstants.StringFromRuleState(RuleState.Disabled);
				XmlAttributeCollection attributes = xmlDocument.DocumentElement.Attributes;
				XmlAttribute refNode = (XmlAttribute)attributes.GetNamedItem("name");
				attributes.Remove(attributes["enabled"]);
				attributes.InsertAfter(xmlAttribute, refNode);
				outerXml = xmlDocument.OuterXml;
			}
			catch (XmlException e)
			{
				throw new ParserException(e);
			}
			return outerXml;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004798 File Offset: 0x00002998
		public Rule GetRule(string ruleString)
		{
			return this.GetRule(ruleString, new RulesCreationContext());
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000047A8 File Offset: 0x000029A8
		public Rule GetRule(string ruleString, RulesCreationContext creationContext)
		{
			XmlTextReader xmlTextReader = null;
			XmlReader xmlReader = null;
			Rule result = null;
			StringReader stringReader = new StringReader(ruleString);
			try
			{
				xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(stringReader);
				xmlReader = XmlReader.Create(xmlTextReader, new XmlReaderSettings
				{
					ConformanceLevel = ConformanceLevel.Auto,
					IgnoreComments = true,
					DtdProcessing = DtdProcessing.Prohibit,
					XmlResolver = null
				});
				this.ReadNext(xmlReader);
				this.VerifyTag(xmlReader, "rule");
				result = this.ParseRule(xmlReader, creationContext);
			}
			catch (XmlException e)
			{
				throw new ParserException(e);
			}
			catch (RulesValidationException e2)
			{
				throw new ParserException(e2, xmlReader);
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

		// Token: 0x06000117 RID: 279 RVA: 0x00004874 File Offset: 0x00002A74
		public RuleCollection LoadStream(Stream stream)
		{
			return this.LoadStream(stream, new RulesCreationContext());
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004884 File Offset: 0x00002A84
		public RuleCollection LoadStream(Stream stream, RulesCreationContext creationContext)
		{
			XmlTextReader xmlTextReader = null;
			XmlReader xmlReader = null;
			RuleCollection result = null;
			try
			{
				xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(stream);
				xmlReader = XmlReader.Create(xmlTextReader, new XmlReaderSettings
				{
					ConformanceLevel = ConformanceLevel.Auto,
					IgnoreComments = true,
					DtdProcessing = DtdProcessing.Prohibit,
					XmlResolver = null
				});
				this.ReadNext(xmlReader);
				if (xmlReader.NodeType == XmlNodeType.XmlDeclaration)
				{
					this.ReadNext(xmlReader);
				}
				this.VerifyTag(xmlReader, "rules");
				result = this.ParseRules(xmlReader, creationContext);
			}
			catch (XmlException e)
			{
				throw new ParserException(e);
			}
			catch (RulesValidationException e2)
			{
				throw new ParserException(e2, xmlReader);
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

		// Token: 0x06000119 RID: 281 RVA: 0x0000494C File Offset: 0x00002B4C
		public Action CreateAction(string actionName, string externalName = null)
		{
			ShortList<Argument> arguments = new ShortList<Argument>();
			return this.CreateAction(actionName, arguments, externalName);
		}

		// Token: 0x0600011A RID: 282
		public abstract Action CreateAction(string actionName, ShortList<Argument> arguments, string externalName = null);

		// Token: 0x0600011B RID: 283
		public abstract Property CreateProperty(string propertyName, string typeName);

		// Token: 0x0600011C RID: 284
		public abstract Property CreateProperty(string propertyName);

		// Token: 0x0600011D RID: 285 RVA: 0x00004968 File Offset: 0x00002B68
		public virtual Rule CreateRule(string ruleName)
		{
			return new Rule(ruleName);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004970 File Offset: 0x00002B70
		public virtual RuleCollection CreateRuleCollection(string ruleName)
		{
			return new RuleCollection(ruleName);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004978 File Offset: 0x00002B78
		protected virtual void CreateRuleSubElements(Rule rule, XmlReader reader, RulesCreationContext creationContext)
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000497A File Offset: 0x00002B7A
		public virtual PredicateCondition CreatePredicate(string name, Property property, ShortList<ShortList<KeyValuePair<string, string>>> valueEntries)
		{
			return this.CreatePredicate(name, property, valueEntries, new RulesCreationContext());
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000498A File Offset: 0x00002B8A
		public virtual PredicateCondition CreatePredicate(string name, Property property, ShortList<ShortList<KeyValuePair<string, string>>> valueEntries, RulesCreationContext creationContext)
		{
			throw new RulesValidationException(RulesStrings.InvalidCondition(name), null);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004998 File Offset: 0x00002B98
		public virtual PredicateCondition CreatePredicate(string name, Property property, ShortList<string> valueEntries)
		{
			return this.CreatePredicate(name, property, valueEntries, new RulesCreationContext());
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000049A8 File Offset: 0x00002BA8
		public virtual PredicateCondition CreatePredicate(string name, Property property, ShortList<string> valueEntries, RulesCreationContext creationContext)
		{
			switch (name)
			{
			case "greaterThan":
				return new GreaterThanPredicate(property, valueEntries, creationContext);
			case "lessThan":
				return new LessThanPredicate(property, valueEntries, creationContext);
			case "greaterThanOrEqual":
				return new GreaterThanOrEqualPredicate(property, valueEntries, creationContext);
			case "lessThanOrEqual":
				return new LessThanOrEqualPredicate(property, valueEntries, creationContext);
			case "equal":
				return new EqualPredicate(property, valueEntries, creationContext);
			case "notEqual":
				return new NotEqualPredicate(property, valueEntries, creationContext);
			case "is":
				return new IsPredicate(property, valueEntries, creationContext);
			case "contains":
				return new ContainsPredicate(property, valueEntries, creationContext);
			case "matches":
				return new LegacyMatchesPredicate(property, valueEntries, creationContext);
			case "matchesRegex":
				return new MatchesRegexPredicate(property, valueEntries, creationContext);
			case "exists":
				return new ExistsPredicate(property, valueEntries, creationContext);
			case "notExists":
				return new NotExistsPredicate(property, valueEntries, creationContext);
			}
			throw new RulesValidationException(RulesStrings.InvalidCondition(name), null);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004B3C File Offset: 0x00002D3C
		protected virtual Rule ParseRuleAttributes(XmlReader reader)
		{
			this.VerifyTag(reader, "rule");
			string requiredAttribute = this.GetRequiredAttribute(reader, "name");
			string attribute = reader.GetAttribute("comments");
			string attribute2 = reader.GetAttribute("enabled");
			string attribute3 = reader.GetAttribute("id");
			Guid empty = Guid.Empty;
			if (attribute3 != null && !Guid.TryParse(attribute3, out empty))
			{
				throw new ParserException(RulesStrings.InvalidAttribute("id", "rule", attribute3), reader);
			}
			RuleState enabled;
			if (string.IsNullOrEmpty(attribute2))
			{
				enabled = RuleState.Enabled;
			}
			else if (!RuleConstants.TryParseEnabled(attribute2, out enabled))
			{
				throw new ParserException(RulesStrings.InvalidAttribute("enabled", "rule", attribute2), reader);
			}
			string attribute4 = reader.GetAttribute("expiryDate");
			DateTime? expiryDate;
			if (!RuleUtils.TryParseNullableDateTimeUtc(attribute4, out expiryDate))
			{
				throw new ParserException(RulesStrings.InvalidAttribute("expiryDate", "rule", attribute4), reader);
			}
			string attribute5 = reader.GetAttribute("activationDate");
			DateTime? activationDate;
			if (!RuleUtils.TryParseNullableDateTimeUtc(attribute5, out activationDate))
			{
				throw new ParserException(RulesStrings.InvalidAttribute("activationDate", "rule", attribute5), reader);
			}
			string attribute6 = reader.GetAttribute("mode");
			RuleMode mode = RuleConstants.TryParseEnum<RuleMode>(attribute6, RuleMode.Enforce);
			string attribute7 = reader.GetAttribute("subType");
			RuleSubType subType = RuleConstants.TryParseEnum<RuleSubType>(attribute7, RuleSubType.None);
			string attribute8 = reader.GetAttribute("errorAction");
			RuleErrorAction errorAction = RuleConstants.TryParseEnum<RuleErrorAction>(attribute8, RuleErrorAction.Ignore);
			Rule rule = this.CreateRule(requiredAttribute);
			rule.SupportGetEstimatedSize = true;
			rule.IsTooAdvancedToParse = false;
			rule.Comments = attribute;
			rule.Enabled = enabled;
			rule.ExpiryDate = expiryDate;
			rule.ActivationDate = activationDate;
			rule.Mode = mode;
			rule.SubType = subType;
			rule.ImmutableId = empty;
			rule.ErrorAction = errorAction;
			return rule;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004CE8 File Offset: 0x00002EE8
		private Property ParseProperty(XmlReader reader)
		{
			string requiredAttribute = this.GetRequiredAttribute(reader, "property");
			string attribute = reader.GetAttribute("type");
			if (requiredAttribute.Equals(string.Empty))
			{
				return null;
			}
			if (string.IsNullOrEmpty(attribute))
			{
				return this.CreateProperty(requiredAttribute);
			}
			return this.CreateProperty(requiredAttribute, attribute);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004D38 File Offset: 0x00002F38
		private RuleCollection ParseRules(XmlReader reader, RulesCreationContext creationContext)
		{
			string requiredAttribute = this.GetRequiredAttribute(reader, "name");
			RuleCollection ruleCollection = this.CreateRuleCollection(requiredAttribute);
			if (reader.IsEmptyElement)
			{
				return ruleCollection;
			}
			this.ReadNext(reader);
			while (this.IsTag(reader, "rule"))
			{
				Rule rule = this.ParseRule(reader, creationContext);
				if (ruleCollection[rule.Name] != null)
				{
					throw new ParserException(RulesStrings.RuleNameExists(rule.Name), reader);
				}
				ruleCollection.Add(rule);
				this.ReadNext(reader);
			}
			this.VerifyEndTag(reader, "rules");
			return ruleCollection;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004DC0 File Offset: 0x00002FC0
		private Rule ParseRule(XmlReader reader, RulesCreationContext creationContext)
		{
			this.VerifyNotEmptyTag(reader);
			Rule rule = this.ParseRuleAttributes(reader);
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
				catch (ArgumentException e)
				{
					throw new ParserException(e, reader);
				}
				catch (FormatException e2)
				{
					throw new ParserException(e2, reader);
				}
				rule.IsTooAdvancedToParse = (v > Rule.HighestHonoredVersion);
				if (rule.IsTooAdvancedToParse)
				{
					this.Skip(reader);
				}
				else
				{
					this.ReadNext(reader);
				}
			}
			if (!rule.IsTooAdvancedToParse)
			{
				this.CreateRuleSubElements(rule, reader, creationContext);
				if (this.IsTag(reader, "tags"))
				{
					this.ParseRuleTags(reader, rule);
				}
				this.VerifyTag(reader, "condition");
				rule.Condition = this.ParseCondition(reader, creationContext);
				this.ReadNext(reader);
				while (this.IsTag(reader, "action"))
				{
					Action action = this.ParseAction(reader);
					creationContext.ConditionAndActionSize += action.GetEstimatedSize();
					rule.Actions.Add(action);
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
			rule.ConditionAndActionSize = creationContext.ConditionAndActionSize;
			creationContext.ConditionAndActionSize = 0;
			return rule;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004F50 File Offset: 0x00003150
		private void ParseRuleTags(XmlReader reader, Rule rule)
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

		// Token: 0x06000129 RID: 297 RVA: 0x00004FE8 File Offset: 0x000031E8
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

		// Token: 0x0600012A RID: 298 RVA: 0x000050A0 File Offset: 0x000032A0
		private Action ParseAction(XmlReader reader)
		{
			string requiredAttribute = this.GetRequiredAttribute(reader, "name");
			string attribute = reader.GetAttribute("externalName");
			ShortList<Argument> shortList = new ShortList<Argument>();
			if (!reader.IsEmptyElement)
			{
				this.ReadNext(reader);
				while (this.IsTag(reader, "argument"))
				{
					shortList.Add(this.ParseArgument(reader));
					this.ReadNext(reader);
				}
				this.VerifyEndTag(reader, "action");
			}
			return this.CreateAction(requiredAttribute, shortList, attribute);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005114 File Offset: 0x00003314
		private Condition ParseCondition(XmlReader reader, RulesCreationContext creationContext)
		{
			this.VerifyNotEmptyTag(reader);
			this.ReadNext(reader);
			Condition result = this.ParseSubCondition(reader, creationContext);
			this.ReadNext(reader);
			this.VerifyEndTag(reader, "condition");
			return result;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000514C File Offset: 0x0000334C
		private Condition ParseSubCondition(XmlReader reader, RulesCreationContext creationContext)
		{
			if (reader.NodeType != XmlNodeType.Element)
			{
				throw new ParserException(RulesStrings.ConditionTagNotFound, reader);
			}
			string name = reader.Name;
			string a;
			if ((a = name) != null)
			{
				if (a == "true")
				{
					if (!reader.IsEmptyElement)
					{
						this.ReadNext(reader);
						this.VerifyEndTag(reader, name);
					}
					creationContext.ConditionAndActionSize += 18;
					return Condition.True;
				}
				if (a == "false")
				{
					if (!reader.IsEmptyElement)
					{
						this.ReadNext(reader);
						this.VerifyEndTag(reader, name);
					}
					creationContext.ConditionAndActionSize += 18;
					return Condition.False;
				}
				if (a == "not")
				{
					this.VerifyNotEmptyTag(reader);
					this.ReadNext(reader);
					NotCondition result = new NotCondition(this.ParseSubCondition(reader, creationContext));
					this.ReadNext(reader);
					this.VerifyEndTag(reader, name);
					creationContext.ConditionAndActionSize += 18;
					return result;
				}
				if (a == "and")
				{
					this.VerifyNotEmptyTag(reader);
					AndCondition andCondition = new AndCondition();
					this.ReadNext(reader);
					do
					{
						andCondition.SubConditions.Add(this.ParseSubCondition(reader, creationContext));
						this.ReadNext(reader);
					}
					while (reader.NodeType == XmlNodeType.Element);
					this.VerifyEndTag(reader, name);
					creationContext.ConditionAndActionSize += 18;
					return andCondition;
				}
				if (a == "or")
				{
					this.VerifyNotEmptyTag(reader);
					OrCondition orCondition = new OrCondition();
					this.ReadNext(reader);
					do
					{
						orCondition.SubConditions.Add(this.ParseSubCondition(reader, creationContext));
						this.ReadNext(reader);
					}
					while (reader.NodeType == XmlNodeType.Element);
					this.VerifyEndTag(reader, name);
					creationContext.ConditionAndActionSize += 18;
					return orCondition;
				}
			}
			return this.CreateSubCondition(name, reader, creationContext);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000530C File Offset: 0x0000350C
		private Condition CreateSubCondition(string conditionName, XmlReader reader, RulesCreationContext creationContext)
		{
			Property property = this.ParseProperty(reader);
			ShortList<string> shortList = new ShortList<string>();
			ShortList<ShortList<KeyValuePair<string, string>>> shortList2 = new ShortList<ShortList<KeyValuePair<string, string>>>();
			bool flag = false;
			if (!reader.IsEmptyElement)
			{
				this.ReadNext(reader);
				while (this.IsTag(reader, "keyValues") && reader.NodeType == XmlNodeType.Element)
				{
					flag = true;
					this.ReadNext(reader);
					ShortList<KeyValuePair<string, string>> shortList3 = new ShortList<KeyValuePair<string, string>>();
					while (this.IsTag(reader, "keyValue") && reader.NodeType == XmlNodeType.Element)
					{
						shortList3.Add(new KeyValuePair<string, string>(reader.GetAttribute("key"), reader.GetAttribute("value")));
						this.ReadNext(reader);
					}
					if (shortList3.Count == 0)
					{
						throw new ParserException(RulesStrings.InconsistentValueTypesInConditionProperties, reader);
					}
					shortList2.Add(shortList3);
					this.VerifyEndTag(reader, "keyValues");
					this.ReadNext(reader);
				}
				if (!flag)
				{
					while (this.IsTag(reader, "value") && reader.NodeType == XmlNodeType.Element)
					{
						shortList.Add(this.ParseSimpleValue(reader));
						flag = false;
						this.ReadNext(reader);
					}
				}
				this.VerifyEndTag(reader, conditionName);
			}
			if (flag)
			{
				return this.CreatePredicate(conditionName, property, shortList2, creationContext);
			}
			return this.CreatePredicate(conditionName, property, shortList, creationContext);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005434 File Offset: 0x00003634
		private string ParseSimpleValue(XmlReader reader)
		{
			this.ReadNext(reader, false);
			if (reader.NodeType != XmlNodeType.Text && reader.NodeType != XmlNodeType.Whitespace)
			{
				throw new ParserException(RulesStrings.ValueTextNotFound, reader);
			}
			string value = reader.Value;
			this.ReadNext(reader);
			this.VerifyEndTag(reader, "value");
			return value;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005484 File Offset: 0x00003684
		private Argument ParseArgument(XmlReader reader)
		{
			string attribute = reader.GetAttribute("value");
			string attribute2 = reader.GetAttribute("property");
			if (attribute != null && attribute2 != null)
			{
				throw new ParserException(RulesStrings.ArgumentIncorrect, reader);
			}
			Argument result;
			if (attribute != null)
			{
				result = new Value(attribute);
			}
			else
			{
				string attribute3 = reader.GetAttribute("type");
				if (string.IsNullOrEmpty(attribute3))
				{
					result = this.CreateProperty(attribute2);
				}
				else
				{
					result = this.CreateProperty(attribute2, attribute3);
				}
			}
			if (!reader.IsEmptyElement)
			{
				this.ReadNext(reader);
				this.VerifyEndTag(reader, "argument");
			}
			return result;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000550A File Offset: 0x0000370A
		protected void ReadNext(XmlReader reader)
		{
			this.ReadNext(reader, true);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005514 File Offset: 0x00003714
		protected void Skip(XmlReader reader)
		{
			reader.Skip();
			while (reader.NodeType == XmlNodeType.Whitespace)
			{
				if (!reader.Read())
				{
					throw new ParserException(RulesStrings.EndOfStream, reader);
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000553C File Offset: 0x0000373C
		protected void ReadNext(XmlReader reader, bool ignoreWhiteSpace)
		{
			if (!reader.Read())
			{
				throw new ParserException(RulesStrings.EndOfStream, reader);
			}
			while (ignoreWhiteSpace && reader.NodeType == XmlNodeType.Whitespace)
			{
				if (!reader.Read())
				{
					throw new ParserException(RulesStrings.EndOfStream, reader);
				}
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005573 File Offset: 0x00003773
		protected void VerifyEndTag(XmlReader reader, string tagName)
		{
			if (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals(tagName))
			{
				throw new ParserException(RulesStrings.EndTagNotFound(tagName), reader);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000559A File Offset: 0x0000379A
		protected void VerifyTag(XmlReader reader, string tagName)
		{
			if (reader.NodeType != XmlNodeType.Element || !reader.Name.Equals(tagName))
			{
				throw new ParserException(RulesStrings.TagNotFound(tagName), reader);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000055C0 File Offset: 0x000037C0
		protected bool IsTag(XmlReader reader, string tagName)
		{
			return reader.NodeType == XmlNodeType.Element && reader.Name.Equals(tagName);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000055DC File Offset: 0x000037DC
		protected void VerifyNotEmptyTag(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				throw new ParserException(RulesStrings.EmptyTag(reader.Name), reader);
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000055F8 File Offset: 0x000037F8
		protected string GetRequiredAttribute(XmlReader reader, string attributeName)
		{
			string attribute = reader.GetAttribute(attributeName);
			if (attribute == null)
			{
				throw new ParserException(RulesStrings.AttributeNotFound(attributeName, reader.Name), reader);
			}
			return attribute;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005624 File Offset: 0x00003824
		protected void VerifyAttributeValue(XmlReader reader, string attributeName, string attributeValue)
		{
			if (string.IsNullOrWhiteSpace(attributeValue))
			{
				throw new ParserException(RulesStrings.AttributeNotFound(attributeName, reader.Name), reader);
			}
		}
	}
}
