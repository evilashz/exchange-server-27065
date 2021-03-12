using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000843 RID: 2115
	[XmlType(TypeName = "PeopleConnectionTokenType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "PeopleConnectionToken", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public sealed class PeopleConnectionToken
	{
		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x000D6FBE File Offset: 0x000D51BE
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x000D6FC6 File Offset: 0x000D51C6
		[DataMember]
		[XmlElement]
		public string AccessToken { get; set; }

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x000D6FCF File Offset: 0x000D51CF
		// (set) Token: 0x06003CFB RID: 15611 RVA: 0x000D6FD7 File Offset: 0x000D51D7
		[XmlElement]
		[DataMember]
		public string ApplicationId { get; set; }

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x000D6FE0 File Offset: 0x000D51E0
		// (set) Token: 0x06003CFD RID: 15613 RVA: 0x000D6FE8 File Offset: 0x000D51E8
		[DataMember]
		[XmlElement]
		public string ApplicationSecret { get; set; }
	}
}
