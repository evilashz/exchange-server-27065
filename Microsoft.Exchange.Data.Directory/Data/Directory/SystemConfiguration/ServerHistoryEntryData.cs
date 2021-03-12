using System;
using System.Text;
using System.Xml;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200062B RID: 1579
	internal sealed class ServerHistoryEntryData
	{
		// Token: 0x06004A8E RID: 19086 RVA: 0x0011355F File Offset: 0x0011175F
		public ServerHistoryEntryData(ServerHistoryEntry entry)
		{
			this.FromBinary(entry.Data);
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x00113573 File Offset: 0x00111773
		public ServerHistoryEntryData(string serverName, DateTime activeTimestamp, DateTime passiveTimestamp, string passiveReason)
		{
			this.ServerName = serverName;
			this.ActiveTimestamp = activeTimestamp;
			this.PassiveTimestamp = passiveTimestamp;
			this.PassiveReason = passiveReason;
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x06004A90 RID: 19088 RVA: 0x00113598 File Offset: 0x00111798
		// (set) Token: 0x06004A91 RID: 19089 RVA: 0x001135A0 File Offset: 0x001117A0
		public string ServerName { get; set; }

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x06004A92 RID: 19090 RVA: 0x001135A9 File Offset: 0x001117A9
		// (set) Token: 0x06004A93 RID: 19091 RVA: 0x001135B1 File Offset: 0x001117B1
		public DateTime ActiveTimestamp { get; set; }

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x06004A94 RID: 19092 RVA: 0x001135BA File Offset: 0x001117BA
		// (set) Token: 0x06004A95 RID: 19093 RVA: 0x001135C2 File Offset: 0x001117C2
		public DateTime PassiveTimestamp { get; set; }

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x06004A96 RID: 19094 RVA: 0x001135CB File Offset: 0x001117CB
		// (set) Token: 0x06004A97 RID: 19095 RVA: 0x001135D3 File Offset: 0x001117D3
		public string PassiveReason { get; set; }

		// Token: 0x06004A98 RID: 19096 RVA: 0x001135DC File Offset: 0x001117DC
		public override string ToString()
		{
			return string.Format("ServerName: {0}, ActiveTimestamp: {1}, PassiveTimestamp: {2}, PassiveReason: {3}", new object[]
			{
				this.ServerName,
				this.ActiveTimestamp,
				this.PassiveTimestamp,
				this.PassiveReason
			});
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x0011362C File Offset: 0x0011182C
		public byte[] ToBinary()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("ServerHistoryEntryData");
			xmlDocument.AppendChild(xmlElement);
			XmlElement xmlElement2 = xmlDocument.CreateElement("ServerName");
			xmlElement2.InnerText = this.ServerName;
			XmlElement xmlElement3 = xmlDocument.CreateElement("ActiveTimestamp");
			xmlElement3.InnerText = this.ActiveTimestamp.ToString();
			XmlElement xmlElement4 = xmlDocument.CreateElement("PassiveTimestamp");
			xmlElement4.InnerText = this.PassiveTimestamp.ToString();
			XmlElement xmlElement5 = xmlDocument.CreateElement("PassiveReason");
			xmlElement5.InnerText = this.PassiveReason;
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			xmlElement.AppendChild(xmlElement4);
			xmlElement.AppendChild(xmlElement5);
			return new UTF8Encoding().GetBytes(xmlDocument.OuterXml);
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x00113708 File Offset: 0x00111908
		public void FromBinary(byte[] data)
		{
			string innerText;
			DateTime activeTimestamp;
			DateTime passiveTimestamp;
			string innerText2;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(new UTF8Encoding().GetString(data));
				string str = "/ServerHistoryEntryData/";
				innerText = xmlDocument.DocumentElement.SelectSingleNode(str + "ServerName").InnerText;
				activeTimestamp = DateTime.Parse(xmlDocument.DocumentElement.SelectSingleNode(str + "ActiveTimestamp").InnerText);
				passiveTimestamp = DateTime.Parse(xmlDocument.DocumentElement.SelectSingleNode(str + "PassiveTimestamp").InnerText);
				innerText2 = xmlDocument.DocumentElement.SelectSingleNode(str + "PassiveReason").InnerText;
			}
			catch (Exception)
			{
				return;
			}
			this.ServerName = innerText;
			this.ActiveTimestamp = activeTimestamp;
			this.PassiveTimestamp = passiveTimestamp;
			this.PassiveReason = innerText2;
		}

		// Token: 0x0400338E RID: 13198
		private const string ServerHistoryEntryDataElement = "ServerHistoryEntryData";

		// Token: 0x0400338F RID: 13199
		private const string ServerNameElement = "ServerName";

		// Token: 0x04003390 RID: 13200
		private const string ActiveTimestampElement = "ActiveTimestamp";

		// Token: 0x04003391 RID: 13201
		private const string PassiveTimestampElement = "PassiveTimestamp";

		// Token: 0x04003392 RID: 13202
		private const string PassiveReasonElement = "PassiveReason";
	}
}
