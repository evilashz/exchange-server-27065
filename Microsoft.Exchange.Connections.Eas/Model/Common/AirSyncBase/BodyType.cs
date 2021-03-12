using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Connections.Eas.Model.Common.AirSyncBase
{
	// Token: 0x02000084 RID: 132
	public enum BodyType
	{
		// Token: 0x04000420 RID: 1056
		[XmlEnum("0")]
		NoType,
		// Token: 0x04000421 RID: 1057
		[XmlEnum("1")]
		PlainText,
		// Token: 0x04000422 RID: 1058
		[XmlEnum("2")]
		HTML,
		// Token: 0x04000423 RID: 1059
		[XmlEnum("3")]
		RTF,
		// Token: 0x04000424 RID: 1060
		[XmlEnum("4")]
		MIME
	}
}
