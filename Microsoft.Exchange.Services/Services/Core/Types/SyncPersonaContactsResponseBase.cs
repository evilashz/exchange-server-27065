using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000563 RID: 1379
	[DataContract]
	public class SyncPersonaContactsResponseBase : ResponseMessage
	{
		// Token: 0x0600268D RID: 9869 RVA: 0x000A6771 File Offset: 0x000A4971
		public SyncPersonaContactsResponseBase()
		{
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000A6779 File Offset: 0x000A4979
		internal SyncPersonaContactsResponseBase(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x000A6783 File Offset: 0x000A4983
		// (set) Token: 0x06002690 RID: 9872 RVA: 0x000A678B File Offset: 0x000A498B
		[DataMember(Name = "SyncState", IsRequired = true)]
		[XmlElement("SyncState", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string SyncState { get; set; }

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x000A6794 File Offset: 0x000A4994
		// (set) Token: 0x06002692 RID: 9874 RVA: 0x000A679C File Offset: 0x000A499C
		[XmlElement("IncludesLastItemInRange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "IncludesLastItemInRange", IsRequired = true, EmitDefaultValue = true)]
		public bool IncludesLastItemInRange { get; set; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x000A67A5 File Offset: 0x000A49A5
		// (set) Token: 0x06002694 RID: 9876 RVA: 0x000A67AD File Offset: 0x000A49AD
		[XmlArray(ElementName = "DeletedPeople")]
		[XmlArrayItem(ElementName = "PersonaId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(EmitDefaultValue = false)]
		public ItemId[] DeletedPeople { get; set; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x000A67B6 File Offset: 0x000A49B6
		// (set) Token: 0x06002696 RID: 9878 RVA: 0x000A67BE File Offset: 0x000A49BE
		[DataMember(EmitDefaultValue = false)]
		[XmlArray(ElementName = "JumpHeaderSortKeys")]
		[XmlArrayItem(ElementName = "JumpHeaderSortKeys", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public string[] JumpHeaderSortKeys { get; set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x000A67C7 File Offset: 0x000A49C7
		// (set) Token: 0x06002698 RID: 9880 RVA: 0x000A67CF File Offset: 0x000A49CF
		[XmlElement("SortKeyVersion", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "SortKeyVersion", IsRequired = true)]
		public string SortKeyVersion { get; set; }
	}
}
