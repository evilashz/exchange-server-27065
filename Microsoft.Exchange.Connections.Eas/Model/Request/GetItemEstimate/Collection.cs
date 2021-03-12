using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSync;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.GetItemEstimate
{
	// Token: 0x020000A0 RID: 160
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "GetItemEstimate", TypeName = "Collection")]
	public class Collection
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000A45B File Offset: 0x0000865B
		public Collection()
		{
			this.Options = new List<Options>();
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000A46E File Offset: 0x0000866E
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000A476 File Offset: 0x00008676
		[XmlElement(ElementName = "SyncKey", Namespace = "AirSync")]
		public string SyncKey { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000A47F File Offset: 0x0000867F
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000A487 File Offset: 0x00008687
		[XmlElement(ElementName = "CollectionId")]
		public string CollectionId { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000A490 File Offset: 0x00008690
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000A498 File Offset: 0x00008698
		[XmlElement(ElementName = "ConversationMode")]
		public object ConversationMode { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000A4A1 File Offset: 0x000086A1
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0000A4A9 File Offset: 0x000086A9
		[XmlElement(ElementName = "Options", Namespace = "AirSync")]
		public List<Options> Options { get; set; }
	}
}
