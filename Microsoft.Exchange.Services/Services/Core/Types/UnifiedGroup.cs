using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000682 RID: 1666
	[XmlType(TypeName = "UnifiedGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "UnifiedGroup", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class UnifiedGroup
	{
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x000B841F File Offset: 0x000B661F
		// (set) Token: 0x06003314 RID: 13076 RVA: 0x000B8427 File Offset: 0x000B6627
		[DataMember(Name = "SmtpAddress", EmitDefaultValue = false)]
		[XmlElement("SmtpAddress", typeof(string))]
		public string SmtpAddress { get; set; }

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000B8430 File Offset: 0x000B6630
		// (set) Token: 0x06003316 RID: 13078 RVA: 0x000B8438 File Offset: 0x000B6638
		[XmlElement("LegacyDN", typeof(string))]
		[DataMember(Name = "LegacyDN", EmitDefaultValue = false)]
		public string LegacyDN { get; set; }

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000B8441 File Offset: 0x000B6641
		// (set) Token: 0x06003318 RID: 13080 RVA: 0x000B8449 File Offset: 0x000B6649
		[DataMember(Name = "DisplayName", EmitDefaultValue = false)]
		[XmlElement("DisplayName", typeof(string))]
		public string DisplayName { get; set; }

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x000B8452 File Offset: 0x000B6652
		// (set) Token: 0x0600331A RID: 13082 RVA: 0x000B845A File Offset: 0x000B665A
		[DataMember(Name = "IsFavorite", EmitDefaultValue = false)]
		[XmlElement("IsFavorite", typeof(bool))]
		public bool IsFavorite { get; set; }

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000B8463 File Offset: 0x000B6663
		// (set) Token: 0x0600331C RID: 13084 RVA: 0x000B846B File Offset: 0x000B666B
		[DataMember(Name = "AccessType", EmitDefaultValue = false)]
		[XmlElement("AccessType", typeof(UnifiedGroupAccessType))]
		public UnifiedGroupAccessType AccessType { get; set; }
	}
}
