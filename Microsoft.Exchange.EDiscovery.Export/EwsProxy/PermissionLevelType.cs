using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200020D RID: 525
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum PermissionLevelType
	{
		// Token: 0x04000E60 RID: 3680
		None,
		// Token: 0x04000E61 RID: 3681
		Owner,
		// Token: 0x04000E62 RID: 3682
		PublishingEditor,
		// Token: 0x04000E63 RID: 3683
		Editor,
		// Token: 0x04000E64 RID: 3684
		PublishingAuthor,
		// Token: 0x04000E65 RID: 3685
		Author,
		// Token: 0x04000E66 RID: 3686
		NoneditingAuthor,
		// Token: 0x04000E67 RID: 3687
		Reviewer,
		// Token: 0x04000E68 RID: 3688
		Contributor,
		// Token: 0x04000E69 RID: 3689
		Custom
	}
}
