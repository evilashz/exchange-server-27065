using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Autodiscover
{
	// Token: 0x020000BB RID: 187
	[XmlType(TypeName = "ActionError")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class ActionError
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000B194 File Offset: 0x00009394
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0000B19C File Offset: 0x0000939C
		[XmlElement(ElementName = "Status")]
		public string Status { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000B1A5 File Offset: 0x000093A5
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0000B1AD File Offset: 0x000093AD
		[XmlElement(ElementName = "Message")]
		public string Message { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000B1B6 File Offset: 0x000093B6
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0000B1BE File Offset: 0x000093BE
		[XmlElement(ElementName = "DebugData")]
		public string DebugData { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000B1C7 File Offset: 0x000093C7
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000B1CF File Offset: 0x000093CF
		[XmlElement(ElementName = "ErrorCode")]
		public int ErrorCode { get; set; }
	}
}
