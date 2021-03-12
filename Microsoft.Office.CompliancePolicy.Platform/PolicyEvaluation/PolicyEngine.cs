using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Office.CompliancePolicy.ComplianceData;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000E6 RID: 230
	public sealed class PolicyEngine
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x0001362C File Offset: 0x0001182C
		private PolicyEngine()
		{
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00013634 File Offset: 0x00011834
		public static PolicyEngine Instance
		{
			get
			{
				return PolicyEngine.instance;
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001363B File Offset: 0x0001183B
		public void Execute(PolicyEvaluationContext context, ICollection<PolicyRule> policyRules)
		{
			if (context.ComplianceItemPagedReader != null)
			{
				PolicyEngine.ExecuteOnPagedReader(context, policyRules);
				return;
			}
			PolicyEngine.ExecuteOnSingleItem(context, policyRules);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00013654 File Offset: 0x00011854
		internal static void ExecuteOnPagedReader(PolicyEvaluationContext context, ICollection<PolicyRule> policyRules)
		{
			PolicyEngine.Trace(context, "Evaluating rule collection on a paged reader", new object[0]);
			if (PolicyEngine.EnterRuleCollection(context) == ExecutionControl.Execute)
			{
				foreach (PolicyRule policyRule in policyRules)
				{
					if (PolicyEngine.ShouldEvaluateRule(context, policyRule))
					{
						PolicyEngine.Trace(context, "Evaluating rule '{0}'", new object[]
						{
							policyRule.Name
						});
						context.CurrentRule = policyRule;
						ExecutionControl executionControl = PolicyEngine.EnterRule(policyRule, context);
						if (executionControl == ExecutionControl.SkipThis)
						{
							PolicyEngine.Trace(context, "Skip rule '{0}' after calling EnterRule", new object[]
							{
								policyRule.Name
							});
						}
						else
						{
							if (executionControl == ExecutionControl.SkipAll)
							{
								PolicyEngine.Trace(context, "Skip rule collection after calling EnterRule", new object[0]);
								break;
							}
							bool flag = PolicyEngine.ApplyActionsOnPagedReaderResults(context, policyRule);
							if (PolicyEngine.ExitRule(policyRule, context) == ExecutionControl.SkipAll || flag)
							{
								PolicyEngine.Trace(context, "Skip rule collection after calling ExitRule", new object[0]);
								break;
							}
						}
					}
				}
			}
			PolicyEngine.Trace(context, "Finished rule collection evaluation", new object[0]);
			PolicyEngine.ExitRuleCollection(context);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00013770 File Offset: 0x00011970
		internal static bool ApplyActionsOnPagedReaderResults(PolicyEvaluationContext context, PolicyRule rule)
		{
			QueryPredicate queryPredicate = rule.Condition as QueryPredicate;
			if (queryPredicate == null)
			{
				throw new CompliancePolicyValidationException("The Query based rule outer predicate must be of a QueryBasedPredicate type");
			}
			context.ComplianceItemPagedReader.Condition = queryPredicate;
			IEnumerable<ComplianceItem> nextPage = context.ComplianceItemPagedReader.GetNextPage();
			while (nextPage != null && nextPage.Any<ComplianceItem>())
			{
				foreach (ComplianceItem sourceItem in nextPage)
				{
					context.SourceItem = sourceItem;
					PolicyEngine.ExecuteActions(context, rule);
				}
				nextPage = context.ComplianceItemPagedReader.GetNextPage();
			}
			context.SourceItem = null;
			return false;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00013814 File Offset: 0x00011A14
		internal static void ExecuteOnSingleItem(PolicyEvaluationContext context, ICollection<PolicyRule> policyRules)
		{
			bool flag = false;
			PolicyEngine.Trace(context, "Evaluating rule collection", new object[0]);
			if (PolicyEngine.EnterRuleCollection(context) == ExecutionControl.Execute)
			{
				foreach (PolicyRule policyRule in policyRules)
				{
					if (PolicyEngine.ShouldEvaluateRule(context, policyRule))
					{
						PolicyEngine.Trace(context, "Evaluating rule '{0}'", new object[]
						{
							policyRule.Name
						});
						context.CurrentRule = policyRule;
						if (context.ComplianceItemPagedReader == null)
						{
							context.RulesEvaluationHistory.AddRuleEvaluationResult(context);
						}
						ExecutionControl executionControl = PolicyEngine.EnterRule(policyRule, context);
						if (executionControl == ExecutionControl.SkipThis)
						{
							PolicyEngine.Trace(context, "Skip rule '{0}' after calling EnterRule", new object[]
							{
								policyRule.Name
							});
						}
						else
						{
							if (executionControl == ExecutionControl.SkipAll)
							{
								PolicyEngine.Trace(context, "Skip rule collection after calling EnterRule", new object[0]);
								break;
							}
							if (PolicyEngine.EvaluateCondition(policyRule.Condition, context))
							{
								flag = PolicyEngine.ExecuteActions(context, policyRule);
							}
							if (PolicyEngine.ExitRule(policyRule, context) == ExecutionControl.SkipAll || flag)
							{
								PolicyEngine.Trace(context, "Skip rule collection after calling ExitRule", new object[0]);
								break;
							}
						}
					}
				}
			}
			PolicyEngine.Trace(context, "Finished rule collection evaluation", new object[0]);
			PolicyEngine.ExitRuleCollection(context);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00013950 File Offset: 0x00011B50
		private static bool EvaluateCondition(Condition condition, PolicyEvaluationContext context)
		{
			if (condition == null)
			{
				return true;
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			bool flag = condition.Evaluate(context);
			stopwatch.Stop();
			if (context.ComplianceItemPagedReader == null)
			{
				RuleEvaluationResult currentRuleResult = context.RulesEvaluationHistory.GetCurrentRuleResult(context);
				currentRuleResult.Predicates.Add(new PredicateEvaluationResult(condition.GetType(), flag, new List<string>(), 0, stopwatch.Elapsed));
			}
			return flag;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000139B8 File Offset: 0x00011BB8
		private static bool ShouldEvaluateRule(PolicyEvaluationContext context, PolicyRule rule)
		{
			if (rule.IsTooAdvancedToParse)
			{
				PolicyEngine.Trace(context, "Skip rule which cannot be parsed due to server being too low of a version '{0}'", new object[]
				{
					rule.Name
				});
				return false;
			}
			if (rule.Enabled != RuleState.Enabled)
			{
				PolicyEngine.Trace(context, "Skip disabled rule '{0}'", new object[]
				{
					rule.Name
				});
				return false;
			}
			if (rule.Mode == RuleMode.PendingDeletion)
			{
				PolicyEngine.Trace(context, "Skip deleted rule '{0}'", new object[]
				{
					rule.Name
				});
				return false;
			}
			if (rule.ExpiryDate != null && DateTime.UtcNow > rule.ExpiryDate.Value.ToUniversalTime())
			{
				PolicyEngine.Trace(context, "Skip rule past expiration date '{0}'", new object[]
				{
					rule.Name
				});
				return false;
			}
			if (rule.ActivationDate != null && DateTime.UtcNow < rule.ActivationDate.Value.ToUniversalTime())
			{
				PolicyEngine.Trace(context, "Skip rule that has not reached activation date '{0}'", new object[]
				{
					rule.Name
				});
				return false;
			}
			if (rule.Actions.Count == 0)
			{
				PolicyEngine.Trace(context, "Skip rule without actions '{0}'", new object[]
				{
					rule.Name
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00013B14 File Offset: 0x00011D14
		private static bool ExecuteActions(PolicyEvaluationContext context, PolicyRule rule)
		{
			PolicyEngine.Trace(context, "Execute Actions for rule '{0}' ", new object[]
			{
				rule.Name
			});
			bool result = false;
			if (PolicyEngine.EnterRuleActionBlock(rule, context))
			{
				using (IEnumerator<Action> enumerator = rule.Actions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Action action = enumerator.Current;
						if (action.ShouldExecute(rule.Mode))
						{
							PolicyEngine.Trace(context, "Execute Action '{0}' ", new object[]
							{
								action.Name
							});
							if (PolicyEngine.ExecuteAction(action, context) == ExecutionControl.SkipAll)
							{
								PolicyEngine.Trace(context, "Action '{0}' halted rules evaluation", new object[]
								{
									action.Name
								});
								result = true;
							}
						}
						else
						{
							PolicyEngine.Trace(context, "Audit Action '{0}'", new object[]
							{
								action.Name
							});
							PolicyEngine.AuditAction(action, context);
						}
					}
					goto IL_FA;
				}
			}
			result = true;
			PolicyEngine.Trace(context, "Actions execution for rule '{0}' skipped by EnterRuleActionBlock result", new object[]
			{
				rule.Name
			});
			IL_FA:
			PolicyEngine.Trace(context, "Finished execution of Actions for rule '{0}' ", new object[]
			{
				rule.Name
			});
			PolicyEngine.ExitRuleActionBlock(rule, context);
			return result;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00013C54 File Offset: 0x00011E54
		private static ExecutionControl EnterRuleCollection(PolicyEvaluationContext context)
		{
			return ExecutionControl.Execute;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00013C57 File Offset: 0x00011E57
		private static ExecutionControl EnterRule(PolicyRule rule, PolicyEvaluationContext context)
		{
			return ExecutionControl.Execute;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00013C5C File Offset: 0x00011E5C
		private static ExecutionControl ExecuteAction(Action action, PolicyEvaluationContext context)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			ExecutionControl result = action.Execute(context);
			stopwatch.Stop();
			if (context.ComplianceItemPagedReader == null)
			{
				RuleEvaluationResult currentRuleResult = context.RulesEvaluationHistory.GetCurrentRuleResult(context);
				currentRuleResult.Actions.Add(new PolicyHistoryResult(action.GetType(), new List<string>(), 0, stopwatch.Elapsed));
			}
			return result;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00013CBB File Offset: 0x00011EBB
		private static void AuditAction(Action action, PolicyEvaluationContext context)
		{
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00013CBD File Offset: 0x00011EBD
		private static ExecutionControl ExitRule(PolicyRule rule, PolicyEvaluationContext context)
		{
			return ExecutionControl.Execute;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00013CC0 File Offset: 0x00011EC0
		private static bool EnterRuleActionBlock(PolicyRule rule, PolicyEvaluationContext context)
		{
			return true;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00013CC3 File Offset: 0x00011EC3
		private static void ExitRuleActionBlock(PolicyRule rule, PolicyEvaluationContext context)
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00013CC5 File Offset: 0x00011EC5
		private static void ExitRuleCollection(PolicyEvaluationContext context)
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00013CC7 File Offset: 0x00011EC7
		private static void Trace(PolicyEvaluationContext context, string traceMessageFormat, params object[] args)
		{
			if (context.Tracer != null)
			{
				context.Tracer.TraceDebug(traceMessageFormat, args);
			}
		}

		// Token: 0x040003AD RID: 941
		private static readonly PolicyEngine instance = new PolicyEngine();
	}
}
