using System;
using System.Xml.Linq;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000007 RID: 7
	internal class DatabaseHealthBreadcrumb
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x000044CD File Offset: 0x000026CD
		internal DatabaseHealthBreadcrumb()
		{
			this.RecordCreation = ExDateTime.UtcNow;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000044E0 File Offset: 0x000026E0
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000044E8 File Offset: 0x000026E8
		internal ExDateTime RecordCreation { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000044F1 File Offset: 0x000026F1
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000044F9 File Offset: 0x000026F9
		internal int DatabaseHealth { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004502 File Offset: 0x00002702
		// (set) Token: 0x060000AD RID: 173 RVA: 0x0000450A File Offset: 0x0000270A
		internal Guid DatabaseGuid { get; set; }

		// Token: 0x060000AE RID: 174 RVA: 0x00004513 File Offset: 0x00002713
		public override string ToString()
		{
			return string.Format("Created: {0}, Database: {1}, DatabaseHealth: {2}", this.RecordCreation, this.DatabaseGuid, this.DatabaseHealth);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004540 File Offset: 0x00002740
		public XElement GetDiagnosticInfo()
		{
			return new XElement("HealthEntry", new object[]
			{
				new XElement("creationTimestamp", this.RecordCreation),
				new XElement("DatabaseGuid", this.DatabaseGuid),
				new XElement("DatabaseHealth", this.DatabaseHealth)
			});
		}
	}
}
