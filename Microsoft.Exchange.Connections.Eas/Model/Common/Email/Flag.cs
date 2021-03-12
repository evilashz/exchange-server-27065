using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Common.Email
{
	// Token: 0x0200008A RID: 138
	[XmlType(Namespace = "Email", TypeName = "Flag")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Flag
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000095AD File Offset: 0x000077AD
		// (set) Token: 0x06000298 RID: 664 RVA: 0x000095B5 File Offset: 0x000077B5
		[XmlElement(ElementName = "CompleteTime")]
		public string CompleteTime { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000095BE File Offset: 0x000077BE
		// (set) Token: 0x0600029A RID: 666 RVA: 0x000095C6 File Offset: 0x000077C6
		[XmlElement(ElementName = "FlagType")]
		public string FlagType { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000095CF File Offset: 0x000077CF
		// (set) Token: 0x0600029C RID: 668 RVA: 0x000095D7 File Offset: 0x000077D7
		[XmlElement(ElementName = "Status")]
		public int Status { get; set; }
	}
}
