using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.TextProcessing;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000032 RID: 50
	public abstract class RulesEvaluationContext
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00005E9D File Offset: 0x0000409D
		public RulesEvaluationContext(RuleCollection rules)
		{
			this.rules = rules;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005EC9 File Offset: 0x000040C9
		public RuleCollection Rules
		{
			get
			{
				return this.rules;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005ED1 File Offset: 0x000040D1
		public virtual bool NeedsLogging
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005ED4 File Offset: 0x000040D4
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00005EDC File Offset: 0x000040DC
		public ITracer Tracer { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00005EE5 File Offset: 0x000040E5
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00005EED File Offset: 0x000040ED
		public Rule CurrentRule
		{
			get
			{
				return this.currentRule;
			}
			set
			{
				this.currentRule = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00005EF6 File Offset: 0x000040F6
		public Dictionary<string, TextScanContext> RegexMatcherCache
		{
			get
			{
				return this.regexMatcherCache;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005EFE File Offset: 0x000040FE
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00005F06 File Offset: 0x00004106
		public bool ShouldExecuteActions
		{
			get
			{
				return this.shouldExecuteActions;
			}
			set
			{
				this.shouldExecuteActions = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005F0F File Offset: 0x0000410F
		internal RulesEvaluationHistory RulesEvaluationHistory
		{
			get
			{
				return this.rulesEvaluationHistory;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005F17 File Offset: 0x00004117
		internal void Trace(string traceMessageFormat, params object[] args)
		{
			if (this.Tracer != null)
			{
				this.Tracer.TraceDebug(traceMessageFormat, args);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005F2E File Offset: 0x0000412E
		public void AddTextProcessingContext(string textId, TextScanContext context)
		{
			if (!this.regexMatcherCache.ContainsKey(textId))
			{
				this.regexMatcherCache.Add(textId, context);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005F70 File Offset: 0x00004170
		public void ResetTextProcessingContext(string textId, bool isMultiValue)
		{
			if (!isMultiValue)
			{
				if (this.regexMatcherCache.ContainsKey(textId))
				{
					this.regexMatcherCache.Remove(textId);
				}
				return;
			}
			List<string> list = new List<string>();
			list.AddRange(from cacheItem in this.RegexMatcherCache
			where cacheItem.Key.StartsWith(textId)
			select cacheItem.Key);
			foreach (string key in list)
			{
				if (this.regexMatcherCache.ContainsKey(key))
				{
					this.regexMatcherCache.Remove(key);
				}
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006050 File Offset: 0x00004250
		public virtual void LogActionExecution(string actionName, string details)
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006054 File Offset: 0x00004254
		public void SetConditionEvaluationMode(ConditionEvaluationMode mode)
		{
			foreach (Rule rule in this.Rules)
			{
				RulesEvaluationContext.SetConditionTreeEvaluationMode(rule.Condition, mode);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000060A8 File Offset: 0x000042A8
		private static void SetConditionTreeEvaluationMode(Condition condition, ConditionEvaluationMode mode)
		{
			condition.EvaluationMode = mode;
			OrCondition orCondition = condition as OrCondition;
			if (orCondition != null)
			{
				foreach (Condition condition2 in orCondition.SubConditions)
				{
					RulesEvaluationContext.SetConditionTreeEvaluationMode(condition2, mode);
				}
			}
			AndCondition andCondition = condition as AndCondition;
			if (andCondition != null)
			{
				foreach (Condition condition3 in andCondition.SubConditions)
				{
					RulesEvaluationContext.SetConditionTreeEvaluationMode(condition3, mode);
				}
			}
		}

		// Token: 0x0400009D RID: 157
		private RuleCollection rules;

		// Token: 0x0400009E RID: 158
		private Rule currentRule;

		// Token: 0x0400009F RID: 159
		private Dictionary<string, TextScanContext> regexMatcherCache = new Dictionary<string, TextScanContext>();

		// Token: 0x040000A0 RID: 160
		private RulesEvaluationHistory rulesEvaluationHistory = new RulesEvaluationHistory();

		// Token: 0x040000A1 RID: 161
		private bool shouldExecuteActions = true;
	}
}
