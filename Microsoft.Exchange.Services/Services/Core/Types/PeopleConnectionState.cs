using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000842 RID: 2114
	[XmlType(TypeName = "PeopleConnectionStateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PeopleConnectionState
	{
		// Token: 0x04002198 RID: 8600
		Disconnected,
		// Token: 0x04002199 RID: 8601
		Connected,
		// Token: 0x0400219A RID: 8602
		ConnectedNeedsToken
	}
}
