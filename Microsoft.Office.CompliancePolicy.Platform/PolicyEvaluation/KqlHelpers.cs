using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000BB RID: 187
	public static class KqlHelpers
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x0000E06C File Offset: 0x0000C26C
		public static string GenerateKqlQuery(QueryPredicate queryPredicate, Dictionary<string, string> workloadPropertyNameMapping)
		{
			if (KqlHelpers.IsEmptyPredicate(queryPredicate))
			{
				return string.Empty;
			}
			Condition subCondition = queryPredicate.SubCondition;
			return KqlHelpers.GenerateKqlQuery(subCondition, workloadPropertyNameMapping);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000E098 File Offset: 0x0000C298
		public static string GenerateHoldDeleteQuery(IList<PolicyRule> rules, Dictionary<string, string> workloadPropertyNameMapping)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PolicyRule rule in rules)
			{
				string text = KqlHelpers.GenerateHoldKeepQuery(rule, workloadPropertyNameMapping);
				if (!string.IsNullOrWhiteSpace(text))
				{
					stringBuilder.Append(string.Format("{0} OR ", text));
				}
			}
			string text2 = stringBuilder.ToString();
			if (!string.IsNullOrWhiteSpace(text2))
			{
				text2 = string.Format("NOT ({0})", text2.Substring(0, text2.Length - 4));
			}
			return text2;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000E13C File Offset: 0x0000C33C
		public static string GenerateHoldKeepQuery(PolicyRule rule, Dictionary<string, string> workloadPropertyNameMapping)
		{
			IEnumerable<Action> source = from a in rule.Actions
			where a is HoldAction
			select a;
			if (source.Count<Action>() > 1)
			{
				throw new CompliancePolicyValidationException("Rule '{0} has more than one Hold action. ", new object[]
				{
					rule.Name
				});
			}
			HoldAction holdAction = (HoldAction)source.FirstOrDefault<Action>();
			if (holdAction == null)
			{
				return string.Empty;
			}
			QueryPredicate queryPredicate = rule.Condition as QueryPredicate;
			if (queryPredicate == null)
			{
				throw new CompliancePolicyValidationException("Rule '{0} has Hold action but contains no Query Predicate. ", new object[]
				{
					rule.Name
				});
			}
			string text = KqlHelpers.GenerateKqlQuery(queryPredicate, workloadPropertyNameMapping);
			if (holdAction.HoldDurationDays == 0)
			{
				return text;
			}
			string text2 = string.Format("{0}>={1}", workloadPropertyNameMapping["Item.CreationAgeInDays"], KqlHelpers.CalculateDateCondition("Item.CreationAgeInDays", holdAction.HoldDurationDays));
			if (string.IsNullOrEmpty(text))
			{
				return text2;
			}
			return string.Format("({0} AND {1})", text2, text);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000E230 File Offset: 0x0000C430
		internal static bool IsEmptyPredicate(QueryPredicate queryPredicate)
		{
			AndCondition andCondition = queryPredicate.SubCondition as AndCondition;
			if (andCondition != null && andCondition.SubConditions.Count == 1)
			{
				TrueCondition trueCondition = andCondition.SubConditions.First<Condition>() as TrueCondition;
				if (trueCondition != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		internal static string GenerateKqlQuery(Condition predicate, Dictionary<string, string> workloadPropertyNameMapping)
		{
			Type type = predicate.GetType();
			string text;
			if (!KqlHelpers.conditionPredicateKqlMapping.TryGetValue(type, out text) && type != typeof(TextQueryPredicate))
			{
				throw new CompliancePolicyValidationException("No KQL operator mapping is available for condition '{0}'", new object[]
				{
					type
				});
			}
			if (type == typeof(AndCondition))
			{
				AndCondition andCondition = predicate as AndCondition;
				return string.Join(text, from condition in andCondition.SubConditions
				select KqlHelpers.GenerateKqlQuery(condition, workloadPropertyNameMapping));
			}
			if (type == typeof(OrCondition))
			{
				OrCondition orCondition = predicate as OrCondition;
				return "(" + string.Join(text, from condition in orCondition.SubConditions
				select KqlHelpers.GenerateKqlQuery(condition, workloadPropertyNameMapping)) + ")";
			}
			if (type == typeof(TrueCondition) || type == typeof(FalseCondition))
			{
				return text;
			}
			if (type == typeof(TextQueryPredicate))
			{
				TextQueryPredicate textQueryPredicate = predicate as TextQueryPredicate;
				return workloadPropertyNameMapping.Aggregate(textQueryPredicate.TextQuery, (string current, KeyValuePair<string, string> mappingItem) => current.Replace(mappingItem.Key, mappingItem.Value));
			}
			if (!(predicate is PredicateCondition))
			{
				throw new CompliancePolicyValidationException("The predicate type '{0}' does not support KQL query", new object[]
				{
					type
				});
			}
			PredicateCondition predicateCondition = predicate as PredicateCondition;
			string arg;
			if (workloadPropertyNameMapping.TryGetValue(predicateCondition.Property.Name, out arg))
			{
				string arg2;
				if (KqlHelpers.calculatableDateProperties.Contains(predicateCondition.Property.Name))
				{
					arg2 = KqlHelpers.CalculateDateCondition(predicateCondition.Property.Name, (int)predicateCondition.Value.ParsedValue);
				}
				else if (predicateCondition.Property.Type == typeof(DateTime))
				{
					DateTime dateTime = ((DateTime)predicateCondition.Value.ParsedValue).ToUniversalTime();
					arg2 = KqlHelpers.FormatDate(dateTime.Year, dateTime.Month, dateTime.Day);
				}
				else
				{
					arg2 = ((predicateCondition.Property.Type == typeof(string)) ? string.Format("\"{0}\"", (string)predicateCondition.Value.ParsedValue) : predicateCondition.Value.ParsedValue.ToString());
				}
				return string.Format("{0}{1}{2}", arg, text, arg2);
			}
			throw new CompliancePolicyValidationException("No workload mapping is provided for common property '{0}'", new object[]
			{
				predicateCondition.Property.Name
			});
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000E578 File Offset: 0x0000C778
		internal static string CalculateDateCondition(string propertyName, int propertyValue)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (propertyName != null)
			{
				if (propertyName == "Item.CreationAgeInYears" || propertyName == "Item.ModificationAgeInYears")
				{
					return KqlHelpers.FormatDate(utcNow.Year - propertyValue, utcNow.Month, utcNow.Day);
				}
				if (propertyName == "Item.CreationAgeInMonths" || propertyName == "Item.ModificationAgeInMonths")
				{
					return KqlHelpers.SubtractMonthsFromDate(propertyValue, utcNow);
				}
				if (propertyName == "Item.CreationAgeInDays" || propertyName == "Item.ModificationAgeInDays")
				{
					return (utcNow - TimeSpan.FromDays((double)propertyValue)).ToString("yyyy-MM-ddT00:00:00Z");
				}
			}
			throw new CompliancePolicyValidationException("Property '{0}' is not recognized as a valid name", new object[]
			{
				propertyName
			});
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000E63C File Offset: 0x0000C83C
		internal static string SubtractMonthsFromDate(int months, DateTime date)
		{
			DateTime dateTime = date.AddMonths(-months);
			return KqlHelpers.FormatDate(dateTime.Year, dateTime.Month, dateTime.Day);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000E66D File Offset: 0x0000C86D
		private static string FormatDate(int year, int month, int day)
		{
			return string.Format("{0}-{1}-{2}T00:00:00Z", year.ToString("D2"), month.ToString("D2"), day.ToString("D2"));
		}

		// Token: 0x040002E5 RID: 741
		private const string DateFormat = "yyyy-MM-ddT00:00:00Z";

		// Token: 0x040002E6 RID: 742
		private const string DoubleDigitFormat = "D2";

		// Token: 0x040002E7 RID: 743
		private static Dictionary<Type, string> conditionPredicateKqlMapping = new Dictionary<Type, string>
		{
			{
				typeof(EqualPredicate),
				"="
			},
			{
				typeof(GreaterThanPredicate),
				">"
			},
			{
				typeof(GreaterThanOrEqualPredicate),
				">="
			},
			{
				typeof(LessThanPredicate),
				"<"
			},
			{
				typeof(LessThanOrEqualPredicate),
				"<="
			},
			{
				typeof(NotEqualPredicate),
				"<>"
			},
			{
				typeof(TrueCondition),
				"Yes"
			},
			{
				typeof(FalseCondition),
				"No"
			},
			{
				typeof(OrCondition),
				" OR "
			},
			{
				typeof(AndCondition),
				" AND "
			}
		};

		// Token: 0x040002E8 RID: 744
		private static HashSet<string> calculatableDateProperties = new HashSet<string>
		{
			"Item.CreationAgeInDays",
			"Item.CreationAgeInMonths",
			"Item.CreationAgeInYears",
			"Item.ModificationAgeInDays",
			"Item.ModificationAgeInMonths",
			"Item.ModificationAgeInYears"
		};
	}
}
