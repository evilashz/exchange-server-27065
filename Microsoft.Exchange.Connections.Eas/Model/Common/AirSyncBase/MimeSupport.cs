using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Connections.Eas.Model.Common.AirSyncBase
{
	// Token: 0x02000086 RID: 134
	public enum MimeSupport
	{
		// Token: 0x04000429 RID: 1065
		[XmlEnum("0")]
		NeverSendMime,
		// Token: 0x0400042A RID: 1066
		[XmlEnum("1")]
		SendMimeForSMime,
		// Token: 0x0400042B RID: 1067
		[XmlEnum("2")]
		SendMimeForAll
	}
}
