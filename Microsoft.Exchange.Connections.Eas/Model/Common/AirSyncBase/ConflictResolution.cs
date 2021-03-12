using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Connections.Eas.Model.Common.AirSyncBase
{
	// Token: 0x02000085 RID: 133
	public enum ConflictResolution
	{
		// Token: 0x04000426 RID: 1062
		[XmlEnum("0")]
		KeepClientVersion,
		// Token: 0x04000427 RID: 1063
		[XmlEnum("1")]
		KeepServerVersion
	}
}
