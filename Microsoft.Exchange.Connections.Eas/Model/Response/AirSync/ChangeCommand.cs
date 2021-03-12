using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSync
{
	// Token: 0x020000AB RID: 171
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSync", TypeName = "ChangeCommand")]
	public class ChangeCommand : Command
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000AD01 File Offset: 0x00008F01
		[XmlElement(ElementName = "Class")]
		public string Class { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000AD0A File Offset: 0x00008F0A
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000AD12 File Offset: 0x00008F12
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000AD1B File Offset: 0x00008F1B
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000AD23 File Offset: 0x00008F23
		[XmlElement(ElementName = "ApplicationData")]
		public ApplicationData ApplicationData { get; set; }
	}
}
