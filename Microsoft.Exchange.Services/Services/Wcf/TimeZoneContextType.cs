using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DCB RID: 3531
	[DataContract(Name = "TimeZoneContext", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlRoot(ElementName = "TimeZoneContext", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TimeZoneContextType
	{
		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x060059E6 RID: 23014 RVA: 0x001187B9 File Offset: 0x001169B9
		// (set) Token: 0x060059E7 RID: 23015 RVA: 0x001187C1 File Offset: 0x001169C1
		[DataMember(Name = "TimeZoneDefinition", EmitDefaultValue = false, Order = 1)]
		[XmlElement]
		public TimeZoneDefinitionType TimeZoneDefinition { get; set; }
	}
}
