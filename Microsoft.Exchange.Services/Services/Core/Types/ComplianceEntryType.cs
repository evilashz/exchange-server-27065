using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004B5 RID: 1205
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ComplianceEntryType
	{
		// Token: 0x060023E1 RID: 9185 RVA: 0x000A4511 File Offset: 0x000A2711
		public ComplianceEntryType(string id, string name, string description)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x000A452E File Offset: 0x000A272E
		// (set) Token: 0x060023E3 RID: 9187 RVA: 0x000A4536 File Offset: 0x000A2736
		[DataMember]
		[XmlElement("Id", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string Id { get; set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x000A453F File Offset: 0x000A273F
		// (set) Token: 0x060023E5 RID: 9189 RVA: 0x000A4547 File Offset: 0x000A2747
		[DataMember]
		[XmlElement("Name", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string Name { get; set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x000A4550 File Offset: 0x000A2750
		// (set) Token: 0x060023E7 RID: 9191 RVA: 0x000A4558 File Offset: 0x000A2758
		[XmlElement("Description", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember]
		public string Description { get; set; }
	}
}
