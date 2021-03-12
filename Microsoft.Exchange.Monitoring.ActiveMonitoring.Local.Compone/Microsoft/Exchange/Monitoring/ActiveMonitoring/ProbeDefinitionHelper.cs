using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x0200007B RID: 123
	internal abstract class ProbeDefinitionHelper : DefinitionHelperBase
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001B08F File Offset: 0x0001928F
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0001B097 File Offset: 0x00019297
		internal string Account { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001B0A0 File Offset: 0x000192A0
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0001B0A8 File Offset: 0x000192A8
		internal string AccountPassword { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001B0B1 File Offset: 0x000192B1
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0001B0B9 File Offset: 0x000192B9
		internal string AccountDisplayName { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001B0C2 File Offset: 0x000192C2
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0001B0CA File Offset: 0x000192CA
		internal string Endpoint { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001B0D3 File Offset: 0x000192D3
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0001B0DB File Offset: 0x000192DB
		internal string SecondaryAccount { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001B0E4 File Offset: 0x000192E4
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0001B0EC File Offset: 0x000192EC
		internal string SecondaryAccountPassword { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001B0F5 File Offset: 0x000192F5
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0001B0FD File Offset: 0x000192FD
		internal string SecondaryAccountDisplayName { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0001B106 File Offset: 0x00019306
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x0001B10E File Offset: 0x0001930E
		internal string SecondaryEndpoint { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001B117 File Offset: 0x00019317
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0001B11F File Offset: 0x0001931F
		internal int Version { get; set; }

		// Token: 0x06000491 RID: 1169 RVA: 0x0001B128 File Offset: 0x00019328
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			stringBuilder.AppendLine("Account: " + this.Account);
			stringBuilder.AppendLine("AccountPassword: " + this.AccountPassword);
			stringBuilder.AppendLine("AccountDisplayName: " + this.AccountDisplayName);
			stringBuilder.AppendLine("Endpoint: " + this.Endpoint);
			stringBuilder.AppendLine("SecondaryAccount: " + this.SecondaryAccount);
			stringBuilder.AppendLine("SecondaryAccountPassword: " + this.SecondaryAccountPassword);
			stringBuilder.AppendLine("SecondaryAccountDisplayName: " + this.SecondaryAccountDisplayName);
			stringBuilder.AppendLine("SecondaryEndpoint: " + this.SecondaryEndpoint);
			return stringBuilder.ToString();
		}

		// Token: 0x06000492 RID: 1170
		internal abstract List<ProbeDefinition> CreateDefinition();

		// Token: 0x06000493 RID: 1171 RVA: 0x0001B208 File Offset: 0x00019408
		internal override void ReadDiscoveryXml()
		{
			base.ReadDiscoveryXml();
			this.Account = base.GetOptionalXmlAttribute<string>("Account", string.Empty);
			this.AccountPassword = base.GetOptionalXmlAttribute<string>("AccountPassword", string.Empty);
			this.AccountDisplayName = base.GetOptionalXmlAttribute<string>("AccountDisplayName", string.Empty);
			this.Endpoint = base.GetOptionalXmlAttribute<string>("Endpoint", string.Empty);
			this.SecondaryAccount = base.GetOptionalXmlAttribute<string>("SecondaryAccount", string.Empty);
			this.SecondaryAccountPassword = base.GetOptionalXmlAttribute<string>("SecondaryAccountPassword", string.Empty);
			this.SecondaryAccountDisplayName = base.GetOptionalXmlAttribute<string>("SecondaryAccountDisplayName", string.Empty);
			this.SecondaryEndpoint = base.GetOptionalXmlAttribute<string>("SecondaryEndpoint", string.Empty);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001B2CC File Offset: 0x000194CC
		internal override string ToString(WorkDefinition workItem)
		{
			ProbeDefinition probeDefinition = (ProbeDefinition)workItem;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString(workItem));
			stringBuilder.AppendLine("Account: " + probeDefinition.Account);
			stringBuilder.AppendLine("AccountPassword: " + probeDefinition.AccountPassword);
			stringBuilder.AppendLine("AccountDisplayName: " + probeDefinition.AccountDisplayName);
			stringBuilder.AppendLine("Endpoint: " + probeDefinition.Endpoint);
			stringBuilder.AppendLine("SecondaryAccount: " + probeDefinition.SecondaryAccount);
			stringBuilder.AppendLine("SecondaryAccountPassword: " + probeDefinition.SecondaryAccountPassword);
			stringBuilder.AppendLine("SecondaryAccountDisplayName: " + probeDefinition.SecondaryAccountDisplayName);
			stringBuilder.AppendLine("SecondaryEndpoint: " + probeDefinition.SecondaryEndpoint);
			return stringBuilder.ToString();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001B3B4 File Offset: 0x000195B4
		protected ProbeDefinition CreateProbeDefinition()
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			base.CreateBaseWorkDefinition(probeDefinition);
			probeDefinition.Account = this.Account;
			probeDefinition.AccountPassword = this.AccountPassword;
			probeDefinition.AccountDisplayName = this.AccountDisplayName;
			probeDefinition.Endpoint = this.Endpoint;
			probeDefinition.SecondaryAccount = this.SecondaryAccount;
			probeDefinition.SecondaryAccountPassword = this.SecondaryAccountPassword;
			probeDefinition.SecondaryAccountDisplayName = this.SecondaryAccountDisplayName;
			probeDefinition.SecondaryEndpoint = this.SecondaryEndpoint;
			return probeDefinition;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001B430 File Offset: 0x00019630
		protected ProbeDefinition CreateProbeDefinition(XmlNode extensionNode)
		{
			ProbeDefinition probeDefinition = this.CreateProbeDefinition();
			if (extensionNode != null)
			{
				probeDefinition.ExtensionAttributes = extensionNode.OuterXml;
				probeDefinition.ParseExtensionAttributes(false);
			}
			return probeDefinition;
		}
	}
}
