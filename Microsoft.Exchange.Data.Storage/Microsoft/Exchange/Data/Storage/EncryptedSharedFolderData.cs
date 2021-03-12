using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D8F RID: 3471
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public sealed class EncryptedSharedFolderData
	{
		// Token: 0x17001FF0 RID: 8176
		// (get) Token: 0x0600777A RID: 30586 RVA: 0x0020EC69 File Offset: 0x0020CE69
		// (set) Token: 0x0600777B RID: 30587 RVA: 0x0020EC71 File Offset: 0x0020CE71
		[XmlElement]
		public EncryptedDataContainer Token { get; set; }

		// Token: 0x17001FF1 RID: 8177
		// (get) Token: 0x0600777C RID: 30588 RVA: 0x0020EC7A File Offset: 0x0020CE7A
		// (set) Token: 0x0600777D RID: 30589 RVA: 0x0020EC82 File Offset: 0x0020CE82
		[XmlElement]
		public EncryptedDataContainer Data { get; set; }
	}
}
