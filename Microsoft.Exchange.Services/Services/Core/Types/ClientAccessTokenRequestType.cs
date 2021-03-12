using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000429 RID: 1065
	[DataContract(Name = "TokenRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ClientAccessTokenRequestType
	{
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x000A0A57 File Offset: 0x0009EC57
		// (set) Token: 0x06001F36 RID: 7990 RVA: 0x000A0A5F File Offset: 0x0009EC5F
		[DataMember]
		public string Id { get; set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x000A0A68 File Offset: 0x0009EC68
		// (set) Token: 0x06001F38 RID: 7992 RVA: 0x000A0A70 File Offset: 0x0009EC70
		[XmlElement]
		[IgnoreDataMember]
		public ClientAccessTokenType TokenType { get; set; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x000A0A79 File Offset: 0x0009EC79
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x000A0A86 File Offset: 0x0009EC86
		[DataMember(Name = "TokenType", IsRequired = true)]
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

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x000A0A94 File Offset: 0x0009EC94
		// (set) Token: 0x06001F3C RID: 7996 RVA: 0x000A0A9C File Offset: 0x0009EC9C
		[DataMember]
		[XmlElement]
		public string Scope { get; set; }
	}
}
