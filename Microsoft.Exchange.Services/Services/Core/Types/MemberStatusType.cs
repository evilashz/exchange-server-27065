using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005C9 RID: 1481
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum MemberStatusType
	{
		// Token: 0x04001AD3 RID: 6867
		Unrecognized,
		// Token: 0x04001AD4 RID: 6868
		Normal,
		// Token: 0x04001AD5 RID: 6869
		Demoted
	}
}
