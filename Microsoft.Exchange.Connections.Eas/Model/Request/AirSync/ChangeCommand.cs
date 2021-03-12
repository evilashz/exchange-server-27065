using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSync
{
	// Token: 0x02000093 RID: 147
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSync", TypeName = "Change")]
	public class ChangeCommand : Command
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00009AE3 File Offset: 0x00007CE3
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00009AEB File Offset: 0x00007CEB
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00009AF4 File Offset: 0x00007CF4
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00009AFC File Offset: 0x00007CFC
		[XmlElement(ElementName = "ApplicationData")]
		public ApplicationData ApplicationData { get; set; }
	}
}
