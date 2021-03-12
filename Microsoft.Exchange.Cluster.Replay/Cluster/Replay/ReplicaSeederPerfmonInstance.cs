using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000391 RID: 913
	internal sealed class ReplicaSeederPerfmonInstance : PerformanceCounterInstance
	{
		// Token: 0x06002482 RID: 9346 RVA: 0x000AC4F4 File Offset: 0x000AA6F4
		internal ReplicaSeederPerfmonInstance(string instanceName, ReplicaSeederPerfmonInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Replica Seeder")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DbSeedingProgress = new ExPerformanceCounter(base.CategoryName, "Database Seeding Progress %", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DbSeedingProgress);
				this.DbSeedingBytesRead = new ExPerformanceCounter(base.CategoryName, "Database Seeding Bytes Read (KB)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DbSeedingBytesRead);
				this.DbSeedingBytesReadPerSecond = new ExPerformanceCounter(base.CategoryName, "Database Seeding Bytes Read (KB/sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DbSeedingBytesReadPerSecond);
				this.DbSeedingBytesWritten = new ExPerformanceCounter(base.CategoryName, "Database Seeding Bytes Written (KB)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DbSeedingBytesWritten);
				this.DbSeedingBytesWrittenPerSecond = new ExPerformanceCounter(base.CategoryName, "Database Seeding Bytes Written (KB/sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DbSeedingBytesWrittenPerSecond);
				this.CiSeedingInProgress = new ExPerformanceCounter(base.CategoryName, "Catalog Seeding In Progress", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CiSeedingInProgress);
				this.CiSeedingPercent = new ExPerformanceCounter(base.CategoryName, "Catalog Seeding Progress %", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CiSeedingPercent);
				this.CiSeedingSuccesses = new ExPerformanceCounter(base.CategoryName, "Catalog Seeding Successes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CiSeedingSuccesses);
				this.CiSeedingFailures = new ExPerformanceCounter(base.CategoryName, "Catalog Seeding Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CiSeedingFailures);
				long num = this.DbSeedingProgress.RawValue;
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

		// Token: 0x06002483 RID: 9347 RVA: 0x000AC71C File Offset: 0x000AA91C
		internal ReplicaSeederPerfmonInstance(string instanceName) : base(instanceName, "MSExchange Replica Seeder")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DbSeedingProgress = new ExPerformanceCounter(base.CategoryName, "Database Seeding Progress %", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DbSeedingProgress);
				this.DbSeedingBytesRead = new ExPerformanceCounter(base.CategoryName, "Database Seeding Bytes Read (KB)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DbSeedingBytesRead);
				this.DbSeedingBytesReadPerSecond = new ExPerformanceCounter(base.CategoryName, "Database Seeding Bytes Read (KB/sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DbSeedingBytesReadPerSecond);
				this.DbSeedingBytesWritten = new ExPerformanceCounter(base.CategoryName, "Database Seeding Bytes Written (KB)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DbSeedingBytesWritten);
				this.DbSeedingBytesWrittenPerSecond = new ExPerformanceCounter(base.CategoryName, "Database Seeding Bytes Written (KB/sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DbSeedingBytesWrittenPerSecond);
				this.CiSeedingInProgress = new ExPerformanceCounter(base.CategoryName, "Catalog Seeding In Progress", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CiSeedingInProgress);
				this.CiSeedingPercent = new ExPerformanceCounter(base.CategoryName, "Catalog Seeding Progress %", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CiSeedingPercent);
				this.CiSeedingSuccesses = new ExPerformanceCounter(base.CategoryName, "Catalog Seeding Successes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CiSeedingSuccesses);
				this.CiSeedingFailures = new ExPerformanceCounter(base.CategoryName, "Catalog Seeding Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CiSeedingFailures);
				long num = this.DbSeedingProgress.RawValue;
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

		// Token: 0x06002484 RID: 9348 RVA: 0x000AC944 File Offset: 0x000AAB44
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

		// Token: 0x040010C9 RID: 4297
		public readonly ExPerformanceCounter DbSeedingProgress;

		// Token: 0x040010CA RID: 4298
		public readonly ExPerformanceCounter DbSeedingBytesRead;

		// Token: 0x040010CB RID: 4299
		public readonly ExPerformanceCounter DbSeedingBytesReadPerSecond;

		// Token: 0x040010CC RID: 4300
		public readonly ExPerformanceCounter DbSeedingBytesWritten;

		// Token: 0x040010CD RID: 4301
		public readonly ExPerformanceCounter DbSeedingBytesWrittenPerSecond;

		// Token: 0x040010CE RID: 4302
		public readonly ExPerformanceCounter CiSeedingInProgress;

		// Token: 0x040010CF RID: 4303
		public readonly ExPerformanceCounter CiSeedingPercent;

		// Token: 0x040010D0 RID: 4304
		public readonly ExPerformanceCounter CiSeedingSuccesses;

		// Token: 0x040010D1 RID: 4305
		public readonly ExPerformanceCounter CiSeedingFailures;
	}
}
