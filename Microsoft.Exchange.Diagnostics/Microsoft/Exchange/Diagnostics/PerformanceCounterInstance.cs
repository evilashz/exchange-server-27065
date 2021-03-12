using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000139 RID: 313
	public abstract class PerformanceCounterInstance
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x000227C6 File Offset: 0x000209C6
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000227CE File Offset: 0x000209CE
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000227D6 File Offset: 0x000209D6
		public void Remove()
		{
			this.counters[0].RemoveInstance();
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000227E8 File Offset: 0x000209E8
		public void Reset()
		{
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				exPerformanceCounter.Reset();
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00022814 File Offset: 0x00020A14
		public void Close()
		{
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				exPerformanceCounter.Close();
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00022840 File Offset: 0x00020A40
		public ExPerformanceCounter GetCounterOfName(string counterName)
		{
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				if (exPerformanceCounter.CounterName.Equals(counterName, StringComparison.CurrentCultureIgnoreCase))
				{
					return exPerformanceCounter;
				}
			}
			return null;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002287C File Offset: 0x00020A7C
		public virtual void GetPerfCounterDiagnosticsInfo(XElement element)
		{
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002287E File Offset: 0x00020A7E
		protected PerformanceCounterInstance(string name, string categoryName)
		{
			this.name = name;
			this.categoryName = categoryName;
		}

		// Token: 0x04000617 RID: 1559
		protected ExPerformanceCounter[] counters;

		// Token: 0x04000618 RID: 1560
		private string name;

		// Token: 0x04000619 RID: 1561
		private string categoryName;
	}
}
