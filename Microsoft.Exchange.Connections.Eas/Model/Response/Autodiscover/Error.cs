using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Autodiscover
{
	// Token: 0x020000BC RID: 188
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(TypeName = "Error")]
	public class Error
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000B1E0 File Offset: 0x000093E0
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x0000B1E8 File Offset: 0x000093E8
		[XmlAttribute(AttributeName = "Id")]
		public string Id { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000B1F1 File Offset: 0x000093F1
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x0000B1F9 File Offset: 0x000093F9
		[XmlAttribute(AttributeName = "Time")]
		public string Time { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000B202 File Offset: 0x00009402
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0000B20A File Offset: 0x0000940A
		[XmlElement(ElementName = "ErrorCode")]
		public int ErrorCode { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000B213 File Offset: 0x00009413
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0000B21B File Offset: 0x0000941B
		[XmlElement(ElementName = "Message")]
		public string Message { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000B224 File Offset: 0x00009424
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x0000B22C File Offset: 0x0000942C
		[XmlElement(ElementName = "DebugData")]
		public string DebugData { get; set; }
	}
}
