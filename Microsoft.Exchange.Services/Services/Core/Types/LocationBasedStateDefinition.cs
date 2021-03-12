using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007E6 RID: 2022
	[XmlType(TypeName = "LocationBasedStateDefinitionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public sealed class LocationBasedStateDefinition : BaseCalendarItemStateDefinition
	{
		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x000D0060 File Offset: 0x000CE260
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x000D0068 File Offset: 0x000CE268
		[XmlElement]
		[DataMember(IsRequired = true, Order = 1)]
		public string OrganizerLocation { get; set; }

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06003B61 RID: 15201 RVA: 0x000D0071 File Offset: 0x000CE271
		// (set) Token: 0x06003B62 RID: 15202 RVA: 0x000D0079 File Offset: 0x000CE279
		[DataMember(IsRequired = true, Order = 2)]
		[XmlElement]
		public string AttendeeLocation { get; set; }
	}
}
