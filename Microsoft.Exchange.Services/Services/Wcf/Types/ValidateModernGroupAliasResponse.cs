using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B40 RID: 2880
	[DataContract(Name = "ValidateModernGroupAliasResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ValidateModernGroupAliasResponse : BaseJsonResponse
	{
		// Token: 0x06005196 RID: 20886 RVA: 0x0010A974 File Offset: 0x00108B74
		internal ValidateModernGroupAliasResponse(string alias, bool isAliasUnique)
		{
			this.Alias = alias;
			this.IsAliasUnique = isAliasUnique;
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06005197 RID: 20887 RVA: 0x0010A98A File Offset: 0x00108B8A
		// (set) Token: 0x06005198 RID: 20888 RVA: 0x0010A992 File Offset: 0x00108B92
		[DataMember(Name = "IsAliasUnique", IsRequired = false)]
		public bool IsAliasUnique { get; set; }

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06005199 RID: 20889 RVA: 0x0010A99B File Offset: 0x00108B9B
		// (set) Token: 0x0600519A RID: 20890 RVA: 0x0010A9A3 File Offset: 0x00108BA3
		[DataMember(Name = "Alias", IsRequired = false)]
		public string Alias { get; set; }
	}
}
