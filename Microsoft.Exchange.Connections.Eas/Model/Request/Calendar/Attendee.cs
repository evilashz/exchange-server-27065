using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.Calendar
{
	// Token: 0x0200009D RID: 157
	[XmlType(Namespace = "Calendar", TypeName = "Attendee")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Attendee
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000A0D4 File Offset: 0x000082D4
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000A0DC File Offset: 0x000082DC
		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000A0E5 File Offset: 0x000082E5
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000A0ED File Offset: 0x000082ED
		[XmlElement(ElementName = "Email")]
		public string Email { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000A0F6 File Offset: 0x000082F6
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000A0FE File Offset: 0x000082FE
		[XmlElement(ElementName = "AttendeeStatus")]
		public byte? AttendeeStatus { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000A107 File Offset: 0x00008307
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000A10F File Offset: 0x0000830F
		[XmlElement(ElementName = "AttendeeType")]
		public byte? AttendeeType { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000A118 File Offset: 0x00008318
		[XmlIgnore]
		public bool AttendeeStatusSpecified
		{
			get
			{
				return this.AttendeeStatus != null;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000A134 File Offset: 0x00008334
		[XmlIgnore]
		public bool AttendeeTypeSpecified
		{
			get
			{
				return this.AttendeeType != null;
			}
		}
	}
}
