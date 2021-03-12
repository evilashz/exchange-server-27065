using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000036 RID: 54
	internal class TransportRulesCostMonitor
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00009949 File Offset: 0x00007B49
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00009951 File Offset: 0x00007B51
		internal TransportRulesCostMonitor.CostReportingDelegate CostReporter { get; set; }

		// Token: 0x060001D9 RID: 473 RVA: 0x0000995A File Offset: 0x00007B5A
		internal TransportRulesCostMonitor(TransportRulesAgentCostComponents component)
		{
			this.Start(component);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000998B File Offset: 0x00007B8B
		internal void IncrementComponentProcessingCost(TimeSpan processingCost, TransportRulesAgentCostComponents component)
		{
			this.SetComponentProcessingCost(processingCost.Add(this.GetComponentProcessingCost(component)), component);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000099A2 File Offset: 0x00007BA2
		internal void SetComponentProcessingCost(TimeSpan processingCost, TransportRulesAgentCostComponents component)
		{
			this.componentCosts[component] = processingCost;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000099B4 File Offset: 0x00007BB4
		internal TimeSpan GetComponentProcessingCost(TransportRulesAgentCostComponents component)
		{
			TimeSpan result;
			if (!this.componentCosts.TryGetValue(component, out result))
			{
				return TimeSpan.Zero;
			}
			return result;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000099E7 File Offset: 0x00007BE7
		internal TimeSpan GetAggregatedProcessingCost()
		{
			return this.componentCosts.Aggregate(TimeSpan.Zero, (TimeSpan current, KeyValuePair<TransportRulesAgentCostComponents, TimeSpan> componentCost) => current + componentCost.Value);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009A16 File Offset: 0x00007C16
		private void Reset()
		{
			this.wallClockStopwatch.Reset();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009A23 File Offset: 0x00007C23
		internal void Start(TransportRulesAgentCostComponents component)
		{
			if (this.currentComponent != null)
			{
				this.Stop();
			}
			this.currentComponent = new TransportRulesAgentCostComponents?(component);
			this.wallClockStopwatch.Start();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009A50 File Offset: 0x00007C50
		internal void Stop()
		{
			this.wallClockStopwatch.Stop();
			if (this.currentComponent != null)
			{
				this.IncrementComponentProcessingCost(this.wallClockStopwatch.Elapsed, this.currentComponent.Value);
				if (this.CostReporter != null)
				{
					Tuple<TimeSpan, string> aggregatedCostForLogging = this.GetAggregatedCostForLogging();
					this.CostReporter((long)aggregatedCostForLogging.Item1.TotalSeconds, aggregatedCostForLogging.Item2);
				}
			}
			this.Reset();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009AC6 File Offset: 0x00007CC6
		internal void StopAndSetReporter(TransportRulesCostMonitor.CostReportingDelegate costReporter)
		{
			this.Stop();
			this.CostReporter = costReporter;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009AD8 File Offset: 0x00007CD8
		internal Tuple<TimeSpan, string> GetAggregatedCostForLogging()
		{
			StringBuilder stringBuilder = new StringBuilder();
			TimeSpan timeSpan = TimeSpan.Zero;
			foreach (object obj in Enum.GetValues(typeof(TransportRulesAgentCostComponents)))
			{
				TransportRulesAgentCostComponents transportRulesAgentCostComponents = (TransportRulesAgentCostComponents)obj;
				TimeSpan componentProcessingCost = this.GetComponentProcessingCost(transportRulesAgentCostComponents);
				if (componentProcessingCost.CompareTo(TimeSpan.Zero) != 0)
				{
					stringBuilder.Append(string.Format("{0}={1},", transportRulesAgentCostComponents.ToString(), (long)componentProcessingCost.TotalMilliseconds));
					timeSpan += componentProcessingCost;
				}
			}
			string item = string.Empty;
			if (stringBuilder.Length > 0)
			{
				item = stringBuilder.ToString(0, stringBuilder.Length - 1);
			}
			return new Tuple<TimeSpan, string>(timeSpan, item);
		}

		// Token: 0x04000175 RID: 373
		private Stopwatch wallClockStopwatch = new Stopwatch();

		// Token: 0x04000176 RID: 374
		private TransportRulesAgentCostComponents? currentComponent = null;

		// Token: 0x04000177 RID: 375
		private Dictionary<TransportRulesAgentCostComponents, TimeSpan> componentCosts = new Dictionary<TransportRulesAgentCostComponents, TimeSpan>();

		// Token: 0x02000037 RID: 55
		// (Invoke) Token: 0x060001E5 RID: 485
		internal delegate void CostReportingDelegate(long cost, string costInfo);
	}
}
