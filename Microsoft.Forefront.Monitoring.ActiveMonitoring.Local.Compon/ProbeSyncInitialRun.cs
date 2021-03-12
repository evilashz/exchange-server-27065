using System;
using System.Collections.Concurrent;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200004C RID: 76
	internal class ProbeSyncInitialRun
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		public ProbeSyncInitialRun(ProbeWorkItem currentProbe)
		{
			this.currentProbe = currentProbe;
			this.ParseWorkContext(currentProbe.Definition.ExtensionAttributes);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		public string ProducerProbeName { get; private set; }

		// Token: 0x060001E7 RID: 487 RVA: 0x0000C4ED File Offset: 0x0000A6ED
		public void MarkCompleted()
		{
			if (this.role == ProbeSyncInitialRun.SyncRole.Producer)
			{
				ProbeSyncInitialRun.completedProbes.TryAdd(this.ProducerProbeName, true);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000C50A File Offset: 0x0000A70A
		public bool CanRun()
		{
			return this.role != ProbeSyncInitialRun.SyncRole.Consumer || ProbeSyncInitialRun.completedProbes.ContainsKey(this.ProducerProbeName);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000C528 File Offset: 0x0000A728
		private void ParseWorkContext(string workContextXml)
		{
			if (string.IsNullOrWhiteSpace(workContextXml))
			{
				throw new ArgumentException("Work Definition XML is null", "workContextXml");
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(workContextXml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("//WorkContext/ProbeSyncInitialRun");
			if (xmlNode != null)
			{
				Utils.CheckXmlElement(xmlNode, "ProbeSyncInitialRun");
				this.role = Utils.GetMandatoryXmlEnumAttribute<ProbeSyncInitialRun.SyncRole>(xmlNode, "Role");
				if (this.role == ProbeSyncInitialRun.SyncRole.Consumer)
				{
					this.ProducerProbeName = Utils.GetMandatoryXmlAttribute<string>(xmlNode, "ProducerProbeName");
					if (string.IsNullOrWhiteSpace(this.ProducerProbeName))
					{
						throw new ArgumentNullException("ProbeSyncInitialRun/ProducerName");
					}
					if (this.ProducerProbeName == this.currentProbe.Definition.Name)
					{
						throw new ArgumentException(string.Format("Producer and consumer cannot be a same probe, current probe: {0}", this.currentProbe.Definition.Name));
					}
				}
				else if (this.role == ProbeSyncInitialRun.SyncRole.Producer)
				{
					this.ProducerProbeName = this.currentProbe.Definition.Name;
					return;
				}
			}
			else
			{
				this.role = ProbeSyncInitialRun.SyncRole.None;
			}
		}

		// Token: 0x04000142 RID: 322
		private static ConcurrentDictionary<string, bool> completedProbes = new ConcurrentDictionary<string, bool>();

		// Token: 0x04000143 RID: 323
		private ProbeSyncInitialRun.SyncRole role;

		// Token: 0x04000144 RID: 324
		private ProbeWorkItem currentProbe;

		// Token: 0x0200004D RID: 77
		private enum SyncRole
		{
			// Token: 0x04000147 RID: 327
			None,
			// Token: 0x04000148 RID: 328
			Producer,
			// Token: 0x04000149 RID: 329
			Consumer
		}
	}
}
