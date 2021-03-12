using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Exchange.DxStore.HA.Events;

namespace Microsoft.Exchange.DxStore.HA
{
	// Token: 0x0200016C RID: 364
	public class DataStoreSnapshotAnalyzer
	{
		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003EA7B File Offset: 0x0003CC7B
		public DataStoreSnapshotAnalyzer(DiffReportVerboseMode verboseMode)
		{
			this.Container = new DataStoreMergedContainer(verboseMode);
			this.AnalysisPhase = "None";
			this.TimingInfo = new Dictionary<string, int>();
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0003EAA5 File Offset: 0x0003CCA5
		// (set) Token: 0x06000EA3 RID: 3747 RVA: 0x0003EAAD File Offset: 0x0003CCAD
		public DataStoreMergedContainer Container { get; set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0003EAB6 File Offset: 0x0003CCB6
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x0003EABE File Offset: 0x0003CCBE
		public XElement ClusdbSnapshot { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0003EAC7 File Offset: 0x0003CCC7
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x0003EACF File Offset: 0x0003CCCF
		public XElement DxStoreSnapshot { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0003EAD8 File Offset: 0x0003CCD8
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x0003EAE0 File Offset: 0x0003CCE0
		public string AnalysisPhase { get; private set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0003EAE9 File Offset: 0x0003CCE9
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x0003EAF1 File Offset: 0x0003CCF1
		public Dictionary<string, int> TimingInfo { get; private set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0003EAFA File Offset: 0x0003CCFA
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x0003EB02 File Offset: 0x0003CD02
		public InstanceClientFactory ClientFactory { get; set; }

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003EB0B File Offset: 0x0003CD0B
		public void InitializeIfRequired()
		{
			if (this.ClientFactory == null)
			{
				this.ClientFactory = this.GetDefaultGroupInstanceClientFactory();
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003EB6C File Offset: 0x0003CD6C
		public void AnalyzeDataStores()
		{
			this.InitializeIfRequired();
			this.AnalysisPhase = "Collecting clusdb snapshot";
			this.MeasureDuration(this.AnalysisPhase, delegate
			{
				this.ClusdbSnapshot = this.GetClusdbSnapshot();
			});
			this.AnalysisPhase = "Merging clusdb snapshot with container";
			this.MeasureDuration(this.AnalysisPhase, delegate
			{
				this.Apply(true, this.ClusdbSnapshot, null);
			});
			this.AnalysisPhase = "Collecting dxstore snapshot";
			this.MeasureDuration(this.AnalysisPhase, delegate
			{
				this.DxStoreSnapshot = this.GetDxStoreSnapshot();
			});
			this.AnalysisPhase = "Merging dxstore snapshot with container";
			this.MeasureDuration(this.AnalysisPhase, delegate
			{
				this.Apply(false, this.DxStoreSnapshot, null);
			});
			this.AnalysisPhase = "Creating diff report";
			this.MeasureDuration(this.AnalysisPhase, delegate
			{
				this.Container.Analyze();
			});
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003EC4D File Offset: 0x0003CE4D
		public string GetTimingInfoAsString()
		{
			return string.Join(";", from ti in this.TimingInfo
			select string.Format("{0} = {1}ms", ti.Key, ti.Value));
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003EC84 File Offset: 0x0003CE84
		public XElement GetClusdbSnapshot()
		{
			string[] filterRootKeys = new string[]
			{
				"Exchange",
				"ExchangeActiveManager",
				"MsExchangeIs"
			};
			ClusdbSnapshotMaker clusdbSnapshotMaker = new ClusdbSnapshotMaker(filterRootKeys, null, false);
			return clusdbSnapshotMaker.GetXElementSnapshot(null);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003ECC4 File Offset: 0x0003CEC4
		public InstanceClientFactory GetDefaultGroupInstanceClientFactory()
		{
			DistributedStoreEventLogger eventLogger = new DistributedStoreEventLogger(false);
			DistributedStoreTopologyProvider distributedStoreTopologyProvider = new DistributedStoreTopologyProvider(eventLogger, null, false);
			InstanceGroupConfig groupConfig = distributedStoreTopologyProvider.GetGroupConfig(null, true);
			if (groupConfig != null)
			{
				return new InstanceClientFactory(groupConfig, null);
			}
			return null;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003ECF8 File Offset: 0x0003CEF8
		public XElement GetDxStoreSnapshot()
		{
			DxStoreInstanceClient localClient = this.ClientFactory.LocalClient;
			InstanceSnapshotInfo instanceSnapshotInfo = localClient.AcquireSnapshot("Public", true, null);
			instanceSnapshotInfo.Decompress();
			return XElement.Parse(instanceSnapshotInfo.Snapshot);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003ED7C File Offset: 0x0003CF7C
		public bool IsPaxosConfiguredAndLeaderExist(out string statusInfoStr)
		{
			statusInfoStr = "<null>";
			this.InitializeIfRequired();
			InstanceStatusInfo si = null;
			Utils.RunBestEffort(delegate
			{
				si = this.ClientFactory.LocalClient.GetStatus(null);
			});
			if (si != null)
			{
				statusInfoStr = Utils.SerializeObjectToJsonString<InstanceStatusInfo>(si, false, true);
			}
			return si != null && si.IsValidLeaderExist();
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003EDE8 File Offset: 0x0003CFE8
		public void Apply(bool isClusdb, XElement element, string fullKeyName = null)
		{
			if (string.IsNullOrEmpty(fullKeyName))
			{
				fullKeyName = element.Attribute("Name").Value;
			}
			DataStoreMergedContainer.KeyEntry keyEntry = this.Container.AddOrUpdateKey(fullKeyName, isClusdb);
			if (element.HasElements)
			{
				List<XElement> list = new List<XElement>();
				foreach (XElement xelement in element.Elements())
				{
					string localName = xelement.Name.LocalName;
					if (Utils.IsEqual(localName, "Key", StringComparison.OrdinalIgnoreCase))
					{
						list.Add(xelement);
					}
					else if (Utils.IsEqual(localName, "Value", StringComparison.OrdinalIgnoreCase))
					{
						string value = xelement.Attribute("Name").Value;
						string value2 = xelement.Attribute("Kind").Value;
						string value3 = xelement.Value;
						keyEntry.AddOrUpdateProperty(value, value3, value2, isClusdb);
					}
				}
				if (list.Count > 0)
				{
					foreach (XElement xelement2 in list)
					{
						string value4 = xelement2.Attribute("Name").Value;
						string fullKeyName2 = Path.Combine(fullKeyName, value4);
						this.Apply(isClusdb, xelement2, fullKeyName2);
					}
				}
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003EF58 File Offset: 0x0003D158
		public void LogDiffDetailsToEventLog()
		{
			this.Container.Analyze();
			DataStoreDiffReport report = this.Container.Report;
			DxStoreHACrimsonEvents.DataStoreDiffStats.Log<bool, int, int, int, int, int, int, int, int, int, int, int, int, int, int>(report.IsEverythingMatches, report.TotalKeysCount, report.TotalPropertiesCount, report.TotalClusdbKeysCount, report.TotalClusdbPropertiesCount, report.TotalDxStoreKeysCount, report.TotalDxStorePropertiesCount, report.CountKeysOnlyInClusdb, report.CountKeysOnlyInDxStore, report.CountKeysInClusdbAndDxStoreAndPropertiesMatch, report.CountKeysInClusdbAndDxStoreButPropertiesDifferent, report.CountPropertiesOnlyInClusdb, report.CountPropertiesOnlyInDxStore, report.CountPropertiesSameInClusdbAndDxStore, report.CountPropertiesDifferentInClusdbAndDxStore);
			string verboseReport = report.VerboseReport;
			int length = verboseReport.Length;
			int num = RegistryParameters.DistributedStoreDiffVerboseReportMaxCharsPerLine;
			if (num == 0)
			{
				num = 500;
			}
			int num2 = (int)Math.Ceiling((double)length / (double)num);
			int num3 = 1;
			int i = 0;
			while (i < length)
			{
				string text = verboseReport.Substring(i, Math.Min(num, length - i));
				DxStoreHACrimsonEvents.DataStoreDiffVerboseReport.Log<int, int, string>(num3, num2, text);
				i += num;
				num3++;
			}
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0003F048 File Offset: 0x0003D248
		public void CopyClusdbSnapshotToDxStore()
		{
			InstanceSnapshotInfo instanceSnapshotInfo = new InstanceSnapshotInfo();
			instanceSnapshotInfo.FullKeyName = "Public";
			instanceSnapshotInfo.Snapshot = this.ClusdbSnapshot.ToString();
			instanceSnapshotInfo.Compress();
			using (InstanceClientFactory defaultGroupInstanceClientFactory = this.GetDefaultGroupInstanceClientFactory())
			{
				DxStoreInstanceClient localClient = defaultGroupInstanceClientFactory.LocalClient;
				localClient.ApplySnapshot(instanceSnapshotInfo, false, null);
			}
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0003F0BC File Offset: 0x0003D2BC
		private void MeasureDuration(string tag, Action action)
		{
			Stopwatch stopwatch = new Stopwatch();
			try
			{
				stopwatch.Start();
				action();
			}
			finally
			{
				TimeSpan elapsed = stopwatch.Elapsed;
				this.TimingInfo[tag] = (int)elapsed.TotalMilliseconds;
			}
		}
	}
}
