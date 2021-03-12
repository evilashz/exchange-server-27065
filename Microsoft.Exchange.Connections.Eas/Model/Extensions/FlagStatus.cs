using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Connections.Eas.Model.Extensions
{
	// Token: 0x0200008D RID: 141
	public enum FlagStatus
	{
		// Token: 0x04000449 RID: 1097
		[XmlEnum("0")]
		NotFlagged,
		// Token: 0x0400044A RID: 1098
		[XmlEnum("1")]
		Complete,
		// Token: 0x0400044B RID: 1099
		[XmlEnum("2")]
		Flagged
	}
}
