using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics.Components.Entities;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000091 RID: 145
	public sealed class HighAvailabilityDiscovery : MaintenanceWorkItem
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x00018E2C File Offset: 0x0001702C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!DiscoveryUtils.IsForefrontForOfficeDatacenter())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonTracer, HighAvailabilityDiscovery.traceContext, "[HighAvailabilityDiscovery.DoWork]: This is not a FFO datacenter machine.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\HighAvailability\\HighAvailabilityDiscovery.cs", 44);
				base.Result.StateAttribute1 = "HighAvailabilityDiscovery: This is not a FFO datacenter machine.";
				return;
			}
			HighAvailabilityDiscovery.HighAvailabilityConfig highAvailabilityConfig = HighAvailabilityDiscovery.ReadConfiguration(base.Definition.ExtensionAttributes);
			this.SetupDiskSpaceMonitor(highAvailabilityConfig.DiskSpaceMonitorConfig);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00018E8F File Offset: 0x0001708F
		private static string GetPercentageFreeSpaceCounterName(DriveInfo driveInfo)
		{
			return string.Format("LogicalDisk\\% Free Space\\{0}", driveInfo.Name.Substring(0, 2));
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00018F40 File Offset: 0x00017140
		private static HighAvailabilityDiscovery.HighAvailabilityConfig ReadConfiguration(string extensionAttributes)
		{
			IEnumerable<HighAvailabilityDiscovery.DiskSpaceMonitorConfig> diskSpaceMonitorConfig = XDocument.Parse(extensionAttributes).Descendants("DiskSpaceMonitor").Select(delegate(XElement dsp)
			{
				HighAvailabilityDiscovery.DiskSpaceMonitorConfig diskSpaceMonitorConfig2 = new HighAvailabilityDiscovery.DiskSpaceMonitorConfig();
				diskSpaceMonitorConfig2.Name = (string)dsp.Attribute("Name");
				diskSpaceMonitorConfig2.Enabled = (((bool?)dsp.Attribute("Enabled")) ?? true);
				HighAvailabilityDiscovery.DiskSpaceMonitorConfig diskSpaceMonitorConfig3 = diskSpaceMonitorConfig2;
				double? num = (double?)dsp.Attribute("PercentFreeSpaceThreshold");
				diskSpaceMonitorConfig3.PercentFreeSpaceThreshold = ((num != null) ? num.GetValueOrDefault() : 10.0);
				return diskSpaceMonitorConfig2;
			});
			return new HighAvailabilityDiscovery.HighAvailabilityConfig
			{
				DiskSpaceMonitorConfig = diskSpaceMonitorConfig
			};
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00018FB4 File Offset: 0x000171B4
		private static HighAvailabilityDiscovery.DiskSpaceMonitorConfig GetDiskSpaceConfig(IEnumerable<HighAvailabilityDiscovery.DiskSpaceMonitorConfig> diskSpaceMonitorConfig, DriveInfo drive)
		{
			HighAvailabilityDiscovery.DiskSpaceMonitorConfig diskSpaceMonitorConfig2 = diskSpaceMonitorConfig.FirstOrDefault((HighAvailabilityDiscovery.DiskSpaceMonitorConfig dsc) => string.Equals(dsc.Name, drive.Name, StringComparison.OrdinalIgnoreCase));
			if (diskSpaceMonitorConfig2 == null)
			{
				diskSpaceMonitorConfig2 = new HighAvailabilityDiscovery.DiskSpaceMonitorConfig
				{
					Name = drive.Name,
					Enabled = true,
					PercentFreeSpaceThreshold = 10.0
				};
			}
			return diskSpaceMonitorConfig2;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00019030 File Offset: 0x00017230
		private void SetupDiskSpaceMonitor(IEnumerable<HighAvailabilityDiscovery.DiskSpaceMonitorConfig> diskSpaceMonitorConfig)
		{
			IEnumerable<DriveInfo> enumerable = from dtm in DriveInfo.GetDrives()
			where dtm.DriveType != DriveType.NoRootDirectory && dtm.DriveType != DriveType.Removable
			select dtm;
			foreach (DriveInfo driveInfo in enumerable)
			{
				HighAvailabilityDiscovery.DiskSpaceMonitorConfig diskSpaceConfig = HighAvailabilityDiscovery.GetDiskSpaceConfig(diskSpaceMonitorConfig, driveInfo);
				if (diskSpaceConfig.Enabled)
				{
					string name = string.Format("DiskSpaceMonitor{0}", driveInfo.Name.Substring(0, 1));
					string sampleMask = PerformanceCounterNotificationItem.GenerateResultName(HighAvailabilityDiscovery.GetPercentageFreeSpaceCounterName(driveInfo));
					string name2 = ExchangeComponent.FfoDeployment.Name;
					Component ffoDeployment = ExchangeComponent.FfoDeployment;
					double percentFreeSpaceThreshold = diskSpaceConfig.PercentFreeSpaceThreshold;
					bool enabled = true;
					MonitorDefinition monitorDefinition = OverallConsecutiveSampleValueBelowThresholdMonitor.CreateDefinition(name, sampleMask, name2, ffoDeployment, percentFreeSpaceThreshold, 3, enabled);
					monitorDefinition.TargetResource = driveInfo.ToString();
					base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, HighAvailabilityDiscovery.traceContext);
					double num = diskSpaceConfig.PercentFreeSpaceThreshold / 100.0;
					string text = string.Format("Drive {0} has less than {1:P2} free disk space (~ < {2:F2}GB).", driveInfo.Name, num, (double)driveInfo.TotalSize * num / Math.Pow(1024.0, 3.0));
					ResponderDefinition definition = EscalateResponder.CreateDefinition(string.Format("DiskSpaceResponder{0}", driveInfo.Name.Substring(0, 1)), ExchangeComponent.FfoDeployment.Name, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.TargetResource, ServiceHealthStatus.None, "OPs SE", text, text, true, NotificationServiceClass.Urgent, (int)TimeSpan.FromHours(24.0).TotalSeconds, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
					base.Broker.AddWorkDefinition<ResponderDefinition>(definition, HighAvailabilityDiscovery.traceContext);
				}
			}
		}

		// Token: 0x04000257 RID: 599
		private const double DefaultPercentFreeSpaceThreshold = 10.0;

		// Token: 0x04000258 RID: 600
		private static TracingContext traceContext = new TracingContext();

		// Token: 0x02000092 RID: 146
		private class HighAvailabilityConfig
		{
			// Token: 0x06000400 RID: 1024 RVA: 0x00019204 File Offset: 0x00017404
			public HighAvailabilityConfig()
			{
				this.DiskSpaceMonitorConfig = new List<HighAvailabilityDiscovery.DiskSpaceMonitorConfig>();
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000401 RID: 1025 RVA: 0x00019217 File Offset: 0x00017417
			// (set) Token: 0x06000402 RID: 1026 RVA: 0x0001921F File Offset: 0x0001741F
			public IEnumerable<HighAvailabilityDiscovery.DiskSpaceMonitorConfig> DiskSpaceMonitorConfig { get; set; }
		}

		// Token: 0x02000093 RID: 147
		private class DiskSpaceMonitorConfig
		{
			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000403 RID: 1027 RVA: 0x00019228 File Offset: 0x00017428
			// (set) Token: 0x06000404 RID: 1028 RVA: 0x00019230 File Offset: 0x00017430
			public string Name { get; set; }

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000405 RID: 1029 RVA: 0x00019239 File Offset: 0x00017439
			// (set) Token: 0x06000406 RID: 1030 RVA: 0x00019241 File Offset: 0x00017441
			public bool Enabled { get; set; }

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000407 RID: 1031 RVA: 0x0001924A File Offset: 0x0001744A
			// (set) Token: 0x06000408 RID: 1032 RVA: 0x00019252 File Offset: 0x00017452
			public double PercentFreeSpaceThreshold { get; set; }

			// Token: 0x06000409 RID: 1033 RVA: 0x0001925B File Offset: 0x0001745B
			public override string ToString()
			{
				return string.Format("Name = {0}; Enabled = {1}; PercentFreeSpaceThreshold = {2:P2};", this.Name, this.Enabled, this.PercentFreeSpaceThreshold / 100.0);
			}
		}
	}
}
