using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005F3 RID: 1523
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class WellKnownResponseObjectType : ResponseObjectType
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x000B3B53 File Offset: 0x000B1D53
		// (set) Token: 0x06002EF1 RID: 12017 RVA: 0x000B3B5B File Offset: 0x000B1D5B
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlIgnore]
		public ItemInformationType AdditionalInfo { get; set; }
	}
}
