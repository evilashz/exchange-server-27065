using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A52 RID: 2642
	internal sealed class MSExchangeUserThrottlingInstance : PerformanceCounterInstance
	{
		// Token: 0x060078AE RID: 30894 RVA: 0x0018FF80 File Offset: 0x0018E180
		internal MSExchangeUserThrottlingInstance(string instanceName, MSExchangeUserThrottlingInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange User Throttling")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.UniqueBudgetsOverBudget = new ExPerformanceCounter(base.CategoryName, "Unique Budgets OverBudget", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.UniqueBudgetsOverBudget);
				this.TotalUniqueBudgets = new ExPerformanceCounter(base.CategoryName, "Total Unique Budgets", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalUniqueBudgets);
				this.DelayedThreads = new ExPerformanceCounter(base.CategoryName, "Delayed Threads", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DelayedThreads);
				this.UsersAtMaxConcurrency = new ExPerformanceCounter(base.CategoryName, "Users At MaxConcurrency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.UsersAtMaxConcurrency);
				this.UsersLockedOut = new ExPerformanceCounter(base.CategoryName, "Users Locked Out", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.UsersLockedOut);
				this.PercentageUsersMicroDelayed = new ExPerformanceCounter(base.CategoryName, "Percentage Users Micro Delayed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageUsersMicroDelayed);
				this.PercentageUsersAtMaximumDelay = new ExPerformanceCounter(base.CategoryName, "Percentage Users At Maximum Delay", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageUsersAtMaximumDelay);
				this.NumberOfUsersAtMaximumDelay = new ExPerformanceCounter(base.CategoryName, "Number Of Users At Maximum Delay", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfUsersAtMaximumDelay);
				this.NumberOfUsersMicroDelayed = new ExPerformanceCounter(base.CategoryName, "Number Of Users Micro Delayed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfUsersMicroDelayed);
				this.BudgetUsageFiveMinuteWindow_99_9 = new ExPerformanceCounter(base.CategoryName, "Budget Usage Five Minute Window 99.9%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageFiveMinuteWindow_99_9);
				this.BudgetUsageFiveMinuteWindow_99 = new ExPerformanceCounter(base.CategoryName, "Budget Usage Five Minute Window 99%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageFiveMinuteWindow_99);
				this.BudgetUsageFiveMinuteWindow_75 = new ExPerformanceCounter(base.CategoryName, "Budget Usage Five Minute Window 75%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageFiveMinuteWindow_75);
				this.AverageBudgetUsageFiveMinuteWindow = new ExPerformanceCounter(base.CategoryName, "Average Budget Usage Five Minute Window", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageBudgetUsageFiveMinuteWindow);
				this.BudgetUsageOneHourWindow_99_9 = new ExPerformanceCounter(base.CategoryName, "Budget Usage One Hour Window 99.9%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageOneHourWindow_99_9);
				this.BudgetUsageOneHourWindow_99 = new ExPerformanceCounter(base.CategoryName, "Budget Usage One Hour Window 99%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageOneHourWindow_99);
				this.BudgetUsageOneHourWindow_75 = new ExPerformanceCounter(base.CategoryName, "Budget Usage One Hour Window 75%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageOneHourWindow_75);
				this.AverageBudgetUsageOneHourWindow = new ExPerformanceCounter(base.CategoryName, "Average Budget Usage One Hour Window", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageBudgetUsageOneHourWindow);
				long num = this.UniqueBudgetsOverBudget.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060078AF RID: 30895 RVA: 0x001902F8 File Offset: 0x0018E4F8
		internal MSExchangeUserThrottlingInstance(string instanceName) : base(instanceName, "MSExchange User Throttling")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.UniqueBudgetsOverBudget = new ExPerformanceCounter(base.CategoryName, "Unique Budgets OverBudget", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.UniqueBudgetsOverBudget);
				this.TotalUniqueBudgets = new ExPerformanceCounter(base.CategoryName, "Total Unique Budgets", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalUniqueBudgets);
				this.DelayedThreads = new ExPerformanceCounter(base.CategoryName, "Delayed Threads", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DelayedThreads);
				this.UsersAtMaxConcurrency = new ExPerformanceCounter(base.CategoryName, "Users At MaxConcurrency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.UsersAtMaxConcurrency);
				this.UsersLockedOut = new ExPerformanceCounter(base.CategoryName, "Users Locked Out", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.UsersLockedOut);
				this.PercentageUsersMicroDelayed = new ExPerformanceCounter(base.CategoryName, "Percentage Users Micro Delayed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageUsersMicroDelayed);
				this.PercentageUsersAtMaximumDelay = new ExPerformanceCounter(base.CategoryName, "Percentage Users At Maximum Delay", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageUsersAtMaximumDelay);
				this.NumberOfUsersAtMaximumDelay = new ExPerformanceCounter(base.CategoryName, "Number Of Users At Maximum Delay", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfUsersAtMaximumDelay);
				this.NumberOfUsersMicroDelayed = new ExPerformanceCounter(base.CategoryName, "Number Of Users Micro Delayed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfUsersMicroDelayed);
				this.BudgetUsageFiveMinuteWindow_99_9 = new ExPerformanceCounter(base.CategoryName, "Budget Usage Five Minute Window 99.9%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageFiveMinuteWindow_99_9);
				this.BudgetUsageFiveMinuteWindow_99 = new ExPerformanceCounter(base.CategoryName, "Budget Usage Five Minute Window 99%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageFiveMinuteWindow_99);
				this.BudgetUsageFiveMinuteWindow_75 = new ExPerformanceCounter(base.CategoryName, "Budget Usage Five Minute Window 75%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageFiveMinuteWindow_75);
				this.AverageBudgetUsageFiveMinuteWindow = new ExPerformanceCounter(base.CategoryName, "Average Budget Usage Five Minute Window", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageBudgetUsageFiveMinuteWindow);
				this.BudgetUsageOneHourWindow_99_9 = new ExPerformanceCounter(base.CategoryName, "Budget Usage One Hour Window 99.9%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageOneHourWindow_99_9);
				this.BudgetUsageOneHourWindow_99 = new ExPerformanceCounter(base.CategoryName, "Budget Usage One Hour Window 99%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageOneHourWindow_99);
				this.BudgetUsageOneHourWindow_75 = new ExPerformanceCounter(base.CategoryName, "Budget Usage One Hour Window 75%", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BudgetUsageOneHourWindow_75);
				this.AverageBudgetUsageOneHourWindow = new ExPerformanceCounter(base.CategoryName, "Average Budget Usage One Hour Window", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageBudgetUsageOneHourWindow);
				long num = this.UniqueBudgetsOverBudget.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060078B0 RID: 30896 RVA: 0x00190670 File Offset: 0x0018E870
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04004F74 RID: 20340
		public readonly ExPerformanceCounter UniqueBudgetsOverBudget;

		// Token: 0x04004F75 RID: 20341
		public readonly ExPerformanceCounter TotalUniqueBudgets;

		// Token: 0x04004F76 RID: 20342
		public readonly ExPerformanceCounter DelayedThreads;

		// Token: 0x04004F77 RID: 20343
		public readonly ExPerformanceCounter UsersAtMaxConcurrency;

		// Token: 0x04004F78 RID: 20344
		public readonly ExPerformanceCounter UsersLockedOut;

		// Token: 0x04004F79 RID: 20345
		public readonly ExPerformanceCounter PercentageUsersMicroDelayed;

		// Token: 0x04004F7A RID: 20346
		public readonly ExPerformanceCounter PercentageUsersAtMaximumDelay;

		// Token: 0x04004F7B RID: 20347
		public readonly ExPerformanceCounter NumberOfUsersAtMaximumDelay;

		// Token: 0x04004F7C RID: 20348
		public readonly ExPerformanceCounter NumberOfUsersMicroDelayed;

		// Token: 0x04004F7D RID: 20349
		public readonly ExPerformanceCounter BudgetUsageFiveMinuteWindow_99_9;

		// Token: 0x04004F7E RID: 20350
		public readonly ExPerformanceCounter BudgetUsageFiveMinuteWindow_99;

		// Token: 0x04004F7F RID: 20351
		public readonly ExPerformanceCounter BudgetUsageFiveMinuteWindow_75;

		// Token: 0x04004F80 RID: 20352
		public readonly ExPerformanceCounter AverageBudgetUsageFiveMinuteWindow;

		// Token: 0x04004F81 RID: 20353
		public readonly ExPerformanceCounter BudgetUsageOneHourWindow_99_9;

		// Token: 0x04004F82 RID: 20354
		public readonly ExPerformanceCounter BudgetUsageOneHourWindow_99;

		// Token: 0x04004F83 RID: 20355
		public readonly ExPerformanceCounter BudgetUsageOneHourWindow_75;

		// Token: 0x04004F84 RID: 20356
		public readonly ExPerformanceCounter AverageBudgetUsageOneHourWindow;
	}
}
