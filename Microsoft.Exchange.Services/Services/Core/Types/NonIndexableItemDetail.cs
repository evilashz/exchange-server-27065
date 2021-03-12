using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000823 RID: 2083
	[DataContract(Name = "NonIndexableItemDetail", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "NonIndexableItemDetailType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class NonIndexableItemDetail
	{
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000D5B2D File Offset: 0x000D3D2D
		// (set) Token: 0x06003C48 RID: 15432 RVA: 0x000D5B35 File Offset: 0x000D3D35
		[XmlElement("ItemId")]
		[DataMember(Name = "ItemId", IsRequired = true)]
		public ItemId ItemId { get; set; }

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x000D5B3E File Offset: 0x000D3D3E
		// (set) Token: 0x06003C4A RID: 15434 RVA: 0x000D5B46 File Offset: 0x000D3D46
		[XmlElement("ErrorCode")]
		[DataMember(Name = "ErrorCode", IsRequired = true)]
		public ItemIndexError ErrorCode { get; set; }

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000D5B4F File Offset: 0x000D3D4F
		// (set) Token: 0x06003C4C RID: 15436 RVA: 0x000D5B57 File Offset: 0x000D3D57
		[XmlElement("ErrorDescription")]
		[DataMember(Name = "ErrorDescription", IsRequired = true)]
		public string ErrorDescription { get; set; }

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x000D5B60 File Offset: 0x000D3D60
		// (set) Token: 0x06003C4E RID: 15438 RVA: 0x000D5B68 File Offset: 0x000D3D68
		[DataMember(Name = "IsPartiallyIndexed", IsRequired = true)]
		[XmlElement("IsPartiallyIndexed")]
		public bool IsPartiallyIndexed { get; set; }

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000D5B71 File Offset: 0x000D3D71
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x000D5B79 File Offset: 0x000D3D79
		[DataMember(Name = "IsPermanentFailure", IsRequired = true)]
		[XmlElement("IsPermanentFailure")]
		public bool IsPermanentFailure { get; set; }

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000D5B82 File Offset: 0x000D3D82
		// (set) Token: 0x06003C52 RID: 15442 RVA: 0x000D5B8A File Offset: 0x000D3D8A
		[DataMember(Name = "SortValue", IsRequired = true)]
		[XmlElement("SortValue")]
		public string SortValue { get; set; }

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000D5B93 File Offset: 0x000D3D93
		// (set) Token: 0x06003C54 RID: 15444 RVA: 0x000D5B9B File Offset: 0x000D3D9B
		[XmlElement("AttemptCount")]
		[DataMember(Name = "AttemptCount", IsRequired = true)]
		public int AttemptCount { get; set; }

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x000D5BA4 File Offset: 0x000D3DA4
		[XmlIgnore]
		public bool LastAttemptTimeSpecified
		{
			get
			{
				return this.LastAttemptTime != null;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06003C56 RID: 15446 RVA: 0x000D5BBF File Offset: 0x000D3DBF
		// (set) Token: 0x06003C57 RID: 15447 RVA: 0x000D5BC7 File Offset: 0x000D3DC7
		[XmlElement("LastAttemptTime")]
		[DataMember(Name = "LastAttemptTime", IsRequired = false)]
		public DateTime? LastAttemptTime { get; set; }

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x000D5BD0 File Offset: 0x000D3DD0
		// (set) Token: 0x06003C59 RID: 15449 RVA: 0x000D5BD8 File Offset: 0x000D3DD8
		[DataMember(Name = "AdditionalInfo", IsRequired = false)]
		[XmlElement("AdditionalInfo")]
		public string AdditionalInfo { get; set; }
	}
}
