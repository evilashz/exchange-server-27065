using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.ComposeMail
{
	// Token: 0x02000062 RID: 98
	[XmlType(Namespace = "ComposeMail", TypeName = "SendMail")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class SendMail
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00005485 File Offset: 0x00003685
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000548D File Offset: 0x0000368D
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }
	}
}
