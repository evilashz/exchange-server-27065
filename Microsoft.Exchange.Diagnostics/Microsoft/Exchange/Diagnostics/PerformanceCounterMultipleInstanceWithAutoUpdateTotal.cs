using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200014D RID: 333
	public sealed class PerformanceCounterMultipleInstanceWithAutoUpdateTotal : PerformanceCounterMultipleInstance
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x00023974 File Offset: 0x00021B74
		public PerformanceCounterMultipleInstanceWithAutoUpdateTotal(string categoryName, CreateInstanceDelegate instanceCreator) : base(categoryName, instanceCreator)
		{
			this.totalInstance = instanceCreator("_Total", null);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00023990 File Offset: 0x00021B90
		public PerformanceCounterMultipleInstanceWithAutoUpdateTotal(string categoryName, CreateInstanceDelegate instanceCreator, CreateTotalInstanceDelegate totalInstanceCreator) : base(categoryName, instanceCreator)
		{
			this.totalInstance = totalInstanceCreator("_Total");
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x000239AB File Offset: 0x00021BAB
		public PerformanceCounterInstance TotalInstance
		{
			get
			{
				return this.totalInstance;
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000239B3 File Offset: 0x00021BB3
		public override PerformanceCounterInstance GetInstance(string instanceName)
		{
			if (PerformanceCounterMultipleInstanceWithAutoUpdateTotal.IsTotalInstanceName(instanceName))
			{
				return this.totalInstance;
			}
			return base.GetInstance(instanceName, this.totalInstance);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000239D1 File Offset: 0x00021BD1
		public override void CloseInstance(string instanceName)
		{
			if (PerformanceCounterMultipleInstanceWithAutoUpdateTotal.IsTotalInstanceName(instanceName))
			{
				throw new ArgumentException("You cannot close the TotalInstance", "instanceName");
			}
			base.CloseInstance(instanceName);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x000239F2 File Offset: 0x00021BF2
		public override void RemoveInstance(string instanceName)
		{
			if (PerformanceCounterMultipleInstanceWithAutoUpdateTotal.IsTotalInstanceName(instanceName))
			{
				throw new ArgumentException("You cannot remove the TotalInstance", "instanceName");
			}
			base.RemoveInstance(instanceName);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00023A13 File Offset: 0x00021C13
		public override void ResetInstance(string instanceName)
		{
			if (PerformanceCounterMultipleInstanceWithAutoUpdateTotal.IsTotalInstanceName(instanceName))
			{
				throw new ArgumentException("You cannot reset the TotalInstance", "instanceName");
			}
			base.ResetInstance(instanceName);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00023A34 File Offset: 0x00021C34
		public override void RemoveAllInstances()
		{
			lock (this)
			{
				base.RemoveAllInstancesInternal();
				this.totalInstance.Close();
				this.totalInstance = base.InstanceCreator("_Total", null);
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00023A94 File Offset: 0x00021C94
		private static bool IsTotalInstanceName(string instanceName)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(instanceName, "_Total");
		}

		// Token: 0x0400066A RID: 1642
		private const string TotalInstanceName = "_Total";

		// Token: 0x0400066B RID: 1643
		private PerformanceCounterInstance totalInstance;
	}
}
