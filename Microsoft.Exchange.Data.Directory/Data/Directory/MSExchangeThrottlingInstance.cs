using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A4E RID: 2638
	internal sealed class MSExchangeThrottlingInstance : PerformanceCounterInstance
	{
		// Token: 0x06007892 RID: 30866 RVA: 0x0018F568 File Offset: 0x0018D768
		internal MSExchangeThrottlingInstance(string instanceName, MSExchangeThrottlingInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Throttling")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageThreadSleepTime = new ExPerformanceCounter(base.CategoryName, "Average Thread Sleep Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageThreadSleepTime);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Active PowerShell Runspaces/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.ActivePowerShellRunspaces = new ExPerformanceCounter(base.CategoryName, "Active PowerShell Runspaces", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.ActivePowerShellRunspaces);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Exchange Executing Cmdlets/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.ExchangeExecutingCmdlets = new ExPerformanceCounter(base.CategoryName, "Exchange Executing Cmdlets", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.ExchangeExecutingCmdlets);
				this.OrganizationThrottlingPolicyCacheHitCount = new ExPerformanceCounter(base.CategoryName, "Organization Throttling Policy Cache Hit Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationThrottlingPolicyCacheHitCount);
				this.OrganizationThrottlingPolicyCacheMissCount = new ExPerformanceCounter(base.CategoryName, "Organization Throttling Policy Cache Miss Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationThrottlingPolicyCacheMissCount);
				this.OrganizationThrottlingPolicyCacheLength = new ExPerformanceCounter(base.CategoryName, "Organization Throttling Policy Cache Length", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationThrottlingPolicyCacheLength);
				this.OrganizationThrottlingPolicyCacheLengthPercentage = new ExPerformanceCounter(base.CategoryName, "Organization Throttling Policy Cache Length Percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationThrottlingPolicyCacheLengthPercentage);
				this.ThrottlingPolicyCacheHitCount = new ExPerformanceCounter(base.CategoryName, "Throttling Policy Cache Hit Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottlingPolicyCacheHitCount);
				this.ThrottlingPolicyCacheMissCount = new ExPerformanceCounter(base.CategoryName, "Throttling Policy Cache Miss Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottlingPolicyCacheMissCount);
				this.ThrottlingPolicyCacheLength = new ExPerformanceCounter(base.CategoryName, "Throttling Policy Cache Length", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottlingPolicyCacheLength);
				this.ThrottlingPolicyCacheLengthPercentage = new ExPerformanceCounter(base.CategoryName, "Throttling Policy Cache Length Percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottlingPolicyCacheLengthPercentage);
				long num = this.AverageThreadSleepTime.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter3 in list)
					{
						exPerformanceCounter3.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06007893 RID: 30867 RVA: 0x0018F83C File Offset: 0x0018DA3C
		internal MSExchangeThrottlingInstance(string instanceName) : base(instanceName, "MSExchange Throttling")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageThreadSleepTime = new ExPerformanceCounter(base.CategoryName, "Average Thread Sleep Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageThreadSleepTime);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Active PowerShell Runspaces/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.ActivePowerShellRunspaces = new ExPerformanceCounter(base.CategoryName, "Active PowerShell Runspaces", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.ActivePowerShellRunspaces);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Exchange Executing Cmdlets/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.ExchangeExecutingCmdlets = new ExPerformanceCounter(base.CategoryName, "Exchange Executing Cmdlets", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.ExchangeExecutingCmdlets);
				this.OrganizationThrottlingPolicyCacheHitCount = new ExPerformanceCounter(base.CategoryName, "Organization Throttling Policy Cache Hit Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationThrottlingPolicyCacheHitCount);
				this.OrganizationThrottlingPolicyCacheMissCount = new ExPerformanceCounter(base.CategoryName, "Organization Throttling Policy Cache Miss Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationThrottlingPolicyCacheMissCount);
				this.OrganizationThrottlingPolicyCacheLength = new ExPerformanceCounter(base.CategoryName, "Organization Throttling Policy Cache Length", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationThrottlingPolicyCacheLength);
				this.OrganizationThrottlingPolicyCacheLengthPercentage = new ExPerformanceCounter(base.CategoryName, "Organization Throttling Policy Cache Length Percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationThrottlingPolicyCacheLengthPercentage);
				this.ThrottlingPolicyCacheHitCount = new ExPerformanceCounter(base.CategoryName, "Throttling Policy Cache Hit Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottlingPolicyCacheHitCount);
				this.ThrottlingPolicyCacheMissCount = new ExPerformanceCounter(base.CategoryName, "Throttling Policy Cache Miss Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottlingPolicyCacheMissCount);
				this.ThrottlingPolicyCacheLength = new ExPerformanceCounter(base.CategoryName, "Throttling Policy Cache Length", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottlingPolicyCacheLength);
				this.ThrottlingPolicyCacheLengthPercentage = new ExPerformanceCounter(base.CategoryName, "Throttling Policy Cache Length Percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottlingPolicyCacheLengthPercentage);
				long num = this.AverageThreadSleepTime.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter3 in list)
					{
						exPerformanceCounter3.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06007894 RID: 30868 RVA: 0x0018FB10 File Offset: 0x0018DD10
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

		// Token: 0x04004F63 RID: 20323
		public readonly ExPerformanceCounter AverageThreadSleepTime;

		// Token: 0x04004F64 RID: 20324
		public readonly ExPerformanceCounter ActivePowerShellRunspaces;

		// Token: 0x04004F65 RID: 20325
		public readonly ExPerformanceCounter ExchangeExecutingCmdlets;

		// Token: 0x04004F66 RID: 20326
		public readonly ExPerformanceCounter OrganizationThrottlingPolicyCacheHitCount;

		// Token: 0x04004F67 RID: 20327
		public readonly ExPerformanceCounter OrganizationThrottlingPolicyCacheMissCount;

		// Token: 0x04004F68 RID: 20328
		public readonly ExPerformanceCounter OrganizationThrottlingPolicyCacheLength;

		// Token: 0x04004F69 RID: 20329
		public readonly ExPerformanceCounter OrganizationThrottlingPolicyCacheLengthPercentage;

		// Token: 0x04004F6A RID: 20330
		public readonly ExPerformanceCounter ThrottlingPolicyCacheHitCount;

		// Token: 0x04004F6B RID: 20331
		public readonly ExPerformanceCounter ThrottlingPolicyCacheMissCount;

		// Token: 0x04004F6C RID: 20332
		public readonly ExPerformanceCounter ThrottlingPolicyCacheLength;

		// Token: 0x04004F6D RID: 20333
		public readonly ExPerformanceCounter ThrottlingPolicyCacheLengthPercentage;
	}
}
