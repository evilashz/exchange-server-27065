using System;
using System.IO;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Probes
{
	// Token: 0x02000260 RID: 608
	public class DiagnosticsAggregationLocalSnapshotProbe : ProbeWorkItem
	{
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0003BDC0 File Offset: 0x00039FC0
		private static ConfigurationLoader<TransportServerConfiguration, TransportServerConfiguration.Builder> LocalServer
		{
			get
			{
				if (DiagnosticsAggregationLocalSnapshotProbe.localServer == null)
				{
					lock (DiagnosticsAggregationLocalSnapshotProbe.locker)
					{
						if (DiagnosticsAggregationLocalSnapshotProbe.localServer == null)
						{
							ConfigurationLoader<TransportServerConfiguration, TransportServerConfiguration.Builder> configurationLoader = new ConfigurationLoader<TransportServerConfiguration, TransportServerConfiguration.Builder>(TimeSpan.Zero);
							configurationLoader.Load();
							DiagnosticsAggregationLocalSnapshotProbe.localServer = configurationLoader;
						}
					}
				}
				return DiagnosticsAggregationLocalSnapshotProbe.localServer;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0003BE24 File Offset: 0x0003A024
		private string QueueLogPath
		{
			get
			{
				if (!(DiagnosticsAggregationLocalSnapshotProbe.LocalServer.Cache.TransportServer.QueueLogPath == null))
				{
					return DiagnosticsAggregationLocalSnapshotProbe.LocalServer.Cache.TransportServer.QueueLogPath.PathName;
				}
				return null;
			}
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0003BE60 File Offset: 0x0003A060
		protected override void DoWork(CancellationToken cancellationToken)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(base.Definition.ExtensionAttributes);
			TimeSpan t = TimeSpan.FromMinutes(30.0);
			XmlAttribute xmlAttribute = (XmlAttribute)xmlDocument.SelectSingleNode("//ExtensionAttributes/@MaximumFileAgeMinutes");
			int num;
			if (xmlAttribute != null && int.TryParse(xmlAttribute.Value, out num))
			{
				t = TimeSpan.FromMinutes((double)num);
			}
			base.Result.StateAttribute3 = t.ToString("c");
			if (!string.IsNullOrEmpty(this.QueueLogPath))
			{
				string text = Path.Combine(this.QueueLogPath, "QueueSnapShot.xml");
				base.Result.StateAttribute1 = text;
				TimeSpan t2 = DateTime.UtcNow - File.GetLastWriteTimeUtc(text);
				base.Result.StateAttribute2 = File.GetLastWriteTimeUtc(text).ToString("U");
				base.Result.StateAttribute4 = t2.ToString("c");
				if (t2 > t)
				{
					throw new ApplicationException("File age exceeds maximum");
				}
			}
		}

		// Token: 0x040009C5 RID: 2501
		private static object locker = new object();

		// Token: 0x040009C6 RID: 2502
		private static ConfigurationLoader<TransportServerConfiguration, TransportServerConfiguration.Builder> localServer;
	}
}
