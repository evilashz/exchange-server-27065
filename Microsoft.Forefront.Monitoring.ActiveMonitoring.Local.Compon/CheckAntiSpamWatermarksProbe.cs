using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Forefront.AntiSpam.Common;
using Microsoft.Forefront.AntiSpam.RulesPublisher;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200001C RID: 28
	public class CheckAntiSpamWatermarksProbe : ProbeWorkItem
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00007B60 File Offset: 0x00005D60
		protected override void DoWork(CancellationToken cancellationToken)
		{
			IEnumerable<string> allowedRegionsToExecute = this.GetAllowedRegionsToExecute(base.Definition.ExtensionAttributes);
			if (this.IsCurrentRegionAllowed(allowedRegionsToExecute))
			{
				this.ValidateSyncWatermarks(cancellationToken);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00007BA8 File Offset: 0x00005DA8
		private bool IsCurrentRegionAllowed(IEnumerable<string> allowedRegions)
		{
			string machineRegion = DatacenterRegistry.GetForefrontRegion();
			return allowedRegions == null || !allowedRegions.Any<string>() || string.IsNullOrWhiteSpace(machineRegion) || allowedRegions.Any((string region) => string.Equals(region, machineRegion, StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007BF4 File Offset: 0x00005DF4
		private IEnumerable<string> GetAllowedRegionsToExecute(string extensionAttributes)
		{
			IEnumerable<string> result = Enumerable.Empty<string>();
			if (!string.IsNullOrWhiteSpace(extensionAttributes))
			{
				try
				{
					XAttribute xattribute = XDocument.Parse(extensionAttributes).Root.Attribute("RegionsToExecute");
					if (xattribute != null)
					{
						result = xattribute.Value.Split(new char[]
						{
							','
						}, StringSplitOptions.RemoveEmptyEntries);
					}
				}
				catch (XmlException)
				{
					WTFDiagnostics.TraceDebug(ExTraceGlobals.BackgroundTracer, base.TraceContext, "Extension Attribute cannot be parsed.", null, "GetAllowedRegionsToExecute", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Background\\Probes\\CheckAntiSpamWatermarksProbe.cs", 98);
				}
			}
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007C80 File Offset: 0x00005E80
		private void ValidateSyncWatermarks(CancellationToken cancellationToken)
		{
			RulesPublisherConfiguration.Load();
			SyncWatermarkCommon syncWatermarkCommon = new SyncWatermarkCommon("SpamRulesPublisher");
			foreach (string text in RulesPublisherConfiguration.Instance.ContinuousUpdateWatermarks)
			{
				syncWatermarkCommon.ValidateWatermarkChangedTime(text, RulesPublisherConfiguration.Instance.ContinuousUpdateWatermarksTimePeriod);
			}
			foreach (string str in RulesPublisherConfiguration.Instance.FastUpdateWatermarks)
			{
				syncWatermarkCommon.ValidateWatermarkChangedTime("Microsoft.Forefront.AntiSpam.DataMigration.ForwardSync.SyncData." + str, RulesPublisherConfiguration.Instance.FastUpdateWatermarksTimePeriod);
			}
			foreach (string str2 in RulesPublisherConfiguration.Instance.SlowUpdateWatermarks)
			{
				syncWatermarkCommon.ValidateWatermarkChangedTime("Microsoft.Forefront.AntiSpam.DataMigration.ForwardSync.SyncData." + str2, RulesPublisherConfiguration.Instance.SlowUpdateWatermarksTimePeriod);
			}
			syncWatermarkCommon.ValidateIPListTimeStampInSpamDB(RulesPublisherConfiguration.Instance.IPListTypesForUpdateCheck, RulesPublisherConfiguration.Instance.FastUpdateWatermarksTimePeriod);
		}
	}
}
