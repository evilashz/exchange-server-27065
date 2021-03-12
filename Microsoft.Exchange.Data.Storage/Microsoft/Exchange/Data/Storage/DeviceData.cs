using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E30 RID: 3632
	public class DeviceData
	{
		// Token: 0x1700219A RID: 8602
		// (get) Token: 0x06007DA2 RID: 32162 RVA: 0x0022A216 File Offset: 0x00228416
		// (set) Token: 0x06007DA3 RID: 32163 RVA: 0x0022A21E File Offset: 0x0022841E
		[XmlIgnore]
		internal StoreObjectId FolderId { get; set; }

		// Token: 0x1700219B RID: 8603
		// (get) Token: 0x06007DA4 RID: 32164 RVA: 0x0022A227 File Offset: 0x00228427
		// (set) Token: 0x06007DA5 RID: 32165 RVA: 0x0022A22F File Offset: 0x0022842F
		[XmlAttribute]
		public string Name { get; set; }

		// Token: 0x1700219C RID: 8604
		// (get) Token: 0x06007DA6 RID: 32166 RVA: 0x0022A238 File Offset: 0x00228438
		// (set) Token: 0x06007DA7 RID: 32167 RVA: 0x0022A240 File Offset: 0x00228440
		[XmlAttribute]
		public DateTime Created { get; set; }

		// Token: 0x1700219D RID: 8605
		// (get) Token: 0x06007DA8 RID: 32168 RVA: 0x0022A249 File Offset: 0x00228449
		// (set) Token: 0x06007DA9 RID: 32169 RVA: 0x0022A251 File Offset: 0x00228451
		public List<SyncStateFolderData> SyncFolders { get; set; }

		// Token: 0x1700219E RID: 8606
		// (get) Token: 0x06007DAA RID: 32170 RVA: 0x0022A25A File Offset: 0x0022845A
		// (set) Token: 0x06007DAB RID: 32171 RVA: 0x0022A262 File Offset: 0x00228462
		[XmlIgnore]
		public bool ShouldAdd { get; set; }
	}
}
