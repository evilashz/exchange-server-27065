using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200006B RID: 107
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AdminAuditPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000310 RID: 784 RVA: 0x000116D4 File Offset: 0x0000F8D4
		internal AdminAuditPerfCountersInstance(string instanceName, AdminAuditPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Admin Audit Log")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Admin Audit Log records saved/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Average Admin Audit Record Size", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalAuditEntrySize = new ExPerformanceCounter(base.CategoryName, "Total Admin Audit Records Size", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalAuditEntrySize);
				this.AverageAuditMessageSizeBase = new ExPerformanceCounter(base.CategoryName, "Base of average size of an Admin Audit Log entry", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAuditMessageSizeBase);
				this.ProcessId = new ExPerformanceCounter(base.CategoryName, "Process ID", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessId);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Average time for saving Admin Audit Log records", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TotalAuditSaveTime = new ExPerformanceCounter(base.CategoryName, "Total time for saving Admin Audit Log records", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TotalAuditSaveTime);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Base of Average time for saving Admin Audit Log records", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.TotalAuditSaved = new ExPerformanceCounter(base.CategoryName, "Total Admin Audit Log records saved", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter,
					exPerformanceCounter4
				});
				list.Add(this.TotalAuditSaved);
				long num = this.TotalAuditSaved.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter5 in list)
					{
						exPerformanceCounter5.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00011910 File Offset: 0x0000FB10
		internal AdminAuditPerfCountersInstance(string instanceName) : base(instanceName, "MSExchange Admin Audit Log")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Admin Audit Log records saved/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Average Admin Audit Record Size", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalAuditEntrySize = new ExPerformanceCounter(base.CategoryName, "Total Admin Audit Records Size", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalAuditEntrySize);
				this.AverageAuditMessageSizeBase = new ExPerformanceCounter(base.CategoryName, "Base of average size of an Admin Audit Log entry", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAuditMessageSizeBase);
				this.ProcessId = new ExPerformanceCounter(base.CategoryName, "Process ID", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessId);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Average time for saving Admin Audit Log records", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TotalAuditSaveTime = new ExPerformanceCounter(base.CategoryName, "Total time for saving Admin Audit Log records", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TotalAuditSaveTime);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Base of Average time for saving Admin Audit Log records", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.TotalAuditSaved = new ExPerformanceCounter(base.CategoryName, "Total Admin Audit Log records saved", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter,
					exPerformanceCounter4
				});
				list.Add(this.TotalAuditSaved);
				long num = this.TotalAuditSaved.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter5 in list)
					{
						exPerformanceCounter5.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00011B4C File Offset: 0x0000FD4C
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

		// Token: 0x040001BE RID: 446
		public readonly ExPerformanceCounter TotalAuditSaved;

		// Token: 0x040001BF RID: 447
		public readonly ExPerformanceCounter TotalAuditSaveTime;

		// Token: 0x040001C0 RID: 448
		public readonly ExPerformanceCounter TotalAuditEntrySize;

		// Token: 0x040001C1 RID: 449
		public readonly ExPerformanceCounter AverageAuditMessageSizeBase;

		// Token: 0x040001C2 RID: 450
		public readonly ExPerformanceCounter ProcessId;
	}
}
