using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003E9 RID: 1001
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "FolderIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class FolderId : BaseFolderId
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x0009DCB6 File Offset: 0x0009BEB6
		// (set) Token: 0x06001C0F RID: 7183 RVA: 0x0009DCBE File Offset: 0x0009BEBE
		[XmlAttribute]
		[DataMember(IsRequired = true, Order = 1)]
		public string Id { get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x0009DCC7 File Offset: 0x0009BEC7
		// (set) Token: 0x06001C11 RID: 7185 RVA: 0x0009DCCF File Offset: 0x0009BECF
		[XmlAttribute]
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string ChangeKey { get; set; }

		// Token: 0x06001C12 RID: 7186 RVA: 0x0009DCD8 File Offset: 0x0009BED8
		internal bool IsPublicFolderId()
		{
			return ServiceIdConverter.IsPublicFolder(this.GetId());
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0009DCE5 File Offset: 0x0009BEE5
		public FolderId()
		{
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0009DCED File Offset: 0x0009BEED
		public FolderId(string id, string changeKey)
		{
			this.Id = id;
			this.ChangeKey = changeKey;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0009DD03 File Offset: 0x0009BF03
		public override string GetId()
		{
			return this.Id;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x0009DD0B File Offset: 0x0009BF0B
		public override string GetChangeKey()
		{
			return this.ChangeKey;
		}
	}
}
