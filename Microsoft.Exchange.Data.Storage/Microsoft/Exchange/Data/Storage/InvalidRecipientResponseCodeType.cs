using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D9C RID: 3484
	[XmlType(TypeName = "InvalidRecipientResponseCodeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum InvalidRecipientResponseCodeType
	{
		// Token: 0x040052F7 RID: 21239
		OtherError,
		// Token: 0x040052F8 RID: 21240
		RecipientOrganizationNotFederated,
		// Token: 0x040052F9 RID: 21241
		CannotObtainTokenFromSTS,
		// Token: 0x040052FA RID: 21242
		SystemPolicyBlocksSharingWithThisRecipient,
		// Token: 0x040052FB RID: 21243
		RecipientOrganizationFederatedWithUnknownTokenIssuer
	}
}
