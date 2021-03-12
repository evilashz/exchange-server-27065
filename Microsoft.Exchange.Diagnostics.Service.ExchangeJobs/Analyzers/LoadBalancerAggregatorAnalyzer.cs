using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Analyzers
{
	// Token: 0x02000002 RID: 2
	public class LoadBalancerAggregatorAnalyzer : PerfLogAggregatorAnalyzer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public LoadBalancerAggregatorAnalyzer(IJob job) : this(job, "LoadBalancerAggregatorAnalyzer")
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DE File Offset: 0x000002DE
		public LoadBalancerAggregatorAnalyzer(IJob job, string outputFile) : base(job, outputFile)
		{
			this.defaultInformation = MachineInformationSource.MachineInformation.GetCurrent();
			this.devices = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002104 File Offset: 0x00000304
		protected override void OutputDataInternal()
		{
			foreach (KeyValuePair<DiagnosticMeasurement, ValueStatistics> keyValuePair in base.CurrentValues)
			{
				DiagnosticMeasurement key = keyValuePair.Key;
				ValueStatistics value = keyValuePair.Value;
				if (value.SampleCount > 0 && !this.devices.Contains(key.MachineName))
				{
					MachineInformationSource.MachineInformation machineInformation = new MachineInformationSource.MachineInformation(key.MachineName, this.defaultInformation.ForestName, this.defaultInformation.SiteName, "LoadBalancer", this.defaultInformation.MaintenanceStatus, this.defaultInformation.MachineVersion);
					OutputStream outputStream = base.Job.GetOutputStream(this, "MachineInformation");
					string text = DateTimeUtils.Floor(DateTime.UtcNow, TimeSpan.FromMinutes(5.0)).ToString("O");
					outputStream.WriteLine("{0},{1}", new object[]
					{
						text,
						machineInformation.ToString()
					});
					this.devices.Add(key.MachineName);
				}
			}
			base.OutputDataInternal();
		}

		// Token: 0x04000001 RID: 1
		private readonly MachineInformationSource.MachineInformation defaultInformation;

		// Token: 0x04000002 RID: 2
		private readonly HashSet<string> devices;
	}
}
