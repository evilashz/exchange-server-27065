using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005C0 RID: 1472
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum RequestType
	{
		// Token: 0x04001A99 RID: 6809
		None,
		// Token: 0x04001A9A RID: 6810
		NewMeetingRequest,
		// Token: 0x04001A9B RID: 6811
		FullUpdate = 65536,
		// Token: 0x04001A9C RID: 6812
		InformationalUpdate = 131072,
		// Token: 0x04001A9D RID: 6813
		SilentUpdate = 262144,
		// Token: 0x04001A9E RID: 6814
		Outdated = 524288,
		// Token: 0x04001A9F RID: 6815
		PrincipalWantsCopy = 1048576
	}
}
