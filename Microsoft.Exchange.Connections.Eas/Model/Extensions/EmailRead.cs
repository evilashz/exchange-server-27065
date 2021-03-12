using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Connections.Eas.Model.Extensions
{
	// Token: 0x0200008C RID: 140
	public enum EmailRead : byte
	{
		// Token: 0x04000446 RID: 1094
		[XmlEnum("0")]
		Unread,
		// Token: 0x04000447 RID: 1095
		[XmlEnum("1")]
		Read
	}
}
