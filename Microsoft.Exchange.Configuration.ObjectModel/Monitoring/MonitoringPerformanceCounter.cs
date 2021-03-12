using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020001F5 RID: 501
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public class MonitoringPerformanceCounter
	{
		// Token: 0x06001193 RID: 4499 RVA: 0x00035DC8 File Offset: 0x00033FC8
		internal MonitoringPerformanceCounter(string performanceObject, string performanceCounter, string performanceInstance, double performanceValue)
		{
			if (string.IsNullOrEmpty(performanceObject))
			{
				throw new ArgumentNullException("performanceObject");
			}
			if (string.IsNullOrEmpty(performanceCounter))
			{
				throw new ArgumentNullException("performanceCounter");
			}
			if (string.IsNullOrEmpty(performanceInstance))
			{
				throw new ArgumentNullException("performanceInstance");
			}
			this.performanceObject = performanceObject;
			this.performanceCounter = performanceCounter;
			this.performanceInstance = performanceInstance;
			this.performanceValue = performanceValue;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00035E31 File Offset: 0x00034031
		public string Object
		{
			get
			{
				return this.performanceObject;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x00035E39 File Offset: 0x00034039
		public string Counter
		{
			get
			{
				return this.performanceCounter;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00035E41 File Offset: 0x00034041
		public string Instance
		{
			get
			{
				return this.performanceInstance;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00035E49 File Offset: 0x00034049
		public double Value
		{
			get
			{
				return this.performanceValue;
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00035E51 File Offset: 0x00034051
		public override string ToString()
		{
			return Strings.MonitoringPerfomanceCounterString(this.Object, this.Counter, this.Instance, this.Value);
		}

		// Token: 0x04000413 RID: 1043
		private string performanceObject;

		// Token: 0x04000414 RID: 1044
		private string performanceCounter;

		// Token: 0x04000415 RID: 1045
		private string performanceInstance;

		// Token: 0x04000416 RID: 1046
		private double performanceValue;
	}
}
