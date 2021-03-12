using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.IsMemberOfProvider
{
	// Token: 0x02000A48 RID: 2632
	internal sealed class IsMemberOfResolverPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06007868 RID: 30824 RVA: 0x0018E848 File Offset: 0x0018CA48
		internal IsMemberOfResolverPerfCountersInstance(string instanceName, IsMemberOfResolverPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "Expanded Groups Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ResolvedGroupsHitCount = new ExPerformanceCounter(base.CategoryName, "Resolved Groups Cache Hit Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResolvedGroupsHitCount);
				this.ResolvedGroupsMissCount = new ExPerformanceCounter(base.CategoryName, "Resolved Groups Cache Miss Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResolvedGroupsMissCount);
				this.ResolvedGroupsCacheSize = new ExPerformanceCounter(base.CategoryName, "Resolved Groups Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResolvedGroupsCacheSize);
				this.ResolvedGroupsCacheSizePercentage = new ExPerformanceCounter(base.CategoryName, "Resolved Groups Cache Size Percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResolvedGroupsCacheSizePercentage);
				this.ExpandedGroupsHitCount = new ExPerformanceCounter(base.CategoryName, "Expanded Groups Cache Hit Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpandedGroupsHitCount);
				this.ExpandedGroupsMissCount = new ExPerformanceCounter(base.CategoryName, "Expanded Groups Cache Miss Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpandedGroupsMissCount);
				this.ExpandedGroupsCacheSize = new ExPerformanceCounter(base.CategoryName, "Expanded Groups Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpandedGroupsCacheSize);
				this.ExpandedGroupsCacheSizePercentage = new ExPerformanceCounter(base.CategoryName, "Expanded Groups Cache Size Percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpandedGroupsCacheSizePercentage);
				this.LdapQueries = new ExPerformanceCounter(base.CategoryName, "LDAP Queries Issued by Expanded Groups.", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LdapQueries);
				long num = this.ResolvedGroupsHitCount.RawValue;
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

		// Token: 0x06007869 RID: 30825 RVA: 0x0018EA70 File Offset: 0x0018CC70
		internal IsMemberOfResolverPerfCountersInstance(string instanceName) : base(instanceName, "Expanded Groups Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ResolvedGroupsHitCount = new ExPerformanceCounter(base.CategoryName, "Resolved Groups Cache Hit Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResolvedGroupsHitCount);
				this.ResolvedGroupsMissCount = new ExPerformanceCounter(base.CategoryName, "Resolved Groups Cache Miss Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResolvedGroupsMissCount);
				this.ResolvedGroupsCacheSize = new ExPerformanceCounter(base.CategoryName, "Resolved Groups Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResolvedGroupsCacheSize);
				this.ResolvedGroupsCacheSizePercentage = new ExPerformanceCounter(base.CategoryName, "Resolved Groups Cache Size Percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResolvedGroupsCacheSizePercentage);
				this.ExpandedGroupsHitCount = new ExPerformanceCounter(base.CategoryName, "Expanded Groups Cache Hit Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpandedGroupsHitCount);
				this.ExpandedGroupsMissCount = new ExPerformanceCounter(base.CategoryName, "Expanded Groups Cache Miss Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpandedGroupsMissCount);
				this.ExpandedGroupsCacheSize = new ExPerformanceCounter(base.CategoryName, "Expanded Groups Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpandedGroupsCacheSize);
				this.ExpandedGroupsCacheSizePercentage = new ExPerformanceCounter(base.CategoryName, "Expanded Groups Cache Size Percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpandedGroupsCacheSizePercentage);
				this.LdapQueries = new ExPerformanceCounter(base.CategoryName, "LDAP Queries Issued by Expanded Groups.", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LdapQueries);
				long num = this.ResolvedGroupsHitCount.RawValue;
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

		// Token: 0x0600786A RID: 30826 RVA: 0x0018EC98 File Offset: 0x0018CE98
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

		// Token: 0x04004F4D RID: 20301
		public readonly ExPerformanceCounter ResolvedGroupsHitCount;

		// Token: 0x04004F4E RID: 20302
		public readonly ExPerformanceCounter ResolvedGroupsMissCount;

		// Token: 0x04004F4F RID: 20303
		public readonly ExPerformanceCounter ResolvedGroupsCacheSize;

		// Token: 0x04004F50 RID: 20304
		public readonly ExPerformanceCounter ResolvedGroupsCacheSizePercentage;

		// Token: 0x04004F51 RID: 20305
		public readonly ExPerformanceCounter ExpandedGroupsHitCount;

		// Token: 0x04004F52 RID: 20306
		public readonly ExPerformanceCounter ExpandedGroupsMissCount;

		// Token: 0x04004F53 RID: 20307
		public readonly ExPerformanceCounter ExpandedGroupsCacheSize;

		// Token: 0x04004F54 RID: 20308
		public readonly ExPerformanceCounter ExpandedGroupsCacheSizePercentage;

		// Token: 0x04004F55 RID: 20309
		public readonly ExPerformanceCounter LdapQueries;
	}
}
