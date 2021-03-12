using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Calendar
{
	// Token: 0x020000C2 RID: 194
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "Calendar", TypeName = "Attendee")]
	public class Attendee
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000B318 File Offset: 0x00009518
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0000B320 File Offset: 0x00009520
		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000B329 File Offset: 0x00009529
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0000B331 File Offset: 0x00009531
		[XmlElement(ElementName = "Email")]
		public string Email { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000B33A File Offset: 0x0000953A
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0000B342 File Offset: 0x00009542
		[XmlElement(ElementName = "AttendeeStatus")]
		public int AttendeeStatus { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000B34B File Offset: 0x0000954B
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x0000B353 File Offset: 0x00009553
		[XmlElement(ElementName = "AttendeeType")]
		public int AttendeeType { get; set; }
	}
}
