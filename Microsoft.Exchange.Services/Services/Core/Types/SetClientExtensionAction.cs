using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200088B RID: 2187
	[XmlType(TypeName = "SetClientExtensionActionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SetClientExtensionAction
	{
		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x000D9225 File Offset: 0x000D7425
		// (set) Token: 0x06003EA9 RID: 16041 RVA: 0x000D922D File Offset: 0x000D742D
		[XmlAttribute]
		public SetClientExtensionActionId ActionId { get; set; }

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06003EAA RID: 16042 RVA: 0x000D9236 File Offset: 0x000D7436
		// (set) Token: 0x06003EAB RID: 16043 RVA: 0x000D923E File Offset: 0x000D743E
		[XmlAttribute]
		public string ExtensionId { get; set; }

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06003EAC RID: 16044 RVA: 0x000D9247 File Offset: 0x000D7447
		// (set) Token: 0x06003EAD RID: 16045 RVA: 0x000D924F File Offset: 0x000D744F
		[XmlElement]
		public ClientExtension ClientExtension { get; set; }
	}
}
