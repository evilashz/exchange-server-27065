using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006BE RID: 1726
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "AttachmentId")]
	[XmlType(TypeName = "AttachmentIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class AttachmentIdType : BaseItemId
	{
		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x000BF82B File Offset: 0x000BDA2B
		// (set) Token: 0x06003518 RID: 13592 RVA: 0x000BF833 File Offset: 0x000BDA33
		[DataMember(IsRequired = true, Order = 1)]
		[XmlAttribute]
		public string Id { get; set; }

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000BF83C File Offset: 0x000BDA3C
		// (set) Token: 0x0600351A RID: 13594 RVA: 0x000BF844 File Offset: 0x000BDA44
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		[XmlAttribute]
		public string RootItemId { get; set; }

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000BF84D File Offset: 0x000BDA4D
		// (set) Token: 0x0600351C RID: 13596 RVA: 0x000BF855 File Offset: 0x000BDA55
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		[XmlAttribute]
		public string RootItemChangeKey { get; set; }

		// Token: 0x0600351D RID: 13597 RVA: 0x000BF85E File Offset: 0x000BDA5E
		public AttachmentIdType()
		{
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000BF866 File Offset: 0x000BDA66
		public AttachmentIdType(string id)
		{
			this.Id = id;
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000BF875 File Offset: 0x000BDA75
		public override string GetId()
		{
			return this.Id;
		}
	}
}
