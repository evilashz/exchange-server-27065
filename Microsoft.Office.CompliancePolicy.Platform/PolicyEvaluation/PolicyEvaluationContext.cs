using System;
using System.Collections.Generic;
using Microsoft.Office.CompliancePolicy.Classification;
using Microsoft.Office.CompliancePolicy.ComplianceData;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000E5 RID: 229
	public sealed class PolicyEvaluationContext
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x00013395 File Offset: 0x00011595
		private PolicyEvaluationContext()
		{
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001339D File Offset: 0x0001159D
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x000133A5 File Offset: 0x000115A5
		public ComplianceItem SourceItem { get; internal set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x000133AE File Offset: 0x000115AE
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x000133B6 File Offset: 0x000115B6
		public ComplianceItemPagedReader ComplianceItemPagedReader { get; private set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x000133BF File Offset: 0x000115BF
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x000133C7 File Offset: 0x000115C7
		public IClassificationRuleStore ClassificationStore { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x000133D0 File Offset: 0x000115D0
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x000133D8 File Offset: 0x000115D8
		public ExecutionLog ExecutionLog { get; private set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x000133E1 File Offset: 0x000115E1
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x000133E9 File Offset: 0x000115E9
		public Auditor Auditor { get; private set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x000133F2 File Offset: 0x000115F2
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x000133FA File Offset: 0x000115FA
		public ICollection<PolicyRule> Rules { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00013403 File Offset: 0x00011603
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x0001340B File Offset: 0x0001160B
		public ITracer Tracer { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00013414 File Offset: 0x00011614
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x0001341C File Offset: 0x0001161C
		public PolicyRule CurrentRule { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00013425 File Offset: 0x00011625
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x0001342D File Offset: 0x0001162D
		public RulesEvaluationHistory RulesEvaluationHistory { get; set; }

		// Token: 0x06000607 RID: 1543 RVA: 0x00013436 File Offset: 0x00011636
		public static PolicyEvaluationContext Create(ComplianceItem sourceItem)
		{
			return PolicyEvaluationContext.Create(sourceItem, null, null, null);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00013441 File Offset: 0x00011641
		public static PolicyEvaluationContext Create(ComplianceItem sourceItem, string correlationId, ExecutionLog executionLog)
		{
			return PolicyEvaluationContext.Create(sourceItem, correlationId, executionLog, null);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001344C File Offset: 0x0001164C
		public static PolicyEvaluationContext Create(ComplianceItem sourceItem, Auditor auditor)
		{
			return PolicyEvaluationContext.Create(sourceItem, null, null, auditor);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00013458 File Offset: 0x00011658
		public static PolicyEvaluationContext Create(ComplianceItem sourceItem, string correlationId, ExecutionLog executionLog, Auditor auditor)
		{
			if (sourceItem == null)
			{
				throw new ArgumentNullException("sourceItem");
			}
			if (executionLog != null && string.IsNullOrWhiteSpace(correlationId))
			{
				throw new ArgumentException("correlationId must not be empty or null if ExecutionLog is supplied");
			}
			return new PolicyEvaluationContext
			{
				SourceItem = sourceItem,
				ExecutionLog = executionLog,
				Auditor = auditor,
				RulesEvaluationHistory = new RulesEvaluationHistory()
			};
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000134B0 File Offset: 0x000116B0
		public static PolicyEvaluationContext Create(ComplianceItemPagedReader complianceItemPagedReader)
		{
			return PolicyEvaluationContext.Create(complianceItemPagedReader, null, null, null);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000134BB File Offset: 0x000116BB
		public static PolicyEvaluationContext Create(ComplianceItemPagedReader complianceItemPagedReader, string correlationId, ExecutionLog executionLog)
		{
			return PolicyEvaluationContext.Create(complianceItemPagedReader, correlationId, executionLog, null);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000134C6 File Offset: 0x000116C6
		public static PolicyEvaluationContext Create(ComplianceItemPagedReader complianceItemPagedReader, Auditor auditor)
		{
			return PolicyEvaluationContext.Create(complianceItemPagedReader, null, null, auditor);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000134D4 File Offset: 0x000116D4
		public static PolicyEvaluationContext Create(ComplianceItemPagedReader complianceItemPagedReader, string correlationId, ExecutionLog executionLog, Auditor auditor)
		{
			if (complianceItemPagedReader == null)
			{
				throw new ArgumentNullException("complianceItemPagedReader");
			}
			if (executionLog != null && string.IsNullOrWhiteSpace(correlationId))
			{
				throw new ArgumentException("correlationId must not be empty or null if ExecutionLog is supplied");
			}
			return new PolicyEvaluationContext
			{
				ComplianceItemPagedReader = complianceItemPagedReader,
				ExecutionLog = executionLog,
				Auditor = auditor
			};
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00013524 File Offset: 0x00011724
		public void SetConditionEvaluationMode(ConditionEvaluationMode mode)
		{
			foreach (PolicyRule policyRule in this.Rules)
			{
				PolicyEvaluationContext.SetConditionTreeEvaluationMode(policyRule.Condition, mode);
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00013578 File Offset: 0x00011778
		private static void SetConditionTreeEvaluationMode(Condition condition, ConditionEvaluationMode mode)
		{
			condition.EvaluationMode = mode;
			OrCondition orCondition = condition as OrCondition;
			if (orCondition != null)
			{
				using (List<Condition>.Enumerator enumerator = orCondition.SubConditions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Condition condition2 = enumerator.Current;
						PolicyEvaluationContext.SetConditionTreeEvaluationMode(condition2, mode);
					}
					return;
				}
			}
			AndCondition andCondition = condition as AndCondition;
			if (andCondition != null)
			{
				foreach (Condition condition3 in andCondition.SubConditions)
				{
					PolicyEvaluationContext.SetConditionTreeEvaluationMode(condition3, mode);
				}
			}
		}
	}
}
