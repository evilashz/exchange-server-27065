using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005BF RID: 1471
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ResponseTypeType
	{
		// Token: 0x04001A92 RID: 6802
		Unknown,
		// Token: 0x04001A93 RID: 6803
		Organizer,
		// Token: 0x04001A94 RID: 6804
		Tentative,
		// Token: 0x04001A95 RID: 6805
		Accept,
		// Token: 0x04001A96 RID: 6806
		Decline,
		// Token: 0x04001A97 RID: 6807
		NoResponseReceived
	}
}
