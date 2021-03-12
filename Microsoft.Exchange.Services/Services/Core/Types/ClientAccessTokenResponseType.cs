using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005AC RID: 1452
	[XmlType(TypeName = "Token", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Token")]
	[Serializable]
	public class ClientAccessTokenResponseType
	{
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x000AEC78 File Offset: 0x000ACE78
		// (set) Token: 0x06002AE2 RID: 10978 RVA: 0x000AEC80 File Offset: 0x000ACE80
		[DataMember(Order = 1)]
		public string Id { get; set; }

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x000AEC89 File Offset: 0x000ACE89
		// (set) Token: 0x06002AE4 RID: 10980 RVA: 0x000AEC91 File Offset: 0x000ACE91
		[IgnoreDataMember]
		[XmlElement]
		public ClientAccessTokenType TokenType { get; set; }

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x000AEC9A File Offset: 0x000ACE9A
		// (set) Token: 0x06002AE6 RID: 10982 RVA: 0x000AECA7 File Offset: 0x000ACEA7
		[DataMember(Name = "TokenType", IsRequired = true, Order = 2)]
		[XmlIgnore]
		public string TokenTypeString
		{
			get
			{
				return EnumUtilities.ToString<ClientAccessTokenType>(this.TokenType);
			}
			set
			{
				this.TokenType = EnumUtilities.Parse<ClientAccessTokenType>(value);
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000AECB5 File Offset: 0x000ACEB5
		// (set) Token: 0x06002AE8 RID: 10984 RVA: 0x000AECBD File Offset: 0x000ACEBD
		[DataMember(Order = 3)]
		public string TokenValue { get; set; }

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x000AECC6 File Offset: 0x000ACEC6
		// (set) Token: 0x06002AEA RID: 10986 RVA: 0x000AECCE File Offset: 0x000ACECE
		[DataMember(Order = 4)]
		public int TTL { get; set; }
	}
}
