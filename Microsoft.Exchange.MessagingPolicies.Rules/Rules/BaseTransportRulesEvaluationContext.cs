using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Filtering;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000009 RID: 9
	internal abstract class BaseTransportRulesEvaluationContext : RulesEvaluationContext
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000037FC File Offset: 0x000019FC
		protected BaseTransportRulesEvaluationContext(RuleCollection rules, ITracer tracer) : base(rules)
		{
			if (tracer == null)
			{
				throw new ArgumentNullException("tracer");
			}
			base.Tracer = tracer;
			this.ActionName = string.Empty;
			this.PredicateName = string.Empty;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000052 RID: 82
		protected abstract FilteringServiceInvokerRequest FilteringServiceInvokerRequest { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000053 RID: 83
		public abstract IStringComparer UserComparer { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000054 RID: 84
		public abstract IStringComparer MembershipChecker { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003830 File Offset: 0x00001A30
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003838 File Offset: 0x00001A38
		public string RuleName { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003841 File Offset: 0x00001A41
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003849 File Offset: 0x00001A49
		public string ActionName { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003852 File Offset: 0x00001A52
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000385A File Offset: 0x00001A5A
		public string PredicateName { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003863 File Offset: 0x00001A63
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000386B File Offset: 0x00001A6B
		public ExecutionStatus ExecutionStatus { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000388C File Offset: 0x00001A8C
		public Dictionary<string, string> MatchedClassifications
		{
			get
			{
				RuleEvaluationResult currentRuleResult = base.RulesEvaluationHistory.GetCurrentRuleResult(this);
				if (currentRuleResult == null)
				{
					return new Dictionary<string, string>();
				}
				IList<PredicateEvaluationResult> predicateEvaluationResult = RuleEvaluationResult.GetPredicateEvaluationResult(typeof(ContainsDataClassificationPredicate), currentRuleResult.Predicates);
				List<string> list = new List<string>();
				foreach (PredicateEvaluationResult predicateEvaluationResult2 in from mcdc in predicateEvaluationResult
				where mcdc.SupplementalInfo == 2
				select mcdc)
				{
					list.AddRange(predicateEvaluationResult2.MatchResults);
				}
				List<string> list2 = new List<string>();
				foreach (PredicateEvaluationResult predicateEvaluationResult3 in from mcdc in predicateEvaluationResult
				where mcdc.SupplementalInfo == 0
				select mcdc)
				{
					list2.AddRange(predicateEvaluationResult3.MatchResults);
				}
				if (list.Count != list2.Count)
				{
					string message = string.Format("Mismatching classification ID and Name collections IDs count: {0} Names count: {1}", list.Count, list2.Count);
					throw new TransportRulePermanentException(message);
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>(list.Count);
				for (int i = 0; i < list2.Count; i++)
				{
					if (!dictionary.ContainsKey(list[i]))
					{
						dictionary.Add(list[i], list2[i]);
					}
				}
				return dictionary;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003A28 File Offset: 0x00001C28
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003A30 File Offset: 0x00001C30
		public bool HaveDataClassificationsBeenRetrieved { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003A3C File Offset: 0x00001C3C
		public IEnumerable<DiscoveredDataClassification> DataClassifications
		{
			get
			{
				if (!this.HaveDataClassificationsBeenRetrieved)
				{
					FilteringResults textExtractionResults = null;
					if (this.ShouldInvokeFips())
					{
						this.dataClassifications = FipsFilteringServiceInvoker.GetDataClassifications(base.Rules, this.FilteringServiceInvokerRequest, base.Tracer, out textExtractionResults);
					}
					this.HaveDataClassificationsBeenRetrieved = true;
					this.OnDataClassificationsRetrieved(textExtractionResults);
				}
				return this.dataClassifications;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003A8E File Offset: 0x00001C8E
		protected virtual void OnDataClassificationsRetrieved(FilteringResults textExtractionResults)
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003A90 File Offset: 0x00001C90
		public virtual bool ShouldInvokeFips()
		{
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003A93 File Offset: 0x00001C93
		public virtual void ResetPerRuleData()
		{
			this.ActionName = string.Empty;
			this.PredicateName = string.Empty;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003AAB File Offset: 0x00001CAB
		internal void SetTestDataClassifications(IEnumerable<DiscoveredDataClassification> classifications)
		{
			this.SetDataClassifications(classifications);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003AB4 File Offset: 0x00001CB4
		internal void SetDataClassifications(IEnumerable<DiscoveredDataClassification> classifications)
		{
			this.dataClassifications = classifications;
			this.HaveDataClassificationsBeenRetrieved = true;
		}

		// Token: 0x04000015 RID: 21
		private IEnumerable<DiscoveredDataClassification> dataClassifications;
	}
}
