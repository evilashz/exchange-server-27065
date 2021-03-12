using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200003C RID: 60
	internal sealed class EhfPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000291 RID: 657 RVA: 0x00011858 File Offset: 0x0000FA58
		internal EhfPerfCountersInstance(string instanceName, EhfPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeEdgeSync EHF Sync Operations")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.PermanentEntryFailuresTotal = new ExPerformanceCounter(base.CategoryName, "Permanent Entry Failures Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PermanentEntryFailuresTotal, new ExPerformanceCounter[0]);
				list.Add(this.PermanentEntryFailuresTotal);
				this.TransientEntryFailuresTotal = new ExPerformanceCounter(base.CategoryName, "Transient Entry Failures Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransientEntryFailuresTotal, new ExPerformanceCounter[0]);
				list.Add(this.TransientEntryFailuresTotal);
				this.EntryCountTotal = new ExPerformanceCounter(base.CategoryName, "Total Count of Entries for All Operations", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EntryCountTotal, new ExPerformanceCounter[0]);
				list.Add(this.EntryCountTotal);
				this.LastEntryCount = new ExPerformanceCounter(base.CategoryName, "Count of Entries in the Last Operation", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LastEntryCount, new ExPerformanceCounter[0]);
				list.Add(this.LastEntryCount);
				this.LastLatency = new ExPerformanceCounter(base.CategoryName, "Latency (msec) for the Last Operation Executed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LastLatency, new ExPerformanceCounter[0]);
				list.Add(this.LastLatency);
				this.AverageLatency = new ExPerformanceCounter(base.CategoryName, "Average Latency (msec) for the Operation", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageLatency, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatency);
				this.AverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "Average Latency Base (msec) for the Operation", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatencyBase);
				this.LastLatencyPerEntry = new ExPerformanceCounter(base.CategoryName, "Latency (msec) Averaged Out for the Entries in the Last Operation Executed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LastLatencyPerEntry, new ExPerformanceCounter[0]);
				list.Add(this.LastLatencyPerEntry);
				this.AverageLatencyPerEntry = new ExPerformanceCounter(base.CategoryName, "Average Latency Per Entry (msec) for the Operation", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageLatencyPerEntry, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatencyPerEntry);
				this.AverageLatencyPerEntryBase = new ExPerformanceCounter(base.CategoryName, "Average Latency Per Entry Base (msec) for the Operation", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageLatencyPerEntryBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatencyPerEntryBase);
				this.OperationsTotal = new ExPerformanceCounter(base.CategoryName, "Operations Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OperationsTotal, new ExPerformanceCounter[0]);
				list.Add(this.OperationsTotal);
				this.SuccessfulOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Successful Operations Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SuccessfulOperationsTotal, new ExPerformanceCounter[0]);
				list.Add(this.SuccessfulOperationsTotal);
				this.TransientFailedOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Transient Failed Operations Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TransientFailedOperationsTotal, new ExPerformanceCounter[0]);
				list.Add(this.TransientFailedOperationsTotal);
				this.TimeoutFailedOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Timeout Failed Operations Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TimeoutFailedOperationsTotal, new ExPerformanceCounter[0]);
				list.Add(this.TimeoutFailedOperationsTotal);
				this.CommunicationFailedOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Communication Failed Operations Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CommunicationFailedOperationsTotal, new ExPerformanceCounter[0]);
				list.Add(this.CommunicationFailedOperationsTotal);
				this.ContractViolationFailedOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Contract Violation Failed Operations Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ContractViolationFailedOperationsTotal, new ExPerformanceCounter[0]);
				list.Add(this.ContractViolationFailedOperationsTotal);
				this.InvalidCredentialsFailedOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Invalid Credentials Failed Operations Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.InvalidCredentialsFailedOperationsTotal, new ExPerformanceCounter[0]);
				list.Add(this.InvalidCredentialsFailedOperationsTotal);
				long num = this.PermanentEntryFailuresTotal.RawValue;
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

		// Token: 0x06000292 RID: 658 RVA: 0x00011C9C File Offset: 0x0000FE9C
		internal EhfPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeEdgeSync EHF Sync Operations")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.PermanentEntryFailuresTotal = new ExPerformanceCounter(base.CategoryName, "Permanent Entry Failures Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PermanentEntryFailuresTotal);
				this.TransientEntryFailuresTotal = new ExPerformanceCounter(base.CategoryName, "Transient Entry Failures Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransientEntryFailuresTotal);
				this.EntryCountTotal = new ExPerformanceCounter(base.CategoryName, "Total Count of Entries for All Operations", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EntryCountTotal);
				this.LastEntryCount = new ExPerformanceCounter(base.CategoryName, "Count of Entries in the Last Operation", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LastEntryCount);
				this.LastLatency = new ExPerformanceCounter(base.CategoryName, "Latency (msec) for the Last Operation Executed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LastLatency);
				this.AverageLatency = new ExPerformanceCounter(base.CategoryName, "Average Latency (msec) for the Operation", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatency);
				this.AverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "Average Latency Base (msec) for the Operation", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatencyBase);
				this.LastLatencyPerEntry = new ExPerformanceCounter(base.CategoryName, "Latency (msec) Averaged Out for the Entries in the Last Operation Executed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LastLatencyPerEntry);
				this.AverageLatencyPerEntry = new ExPerformanceCounter(base.CategoryName, "Average Latency Per Entry (msec) for the Operation", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatencyPerEntry);
				this.AverageLatencyPerEntryBase = new ExPerformanceCounter(base.CategoryName, "Average Latency Per Entry Base (msec) for the Operation", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatencyPerEntryBase);
				this.OperationsTotal = new ExPerformanceCounter(base.CategoryName, "Operations Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OperationsTotal);
				this.SuccessfulOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Successful Operations Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SuccessfulOperationsTotal);
				this.TransientFailedOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Transient Failed Operations Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TransientFailedOperationsTotal);
				this.TimeoutFailedOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Timeout Failed Operations Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeoutFailedOperationsTotal);
				this.CommunicationFailedOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Communication Failed Operations Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CommunicationFailedOperationsTotal);
				this.ContractViolationFailedOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Contract Violation Failed Operations Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ContractViolationFailedOperationsTotal);
				this.InvalidCredentialsFailedOperationsTotal = new ExPerformanceCounter(base.CategoryName, "Invalid Credentials Failed Operations Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InvalidCredentialsFailedOperationsTotal);
				long num = this.PermanentEntryFailuresTotal.RawValue;
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

		// Token: 0x06000293 RID: 659 RVA: 0x00012024 File Offset: 0x00010224
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

		// Token: 0x0400011B RID: 283
		public readonly ExPerformanceCounter PermanentEntryFailuresTotal;

		// Token: 0x0400011C RID: 284
		public readonly ExPerformanceCounter TransientEntryFailuresTotal;

		// Token: 0x0400011D RID: 285
		public readonly ExPerformanceCounter EntryCountTotal;

		// Token: 0x0400011E RID: 286
		public readonly ExPerformanceCounter LastEntryCount;

		// Token: 0x0400011F RID: 287
		public readonly ExPerformanceCounter LastLatency;

		// Token: 0x04000120 RID: 288
		public readonly ExPerformanceCounter AverageLatency;

		// Token: 0x04000121 RID: 289
		public readonly ExPerformanceCounter AverageLatencyBase;

		// Token: 0x04000122 RID: 290
		public readonly ExPerformanceCounter LastLatencyPerEntry;

		// Token: 0x04000123 RID: 291
		public readonly ExPerformanceCounter AverageLatencyPerEntry;

		// Token: 0x04000124 RID: 292
		public readonly ExPerformanceCounter AverageLatencyPerEntryBase;

		// Token: 0x04000125 RID: 293
		public readonly ExPerformanceCounter OperationsTotal;

		// Token: 0x04000126 RID: 294
		public readonly ExPerformanceCounter SuccessfulOperationsTotal;

		// Token: 0x04000127 RID: 295
		public readonly ExPerformanceCounter TransientFailedOperationsTotal;

		// Token: 0x04000128 RID: 296
		public readonly ExPerformanceCounter TimeoutFailedOperationsTotal;

		// Token: 0x04000129 RID: 297
		public readonly ExPerformanceCounter CommunicationFailedOperationsTotal;

		// Token: 0x0400012A RID: 298
		public readonly ExPerformanceCounter ContractViolationFailedOperationsTotal;

		// Token: 0x0400012B RID: 299
		public readonly ExPerformanceCounter InvalidCredentialsFailedOperationsTotal;
	}
}
