using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000095 RID: 149
	[KnownType(typeof(AlternateMailboxCollectionSetting))]
	[KnownType(typeof(WebClientUrlCollectionSetting))]
	[KnownType(typeof(StringSetting))]
	[KnownType(typeof(ProtocolConnectionCollectionSetting))]
	[KnownType(typeof(DocumentSharingLocationCollectionSetting))]
	[DataContract(Name = "UserSetting", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public abstract class UserSetting
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0001790A File Offset: 0x00015B0A
		public UserSetting()
		{
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00017912 File Offset: 0x00015B12
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0001791A File Offset: 0x00015B1A
		[DataMember(IsRequired = true)]
		public string Name { get; set; }
	}
}
