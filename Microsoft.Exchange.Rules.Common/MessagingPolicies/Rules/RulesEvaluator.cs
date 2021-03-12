using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000035 RID: 53
	public class RulesEvaluator
	{
		// Token: 0x06000166 RID: 358 RVA: 0x000062B4 File Offset: 0x000044B4
		public RulesEvaluator(RulesEvaluationContext context)
		{
			this.context = context;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000062C3 File Offset: 0x000044C3
		public RuleCollection Rules
		{
			get
			{
				return this.Context.Rules;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000062D0 File Offset: 0x000044D0
		public RulesEvaluationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000062D8 File Offset: 0x000044D8
		public void Run()
		{
			bool flag = false;
			this.Trace("Evaluating rule collection '{0}'", new object[]
			{
				this.Rules.Name
			});
			this.EnterRuleCollection();
			if (this.EnterRuleCollection() == ExecutionControl.Execute)
			{
				foreach (Rule rule in this.Rules)
				{
					if (this.ShouldEvaluateRule(rule))
					{
						this.Trace("Evaluating rule '{0}'", new object[]
						{
							rule.Name
						});
						this.Context.CurrentRule = rule;
						this.Context.ShouldExecuteActions = true;
						ExecutionControl executionControl = this.EnterRule();
						if (executionControl == ExecutionControl.SkipThis)
						{
							this.Trace("Skip rule '{0}' after calling EnterRule", new object[]
							{
								rule.Name
							});
						}
						else
						{
							if (executionControl == ExecutionControl.SkipAll)
							{
								this.Trace("Skip rule collection '{0}' after calling EnterRule", new object[]
								{
									this.Rules.Name
								});
								break;
							}
							if (this.EvaluateCondition(rule.Condition, this.Context))
							{
								flag = this.ExecuteActions();
							}
							else if (this.context.NeedsLogging)
							{
								this.context.LogActionExecution("NoAction", "Conditions evaluated to false.  Rule skipped.");
							}
							if (this.ExitRule() == ExecutionControl.SkipAll || flag)
							{
								this.Trace("Skip rule collection '{0}' after calling ExitRule", new object[]
								{
									this.Rules.Name
								});
								break;
							}
						}
					}
				}
			}
			this.Trace("Finished evaluation of rule collection '{0}'", new object[]
			{
				this.Rules.Name
			});
			this.ExitRuleCollection();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000064A4 File Offset: 0x000046A4
		protected virtual bool EvaluateCondition(Condition condition, RulesEvaluationContext evaluationContext)
		{
			this.Context.RulesEvaluationHistory.AddRuleEvaluationResult(this.Context);
			bool flag = condition.Evaluate(evaluationContext);
			this.context.RulesEvaluationHistory.SetCurrentRuleIsMatch(this.Context, flag);
			this.Trace("Condition evaluated as {0}Match", new object[]
			{
				flag ? string.Empty : "Not "
			});
			return flag;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000650C File Offset: 0x0000470C
		private bool ShouldEvaluateRule(Rule rule)
		{
			if (rule.IsTooAdvancedToParse)
			{
				this.Trace("Skip rule which cannot be parsed due to server being too low of a version '{0}'", new object[]
				{
					rule.Name
				});
				return false;
			}
			if (rule.Enabled != RuleState.Enabled)
			{
				this.Trace("Skip disabled rule '{0}'", new object[]
				{
					rule.Name
				});
				return false;
			}
			if (rule.ExpiryDate != null && DateTime.UtcNow > rule.ExpiryDate.Value.ToUniversalTime())
			{
				this.Trace("Skip rule past expiration date '{0}'", new object[]
				{
					rule.Name
				});
				return false;
			}
			if (rule.ActivationDate != null && DateTime.UtcNow < rule.ActivationDate.Value.ToUniversalTime())
			{
				this.Trace("Skip rule that has not reached activation date '{0}'", new object[]
				{
					rule.Name
				});
				return false;
			}
			if (rule.Actions.Count == 0)
			{
				this.Trace("Skip rule without actions '{0}'", new object[]
				{
					rule.Name
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006640 File Offset: 0x00004840
		private bool ExecuteActions()
		{
			Rule currentRule = this.Context.CurrentRule;
			this.Trace("Execute Actions for rule '{0}' ", new object[]
			{
				currentRule.Name
			});
			bool result = false;
			if (this.EnterRuleActionBlock())
			{
				using (ShortList<Action>.Enumerator enumerator = currentRule.Actions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Action action = enumerator.Current;
						if (action.ShouldExecute(currentRule.Mode, this.Context))
						{
							this.Trace("Execute Action '{0}' ", new object[]
							{
								action.Name
							});
							if (this.ExecuteAction(action, this.context) == ExecutionControl.SkipAll)
							{
								this.Trace("Action '{0}' halted rules evaluation", new object[]
								{
									action.Name
								});
								result = true;
							}
						}
						else
						{
							this.Trace("Audit Action '{0}'", new object[]
							{
								action.Name
							});
							this.AuditAction(action, this.context);
						}
						RuleEvaluationResult currentRuleResult = this.Context.RulesEvaluationHistory.GetCurrentRuleResult(this.Context);
						if (currentRuleResult != null)
						{
							currentRuleResult.Actions.Add(action.Name);
						}
					}
					goto IL_14C;
				}
			}
			result = true;
			this.Trace("Actions execution for rule '{0}' skipped by EnterRuleActionBlock result", new object[]
			{
				currentRule.Name
			});
			IL_14C:
			this.Trace("Finished execution of Actions for rule '{0}' ", new object[]
			{
				currentRule.Name
			});
			this.ExitRuleActionBlock();
			return result;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000067D0 File Offset: 0x000049D0
		protected virtual ExecutionControl EnterRuleCollection()
		{
			return ExecutionControl.Execute;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000067D3 File Offset: 0x000049D3
		protected virtual ExecutionControl EnterRule()
		{
			return ExecutionControl.Execute;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000067D6 File Offset: 0x000049D6
		protected virtual ExecutionControl ExecuteAction(Action action, RulesEvaluationContext context)
		{
			return action.Execute(context);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000067DF File Offset: 0x000049DF
		protected virtual void AuditAction(Action action, RulesEvaluationContext context)
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000067E1 File Offset: 0x000049E1
		protected virtual ExecutionControl ExitRule()
		{
			return ExecutionControl.Execute;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000067E4 File Offset: 0x000049E4
		protected virtual bool EnterRuleActionBlock()
		{
			return true;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000067E7 File Offset: 0x000049E7
		protected virtual void ExitRuleActionBlock()
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000067E9 File Offset: 0x000049E9
		protected virtual void ExitRuleCollection()
		{
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000067EB File Offset: 0x000049EB
		private void Trace(string traceMessageFormat, params object[] args)
		{
			this.context.Trace(traceMessageFormat, args);
		}

		// Token: 0x040000A9 RID: 169
		private RulesEvaluationContext context;
	}
}
