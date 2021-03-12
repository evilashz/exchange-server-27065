using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200089C RID: 2204
	[XmlType(TypeName = "SubscriptionStatusType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SubscriptionStatus
	{
		// Token: 0x04002414 RID: 9236
		Invalid = -1,
		// Token: 0x04002415 RID: 9237
		[XmlEnum("OK")]
		OK,
		// Token: 0x04002416 RID: 9238
		[XmlEnum("Unsubscribe")]
		Unsubscribe
	}
}
