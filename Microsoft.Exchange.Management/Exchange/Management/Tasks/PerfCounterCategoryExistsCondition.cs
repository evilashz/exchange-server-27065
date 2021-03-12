using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000621 RID: 1569
	[Serializable]
	internal sealed class PerfCounterCategoryExistsCondition : Condition
	{
		// Token: 0x0600379D RID: 14237 RVA: 0x000E6B04 File Offset: 0x000E4D04
		public PerfCounterCategoryExistsCondition(string categoryName)
		{
			this.categoryName = categoryName;
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000E6B14 File Offset: 0x000E4D14
		public override bool Verify()
		{
			TaskLogger.LogEnter();
			bool flag = false;
			int num = 0;
			try
			{
				IL_09:
				Exception ex = null;
				try
				{
					flag = PerformanceCounterCategory.Exists(this.CategoryName);
				}
				catch (OverflowException ex2)
				{
					ex = ex2;
				}
				catch (FormatException ex3)
				{
					ex = ex3;
				}
				catch (InvalidOperationException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					TaskLogger.Trace(new LocalizedString(string.Format("PerformanceCounterCategory.Exists(\"{0}\") thrown exception {1}", this.CategoryName, ex)));
					throw new CorruptedPerformanceCountersException(ex);
				}
				TaskLogger.Trace(new LocalizedString(string.Format("PerformanceCounterCategory.Exists(\"{0}\") returned {1}", this.CategoryName, flag)));
			}
			catch (Win32Exception ex5)
			{
				if (ex5.NativeErrorCode == 21 && num < 10)
				{
					TaskLogger.Trace(new LocalizedString("Got \"Device is not ready exception\"; will sleep and retry"));
					PerformanceCounter.CloseSharedResources();
					Thread.Sleep(1000);
					num++;
					goto IL_09;
				}
				throw;
			}
			TaskLogger.LogExit();
			return flag;
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000E6C0C File Offset: 0x000E4E0C
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
		}

		// Token: 0x040025A5 RID: 9637
		private readonly string categoryName;
	}
}
