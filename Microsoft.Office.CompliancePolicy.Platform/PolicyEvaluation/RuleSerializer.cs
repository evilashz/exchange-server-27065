using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000DF RID: 223
	public class RuleSerializer
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x00012288 File Offset: 0x00010488
		public void SaveRulesToStream(IEnumerable<PolicyRule> rules, Stream stream)
		{
			XmlWriterSettings settings = new XmlWriterSettings
			{
				CloseOutput = false,
				Indent = false
			};
			if (stream.CanSeek)
			{
				stream.Seek(0L, SeekOrigin.Begin);
			}
			XmlWriter xmlWriter = XmlWriter.Create(stream, settings);
			this.SaveRules(xmlWriter, rules);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000122D0 File Offset: 0x000104D0
		public string SaveRuleToString(PolicyRule rule)
		{
			XmlWriterSettings settings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				Indent = false
			};
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);
				this.SaveRule(xmlWriter, rule);
				xmlWriter.Close();
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00012338 File Offset: 0x00010538
		internal void SaveActionArgument(XmlWriter xmlWriter, Argument argument)
		{
			if (argument is Value)
			{
				Value value = argument as Value;
				xmlWriter.WriteStartElement("argument");
				if (value.RawValues.Count > 1)
				{
					using (List<string>.Enumerator enumerator = value.RawValues.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string value2 = enumerator.Current;
							xmlWriter.WriteStartElement("value");
							xmlWriter.WriteValue(value2);
							xmlWriter.WriteEndElement();
						}
						goto IL_89;
					}
				}
				xmlWriter.WriteAttributeString("value", value.RawValues[0]);
				IL_89:
				xmlWriter.WriteEndElement();
				return;
			}
			if (argument is Property)
			{
				xmlWriter.WriteStartElement("argument");
				this.SaveProperty(xmlWriter, argument as Property);
				xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001240C File Offset: 0x0001060C
		protected virtual void SaveRuleSubElements(XmlWriter writer, PolicyRule rule)
		{
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00012410 File Offset: 0x00010610
		protected virtual void SaveProperty(XmlWriter xmlWriter, Property property)
		{
			xmlWriter.WriteAttributeString("property", property.Name);
			xmlWriter.WriteAttributeString("type", property.Type.ToString());
			if (!string.IsNullOrWhiteSpace(property.SupplementalInfo))
			{
				xmlWriter.WriteAttributeString("suppl", property.SupplementalInfo);
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00012464 File Offset: 0x00010664
		protected virtual void SaveValue(XmlWriter xmlWriter, Value value)
		{
			if (value != null)
			{
				if (value.ParsedValue is List<List<KeyValuePair<string, string>>>)
				{
					List<List<KeyValuePair<string, string>>> list = (List<List<KeyValuePair<string, string>>>)value.ParsedValue;
					using (List<List<KeyValuePair<string, string>>>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							List<KeyValuePair<string, string>> list2 = enumerator.Current;
							xmlWriter.WriteStartElement("keyValues");
							foreach (KeyValuePair<string, string> keyValuePair in list2)
							{
								xmlWriter.WriteStartElement("keyValue");
								xmlWriter.WriteAttributeString("key", keyValuePair.Key);
								xmlWriter.WriteAttributeString("value", keyValuePair.Value);
								xmlWriter.WriteEndElement();
							}
							xmlWriter.WriteEndElement();
						}
						return;
					}
				}
				foreach (string value2 in value.RawValues)
				{
					xmlWriter.WriteStartElement("value");
					xmlWriter.WriteValue(value2);
					xmlWriter.WriteEndElement();
				}
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000125A4 File Offset: 0x000107A4
		protected virtual void SaveRuleAttributes(XmlWriter xmlWriter, PolicyRule rule)
		{
			if (!string.IsNullOrEmpty(rule.Comments))
			{
				xmlWriter.WriteAttributeString("comments", rule.Comments);
			}
			if (rule.Enabled != RuleState.Enabled)
			{
				xmlWriter.WriteAttributeString("enabled", RuleConstants.StringFromRuleState(rule.Enabled));
			}
			if (rule.ExpiryDate != null)
			{
				xmlWriter.WriteAttributeString("expiryDate", PolicyUtils.DateTimeToUtcString(rule.ExpiryDate.Value));
			}
			if (rule.ActivationDate != null)
			{
				xmlWriter.WriteAttributeString("activationDate", PolicyUtils.DateTimeToUtcString(rule.ActivationDate.Value));
			}
			if (rule.ImmutableId != Guid.Empty)
			{
				xmlWriter.WriteAttributeString("id", rule.ImmutableId.ToString());
			}
			if (rule.Mode != RuleMode.Enforce)
			{
				xmlWriter.WriteAttributeString("mode", Enum.GetName(typeof(RuleMode), rule.Mode));
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000126AC File Offset: 0x000108AC
		private void SaveRules(XmlWriter xmlWriter, IEnumerable<PolicyRule> rules)
		{
			xmlWriter.WriteStartElement("rules");
			xmlWriter.WriteAttributeString("name", "rulesVersioned");
			foreach (PolicyRule rule in rules)
			{
				this.SaveRule(xmlWriter, rule);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001271C File Offset: 0x0001091C
		private void SaveRule(XmlWriter xmlWriter, PolicyRule rule)
		{
			xmlWriter.WriteStartElement("rule");
			xmlWriter.WriteAttributeString("name", rule.Name);
			this.SaveRuleAttributes(xmlWriter, rule);
			xmlWriter.WriteStartElement("version");
			xmlWriter.WriteAttributeString("requiredMinVersion", rule.MinimumVersion.ToString());
			this.SaveRuleSubElements(xmlWriter, rule);
			this.SaveTags(xmlWriter, rule.GetTags());
			this.SaveCondition(xmlWriter, rule.Condition);
			foreach (Action action in rule.Actions)
			{
				this.SaveAction(xmlWriter, action);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndElement();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000127E0 File Offset: 0x000109E0
		private void SaveTags(XmlWriter xmlWriter, IEnumerable<RuleTag> ruleTags)
		{
			if (ruleTags.Any<RuleTag>())
			{
				xmlWriter.WriteStartElement("tags");
				foreach (RuleTag ruleTag in ruleTags)
				{
					xmlWriter.WriteStartElement("tag");
					xmlWriter.WriteAttributeString("name", ruleTag.Name);
					xmlWriter.WriteAttributeString("type", ruleTag.TagType);
					foreach (KeyValuePair<string, string> keyValuePair in ruleTag.Data)
					{
						xmlWriter.WriteAttributeString(keyValuePair.Key, keyValuePair.Value);
					}
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000128C4 File Offset: 0x00010AC4
		private void SaveCondition(XmlWriter xmlWriter, Condition condition)
		{
			xmlWriter.WriteStartElement("condition");
			this.SaveSubCondition(xmlWriter, condition);
			xmlWriter.WriteEndElement();
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000128E0 File Offset: 0x00010AE0
		private void SaveSubCondition(XmlWriter xmlWriter, Condition condition)
		{
			switch (condition.ConditionType)
			{
			case ConditionType.And:
				xmlWriter.WriteStartElement("and");
				using (List<Condition>.Enumerator enumerator = ((AndCondition)condition).SubConditions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Condition condition2 = enumerator.Current;
						this.SaveSubCondition(xmlWriter, condition2);
					}
					goto IL_157;
				}
				break;
			case ConditionType.Or:
				break;
			case ConditionType.Not:
				xmlWriter.WriteStartElement("not");
				this.SaveSubCondition(xmlWriter, ((NotCondition)condition).SubCondition);
				goto IL_157;
			case ConditionType.True:
				xmlWriter.WriteStartElement("true");
				goto IL_157;
			case ConditionType.False:
				xmlWriter.WriteStartElement("false");
				goto IL_157;
			case ConditionType.Predicate:
			{
				PredicateCondition predicateCondition = (PredicateCondition)condition;
				xmlWriter.WriteStartElement(predicateCondition.Name);
				this.SaveProperty(xmlWriter, predicateCondition.Property);
				this.SaveValue(xmlWriter, predicateCondition.Value);
				goto IL_157;
			}
			case ConditionType.DynamicQuery:
				goto IL_103;
			default:
				throw new NotSupportedException();
			}
			xmlWriter.WriteStartElement("or");
			using (List<Condition>.Enumerator enumerator2 = ((OrCondition)condition).SubConditions.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Condition condition3 = enumerator2.Current;
					this.SaveSubCondition(xmlWriter, condition3);
				}
				goto IL_157;
			}
			IL_103:
			xmlWriter.WriteStartElement("queryMatch");
			this.SaveSubCondition(xmlWriter, ((QueryPredicate)condition).SubCondition);
			IL_157:
			xmlWriter.WriteEndElement();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00012A68 File Offset: 0x00010C68
		private void SaveAction(XmlWriter xmlWriter, Action action)
		{
			xmlWriter.WriteStartElement("action");
			xmlWriter.WriteAttributeString("name", action.Name);
			if (action.HasExternalName)
			{
				xmlWriter.WriteAttributeString("externalName", action.ExternalName);
			}
			foreach (Argument argument in action.Arguments)
			{
				this.SaveActionArgument(xmlWriter, argument);
			}
			xmlWriter.WriteEndElement();
		}
	}
}
