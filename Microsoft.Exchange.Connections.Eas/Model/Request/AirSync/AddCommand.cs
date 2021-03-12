using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSync
{
	// Token: 0x02000090 RID: 144
	[XmlType(Namespace = "AirSync", TypeName = "Add")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class AddCommand : Command
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00009732 File Offset: 0x00007932
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000973A File Offset: 0x0000793A
		[XmlElement(ElementName = "Class")]
		public string Class { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00009743 File Offset: 0x00007943
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000974B File Offset: 0x0000794B
		[XmlElement(ElementName = "ClientId")]
		public string ClientId { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00009754 File Offset: 0x00007954
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000975C File Offset: 0x0000795C
		[XmlElement(ElementName = "ApplicationData")]
		public ApplicationData ApplicationData { get; set; }
	}
}
