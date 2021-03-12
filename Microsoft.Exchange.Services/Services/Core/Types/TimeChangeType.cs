using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200067F RID: 1663
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TimeChangeType
	{
		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003308 RID: 13064 RVA: 0x000B83C2 File Offset: 0x000B65C2
		// (set) Token: 0x06003309 RID: 13065 RVA: 0x000B83CA File Offset: 0x000B65CA
		[XmlElement]
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 1)]
		public string Offset { get; set; }

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x000B83D3 File Offset: 0x000B65D3
		// (set) Token: 0x0600330B RID: 13067 RVA: 0x000B83DB File Offset: 0x000B65DB
		[XmlElement]
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 2)]
		public string AbsoluteDate { get; set; }

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x000B83E4 File Offset: 0x000B65E4
		// (set) Token: 0x0600330D RID: 13069 RVA: 0x000B83EC File Offset: 0x000B65EC
		[XmlElement]
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 3)]
		public RelativeYearlyRecurrencePatternType RelativeYearlyRecurrence { get; set; }

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x000B83F5 File Offset: 0x000B65F5
		// (set) Token: 0x0600330F RID: 13071 RVA: 0x000B83FD File Offset: 0x000B65FD
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 4)]
		[XmlElement]
		public string Time { get; set; }

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000B8406 File Offset: 0x000B6606
		// (set) Token: 0x06003311 RID: 13073 RVA: 0x000B840E File Offset: 0x000B660E
		[XmlAttribute]
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public string TimeZoneName { get; set; }
	}
}
