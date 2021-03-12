using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Autodiscover
{
	// Token: 0x020000BA RID: 186
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(TypeName = "Action")]
	public class Action
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0000B159 File Offset: 0x00009359
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0000B161 File Offset: 0x00009361
		[XmlElement(ElementName = "Settings")]
		public Settings Settings { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0000B16A File Offset: 0x0000936A
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0000B172 File Offset: 0x00009372
		[XmlElement(ElementName = "Error")]
		public ActionError Error { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000B17B File Offset: 0x0000937B
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0000B183 File Offset: 0x00009383
		[XmlElement(ElementName = "Redirect")]
		public string Redirect { get; set; }
	}
}
