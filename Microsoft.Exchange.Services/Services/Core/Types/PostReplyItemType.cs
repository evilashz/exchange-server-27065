using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200065C RID: 1628
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "PostReplyItem", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class PostReplyItemType : PostReplyItemBaseType
	{
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x000B77C1 File Offset: 0x000B59C1
		// (set) Token: 0x06003216 RID: 12822 RVA: 0x000B77C9 File Offset: 0x000B59C9
		[DataMember(EmitDefaultValue = false)]
		public BodyContentType NewBodyContent { get; set; }
	}
}
