using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000748 RID: 1864
	[XmlType(TypeName = "DeliverMeetingRequestsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DeliverMeetingRequestsType
	{
		// Token: 0x04001F11 RID: 7953
		None,
		// Token: 0x04001F12 RID: 7954
		DelegatesOnly,
		// Token: 0x04001F13 RID: 7955
		DelegatesAndMe,
		// Token: 0x04001F14 RID: 7956
		DelegatesAndSendInformationToMe,
		// Token: 0x04001F15 RID: 7957
		NoForward
	}
}
