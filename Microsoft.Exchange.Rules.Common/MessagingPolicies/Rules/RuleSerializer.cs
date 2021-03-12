using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000030 RID: 48
	public class RuleSerializer
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000564C File Offset: 0x0000384C
		public void SaveRulesToStream(RuleCollection rules, Stream stream)
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

		// Token: 0x0600013B RID: 315 RVA: 0x00005694 File Offset: 0x00003894
		public string SaveRuleToString(Rule rule)
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

		// Token: 0x0600013C RID: 316 RVA: 0x000056FC File Offset: 0x000038FC
		protected virtual void SaveRuleSubElements(XmlWriter writer, Rule rule)
		{
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000056FE File Offset: 0x000038FE
		protected virtual void SaveProperty(XmlWriter xmlWriter, Property property)
		{
			xmlWriter.WriteAttributeString("property", property.Name);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005714 File Offset: 0x00003914
		protected virtual void SaveValue(XmlWriter xmlWriter, Value value)
		{
			if (value != null)
			{
				if (value.ParsedValue is ShortList<ShortList<KeyValuePair<string, string>>>)
				{
					ShortList<ShortList<KeyValuePair<string, string>>> shortList = (ShortList<ShortList<KeyValuePair<string, string>>>)value.ParsedValue;
					using (ShortList<ShortList<KeyValuePair<string, string>>>.Enumerator enumerator = shortList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ShortList<KeyValuePair<string, string>> shortList2 = enumerator.Current;
							xmlWriter.WriteStartElement("keyValues");
							foreach (KeyValuePair<string, string> keyValuePair in shortList2)
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

		// Token: 0x0600013F RID: 319 RVA: 0x00005854 File Offset: 0x00003A54
		protected virtual void SaveRuleAttributes(XmlWriter xmlWriter, Rule rule)
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
				xmlWriter.WriteAttributeString("expiryDate", RuleUtils.DateTimeToUtcString(rule.ExpiryDate.Value));
			}
			if (rule.ActivationDate != null)
			{
				xmlWriter.WriteAttributeString("activationDate", RuleUtils.DateTimeToUtcString(rule.ActivationDate.Value));
			}
			if (rule.ImmutableId != Guid.Empty)
			{
				xmlWriter.WriteAttributeString("id", rule.ImmutableId.ToString());
			}
			if (rule.Mode != RuleMode.Enforce)
			{
				xmlWriter.WriteAttributeString("mode", Enum.GetName(typeof(RuleMode), rule.Mode));
			}
			if (rule.SubType != RuleSubType.None)
			{
				xmlWriter.WriteAttributeString("subType", Enum.GetName(typeof(RuleSubType), rule.SubType));
			}
			if (rule.ErrorAction != RuleErrorAction.Ignore)
			{
				xmlWriter.WriteAttributeString("errorAction", Enum.GetName(typeof(RuleErrorAction), rule.ErrorAction));
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000059B4 File Offset: 0x00003BB4
		private void SaveRules(XmlWriter xmlWriter, RuleCollection rules)
		{
			xmlWriter.WriteStartElement("rules");
			xmlWriter.WriteAttributeString("name", rules.Name);
			foreach (Rule rule in rules)
			{
				this.SaveRule(xmlWriter, rule);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005A28 File Offset: 0x00003C28
		private void SaveRule(XmlWriter xmlWriter, Rule rule)
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

		// Token: 0x06000142 RID: 322 RVA: 0x00005AF0 File Offset: 0x00003CF0
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

		// Token: 0x06000143 RID: 323 RVA: 0x00005BD4 File Offset: 0x00003DD4
		private void SaveCondition(XmlWriter xmlWriter, Condition condition)
		{
			xmlWriter.WriteStartElement("condition");
			this.SaveSubCondition(xmlWriter, condition);
			xmlWriter.WriteEndElement();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005BF0 File Offset: 0x00003DF0
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
					goto IL_134;
				}
				break;
			case ConditionType.Or:
				break;
			case ConditionType.Not:
				xmlWriter.WriteStartElement("not");
				this.SaveSubCondition(xmlWriter, ((NotCondition)condition).SubCondition);
				goto IL_134;
			case ConditionType.True:
				xmlWriter.WriteStartElement("true");
				goto IL_134;
			case ConditionType.False:
				xmlWriter.WriteStartElement("false");
				goto IL_134;
			case ConditionType.Predicate:
				goto IL_FF;
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
				goto IL_134;
			}
			IL_FF:
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			xmlWriter.WriteStartElement(predicateCondition.Name);
			this.SaveProperty(xmlWriter, predicateCondition.Property);
			this.SaveValue(xmlWriter, predicateCondition.Value);
			IL_134:
			xmlWriter.WriteEndElement();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005D54 File Offset: 0x00003F54
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

		// Token: 0x06000146 RID: 326 RVA: 0x00005DE0 File Offset: 0x00003FE0
		private void SaveActionArgument(XmlWriter xmlWriter, Argument argument)
		{
			if (!(argument is Value))
			{
				if (argument is Property)
				{
					xmlWriter.WriteStartElement("argument");
					this.SaveProperty(xmlWriter, argument as Property);
					xmlWriter.WriteEndElement();
				}
				return;
			}
			Value value = argument as Value;
			xmlWriter.WriteStartElement("argument");
			if (value.RawValues.Count != 1)
			{
				throw new InvalidOperationException("Action argument can only have one value!");
			}
			xmlWriter.WriteAttributeString("value", value.RawValues[0]);
			xmlWriter.WriteEndElement();
		}
	}
}
