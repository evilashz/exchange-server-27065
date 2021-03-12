using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000721 RID: 1825
	[XmlType(TypeName = "ClientExtensionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ClientExtension
	{
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06003754 RID: 14164 RVA: 0x000C56DF File Offset: 0x000C38DF
		// (set) Token: 0x06003755 RID: 14165 RVA: 0x000C56E7 File Offset: 0x000C38E7
		[XmlAttribute]
		public bool IsAvailable { get; set; }

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06003756 RID: 14166 RVA: 0x000C56F0 File Offset: 0x000C38F0
		// (set) Token: 0x06003757 RID: 14167 RVA: 0x000C56F8 File Offset: 0x000C38F8
		[XmlAttribute]
		public bool IsMandatory { get; set; }

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06003758 RID: 14168 RVA: 0x000C5701 File Offset: 0x000C3901
		// (set) Token: 0x06003759 RID: 14169 RVA: 0x000C5709 File Offset: 0x000C3909
		[XmlAttribute]
		public bool IsEnabledByDefault { get; set; }

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x000C5712 File Offset: 0x000C3912
		// (set) Token: 0x0600375B RID: 14171 RVA: 0x000C571A File Offset: 0x000C391A
		[XmlAttribute]
		public ClientExtensionProvidedTo ProvidedTo { get; set; }

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x0600375C RID: 14172 RVA: 0x000C5723 File Offset: 0x000C3923
		// (set) Token: 0x0600375D RID: 14173 RVA: 0x000C572B File Offset: 0x000C392B
		[XmlAttribute]
		public ExtensionType Type { get; set; }

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x000C5734 File Offset: 0x000C3934
		// (set) Token: 0x0600375F RID: 14175 RVA: 0x000C573C File Offset: 0x000C393C
		[XmlAttribute]
		public ExtensionInstallScope Scope { get; set; }

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000C5745 File Offset: 0x000C3945
		// (set) Token: 0x06003761 RID: 14177 RVA: 0x000C574D File Offset: 0x000C394D
		[XmlAttribute]
		public string MarketplaceAssetId { get; set; }

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06003762 RID: 14178 RVA: 0x000C5756 File Offset: 0x000C3956
		// (set) Token: 0x06003763 RID: 14179 RVA: 0x000C575E File Offset: 0x000C395E
		[XmlAttribute]
		public string MarketplaceContentMarket { get; set; }

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x000C5767 File Offset: 0x000C3967
		// (set) Token: 0x06003765 RID: 14181 RVA: 0x000C576F File Offset: 0x000C396F
		[XmlAttribute]
		public string AppStatus { get; set; }

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x000C5778 File Offset: 0x000C3978
		// (set) Token: 0x06003767 RID: 14183 RVA: 0x000C5780 File Offset: 0x000C3980
		[XmlAttribute]
		public string Etoken { get; set; }

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06003768 RID: 14184 RVA: 0x000C5789 File Offset: 0x000C3989
		// (set) Token: 0x06003769 RID: 14185 RVA: 0x000C5791 File Offset: 0x000C3991
		[XmlArrayItem("String", IsNullable = false)]
		public string[] SpecificUsers { get; set; }

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x000C579A File Offset: 0x000C399A
		// (set) Token: 0x0600376B RID: 14187 RVA: 0x000C57A2 File Offset: 0x000C39A2
		[XmlElement]
		public string Manifest { get; set; }
	}
}
