using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002AC RID: 684
	public class PiiLocString
	{
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0004E411 File Offset: 0x0004C611
		// (set) Token: 0x060018AD RID: 6317 RVA: 0x0004E419 File Offset: 0x0004C619
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0004E422 File Offset: 0x0004C622
		// (set) Token: 0x060018AF RID: 6319 RVA: 0x0004E42A File Offset: 0x0004C62A
		[XmlAttribute(AttributeName = "piiParams")]
		public int[] Parameters { get; set; }
	}
}
