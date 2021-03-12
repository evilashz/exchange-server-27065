using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002EE RID: 750
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum PermissionLevelType
	{
		// Token: 0x040012B2 RID: 4786
		None,
		// Token: 0x040012B3 RID: 4787
		Owner,
		// Token: 0x040012B4 RID: 4788
		PublishingEditor,
		// Token: 0x040012B5 RID: 4789
		Editor,
		// Token: 0x040012B6 RID: 4790
		PublishingAuthor,
		// Token: 0x040012B7 RID: 4791
		Author,
		// Token: 0x040012B8 RID: 4792
		NoneditingAuthor,
		// Token: 0x040012B9 RID: 4793
		Reviewer,
		// Token: 0x040012BA RID: 4794
		Contributor,
		// Token: 0x040012BB RID: 4795
		Custom
	}
}
