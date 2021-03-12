using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000C8 RID: 200
	[KnownType(typeof(CaaRegion))]
	[XmlType("Region")]
	[DataContract(Name = "Region")]
	public class CaaRegion
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000C05B File Offset: 0x0000A25B
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0000C063 File Offset: 0x0000A263
		[XmlAttribute("name")]
		[DataMember(Name = "name", EmitDefaultValue = true)]
		public string Name { get; set; }
	}
}
