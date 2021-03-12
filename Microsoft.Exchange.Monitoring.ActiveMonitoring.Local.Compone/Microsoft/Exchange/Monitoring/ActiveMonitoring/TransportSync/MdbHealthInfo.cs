using System;
using System.Text;
using System.Xml;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x02000117 RID: 279
	public class MdbHealthInfo
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x00031990 File Offset: 0x0002FB90
		internal MdbHealthInfo(XmlReader xmlReader)
		{
			if (xmlReader.Name != "Database")
			{
				throw new ArgumentException("Invalid Xml Node was passed in", "Database");
			}
			while (xmlReader.Read())
			{
				string name = xmlReader.Name;
				string a;
				if ((a = name) != null)
				{
					if (!(a == "databaseId"))
					{
						if (a == "enabled")
						{
							this.Enabled = bool.Parse(xmlReader.ReadString());
						}
					}
					else
					{
						this.MdbGuid = new Guid(xmlReader.ReadString());
					}
				}
				if (name == "Database")
				{
					return;
				}
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00031A27 File Offset: 0x0002FC27
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x00031A2F File Offset: 0x0002FC2F
		public Guid MdbGuid { get; private set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00031A38 File Offset: 0x0002FC38
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x00031A40 File Offset: 0x0002FC40
		public bool Enabled { get; private set; }

		// Token: 0x0600085D RID: 2141 RVA: 0x00031A4C File Offset: 0x0002FC4C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("MDBGuid={0} Enabled={1}", this.MdbGuid, this.Enabled);
			return stringBuilder.ToString();
		}
	}
}
