using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D90 RID: 3472
	[Serializable]
	public sealed class EncryptedDataContainer
	{
		// Token: 0x17001FF2 RID: 8178
		// (get) Token: 0x0600777F RID: 30591 RVA: 0x0020EC93 File Offset: 0x0020CE93
		// (set) Token: 0x06007780 RID: 30592 RVA: 0x0020EC9B File Offset: 0x0020CE9B
		[XmlAnyElement]
		public XmlElement EncryptedData { get; set; }
	}
}
