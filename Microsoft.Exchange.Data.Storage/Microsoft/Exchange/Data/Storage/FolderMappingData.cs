using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E32 RID: 3634
	public class FolderMappingData
	{
		// Token: 0x170021A5 RID: 8613
		// (get) Token: 0x06007DBA RID: 32186 RVA: 0x0022A2E1 File Offset: 0x002284E1
		// (set) Token: 0x06007DBB RID: 32187 RVA: 0x0022A2E9 File Offset: 0x002284E9
		public string ShortId { get; set; }

		// Token: 0x170021A6 RID: 8614
		// (get) Token: 0x06007DBC RID: 32188 RVA: 0x0022A2F2 File Offset: 0x002284F2
		// (set) Token: 0x06007DBD RID: 32189 RVA: 0x0022A2FA File Offset: 0x002284FA
		public string LongId { get; set; }

		// Token: 0x170021A7 RID: 8615
		// (get) Token: 0x06007DBE RID: 32190 RVA: 0x0022A303 File Offset: 0x00228503
		// (set) Token: 0x06007DBF RID: 32191 RVA: 0x0022A30B File Offset: 0x0022850B
		[XmlAttribute]
		public string Name { get; set; }

		// Token: 0x170021A8 RID: 8616
		// (get) Token: 0x06007DC0 RID: 32192 RVA: 0x0022A314 File Offset: 0x00228514
		// (set) Token: 0x06007DC1 RID: 32193 RVA: 0x0022A31C File Offset: 0x0022851C
		[XmlAttribute("FolderType")]
		public string DefaultFolderType { get; set; }

		// Token: 0x170021A9 RID: 8617
		// (get) Token: 0x06007DC2 RID: 32194 RVA: 0x0022A325 File Offset: 0x00228525
		// (set) Token: 0x06007DC3 RID: 32195 RVA: 0x0022A32D File Offset: 0x0022852D
		public string Exception { get; set; }
	}
}
